// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Atel;

[ExtendedLayout(ExtendedLayoutKind.CStruct)]
public struct AtelWorkThread {
    public InlineArray4<int>   script_offset_stack;
    public InlineArray4<short> script_idx_stack;

    public byte* pc;

    private byte __0x1C;
    public  byte stack_depth;
    private byte __0x1E;
    public  byte wait_state;

    public int   reg_x;
    public int   reg_y;
    public float reg_a;

    public AtelWorkThreadStorage thread_local_storage;

    public  ushort ctrl_idx;
    private ushort __0x42;

    public void* motion;
    public void* rotation;
}

[InlineArray(20)]
public struct AtelWorkThreadStorage {
    private byte _e0;
}
