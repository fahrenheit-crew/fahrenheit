// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX2.Battle;

[StructLayout(LayoutKind.Sequential, Size = 0x14)]
public struct DamageBuffer {
    public byte   chr_id;
    public byte   is_alive;
    public byte   chain_count;
    public byte   unk1;
    public ushort com_id;
    public ushort target_stat;
    public int    damage_hp;
    public int    damage_mp;
    public int    damage_atb;
}
