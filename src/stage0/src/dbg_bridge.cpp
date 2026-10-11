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
BOOL   g_bridge_init = FALSE;   // Whether the pipe was initialized.

FH_DBG_COMM_MSG g_bridge_msg;
HANDLE          g_bridge_lock;

// Communicates with Stage 1 using the debug pipe to obtain pointers to its functions.
static DWORD WINAPI s0_dbg_bridge_proc(LPVOID lpvParam) {
    if (!ConnectNamedPipe(g_bridge_pipe, NULL) && GetLastError() != ERROR_PIPE_CONNECTED) {
        fwprintf_s(stderr, L"[!] Failed to connect to debug pipe with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    g_bridge_lock = CreateEventW(NULL, TRUE, FALSE, NULL);

    if (g_bridge_lock == nullptr) {
        fwprintf_s(stderr, L"[!] CreateEventW() failed with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    while (true) {
        if (WaitForSingleObject(g_bridge_lock, INFINITE) != WAIT_OBJECT_0) {
            fwprintf_s(stderr, L"[!] Debug pipe server aborting. WaitForSingleObject failed with code 0x%X.\n", GetLastError());
            break;
        }

        DWORD bytes_written = 0;
        if (!WriteFile(
            g_bridge_pipe,
            &g_bridge_msg,
            sizeof(g_bridge_msg),
            &bytes_written,
            NULL
        )) {
            fwprintf_s(stderr, L"[!] Debug pipe server aborting. WriteFile failed with code 0x%X.\n", GetLastError());
            break;
        }

        if (!ResetEvent(g_bridge_lock)) {
            fwprintf_s(stderr, L"[!] Debug pipe server aborting. ResetEvent failed with code 0x%X.\n", GetLastError());
            break;
        }
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
        PIPE_ACCESS_DUPLEX,
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

    HRESULT hr = SetThreadDescription(h_thread, L"Stage 0 - Debug Pipe Server");
    if (FAILED(hr)) {
        fwprintf_s(stderr, L"[!] Failed to set debug pipe thread name with code 0x%X.\n", hr);
        return FALSE;
    }

    if (!CloseHandle(h_thread)) {
        fwprintf_s(stderr, L"[!] Failed to close debug pipe thread with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    return TRUE;
}
