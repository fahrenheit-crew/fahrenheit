// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Gui;

/// <summary>The base class for custom settings user interfaces.</summary>
public abstract class FhSettingsUi : FhFullscreenUi {
    protected OrderedDictionary<FhModContext, List<FhModuleContext>> settings_map = [];

    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        _logger.Info($"Registering new settings UI: {ModuleType}");
        FhInternal.Settings.register_ui(this);

        FhApi.Events.Common.GameLoop.PostOpenSettingsMenu.subscribe(post_open_menu);

        return true;
    }

    internal override void post_open_menu(EventArgs e) {
        base.post_open_menu(e);

        settings_map = [];

        foreach (FhModContext mod in FhApi.Mods.get_mods()) {
            List<FhModuleContext> modules = [];
            bool has_settings = false;

            foreach (FhModuleContext module in mod.Modules) {
                if (!FhInternal.Settings.try_get_settings(module.Module, out _))
                    continue;

                has_settings = true;
                modules.Add(module);
            }

            if (has_settings) {
                settings_map.Add(mod, modules);
            }
        }
    }
}
