// SPDX-License-Identifier: MIT

using Fahrenheit.FFX.Ids;

namespace Fahrenheit.FFX;

[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 0x68C0)]
public unsafe struct SaveData {
    [InlineArray(20)]
    public struct Name {
        private byte _b;
    }

    [InlineArray(18)]
    public struct NamesGroup {
        private Name _data;
    }

    [InlineArray(256)]
    public struct EventFlags {
        private byte _data;
    }

    [InlineArray(256)]
    public struct InventoryTypes {
        private T_XCommandId _data;
    }

    [InlineArray(256)]
    public struct InventoryCounts {
        private byte _data;
    }

    [InlineArray(16)]
    public struct ItemCheckField {
        private ushort _data;

        public bool get(int item_id) {
            int id = item_id & 0xFFF;
            return this[id >> 4].get_bit(id & 0xF);
        }

        public void set(int item_id, bool value) {
            int id = item_id & 0xFFF;
            this[id >> 4].set_bit(id & 0xF, value);
        }
    }

    [InlineArray(512)]
    public struct MonsterCaptureCounts {
        private byte _data;
    }

    [InlineArray(32)]
    public struct MonsterCheckField {
        private ushort _data;

        public bool get(int mon_id) {
            int id = mon_id & 0xFFF;
            return this[id >> 4].get_bit(id & 0xF);
        }

        public void set(int mon_id, bool value) {
            int id = mon_id & 0xFFF;
            this[id >> 4].set_bit(id & 0xF, value);
        }
    }

    [InlineArray(8)]
    public struct KeyItemsCheckField {
        private ushort _data;

        public bool get(int key_item_id) {
            int id = key_item_id & 0xFFF;
            return this[id >> 4].get_bit(id & 0xF);
        }

        public void set(int key_item_id, bool value) {
            int id = key_item_id & 0xFFF;
            this[id >> 4].set_bit(id & 0xF, value);
        }
    }

    [InlineArray(200)]
    public struct EquipmentArray {
        private Equipment _data;
    }

    [InlineArray(18)]
    public struct PlySaveArray {
        private PlySave _data;

        public PlySave tidus   => this[PlySaveId.PC_TIDUS];
        public PlySave yuna    => this[PlySaveId.PC_YUNA];
        public PlySave auron   => this[PlySaveId.PC_AURON];
        public PlySave kimahri => this[PlySaveId.PC_KIMAHRI];
        public PlySave wakka   => this[PlySaveId.PC_WAKKA];
        public PlySave lulu    => this[PlySaveId.PC_LULU];
        public PlySave rikku   => this[PlySaveId.PC_RIKKU];
        public PlySave seymour => this[PlySaveId.PC_SEYMOUR];
        public PlySave valefor => this[PlySaveId.PC_VALEFOR];
        public PlySave ifrit   => this[PlySaveId.PC_IFRIT];
        public PlySave ixion   => this[PlySaveId.PC_IXION];
        public PlySave shiva   => this[PlySaveId.PC_SHIVA];
        public PlySave bahamut => this[PlySaveId.PC_BAHAMUT];
        public PlySave anima   => this[PlySaveId.PC_ANIMA];
        public PlySave yojimbo => this[PlySaveId.PC_YOJIMBO];
        public PlySave cindy   => this[PlySaveId.PC_MAGUS1];
        public PlySave sandy   => this[PlySaveId.PC_MAGUS2];
        public PlySave mindy   => this[PlySaveId.PC_MAGUS3];
    }

    [InlineArray(22)]
    public struct PlyComCheckField {
        private ushort _data;

        public bool get(int com_id) {
            int id = com_id & 0xFFF;
            return this[id >> 4].get_bit(id & 0xF);
        }

        public void set(int com_id, bool value) {
            int id = com_id & 0xFFF;
            this[id >> 4].set_bit(id & 0xF, value);
        }
    }

    [InlineArray(18)]
    public struct PlyComCheckFields {
        private PlyComCheckField _data;

        public PlyComCheckField tidus   => this[PlySaveId.PC_TIDUS];
        public PlyComCheckField yuna    => this[PlySaveId.PC_YUNA];
        public PlyComCheckField auron   => this[PlySaveId.PC_AURON];
        public PlyComCheckField kimahri => this[PlySaveId.PC_KIMAHRI];
        public PlyComCheckField wakka   => this[PlySaveId.PC_WAKKA];
        public PlyComCheckField lulu    => this[PlySaveId.PC_LULU];
        public PlyComCheckField rikku   => this[PlySaveId.PC_RIKKU];
        public PlyComCheckField seymour => this[PlySaveId.PC_SEYMOUR];
        public PlyComCheckField valefor => this[PlySaveId.PC_VALEFOR];
        public PlyComCheckField ifrit   => this[PlySaveId.PC_IFRIT];
        public PlyComCheckField ixion   => this[PlySaveId.PC_IXION];
        public PlyComCheckField shiva   => this[PlySaveId.PC_SHIVA];
        public PlyComCheckField bahamut => this[PlySaveId.PC_BAHAMUT];
        public PlyComCheckField anima   => this[PlySaveId.PC_ANIMA];
        public PlyComCheckField yojimbo => this[PlySaveId.PC_YOJIMBO];
        public PlyComCheckField cindy   => this[PlySaveId.PC_MAGUS1];
        public PlyComCheckField sandy   => this[PlySaveId.PC_MAGUS2];
        public PlyComCheckField mindy   => this[PlySaveId.PC_MAGUS3];
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct JechtSphereData {
        public  ushort flags_spheres_seen;
        private byte   __0x2;
        public  byte   collected_amount;

        public bool seen_besaid_village     { readonly get { return flags_spheres_seen.get_bit(0); } set { flags_spheres_seen.set_bit(0, value); } }
        public bool seen_thunder_plains     { readonly get { return flags_spheres_seen.get_bit(1); } set { flags_spheres_seen.set_bit(1, value); } }
        public bool seen_moonflow           { readonly get { return flags_spheres_seen.get_bit(2); } set { flags_spheres_seen.set_bit(2, value); } }
        public bool seen_miihen             { readonly get { return flags_spheres_seen.get_bit(3); } set { flags_spheres_seen.set_bit(3, value); } }
        public bool seen_luca_stadium       { readonly get { return flags_spheres_seen.get_bit(4); } set { flags_spheres_seen.set_bit(4, value); } }
        public bool seen_ss_liki            { readonly get { return flags_spheres_seen.get_bit(5); } set { flags_spheres_seen.set_bit(5, value); } }
        public bool seen_mt_gagazet         { readonly get { return flags_spheres_seen.get_bit(6); } set { flags_spheres_seen.set_bit(6, value); } }
        public bool seen_macalania_woods    { readonly get { return flags_spheres_seen.get_bit(7); } set { flags_spheres_seen.set_bit(7, value); } }
        public bool seen_mushroom_rock_road { readonly get { return flags_spheres_seen.get_bit(8); } set { flags_spheres_seen.set_bit(8, value); } }
    }


    [FieldOffset(0x0)]    public       ushort          current_room_id;
    [FieldOffset(0x2)]    public       ushort          last_room_id;
    [FieldOffset(0x4)]    public       ushort          now_eventjump_map_no;
    [FieldOffset(0x6)]    public       ushort          last_eventjump_map_no;
    [FieldOffset(0x8)]    public       ushort          now_eventjump_map_id;
    [FieldOffset(0xA)]    public       ushort          last_eventjump_map_id;
    [FieldOffset(0xC)]    public       byte            current_spawnpoint;
    [FieldOffset(0xD)]    public       byte            last_spawnpoint;
    [FieldOffset(0xE)]    public       ushort          atel_save_dic_index;
    [FieldOffset(0x10)]   public       byte            atel_battle_scene_group;
    [FieldOffset(0x11)]   public       byte            fade_mode;
    [FieldOffset(0x12)]   public       byte            fade_time;
    [FieldOffset(0x13)]   public       byte            battle_status; // bitfield
    [FieldOffset(0x18)]   public fixed uint            flying_ship_pos[2]; // bitfield
    [FieldOffset(0x20)]   public       byte            atel_is_push_member;
    [FieldOffset(0x21)]   public fixed byte            atel_push_frontline[3];
    [FieldOffset(0x24)]   public       byte            atel_push_party;
    [FieldOffset(0x28)]   public       byte            is_cam_underwater;
    [FieldOffset(0x29)]   public       byte            is_map_underwater;
    [FieldOffset(0x2B)]   public       byte            tk_event_new_game;
    [FieldOffset(0x2C)]   public fixed uint            affection[8];
    [FieldOffset(0x4C)]   public fixed uint            affection_room_flags[20]; // bitfield
    [FieldOffset(0xB4)]   public       ushort          item_map_x; // meaning unknown
    [FieldOffset(0xB6)]   public       ushort          item_map_y; // meaning unknown
    [FieldOffset(0xB8)]   public       ushort          saved_current_spawnpoint; // Used when loading save
    [FieldOffset(0xBA)]   public       ushort          saved_current_room_id;    // Used when loading save
    [FieldOffset(0xBC)]   public       int             time;
    [FieldOffset(0xC0)]   public       ushort          saved_current_room_id2; // Never read?
    [FieldOffset(0xC2)]   public       ushort          saved_last_room_id; // Used when loading save
    [FieldOffset(0xC4)]   public       ushort          saved_now_eventjump_map_no;
    [FieldOffset(0xC6)]   public       ushort          saved_last_eventjump_map_no;
    [FieldOffset(0xC8)]   public       ushort          saved_now_eventjump_map_id;
    [FieldOffset(0xCA)]   public       ushort          saved_last_eventjump_map_id;
    [FieldOffset(0xCC)]   public       byte            saved_current_spawnpoint2; // Never read?
    [FieldOffset(0xCE)]   public       byte            saved_last_spawnpoint; // Used when loading save
    [FieldOffset(0xD1)]   public       byte            albhed_rikku;
    [FieldOffset(0xD2)]   public       byte            drop_shadow_mode;
    [FieldOffset(0xD4)]   public       ushort          atel_force_place_id_value;
    [FieldOffset(0xD6)]   public       byte            atel_force_place_id;
    [FieldOffset(0xD7)]   public       byte            atel_water_btl_effect;
    [FieldOffset(0xD8)]   public       uint            on_memory_movie_file_no;
    [FieldOffset(0xDC)]   public       uint            on_memory_movie_mode;
    [FieldOffset(0xE0)]   public       uint            on_memory_movie;
    [FieldOffset(0xE4)]   public       int             rand_encounter_modifiers;
    [FieldOffset(0xE8)]   public       ushort          btl_end_tag_always;
    [FieldOffset(0xEA)]   public       ushort          sphere_monitor;
    [FieldOffset(0x279)]  public       byte            progression_flags_calm_lands_quest;
    [FieldOffset(0x287)]  public       byte            progression_flags_belgemine_fight;
    [FieldOffset(0x28B)]  public       byte            progression_flags_monster_arena_unlock_quest;
    [FieldOffset(0x2F0)]  public       byte            progression_flags_energy_blast;
    [FieldOffset(0x300)]  public       byte            progression_flags_remiem_temple;
    [FieldOffset(0x337)]  public       byte            luca_controllable_character;
    [FieldOffset(0x3BE)]  public       byte            progression_flags_omega_ruins;
    [FieldOffset(0x3C0)]  public       byte            progression_flags_home;
    [FieldOffset(0x3F1)]  public       byte            progression_flags_thunder_plains;
    [FieldOffset(0x3FC)]  public       ushort          lightning_dodging_total_bolts;
    [FieldOffset(0x3FE)]  public       ushort          lightning_dodging_total_dodges;
    [FieldOffset(0x400)]  public       ushort          lightning_dodging_highest_consecutive_dodges;
    [FieldOffset(0x5EC)]  public       bool            soundtrack_type;
    [FieldOffset(0xBEC)]  public       ushort          story_progress;
    [FieldOffset(0xC38)]  public       bool            celestial_mirror_obtained;
    [FieldOffset(0xC39)]  public       byte            celestials_obtained; // bitfield
    [FieldOffset(0xC5C)]  public       byte            anima_seals_unlocked;
    [FieldOffset(0xC60)]  public       uint            current_airship_location;
    [FieldOffset(0xC6C)]  public       byte            celestials_half_powered; // bitfield
    [FieldOffset(0xC6D)]  public       byte            celestials_full_powered; // bitfield
    [FieldOffset(0xC7B)]  public       byte            optional_aeons_unlocked;
    [FieldOffset(0xC7C)]  public       JechtSphereData jecht_spheres;
    [FieldOffset(0xC81)]  public       ushort          unlocked_airship_destinations;
    [FieldOffset(0xC89)]  public       byte            completion_flags_dark_valefor;
    [FieldOffset(0xC8A)]  public       byte            completion_flags_dark_ifrit;
    [FieldOffset(0xC8B)]  public       byte            completion_flags_dark_ixion;
    [FieldOffset(0xC8C)]  public       byte            completion_flags_dark_shiva;
    [FieldOffset(0xC8D)]  public       byte            completion_flags_dark_bahamut;
    [FieldOffset(0xC8E)]  public       byte            completion_flags_dark_yojimbo;
    [FieldOffset(0xC8F)]  public       byte            completion_flags_dark_anima;
    [FieldOffset(0xC90)]  public       byte            completion_flags_dark_magus_sisters;
    [FieldOffset(0xC91)]  public       byte            penance_unlock_state;
    [FieldOffset(0xCD7)]  public fixed byte            battle_dialog_lines_seen[42];
    [FieldOffset(0x3D0C)] public       uint            config;
    [FieldOffset(0x3D10)] public       uint            unlocked_primers;
    [FieldOffset(0x3D14)] public       uint            battle_count;
    [FieldOffset(0x3D48)] public       uint            gil;
    [FieldOffset(0x3D58)] public fixed byte            party_frontline[3];
    [FieldOffset(0x3D5B)] public fixed byte            party_backline[17];
    [FieldOffset(0x3D6C)] public       LimitAbilityMap ability_map_limit;
    [FieldOffset(0x3DA4)] public       uint            yojimbo_compatibility;
    [FieldOffset(0x3DA8)] public       uint            yojimbo_type; // unknown size
    [FieldOffset(0x3DAC)] public       uint            tidus_limit_uses;
    [FieldOffset(0x3DB0)] public       uint            successful_rikku_steals;
    [FieldOffset(0x3DB4)] public       uint            bribe_gil_spent;

    [FieldOffset(0x3DCC)] public EventFlags           event_flags;
    [FieldOffset(0x3ECC)] public InventoryTypes       inventory_ids;
    [FieldOffset(0x40CC)] public InventoryCounts      inventory_counts;
    [FieldOffset(0x41CC)] public ItemCheckField       items_acquired;
    [FieldOffset(0x41EC)] public ItemCheckField       items_used;
    [FieldOffset(0x420C)] public MonsterCaptureCounts monsters_captured;
    [FieldOffset(0x440C)] public MonsterCheckField    monsters_seen;
    [FieldOffset(0x444C)] public MonsterCheckField    monsters_defeated;
    [FieldOffset(0x448C)] public KeyItemsCheckField   key_items;
    [FieldOffset(0x449C)] public EquipmentArray       equipment;
    [FieldOffset(0x55CC)] public PlySaveArray         ply_saves;
    [FieldOffset(0x6034)] public PlyComCheckFields    commands_used;
    [FieldOffset(0x634C)] public NamesGroup           character_names;

    public bool get_affection_room_gained(int room_id) {
        return room_id < 0x280 && affection_room_flags[room_id / 32].get_bit(room_id % 32);
    }

    public void set_affection_room_gained(int room_id, bool value) {
        if (room_id >= 0x280) return;
        affection_room_flags[room_id / 32].set_bit(room_id % 32, value);
    }

    public uint get_item_count(int item_id) {
        for (int i = 0; i < 256; i++) {
            if (inventory_ids[i] == item_id) return inventory_counts[i];
        }
        return 0;
    }

    public bool rand_encounters_no_fanfare  { readonly get { return rand_encounter_modifiers.get_bit( 8); } set { rand_encounter_modifiers.set_bit( 8, value); } }
    public bool rand_encounters_no_gameover { readonly get { return rand_encounter_modifiers.get_bit( 9); } set { rand_encounter_modifiers.set_bit( 9, value); } }
    public bool rand_encounters_no_music    { readonly get { return rand_encounter_modifiers.get_bit(10); } set { rand_encounter_modifiers.set_bit(10, value); } }

    public bool battle_status_sys       { readonly get { return battle_status.get_bit(1); } set { battle_status.set_bit(1, value); } }
    public bool battle_status_dummy_enc { readonly get { return battle_status.get_bit(2); } set { battle_status.set_bit(2, value); } }

    public bool config_stereo          { readonly get { return config.get_bit ( 0);    } set { config.set_bit ( 0, value);    } }
    public bool config_memory_cursor   { readonly get { return config.get_bit ( 1);    } set { config.set_bit ( 1, value);    } }
    public bool config_controller      { readonly get { return config.get_bit ( 2);    } set { config.set_bit ( 2, value);    } }
    public bool config_hiragana        { readonly get { return config.get_bit ( 3);    } set { config.set_bit ( 3, value);    } }
    public bool config_vibrate         { readonly get { return config.get_bit ( 4);    } set { config.set_bit ( 4, value);    } }
    public bool config_subtitles_names { readonly get { return config.get_bit ( 5);    } set { config.set_bit ( 5, value);    } }
    public bool config_show_map        { readonly get { return config.get_bit ( 6);    } set { config.set_bit ( 6, value);    } }
    public bool config_subtitles       { readonly get { return config.get_bit ( 9);    } set { config.set_bit ( 9, value);    } }
    public bool config_show_help       { readonly get { return config.get_bit (10);    } set { config.set_bit (10, value);    } }
    public bool config_short_aeons     { readonly get { return config.get_bit (11);    } set { config.set_bit (11, value);    } }
    public uint config_grid_type       { readonly get { return config.get_bits(14, 2); } set { config.set_bits(14, 2, value); } } // 0: Original, 1: Standard, 2: Expert, Default: Expert [citation needed]

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

    public bool has_unlocked_airship_destination_baaj_temple    { readonly get { return unlocked_airship_destinations.get_bit( 0); } set { unlocked_airship_destinations.set_bit( 0, value); } }
    public bool has_unlocked_airship_destination_sin            { readonly get { return unlocked_airship_destinations.get_bit( 1); } set { unlocked_airship_destinations.set_bit( 1, value); } }
    public bool has_unlocked_airship_destination_omega_ruins    { readonly get { return unlocked_airship_destinations.get_bit( 2); } set { unlocked_airship_destinations.set_bit( 2, value); } }
    public bool has_unlocked_airship_destination_highbridge     { readonly get { return unlocked_airship_destinations.get_bit( 3); } set { unlocked_airship_destinations.set_bit( 3, value); } }
    public bool has_unlocked_airship_destination_besaid_ruins_1 { readonly get { return unlocked_airship_destinations.get_bit( 4); } set { unlocked_airship_destinations.set_bit( 4, value); } }
    public bool has_unlocked_airship_destination_mushroom_rock  { readonly get { return unlocked_airship_destinations.get_bit( 5); } set { unlocked_airship_destinations.set_bit( 5, value); } }
    public bool has_unlocked_airship_destination_besaid_ruins_2 { readonly get { return unlocked_airship_destinations.get_bit( 6); } set { unlocked_airship_destinations.set_bit( 6, value); } }
    public bool has_unlocked_airship_destination_besaid_falls   { readonly get { return unlocked_airship_destinations.get_bit( 7); } set { unlocked_airship_destinations.set_bit( 7, value); } }
    public bool has_unlocked_airship_destination_miihen_ruins   { readonly get { return unlocked_airship_destinations.get_bit( 8); } set { unlocked_airship_destinations.set_bit( 8, value); } }
    public bool has_unlocked_airship_destination_battle_site    { readonly get { return unlocked_airship_destinations.get_bit( 9); } set { unlocked_airship_destinations.set_bit( 9, value); } }
    public bool has_unlocked_airship_destination_sanubia_sands  { readonly get { return unlocked_airship_destinations.get_bit(10); } set { unlocked_airship_destinations.set_bit(10, value); } }
    public bool has_unlocked_airship_destination_penance        { readonly get { return unlocked_airship_destinations.get_bit(11); } set { unlocked_airship_destinations.set_bit(11, value); } }
    
    public bool has_anima         { readonly get { return optional_aeons_unlocked.get_bit(0); } set { optional_aeons_unlocked.set_bit(0, value); } }
    public bool has_yojimbo       { readonly get { return optional_aeons_unlocked.get_bit(1); } set { optional_aeons_unlocked.set_bit(1, value); } }
    public bool has_magus_sisters { readonly get { return optional_aeons_unlocked.get_bit(2); } set { optional_aeons_unlocked.set_bit(2, value); } }
}
