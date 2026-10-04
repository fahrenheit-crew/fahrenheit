// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

/// <summary>
///     Allows querying of input state.
/// </summary>
public unsafe class FhInput {

    public class InputAction(ushort mask) {
        public bool is_pressed => (*raw & mask) != 0;
    }

    private static readonly nint _ptr_input_raw = FhUtil.select(0xF270C0, 0xD93A90, 0xD93A90);
    private                 uint _input_lock    = 0;

    public readonly InputAction l2       = new(0x1);
    public readonly InputAction r2       = new(0x2);
    public readonly InputAction l1       = new(0x4);
    public readonly InputAction r1       = new(0x8);
    public readonly InputAction square   = new(0x10);
    public readonly InputAction confirm  = new(0x20);
    public readonly InputAction cancel   = new(0x40);
    public readonly InputAction triangle = new(0x80);
    public readonly InputAction select   = new(0x100);
    public readonly InputAction start    = new(0x800);
    public readonly InputAction up       = new(0x1000);
    public readonly InputAction right    = new(0x2000);
    public readonly InputAction down     = new(0x4000);
    public readonly InputAction left     = new(0x8000);

    public static ushort* raw => FhUtil.ptr_at<ushort>(_ptr_input_raw);

    public void consume_all() {
        *raw = 0;
    }

    /* [fkelava 04/10/26 14:42]
     * The input lock is used while displaying full-screen ImGui interfaces,
     * to block the game from responding to any input.
     */

    /// <summary>
    ///     Returns whether the input lock is asserted.
    ///     While it is, inputs are not forwarded to the game.
    /// </summary>
    internal bool lock_get() => _input_lock != 0;

    /// <summary>
    ///     Asserts the input lock, blocking the game from receiving input.
    /// </summary>
    /// <remarks>The call must be paired with a call to <see cref="lock_off"/>.</remarks>
    internal void lock_on() => Interlocked.Increment(ref _input_lock);

    /// <summary>
    ///     Releases the input lock, unblocking the game from receiving input.
    /// </summary>
    /// <remarks>The call must be paired with a call to <see cref="lock_on"/>.</remarks>
    internal void lock_off() => uint.Max(0, Interlocked.Decrement(ref _input_lock));

}
