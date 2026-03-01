// SPDX-License-Identifier: MIT

namespace Fahrenheit.FFX;

[InlineArray(20)]
public struct PlySaveLimitModeCtrArray {
    private ushort _u;
}

[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 0x94)]
public struct PlySave {
    [FieldOffset(0x00)] public  uint                     name_offset;
    [FieldOffset(0x04)] public  uint                     base_hp;
    [FieldOffset(0x08)] public  uint                     base_mp;
    [FieldOffset(0x0C)] public  byte                     base_strength;
    [FieldOffset(0x0D)] public  byte                     base_defense;
    [FieldOffset(0x0E)] public  byte                     base_magic;
    [FieldOffset(0x0F)] public  byte                     base_magic_defense;
    [FieldOffset(0x10)] public  byte                     base_agility;
    [FieldOffset(0x11)] public  byte                     base_luck;
    [FieldOffset(0x12)] public  byte                     base_evasion;
    [FieldOffset(0x13)] public  byte                     base_accuracy;
    [FieldOffset(0x14)] public  uint                     total_ap;
    [FieldOffset(0x18)] public  uint                     ap;
    [FieldOffset(0x1C)] public  uint                     hp;
    [FieldOffset(0x20)] public  uint                     mp;
    [FieldOffset(0x24)] public  uint                     max_hp;
    [FieldOffset(0x28)] public  uint                     max_mp;
    [FieldOffset(0x2C)] public  byte                     ply_flags;
    [FieldOffset(0x2D)] public  byte                     wpn_inv_idx;
    [FieldOffset(0x2E)] public  byte                     arm_inv_idx;
    [FieldOffset(0x2F)] public  byte                     strength;
    [FieldOffset(0x30)] public  byte                     defense;
    [FieldOffset(0x31)] public  byte                     magic;
    [FieldOffset(0x32)] public  byte                     magic_defense;
    [FieldOffset(0x33)] public  byte                     agility;
    [FieldOffset(0x34)] public  byte                     luck;
    [FieldOffset(0x35)] public  byte                     evasion;
    [FieldOffset(0x36)] public  byte                     accuracy;
    [FieldOffset(0x37)] public  byte                     poison_dmg;
    [FieldOffset(0x38)] public  byte                     limit_mode_index;
    [FieldOffset(0x39)] public  byte                     limit_charge;
    [FieldOffset(0x3A)] public  byte                     limit_charge_max;
    [FieldOffset(0x3B)] public  byte                     slv_available;
    [FieldOffset(0x3C)] public  byte                     slv_spent;
    [FieldOffset(0x3D)] public  byte                     __0x3D; // Set to 0x17 on aeon death -> Aeon becomes greyed out in summon menu and unselectable
    [FieldOffset(0x3E)] public  AbilityMap               abi_map;
    [FieldOffset(0x4A)] public  AutoAbilityEffectsMap    auto_ability_effects;
    [FieldOffset(0x50)] public  uint                     battle_count;
    [FieldOffset(0x54)] public  uint                     enemies_defeated;
    [FieldOffset(0x58)] public  uint                     deaths;
    [FieldOffset(0x5C)] public  uint                     limits_charged;
    [FieldOffset(0x60)] public  PlySaveLimitModeCtrArray limit_mode_counters;
    [FieldOffset(0x88)] public  OverdriveModeFlags       obtained_limit_modes;
    [FieldOffset(0x8C)] private uint                     __0x8C;
    [FieldOffset(0x90)] private uint                     __0x90;

    public bool join   { readonly get { return ply_flags.get_bit(0); } set { ply_flags.set_bit(0, value); } }
    public bool joined { readonly get { return ply_flags.get_bit(4); } set { ply_flags.set_bit(4, value); } }

    public ushort limit_mode_ctr_warrior   { readonly get { return limit_mode_counters[ 0]; } set { limit_mode_counters[ 0] = value; } }
    public ushort limit_mode_ctr_comrade   { readonly get { return limit_mode_counters[ 1]; } set { limit_mode_counters[ 1] = value; } }
    public ushort limit_mode_ctr_stoic     { readonly get { return limit_mode_counters[ 2]; } set { limit_mode_counters[ 2] = value; } }
    public ushort limit_mode_ctr_healer    { readonly get { return limit_mode_counters[ 3]; } set { limit_mode_counters[ 3] = value; } }
    public ushort limit_mode_ctr_tactician { readonly get { return limit_mode_counters[ 4]; } set { limit_mode_counters[ 4] = value; } }
    public ushort limit_mode_ctr_victim    { readonly get { return limit_mode_counters[ 5]; } set { limit_mode_counters[ 5] = value; } }
    public ushort limit_mode_ctr_dancer    { readonly get { return limit_mode_counters[ 6]; } set { limit_mode_counters[ 6] = value; } }
    public ushort limit_mode_ctr_avenger   { readonly get { return limit_mode_counters[ 7]; } set { limit_mode_counters[ 7] = value; } }
    public ushort limit_mode_ctr_slayer    { readonly get { return limit_mode_counters[ 8]; } set { limit_mode_counters[ 8] = value; } }
    public ushort limit_mode_ctr_hero      { readonly get { return limit_mode_counters[ 9]; } set { limit_mode_counters[ 9] = value; } }
    public ushort limit_mode_ctr_rook      { readonly get { return limit_mode_counters[10]; } set { limit_mode_counters[10] = value; } }
    public ushort limit_mode_ctr_victor    { readonly get { return limit_mode_counters[11]; } set { limit_mode_counters[11] = value; } }
    public ushort limit_mode_ctr_coward    { readonly get { return limit_mode_counters[12]; } set { limit_mode_counters[12] = value; } }
    public ushort limit_mode_ctr_ally      { readonly get { return limit_mode_counters[13]; } set { limit_mode_counters[13] = value; } }
    public ushort limit_mode_ctr_sufferer  { readonly get { return limit_mode_counters[14]; } set { limit_mode_counters[14] = value; } }
    public ushort limit_mode_ctr_daredevil { readonly get { return limit_mode_counters[15]; } set { limit_mode_counters[15] = value; } }
    public ushort limit_mode_ctr_loner     { readonly get { return limit_mode_counters[16]; } set { limit_mode_counters[16] = value; } }
    public ushort limit_mode_ctr_unused1   { readonly get { return limit_mode_counters[17]; } set { limit_mode_counters[17] = value; } }
    public ushort limit_mode_ctr_unused2   { readonly get { return limit_mode_counters[18]; } set { limit_mode_counters[18] = value; } }
    public ushort limit_mode_ctr_aeons     { readonly get { return limit_mode_counters[19]; } set { limit_mode_counters[19] = value; } }
}

[Flags]
public enum OverdriveModeFlags : uint {
    NONE      =       0,
    WARRIOR   = 1 <<  0,
    COMRADE   = 1 <<  1,
    STOIC     = 1 <<  2,
    HEALER    = 1 <<  3,
    TACTICIAN = 1 <<  4,
    VICTIM    = 1 <<  5,
    DANCER    = 1 <<  6,
    AVENGER   = 1 <<  7,
    SLAYER    = 1 <<  8,
    HERO      = 1 <<  9,
    ROOK      = 1 << 10,
    VICTOR    = 1 << 11,
    COWARD    = 1 << 12,
    ALLY      = 1 << 13,
    SUFFERER  = 1 << 14,
    DAREDEVIL = 1 << 15,
    LONER     = 1 << 16,
    UNUSED1   = 1 << 17,
    UNUSED2   = 1 << 18,
    AEONS     = 1 << 19,
}

public static partial class FhEnumExt {
    public static bool warrior  (this OverdriveModeFlags flags) => flags.HasFlag(OverdriveModeFlags.WARRIOR);
    public static bool comrade  (this OverdriveModeFlags flags) => flags.HasFlag(OverdriveModeFlags.COMRADE);
    public static bool stoic    (this OverdriveModeFlags flags) => flags.HasFlag(OverdriveModeFlags.STOIC);
    public static bool healer   (this OverdriveModeFlags flags) => flags.HasFlag(OverdriveModeFlags.HEALER);
    public static bool tactician(this OverdriveModeFlags flags) => flags.HasFlag(OverdriveModeFlags.TACTICIAN);
    public static bool victim   (this OverdriveModeFlags flags) => flags.HasFlag(OverdriveModeFlags.VICTIM);
    public static bool dancer   (this OverdriveModeFlags flags) => flags.HasFlag(OverdriveModeFlags.DANCER);
    public static bool avenger  (this OverdriveModeFlags flags) => flags.HasFlag(OverdriveModeFlags.AVENGER);
    public static bool slayer   (this OverdriveModeFlags flags) => flags.HasFlag(OverdriveModeFlags.SLAYER);
    public static bool hero     (this OverdriveModeFlags flags) => flags.HasFlag(OverdriveModeFlags.HERO);
    public static bool rook     (this OverdriveModeFlags flags) => flags.HasFlag(OverdriveModeFlags.ROOK);
    public static bool victor   (this OverdriveModeFlags flags) => flags.HasFlag(OverdriveModeFlags.VICTOR);
    public static bool coward   (this OverdriveModeFlags flags) => flags.HasFlag(OverdriveModeFlags.COWARD);
    public static bool ally     (this OverdriveModeFlags flags) => flags.HasFlag(OverdriveModeFlags.ALLY);
    public static bool sufferer (this OverdriveModeFlags flags) => flags.HasFlag(OverdriveModeFlags.SUFFERER);
    public static bool daredevil(this OverdriveModeFlags flags) => flags.HasFlag(OverdriveModeFlags.DAREDEVIL);
    public static bool loner    (this OverdriveModeFlags flags) => flags.HasFlag(OverdriveModeFlags.LONER);
    public static bool unused1  (this OverdriveModeFlags flags) => flags.HasFlag(OverdriveModeFlags.UNUSED1);
    public static bool unused2  (this OverdriveModeFlags flags) => flags.HasFlag(OverdriveModeFlags.UNUSED2);
    public static bool aeons    (this OverdriveModeFlags flags) => flags.HasFlag(OverdriveModeFlags.AEONS);
}
