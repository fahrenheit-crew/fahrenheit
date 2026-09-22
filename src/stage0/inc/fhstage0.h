// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

#pragma once

#pragma comment(lib, "pathcch.lib")
#pragma comment(lib, "dbghelp.lib")
#pragma comment(lib, "dbgshim.lib")
#pragma comment(lib, "corguids.lib")

#define WIN32_LEAN_AND_MEAN // Exclude rarely-used stuff from Windows headers

// STL
#include <stdexcept>
#include <string>
#include <set>
#include <vector>

// Win32
#include <windows.h>
#include <strsafe.h>
#include <PathCch.h>
#include <conio.h>

// Win32 debugging
#include <DbgHelp.h>

// .NET debugging
#include <cor.h>
#include <cordebug.h>
#include <dbgshim.h>

// IAT patching
#include <detours/detours.h>

#ifdef _DEBUG
#define MINHOOK_DLL "minhook.x32d.dll"
#else
#define MINHOOK_DLL "minhook.x32.dll"
#endif

extern wchar_t g_path_dir_cache[MAX_PATH]; // The full path to the 'cache' directory, used to store symbols.
extern wchar_t g_path_dir_crash[MAX_PATH]; // The full path to the 'crash' directory, used to store core dumps.
