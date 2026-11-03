// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

/* [fkelava 12/09/26 23:26]
 * As a debugger, Stage 0 has access to otherwise unobtainable information about process execution.
 *
 * To make it available to Fahrenheit across the process boundary, Stage 0 has a 'debug link' (a named pipe)
 * to which Stage 1 connects on the other end, allowing Fahrenheit access to this information.
 */

#include <fhstage0.h>

// STL
#include <queue>

HANDLE g_link_pipe = nullptr; // A named pipe to communicate with Stage 1.
HANDLE g_link_lock = nullptr; // Serializes message sending.
BOOL   g_link_init = FALSE;   // Whether the link was initialized.

std::queue<FH_DBG_MSG> g_msgs;

// Drains the pending message queue.
static BOOL s0_dbg_link_drain_queue() {
    while (!g_msgs.empty()) {
        FH_DBG_MSG msg           = g_msgs.front();
        DWORD      bytes_written = 0;

        if (!WriteFile(
            g_link_pipe,
            &msg,
            sizeof(FH_DBG_MSG),
            &bytes_written,
            NULL
        )) {
            return FALSE;
        }

        g_msgs.pop();
    }

    return TRUE;
}

// Communicates with Stage 1 using the debug link.
static DWORD WINAPI s0_dbg_link_proc(LPVOID lpvParam) {
    if (!ConnectNamedPipe(g_link_pipe, NULL) && GetLastError() != ERROR_PIPE_CONNECTED) {
        fwprintf_s(stderr, L"[!] Failed to connect to debug link with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    while (true) {
        /* [fkelava 11/10/26 17:01]
         * https://learn.microsoft.com/en-us/windows/win32/api/synchapi/nf-synchapi-createeventw
         * > {...} system automatically resets the event state to nonsignaled after a single waiting thread has been released.
         */
        if (WaitForSingleObject(g_link_lock, INFINITE) != WAIT_OBJECT_0) {
            fwprintf_s(stderr, L"[!] Debug link server aborting. WaitForSingleObject failed with code 0x%X.\n", GetLastError());
            break;
        }

        if (!s0_dbg_link_drain_queue()) {
            fwprintf_s(stderr, L"[!] Debug link server aborting. WriteFile failed with code 0x%X.\n", GetLastError());
            break;
        }
    }

    if (!CloseHandle(g_link_pipe)) {
        fwprintf_s(stderr, L"[!] Failed to close debug link handle with code 0x%X.\n", GetLastError());
    }

    return FALSE;
}

// Kicks off a thread that operates the debug link with Stage 1.
BOOL s0_dbg_link_init() {
    if (g_link_init)
        return TRUE;

    g_link_init = TRUE;
    g_link_pipe = CreateNamedPipeW(
        FH_DBG_LINK_NAME,
        PIPE_ACCESS_DUPLEX,
        PIPE_TYPE_MESSAGE | PIPE_READMODE_MESSAGE | PIPE_WAIT,
        2,
        4096,
        4096,
        0,
        nullptr
    );

    if (g_link_pipe == nullptr || g_link_pipe == INVALID_HANDLE_VALUE) {
        fwprintf_s(stderr, L"[!] Failed to create debug link.\n");
        return FALSE;
    }

    g_link_lock = CreateEventW(NULL, FALSE, FALSE, NULL);

    if (g_link_lock == nullptr || g_link_lock == INVALID_HANDLE_VALUE) {
        fwprintf_s(stderr, L"[!] CreateEventW() failed with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    DWORD  id_thread;
    HANDLE h_thread = CreateThread(
        nullptr,
        0,
        s0_dbg_link_proc,
        nullptr,
        0,
        &id_thread
    );

    if (h_thread == nullptr || h_thread == INVALID_HANDLE_VALUE) {
        fwprintf_s(stderr, L"[!] Failed to create debug link thread with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    HRESULT hr = SetThreadDescription(h_thread, L"Stage 0 - Debug Link Server");
    if (FAILED(hr)) {
        fwprintf_s(stderr, L"[!] Failed to set debug link thread name with code 0x%X.\n", hr);
        return FALSE;
    }

    if (!CloseHandle(h_thread)) {
        fwprintf_s(stderr, L"[!] Failed to close debug link thread handle with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    return TRUE;
}

// Sends a module load message through the debug link.
void s0_dbg_link_send(FH_DBG_MOD_LOAD data_load) {
    FH_DBG_MSG msg = { };
    msg.type          = MSG_LOAD;
    msg.data.mod_load = data_load;

    g_msgs.emplace(msg);

    if (g_link_lock == nullptr || g_link_lock == INVALID_HANDLE_VALUE)
        return;

    if (!SetEvent(g_link_lock)) {
        fwprintf_s(stderr, L"[!] Failed to signal debug link thread with code 0x%X.\n", GetLastError());
        return;
    }
}

// Sends a module unload message through the debug link.
void s0_dbg_link_send(FH_DBG_MOD_UNLOAD data_unload) {
    FH_DBG_MSG msg = { };
    msg.type            = MSG_UNLOAD;
    msg.data.mod_unload = data_unload;

    g_msgs.emplace(msg);

    if (g_link_lock == nullptr || g_link_lock == INVALID_HANDLE_VALUE)
        return;

    if (!SetEvent(g_link_lock)) {
        fwprintf_s(stderr, L"[!] Failed to signal debug link thread with code 0x%X.\n", GetLastError());
        return;
    }
}
