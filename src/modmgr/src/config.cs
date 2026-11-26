// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

using System.IO;
using System.Text.Json;

namespace Fahrenheit.Tools.ModManager;

internal record struct ModManagerConfig() {
    public bool   show_advanced_options = false;
    public string path_game             = string.Empty;

    // Linux only
    public string path_proton_exe    = string.Empty;
    public string path_proton_prefix = string.Empty;

    public string path_proton_install =>
        Path.GetFullPath(Path.Combine(path_proton_exe, "..", "..", ".."));
}

internal static partial class ModManager {
    internal static ModManagerConfig Config        = new();
    internal static ModManagerConfig config_backup = new();

    internal const string config_name = "fhmodmgr.config.json";
    //internal static string config_fn => Path.Join(FhEnvironment.Finder.Config.FullName, config_name);

    internal static void save_config() {
        using FileStream config_fs = File.Open(config_name, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
        config_fs.Write(JsonSerializer.SerializeToUtf8Bytes(Config, FhUtil.InternalJsonOpts));
    }

    internal static void load_config() {
        try {
            using FileStream config_fs = File.Open(config_name, FileMode.Open, FileAccess.Read, FileShare.Read);
            Config = JsonSerializer.Deserialize<ModManagerConfig>(config_fs, FhUtil.InternalJsonOpts);
        } catch {
            // If anything goes wrong, we fall back to defaults
        }
    }

    internal static void show_config() {
        config_backup = Config;
        _show_config_window = true;
    }

    internal static void render_config_path(string label, ref string path, int max_length) {
        ImGui.SetNextItemWidth(250f);
        ImGui.InputText($"###input-{label}", ref path, (nuint)max_length);

        ImGui.SameLine();
        if (ImGui.Button($"Locate##{label}")) {
            //TODO: Add path location
        }

        ImGui.SameLine();
        ImGui.Text(label);
    }

    internal static bool _show_config_window;
    internal static void render_config() {
        if (!_show_config_window) return;

        if (!ImGui.Begin("Settings", ImGuiWindowFlags.NoDocking)) {
            ImGui.End();
            return;
        }

        ImGui.Checkbox("Show Advanced Options", ref config_backup.show_advanced_options);

        // Unfortunately `PathInternal.MaxShortPath` is internal, so we must hardcode 260
        render_config_path("Path to Game", ref config_backup.path_game, 260);

        if (OperatingSystem.IsLinux()) {
            render_config_path("Path to Proton Executable", ref config_backup.path_proton_exe,    4096);
            render_config_path("Path to Proton Prefix",     ref config_backup.path_proton_prefix, 4096);
        }

        ImGui.Separator();

        if (ImGui.Button("Reset to Defaults")) {
            config_backup = new();
        }

        ImGui.SameLine();

        if (Config == config_backup) {
            ImGui.BeginDisabled();
            ImGui.Button("Apply");
            ImGui.EndDisabled();
        } else if (ImGui.Button("Apply")) {
            Config = config_backup;
        }

        ImGui.SameLine();

        if (ImGui.Button("Cancel")) {
            _show_config_window = false;
            config_backup = Config;
        }

        ImGui.End();
    }
}
