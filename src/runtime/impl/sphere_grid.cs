// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

using Fahrenheit.FFX;
using Fahrenheit.FFX.Ids;

using FhXCall = Fahrenheit.FFX.FhCall;

namespace Fahrenheit.Runtime.Impl;

[FhLoad(FhGameId.FFX)]
public unsafe class SphereGridReimplModule : FhModule {
    private LpAbilityMapEngine* lpamng => Globals.SphereGrid.lpamng;
    private SphereGridNode* selected_node => &lpamng->nodes[lpamng->selected_node_idx];

    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        return FhXCall.AbmapState_ChoosingMoveTarget.hook(this, h_state_choosing_move_target);
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

    public void init_moving(int chr_id, short node_idx) {
        lpamng->move_next_target_node_idx = lpamng->party_infos[chr_id].current_node_idx;
        lpamng->move_start_node_idx = lpamng->move_next_target_node_idx;
        lpamng->move_last_target_node_idx = node_idx;

        lpamng->moving_progress = 1f;
        lpamng->moving_speed    = 0f;

        lpamng->moving_ply_id = (byte)chr_id;

        if (lpamng->fn_ctrl_backup == null) {
            lpamng->fn_ctrl_backup = lpamng->fn_ctrl;
            lpamng->fn_ctrl = get_fnptr(0x659990);
        }
    }

    public void h_state_choosing_move_target() {
        FhXCall.FUN_00a58ff0.fnptr!(get_fnptr(0x645000));

        if (lpamng->__0x115CD == 0 && lpamng->fn_ctrl_backup == null) {
            lpamng->slv_queued = (selected_node->move_cost + 3) >> 2;

            // Confirm button
            if (lpamng->abmap_input[1].get_bit(5)) {
                limit_all_link_flags((SphereGridLinkProperties)0b11111000);

                lpamng->fn_ctrl = get_fnptr(0x648230);
                lpamng->fn_help = null;
                lpamng->__0x115C3 = 0;
                init_moving(lpamng->current_ply_id, lpamng->selected_node_idx);
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
    }
}
