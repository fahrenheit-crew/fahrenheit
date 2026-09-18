// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX.SphereGrid;

[Flags]
public enum SphereGridLinkProperties : byte {
    NONE           = 0,
    CAN_MOVE_THROUGH         = 1 << 0,
    CONNECTED      = 1 << 1,
    FLAG_2         = 1 << 2,
    JUST_ACTIVATED = 1 << 3,
}

public static partial class FhEnumExt {
    extension(SphereGridLinkProperties flags) {
        public bool can_move_through {
            get { return flags.HasFlag(SphereGridLinkProperties.CAN_MOVE_THROUGH); }
            set { if (value) flags |= (SphereGridLinkProperties.CAN_MOVE_THROUGH); else flags &= ~(SphereGridLinkProperties.CAN_MOVE_THROUGH); }
        }

        public bool connected {
            get { return flags.HasFlag(SphereGridLinkProperties.CONNECTED); }
            set { if (value) flags |= (SphereGridLinkProperties.CONNECTED); else flags &= ~(SphereGridLinkProperties.CONNECTED); }
        }

        public bool flag2 {
            get { return flags.HasFlag(SphereGridLinkProperties.FLAG_2); }
            set { if (value) flags |= (SphereGridLinkProperties.FLAG_2); else flags &= ~(SphereGridLinkProperties.FLAG_2); }
        }

        public bool just_activated {
            get { return flags.HasFlag(SphereGridLinkProperties.JUST_ACTIVATED); }
            set { if (value) flags |= (SphereGridLinkProperties.JUST_ACTIVATED); else flags &= ~(SphereGridLinkProperties.JUST_ACTIVATED); }
        }
    }
}

[StructLayout(LayoutKind.Sequential)]
public struct SphereGridLinkPoint {
    public float x;
    public float y;
    public float offset_x1;
    public float offset_y2;
    public float offset_x2;
    public float offset_y1;

    //TODO: Document why this uses `Unsafe`.
    public Vector2 pos {
        get => Unsafe.As<float, Vector2>(ref x);
        set => Unsafe.As<float, Vector2>(ref x) = value;
    }
    public Vector2 offset_1 => new(offset_x1, offset_y1);
    public Vector2 offset_2 => new(offset_x2, offset_y2);
}

[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x14)]
public unsafe struct SphereGridLink {
    [FieldOffset(0x0)]  public short node_a_idx;
    [FieldOffset(0x2)]  public short node_b_idx;
    [FieldOffset(0x4)]  public short anchor_idx;

    [FieldOffset(0xC)]  public byte  activated_by;
    [FieldOffset(0xD)]  public byte  point_count;

    [FieldOffset(0xE)]  public SphereGridLinkProperties flags;

    [FieldOffset(0x10)] public SphereGridLinkPoint* points;

    public readonly SphereGridNode node_a => Globals.SphereGrid.lpamng->nodes[node_a_idx];
    public readonly SphereGridNode node_b => Globals.SphereGrid.lpamng->nodes[node_b_idx];
    public readonly SphereGridNode anchor => Globals.SphereGrid.lpamng->nodes[anchor_idx];

    public Vector2 get_midpoint() {
        int                  mid_point_idx = (point_count - 1) / 2;
        SphereGridLinkPoint* mid_point     = points + mid_point_idx;

        if (point_count % 2 == 1) {
            return mid_point->pos;
        }

        int                  mid_point2_idx = mid_point_idx + 1;
        SphereGridLinkPoint* mid_point2     = points + mid_point2_idx;

        return (mid_point->pos + mid_point2->pos) / 2f;
    }
}
