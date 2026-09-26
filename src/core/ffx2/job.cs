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

/// <remarks>  
///     The meaning of the fields is contextual. `ability` can be a command or auto-ability.  
///     `requirement` can be the index of a Garment Grid gate, a minimum AP requirement,  
///     or a level requirement for creatures.  
/// </remarks>  
[StructLayout(LayoutKind.Sequential, Size = 0x4)]
public struct Ability {
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
    [FieldOffset(0x00)] public ExcelTextOffset       help;
    [FieldOffset(0x04)] public InlineArray2<Ability> abilities;

    [FieldOffset(0x1C)] public StatChanges stat_changes;
    
    [FieldOffset(0x28)] public FeedStatChanges level_growth; // Which stat increases on Level Up
}

[StructLayout(LayoutKind.Sequential)]
public struct Job {
    public ExcelTextOffset name;
    public ExcelTextOffset help;
    public byte            user;
    public byte            data;
    public byte            index;
    public byte            icon;
    public T_X2CommandId   berserk_action;

    public StatGrowthHp growth_hp;
    public StatGrowthMp growth_mp;

    public StatGrowthGeneric growth_strength;
    public StatGrowthGeneric growth_defense;
    public StatGrowthGeneric growth_magic;
    public StatGrowthGeneric growth_magic_defense;
    public StatGrowthGeneric growth_agility;
    public StatGrowthGeneric growth_evasion;
    public StatGrowthGeneric growth_accuracy;
    public StatGrowthGeneric growth_luck;

    public InlineArray16<Ability> abilities;

    public InlineArray3<JobWeapons> weapon_data;

    public JobCreatureData creature_data;
}
