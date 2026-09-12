// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

#pragma once
#pragma comment(lib, "pathcch.lib")

#define WIN32_LEAN_AND_MEAN // Exclude rarely-used stuff from Windows headers

// STL
#include <iostream>

// Win32
#include <windows.h>
#include <PathCch.h>

// .NET hosting
#include <nethost.h>
#include <coreclr_delegates.h>
#include <hostfxr.h>

// IAT patching
#include <detours/detours.h>

// Hooking
#include <MinHook.h>
