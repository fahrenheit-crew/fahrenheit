// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

#pragma once

#define WIN32_LEAN_AND_MEAN // Exclude rarely-used stuff from Windows headers
#define MAX_SYM_NAME 2000   // From <dbghelp.h>, to avoid including the whole header.

// Win32
#include <windows.h>
#include <strsafe.h>
#include <pathcch.h>

// STL
#include <vector>

// Stage 1 Debug Bridge defs
#include <fhstage1.h>

// Determines the type of a given stack frame.
enum S0_FRAME_TYPE {
    FRAME_NATIVE  = 1,
    FRAME_MANAGED = 2
};

// Describes a unique stack frame.
struct S0_FRAME_DATA {
    S0_FRAME_TYPE frame_type               = FRAME_NATIVE;
    wchar_t       frame_name[MAX_SYM_NAME] = L"Unknown frame.\n";
};

BOOL s0_dbg_cor_init(
    LPVOID ptr_coreclr,
    LPWSTR path_coreclr
);

HRESULT s0_dbg_cor_stack_walk(
    HANDLE h_process,
    DWORD  id_thread
);

BOOL s0_dbg_bridge_init();
void s0_dbg_bridge_send(FH_DBG_MOD_LOAD   mod_load);
void s0_dbg_bridge_send(FH_DBG_MOD_UNLOAD mod_unload);

void s0_dbg_w32_main();

// Global variables
inline wchar_t                    g_path_dir_cache[MAX_PATH] = { 0 }; // The full path to the 'cache' directory, used to store symbols.
inline wchar_t                    g_path_dir_crash[MAX_PATH] = { 0 }; // The full path to the 'crash' directory, used to store core dumps.
inline std::vector<S0_FRAME_DATA> g_frames;                           // The frames on the stack at the time of exception.
