// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime.Gui;

public partial class FhSettingsUiX {
    private abstract class Renderer_Base<T> : SettingRenderer
        where T : FhSetting {

        public override Vector2 get_size(FhSettingsUiX ui) => new(0f, 65f);

        public override void handle_input(FhSettingsUiX ui, FhSetting setting) {
            handle_input(ui, (T)setting);
        }

        public override void render(FhSettingsUiX ui, FhSetting setting, Rect max_bounds) {
            render(ui, (T)setting, max_bounds);
        }

        protected abstract void handle_input(FhSettingsUiX ui, T setting);
        protected abstract void render(FhSettingsUiX ui, T setting, Rect max_bounds);

        protected bool render_background(
            FhSettingsUiX ui,

            Rect max_bounds,

            [NotNullWhen(true)] out Rect? name_bounds,
            [NotNullWhen(true)] out Rect? value_bounds
        ) {
            if (!ui._texture_battle_kuang.try_use(out ImTextureRef battle_kuang, out FhTextureMetadata? battle_kuang_meta)) {
                name_bounds  = null;
                value_bounds = null;
                return false;
            }

            GradientStep[] make_gradient(uint color) {
                return [
                    new(0.0f, color & 0x00FFFFFF),
                    new(0.2f, color),
                    new(0.8f, color),
                    new(1.0f, color & 0x00FFFFFF),
                ];
            }

            ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

            // Shadow
            float shadow_height = 5f;
            Rect shadow = max_bounds with {
                pos  = max_bounds.pos  with { Y = max_bounds.bottom_left.Y - shadow_height },
                size = max_bounds.size with { Y = shadow_height },
            };

            GradientStep[] shadow_steps = make_gradient(0x60000000);

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
            }.as_uv(battle_kuang_meta.size);

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

            float separator_width = 245f;

            float separator_diff_width = separator_start.size.Y;

            Vector2 separator_tl = separator_start.pos with {
                X = separator_start.pos.X + separator_start.size.X,
            };

            Vector2 separator_tr = separator_tl with {
                X = separator_tl.X + separator_width - separator_diff_width,
            };

            Vector2 separator_bl = separator_start.pos + separator_start.size;

            Vector2 separator_br = separator_bl with {
                X = separator_bl.X + separator_width,
            };

            separator_tl *= ui.aspect_scale;
            separator_tr *= ui.aspect_scale;
            separator_bl *= ui.aspect_scale;
            separator_br *= ui.aspect_scale;

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
            GradientStep[] top_steps    = make_gradient(0xFFCCCCDD);
            GradientStep[] bottom_steps = make_gradient(0xFF000000);

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

            name_bounds = new Rect {
                pos  = bg_bounds.top_left,
                size = bg_bounds.size with {
                    X = separator_start.left.X - bg_bounds.left.X + 5f,
                },
            };

            float separator_right = separator_start.right.X + 10f;

            value_bounds = new Rect {
                pos  = separator_start.top_right with {
                    X = separator_right,
                },
                size = bg_bounds.size with {
                    X = bg_bounds.right.X - separator_right,
                },
            };

            return true;
        }

        protected void render_name(FhSettingsUiX ui, Rect name_bounds, string name) {
            ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

            FhApi.Gui.draw_text(
                draw,
                name_bounds.right * ui.aspect_scale,
                name,
                ui.text_config with { align = Alignment2D.RIGHT }
            );
        }
    }

    private sealed class Renderer_Category : Renderer_Base<FhSettingsCategory> {
        public static Renderer_Category instance = new();

        public override Vector2 get_size(FhSettingsUiX ui) {
            return new Vector2(0f, 45f);
        }

        private GradientStep[] make_gradient(uint color) {
            return [
                new(0.0f,  color & 0x00FFFFFF),
                new(0.05f, color),
                new(0.6f,  color),
                new(1.0f,  color & 0x00FFFFFF),
            ];
        }

        protected override void render(FhSettingsUiX ui, FhSettingsCategory category, Rect max_bounds) {
            if (!ui._texture_battle.try_use(out ImTextureRef battle, out FhTextureMetadata? battle_meta)) {
                return;
            }

            ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

            // Shadow
            float shadow_height = 5f;
            Rect shadow = max_bounds with {
                pos  = max_bounds.pos  with { Y = max_bounds.bottom_left.Y - shadow_height },
                size = max_bounds.size with { Y = shadow_height },
            };

            GradientStep[] shadow_steps = make_gradient(0x60000000);

            FhApi.Gui.draw_rectangle_gradient(
                draw,
                shadow.scale_to_aspect(ui.aspect_helper),
                GradientDirection.RIGHT,
                shadow_steps
            );

            // Background
            Rect bg_bounds = max_bounds;
            bg_bounds.size.Y -= shadow_height;

            GradientStep[] bg_steps = make_gradient(0xA070212A);

            FhApi.Gui.draw_rectangle_gradient(
                draw,
                bg_bounds.scale_to_aspect(ui.aspect_helper),
                GradientDirection.RIGHT,
                bg_steps
            );

            // Separator
            Rect sep_bounds = (bg_bounds with {
                pos = bg_bounds.pos with {
                    Y = bg_bounds.pos.Y + bg_bounds.size.Y - 2f,
                },

                size = bg_bounds.size with {
                    Y = 1f,
                },
            }).scale_to_aspect(ui.aspect_helper, new(1f));

            GradientStep[] sep_steps = make_gradient(0xFFCCCCDD);

            FhApi.Gui.draw_rectangle_gradient(
                draw,
                sep_bounds,
                GradientDirection.RIGHT,
                sep_steps
            );

            // Arrow
            Rect arrow_tuv = new Rect {
                pos  = new(706f, 376f),
                size = new(49f, 49f),
            }.scale_raw(Vector2.One / battle_meta.size);

            // We do some shenanigans for the various corner accessors
            // to work like we want them to
            arrow_tuv.pos.Y  += arrow_tuv.size.Y;
            arrow_tuv.size.Y *= -1f;

            Rect arrow_bounds = new() {
                pos = bg_bounds.left with {
                    X = bg_bounds.left.X + 35f,
                },
                size = new(36f, 36f),
            };

            arrow_bounds.pos.Y -= arrow_bounds.size.Y / 2f;

            Rect arrow_suv = arrow_bounds.scale_to_aspect(ui.aspect_helper);

            Rect arrow_hitbox = arrow_bounds.expand(
                new(
                    25f,
                    (max_bounds.size.Y - shadow_height - arrow_bounds.size.Y) / 2f
                ),
                new(Alignment.BEGIN, Alignment.CENTER)
            ).scale_to_aspect(ui.aspect_helper);

            bool hovering_arrow = ui.mouse_hovered(arrow_hitbox, false);
            uint arrow_color = hovering_arrow ? 0xFFA0F0FF : 0xFFFFFFFF;

            if (category.collapsed) {
                // Upside-right
                draw.AddImageQuad(
                    battle,

                    arrow_suv.top_right,
                    arrow_suv.bottom_right,
                    arrow_suv.bottom_left,
                    arrow_suv.top_left,

                    arrow_tuv.top_left,
                    arrow_tuv.top_right,
                    arrow_tuv.bottom_right,
                    arrow_tuv.bottom_left,

                    arrow_color
                );
            }
            else {
                // Upside-down
                draw.AddImageQuad(
                    battle,

                    arrow_suv.bottom_left,
                    arrow_suv.bottom_right,
                    arrow_suv.top_right,
                    arrow_suv.top_left,

                    arrow_tuv.top_left,
                    arrow_tuv.top_right,
                    arrow_tuv.bottom_right,
                    arrow_tuv.bottom_left,

                    arrow_color
                );
            }

            // Name
            Vector2 name_pos = arrow_bounds.right;
            name_pos.X += 30f;

            FhApi.Gui.draw_text(
                draw,
                name_pos * ui.aspect_scale,
                category.name,
                ui.text_config with { align = Alignment2D.LEFT }
            );

            // Input handling
            if (ui.mouse_clicked(arrow_hitbox)) {
                category.collapsed ^= true;
            }
        }

        protected override void handle_input(FhSettingsUiX ui, FhSettingsCategory category) {
            if (!FhApi.Gui.is_any_pressed(FhApi.Gui.keys_confirm)) return;

            category.collapsed ^= true;
        }
    }

    private sealed class Renderer_Toggle : Renderer_Base<FhSettingToggle> {
        public static Renderer_Toggle instance = new();

        private static string _text_on  => FhApi.Localization.localize($"{typeof(FhSettingsUi)}.input.on");
        private static string _text_off => FhApi.Localization.localize($"{typeof(FhSettingsUi)}.input.off");

        private void render_options(FhSettingsUiX ui, FhSettingToggle toggle, Rect value_bounds) {
            if (!ui._texture_meswin.try_use(out ImTextureRef meswin, out FhTextureMetadata? meswin_meta)) {
                return;
            }

            ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

            // Options
            Vector2 on_pos  = value_bounds.left;
            Vector2 off_pos = value_bounds.left;

            on_pos.X  += value_bounds.size.X * 3f/12f;
            off_pos.X += value_bounds.size.X * 8f/12f;

            FhApi.Gui.draw_text(
                draw,
                on_pos * ui.aspect_scale,
                _text_on,
                ui.text_config with { align = Alignment2D.CENTER }
            );

            FhApi.Gui.draw_text(
                draw,
                off_pos * ui.aspect_scale,
                _text_off,
                ui.text_config with { align = Alignment2D.CENTER }
            );

            // Underline
            Vector2 underline_size = new(220f, 9f);

            Rect underline_on = new Rect {
                pos = new() {
                    X = on_pos.X - underline_size.X / 2f,
                    Y = on_pos.Y + 15f,
                },
                size = underline_size,
            }.scale_to_aspect(ui.aspect_helper);

            Rect underline_off = new Rect {
                pos = new() {
                    X = off_pos.X - underline_size.X / 2f,
                    Y = off_pos.Y + 15f,
                },
                size = underline_size,
            }.scale_to_aspect(ui.aspect_helper);

            (Rect underline_active, Rect underline_inactive) =
                toggle.get()
                    ? (underline_on,  underline_off)
                    : (underline_off, underline_on);

            UV underline_tuv = new Rect {
                pos  = new(0f, 582f),
                size = new(300f, 9f),
            }.as_uv(meswin_meta.size);

            UV underline_suv = underline_active.as_uv();

            draw.AddImage(
                meswin,
                underline_suv.p0,
                underline_suv.p1,
                underline_tuv.p0,
                underline_tuv.p1
            );

            Rect hitbox_inactive = new Rect {
                pos  = (toggle.get() ? off_pos : on_pos) with {
                    Y = value_bounds.top.Y,
                },
                size = new() {
                    Y = value_bounds.size.Y,
                },
            }.expand(
                new(off_pos.X - on_pos.X, 0f),
                new(Alignment.CENTER, Alignment.BEGIN)
            ).scale_to_aspect(ui.aspect_helper);

            // Extra underline on hover
            if (ui.mouse_hovered(hitbox_inactive, false)) {
                underline_suv = underline_inactive.as_uv();

                draw.AddImage(
                    meswin,
                    underline_suv.p0,
                    underline_suv.p1,
                    underline_tuv.p0,
                    underline_tuv.p1,
                    0x80FFFFFF
                );
            }

            if (ui.mouse_clicked(hitbox_inactive)) {
                toggle.set(!toggle.get());
            }
        }

        protected override void render(FhSettingsUiX ui, FhSettingToggle toggle, Rect max_bounds) {
            if (!render_background(
                ui,
                max_bounds,
                out Rect? name_bounds,
                out Rect? value_bounds
            )) {
                return;
            }

            render_name(ui, (Rect)name_bounds, toggle.name);

            render_options(ui, toggle, (Rect)value_bounds);
        }

        protected override void handle_input(FhSettingsUiX ui, FhSettingToggle toggle) {
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

    private sealed class Renderer_Number : Renderer_Base<FhSettingNumber> {
        public static Renderer_Number instance = new();

        public override Vector2 get_size(FhSettingsUiX ui) {
            //TODO: Implement size calculation
            return new Vector2(0f, 0f);
        }

        protected override void render(FhSettingsUiX ui, FhSettingNumber number, Rect max_bounds) {
            //TODO: Implement rendering
        }

        protected override void handle_input(FhSettingsUiX ui, FhSettingNumber number) {
            //TODO: Implement input handling
        }
    }

    private class Renderer_Carousel<T> : Renderer_Base<FhSettingCarousel<T>> {
        private const float SPIN_SPEED = 2f; // in progress per second
        private const float OPTION_SPIN_DIFF = 1f/3f;

        // Between -1 and 1. 0 when no spin is happening
        // Technically between -OPTION_SPIN_DIFF and OPTION_SPIN_DIFF
        // Equal to the spin progress of the current value
        private Dictionary<FhSetting, float> spin_progress = [];

        private int get_prev_index(ReadOnlySpan<T> choices, T choice) {
            int index = choices.IndexOf(choice);
            if (index == 0) return choices.Length - 1;
            return index - 1;
        }

        private int get_next_index(ReadOnlySpan<T> choices, T choice) {
            int index = choices.IndexOf(choice);
            return (index + 1) % choices.Length;
        }

        private float remap_spin_progress(float t) {
            // We remap progress to give us a nice arc
            //TODO: Redo this arc: bezier((0,0), (0.25, 0), (0.2, 0.8), (1, 1))
            float sign = t < 0 ? -1 : 1;
            return sign * (1 - float.Cos(float.Pow(float.Abs(t), 0.7f) * float.Pi)) / 2f;
        }

        private uint get_alpha_for_spin(float progress) {
            byte alpha = (byte)(0x60 + (0xFF - 0x60) * (1f - float.Abs(remap_spin_progress(progress))));
            return (uint)((alpha << 24) | 0xFFFFFF);
        }

        private Vector2 get_offset_for_spin(Vector2 center, Vector2 side, float progress) {
            Vector2 max_offset = side - center;

            bool other_way = progress < 0f;

            progress = float.Abs(remap_spin_progress(progress));

            Vector2 offset = Vector2.Lerp(Vector2.Zero, max_offset, progress);

            if (other_way) offset.X *= -1f;

            return offset;
        }

        private void render_option(FhSettingsUiX ui, FhSettingCarousel<T> carousel, int index, float progress, Rect value_bounds) {
            Vector2 center = value_bounds.center;
            Vector2 right  = value_bounds.right;
            right.X -= value_bounds.size.X / 6f;

            ReadOnlySpan<T> choices = carousel.get_choice_values();

            Vector2 pos   = center + get_offset_for_spin(center, right, progress);
            uint    color = get_alpha_for_spin(progress);
            float   scale = 1f - float.Abs(remap_spin_progress(progress)) / 3f;
            string  name  = FhApi.Localization.localize(carousel.get_choice_name(choices[index]));

            ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

            FhApi.Gui.draw_text(
                draw,
                pos * ui.aspect_scale,
                name,
                ui.text_config with {
                    align     = Alignment2D.CENTER,
                    font_size = ui.text_config.font_size * scale,
                    color     = color,
                }
            );
        }

        private void render_options(FhSettingsUiX ui, FhSettingCarousel<T> carousel, Rect value_bounds) {
            T curr_choice = carousel.get();
            ReadOnlySpan<T> choices = carousel.get_choice_values();

            int curr_index = choices.IndexOf(curr_choice);
            int prev_index = get_prev_index(choices, curr_choice);
            int next_index = get_next_index(choices, curr_choice);

            int[] choice_indices = spin_progress[carousel] switch {
                0f => [
                    prev_index,
                    curr_index,
                    next_index,
                ],

                > 0f => [
                    prev_index,
                    curr_index,
                    next_index,
                    get_next_index(choices, choices[next_index]),
                ],

                < 0f => [
                    get_prev_index(choices, choices[prev_index]),
                    prev_index,
                    curr_index,
                    next_index,
                ],

                _ => throw new UnreachableException(),
            };

            int main_index = spin_progress[carousel] switch {
                  0f => 1,
                > 0f => 1,
                < 0f => 2,

                _ => throw new UnreachableException(),
            };

            for (int i = 0; i < main_index; i++) {
                float spin = spin_progress[carousel] - OPTION_SPIN_DIFF * (main_index - i);

                // This shouldn't be necessary but let's do it anyway
                spin = float.Clamp(spin, -1f, 1f);

                render_option(ui, carousel, choice_indices[i], spin, value_bounds);
            }

            render_option(ui, carousel, choice_indices[main_index], spin_progress[carousel], value_bounds);

            for (int i = main_index + 1; i < choice_indices.Length; i++) {
                float spin = spin_progress[carousel] + OPTION_SPIN_DIFF * (i - main_index);

                // This shouldn't be necessary but let's do it anyway
                spin = float.Clamp(spin, -1f, 1f);

                render_option(ui, carousel, choice_indices[i], spin, value_bounds);
            }
        }

        private void render_arrows(FhSettingsUiX ui, FhSettingCarousel<T> carousel, Rect value_bounds) {
            if (!ui._texture_battle.try_use(out ImTextureRef battle, out FhTextureMetadata? battle_meta)) {
                return;
            }

            float arrow_progress = float.Clamp(spin_progress[carousel] * 5f, 0f, 1f);

            Vector2 max_offset   = new(10f, 0f);

            UV arrow_tuv = new Rect {
                pos  = new(724f, 680f),
                size = new( 36f,  48f),
            }.as_uv(battle_meta.size);

            Vector2 arrow_size = new(36f, 48f);

            Rect arrow_left  = new() { pos = value_bounds.left  + max_offset };
            Rect arrow_right = new() { pos = value_bounds.right - max_offset };

            arrow_left .expand(arrow_size, new(Alignment.BEGIN, Alignment.CENTER));
            arrow_right.expand(arrow_size, new(Alignment.END,   Alignment.CENTER));

            if (arrow_progress < 0f)
                arrow_left.pos  += Vector2.Lerp(Vector2.Zero, max_offset, arrow_progress);
            else if (arrow_progress > 0f)
                arrow_right.pos += Vector2.Lerp(Vector2.Zero, max_offset, arrow_progress);

            UV arrow_left_suv  = arrow_left .scale_to_aspect(ui.aspect_helper).as_uv();
            UV arrow_right_suv = arrow_right.scale_to_aspect(ui.aspect_helper).as_uv();

            ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

            draw.AddImage(
                battle,
                arrow_left_suv.p0,
                arrow_left_suv.p1,
                arrow_tuv.p0,
                arrow_tuv.p1
            );

            draw.AddImage(
                battle,
                arrow_right_suv.p0,
                arrow_right_suv.p1,
                arrow_tuv.p0,
                arrow_tuv.p1
            );
        }

        protected override void render(FhSettingsUiX ui, FhSettingCarousel<T> carousel, Rect max_bounds) {
            if (!render_background(
                ui,
                max_bounds,
                out Rect? name_bounds,
                out Rect? value_bounds
            )) {
                return;
            }

            render_name(ui, (Rect)name_bounds, carousel.name);

            spin_progress.TryAdd(carousel, 0f);

            if (spin_progress[carousel] != 0f) {
                float spin_delta = SPIN_SPEED * ImGui.GetIO().DeltaTime;
                spin_delta = float.Min(spin_delta, spin_progress[carousel]);

                spin_progress[carousel] -= spin_delta * float.Sign(spin_progress[carousel]);
                spin_progress[carousel]  = float.Clamp(spin_progress[carousel], 0f, 1f);
            }

            render_options(ui, carousel, (Rect)value_bounds);
            render_arrows (ui, carousel, (Rect)value_bounds);
        }

        private void spin_left(FhSettingCarousel<T> carousel) {
            ReadOnlySpan<T> choices = carousel.get_choice_values();

            T curr_choice = carousel.get();
            int  prev_index  = get_prev_index(choices, curr_choice);
            T prev_choice = choices[prev_index];

            carousel.set(prev_choice);

            spin_progress[carousel] = OPTION_SPIN_DIFF;
        }

        private void spin_right(FhSettingCarousel<T> carousel) {
            ReadOnlySpan<T> choices = carousel.get_choice_values();

            T curr_choice = carousel.get();
            int  next_index  = get_next_index(choices, curr_choice);
            T next_choice = choices[next_index];

            carousel.set(next_choice);

            spin_progress[carousel] = -OPTION_SPIN_DIFF;
        }

        protected override void handle_input(FhSettingsUiX ui, FhSettingCarousel<T> choice) {
            if (spin_progress[choice] != 0f) return;

            if (FhApi.Gui.is_any_down(FhApi.Gui.keys_left)) {
                spin_right(choice);
                return;
            }

            if (FhApi.Gui.is_any_down(FhApi.Gui.keys_right)) {
                spin_left(choice);
                return;
            }
        }
    }

    private sealed class Renderer_Choice : Renderer_Carousel<long> {
        public static Renderer_Choice instance = new();
    }

    private sealed class Renderer_DynamicChoice : Renderer_Carousel<object> {
        public static Renderer_DynamicChoice instance = new();
    }
}
