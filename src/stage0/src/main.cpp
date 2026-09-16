// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

/* [fkelava 12/09/26 23:26]
 * One of Fahrenheit's primary design tenets is that it should apply no permanent
 * modifications to the game binary or folder whatsoever. In keeping with that,
 * instead of modifying one of the DLLs the game imports as UnX and ffgriever EFL do,
 * it has an explicit launcher system- the Stage 0 and 1 loaders. If the game is not
 * launched using it, you get a pristine, unmodified original game.
 *
 * The method of choice applied here is reversible IAT patching using MS Detours.
 * Stage 0 creates the game process and rewrites the IAT to load Stage 1 first,
 * then serves as the standard output/error pipe for the game.
 * Stage 1 reverses that modification, then bootstraps .NET and Fahrenheit.
 *
 * Stage 0 also acts as a crash handler/debugger for the target binary. See `dbg.cpp`.
 */

#include "fhstage0.h"

void stage0_dbg_loop(); // Forward declaration of debugger loop function.

int wmain(
    int      argc,
    wchar_t* argv[ ]
) {
    if (argc < 2) {
        fwprintf_s(stderr, L"Invalid call. You must specify an executable to launch.\n");
        fwprintf_s(stderr, L"Usage: fhstage0.exe {EXECUTABLE_TO_LAUNCH} {ARGS}\n");
        return 1;
    }

    LPCSTR              path_dll = "fhstage1.dll";
    PROCESS_INFORMATION pi;
    STARTUPINFO         si = { 0 };

    si.cb = sizeof(si);

    wchar_t args[1024] = { 0 };

    // Set up args as the game expects them to be.
    for (int i = 1; i < argc; i++) {
        if (FAILED(StringCchCatW(args, 1024, argv[i])) ||
            FAILED(StringCchCatW(args, 1024, L" "))
        ) {
            fwprintf_s(stderr, L"[!] StringCchCatW() failed\n");
            return 1;
        }
    }

    bool  external_debug = wcsstr(args, L"--debug") != NULL;
    DWORD creation_flags = external_debug
        ? CREATE_SUSPENDED
        : DEBUG_ONLY_THIS_PROCESS; // A debugged process is implicitly suspended until debug events are handled/pumped.

    // Create target process in suspended or debugged state.
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
        fwprintf_s(stderr, L"Failed to create target process.\n");
        return 1;
    }

    // Pause for external debugger attach if `--debug` arg is passed.
    if (external_debug) {
        fwprintf_s(stdout, L"You can now attach a debugger; press any key to attempt launch.\n");
        int i = _getch();
    }

    // Patch IAT of suspended process to inject Stage 1 DLL at position 1.
    if (!DetourUpdateProcessWithDll(pi.hProcess, &path_dll, 1)) {
        TerminateProcess(pi.hProcess, ~0u);
        return FALSE;
    }

    fwprintf_s(stdout, L"Stage 0 Loader complete. Moving to Stage 1.\n");

    // Either wait for the process to exit if an external debugger is connected,
    // or begin pumping debug events with the Stage 0 stub debugger.
    if (external_debug) {
        ResumeThread       (pi.hThread);
        WaitForSingleObject(pi.hProcess, INFINITE);
    }
    else { stage0_dbg_loop(); }

    DWORD exit_code;
    BOOL  result = GetExitCodeProcess(pi.hProcess, &exit_code);

    CloseHandle(pi.hProcess);
    CloseHandle(pi.hThread);

    fwprintf_s(stdout, L"\n");

    if (exit_code != 0) {
        fwprintf_s(stdout, L"Process exited with code 0x%X.\n", exit_code);
        fwprintf_s(stdout, L"If reporting an issue, please include any core dump (*.dmp) you see in the game directory.\n");
    }
    else {
        fwprintf_s(stdout, L"Process ended by user.\n");
    }

    return exit_code;
}
