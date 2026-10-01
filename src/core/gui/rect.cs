// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Gui;

/// <summary>An axis-aligned rectangle.</summary>
public struct Rect {
    /// <summary>The position of the rectangle.</summary>
    public Vector2 pos;

    /// <summary>The size of the rectangle.</summary>
    public Vector2 size;


    /// <summary>The top-left corner of the rectangle.</summary>
    public Vector2 top_left     => pos;

    /// <summary>The top-right corner of the rectangle.</summary>
    public Vector2 top_right    => pos with { X = pos.X + size.X };

    /// <summary>The bottom-left corner of the rectangle.</summary>
    public Vector2 bottom_left  => pos with { Y = pos.Y + size.Y };

    /// <summary>The bottom-right corner of the rectangle.</summary>
    public Vector2 bottom_right => pos + size;


    /// <summary>The center of the top edge of the rectangle.</summary>
    public Vector2 top    => pos with { X = pos.X + size.X / 2f };

    /// <summary>The center of the left edge of the rectangle.</summary>
    public Vector2 left   => pos with { Y = pos.Y + size.Y / 2f };

    /// <summary>The center of the bottom edge of the rectangle.</summary>
    public Vector2 bottom => new(pos.X + size.X / 2f, pos.Y + size.Y);

    /// <summary>The center of the right edge of the rectangle.</summary>
    public Vector2 right  => new(pos.X + size.X, pos.Y + size.Y / 2f);


    /// <summary>The center point of the rectangle.</summary>
    public Vector2 center => new(pos.X + size.X / 2f, pos.Y + size.Y / 2f);


    /// <summary>
    ///     Retrieve the top-left and bottom-right corners
    ///     of the rectangle as UV coordinates.
    /// </summary>
    /// <returns>The corners as UV coordinates.</returns>
    public UV as_uv() {
        return new(top_left, bottom_right);
    }

    /// <summary>
    ///     Retrieve the rectangle corners as UV coordinates of a texture of the given size.
    ///     When <paramref name="flipped"/>, the coordinates will automatically be flipped to match DDS textures.
    /// </summary>
    /// <param name="texture_size">The size of the texture to use.</param>
    /// <param name="flipped">Whether the UV coordinates should be flipped for use with DDS textures.</param>
    /// <returns>The calculated UV coordinates.</returns>
    public UV as_uv(Vector2 texture_size, bool flipped = true) {
        Rect scaled = scale_raw(Vector2.One / texture_size);

        return flipped
            ? new(scaled.bottom_left, scaled.top_right)
            : new(scaled.top_left,    scaled.bottom_right);
    }

    /// <summary>Determine whether a given point lies inside the rectangle.</summary>
    /// <param name="point">The point to test with.</param>
    /// <returns>Whether the point is within the rectangle.</returns>
    public bool contains(Vector2 point) {
        Vector2 pos2 = pos + size;

        return pos.X <= point.X && point.X < pos2.X
            && pos.Y <= point.Y && point.Y < pos2.Y;
    }

    private void adjust_pos_for_size(Vector2 size_increase, Alignment2D align) {
        pos.X = align.h switch {
            Alignment.BEGIN  => pos.X,
            Alignment.CENTER => pos.X - size_increase.X / 2f,
            Alignment.END    => pos.X - size_increase.X,

            _ => throw new NotImplementedException(),
        };

        pos.Y = align.v switch {
            Alignment.BEGIN  => pos.Y,
            Alignment.CENTER => pos.Y - size_increase.Y / 2f,
            Alignment.END    => pos.Y - size_increase.Y,

            _ => throw new NotImplementedException(),
        };
    }

    /// <summary>Expand the rectangle by a given amount out from the specified alignment.</summary>
    /// <param name="by">The size to expand the rectangle by.</param>
    /// <param name="align">The alignment to expand the rectangle out from.</param>
    /// <returns>The expanded rectangle.</returns>
    /// <remarks>When center-aligned, the size to expand by will be split evenly between both sides.</remarks>
    public Rect expand(Vector2 by, Alignment2D align) {
        Rect expanded = this with { size = size + by };
        expanded.adjust_pos_for_size(by, align);

        return expanded;
    }

    /// <summary>Scale the rectangle by a given scalar out from the specified alignment.</summary>
    /// <param name="by">The amount to scale the rectangle by.</param>
    /// <param name="align">The alignment to scale the rectangle out from.</param>
    /// <returns>The scaled rectangle.</returns>
    /// <seealso cref="scale_raw"/>
    /// <seealso cref="scale_to_aspect"/>
    public Rect scale(Vector2 by, Alignment2D align) {
        Vector2 old_size = size;

        Rect scaled = this with { size = size * by };
        scaled.adjust_pos_for_size(scaled.size - old_size, align);

        return scaled;
    }

    /// <summary>Multiply both the position and size of the rectangle by a given value.</summary>
    /// <param name="by">The value to multiply the position and size by.</param>
    /// <returns>The scaled rectangle.</returns>
    /// <seealso cref="scale"/>
    /// <seealso cref="scale_to_aspect"/>
    public Rect scale_raw(Vector2 by) {
        return new Rect {
            pos  = pos  * by,
            size = size * by,
        };
    }

    /// <summary>Scale the rectangle to a certain aspect ratio.</summary>
    /// <param name="aspect_helper">
    ///     A <see cref="Rect"/> where: <ul>
    ///         <li>the <c>pos</c> is equal to the offset required to center the screen for the desired aspect ratio,</li>
    ///         <li>the <c>size</c> is equal to the ratio between the desired and current display size.</li>
    ///     </ul>
    /// </param>
    /// <returns>The scaled rectangle.</returns>
    /// <seealso cref="scale"/>
    /// <seealso cref="scale_raw"/>
    public Rect scale_to_aspect(Rect aspect_helper) {
        return new Rect {
            pos  = pos  * aspect_helper.size + aspect_helper.pos,
            size = size * aspect_helper.size,
        };
    }
}
