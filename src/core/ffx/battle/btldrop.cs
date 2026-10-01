// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX.Battle;

[StructLayout(LayoutKind.Sequential)]
public struct ChrItemLoot {
    public ushort item_primary_common;
    public ushort item_primary_rare;
    public ushort item_secondary_common;
    public ushort item_secondary_rare;
    public byte   amount_primary_common;
    public byte   amount_primary_rare;
    public byte   amount_secondary_common;
    public byte   amount_secondary_rare;
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct ChrStealLoot {
    public ushort item_common;
    public ushort item_rare;
    public byte   amount_common;
    public byte   amount_rare;
    public ushort item_bribe;
    public byte   amount_bribe;
}

[InlineArray(8)]
public struct ChrEquipmentLootAbilitiesArray {
    private ushort _u;
}

[StructLayout(LayoutKind.Sequential)]
public struct ChrEquipmentLootAbilities {
    public ChrEquipmentLootAbilitiesArray weapon_abilities;
    public ChrEquipmentLootAbilitiesArray armor_abilities;
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct ChrEquipmentLoot {
    public byte slot_count;
    public byte dmg_formula;
    public byte crit_bonus;
    public byte power;
    public byte ability_count;

    public ChrEquipmentLootAbilities abilities_tidus;
    public ChrEquipmentLootAbilities abilities_yuna;
    public ChrEquipmentLootAbilities abilities_auron;
    public ChrEquipmentLootAbilities abilities_kimahri;
    public ChrEquipmentLootAbilities abilities_wakka;
    public ChrEquipmentLootAbilities abilities_lulu;
    public ChrEquipmentLootAbilities abilities_rikku;
}

[StructLayout(LayoutKind.Sequential)]
public struct ChrLoot {
    public ushort gil;
    public ushort ap;
    public ushort ap_overkill;
    public ushort ronso_rage;

    public byte drop_chance_primary;
    public byte drop_chance_secondary;
    public byte steal_chance;
    public byte drop_chance_equipment;

    public ChrItemLoot      item_loot;
    public ChrItemLoot      item_loot_overkill;
    public ChrStealLoot     steal_loot;
    public ChrEquipmentLoot equipment_loot;

    public byte zanmato_level;
    public byte gil_steal;
    public uint monster_arena_price;
}
