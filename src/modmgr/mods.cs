// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Tools.ModManager;


internal sealed record FhInstalledMod(
    FhManifest Manifest,
    string DirectoryPath,
    bool DirectoryExists,
    bool ManifestExists,
    string ManifestError,
    int? LoadOrderIndex) {
    internal bool IsEnabled         => LoadOrderIndex.HasValue;
    internal bool HasValidManifest  => DirectoryExists && ManifestExists && string.IsNullOrWhiteSpace(ManifestError);
}

/// <summary>
///    Lists of enabled and disabled mods, plus any warnings encountered while scanning the game directory.
/// </summary>
internal sealed record FhModCatalog(
    IReadOnlyList<FhInstalledMod> Enabled,
    IReadOnlyList<FhInstalledMod> Disabled,
    IReadOnlyList<string> Warnings,
    string ModsDirectory,
    string LoadOrderPath);


internal static class FhModScanner {
    /// <summary>
    ///    Scans the game directory for installed mods and returns a catalog of them.
    /// </summary>
    internal static FhModCatalog scan(string game_directory) {
        List<FhInstalledMod> enabled    = [];
        List<FhInstalledMod> disabled   = [];
        List<string> warnings           = [];

        string normalized_game_directory;

        try {
            normalized_game_directory = FhModManagerSettingsStore.normalize_path(game_directory);
        }
        catch (Exception exception) {
            warnings.Add($"The game directory is invalid: {exception.Message}");
            return new(enabled, disabled, warnings, "", "");
        }

        string fahrenheit_directory = Directory.GetParent(AppContext.BaseDirectory)!.FullName;
        string mods_directory       = Path.Join(fahrenheit_directory, "mods");

        if (!Directory.Exists(mods_directory) && Directory.Exists(Path.Join(game_directory, "fahrenheit", "mods"))) {
            warnings.Add("The Fahrenheit mods directory was not found in the application directory. Checking the game directory instead.");
            warnings.Add("If you installed Fahrenheit to a custom location, please move the bin directory to 'game directory/fahrenheit'");
            mods_directory = Path.Join(game_directory, "fahrenheit", "mods");
        }

        string load_order_path = Path.Join(mods_directory, "loadorder");

        // Check for missing directories.
        bool missing_directory_warning = false;

        if (!Directory.Exists(normalized_game_directory)) {
            missing_directory_warning = true;
            warnings.Add($"The game directory does not exist:\n{normalized_game_directory}");
        }
        if (!Directory.Exists(fahrenheit_directory)) {
            missing_directory_warning = true;
            warnings.Add($"Fahrenheit is not installed at:\n{fahrenheit_directory}");
        }
        if (!Directory.Exists(mods_directory)) {
            missing_directory_warning = true;
            warnings.Add($"The Fahrenheit mods directory does not exist:\n{mods_directory}");
        }

        // Return early if any of the directories are missing.
        if (missing_directory_warning) {
            return new(enabled, disabled, warnings, mods_directory, load_order_path);
        }

        string[] raw_load_order = [];

        if (File.Exists(load_order_path)) {
            try {
                raw_load_order = File.ReadAllLines(load_order_path);
            }
            catch (Exception exception) {
                warnings.Add($"The load order could not be read:\n{exception.Message}");
            }
        }
        else {
            warnings.Add($"The load order file does not exist:\n{load_order_path}");
        }

        HashSet<string> enabled_ids = new(StringComparer.OrdinalIgnoreCase);

        // Read the load order file into the enabled list, surfacing any issues as warnings.
        for (int index = 0; index < raw_load_order.Length; index++) {
            string raw_mod_id = raw_load_order[index];

            if (string.IsNullOrWhiteSpace(raw_mod_id)) {
                warnings.Add($"Load-order line {index + 1} is empty. Every line should contain a mod ID.");
                continue;
            }

            string mod_id = raw_mod_id.Trim();

            if (enabled_ids.Contains(mod_id)) {
                warnings.Add($"Duplicate '{mod_id}' entries in the load order. Edits made here will purge the duplicate entry.");
                continue;
            }

            enabled_ids.Add(mod_id);
            enabled.Add(_read_mod(mods_directory, mod_id, index));
        }

        IEnumerable<string> mod_directories = Directory.EnumerateDirectories( mods_directory, "*", SearchOption.TopDirectoryOnly);

        // Any mod directories not already in the enabled list default to the disabled list.
        foreach (string mod_directory in mod_directories) {
            string mod_id = Path.GetFileName(mod_directory);

            if (enabled_ids.Contains(mod_id)) {
                continue;
            }

            disabled.Add(_read_mod(mods_directory, mod_id, null));
        }

        disabled.Sort(static (left, right) => StringComparer.OrdinalIgnoreCase.Compare(left.Manifest.Name, right.Manifest.Name));

        return new(enabled, disabled, warnings, mods_directory, load_order_path);
    }

    /// <summary>
    ///    Loads a mod's manifest and returns an FhInstalledMod record. 
    ///    If the mod directory or manifest is missing or malformed, the returned record will report error details.
    /// </summary>
    private static FhInstalledMod _read_mod(string mods_directory, string mod_id, int? load_order_index) {
        string mod_directory = Path.Join(mods_directory, mod_id);
        // guard against mod directory not existing after it was enumerated
        bool directory_exists = Directory.Exists(mod_directory);

        if (!directory_exists) {
            return new(new FhManifest(mod_id, mod_id, "", "", "", "", [], [], FhManifestFlags.NONE), mod_directory, false, false, "The mod directory does not exist.", load_order_index);
        }

        string manifest_path = Path.Join(mod_directory, $"{mod_id}.manifest.json");

        if (!File.Exists(manifest_path)) {
            return new(new FhManifest(mod_id, mod_id, "", "", "", "", [], [], FhManifestFlags.NONE), mod_directory, true, false, $"Manifest not found: {manifest_path}", load_order_index);
        }

        try {
            using JsonDocument document = JsonDocument.Parse(File.ReadAllText(manifest_path));

            JsonElement root = document.RootElement;

            // Version/Authors are purely cosmetic, so they default to blank and get
            // skipped by the UI; Name falls back to the mod_id
            string name     = _read_string(root, "Name", mod_id);
            string version  = _read_string(root, "Version", "0");
            string authors  = _read_string(root, "Authors", "Unknown");

            return new(new FhManifest(mod_id, name, "", authors, version, "", [], [], FhManifestFlags.NONE), mod_directory, true, true, "", load_order_index);
        }
        catch (Exception exception) {
            return new(new FhManifest(mod_id, mod_id, "", "", "", "", [], [], FhManifestFlags.NONE), mod_directory, true, true, exception.Message, load_order_index);
        }
    }

    /// <summary>
    ///    Reads a string property from mod manifest, defaults to fallback if found value invalid.
    /// </summary>
    private static string _read_string(JsonElement root, string property_name, string fallback) {
        if (!root.TryGetProperty(property_name, out JsonElement property)) {
            return fallback;
        }

        if (property.ValueKind != JsonValueKind.String) {
            return fallback;
        }

        string? readValue = property.GetString();
        return string.IsNullOrEmpty(readValue) ? fallback : readValue!;
    }
}

/// <summary>
///    Reads and writes the load order file, enabling/disabling mods and moving them up/down.
/// </summary>
internal static class FhLoadOrderEditor {
    /// <summary>
    ///    Enables or disables a mod by adding or removing its ID from the load order file.
    /// </summary>
    internal static bool try_set_enabled(string load_order_path, FhInstalledMod mod, bool enabled, out string error) {
        error = "";

        try {
            string? mods_directory = Path.GetDirectoryName(load_order_path);

            if (mods_directory == null || !Directory.Exists(mods_directory)) {
                error = "The Fahrenheit mods directory does not exist.";

                return false;
            }

            List<string> load_order = _read_normalized_load_order(load_order_path);

            bool already_enabled = false;

            foreach (string entry in load_order) {
                if (string.Equals(entry, mod.Manifest.Id, StringComparison.OrdinalIgnoreCase)) {
                    already_enabled = true;
                    break;
                }
            }

            if (enabled) {
                if (!already_enabled) {
                    /*
                     * New mods are appended to the end of the load order.
                     * Later EFL mods supersede earlier replacements.
                     */
                    load_order.Add(mod.Manifest.Id);
                }
            }
            else {
                load_order.RemoveAll(
                    entry => string.Equals(entry, mod.Manifest.Id, StringComparison.OrdinalIgnoreCase));
            }

            _write_load_order(load_order_path, load_order);

            return true;
        }
        catch (UnauthorizedAccessException) {
            error = "Windows denied permission to update loadorder file.";

            return false;
        }
        catch (Exception exception) {
            error = $"The load order could not be updated.\n\n{exception.Message}";

            return false;
        }
    }

    /// <summary>
    ///    Appends any of `mod_ids` not already present (case-insensitively) to the end of the load order, preserving the order they're given in.
    /// </summary>
    internal static bool try_append_all(string load_order_path, IReadOnlyList<string> mod_ids, out string error) {
        error = "";

        try {
            List<string> load_order = _read_normalized_load_order(load_order_path);

            HashSet<string> existing = new(load_order, StringComparer.OrdinalIgnoreCase);

            foreach (string mod_id in mod_ids) {
                if (existing.Add(mod_id)) {
                    load_order.Add(mod_id);
                }
            }

            _write_load_order(load_order_path, load_order);

            return true;
        }
        catch (Exception exception) {
            error = $"The load order could not be updated.\n\n{exception.Message}";

            return false;
        }
    }

    /// <summary>
    ///    Reads the load order file into a de-duplicated, whitespace-trimmed list.
    ///    Edits made to the list through the UI are written back out atomically by _write_load_order.
    /// </summary>
    private static List<string> _read_normalized_load_order(string load_order_path) {
        List<string> load_order = [];

        if (!File.Exists(load_order_path)) {
            return load_order;
        }

        HashSet<string> seen_mods = new(StringComparer.OrdinalIgnoreCase);

        foreach (string raw_entry in File.ReadAllLines(load_order_path)) {
            string entry = raw_entry.Trim();

            if (string.IsNullOrWhiteSpace(entry)) {
                continue;
            }

            if (seen_mods.Add(entry)) {
                load_order.Add(entry);
            }
        }

        return load_order;
    }

    /// <summary>
    ///    Joins the entries back into a loadorder file and writes it atomically.
    /// </summary>
    private static void _write_load_order(string load_order_path, IReadOnlyList<string> entries) {
        string contents = string.Join(Environment.NewLine, entries);

        if (entries.Count > 0) {
            contents += Environment.NewLine;
        }

        FhModManagerSettingsStore.write_atomic(load_order_path, contents);
    }

    /// <summary>
    ///    Moves the mod up or down by one position in the load order.
    /// </summary>
    internal static bool try_move(string load_order_path, FhInstalledMod mod, int direction, out string error) {
        if (direction != -1 && direction != 1) {
            error = "Load-order direction must be -1 or 1.";
            return false;
        }

        return _try_reposition(load_order_path, mod, current_index => current_index + direction, out error);
    }

    /// <summary>
    ///    Moves the mod to an arbitrary position in the load order from drag/drop UI.
    /// </summary>
    internal static bool try_move_to(string load_order_path, FhInstalledMod mod, int target_index, out string error) {
        return _try_reposition(load_order_path, mod, _ => target_index, out error);
    }

    /// <summary>
    ///    Attempts to reposition the mod in the load order (if changing).
    /// </summary>
    private static bool _try_reposition(
        string load_order_path,
        FhInstalledMod mod,
        Func<int, int> compute_target_index,
        out string error) {
        error = "";

        if (!mod.HasValidManifest) {
            error = $"The mod '{mod.Manifest.Id}' does not have a valid manifest.";
            return false;
        }

        try {
            if (!File.Exists(load_order_path)) {
                error = $"The Fahrenheit load-order file does not exist:\n\n{load_order_path}";
                return false;
            }

            List<string> load_order = _read_normalized_load_order(load_order_path);

            int current_index = load_order.FindIndex(
                entry => string.Equals(entry, mod.Manifest.Id, StringComparison.OrdinalIgnoreCase));

            if (current_index < 0) {
                error = $"The enabled mod '{mod.Manifest.Id}' was not found in the load order.";
                return false;
            }

            load_order.RemoveAt(current_index);

            int target_index = Math.Clamp(compute_target_index(current_index), 0, load_order.Count);

            load_order.Insert(target_index, mod.Manifest.Id);

            if (target_index == current_index) {
                // No actual change (e.g. already at the top/bottom, or dropped
                // back where it started) - skip the write.
                return true;
            }

            _write_load_order(load_order_path, load_order);

            return true;
        }
        catch (UnauthorizedAccessException) {
            error = "Windows denied permission to update the load order.";
            return false;
        }
        catch (Exception exception) {
            error = $"The load order could not be changed.\n\n{exception.Message}";
            return false;
        }
    }
}
