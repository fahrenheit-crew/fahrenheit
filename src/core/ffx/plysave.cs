// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX;

[InlineArray(20)]
public struct PlySaveLimitModeCtrArray {
    private ushort _u;
}

[StructLayout(LayoutKind.Sequential, Pack = 2)]
public struct PlySave {
    public ExcelTextOffset name;

    public uint base_hp;
    public uint base_mp;
    public byte base_strength;
    public byte base_defense;
    public byte base_magic;
    public byte base_magic_defense;
    public byte base_agility;
    public byte base_luck;
    public byte base_evasion;
    public byte base_accuracy;

    public uint total_ap;
    public uint ap;

    public uint hp;
    public uint mp;
    public uint max_hp;
    public uint max_mp;

    public byte ply_flags;

    public byte wpn_inv_idx;
    public byte arm_inv_idx;

    public byte strength;
    public byte defense;
    public byte magic;
    public byte magic_defense;
    public byte agility;
    public byte luck;
    public byte evasion;
    public byte accuracy;

    public byte poison_dmg;

    public byte limit_mode_index;
    public byte limit_charge;
    public byte limit_charge_max;

    public byte slv_available;
    public byte slv_spent;

    public byte battles_until_recovery;

    public AbilityMap            abi_map;
    public AutoAbilityEffectsMap auto_ability_effects;

    public uint battle_count;
    public uint enemies_defeated;
    public uint deaths;
    public uint limits_charged;

    public PlySaveLimitModeCtrArray limit_mode_counters;
    public OverdriveModeFlags       obtained_limit_modes;

    private uint __0x8C;
    private uint __0x90;

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
    extension(OverdriveModeFlags flags) {
        public bool warrior {
            get { return flags.HasFlag(OverdriveModeFlags.WARRIOR); }
            set { if (value) flags |= (OverdriveModeFlags.WARRIOR); else flags &= ~(OverdriveModeFlags.WARRIOR); }
        }

        public bool comrade {
            get { return flags.HasFlag(OverdriveModeFlags.COMRADE); }
            set { if (value) flags |= (OverdriveModeFlags.COMRADE); else flags &= ~(OverdriveModeFlags.COMRADE); }
        }

        public bool stoic {
            get { return flags.HasFlag(OverdriveModeFlags.STOIC); }
            set { if (value) flags |= (OverdriveModeFlags.STOIC); else flags &= ~(OverdriveModeFlags.STOIC); }
        }

        public bool healer {
            get { return flags.HasFlag(OverdriveModeFlags.HEALER); }
            set { if (value) flags |= (OverdriveModeFlags.HEALER); else flags &= ~(OverdriveModeFlags.HEALER); }
        }

        public bool tactician {
            get { return flags.HasFlag(OverdriveModeFlags.TACTICIAN); }
            set { if (value) flags |= (OverdriveModeFlags.TACTICIAN); else flags &= ~(OverdriveModeFlags.TACTICIAN); }
        }

        public bool victim {
            get { return flags.HasFlag(OverdriveModeFlags.VICTIM); }
            set { if (value) flags |= (OverdriveModeFlags.VICTIM); else flags &= ~(OverdriveModeFlags.VICTIM); }
        }

        public bool dancer {
            get { return flags.HasFlag(OverdriveModeFlags.DANCER); }
            set { if (value) flags |= (OverdriveModeFlags.DANCER); else flags &= ~(OverdriveModeFlags.DANCER); }
        }

        public bool avenger {
            get { return flags.HasFlag(OverdriveModeFlags.AVENGER); }
            set { if (value) flags |= (OverdriveModeFlags.AVENGER); else flags &= ~(OverdriveModeFlags.AVENGER); }
        }

        public bool slayer {
            get { return flags.HasFlag(OverdriveModeFlags.SLAYER); }
            set { if (value) flags |= (OverdriveModeFlags.SLAYER); else flags &= ~(OverdriveModeFlags.SLAYER); }
        }

        public bool hero {
            get { return flags.HasFlag(OverdriveModeFlags.HERO); }
            set { if (value) flags |= (OverdriveModeFlags.HERO); else flags &= ~(OverdriveModeFlags.HERO); }
        }

        public bool rook {
            get { return flags.HasFlag(OverdriveModeFlags.ROOK); }
            set { if (value) flags |= (OverdriveModeFlags.ROOK); else flags &= ~(OverdriveModeFlags.ROOK); }
        }

        public bool victor {
            get { return flags.HasFlag(OverdriveModeFlags.VICTOR); }
            set { if (value) flags |= (OverdriveModeFlags.VICTOR); else flags &= ~(OverdriveModeFlags.VICTOR); }
        }

        public bool coward {
            get { return flags.HasFlag(OverdriveModeFlags.COWARD); }
            set { if (value) flags |= (OverdriveModeFlags.COWARD); else flags &= ~(OverdriveModeFlags.COWARD); }
        }

        public bool ally {
            get { return flags.HasFlag(OverdriveModeFlags.ALLY); }
            set { if (value) flags |= (OverdriveModeFlags.ALLY); else flags &= ~(OverdriveModeFlags.ALLY); }
        }

        public bool sufferer {
            get { return flags.HasFlag(OverdriveModeFlags.SUFFERER); }
            set { if (value) flags |= (OverdriveModeFlags.SUFFERER); else flags &= ~(OverdriveModeFlags.SUFFERER); }
        }

        public bool daredevil {
            get { return flags.HasFlag(OverdriveModeFlags.DAREDEVIL); }
            set { if (value) flags |= (OverdriveModeFlags.DAREDEVIL); else flags &= ~(OverdriveModeFlags.DAREDEVIL); }
        }

        public bool loner {
            get { return flags.HasFlag(OverdriveModeFlags.LONER); }
            set { if (value) flags |= (OverdriveModeFlags.LONER); else flags &= ~(OverdriveModeFlags.LONER); }
        }

        public bool unused1 {
            get { return flags.HasFlag(OverdriveModeFlags.UNUSED1); }
            set { if (value) flags |= (OverdriveModeFlags.UNUSED1); else flags &= ~(OverdriveModeFlags.UNUSED1); }
        }

        public bool unused2 {
            get { return flags.HasFlag(OverdriveModeFlags.UNUSED2); }
            set { if (value) flags |= (OverdriveModeFlags.UNUSED2); else flags &= ~(OverdriveModeFlags.UNUSED2); }
        }

        public bool aeons {
            get { return flags.HasFlag(OverdriveModeFlags.AEONS); }
            set { if (value) flags |= (OverdriveModeFlags.AEONS); else flags &= ~(OverdriveModeFlags.AEONS); }
        }
    }
}
