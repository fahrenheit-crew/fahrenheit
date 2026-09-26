// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX2;

[StructLayout(LayoutKind.Explicit, Size = 0x910)]
public unsafe struct Actor {
    [FieldOffset(0x0)]   public ushort    chr_num;
    [FieldOffset(0x2)]   public ushort    chr_enabled;
    [FieldOffset(0x4)]   public byte*     chr_name;
    [FieldOffset(0xC)]   public Vector4   chr_pos_vec;

    [FieldOffset(0x198)]   public uint    chr_id;
}
