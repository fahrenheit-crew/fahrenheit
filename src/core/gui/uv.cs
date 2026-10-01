// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Gui;

/// <summary>Represents a pair of UV coordinates.</summary>
/// <param name="p0">The first set of UV coordinates.</param>
/// <param name="p1">The second set of UV coordinates.</param>
public readonly record struct UV(Vector2 p0, Vector2 p1);

public static partial class FhExt {
    extension(UV uv) {
        /// <summary>Move the UV coordinates by <paramref name="offset"/>.</summary>
        /// <param name="offset">The offset to move the coordinates by.</param>
        /// <returns>The moved UV coordinates.</returns>
        public UV move(Vector2 offset) {
            return new(uv.p0 + offset, uv.p1 + offset);
        }

        /// <summary>Scale the UV coordinates by the given scalar.</summary>
        /// <param name="scalar">The value to scale the UV coordinates by.</param>
        /// <returns>The scaled UV coordinates.</returns>
        public UV scale(Vector2 scalar) {
            return new(uv.p0 * scalar, uv.p1 * scalar);
        }

        /// <summary>Map the UV coordinates to a texture of the given size.</summary>
        /// <param name="texture_size">The size of the texture to map to.</param>
        /// <returns>The mapped UV coordinates.</returns>
        public UV map_to(Vector2 texture_size) {
            return uv.scale(texture_size.inverse());
        }
    }
}
