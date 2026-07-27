// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

using System.IO;
using System.Text.Json;

namespace Fahrenheit.Tools.ModManager;

internal struct ModManagerConfig {

}

internal static partial class ModManager {
    internal static ModManagerConfig Config;

    internal const string config_name = "fhmodmgr.config.json";
    internal static string config_fn => Path.Join(FhEnvironment.Finder.Config.FullName, config_name);

    internal static void save_config() {
        using FileStream config_fs = File.Open(config_fn, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
        config_fs.Write(JsonSerializer.SerializeToUtf8Bytes(Config, FhUtil.InternalJsonOpts));
    }

    internal static void load_config() {
        try {
            using FileStream config_fs = File.Open(config_fn, FileMode.Open, FileAccess.Read, FileShare.Read);
            Config = JsonSerializer.Deserialize<ModManagerConfig>(config_fs, FhUtil.InternalJsonOpts);
        } catch {
            // If anything goes wrong, we fall back to config defaults
        }
    }
}
