// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

/* [fkelava 12/11/26 14:39]
 * Mostly a verbatim copy of the SDL3/OGL3 example. Unlike the C# equivalent, here
 * we have the option of following the example more closely.
 */

/* [fkelava 12/11/2026 15:44]
 * These are required for SDL3 static linking to succeed.
 */

#pragma comment(lib, "winmm")
#pragma comment(lib, "setupapi")
#pragma comment(lib, "version")

#include <windows.h>

// ImGui
#include <imgui.h>
#include <imgui_impl_opengl3.h>
#include <imgui_impl_sdl3.h>

// SDL3
#include <SDL3/SDL.h>
#include <SDL3/SDL_opengl.h>

SDL_Window* g_window;

int WINAPI WinMain(
    _In_     HINSTANCE hInstance,
    _In_opt_ HINSTANCE hPrevInstance,
    _In_     LPSTR     lpCmdLine,
    _In_     int       nCmdShow
) {
    if (!SDL_Init(SDL_INIT_VIDEO | SDL_INIT_EVENTS)) {
        fwprintf_s(stdout, L"Fault in SDL_Init() - %hs\n", SDL_GetError());
        return 1;
    }

    SDL_GL_SetAttribute(SDL_GL_DOUBLEBUFFER, 1);
    SDL_GL_SetAttribute(SDL_GL_DEPTH_SIZE,   24);
    SDL_GL_SetAttribute(SDL_GL_STENCIL_SIZE, 8);

    SDL_DisplayID main_display_id    = SDL_GetPrimaryDisplay();
    float         main_display_scale = SDL_GetDisplayContentScale(main_display_id);

    if (fpclassify(main_display_scale) == FP_ZERO) {
        fwprintf_s(stdout, L"Fault in SDL_GetDisplayContentScale() - %hs\n", SDL_GetError());
        return 1;
    }

    SDL_WindowFlags sdl_window_flags =
          SDL_WINDOW_OPENGL
        | SDL_WINDOW_RESIZABLE
        | SDL_WINDOW_HIDDEN
        | SDL_WINDOW_HIGH_PIXEL_DENSITY;

    SDL_Window* sdl_window = SDL_CreateWindow(
        "Fahrenheit Setup",
        (int) (1280 * main_display_scale),
        (int) (800  * main_display_scale),
        sdl_window_flags
    );

    if (sdl_window == nullptr) {
        fwprintf_s(stdout, L"Fault in SDL_CreateWindow() - %hs\n", SDL_GetError());
        return 1;
    }

    SDL_WindowID  sdl_window_id = SDL_GetWindowID     (sdl_window);
    SDL_GLContext sdl_context   = SDL_GL_CreateContext(sdl_window);

    if (sdl_context == nullptr) {
        fwprintf_s(stdout, L"Fault in SDL_GL_CreateContext() - %hs\n", SDL_GetError());
        return 1;
    }

    if (!SDL_GL_MakeCurrent(sdl_window, sdl_context)) {
        fwprintf_s(stdout, L"Fault in SDL_GL_MakeCurrent() - %hs\n", SDL_GetError());
        return 1;
    }

    if (!SDL_GL_SetSwapInterval(1)) {
        fwprintf_s(stdout, L"Fault in SDL_GL_SetSwapInterval() - %hs\n", SDL_GetError());
        return 1;
    }

    if (!SDL_SetWindowPosition(
        sdl_window,
        SDL_WINDOWPOS_CENTERED,
        SDL_WINDOWPOS_CENTERED
    )) {
        fwprintf_s(stdout, L"Fault in SDL_SetWindowPosition() - %hs\n", SDL_GetError());
    }

    if (!SDL_ShowWindow(sdl_window)) {
        fwprintf_s(stdout, L"Fault in SDL_ShowWindow() - %hs\n", SDL_GetError());
        return 1;
    }

    ImGuiContext* ctx   = ImGui::CreateContext();
    ImGuiIO&      io    = ImGui::GetIO(); (void)io;
    ImGuiStyle    style = ImGui::GetStyle();

    io.ConfigFlags |= ImGuiConfigFlags_NavEnableKeyboard; // Enable Keyboard Controls
    io.ConfigFlags |= ImGuiConfigFlags_NavEnableGamepad;  // Enable Gamepad Controls

    IMGUI_CHECKVERSION();

    return 0;
}
