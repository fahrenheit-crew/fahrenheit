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

    private class Renderer_Category
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

    private class Renderer_Toggle
        : FhSettingRenderer<FhSettingToggle, FhSettingsUiX> {

        internal protected override Vector2 get_size(FhSettingsUiX ui) {
            return new Vector2(0f, 65f);
        }

        internal protected override void render(FhSettingsUiX ui, FhSettingToggle toggle, Rect max_bounds) {
            if (
                !ui._texture_battle_kuang.try_use(out ImTextureRef battle_kuang, out _)
                || !ui._texture_meswin   .try_use(out ImTextureRef meswin, out _)
            ) {
                return;
            }

            ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

            // Shadow
            float shadow_height = 5f;
            Rect shadow = max_bounds with {
                pos  = max_bounds.pos  with { Y = max_bounds.bottom_left.Y - shadow_height },
                size = max_bounds.size with { Y = shadow_height },
            };

            GradientStep[] shadow_steps = [
                new(0.0f, 0x00000000),
                new(0.2f, 0x80000000),
                new(0.8f, 0x80000000),
                new(1.0f, 0x00000000),
            ];

            FhApi.Gui.draw_rectangle_gradient(
                draw,
                shadow.scale_to_aspect(ui.aspect_helper),
                GradientDirection.RIGHT,
                shadow_steps
            );

            // Background
            Rect bg_bounds = max_bounds;
            bg_bounds.size.Y -= shadow_height;

            GradientStep[] bg_steps = [
                new(0.0f, 0x0070212A),
                new(0.2f, 0xA070212A),
                new(0.5f, 0xA070212A),
                new(0.8f, 0xA0030303),
                new(1.0f, 0x00030303),
            ];

            FhApi.Gui.draw_rectangle_gradient(
                draw,
                bg_bounds.scale_to_aspect(ui.aspect_helper),
                GradientDirection.RIGHT,
                bg_steps
            );

            // Separator
            UV separator_tuv = new Rect {
                pos  = new(1515f, 797f),
                size = new(88f, 57f),
            }.as_uv(ui._tex_battle_kuang_size);

            Rect separator_start = new() {
                pos  = new(
                    bg_bounds.pos.X + bg_bounds.size.X * 0.32f,
                    bg_bounds.pos.Y + 2f
                ),
                size = new(88f, 57f),
            };

            UV separator_suv = separator_start
                .scale_to_aspect(ui.aspect_helper)
                .as_uv();

            draw.AddImage(
                battle_kuang,
                separator_suv.p0,
                separator_suv.p1,
                separator_tuv.p0,
                separator_tuv.p1,
                0x80FFFFFF
            );

            // Separator Fade
            uint color_separator_l = 0x70FFFFFF;
            uint color_separator_r = 0x00FFFFFF;

            float separator_width = 245f * ui.aspect_scale.X;

            float separator_diff_width = separator_start.size.Y;

            Vector2 separator_tl = ui.aspect_scale * separator_start.pos with {
                X = separator_start.pos.X + separator_start.size.X,
            };

            Vector2 separator_tr = separator_tl with {
                X = separator_tl.X + separator_width - separator_diff_width,
            };

            Vector2 separator_bl = ui.aspect_scale * (separator_start.pos + separator_start.size);

            Vector2 separator_br = separator_bl with {
                X = separator_bl.X + separator_width,
            };

            FhApi.Gui.draw_quad_gradient(
                draw,
                [
                    separator_tl,
                    separator_tr,
                    separator_bl,
                    separator_br,
                ],
                [
                    color_separator_l,
                    color_separator_r,
                    color_separator_l,
                    color_separator_r,
                ]
            );

            // Borders
            GradientStep[] top_steps = [
                new(0.0f, 0x00CCCCDD),
                new(0.2f, 0xFFCCCCDD),
                new(0.8f, 0xFFCCCCDD),
                new(1.0f, 0x00CCCCDD),
            ];

            GradientStep[] bottom_steps = [
                new(0.0f, 0x00000000),
                new(0.2f, 0xFF000000),
                new(0.8f, 0xFF000000),
                new(1.0f, 0x00000000),
            ];

            Rect top_border = new Rect {
                pos  = bg_bounds.pos  with { Y = bg_bounds.top.Y + 1f },
                size = bg_bounds.size with { Y = 1f },
            }.scale_to_aspect(ui.aspect_helper, new(1f));

            Rect bottom_border = new Rect {
                pos  = bg_bounds.pos  with { Y = bg_bounds.bottom.Y - 1f },
                size = bg_bounds.size with { Y = 1f },
            }.scale_to_aspect(ui.aspect_helper, new(1f));

            FhApi.Gui.draw_rectangle_gradient(
                draw,
                top_border,
                GradientDirection.RIGHT,
                top_steps
            );

            FhApi.Gui.draw_rectangle_gradient(
                draw,
                bottom_border,
                GradientDirection.RIGHT,
                bottom_steps
            );

            // Name
            float font_size = 36f * ui.font_scale;

            Vector2 name_pos = separator_start.left;
            name_pos.X += 5f;

            FhApi.Gui.draw_text(
                draw,
                name_pos * ui.aspect_scale,
                toggle.name,
                font_size,
                true,
                new(Alignment.END, Alignment.CENTER)
            );

            // Options
            float post_separator_width = bg_bounds.right.X - separator_start.left.X;

            Vector2 on_pos  = separator_start.left;
            Vector2 off_pos = separator_start.left;

            on_pos.X  += post_separator_width * 1f/3f;
            off_pos.X += post_separator_width * 2f/3f;

            FhApi.Gui.draw_text(
                draw,
                on_pos * ui.aspect_scale,
                FhApi.Localization.localize($"{typeof(FhSettingsUiBase)}.input.on"),
                font_size,
                true,
                new(Alignment.CENTER, Alignment.CENTER)
            );

            FhApi.Gui.draw_text(
                draw,
                off_pos * ui.aspect_scale,
                FhApi.Localization.localize($"{typeof(FhSettingsUiBase)}.input.off"),
                font_size,
                true,
                new(Alignment.CENTER, Alignment.CENTER)
            );

            // Underline
            Vector2 enabled_pos = toggle.get() ? on_pos : off_pos;

            Vector2 underline_pos = enabled_pos with {
                Y = enabled_pos.Y + 15f,
            };

            Vector2 underline_size = new(220f, 9f);

            underline_pos.X -= underline_size.X / 2f;

            UV underline_tuv = new Rect {
                pos  = new(0f, 582f),
                size = new(300f, 9f),
            }.as_uv(ui._tex_meswin_size);

            UV underline_suv = new Rect {
                pos  = underline_pos,
                size = underline_size,
            }.scale_to_aspect(ui.aspect_helper).as_uv();

            draw.AddImage(
                meswin,
                underline_suv.p0,
                underline_suv.p1,
                underline_tuv.p0,
                underline_tuv.p1
            );
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

    private class Renderer_Text
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

    private class Renderer_Number<T>
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
