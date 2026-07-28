// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

/// <summary>
///     Provides access to a module's essential files and directories.
/// </summary>
internal sealed record FhModulePaths(
    string GlobalStatePath,
    string GlobalConfigPath,
    string LocalConfigPath);

/// <summary>
///     Provides access to a mod's essential files and directories.
/// </summary>
public sealed record FhModPaths(
    string        ManifestPath,
    DirectoryInfo ModDir,
    DirectoryInfo ResourcesDir,
    DirectoryInfo EflDir,
    DirectoryInfo LangDir);

/// <summary>
///     Resolves the paths of directories and files required by the framework.
/// </summary>
internal sealed class FhFinder {
    private const string _dirname_bin   = "bin";
    private const string _dirname_cfg   = "config";
    private const string _dirname_mods  = "mods";
    private const string _dirname_logs  = "logs";
    private const string _dirname_state = "state";
    private const string _dirname_saves = "saves";
    private const string _dirname_lang  = "lang";
    private const string _dirname_rsrc  = "resources";
    private const string _dirname_efl   = "efl";

    internal readonly DirectoryInfo Game;
    internal readonly DirectoryInfo Binaries;
    internal readonly DirectoryInfo Config;
    internal readonly DirectoryInfo Mods;
    internal readonly DirectoryInfo Logs;
    internal readonly DirectoryInfo State;
    internal readonly DirectoryInfo Saves;

    internal FhFinder() {
        // If this somehow throws, we really have no business executing at all.
        string cwd_parent = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName!;

        Game     = new(Path.Join(cwd_parent, @"..\..\"));
        Binaries = Directory.CreateDirectory(Path.Join(cwd_parent, _dirname_bin));
        Config   = Directory.CreateDirectory(Path.Join(cwd_parent, _dirname_cfg));
        Mods     = Directory.CreateDirectory(Path.Join(cwd_parent, _dirname_mods));
        Logs     = Directory.CreateDirectory(Path.Join(cwd_parent, _dirname_logs, FhUtil.get_timestamp_string()));
        State    = Directory.CreateDirectory(Path.Join(cwd_parent, _dirname_state));
        Saves    = Directory.CreateDirectory(Path.Join(cwd_parent, _dirname_saves));
    }

    /// <summary>
    ///     Gets the full path of the game's INI setting file.
    /// </summary>
    public string get_path_settings() {
        return Path.Join(
            Environment.GetFolderPath(Environment.SpecialFolder.Personal),
            "SQUARE ENIX",
            "FINAL FANTASY X&X-2 HD Remaster",
            "GameSetting.ini");
    }

    /// <summary>
    ///     Returns path information for the DLL belonging to mod <paramref name="mod_name"/>.
    /// </summary>
    public string get_for_dll(string mod_name) {
        bool   is_runtime = mod_name.Equals("fhr", StringComparison.OrdinalIgnoreCase);
        string mod_dir    = is_runtime ? Binaries.FullName : Path.Join(Mods.FullName, mod_name);

        return Path.Join(mod_dir, $"{mod_name}.dll");
    }

    /// <summary>
    ///     Returns path information for mod <paramref name="mod_name"/>.
    /// </summary>
    public FhModPaths get_for_mod(string mod_name) {
        bool   is_runtime = mod_name.Equals("fhr", StringComparison.OrdinalIgnoreCase);
        string mod_dir    = is_runtime ? Binaries.FullName : Path.Join(Mods.FullName, mod_name);

        return new FhModPaths(
            ManifestPath: Path.Join(mod_dir, $"{mod_name}.manifest.json"),
            ModDir:       Directory.CreateDirectory(mod_dir),
            ResourcesDir: Directory.CreateDirectory(Path.Join(mod_dir, _dirname_rsrc)),
            EflDir:       Directory.CreateDirectory(Path.Join(mod_dir, _dirname_efl)),
            LangDir:      Directory.CreateDirectory(Path.Join(mod_dir, _dirname_lang))
            );
    }

    /// <summary>
    ///     Returns path information for module <paramref name="module_name"/> of mod <paramref name="mod_name"/>.
    /// </summary>
    public FhModulePaths get_for_module(string mod_name, string module_name) {
        string dir_state_global  = Path.Join(State.FullName, "global", mod_name);
        string path_state_global = Path.Join(dir_state_global, module_name);

        string dir_cfg_global  = Path.Join(Config.FullName, "global", mod_name);
        string path_cfg_global = Path.Join(dir_cfg_global, $"{module_name}.json");

        string dir_cfg_local  = Path.Join(Config.FullName, "local", "default", mod_name);
        string path_cfg_local = Path.Join(dir_cfg_local, $"{module_name}.json");

        Directory.CreateDirectory(dir_state_global);
        Directory.CreateDirectory(dir_cfg_global);
        Directory.CreateDirectory(dir_cfg_local);

        return new FhModulePaths(
            GlobalStatePath:  path_state_global,
            GlobalConfigPath: path_cfg_global,
            LocalConfigPath:  path_cfg_local
            );
    }
}
