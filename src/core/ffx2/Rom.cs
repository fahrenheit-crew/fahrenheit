// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

// ffx_ps2/ffx2/master/jppc/battle/kernel/rom.h
// Switch release of FFX/X-2 HD

namespace Fahrenheit.FFX2;

public partial struct Rom
{
    public uint poison_time;
    public uint poison_damage;
    public uint regen_time;
    public uint regen_damage;
    public _count_value_e__FixedBuffer count_value;
    public _rapid_shot_e__FixedBuffer rapid_shot;
    public _ATB_speed_e__FixedBuffer ATB_speed;
    public _delay_count_e__FixedBuffer delay_count;
    public _off_count_e__FixedBuffer off_count;

    [InlineArray(2)]
    public partial struct _count_value_e__FixedBuffer
    {
        public uint e0;
    }

    [InlineArray(3)]
    public partial struct _rapid_shot_e__FixedBuffer
    {
        public int e0;
    }

    [InlineArray(4)]
    public partial struct _ATB_speed_e__FixedBuffer
    {
        public short e0;
    }

    [InlineArray(2)]
    public partial struct _delay_count_e__FixedBuffer
    {
        public uint e0;
    }

    [InlineArray(24)]
    public partial struct _off_count_e__FixedBuffer
    {
        public uint e0;
    }
}
