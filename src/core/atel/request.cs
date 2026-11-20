// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Atel;

[ExtendedLayout(ExtendedLayoutKind.CStruct)]
public struct AtelRequest {
    public  ushort worker_idx;
    private ushort __0x2;
    public  ushort slot;
    public  ushort entry_point_idx;
}
