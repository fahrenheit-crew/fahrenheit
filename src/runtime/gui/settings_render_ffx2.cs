// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime.Gui;

public partial class FhSettingsUiX2 {
    private abstract class Renderer_Base<T> : SettingRenderer
        where T : FhSetting {

        public override Vector2 get_size(FhSettingsUiBase ui) => get_size((FhSettingsUiX2) ui);

        public override sealed bool handle_input(FhSettingsUiBase ui, FhSetting setting) {
            return handle_input((FhSettingsUiX2) ui, (T) setting);
        }

        public override sealed void render(FhSettingsUiBase ui, FhSetting setting, Rect max_bounds) {
            render((FhSettingsUiX2) ui, (T) setting, max_bounds);
        }

        protected virtual Vector2 get_size(FhSettingsUiX2 ui) => new(0f, 60f);
        protected abstract bool handle_input(FhSettingsUiX2 ui, T setting);
        protected abstract void render(FhSettingsUiX2 ui, T setting, Rect max_bounds);

        protected bool render_background(
            FhSettingsUiX2 ui,

            Rect max_bounds,

            [NotNullWhen(true)] out Rect? name_bounds,
            [NotNullWhen(true)] out Rect? value_bounds
        ) {
            if (!ui._texture_plate    .try_use(out ImTextureRef plate,  out FhTextureMetadata? plate_meta)
                || !ui._texture_window.try_use(out ImTextureRef window, out FhTextureMetadata? window_meta)
            ) {
                name_bounds = null;
                value_bounds = null;
                return false;
            }

            ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

            // Background
            // Divide the bounds width into plate, gap, and window
            float plate_width = max_bounds.size.X / 2.5f;

            float gap = plate_width / 8.8f;

            float window_width = float.Max(0f, max_bounds.size.X - plate_width - gap);

            Rect plate_bounds = new() {
                pos = max_bounds.pos,
                size = max_bounds.size with {
                    X = plate_width
                }
            };

            Rect window_bounds = new() {
                pos = max_bounds.pos with {
                    X = plate_bounds.right.X + gap
                },
                size = max_bounds.size with {
                    X = window_width
                }
            };

            FhApi.Gui.draw_x2_plate(
                draw,
                plate_bounds.scale_to_aspect(ui.aspect_helper),
                ui.setting_idx,
                ui.plate_config
            );

            NineSliceHelper window_suv = NineSliceHelper.create(
                Vector2.One,
                window_bounds.scale_to_aspect(ui.aspect_helper),
                new(3f)
            );

            for (int slice_idx = 0; slice_idx < 9; slice_idx++) {
                Vector2[] screen_uv = window_suv.get_uvs(slice_idx);

                int       alt_idx    = 3 * (2 - slice_idx / 3) + (slice_idx % 3);
                Vector2[] alt_tex_uv = ui.window_helper.get_uvs(alt_idx);

                Vector2 screen_p0 = screen_uv[0];
                Vector2 screen_p1 = screen_uv[3];

                (screen_p0.Y, screen_p1.Y) = (screen_p1.Y, screen_p0.Y);

                draw.AddImage(window, screen_p0, screen_p1, alt_tex_uv[0], alt_tex_uv[3], 0xFF808080);
            }

            name_bounds  = plate_bounds;
            value_bounds = window_bounds;

            return true;
        }

        protected void render_name(FhSettingsUiX2 ui, Rect name_bounds, string name) {
            ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

            Vector2 text_pos = name_bounds.center;
            text_pos.Y -= 1f;

            FhApi.Gui.draw_text(
                draw,
                text_pos * ui.aspect_scale + ui.aspect_helper.pos,
                name,
                ui.text_config with { align = Alignment2D.CENTER }
            );
        }
    }

    private sealed class Renderer_Category : Renderer_Base<FhSettingsCategory> {
        public static Renderer_Category instance = new();

        protected override Vector2 get_size(FhSettingsUiX2 ui) {
            return new Vector2(0f, 40f);
        }

        protected override void render(FhSettingsUiX2 ui, FhSettingsCategory category, Rect max_bounds) {
            if (!ui._texture_b_menu1.try_use(out ImTextureRef b_menu1, out FhTextureMetadata? b_menu1_meta)) {
                return;
            }

            ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

            // Background
            FhApi.Gui.draw_x2_plate(
                draw,
                max_bounds.scale_to_aspect(ui.aspect_helper),
                2,
                ui.plate_config with {
                    color = 0xFFAEBEC3,
                    draw_header = true,
                }
            );

            // Arrow
            Rect arrow_tuv = new Rect {
                pos  = new(965f, 913f),
                size = new( 54f,  30f),
            }.scale_raw(Vector2.One / b_menu1_meta.size);

            // We do some shenanigans for the various corner accessors
            // to work like we want them to
            arrow_tuv.pos.Y  += arrow_tuv.size.Y;
            arrow_tuv.size.Y *= -1f;

            Rect arrow_bounds = new() {
                pos = max_bounds.left with {
                    X = max_bounds.left.X + 35f,
                },
                size = new(28f, 24f),
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

            uint arrow_color = hovering_arrow ? 0xFFA0F0FF : 0xFFFFFFFF;

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
            Vector2 name_pos = arrow_bounds.right;
            name_pos.X      += 30f;
            name_pos.Y      -=  1f;

            FhApi.Gui.draw_text(
                draw,
                name_pos * ui.aspect_scale + ui.aspect_helper.pos,
                category.name,
                ui.text_config with {
                    font_size = 40f * ui.font_scale,
                    align = Alignment2D.LEFT
                }
            );

            // Input handling
            if (ui.mouse_clicked(arrow_hitbox)) {
                category.collapsed ^= true;
            }
        }

        protected override bool handle_input(FhSettingsUiX2 ui, FhSettingsCategory category) {
            if (!FhApi.Gui.is_any_pressed(FhApi.Gui.keys_confirm))
                return false;

            category.collapsed ^= true;
            return true;
        }
    }

    private sealed class Renderer_Toggle : Renderer_Base<FhSettingToggle> {
        public static Renderer_Toggle instance = new();
        private static string _text_on => FhApi.Localization.localize($"{typeof(FhSettingsUi)}.input.on");
        private static string _text_off => FhApi.Localization.localize($"{typeof(FhSettingsUi)}.input.off");

        private void render_options(FhSettingsUiX2 ui, FhSettingToggle toggle, Rect value_bounds) {
            if (!ui._texture_meswin.try_use(out ImTextureRef meswin, out FhTextureMetadata? meswin_meta)) {
                return;
            }

            ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

            // Options
            Vector2 on_pos  = value_bounds.left;
            Vector2 off_pos = value_bounds.left;

            on_pos.X  += value_bounds.size.X * 3.5f / 12f;
            off_pos.X += value_bounds.size.X * 8.5f / 12f;

            FhApi.Gui.draw_text(
                draw,
                on_pos * ui.aspect_scale + ui.aspect_helper.pos,
                _text_on,
                ui.text_config with { align = Alignment2D.CENTER }
            );

            FhApi.Gui.draw_text(
                draw,
                off_pos * ui.aspect_scale + ui.aspect_helper.pos,
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
                    ? (underline_on, underline_off)
                    : (underline_off, underline_on);

            UV underline_tuv = new Rect {
                pos  = new(  0f, 582f),
                size = new(300f,   9f),
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

        protected override void render(FhSettingsUiX2 ui, FhSettingToggle toggle, Rect max_bounds) {
            if (!render_background(
                ui,
                max_bounds,
                out Rect? name_bounds,
                out Rect? value_bounds
            )) {
                return;
            }

            render_name(ui, (Rect) name_bounds, toggle.name);

            render_options(ui, toggle, (Rect) value_bounds);
        }

        protected override bool handle_input(FhSettingsUiX2 ui, FhSettingToggle toggle) {
            ImGuiKey[] toggle_keys = [
                .. FhApi.Gui.keys_confirm,
                .. FhApi.Gui.keys_left,
                .. FhApi.Gui.keys_right,
            ];

            if (FhApi.Gui.is_any_pressed(toggle_keys)) {
                toggle.set(!toggle.get());
                return true;
            }

            return false;
        }
    }

    private sealed class Renderer_Number : Renderer_Base<FhSettingNumber> {
        public static Renderer_Number instance = new();

        protected override void render(FhSettingsUiX2 ui, FhSettingNumber number, Rect max_bounds) {
            //TODO: Implement rendering
        }

        protected override bool handle_input(FhSettingsUiX2 ui, FhSettingNumber number) {
            //TODO: Implement input handling
            return false;
        }
    }

    private class Renderer_Carousel<T> : Renderer_Base<FhSettingCarousel<T>> {
        private const float SPIN_SPEED = 1.6f; // in progress per second
        private const float OPTION_SPIN_DIFF = 1f/2f;

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
            return sign * float.Abs(1f - float.Cos((t + 1f) * (float.Pi / 2f)) - 1f);
        }

        private uint get_alpha_for_spin(float progress) {
            byte alpha = (byte)(0xFF * float.Pow(1f - remap_spin_progress(progress), 0.5f));
            return (uint)((alpha << 24) | 0xFFFFFF);
        }

        private Vector2 get_offset_for_spin(Vector2 center, Vector2 side, float progress) {
            Vector2 max_offset = side - center;

            bool other_way = progress < 0f;

            progress = float.Abs(1f - float.Cos((progress + 1f) * (float.Pi / 2f)) - 1f);

            Vector2 offset = Vector2.Lerp(Vector2.Zero, max_offset, progress);

            if (other_way) offset.X *= -1f;

            return offset;
        }

        private void render_option(FhSettingsUiX2 ui, FhSettingCarousel<T> carousel, int index, float progress, Rect value_bounds) {
            if (progress < -1f || progress > 1f) {
                return;
            }

            Vector2 center = value_bounds.center;
            Vector2 right  = value_bounds.right;
            right.X -= value_bounds.size.X / 6f;

            ReadOnlySpan<T> choices = carousel.get_choice_values();

            float abs_progress = float.Abs(progress);

            Vector2 pos   = center + get_offset_for_spin(center, right, progress);
            uint    color = get_alpha_for_spin(abs_progress);
            float   scale = 1f - remap_spin_progress(abs_progress) / 3f;
            string  name  = FhApi.Localization.localize(carousel.get_choice_name(choices[index]));

            ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

            FhApi.Gui.draw_text(
                draw,
                pos * ui.aspect_scale + ui.aspect_helper.pos,
                name,
                ui.text_config with {
                    align     = Alignment2D.CENTER,
                    font_size = ui.text_config.font_size * scale,
                    color     = color,
                }
            );
        }

        private void render_options(FhSettingsUiX2 ui, FhSettingCarousel<T> carousel, Rect value_bounds) {
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
                    get_prev_index(choices, choices[prev_index]),
                    prev_index,
                    curr_index,
                    next_index,
                ],

                < 0f => [
                    prev_index,
                    curr_index,
                    next_index,
                    get_next_index(choices, choices[next_index]),
                ],

                _ => throw new UnreachableException(),
            };

            int main_index = spin_progress[carousel] switch {
                  0f => 1,
                > 0f => 2,
                < 0f => 1,

                _ => throw new UnreachableException(),
            };

            for (int i = 0; i < main_index; i++) {
                float spin = spin_progress[carousel] - OPTION_SPIN_DIFF * (main_index - i);

                render_option(ui, carousel, choice_indices[i], spin, value_bounds);
            }

            render_option(ui, carousel, choice_indices[main_index], spin_progress[carousel], value_bounds);

            for (int i = main_index + 1; i < choice_indices.Length; i++) {
                float spin = spin_progress[carousel] + OPTION_SPIN_DIFF * (i - main_index);

                render_option(ui, carousel, choice_indices[i], spin, value_bounds);
            }
        }

        protected void render_arrows(FhSettingsUiX2 ui, FhSettingCarousel<T> carousel, Rect value_bounds) {
            if (!ui._texture_b_menu1.try_use(out ImTextureRef b_menu1, out FhTextureMetadata? b_menu1_meta)) {
                return;
            }

            float arrow_progress = float.Clamp(float.Sign(spin_progress[carousel]) * float.Pow(spin_progress[carousel] * (1f / OPTION_SPIN_DIFF), 2f), -1f, 1f);

            Vector2 max_offset = new(10f, 0f);

            Rect arrow_tuv = new Rect {
                pos  = new(965f, 913f),
                size = new( 54f,  30f),
            }.scale_raw(Vector2.One / b_menu1_meta.size);

            Vector2 arrow_size = new(26f, 31f);

            Rect arrow_left  = new() { pos = value_bounds.left  + max_offset };
            Rect arrow_right = new() { pos = value_bounds.right - max_offset };

            arrow_left  = arrow_left .expand(arrow_size, new(Alignment.BEGIN, Alignment.CENTER));
            arrow_right = arrow_right.expand(arrow_size, new(Alignment.END,   Alignment.CENTER));

            if (arrow_progress < 0f)
                arrow_left.pos  -= Vector2.Lerp(Vector2.Zero, max_offset, float.Abs(arrow_progress));
            else if (arrow_progress > 0f)
                arrow_right.pos += Vector2.Lerp(Vector2.Zero, max_offset, arrow_progress);

            Rect arrow_left_suv  = arrow_left .scale_to_aspect(ui.aspect_helper);
            Rect arrow_right_suv = arrow_right.scale_to_aspect(ui.aspect_helper);

            ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

            draw.AddImageQuad(
                b_menu1,

                arrow_left_suv.top_right,
                arrow_left_suv.bottom_right,
                arrow_left_suv.bottom_left,
                arrow_left_suv.top_left,

                arrow_tuv.bottom_left,
                arrow_tuv.bottom_right,
                arrow_tuv.top_right,
                arrow_tuv.top_left
            );

            draw.AddImageQuad(
                b_menu1,

                arrow_right_suv.top_left,
                arrow_right_suv.bottom_left,
                arrow_right_suv.bottom_right,
                arrow_right_suv.top_right,

                arrow_tuv.bottom_left,
                arrow_tuv.bottom_right,
                arrow_tuv.top_right,
                arrow_tuv.top_left
            );
        }

        protected override void render(FhSettingsUiX2 ui, FhSettingCarousel<T> carousel, Rect max_bounds) {
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
                spin_delta = float.Min(spin_delta, float.Abs(spin_progress[carousel]));

                spin_progress[carousel] -= spin_delta * float.Sign(spin_progress[carousel]);
                spin_progress[carousel]  = float.Clamp(spin_progress[carousel], -1f, 1f);
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

        protected override bool handle_input(FhSettingsUiX2 ui, FhSettingCarousel<T> choice) {
            if (spin_progress[choice] != 0f)
                return false;

            if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_left)) {
                spin_right(choice);
                return true;
            }

            if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_right)) {
                spin_left(choice);
                return true;
            }

            return false;
        }
    }

    private sealed class Renderer_Choice : Renderer_Carousel<long> {
        public static Renderer_Choice instance = new();
    }
    private sealed class Renderer_DynamicChoice : Renderer_Carousel<object> {
        public static Renderer_DynamicChoice instance = new();
    }
}
