// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

// ffx_ps2/ffx2/master/jppc/lastmiss/kernel/lm_command.h
// ffx_ps2/ffx2/master/jppc/lastmiss/kernel/lm_command.ath
// Steam release of FFX/X-2 HD

namespace Fahrenheit.FFX2;

public partial struct LmCommand
{
    public uint name;
    public byte name_yn;
    public byte dummy1;
    public byte dummy2;
    public byte dummy3;
    public uint help;
    public byte help_yn;
    public byte dummy4;
    public byte dummy5;
    public byte dummy6;
    public uint information;
    public ushort personal_job;
    public byte char_job;
    public byte short_dist;
    public byte shot_range;
    public byte long_dist;
    public byte long_range;
    public byte stairchk_yn;
    public byte cursol_dist_yn;
    public byte cursol_cat;
    public byte target_pos;
    public byte cmdend;
    public byte cmdend_menu;
    public byte dummy7;

    public short effect_no;
    public byte read_motion;
    public byte read_effect;
    public byte trun_change;
    public byte motion;
    public byte steal_st;
    public byte steal_hit;
    public byte use_mp;
    public byte use_hp;
    public byte target_param;
    public byte retdmg_motion;
    public byte critical;
    public byte calc_id;
    public ushort calc_no;
    public ushort atk_cnt;
    public ushort target_item_category;
    public byte category_abilty;
    public byte category_dmg_ret;
    public byte hit;
    public byte dark_hit;
    public byte hit_calc_id;
    public byte blue_magic;
    public byte attribute_atk;
    public byte conf_use;
    public byte jibaku;
    public byte status_chg_target;
    public byte status_chg;
    public byte status_onoff;
    public byte status_hit;
}
