// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX2;

[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 0x16660)]
public unsafe struct SaveData {
    public struct Name {
        private fixed byte _name[40];

        public byte* raw  { get { fixed (byte* temp = _name) return temp; } }
    }

    public struct LearnJobs {
        public fixed ushort jobs[30];

        public ushort gunner        { get { return jobs[ 1]; } set { jobs[ 1] = value; } }
        public ushort gun_mage      { get { return jobs[ 2]; } set { jobs[ 2] = value; } }
        public ushort alchemist     { get { return jobs[ 3]; } set { jobs[ 3] = value; } }
        public ushort warrior       { get { return jobs[ 4]; } set { jobs[ 4] = value; } }
        public ushort samurai       { get { return jobs[ 5]; } set { jobs[ 5] = value; } }
        public ushort dark_knight   { get { return jobs[ 6]; } set { jobs[ 6] = value; } }
        public ushort berserker     { get { return jobs[ 7]; } set { jobs[ 7] = value; } }
        public ushort songstress    { get { return jobs[ 8]; } set { jobs[ 8] = value; } }
        public ushort black_mage    { get { return jobs[ 9]; } set { jobs[ 9] = value; } }
        public ushort white_mage    { get { return jobs[10]; } set { jobs[10] = value; } }
        public ushort thief         { get { return jobs[11]; } set { jobs[11] = value; } }
        public ushort trainer       { get { return jobs[12]; } set { jobs[12] = value; } }
        public ushort lady_luck     { get { return jobs[13]; } set { jobs[13] = value; } }
        public ushort mascot        { get { return jobs[14]; } set { jobs[14] = value; } }
        public ushort special       { get { return jobs[15]; } set { jobs[15] = value; } }
        public ushort special_right { get { return jobs[16]; } set { jobs[16] = value; } } // Used by child 1
        public ushort special_left  { get { return jobs[17]; } set { jobs[17] = value; } } // Used by child 2

        public ushort psychic       { get { return jobs[28]; } set { jobs[28] = value; } }
        public ushort festivalist   { get { return jobs[29]; } set { jobs[29] = value; } }
    }

    [StructLayout(LayoutKind.Explicit, Pack = 4)]
    public struct CupData {
        [FieldOffset(0x0)]  public byte   standard_cup_entry_count;
        [FieldOffset(0x1)]  public byte   standard_cup_hard_entry_count;
        [FieldOffset(0x2)]  public byte   grand_cup_entry_count;
        [FieldOffset(0x3)]  public byte   grand_cup_hard_entry_count;
        [FieldOffset(0x4)]  public byte   chocobo_cup_entry_count;
        [FieldOffset(0x5)]  public byte   cactuar_cup_entry_count;
        [FieldOffset(0x6)]  public byte   youth_league_tournament_entry_count;
        [FieldOffset(0x7)]  public byte   aeon_cup_entry_count;
        [FieldOffset(0x8)]  public byte   fiend_world_cup_entry_count;
        [FieldOffset(0x9)]  public byte   farplane_cup_entry_count;
        [FieldOffset(0xEF)] public ushort unlocked_cups_1; // Bitfield of which fiend arena cups are unlocked
        [FieldOffset(0xF2)] public bool   skip_fiend_arena_intro;
        [FieldOffset(0xF5)] public ushort unlocked_cups_2;
        [FieldOffset(0xFC)] public bool   farplane_cup_unlocked; // For some reason its own field?

        public bool has_unlocked_standard_cup            { readonly get { return unlocked_cups_1.get_bit(0); } set { unlocked_cups_1.set_bit(0, value); } }
        public bool has_unlocked_standard_cup_hard       { readonly get { return unlocked_cups_1.get_bit(1); } set { unlocked_cups_1.set_bit(1, value); } }
        public bool has_unlocked_grand_cup               { readonly get { return unlocked_cups_1.get_bit(2); } set { unlocked_cups_1.set_bit(2, value); } }
        public bool has_unlocked_grand_cup_hard          { readonly get { return unlocked_cups_1.get_bit(3); } set { unlocked_cups_1.set_bit(3, value); } }
        public bool has_unlocked_chocobo_cup             { readonly get { return unlocked_cups_1.get_bit(4); } set { unlocked_cups_1.set_bit(4, value); } }
        public bool has_unlocked_cactuar_cup             { readonly get { return unlocked_cups_1.get_bit(5); } set { unlocked_cups_1.set_bit(5, value); } }
        public bool has_unlocked_youth_league_tournament { readonly get { return unlocked_cups_1.get_bit(6); } set { unlocked_cups_1.set_bit(6, value); } }
        public bool has_unlocked_aeon_cup                { readonly get { return unlocked_cups_1.get_bit(7); } set { unlocked_cups_1.set_bit(7, value); } }

        public bool has_unlocked_fiend_world_cup { readonly get { return unlocked_cups_2.get_bit(0); } set { unlocked_cups_2.set_bit(0, value); } }
        public bool has_unlocked_farplane_cup    { readonly get { return unlocked_cups_2.get_bit(1); } set { unlocked_cups_2.set_bit(1, value); } }
    }

    [InlineArray(0x17)]
    public struct PlySaveArray {
        private PlySave _data;
    }

    [InlineArray(8)]
    public struct FriendMonsterArray {
        private FriendMonster _data;
    }

    [InlineArray(0x17)]
    public struct NamesGroup {
        private Name _data;
    }

    [InlineArray(9)]
    public struct Learn {
        private LearnJobs _data;
    }

    [InlineArray(8)]
    public struct TrapPodTypes {
        private byte _data;
    }

    [InlineArray(8)]
    public struct TaleBonus {
        private byte _data;
    }

    [InlineArray(8)]
    public struct TaleProgression {
        private byte _data;
    }

    [InlineArray(8)]
    public struct CapturedCreatureIds {
        private uint _data;
    }

    [FieldOffset(0x0)]     public ushort                 current_room_id;
    [FieldOffset(0x2)]     public ushort                 last_room_id;
    [FieldOffset(0x4)]     public ushort                 now_eventjump_map_no;
    [FieldOffset(0x6)]     public ushort                 last_eventjump_map_no;
    [FieldOffset(0x8)]     public ushort                 now_eventjump_map_id;
    [FieldOffset(0xA)]     public ushort                 last_eventjump_map_id;
    [FieldOffset(0xC)]     public byte                   current_spawnpoint;
    [FieldOffset(0xD)]     public byte                   last_spawnpoint;
    [FieldOffset(0xE)]     public ushort                 atel_save_dic_index;
    [FieldOffset(0x10)]    public byte                   atel_battle_scene_group;
    [FieldOffset(0x11)]    public byte                   fade_mode;
    [FieldOffset(0x12)]    public byte                   fade_time;
    [FieldOffset(0x13)]    public byte                   battle_status;
    [FieldOffset(0x18)]    public InlineArray2<uint>     flying_ship_pos; // bitfield
    [FieldOffset(0x20)]    public byte                   atel_is_push_member;
    [FieldOffset(0x21)]    public InlineArray3<byte>     atel_push_frontline;
    [FieldOffset(0x24)]    public byte                   atel_push_party;
    [FieldOffset(0x28)]    public byte                   is_cam_underwater;
    [FieldOffset(0x29)]    public byte                   is_map_underwater;
    [FieldOffset(0x2B)]    public byte                   tk_event_new_game;
    [FieldOffset(0x2C)]    public InlineArray8<uint>     affection;
    [FieldOffset(0x4C)]    public InlineArray20<uint>    affection_room_flags;
    [FieldOffset(0xB4)]    public ushort                 item_map_x;
    [FieldOffset(0xB6)]    public ushort                 item_map_y;
    [FieldOffset(0xBC)]    public int                    time;
    [FieldOffset(0xD1)]    public bool                   albhed_rikku;
    [FieldOffset(0xD2)]    public byte                   drop_shadow_mode;
    [FieldOffset(0xD4)]    public ushort                 atel_force_place_id_value;
    [FieldOffset(0xD6)]    public bool                   atel_force_place_id;
    [FieldOffset(0xD7)]    public byte                   atel_water_btl_effect;
    [FieldOffset(0xD8)]    public uint                   on_memory_movie_file_no;
    [FieldOffset(0xDC)]    public uint                   on_memory_movie_mode;
    [FieldOffset(0xE0)]    public uint                   on_memory_movie;
    [FieldOffset(0xE4)]    public int                    rand_encounter_modifiers;
    [FieldOffset(0xE8)]    public ushort                 btl_end_tag_always;
    [FieldOffset(0xEA)]    public ushort                 sphere_monitor;
    [FieldOffset(0x11C)]   public ushort                 event_skip_room;
    [FieldOffset(0x11E)]   public ushort                 event_skip_spawnpoint;
    [FieldOffset(0x124)]   public ushort                 event_skip_flag;
    [FieldOffset(0x126)]   public ushort                 event_skip_menu;
    [FieldOffset(0x128)]   public uint                   light_id;
    [FieldOffset(0x12C)]   public uint                   user_timer_upper;  // seconds
    [FieldOffset(0x130)]   public uint                   user_timer_lower;  // milliseconds
    [FieldOffset(0x134)]   public uint                   user_timer_status; // bit 1: count up, bit 2: count down, bit 3: stop?
    [FieldOffset(0x138)]   public uint                   voice_flag_count_all;
    [FieldOffset(0x13C)]   public uint                   voice_flag_crc;
    [FieldOffset(0x140)]   public uint                   voice_flag_2;  // meaning unknown
    [FieldOffset(0x148)]   public uint                   save_clear_counter;
    [FieldOffset(0x1EC)]   public byte                   scenario_list;
    [FieldOffset(0x44D)]   public CupData                fiend_arena_cup_data;
    [FieldOffset(0xBEC)]   public ushort                 story_progress;
    [FieldOffset(0xD60)]   public uint                   experiment_attack_level; // Experiment Boss
    [FieldOffset(0xD64)]   public uint                   experiment_defense_level;
    [FieldOffset(0xD68)]   public uint                   experiment_special_level;
    [FieldOffset(0x10FC)]  public byte                   trap_pod_count;
    [FieldOffset(0x10FD)]  public TrapPodTypes           trap_pods; // Type of each trap pod currently owned (S/M/L/SP)
    [FieldOffset(0x1105)]  public TaleBonus              creature_tale_bonus;
    [FieldOffset(0x110D)]  public TaleProgression        creature_tale_progression;
    [FieldOffset(0x114C)]  public byte                   current_chapter;
    [FieldOffset(0x1188)]  public CapturedCreatureIds    creature_ids; // IDs of current captured creatures
    [FieldOffset(0x11B1)]  public byte                   recruited_creature_count;
    [FieldOffset(0x21EC)]  public InlineArray16384<byte> voice_flags; // bitfield of heard voicelines?
    [FieldOffset(0x61F0)]  public uint                   result_card;
    [FieldOffset(0x6FD0)]  public uint                   walk_map;
    // start of btl_party (ffx_ps2\ffx2\master\jppc\battle\kernel\party.h)
    [FieldOffset(0x77D0)]  public uint                   config;
    [FieldOffset(0x77D4)]  public uint                   unlocked_primers;
    [FieldOffset(0x77D8)]  public uint                   gil;
    [FieldOffset(0x77DC)]  public uint                   play_time;   // from party.h, always 0?
    [FieldOffset(0x77E0)]  public uint                   battle_time; // from party.h, always 0?
    [FieldOffset(0x77E4)]  public uint                   battle_count;
    [FieldOffset(0x77E8)]  public InlineArray3<byte>     party;
    [FieldOffset(0x77EB)]  public byte                   atb_speed; // 0: Slow, 1: Normal, 2: Fast
    [FieldOffset(0x77EC)]  public InlineArray8<ushort>   item_type;
    [FieldOffset(0x77FC)]  public InlineArray8<byte>     item_num;
    [FieldOffset(0x7804)]  public InlineArray2<int>      plate; // bitfield
    [FieldOffset(0x780C)]  public InlineArray30<byte>    dre_sphere;
    [FieldOffset(0x782C)]  public int                    reserve5;
    // end of btl_party
    [FieldOffset(0x7840)]  public InlineArray128<byte> event_flag;

    [FieldOffset(0x7940)]  public InlineArray68<ushort> inventory_ids;
    [FieldOffset(0x7B40)]  public InlineArray68<byte>   inventory_counts;
    [FieldOffset(0x7C40)]  public ushort                inventory_check; // bitfield of unknown size
    [FieldOffset(0x7C60)]  public byte                  inventory_use;   // unused?

    [FieldOffset(0x7C80)]  public InlineArray128<ushort> accessory_ids;
    [FieldOffset(0x7D80)]  public InlineArray128<byte>   accessory_counts;
    [FieldOffset(0x7E00)]  public byte                   accessory_check; // unused?
    [FieldOffset(0x7E10)]  public byte                   accessory_use;   // unused?

    [FieldOffset(0x8020)]  public InlineArray32<ushort> monster_meet;     // bitfield
    [FieldOffset(0x8060)]  public InlineArray32<ushort> monster_defeat;   // bitfield
    [FieldOffset(0x80A0)]  public InlineArray32<ushort> monster_oversoul; // bitfield

    [FieldOffset(0x8120)]  public InlineArray8<ushort> important_items;
    [FieldOffset(0x81B0)]  public PlySaveArray         ply_saves;
    [FieldOffset(0x8D30)]  public byte                 ap_0x3; // 2d array [chr_id][ply_command_id] (if ability_id is 0x3XXX and XXX < 0x250)
    [FieldOffset(0x91D0)]  public byte                 ap_0x8; // 2d array [chr_id][ability_id] (if ability_id is 0x8XXX and XXX < 0x100)
    [FieldOffset(0xB114)]  public uint                 save_crc;
    [FieldOffset(0xC8D0)]  public Learn                learn; // 2d array. [chr_id][job_id]
    [FieldOffset(0xCAEC)]  public NamesGroup           character_names;
    [FieldOffset(0xCE84)]  public NamesGroup           character_names_default; // Only used for captured monsters?
    [FieldOffset(0xD21C)]  public byte                 dress_up_count; // chr_id * 0x5a + ??? * 0x1e + job_id? (uses of each job per character?)
    [FieldOffset(0xD548)]  public InlineArray14<byte>  extend_party;
    [FieldOffset(0xD570)]  public FriendMonsterArray   friend_monster;

    // Last Mission
    [FieldOffset(0x14730)] public byte dgn_save_data;
    [FieldOffset(0x14840)] public uint lm_gil;

    public bool get_met_monster(int monster_id) {
        return monster_id < 0x200 && monster_meet[monster_id / 16].get_bit(monster_id % 16);
    }

    public void set_met_monster(int monster_id, bool value) {
        if (monster_id >= 0x200) return;
        monster_meet[monster_id / 16].set_bit(monster_id % 16, value);
    }

    public bool get_defeated_monster(int monster_id) {
        return monster_id < 0x200 && monster_defeat[monster_id / 16].get_bit(monster_id % 16);
    }

    public void set_defeated_monster(int monster_id, bool value) {
        if (monster_id >= 0x200) return;
        monster_defeat[monster_id / 16].set_bit(monster_id % 16, value);
    }

    public bool get_oversoul_monster(int monster_id) {
        return monster_id < 0x200 && monster_oversoul[monster_id / 16].get_bit(monster_id % 16);
    }

    public void set_oversoul_monster(int monster_id, bool value) {
        if (monster_id >= 0x200) return;
        monster_oversoul[monster_id / 16].set_bit(monster_id % 16, value);
    }

    public bool get_affection_room_gained(int room_id) {
        return room_id < 0x280 && affection_room_flags[room_id / 32].get_bit(room_id % 32);
    }

    public void set_affection_room_gained(int room_id, bool value) {
        if (room_id >= 0x280) return;
        affection_room_flags[room_id / 32].set_bit(room_id % 32, value);
    }

    public uint get_item_count(int item_id) {
        for (int i = 0; i < 0x44; i++) {
            if (inventory_ids[i] == item_id) return inventory_counts[i];
        }
        return 0;
    }

    public bool has_key_item(int key_item_id) {
        return key_item_id < 0x80 && important_items[key_item_id / 16].get_bit(key_item_id % 16);
    }

    public void set_key_item(int key_item_id, bool value) {
        if (key_item_id >= 0x80) return;
        important_items[key_item_id / 16].set_bit(key_item_id % 16, value);
    }

    // untested
    public bool rand_encounters_no_fanfare  { readonly get { return rand_encounter_modifiers.get_bit( 8); } set { rand_encounter_modifiers.set_bit( 8, value); } }
    public bool rand_encounters_no_gameover { readonly get { return rand_encounter_modifiers.get_bit( 9); } set { rand_encounter_modifiers.set_bit( 9, value); } }
    public bool rand_encounters_no_music    { readonly get { return rand_encounter_modifiers.get_bit(10); } set { rand_encounter_modifiers.set_bit(10, value); } }

    public bool battle_status_sys       { readonly get { return battle_status.get_bit(1); } set { battle_status.set_bit(1, value); } }
    public bool battle_status_dummy_enc { readonly get { return battle_status.get_bit(2); } set { battle_status.set_bit(2, value); } }

    public bool config_stereo          { readonly get { return config.get_bit ( 0);    } set { config.set_bit ( 0, value);    } }
    public bool config_cursor_save     { readonly get { return config.get_bit ( 1);    } set { config.set_bit ( 1, value);    } }
    public bool config_controller      { readonly get { return config.get_bit ( 2);    } set { config.set_bit ( 2, value);    } }
    public bool config_hiragana        { readonly get { return config.get_bit ( 3);    } set { config.set_bit ( 3, value);    } }
    public bool config_vibrate         { readonly get { return config.get_bit ( 4);    } set { config.set_bit ( 4, value);    } }
    public bool config_credit_name     { readonly get { return config.get_bit ( 5);    } set { config.set_bit ( 5, value);    } }
    public bool config_guide_map       { readonly get { return config.get_bit ( 6);    } set { config.set_bit ( 6, value);    } }
    public bool config_stereo2         { readonly get { return config.get_bit ( 7);    } set { config.set_bit ( 7, value);    } } // from party.h
    public bool config_voice           { readonly get { return config.get_bit ( 8);    } set { config.set_bit ( 8, value);    } } // from party.h
    public bool config_subtitle        { readonly get { return config.get_bit ( 9);    } set { config.set_bit ( 9, value);    } }
    public bool config_help            { readonly get { return config.get_bit (10);    } set { config.set_bit (10, value);    } } // from party.h
    public bool config_atb_wait        { readonly get { return config.get_bit (11);    } set { config.set_bit (11, value);    } }
    public bool config_direction       { readonly get { return config.get_bit (12);    } set { config.set_bit (12, value);    } } // from party.h
    public bool config_hdd             { readonly get { return config.get_bit (13);    } set { config.set_bit (13, value);    } } // from party.h
    public uint config_change_effect   { readonly get { return config.get_bits(14, 2); } set { config.set_bits(14, 2, value); } }
    public bool config_english         { readonly get { return config.get_bit (15);    } set { config.set_bit (15, value);    } } // from party.h, unused? (overlaps with config_change_effect)
    public bool config_battle_subtitle { readonly get { return config.get_bit (16);    } set { config.set_bit (16, value);    } }

    public bool has_unlocked_primer_1  { readonly get { return unlocked_primers.get_bit( 0); } set { unlocked_primers.set_bit( 0, value); } }
    public bool has_unlocked_primer_2  { readonly get { return unlocked_primers.get_bit( 1); } set { unlocked_primers.set_bit( 1, value); } }
    public bool has_unlocked_primer_3  { readonly get { return unlocked_primers.get_bit( 2); } set { unlocked_primers.set_bit( 2, value); } }
    public bool has_unlocked_primer_4  { readonly get { return unlocked_primers.get_bit( 3); } set { unlocked_primers.set_bit( 3, value); } }
    public bool has_unlocked_primer_5  { readonly get { return unlocked_primers.get_bit( 4); } set { unlocked_primers.set_bit( 4, value); } }
    public bool has_unlocked_primer_6  { readonly get { return unlocked_primers.get_bit( 5); } set { unlocked_primers.set_bit( 5, value); } }
    public bool has_unlocked_primer_7  { readonly get { return unlocked_primers.get_bit( 6); } set { unlocked_primers.set_bit( 6, value); } }
    public bool has_unlocked_primer_8  { readonly get { return unlocked_primers.get_bit( 7); } set { unlocked_primers.set_bit( 7, value); } }
    public bool has_unlocked_primer_9  { readonly get { return unlocked_primers.get_bit( 8); } set { unlocked_primers.set_bit( 8, value); } }
    public bool has_unlocked_primer_10 { readonly get { return unlocked_primers.get_bit( 9); } set { unlocked_primers.set_bit( 9, value); } }
    public bool has_unlocked_primer_11 { readonly get { return unlocked_primers.get_bit(10); } set { unlocked_primers.set_bit(10, value); } }
    public bool has_unlocked_primer_12 { readonly get { return unlocked_primers.get_bit(11); } set { unlocked_primers.set_bit(11, value); } }
    public bool has_unlocked_primer_13 { readonly get { return unlocked_primers.get_bit(12); } set { unlocked_primers.set_bit(12, value); } }
    public bool has_unlocked_primer_14 { readonly get { return unlocked_primers.get_bit(13); } set { unlocked_primers.set_bit(13, value); } }
    public bool has_unlocked_primer_15 { readonly get { return unlocked_primers.get_bit(14); } set { unlocked_primers.set_bit(14, value); } }
    public bool has_unlocked_primer_16 { readonly get { return unlocked_primers.get_bit(15); } set { unlocked_primers.set_bit(15, value); } }
    public bool has_unlocked_primer_17 { readonly get { return unlocked_primers.get_bit(16); } set { unlocked_primers.set_bit(16, value); } }
    public bool has_unlocked_primer_18 { readonly get { return unlocked_primers.get_bit(17); } set { unlocked_primers.set_bit(17, value); } }
    public bool has_unlocked_primer_19 { readonly get { return unlocked_primers.get_bit(18); } set { unlocked_primers.set_bit(18, value); } }
    public bool has_unlocked_primer_20 { readonly get { return unlocked_primers.get_bit(19); } set { unlocked_primers.set_bit(19, value); } }
    public bool has_unlocked_primer_21 { readonly get { return unlocked_primers.get_bit(20); } set { unlocked_primers.set_bit(20, value); } }
    public bool has_unlocked_primer_22 { readonly get { return unlocked_primers.get_bit(21); } set { unlocked_primers.set_bit(21, value); } }
    public bool has_unlocked_primer_23 { readonly get { return unlocked_primers.get_bit(22); } set { unlocked_primers.set_bit(22, value); } }
    public bool has_unlocked_primer_24 { readonly get { return unlocked_primers.get_bit(23); } set { unlocked_primers.set_bit(23, value); } }
    public bool has_unlocked_primer_25 { readonly get { return unlocked_primers.get_bit(24); } set { unlocked_primers.set_bit(24, value); } }
    public bool has_unlocked_primer_26 { readonly get { return unlocked_primers.get_bit(25); } set { unlocked_primers.set_bit(25, value); } }
}
