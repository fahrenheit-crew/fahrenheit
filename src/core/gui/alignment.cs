// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Gui;

/// <summary>Represents the alignment of some element on one axis.</summary>
public enum Alignment {
    /// <summary>The element should be aligned to the beginning of the axis.</summary>
    BEGIN  = 0,

    /// <summary>The element should be aligned to the center of the axis.</summary>
    CENTER = 1,

    /// <summary>The element should be aligned to the end of the axis.</summary>
    END    = 2,
}

/// <summary>Represents the alignment of some element in 2D.</summary>
/// <param name="h">The horizontal alignment of the element.</param>
/// <param name="v">The vertical alignment of the element.</param>
public readonly record struct Alignment2D (
    Alignment h,
    Alignment v
) {
    public static readonly Alignment2D TOP_LEFT     = new(Alignment.BEGIN, Alignment.BEGIN);
    public static readonly Alignment2D TOP_RIGHT    = new(Alignment.END,   Alignment.BEGIN);
    public static readonly Alignment2D BOTTOM_LEFT  = new(Alignment.BEGIN, Alignment.END);
    public static readonly Alignment2D BOTTOM_RIGHT = new(Alignment.END,   Alignment.END);

    public static readonly Alignment2D TOP    = new(Alignment.CENTER, Alignment.BEGIN);
    public static readonly Alignment2D LEFT   = new(Alignment.BEGIN,  Alignment.CENTER);
    public static readonly Alignment2D BOTTOM = new(Alignment.CENTER, Alignment.END);
    public static readonly Alignment2D RIGHT  = new(Alignment.END,    Alignment.CENTER);

    public static readonly Alignment2D CENTER = new(Alignment.CENTER, Alignment.CENTER);
}
