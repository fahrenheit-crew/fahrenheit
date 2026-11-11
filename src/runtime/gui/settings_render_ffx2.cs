// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime.Gui;

public partial class FhSettingsUiX2 {

    private abstract class Renderer_Base<T> : FhSettingRenderer<T, FhSettingsUiX2>
        where T : FhSetting {

        internal protected override Vector2 get_size(FhSettingsUiX2 ui) {
            return new Vector2(0f, 65f);
        }

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

            name_bounds  = plate_bounds .scale_to_aspect(ui.aspect_helper);
            value_bounds = window_bounds.scale_to_aspect(ui.aspect_helper);

            return true;
        }

        protected void render_name(FhSettingsUiX2 ui, Rect name_bounds, string name) {
            ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

            FhApi.Gui.draw_text(
                draw,
                name_bounds.right * ui.aspect_scale + ui.aspect_helper.pos,
                name,
                ui.text_config with { align = Alignment2D.RIGHT }
            );
        }
    }

    private class Renderer_Category
        : FhSettingRenderer<FhSettingsCategory, FhSettingsUiX2> {

        internal protected override Vector2 get_size(FhSettingsUiX2 ui) {
            return new Vector2(0f, 45f);
        }

        internal protected override void render(FhSettingsUiX2 ui, FhSettingsCategory category, Rect max_bounds) {
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
                size = new(29f, 25f),
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

            FhApi.Gui.draw_text(
                draw,
                name_pos * ui.aspect_scale + ui.aspect_helper.pos,
                category.name,
                ui.text_config with { align = Alignment2D.LEFT }
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

    private class Renderer_Toggle : Renderer_Base<FhSettingToggle> {
        private static string _text_on => FhApi.Localization.localize($"{typeof(FhSettingsUiSelector)}.input.on");
        private static string _text_off => FhApi.Localization.localize($"{typeof(FhSettingsUiSelector)}.input.off");

        protected void render_options(FhSettingsUiX2 ui, FhSettingToggle toggle, Rect value_bounds) {
            if (!ui._texture_meswin.try_use(out ImTextureRef meswin, out FhTextureMetadata? meswin_meta)) {
                return;
            }

            ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

            // Options
            Vector2 on_pos  = value_bounds.left;
            Vector2 off_pos = value_bounds.left;

            on_pos.X  += value_bounds.size.X * 3f / 12f;
            off_pos.X += value_bounds.size.X * 8f / 12f;

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

        internal protected override void render(FhSettingsUiX2 ui, FhSettingToggle toggle, Rect max_bounds) {
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

    private class Renderer_Choice<T>
        : Renderer_Base<FhSettingChoice<T>>
        where T : struct, Enum {

        protected const float SPIN_SPEED = 2f; // in progress per second
        protected const float OPTION_SPIN_DIFF = 1f/3f;

        // Between -1 and 1. 0 when no spin is happening
        // Technically between -OPTION_SPIN_DIFF and OPTION_SPIN_DIFF
        // Equal to the spin progress of the current value
        protected Dictionary<FhSettingChoice<T>, float> spin_progress = [];

        protected int get_prev_index(ReadOnlySpan<T> choices, T choice) {
            int index = choices.IndexOf(choice);
            if (index == 0) return choices.Length - 1;
            return index - 1;
        }

        protected int get_next_index(ReadOnlySpan<T> choices, T choice) {
            int index = choices.IndexOf(choice);
            if (index == choices.Length - 1) return 0;
            return index + 1;
        }

        protected int get_prev_prev_index(ReadOnlySpan<T> choices, T choice) {
            int index = choices.IndexOf(choice);

            if (index == 0)
                index = choices.Length - 1;
            else
                index -= 1;

            if (index == 0)
                return choices.Length - 1;

            return index - 1;
        }

        protected int get_next_next_index(ReadOnlySpan<T> choices, T choice) {
            int index = choices.IndexOf(choice);

            if (index == choices.Length - 1)
                index = 0;
            else
                index += 1;

            if (index == choices.Length - 1)
                return 0;

            return index + 1;
        }

        protected float map_spin_progress(float progress) {
            // We remap progress to give us a nice arc
            return (float) (
                0.7372493987604 * float.Pow(progress, 4)
                + 0.2627506012396 * float.Pow(progress, 2)
            );
        }

        protected uint get_alpha_for_spin(float progress) {
            return (uint) (((byte) (progress * 0xFF) << 24) | 0xFFFFFF);
        }

        protected Vector2 get_offset_for_spin(Vector2 center, Vector2 side, float progress) {
            Vector2 max_offset = side - center;

            bool other_way = progress < 0f;

            progress = map_spin_progress(progress);

            Vector2 offset = Vector2.Lerp(center, side, progress);

            if (other_way) offset.X *= 1f;

            return offset;
        }

        protected void render_option(FhSettingsUiX2 ui, FhSettingChoice<T> choice, int index, float progress, Rect value_bounds) {
            Vector2 center = value_bounds.center;
            Vector2 right  = value_bounds.right;
            right.X       -= value_bounds.size.X / 6f;

            Vector2 pos   = center + get_offset_for_spin(center, right, progress);
            uint    color = get_alpha_for_spin(progress);
            float   scale = 1f - map_spin_progress(progress) / 3f;
            string  name  = FhApi.Localization.localize(choice.choice_names[index]);

            ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

            FhApi.Gui.draw_text(
                draw,
                pos * ui.aspect_scale + ui.aspect_helper.pos,
                name,
                ui.text_config with {
                    align = Alignment2D.CENTER,
                    font_size = ui.text_config.font_size * scale,
                    color = color,
                }
            );
        }

        protected void render_options(FhSettingsUiX2 ui, FhSettingChoice<T> choice, Rect value_bounds) {
            T curr_choice = choice.get();

            int curr_index = choice.choices.IndexOf(curr_choice);
            int prev_index = get_prev_index(choice.choices, curr_choice);
            int next_index = get_next_index(choice.choices, curr_choice);

            int[] choice_indices = spin_progress[choice] switch {
                0f => [
                    prev_index,
                    curr_index,
                    next_index,
                ],

                > 0f => [
                    prev_index,
                    curr_index,
                    next_index,
                    get_next_index(choice.choices, choice.choices[next_index]),
                ],

                < 0f => [
                    get_prev_index(choice.choices, choice.choices[prev_index]),
                    prev_index,
                    curr_index,
                    next_index,
                ],

                _ => throw new UnreachableException(),
            };

            int main_index = spin_progress[choice] switch {
                  0f => 1,
                > 0f => 1,
                < 0f => 2,

                _ => throw new UnreachableException(),
            };

            for (int i = 0; i < main_index; i++) {
                float spin = spin_progress[choice] - OPTION_SPIN_DIFF * (main_index - i);

                // This shouldn't be necessary but let's do it anyway
                spin = float.Clamp(spin, -1f, 1f);

                render_option(ui, choice, i, spin, value_bounds);
            }

            render_option(ui, choice, main_index, spin_progress[choice], value_bounds);

            for (int i = main_index + 1; i < choice_indices.Length; i++) {
                float spin = spin_progress[choice] + OPTION_SPIN_DIFF * (i - main_index);

                // This shouldn't be necessary but let's do it anyway
                spin = float.Clamp(spin, -1f, 1f);

                render_option(ui, choice, i, spin, value_bounds);
            }
        }

        protected void render_arrows(FhSettingsUiX2 ui, FhSettingChoice<T> choice, Rect value_bounds) {
            if (!ui._texture_b_menu1.try_use(out ImTextureRef b_menu1, out FhTextureMetadata? b_menu1_meta)) {
                return;
            }

            float arrow_progress = float.Clamp(spin_progress[choice] * 5f, 0f, 1f);

            Vector2 max_offset = new(10f, 0f);

            UV arrow_tuv = new Rect {
                pos  = new(965f, 913f),
                size = new( 54f,  30f),
            }.as_uv(b_menu1_meta.size);

            Vector2 arrow_size = new(30f, 42f);

            Rect arrow_left  = new() { pos = value_bounds.left  + max_offset };
            Rect arrow_right = new() { pos = value_bounds.right - max_offset };

            arrow_left .expand(arrow_size, new(Alignment.BEGIN, Alignment.CENTER));
            arrow_right.expand(arrow_size, new(Alignment.END, Alignment.CENTER));

            if (arrow_progress < 0f)
                arrow_left.pos += Vector2.Lerp(Vector2.Zero, max_offset, arrow_progress);
            else if (arrow_progress > 0f)
                arrow_right.pos += Vector2.Lerp(Vector2.Zero, max_offset, arrow_progress);

            UV arrow_left_suv  = arrow_left .scale_to_aspect(ui.aspect_helper).as_uv();
            UV arrow_right_suv = arrow_right.scale_to_aspect(ui.aspect_helper).as_uv();

            ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

            draw.AddImage(
                b_menu1,
                arrow_left_suv.p0,
                arrow_left_suv.p1,
                arrow_tuv.p1,
                arrow_tuv.p0
            );

            draw.AddImage(
                b_menu1,
                arrow_right_suv.p0,
                arrow_right_suv.p1,
                arrow_tuv.p0,
                arrow_tuv.p1
            );
        }

        internal protected override void render(FhSettingsUiX2 ui, FhSettingChoice<T> choice, Rect max_bounds) {
            if (!render_background(
                ui,
                max_bounds,
                out Rect? name_bounds,
                out Rect? value_bounds
            )) {
                return;
            }

            render_name(ui, (Rect) name_bounds, choice.name);

            if (spin_progress[choice] != 0f) {
                float spin_delta = SPIN_SPEED * ImGui.GetIO().DeltaTime;
                spin_delta = float.Min(spin_delta, spin_progress[choice]);

                spin_progress[choice] -= spin_delta * float.Sign(spin_progress[choice]);
                spin_progress[choice] = float.Clamp(spin_progress[choice], 0f, 1f);
            }

            render_options(ui, choice, (Rect) value_bounds);
            render_arrows(ui, choice, (Rect) value_bounds);
        }

        protected void spin_left(FhSettingChoice<T> choice) {
            T   curr_choice = choice.get();
            int prev_index  = get_prev_index(choice.choices, curr_choice);
            T   prev_choice = choice.choices[prev_index];

            choice.set(prev_choice);

            spin_progress[choice] = OPTION_SPIN_DIFF;
        }

        protected void spin_right(FhSettingChoice<T> choice) {
            T   curr_choice = choice.get();
            int next_index  = get_next_index(choice.choices, curr_choice);
            T   next_choice = choice.choices[next_index];

            choice.set(next_choice);

            spin_progress[choice] = -OPTION_SPIN_DIFF;
        }

        internal protected override void handle_input(FhSettingsUiX2 ui, FhSettingChoice<T> choice) {
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
}
