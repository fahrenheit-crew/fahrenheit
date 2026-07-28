// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Tools.ModManager;

internal static partial class ModManager {
    private static void _render_menu_bar() {
        if (!ImGui.BeginMenuBar()) return;

        if (ImGui.BeginMenu("Mods")) {
            ImGui.MenuItem("Install External Mod");
            ImGui.MenuItem("Import EFL Mod");
            ImGui.Separator();

            ImGui.MenuItem("Import Mod Pack");
            ImGui.MenuItem("Export Mod Pack");

            ImGui.EndMenu();
        }

        if (ImGui.BeginMenu("Play")) {
            ImGui.MenuItem("Final Fantasy X");
            ImGui.MenuItem("Final Fantasy X-2");
            ImGui.MenuItem("Final Fantasy X-2 Last Mission");

            if (Config.show_advanced_options) {
                ImGui.Separator();

                ImGui.MenuItem("Final Fantasy X (for Debugging)");
                ImGui.MenuItem("Final Fantasy X-2 HD (for Debugging)");
                ImGui.MenuItem("Final Fantasy X-2 Last Mission (for Debugging)");
            }

            ImGui.EndMenu();
        }

        if (ImGui.BeginMenu("Manager")) {
            ImGui.MenuItem("About");

            if (ImGui.MenuItem("Settings") && !_show_config_window) {
                show_config();
            }

            ImGui.EndMenu();
        }

        ImGui.EndMenuBar();
    }
}
