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

bool vc_x86  = bridge_check_vcredist_x86();
bool vc_x64  = bridge_check_vcredist_x64();
bool net_x86 = bridge_check_nethost_x86();
bool net_x64 = bridge_check_nethost_x64();

void bridge_ui() {
    ImGui::ShowDemoWindow();
}
