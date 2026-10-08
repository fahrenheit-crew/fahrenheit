// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Tools.ModManager;

internal static unsafe partial class FhModManagerUI {
    private static void _refresh_mods() {
        try {
            string normalized = FhModManagerSettingsStore.normalize_path(_game_directory_input);

            _catalog = FhModScanner.scan(normalized);

            _set_status("Mod list refreshed.");
        }
        catch (Exception exception) {
            _set_status($"The mod list could not be refreshed.\n\n{exception.Message}", true);
        }
    }

    private static void _open_folder(string path) {
        if (!FhShell.try_open_folder(path, out string error)) {
            _set_status(error, true);
        }
    }

    // todo:
    //  Stubbed - only opens the file picker for now.
    ///<summary>
    ///    Opens a file picker to select a compressed mod file. into the mods directory.
    ///    If the mod is not already installed, it will be extracted into a subdirectory named
    ///    after the mod's manifest name, or the file name if no manifest is present.
    /// </summary>
    private static void _install_mod_from_file() {
        Dialog.FileOpen(string.Empty, string.Empty);
    }

    /// <summary>
    ///     Exports current list of enabled mods to a typeless file; one per line.
    ///     Mod must have a name in the manifest to be included in the export.
    /// </summary>
    private static void _export_mod_list() {
        DialogResult result = Dialog.FileSave(string.Empty, string.Empty);

        if (!result.IsOk || result.Path == null) {
            return;
        }

        try {
            List<string> lines = [];

            foreach (FhInstalledMod mod in _catalog.Enabled) {
                if (string.IsNullOrEmpty(mod.Manifest.Name)) {
                    continue;
                }
                lines.Add(mod.Manifest.Name);
            }

            string contents = string.Join(Environment.NewLine, lines);

            if (lines.Count > 0) {
                contents += Environment.NewLine;
            }

            File.WriteAllText(result.Path, contents);

            _set_status("Mod list exported.");
        }
        catch (Exception exception) {
            _set_status($"The mod list could not be exported.\n\n{exception.Message}", true);
        }
    }

    // todo:
    //  Validation logic on contents
    //  Support file types (zip, rar, 7z, tar.gz)?
    /// <summary>
    ///    Imports a list of enabled mods from a typeless file; one per line.
    ///    Move any net new mods that are not in the current mods directory.
    /// </summary>
    private static void _import_mod_pack() {
        DialogResult result = Dialog.FileOpen("zip", string.Empty);

        if (!result.IsOk || result.Path == null) {
            return;
        }

        ResultsMessage pack_result = FhModPackImporter.import(_catalog.ModsDirectory, _catalog.LoadOrderPath, result.Path);

        _set_status(pack_result.Message, !pack_result.Success);

        if (pack_result.Success) {
            _rescan_mods();
        }
    }

    /// <summary>
    ///   Exports current list of enabled mods' as zip file.
    ///   File includes all mod directories with a loadorder file.
    /// </summary>
    private static void _export_mod_pack() {
        DialogResult result = Dialog.FileSave("zip", string.Empty);

        if (!result.IsOk || result.Path == null) {
            return;
        }

        // ensure the file has a .zip extension, since the file picker doesn't enforce it
        string destination_path = result.Path.EndsWith(".zip", StringComparison.OrdinalIgnoreCase) ? result.Path : $"{result.Path}.zip";

        ResultsMessage pack_result = FhModPackExporter.export(destination_path, _catalog.Enabled);
        _set_status(pack_result.Message, !pack_result.Success);
    }

    /// <summary>
    ///     Launches the specified target executable with optional arguments.
    /// </summary>
    private static void _launch_game(FhGameId target, string[]? args = null) {
        ResultsMessage result = FhGameLauncher.launch(target, _game_directory_input, args ?? []);
        _set_status(result.Message, !result.Success);
    }

    /// <summary>
    ///     Takes a pending action from the specified field, clearing it and returning the action.
    /// </summary>
    private static bool _take_pending<T>(ref T? pending, out T action) where T : class {
        if (pending == null) {
            action = null!;
            return false;
        }

        action = pending;
        pending = null;

        return true;
    }

    /// <summary>
    ///     Applies the result of a load order modification.
    ///     Rescans loadorder file afterwards and sets the status bar message.
    /// </summary>
    private static void _apply_load_order_result(bool success, string error, string success_message) {
        if (!success) {
            _set_status(error, true);
            return;
        }

        _rescan_mods();
        _set_status(success_message);
    }

    /// <summary>
    ///    Applies the pending mod toggle action queued this frame by ui_mod_list.cs, if any.
    /// </summary>
    private static void _apply_pending_mod_toggle() {
        if (!_take_pending(ref _pending_mod_toggle, out FhPendingModToggle action)) {
            return;
        }

        bool success = FhLoadOrderEditor.try_set_enabled(_catalog.LoadOrderPath, action.Mod, action.Enable, out string error);

        _apply_load_order_result(success, error, action.Enable ? $"Enabled {action.Mod.Manifest.Name}." : $"Disabled {action.Mod.Manifest.Name}.");
    }

    /// <summary>
    ///   Applies the pending mod move action queued this frame by ui_mod_list.cs, if any.
    /// </summary>
    private static void _apply_pending_load_order_move() {
        if (!_take_pending(ref _pending_load_order_move, out FhPendingLoadOrderMove action)) {
            return;
        }

        bool   success        = FhLoadOrderEditor.try_move(_catalog.LoadOrderPath, action.Mod, action.Direction, out string error);
        string direction_name = action.Direction < 0 ? "up" : "down";

        _apply_load_order_result(success, error, $"Moved {action.Mod.Manifest.Name} {direction_name}.");
    }

    /// <summary>
    ///  Applies the pending mod preview move action queued this frame by ui_drag_handle.cs, if any.
    /// </summary>
    private static void _apply_pending_preview_move() {
        if (!_take_pending(ref _pending_preview_move, out FhPendingPreviewMove action)) {
            return;
        }

        if (action.FromIndex < 0 || action.FromIndex >= _catalog.Enabled.Count
            || action.ToIndex < 0 || action.ToIndex >= _catalog.Enabled.Count) {
            return;
        }

        List<FhInstalledMod> reordered = [.. _catalog.Enabled];

        FhInstalledMod moved = reordered[action.FromIndex];
        reordered.RemoveAt(action.FromIndex);
        reordered.Insert(action.ToIndex, moved);

        _catalog = _catalog with { Enabled = reordered };
    }

    /// <summary>
    ///  Applies the pending mod drop action queued this frame by ui_drag_handle.cs, if any.
    /// </summary>
    private static void _apply_pending_load_order_drop() {
        if (!_take_pending(ref _pending_load_order_drop, out FhPendingLoadOrderDrop action)) {
            return;
        }

        bool success = FhLoadOrderEditor.try_move_to(_catalog.LoadOrderPath, action.Mod, action.TargetIndex, out string error);
        _apply_load_order_result(success, error, $"Moved {action.Mod.Manifest.Name}.");
    }
}
