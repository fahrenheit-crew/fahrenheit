// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Tools.ModManager;

internal static unsafe partial class FhModManagerUI {
    private static readonly FhModManagerSettings _settings;

    private static string       _game_directory_input;
    private static FhModCatalog _catalog;

    static FhModManagerUI() {
        _settings = FhModManagerSettingsStore.load(out string settings_warning);

        _game_directory_input = _settings.GameDirectory;
        _catalog = FhModScanner.scan(_settings.GameDirectory);
        _set_status(settings_warning);
    }

    public static void UI() {
        _handle_modals();

        ImGuiViewportPtr viewport = ImGui.GetMainViewport();

        ImGui.SetNextWindowPos(viewport.WorkPos);
        ImGui.SetNextWindowSize(viewport.WorkSize);

        ImGuiWindowFlags window_flags =
          ImGuiWindowFlags.MenuBar
        | ImGuiWindowFlags.NoTitleBar
        | ImGuiWindowFlags.NoResize
        | ImGuiWindowFlags.NoMove
        | ImGuiWindowFlags.NoCollapse
        | ImGuiWindowFlags.NoSavedSettings
        | ImGuiWindowFlags.NoScrollbar
        | ImGuiWindowFlags.NoScrollWithMouse;

        if (!ImGui.Begin("Fahrenheit Mod Manager###FhModManager", window_flags)) {
            ImGui.End();
            return;
        }

        _render_main_menu();
        ImGui.Spacing();
        _render_warnings();
        ImGui.Spacing();
        ImGui.Separator();
        ImGui.Spacing();
        _render_mod_lists();
        _render_status_bar();
        ImGui.End();
        _apply_pending_mod_toggle();
        _apply_pending_load_order_move();
        _apply_pending_preview_move();
        _apply_pending_load_order_drop();
    }

    private static void _render_warnings() {
        if (_catalog.Warnings.Count == 0) {
            return;
        }

        ImGui.Spacing();

        Vector4 warning_color = ImGui.GetStyle().Colors[(int)ImGuiCol.DragDropTarget];

        foreach (string warning in _catalog.Warnings) {
            ImGui.TextColored(warning_color, warning);
        }
    }
}
