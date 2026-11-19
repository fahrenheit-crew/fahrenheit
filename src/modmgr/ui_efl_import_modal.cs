// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Tools.ModManager;

internal static unsafe partial class FhModManagerUI {
    private const   int     MOD_ID_INPUT_LENGTH = 128;
    private static  bool    _show_efl_import_dialog;
    private static  int     _efl_import_game_index;
    private static  string  _efl_import_mod_id       = "";
    private static  string  _efl_import_mod_name     = "";
    private static  string  _efl_import_source_folder = "";

    /// <summary>
    ///    Renders the "Import EFL Mod" modal dialog for importing a loose file tree as a VBF archive.
    ///    Enables user to create a mod manifest.
    ///    User prompted to select a game, mod ID, optional mod name, and source folder.
    /// </summary>
    private static void _render_efl_import_modal() {
        _center_next_window(width_fraction: 0.25F, min_width: 480F, max_width: 700F);

        if (!ImGui.BeginPopupModal("Import EFL Mod", ImGuiWindowFlags.NoResize)) {
            return;
        }

        _text_wrapped("Import a loose file tree already laid out like a VBF archive (e.g. FFX_Data/ffx_ps2/...).");

        ImGui.Spacing();

        ImGui.Text("Game");
        ImGui.RadioButton("Final Fantasy X##EflGame", ref _efl_import_game_index, 0);
        ImGui.SameLine();
        ImGui.RadioButton("Final Fantasy X-2##EflGame", ref _efl_import_game_index, 1);

        ImGui.Spacing();

        ImGui.Text("Mod ID");
        ImGui.SetNextItemWidth(-1F);
        ImGui.InputText("##EflImportModId", ref _efl_import_mod_id, MOD_ID_INPUT_LENGTH);
        ImGui.Text("Mod name (optional, defaults to the ID)");
        ImGui.SetNextItemWidth(-1F);
        ImGui.InputText("##EflImportModName", ref _efl_import_mod_name, MOD_ID_INPUT_LENGTH);

        ImGui.Spacing();

        ImGui.Text("Source folder");

        _text_disabled_wrapped(string.IsNullOrWhiteSpace(_efl_import_source_folder) ? "(not selected)" : _efl_import_source_folder);

        if (ImGui.Button("Browse##EflImportSource")) {
            DialogResult result = Dialog.FolderPicker(_efl_import_source_folder);

            if (result.IsOk && result.Path != null) {
                _efl_import_source_folder = result.Path;
            }
        }

        ImGui.Spacing();
        ImGui.Separator();
        ImGui.Spacing();

        bool can_import = !string.IsNullOrWhiteSpace(_efl_import_mod_id) && !string.IsNullOrWhiteSpace(_efl_import_source_folder);

        if (!can_import) {
            ImGui.BeginDisabled();
        }

        if (ImGui.Button("Import")) {
            _import_efl_mod();
        }

        if (!can_import) {
            ImGui.EndDisabled();
        }

        ImGui.SameLine();

        if (ImGui.Button("Cancel")) {
            _show_efl_import_dialog = false;
            ImGui.CloseCurrentPopup();
        }

        ImGui.EndPopup();
    }

    /// todo - add validation logic to ensure source folder adheres to supported structure.
    /// <summary>
    ///    Performs the import of the loose assets.
    /// </summary>
    private static void _import_efl_mod() {
        FhGameId game   = _efl_import_game_index == 1 ? FhGameId.FFX2 : FhGameId.FFX;
        string mod_id   = _efl_import_mod_id.Trim();
        string mod_name = string.IsNullOrWhiteSpace(_efl_import_mod_name) ? mod_id : _efl_import_mod_name.Trim();

        ResultsMessage result = FhEflImporter.import(
            _catalog.ModsDirectory,
            mod_id,
            mod_name,
            game,
            _efl_import_source_folder);

        _set_status(result.Message, !result.Success);

        // Leave the dialog open on failure so the user can fix the input without
        // retyping everything (e.g. picking a different mod ID).
        if (!result.Success) {
            return;
        }

        _show_efl_import_dialog    = false;
        _efl_import_game_index     = 0;
        _efl_import_mod_id         = "";
        _efl_import_mod_name       = "";
        _efl_import_source_folder  = "";

        _rescan_mods();
        ImGui.CloseCurrentPopup();
    }
}
