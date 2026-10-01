// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX;

[StructLayout(LayoutKind.Sequential)]
public struct AutoAbility {
    public ExcelSimplifiableTextOffset name;
    public ExcelSimplifiableTextOffset desc;

    public bool is_sos;

    public ElementFlags elem_strike;
    public ElementFlags elem_absorb;
    public ElementFlags elem_ignore;
    public ElementFlags elem_resist;
    public ElementFlags elem_weak;

    public StatusMap         status_inflict;
    public StatusDurationMap status_duration;
    public StatusMap         status_resist;

    public byte              stat_inc_amount;
    public StatIncreaseFlags stat_inc_flags;

    public StatusPermanentFlags status_auto_permanent;
    public StatusTemporalFlags  status_auto_temporal;
    public StatusExtraFlags     status_auto_extra;

    public StatusExtraFlags status_inflict_extra;
    public StatusExtraFlags status_resist_extra;

    public AutoAbilityEffectsMap auto_ability_effects;

    public byte icon;
    public byte group_idx;
    public byte group_level;
    public byte international_bonus_idx;
}

[Flags]
public enum StatIncreaseFlags : ushort {
    NONE                =       0,
    STRENGTH            = 1 <<  0,
    DEFENSE             = 1 <<  1,
    MAGIC               = 1 <<  2,
    MAGIC_DEFENSE       = 1 <<  3,
    AGILITY             = 1 <<  4,
    LUCK                = 1 <<  5,
    EVASION             = 1 <<  6,
    ACCURACY            = 1 <<  7,
    HP                  = 1 <<  8,
    MP                  = 1 <<  9,
    STRENGTH_BONUS      = 1 << 10,
    MAGIC_BONUS         = 1 << 11,
    DEFENSE_BONUS       = 1 << 12,
    MAGIC_DEFENSE_BONUS = 1 << 13,
}

public static partial class FhEnumExt {
    extension(StatIncreaseFlags flags) {
        public bool strength {
            get { return flags.HasFlag(StatIncreaseFlags.STRENGTH); }
            set { if (value) flags |= (StatIncreaseFlags.STRENGTH); else flags &= ~(StatIncreaseFlags.STRENGTH); }
        }

        public bool defense {
            get { return flags.HasFlag(StatIncreaseFlags.DEFENSE); }
            set { if (value) flags |= (StatIncreaseFlags.DEFENSE); else flags &= ~(StatIncreaseFlags.DEFENSE); }
        }

        public bool magic {
            get { return flags.HasFlag(StatIncreaseFlags.MAGIC); }
            set { if (value) flags |= (StatIncreaseFlags.MAGIC); else flags &= ~(StatIncreaseFlags.MAGIC); }
        }

        public bool magic_defense {
            get { return flags.HasFlag(StatIncreaseFlags.MAGIC_DEFENSE); }
            set { if (value) flags |= (StatIncreaseFlags.MAGIC_DEFENSE); else flags &= ~(StatIncreaseFlags.MAGIC_DEFENSE); }
        }

        public bool agility {
            get { return flags.HasFlag(StatIncreaseFlags.AGILITY); }
            set { if (value) flags |= (StatIncreaseFlags.AGILITY); else flags &= ~(StatIncreaseFlags.AGILITY); }
        }

        public bool luck {
            get { return flags.HasFlag(StatIncreaseFlags.LUCK); }
            set { if (value) flags |= (StatIncreaseFlags.LUCK); else flags &= ~(StatIncreaseFlags.LUCK); }
        }

        public bool evasion {
            get { return flags.HasFlag(StatIncreaseFlags.EVASION); }
            set { if (value) flags |= (StatIncreaseFlags.EVASION); else flags &= ~(StatIncreaseFlags.EVASION); }
        }

        public bool accuracy {
            get { return flags.HasFlag(StatIncreaseFlags.ACCURACY); }
            set { if (value) flags |= (StatIncreaseFlags.ACCURACY); else flags &= ~(StatIncreaseFlags.ACCURACY); }
        }


        public bool hp {
            get { return flags.HasFlag(StatIncreaseFlags.HP); }
            set { if (value) flags |= (StatIncreaseFlags.HP); else flags &= ~(StatIncreaseFlags.HP); }
        }

        public bool mp {
            get { return flags.HasFlag(StatIncreaseFlags.MP); }
            set { if (value) flags |= (StatIncreaseFlags.MP); else flags &= ~(StatIncreaseFlags.MP); }
        }


        public bool strength_bonus {
            get { return flags.HasFlag(StatIncreaseFlags.STRENGTH_BONUS); }
            set { if (value) flags |= (StatIncreaseFlags.STRENGTH_BONUS); else flags &= ~(StatIncreaseFlags.STRENGTH_BONUS); }
        }

        public bool defense_bonus {
            get { return flags.HasFlag(StatIncreaseFlags.DEFENSE_BONUS); }
            set { if (value) flags |= (StatIncreaseFlags.DEFENSE_BONUS); else flags &= ~(StatIncreaseFlags.DEFENSE_BONUS); }
        }

        public bool magic_bonus {
            get { return flags.HasFlag(StatIncreaseFlags.MAGIC_BONUS); }
            set { if (value) flags |= (StatIncreaseFlags.MAGIC_BONUS); else flags &= ~(StatIncreaseFlags.MAGIC_BONUS); }
        }

        public bool magic_defense_bonus {
            get { return flags.HasFlag(StatIncreaseFlags.MAGIC_DEFENSE_BONUS); }
            set { if (value) flags |= (StatIncreaseFlags.MAGIC_DEFENSE_BONUS); else flags &= ~(StatIncreaseFlags.MAGIC_DEFENSE_BONUS); }
        }
    }
}
