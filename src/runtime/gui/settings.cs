// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime.Gui;

[FhLoad(FhGameId.FFX | FhGameId.FFX2 | FhGameId.FFX2LM)]
public sealed class FhSettingsUiSelector : FhModule {
    private FhSettingsUiX?  _ui_x;
    private FhSettingsUiX2? _ui_x2;

    private string _selected_ui = string.Empty;

    public bool is_open { get; private set; }

    internal sealed class UiSelectionSetting(string id, string def_value) : FhSettingDynamicChoice<string>(id, def_value) {
        protected override IReadOnlyList<object> init_choices() {
            //TODO: Unhardcode this once we can safely retrieve all registered UIs (post module ctor, pre module init)
            List<string> ui_types = [
                typeof(FhSettingsUiX) .FullName!,
                typeof(FhSettingsUiX2).FullName!,
            ];

            return ui_types;
        }

        internal override void init() {
            base.init();

            set(get_default_ui_id());
        }
    }

    internal class Settings : FhSettingProvider<FhSettingsUiSelector> {
        public readonly UiSelectionSetting selected_ui = new("selected_ui", string.Empty);

        internal protected override IEnumerable<FhSetting> get() {
            return [ selected_ui ];
        }
    }

    internal readonly Settings settings = new();

    private static string get_default_ui_id() {
        return FhUtil.select(
            typeof(FhSettingsUiX) .FullName!,
            typeof(FhSettingsUiX2).FullName!,
            typeof(FhSettingsUiX2).FullName!
        );
    }

    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        return new FhModuleHandle<FhSettingsUiX> (this).try_get_module(out _ui_x)
            && new FhModuleHandle<FhSettingsUiX2>(this).try_get_module(out _ui_x2);;
    }

    private void open() {
        _selected_ui = settings.selected_ui.get();
        if (FhInternal.Settings.get_ui(_selected_ui, out _)) return;

        _logger.Warning($"Failed to find desired settings UI \"{settings.selected_ui.get()}\", falling back to default.");

        settings.selected_ui.set(get_default_ui_id());
        _selected_ui = settings.selected_ui.get();
        if (FhInternal.Settings.get_ui(settings.selected_ui.get(), out _)) return;

        // Something has gone disastrously wrong – we're missing our default UI!
        _logger.Error("Failed to find default settings UI.");

        throw new NotImplementedException("Failed to find default settings UI.");
    }

    private void close() {
        _selected_ui = string.Empty;
    }

    public override void render_imgui() {
        if (is_open != (is_open ^= ImGui.IsKeyPressed(ImGuiKey.F7))) {
            if (is_open) {
                open();
                FhApi.Events.Common.GameLoop.PostOpenSettingsMenu.invoke(EventArgs.Empty);
            }
            else {
                close();
                FhApi.Events.Common.GameLoop.PostCloseSettingsMenu.invoke(EventArgs.Empty);
            }
        }

        //TODO: Add visual button to open the UI on the main menu
        //TODO: Prevent settings UI from being opened outside of the main menu

        if (!is_open) {
            return;
        }

        FhInternal.Settings.get_ui(_selected_ui, out FhSettingsUi? ui);
        ui!.render_ui();
    }
}
