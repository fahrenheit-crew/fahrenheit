// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

#define WIN32_LEAN_AND_MEAN // Exclude rarely-used stuff from Windows headers

// Win32
#include <windows.h>
#include <strsafe.h>

// This file implements the Stage 1 Debug Bridge.
#include <fhstage1.h>

fn_s1_load g_fnptr_load_cb; // A pointer to a callback to invoke when loading a DLL.
fn_s1_free g_fnptr_free_cb; // A pointer to a callback to invoke when unloading a DLL.

HANDLE g_lock_cb;     // A event blocking access to the load/unload functions while callbacks are executing.
HANDLE g_bridge_pipe; // A named pipe to communicate with Stage 0.

// Prepares for communication with Stage 0 through the debug pipe.
BOOL s1_bridge_init() {
    if (!WaitNamedPipeW(
        FH_DBG_PIPE_NAME,
        NMPWAIT_WAIT_FOREVER
    )) {
        /* [fkelava 08/10/26 23:08]
         * https://learn.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-waitnamedpipea#remarks
         * > If no instances of the specified named pipe exist, {...} returns immediately regardless of time-out.
         *
         * If there is no such pipe, we can assume we're in --extdbg mode and the debug bridge should not be enabled.
         */
        return TRUE;
    }

    g_lock_cb = CreateEventW(NULL, TRUE, TRUE, NULL);

    if (g_lock_cb == nullptr) {
        fwprintf_s(stderr, L"[!] CreateEventW() failed with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    g_bridge_pipe = CreateFileW(
        FH_DBG_PIPE_NAME,
        GENERIC_WRITE,
        0,
        nullptr,
        OPEN_EXISTING,
        0,
        nullptr
    );

    if (g_bridge_pipe == nullptr || g_bridge_pipe == INVALID_HANDLE_VALUE) {
        fwprintf_s(stderr, L"[!] CreateFileW() failed with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    DWORD pipe_mode = PIPE_READMODE_MESSAGE;
    if (!SetNamedPipeHandleState(
        g_bridge_pipe,
        &pipe_mode,
        nullptr,
        nullptr)
    ) {
        fwprintf_s(stderr, L"[!] SetNamedPipeHandleState() failed with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    HANDLE h_process_heap = GetProcessHeap();
    if (h_process_heap == nullptr || h_process_heap == INVALID_HANDLE_VALUE) {
        fwprintf_s(stderr, L"[!] GetProcessHeap() failed with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    // TODO: Amend to include actual messaging type.
    DWORD          message_size = sizeof(LPVOID);
    unsigned char* message      = (unsigned char*) HeapAlloc(h_process_heap, HEAP_ZERO_MEMORY, message_size);

    if (message == nullptr) {
        fwprintf_s(stderr, L"[!] Failed to allocate memory for debug pipe message.\n");
        return FALSE;
    }

    DWORD bytes_written = 0;
    if (!WriteFile(
        g_bridge_pipe,
        message,
        message_size,
        &bytes_written,
        NULL
    )) {
        fwprintf_s(stderr, L"[!] Failed to write through the debug pipe with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    if (!HeapFree(h_process_heap, 0, message)) {
        fwprintf_s(stderr, L"[!] Failed to free memory for debug pipe message with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    if (!CloseHandle(g_bridge_pipe)) {
        fwprintf_s(stderr, L"[!] Failed to close debug pipe with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    return TRUE;
}

// Invoked by Stage 0 when a library is loaded.
void s1_bridge_load(
    DLL_LOAD_DATA* ptr_load_data
) {
    WaitForSingleObject(g_lock_cb, INFINITE);

    if (g_fnptr_load_cb != nullptr) {
        g_fnptr_load_cb(ptr_load_data);
    }
}

// Invoked by Stage 0 when a library is freed.
void s1_bridge_free(
    void* ptr_dll_base //
) {
    WaitForSingleObject(g_lock_cb, INFINITE);

    if (g_fnptr_free_cb != nullptr) {
        g_fnptr_free_cb(ptr_dll_base);
    }
}

// Registers a function to be invoked when a library is loaded.
void s1_bridge_register_load_cb(
    fn_s1_load fnptr_cb // A pointer to the callback to invoke.
) {
    // The user could be in --extdbg mode. Do not proceed.
    if (g_lock_cb == nullptr || g_lock_cb == INVALID_HANDLE_VALUE)
        return;

    if (!ResetEvent(g_lock_cb)) {
        fwprintf_s(stderr, L"ResetEvent() failed with code 0x%X - callback aborted.\n", GetLastError());
        return;
    }

    g_fnptr_load_cb = fnptr_cb;

    if (!SetEvent(g_lock_cb)) {
        fwprintf_s(stderr, L"SetEvent() failed with code 0x%X - callback aborted.\n", GetLastError());
        return;
    }
}

// Registers a function to be invoked when a library is freed.
void s1_bridge_register_free_cb(
    fn_s1_free fnptr_cb // A pointer to the callback to invoke.
) {
    // The user could be in --extdbg mode. Do not proceed.
    if (g_lock_cb == nullptr || g_lock_cb == INVALID_HANDLE_VALUE)
        return;

    if (!ResetEvent(g_lock_cb)) {
        fwprintf_s(stderr, L"ResetEvent() failed with code 0x%X - callback aborted.\n", GetLastError());
        return;
    }

    g_fnptr_free_cb = fnptr_cb;

    if (!SetEvent(g_lock_cb)) {
        fwprintf_s(stderr, L"SetEvent() failed with code 0x%X - callback aborted.\n", GetLastError());
        return;
    }
}
