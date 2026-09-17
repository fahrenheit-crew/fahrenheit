// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX.SphereGrid;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct SphereGridPlyInfo {
    public  Vector4 pos;
    public  Vector4 label_pos;
    public  uint    a;
    public  uint    b;
    public  uint    c;
    public  byte*   chr_name;
    public  short   name_width; // min 32
    public  short   __0x32;
    private ushort  __0x34_pad;
    private ushort  __0x36_pad;
    public  short   __0x38;
    private ushort  __0x3A_pad;
    public  float   pos_circle_radius;
    public  uint    __0x40;
    public  short   current_node_idx;
    public  short   __0x46;
    public  short   __0x48;
    public  short   __0x4A;
    public  short   __0x4C;
    public  byte    __0x4E;
    private byte    __0x4F_pad;
}
