// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX;

[InlineArray(7)]
public struct WeaponNameTextArray {
    private ExcelTextOffset _e0;
}

[InlineArray(7)]
public struct WeaponNameModelIdArray {
    private ushort _e0;
}

[StructLayout(LayoutKind.Sequential)]
public struct WeaponName {
    public  WeaponNameTextArray    names;
    public  WeaponNameTextArray    names_hira;
    public  WeaponNameModelIdArray model_ids;
    private ushort                 _0x46;
}
