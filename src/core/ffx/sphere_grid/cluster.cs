// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX.SphereGrid;

public static class SphereGridClusterType {
    public const short SINGLE = 0;
    public const short SMALL  = 1;
    public const short MEDIUM = 2;
    public const short BIG    = 3;

    public const short SINGLE_ALT = SINGLE + 4;
    public const short SMALL_ALT  = SMALL  + 4;
    public const short MEDIUM_ALT = MEDIUM + 4;
    public const short BIG_ALT    = BIG    + 4;
}

[StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x10)]
public unsafe struct SphereGridCluster {
    public  short x;
    public  short y;
    private short __0x4;
    public  short type;

    public Vector2 pos {
        get => new(x, y);
        set {
            x = (short)value.X;
            y = (short)value.Y;
        }
    }

    public readonly Vector2 size => Globals.SphereGrid.lpamng->cluster_sizes[type].xy;
}
