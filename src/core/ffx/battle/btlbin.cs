// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX.Battle;

// BtlBin related structs have been migrated in current state from Archipelago, in order to support call.cs delegate migration
// May require further refining.
[ExtendedLayout(ExtendedLayoutKind.CStruct)]
public unsafe struct BtlBinField {
    [InlineArray(8)]
    public struct FieldName {
        private byte _data;
    }

    public short     id;
    public short     data_offset;
    public short     __0x4;
    public FieldName name;
}

[StructLayout(LayoutKind.Explicit, Size = 0x2)]
public unsafe struct BtlBinEncounter {
    [FieldOffset(0x0)] public byte total_formation_count;
    [FieldOffset(0x1)] public byte group_count;

    [FieldOffset(0x2)] public BtlBinGroup first_group;

    //TODO: Add a helper to access each group by index

    //[UnscopedRef]
    //public Span<BtlBinGroup> groups => MemoryMarshal.CreateSpan(ref _first_group, group_count);

}

[StructLayout(LayoutKind.Explicit, Size = 0x5)]
public unsafe struct BtlBinGroup {
    [FieldOffset(0x0)] public byte formation_count;
    [FieldOffset(0x1)] public short battlefield;
    [FieldOffset(0x3)] public byte grace;
    [FieldOffset(0x4)] public byte total_weight;

    [FieldOffset(0x5)] public BtlBinFormation first_formation;

    //TODO: Add a helper to access each formation by index

    //[UnscopedRef]
    //public Span<BtlBinFormation> formations => MemoryMarshal.CreateSpan(ref _first_formation, formation_count);
}

[ExtendedLayout(ExtendedLayoutKind.CStruct)]
public unsafe struct BtlBinFormation {
    public byte index;
    public byte weight;
}
