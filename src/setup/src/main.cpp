// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

/* [fkelava 12/11/26 14:39]
 * This is the generic tool program stub. Do not edit this unless you encounter a bug.
 * To edit the tool's UI, please open `ui.cpp`.
 *
 * Mostly a verbatim copy of the SDL3/OGL3 example. Unlike the C# equivalent, here
 * we have the option of following the example more closely.
 */

#pragma comment(lib, "winmm")    // Required for SDL3 static linking.
#pragma comment(lib, "setupapi") // Required for SDL3 static linking.
#pragma comment(lib, "version")  // Required for SDL3 static linking.
#pragma comment(lib, "opengl32") // Required for OGL3 linking.

#include <fhsetup.h>

// Win32
#include <windows.h>

// ImGui
#include <imgui.h>
#include <imgui_impl_opengl3.h>
#include <imgui_impl_sdl3.h>

// SDL3
#include <SDL3/SDL.h>
#include <SDL3/SDL_opengl.h>

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

    IMGUI_CHECKVERSION();

    ImGuiContext* ctx   = ImGui::CreateContext();
    ImGuiIO&      io    = ImGui::GetIO(); (void)io;
    ImGuiStyle    style = ImGui::GetStyle();

    io.ConfigFlags |= ImGuiConfigFlags_NavEnableKeyboard; // Enable Keyboard Controls
    io.ConfigFlags |= ImGuiConfigFlags_NavEnableGamepad;  // Enable Gamepad Controls
    io.ConfigFlags |= ImGuiConfigFlags_DockingEnable;
    io.ConfigFlags |= ImGuiConfigFlags_ViewportsEnable;

    io.IniFilename = nullptr; // Do not save any settings.

    ImGui::StyleColorsDark();

    if (!ImGui_ImplSDL3_InitForOpenGL(sdl_window, sdl_context)) {
        fwprintf_s(stdout, L"Fault in ImGui_ImplSDL3_InitForOpenGL().\n");
        return 1;
    }

    if (!ImGui_ImplOpenGL3_Init()) {
        fwprintf_s(stdout, L"Fault in ImGui_ImplOpenGL3_Init().\n");
        return 1;
    }

    bool      quit = false;
    SDL_Event event;

    while (!quit) {
        // Poll and handle events (inputs, window resize, etc.)
        while (SDL_PollEvent(&event)) {
            ImGui_ImplSDL3_ProcessEvent(&event);

            quit = event.type == SDL_EVENT_QUIT ||
                   event.type == SDL_EVENT_WINDOW_CLOSE_REQUESTED && event.window.windowID == sdl_window_id;
        }

        if (SDL_GetWindowFlags(sdl_window) & SDL_WINDOW_MINIMIZED) {
            SDL_Delay(10);
            continue;
        }

        // Start the Dear ImGui frame
        ImGui_ImplOpenGL3_NewFrame();
        ImGui_ImplSDL3_NewFrame();

        ImGui::NewFrame();

        // Draw the actual setup UI
        bridge_ui();

        // Rendering
        ImGui::Render();

        glViewport(
            0,
            0,
            (int) io.DisplaySize.x,
            (int) io.DisplaySize.y
        );

        glClearColor(
            0.45F,
            0.55F,
            0.60F,
            1.00F
        );

        glClear(GL_COLOR_BUFFER_BIT);

        ImGui_ImplOpenGL3_RenderDrawData(ImGui::GetDrawData());

        // Update and Render additional Platform Windows
        // (Platform functions may change the current OpenGL context, so we save/restore it to make it easier to paste this code elsewhere.)
        if (io.ConfigFlags & ImGuiConfigFlags_ViewportsEnable)
        {
            SDL_Window*   current_window  = SDL_GL_GetCurrentWindow();
            SDL_GLContext current_context = SDL_GL_GetCurrentContext();

            ImGui::UpdatePlatformWindows();
            ImGui::RenderPlatformWindowsDefault();

            if (!SDL_GL_MakeCurrent(current_window, current_context)) {
                fwprintf_s(stdout, L"Fault in SDL_GL_MakeCurrent() - %hs\n", SDL_GetError());
                return 1;
            }
        }

        if (!SDL_GL_SwapWindow(sdl_window)) {
            fwprintf_s(stdout, L"Fault in SDL_GL_SwapWindow() - %hs\n", SDL_GetError());
            return 1;
        }
    }

    ImGui_ImplOpenGL3_Shutdown();
    ImGui_ImplSDL3_Shutdown();

    ImGui::DestroyContext();

    SDL_GL_DestroyContext(sdl_context);
    SDL_DestroyWindow    (sdl_window);
    SDL_Quit();

    return 0;
}
