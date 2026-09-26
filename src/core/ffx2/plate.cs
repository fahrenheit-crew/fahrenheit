// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

// ffx_ps2/ffx2/master/jppc/battle/kernel/plate.h
// Switch release of FFX/X-2 HD

namespace Fahrenheit.FFX2;

[InlineArray(4)]
public struct PlateMessages {
    public ExcelTextOffset e0;
}

[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 0x38)]
public struct PlateCreatureData {
    [FieldOffset(0x00)] public ExcelTextOffset       help;
    [FieldOffset(0x04)] public InlineArray2<Ability> abilities;

    [FieldOffset(0x2C)] public StatChanges stat_changes;
}

[StructLayout(LayoutKind.Sequential)]
public struct Plate {
    public ExcelTextOffset name;
    public ExcelTextOffset help;
    public PlateMessages   messages;

    public ushort      bonus;
    public byte        icon;
    public StatChanges stat_changes;

    private byte reserve1;
    private byte reserve2;
    private byte reserve3;

    public InlineArray8<Ability> skill;

    public PlateCreatureData creature_data;
}
