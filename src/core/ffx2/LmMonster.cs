// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

// ffx_ps2/ffx2/master/jppc/lastmiss/kernel/lm_monster.h
// Steam release of FFX/X-2 HD

namespace Fahrenheit.FFX2;

public partial struct LmMonster
{
    public uint name;
    public uint help;
    public byte lv;
    public byte dummy1;
    public byte dummy2;
    public byte dummy3;
    public uint hp;
    public uint mp;
    public byte str;
    public byte mag;
    public byte vit;
    public byte spirit;
    public byte hit;
    public byte avoid;
    public byte dummy4;
    public byte dummy5;
    public uint os_hp;
    public uint os_mp;
    public byte os_str;
    public byte os_mag;
    public byte os_vit;
    public byte os_spirit;
    public byte os_hit;
    public byte os_avoid;
    public byte move;
    public byte fix_dmg;
    public byte stair_move;
    public byte size_square;
    public byte dummy6;
    public byte dummy7;
    public uint size_real;
    public byte think_movepat;
    public byte dummy8;
    public byte dummy9;
    public byte dummy10;
    public uint view_dist_normal;
    public byte view_range_normal;
    public byte dummy11;
    public byte dummy12;
    public byte dummy13;
    public uint view_dist_battle;
    public byte view_range_battle;
    public byte view_obstacle;
    public byte effe_zantetsu;
    public byte hit_zantetsu;
    public byte hit_carry_mon;
    public byte ele_holy;
    public byte ele_gravit;
    public byte ele_fire;
    public byte ele_thunder;
    public byte ele_ice;
    public byte ele_water;
    public byte dummy14;
    public ushort item;
    public byte item_data;
    public byte steal_item_hit;
    public ushort os_item;
    public byte os_item_data;
    public byte os_steal_item_hit;

    public int steal_mon_skill;
    public byte steal_mon_skill_hit;
    public byte dummy15;
    public byte dummy16;
    public byte dummy17;

    public int exp;
    public byte ap;
    public byte steal_exp_hit;
    public byte steal_exp_rate;
    public byte mgun_atk;
    public byte type_sky;
    public byte prohibit_timestop;
    public byte prohibit_smell_player;
    public byte prohibit_ratedmg;
    public byte prohibit_posion;
    public byte prohibit_blindness;
    public byte prohibit_sleep;
    public byte prohibit_confusion;
    public byte prohibit_stop;
    public byte prohibit_dead_count;
    public byte prohibit_silence;
    public byte prohibit_berserk;
    public byte prohibit_slow;
    public byte prohibit_movestop;
    public byte prohibit_consump_2mp;
    public byte prohibit_drop_money;
    public byte prohibit_oversoul;
    public byte prohibit_change_money;
    public byte prohibit_change_dress;
}
