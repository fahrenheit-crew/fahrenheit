// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

#pragma once

/* [fkelava 08/10/26 19:41]
 * Fahrenheit is a .NET plugin framework and thus not NativeAOT capable.
 * [UnmanagedCallersOnly] methods can't be put out as native exports.
 *
 * Stage 1 thus exposes a little shim to which Stage 0 attaches on one side
 * and Fahrenheit on the other, allowing them to communicate.
 *
 * Currently this is only used for Stage 0 to inform Fahrenheit that a DLL
 * (un)load is occurring, so it can evict its hook and address caches appropriately.
 */

// A structure describing a loaded DLL.
struct DLL_LOAD_DATA {
    void*         dll_base;
    unsigned long dll_size;
    wchar_t       dll_name[260];
};

using fn_s1_load = void (__stdcall*) (DLL_LOAD_DATA* ptr_load_data);
using fn_s1_free = void (__stdcall*) (void*          ptr_dll_base);

extern "C" {
    __declspec(dllexport) void s1_bridge_register_load_cb(fn_s1_load fnptr_cb);
    __declspec(dllexport) void s1_bridge_register_free_cb(fn_s1_free fnptr_cb);
}

typedef enum _FH_COMM_MSG_TYPE {
    MSG_INIT = 1,
} FH_COMM_MSG_TYPE;

typedef struct _FH_DBG_COMM_MSG {
    FH_COMM_MSG_TYPE msg_type;
    union {

    } u;
} FH_DBG_COMM_MSG, * LPFH_DBG_COMM_MSG;

#define FH_DBG_PIPE_NAME L"\\\\.\\pipe\\fahrenheit-crew.fahrenheit.DBG_COMM"
