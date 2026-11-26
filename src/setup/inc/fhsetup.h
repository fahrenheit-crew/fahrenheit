// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

#pragma once

// SDL3
#include <SDL3/SDL.h>
#include <SDL3/SDL_opengl.h>

// The possible types of redistributable checks.
enum FH_CHECK_TYPE {
    X86   = 1,
    X64   = 2,
    ARM64 = 3,
};

// The possible results of a system check function.
enum FH_CHECK_RESULT : short {
    SUCCESS        =  1, // The check was successful.
    FAILURE        =  2, // The check failed. The requisite object is not present.
    MISSING_BIN    =  3, // Either game binary is missing.
    INVALID_BIN    =  4, // Either game binary is invalid.
    MISSING_VBF    =  5, // Either VBF archive is missing.
    INTERNAL_ERROR = -1, // An internal error occurred.
};

// Displays the setup user interface.
void setup_ui_main();

// Rescans all dependencies.
void setup_check_rescan();

// Determines whether the specified VC Redistributable is installed on this system.
FH_CHECK_RESULT setup_check_vcredist(
    FH_CHECK_TYPE type // [in] The type of VC++ redistributable to query.
);

// Determines whether the specified .NET Runtime is installed on this system.
FH_CHECK_RESULT setup_check_nethost (
    FH_CHECK_TYPE type // [in] The type of .NET runtime to query.
);

// Determines whether a given directory is a valid game directory.
FH_CHECK_RESULT setup_check_gamedir (
    const wchar_t* ptr_path // [in] An absolute path to a user-supplied directory to check.
);

inline FH_CHECK_RESULT g_check_vc_x86;  // The result of the last x86 VC++ Redistributable scan.
inline FH_CHECK_RESULT g_check_vc_x64;  // The result of the last x64 VC++ Redistributable scan.
inline FH_CHECK_RESULT g_check_net_x86; // The result of the last x86 .NET Runtime scan.
inline FH_CHECK_RESULT g_check_net_x64; // The result of the last x64 .NET Runtime scan.
inline FH_CHECK_RESULT g_check_game;    // The result of the game directory check, if any.

inline SDL_Window*   g_sdl_window_ptr;
inline SDL_WindowID  g_sdl_window_id;
inline SDL_GLContext g_sdl_context;

