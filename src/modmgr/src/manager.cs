// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace Fahrenheit.Tools.ModManager;

internal enum ModLoadResult {
    OK = 0,

    MISSING_MANIFEST = 1,
    INVALID_MANIFEST = 2,
    FAILED_TO_OPEN   = 3,
}

internal static partial class ModManager {
    internal static FhImGuiHelper ImGuiHelper = new();

    internal static LinkedList<ModData> installed_mods = [];

    internal static void init() {
        load_config();
        populate_installed_mods();
    }

    internal static void populate_installed_mods() {
        List<(string, ModLoadResult)> failures = [];
        HashSet<string> loadorder_mods = [];

        installed_mods = [];

        // Handle the mods in loadorder first so the linked list is pre-sorted
        string loadorder_fn = Path.Join(FhEnvironment.Finder.Mods.FullName, "loadorder");

        if (File.Exists(loadorder_fn)) {
            debug_log.Add("Found loadorder!");

            using FileStream loadorder_fs = File.OpenRead(loadorder_fn);
            using StreamReader loadorder_read = new(loadorder_fs);

            while (loadorder_read.ReadLine() is { } mod_name) {
                loadorder_mods.Add(mod_name);

                debug_log.Add($"Trying to load mod {mod_name}...");

                var load_result = try_load_mod(mod_name);

                if (load_result.result == ModLoadResult.OK) {
                    DirectoryInfo mod_dir = new(Path.Join(FhEnvironment.Finder.Mods.FullName, mod_name));

                    ModData mod = new() {
                        manifest = load_result.manifest!,
                        enabled = true,
                        size_on_disk = ModManagerUtil.get_folder_size(mod_dir),
                    };

                    installed_mods.AddLast(mod);
                    debug_log.Add($"  Loaded mod!");
                } else {
                    debug_log.Add($"  Failed to load mod: {load_result.result.ToString()}");
                    failures.Add((mod_name, load_result.result));
                }
            }
        }

        // Now we iterate over all the mod directories to find disabled mods
        foreach (DirectoryInfo mod_dir in FhEnvironment.Finder.Mods.EnumerateDirectories()) {
            string mod_name = mod_dir.Name;

            // Skip enabled mods
            if (loadorder_mods.Contains(mod_name)) continue;

            debug_log.Add($"Trying to load mod {mod_name}...");

            var load_result = try_load_mod(mod_name);

            if (load_result.result == ModLoadResult.OK) {
                ModData mod = new() {
                    manifest = load_result.manifest!,
                    enabled = false,
                    size_on_disk = ModManagerUtil.get_folder_size(mod_dir),
                };
                installed_mods.AddLast(mod);
                debug_log.Add($"  Loaded mod!");
            } else {
                debug_log.Add($"  Failed to load mod: {load_result.result.ToString()}");
                failures.Add((mod_name, load_result.result));
            }
        }
    }

    internal static (FhManifest? manifest, ModLoadResult result) try_load_mod(string mod_name) {
        string manifest_fn = Path.Join(FhEnvironment.Finder.Mods.FullName, mod_name, $"{mod_name}.manifest.json");

        if (!File.Exists(manifest_fn)) {
            return (null, ModLoadResult.MISSING_MANIFEST);
        }

        try {
            using FileStream manifest_fs = File.OpenRead(manifest_fn);

            FhManifest? manifest = JsonSerializer.Deserialize<FhManifest>(manifest_fs, FhUtil.InternalJsonOpts);

            if (manifest is null) {
                return (null, ModLoadResult.INVALID_MANIFEST);
            }

            return (manifest, ModLoadResult.OK);
        }
        catch (Exception e) {
            debug_log.Add($"[Error] {e.Message}");
            return (null, ModLoadResult.FAILED_TO_OPEN);
        }
    }

    internal static void launch_game(FhGameId game_id, bool debug, string[] extra_args) {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
            launch_game_windows(game_id, debug, extra_args);
        } else {
            launch_game_linux(game_id, debug, extra_args);
        }
    }

    internal static void launch_game_windows(FhGameId game_id, bool debug, string[] extra_args) {
        string bin_folder = FhEnvironment.Finder.Binaries.FullName;

        ProcessStartInfo start_info = new() {
            FileName = Path.Join(bin_folder, "fhstage0.exe"),
            WorkingDirectory = bin_folder,
            UseShellExecute = true,
        };

        start_info.ArgumentList.Add(get_path_for_game(game_id));

        if (game_id == FhGameId.FFX2LM) {
            start_info.ArgumentList.Add("--FFX2_LASTMISSION");
        }

        if (debug) {
            start_info.ArgumentList.Add("--debug");
        }


    }

    internal static void launch_game_linux(FhGameId game_id, bool debug, string[] extra_args) {
        throw new NotImplementedException("Launching the game on Linux has not been implemented yet. We apologize.");
    }

    internal static string get_path_for_game(FhGameId game) {
        return game switch {
            FhGameId.FFX    => "../../FFX.exe",

            FhGameId.FFX2
         or FhGameId.FFX2LM => "../../FFX-2.exe",

            _ => throw new NotImplementedException($"Unknown game ID: {game}"),
        };
    }
}
