// SPDX-License-Identifier: MIT

namespace Fahrenheit.Core.FFX.Battle;

[StructLayout(LayoutKind.Explicit, Size = 0x1EC)]
public struct ChrRam {
    [InlineArray(40)]
    public struct ChrName {
        private byte _data;
    }

    [FieldOffset(0x0)]   public ChrName               name;
    [FieldOffset(0x50)]  public byte                  gender;
    [FieldOffset(0x52)]  public byte                  wpn_inv_idx;
    [FieldOffset(0x53)]  public byte                  arm_inv_idx;
    [FieldOffset(0x54)]  public int                   max_hp;
    [FieldOffset(0x58)]  public int                   max_mp;
    [FieldOffset(0x5C)]  public int                   base_max_hp;
    [FieldOffset(0x60)]  public int                   base_max_mp;
    [FieldOffset(0x64)]  public int                   overkill_threshold;
    [FieldOffset(0x68)]  public byte                  strength;
    [FieldOffset(0x69)]  public byte                  defense;
    [FieldOffset(0x6A)]  public byte                  magic;
    [FieldOffset(0x6B)]  public byte                  magic_defense;
    [FieldOffset(0x6C)]  public byte                  agility;
    [FieldOffset(0x6D)]  public byte                  luck;
    [FieldOffset(0x6E)]  public byte                  evasion;
    [FieldOffset(0x6F)]  public byte                  accuracy;
    [FieldOffset(0x70)]  public byte                  strength_up;
    [FieldOffset(0x71)]  public byte                  defense_up;
    [FieldOffset(0x72)]  public byte                  magic_up;
    [FieldOffset(0x73)]  public byte                  magic_defense_up;
    [FieldOffset(0x74)]  public byte                  agility_up;
    [FieldOffset(0x75)]  public byte                  luck_up;
    [FieldOffset(0x76)]  public byte                  evasion_up;
    [FieldOffset(0x77)]  public byte                  accuracy_up;
    [FieldOffset(0x78)]  public ChrResistFlags        special_resistances;
    [FieldOffset(0x7A)]  public byte                  poison_dmg;
    [FieldOffset(0x7B)]  public byte                  limit_mode_selected;
    [FieldOffset(0x7C)]  public byte                  limit_charge;
    [FieldOffset(0x7D)]  public byte                  limit_charge_max;
    [FieldOffset(0x81)]  public byte                  wpn_dmg_formula;
    [FieldOffset(0x83)]  public byte                  stat_icon_number;
    [FieldOffset(0x84)]  public byte                  provoked_by_id;
    [FieldOffset(0x85)]  public byte                  threatened_by_id;
    [FieldOffset(0x87)]  public byte                  wpn_power;
    [FieldOffset(0x88)]  public byte                  doom_counter;
    [FieldOffset(0x89)]  public byte                  doom_counter_init;
    [FieldOffset(0x8B)]  public bool                  stat_prov_command_flag;
    [FieldOffset(0x90)]  public int                   hp;
    [FieldOffset(0x94)]  public int                   mp;
    [FieldOffset(0x98)]  public byte                  wpn_crit_bonus;
    [FieldOffset(0x99)]  public ElementFlags          elem_wpn;
    [FieldOffset(0x9A)]  public ElementFlags          elem_absorb;
    [FieldOffset(0x9B)]  public ElementFlags          elem_ignore;
    [FieldOffset(0x9C)]  public ElementFlags          elem_resist;
    [FieldOffset(0x9D)]  public ElementFlags          elem_weak;
    [FieldOffset(0x9E)]  public StatusMap             status_inflict;
    [FieldOffset(0xB7)]  public StatusDurationMap     status_duration_inflict;
    [FieldOffset(0xC4)]  public StatusExtraFlags      status_inflict_extra;
    [FieldOffset(0xC6)]  public StatusPermanentFlags  status_suffer;
    [FieldOffset(0xC8)]  public StatusDurationMap     status_suffer_turns_left;
    [FieldOffset(0xD6)]  public StatusExtraFlags      status_suffer_extra;
    [FieldOffset(0xEA)]  public StatusPermanentFlags  status_auto_full_permanent;
    [FieldOffset(0xEC)]  public StatusTemporalFlags   status_auto_full_temporal;
    [FieldOffset(0xEE)]  public StatusExtraFlags      status_auto_full_extra;
    [FieldOffset(0xF0)]  public StatusPermanentFlags  status_auto_innate_permanent;
    [FieldOffset(0xF2)]  public StatusTemporalFlags   status_auto_innate_temporal;
    [FieldOffset(0xF4)]  public StatusExtraFlags      status_auto_innate_extra;
    [FieldOffset(0xF6)]  public StatusPermanentFlags  status_auto_sos_permanent;
    [FieldOffset(0xF8)]  public StatusTemporalFlags   status_auto_sos_temporal;
    [FieldOffset(0xFA)]  public StatusExtraFlags      status_auto_sos_extra;
    [FieldOffset(0xFD)]  public byte                  weak_level_full;
    [FieldOffset(0xFE)]  public byte                  weak_level_hp;
    [FieldOffset(0x101)] public StatusMap             status_resist;
    [FieldOffset(0x11A)] public StatusExtraFlags      status_resist_extra;
    [FieldOffset(0x11C)] public byte                  ctb;
    [FieldOffset(0x11D)] public byte                  default_turn_delay;
    [FieldOffset(0x11E)] public byte                  cheer_stacks;
    [FieldOffset(0x11F)] public byte                  aim_stacks;
    [FieldOffset(0x120)] public byte                  focus_stacks;
    [FieldOffset(0x121)] public byte                  reflex_stacks;
    [FieldOffset(0x122)] public byte                  luck_stacks;
    [FieldOffset(0x123)] public byte                  jinx_stacks;
    [FieldOffset(0x124)] public AbilityMap            abilities;
    [FieldOffset(0x17C)] public AutoAbilityEffectsMap auto_ability_effects;
    [FieldOffset(0x18E)] public byte                  stat_use_mp0;
    [FieldOffset(0x191)] public byte                  summoned_by_id;
    [FieldOffset(0x192)] public byte                  regen_strength;
    [FieldOffset(0x19B)] public byte                  stat_attack_num;
    [FieldOffset(0x19F)] public byte                  stat_command_exe_count;
    [FieldOffset(0x1A0)] public byte                  stat_consent;
    [FieldOffset(0x1A1)] public byte                  stat_energy;
    [FieldOffset(0x1A4)] public int                   current_hp;
    [FieldOffset(0x1A8)] public int                   current_mp;
    [FieldOffset(0x1AC)] public int                   current_ctb;
    [FieldOffset(0x1C0)] public sbyte                 stat_death_status;
    [FieldOffset(0x1C4)] public uint                  bribe_gil_spent;
    [FieldOffset(0x1D8)] public bool                  stat_limit_bar_flag;
    [FieldOffset(0x1D9)] public byte                  stat_limit_bar_flag_cam;

    // Gender
    public bool is_male   { readonly get { return gender.get_bit(0); } set { gender.set_bit(0, value); } }
    public bool is_female { readonly get { return gender.get_bit(1); } set { gender.set_bit(1, value); } }
    public bool is_aeon   { readonly get { return gender.get_bit(2); } set { gender.set_bit(2, value); } }
}

[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 0xF90)]
public unsafe struct Chr {
    [FieldOffset(0x0)]   public Actor*   actor;
    [FieldOffset(0x4)]   public uint     model_id;
    [FieldOffset(0xC)]   public ushort   id;
    [FieldOffset(0xE)]   public ushort   chr_id;
    [FieldOffset(0x16)]  public byte     stat_visible;
    [FieldOffset(0x17)]  public byte     stat_hide;
    [FieldOffset(0x18)]  public sbyte    stat_visible_eff;
    [FieldOffset(0x19)]  public byte     stat_visible_cam;
    [FieldOffset(0x1B)]  public byte     stat_visible_out;
    [FieldOffset(0x1C)]  public sbyte    stat_visible_out_on;
    [FieldOffset(0x21)]  public bool     stat_motion_dispose_flag;
    [FieldOffset(0x22)]  public bool     stat_model_dispose_flag;
    [FieldOffset(0x23)]  public bool     stat_fast_model_flag;
    [FieldOffset(0x24)]  public byte     stat_model;
    [FieldOffset(0x26)]  public sbyte    stat_shadow;
    [FieldOffset(0x2D)]  public bool     stat_bodyhit_flag;
    [FieldOffset(0x3B)]  public bool     eternal_autolife;
    [FieldOffset(0x48)]  public nint     ptr_mon_wep_bin;
    [FieldOffset(0x182)] public byte     grav_mode;
    [FieldOffset(0x185)] public byte     field_mode;
    [FieldOffset(0x186)] public byte     motion_type;
    [FieldOffset(0x3F4)] public byte     death_level;
    [FieldOffset(0x3F5)] public byte     death_pattern;
    [FieldOffset(0x3F8)] public bool     stat_center_chr_flag;
    [FieldOffset(0x3F9)] public byte     stat_death_return;
    [FieldOffset(0x400)] public byte     motion_speed_normal;
    [FieldOffset(0x405)] public byte     motion_speed_normal_init;
    [FieldOffset(0x408)] public byte     stat_attack_motion_type;
    [FieldOffset(0x409)] public byte     stat_return_motion_type;
    [FieldOffset(0x40A)] public bool     stat_attack_return_flag;
    [FieldOffset(0x40C)] public byte     stat_attack_normal_frame;
    [FieldOffset(0x40D)] public byte     stat_attack_near_frame;
    [FieldOffset(0x40E)] public byte     stat_attack_motion_frame;
    [FieldOffset(0x412)] public bool     stat_move_flag;
    [FieldOffset(0x416)] public byte     stat_move_target;
    [FieldOffset(0x41B)] public bool     stat_direction_fix_flag;
    [FieldOffset(0x41C)] public bool     stat_direction_change_flag;
    [FieldOffset(0x41D)] public byte     stat_direction_change_effect;
    [FieldOffset(0x41E)] public bool     stat_disable_move_flag;
    [FieldOffset(0x41F)] public bool     stat_disable_jump_flag;
    [FieldOffset(0x420)] public byte     stat_motionlv;
    [FieldOffset(0x422)] public byte     stat_damage_chr;
    [FieldOffset(0x423)] public bool     stat_appear_invisible_flag;
    [FieldOffset(0x424)] public byte     stat_appear_count;
    [FieldOffset(0x425)] public bool     stat_avoid_flag;
    [FieldOffset(0x426)] public bool     stat_adjust_pos_flag;
    [FieldOffset(0x433)] public bool     stat_hit_terminate_flag;
    [FieldOffset(0x437)] public bool     stat_neck_target_flag;
    [FieldOffset(0x438)] public byte     neck_target_id;
    [FieldOffset(0x43B)] public byte     stat_win_pose;
    [FieldOffset(0x43C)] public sbyte    stat_win_se;
    [FieldOffset(0x43D)] public bool     stat_appear_motion_flag;
    [FieldOffset(0x43E)] public byte     stat_live_motion;
    [FieldOffset(0x442)] public byte     stat_num_print_element;
    [FieldOffset(0x446)] public byte     stat_command_type;
    [FieldOffset(0x447)] public byte     stat_inv_motion;
    [FieldOffset(0x448)] public bool     stat_wait_motion_flag;
    [FieldOffset(0x44A)] public sbyte    stat_near_motion;
    [FieldOffset(0x44B)] public sbyte    stat_near_motion_set;
    [FieldOffset(0x44E)] public byte     stat_dmg_dir;
    [FieldOffset(0x44F)] public byte     stat_weak_motion;
    [FieldOffset(0x451)] public byte     stat_magiclv;
    [FieldOffset(0x4B4)] public byte     stat_own_attack_near;
    [FieldOffset(0x4B8)] public byte     stat_idle2_prob;
    [FieldOffset(0x4B9)] public byte     stat_attack_inc_speed;
    [FieldOffset(0x4BA)] public byte     stat_attack_dec_speed;
    [FieldOffset(0x4BB)] public byte     stat_motion_num;
    [FieldOffset(0x4FC)] public ushort   area;
    [FieldOffset(0x4FE)] public byte     pos;
    [FieldOffset(0x4FF)] public byte     stat_far;
    [FieldOffset(0x504)] public byte     stat_group;
    [FieldOffset(0x505)] public bool     flying;
    [FieldOffset(0x508)] public byte     stat_adjust_pos;
    [FieldOffset(0x509)] public byte     stat_move_area;
    [FieldOffset(0x50B)] public byte     stat_move_pos;
    [FieldOffset(0x50D)] public byte     stat_height_on;
    [FieldOffset(0x50E)] public bool     stat_sleep_recover_flag;
    [FieldOffset(0x540)] public ChrRam   ram;
    [FieldOffset(0xD34)] public int      damage_hp;
    [FieldOffset(0xD38)] public int      damage_mp;
    [FieldOffset(0xD3C)] public int      damage_ctb;
    [FieldOffset(0xDC8)] public byte     in_battle;
    [FieldOffset(0xDCA)] public byte     stat_target_list;
    [FieldOffset(0xDCC)] public byte     stat_death;
    [FieldOffset(0xDCD)] public bool     stat_escape_flag;
    [FieldOffset(0xDCE)] public byte     stat_stone;
    [FieldOffset(0xDD0)] public bool     should_try_capture; // Set to true when a capture should be attempted if the current attack kills the monster
    [FieldOffset(0xDD1)] public bool     captured;           // Set to true when an attempted capture succeeds
    [FieldOffset(0xDD2)] public bool     stat_exist_flag;
    [FieldOffset(0xDD6)] public byte     stat_action;
    [FieldOffset(0xDD7)] public bool     in_ctb_list;
    [FieldOffset(0xDD8)] public bool     in_hp_list;
    [FieldOffset(0xDD9)] public byte     stat_cursor;
    [FieldOffset(0xDDA)] public bool     stat_effect_target_flag;
    [FieldOffset(0xDDB)] public bool     stat_regen_damage_flag;
    [FieldOffset(0xDDD)] public byte     stat_event_chr;
    [FieldOffset(0xDDE)] public bool     stat_blow_exist_flag;
    [FieldOffset(0xDEB)] public byte     steal_count;
    [FieldOffset(0xDEC)] public byte     stat_will_die;
    [FieldOffset(0xDF9)] public byte     stat_cursor_element;
    [FieldOffset(0xE0C)] public byte     stat_effvar;
    [FieldOffset(0xE0E)] public byte     stat_effect_hit_num;
    [FieldOffset(0xE10)] public byte     stat_magic_effect_ground;
    [FieldOffset(0xE11)] public byte     stat_magic_effect_water;
    [FieldOffset(0xE1A)] public short    stat_sound_hit_num;
    [FieldOffset(0xF74)] public byte     stat_info_mes_id;
    [FieldOffset(0xF75)] public byte     stat_live_mes_id;
    [FieldOffset(0xF78)] public nint     ptr_script_chunks;
    [FieldOffset(0xF7C)] public nint     ptr_script_data;
    [FieldOffset(0xF80)] public nint     ptr_base_stats;
    [FieldOffset(0xF84)] public nint     ptr_base_en_stats;
    [FieldOffset(0xF88)] public ChrLoot* loot;

    public bool stat_inv_physic_motion { readonly get { return stat_inv_motion.get_bit(0); } set { stat_inv_motion.set_bit(0, value); } }
    public bool stat_inv_magic_motion  { readonly get { return stat_inv_motion.get_bit(1); } set { stat_inv_motion.set_bit(1, value); } }
}
