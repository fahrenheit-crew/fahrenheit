// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX;

[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 0x2c)]
public unsafe struct TOMesWinWork {
    [FieldOffset(0x08)] public byte* text;
    [FieldOffset(0x0C)] public byte* __0xc;
    [FieldOffset(0x14)] public short status;
    [FieldOffset(0x16)] public short __0x16;
    [FieldOffset(0x18)] public int   __0x18;
    [FieldOffset(0x1D)] public byte  __0x1d;
    [FieldOffset(0x20)] public byte  __0x20;
}
