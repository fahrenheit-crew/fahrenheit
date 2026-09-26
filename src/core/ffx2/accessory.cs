// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

// ffx2/master/jppc/battle/kernel/accessory.h
// Switch release of FFX/X-2 HD

namespace Fahrenheit.FFX2;

[StructLayout(LayoutKind.Sequential)]  
public struct FeedStatChanges {  
    public int  hp;  
    public int  mp;  
    public byte strength;  
    public byte defense;  
    public byte magic;  
    public byte magic_defense;  
    public byte agility;  
    public byte luck;  
    public byte evasion;  
    public byte accuracy;  
}

[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 0x30)]
public struct AccessoryCreatureData {
    [FieldOffset(0x00)] public ExcelTextOffset       help;
    [FieldOffset(0x04)] public InlineArray2<Ability> abilities;

    [FieldOffset(0x1D)] public byte            feed_amount;
    [FieldOffset(0x1E)] public ushort          ability_to_learn;
    [FieldOffset(0x20)] public FeedStatChanges feed_stats;
}

[StructLayout(LayoutKind.Sequential)]
public struct Accessory {
    public ExcelTextOffset name;
    public ExcelTextOffset help;

    public byte ext_data;
    public byte equip;
    public byte user;
    public byte icon;
    public byte seq;

    private byte reserve;

    public StatChanges stat_changes;

    public InlineArray2<Ability> abilities;

    public uint price;

    public AccessoryCreatureData creature_data;
}
