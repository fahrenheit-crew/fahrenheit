// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Gui;

public partial class FhGui {
    public struct DrawPlateConfig {
        /// <summary>The texture to draw.</summary>
        public FhTexture texture;

        /// <summary>The color of the plate, in the format 0xAABBGGRR, i.e. RGBA8.</summary>
        public uint color = 0xFFE4F0F1;

        /// <summary>The width of the plate border.</summary>
        public float border_width = 5f;

        /// <summary>The color of the border highlight, in the format 0xAABBGGRR, i.e. RGBA8.</summary>
        public uint border_highlight = 0x28FFFFFF;

        /// <summary>The color of the border shadow, in the format 0xAABBGGRR, i.e. RGBA8.</summary>
        public uint border_shadow = 0x80000000;

        /// <summary>The offset of the plate's shadow from the plate.</summary>
        public Vector2 shadow_offset = new(0f, 0f);

        /// <summary>Whether to draw the plate in a header-like style.</summary>
        public bool draw_header = false;

        public DrawPlateConfig(FhTexture plate_texture) {
            texture = plate_texture;
        }
    }

    public struct DrawWaterConfig {
        /// <summary>The texture to draw.</summary>
        public FhTexture texture;

        /// <summary>The position to start moving from on the texture UV.</summary>
        public Vector2 move_offset = new(0f);

        /// <summary>The speed at which the texture moves.</summary>
        public Vector2 move_speed = new Vector2(1f) * 100;
        /// <summary>The amount of time, in seconds, in which the texture fades in and out.</summary>
        public float fade_cycle_length = 0.66f;

        /// <summary>The minimum alpha the texture can fade to.</summary>
        public uint min_alpha = 0x20;

        /// <summary>The maximum alpha the texture can fade to.</summary>
        public uint max_alpha = 0xA0;

        /// <summary>The color of the water, in the format 0xAABBGGRR, i.e. RGBA8.</summary>
        public uint color = 0xFFFFFFFF;

        public DrawWaterConfig(FhTexture water_texture) {
            texture = water_texture;
        }
    }

    /// <summary>Render a plate in the style of FFX-2 with a given position, size, color, and shadow.</summary>
    /// <param name="draw">The draw list to add the texture to.</param>
    /// <param name="bounds">The position and size of the texture.</param>
    /// <param name="tex_slice_idx">The number used to calculate the vertical UV coordinates.</param>
    /// <param name="config">The configuration to draw the plate with.</param>
    public void draw_x2_plate(
        ImDrawListPtr   draw,
        Rect            bounds,
        int             tex_slice_idx,
        DrawPlateConfig config
    ) {
        if (!config.texture.try_use(out ImTextureRef plate, out FhTextureMetadata? plate_meta)) return;

        tex_slice_idx %= (config.draw_header ? 16 : 8);

        float slice_size  = config.draw_header ? 32f: 64f;
        float plate_max_y = 512f - (tex_slice_idx * slice_size);

        UV plate_tuv = new Rect {
            pos  = new(  0f, plate_max_y - slice_size),
            size = new(512f,               slice_size),
        }.as_uv(plate_meta.size);

        UV plate_suv = bounds.as_uv();

        // Shadow behind plate
        draw.AddRectFilled(
            plate_suv.p0 + config.shadow_offset,
            plate_suv.p1 + config.shadow_offset,
            0x80000000
        );

        draw.AddImage(
            plate,
            plate_suv.p0,
            plate_suv.p1,
            plate_tuv.p0,
            plate_tuv.p1,
            config.color
        );

        draw_x2_plate_edges(
            draw,
            bounds,
            config.border_width,
            config.border_highlight,
            config.border_shadow
        );

        if (config.draw_header) {
            draw.AddRectFilled(
                plate_suv.p0,
                plate_suv.p1,
                0x63000000
            );
        }
    }

    /// <summary>Draws the border effect on an FFX-2 style plate.</summary>
    /// <param name="draw">The draw list to add the texture to.</param>
    /// <param name="bounds">The position to draw the plate borders at.</param>
    /// <param name="width">The width of the plate border.</param>
    /// <param name="highlight">The color of the top and left edges, in the format 0xAABBGGRR, i.e. RGBA8.</param>
    /// <param name="shadow">The color of the bottom and right edges, in the format 0xAABBGGRR, i.e. RGBA8.</param>
    public void draw_x2_plate_edges(
        ImDrawListPtr draw,
        Rect          bounds,
        float         width,
        uint          highlight,
        uint          shadow
    ) {
        bounds.pos  = Vector2.Round(bounds.pos);
        bounds.size = Vector2.Round(bounds.size);

        float shrink_size  = float .Round(-width * 2f);
        Rect  inner_bounds = bounds.expand(new(shrink_size), Alignment2D.CENTER);

        // We define vertices in a clockwise winding order for AddConvexPolyFilled to render them properly.
        Span<Vector2> border_top = [
            bounds      .top_left,
            bounds      .top_right,
            inner_bounds.top_right,
            inner_bounds.top_left with { X = bounds.top_left.X },
        ];

        Span<Vector2> border_left = [
            bounds      .top_left with { Y = inner_bounds.top_left.Y },
            inner_bounds.top_left,
            inner_bounds.bottom_left,
            bounds      .bottom_left,
        ];

        Span<Vector2> border_bottom = [
            bounds      .bottom_left,
            inner_bounds.bottom_left,
            inner_bounds.bottom_right,
            bounds      .bottom_right with { X = inner_bounds.bottom_right.X },
        ];

        Span<Vector2> border_right = [
            bounds      .top_right,
            bounds      .bottom_right,
            inner_bounds.bottom_right with { Y = bounds.bottom_right.Y },
            inner_bounds.top_right,
        ];

        draw.AddConvexPolyFilled(border_top   [0], 4, highlight);
        draw.AddConvexPolyFilled(border_bottom[0], 4, shadow);
        draw.AddConvexPolyFilled(border_left  [0], 4, highlight);
        draw.AddConvexPolyFilled(border_right [0], 4, shadow);
    }

    /// <summary>Render a cursor in the style of FFX-2.</summary>
    /// <param name="target_pos">The position the cursor should point at.</param>
    /// <param name="config">The configuration to draw the cursor with.</param>
    public void draw_x2_cursor(
        ImDrawListPtr    draw,
        Vector2          target_pos,
        DrawCursorConfig config
    ) {
        if (!config.texture.try_use(out ImTextureRef freetex, out FhTextureMetadata? freetex_meta)) return;

        UV cursor_tuv = new UV(
            new(  9f, 572f),
            new(180f, 478f)
        ).map_to(freetex_meta.size);

        float overlap_amount = config.overlap ? config.cursor_size.X * 0.1f : 0f;

        Vector2 cursor_top_left = new(
            target_pos.X - config.cursor_size.X + overlap_amount,
            target_pos.Y - config.cursor_size.Y / 2f + (0.04f * config.cursor_size.X) // Centers the cursor
        );

        float loop_time     = (float)(ImGui.GetTime() % 0.53f);
        float loop_progress = loop_time / 0.53f;

        // Draw trailing/fade out effect for cursor
        // Ghost Cursor 1
        if (loop_progress >= 0.12f) {
            float offset = -0.05f * config.cursor_size.X;
            float alpha  = 0.25f;

            // Sync movement with other cursors
            if (loop_progress >= 0.48f) {
                float fade_time = (loop_progress - 0.48f) / 0.52f;
                offset += fade_time * (config.max_offset * 0.55f);
                alpha *= 1f - fade_time;
            }

            UV ghost_suv = new Rect {
                pos  = cursor_top_left + new Vector2(offset, 0f),
                size = config.cursor_size,
            }.as_uv();

            uint color = ImGui.ColorConvertFloat4ToU32(new Vector4(1f, 1f, 1f, alpha));

            draw.AddImage(
                freetex,
                ghost_suv.p0,
                ghost_suv.p1,
                cursor_tuv.p0,
                cursor_tuv.p1,
                color
            );
        }

        // Ghost Cursor 2
        if (loop_progress >= 0.25f) {
            float offset = -0.04f * config.cursor_size.X;
            float alpha  = 0.85f;

            // Start moving
            if (loop_progress >= 0.38f && loop_progress < 0.48f) {
                float slide_progress = (loop_progress - 0.38f) / 0.10f;
                offset += slide_progress * (config.max_offset * 0.20f);
            }
            // Sync movement with other cursors
            else if (loop_progress >= 0.48f) {
                float fade_time = (loop_progress - 0.48f) / 0.52f;
                offset += (config.max_offset * 0.20f) + (fade_time * (config.max_offset * 0.55f));
                alpha *= 1f - fade_time;
            }

            UV ghost_suv = new Rect {
                pos  = cursor_top_left + new Vector2(offset, 0f),
                size = config.cursor_size,
            }.as_uv();

            uint color = ImGui.ColorConvertFloat4ToU32(new Vector4(1f, 1f, 1f, alpha));

            draw.AddImage(
                freetex,
                ghost_suv.p0,
                ghost_suv.p1,
                cursor_tuv.p0,
                cursor_tuv.p1,
                color
            );
        }

        // Main Cursor
        UV main_suv = new Rect {
            pos  = cursor_top_left + new Vector2(loop_progress * config.max_offset, 0f),
            size = config.cursor_size,
        }.as_uv();

        draw.AddImage(
            freetex,
            main_suv.p0,
            main_suv.p1,
            cursor_tuv.p0,
            cursor_tuv.p1
        );
    }

    /// <summary>Render an FFX-2 style moving water texture with a given position, size, move speed, and color.</summary>
    /// <param name="draw">The draw list to add the texture to.</param>
    /// <param name="bounds">The position to draw the texture at.</param>
    /// <param name="config">The configuration to draw the water with.</param>
    public void draw_x2_water(
        ImDrawListPtr   draw,
        Rect            bounds,
        DrawWaterConfig config
    ) {
        if (!config.texture.try_use(out ImTextureRef water, out FhTextureMetadata? water_meta)) return;

        // Fade the wave in and out
        float alpha;
        if (config.fade_speed <= 0f) {
            alpha = 1f;
        }
        else {
            float fade_progress = float.Sin((float)ImGui.GetTime() * float.Pi / config.fade_speed) / 2f + 0.5f;
            alpha = float.Lerp(config.min_alpha, config.max_alpha, fade_progress);
        }

        config.color = ((uint)alpha << 24) | (config.color & 0x00FFFFFF);

        float move_x = (float)ImGui.GetTime() * config.move_speed.X;
        float move_y = (float)ImGui.GetTime() * config.move_speed.Y;

        // We use % and for loops to seamlessly loop the texture continually and simulate wrapping
        Vector2 wave_offset = new Vector2(
            (config.move_offset.X + move_x) % water_meta.size.X,
            (config.move_offset.Y + move_y) % water_meta.size.Y
        );

        float x_offset = bounds.pos.X + wave_offset.X - water_meta.size.X;
        float y_offset = bounds.pos.Y + wave_offset.Y - water_meta.size.Y;

        // TODO-C#15: Use labeled continues to reduce indendation by putting the for loops next to each other.

        for (float x = x_offset; x < bounds.pos.X + bounds.size.X; x += water_meta.size.X) {
            float min_x = float.Max(bounds.pos.X, x);
            float max_x = float.Min(bounds.pos.X + bounds.size.X, x + water_meta.size.X);

            if (max_x <= min_x) continue;

            float uv_x1 = (min_x - x) / water_meta.size.X;
            float uv_x2 = (max_x - x) / water_meta.size.X;

            for (float y = y_offset; y < bounds.pos.Y + bounds.size.Y; y += water_meta.size.Y) {
                float min_y = float.Max(bounds.pos.Y, y);
                float max_y = float.Min(bounds.pos.Y + bounds.size.Y, y + water_meta.size.Y);

                if (max_y <= min_y) continue;

                float uv_y1 = (min_y - y) / water_meta.size.Y;
                float uv_y2 = (max_y - y) / water_meta.size.Y;

                Vector2 suv_p0 = new Vector2(min_x, min_y);
                Vector2 suv_p1 = new Vector2(max_x, max_y);
                Vector2 tuv_p0 = new Vector2(uv_x1, uv_y1);
                Vector2 tuv_p1 = new Vector2(uv_x2, uv_y2);

                draw.AddImage(
                    water,
                    suv_p0,
                    suv_p1,
                    tuv_p0,
                    tuv_p1,
                    config.color
                );
            }
        }
    }
}
