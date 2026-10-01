// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

[StructLayout(LayoutKind.Sequential, Size = 0x6C)]
[DebuggerDisplay("{name}")]
public unsafe struct DynGeoDataSegment {
    public uint  _0x00;
    public uint  count_vertices;
    public uint  count_indices;
    public byte* ptr_buffer_vertex;
    public byte* ptr_buffer_normal;
    public byte* ptr_buffer_color;
    public byte* ptr_buffer_uv;
    public byte* ptr_buffer_index;
    public byte* ptr_name;
    public uint  _0x24;
    public uint  _0x28;
    public uint  _0x2C;
    public uint  _0x30;
    public uint  _0x34;
    public uint  _0x38;
    public uint  _0x3C;
    public uint  _0x40;
    public uint  _0x44;
    public uint  _0x48;
    public uint  _0x4C;
    public uint  _0x50;
    public uint  _0x54;
    public uint  _0x58;
    public uint  _0x5C;
    public uint  _0x60;
    public uint  _0x64;
    public uint  _0x68;

    public string? name => Marshal.PtrToStringAnsi((nint)ptr_name);
}

[StructLayout(LayoutKind.Sequential, Size = 0xD0)]
public unsafe struct DynGeoData {
    public Matrix4x4          _0x00;
    public Matrix4x4          _0x40;
    public uint               _0x80;
    public uint               _0x84;
    public float              _0x88;
    public float              _0x8C;
    public uint               count_segments;
    public DynGeoDataSegment* ptr_segments;
    public float              _0x98;
    public float              _0x9C;
    public float              _0xA0;
    public float              _0xA4;
    public float              _0xA8;
    public float              _0xAC;
    public uint               _0xB0;
    public uint               _0xB4;
    public uint               _0xB8;
    public uint               _0xBC;
    public ushort             _0xC0;
    public ushort             _0xC2;
    public ushort             _0xC4;
    public ushort             _0xC6;
    public uint               _0xC8;
    public uint               _0xCC;
}

[StructLayout(LayoutKind.Sequential, Size = 0xD0)]
public unsafe struct ClassDynamicGeometry {
    public uint        ptr_vftable;
    public uint        _0x04;
    public uint        _0x08;
    public uint        ptr_PMeshInstance;
    public uint        _0x10;
    public uint        _0x14;
    public uint        _0x18;
    public uint        _0x1C;
    public uint        _0x20;
    public DynGeoData* _0x24;
    public uint        _0x28;
    public uint        _0x2C;
    public uint        _0x30;
    public uint        _0x34;
    public uint        _0x38;
    public uint        flags;
    public uint        _0x40;
    public uint        _0x44;
    public uint        _0x48;
    public uint        _0x4C;
    public byte        _0x50;
    public byte        type;
    public ushort      _0x52;
    public uint        _0x54;
    public uint        _0x58;
    public uint        _0x5C;
    public uint        _0x60;
    public uint        _0x64;
    public uint        _0x68;
    public uint        _0x6C;
    public uint        _0x70;
    public uint        _0x74;
    public uint        _0x78;
    public uint        _0x7C;
    public uint        _0x80;
    public uint        _0x84;
    public uint        _0x88;
    public uint        _0x8C;
    public uint        _0x90;
    public uint        _0x94;
    public uint        _0x98;
    public uint        _0x9C;
    public uint        _0xA0;
    public uint        _0xA4;
    public uint        _0xA8;
    public uint        _0xAC;
    public uint        _0xB0;
    public uint        _0xB4;
    public uint        _0xB8;
    public uint        _0xBC;
    public uint        _0xC0;
    public uint        _0xC4;
    public uint        _0xC8;
    public uint        _0xCC;
}

[StructLayout(LayoutKind.Sequential, Size = 0xF8)]
public unsafe struct VFXDynamicGeometry {
    public ClassDynamicGeometry base_ClassDynamicGeometry;

    public uint    _0xD0;
    public uint    _0xD4;
    public uint    _0xD8;
    public float   _0xDC;
    public float   _0xE0;
    public float   _0xE4;
    public Vector4 _0xE8;
}

[StructLayout(LayoutKind.Sequential, Size = 0x50)]
public unsafe struct ClassVFXRenderDataTable {
    public uint                _0x00;
    public uint                _0x04;
    public uint                _0x08;
    public uint                _0x0C;
    public uint                _0x10;
    public uint                _0x14;
    public uint                _0x18;
    public uint                _0x1C;
    public uint                last_instance;
    public uint                last_data;
    public VFXDynamicGeometry* ptr_last_vfxdg;        // [3000]
    public DynGeoData*         ptr_pool_data;
    public VFXDynamicGeometry* ptr_pool_vfxdg;
    public byte*               ptr_pool_vfxdg_rent;   // [3000] - 1 at a position indicates that VFXdg is in use
    public uint                count_vfxdg;
    public DynGeoDataSegment*  ptr_pool_segment;      // [4000]
    public byte*               ptr_pool_name;
    public byte*               ptr_pool_segment_rent; // [4000] - 1 at any position indicates that segment is in use
    public uint                count_segments;
    public uint                _0x4C;

    public Span<VFXDynamicGeometry> vfxdg   => new(ptr_pool_vfxdg,   (int) count_vfxdg);
    public Span<DynGeoDataSegment>  segment => new(ptr_pool_segment, (int) count_segments);
}
