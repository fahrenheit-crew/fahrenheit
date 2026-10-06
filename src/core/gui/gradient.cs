// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Gui;

public enum GradientDirection {
    UP    = 0,
    RIGHT = 1,
    DOWN  = 2,
    LEFT  = 3,
}

/// <summary>A descriptor of a single step in a gradient.</summary>
public struct GradientStep {
    /// <summary>How far through the gradient this step occurs as a value between 0 and 1.</summary>
    public float progress;

    /// <summary>The color on the left side of the step, relative to the gradient's direction.</summary>
    public uint color_a;

    /// <summary>The color on the right side of the step, relative to the gradient's direction.</summary>
    public uint color_b;

    /// <summary>Create a new GradientStep.</summary>
    /// <param name="t">How far through the gradient this step occurs as a value between 0 and 1.</param>
    /// <param name="col_a">The color on the left side of the step, relative to the gradient's direction.</param>
    /// <param name="col_b">The color on the right side of the step, relative to the gradient's direction.</param>
    public GradientStep(float t, uint col_a, uint col_b) {
        progress = t;
        color_a  = col_a;
        color_b  = col_b;
    }

    /// <summary>Create a new GradientStep.</summary>
    /// <param name="t">How far through the gradient this step occurs as a value between 0 and 1.</param>
    /// <param name="color">The color of the step.</param>
    public GradientStep(float t, uint color) {
        progress = t;
        color_a  = color;
        color_b  = color;
    }
}
