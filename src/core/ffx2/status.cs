// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX2;

[StructLayout(LayoutKind.Sequential)]
public struct StatusMap {
    public byte death;
    public byte petrification;
    public byte sleep;
    public byte silence;
    public byte darkness;
    public byte poison;
    public byte confusion;
    public byte berserk;
    public byte curse;
    public byte sentinel;
    public byte eject;
    public byte double_hp;
    public byte double_mp;
    public byte spellspring;
    public byte damage_9999;
    public byte always_critical;
    public byte pointless;
    public byte itchy;
    public byte auto_life;

    private byte unused1;
    private byte unused2;
    private byte unused3;
    private byte unused4;
    private byte unused5;
}

[StructLayout(LayoutKind.Sequential)]
public struct StatusDurationMap {
    public int death;
    public int petrification;
    public int sleep;
    public int silence;
    public int darkness;
    public int poison;
    public int confusion;
    public int berserk;
    public int curse;
    public int sentinel;
    public int eject;
    public int double_hp;
    public int double_mp;
    public int spellspring;
    public int damage_9999;
    public int always_critical;
    public int pointless;
    public int itchy;
    public int auto_life;

    private int unused1;
    private int unused2;
    private int unused3;
    private int unused4;
    private int unused5;
}

[Flags]
public enum StatusFlags : uint {
    NONE            =       0,
    DEATH           = 1 <<  0,
    PETRIFICATION   = 1 <<  1,
    SLEEP           = 1 <<  2,
    SILENCE         = 1 <<  3,
    DARKNESS        = 1 <<  4,
    POISON          = 1 <<  5,
    CONFUSION       = 1 <<  6,
    BERSERK         = 1 <<  7,
    CURSE           = 1 <<  8,
    SENTINEL        = 1 <<  9,
    EJECT           = 1 << 10,
    DOUBLE_HP       = 1 << 11,
    DOUBLE_MP       = 1 << 12,
    SPELLSPRING     = 1 << 13,
    DAMAGE_9999     = 1 << 14,
    ALWAYS_CRITICAL = 1 << 15,
    POINTLESS       = 1 << 16,
    ITCHY           = 1 << 17,
    AUTO_LIFE       = 1 << 18,
}

public static partial class FhEnumExt {
    extension(StatusFlags flags) {
        public bool death {
            get { return flags.HasFlag(StatusFlags.DEATH); }
            set { if (value) flags |= StatusFlags.DEATH; else flags &= ~StatusFlags.DEATH; }
        }

        public bool petrification {
            get { return flags.HasFlag(StatusFlags.PETRIFICATION); }
            set { if (value) flags |= StatusFlags.PETRIFICATION; else flags &= ~StatusFlags.PETRIFICATION; }
        }

        public bool sleep {
            get { return flags.HasFlag(StatusFlags.SLEEP); }
            set { if (value) flags |= StatusFlags.SLEEP; else flags &= ~StatusFlags.SLEEP; }
        }

        public bool silence {
            get { return flags.HasFlag(StatusFlags.SILENCE); }
            set { if (value) flags |= StatusFlags.SILENCE; else flags &= ~StatusFlags.SILENCE; }
        }

        public bool darkness {
            get { return flags.HasFlag(StatusFlags.DARKNESS); }
            set { if (value) flags |= StatusFlags.DARKNESS; else flags &= ~StatusFlags.DARKNESS; }
        }

        public bool poison {
            get { return flags.HasFlag(StatusFlags.POISON); }
            set { if (value) flags |= StatusFlags.POISON; else flags &= ~StatusFlags.POISON; }
        }

        public bool confusion {
            get { return flags.HasFlag(StatusFlags.CONFUSION); }
            set { if (value) flags |= StatusFlags.CONFUSION; else flags &= ~StatusFlags.CONFUSION; }
        }

        public bool berserk {
            get { return flags.HasFlag(StatusFlags.BERSERK); }
            set { if (value) flags |= StatusFlags.BERSERK; else flags &= ~StatusFlags.BERSERK; }
        }

        public bool curse {
            get { return flags.HasFlag(StatusFlags.CURSE); }
            set { if (value) flags |= StatusFlags.CURSE; else flags &= ~StatusFlags.CURSE; }
        }

        public bool sentinel {
            get { return flags.HasFlag(StatusFlags.SENTINEL); }
            set { if (value) flags |= StatusFlags.SENTINEL; else flags &= ~StatusFlags.SENTINEL; }
        }

        public bool eject {
            get { return flags.HasFlag(StatusFlags.EJECT); }
            set { if (value) flags |= StatusFlags.EJECT; else flags &= ~StatusFlags.EJECT; }
        }

        public bool double_hp {
            get { return flags.HasFlag(StatusFlags.DOUBLE_HP); }
            set { if (value) flags |= StatusFlags.DOUBLE_HP; else flags &= ~StatusFlags.DOUBLE_HP; }
        }

        public bool double_mp {
            get { return flags.HasFlag(StatusFlags.DOUBLE_MP); }
            set { if (value) flags |= StatusFlags.DOUBLE_MP; else flags &= ~StatusFlags.DOUBLE_MP; }
        }

        public bool spellspring {
            get { return flags.HasFlag(StatusFlags.SPELLSPRING); }
            set { if (value) flags |= StatusFlags.SPELLSPRING; else flags &= ~StatusFlags.SPELLSPRING; }
        }

        public bool damage_9999 {
            get { return flags.HasFlag(StatusFlags.DAMAGE_9999); }
            set { if (value) flags |= StatusFlags.DAMAGE_9999; else flags &= ~StatusFlags.DAMAGE_9999; }
        }

        public bool always_critical {
            get { return flags.HasFlag(StatusFlags.ALWAYS_CRITICAL); }
            set { if (value) flags |= StatusFlags.ALWAYS_CRITICAL; else flags &= ~StatusFlags.ALWAYS_CRITICAL; }
        }

        public bool pointless {
            get { return flags.HasFlag(StatusFlags.POINTLESS); }
            set { if (value) flags |= StatusFlags.POINTLESS; else flags &= ~StatusFlags.POINTLESS; }
        }

        public bool itchy {
            get { return flags.HasFlag(StatusFlags.ITCHY); }
            set { if (value) flags |= StatusFlags.ITCHY; else flags &= ~StatusFlags.ITCHY; }
        }

        public bool auto_life {
            get { return flags.HasFlag(StatusFlags.AUTO_LIFE); }
            set { if (value) flags |= StatusFlags.AUTO_LIFE; else flags &= ~StatusFlags.AUTO_LIFE; }
        }
    }
}

[StructLayout(LayoutKind.Sequential)]
public struct StatusMap2 {
    public byte shell;
    public byte protect;
    public byte reflect;
    public byte regen;
    public byte haste;
    public byte slow;
    public byte stop;
    public byte strength_bonus;
    public byte magic_bonus;
    public byte defense_bonus;
    public byte magic_defense_bonus;
    public byte accuracy_bonus;
    public byte evasion_bonus;
    public byte luck_bonus;
    public byte doom_counter;
    public byte immunity_physical_damage;
    public byte immunity_magical_damage;
    public byte invincible;

    private byte unused1;
    private byte unused2;
    private byte unused3;
    private byte unused4;
    private byte unused5;
    private byte unused6;
}

[StructLayout(LayoutKind.Sequential)]
public struct StatusDurationMap2 {
    public sbyte shell;
    public sbyte protect;
    public sbyte reflect;
    public sbyte regen;
    public sbyte haste;
    public sbyte slow;
    public sbyte stop;
    public sbyte strength_bonus;
    public sbyte magic_bonus;
    public sbyte defense_bonus;
    public sbyte magic_defense_bonus;
    public sbyte accuracy_bonus;
    public sbyte evasion_bonus;
    public sbyte luck_bonus;
    public sbyte doom_counter;
    public sbyte immunity_physical_damage;
    public sbyte immunity_magical_damage;
    public sbyte invincible;

    private sbyte unused1;
    private sbyte unused2;
    private sbyte unused3;
    private sbyte unused4;
    private sbyte unused5;
    private sbyte unused6;
}

/// <remarks> 
///     These flags are used in Auto Ability and MonStats structures to toggle an Auto-Status flag.
///     They are NOT used for checking whether a character currently has the status or not; that is
///     done by checking if the StatusDurationMap2 remaining is greater than 0.
/// </remarks>
[Flags]
public enum StatusFlags2 : uint {
    NONE                     =       0,
    SHELL                    = 1 <<  0,
    PROTECT                  = 1 <<  1,
    REFLECT                  = 1 <<  2,
    REGEN                    = 1 <<  3,
    HASTE                    = 1 <<  4,
    SLOW                     = 1 <<  5,
    STOP                     = 1 <<  6,
    STRENGTH_BONUS           = 1 <<  7,
    MAGIC_BONUS              = 1 <<  8,
    DEFENSE_BONUS            = 1 <<  9,
    MAGIC_DEFENSE_BONUS      = 1 << 10,
    ACCURACY_BONUS           = 1 << 11,
    EVASION_BONUS            = 1 << 12,
    LUCK_BONUS               = 1 << 13,
    DOOM                     = 1 << 14,
    IMMUNITY_PHYSICAL_DAMAGE = 1 << 15,
    IMMUNITY_MAGICAL_DAMAGE  = 1 << 16,
    INVINCIBLE               = 1 << 17,
}
