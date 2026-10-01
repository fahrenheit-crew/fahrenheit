// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Gui;

/// <summary>
///     A helper for calculating UV coordinates for
///     <a href="https://en.wikipedia.org/wiki/9-slice_scaling">9-slice scaling</a>.
/// </summary>
public sealed class NineSliceHelper {
    private readonly Vector2[,] _points = new Vector2[4, 4];
    private bool _finalized;

    /// <summary>
    ///     Create a NineSliceHelper with given top-left and bottom-right corners and a specified corner size.
    ///     The helper is automatically finalized using the provided texture size.
    /// </summary>
    /// <remarks>
    ///     This method assumes all corners are of a uniform size.
    ///     If you wish to create a NineSliceHelper with varied corners, please use the individual slice methods.
    /// </remarks>
    /// <param name="texture_size">The texture size to finalize the helper with.</param>
    /// <param name="top_left">The coordinates of the top-left corner of the top-left slice.</param>
    /// <param name="bottom_right">The coordinates of the bottom-right corner of the bottom-right slice.</param>
    /// <param name="corner_size">The size of each corner.</param>
    /// <returns>A new NineSliceHelper with the specificed parameters.</returns>
    public static NineSliceHelper create(
        Vector2 texture_size,
        Vector2 top_left,
        Vector2 bottom_right,
        Vector2 corner_size
    ) {
        Vector2 top_right   = new(bottom_right.X, top_left.Y    );
        Vector2 bottom_left = new(top_left.X,     bottom_right.Y);

        return new NineSliceHelper()
            .slice_top_left    (top_left,     corner_size)
            .slice_top_right   (top_right,    corner_size)
            .slice_bottom_left (bottom_left,  corner_size)
            .slice_bottom_right(bottom_right, corner_size)
            .finalize          (texture_size);
    }

    /// <summary>
    ///     Create a NineSliceHelper with given a rectangle and a specified corner size.
    ///     The helper is automatically finalized using the provided texture size.
    /// </summary>
    /// <remarks>
    ///     This method assumes all corners are of a uniform size.
    ///     If you wish to create a NineSliceHelper with varied corners, please use the individual slice methods.
    /// </remarks>
    /// <param name="texture_size">The texture size to finalize the helper with.</param>
    /// <param name="rect">The bounding rectangle of the nine-slice texture.</param>
    /// <param name="corner_size">The size of each corner.</param>
    /// <returns>A new NineSliceHelper with the specificed parameters.</returns>
    public static NineSliceHelper create(
        Vector2 texture_size,
        Rect    rect,
        Vector2 corner_size
    ) {
        return create(texture_size, rect.pos, rect.pos + rect.size, corner_size);
    }

    /// <summary>Set up the coordinates for the top-left slice.</summary>
    /// <param name="corner">The coordinate of the top-left corner.</param>
    /// <param name="size">The size of the top-left corner.</param>
    /// <returns>This helper, to allow chaining method calls.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the NineSliceHelper was already finalized.</exception>
    public NineSliceHelper slice_top_left(
        Vector2 corner,
        Vector2 size
    ) {
        if (_finalized) {
            throw new InvalidOperationException("Cannot modify NineSliceHelper slices after it has been finalized.");
        }

        /*
         * We set these points up:
         * 1 2 - -
         * 3 4 - -
         * - - - -
         * - - - -
         */

        _points[0, 0] = corner;
        _points[1, 0] = corner with { X = corner.X + size.X };
        _points[0, 1] = corner with { Y = corner.Y + size.Y };
        _points[1, 1] = corner + size;

        return this;
    }

    /// <summary>Set up the coordinates for the top-right slice.</summary>
    /// <param name="corner">The coordinate of the top-right corner.</param>
    /// <param name="size">The size of the top-right corner.</param>
    /// <returns>This helper, to allow chaining method calls.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the NineSliceHelper was already finalized.</exception>
    public NineSliceHelper slice_top_right(
        Vector2 corner,
        Vector2 size
    ) {
        if (_finalized) {
            throw new InvalidOperationException("Cannot modify NineSliceHelper slices after it has been finalized.");
        }

        // Adjust size to move away from the corner
        size.X = -size.X;

        /*
         * We set these points up:
         * - - 2 1
         * - - 4 3
         * - - - -
         * - - - -
         */

        _points[3, 0] = corner;
        _points[2, 0] = corner with { X = corner.X + size.X };
        _points[3, 1] = corner with { Y = corner.Y + size.Y };
        _points[2, 1] = corner + size;

        return this;
    }

    /// <summary>Set up the coordinates for the bottom-left slice.</summary>
    /// <param name="corner">The coordinate of the bottom-left corner.</param>
    /// <param name="size">The size of the bottom-left corner.</param>
    /// <returns>This helper, to allow chaining method calls.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the NineSliceHelper was already finalized.</exception>
    public NineSliceHelper slice_bottom_left(
        Vector2 corner,
        Vector2 size
    ) {
        if (_finalized) {
            throw new InvalidOperationException("Cannot modify NineSliceHelper slices after it has been finalized.");
        }

        // Adjust size to move away from the corner
        size.Y = -size.Y;

        /*
         * We set these points up:
         * - - - -
         * - - - -
         * 3 4 - -
         * 1 2 - -
         */

        _points[0, 3] = corner;
        _points[1, 3] = corner with { X = corner.X + size.X };
        _points[0, 2] = corner with { Y = corner.Y + size.Y };
        _points[1, 2] = corner + size;

        return this;
    }

    /// <summary>Set up the coordinates for the bottom-right slice.</summary>
    /// <param name="corner">The coordinate of the bottom-right corner.</param>
    /// <param name="size">The size of the bottom-right corner.</param>
    /// <returns>This helper, to allow chaining method calls.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the NineSliceHelper was already finalized.</exception>
    public NineSliceHelper slice_bottom_right(
        Vector2 corner,
        Vector2 size
    ) {
        if (_finalized) {
            throw new InvalidOperationException("Cannot modify NineSliceHelper slices after it has been finalized.");
        }

        // Adjust size to move away from the corner
        size.X = -size.X;
        size.Y = -size.Y;

        /*
         * We set these points up:
         * - - - -
         * - - - -
         * - - 4 3
         * - - 2 1
         */

        _points[3, 3] = corner;
        _points[2, 3] = corner with { X = corner.X + size.X };
        _points[3, 2] = corner with { Y = corner.Y + size.Y };
        _points[2, 2] = corner + size;

        return this;
    }

    /// <summary>
    ///     Finalize the helper, converting the coordinates provided to it into UV coordinates
    ///     referencing a texture of the provided size.
    /// </summary>
    /// <param name="texture_size">The size of the texture to use for calculating the UV coordinates.</param>
    /// <returns>This helper.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the NineSliceHelper was already finalized.</exception>
    public NineSliceHelper finalize(
        Vector2 texture_size
    ) {
        if (_finalized) {
            throw new InvalidOperationException("Cannot finalize a NineSliceHelper twice.");
        }

        for (int x = 0; x < 4; x++) {
            for (int y = 0; y < 4; y++) {
                _points[x, y] /= texture_size;
            }
        }

        _finalized = true;
        return this;
    }

    /// <summary>Get the UV coordinates for a certain slice of the 9-slice.</summary>
    /// <param name="slice_idx">The index of the slice to get the UV coordinates for.</param>
    /// <returns>
    ///     An array of four points containing the top-left, top-right, bottom-left, and bottom-right
    ///     UVs of the slice, in order.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the NineSliceHelper was not yet finalized.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown when <paramref name="slice_idx"/> is lower than 0 or greater than 8.
    /// </exception>
    public Vector2[] get_uvs(int slice_idx) {
        if (!_finalized) {
            throw new InvalidOperationException("Cannot get 9-slice UVs before the NineSliceHelper is finalized.");
        }

        return slice_idx switch {
            0 => [ _points[0, 0], _points[1, 0], _points[0, 1], _points[1, 1] ],
            1 => [ _points[1, 0], _points[2, 0], _points[1, 1], _points[2, 1] ],
            2 => [ _points[2, 0], _points[3, 0], _points[2, 1], _points[3, 1] ],
            3 => [ _points[0, 1], _points[1, 1], _points[0, 2], _points[1, 2] ],
            4 => [ _points[1, 1], _points[2, 1], _points[1, 2], _points[2, 2] ],
            5 => [ _points[2, 1], _points[3, 1], _points[2, 2], _points[3, 2] ],
            6 => [ _points[0, 2], _points[1, 2], _points[0, 3], _points[1, 3] ],
            7 => [ _points[1, 2], _points[2, 2], _points[1, 3], _points[2, 3] ],
            8 => [ _points[2, 2], _points[3, 2], _points[2, 3], _points[3, 3] ],
            _ => throw new ArgumentOutOfRangeException(nameof(slice_idx), $"{nameof(slice_idx)} must be between 0 and 8."),
        };
    }
}
