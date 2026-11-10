// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX2;

[StructLayout(LayoutKind.Explicit, Size = 0x180)]
public unsafe struct FriendMonsterCommand {
    [FieldOffset(0x0)]   public ushort              command_id;
    [FieldOffset(0xC6)]  public InlineArray11<byte> __0xC6;
    [FieldOffset(0x17F)] public byte                properties;

    public byte get_double  => (byte)(((properties >> 1) & 7) | 8); // Unsure about the | 8
    public byte get_reflect => (byte)( (properties >> 5)      | 8); // Unsure about the | 8
}

[InlineArray(8)]
public struct FriendMonsterCommandArray {
    private FriendMonsterCommand _data;
}

[StructLayout(LayoutKind.Explicit, Size = 0xE38)]
public unsafe struct FriendMonster {
    [FieldOffset(0x0)]   public InlineArray4<ushort>      commands_0x3;
    [FieldOffset(0x8)]   public InlineArray4<ushort>      commands_0x8;
    [FieldOffset(0x10)]  public InlineArray37<ushort>     learn_0x3;
    [FieldOffset(0x5A)]  public InlineArray37<ushort>     learn_0x4;
    [FieldOffset(0xA4)]  public InlineArray16<ushort>     learn_0x8;
    [FieldOffset(0xC4)]  public FriendMonsterCommandArray __0xC4;
    [FieldOffset(0xCC4)] public InlineArray370<byte>      __0xCC4; // max hp?

    public bool get_command_learned(ushort command_id) {
        int command_type = command_id >> 0xC;
        int offset       = (command_id >> 4) & 0xFF;

        return command_type switch {
            8 => offset <= 15 && (learn_0x8[offset] & (1 << (command_id & 0xF))) != 0,
            4 => offset <= 36 && (learn_0x4[offset] & (1 << (command_id & 0xF))) != 0,
            3 => offset <= 36 && (learn_0x3[offset] & (1 << (command_id & 0xF))) != 0,
            _ => false
        };
    }
}
