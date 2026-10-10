// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Atel;

public enum AtelSignalState : byte {
    IGNORE       = 0x0,
    RUN          = 0x1,
    COMPLETE     = 0x2,
    ACKNOWLEDGED = 0x3,
}

[StructLayout(LayoutKind.Sequential, Size = 0x16)]
public unsafe struct AtelSignal {

    [InlineArray(1)]
    public struct AtelSignalFlags {
        private byte e0;

        public byte priority       { readonly get { return this[0].get_bits(0, 4); } set { this[0].set_bits(0, 4, value); } }
        public byte process_status { readonly get { return this[0].get_bits(4, 4); } set { this[0].set_bits(4, 4, value); } }
    }

    public AtelSignal* ptr_next;
    public AtelSignal* ptr_prev;

    public ushort entry_point;
    public ushort idx_work_src;
    public ushort idx_work_tgt;

    public AtelSignalFlags flags;
    public AtelSignalState state;

    private short __0x10;
    private short __0x12;

    public ushort idx_work_ctrl;
}
