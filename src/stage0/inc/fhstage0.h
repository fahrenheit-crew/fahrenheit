// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

#pragma once

#define WIN32_LEAN_AND_MEAN // Exclude rarely-used stuff from Windows headers

// Win32
#include <windows.h>
#include <strsafe.h>
#include <pathcch.h>

// STL
#include <vector>

// From <dbghelp.h>, to avoid including the whole header.
#define MAX_SYM_NAME 2000

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

// Prepares the necessary DLL paths for CLR debugging.
BOOL s0_dbg_cor_init(
    LPVOID ptr_coreclr, // The pointer to the image base of the `coreclr.dll` for this session.
    LPWSTR path_coreclr // The full path to `coreclr.dll` for this session.
);

// Performs a managed stack walk, gathering any available symbols.
HRESULT s0_dbg_cor_stack_walk(
    HANDLE h_process, // A handle to the process the fault occurred in.
    DWORD  id_thread  // The ID of the faulting thread in the process that encountered an exception.
);

// Kicks off a thread that communicates with the debug pipe in Stage 1.
BOOL s0_dbg_bridge_init();

// Debugger main loop.
void s0_dbg_w32_main();

// Global variables
inline wchar_t                    g_path_dir_cache[MAX_PATH] = { 0 }; // The full path to the 'cache' directory, used to store symbols.
inline wchar_t                    g_path_dir_crash[MAX_PATH] = { 0 }; // The full path to the 'crash' directory, used to store core dumps.
inline std::vector<S0_FRAME_DATA> g_frames;                           // The frames on the stack at the time of exception.
