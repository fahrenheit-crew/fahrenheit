// SPDX-License-Identifier: MIT

namespace Fahrenheit.FFX;

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

[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x10)]
public unsafe struct SphereGridCluster {
    [FieldOffset(0x0)] public short x;
    [FieldOffset(0x2)] public short y;
    [FieldOffset(0x6)] public short type;

    public Vector2 pos {
        get => new(x, y);
        set {
            x = (short)value.X;
            y = (short)value.Y;
        }
    }

    public readonly Vector2 size => Globals.SphereGrid.lpamng->cluster_sizes[type].xy;
}

[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x14)]
public unsafe struct SphereGridLink {
    [FieldOffset(0x0)]  public short node_a_idx;
    [FieldOffset(0x2)]  public short node_b_idx;
    [FieldOffset(0x4)]  public short anchor_idx;
    [FieldOffset(0xC)]  public byte  activated_by;
    [FieldOffset(0xD)]  public byte  point_count;
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

[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x18)]
public struct SphereGridLinkPoint {
    [FieldOffset(0x0)]  public float x;
    [FieldOffset(0x4)]  public float y;
    [FieldOffset(0x8)]  public float offset_x1;
    [FieldOffset(0xC)]  public float offset_y2;
    [FieldOffset(0x10)] public float offset_x2;
    [FieldOffset(0x14)] public float offset_y1;

    //TODO: Document why this uses `Unsafe`.
    public Vector2 pos {
        get => Unsafe.As<float, Vector2>(ref x);
        set => Unsafe.As<float, Vector2>(ref x) = value;
    }
    public Vector2 offset_1 => new(offset_x1, offset_y1);
    public Vector2 offset_2 => new(offset_x2, offset_y2);
}

[Flags]
public enum SphereGridNodeProperties : byte {
    NONE        = 0,
    CAN_TARGET  = 1 << 0,
    HIGHLIGHTED = 1 << 1,
}

public static partial class FhEnumExt {
    public static bool can_target    (this SphereGridNodeProperties flags) => flags.HasFlag(SphereGridNodeProperties.CAN_TARGET);
    public static bool is_highlighted(this SphereGridNodeProperties flags) => flags.HasFlag(SphereGridNodeProperties.HIGHLIGHTED);
}

[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x28)]
public unsafe struct SphereGridNode {
    [InlineArray(5)]
    public struct LinkPtrArray {
        private uint _ptr;
    }

    [FieldOffset(0x0)]  public  short                    x;
    [FieldOffset(0x2)]  public  short                    y;
    [FieldOffset(0x6)]  private short                    _node_type;
    [FieldOffset(0xC)]  public  LinkPtrArray             link_ptrs;
    [FieldOffset(0x21)] public  byte                     activated_by;
    [FieldOffset(0x22)] public  SphereGridNodeProperties properties;
    [FieldOffset(0x24)] public  byte                     move_cost; // Only nonzero when moving

    public NodeType node_type {
        get => _node_type == -1 ? NodeType.NULL : (NodeType)_node_type;
        set => _node_type = (short)value;
    }
    public SphereGridNodeTypeInfo type_info => Globals.SphereGrid.lpamng->node_type_infos[node_type.normalize()];

    public Vector2 pos => new(x, y);
    public Vector2 size => type_info.size;

    public SphereGridLink* get_link(int idx) {
        return (SphereGridLink*)link_ptrs[idx];
    }

    public int link_count {
        get {
            int count = 0;

            foreach (uint ptr in link_ptrs) {
                if ((SphereGridLink*)ptr is not null) {
                    count++;
                }
            }

            return count;
        }
    }

    /// <summary>Get the indices of nodes connected to this node by at least one link.</summary>
    /// <param name="self_idx">
    ///     The index of the node this is called on.<br/>
    ///     If <c>null</c>, attempts to search for it in <see cref="Globals.SphereGrid.lpamng"/>.
    /// </param>
    /// <returns>A HashSet of the neighbouring nodes.</returns>
    public HashSet<short> get_neighbour_indices(short? self_idx) {
        if (self_idx is null && Globals.SphereGrid.lpamng->get_node_idx(this, out short? node_idx)) {
            self_idx ??= node_idx;
        }

        HashSet<short> set = [];

        foreach (uint ptr in link_ptrs) {
            SphereGridLink* link = (SphereGridLink*)ptr;

            short other_idx = link->node_a_idx != self_idx ? link->node_a_idx : link->node_b_idx;
            set.Add(other_idx);
        }

        return set;
    }
}

[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x30)]
public unsafe struct SphereGridNodeTypeInfo {
    [FieldOffset(0xC)] public short width;
    [FieldOffset(0xE)] public short height;

    public Vector2 size => new Vector2(width, height) * new Vector2(Globals.SphereGrid.lpamng->current_zoom);
}

[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x50)]
public unsafe struct SphereGridChrInfo {
    [FieldOffset(0x0)]  public Vector4 pos;
    [FieldOffset(0x10)] public Vector4 label_pos;
    [FieldOffset(0x2C)] public byte*   chr_name;
    [FieldOffset(0x30)] public short   name_width; // min 32
    [FieldOffset(0x3C)] public float   pos_circle_radius;
    [FieldOffset(0x44)] public short   current_node_idx;
}

public enum NodeType : byte {
    LOCK_3 = 0x00,

    EMPTY_NODE = 0x01,

    STRENGTH_1 = 0x02,
    STRENGTH_2 = 0x03,
    STRENGTH_3 = 0x04,
    STRENGTH_4 = 0x05,

    DEFENSE_1 = 0x06,
    DEFENSE_2 = 0x07,
    DEFENSE_3 = 0x08,
    DEFENSE_4 = 0x09,

    MAGIC_1 = 0x0A,
    MAGIC_2 = 0x0B,
    MAGIC_3 = 0x0C,
    MAGIC_4 = 0x0D,

    MAGIC_DEFENSE_1 = 0x0E,
    MAGIC_DEFENSE_2 = 0x0F,
    MAGIC_DEFENSE_3 = 0x10,
    MAGIC_DEFENSE_4 = 0x11,

    AGILITY_1 = 0x12,
    AGILITY_2 = 0x13,
    AGILITY_3 = 0x14,
    AGILITY_4 = 0x15,

    LUCK_1 = 0x16,
    LUCK_2 = 0x17,
    LUCK_3 = 0x18,
    LUCK_4 = 0x19,

    EVASION_1 = 0x1A,
    EVASION_2 = 0x1B,
    EVASION_3 = 0x1C,
    EVASION_4 = 0x1D,

    ACCURACY_1 = 0x1E,
    ACCURACY_2 = 0x1F,
    ACCURACY_3 = 0x20,
    ACCURACY_4 = 0x21,

    HP_200 = 0x22,
    HP_300 = 0x23,

    MP_40 = 0x24,
    MP_20 = 0x25,
    MP_10 = 0x26,

    LOCK_1 = 0x27,
    LOCK_2 = 0x28,
    // LOCK_3 = 0x00,
    LOCK_4 = 0x29,

    DELAY_ATTACK   = 0x2A,
    DELAY_BUSTER   = 0x2B,
    SLEEP_ATTACK   = 0x2C,
    SILENCE_ATTACK = 0x2D,
    DARK_ATTACK    = 0x2E,
    ZOMBIE_ATTACK  = 0x2F,
    SLEEP_BUSTER   = 0x30,
    SILENCE_BUSTER = 0x31,
    DARK_BUSTER    = 0x32,
    TRIPLE_FOUL    = 0x33,
    POWER_BREAK    = 0x34,
    MAGIC_BREAK    = 0x35,
    ARMOR_BREAK    = 0x36,
    MENTAL_BREAK   = 0x37,
    MUG            = 0x38,
    QUICK_HIT      = 0x39,

    STEAL        = 0x3A,
    USE          = 0x3B,
    FLEE         = 0x3C,
    PRAY         = 0x3D,
    CHEER        = 0x3E,
    FOCUS        = 0x3F,
    REFLEX       = 0x40,
    AIM          = 0x41,
    LUCK         = 0x42,
    JINX         = 0x43,
    LANCET       = 0x44,
    GUARD        = 0x45,
    SENTINEL     = 0x46,
    SPARE_CHANGE = 0x47,
    THREATEN     = 0x48,
    PROVOKE      = 0x49,
    ENTRUST      = 0x4A,
    COPYCAT      = 0x4B,
    DOUBLECAST   = 0x4C,
    BRIBE        = 0x4D,

    CURE      = 0x4E,
    CURA      = 0x4F,
    CURAGA    = 0x50,
    NUL_FROST = 0x51,
    NUL_BLAZE = 0x52,
    NUL_SHOCK = 0x53,
    NUL_TIDE  = 0x54,
    SCAN      = 0x55,
    ESUNA     = 0x56,
    LIFE      = 0x57,
    FULL_LIFE = 0x58,
    HASTE     = 0x59,
    HASTEGA   = 0x5A,
    SLOW      = 0x5B,
    SLOWGA    = 0x5C,
    SHELL     = 0x5D,
    PROTECT   = 0x5E,
    REFLECT   = 0x5F,
    DISPEL    = 0x60,
    REGEN     = 0x61,
    HOLY      = 0x62,
    AUTO_LIFE = 0x63,

    BLIZZARD = 0x64,
    FIRE     = 0x65,
    THUNDER  = 0x66,
    WATER    = 0x67,
    FIRA     = 0x68,
    BLIZZARA = 0x69,
    THUNDARA = 0x6A,
    WATERA   = 0x6B,
    FIRAGA   = 0x6C,
    BLIZZAGA = 0x6D,
    THUNDAGA = 0x6E,
    WATERGA  = 0x6F,
    BIO      = 0x70,
    DEMI     = 0x71,
    DEATH    = 0x72,
    DRAIN    = 0x73,
    OSMOSE   = 0x74,
    FLARE    = 0x75,
    ULTIMA   = 0x76,

    PILFER_GIL      = 0x77,
    FULL_BREAK      = 0x78,
    EXTRACT_POWER   = 0x79,
    EXTRACT_MANA    = 0x7A,
    EXTRACT_SPEED   = 0x7B,
    EXTRACT_ABILITY = 0x7C,

    NAB_GIL       = 0x7D,
    QUICK_POCKETS = 0x7E,

    NULL = 0xFF,
}

public static partial class FhEnumExt {
    public static bool is_lock_node(this NodeType node_type) {
        return node_type is NodeType.LOCK_1
                         or NodeType.LOCK_2
                         or NodeType.LOCK_3
                         or NodeType.LOCK_4;
    }

    public static bool is_attribute_node(this NodeType node_type) {
        return node_type is >= NodeType.STRENGTH_1 and <= NodeType.MP_10;
    }

    public static bool is_skill_node(this NodeType node_type) {
        return node_type is >= NodeType.DELAY_ATTACK and <= NodeType.QUICK_HIT
                         or >= NodeType.FULL_BREAK and <= NodeType.NAB_GIL;
    }

    public static bool is_special_node(this NodeType node_type) {
        return node_type is >= NodeType.STEAL and <= NodeType.BRIBE
                         or NodeType.PILFER_GIL
                         or NodeType.QUICK_POCKETS;
    }

    public static bool is_white_magic(this NodeType node_type) {
        return node_type is >= NodeType.CURE and <= NodeType.AUTO_LIFE;
    }

    public static bool is_black_magic(this NodeType node_type) {
        return node_type is >= NodeType.BLIZZARD and <= NodeType.ULTIMA;
    }

    public static bool is_ability_node(this NodeType node_type) {
        return node_type.is_skill_node()
            || node_type.is_special_node()
            || node_type.is_white_magic()
            || node_type.is_black_magic();
    }

    public static int normalize(this NodeType node_type) {
        if (node_type == NodeType.NULL) return -1;
        if ((int)node_type > (int)NodeType.QUICK_POCKETS) return 0;
        return (int)node_type;
    }
}
