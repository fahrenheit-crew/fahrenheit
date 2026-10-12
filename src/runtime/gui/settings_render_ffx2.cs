// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime.Gui;

using SettingRenderer = FhSettingRenderer<FhSetting, FhSettingsUiX2>;

public partial class FhSettingsUiX2 {
    public interface ICapturingRenderer {
        public string get_input_help_text();
    }

    private class Renderer_Category
        : FhSettingRenderer<FhSettingsCategory, FhSettingsUiX2> {

        internal protected override Vector2 get_size(FhSettingsUiX2 ui) {
            return new Vector2(0f, 38f);
        }

        internal protected override void render(FhSettingsUiX2 ui, FhSettingsCategory category, Rect max_bounds) {
            if (
                !ui._texture_plate  .try_use(out ImTextureRef plate,   out _)
             || !ui._texture_b_menu1.try_use(out ImTextureRef b_menu1, out _)
            ) {
                return;
            }

            ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

            UV plate_tuv = new Rect {
                pos  = new(  0f, 252f),
                size = new(512f,  38f),
            }.as_uv(ui._tex_plate_size);

            uint color = 0xFFFFFFFF;

            float top_width    = 5f * ui.aspect_scale.Y;
            float left_width   = 6f * ui.aspect_scale.Y;
            float bottom_width = 4f * ui.aspect_scale.Y;
            float right_width  = 6f * ui.aspect_scale.Y;

            FhApi.Gui.X2.draw_plate(
                draw,
                max_bounds.scale_to_aspect(ui.aspect_helper),
                plate,
                plate_tuv,
                color,
                top_width,
                left_width,
                bottom_width,
                right_width,
                ui._shadow_pos
            );

            // Arrow
            Rect arrow_tuv = new Rect {
                pos  = new(965f, 913f),
                size = new( 53f,  30f),
            }.scale_raw(Vector2.One / ui._tex_b_menu1_size);
            // We do some shenanigans for the various corner accessors
            // to work like we want them to
            arrow_tuv.pos.Y += arrow_tuv.size.Y;
            arrow_tuv.size.Y *= -1f;

            Rect arrow_bounds = new() {
                pos = max_bounds.left with {
                    X = max_bounds.left.X + 35f,
                },
                size = new(32f, 22f),
            };

            arrow_bounds.pos.Y -= arrow_bounds.size.Y / 2f;

            Rect arrow_suv = arrow_bounds.scale_to_aspect(ui.aspect_helper);

            Rect arrow_hitbox = arrow_bounds.expand(
                new(
                    25f,
                    (max_bounds.size.Y - arrow_bounds.size.Y) / 2f
                ),
                new(Alignment.BEGIN, Alignment.CENTER)
            ).scale_to_aspect(ui.aspect_helper);

            bool hovering_arrow = ui.mouse_hovered(arrow_hitbox, false);
            uint arrow_color = hovering_arrow ? 0xFFE0F0FF : 0xFFFFFFFF; // TODO: Find a better hovered color

            if (category.collapsed) {
                // Upside-right
                draw.AddImageQuad(
                    b_menu1,

                    arrow_suv.top_right,
                    arrow_suv.bottom_right,
                    arrow_suv.bottom_left,
                    arrow_suv.top_left,

                    arrow_tuv.bottom_left,
                    arrow_tuv.bottom_right,
                    arrow_tuv.top_right,
                    arrow_tuv.top_left,

                    arrow_color
                );
            }
            else {
                // Upside-down
                draw.AddImageQuad(
                    b_menu1,

                    arrow_suv.bottom_left,
                    arrow_suv.bottom_right,
                    arrow_suv.top_right,
                    arrow_suv.top_left,

                    arrow_tuv.bottom_left,
                    arrow_tuv.bottom_right,
                    arrow_tuv.top_right,
                    arrow_tuv.top_left,

                    arrow_color
                );
            }

            // Name
            float font_size = 30f * ui.font_scale;

            Vector2 name_pos = arrow_bounds.right;
            name_pos.X += 30f;
            name_pos.Y -=  1f;

            FhApi.Gui.draw_text(
                draw,
                name_pos * ui.aspect_scale + ui.aspect_helper.pos,
                category.name,
                font_size,
                true,
                new(Alignment.BEGIN, Alignment.CENTER)
            );

            // Input handling
            if (ui.mouse_clicked(arrow_hitbox)) {
                category.collapsed ^= true;
            }
        }

        internal protected override void handle_input(FhSettingsUiX2 ui, FhSettingsCategory category) {
            if (!FhApi.Gui.is_any_pressed(FhApi.Gui.keys_confirm)) return;

            category.collapsed ^= true;
        }
    }

    private class Renderer_Toggle
        : FhSettingRenderer<FhSettingToggle, FhSettingsUiX2> {

        internal protected override Vector2 get_size(FhSettingsUiX2 ui) {
            return new Vector2(0f, 60f);
        }

        internal protected override void render(FhSettingsUiX2 ui, FhSettingToggle toggle, Rect max_bounds) {
            if (
                !ui._texture_plate .try_use(out ImTextureRef plate,  out _)
             || !ui._texture_window.try_use(out ImTextureRef window, out _)
             || !ui._texture_wave  .try_use(out ImTextureRef wave,   out _)
            ) {
                return;
            }

            ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

            Rect plate_bounds = new Rect {
                pos  = max_bounds.pos,
                size = new Vector2(440f, max_bounds.size.Y),
            };

            Rect window_bounds = new Rect {
                pos  = max_bounds.pos + new Vector2(500f, 0f),
                size = new Vector2(600f, max_bounds.size.Y),
            };

            float slice_size  = 64f;
            float plate_max_y = 512f - ((ui._setting_idx % 8) * slice_size);

            UV plate_tuv = new Rect {
                pos  = new(  0f, plate_max_y - slice_size),
                size = new(512f,               slice_size),
            }.as_uv(ui._tex_plate_size);

            uint color = 0xFFFFFFFF;

            float border_width = 5f * ui.aspect_scale.Y;

            FhApi.Gui.X2.draw_plate(
                draw,
                plate_bounds.scale_to_aspect(ui.aspect_helper),
                plate,
                plate_tuv,
                color,
                border_width,
                ui._shadow_pos
            );

            FhApi.Gui.X2.draw_message_window(
                draw,
                window_bounds.scale_to_aspect(ui.aspect_helper),
                window,
                ui._message_window,
                new Vector2(4f, 4f) * ui.aspect_scale,
                0xFF808080,
                wave,
                1f,
                0x80A1C6C6
            );

            float font_size = 36f * ui.font_scale;

            // Name
            Vector2 name_pos = plate_bounds.center;
            name_pos.Y -= 1f;

            FhApi.Gui.draw_text(
                draw,
                name_pos * ui.aspect_scale + ui.aspect_helper.pos,
                toggle.name,
                font_size,
                true,
                new(Alignment.CENTER, Alignment.CENTER)
            );

            // Options
            // TODO: If option is selected/active, draw with color 0xFF19D8FF
            Vector2 on_pos  = window_bounds.left  + new Vector2(175f, -1f);
            Vector2 off_pos = window_bounds.right - new Vector2(175f,  1f);

            Vector2 on_size = FhApi.Gui.draw_text(
                draw,
                on_pos * ui.aspect_scale + ui.aspect_helper.pos,
                FhApi.Localization.localize($"{typeof(FhSettingsUiBase)}.input.on"),
                font_size,
                true,
                new(Alignment.CENTER, Alignment.CENTER)
            );

            Vector2 off_size = FhApi.Gui.draw_text(
                draw,
                off_pos * ui.aspect_scale + ui.aspect_helper.pos,
                FhApi.Localization.localize($"{typeof(FhSettingsUiBase)}.input.off"),
                font_size,
                true,
                new(Alignment.CENTER, Alignment.CENTER)
            );

            // Highlight
            Vector2 highlight_pos = toggle.get() ? on_pos : off_pos;

            Vector2 highlight_size = new(135f, 38f);

            highlight_pos.X -= highlight_size.X / 2f;
            highlight_pos.Y -= 18f;

            GradientStep[] steps = [
                new(0.0f, 0x000E91A9),
                new(0.2f, 0x400E91A9),
                new(0.8f, 0x400E91A9),
                new(1.0f, 0x000E91A9),
            ];

            Rect enabled_highlight = new Rect {
                pos  = highlight_pos,
                size = highlight_size,
            }.scale_to_aspect(ui.aspect_helper);

            FhApi.Gui.draw_rectangle_gradient(
                draw,
                enabled_highlight,
                GradientDirection.RIGHT,
                steps
            );
        }

        internal protected override void handle_input(FhSettingsUiX2 ui, FhSettingToggle toggle) {
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

    private class Renderer_Text
        : FhSettingRenderer<FhSettingText, FhSettingsUiX2>,
          ICapturingRenderer {

        private FhSettingText? focused_setting;
        private string?        buffer;

        internal protected override Vector2 get_size(FhSettingsUiX2 ui) {
            //TODO: Implement size calculation
            return new Vector2(0f, 0f);
        }

        internal protected override void render(FhSettingsUiX2 ui, FhSettingText text, Rect max_bounds) {
            //TODO: Implement rendering
        }

        internal protected override void handle_input(FhSettingsUiX2 ui, FhSettingText text) {
            ImGuiIOPtr io = ImGui.GetIO();

            if (focused_setting == null) {
                if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_confirm)) {
                    io.WantCaptureKeyboard = true;

                    ui.try_capture_setting(this, text);

                    focused_setting = text;
                    buffer = text.get();
                }

                return;
            }

            if (focused_setting != text) return;

            if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_confirm)) {
                io.WantCaptureKeyboard = true;

                text.set(buffer!);

                ui.try_release_setting(this);

                focused_setting = null;
                buffer = null;
                return;
            }

            if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_cancel)) {
                io.WantCaptureKeyboard = true;

                ui.try_release_setting(this);

                focused_setting = null;
                buffer = null;
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

    private class Renderer_Number<T>
        : FhSettingRenderer<FhSettingNumber<T>, FhSettingsUiX2>
        where T : unmanaged, INumber<T> {

        internal protected override Vector2 get_size(FhSettingsUiX2 ui) {
            //TODO: Implement size calculation
            return new Vector2(0f, 0f);
        }

        internal protected override void render(FhSettingsUiX2 ui, FhSettingNumber<T> number, Rect max_bounds) {
            //TODO: Implement rendering
        }

        internal protected override void handle_input(FhSettingsUiX2 ui, FhSettingNumber<T> number) {
            //TODO: Implement input handling
        }
    }
}
