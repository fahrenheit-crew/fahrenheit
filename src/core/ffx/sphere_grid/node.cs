// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX.SphereGrid;

[Flags]
public enum SphereGridNodeProperties : byte {
    NONE        = 0,
    CAN_TARGET  = 1 << 0,
    HIGHLIGHTED = 1 << 1,
}

public static partial class FhEnumExt {
    extension(SphereGridNodeProperties flags) {
        public bool can_target {
            get { return flags.HasFlag(SphereGridNodeProperties.CAN_TARGET); }
            set { if (value) flags |= (SphereGridNodeProperties.CAN_TARGET); else flags &= ~(SphereGridNodeProperties.CAN_TARGET); }
        }

        public bool is_highlighted {
            get { return flags.HasFlag(SphereGridNodeProperties.HIGHLIGHTED); }
            set { if (value) flags |= (SphereGridNodeProperties.HIGHLIGHTED); else flags &= ~(SphereGridNodeProperties.HIGHLIGHTED); }
        }
    }
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
    [FieldOffset(0x24)] public  short                    move_cost; // Only nonzero when moving

    [FieldOffset(0x26)] public  ushort                   __0x26;

    public NodeType node_type {
        get => _node_type == -1 ? NodeType.NULL : (NodeType)_node_type;
        set => _node_type = (short)value;
    }
    public SphereGridNodeTypeUiInfo type_info => Globals.SphereGrid.lpamng->node_type_infos[node_type.normalize()];

    public Vector2 pos => new(x, y);
    public Vector2 size => type_info.size;

    public SphereGridLink* get_link(int idx) {
        return (SphereGridLink*)link_ptrs[idx];
    }

    public int get_link_count() {
        int count = 0;

        foreach (uint ptr in link_ptrs) {
            if (ptr != 0) count++;
        }

        return count;
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
            if (ptr == 0) continue;

            SphereGridLink* link = (SphereGridLink*)ptr;

            short other_idx = link->node_a_idx != self_idx ? link->node_a_idx : link->node_b_idx;
            set.Add(other_idx);
        }

        return set;
    }
}
