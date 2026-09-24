// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX.SphereGrid;

public enum SphereGridMenuScrollDirection : byte {
    NONE = 0,
    UP   = 1,
    DOWN = 2,
}

public enum SphereGridMenuId : int {
    PREVIEW_STATS     = 1,
    PREVIEW_SPECIAL   = 2,
    PREVIEW_SKILLS    = 3,
    PREVIEW_WHT_MAGIC = 4,
    PREVIEW_BLK_MAGIC = 5,
    PREVIEW_PROMPT    = 6,

    ACTION_PROMPT     = 7,
    USE_ITEM          = 8,

    MOVE_CONFIRM_PROMPT =  9,
    QUIT_CONFIRM_PROMPT = 10,
    YES_NO_PROMPT       = 11,
}

[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0xC)]
public unsafe struct SphereGridMenuEntry {
    [FieldOffset(0x0)] public byte* text;
    [FieldOffset(0x4)] public byte  __0x4;
    [FieldOffset(0x8)] public int   data;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct SphereGridMenu {
    [InlineArray(64)]
    public struct EntryList {
        private SphereGridMenuEntry _data;
    }

    public short target_x;
    public short target_y;
    public short target_width;
    public short target_height;

    public short x;
    public short y;
    public short width;
    public short height;

    public short target__0x14;
    public short target_row_count;
    public short __0x14;
    public short row_count;

    public short selected_idx;
    public short scrolled_amount;

    private byte __0x1C;
    private byte __0x1D;

    public short entry_count;

    public short __0x20;

    public byte column_count;
    public bool is_visible;

    public byte __0x24;

    public bool render_cursor;
    public SphereGridMenuScrollDirection scroll_direction;
    public byte scroll_progress; //TODO: Find a better name for this
    public bool is_full;

    private byte __0x29;

    private short __0x2A;
    private short __0x2C;
    private short __0x2E;
    private short __0x30;

    private byte __0x32;
    private byte __0x33;

    public void* fn__0x34;
    public void* fn__0x38;
    public void* fn_help;

    public EntryList entries;
}

[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x2710)]
public unsafe struct SphereGridMenuList {
    [InlineArray(12)]
    public struct MenuList {
        private SphereGridMenu _data;

        public SphereGridMenu preview_stats       => this[(int)SphereGridMenuId.PREVIEW_STATS];
        public SphereGridMenu preview_special     => this[(int)SphereGridMenuId.PREVIEW_SPECIAL];
        public SphereGridMenu preview_skills      => this[(int)SphereGridMenuId.PREVIEW_SKILLS];
        public SphereGridMenu preview_white_magic => this[(int)SphereGridMenuId.PREVIEW_WHT_MAGIC];
        public SphereGridMenu preview_black_magic => this[(int)SphereGridMenuId.PREVIEW_BLK_MAGIC];
        public SphereGridMenu preview_prompt      => this[(int)SphereGridMenuId.PREVIEW_PROMPT];
        public SphereGridMenu action_prompt       => this[(int)SphereGridMenuId.ACTION_PROMPT];
        public SphereGridMenu use_item            => this[(int)SphereGridMenuId.USE_ITEM];
        public SphereGridMenu move_confirm_prompt => this[(int)SphereGridMenuId.MOVE_CONFIRM_PROMPT];
        public SphereGridMenu quit_confirm_prompt => this[(int)SphereGridMenuId.QUIT_CONFIRM_PROMPT];
        public SphereGridMenu quit_prompt         => this[(int)SphereGridMenuId.YES_NO_PROMPT];
    }

    [FieldOffset(0x0)] public MenuList menus;

    [FieldOffset(0x2700)] public int idx;

    [FieldOffset(0x2704)] public void* fn_ctrl;
}
