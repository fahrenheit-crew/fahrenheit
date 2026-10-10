// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

#pragma once

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

enum FH_COMM_MSG_TYPE {
    MSG_LOAD   = 1,
    MSG_UNLOAD = 2
};

//
struct FH_DBG_COMM_MSG {
    FH_COMM_MSG_TYPE msg_type;
    union {

    } u;
};

#define FH_DBG_PIPE_NAME L"\\\\.\\pipe\\fahrenheit-crew.fahrenheit.DBG_COMM"
