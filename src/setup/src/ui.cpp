// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

#include <fhsetup.h>

// Win32
#include <pathcch.h>

// ImGui
#include <imgui.h>

const ImVec4 g_col_green = ImVec4(0.2f, 1.0f, 0.2f, 1.0f);
const ImVec4 g_col_red   = ImVec4(1.0f, 0.2f, 0.2f, 1.0f);

const SDL_DialogFileFilter g_game_filters[] = {
    { "Game Executables", "exe" }
};

// The folder select dialog callback.
static void SDLCALL setup_ui_dialog_cb(
    void*              userdata, // [in] An app-provided pointer, for the callback's use. Always NULL.
    const char* const* filelist, // [in] The file(s) chosen by the user.
    int                filter    // [in] Index of the selected filter. In our case, zero, because we use none.
) {
    if (filelist == nullptr) {
        fwprintf_s(stderr, L"Fault in dialog callback - %hs\n", SDL_GetError());
        return;
    }

    if (*filelist == nullptr) {
        fwprintf_s(stdout, L"No folder selected. The dialog was canceled.\n");
        return;
    }

    wchar_t path[MAX_PATH] = { 0 };

    if (MultiByteToWideChar(CP_UTF8, 0, *filelist, -1, path, MAX_PATH) == 0) {
        fwprintf_s(stderr, L"MultiByteToWideChar() failed with code 0x%X.\n", GetLastError());
        return;
    }

    HRESULT hr = PathCchRemoveFileSpec(path, MAX_PATH);
    if (hr != S_OK) {
        fwprintf_s(stderr, L"PathCchRemoveFileSpec() failed for path %s, error code: %X\n", path, hr);
        return;
    }

    g_check_game = setup_check_gamedir(path);
}

static void setup_ui_status(
    const char* label,
    const char* status,
    ImVec4      status_color
) {
    ImGui::Text       (label);
    ImGui::SameLine   (130);
    ImGui::TextColored(status_color, status);
}

static void setup_ui_flag(
    const char*           label,
          FH_CHECK_RESULT check_result
) {
    const char*  status = "NOT FOUND";
          ImVec4 color  = g_col_red;

    if (check_result == SUCCESS) {
        status = "FOUND";
        color  = g_col_green;
    }

    setup_ui_status(label, status, color);
}

void setup_ui_main() {
    ImGuiViewport* viewport = ImGui::GetMainViewport();
    ImGuiStyle&    style    = ImGui::GetStyle();

    ImGui::SetNextWindowPos (viewport->WorkPos);
    ImGui::SetNextWindowSize(viewport->WorkSize);

    if (!ImGui::Begin(
        "Fahrenheit Setup Helper",
        nullptr,
        ImGuiWindowFlags_NoDecoration
    )) {
        ImGui::End();
        return;
    }

    setup_ui_flag("VC++ x86:", g_check_vc_x86);
    setup_ui_flag("VC++ x64:", g_check_vc_x64);

    setup_ui_flag(".NET Runtime x86:", g_check_net_x86);
    setup_ui_flag(".NET Runtime x64:", g_check_net_x64);

    const char* game_label  = "Game:";
    const char* game_status = "NOT FOUND";

    switch (g_check_game) {
        case SUCCESS:        game_status = "ALL GOOD";           break;
        case MISSING_BIN:    game_status = "MISSING EXECUTABLE"; break;
        case INVALID_BIN:    game_status = "INVALID EXECUTABLE"; break;
        case MISSING_VBF:    game_status = "MISSING DATA";       break;
        case INTERNAL_ERROR: game_status = "INTERNAL ERROR";     break;
    }

    setup_ui_status(
        game_label,
        game_status,
        g_check_game == SUCCESS ? g_col_green : g_col_red
    );

    float  btn_size_x = 300 - style.WindowPadding.x * 2;
    float  btn_size_y = 26;
    ImVec2 btn_size   = ImVec2(btn_size_x, btn_size_y);

    if (ImGui::Button("Rescan", btn_size)) {
        setup_check_rescan();
    }

    if (ImGui::Button("Locate Game", btn_size)) {
        SDL_ShowOpenFileDialog(
            &setup_ui_dialog_cb,
            nullptr,
            g_sdl_window_ptr,
            g_game_filters,
            1,
            nullptr,
            false
        );
    }

    ImGui::End();
}
