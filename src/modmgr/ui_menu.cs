// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Tools.ModManager;

internal static unsafe partial class FhModManagerUI {
    private static Vector2 MENU_BAR_FRAME_PADDING => new Vector2(10F, 8F);

    /// <summary>
    ///    Renders the main menu bar at the top of the window.
    ///    "Mods", "Play", "Settings", "About" menus, and their actions.
    /// </summary>
    private static void _render_main_menu() {
        if (!ImGui.BeginMenuBar()) {
            return;
        }


        ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, MENU_BAR_FRAME_PADDING);

        if (ImGui.BeginMenu("Mods")) {
            if (ImGui.MenuItem("Install from File")) {
                _install_mod_from_file();
            }

            if (ImGui.MenuItem("Import EFL Mod")) {
                _show_efl_import_dialog = true;
            }

            ImGui.Separator();

            if (ImGui.MenuItem("Refresh Mod List")) {
                _refresh_mods();
            }

            if (ImGui.MenuItem("Export Mod List")) {
                // currently this is just exporting the 'loadorder' file with extra steps.
                // likely not the desired behavior, but left for now.
                _export_mod_list();
            }

            ImGui.Separator();

            if (ImGui.MenuItem("Import Mod Pack")) {
                _import_mod_pack();
            }

            if (ImGui.MenuItem("Export Mod Pack")) {
                _export_mod_pack();
            }

            ImGui.EndMenu();
        }

        if (ImGui.BeginMenu("Play")) {
            if (ImGui.MenuItem("Final Fantasy X HD Remaster")) {
                _launch_game(FhGameId.FFX);
            }

            if (ImGui.MenuItem("Final Fantasy X-2 HD Remaster")) {
                _launch_game(FhGameId.FFX2);
            }

            if (ImGui.MenuItem("Final Fantasy X-2 HD Remaster Last Mission")) {
                _launch_game(FhGameId.FFX2, ["FFX2_LASTMISSION"]);
            }

            ImGui.Separator();

            if (ImGui.MenuItem("Final Fantasy X HD Remaster (Debug)")) {
                _launch_game(FhGameId.FFX, ["--debug"]);
            }

            if (ImGui.MenuItem("Final Fantasy X-2 HD Remaster (Debug)")) {
                _launch_game(FhGameId.FFX2, ["--debug"]);
            }

            if (ImGui.MenuItem("Final Fantasy X-2 HD Remaster Last Mission (Debug)")) {
                _launch_game(FhGameId.FFX2, ["FFX2_LASTMISSION", "--debug"]);
            }

            ImGui.EndMenu();
        }

        if (ImGui.MenuItem("Settings")) {
            _show_settings_dialog = true;
        }

        Vector2 menu_cluster_max = ImGui.GetItemRectMax();

        // todo - fill out with menu items or remove
        ImGui.MenuItem("About");
        ImGui.PopStyleVar();
        ImGui.EndMenuBar();
    }

    /// <summary>
    ///    Opens and renders any modal popups that have been requested by the menu bar.
    /// </summary>
    private static void _handle_modals() {
        if (_show_efl_import_dialog) {
            ImGui.OpenPopup("Import EFL Mod");
        }

        if (_show_settings_dialog) {
            ImGui.OpenPopup("Settings");
        }

        _render_efl_import_modal();
        _render_settings_modal();
    }
}
