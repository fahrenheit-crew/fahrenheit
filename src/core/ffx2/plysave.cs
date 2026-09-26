// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

// ffx_ps2/ffx2/master/jppc/battle/kernel/ply_save.h
// Switch release of FFX/X-2 HD

namespace Fahrenheit.FFX2;

public enum CreatureSize : byte {
    NONE   = 0,
    SMALL  = 1,
    MEDIUM = 2,
    LARGE  = 3,
}

[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 0x28)]
public struct PlySaveCreatureData {
    [FieldOffset(0x20)] public ushort       creature; // Monster/Model ID
    [FieldOffset(0x22)] public CreatureSize size;
}

[StructLayout(LayoutKind.Sequential)]
public struct PlySave {
    public ExcelTextOffset name;

    public uint bonus_hp;
    public uint bonus_mp;
    public byte bonus_strength;
    public byte bonus_defense;
    public byte bonus_magic;
    public byte bonus_magic_defense;
    public byte bonus_agility;
    public byte bonus_luck;
    public byte bonus_evasion;
    public byte bonus_accuracy;

    public uint total_exp;
    public uint exp;

    public uint hp;
    public uint mp;
    public uint max_hp;
    public uint max_mp;

    public byte ply_flags;

    public byte strength;
    public byte defense;
    public byte magic;
    public byte magic_defense;
    public byte agility;
    public byte accuracy;
    public byte evasion;
    public byte luck;

    public byte level;

    public T_X2JobId   equipped_job;
    public T_X2PlateId equipped_plate;

    public InlineArray2<T_X2AccessoryId> equipped_accessories;

    public AbilityMap abi_map;

    public uint escape_count;
    public uint enemies_defeated;
    public uint deaths;
    public uint status;
    
    public AutoAbilityEffectsMap auto_ability_effects;
    
    public T_X2JobId before_job; // Last equipped Dressphere?

    public PlySaveCreatureData creature_data;

    public bool party_join   { readonly get { return ply_flags.get_bit(0); } set { ply_flags.set_bit(0, value); } }
    public bool party_out    { readonly get { return ply_flags.get_bit(1); } set { ply_flags.set_bit(1, value); } }
    public bool party_fixed  { readonly get { return ply_flags.get_bit(2); } set { ply_flags.set_bit(2, value); } }
    public bool party_joined { readonly get { return ply_flags.get_bit(4); } set { ply_flags.set_bit(4, value); } }
}
