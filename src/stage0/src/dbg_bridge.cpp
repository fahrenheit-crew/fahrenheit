// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

/* [fkelava 12/09/26 23:26]
 * As a debugger, Stage 0 has a wealth of otherwise unavailable information about process execution.
 *
 * To make it available to Fahrenheit across the process boundary, Stage 0 creates a named pipe
 * to which Stage 1 and Fahrenheit connect on the other end.
 */

#include <fhstage0.h>
#include <fhstage1.h>

HANDLE g_bridge_pipe = nullptr; // A named pipe to communicate with Stage 1.
BOOL   g_bridge_init = FALSE;   // A flag marking pipe initialization as complete.

// Communicates with Stage 1 using the debug pipe to obtain pointers to its functions.
static DWORD WINAPI s0_dbg_bridge_proc(LPVOID lpvParam) {
    if (!ConnectNamedPipe(g_bridge_pipe, NULL) && GetLastError() != ERROR_PIPE_CONNECTED) {
        fwprintf_s(stderr, L"[!] Failed to connect to debug pipe with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    HANDLE h_process_heap = GetProcessHeap();
    if (h_process_heap == nullptr || h_process_heap == INVALID_HANDLE_VALUE) {
        fwprintf_s(stderr, L"[!] GetProcessHeap() failed with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    // TODO: Amend to include actual messaging type
    DWORD          message_size = sizeof(LPVOID);
    unsigned char* message = (unsigned char*)HeapAlloc(h_process_heap, HEAP_ZERO_MEMORY, message_size);

    if (message == nullptr) {
        fwprintf_s(stderr, L"[!] Failed to allocate memory for debug pipe message.\n");
        return FALSE;
    }

    DWORD bytes_read = 0;
    if (!ReadFile(
        g_bridge_pipe,
        message,
        message_size,
        &bytes_read,
        NULL
    )) {
        fwprintf_s(stderr, L"[!] Failed to read debug pipe message with code 0x%X.\n", GetLastError());
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

// Kicks off a thread that communicates with the debug pipe in Stage 1.
BOOL s0_dbg_bridge_init() {
    if (g_bridge_init)
        return TRUE;

    g_bridge_init = TRUE;
    g_bridge_pipe = CreateNamedPipeW(
        FH_DBG_PIPE_NAME,
        PIPE_ACCESS_INBOUND,
        PIPE_TYPE_MESSAGE | PIPE_READMODE_MESSAGE | PIPE_WAIT,
        2,
        4096,
        4096,
        0,
        nullptr
    );

    if (g_bridge_pipe == nullptr || g_bridge_pipe == INVALID_HANDLE_VALUE) {
        fwprintf_s(stderr, L"[!] Failed to create debug pipe.\n");
        return FALSE;
    }

    DWORD  id_thread;
    HANDLE h_thread = CreateThread(
        nullptr,
        0,
        s0_dbg_bridge_proc,
        nullptr,
        0,
        &id_thread
    );

    if (h_thread == nullptr || h_thread == INVALID_HANDLE_VALUE) {
        fwprintf_s(stderr, L"[!] Failed to create debug pipe thread with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    if (!CloseHandle(h_thread)) {
        fwprintf_s(stderr, L"[!] Failed to close debug pipe thread with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    return TRUE;
}
