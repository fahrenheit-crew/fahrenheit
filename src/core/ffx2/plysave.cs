// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

// ffx_ps2/ffx2/master/jppc/battle/kernel/ply_save.h
// Switch release of FFX/X-2 HD

namespace Fahrenheit.FFX2;

[InlineArray(2)]
public partial struct Accessories {
    public ushort e0;
}

[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 0x80)]
public struct PlySave {
    [FieldOffset(0x00)] public ExcelTextOffset name_offset;

    [FieldOffset(0x04)] public uint bonus_hp;
    [FieldOffset(0x08)] public uint bonus_mp;
    [FieldOffset(0x0C)] public byte bonus_strength;
    [FieldOffset(0x0D)] public byte bonus_defense;
    [FieldOffset(0x0E)] public byte bonus_magic;
    [FieldOffset(0x0F)] public byte bonus_magic_defense;
    [FieldOffset(0x10)] public byte bonus_agility;
    [FieldOffset(0x11)] public byte bonus_luck;
    [FieldOffset(0x12)] public byte bonus_evasion;
    [FieldOffset(0x13)] public byte bonus_accuracy;

    [FieldOffset(0x14)] public uint total_exp;
    [FieldOffset(0x18)] public uint exp;

    [FieldOffset(0x1C)] public uint hp;
    [FieldOffset(0x20)] public uint mp;
    [FieldOffset(0x24)] public uint max_hp;
    [FieldOffset(0x28)] public uint max_mp;

    [FieldOffset(0x2C)] public byte ply_flags;

    [FieldOffset(0x2D)] public byte strength;
    [FieldOffset(0x2E)] public byte defense;
    [FieldOffset(0x2F)] public byte magic;
    [FieldOffset(0x30)] public byte magic_defense;
    [FieldOffset(0x31)] public byte agility;
    [FieldOffset(0x32)] public byte accuracy;
    [FieldOffset(0x33)] public byte evasion;
    [FieldOffset(0x34)] public byte luck;

    [FieldOffset(0x35)] public byte level;

    [FieldOffset(0x36)] public ushort      equipped_job;
    [FieldOffset(0x38)] public ushort      equipped_plate;
    [FieldOffset(0x3A)] public Accessories equipped_accessory;

    [FieldOffset(0x3E)] public ushort abi_map;

    [FieldOffset(0x40)] public uint escape_count;
    [FieldOffset(0x44)] public uint enemies_defeated;
    [FieldOffset(0x48)] public uint deaths;
    [FieldOffset(0x4C)] public uint status;
    
    [FieldOffset(0x50)] public AutoAbilityEffectsMap auto_ability_effects;
    
    [FieldOffset(0x56)] public ushort before_job; // Last equipped Dressphere?
    
    // 0x58 onwards seems to be entirely creature related, YRP have no data here besides size.
    [FieldOffset(0x78)] public ushort creature;
    [FieldOffset(0x7A)] public byte   size;

    public bool party_join   { readonly get { return ply_flags.get_bit(0); } set { ply_flags.set_bit(0, value); } }
    public bool party_out    { readonly get { return ply_flags.get_bit(1); } set { ply_flags.set_bit(1, value); } }
    public bool party_fixed  { readonly get { return ply_flags.get_bit(2); } set { ply_flags.set_bit(2, value); } }
    public bool party_joined { readonly get { return ply_flags.get_bit(4); } set { ply_flags.set_bit(4, value); } }
}
