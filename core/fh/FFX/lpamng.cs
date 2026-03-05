// SPDX-License-Identifier: MIT

namespace Fahrenheit.FFX;

public enum SphereGridTilt : byte {
    FLAT,
    SLIGHT_TILT,
    FAR_TILT,
}

public enum SphereGridZoom : byte {
    CLOSE,
    MEDIUM,
    FAR,
    VERY_FAR, // supported but not allowed by vanilla
}

public static class SphereGridZoomExt {
    public static float get_zoom(this SphereGridZoom zoom_level) {
        return zoom_level switch {
            SphereGridZoom.VERY_FAR => 0.125f,
            SphereGridZoom.FAR     => 0.25f,
            SphereGridZoom.MEDIUM  => 0.5f,
            SphereGridZoom.CLOSE   => 1.0f,
            _                      => 0.5f,
        };
    }

    public static SphereGridZoom get_closest(float zoom, bool allow_very_far = false) {
        return zoom switch {
             <= 0.1875f => allow_very_far ? SphereGridZoom.VERY_FAR : SphereGridZoom.FAR,
             <= 0.375f  => SphereGridZoom.FAR,
             <= 0.75f   => SphereGridZoom.MEDIUM,
            _           => SphereGridZoom.CLOSE,
        };
    }
}

[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x12FC0)]
public unsafe struct LpAbilityMapEngine {
    [InlineArray(128)]
    public struct SphereGridClusterArray {
        private SphereGridCluster _data;
    }

    [InlineArray(1024)]
    public struct SphereGridNodeArray {
        private SphereGridNode _data;
    }

    [InlineArray(1024)]
    public struct SphereGridLinkArray {
        private SphereGridLink _data;
    }

    [InlineArray(8)]
    public struct SphereGridClusterSizesArray {
        private Vec2s16 _data;
    }

    [InlineArray(130)]
    public struct SphereGridNodeTypeInfoArray {
        private SphereGridNodeTypeInfo _data;
    }

    [InlineArray(7)]
    public struct SphereGridChrInfoArray {
        private SphereGridChrInfo _data;
    }

    [FieldOffset(0x0)]     public short cluster_count;
    [FieldOffset(0x2)]     public short node_count;
    [FieldOffset(0x4)]     public short link_count;

    [FieldOffset(0x8)]     public SphereGridClusterArray clusters;
    [FieldOffset(0x808)]   public SphereGridNodeArray    nodes;
    [FieldOffset(0xA808)]  public SphereGridLinkArray    links;

    [FieldOffset(0xF808)]  public SphereGridClusterSizesArray cluster_sizes;
    [FieldOffset(0xF828)]  public SphereGridNodeTypeInfoArray node_type_infos;
    [FieldOffset(0x11088)] public SphereGridChrInfoArray      party_infos;

    [FieldOffset(0x112F4)] public float          current_halo_width;
    [FieldOffset(0x112FC)] public short          selected_node_idx;
    [FieldOffset(0x11308)] public Vector4        cam_desired_pos;
    [FieldOffset(0x11318)] public Vector4        cam_limited_pos;
    [FieldOffset(0x11350)] public Vector4        zoom_vector; // Only .x matters
    [FieldOffset(0x115BC)] public byte           current_chr_id;
    [FieldOffset(0x115CB)] public SphereGridTilt tilt_level;
    [FieldOffset(0x115CC)] public SphereGridZoom zoom_level;
    [FieldOffset(0x115D0)] public ushort         zoom_time_left; // in frames
    [FieldOffset(0x115DC)] public float          start_zoom;
    [FieldOffset(0x115E0)] public float          target_zoom;
    [FieldOffset(0x11620)] public float          moving_progress; // per link/knot
    [FieldOffset(0x11624)] public float          moving_speed;
    [FieldOffset(0x11628)] public float          moving_halo_start_width;
    [FieldOffset(0x1162C)] public float          moving_halo_target_width;
    [FieldOffset(0x11630)] public short          move_start_node_idx;
    [FieldOffset(0x11632)] public short          next_move_target_node_idx;
    [FieldOffset(0x11634)] public short          last_move_target_node_idx;
    [FieldOffset(0x11638)] public byte           moving_chr_id;
    [FieldOffset(0x1165C)] public byte*          activated_node_name_ptr;
    [FieldOffset(0x116A8)] public int            should_update_node; // a node idx to update a specific node, -1 for all
    [FieldOffset(0x116AC)] public int            should_update;

    public float current_zoom {
        get => zoom_vector.X;
        set => zoom_vector.X = zoom_vector.Y = zoom_vector.Z = value;
    }

    public bool get_node_idx(SphereGridNode node, out short? node_idx) {
        SphereGridNode* node_ptr = &node;

        fixed (SphereGridNode* first_node_ptr = &nodes[0]) {
            long idx = (node_ptr - first_node_ptr) / sizeof(SphereGridNode);

            if (idx is < 0 or > 1024) {
                node_idx = null;
                return false;
            }

            node_idx = (short)idx;
            return true;
        }
    }
}
