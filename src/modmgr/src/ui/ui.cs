// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Tools.ModManager;

internal static partial class ModManager {
    internal static List<string> debug_log = [];

    public static void render() {
        // ImGui.ShowDemoWindow();

        ImGuiWindowFlags main_window_flags =
                ImGuiWindowFlags.MenuBar
              | ImGuiWindowFlags.NoDecoration
              | ImGuiWindowFlags.NoMove
              | ImGuiWindowFlags.NoBringToFrontOnFocus;

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

        ImGui.Text("Installed mods:");
        ImGui.Indent();
        render_modlist();
        ImGui.Unindent();

        ImGui.Text("Log:");
        ImGui.Indent();
        foreach (string line in debug_log) {
            ImGui.Text(line);
        }
        ImGui.Unindent();

        ImGui.End();

        // Additional windows
        render_config();
    }
}
