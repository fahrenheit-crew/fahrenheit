// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

/// <summary>
///     An opaque structure used by the game's allocator.
/// </summary>
[ExtendedLayout(ExtendedLayoutKind.CStruct)]
internal struct __ALLOC_STRUCT {
    public CRITICAL_SECTION crit_sec;
    public __ALLOC_DATA     data;
}

/// <summary>
///     An opaque structure used by the game's allocator.
/// </summary>
[ExtendedLayout(ExtendedLayoutKind.CStruct)]
internal struct __ALLOC_DATA {
    public uint size;
    public uint __0x04;
    public uint __0x08;
    public uint addr_primary_pool;
    public uint align;
    public uint addr_last_reserve;
}
