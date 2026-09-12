// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

#include "fhstage0.h"

// TODO: adapt the S1 EH code to actually _work_ in this new context

DWORD               g_eh_thread_faulting_id; // The ID of the thread that threw a structured exception.
DWORD               g_eh_thread_handler_id;  // The ID of the thread handling structured exceptions.
HANDLE              g_eh_thread_handler;     // The handle to the thread handling structured exceptions.
EXCEPTION_POINTERS* g_eh_exception_ptr;      // A pointer to the exception bringing the process down.

/*
 * Filters a core dump to exclude objects which we do not want to record.
 */

static BOOL CALLBACK stage0_dbg_filter_dump(
          PVOID                     ptr_callback_param,
    const PMINIDUMP_CALLBACK_INPUT  ptr_callback_input,
          PMINIDUMP_CALLBACK_OUTPUT ptr_callback_output) {
    if (!ptr_callback_input || !ptr_callback_output) return FALSE;

    switch (ptr_callback_input->CallbackType) {
        case CancelCallback:
            return FALSE;

        case IncludeThreadCallback: {
            // Exclude the thread which writes the minidump.
            return ptr_callback_input->IncludeThread.ThreadId != g_eh_thread_handler_id;
        } break;
    }

    return TRUE;
}

/*
 * Writes a customized core dump.
 */

static DWORD CALLBACK stage0_dbg_create_dump(LPVOID ptr_thread_parameter) {
    HANDLE hFile = CreateFileW(
        L"crash_dump.dmp",
        GENERIC_READ | GENERIC_WRITE,
        0,
        nullptr,
        CREATE_ALWAYS,
        FILE_ATTRIBUTE_NORMAL,
        nullptr);

    if (hFile == NULL || hFile == INVALID_HANDLE_VALUE) {
        std::wcerr << "Failed to open a file to write the core dump to." << std::endl;
        return 1;
    }

    HANDLE        hProcess  = GetCurrentProcess();    // TODO: WRONG
    DWORD         ProcessId = GetProcessId(hProcess); // TODO: WRONG
    MINIDUMP_TYPE DumpType  = (MINIDUMP_TYPE)(
                              MiniDumpNormal
                            | MiniDumpWithDataSegs
                            | MiniDumpWithHandleData
                            | MiniDumpWithFullMemoryInfo
                            | MiniDumpWithThreadInfo
                            | MiniDumpWithProcessThreadData
                            | MiniDumpWithUnloadedModules);

    /* [fkelava 11/06/26 21:24]
     * For ClientPointers:
     * https://learn.microsoft.com/en-us/windows/win32/api/minidumpapiset/ns-minidumpapiset-minidump_exception_information#members
     * > If you are accessing local memory (in the calling process) you should not set this member to TRUE.
     */
    MINIDUMP_EXCEPTION_INFORMATION mdei = { 0 };
    mdei.ThreadId          = g_eh_thread_faulting_id; // TODO: WRONG
    mdei.ExceptionPointers = g_eh_exception_ptr;      // TODO: WRONG
    mdei.ClientPointers    = FALSE;

    MINIDUMP_CALLBACK_INFORMATION mci = { 0 };
    mci.CallbackRoutine = (MINIDUMP_CALLBACK_ROUTINE)stage0_dbg_filter_dump;
    mci.CallbackParam   = nullptr;

    PMINIDUMP_EXCEPTION_INFORMATION ExceptionParam = g_eh_exception_ptr != nullptr ? &mdei : nullptr;
    PMINIDUMP_CALLBACK_INFORMATION  CallbackParam  = &mci;

    std::wcerr << "Dumping process core. Please wait." << std::endl;

    BOOL rv = MiniDumpWriteDump(
        hProcess,
        ProcessId,
        hFile,
        DumpType,
        ExceptionParam,
        nullptr,
        CallbackParam);

    if (!rv) {
        std::wcerr << "Failed to capture a core dump." << std::endl;
        return 1;
    }

    CloseHandle(hFile);
    return 0;
}

void dbg_loop() {

    while (true) {
        DEBUG_EVENT event;

        WaitForDebugEventEx(&event, INFINITE);

        if (event.dwDebugEventCode == EXIT_PROCESS_DEBUG_EVENT)
            break;

        ContinueDebugEvent(
            event.dwProcessId,
            event.dwThreadId,
            DBG_EXCEPTION_NOT_HANDLED
        );
    }

}

