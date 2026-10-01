// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX;

public enum VpaTriCollisionGroup {
    Pass        = 0,
    BlockAll    = 1,
    BlockNPC    = 2,
    BlockPlayer = 14,
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct VpaTri {
    public  InlineArray3<short> vertex_indices;
    public  InlineArray3<short> neighbour_indices;
    private int                 data;

    //public readonly VpaVertex* vertex_by_index   (int idx) => Globals.Map.vertices + vertex_indices   [idx];
    //public readonly VpaTri*    neighbour_by_index(int idx) => Globals.Map.tris     + neighbour_indices[idx];

    //public readonly VpaVertex* vert_a { get { return Globals.Map.vertices + vertex_indices[0]; } }
    //public readonly VpaVertex* vert_b { get { return Globals.Map.vertices + vertex_indices[1]; } }
    //public readonly VpaVertex* vert_c { get { return Globals.Map.vertices + vertex_indices[2]; } }

    //public readonly VpaTri* neighbour_a { get { return Globals.Map.tris + neighbour_indices[0]; } }
    //public readonly VpaTri* neighbour_b { get { return Globals.Map.tris + neighbour_indices[1]; } }
    //public readonly VpaTri* neighbour_c { get { return Globals.Map.tris + neighbour_indices[2]; } }

    public VpaTriCollisionGroup collision_group { readonly get { return (VpaTriCollisionGroup)data.get_bits(0, 7); } set { data.set_bits(0,  7,  (int)value); } }
    public int                  battle_zone     { readonly get { return data.get_bits(7,  2);                      } set { data.set_bits(7,  2,  value);      } }
    public int                  unk1            { readonly get { return data.get_bits(9,  2);                      } set { data.set_bits(9,  2,  value);      } }
    public int                  location        { readonly get { return data.get_bits(11, 2);                      } set { data.set_bits(11, 2,  value);      } }
    public int                  unk2            { readonly get { return data.get_bits(13, 2);                      } set { data.set_bits(13, 2,  value);      } }
    public int                  sound_type      { readonly get { return data.get_bits(15, 2);                      } set { data.set_bits(15, 2,  value);      } }
    public int                  upper           { readonly get { return data.get_bits(17, 15);                     } set { data.set_bits(17, 15, value);      } }
}

public struct VpaVertex {
    public  short x;
    public  short y;
    public  short z;
    private short unused;
}

[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x20)]
public unsafe struct VpaNavMesh {
    [FieldOffset(0x0A)] public ushort vertex_count;
    [FieldOffset(0x0C)] public float  scale;
    [FieldOffset(0x18)] public uint   vertices_offset;
    [FieldOffset(0x1C)] public uint   tri_info_offset;
}
