// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

/* [fkelava 15/8/25 17:41]
 * Place the setup UI in this method. The program stub will invoke it.
 */

#include <fhsetup.h>

// ImGui
#include <imgui.h>

const char* g_selected_dir; // The directory the user provides as the game directory.

bool g_have_vc_x86;  // Whether a valid x86 VC++ Redistributable was detected on the last scan.
bool g_have_vc_x64;  // Whether a valid x86 VC++ Redistributable was detected on the last scan.
bool g_have_net_x86; // Whether a valid x86 .NET Runtime was detected on the last scan.
bool g_have_net_x64; // Whether a valid x64 .NET Runtime was detected on the last scan.


// The folder select dialog callback.
static void SDLCALL setup_ui_dlg_cb(
    void*              userdata, // [in] An app-provided pointer, for the callback's use. Always NULL.
    const char* const* filelist, // [in] The file(s) chosen by the user.
    int                filter    // [in] Index of the selected filter. In our case, zero, because we use none.
) {
    if (filelist == nullptr) {
        fwprintf_s(stdout, L"Fault in dialog callback - %hs\n", SDL_GetError());
        return;
    }

    if (*filelist == nullptr) {
        fwprintf_s(stdout, L"No folder selected. The dialog was canceled.\n");
        return;
    }

    g_selected_dir = *filelist;
}

void setup_ui_rescan() {
    g_have_vc_x86  = setup_check_vcredist(X86);
    g_have_vc_x64  = setup_check_vcredist(X64);
    g_have_net_x86 = setup_check_nethost (X86);
    g_have_net_x64 = setup_check_nethost (X64);
}

void setup_ui_main() {
    ImGui::ShowDemoWindow();

    if (!ImGui::Begin("WOW")) {
        ImGui::End();
        return;
    }

    ImGui::BeginDisabled();

    ImGui::Checkbox("VC++ x86", &g_have_vc_x86);
    ImGui::Checkbox("VC++ x64", &g_have_vc_x64);
    ImGui::Checkbox(".NET x86", &g_have_net_x86);
    ImGui::Checkbox(".NET x64", &g_have_net_x64);

    ImGui::EndDisabled();

    if (ImGui::Button("Rescan", { 100, 100 } )) {
        setup_ui_rescan();
    }

    if (ImGui::Button("Dialog", { 100, 100 } )) {
        SDL_ShowOpenFolderDialog(
            &setup_ui_dlg_cb,
            nullptr,
            g_sdl_window_ptr,
            nullptr,
            false
        );
    }

    ImGui::End();
}
