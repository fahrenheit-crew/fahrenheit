// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Gui;

public partial class FhGui {
    public FhGuiX2 X2 = new();

    public class FhGuiX2 {

        /// <summary>Draws a "plate" with a given position, size, color, and shadow.</summary>
        /// <param name="draw">The draw list to add the texture to.</param>
        /// <param name="bounds">The position to draw the texture at.</param>
        /// <param name="plate">The texture to be drawn.</param>
        /// <param name="plate_tuv">The UVs to draw the texture with.</param>
        /// <param name="color">The color to draw the texture with in the format <c>0xAABBGGRR</c>, i.e. RGBA8.</param>
        /// <param name="border_width">The width of the plate's beveled border accents.</param>
        /// <param name="shadow_pos">The position of the shadow to draw behind the texture.</param>
        public void draw_plate(
            ImDrawListPtr draw,
            Rect          bounds,
            ImTextureRef  plate,
            UV            plate_tuv,
            uint          color,
            float         border_width,
            Vector2       shadow_pos
        ) {
            UV plate_suv = bounds.as_uv();

            // Shadow behind plate
            draw.AddRectFilled(
                plate_suv.p0 + shadow_pos,
                plate_suv.p1 + shadow_pos,
                0x80000000
            );

            draw.AddImage(
                plate,
                plate_suv.p0,
                plate_suv.p1,
                plate_tuv.p0,
                plate_tuv.p1,
                color
            );

            // Top Highlight
            Vector2[] top_highlight = {
                bounds.top_left,
                bounds.top_right,
                bounds.top_right + new Vector2(-border_width, border_width),
                bounds.top_left  + new Vector2(            0, border_width)
            };
            draw.AddConvexPolyFilled(
                top_highlight[0],
                4,
                0x28FFFFFF
            );

            // Left Highlight
            Vector2[] left_highlight = {
                bounds.top_left    + new Vector2(           0,  border_width),
                bounds.top_left    + new Vector2(border_width,  border_width),
                bounds.bottom_left + new Vector2(border_width, -border_width),
                bounds.bottom_left
            };
            draw.AddConvexPolyFilled(
                left_highlight[0],
                4,
                0x28FFFFFF
            );

            // Bottom Shadow
            Vector2[] bottom_shadow = {
                bounds.bottom_left  + new Vector2(border_width, -border_width),
                bounds.bottom_right + new Vector2(           0, -border_width),
                bounds.bottom_right,
                bounds.bottom_left
            };
            draw.AddConvexPolyFilled(
                bottom_shadow[0],
                4,
                0x80000000
            );

            // Right Shadow
            Vector2[] right_shadow = {
                bounds.top_right,
                bounds.top_right    + new Vector2(-border_width,  border_width),
                bounds.bottom_right + new Vector2(-border_width, -border_width),
                bounds.bottom_right + new Vector2(            0, -border_width)
            };
            draw.AddConvexPolyFilled(
                right_shadow[0],
                4,
                0x80000000
            );
        }

        public void draw_plate(
            ImDrawListPtr draw,
            Rect          bounds,
            ImTextureRef  plate,
            UV            plate_tuv,
            uint          color,
            float         top_width,
            float         left_width,
            float         bottom_width,
            float         right_width,
            Vector2       shadow_pos
        ) {
            UV plate_suv = bounds.as_uv();

            // Shadow behind plate
            draw.AddRectFilled(
                plate_suv.p0 + shadow_pos,
                plate_suv.p1 + shadow_pos,
                0x80000000
            );

            draw.AddImage(
                plate,
                plate_suv.p0,
                plate_suv.p1,
                plate_tuv.p0,
                plate_tuv.p1,
                color
            );

            // Top Highlight
            Vector2[] top_highlight = {
                bounds.top_left,
                bounds.top_right,
                bounds.top_right + new Vector2(-right_width, top_width),
                bounds.top_left  + new Vector2(           0, top_width)
            };
            draw.AddConvexPolyFilled(
                top_highlight[0],
                4,
                0x28FFFFFF
            );

            // Left Highlight
            Vector2[] left_highlight = {
                bounds.top_left    + new Vector2(         0,     top_width),
                bounds.top_left    + new Vector2(left_width,     top_width),
                bounds.bottom_left + new Vector2(left_width, -bottom_width),
                bounds.bottom_left
            };
            draw.AddConvexPolyFilled(
                left_highlight[0],
                4,
                0x28FFFFFF
            );

            // Bottom Shadow
            Vector2[] bottom_shadow = {
                bounds.bottom_left  + new Vector2(left_width, -bottom_width),
                bounds.bottom_right + new Vector2(         0, -bottom_width),
                bounds.bottom_right,
                bounds.bottom_left
            };
            draw.AddConvexPolyFilled(
                bottom_shadow[0],
                4,
                0x80000000
            );

            // Right Shadow
            Vector2[] right_shadow = {
                bounds.top_right,
                bounds.top_right    + new Vector2(-right_width,     top_width),
                bounds.bottom_right + new Vector2(-right_width, -bottom_width),
                bounds.bottom_right + new Vector2(           0, -bottom_width)
            };
            draw.AddConvexPolyFilled(
                right_shadow[0],
                4,
                0x80000000
            );
        }

        /// <summary>Draws a "message window" with a given position, size, and color.</summary>
        /// <param name="draw">The draw list to add the texture to.</param>
        /// <param name="bounds">The position to draw the texture at.</param>
        /// <param name="window">The texture to be drawn.</param>
        /// <param name="window_tuv">The UVs to draw the texture with.</param>
        /// <param name="corner_size">The size of the texture's corners.</param>
        /// <param name="window_color">The color to draw the texture with in the format <c>0xAABBGGRR</c>, i.e. RGBA8.</param>
        public void draw_message_window(
            ImDrawListPtr   draw,
            Rect            bounds,
            ImTextureRef    window,
            NineSliceHelper window_tuv,
            Vector2         corner_size,
            uint            window_color
        ) {
            NineSliceHelper screen_border_helper = NineSliceHelper.create(
                Vector2.One,
                bounds,
                corner_size
            );

            for (int slice_idx = 0; slice_idx < 9; slice_idx++) {
                Vector2[] screen_uv = screen_border_helper.get_uvs(slice_idx);

                int       alt_idx    = 3 * (2 - slice_idx / 3) + (slice_idx % 3);
                Vector2[] alt_tex_uv = window_tuv.get_uvs(alt_idx);

                Vector2 screen_p0 = screen_uv[0];
                Vector2 screen_p1 = screen_uv[3];

                (screen_p0.Y, screen_p1.Y) = (screen_p1.Y, screen_p0.Y);

                draw.AddImage(window, screen_p0, screen_p1, alt_tex_uv[0], alt_tex_uv[3], window_color);
            }
        }

        public void draw_message_window(
            ImDrawListPtr   draw,
            Rect            bounds,
            ImTextureRef    window,
            NineSliceHelper window_tuv,
            Vector2         corner_size,
            uint            window_color,
            ImTextureRef    wave,
            float           wave_speed,
            uint            wave_color
        ) {
            draw_water(
                draw,
                bounds,
                wave,
                wave_speed,
                wave_color
            );

            draw_message_window(
                draw,
                bounds,
                window,
                window_tuv,
                corner_size,
                window_color
            );
        }

        /// <summary>Draws a "water/wave" texture animation with a given position, size, move speed, and color.</summary>
        /// <param name="draw">The draw list to add the texture to.</param>
        /// <param name="bounds">The position to draw the texture at.</param>
        /// <param name="wave">The texture to be drawn.</param>
        /// <param name="wave_speed">The speed at which the texture moves/animates (in units per second).</param>
        /// <param name="wave_color">The color to draw the texture with in the format <c>0xAABBGGRR</c>, i.e. RGBA8.</param>
        public void draw_water(
            ImDrawListPtr draw,
            Rect          bounds,
            ImTextureRef  wave,
            float         wave_speed,
            uint          wave_color
        ) {
            // Fade the waves in and out over a cycle
            float alpha1 = MathF.Sin((float)ImGui.GetTime() * MathF.PI / 0.58f) / 2f + 0.5f;
            float alpha2 = MathF.Sin((float)ImGui.GetTime() * MathF.PI / 0.66f) / 2f + 0.5f;

            uint color1 = ((uint)(alpha1 * 0x80) << 24) | wave_color & 0x00FFFFFF;
            uint color2 = ((uint)(alpha2 * 0x80) << 24) | wave_color & 0x00FFFFFF;

            Vector2 tex_size = new(1024f);

            float wave_offset = ((float)ImGui.GetTime() * (wave_speed * 100)) % 1024f;

            // First wave, moves left -> right
            {
                float x_offset = bounds.pos.X + wave_offset - tex_size.X;

                for (float x = x_offset; x < bounds.pos.X + bounds.size.X; x += tex_size.X) {
                    float min_x = Math.Max(bounds.pos.X, x);
                    float max_x = Math.Min(bounds.pos.X + bounds.size.X, x + tex_size.X);

                    if (max_x > min_x) {
                        float uv_min_x = (min_x - x) / tex_size.X;
                        float uv_max_x = (max_x - x) / tex_size.X;

                        Vector2 wave_screen0 = new Vector2(min_x, bounds.pos.Y);
                        Vector2 wave_screen1 = new Vector2(max_x, bounds.pos.Y + bounds.size.Y);
                        Vector2 tex_uv0      = new Vector2(uv_min_x, 0f);
                        Vector2 tex_uv1      = new Vector2(uv_max_x, bounds.size.Y / tex_size.Y);

                        draw.AddImage(
                            wave,
                            wave_screen0,
                            wave_screen1,
                            tex_uv0,
                            tex_uv1,
                            color1
                        );
                    }
                }
            }

            // Second wave, moves diagonally from top-left to bottom-right
            {
                float x_offset = bounds.pos.X + wave_offset - tex_size.X;
                float y_offset = bounds.pos.Y + wave_offset - tex_size.Y;

                for (float x = x_offset; x < bounds.pos.X + bounds.size.X; x += tex_size.X) {
                    float min_x = Math.Max(bounds.pos.X, x);
                    float max_x = Math.Min(bounds.pos.X + bounds.size.X, x + tex_size.X);

                    if (max_x > min_x) {
                        float uv_min_x = (min_x - x) / tex_size.X;
                        float uv_max_x = (max_x - x) / tex_size.X;

                        for (float y = y_offset; y < bounds.pos.Y + bounds.size.Y; y += tex_size.Y) {
                            float min_y = Math.Max(bounds.pos.Y, y);
                            float max_y = Math.Min(bounds.pos.Y + bounds.size.Y, y + tex_size.Y);

                            if (max_y > min_y) {
                                float uv_min_y = (min_y - y) / tex_size.Y;
                                float uv_max_y = (max_y - y) / tex_size.Y;

                                Vector2 wave_screen0 = new Vector2(min_x, min_y);
                                Vector2 wave_screen1 = new Vector2(max_x, max_y);
                                Vector2 tex_uv0      = new Vector2(uv_min_x, uv_min_y);
                                Vector2 tex_uv1      = new Vector2(uv_max_x, uv_max_y);

                                draw.AddImage(
                                    wave,
                                    wave_screen0,
                                    wave_screen1,
                                    tex_uv0,
                                    tex_uv1,
                                    color2
                                );
                            }
                        }
                    }
                }
            }
        }
    }
}
