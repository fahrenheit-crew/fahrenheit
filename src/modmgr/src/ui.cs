// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Tools.ModManager;

internal static partial class ModManager {
    public static void render() {
        ImGui.ShowDemoWindow();

        ImGuiWindowFlags main_window_flags =
                ImGuiWindowFlags.MenuBar
              | ImGuiWindowFlags.NoDecoration
              | ImGuiWindowFlags.NoMove;

        ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0f);
        ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 0f);

        ImGui.SetNextWindowPos(new(0, 0));
        ImGui.SetNextWindowSize(ImGui.GetMainViewport().WorkSize);

        if (!ImGui.Begin("###Main Window", main_window_flags)) {
            ImGui.PopStyleVar(2);
            ImGui.End();
            return;
        }
        ImGui.PopStyleVar(2);

        _render_menu_bar();

        ImGui.Text("This is a mod manager! :D");

        ImGui.End();
    }

    private static void _render_menu_bar() {
        if (!ImGui.BeginMenuBar()) return;

        if (ImGui.BeginMenu("Mods")) {
            ImGui.MenuItem("Install External Mod");
            ImGui.MenuItem("Import EFL Mod");
            ImGui.Separator();

            ImGui.MenuItem("Import Mod List");
            ImGui.MenuItem("Export Mod List");
            ImGui.Separator();

            ImGui.MenuItem("Import Mod Pack");
            ImGui.MenuItem("Export Mod Pack");

            ImGui.EndMenu();
        }

        if (ImGui.BeginMenu("Play")) {
            ImGui.MenuItem("Final Fantasy X HD Remaster");
            ImGui.MenuItem("Final Fantasy X-2 HD Remaster");
            ImGui.Separator();

            ImGui.MenuItem("Final Fantasy X HD Remaster (for Debugging)");
            ImGui.MenuItem("Final Fantasy X-2 HD Remaster (for Debugging)");

            ImGui.EndMenu();
        }

        ImGui.EndMenuBar();
    }
}
