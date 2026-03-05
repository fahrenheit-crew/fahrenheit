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

    [FieldOffset(0x112B8)] public Vector4              __0x112B8;
    [FieldOffset(0x112D8)] public uint                 __0x112D8;
    [FieldOffset(0x112DC)] public uint                 __0x112DC;
    [FieldOffset(0x112E0)] public uint                 __0x112E0;
    [FieldOffset(0x112F4)] public float                current_halo_width;
    [FieldOffset(0x112F8)] public uint                 __0x112F8;
    [FieldOffset(0x112FC)] public short                selected_node_idx;
    [FieldOffset(0x11306)] public byte                 __0x11306;
    [FieldOffset(0x11308)] public Vector4              cam_desired_pos;
    [FieldOffset(0x11318)] public Vector4              cam_limited_pos;
    [FieldOffset(0x11340)] public Vector4i             tilt_vector;
    [FieldOffset(0x11350)] public Vector4              zoom_vector; // Only .x matters
    [FieldOffset(0x113E0)] public Matrix4x4            __0x113E0;
    [FieldOffset(0x11520)] public Vector4              __0x11520;
    [FieldOffset(0x11560)] public Vector4              __0x11560;
    [FieldOffset(0x115A0)] public float                __0x115A0;
    [FieldOffset(0x115A4)] public float                __0x115A4;
    [FieldOffset(0x115A8)] public int                  __0x115A8;
    [FieldOffset(0x115AC)] public int                  __0x115AC;
    [FieldOffset(0x115B0)] public int                  __0x115B0;
    [FieldOffset(0x115B4)] public int                  __0x115B4;
    [FieldOffset(0x115B8)] public byte                 fade_timer;
    [FieldOffset(0x115B9)] public byte                 __0x115B9;
    [FieldOffset(0x115BC)] public byte                 current_chr_id;
    [FieldOffset(0x115BD)] public byte                 __0x115BD;
    [FieldOffset(0x115BE)] public byte                 __0x115BE;
    [FieldOffset(0x115BF)] public byte                 __0x115BF;
    [FieldOffset(0x115C3)] public byte                 __0x115C3;
    [FieldOffset(0x115C4)] public byte                 __0x115C4;
    [FieldOffset(0x115C5)] public byte                 __0x115C5;
    [FieldOffset(0x115C6)] public byte                 __0x115C6;
    [FieldOffset(0x115C7)] public byte                 __0x115C7;
    [FieldOffset(0x115C8)] public byte                 __0x115C8;
    [FieldOffset(0x115C9)] public byte                 available_indicators;
    [FieldOffset(0x115CB)] public SphereGridTilt       tilt_level;
    [FieldOffset(0x115CC)] public SphereGridZoom       zoom_level;
    [FieldOffset(0x115CD)] public byte                 __0x115CD;
    [FieldOffset(0x115CE)] public ushort               tilt_time_left; // in frames
    [FieldOffset(0x115D0)] public ushort               zoom_time_left; // in frames
    [FieldOffset(0x115D4)] public float                tilt_start;
    [FieldOffset(0x115D8)] public float                tilt_target;
    [FieldOffset(0x115DC)] public float                zoom_start;
    [FieldOffset(0x115E0)] public float                zoom_target;
    [FieldOffset(0x115E8)] public float                fade_dark_inv_alpha;
    [FieldOffset(0x115F4)] public float                halo_alpha;
    [FieldOffset(0x115F8)] public SphereGridLink*      next_move_link;
    [FieldOffset(0x115FC)] public float                x_min;
    [FieldOffset(0x11600)] public float                y_min;
    [FieldOffset(0x11604)] public float                x_max;
    [FieldOffset(0x11608)] public float                y_max;
    [FieldOffset(0x1160C)] public float                x_min2;
    [FieldOffset(0x11610)] public float                y_min2;
    [FieldOffset(0x11614)] public float                x_max2;
    [FieldOffset(0x11618)] public float                y_max2;
    [FieldOffset(0x1161C)] public int                  slv_queued;
    [FieldOffset(0x11620)] public float                moving_progress; // per link/knot
    [FieldOffset(0x11624)] public float                moving_speed;
    [FieldOffset(0x11628)] public float                moving_halo_start_width;
    [FieldOffset(0x1162C)] public float                moving_halo_target_width;
    [FieldOffset(0x11630)] public short                move_start_node_idx;
    [FieldOffset(0x11632)] public short                move_next_target_node_idx;
    [FieldOffset(0x11634)] public short                move_last_target_node_idx;
    [FieldOffset(0x11636)] public short                move_next_link_anchor_idx;
    [FieldOffset(0x11638)] public byte                 moving_chr_id;
    [FieldOffset(0x1163C)] public Vector4              move_prev_node_pos;
    [FieldOffset(0x1164C)] public byte                 __0x1164C;
    [FieldOffset(0x1164D)] public byte                 __0x1164D;
    [FieldOffset(0x1164E)] public ushort               __0x1164E;
    [FieldOffset(0x11650)] public byte                 __0x11650;
    [FieldOffset(0x11654)] public byte*                node_change_text;
    [FieldOffset(0x11658)] public byte*                previous_activated_node_name_ptr;
    [FieldOffset(0x1165C)] public byte*                activated_node_name_ptr;
    [FieldOffset(0x11666)] public ushort               __0x11666;
    [FieldOffset(0x11668)] public SphereGridLinkPoint* link_points;
    [FieldOffset(0x1166C)] public InlineArray4<ushort> abmap_input;
    [FieldOffset(0x11698)] public short                __0x11698;
    [FieldOffset(0x1169C)] public uint                 __0x1169C;
    [FieldOffset(0x116A0)] public uint                 __0x116A0;
    [FieldOffset(0x116A4)] public uint                 __0x116A4;
    [FieldOffset(0x116A8)] public int                  should_update_node; // a node idx to update a specific node, -1 for all
    [FieldOffset(0x116AC)] public int                  should_update;
    [FieldOffset(0x116B0)] public int                  __0x116B0;

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
