// SPDX-License-Identifier: MIT

namespace Fahrenheit.FFX;

[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 0x98)]
public unsafe struct TkWindow {
    [FieldOffset(0x00)] public uint  __0x00;
    [FieldOffset(0x04)] public uint  __0x04;
    [FieldOffset(0x08)] public delegate* unmanaged[Cdecl]<TkWindow*, void> init_function;
    [FieldOffset(0x0c)] public delegate* unmanaged[Cdecl]<TkWindow*, void> state_function;
    [FieldOffset(0x10)] public delegate* unmanaged[Cdecl]<TkWindow*, void> render_function;
    [FieldOffset(0x14)] public delegate* unmanaged[Cdecl]<TkWindow*, bool> destroy_condition_function;
    [FieldOffset(0x18)] public delegate* unmanaged[Cdecl]<TkWindow*, void> destructor_function;
    [FieldOffset(0x1c)] public void* unknown_function;

    [FieldOffset(0x28)] public uint  current_state;
    [FieldOffset(0x2c)] public short __0x2c;

    [FieldOffset(0x30)] public short num_items;
    [FieldOffset(0x32)] public short visible_item_offset;
    [FieldOffset(0x34)] public short scroll_offset;
    [FieldOffset(0x36)] public byte  menu_group;
    [FieldOffset(0x37)] public byte  __0x37;
    [FieldOffset(0x38)] public short __0x38;
    [FieldOffset(0x3a)] public short max_visible_items;

    [FieldOffset(0x3e)] public byte  render_priority;
    [FieldOffset(0x3f)] public byte  state_priority;
    [FieldOffset(0x40)] public bool  is_active;
    [FieldOffset(0x41)] public bool  should_destroy;
    [FieldOffset(0x42)] public byte  __0x42;
    [FieldOffset(0x43)] public byte  __0x43;
    [FieldOffset(0x44)] public byte  __0x44;
    [FieldOffset(0x45)] public sbyte exit_value;
    [FieldOffset(0x46)] public short scroll_delta;
    [FieldOffset(0x48)] public short selected_index;

    [FieldOffset(0x58)] public fixed uint data[16]; // menu-specific
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct TkMenu {
    public delegate* unmanaged[Cdecl]<TkMenu*, int, void> ctrl;
    public delegate* unmanaged[Cdecl]<TkMenu*,      void> draw;
    public delegate* unmanaged[Cdecl]<TkMenu*, int, void> init;
    public nint __0x0c;
    public nint sleep;
    public delegate* unmanaged[Cdecl]<TkMenu*, void> deactivate;
    public byte __0x18;
    public byte __0x19;
    public byte __0x1a;
    public byte __0x1b;
    public int  state;
}
