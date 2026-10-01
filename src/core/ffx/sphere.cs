// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX;

[Flags]
public enum SphereRange : byte {
    NORMAL    = 1,
    UNLIMITED = 1 << 5
}

public enum SphereBehavior : ushort {
    NONE      = 0,
    ACTIVATOR = 1,
    MODIFIER  = 2
}

[Flags]
public enum SphereTargets : ushort {
    STRENGTH      = 1,
    DEFENSE       = 1 << 1,
    MAGIC         = 1 << 2,
    MAGIC_DEFENSE = 1 << 3,
    AGILITY       = 1 << 4,
    LUCK          = 1 << 5,
    EVASION       = 1 << 6,
    ACCURACY      = 1 << 7,
    HP            = 1 << 8,
    MP            = 1 << 9,
    ABILITY       = 1 << 10
}

[StructLayout(LayoutKind.Sequential)]
public struct Sphere {
    public ExcelSimplifiableTextOffset help;

    public SphereBehavior type;
    public SphereTargets  activates;
    public SphereRange    range;
    public byte           special_role;

    private ushort _0x0E;
}
