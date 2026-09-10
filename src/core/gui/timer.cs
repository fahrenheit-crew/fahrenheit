// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

//TODO: Triage whether this better belongs in a namespace like a potential `Fahrenheit.Util`
//      due to its possible usecases outside of GUI.
namespace Fahrenheit.Gui;

/// <summary>
///     Timers count down from a specified time to zero, and
///     optionally perform an action when they reach zero.
/// </summary>
/// <remarks>
///     The timer must be ticked manually by calling <see cref="tick"/>.
///     This allows for greater control over the timer.
/// </remarks>
public class Timer {
    protected float   length;
    protected float   remaining;
    protected Action? on_end;

    /// <summary>Whether the timer is done running.</summary>
    public bool is_done => remaining < 0.0001f;

    /// <summary>The percentage of the timer's total length that has elapsed, between 0 and 1.</summary>
    public float progress => length < 0.0001f ? 1f : (1f - remaining / length);

    /// <summary>
    ///     Create a new timer that will run for the specified time,
    ///     and optionally perform an action at the end.
    /// </summary>
    /// <param name="time">How long the timer should run for.</param>
    /// <param name="when_done">
    ///     Optional. If supplied, the timer will invoke
    ///     this action once it is done.
    /// </param>
    public Timer(float time, Action? when_done = null) {
        remaining = length = time;
        on_end    = when_done;

        if (length < 0.0001f && on_end is not null) {
            on_end();
        }
    }

    /// <summary>Reduce the remaining time in the timer down by the specified delta time.</summary>
    /// <param name="delta">The amount of time to reduce the remaining time by.</param>
    /// <returns>Whether the timer has finished running with this tick.</returns>
    public bool tick(float delta) {
        float old_remaining = remaining;
        remaining = float.Max(remaining - delta, 0f);

        if (old_remaining > remaining && is_done && on_end is not null) {
            on_end();
        }

        return is_done;
    }

    /// <summary>Restart the timer, optionally with a new length and action.</summary>
    /// <param name="new_length">Optional. If supplied, the timer will be set to this amount.</param>
    /// <param name="when_done">
    ///     Optional. If supplied, the timer will perform
    ///     this action when it finishes running.
    /// </param>
    public void restart(float? new_length = null, Action? when_done = null) {
        length    = new_length ?? length;
        remaining = length;

        on_end = when_done;

        if (length < 0.0001f && on_end is not null) {
            on_end();
        }
    }
}
