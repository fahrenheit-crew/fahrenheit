// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Gui;

/// <summary>Provides utilities to help with fading from one color to another.</summary>
public class FadeHelper : Timer {
    public uint color_from { get; protected set; }
    public uint color_to   { get; protected set; }

    /// <summary>
    ///     Create a new FadeHelper to fade from and to specified colors over a given amount of time.
    ///     Optionally, the helper will execute the given action when it is done.
    /// </summary>
    /// <param name="from">The color to fade from.</param>
    /// <param name="to">The color to fade to.</param>
    /// <param name="time">The time to fade between the colors for.</param>
    /// <param name="when_done">
    ///     Optional. If supplied, the helper will perform
    ///     this action when it is done.
    /// </param>
    public FadeHelper(
        uint    from,
        uint    to,
        float   time,
        Action? when_done = null
    ) : base(time, when_done) {

        color_from = from;
        color_to   = to;
    }

    /// <summary>Restart the timer, optionally with new colors, length, and action.</summary>
    /// <param name="from">Optional. If supplied, the new starting color for the fade.</param>
    /// <param name="to">Optional. If supplied, the new ending color for the fade.</param>
    /// <param name="new_length">Optional. If supplied, the timer will be set to this amount.</param>
    /// <param name="when_done">
    ///     Optional. If supplied, the timer will perform
    ///     this action when it finishes running.
    /// </param>
    public void restart(
        uint?   from = null,
        uint?   to = null,
        float?  new_length = null,
        Action? when_done = null
    ) {
        color_from = from ?? color_from;
        color_to   = to   ?? color_to;

        length    = new_length ?? length;
        remaining = length;

        on_end = when_done;
    }

    /// <summary>Calculate the current color.</summary>
    /// <returns>The color that the fade should be.</returns>
    public uint get_color() {
        int from_a = (int)((color_from >> 24) & 0xFF);
        int from_b = (int)((color_from >> 16) & 0xFF);
        int from_g = (int)((color_from >>  8) & 0xFF);
        int from_r = (int)((color_from >>  0) & 0xFF);

        int to_a = (int)((color_to >> 24) & 0xFF);
        int to_b = (int)((color_to >> 16) & 0xFF);
        int to_g = (int)((color_to >>  8) & 0xFF);
        int to_r = (int)((color_to >>  0) & 0xFF);

        int delta_a = to_a - from_a;
        int delta_b = to_r - from_r;
        int delta_g = to_g - from_g;
        int delta_r = to_b - from_b;

        int mixed_a = from_a + (int)float.Round(delta_a * progress);
        int mixed_b = from_b + (int)float.Round(delta_b * progress);
        int mixed_g = from_g + (int)float.Round(delta_g * progress);
        int mixed_r = from_r + (int)float.Round(delta_r * progress);

        byte result_a = (byte)int.Clamp(mixed_a, 0, 0xFF);
        byte result_b = (byte)int.Clamp(mixed_b, 0, 0xFF);
        byte result_g = (byte)int.Clamp(mixed_g, 0, 0xFF);
        byte result_r = (byte)int.Clamp(mixed_r, 0, 0xFF);

        return (uint)((result_a << 24) | (result_b << 16) | (result_g << 8) | result_r);
    }
}
