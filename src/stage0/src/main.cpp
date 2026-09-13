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
 * As Fahrenheit is a .NET modding system for native binaries, it follows that debugging
 * and stack walking must be carried out in "mixed" mode. Any errors that occur should
 * ideally include both managed and native frames for the developer's convenience.
 * Some systems, like Dalamud, implement this using a dedicated crash handler process.
 *
 * We go the other way around- Stage 0 is repurposed as a stub debugger that "handles"
 * exception events, triggers core dumping, and surfaces exception information to the end user.
 */

#include "fhstage0.h"

// Filters objects from a core dump being created.
static BOOL CALLBACK stage0_dbg_filter_dump(
          PVOID                     ptr_callback_param,
    const PMINIDUMP_CALLBACK_INPUT  ptr_callback_input,
          PMINIDUMP_CALLBACK_OUTPUT ptr_callback_output
) {
    if (!ptr_callback_input || !ptr_callback_output) return FALSE;

    switch (ptr_callback_input->CallbackType) {
        case CancelCallback:
            return FALSE;
    }

    return TRUE;
}

// Writes a core dump to disk.
static void stage0_dbg_create_dump(
    HANDLE            h_process,           // The handle to the process being dumped.
    DWORD             id_process,          // The ID of the process being dumped.
    DWORD             id_thread,           // The ID of the faulting thread in the process being dumped.
    EXCEPTION_RECORD* ptr_exception_record // A pointer to the record of the exception bringing the process down.
) {
    HANDLE dump_handle = CreateFileW(
        L"crash_dump.dmp",
        GENERIC_READ | GENERIC_WRITE,
        0,
        nullptr,
        CREATE_ALWAYS,
        FILE_ATTRIBUTE_NORMAL,
        nullptr);

    if (dump_handle == NULL || dump_handle == INVALID_HANDLE_VALUE) {
        std::wcerr << "Failed to open a file to write the core dump to." << std::endl;
        return;
    }

    MINIDUMP_TYPE dump_type = (MINIDUMP_TYPE)(
        MiniDumpNormal
      | MiniDumpWithDataSegs
      | MiniDumpWithHandleData
      | MiniDumpWithFullMemoryInfo
      | MiniDumpWithThreadInfo
      | MiniDumpWithProcessThreadData
      | MiniDumpWithUnloadedModules);

    /* [fkelava 11/06/26 21:24]
     * Here we have a problem. MiniDumpWriteDump expects, in MINIDUMP_EXCEPTION_INFORMATION, a PEXCEPTION_POINTERS
     * consisting of a CONTEXT and EXCEPTION_RECORD. But a debugger, in EXCEPTION_DEBUG_INFO, only gets an EXCEPTION_RECORD.
     *
     * Stage0 being a debugger, GetThreadContext solves that, but there's a catch. MINIDUMP_EXCEPTION_INFORMATION has a ClientPointers field:
     * > Determines where to get the memory regions pointed to by the ExceptionPointers member.
     * > Set to TRUE if the memory resides in the process being debugged (the target process of the debugger). Otherwise, set to FALSE {...}
     *
     * You'd think TRUE applies in this case. Not so: that results in the dump not having an exception record stored.
     * Because the context is created _here_, FALSE leads to it being properly found. But that, _too_, cannot be correct;
     * the exception record resides in the process being debugged, while the context resides in the debugger.
     *
     * What to do then? The docs do not say, and no example is readily found. We use FALSE as the lesser evil.
     *
     * See:
     * - https://learn.microsoft.com/en-us/windows/win32/api/minwinbase/ns-minwinbase-exception_debug_info
     * - https://learn.microsoft.com/en-us/windows/win32/api/winnt/ns-winnt-exception_pointers
     * - https://learn.microsoft.com/en-us/windows/win32/api/minidumpapiset/ns-minidumpapiset-minidump_exception_information
     */

    CONTEXT faulting_thread_context = { 0 };
    faulting_thread_context.ContextFlags = CONTEXT_ALL;

    HANDLE faulting_thread_handle = OpenThread(
        THREAD_GET_CONTEXT,
        FALSE,
        id_thread
    );

    if (faulting_thread_handle == nullptr || faulting_thread_handle == INVALID_HANDLE_VALUE) {
        std::wcerr << "Failed to open the faulting thread for context capture." << std::endl;
        return;
    }

    if (!GetThreadContext(faulting_thread_handle, &faulting_thread_context)) {
        std::wcerr << "Failed to capture the faulting thread's context." << std::endl;
        return;
    }

    EXCEPTION_POINTERS exception_pointers = { 0 };
    exception_pointers.ContextRecord   = &faulting_thread_context;
    exception_pointers.ExceptionRecord = ptr_exception_record;

    MINIDUMP_EXCEPTION_INFORMATION info_dump_exception = { 0 };
    info_dump_exception.ThreadId          = id_thread;
    info_dump_exception.ExceptionPointers = &exception_pointers;
    info_dump_exception.ClientPointers    = FALSE;

    MINIDUMP_CALLBACK_INFORMATION info_dump_callback = { 0 };
    info_dump_callback.CallbackRoutine = (MINIDUMP_CALLBACK_ROUTINE)stage0_dbg_filter_dump;
    info_dump_callback.CallbackParam   = nullptr;

    std::wcerr << "Dumping process core. Please wait." << std::endl;

    if (!MiniDumpWriteDump(
        h_process,
        id_process,
        dump_handle,
        dump_type,
        &info_dump_exception,
        nullptr,
        &info_dump_callback
    )) {
        std::wcerr << "Failed to capture a core dump." << std::endl;
    }

    CloseHandle(dump_handle);
}

// Handles exception events, returning whether to continue or treat the exception as unhandled.
static DWORD stage0_dbg_exception(
    HANDLE                h_process,         // The handle to the process that encountered an exception.
    DWORD                 id_process,        // The ID of the process that encountered an exception.
    DWORD                 id_thread,         // The ID of the faulting thread in the process that encountered an exception.
    EXCEPTION_DEBUG_INFO* ptr_info_exception // A pointer to information about the exception.
) {
    /* [fkelava 12/09/26 23:50]
     * https://learn.microsoft.com/en-us/windows/win32/api/minwinbase/ns-minwinbase-exception_debug_info#members
     * > If this member is zero, the debugger has previously encountered the exception.
     *
     * We only "handle" exceptions (i.e. dump core) in the first instance.
     */
    if (ptr_info_exception->dwFirstChance == 0)
        return DBG_EXCEPTION_NOT_HANDLED;

    if ((ptr_info_exception->ExceptionRecord.ExceptionFlags & EXCEPTION_NONCONTINUABLE) == EXCEPTION_NONCONTINUABLE) {
        stage0_dbg_create_dump(
            h_process,
            id_process,
            id_thread,
            &ptr_info_exception->ExceptionRecord);

        return DBG_EXCEPTION_NOT_HANDLED;
    }

    return DBG_CONTINUE;
}

// The main loop of the debugger. Handles incoming debug events.
static void stage0_dbg_loop() {
    /* [fkelava 13/09/26 02:39]
     * See https://learn.microsoft.com/en-us/windows/win32/debug/debugging-events,
     * https://learn.microsoft.com/en-us/windows/win32/debug/writing-the-debugger-s-main-loop.
     *
     * The relevant passages are given in comments.
     */

    HANDLE h_process = { 0 };

    while (true) {
        DEBUG_EVENT event;
        DWORD       continue_state = DBG_EXCEPTION_NOT_HANDLED;

        WaitForDebugEventEx(&event, INFINITE);

        DWORD event_code = event.dwDebugEventCode;
        DWORD id_thread  = event.dwThreadId;
        DWORD id_process = event.dwProcessId;

        if (event_code == CREATE_PROCESS_DEBUG_EVENT) {
            h_process = event.u.CreateProcessInfo.hProcess;

            /* [fkelava 13/09/26 02:03]
             * > The handle to the process's image file has GENERIC_READ access and is opened for read-sharing.
             * > The debugger should close this handle while processing CREATE_PROCESS_DEBUG_EVENT.
             */
            HANDLE image_file_handle = event.u.CreateProcessInfo.hFile;

            if (image_file_handle != nullptr && image_file_handle != INVALID_HANDLE_VALUE) {
                CloseHandle(image_file_handle);
            }
        }

        if (event_code == EXIT_PROCESS_DEBUG_EVENT) {
            /* [fkelava 13/09/26 02:03]
             * > The kernel-mode portion of process shutdown cannot be completed
             * > until the debugger that receives this event calls ContinueDebugEvent.
             * >
             * > The system closes the debugger's handle to the exiting process
             * > and all of the process's threads. The debugger should not close these handles.
             */

            ContinueDebugEvent(id_process, id_thread, continue_state);
            return;
        }

        if (event_code == EXCEPTION_DEBUG_EVENT) {
            continue_state = stage0_dbg_exception(h_process, id_process, id_thread, &event.u.Exception);
        }

        ContinueDebugEvent(id_process, id_thread, continue_state);
    }
}

int wmain(
    int      argc,
    wchar_t* argv[ ]
) {
    if (argc < 2) {
        std::wcerr << "Invalid call. You must specify an executable to launch.\n";
        std::wcerr << "Usage: fhstage0.exe {EXECUTABLE_TO_LAUNCH} {ARGS}\n";
        return 1;
    }

    LPCSTR              szDllPath = "fhstage1.dll";
    PROCESS_INFORMATION pi;
    STARTUPINFO         si = { 0 };

    si.cb = sizeof(si);

    // Set up args as the game expects them to be.
    std::wstring args;

    for (int i = 1; i < argc; i++) {
        args.append(argv[i]);
        args.append(L" ");
    }

    bool  external_debug = wcsstr(args.c_str(), L"--debug") != NULL;
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
        std::wcerr << "Failed to create target process.\n";
        return 1;
    }

    // Pause for external debugger attach if `--debug` arg is passed.
    if (external_debug) {
        std::wcout << "You can now attach a debugger; press any key to attempt launch.\n";
        int i = _getch();
    }


    // Patch IAT of suspended process to inject Stage 1 DLL at position 1.
    if (!DetourUpdateProcessWithDll(pi.hProcess, &szDllPath, 1)) {
        TerminateProcess(pi.hProcess, ~0u);
        return FALSE;
    }

    std::wcout << "Stage 0 Loader complete. Moving to Stage 1.\n";

    // Either wait for the process to exit if an external debugger is connected,
    // or begin pumping debug events with the Stage 0 stub debugger.
    if (external_debug) {
        ResumeThread       (pi.hThread);
        WaitForSingleObject(pi.hProcess, INFINITE);
    }
    else { stage0_dbg_loop(); }

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
