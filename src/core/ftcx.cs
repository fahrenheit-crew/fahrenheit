// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

/* [fkelava 28/10/25 16:26]
 * FTCX is the original font format of the games. While seemingly unused in the Remaster,
 * which has a separate font, the files are still loaded by the games for an unknown purpose.
 */

[ExtendedLayout(ExtendedLayoutKind.CStruct)]
public struct FTCX_TILE_INFO {
    public uint   tile_count;
    public ushort tile_width;
    public ushort tile_height;
    public uint   padding1;
    public uint   padding2;
}

[ExtendedLayout(ExtendedLayoutKind.CStruct)]
public struct FTCX_IMAGE_INFO {
    public uint   data_ptr;
    public uint   data_size;
    public ushort data_width;
    public ushort data_height;
    public uint   padding;
};

[ExtendedLayout(ExtendedLayoutKind.CStruct)]
public struct FTCX_WIDTH_INFO {
    public uint data_ptr;
    public uint data_size;
    public uint padding1;
    public uint padding2;
};

[ExtendedLayout(ExtendedLayoutKind.CStruct)]
public struct FTCX {
    public uint            magic;
    public ushort          reserved1; // seems constant (200), must be > 199
    public ushort          reserved2; // seems constant (1556)
    public ushort          type;      // 0-6
    public ushort          reserved3;
    public uint            reserved4;
    public FTCX_TILE_INFO  tile_info;
    public FTCX_IMAGE_INFO image_info;
    public FTCX_WIDTH_INFO width_info;
}
