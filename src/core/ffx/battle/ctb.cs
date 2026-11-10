// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX.Battle;

[StructLayout(LayoutKind.Sequential, Size = 0x4)]
public struct CtbEntry {
    private byte  __0x0;
    private short __0x2;
}

//TODO: Find a better name for this
/// <summary>
///     CTB constants for a given agility value.
/// </summary>
public struct CtbBaseData {
    /// <summary>
    ///     How much delay a rank 1 command incurs.
    /// </summary>
    public byte tick_speed;

    /// <summary>
    ///     How much the initial CTB value assigned at the beginning of battle varies.<br/>
    ///     The initial CTB value is equal to <c>chr.ram.default_turn_delay - (rng % (initial_ctb_variance + 1))</c>.
    /// </summary>
    public byte initial_ctb_variance;
}
