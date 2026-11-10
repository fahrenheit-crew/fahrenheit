// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

#define WIN32_LEAN_AND_MEAN // Exclude rarely-used stuff from Windows headers

// Win32
#include <windows.h>
#include <strsafe.h>

// STL
#include <queue>

// This file implements the Stage 1 Debug Bridge.
#include <fhstage1.h>

fn_s1_load   g_fnptr_load_cb;   // A pointer to a callback to invoke when loading a DLL.
fn_s1_unload g_fnptr_unload_cb; // A pointer to a callback to invoke when unloading a DLL.
HANDLE       g_lock_cb;         // Serializes access to the callback pointers.

HANDLE g_link_pipe; // A named pipe to communicate with Stage 0.

std::queue<FH_DBG_MSG> g_msgs;

// Drains the queue of as-yet unprocessed messages.
static void s1_dbg_link_drain_queue() {
    /* [fkelava 11/10/26 21:45]
     * `g_lock_cb` must be held here.
     */
    if (g_fnptr_load_cb == nullptr || g_fnptr_unload_cb == nullptr)
        return;

    while (!g_msgs.empty()) {
        FH_DBG_MSG msg = g_msgs.front();

        switch (msg.type) {
            case MSG_LOAD: {
                g_fnptr_load_cb  (&msg.data.mod_load);
                break;
            }
            case MSG_UNLOAD: {
                g_fnptr_unload_cb(&msg.data.mod_unload);
                break;
            }
        }

        g_msgs.pop();
    }
}

// Sets the function to be invoked when a library is loaded.
void s1_dbg_link_set_load_cb(
    fn_s1_load fnptr_cb // A pointer to the callback to invoke.
) {
    if (g_lock_cb == nullptr || g_lock_cb == INVALID_HANDLE_VALUE) {
        fwprintf_s(stderr, L"[!] Load callbacks do not execute in --extdbg mode.\n");
        return;
    }

    if (WaitForSingleObject(g_lock_cb, INFINITE) != WAIT_OBJECT_0) {
        fwprintf_s(stderr, L"[!] WaitForSingleObject failed with code 0x%X.\n", GetLastError());
        return;
    }

    g_fnptr_load_cb = fnptr_cb;

    if (!SetEvent(g_lock_cb)) {
        fwprintf_s(stderr, L"SetEvent() failed with code 0x%X while setting load callback.\n", GetLastError());
        return;
    }
}

// Sets the function to be invoked when a library is freed.
void s1_dbg_link_set_unload_cb(
    fn_s1_unload fnptr_cb // A pointer to the callback to invoke.
) {
    if (g_lock_cb == nullptr || g_lock_cb == INVALID_HANDLE_VALUE) {
        fwprintf_s(stderr, L"[!] Unload callbacks do not execute in --extdbg mode.\n");
        return;
    }

    if (WaitForSingleObject(g_lock_cb, INFINITE) != WAIT_OBJECT_0) {
        fwprintf_s(stderr, L"[!] WaitForSingleObject failed with code 0x%X.\n", GetLastError());
        return;
    }

    g_fnptr_unload_cb = fnptr_cb;

    if (!SetEvent(g_lock_cb)) {
        fwprintf_s(stderr, L"SetEvent() failed with code 0x%X while setting unload callback.\n", GetLastError());
        return;
    }
}

/* [fkelava 11/10/26 01:03]
 * https://learn.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-createnamedpipea
 * Read this thoroughly. Comments throughout quote from this article.
 */

// The debug pipe thread procedure.
static DWORD WINAPI s1_dbg_link_proc(LPVOID lpvParam) {
    while (true) {
        /* [fkelava 11/10/26 03:00]
         * The pipe is created in blocking (PIPE_WAIT) mode by Stage 0. Thus:
         *
         * > When the pipe handle is specified in {...} ReadFile {...} the operations are not
         * > completed until there is data to read, all data is written, or a client is connected.
         */
        FH_DBG_MSG msg        = { };
        DWORD      bytes_read = 0;

        if (!ReadFile(
            g_link_pipe,
            &msg,
            sizeof(FH_DBG_MSG),
            &bytes_read,
            NULL
        )) {
            fwprintf_s(stderr, L"[!] Debug link client aborting. ReadFile failed with code 0x%X.\n", GetLastError());
            break;
        }

        g_msgs.emplace(msg);

        if (WaitForSingleObject(g_lock_cb, INFINITE) != WAIT_OBJECT_0) {
            fwprintf_s(stderr, L"[!] Debug link client aborting. WaitForSingleObject failed with code 0x%X.\n", GetLastError());
            break;
        }

        s1_dbg_link_drain_queue();

        if (!SetEvent(g_lock_cb)) {
            fwprintf_s(stderr, L"[!] Debug link client aborting. SetEvent failed with code 0x%X.\n", GetLastError());
            break;
        }
    }

    if (!CloseHandle(g_link_pipe)) {
        fwprintf_s(stderr, L"[!] Failed to close debug link with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    return TRUE;
}

// Prepares for communication with Stage 0 through the debug pipe.
BOOL s1_dbg_link_init() {
    /* [fkelava 08/10/26 23:08]
     * > If no instances of the specified named pipe exist, {...} returns immediately regardless of time-out.
     * If the pipe doesn't exist, we can assume we're in --extdbg mode and the debug bridge should not be enabled.
     */
    if (!WaitNamedPipeW(
        FH_DBG_LINK_NAME,
        NMPWAIT_WAIT_FOREVER
    )) {
        return TRUE;
    }

    g_lock_cb = CreateEventW(NULL, FALSE, TRUE, NULL);

    if (g_lock_cb == nullptr) {
        fwprintf_s(stderr, L"[!] CreateEventW() failed with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    /* [fkelava 11/10/26 00:57]
     * This is a duplex pipe. Therefore:
     * > The client can specify GENERIC_READ or GENERIC_WRITE, or both,
     * > when it connects to the pipe using the CreateFile function.
     */
    g_link_pipe = CreateFileW(
        FH_DBG_LINK_NAME,
        GENERIC_READ | GENERIC_WRITE,
        0,
        nullptr,
        OPEN_EXISTING,
        0,
        nullptr
    );

    if (g_link_pipe == nullptr || g_link_pipe == INVALID_HANDLE_VALUE) {
        fwprintf_s(stderr, L"[!] CreateFileW() failed with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    /* [fkelava 11/10/26 01:02]
     * > The client side of a named pipe starts out in byte mode, even if the server side
     * > is in message mode. To avoid problems receiving data, set the client side to message mode as well.
     */
    DWORD pipe_mode = PIPE_READMODE_MESSAGE;
    if (!SetNamedPipeHandleState(
        g_link_pipe,
        &pipe_mode,
        nullptr,
        nullptr)
    ) {
        fwprintf_s(stderr, L"[!] SetNamedPipeHandleState() failed with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    DWORD  id_thread;
    HANDLE h_thread = CreateThread(
        nullptr,
        0,
        s1_dbg_link_proc,
        nullptr,
        0,
        &id_thread
    );

    if (h_thread == nullptr || h_thread == INVALID_HANDLE_VALUE) {
        fwprintf_s(stderr, L"[!] Failed to create debug link thread with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    HRESULT hr = SetThreadDescription(h_thread, L"Stage 1 - Debug Link Client");
    if (FAILED(hr)) {
        fwprintf_s(stderr, L"[!] Failed to set debug link thread name with code 0x%X.\n", hr);
        return FALSE;
    }

    return TRUE;
}
