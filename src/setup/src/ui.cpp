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

bool vc_x86;
bool vc_x64;
bool net_x86;
bool net_x64;

void setup_ui_rescan() {
    vc_x86  = setup_check_vcredist(X86);
    vc_x64  = setup_check_vcredist(X64);
    net_x86 = setup_check_nethost (X86);
    net_x64 = setup_check_nethost (X64);
}

void setup_ui_main() {
    ImGui::ShowDemoWindow();

    if (!ImGui::Begin("WOW")) {
        ImGui::End();
        return;
    }

    ImGui::BeginDisabled();

    ImGui::Checkbox("VC++ x86", &vc_x86);
    ImGui::Checkbox("VC++ x64", &vc_x64);
    ImGui::Checkbox(".NET x86", &net_x86);
    ImGui::Checkbox(".NET x64", &net_x64);

    ImGui::EndDisabled();

    if (ImGui::Button("Rescan", { 100, 100 } )) {
        setup_ui_rescan();
    }

    ImGui::End();
}
