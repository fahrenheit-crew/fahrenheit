// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX;

[Flags]
public enum PlyGender : byte {
    NONE  = 0,
    MAN   = 1,
    WOMAN = 2,
    AEON  = 4,
}

/// <summary>
///     As the name <c>PlyRom</c> (Player Read-Only Memory) indicates,<br/>
///     this struct contains a few constants player characters use.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct PlyRom {
    /// <summary>
    ///     The help text displayed when switching to the character.
    /// </summary>
    public ExcelSimplifiableTextOffset switch_text;

    /// <summary>
    ///     The text displayed when scanning a character.
    /// </summary>
    public ExcelSimplifiableTextOffset scan_text;

    /// <summary>
    ///     The gender of the player: man, woman, or aeon.
    /// </summary>
    public PlyGender gender;

    /// <summary>
    ///     The cubic multiplier in the formula for the next level requirement.<br/>
    ///     The formula is ax<sup>3</sup>/100 + bx<sup>2</sup>/10 + c(x + 1),<br/>
    ///     where x is the total amount of gained sphere levels.
    /// </summary>
    public byte slv_req_mult_a;

    /// <summary>
    ///     The quadratic multiplier in the formula for the next level requirement.<br/>
    ///     The formula is ax<sup>3</sup>/100 + bx<sup>2</sup>/10 + c(x + 1),<br/>
    ///     where x is the total amount of gained sphere levels.
    /// </summary>
    public byte slv_req_mult_b;

    /// <summary>
    ///     The linear multiplier in the formula for the next level requirement.<br/>
    ///     The formula is ax<sup>3</sup>/100 + bx<sup>2</sup>/10 + c(x + 1),<br/>
    ///     where x is the total amount of gained sphere levels.
    /// </summary>
    public byte slv_req_mult_c;

    /// <summary>
    ///     The maximum amount of AP required to reach the next level.
    ///     In vanilla, applies from the 101st sphere level onwards.
    /// </summary>
    public int slv_req_max;

    /// <summary>
    ///     Factors in the calculation of aeon stat boosts based on Yuna's stats.
    /// </summary>
    public AeonStatBoostsScaling aeon_stat_scaling;

    /// <summary>
    ///     The number of turns that Doom takes to kill the character.
    /// </summary>
    public byte doom_duration;

    // chr.ram.__0x199 is set to this
    public byte __0x2B;
}
