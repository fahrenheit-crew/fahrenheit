// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

using Fahrenheit.FFX;
using Fahrenheit.FFX.Ids;
using Fahrenheit.FFX.SphereGrid;

using FhXCall = Fahrenheit.FFX.FhCall;

using static Fahrenheit.FFX.Globals.SphereGrid;

namespace Fahrenheit.Runtime.Impl;

[FhLoad(FhGameId.FFX)]
public unsafe class SphereGridModule : FhModule {
    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        return FhXCall.AbmapState_ChoosingMoveTarget.hook(this, h_state_choosing_move_target)
            && FhXCall.AbmapState_MovingToTarget.hook(this, h_state_moving)
            && FhXCall.AbmapState_ChoosingActivationTarget.hook(this, h_state_choosing_activation_target)
            && FhXCall.AbmapState_Warping.hook(this, h_state_warping)
            && FhXCall.AbmapCalcMoveCosts.hook(this, h_calc_move_costs)
            && FhXCall.AbmapCalcMoveCost.hook(this, h_calc_move_cost)
            && FhXCall.AbmapInitChoosingMoveTarget.hook(this, h_init_choose_move_target)
            && FhXCall.eiAbmStart.hook(this, h_open_sphere_grid);
    }

    private void* get_fnptr(uint address) {
        return (void*)(FhEnvironment.BaseAddr + address);
    }

    public void limit_all_link_flags(SphereGridLinkProperties props) {
        for (int link_idx = 0; link_idx < lpamng->link_count; link_idx++) {
            SphereGridLink* link = &lpamng->links[link_idx];
            link->flags &= props;
        }
    }

    public void limit_all_node_flags(SphereGridNodeProperties props) {
        for (int node_idx = 0; node_idx < lpamng->node_count; node_idx++) {
            SphereGridNode* node = &lpamng->nodes[node_idx];
            if (node->node_type == NodeType.NULL) continue;

            node->flags &= props;
        }
    }


    public int h_calc_move_cost(short target_node, int running_total_cost) {
        int target_cost   = running_total_cost + *move_prev_link_cost;
        int existing_cost = lpamng->nodes[target_node].move_cost;

        if (target_cost > *slv_available_for_move // More expensive than we can spend
            || (existing_cost != -1 && target_cost >= existing_cost) // More expensive than a previous option
        ) {
            return 1;
        }

        lpamng->nodes[target_node].move_cost = (byte)target_cost;

        for (int i = 0; i < 5; i++) {
            SphereGridLink* link = (SphereGridLink*)lpamng->nodes[target_node].link_ptrs[i];
            if (link == null) {
                return 0;
            }

            if (link->flags.can_move_through || link->flags.connected) {
                continue;
            }

            *move_prev_link_cost = (link->activated_by & *move_ply_mask) != 0 ? 1 : 4;

            short other_node_idx =
                link->node_a_idx != target_node
                    ? link->node_a_idx
                    : link->node_b_idx;

            link->flags.can_move_through = true;
            link->flags.flag2 = true;

            if (h_calc_move_cost(other_node_idx, target_cost) != 0)
                link->flags.flag2 = false;
            else
                link->flags.can_move_through = false;
        }

        return 0;
    }

    public void h_calc_move_costs(short target_node, short slv, byte ply_id) {
        // Reset everything first
        for (int node_idx = 0; node_idx < lpamng->node_count; node_idx++) {
            if (lpamng->nodes[node_idx].node_type != NodeType.NULL) {
                lpamng->nodes[node_idx].move_cost = -1;
            }
        }

        for (int link_idx = 0; link_idx < lpamng->link_count; link_idx++) {
            lpamng->links[link_idx].flags = SphereGridLinkProperties.NONE;
        }

        FhXCall.AbmapFlagConnectedLinks.fnptr!(SphereGridLinkProperties.CONNECTED);

        *slv_available_for_move = slv << 2;
        *move_ply_mask = 1 << (ply_id & 0x1F);
        *move_prev_link_cost = 0;

        h_calc_move_cost(target_node, 0);
    }

    public void h_init_choose_move_target() {
        short start_idx = lpamng->party_infos[lpamng->current_ply_id].current_node_idx;
        short slv       = Globals.save_data->ply_saves[lpamng->current_ply_id].slv_available;

        lpamng->fn_ctrl = get_fnptr(0x644EF0);
        lpamng->fn_help = get_fnptr(0x645440);

        h_calc_move_costs(start_idx, slv, lpamng->current_ply_id);

        for (int node_idx = 0; node_idx < lpamng->node_count; node_idx++) {
            SphereGridNode* node = &lpamng->nodes[node_idx];
            if (node->node_type == NodeType.NULL || node->move_cost < 0)
                continue;

            bool can_target = node->move_cost <= (slv << 2);
            const SphereGridNodeProperties MASK =
                SphereGridNodeProperties.CAN_TARGET
              | SphereGridNodeProperties.HIGHLIGHTED;

            if (can_target)
                node->flags |=  MASK;
            else
                node->flags &= ~MASK;
        }

        lpamng->__0x116A0 = 0;
        lpamng->__0x1169C = 0;

        lpamng->slv_queued = 0;

        FhXCall.AbmapInitHoming.fnptr!(start_idx, 0.25f);

        if (lpamng->fn_ctrl_backup == null) {
            lpamng->fn_ctrl_backup = lpamng->fn_ctrl;
            lpamng->fn_ctrl = get_fnptr(0x659E80);
        }

        lpamng->__0x115C3 = 1;
    }

    public void init_moving(int ply_id, short node_idx) {
        lpamng->move_next_knot_idx = lpamng->party_infos[ply_id].current_node_idx;
        lpamng->move_start_node_idx = lpamng->move_next_knot_idx;
        lpamng->move_target_node_idx = node_idx;

        lpamng->move_progress = 1f;
        lpamng->move_speed    = 0f;

        lpamng->move_ply_id = (byte)ply_id;

        if (lpamng->fn_ctrl_backup == null) {
            lpamng->fn_ctrl_backup = lpamng->fn_ctrl;
            lpamng->fn_ctrl = get_fnptr(0x659990);
        }
    }

    public void h_state_choosing_move_target() {
        FhXCall.FUN_00a58ff0.fnptr!(get_fnptr(0x645000));

        if (lpamng->__0x115CD != 0 || lpamng->fn_ctrl_backup != null) return;

        lpamng->slv_queued = (lpamng->nodes[lpamng->selected_node_idx].move_cost + 3) >> 2;

        // Confirm button
        if (lpamng->abmap_input[1].get_bit(5)) {
            limit_all_link_flags((SphereGridLinkProperties)0b11111000);

            lpamng->fn_ctrl = get_fnptr(0x648230);
            lpamng->fn_help = null;
            lpamng->__0x115C3 = 0;
            init_moving(lpamng->current_ply_id, lpamng->selected_node_idx);
            return;
        }

        // Cancel button
        if (lpamng->abmap_input[1].get_bit(6)) {
            limit_all_link_flags((SphereGridLinkProperties)0b11110000);
            FhCall.SndSepPlaySimple.fnptr!(SoundId.UI_CANCEL);

            lpamng->__0x115C3 = 0;
            lpamng->slv_queued = 0;

            FhXCall.FUN_00a59950.fnptr!();
            FhXCall.FUN_00a596d0.fnptr!(7);
        }
    }

    public void h_state_warping() {
        // Warping happens in three stages:
        //   1. Disappear
        //   2. Move to the target node
        //   3. Reappear

        byte stage = lpamng->__0x1164C;

        switch (stage) {
            case 0:

                lpamng->__0x1164D += 1;

                lpamng->__0x115C6 = (byte)(0x80 - (lpamng->__0x1164D << 7) / 0x28);
                lpamng->__0x115C6 = byte.Min(lpamng->__0x115C6, 0x80);

                if (FhUtil.get_at<short>(0x168607E) == 0) {
                    lpamng->party_infos[lpamng->move_ply_id].pos_circle_radius = 0f;
                    lpamng->__0x1164C = 1;
                    lpamng->__0x115C6 = 0;
                }

                break;

            case 1:
                SphereGridNode target_node = lpamng->nodes[lpamng->move_target_node_idx];

                Vector4 target_pos = new(target_node.x, target_node.y, 0f, 1f);

                lpamng->move_progress += 1f/12f;
                if (1f <= lpamng->move_progress){
                    lpamng->__0x1164C = 2;
                    lpamng->__0x1164D = 0;

                    FhCall.SndSepPlaySimple.fnptr!(0x80000070);

                    lpamng->cam_desired_pos = target_pos;

                    FhXCall.pppCreateHeap.fnptr!(
                        FhUtil.ptr_at<int>(0x1686034),
                        FhUtil.ptr_at<int>(0x12C1830),
                        0x7D000
                    );

                    FhXCall.FUN_00a5bad0.fnptr!(
                        FhUtil.ptr_at<int>(0x1686060),
                        1,
                        target_node.x, target_node.y,
                        0, 0, 0, 0,
                        0.5f, 0.5f, 0.5f
                    );

                    lpamng->party_infos[lpamng->move_ply_id].current_node_idx
                        = lpamng->move_target_node_idx;

                    FhXCall.FUN_00a5a990.fnptr!(lpamng->move_ply_id);
                    FhXCall.AbmapPositionPlyTag.fnptr!(lpamng->move_ply_id);

                    lpamng->__0x115C7 = 1;

                    FhXCall.FUN_00a5b030.fnptr!();
                    break;
                }

                lpamng->cam_desired_pos = Vector4.Lerp(lpamng->move_prev_node_pos, target_pos, lpamng->move_progress);

                break;

            case 2:
                lpamng->__0x1164D += 1;


                lpamng->__0x115C6 = (byte)((lpamng->__0x1164D << 7) / 0x28);
                lpamng->__0x115C6 = byte.Min(lpamng->__0x115C6, 0x80);

                if (FhUtil.get_at<short>(0x168607E) == 0) {
                    lpamng->__0x115C6 = 0x80;

                    lpamng->fn_ctrl = lpamng->fn_ctrl_backup;
                    lpamng->fn_help = lpamng->fn_help_backup;

                    lpamng->fn_ctrl_backup = null;
                    lpamng->fn_help_backup = null;
                }

                break;
        }
    }

    public void h_state_choosing_activation_target() {
        FhXCall.FUN_00a58ff0.fnptr!(get_fnptr(0x645000));

        if (lpamng->__0x115CD != 0
            || lpamng->fn_ctrl_backup != null
            || menu_list->menus[8].fn__0x34 != null
        ) {
            return;
        }

        if (lpamng->abmap_input[1].get_bit(5)) {
            SphereGridMenu* use_menu = &menu_list->menus[(int)SphereGridMenuId.USE_ITEM];

            use_menu->is_visible = false;
            FhXCall.FUN_00a59680.fnptr!(8);
            FhXCall.AbmapResetToIdle.fnptr!();

            FhXCall.AbmapTryUseItem.fnptr!(
                lpamng->current_ply_id,
                lpamng->selected_node_idx,
                use_menu->entries[use_menu->selected_idx].data & 0xFFFF
            );

            use_menu->entry_count = 0;

            FhXCall.FUN_00a45fd0.fnptr!(8, 3);

            use_menu->selected_idx = short.Clamp(
                use_menu->selected_idx,
                0,
                (short)(use_menu->entry_count - 1)
            );

            use_menu->scrolled_amount = short.Clamp(
                use_menu->scrolled_amount,
                0,
                (short)(use_menu->entry_count - use_menu->row_count)
            );

            for (int node_idx = 0; node_idx < lpamng->node_count; node_idx++) {
                SphereGridNode* node = &lpamng->nodes[node_idx];
                if (node->node_type == NodeType.NULL) continue;

                node->flags = SphereGridNodeProperties.NONE;
            }

            return;
        }

        if (lpamng->abmap_input[1].get_bit(6)) {
            FhXCall.FUN_00a598a0.fnptr!();
            FhXCall.FUN_00a596d0.fnptr!(8);

            for (int node_idx = 0; node_idx < lpamng->node_count; node_idx++) {
                SphereGridNode* node = &lpamng->nodes[node_idx];
                if (node->node_type == NodeType.NULL) continue;

                node->flags = SphereGridNodeProperties.NONE;
            }

            FhCall.SndSepPlaySimple.fnptr!(0x80000004);
            return;
        }
    }

    public void menu_add_entries(SphereGridMenu* menu, IEnumerable<(uint text, byte __0x4, int data)> entries) {
        foreach ((uint text, byte __0x4, int data) entry in entries) {
            int entry_idx = menu->entry_count;

            if (entry_idx >= 0x40) {
                throw new Exception("Attempted to add entry to a completely full SphereGridMenu.");
            }

            menu->entries[entry_idx].text  = (byte*)entry.text;
            menu->entries[entry_idx].__0x4 = entry.__0x4;
            menu->entries[entry_idx].data  = entry.data;

            menu->entry_count += 1;
            menu->is_full = menu->entry_count < (menu->column_count * menu->row_count);
        }
    }

    public void init_menu(
        SphereGridMenu* menu,
        short x, short y,
        short width, short height,
        short __0x14, short __0x20,
        byte col_count, short row_count,
        void* fn__0x34, void* fn__0x38, void* fn_help,
        bool is_visible = false, bool render_cursor = false
    ) {
        menu->target_x = menu->x = x;
        menu->target_y = menu->y = y;

        menu->target_width  = menu->width  = width;
        menu->target_height = menu->height = height;

        menu->target__0x14 = menu->__0x14 = __0x14;
        menu->__0x20 = __0x20;
        menu->__0x24 = 0;

        menu->target_row_count = menu->row_count = row_count;
        menu->column_count = col_count;

        menu->is_visible = is_visible;

        menu->scroll_direction = SphereGridMenuScrollDirection.NONE;

        menu->scrolled_amount = 0;
        menu->selected_idx    = 0;

        menu->render_cursor = is_visible && render_cursor;

        menu->entry_count = 0;
        menu->is_full = false;

        menu->fn__0x34 = fn__0x34;
        menu->fn__0x38 = fn__0x38;
        menu->fn_help  = fn_help;
    }

    public void init_menu_0() {
        SphereGridMenu* menu = &menu_list->menus[0];

        init_menu(
            menu,
            x: 48, y: 35, width: 416, height: 20,
            __0x14: 26, __0x20: 15,
            col_count: 1, row_count: 1,
            fn__0x34: null,
            fn__0x38: get_fnptr(0x64F930),
            fn_help:  get_fnptr(0x6570A0),
            is_visible: true
        );
    }

    public void init_menu_action_prompt() {
        SphereGridMenu* menu = &menu_list->menus[(int)SphereGridMenuId.ACTION_PROMPT];

        init_menu(
            menu,
            x:  48,
            y: 205,
            width:  80,
            height: 40,
            __0x14: 5,
            __0x20: (short)(is_cjk ? 18 : 10),
            col_count: 1,
            row_count: 2,
            fn__0x34: null,
            fn__0x38: get_fnptr(0x64F930),
            fn_help:  get_fnptr(0x6570A0)
        );

        // Fill it in!
        bool hira = Globals.save_data->config_hiragana;
        byte* use_text  = FhXCall.MsMenuGetText.fnptr!(9, 30, hira);
        byte* move_text = FhXCall.MsMenuGetText.fnptr!(9, 31, hira);

        menu_add_entries(menu, [
            ((uint)move_text, 0, 31),
            ((uint)use_text , 0, 30),
        ]);
    }

    public void init_menu_use_item() {
        SphereGridMenu* menu = &menu_list->menus[(int)SphereGridMenuId.USE_ITEM];

        init_menu(
            menu,
            x:  48,
            y: 205,
            width:  (short)(is_cjk ? 144 : 176),
            height: 80,
            __0x14: (short)(is_cjk ? 9 : 11),
            __0x20: 8,
            col_count: 1,
            row_count: 4,
            fn__0x34: null,
            fn__0x38: get_fnptr(0x64F630),
            fn_help:  get_fnptr(0x657040)
        );

        // Fill it in!
        FhXCall.FUN_00a45fd0.fnptr!(8, 3);
    }

    public void init_menu_preview_prompt() {
        SphereGridMenu* menu = &menu_list->menus[(int)SphereGridMenuId.PREVIEW_PROMPT];

        init_menu(
            menu,
            x:  48,
            y: 356,
            width:  (short)(is_cjk ? 128 : 176),
            height: 20,
            __0x14: (short)(is_cjk ? 8 : 11),
            __0x20: 20,
            col_count: 1,
            row_count: 1,
            fn__0x34: null,
            fn__0x38: get_fnptr(0x64F9E0),
            fn_help:  null
        );

        // Fill it in!
        bool  hira = Globals.save_data->config_hiragana;
        byte* text = FhXCall.MsMenuGetText.fnptr!(9, 35, hira);

        menu_add_entries(menu, [
            ((uint)text, 0, 35),
        ]);
    }

    public void init_menu_preview_stats() {
        SphereGridMenu* menu = &menu_list->menus[(int)SphereGridMenuId.PREVIEW_STATS];

        init_menu(
            menu,
            x: (short)(is_cjk ? 160 : 136),
            y: 276,
            width:  (short)(is_cjk ? 192 : 240),
            height: 100,
            __0x14: (short)(is_cjk ? 12 : 15),
            __0x20: 32,
            col_count: 2,
            row_count: 5,
            fn__0x34: null,
            fn__0x38: get_fnptr(0x64FA30),
            fn_help:  null
        );

        // Fill it in!
        bool  hira = Globals.save_data->config_hiragana;
        byte* text_hp  = FhXCall.MsMenuGetText.fnptr!(11, 0, hira);
        byte* text_mp  = FhXCall.MsMenuGetText.fnptr!(11, 1, hira);
        byte* text_str = FhXCall.MsMenuGetText.fnptr!(11, 7, hira);
        byte* text_agi = FhXCall.MsMenuGetText.fnptr!(11, 11, hira);
        byte* text_def = FhXCall.MsMenuGetText.fnptr!(11, 8, hira);
        byte* text_lck = FhXCall.MsMenuGetText.fnptr!(11, 12, hira);
        byte* text_mag = FhXCall.MsMenuGetText.fnptr!(11, 9, hira);
        byte* text_eva = FhXCall.MsMenuGetText.fnptr!(11, 13, hira);
        byte* text_mdf = FhXCall.MsMenuGetText.fnptr!(11, 10, hira);
        byte* text_acc = FhXCall.MsMenuGetText.fnptr!(11, 14, hira);

        menu_add_entries(menu, [
            ((uint)text_hp , 0, 0),
            ((uint)text_mp , 0, 1),
            ((uint)text_str, 0, 2),
            ((uint)text_agi, 0, 3),
            ((uint)text_def, 0, 4),
            ((uint)text_lck, 0, 5),
            ((uint)text_mag, 0, 6),
            ((uint)text_eva, 0, 7),
            ((uint)text_mdf, 0, 8),
            ((uint)text_acc, 0, 9),
        ]);
    }

    public void init_menu_preview_skills() {
        SphereGridMenu* menu = &menu_list->menus[(int)SphereGridMenuId.PREVIEW_SKILLS];

        init_menu(
            menu,
            x: (short)(is_cjk ? 95 : 72),
            y: 216,
            width:  (short)(is_cjk ? 320 : 368),
            height: 160,
            __0x14: (short)(is_cjk ? 20 : 23),
            __0x20: 32,
            col_count: 3,
            row_count: 8,
            fn__0x34: null,
            fn__0x38: get_fnptr(0x64F250),
            fn_help:  null
        );

        // Fill it in!
        FhXCall.FUN_00a459e0.fnptr!(3, 64);
    }

    public void init_menu_preview_special() {
        SphereGridMenu* menu = &menu_list->menus[(int)SphereGridMenuId.PREVIEW_SPECIAL];

        init_menu(
            menu,
            x: (short)(is_cjk ?  96 :  72),
            y: (short)(is_cjk ? 256 : 216),
            width:  (short)(is_cjk ? 320 : 368),
            height: (short)(is_cjk ? 120 : 160),
            __0x14: (short)(is_cjk ? 20 : 23),
            __0x20: 32,
            col_count:  (byte)(is_cjk ? 4 : 3),
            row_count: (short)(is_cjk ? 6 : 8),
            fn__0x34: null,
            fn__0x38: get_fnptr(0x64F9A0),
            fn_help:  null
        );

        // Fill it in!
        FhXCall.FUN_00a459e0.fnptr!(2, 65);
    }

    public void init_menu_preview_white_magic() {
        SphereGridMenu* menu = &menu_list->menus[(int)SphereGridMenuId.PREVIEW_WHT_MAGIC];

        // (short)(is_cjk ?  : ),

        init_menu(
            menu,
            x: (short)(is_cjk ? 96 : 64),
            y: 256,
            width:  (short)(is_cjk ? 320 : 384),
            height: 120,
            __0x14: (short)(is_cjk ? 20 : 24),
            __0x20: 32,
            col_count: 4,
            row_count: 6,
            fn__0x34: null,
            fn__0x38: get_fnptr(0x64F250),
            fn_help:  null
        );

        // Fill it in!
        FhXCall.FUN_00a459e0.fnptr!(4, 69);
    }

    public void init_menu_preview_black_magic() {
        SphereGridMenu* menu = &menu_list->menus[(int)SphereGridMenuId.PREVIEW_BLK_MAGIC];

        init_menu(
            menu,
            x: (short)(is_cjk ? 96 : 64),
            y: 276,
            width:  (short)(is_cjk ? 320 : 384),
            height: 100,
            __0x14: (short)(is_cjk ? 20 : 24),
            __0x20: 32,
            col_count: 4,
            row_count: 5,
            fn__0x34: null,
            fn__0x38: get_fnptr(0x64F250),
            fn_help:  null
        );

        // Fill it in!
        FhXCall.FUN_00a459e0.fnptr!(5, 68);
    }

    public void init_menu_move_confirm_prompt() {
        SphereGridMenu* menu = &menu_list->menus[(int)SphereGridMenuId.MOVE_CONFIRM_PROMPT];

        init_menu(
            menu,
            x:  48,
            y: 205,
            width:  (short)(is_cjk ? 96 : 144),
            height: 20,
            __0x14: (short)(is_cjk ? 6 : 9),
            __0x20: 10,
            col_count: 1,
            row_count: 1,
            fn__0x34: null,
            fn__0x38: get_fnptr(0x64FB70),
            fn_help:  null
        );

        // Fill it in!
        bool  hira = Globals.save_data->config_hiragana;
        byte* text = FhXCall.MsMenuGetText.fnptr!(9, 43, hira);

        menu_add_entries(menu, [
            ((uint)text, 0, 43),
        ]);
    }

    public void init_menu_quit_confirm_prompt() {
        SphereGridMenu* menu = &menu_list->menus[(int)SphereGridMenuId.QUIT_CONFIRM_PROMPT];

        init_menu(
            menu,
            x:  48,
            y: 205,
            width:  (short)(is_cjk ? 112 : 144),
            height: 20,
            __0x14: (short)(is_cjk ? 7 : 9),
            __0x20: 10,
            col_count: 1,
            row_count: 1,
            fn__0x34: null,
            fn__0x38: get_fnptr(0x64FB70),
            fn_help:  null
        );

        // Fill it in!
        bool  hira = Globals.save_data->config_hiragana;
        byte* text = FhXCall.MsMenuGetText.fnptr!(9, 46, hira);

        menu_add_entries(menu, [
            ((uint)text, 0, 46),
        ]);
    }

    public void init_menu_yes_no_prompt() {
        SphereGridMenu* menu = &menu_list->menus[(int)SphereGridMenuId.YES_NO_PROMPT];

        init_menu(
            menu,
            x:  48,
            y: 245,
            width:  80,
            height: 40,
            __0x14: 5,
            __0x20: 15,
            col_count: 1,
            row_count: 2,
            fn__0x34: null,
            fn__0x38: get_fnptr(0x64F930),
            fn_help:  null
        );

        // Fill it in!
        bool  hira = Globals.save_data->config_hiragana;
        byte* text_yes = FhXCall.MsMenuGetText.fnptr!(9, 44, hira);
        byte* text_no  = FhXCall.MsMenuGetText.fnptr!(9, 45, hira);

        menu_add_entries(menu, [
            ((uint)text_yes, 0, 44),
            ((uint)text_no , 0, 45),
        ]);
    }

    public void h_open_sphere_grid() {
        is_open = false;
        is_cjk  = FhGlobal.lang_id switch {
            FhLangId.Japanese or
            FhLangId.Korean   or
            FhLangId.Chinese  or
            FhLangId.Debug    => true,

            _ => false,
        };

        FhXCall.InitLpamng.fnptr!(); // Sets the current_ply_id field!!
        FhXCall.FUN_00a57620.fnptr!();

        //TODO: Reimplement this when adding a custom sphere grid registry.
        FhXCall.LoadAbmap.fnptr!();
        FhXCall.FUN_00901100.fnptr!();

        lpamng->available_indicators = 0;
        for (int ply_id = 0; ply_id < 7; ply_id++) {
            if (!Globals.save_data->ply_saves[ply_id].joined) continue;

            lpamng->available_indicators |= (byte)(1 << ply_id);

            //TODO: Reimplement this when adding a custom character registry.
            FhXCall.FUN_00a57f80.fnptr!(ply_id, 0, 2f, 0x80404040, 0x80404040, 0x80808080);
        }

        //TODO: Reimplement this when adding a custom character registry.
        FhXCall.LoadSaveAbmap.fnptr!();

        for (int ply_id = 0; ply_id < 7; ply_id++) {
            SphereGridPlyInfo* ply_info = &lpamng->party_infos[ply_id];

            if (ply_info->pos_circle_radius <= 0f) continue;

            SphereGridNode* current_node = &lpamng->nodes[ply_info->current_node_idx];

            ply_info->pos = new Vector4(
                current_node->x,
                current_node->y,
                0f,
                1f
            );

            short width = lpamng->node_type_infos[current_node->node_type.normalize()].width;
            ply_info->pos_circle_radius  = width / 2f + 3f;

            FhXCall.AbmapPositionPlyTag.fnptr!(ply_id);
        }

        lpamng->__0x115C7 = 1;
        lpamng->selected_node_idx = lpamng->party_infos[lpamng->current_ply_id].current_node_idx;
        int selected_idx = lpamng->selected_node_idx;

        lpamng->__0x112B8 = new Vector4(
            lpamng->nodes[selected_idx].x,
            lpamng->nodes[selected_idx].y,
            0f,
            1f
        );

        short selected_node_width = lpamng->node_type_infos[lpamng->nodes[selected_idx].node_type.normalize()].width;

        lpamng->__0x11306 = 0;
        lpamng->__0x112D8 = 0x80808080;
        lpamng->__0x112DC = 0x80808080;
        lpamng->current_halo_width = selected_node_width / 2f + 3f;
        lpamng->__0x112E0 = 0x80FFFFFF;
        lpamng->__0x112F8 = 2f;

        lpamng->cam_desired_pos = lpamng->__0x112B8;

        lpamng->link_points = FhUtil.ptr_at<LpAbilityMapEngine.SphereGridLinkPointArray>(0x1293160);
        FhXCall.AbmapCalcLinkPoints.fnptr!();

        FhXCall.FUN_00a57120.fnptr!();

        //TODO: Reimplement this when adding a custom character registry.
        FhXCall.FUN_00a5b030.fnptr!();

        FhXCall.pppInitEnv.fnptr!(
            FhUtil.ptr_at<int>(0x1686034),
            FhUtil.ptr_at<int>(0x1F05800),
            FhUtil.ptr_at<int>(0x12C1830),
            0x7D000
        );

        // Initialize our menus!
        init_menu_0();
        init_menu_action_prompt();
        init_menu_use_item();
        init_menu_preview_prompt();
        init_menu_preview_stats();
        init_menu_preview_skills();
        init_menu_preview_special();
        init_menu_preview_white_magic();
        init_menu_preview_black_magic();
        init_menu_move_confirm_prompt();
        init_menu_quit_confirm_prompt();
        init_menu_yes_no_prompt();

        FhXCall.FUN_00639140.fnptr!(lpamng->__0x116A4);
        FhXCall.FUN_0063dff0.fnptr!();

        FhXCall.graphicSetFlipVsnc.fnptr!(2);

        void* alloc1 = NativeMemory.Alloc(0x200);
        new Span<byte>(alloc1, 0x200).Fill(0xCD);
        FhUtil.set_at<nint>(0x16860EC, (nint)alloc1);

        void* alloc2 = NativeMemory.Alloc(0x200);
        new Span<byte>(alloc2, 0x200).Fill(0xCD);
        FhUtil.set_at<nint>(0x16860F0, (nint)alloc2);
    }

    public void h_state_moving() {
        lpamng->move_progress += lpamng->move_speed;
        float move_t = lpamng->move_progress;

        SphereGridPlyInfo* ply_info = &lpamng->party_infos[lpamng->move_ply_id];

        if (lpamng->should_update == 0) {
            lpamng->should_update = 1;
        }

        SphereGridNode* next_node = &lpamng->nodes[lpamng->move_next_knot_idx];
        Vector4 next_node_pos = new(next_node->x, next_node->y, 0f, 1f);

        while (move_t >= 1f) {
            lpamng->move_next_link = null;

            if (lpamng->move_next_knot_idx == lpamng->move_target_node_idx) {
                ply_info->pos = next_node_pos;
                lpamng->__0x115C7 = 1;
                lpamng->fn_ctrl = lpamng->fn_ctrl_backup;
                lpamng->fn_ctrl_backup = null;
                return;
            }

            SphereGridLink* next_link = null;
            lpamng->move_next_knot_idx = FhXCall.AbmapFindNextConnectingNode.fnptr!(
                lpamng->move_next_knot_idx,
                lpamng->move_target_node_idx,
                &next_link
            );

            if (lpamng->move_next_knot_idx == -1) {
                ply_info->pos = next_node_pos;
                lpamng->__0x115C7 = 1;
                lpamng->fn_ctrl = lpamng->fn_ctrl_backup;
                lpamng->fn_ctrl_backup = null;
                return;
            }

            lpamng->move_next_link_anchor_idx = next_link->anchor_idx;

            if (!next_link->activated_by.get_bit(lpamng->move_ply_id)) {
                next_link->activated_by.set_bit(lpamng->move_ply_id, true);
                next_link->flags.just_activated = true;
                lpamng->move_next_link = next_link;
            }

            ply_info->current_node_idx = lpamng->move_next_knot_idx;
            lpamng->move_prev_node_pos = ply_info->pos;

            next_node_pos.X = lpamng->nodes[lpamng->move_next_knot_idx].x;
            next_node_pos.Y = lpamng->nodes[lpamng->move_next_knot_idx].y;

            Vector4 Vector4_00c8f820 = lpamng->move_prev_node_pos;
            Vector4 Vector4_00c8f830 = (next_node_pos - Vector4_00c8f820) with { W = next_node_pos.W };

            // FhXCall.restoreVf00Register();

            float move_length = Vector4_00c8f830.Length();
            FhUtil.set_at<float>(0x88F788, move_length);
            float new_move_speed = move_length <= 0f ? 0.53333336f : 8f / move_length;

            lpamng->move_speed = new_move_speed;
            lpamng->move_progress -= 1f;

            float next_node_radius =
                lpamng->node_type_infos[lpamng->nodes[ply_info->current_node_idx].node_type.normalize()].width / 2f;

            lpamng->move_halo_start_radius  = ply_info->pos_circle_radius;
            lpamng->move_halo_target_radius = next_node_radius + 3f;

            next_node_pos.X = lpamng->nodes[lpamng->move_next_knot_idx].x;
            next_node_pos.Y = lpamng->nodes[lpamng->move_next_knot_idx].y;

            move_t = lpamng->move_progress;

            // Vanilla breaks here. This causes undesirable consequences
            // when move_t is still >= 1, so we continue instead.
        }

        next_node_pos.Z = 0f;
        next_node_pos.W = 1f;

        short next_anchor_idx = lpamng->move_next_link_anchor_idx;

        if (next_anchor_idx == -1) {
            // Straight link!
            Vector4 pos_lerp = Vector4.Lerp(lpamng->move_prev_node_pos, next_node_pos, lpamng->move_progress);
            ply_info->pos.X = pos_lerp.X;
            ply_info->pos.Y = pos_lerp.Y;
        }
        else {
            // Curved link!
            Vector4 anchor_pos = lpamng->nodes[next_anchor_idx].pos.AsVector4Unsafe() with { Z = 0f, W = 1f };
            FhXCall.AbmapUpdateMovingPlyPos.fnptr!(
                ply_info,
                &lpamng->move_prev_node_pos,
                &next_node_pos,
                &anchor_pos,
                lpamng->move_progress
            );
        }

        ply_info->pos_circle_radius = float.Lerp(
            lpamng->move_halo_start_radius,
            lpamng->move_halo_target_radius,
            lpamng->move_progress
        );

        lpamng->__0x115C7 = 1;
        FhXCall.FUN_00a5b030.fnptr!();
    }
}
