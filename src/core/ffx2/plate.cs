// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

// ffx_ps2/ffx2/master/jppc/battle/kernel/plate.h
// Switch release of FFX/X-2 HD

namespace Fahrenheit.FFX2;

[InlineArray(4)]
public struct PlateMessages {
    public uint e0;
}

[InlineArray(16)]
public struct PlateBenefits {
    public ushort e0;
}

[InlineArray(4)]
public partial struct Abilities {
    public ushort e0;
}

[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 0x80)]
public struct Plate {
    [FieldOffset(0x00)] public  ExcelTextOffset name_offset;
    [FieldOffset(0x04)] public  ExcelTextOffset help_offset;
    [FieldOffset(0x08)] public  PlateMessages   messages;
    [FieldOffset(0x18)] public  ushort          bonus;
    [FieldOffset(0x1A)] public  byte            icon;
    [FieldOffset(0x1B)] public  byte            bonus_hp;
    [FieldOffset(0x1C)] public  byte            bonus_mp;
    [FieldOffset(0x1D)] public  byte            bonus_strength;
    [FieldOffset(0x1E)] public  byte            bonus_defense;
    [FieldOffset(0x1F)] public  byte            bonus_magic;
    [FieldOffset(0x20)] public  byte            bonus_magic_defense;
    [FieldOffset(0x21)] public  byte            bonus_agility;
    [FieldOffset(0x22)] public  byte            bonus_accuracy;
    [FieldOffset(0x23)] public  byte            bonus_evasion;
    [FieldOffset(0x24)] public  byte            bonus_luck;
    // 0x25 - 0x27 are seemingly unused, called "reserve1/2/3" in plate.h
    [FieldOffset(0x28)] public  PlateBenefits   skill;
    [FieldOffset(0x48)] public  ExcelTextOffset creature_help_offset;
    [FieldOffset(0x4C)] public  Abilities       creature_abilities;
    // 0x54 - 0x73 unknown?
    [FieldOffset(0x74)] public  byte            creature_bonus_hp;
    [FieldOffset(0x75)] public  byte            creature_bonus_mp;
    [FieldOffset(0x76)] public  byte            creature_bonus_strength;
    [FieldOffset(0x77)] public  byte            creature_bonus_defense;
    [FieldOffset(0x78)] public  byte            creature_bonus_magic;
    [FieldOffset(0x79)] public  byte            creature_bonus_magic_defense;
    [FieldOffset(0x7A)] public  byte            creature_bonus_agility;
    [FieldOffset(0x7B)] public  byte            creature_bonus_accuracy;
    [FieldOffset(0x7C)] public  byte            creature_bonus_evasion;
    [FieldOffset(0x7D)] public  byte            creature_bonus_luck;
    // 0x7E & 0x7F are also seemingly unused
}
