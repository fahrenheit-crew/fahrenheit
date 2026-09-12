// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

#include "fhstage0.h"

void dbg_loop(); // Forward declaration of debugger loop

int wmain(int argc, wchar_t* argv[ ]) {
    if (argc < 2) {
        std::wcerr << "Invalid call. You must specify an executable to launch.\n";
        std::wcerr << "Usage: fhstage0.exe {EXECUTABLE_TO_LAUNCH} {ARGS}\n";
        return 1;
    }

    LPCSTR              szDllPath  = "fhstage1.dll";
    PROCESS_INFORMATION pi;
    STARTUPINFO         si = { 0 };

    si.cb = sizeof(si);

    //
    // STEP 1:
    // Set up args as the game expects them to be.
    //

    std::wstring args;

    for (int i = 1; i < argc; i++) {
        args.append(argv[i]);
        args.append(L" ");
    }

    /* [fkelava 09/09/26 11:46]
     * Well-featured systems like Dalamud have an external crash handler that kicks in
     * when the target binary faults. We could borrow that design, but this is where
     * having a dedicated launcher/injector pays off; we can repurpose it as a debugger,
     * and make a crash report from Win32 debugger exception events.
     *
     * Passing `--debug` disengages this for when a regular debugger is in use.
     */

    bool  external_debug = wcsstr(args.c_str(), L"--debug") != NULL;
    DWORD creation_flags = external_debug
        ? CREATE_SUSPENDED
        : DEBUG_ONLY_THIS_PROCESS; // A debugged process is implicitly suspended until debug events are handled/pumped.

    //
    // STEP 2:
    // Create process in PROCESS_SUSPENDED state.
    //

    if (!CreateProcessW(
        NULL,
        &args[0],
        NULL,
        NULL,
        FALSE,
        creation_flags,
        NULL,
        NULL,
        &si,
        &pi
    )) {
        std::wcerr << "Failed to create target process.\n";
        return 1;
    }

    //
    // STEP 3:
    // Pause for debugger attach if `--debug` arg is passed.
    //

    if (external_debug) {
        std::wcout << "You can now attach a debugger; press any key to attempt launch.\n";
        int i = _getch();
    }

    //
    // STEP 4:
    // Patch IAT of suspended process to inject Stage 1 DLL at position 1.
    //

    if (!DetourUpdateProcessWithDll(pi.hProcess, &szDllPath, 1)) {
        TerminateProcess(pi.hProcess, ~0u);
        return FALSE;
    }

    std::wcout << "Stage 0 Loader complete. Moving to Stage 1.\n";

    //
    // STEP 5:
    // Stage 1 loads first, hooks program entrypoint and performs .NET hosting and
    // initialization, undoes IAT changes, pipes process stdout/stderr to Stage 0
    // console, then program execution proceeds.
    //

    if (external_debug) {
        ResumeThread       (pi.hThread);
        WaitForSingleObject(pi.hProcess, INFINITE);
    }
    else { dbg_loop(); }

    //
    // STEP 6:
    // Wait for program to (un)naturally terminate.
    //

    DWORD exitCode;
    BOOL  result = GetExitCodeProcess(pi.hProcess, &exitCode);

    CloseHandle(pi.hProcess);
    CloseHandle(pi.hThread);

    std::wcout << std::endl;

    if (exitCode != 0) {
        std::wcout << "Process exited with code " << std::hex << exitCode << std::endl;
        std::wcout << "If reporting an issue, please include any core dump (*.dmp) you see in the game directory.\n";
    }
    else {
        std::wcout << "Process ended by user.\n";
    }

    return exitCode;
}
