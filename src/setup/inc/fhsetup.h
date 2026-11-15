// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

#pragma once

// SDL3
#include <SDL3/SDL.h>
#include <SDL3/SDL_opengl.h>

enum FH_SETUP_REDIST_TYPE {
    X86   = 1,
    X64   = 2,
    ARM64 = 3,
};

enum FH_SETUP_GAMEDIR_ERROR {
    MISSING_BIN    = 1,      // Either game binary is missing.
    INVALID_BIN    = 2,      // Either game binary is invalid.
    MISSING_VBF    = 3,      // Either VBF archive is missing.
    INTERNAL_ERROR = 0xFFFF, // An internal error occurred.
};

void setup_ui_main(); // Displays the setup user interface.

bool setup_check_vcredist(      FH_SETUP_REDIST_TYPE type);     // Determines whether the specified VC Redistributable is installed on this system.
bool setup_check_nethost (      FH_SETUP_REDIST_TYPE type);     // Determines whether the specified .NET Runtime is installed on this system.
bool setup_check_gamedir (const char*                ptr_path); // Determines whether a given directory is a valid game directory.

inline SDL_Window*   g_sdl_window_ptr;
inline SDL_WindowID  g_sdl_window_id;
inline SDL_GLContext g_sdl_context;
