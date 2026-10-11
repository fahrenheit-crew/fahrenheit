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

HANDLE g_lock_cb;     // An event blocking access to the load/unload functions while callbacks are executing.
HANDLE g_bridge_pipe; // A named pipe to communicate with Stage 0.

FH_DBG_COMM_MSG g_bridge_msg;

/* [fkelava 11/10/26 01:03]
 * https://learn.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-createnamedpipea
 * Read this thoroughly. Comments throughout quote from this article.
 */

// The debug pipe thread procedure.
static DWORD WINAPI s1_dbg_bridge_proc(LPVOID lpvParam) {
    while (true) {
        /* [fkelava 11/10/26 03:00]
         * The pipe is created in blocking (PIPE_WAIT) mode by Stage 0. Thus:
         *
         * > When the pipe handle is specified in {...} ReadFile {...} the operations are not
         * > completed until there is data to read, all data is written, or a client is connected.
         */

        DWORD bytes_read = 0;
        if (!ReadFile(
            g_bridge_pipe,
            &g_bridge_msg,
            sizeof(g_bridge_msg),
            &bytes_read,
            NULL
        )) {
            fwprintf_s(stderr, L"[!] Debug pipe client aborting. ReadFile failed with code 0x%X.\n", GetLastError());
            break;
        }

        // TODO: Switch on message type and do shit
    }

    if (!CloseHandle(g_bridge_pipe)) {
        fwprintf_s(stderr, L"[!] Failed to close debug pipe with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    return TRUE;
}

// Prepares for communication with Stage 0 through the debug pipe.
BOOL s1_bridge_init() {

    /* [fkelava 08/10/26 23:08]
     * > If no instances of the specified named pipe exist, {...} returns immediately regardless of time-out.
     * If the pipe doesn't exist, we can assume we're in --extdbg mode and the debug bridge should not be enabled.
     */
    if (!WaitNamedPipeW(
        FH_DBG_PIPE_NAME,
        NMPWAIT_WAIT_FOREVER
    )) {
        return TRUE;
    }

    g_lock_cb = CreateEventW(NULL, TRUE, TRUE, NULL);

    if (g_lock_cb == nullptr) {
        fwprintf_s(stderr, L"[!] CreateEventW() failed with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    /* [fkelava 11/10/26 00:57]
     * This is a duplex pipe. Therefore:
     * > The client can specify GENERIC_READ or GENERIC_WRITE, or both,
     * > when it connects to the pipe using the CreateFile function.
     */
    g_bridge_pipe = CreateFileW(
        FH_DBG_PIPE_NAME,
        GENERIC_READ | GENERIC_WRITE,
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

    /* [fkelava 11/10/26 01:02]
     * > The client side of a named pipe starts out in byte mode, even if the server side
     * > is in message mode. To avoid problems receiving data, set the client side to message mode as well.
     */
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

    DWORD  id_thread;
    HANDLE h_thread = CreateThread(
        nullptr,
        0,
        s1_dbg_bridge_proc,
        nullptr,
        0,
        &id_thread
    );

    if (h_thread == nullptr || h_thread == INVALID_HANDLE_VALUE) {
        fwprintf_s(stderr, L"[!] Failed to create debug pipe thread with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    HRESULT hr = SetThreadDescription(h_thread, L"Stage 1 - Debug Pipe Client");
    if (FAILED(hr)) {
        fwprintf_s(stderr, L"[!] Failed to set debug pipe thread name with code 0x%X.\n", hr);
        return FALSE;
    }

    return TRUE;
}

// Invoked in response to a Stage 0 module load message.
void s1_bridge_load(
    DLL_LOAD_DATA* ptr_load_data // A pointer to a structure describing the loaded module.
) {
    if (WaitForSingleObject(g_lock_cb, INFINITE) != WAIT_OBJECT_0) {
        fwprintf_s(stderr, L"[!] WaitForSingleObject failed with code 0x%X - load message dropped.\n", GetLastError());
        return;
    }

    if (g_fnptr_load_cb != nullptr) {
        g_fnptr_load_cb(ptr_load_data);
    }
}

// Invoked in response to a Stage 0 module unload message.
void s1_bridge_free(
    void* ptr_dll_base // The base address of the module being unloaded.
) {
    if (WaitForSingleObject(g_lock_cb, INFINITE) != WAIT_OBJECT_0) {
        fwprintf_s(stderr, L"[!] WaitForSingleObject failed with code 0x%X - free message dropped.\n", GetLastError());
        return;
    }

    if (g_fnptr_free_cb != nullptr) {
        g_fnptr_free_cb(ptr_dll_base);
    }
}

// Registers a function to be invoked when a library is loaded.
void s1_bridge_register_load_cb(
    fn_s1_load fnptr_cb // A pointer to the callback to invoke.
) {
    // In --extdbg mode. Do not proceed.
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
    // In --extdbg mode. Do not proceed.
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
