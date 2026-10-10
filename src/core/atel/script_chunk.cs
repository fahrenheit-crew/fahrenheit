// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Atel;

public struct MapEntrance {
    public short unknown00;
    public short unknown02;
    public short unknown04;
    public short unknown06;
    public float rotation;
    public float x;
    public float y;
    public float z;
    public int   unknown18;
    public int   unknown1C;

    public readonly Vector3 pos => new(x, y, z);
}

[StructLayout(LayoutKind.Sequential, Size = 0x38)]
public unsafe struct AtelScriptChunk {
    public  uint   code_length;
    public  uint   map_start;
    public  uint   offset_author;
    public  uint   offset_name;
    public  uint   offset_jumps_end;
    private ushort __0x14;
    private ushort __0x16;
    public  ushort main_script_idx;
    private ushort __0x1A;
    private ushort __0x1C;
    public  ushort zone_bytes;
    public  uint   offset_event_data;
    private uint   __0x24;
    public  uint   offset_area;
    public  uint   offset_other;
    public  uint   offset_code;
    public  ushort script_num;
    public  ushort script_num_except_subroutines;

    public ushort* script_header_offsets { get { fixed (AtelScriptChunk* address = &this) { return (ushort*)(address + 1); } } }

    public readonly ReadOnlySpan<MapEntrance> map_entrances {
        get {
            fixed (AtelScriptChunk* address = &this) {
                return new((MapEntrance*)((nint)address + map_start), (int)(offset_author - map_start) / 0x20);
            }
        }
    }
}
