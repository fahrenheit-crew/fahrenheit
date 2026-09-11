// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime.Gui;

/* [fkelava 07/05/26 17:39]
 * The game's original save UI is implemented in ActionScript, in a Flash file rendered
 * using the Iggy library. The game interacts with it using a callback system.
 * This is inefficient, and execution time scales almost quadratically with the number of saves.
 *
 * Since we allow multiple sets of saves, each of an unlimited size, the original UI becomes too slow.
 * For that reason, and to permit its customization, we bypass it in favor of ImGui replacements.
 */

/// <summary>
///     Implements Fahrenheit's replacement save/load user interface.
/// </summary>
[FhLoad(FhGameId.FFX | FhGameId.FFX2 | FhGameId.FFX2LM)]
public sealed class FhSaveUiSelector : FhModule {
    private FhSaveUiX?  _ui_x;
    private FhSaveUiX2? _ui_x2;

    private class FhSaveUiSettings {
        //TODO: Change this to a Set-based dropdown once that's created.
        public readonly FhSettingText selected_ui = new("selected_ui", "");
    }

    private readonly FhSaveUiSettings _settings = new();

    public FhSaveUiSelector() {
        settings = new FhSettingsCategory("fhsaveui", [
            _settings.selected_ui,
        ]);
    }

    private string get_default_ui_id() {
        return FhGlobal.game_id switch {
            FhGameId.FFX    => _ui_x! .ModuleType,
            FhGameId.FFX2   or
            FhGameId.FFX2LM => _ui_x2!.ModuleType,

            _ => throw new NotImplementedException(),
        };
    }

    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        bool got_modules = FhGlobal.game_id switch {
            FhGameId.FFX    => new FhModuleHandle<FhSaveUiX> (this).try_get_module(out _ui_x),
            FhGameId.FFX2   or
            FhGameId.FFX2LM => new FhModuleHandle<FhSaveUiX2>(this).try_get_module(out _ui_x2),

            _ => throw new NotImplementedException(),
        };

        _settings.selected_ui.set(get_default_ui_id());

        return got_modules;
    }

    public override void render_imgui() {
        if (FhApi.Saves.get_system_mode(out FhSaveSystemMode? mode)
            && mode is FhSaveSystemMode.NULL
        ) {
            return;
        }

        if (!FhApi.Saves.get_ui(_settings.selected_ui.get(), out FhSaveUi? ui)) {
            // Previous UI is missing (provider mod updated and broke API?)
            // So we try to fall back to default
            _logger.Warning($"Failed to find desired save/load UI \"{_settings.selected_ui.get()}\", falling back to default.");

            _settings.selected_ui.set(get_default_ui_id());

            if (!FhApi.Saves.get_ui(_settings.selected_ui.get(), out ui)) {
                // Something has gone disastrously wrong – we're missing our default UI!
                _logger.Error("Failed to find default save/load UI.");
                throw new NotImplementedException("Failed to find default save/load UI.");
            }
        }

        if (FhSavePal.pal_get_screen_state() is FhSaveScreenState.OPEN) {
            ui.render_ui();
        }
    }
}
