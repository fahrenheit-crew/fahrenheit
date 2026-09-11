// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Atel;

[StructLayout(LayoutKind.Sequential, Size = 0x4C)]
public unsafe struct AtelWorkThread {
    public  InlineArray4<int>     script_offset_stack;
    public  InlineArray4<short>   script_idx_stack;
    public  byte*                 pc;
    private byte                  __0x1C;
    public  byte                  stack_depth;
    private byte                  __0x1E;
    public  byte                  wait_state;
    public  int                   reg_x;
    public  int                   reg_y;
    public  float                 reg_a;
    public  AtelWorkThreadStorage thread_local_storage;
    public  ushort                ctrl_idx;
    private ushort                __0x42;
    public  void*                 motion;
    public  void*                 rotation;
}

[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x14)]
public unsafe struct AtelWorkThreadStorage {
    private static void throw_if_var_oversized<T>(int offset, bool is_read) where T : unmanaged {
        int oversized_by = offset + sizeof(T) - sizeof(AtelWorkThreadStorage);

        if (oversized_by > 0) {
            throw new ArgumentOutOfRangeException(
                paramName: nameof(offset),
                message:   $"Cannot {(is_read ? "read" : "write")} value of type {typeof(T).FullName} from AtelWorkThread storage at offset {offset}; the value is {oversized_by} bytes too large."
            );
        }
    }

    public T get_value<T>(int offset) where T : unmanaged {
        throw_if_var_oversized<T>(offset, is_read: true);

        fixed (AtelWorkThreadStorage* storage = &this) {
            return *(T*)((nint)storage + offset);
        }
    }

    public void set_value<T>(int offset, T value) where T : unmanaged {
        throw_if_var_oversized<T>(offset, is_read: false);

        fixed (AtelWorkThreadStorage* storage = &this) {
            *(T*)((nint)storage + offset) = value;
        }
    }
}
