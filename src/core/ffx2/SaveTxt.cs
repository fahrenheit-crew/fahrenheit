// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

// ffx_ps2/ffx2/master/jppc/battle/kernel/save_txt.h
// Switch release of FFX/X-2 HD

namespace Fahrenheit.FFX2;

public partial struct SaveTxt
{
    public _command_e__FixedBuffer command;
    public _help_e__FixedBuffer help;

    [InlineArray(2)]
    public partial struct _command_e__FixedBuffer
    {
        public uint e0;
    }

    [InlineArray(2)]
    public partial struct _help_e__FixedBuffer
    {
        public uint e0;
    }
}
