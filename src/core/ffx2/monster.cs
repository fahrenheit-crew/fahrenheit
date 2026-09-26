// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

// ffx_ps2/ffx2/master/jppc/battle/kernel/monster.h
// Switch release of FFX/X-2 HD

using Fahrenheit.FFX2.Battle;

namespace Fahrenheit.FFX2;

[Flags]
public enum SpecialImmunities : ushort {
    NONE            = 0,
    FRACTIONAL_DMG  = 1 << 0,
    DAMAGE_NOT_STOP = 1 << 1, // ?
    RELIFE          = 1 << 2,

    DELAY           = 1 << 6,
    ZANTETSU        = 1 << 7,
    BRIBE           = 1 << 8,
    MAGIC_CANCEL    = 1 << 9, // ?
}

[StructLayout(LayoutKind.Sequential)]
public struct Monster {
    public ExcelTextOffset name;
    public ExcelTextOffset help;

    public uint max_hp;
    public uint max_mp;
    public byte level;
    public byte strength;
    public byte defense;
    public byte magic;
    public byte magic_defense;
    public byte agility;
    public byte accuracy;
    public byte evasion;
    public byte luck;

    public byte thinking_time; // Delay before a monster actually queues a command on their turn?

    public SpecialImmunities special_immunities;

    public ElementFlags elem_absorb;
    public ElementFlags elem_ignore;
    public ElementFlags elem_resist;
    public ElementFlags elem_weak;

    public StatusMap  status_resist1;
    public StatusMap2 status_resist2;

    public StatusFlags  status_auto1;
    public StatusFlags2 status_auto2;

    public StatusDurationMap2 status_time;

    public InlineArray16<T_X2CommandId> abilities;
    public T_X2CommandId                berserk_action;

    public ushort model;
    public ushort motion;
    public ushort sound;

    public ushort oversoul;

    public FiendSpecies monster_type; // What species the monster is for double damage commands

    public ChrLoot loot;

    public byte def_zantetsu;

    private byte   reserve1;
    private ushort reserve2;
}
