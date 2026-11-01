// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Tools.ModManager;

internal static class FhEflImporter {
    private static readonly JsonSerializerOptions _json_options = new() {
        WriteIndented = true
    };

    // Copies a loose file tree already laid out like a VBF (e.g. FFX_Data/ffx_ps2/...,
    // as documented in src/runtime/fileloader.cs) into a new mod's efl/x or efl/x2
    // folder, generating a manifest.json for it.
    internal static ResultsMessage import(
        string mods_directory,
        string mod_id,
        string mod_name,
        FhGameId game,
        string source_folder) {
        if (string.IsNullOrWhiteSpace(mod_id)) {
            return new(false, "A mod ID is required.");
        }

        if (!Directory.Exists(source_folder)) {
            return new(false, "The source folder does not exist.");
        }

        string mod_directory = Path.Join(mods_directory, mod_id);

        if (Directory.Exists(mod_directory)) {
            return new(false, $"A mod named '{mod_id}' already exists.");
        }

        try {
            string efl_subfolder = game == FhGameId.FFX2 ? "x2" : "x";
            string efl_directory = Path.Join(mod_directory, "efl", efl_subfolder);

            Directory.CreateDirectory(efl_directory);

            _copy_directory(source_folder, efl_directory);

            FhManifest manifest = new(
                mod_id,
                mod_name,
                "",
                "",
                "1.0.0",
                "",
                [],
                [],
                FhManifestFlags.NONE);

            string manifest_path = Path.Join(
                mod_directory,
                $"{mod_id}.manifest.json");

            File.WriteAllText(
                manifest_path,
                JsonSerializer.Serialize(manifest, _json_options));

            return new(true, $"Imported '{mod_name}' as an EFL mod.");
        }
        catch (Exception exception) {
            // A failed import shouldn't leave behind a half-populated mod folder
            // for FhModScanner to trip over on the next scan.
            _try_delete_directory(mod_directory);

            return new(
                false,
                "The mod could not be imported.\n\n" + exception.Message);
        }
    }

    private static void _copy_directory(string source, string destination) {
        foreach (string source_file in Directory.EnumerateFiles(
                source,
                "*",
                SearchOption.AllDirectories)) {
            string relative_path = Path.GetRelativePath(source, source_file);
            string destination_file = Path.Join(destination, relative_path);

            Directory.CreateDirectory(
                Path.GetDirectoryName(destination_file)!);

            File.Copy(source_file, destination_file, false);
        }
    }

    private static void _try_delete_directory(string directory) {
        try {
            if (Directory.Exists(directory)) {
                Directory.Delete(directory, true);
            }
        }
        catch {
            // Best effort; the outer failure message already covers the real error.
        }
    }
}

/// <summary>
///    Zips a set of enabled mods' directories plus a `loadorder` file into a single distributable .zip file.
/// </summary>
internal static class FhModPackExporter {
    internal static ResultsMessage export(string destination_zip_path, IReadOnlyList<FhInstalledMod> mods_in_order) {
        List<string> included_ids = [];
        List<string> skipped_ids = [];

        try {
            using FileStream zip_stream = File.Create(destination_zip_path);
            using ZipArchive archive = new(zip_stream, ZipArchiveMode.Create);
    
            foreach (FhInstalledMod mod in mods_in_order) {
                if (!mod.DirectoryExists) {
                    skipped_ids.Add(mod.Manifest.Id);
                    continue;
                }

                IEnumerable<string> files = Directory.EnumerateFiles(mod.DirectoryPath, "*", SearchOption.AllDirectories);
                foreach (string mod_file in files) {
                    string relative_path = Path.GetRelativePath(mod.DirectoryPath, mod_file).Replace('\\', '/');
                    archive.CreateEntryFromFile(mod_file, $"{mod.Manifest.Id}/{relative_path}", CompressionLevel.Optimal);
                }

                included_ids.Add(mod.Manifest.Id);
            }

            using StreamWriter writer = new(archive.CreateEntry("loadorder").Open());

            foreach (string mod_id in included_ids) {
                writer.WriteLine(mod_id);
            }
        }
        catch (Exception exception) {
            _try_delete_file(destination_zip_path);
            return new(false, $"The mod pack could not be exported.\n\n{exception.Message}");
        }

        if (included_ids.Count == 0) {
            _try_delete_file(destination_zip_path);
            return new(false, "No enabled mods were available to export.");
        }

        string message = $"Exported {included_ids.Count} mod(s) to the pack.";

        if (skipped_ids.Count > 0) {
            message = $"{message}\n\nSkipped (missing on disk): {string.Join(", ", skipped_ids)}";
        }

        return new(true, message);
    }

    private static void _try_delete_file(string path) {
        try {
            if (File.Exists(path)) {
                File.Delete(path);
            }
        }
        catch {
            // Best effort; the outer failure message already covers the real error.
        }
    }
}

/// <summary>
///    Extracts a mod pack's not-already-installed mods.
///    Appends them to the already-existing `loadorder` in the pack's defined order.
///    Existing mods are skipped rather than overwritten.
/// </summary>
internal static class FhModPackImporter {
    internal static ResultsMessage import(
        string mods_directory,
        string load_order_path,
        string source_zip_path) {
        List<string> installed_ids = [];
        List<string> skipped_existing_ids = [];
        List<string> pack_load_order = [];

        try {
            using FileStream zip_stream = File.OpenRead(source_zip_path);
            using ZipArchive archive = new(zip_stream, ZipArchiveMode.Read);

            ZipArchiveEntry? load_order_entry = archive.GetEntry("loadorder");

            if (load_order_entry != null) {
                using StreamReader reader = new(load_order_entry.Open());

                string? line;

                while ((line = reader.ReadLine()) != null) {
                    string trimmed = line.Trim();

                    if (!string.IsNullOrWhiteSpace(trimmed)) {
                        pack_load_order.Add(trimmed);
                    }
                }
            }

            Dictionary<string, List<ZipArchiveEntry>> entries_by_mod_id = new(StringComparer.OrdinalIgnoreCase);

            foreach (ZipArchiveEntry entry in archive.Entries) {
                string entry_path = entry.FullName.Replace('\\', '/');
                int separator_index = entry_path.IndexOf('/');

                // Entries not nested under a mod folder - namely `loadorder` itself.
                if (separator_index <= 0) {
                    continue;
                }

                string mod_id = entry_path[..separator_index];

                if (!entries_by_mod_id.TryGetValue(mod_id, out List<ZipArchiveEntry>? mod_entries)) {
                    mod_entries = [];
                    entries_by_mod_id[mod_id] = mod_entries;
                }

                mod_entries.Add(entry);
            }

            string mods_directory_full = Path.GetFullPath(mods_directory) + Path.DirectorySeparatorChar;

            foreach ((string mod_id, List<ZipArchiveEntry> mod_entries) in entries_by_mod_id) {
                string mod_directory = Path.Join(mods_directory, mod_id);

                if (Directory.Exists(mod_directory)) {
                    skipped_existing_ids.Add(mod_id);
                    continue;
                }

                foreach (ZipArchiveEntry entry in mod_entries) {
                    // Directory entries end with '/' and have nothing to extract.
                    if (entry.Name.Length == 0) {
                        continue;
                    }

                    string destination_path = Path.GetFullPath(
                        Path.Join(mods_directory, entry.FullName));

                    // Zip-slip guard: refuse any entry whose path would land outside the mods directory
                    if (!destination_path.StartsWith(mods_directory_full, StringComparison.OrdinalIgnoreCase)) {
                        continue;
                    }

                    Directory.CreateDirectory(Path.GetDirectoryName(destination_path)!);
                    entry.ExtractToFile(destination_path, true);
                }

                installed_ids.Add(mod_id);
            }
        }
        catch (Exception exception) {
            return new(false, $"The mod pack could not be imported.\n\n{exception.Message}");
        }

        if (installed_ids.Count == 0) {
            return new(
                false,
                skipped_existing_ids.Count > 0
                    ? $"No mods were installed; all mods in the pack already exist:\n{string.Join(", ", skipped_existing_ids)}"
                    : "The pack did not contain any mods.");
        }

        // Preserve the pack's own ordering for the mods we actually installed,
        // falling back to install order for anything the pack didn't list.
        List<string> ordered_new_ids = [];
        HashSet<string> remaining = new(installed_ids, StringComparer.OrdinalIgnoreCase);

        foreach (string mod_id in pack_load_order) {
            if (remaining.Remove(mod_id)) {
                ordered_new_ids.Add(mod_id);
            }
        }

        foreach (string mod_id in installed_ids) {
            if (remaining.Contains(mod_id)) {
                ordered_new_ids.Add(mod_id);
                remaining.Remove(mod_id);
            }
        }

        if (!FhLoadOrderEditor.try_append_all(load_order_path, ordered_new_ids, out string load_order_error)) {
            return new(
                false,
                $"Installed {installed_ids.Count} mod(s), but the load order could not be updated.\n\n{load_order_error}");
        }

        string message = $"Installed {installed_ids.Count} mod(s) from the pack.";

        if (skipped_existing_ids.Count > 0) {
            message = $"{message}\n\nSkipped (already installed): {string.Join(", ", skipped_existing_ids)}";
        }

        return new(true, message);
    }
}
