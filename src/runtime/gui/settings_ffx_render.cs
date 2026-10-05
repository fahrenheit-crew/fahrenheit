// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime.Gui;

using SettingRenderer = FhSettingRenderer<FhSetting, FhSettingsUiX>;

public partial class FhSettingsUiX {
    public interface ICapturingRenderer {
        public string get_input_help_text();
    }

    private class FhSettingsUiXRenderer_Category
        : FhSettingRenderer<FhSettingsCategory, FhSettingsUiX> {

        internal protected override Vector2 get_size(FhSettingsUiX ui) {
            //TODO: Implement size calculation
            return new Vector2(0f, 0f);
        }

        internal protected override void render(FhSettingsUiX ui, FhSettingsCategory category, Rect max_bounds) {
            //TODO: Implement rendering
        }

        internal protected override void handle_input(FhSettingsUiX ui, FhSettingsCategory category) {
            if (!FhApi.Gui.is_any_pressed(FhApi.Gui.keys_confirm)) return;

            category.collapsed ^= true;
        }
    }

    private class FhSettingsUiXRenderer_Toggle
        : FhSettingRenderer<FhSettingToggle, FhSettingsUiX> {

        internal protected override Vector2 get_size(FhSettingsUiX ui) {
            //TODO: Implement size calculation
            return new Vector2(0f, 0f);
        }

        internal protected override void render(FhSettingsUiX ui, FhSettingToggle toggle, Rect max_bounds) {
            //TODO: Implement rendering
        }

        internal protected override void handle_input(FhSettingsUiX ui, FhSettingToggle toggle) {
            ImGuiKey[] toggle_keys = [
                .. FhApi.Gui.keys_confirm,
                .. FhApi.Gui.keys_left,
                .. FhApi.Gui.keys_right,
            ];

            if (FhApi.Gui.is_any_pressed(toggle_keys)) {
                toggle.set(!toggle.get());
            }
        }
    }

    private class FhSettingsUiXRenderer_Text
        : FhSettingRenderer<FhSettingText, FhSettingsUiX>,
          ICapturingRenderer {

        private FhSettingText? focused_setting;
        private string?        buffer;

        internal protected override Vector2 get_size(FhSettingsUiX ui) {
            //TODO: Implement size calculation
            return new Vector2(0f, 0f);
        }

        internal protected override void render(FhSettingsUiX ui, FhSettingText text, Rect max_bounds) {
            //TODO: Implement rendering
        }

        internal protected override void handle_input(FhSettingsUiX ui, FhSettingText text) {
            ImGuiIOPtr io = ImGui.GetIO();

            if (focused_setting == null) {
                if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_confirm)) {
                    io.WantCaptureKeyboard = true;

                    ui.try_capture_setting(this, text);

                    focused_setting = text;
                    buffer          = text.get();
                }

                return;
            }

            if (focused_setting != text) return;

            if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_confirm)) {
                io.WantCaptureKeyboard = true;

                text.set(buffer!);

                ui.try_release_setting(this);

                focused_setting = null;
                buffer          = null;
                return;
            }

            if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_cancel)) {
                io.WantCaptureKeyboard = true;

                ui.try_release_setting(this);

                focused_setting = null;
                buffer          = null;
                return;
            }

            //TODO: Implement input handling

            // Yes, we're making a single-line text editor here.
            // This should probably be a helper on the `FhSettingText` class once it's done.
        }

        public string get_input_help_text() {
            return FhApi.Localization.localize($"{typeof(FhSettingsUiBase).FullName}.help.input.text");
        }
    }

    private class FhSettingsUiXRenderer_Number<T>
        : FhSettingRenderer<FhSettingNumber<T>, FhSettingsUiX>
        where T : unmanaged, INumber<T> {

        internal protected override Vector2 get_size(FhSettingsUiX ui) {
            //TODO: Implement size calculation
            return new Vector2(0f, 0f);
        }

        internal protected override void render(FhSettingsUiX ui, FhSettingNumber<T> number, Rect max_bounds) {
            //TODO: Implement rendering
        }

        internal protected override void handle_input(FhSettingsUiX ui, FhSettingNumber<T> number) {
            //TODO: Implement input handling
        }
    }
}
