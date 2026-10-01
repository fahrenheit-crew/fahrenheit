// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX;

[StructLayout(LayoutKind.Sequential)]
public struct StatusMap {
    public byte death;
    public byte zombie;
    public byte petrification;
    public byte poison;
    public byte power_break;
    public byte magic_break;
    public byte armor_break;
    public byte mental_break;
    public byte confusion;
    public byte berserk;
    public byte provoke;
    public byte threaten;
    public byte sleep;
    public byte silence;
    public byte darkness;
    public byte shell;
    public byte protect;
    public byte reflect;
    public byte nul_tide;
    public byte nul_blaze;
    public byte nul_shock;
    public byte nul_frost;
    public byte regen;
    public byte haste;
    public byte slow;
}

[StructLayout(LayoutKind.Sequential)]
public struct StatusDurationMap {
    public byte sleep;
    public byte silence;
    public byte darkness;
    public byte shell;
    public byte protect;
    public byte reflect;
    public byte nul_tide;
    public byte nul_blaze;
    public byte nul_shock;
    public byte nul_frost;
    public byte regen;
    public byte haste;
    public byte slow;
}

[Flags]
public enum StatusPermanentFlags : ushort {
    NONE          =       0,
    DEATH         = 1 <<  0,
    ZOMBIE        = 1 <<  1,
    PETRIFICATION = 1 <<  2,
    POISON        = 1 <<  3,
    POWER_BREAK   = 1 <<  4,
    MAGIC_BREAK   = 1 <<  5,
    ARMOR_BREAK   = 1 <<  6,
    MENTAL_BREAK  = 1 <<  7,
    CONFUSION     = 1 <<  8,
    BERSERK       = 1 <<  9,
    PROVOKE       = 1 << 10,
    THREATEN      = 1 << 11,
}

public static partial class FhEnumExt {
    extension(StatusPermanentFlags flags) {
        public bool death {
            get { return flags.HasFlag(StatusPermanentFlags.DEATH); }
            set { if (value) flags |= (StatusPermanentFlags.DEATH); else flags &= ~(StatusPermanentFlags.DEATH); }
        }

        public bool zombie {
            get { return flags.HasFlag(StatusPermanentFlags.ZOMBIE); }
            set { if (value) flags |= (StatusPermanentFlags.ZOMBIE); else flags &= ~(StatusPermanentFlags.ZOMBIE); }
        }

        public bool petrification {
            get { return flags.HasFlag(StatusPermanentFlags.PETRIFICATION); }
            set { if (value) flags |= (StatusPermanentFlags.PETRIFICATION); else flags &= ~(StatusPermanentFlags.PETRIFICATION); }
        }

        public bool poison {
            get { return flags.HasFlag(StatusPermanentFlags.POISON); }
            set { if (value) flags |= (StatusPermanentFlags.POISON); else flags &= ~(StatusPermanentFlags.POISON); }
        }

        public bool confusion {
            get { return flags.HasFlag(StatusPermanentFlags.CONFUSION); }
            set { if (value) flags |= (StatusPermanentFlags.CONFUSION); else flags &= ~(StatusPermanentFlags.CONFUSION); }
        }

        public bool berserk {
            get { return flags.HasFlag(StatusPermanentFlags.BERSERK); }
            set { if (value) flags |= (StatusPermanentFlags.BERSERK); else flags &= ~(StatusPermanentFlags.BERSERK); }
        }

        public bool provoke {
            get { return flags.HasFlag(StatusPermanentFlags.PROVOKE); }
            set { if (value) flags |= (StatusPermanentFlags.PROVOKE); else flags &= ~(StatusPermanentFlags.PROVOKE); }
        }

        public bool threaten {
            get { return flags.HasFlag(StatusPermanentFlags.THREATEN); }
            set { if (value) flags |= (StatusPermanentFlags.THREATEN); else flags &= ~(StatusPermanentFlags.THREATEN); }
        }


        public bool power_break {
            get { return flags.HasFlag(StatusPermanentFlags.POWER_BREAK); }
            set { if (value) flags |= (StatusPermanentFlags.POWER_BREAK); else flags &= ~(StatusPermanentFlags.POWER_BREAK); }
        }

        public bool magic_break {
            get { return flags.HasFlag(StatusPermanentFlags.MAGIC_BREAK); }
            set { if (value) flags |= (StatusPermanentFlags.MAGIC_BREAK); else flags &= ~(StatusPermanentFlags.MAGIC_BREAK); }
        }

        public bool armor_break {
            get { return flags.HasFlag(StatusPermanentFlags.ARMOR_BREAK); }
            set { if (value) flags |= (StatusPermanentFlags.ARMOR_BREAK); else flags &= ~(StatusPermanentFlags.ARMOR_BREAK); }
        }

        public bool mental_break {
            get { return flags.HasFlag(StatusPermanentFlags.MENTAL_BREAK); }
            set { if (value) flags |= (StatusPermanentFlags.MENTAL_BREAK); else flags &= ~(StatusPermanentFlags.MENTAL_BREAK); }
        }
    }
}

[Flags]
public enum StatusTemporalFlags : ushort {
    NONE        =       0,
    SLEEP       = 1 <<  0,
    SILENCE     = 1 <<  1,
    DARKNESS    = 1 <<  2,
    SHELL       = 1 <<  3,
    PROTECT     = 1 <<  4,
    REFLECT     = 1 <<  5,
    NUL_WATER   = 1 <<  6,
    NUL_FIRE    = 1 <<  7,
    NUL_THUNDER = 1 <<  8,
    NUL_ICE     = 1 <<  9,
    REGEN       = 1 << 10,
    HASTE       = 1 << 11,
    SLOW        = 1 << 12,
}

public static partial class FhEnumExt {
    extension(StatusTemporalFlags flags) {
        public bool sleep {
            get { return flags.HasFlag(StatusTemporalFlags.SLEEP); }
            set { if (value) flags |= (StatusTemporalFlags.SLEEP); else flags &= ~(StatusTemporalFlags.SLEEP); }
        }

        public bool silence {
            get { return flags.HasFlag(StatusTemporalFlags.SILENCE); }
            set { if (value) flags |= (StatusTemporalFlags.SILENCE); else flags &= ~(StatusTemporalFlags.SILENCE); }
        }

        public bool darkness {
            get { return flags.HasFlag(StatusTemporalFlags.DARKNESS); }
            set { if (value) flags |= (StatusTemporalFlags.DARKNESS); else flags &= ~(StatusTemporalFlags.DARKNESS); }
        }


        public bool shell {
            get { return flags.HasFlag(StatusTemporalFlags.SHELL); }
            set { if (value) flags |= (StatusTemporalFlags.SHELL); else flags &= ~(StatusTemporalFlags.SHELL); }
        }

        public bool protect {
            get { return flags.HasFlag(StatusTemporalFlags.PROTECT); }
            set { if (value) flags |= (StatusTemporalFlags.PROTECT); else flags &= ~(StatusTemporalFlags.PROTECT); }
        }

        public bool reflect {
            get { return flags.HasFlag(StatusTemporalFlags.REFLECT); }
            set { if (value) flags |= (StatusTemporalFlags.REFLECT); else flags &= ~(StatusTemporalFlags.REFLECT); }
        }

        public bool regen {
            get { return flags.HasFlag(StatusTemporalFlags.REGEN); }
            set { if (value) flags |= (StatusTemporalFlags.REGEN); else flags &= ~(StatusTemporalFlags.REGEN); }
        }

        public bool haste {
            get { return flags.HasFlag(StatusTemporalFlags.HASTE); }
            set { if (value) flags |= (StatusTemporalFlags.HASTE); else flags &= ~(StatusTemporalFlags.HASTE); }
        }

        public bool slow {
            get { return flags.HasFlag(StatusTemporalFlags.SLOW); }
            set { if (value) flags |= (StatusTemporalFlags.SLOW); else flags &= ~(StatusTemporalFlags.SLOW); }
        }


        public bool nul_water {
            get { return flags.HasFlag(StatusTemporalFlags.NUL_WATER); }
            set { if (value) flags |= (StatusTemporalFlags.NUL_WATER); else flags &= ~(StatusTemporalFlags.NUL_WATER); }
        }

        public bool nul_fire {
            get { return flags.HasFlag(StatusTemporalFlags.NUL_FIRE); }
            set { if (value) flags |= (StatusTemporalFlags.NUL_FIRE); else flags &= ~(StatusTemporalFlags.NUL_FIRE); }
        }

        public bool nul_thunder {
            get { return flags.HasFlag(StatusTemporalFlags.NUL_THUNDER); }
            set { if (value) flags |= (StatusTemporalFlags.NUL_THUNDER); else flags &= ~(StatusTemporalFlags.NUL_THUNDER); }
        }

        public bool nul_ice {
            get { return flags.HasFlag(StatusTemporalFlags.NUL_ICE); }
            set { if (value) flags |= (StatusTemporalFlags.NUL_ICE); else flags &= ~(StatusTemporalFlags.NUL_ICE); }
        }
    }
}

[Flags]
public enum StatusExtraFlags : ushort {
    NONE            =       0,
    SCAN            = 1 <<  0,
    DISTILL_POWER   = 1 <<  1,
    DISTILL_MANA    = 1 <<  2,
    DISTILL_SPEED   = 1 <<  3,
    DISTILL_MOVE    = 1 <<  4,
    DISTILL_ABILITY = 1 <<  5,
    SHIELD          = 1 <<  6,
    BOOST           = 1 <<  7,
    EJECT           = 1 <<  8,
    AUTO_LIFE       = 1 <<  9,
    CURSE           = 1 << 10,
    DEFEND          = 1 << 11,
    GUARD           = 1 << 12,
    SENTINEL        = 1 << 13,
    DOOM            = 1 << 14,
}

public static partial class FhEnumExt {
    extension(StatusExtraFlags flags) {
        public bool distill_power {
            get { return flags.HasFlag(StatusExtraFlags.DISTILL_POWER); }
            set { if (value) flags |= (StatusExtraFlags.DISTILL_POWER); else flags &= ~(StatusExtraFlags.DISTILL_POWER); }
        }

        public bool distill_mana {
            get { return flags.HasFlag(StatusExtraFlags.DISTILL_MANA); }
            set { if (value) flags |= (StatusExtraFlags.DISTILL_MANA); else flags &= ~(StatusExtraFlags.DISTILL_MANA); }
        }

        public bool distill_speed {
            get { return flags.HasFlag(StatusExtraFlags.DISTILL_SPEED); }
            set { if (value) flags |= (StatusExtraFlags.DISTILL_SPEED); else flags &= ~(StatusExtraFlags.DISTILL_SPEED); }
        }

        public bool distill_move {
            get { return flags.HasFlag(StatusExtraFlags.DISTILL_MOVE); }
            set { if (value) flags |= (StatusExtraFlags.DISTILL_MOVE); else flags &= ~(StatusExtraFlags.DISTILL_MOVE); }
        }

        public bool distill_ability {
            get { return flags.HasFlag(StatusExtraFlags.DISTILL_ABILITY); }
            set { if (value) flags |= (StatusExtraFlags.DISTILL_ABILITY); else flags &= ~(StatusExtraFlags.DISTILL_ABILITY); }
        }


        public bool shield {
            get { return flags.HasFlag(StatusExtraFlags.SHIELD); }
            set { if (value) flags |= (StatusExtraFlags.SHIELD); else flags &= ~(StatusExtraFlags.SHIELD); }
        }

        public bool boost {
            get { return flags.HasFlag(StatusExtraFlags.BOOST); }
            set { if (value) flags |= (StatusExtraFlags.BOOST); else flags &= ~(StatusExtraFlags.BOOST); }
        }

        public bool scan {
            get { return flags.HasFlag(StatusExtraFlags.SCAN); }
            set { if (value) flags |= (StatusExtraFlags.SCAN); else flags &= ~(StatusExtraFlags.SCAN); }
        }

        public bool eject {
            get { return flags.HasFlag(StatusExtraFlags.EJECT); }
            set { if (value) flags |= (StatusExtraFlags.EJECT); else flags &= ~(StatusExtraFlags.EJECT); }
        }

        public bool auto_life {
            get { return flags.HasFlag(StatusExtraFlags.AUTO_LIFE); }
            set { if (value) flags |= (StatusExtraFlags.AUTO_LIFE); else flags &= ~(StatusExtraFlags.AUTO_LIFE); }
        }

        public bool curse {
            get { return flags.HasFlag(StatusExtraFlags.CURSE); }
            set { if (value) flags |= (StatusExtraFlags.CURSE); else flags &= ~(StatusExtraFlags.CURSE); }
        }

        public bool defend {
            get { return flags.HasFlag(StatusExtraFlags.DEFEND); }
            set { if (value) flags |= (StatusExtraFlags.DEFEND); else flags &= ~(StatusExtraFlags.DEFEND); }
        }

        public bool guard {
            get { return flags.HasFlag(StatusExtraFlags.GUARD); }
            set { if (value) flags |= (StatusExtraFlags.GUARD); else flags &= ~(StatusExtraFlags.GUARD); }
        }

        public bool sentinel {
            get { return flags.HasFlag(StatusExtraFlags.SENTINEL); }
            set { if (value) flags |= (StatusExtraFlags.SENTINEL); else flags &= ~(StatusExtraFlags.SENTINEL); }
        }

        public bool doom {
            get { return flags.HasFlag(StatusExtraFlags.DOOM); }
            set { if (value) flags |= (StatusExtraFlags.DOOM); else flags &= ~(StatusExtraFlags.DOOM); }
        }
    }
}

[Flags]
public enum ChrResistFlags : ushort {
    NONE                       =       0,
    ARMORED                    = 1 <<  0,
    IMMUNITY_FRACTIONAL_DAMAGE = 1 <<  1,
    IMMUNITY_LIFE              = 1 <<  2,
    IMMUNITY_SENSOR            = 1 <<  3,
    IMMUNITY_SCAN              = 1 <<  4,
    IMMUNITY_PHYSICAL_DAMAGE   = 1 <<  5,
    IMMUNITY_MAGICAL_DAMAGE    = 1 <<  6,
    IMMUNITY_HP_DAMAGE         = 1 <<  7,
    IMMUNITY_CTB_DAMAGE        = 1 <<  8,
    IMMUNITY_ZANMATO           = 1 <<  9,
    IMMUNITY_BRIBE             = 1 << 10,
}

public static partial class FhEnumExt {
    extension(ChrResistFlags flags) {
        public bool is_armored {
            get { return flags.HasFlag(ChrResistFlags.ARMORED); }
            set { if (value) flags |= (ChrResistFlags.ARMORED); else flags &= ~(ChrResistFlags.ARMORED); }
        }

        public bool resists_fractional_damage {
            get { return flags.HasFlag(ChrResistFlags.IMMUNITY_FRACTIONAL_DAMAGE); }
            set { if (value) flags |= (ChrResistFlags.IMMUNITY_FRACTIONAL_DAMAGE); else flags &= ~(ChrResistFlags.IMMUNITY_FRACTIONAL_DAMAGE); }
        }

        public bool resists_life {
            get { return flags.HasFlag(ChrResistFlags.IMMUNITY_LIFE); }
            set { if (value) flags |= (ChrResistFlags.IMMUNITY_LIFE); else flags &= ~(ChrResistFlags.IMMUNITY_LIFE); }
        }

        public bool resists_sensor {
            get { return flags.HasFlag(ChrResistFlags.IMMUNITY_SENSOR); }
            set { if (value) flags |= (ChrResistFlags.IMMUNITY_SENSOR); else flags &= ~(ChrResistFlags.IMMUNITY_SENSOR); }
        }

        public bool resists_scan {
            get { return flags.HasFlag(ChrResistFlags.IMMUNITY_SCAN); }
            set { if (value) flags |= (ChrResistFlags.IMMUNITY_SCAN); else flags &= ~(ChrResistFlags.IMMUNITY_SCAN); }
        }

        public bool resists_physical_damage {
            get { return flags.HasFlag(ChrResistFlags.IMMUNITY_PHYSICAL_DAMAGE); }
            set { if (value) flags |= (ChrResistFlags.IMMUNITY_PHYSICAL_DAMAGE); else flags &= ~(ChrResistFlags.IMMUNITY_PHYSICAL_DAMAGE); }
        }

        public bool resists_magical_damage {
            get { return flags.HasFlag(ChrResistFlags.IMMUNITY_MAGICAL_DAMAGE); }
            set { if (value) flags |= (ChrResistFlags.IMMUNITY_MAGICAL_DAMAGE); else flags &= ~(ChrResistFlags.IMMUNITY_MAGICAL_DAMAGE); }
        }

        public bool resists_hp_damage {
            get { return flags.HasFlag(ChrResistFlags.IMMUNITY_HP_DAMAGE); }
            set { if (value) flags |= (ChrResistFlags.IMMUNITY_HP_DAMAGE); else flags &= ~(ChrResistFlags.IMMUNITY_HP_DAMAGE); }
        }

        public bool resists_ctb_damage {
            get { return flags.HasFlag(ChrResistFlags.IMMUNITY_CTB_DAMAGE); }
            set { if (value) flags |= (ChrResistFlags.IMMUNITY_CTB_DAMAGE); else flags &= ~(ChrResistFlags.IMMUNITY_CTB_DAMAGE); }
        }

        public bool resists_zanmato {
            get { return flags.HasFlag(ChrResistFlags.IMMUNITY_ZANMATO); }
            set { if (value) flags |= (ChrResistFlags.IMMUNITY_ZANMATO); else flags &= ~(ChrResistFlags.IMMUNITY_ZANMATO); }
        }

        public bool resists_bribe {
            get { return flags.HasFlag(ChrResistFlags.IMMUNITY_BRIBE); }
            set { if (value) flags |= (ChrResistFlags.IMMUNITY_BRIBE); else flags &= ~(ChrResistFlags.IMMUNITY_BRIBE); }
        }
    }
}
