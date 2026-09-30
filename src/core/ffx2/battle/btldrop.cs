// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX2.Battle;

[StructLayout(LayoutKind.Sequential)]
public struct ChrItemLoot {
    public ushort item_common;
    public ushort item_common_amount;

    public ushort item_rare;
    public ushort item_rare_amount;

    public ushort steal_common;
    public ushort steal_common_amount;

    public ushort steal_rare;
    public ushort steal_rare_amount;

    public ushort bribe_common;
    public ushort bribe_common_amount;

    public ushort bribe_rare;
    public ushort bribe_rare_amount;
}

[StructLayout(LayoutKind.Sequential)]
public struct ChrLoot {
    public int         exp;
    public int         gil;
    public int         gil_to_steal;
    public ushort      ap;
    public byte        drop_chance;
    public byte        steal_chance;
    public ChrItemLoot loot;
}
