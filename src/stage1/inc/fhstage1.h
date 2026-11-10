// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

#pragma once

// A structure describing a loaded module.
struct FH_DBG_MOD_LOAD {
    void*         module_base;
    unsigned long module_size;
    wchar_t       module_name[260];
};

// A structure describing an unloaded module.
struct FH_DBG_MOD_UNLOAD {
    void* module_base;
};

using fn_s1_load   = void (__stdcall*) (FH_DBG_MOD_LOAD*   ptr_data_load);
using fn_s1_unload = void (__stdcall*) (FH_DBG_MOD_UNLOAD* ptr_data_unload);

extern "C" {
    __declspec(dllexport) void s1_dbg_link_set_load_cb  (fn_s1_load   fnptr_cb);
    __declspec(dllexport) void s1_dbg_link_set_unload_cb(fn_s1_unload fnptr_cb);
}

enum FH_DBG_MSG_TYPE {
    MSG_LOAD   = 1,
    MSG_UNLOAD = 2
};

// Describes an event transpiring in the process.
struct FH_DBG_MSG {
    FH_DBG_MSG_TYPE type;
    union {
        FH_DBG_MOD_LOAD   mod_load;
        FH_DBG_MOD_UNLOAD mod_unload;
    } data;
};

constexpr const wchar_t* FH_DBG_LINK_NAME = LR"(\\.\pipe\fahrenheit-crew.fahrenheit.DBG_LINK)";
