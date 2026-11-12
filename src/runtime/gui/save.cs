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

    internal sealed class UiSelectionSetting(string id, string def_value) : FhSettingDynamicChoice<string>(id, def_value) {
        protected override IReadOnlyList<object> init_choices() {
            //TODO: Unhardcode this once we can safely retrieve all registered UIs (post module ctor, pre module init)
            List<string> ui_types = [
                typeof(FhSaveUiX) .FullName!,
                typeof(FhSaveUiX2).FullName!,
            ];

            return ui_types;
        }

        internal override void init() {
            base.init();

            set(get_default_ui_id());
        }
    }

    internal sealed class Settings : FhSettingProvider<FhSaveUiSelector> {
        public readonly UiSelectionSetting selected_ui = new("selected_ui", string.Empty);

        internal protected override IEnumerable<FhSetting> get() {
            return [ selected_ui ];
        }
    }

    internal readonly Settings settings = new();

    private static string get_default_ui_id() {
        return FhUtil.select(
            typeof(FhSaveUiX) .FullName!,
            typeof(FhSaveUiX2).FullName!,
            typeof(FhSaveUiX2).FullName!
        );
    }

    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        return new FhModuleHandle<FhSaveUiX> (this).try_get_module(out _ui_x)
            && new FhModuleHandle<FhSaveUiX2>(this).try_get_module(out _ui_x2);
    }

    private FhSaveUi get_ui() {
        FhSaveUi? ui;

        if (FhApi.Saves.ui_get(settings.selected_ui.get(), out ui)) {
            return ui;
        }

        // Previous UI is missing (provider mod updated and broke API?)
        // So we try to fall back to default
        _logger.Warning($"Failed to find desired save/load UI \"{settings.selected_ui.get()}\", falling back to default.");

        settings.selected_ui.set(get_default_ui_id());

        if (FhApi.Saves.ui_get(settings.selected_ui.get(), out ui)) {
            return ui;
        }

        // Something has gone disastrously wrong – we're missing our default UI!
        _logger.Error("Failed to find default save/load UI.");
        throw new NotImplementedException("Failed to find default save/load UI.");
    }

    public override void render_imgui() {
        if (!FhApi.Saves.impl_get_system_mode(out FhSaveSystemMode? mode)
            || mode is FhSaveSystemMode.NULL
        ) {
            return;
        }

        FhSaveUi ui = get_ui();

        if (FhSaves.pal_state_screen is SaveScreenState.OPEN) {
            ui.render_ui();
        }
    }
}
