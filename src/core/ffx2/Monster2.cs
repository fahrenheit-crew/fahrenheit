// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

// ffx_ps2/ffx2/master/jppc/battle/kernel/monster2.h
// Switch release of FFX/X-2 HD

namespace Fahrenheit.FFX2;

public partial struct Monster2
{
    public uint name;
    public uint help;
    public uint hp_max;
    public uint mp_max;
    public byte level;
    public byte str;
    public byte vit;
    public byte mag;
    public byte spirit;
    public byte dex;
    public byte hit;
    public byte avoid;
    public byte luck;
    public byte thinking_time;
    public ushort special;
    public byte abs_element;
    public byte inv_element;
    public byte half_element;
    public byte weak_element;
    public _def_status_e__FixedBuffer def_status;
    public _def_status2_e__FixedBuffer def_status2;

    public int auto_status;

    public int auto_status2;
    public _status_time_e__FixedBuffer status_time;
    public _waza_e__FixedBuffer waza;
    public ushort basaku;
    public ushort mon_model;
    public ushort mon_motion;
    public ushort monster_sound;
    public ushort oversoul;
    public ushort monster_type;

    public int exp;

    public int gill;

    public int steal_gill;
    public ushort get_ap;
    public byte drop;
    public byte steal;
    public _drop_item_e__FixedBuffer drop_item;
    public _steal_item_e__FixedBuffer steal_item;
    public _bribery_item_e__FixedBuffer bribery_item;
    public byte def_zantetu;
    public byte reserve1;
    public ushort reserve2;

    [InlineArray(24)]
    public partial struct _def_status_e__FixedBuffer
    {
        public byte e0;
    }

    [InlineArray(24)]
    public partial struct _def_status2_e__FixedBuffer
    {
        public byte e0;
    }

    [InlineArray(24)]
    public partial struct _status_time_e__FixedBuffer
    {
        public sbyte e0;
    }

    [InlineArray(16)]
    public partial struct _waza_e__FixedBuffer
    {
        public ushort e0;
    }

    [InlineArray(4)]
    public partial struct _drop_item_e__FixedBuffer
    {
        public ushort e0;
    }

    [InlineArray(4)]
    public partial struct _steal_item_e__FixedBuffer
    {
        public ushort e0;
    }

    [InlineArray(4)]
    public partial struct _bribery_item_e__FixedBuffer
    {
        public ushort e0;
    }
}
