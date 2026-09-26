// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

// ffx_ps2/ffx2/master/jppc/battle/kernel/a_ability.h
// Switch release of FFX/X-2 HD

namespace Fahrenheit.FFX2;

/// <remarks> 
///     Determines what "type" the Auto Ability is.
/// </remarks>
[Flags]
public enum AAbilityFlags : uint {
    NONE  =       0,
    SOS   = 1 <<  0,
    STAT  = 1 <<  2,
    TURBO = 1 <<  3, // Only toggled on Turbo Swordplay and Turbo Instinct?
}

[StructLayout(LayoutKind.Sequential)]
public struct AutoAbility {
    public ExcelTextOffset name;
    public ExcelTextOffset help;

    private InlineArray4<short> reserve1;

    public T_X2CommandId command_menu;        // Which menus actions' cast times to reduce
    public sbyte         cast_time_reduction; // Percentage

    private byte reserve2;

    public AAbilityFlags special_data;

    public ElementFlags elem_strike;
    public ElementFlags elem_absorb;
    public ElementFlags elem_ignore;
    public ElementFlags elem_resist;
    public ElementFlags elem_weak;

    public StatChanges stat_changes;

    public StatusMap  status_inflict1;
    public StatusMap2 status_inflict2;

    public StatusMap  status_resist1;
    public StatusMap2 status_resist2;

    public StatusFlags  status_auto1;
    public StatusFlags2 status_auto2;

    public StatusDurationMap2 status_time;

    public AutoAbilityEffectsMap auto_ability_effects;

    public byte icon;

    private byte   reserve3;
    private ushort reserve4;

    public ushort ap;
}
