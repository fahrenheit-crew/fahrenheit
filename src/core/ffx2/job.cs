// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX2;

/// <summary>
///     Variable data used for the HP growth formula.<br/>
///     The formula is: `base` + `linear_mult` * level - (level^2) / (`quadratic_div` / 10)
/// </summary>
public struct StatGrowthHp {
    public byte linear_mult;
    public byte quadratic_div;
    public byte base_amount;
}

/// <summary>
///     Variable data used for the MP growth formula.<br/>
///     The formula is: `base` + (`linear_mult` / 10) * level - (level^2) / `quadratic_div`
/// </summary>
public struct StatGrowthMp {
    public byte linear_mult;
    public byte quadratic_div;
    public byte base_amount;
}

/// <summary>
///     Variable data used for the generic stat growth formula.<br/>
///     This formula is used for strength, magic, defense, magic defense,
///     agility, accuracy, evasion, and luck.<br/>
///     The formula is: `base` + (`linear_mult` / 10) * level + level / `linear_div` - (level^2 / 16) / `quadratic_div_a` / `quadratic_div_b`
/// </summary>
public struct StatGrowthGeneric {
    public byte linear_mult;
    public byte linear_div;
    public byte base_amount;
    public byte quadratic_div_a;
    public byte quadratic_div_b;
}

[StructLayout(LayoutKind.Sequential, Size = 0x4)]
public struct JobAbility {
    public ushort requirement;
    public ushort ability;
}

[StructLayout(LayoutKind.Sequential, Size = 0xA)]
public struct StatChanges {
    public sbyte hp;
    public sbyte mp;
    public sbyte strength;
    public sbyte defense;
    public sbyte magic;
    public sbyte magic_defense;
    public sbyte agility;
    public sbyte accuracy;
    public sbyte evasion;
    public sbyte luck;
}

[StructLayout(LayoutKind.Sequential, Size = 0x4)]
public struct JobWeaponData {
    public ushort weapon_model;
    public ushort weapon_position;
}

[InlineArray(4)]
public struct JobWeapons {
    private JobWeaponData _data;
}

[StructLayout(LayoutKind.Explicit, Size = 0x38)]
public struct JobCreatureData {
    [FieldOffset(0x00)] public ExcelTextOffset help_text;
    [FieldOffset(0x04)] public ushort          ability_prerequisite;
    [FieldOffset(0x06)] public T_X2CommandId   ability;
    [FieldOffset(0x08)] public ushort          auto_ability_prerequisite;
    [FieldOffset(0x0A)] public ushort          auto_ability;

    [FieldOffset(0x1C)] public StatChanges stat_changes;
}

[StructLayout(LayoutKind.Explicit, Size = 0xE4)]
public struct Job {
    [FieldOffset(0x00)] public ExcelTextOffset name_offset;
    [FieldOffset(0x04)] public ExcelTextOffset help_offset;
    [FieldOffset(0x08)] public byte            user;
    [FieldOffset(0x0A)] public byte            dressphere_menu_ordering;
    [FieldOffset(0x0B)] public byte            icon;
    [FieldOffset(0x0C)] public T_X2CommandId   berserk_action;

    [FieldOffset(0x0E)] public StatGrowthHp growth_hp;
    [FieldOffset(0x11)] public StatGrowthMp growth_mp;

    [FieldOffset(0x14)] public StatGrowthGeneric growth_strength;
    [FieldOffset(0x19)] public StatGrowthGeneric growth_defense;
    [FieldOffset(0x1E)] public StatGrowthGeneric growth_magic;
    [FieldOffset(0x23)] public StatGrowthGeneric growth_magic_defense;
    [FieldOffset(0x28)] public StatGrowthGeneric growth_agility;
    [FieldOffset(0x2D)] public StatGrowthGeneric growth_evasion;
    [FieldOffset(0x32)] public StatGrowthGeneric growth_accuracy;
    [FieldOffset(0x37)] public StatGrowthGeneric growth_luck;

    [FieldOffset(0x3c)] public InlineArray16<JobAbility> dressphere_abilities;

    [FieldOffset(0x7c)] public JobWeapons yuna_weapon_data;
    [FieldOffset(0x8c)] public JobWeapons rikku_weapon_data;
    [FieldOffset(0x9c)] public JobWeapons paine_weapon_data;

    [FieldOffset(0xAC)] public JobCreatureData creature_data;
}
