// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Tools.ModManager;

internal static unsafe partial class FhModManagerUI {
    private const int GAME_DIRECTORY_INPUT_LENGTH = 1024;
    private static bool _show_settings_dialog;

    /// <summary>
    ///    Renders the "Settings" modal, which allows the user to set the game directory.
    /// </summary>
    private static void _render_settings_modal() {
        _center_next_window(width_fraction: 0.42F, min_width: 900F, max_width: 1300F);

 
        bool modal_open = true;
        bool popup_open = ImGui.BeginPopupModal("Settings", ref modal_open, ImGuiWindowFlags.NoResize);


        if (!modal_open) {
            _show_settings_dialog = false;
        }

        if (!popup_open) {
            return;
        }

        if (ImGui.IsMouseClicked(ImGuiMouseButton.Left)
            && !ImGui.IsWindowHovered(ImGuiHoveredFlags.RootAndChildWindows | ImGuiHoveredFlags.AllowWhenBlockedByPopup)) {
            _show_settings_dialog = false;
            ImGui.CloseCurrentPopup();
        }

        _render_game_directory_row();
        ImGui.EndPopup();
    }

    /// <summary>
    ///    Renders the row in the settings modal that allows the user to input or browse for the game directory.
    /// </summary>
    private static void _render_game_directory_row() {
        ImGui.SeparatorText("FF X/X-2 HD Remaster Location");

        bool    valid;
        string? reason;

        try {
            string normalized = FhModManagerSettingsStore.normalize_path(_game_directory_input);
            (valid, reason) = _check_game_location(normalized);
        }
        catch (Exception exception) {
            valid  = false;
            reason = exception.Message;
        }

        _status_icon(valid, reason);
        ImGui.SameLine();

        float browse_width  = _get_button_width("Browse");
        float open_width    = _get_button_width("Open");
        float buttons_width = browse_width + open_width + (ImGui.GetStyle().ItemSpacing.X * 2F);

        ImGui.SetNextItemWidth(ImGui.GetContentRegionAvail().X - buttons_width);

        bool submitted = ImGui.InputText("##GameLocationInput", ref _game_directory_input, GAME_DIRECTORY_INPUT_LENGTH, ImGuiInputTextFlags.EnterReturnsTrue);

        ImGui.SameLine();

        bool browse_pressed = ImGui.Button("Browse##Game", new Vector2(browse_width, 0F));

        ImGui.SameLine();

        bool open_pressed = ImGui.Button("Open##Game", new Vector2(open_width, 0F));

        if (browse_pressed) {
            _browse_game_directory();
        }
        else if (submitted) {
            _save_game_directory();
        }
        else if (open_pressed) {
            _open_folder(_game_directory_input);
        }
    }

    /// <summary>
    ///    Checks if the given game directory is valid by verifying the existence of the directory and a known executable.
    /// </summary>
    private static (bool IsValid, string? Reason) _check_game_location(string normalized_game_directory) {
        if (!Directory.Exists(normalized_game_directory)) {
            return (false, "This folder does not exist.");
        }

        if (!File.Exists(Path.Join(normalized_game_directory, "FFX&X-2_LAUNCHER.exe"))) {
            return (false, "FFX&X-2_LAUNCHER.exe was not found here.");
        }

        return (true, null);
    }

    /// <summary>
    ///    Opens a folder picker dialog to select the game directory. If a valid directory is selected, it saves the new location.
    /// </summary>
    private static void _browse_game_directory() {
        DialogResult result = Dialog.FolderPicker(_game_directory_input);

        if (!result.IsOk || result.Path == null) {
            return;
        }

        _game_directory_input = result.Path;
        _save_game_directory();
    }

    /// <summary>
    ///    Saves the game directory to the settings store and rescans mods if successful.
    /// </summary>
    private static void _save_game_directory() {
        try {
            string normalized       = FhModManagerSettingsStore.normalize_path(_game_directory_input);
            _game_directory_input   = normalized;
            _settings.GameDirectory = normalized;

            if (!FhModManagerSettingsStore.try_save( _settings, out string save_error)) {
                _set_status(save_error,true);
                return;
            }

            _rescan_mods();
            _set_status("Game location saved.");
        }
        catch (Exception exception) {
            _set_status($"The game location is invalid.\n\n{exception.Message}", true);
        }
    }
}
