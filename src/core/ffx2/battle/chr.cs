// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX2.Battle;

[StructLayout(LayoutKind.Explicit, Size = 0x370)]
public struct ChrRam {
    [InlineArray(40)]
    public struct ChrName {
        private byte _data;
    }

    [FieldOffset(0x0)]  public ChrName name;
    [FieldOffset(0x28)] public uint    level;

    [FieldOffset(0x2C)] public int max_hp;
    [FieldOffset(0x30)] public int max_mp;
    [FieldOffset(0x34)] public int base_max_hp;
    [FieldOffset(0x38)] public int base_max_mp;

    [FieldOffset(0x3E)] public byte strength;
    [FieldOffset(0x3F)] public byte defense;
    [FieldOffset(0x40)] public byte magic;
    [FieldOffset(0x41)] public byte magic_defense;
    [FieldOffset(0x42)] public byte agility;
    [FieldOffset(0x43)] public byte luck;
    [FieldOffset(0x44)] public byte evasion;
    [FieldOffset(0x45)] public byte accuracy;

}

[StructLayout(LayoutKind.Explicit, Size = 0x17E0)]
public struct Chr {
    [FieldOffset(0x358)] public ChrRam ram;
}
