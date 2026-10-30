// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Gui;

public partial class FhGui {
    /// <summary>Render the cursor.</summary>
    /// <param name="target_pos">The position the cursor should point at.</param>
    /// <param name="config">The configuration to draw the cursor with.</param>
    public void draw_x_cursor(
        ImDrawListPtr    draw,
        Vector2          target_pos,
        DrawCursorConfig config
    ) {
        if (!config.texture.try_use(out ImTextureRef meswin, out FhTextureMetadata? meswin_meta)) {
            return;
        }

        UV cursor_tuv = new UV(
            new(157f, 244f),
            new( 90f, 200f)
        ).map_to(meswin_meta.size);

        float cycle_length = 0.38f;

        // Multiply by pi so cycle_length can be expressed in seconds.
        float cycle_t = float.Sin((float)ImGui.GetTime() * float.Pi / cycle_length) / 2f + 0.5f;
        float offset  = -(config.max_offset * cycle_t);

        float overlap_amount = config.overlap ? config.cursor_size.X * 0.1f : 0f;

        Vector2 cursor_center = new(
            target_pos.X - config.cursor_size.X / 2f + overlap_amount - offset,
            target_pos.Y
        );

        UV cursor_suv = new Rect { pos = cursor_center }
            .expand(config.cursor_size, Alignment2D.CENTER)
            .as_uv();

        draw.AddImage(
            meswin,
            cursor_suv.p0,
            cursor_suv.p1,
            cursor_tuv.p0,
            cursor_tuv.p1
        );
    }
}
