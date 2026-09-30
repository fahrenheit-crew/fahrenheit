// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

// ffx_ps2/ffx2/master/jppc/battle/kernel/party.h
// Switch release of FFX/X-2 HD

namespace Fahrenheit.FFX2;

public enum AtbSpeed : byte {
    SLOW   = 0,
    NORMAL = 1,
    FAST   = 2
}

[StructLayout(LayoutKind.Sequential)]
public struct Party {
    public int  config;
    public uint unlocked_primers;
    public uint gil;
    public uint play_time;   // Never gets written to?
    public uint battle_time; // Never gets written to?
    public uint battle_count;

    public InlineArray3<byte> party;

    public AtbSpeed atb_speed;

    public InlineArray8<T_X2CommandId> item_type;
    public InlineArray8<byte>          item_num;

    public InlineArray2<int>   plates_obtained; // Bitfield
    public InlineArray30<byte> job_count;

    public int escape_count; // Never gets written to?

    private ushort reserve;
}
