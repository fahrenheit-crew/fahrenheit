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

#ifdef _DEBUG
constexpr auto MINHOOK_DLL = "minhook.x32d.dll";
#else
constexpr auto MINHOOK_DLL = "minhook.x32.dll";
#endif

inline wchar_t g_path_dir_cache[MAX_PATH] = { 0 }; // The full path to the 'cache' directory, used to store symbols.
inline wchar_t g_path_dir_crash[MAX_PATH] = { 0 }; // The full path to the 'crash' directory, used to store core dumps.

inline wchar_t g_path_coreclr     [MAX_PATH] = { 0 }; // The full path to the loaded CoreCLR.
inline wchar_t g_path_mscordbi    [MAX_PATH] = { 0 }; // The full path to the `mscordbi` module for the given CoreCLR.
inline wchar_t g_path_mscordacwks [MAX_PATH] = { 0 }; // The full path to the `mscordacwks` module for the given CoreCLR.
inline wchar_t g_path_mscordaccore[MAX_PATH] = { 0 }; // The full path to the `mscordaccore` module for the given CoreCLR.

inline wchar_t g_target     [MAX_PATH] = { 0 }; // The path to the target binary.
inline wchar_t g_args_target[1024]     = { 0 }; // The command-line arguments to pass to the target.
inline wchar_t g_args_self  [1024]     = { 0 }; // The command-line arguments to Stage 0.
inline wchar_t g_dir_target [MAX_PATH] = { 0 }; // The directory the target binary is in.
inline char    g_dir_self   [MAX_PATH] = { 0 }; // The directory `fhstage0` is in.
