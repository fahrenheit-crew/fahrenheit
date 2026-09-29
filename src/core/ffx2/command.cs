// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

// ffx2/master/jppc/battle/kernel/command.h 
// Switch release of FFX/X-2 HD

namespace Fahrenheit.FFX2;

[StructLayout(LayoutKind.Sequential)]
public struct Command {
    public ExcelTextOffset name;
    public ExcelTextOffset help;

    public ushort anim_1;
    public ushort anim_2;
    public byte   caster_anim;

    public MenuFlags flags_menu;
    public byte      sub_menu_cat2;
    public byte      sub_menu_cat;

    public TargetFlags flags_target;
    public MiscFlags   flags_misc;

    private uint reserve1;

    public DamageFlags       flags_damage;
    public PartyPreviewFlags party_preview;

    public ushort cost_atb;
    public ushort cost_cast;
    public byte   cost_mp;

    public DamageClass damage_class;
    public byte        dmg_formula;
    public byte        crit_bonus;
    public byte        accuracy;
    public byte        power;
    public byte        hit_count;
    public byte        shatter_chance;

    public ElementFlags element;

    public StatusMap          status_inflict1;
    public StatusMap2         status_inflict2;
    public StatusDurationMap2 status_time;

    public byte icon;

    public FiendSpecies species_effectiveness; // Which species to target for double damage

    public byte          magic_cancel;
    public byte          ordering_idx1;
    public T_X2CommandId blue_bullet;
    public ushort        ordering_idx2;

    private uint reserve2; // Seems related to cast animation?

    public byte btl_sequence;
    public byte get_ap;

    public struct MenuFlags {
        private byte _flags;

        public  bool is_top_level_in_menu { get { return _flags.get_bit(0); } set { _flags.set_bit(0, value); } }
        private bool _menu_f4             { get { return _flags.get_bit(3); } set { _flags.set_bit(3, value); } }
        public  bool opens_sub_menu       { get { return _flags.get_bit(4); } set { _flags.set_bit(4, value); } }
    }

    public struct TargetFlags {
        private uint _flags;

        public bool can_target        { readonly get { return _flags.get_bit( 0); } set { _flags.set_bit( 0, value); } }
        public bool targets_enemies   { readonly get { return _flags.get_bit( 1); } set { _flags.set_bit( 1, value); } }
        public bool targets_multiple  { readonly get { return _flags.get_bit( 2); } set { _flags.set_bit( 2, value); } }
        public bool targets_self_only { readonly get { return _flags.get_bit( 3); } set { _flags.set_bit( 3, value); } }
        public bool targets_move      { readonly get { return _flags.get_bit( 4); } set { _flags.set_bit( 4, value); } }
        public bool targets_team      { readonly get { return _flags.get_bit( 5); } set { _flags.set_bit( 5, value); } }
        public bool targets_dead      { readonly get { return _flags.get_bit( 6); } set { _flags.set_bit( 6, value); } }
        public bool targets_all       { readonly get { return _flags.get_bit( 7); } set { _flags.set_bit( 7, value); } }
        public bool targets_right     { readonly get { return _flags.get_bit( 8); } set { _flags.set_bit( 8, value); } }
        public bool targets_left      { readonly get { return _flags.get_bit( 9); } set { _flags.set_bit( 9, value); } }
        public bool targets_ranged    { readonly get { return _flags.get_bit(10); } set { _flags.set_bit(10, value); } }
    }

    public struct MiscFlags {
        private uint _flags;

        public bool is_usable_outside_combat  { readonly get { return _flags.get_bit ( 0);    } set { _flags.set_bit ( 0,    value); } }
        public bool is_usable_in_combat       { readonly get { return _flags.get_bit ( 1);    } set { _flags.set_bit ( 1,    value); } }
        public bool display_move_name         { readonly get { return _flags.get_bit ( 2);    } set { _flags.set_bit ( 2,    value); } }
        public uint accuracy_formula          { readonly get { return _flags.get_bits( 3, 3); } set { _flags.set_bits( 3, 3, value); } }
        public bool is_affected_by_darkness   { readonly get { return _flags.get_bit ( 6);    } set { _flags.set_bit ( 6,    value); } }
        public bool is_affected_by_reflect    { readonly get { return _flags.get_bit ( 7);    } set { _flags.set_bit ( 7,    value); } }
        public bool absorbs_dmg               { readonly get { return _flags.get_bit ( 8);    } set { _flags.set_bit ( 8,    value); } }
        public bool steals_item               { readonly get { return _flags.get_bit ( 9);    } set { _flags.set_bit ( 9,    value); } }
        public bool _hit_effect_change        { readonly get { return _flags.get_bit (10);    } set { _flags.set_bit (10,    value); } }
        public bool uses_dancing_cast_anim    { readonly get { return _flags.get_bit (11);    } set { _flags.set_bit (11,    value); } }
        public bool inflicts_delay_weak       { readonly get { return _flags.get_bit (12);    } set { _flags.set_bit (12,    value); } }
        public bool inflicts_delay_strong     { readonly get { return _flags.get_bit (13);    } set { _flags.set_bit (13,    value); } }
        public bool targets_randomly          { readonly get { return _flags.get_bit (14);    } set { _flags.set_bit (14,    value); } }
        public bool is_affected_by_silence    { readonly get { return _flags.get_bit (15);    } set { _flags.set_bit (15,    value); } }
        public bool uses_character_properties { readonly get { return _flags.get_bit (16);    } set { _flags.set_bit (16,    value); } }
        public bool destroys_user             { readonly get { return _flags.get_bit (17);    } set { _flags.set_bit (17,    value); } }
        public bool misses_living_targets     { readonly get { return _flags.get_bit (18);    } set { _flags.set_bit (18,    value); } }
        public bool is_reels_result           { readonly get { return _flags.get_bit (19);    } set { _flags.set_bit (19,    value); } }
        public bool is_mix_result             { readonly get { return _flags.get_bit (20);    } set { _flags.set_bit (20,    value); } }
        public bool show_user_casting_effects { readonly get { return _flags.get_bit (21);    } set { _flags.set_bit (21,    value); } }
        public bool disables_neck_movement    { readonly get { return _flags.get_bit (22);    } set { _flags.set_bit (22,    value); } }
        public bool _vanish_cursor            { readonly get { return _flags.get_bit (23);    } set { _flags.set_bit (23,    value); } }
        public bool enables_wait_mode         { readonly get { return _flags.get_bit (24);    } set { _flags.set_bit (24,    value); } }
        public bool halve_music_volume        { readonly get { return _flags.get_bit (25);    } set { _flags.set_bit (25,    value); } }
        public bool is_bribe                  { readonly get { return _flags.get_bit (26);    } set { _flags.set_bit (26,    value); } }
        public bool should_run_away           { readonly get { return _flags.get_bit (27);    } set { _flags.set_bit (27,    value); } }
        public bool sacrifices_hp             { readonly get { return _flags.get_bit (28);    } set { _flags.set_bit (28,    value); } } // Dark Knight's "Darkness"
        public bool can_use_when_confused     { readonly get { return _flags.get_bit (29);    } set { _flags.set_bit (29,    value); } }
        public bool _non_damage               { readonly get { return _flags.get_bit (30);    } set { _flags.set_bit (30,    value); } }
        public bool _non_damage_perfect       { readonly get { return _flags.get_bit (31);    } set { _flags.set_bit (31,    value); } }
    }

    public struct DamageFlags {
        private uint _flags;

        public bool deals_physical_damage      { readonly get { return _flags.get_bit(0); } set { _flags.set_bit(0, value); } }
        public bool deals_magical_damage       { readonly get { return _flags.get_bit(1); } set { _flags.set_bit(1, value); } }
        public bool can_crit                   { readonly get { return _flags.get_bit(2); } set { _flags.set_bit(2, value); } }
        public bool gives_crit_bonus           { readonly get { return _flags.get_bit(3); } set { _flags.set_bit(3, value); } }
        public bool is_heal                    { readonly get { return _flags.get_bit(4); } set { _flags.set_bit(4, value); } }
        public bool is_cleanse                 { readonly get { return _flags.get_bit(5); } set { _flags.set_bit(5, value); } }
        public bool ignores_break_damage_limit { readonly get { return _flags.get_bit(6); } set { _flags.set_bit(6, value); } }
        public bool innate_break_damage_limit  { readonly get { return _flags.get_bit(7); } set { _flags.set_bit(7, value); } }
        public bool steals_gil                 { readonly get { return _flags.get_bit(8); } set { _flags.set_bit(8, value); } }
    }

    public struct PartyPreviewFlags {
        private ushort _flags;

        public bool enabled { readonly get { return _flags.get_bit(0); } set { _flags.set_bit(0, value); } }
        public bool mp      { readonly get { return _flags.get_bit(1); } set { _flags.set_bit(1, value); } }
        public bool status  { readonly get { return _flags.get_bit(2); } set { _flags.set_bit(2, value); } }
        public bool hp      { readonly get { return _flags.get_bit(6); } set { _flags.set_bit(6, value); } }
    }
}

[StructLayout(LayoutKind.Sequential)]
public struct PCommandData {
    public ushort    ap;
    public T_X2JobId job_use;

    private ushort reserve3;
}

[StructLayout(LayoutKind.Sequential)]
public struct MCommandData {
    public ushort ap;
}

[StructLayout(LayoutKind.Sequential)]
public struct ItemData {
    public byte element;
    public byte level;
    public uint price;

    // Creature Data
    private byte reserve3;

    public byte            feed_amount;
    public ushort          ability_to_learn;
    public FeedStatChanges feed_stats;
}

[StructLayout(LayoutKind.Sequential)]
public struct PCommand {
    public Command      command;
    public PCommandData command_pdata;
}

[StructLayout(LayoutKind.Sequential)]
public struct MCommand {
    public Command      command;
    public MCommandData command_mdata;
}

[StructLayout(LayoutKind.Sequential)]
public struct Item {
    public Command  command;
    public ItemData item_data;
}
