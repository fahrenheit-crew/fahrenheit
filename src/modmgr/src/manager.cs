// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

using System.Collections.Generic;

namespace Fahrenheit.Tools.ModManager;

internal static partial class ModManager {
    private static string _dir_game;

    private static List<ModData> _installed_mods = [];

    internal static void init() {
        load_config();
        detect_game_dir();
        populate_installed_mods();
    }

    internal static void detect_game_dir() {
    }

    internal static void populate_installed_mods() {
        _installed_mods = [];


    }
}
