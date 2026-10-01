// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Tools.ModManager;

/// <summary>
///    Defines FhModManagerSettings persisted to fhmodmgr.json.
/// </summary>
internal sealed class FhModManagerSettings {
    public string GameDirectory { get; set; } = FhModManagerSettingsStore.DEFAULT_GAME_DIRECTORY;
}

/// <summary>
///    Loads and saves FhModManagerSettings to fhmodmgr.json in the same directory as the executable.
/// </summary>
internal static class FhModManagerSettingsStore {
    internal const string DEFAULT_GAME_DIRECTORY = @"C:\Program Files (x86)\Steam\steamapps\common\FINAL FANTASY FFX&FFX-2 HD Remaster";

    private static readonly JsonSerializerOptions _json_options = new() {
        WriteIndented = true
    };

    internal static string SettingsPath {
        get {
            return Path.Join( AppContext.BaseDirectory, "fhmodmgr.json");
        }
    }

    /// <summary>
    ///    Loads the settings from fhmodmgr.json, returning a warning message if the load fails or the settings are empty.
    /// </summary>
    internal static FhModManagerSettings load(out string warning) {
        warning = "";

        if (!File.Exists(SettingsPath)) {
            return new();
        }

        try {
            string json = File.ReadAllText(SettingsPath);
            FhModManagerSettings? settings = JsonSerializer.Deserialize<FhModManagerSettings>(json, _json_options);

            if (settings == null || string.IsNullOrWhiteSpace(settings.GameDirectory)) {
                warning = "The saved settings were empty. Using the default game location.";
                return new();
            }

            settings.GameDirectory = normalize_path(settings.GameDirectory);
            return settings;
        }
        catch (Exception exception) {
            warning = $"The saved settings could not be read. Using the default game location.\n\n{exception.Message}";

            return new();
        }
    }

    /// <summary>
    ///    Saves the given settings to fhmodmgr.json, returning false and an error message if the save fails.
    /// </summary>
    internal static bool try_save(FhModManagerSettings settings, out string error) {
        error = "";

        try {
            // Defensive: normalize even though every current caller already
            // does, so a save can never persist a non-canonical path.
            settings.GameDirectory = normalize_path(settings.GameDirectory);
            string json = JsonSerializer.Serialize(settings, _json_options);
            write_atomic(SettingsPath, json);
            return true;
        }
        catch (Exception exception) {
            error = $"The settings could not be saved.\n\n{exception.Message}";
            return false;
        }
    }

    /// <summary>
    ///    Normalizes a path by: trimming whitespace/quotes, resolving relative paths to absolute, removing any trailing directory separators
    /// </summary>
    internal static string normalize_path(string path) {
        string cleaned_path = path.Trim().Trim('"');
        string full_path = Path.GetFullPath(cleaned_path);
        return Path.TrimEndingDirectorySeparator(full_path);
    }

    /// <summary>
    ///    Writes a file atomically by staging it under a temporary name and renaming it into place.
    /// </summary>
    internal static void write_atomic(string destination, string contents) {
        string temporary_path = $"{destination}.tmp";

        try {
            File.WriteAllText(temporary_path, contents);
            File.Move(temporary_path, destination, true);
        }
        finally {
            if (File.Exists(temporary_path)) {
                File.Delete(temporary_path);
            }
        }
    }
}
