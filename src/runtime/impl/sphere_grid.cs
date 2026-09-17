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
            && FhXCall.AbmapState_Warping.hook(this, h_state_warping)
            && FhXCall.AbmapCalcMoveCosts.hook(this, h_calc_move_costs)
            && FhXCall.AbmapCalcMoveCost.hook(this, h_calc_move_cost)
            && FhXCall.AbmapInitChoosingMoveTarget.hook(this, h_init_choose_move_target);
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
                node->properties |=  MASK;
            else
                node->properties &= ~MASK;
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
        lpamng->move_next_knot_node_idx = lpamng->party_infos[ply_id].current_node_idx;
        lpamng->move_start_node_idx = lpamng->move_next_knot_node_idx;
        lpamng->move_target_node_idx = node_idx;

        lpamng->moving_progress = 1f;
        lpamng->moving_speed    = 0f;

        lpamng->moving_ply_id = (byte)ply_id;

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
                    lpamng->party_infos[lpamng->moving_ply_id].pos_circle_radius = 0f;
                    lpamng->__0x1164C = 1;
                    lpamng->__0x115C6 = 0;
                }

                break;

            case 1:
                SphereGridNode target_node = lpamng->nodes[lpamng->move_target_node_idx];

                Vector4 target_pos = new(target_node.x, target_node.y, 0f, 1f);

                lpamng->moving_progress += 1f/12f;
                if (1f <= lpamng->moving_progress){
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

                    lpamng->party_infos[lpamng->moving_ply_id].current_node_idx
                        = lpamng->move_target_node_idx;

                    FhXCall.FUN_00a5a990.fnptr!(lpamng->moving_ply_id);
                    FhXCall.AbmapPositionPlyTag.fnptr!(lpamng->moving_ply_id);

                    lpamng->__0x115C7 = 1;

                    FhXCall.FUN_00a5b030.fnptr!();
                    break;
                }

                lpamng->cam_desired_pos = Vector4.Lerp(lpamng->move_prev_node_pos, target_pos, lpamng->moving_progress);

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
}
