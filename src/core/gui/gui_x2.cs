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
        /// <param name="plate">The texture to be drawn.</param>
        /// <param name="bounds">The position and size of the texture.</param>
        /// <param name="index">The number used to calculate the vertical UV coordinates.</param>
        /// <param name="color">The color to draw the texture with in the format <c>0xAABBGGRR</c>, i.e. RGBA8.</param>
        /// <param name="border_width">The width of the plate's beveled border accents.</param>
        /// <param name="shadow_pos">The position of the shadow to draw behind the texture.</param>
        /// <param name="header">Whether to draw a header plate above the rest.</param>
        public void draw_plate(
            ImDrawListPtr draw,
            ImTextureRef  plate,
            Rect          bounds,
            int           index,
            uint          color,
            float         border_width,
            Vector2       shadow_pos,
            bool          header = false
        ) {
            float slice_size  = 64f;
            float plate_max_y = 512f - ((index % 8) * slice_size);

            if (header) {
                slice_size  = 32f;
                plate_max_y = 512f - ((index % 16) * slice_size);
            }

            UV plate_tuv = new Rect {
                pos  = new(  0f, plate_max_y - slice_size),
                size = new(512f,               slice_size),
            }.as_uv(new(512f));

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

            draw_plate_edges(
                draw,
                bounds,
                border_width,
                0x28FFFFFF,
                0x80000000
            );

            if (header) {
                draw.AddRectFilled(
                    plate_suv.p0,
                    plate_suv.p1,
                    0x63000000
                );
            }
        }

        public void draw_plate_edges(
            ImDrawListPtr draw,
            Rect          bounds,
            float         border_width,
            uint          highlight,
            uint          shadow
        ) {
            bounds.pos  = Vector2.Round(bounds.pos);
            bounds.size = Vector2.Round(bounds.size);

            float shrink_size  = float .Round(-border_width * 2f);
            Rect  inner_bounds = bounds.expand(new(shrink_size), Alignment2D.CENTER);

            Span<Vector2> border_top = [
                bounds      .top_left,
                bounds      .top_right,
                inner_bounds.top_right,
                inner_bounds.top_left with { X = bounds.top_left.X },
            ];

            Span<Vector2> border_left = [
                bounds      .top_left    with { Y = inner_bounds.top_left.Y },
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
    }
}
