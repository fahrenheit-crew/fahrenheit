// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

using Fahrenheit.Atel;
using Fahrenheit.FFX.Battle;
using Fahrenheit.FFX.SphereGrid;

namespace Fahrenheit.FFX;

public unsafe static class Globals {
    public static LocalizationManager* localization_manager => (LocalizationManager*)FhUtil.get_at<nint>(0x8DED48);

    public static class Map {
        public static int*        tri_count => FhUtil.ptr_at<int>       (0xF01A48);
        public static VpaTri*     tris      => FhUtil.ptr_at<VpaTri>    (0xF01A44);
        public static VpaVertex*  vertices  => FhUtil.ptr_at<VpaVertex> (0xF01A4C);
        public static float*      scale     => FhUtil.ptr_at<float>     (0xF01A50);
        public static VpaNavMesh* navmesh   => FhUtil.ptr_at<VpaNavMesh>(0xF01A54);
    }

    public static class Magic {
        public static int*  current_id           => FhUtil.ptr_at<int> (0x864CA0);
        public static int*  current_handle       => FhUtil.ptr_at<int> (0x864CA8);
        public static int*  to_be_deleted_id     => FhUtil.ptr_at<int> (0x864CA4);
        public static int*  to_be_deleted_handle => FhUtil.ptr_at<int> (0x864CAC);
        public static int*  effect_ptr           => FhUtil.ptr_at<int> (0xD33360);
        public static byte* effect_status_flag   => FhUtil.ptr_at<byte>(0xD33364);
    }

    public static class Atel {
        public static int*                  request_count      => FhUtil.ptr_at<int>                 (0xD34564);
        public static AtelRequest*          request_list       => FhUtil.ptr_at<AtelRequest>         (0xD35D68);
        public static AtelWorkerController* controllers        => FhUtil.ptr_at<AtelWorkerController>(0xF25B60);
        public static AtelBasicWorker*      current_worker     => FhUtil.ptr_at<AtelBasicWorker>     (0xF270A4);
        public static AtelWorkerController* current_controller => (AtelWorkerController*)FhUtil.get_at<nint>(0xF26AE8);
    }

    public static class SphereGrid {
        public static bool* is_open => FhUtil.ptr_at<bool>(0x1685f70);
        public static int*  slv_available_for_move => FhUtil.ptr_at<int>(0x16860FC);
        public static int*  move_ply_mask => FhUtil.ptr_at<int>(0x1686104);
        public static int*  move_prev_link_cost => FhUtil.ptr_at<int>(0x1686100);
        public static LpAbilityMapEngine* lpamng => (LpAbilityMapEngine*)FhUtil.get_at<nint>(0x1F05834);
    }

    public static class Battle {
        public static Chr* player_characters  => (Chr*)FhUtil.get_at<nint>(0xD334CC);
        public static Chr* monster_characters => (Chr*)FhUtil.get_at<nint>(0xD34460);

        public static Btl* btl => FhUtil.ptr_at<Btl>(0xD2A8D0);
        public static BtlRewardData* reward_data => FhUtil.ptr_at<BtlRewardData>(0x1F10EA0);

        public static BtlWindow* windows => FhUtil.ptr_at<BtlWindow>(0xF3C910);
        public static BtlWindow* cur_window => windows + FhUtil.get_at<byte>(0x1FCC092);
        public static BtlStatusWindow* status_windows => FhUtil.ptr_at<BtlStatusWindow>(0xF3F798);

        public static byte* hit_chance_table => FhUtil.ptr_at<byte>(0x8421E0);

        public static EncounterLevel* encounter_level => FhUtil.ptr_at<EncounterLevel>(0xC421C8);
    }

    public static class OverdriveInfo {
        public static class Times {
            public static float* spiral_cut      => FhUtil.ptr_at<float>(0x886BF0);
            public static float* slice_and_dice  => FhUtil.ptr_at<float>(0x886BF4);
            public static float* energy_rain     => FhUtil.ptr_at<float>(0x886BF8);
            public static float* blitz_ace       => FhUtil.ptr_at<float>(0x886BFC);

            public static float* dragon_fang     => FhUtil.ptr_at<float>(0x886B60);
            public static float* shooting_star   => FhUtil.ptr_at<float>(0x886B64);
            public static float* banishing_blade => FhUtil.ptr_at<float>(0x886B68);
            public static float* tornado         => FhUtil.ptr_at<float>(0x886B6C);
        }
    }
    public static FhInput Input => FhApi.Input;

    public static SaveData* save_data => FhUtil.ptr_at<SaveData>(0xD2CA90);

    public static Actor*         actors          => (Actor*)*FhUtil.ptr_at<nint>(0x1FC44E4);

    public static int* event_id => FhUtil.ptr_at<int>(0xefbbf8);
}
