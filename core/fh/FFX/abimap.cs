// SPDX-License-Identifier: MIT

namespace Fahrenheit.FFX;

[InlineArray(3)]
public struct AbilityMap {
    private uint _u;

    public bool has_attack          { readonly get { return this[0].get_bit( 0); } set { this[0].set_bit( 0, value); } }
    public bool has_item            { readonly get { return this[0].get_bit( 1); } set { this[0].set_bit( 1, value); } }
    public bool has_switch          { readonly get { return this[0].get_bit( 2); } set { this[0].set_bit( 2, value); } }
    public bool has_escape          { readonly get { return this[0].get_bit( 3); } set { this[0].set_bit( 3, value); } }
    public bool has_weapon_change   { readonly get { return this[0].get_bit( 4); } set { this[0].set_bit( 4, value); } }
    public bool has_armor_change    { readonly get { return this[0].get_bit( 5); } set { this[0].set_bit( 5, value); } }
    public bool has_delay_attack    { readonly get { return this[0].get_bit( 6); } set { this[0].set_bit( 6, value); } }
    public bool has_delay_buster    { readonly get { return this[0].get_bit( 7); } set { this[0].set_bit( 7, value); } }
    public bool has_sleep_attack    { readonly get { return this[0].get_bit( 8); } set { this[0].set_bit( 8, value); } }
    public bool has_silence_attack  { readonly get { return this[0].get_bit( 9); } set { this[0].set_bit( 9, value); } }
    public bool has_dark_attack     { readonly get { return this[0].get_bit(10); } set { this[0].set_bit(10, value); } }
    public bool has_zombie_attack   { readonly get { return this[0].get_bit(11); } set { this[0].set_bit(11, value); } }
    public bool has_sleep_buster    { readonly get { return this[0].get_bit(12); } set { this[0].set_bit(12, value); } }
    public bool has_silence_buster  { readonly get { return this[0].get_bit(13); } set { this[0].set_bit(13, value); } }
    public bool has_dark_buster     { readonly get { return this[0].get_bit(14); } set { this[0].set_bit(14, value); } }
    public bool has_triple_foul     { readonly get { return this[0].get_bit(15); } set { this[0].set_bit(15, value); } }

    public bool has_power_break     { readonly get { return this[0].get_bit(16); } set { this[0].set_bit(16, value); } }
    public bool has_magic_break     { readonly get { return this[0].get_bit(17); } set { this[0].set_bit(17, value); } }
    public bool has_armor_break     { readonly get { return this[0].get_bit(18); } set { this[0].set_bit(18, value); } }
    public bool has_mental_break    { readonly get { return this[0].get_bit(19); } set { this[0].set_bit(19, value); } }
    public bool has_mug             { readonly get { return this[0].get_bit(20); } set { this[0].set_bit(20, value); } }
    public bool has_quick_hit       { readonly get { return this[0].get_bit(21); } set { this[0].set_bit(21, value); } }
    public bool has_steal           { readonly get { return this[0].get_bit(22); } set { this[0].set_bit(22, value); } }
    public bool has_use             { readonly get { return this[0].get_bit(23); } set { this[0].set_bit(23, value); } }
    public bool has_flee            { readonly get { return this[0].get_bit(24); } set { this[0].set_bit(24, value); } }
    public bool has_pray            { readonly get { return this[0].get_bit(25); } set { this[0].set_bit(25, value); } }
    public bool has_cheer           { readonly get { return this[0].get_bit(26); } set { this[0].set_bit(26, value); } }
    public bool has_aim             { readonly get { return this[0].get_bit(27); } set { this[0].set_bit(27, value); } }
    public bool has_focus           { readonly get { return this[0].get_bit(28); } set { this[0].set_bit(28, value); } }
    public bool has_reflex          { readonly get { return this[0].get_bit(29); } set { this[0].set_bit(29, value); } }
    public bool has_luck            { readonly get { return this[0].get_bit(30); } set { this[0].set_bit(30, value); } }
    public bool has_jinx            { readonly get { return this[0].get_bit(31); } set { this[0].set_bit(31, value); } }

    public bool has_lancet          { readonly get { return this[1].get_bit( 0); } set { this[1].set_bit( 0, value); } }
    public bool has_unused          { readonly get { return this[1].get_bit( 1); } set { this[1].set_bit( 1, value); } }
    public bool has_guard           { readonly get { return this[1].get_bit( 2); } set { this[1].set_bit( 2, value); } }
    public bool has_sentinel        { readonly get { return this[1].get_bit( 3); } set { this[1].set_bit( 3, value); } }
    public bool has_spare_change    { readonly get { return this[1].get_bit( 4); } set { this[1].set_bit( 4, value); } }
    public bool has_threaten        { readonly get { return this[1].get_bit( 5); } set { this[1].set_bit( 5, value); } }
    public bool has_provoke         { readonly get { return this[1].get_bit( 6); } set { this[1].set_bit( 6, value); } }
    public bool has_entrust         { readonly get { return this[1].get_bit( 7); } set { this[1].set_bit( 7, value); } }
    public bool has_copycat         { readonly get { return this[1].get_bit( 8); } set { this[1].set_bit( 8, value); } }
    public bool has_doublecast      { readonly get { return this[1].get_bit( 9); } set { this[1].set_bit( 9, value); } }
    public bool has_bribe           { readonly get { return this[1].get_bit(10); } set { this[1].set_bit(10, value); } }
    public bool has_cure            { readonly get { return this[1].get_bit(11); } set { this[1].set_bit(11, value); } }
    public bool has_cura            { readonly get { return this[1].get_bit(12); } set { this[1].set_bit(12, value); } }
    public bool has_curaga          { readonly get { return this[1].get_bit(13); } set { this[1].set_bit(13, value); } }
    public bool has_nul_frost       { readonly get { return this[1].get_bit(14); } set { this[1].set_bit(14, value); } }
    public bool has_nul_blaze       { readonly get { return this[1].get_bit(15); } set { this[1].set_bit(15, value); } }

    public bool has_nul_shock       { readonly get { return this[1].get_bit(16); } set { this[1].set_bit(16, value); } }
    public bool has_nul_tide        { readonly get { return this[1].get_bit(17); } set { this[1].set_bit(17, value); } }
    public bool has_scan            { readonly get { return this[1].get_bit(18); } set { this[1].set_bit(18, value); } }
    public bool has_esuna           { readonly get { return this[1].get_bit(19); } set { this[1].set_bit(19, value); } }
    public bool has_life            { readonly get { return this[1].get_bit(20); } set { this[1].set_bit(20, value); } }
    public bool has_full_life       { readonly get { return this[1].get_bit(21); } set { this[1].set_bit(21, value); } }
    public bool has_haste           { readonly get { return this[1].get_bit(22); } set { this[1].set_bit(22, value); } }
    public bool has_hastega         { readonly get { return this[1].get_bit(23); } set { this[1].set_bit(23, value); } }
    public bool has_slow            { readonly get { return this[1].get_bit(24); } set { this[1].set_bit(24, value); } }
    public bool has_slowga          { readonly get { return this[1].get_bit(25); } set { this[1].set_bit(25, value); } }
    public bool has_shell           { readonly get { return this[1].get_bit(26); } set { this[1].set_bit(26, value); } }
    public bool has_protect         { readonly get { return this[1].get_bit(27); } set { this[1].set_bit(27, value); } }
    public bool has_reflect         { readonly get { return this[1].get_bit(28); } set { this[1].set_bit(28, value); } }
    public bool has_dispel          { readonly get { return this[1].get_bit(29); } set { this[1].set_bit(29, value); } }
    public bool has_regen           { readonly get { return this[1].get_bit(30); } set { this[1].set_bit(30, value); } }
    public bool has_holy            { readonly get { return this[1].get_bit(31); } set { this[1].set_bit(31, value); } }

    public bool has_auto_life       { readonly get { return this[2].get_bit( 0); } set { this[2].set_bit( 0, value); } }
    public bool has_blizzard        { readonly get { return this[2].get_bit( 1); } set { this[2].set_bit( 1, value); } }
    public bool has_fire            { readonly get { return this[2].get_bit( 2); } set { this[2].set_bit( 2, value); } }
    public bool has_thunder         { readonly get { return this[2].get_bit( 3); } set { this[2].set_bit( 3, value); } }
    public bool has_water           { readonly get { return this[2].get_bit( 4); } set { this[2].set_bit( 4, value); } }
    public bool has_fira            { readonly get { return this[2].get_bit( 5); } set { this[2].set_bit( 5, value); } }
    public bool has_blizzara        { readonly get { return this[2].get_bit( 6); } set { this[2].set_bit( 6, value); } }
    public bool has_thundara        { readonly get { return this[2].get_bit( 7); } set { this[2].set_bit( 7, value); } }
    public bool has_watera          { readonly get { return this[2].get_bit( 8); } set { this[2].set_bit( 8, value); } }
    public bool has_firaga          { readonly get { return this[2].get_bit( 9); } set { this[2].set_bit( 9, value); } }
    public bool has_blizzaga        { readonly get { return this[2].get_bit(10); } set { this[2].set_bit(10, value); } }
    public bool has_thundaga        { readonly get { return this[2].get_bit(11); } set { this[2].set_bit(11, value); } }
    public bool has_waterga         { readonly get { return this[2].get_bit(12); } set { this[2].set_bit(12, value); } }
    public bool has_bio             { readonly get { return this[2].get_bit(13); } set { this[2].set_bit(13, value); } }
    public bool has_demi            { readonly get { return this[2].get_bit(14); } set { this[2].set_bit(14, value); } }
    public bool has_death           { readonly get { return this[2].get_bit(15); } set { this[2].set_bit(15, value); } }

    public bool has_drain           { readonly get { return this[2].get_bit(16); } set { this[2].set_bit(16, value); } }
    public bool has_osmose          { readonly get { return this[2].get_bit(17); } set { this[2].set_bit(17, value); } }
    public bool has_flare           { readonly get { return this[2].get_bit(18); } set { this[2].set_bit(18, value); } }
    public bool has_ultima          { readonly get { return this[2].get_bit(19); } set { this[2].set_bit(19, value); } }
    public bool has_shield          { readonly get { return this[2].get_bit(20); } set { this[2].set_bit(20, value); } }
    public bool has_boost           { readonly get { return this[2].get_bit(21); } set { this[2].set_bit(21, value); } }
    public bool has_dismiss         { readonly get { return this[2].get_bit(22); } set { this[2].set_bit(22, value); } }
    public bool has_dismiss_yojimbo { readonly get { return this[2].get_bit(23); } set { this[2].set_bit(23, value); } }
    public bool has_pilfer_gil      { readonly get { return this[2].get_bit(24); } set { this[2].set_bit(24, value); } }
    public bool has_full_break      { readonly get { return this[2].get_bit(25); } set { this[2].set_bit(25, value); } }
    public bool has_extract_power   { readonly get { return this[2].get_bit(26); } set { this[2].set_bit(26, value); } }
    public bool has_extract_mana    { readonly get { return this[2].get_bit(27); } set { this[2].set_bit(27, value); } }
    public bool has_extract_speed   { readonly get { return this[2].get_bit(28); } set { this[2].set_bit(28, value); } }
    public bool has_extract_ability { readonly get { return this[2].get_bit(29); } set { this[2].set_bit(29, value); } }
    public bool has_nab_gil         { readonly get { return this[2].get_bit(30); } set { this[2].set_bit(30, value); } }
    public bool has_quick_pockets   { readonly get { return this[2].get_bit(31); } set { this[2].set_bit(31, value); } }
}

[InlineArray(14)]
public struct LimitAbilityMap {
    private ushort _u;

    public bool has_spiral_cut           { readonly get { return this[ 0].get_bit( 0); } set { this[ 0].set_bit( 0, value); } }
    public bool has_slice_n_dice         { readonly get { return this[ 0].get_bit( 1); } set { this[ 0].set_bit( 1, value); } }
    public bool has_energy_rain          { readonly get { return this[ 0].get_bit( 2); } set { this[ 0].set_bit( 2, value); } }
    public bool has_blitz_ace            { readonly get { return this[ 0].get_bit( 3); } set { this[ 0].set_bit( 3, value); } }
    public bool has_shooting_star        { readonly get { return this[ 0].get_bit( 4); } set { this[ 0].set_bit( 4, value); } }
    public bool has_dragon_fang          { readonly get { return this[ 0].get_bit( 5); } set { this[ 0].set_bit( 5, value); } }
    public bool has_banishing_blade      { readonly get { return this[ 0].get_bit( 6); } set { this[ 0].set_bit( 6, value); } }
    public bool has_tornado              { readonly get { return this[ 0].get_bit( 7); } set { this[ 0].set_bit( 7, value); } }

    public bool has_jump                 { readonly get { return this[ 0].get_bit( 8); } set { this[ 0].set_bit( 8, value); } }
    public bool has_fire_breath          { readonly get { return this[ 0].get_bit( 9); } set { this[ 0].set_bit( 9, value); } }
    public bool has_seed_cannon          { readonly get { return this[ 0].get_bit(10); } set { this[ 0].set_bit(10, value); } }
    public bool has_self_destruct        { readonly get { return this[ 0].get_bit(11); } set { this[ 0].set_bit(11, value); } }
    public bool has_thrust_kick          { readonly get { return this[ 0].get_bit(12); } set { this[ 0].set_bit(12, value); } }
    public bool has_stone_breath         { readonly get { return this[ 0].get_bit(13); } set { this[ 0].set_bit(13, value); } }
    public bool has_aqua_breath          { readonly get { return this[ 0].get_bit(14); } set { this[ 0].set_bit(14, value); } }
    public bool has_doom                 { readonly get { return this[ 0].get_bit(15); } set { this[ 0].set_bit(15, value); } }

    public bool has_white_wind           { readonly get { return this[ 1].get_bit( 0); } set { this[ 1].set_bit( 0, value); } }
    public bool has_bad_breath           { readonly get { return this[ 1].get_bit( 1); } set { this[ 1].set_bit( 1, value); } }
    public bool has_mighty_guard         { readonly get { return this[ 1].get_bit( 2); } set { this[ 1].set_bit( 2, value); } }
    public bool has_nova                 { readonly get { return this[ 1].get_bit( 3); } set { this[ 1].set_bit( 3, value); } }
    public bool has_element_reels        { readonly get { return this[ 1].get_bit( 4); } set { this[ 1].set_bit( 4, value); } }
    public bool has_attack_reels         { readonly get { return this[ 1].get_bit( 5); } set { this[ 1].set_bit( 5, value); } }
    public bool has_status_reels         { readonly get { return this[ 1].get_bit( 6); } set { this[ 1].set_bit( 6, value); } }
    public bool has_aurochs_reels        { readonly get { return this[ 1].get_bit( 7); } set { this[ 1].set_bit( 7, value); } }

    public bool has_blizzard_fury        { readonly get { return this[ 1].get_bit( 8); } set { this[ 1].set_bit( 8, value); } }
    public bool has_fire_fury            { readonly get { return this[ 1].get_bit( 9); } set { this[ 1].set_bit( 9, value); } }
    public bool has_thunder_fury         { readonly get { return this[ 1].get_bit(10); } set { this[ 1].set_bit(10, value); } }
    public bool has_water_fury           { readonly get { return this[ 1].get_bit(11); } set { this[ 1].set_bit(11, value); } }
    public bool has_fira_fury            { readonly get { return this[ 1].get_bit(12); } set { this[ 1].set_bit(12, value); } }
    public bool has_blizzara_fury        { readonly get { return this[ 1].get_bit(13); } set { this[ 1].set_bit(13, value); } }
    public bool has_thundara_fury        { readonly get { return this[ 1].get_bit(14); } set { this[ 1].set_bit(14, value); } }
    public bool has_watera_fury          { readonly get { return this[ 1].get_bit(15); } set { this[ 1].set_bit(15, value); } }

    public bool has_firaga_fury          { readonly get { return this[ 2].get_bit( 0); } set { this[ 2].set_bit( 0, value); } }
    public bool has_blizzaga_fury        { readonly get { return this[ 2].get_bit( 1); } set { this[ 2].set_bit( 1, value); } }
    public bool has_thundaga_fury        { readonly get { return this[ 2].get_bit( 2); } set { this[ 2].set_bit( 2, value); } }
    public bool has_waterga_fury         { readonly get { return this[ 2].get_bit( 3); } set { this[ 2].set_bit( 3, value); } }
    public bool has_bio_fury             { readonly get { return this[ 2].get_bit( 4); } set { this[ 2].set_bit( 4, value); } }
    public bool has_demi_fury            { readonly get { return this[ 2].get_bit( 5); } set { this[ 2].set_bit( 5, value); } }
    public bool has_death_fury           { readonly get { return this[ 2].get_bit( 6); } set { this[ 2].set_bit( 6, value); } }
    public bool has_drain_fury           { readonly get { return this[ 2].get_bit( 7); } set { this[ 2].set_bit( 7, value); } }

    public bool has_osmose_fury          { readonly get { return this[ 2].get_bit( 8); } set { this[ 2].set_bit( 8, value); } }
    public bool has_flare_fury           { readonly get { return this[ 2].get_bit( 9); } set { this[ 2].set_bit( 9, value); } }
    public bool has_ultima_fury          { readonly get { return this[ 2].get_bit(10); } set { this[ 2].set_bit(10, value); } }
    public bool has_grenade              { readonly get { return this[ 2].get_bit(11); } set { this[ 2].set_bit(11, value); } }
    public bool has_frag_grenade         { readonly get { return this[ 2].get_bit(12); } set { this[ 2].set_bit(12, value); } }
    public bool has_pineapple            { readonly get { return this[ 2].get_bit(13); } set { this[ 2].set_bit(13, value); } }
    public bool has_potato_masher        { readonly get { return this[ 2].get_bit(14); } set { this[ 2].set_bit(14, value); } }
    public bool has_cluster_bomb         { readonly get { return this[ 2].get_bit(15); } set { this[ 2].set_bit(15, value); } }

    public bool has_tallboy              { readonly get { return this[ 3].get_bit( 0); } set { this[ 3].set_bit( 0, value); } }
    public bool has_blaster_mine         { readonly get { return this[ 3].get_bit( 1); } set { this[ 3].set_bit( 1, value); } }
    public bool has_hazardous_shell      { readonly get { return this[ 3].get_bit( 2); } set { this[ 3].set_bit( 2, value); } }
    public bool has_calamity_bomb        { readonly get { return this[ 3].get_bit( 3); } set { this[ 3].set_bit( 3, value); } }
    public bool has_chaos_grenade        { readonly get { return this[ 3].get_bit( 4); } set { this[ 3].set_bit( 4, value); } }
    public bool has_heat_blaster         { readonly get { return this[ 3].get_bit( 5); } set { this[ 3].set_bit( 5, value); } }
    public bool has_firestorm            { readonly get { return this[ 3].get_bit( 6); } set { this[ 3].set_bit( 6, value); } }
    public bool has_burning_soul         { readonly get { return this[ 3].get_bit( 7); } set { this[ 3].set_bit( 7, value); } }

    public bool has_brimstone            { readonly get { return this[ 3].get_bit( 8); } set { this[ 3].set_bit( 8, value); } }
    public bool has_abaddon_flame        { readonly get { return this[ 3].get_bit( 9); } set { this[ 3].set_bit( 9, value); } }
    public bool has_snow_flurry          { readonly get { return this[ 3].get_bit(10); } set { this[ 3].set_bit(10, value); } }
    public bool has_icefall              { readonly get { return this[ 3].get_bit(11); } set { this[ 3].set_bit(11, value); } }
    public bool has_winter_storm         { readonly get { return this[ 3].get_bit(12); } set { this[ 3].set_bit(12, value); } }
    public bool has_black_ice            { readonly get { return this[ 3].get_bit(13); } set { this[ 3].set_bit(13, value); } }
    public bool has_krysta               { readonly get { return this[ 3].get_bit(14); } set { this[ 3].set_bit(14, value); } }
    public bool has_thunderbolt          { readonly get { return this[ 3].get_bit(15); } set { this[ 3].set_bit(15, value); } }

    public bool has_rolling_thunder      { readonly get { return this[ 4].get_bit( 0); } set { this[ 4].set_bit( 0, value); } }
    public bool has_lightning_bolt       { readonly get { return this[ 4].get_bit( 1); } set { this[ 4].set_bit( 1, value); } }
    public bool has_electroshock         { readonly get { return this[ 4].get_bit( 2); } set { this[ 4].set_bit( 2, value); } }
    public bool has_thunderblast         { readonly get { return this[ 4].get_bit( 3); } set { this[ 4].set_bit( 3, value); } }
    public bool has_waterfall            { readonly get { return this[ 4].get_bit( 4); } set { this[ 4].set_bit( 4, value); } }
    public bool has_flash_flood          { readonly get { return this[ 4].get_bit( 5); } set { this[ 4].set_bit( 5, value); } }
    public bool has_tidal_wave           { readonly get { return this[ 4].get_bit( 6); } set { this[ 4].set_bit( 6, value); } }
    public bool has_aqua_toxin           { readonly get { return this[ 4].get_bit( 7); } set { this[ 4].set_bit( 7, value); } }

    public bool has_dark_rain            { readonly get { return this[ 4].get_bit( 8); } set { this[ 4].set_bit( 8, value); } }
    public bool has_nega_burst           { readonly get { return this[ 4].get_bit( 9); } set { this[ 4].set_bit( 9, value); } }
    public bool has_black_hole           { readonly get { return this[ 4].get_bit(10); } set { this[ 4].set_bit(10, value); } }
    public bool has_sunburst             { readonly get { return this[ 4].get_bit(11); } set { this[ 4].set_bit(11, value); } }
    public bool has_ultra_potion         { readonly get { return this[ 4].get_bit(12); } set { this[ 4].set_bit(12, value); } }
    public bool has_panacea              { readonly get { return this[ 4].get_bit(13); } set { this[ 4].set_bit(13, value); } }
    public bool has_ultra_cure           { readonly get { return this[ 4].get_bit(14); } set { this[ 4].set_bit(14, value); } }
    public bool has_mega_phoenix         { readonly get { return this[ 4].get_bit(15); } set { this[ 4].set_bit(15, value); } }

    public bool has_final_phoenix        { readonly get { return this[ 5].get_bit( 0); } set { this[ 5].set_bit( 0, value); } }
    public bool has_elixir               { readonly get { return this[ 5].get_bit( 1); } set { this[ 5].set_bit( 1, value); } }
    public bool has_megalixir            { readonly get { return this[ 5].get_bit( 2); } set { this[ 5].set_bit( 2, value); } }
    public bool has_super_elixir         { readonly get { return this[ 5].get_bit( 3); } set { this[ 5].set_bit( 3, value); } }
    public bool has_final_elixir         { readonly get { return this[ 5].get_bit( 4); } set { this[ 5].set_bit( 4, value); } }
    public bool has_nulall_mix           { readonly get { return this[ 5].get_bit( 5); } set { this[ 5].set_bit( 5, value); } }
    public bool has_mega_nulall          { readonly get { return this[ 5].get_bit( 6); } set { this[ 5].set_bit( 6, value); } }
    public bool has_hyper_nulall         { readonly get { return this[ 5].get_bit( 7); } set { this[ 5].set_bit( 7, value); } }

    public bool has_ultra_nulall         { readonly get { return this[ 5].get_bit( 8); } set { this[ 5].set_bit( 8, value); } }
    public bool has_mighty_wall          { readonly get { return this[ 5].get_bit( 9); } set { this[ 5].set_bit( 9, value); } }
    public bool has_mighty_g             { readonly get { return this[ 5].get_bit(10); } set { this[ 5].set_bit(10, value); } }
    public bool has_super_mighty_g       { readonly get { return this[ 5].get_bit(11); } set { this[ 5].set_bit(11, value); } }
    public bool has_hyper_mighty_g       { readonly get { return this[ 5].get_bit(12); } set { this[ 5].set_bit(12, value); } }
    public bool has_vitality             { readonly get { return this[ 5].get_bit(13); } set { this[ 5].set_bit(13, value); } }
    public bool has_mega_vitality        { readonly get { return this[ 5].get_bit(14); } set { this[ 5].set_bit(14, value); } }
    public bool has_hyper_vitality       { readonly get { return this[ 5].get_bit(15); } set { this[ 5].set_bit(15, value); } }
    
    public bool has_mana                 { readonly get { return this[ 6].get_bit( 0); } set { this[ 6].set_bit( 0, value); } }
    public bool has_mega_mana            { readonly get { return this[ 6].get_bit( 1); } set { this[ 6].set_bit( 1, value); } }
    public bool has_hyper_mana           { readonly get { return this[ 6].get_bit( 2); } set { this[ 6].set_bit( 2, value); } }
    public bool has_freedom              { readonly get { return this[ 6].get_bit( 3); } set { this[ 6].set_bit( 3, value); } }
    public bool has_freedom_x            { readonly get { return this[ 6].get_bit( 4); } set { this[ 6].set_bit( 4, value); } }
    public bool has_quartet_of_9         { readonly get { return this[ 6].get_bit( 5); } set { this[ 6].set_bit( 5, value); } }
    public bool has_trio_of_9999         { readonly get { return this[ 6].get_bit( 6); } set { this[ 6].set_bit( 6, value); } }
    public bool has_hero_drink           { readonly get { return this[ 6].get_bit( 7); } set { this[ 6].set_bit( 7, value); } }

    public bool has_miracle_drink        { readonly get { return this[ 6].get_bit( 8); } set { this[ 6].set_bit( 8, value); } }
    public bool has_hot_spurs            { readonly get { return this[ 6].get_bit( 9); } set { this[ 6].set_bit( 9, value); } }
    public bool has_eccentrick           { readonly get { return this[ 6].get_bit(10); } set { this[ 6].set_bit(10, value); } }
    public bool has_valefor_attack       { readonly get { return this[ 6].get_bit(11); } set { this[ 6].set_bit(11, value); } }
    public bool has_sonic_wings          { readonly get { return this[ 6].get_bit(12); } set { this[ 6].set_bit(12, value); } }
    public bool has_energy_blast         { readonly get { return this[ 6].get_bit(13); } set { this[ 6].set_bit(13, value); } }
    public bool has_energy_ray           { readonly get { return this[ 6].get_bit(14); } set { this[ 6].set_bit(14, value); } }
    public bool has_ifrit_attack         { readonly get { return this[ 6].get_bit(15); } set { this[ 6].set_bit(15, value); } }

    public bool has_meteor_strike        { readonly get { return this[ 7].get_bit( 0); } set { this[ 7].set_bit( 0, value); } }
    public bool has_hellfire             { readonly get { return this[ 7].get_bit( 1); } set { this[ 7].set_bit( 1, value); } }
    public bool has_ixion_attack         { readonly get { return this[ 7].get_bit( 2); } set { this[ 7].set_bit( 2, value); } }
    public bool has_aerospark            { readonly get { return this[ 7].get_bit( 3); } set { this[ 7].set_bit( 3, value); } }
    public bool has_thors_hammer         { readonly get { return this[ 7].get_bit( 4); } set { this[ 7].set_bit( 4, value); } }
    public bool has_shiva_attack         { readonly get { return this[ 7].get_bit( 5); } set { this[ 7].set_bit( 5, value); } }
    public bool has_heavenly_strike      { readonly get { return this[ 7].get_bit( 6); } set { this[ 7].set_bit( 6, value); } }
    public bool has_diamond_dust         { readonly get { return this[ 7].get_bit( 7); } set { this[ 7].set_bit( 7, value); } }

    public bool has_bahamut_attack       { readonly get { return this[ 7].get_bit( 8); } set { this[ 7].set_bit( 8, value); } }
    public bool has_impulse              { readonly get { return this[ 7].get_bit( 9); } set { this[ 7].set_bit( 9, value); } }
    public bool has_mega_flare           { readonly get { return this[ 7].get_bit(10); } set { this[ 7].set_bit(10, value); } }
    public bool has_anima_attack         { readonly get { return this[ 7].get_bit(11); } set { this[ 7].set_bit(11, value); } }
    public bool has_pain                 { readonly get { return this[ 7].get_bit(12); } set { this[ 7].set_bit(12, value); } }
    public bool has_oblivion             { readonly get { return this[ 7].get_bit(13); } set { this[ 7].set_bit(13, value); } }
    public bool has_daigoro              { readonly get { return this[ 7].get_bit(14); } set { this[ 7].set_bit(14, value); } }
    public bool has_kozuka               { readonly get { return this[ 7].get_bit(15); } set { this[ 7].set_bit(15, value); } }

    public bool has_wakizashi_single     { readonly get { return this[ 8].get_bit( 0); } set { this[ 8].set_bit( 0, value); } }
    public bool has_wakizashi_all        { readonly get { return this[ 8].get_bit( 1); } set { this[ 8].set_bit( 1, value); } }
    public bool has_zanmato              { readonly get { return this[ 8].get_bit( 2); } set { this[ 8].set_bit( 2, value); } }
    public bool has_requiem              { readonly get { return this[ 8].get_bit( 3); } set { this[ 8].set_bit( 3, value); } }
    public bool has_cindy_attack         { readonly get { return this[ 8].get_bit( 4); } set { this[ 8].set_bit( 4, value); } }
    public bool has_camisade             { readonly get { return this[ 8].get_bit( 5); } set { this[ 8].set_bit( 5, value); } }
    public bool has_sandy_attack         { readonly get { return this[ 8].get_bit( 6); } set { this[ 8].set_bit( 6, value); } }
    public bool has_razzia               { readonly get { return this[ 8].get_bit( 7); } set { this[ 8].set_bit( 7, value); } }

    public bool has_mindy_attack         { readonly get { return this[ 8].get_bit( 8); } set { this[ 8].set_bit( 8, value); } }
    public bool has_passado              { readonly get { return this[ 8].get_bit( 9); } set { this[ 8].set_bit( 9, value); } }
    public bool has_delta_attack         { readonly get { return this[ 8].get_bit(10); } set { this[ 8].set_bit(10, value); } }
    public bool has_spiral_cut_fail      { readonly get { return this[ 8].get_bit(11); } set { this[ 8].set_bit(11, value); } }
    public bool has_slice_n_dice_fail    { readonly get { return this[ 8].get_bit(12); } set { this[ 8].set_bit(12, value); } }
    public bool has_energy_rain_fail     { readonly get { return this[ 8].get_bit(13); } set { this[ 8].set_bit(13, value); } }
    public bool has_blitz_ace_fail       { readonly get { return this[ 8].get_bit(14); } set { this[ 8].set_bit(14, value); } }
    public bool has_fire_shot_single     { readonly get { return this[ 8].get_bit(15); } set { this[ 8].set_bit(15, value); } }

    public bool has_fire_shot_all        { readonly get { return this[ 9].get_bit( 0); } set { this[ 9].set_bit( 0, value); } }
    public bool has_ice_shot_single      { readonly get { return this[ 9].get_bit( 1); } set { this[ 9].set_bit( 1, value); } }
    public bool has_ice_shot_all         { readonly get { return this[ 9].get_bit( 2); } set { this[ 9].set_bit( 2, value); } }
    public bool has_water_shot_single    { readonly get { return this[ 9].get_bit( 3); } set { this[ 9].set_bit( 3, value); } }
    public bool has_water_shot_all       { readonly get { return this[ 9].get_bit( 4); } set { this[ 9].set_bit( 4, value); } }
    public bool has_thunder_shot_single  { readonly get { return this[ 9].get_bit( 5); } set { this[ 9].set_bit( 5, value); } }
    public bool has_thunder_shot_all     { readonly get { return this[ 9].get_bit( 6); } set { this[ 9].set_bit( 6, value); } }
    public bool has_havoc_shot_single    { readonly get { return this[ 9].get_bit( 7); } set { this[ 9].set_bit( 7, value); } }

    public bool has_havoc_shot_all       { readonly get { return this[ 9].get_bit( 8); } set { this[ 9].set_bit( 8, value); } }
    public bool has_time_shot_single     { readonly get { return this[ 9].get_bit( 9); } set { this[ 9].set_bit( 9, value); } }
    public bool has_time_shot_all        { readonly get { return this[ 9].get_bit(10); } set { this[ 9].set_bit(10, value); } }
    public bool has_break_shot_single    { readonly get { return this[ 9].get_bit(11); } set { this[ 9].set_bit(11, value); } }
    public bool has_break_shot_all       { readonly get { return this[ 9].get_bit(12); } set { this[ 9].set_bit(12, value); } }
    public bool has_aurochs_shot_single  { readonly get { return this[ 9].get_bit(13); } set { this[ 9].set_bit(13, value); } }
    public bool has_aurochs_shot_all     { readonly get { return this[ 9].get_bit(14); } set { this[ 9].set_bit(14, value); } }
    public bool has_summon_magus_sisters { readonly get { return this[ 9].get_bit(15); } set { this[ 9].set_bit(15, value); } }

    public bool has_summon_aeon          { readonly get { return this[10].get_bit( 0); } set { this[10].set_bit( 0, value); } }
    public bool has_pincer_attack        { readonly get { return this[10].get_bit( 1); } set { this[10].set_bit( 1, value); } }
    public bool has_use_crane            { readonly get { return this[10].get_bit( 2); } set { this[10].set_bit( 2, value); } }
    public bool has_move                 { readonly get { return this[10].get_bit( 3); } set { this[10].set_bit( 3, value); } }
    public bool has_stand_by             { readonly get { return this[10].get_bit( 4); } set { this[10].set_bit( 4, value); } }
    public bool has_talk                 { readonly get { return this[10].get_bit( 5); } set { this[10].set_bit( 5, value); } }
    public bool has_move_in              { readonly get { return this[10].get_bit( 6); } set { this[10].set_bit( 6, value); } }
    public bool has_pull_back            { readonly get { return this[10].get_bit( 7); } set { this[10].set_bit( 7, value); } }

    public bool has_cancel               { readonly get { return this[10].get_bit( 8); } set { this[10].set_bit( 8, value); } }
    public bool has_struggle             { readonly get { return this[10].get_bit( 9); } set { this[10].set_bit( 9, value); } }
    public bool has_shooting_star_fail   { readonly get { return this[10].get_bit(10); } set { this[10].set_bit(10, value); } }
    public bool has_dragon_fang_fail     { readonly get { return this[10].get_bit(11); } set { this[10].set_bit(11, value); } }
    public bool has_banishing_blade_fail { readonly get { return this[10].get_bit(12); } set { this[10].set_bit(12, value); } }
    public bool has_tornado_fail         { readonly get { return this[10].get_bit(13); } set { this[10].set_bit(13, value); } }
    public bool has_shooting_star_dmg    { readonly get { return this[10].get_bit(14); } set { this[10].set_bit(14, value); } }
    public bool has_dragon_fang_dmg      { readonly get { return this[10].get_bit(15); } set { this[10].set_bit(15, value); } }

    public bool has_banishing_blade_dmg  { readonly get { return this[11].get_bit( 0); } set { this[11].set_bit( 0, value); } }
    public bool has_tornado_dmg          { readonly get { return this[11].get_bit( 1); } set { this[11].set_bit( 1, value); } }
    public bool has_blitz_ace_finale     { readonly get { return this[11].get_bit( 2); } set { this[11].set_bit( 2, value); } }
    public bool has_skill                { readonly get { return this[11].get_bit( 3); } set { this[11].set_bit( 3, value); } }
    public bool has_special              { readonly get { return this[11].get_bit( 4); } set { this[11].set_bit( 4, value); } }
    public bool has_blk_magic            { readonly get { return this[11].get_bit( 5); } set { this[11].set_bit( 5, value); } }
    public bool has_wht_magic            { readonly get { return this[11].get_bit( 6); } set { this[11].set_bit( 6, value); } }
    public bool has_summon               { readonly get { return this[11].get_bit( 7); } set { this[11].set_bit( 7, value); } }

    public bool has_grand_summon         { readonly get { return this[11].get_bit( 8); } set { this[11].set_bit( 8, value); } }
    public bool has_swordplay            { readonly get { return this[11].get_bit( 9); } set { this[11].set_bit( 9, value); } }
    public bool has_ronso_rage           { readonly get { return this[11].get_bit(10); } set { this[11].set_bit(10, value); } }
    public bool has_bushido              { readonly get { return this[11].get_bit(11); } set { this[11].set_bit(11, value); } }
    public bool has_slots                { readonly get { return this[11].get_bit(12); } set { this[11].set_bit(12, value); } }
    public bool has_fury                 { readonly get { return this[11].get_bit(13); } set { this[11].set_bit(13, value); } }
    public bool has_mix                  { readonly get { return this[11].get_bit(14); } set { this[11].set_bit(14, value); } }
    public bool has_auto_life_ex         { readonly get { return this[11].get_bit(15); } set { this[11].set_bit(15, value); } }

    public bool has_death_ex             { readonly get { return this[12].get_bit( 0); } set { this[12].set_bit( 0, value); } }
    public bool has_gil_bribe            { readonly get { return this[12].get_bit( 1); } set { this[12].set_bit( 1, value); } }
    public bool has_gil_yojimbo          { readonly get { return this[12].get_bit( 2); } set { this[12].set_bit( 2, value); } }
    public bool has_pay                  { readonly get { return this[12].get_bit( 3); } set { this[12].set_bit( 3, value); } }
    public bool has_do_as_you_will       { readonly get { return this[12].get_bit( 4); } set { this[12].set_bit( 4, value); } }
    public bool has_one_more_time        { readonly get { return this[12].get_bit( 5); } set { this[12].set_bit( 5, value); } }
    public bool has_fight                { readonly get { return this[12].get_bit( 6); } set { this[12].set_bit( 6, value); } }
    public bool has_go_go                { readonly get { return this[12].get_bit( 7); } set { this[12].set_bit( 7, value); } }

    public bool has_help_each_other      { readonly get { return this[12].get_bit( 8); } set { this[12].set_bit( 8, value); } }
    public bool has_combine_powers       { readonly get { return this[12].get_bit( 9); } set { this[12].set_bit( 9, value); } }
    public bool has_defense              { readonly get { return this[12].get_bit(10); } set { this[12].set_bit(10, value); } }
    public bool has_are_you_all_right    { readonly get { return this[12].get_bit(11); } set { this[12].set_bit(11, value); } }
    public bool has_taking_a_break       { readonly get { return this[12].get_bit(12); } set { this[12].set_bit(12, value); } }
    public bool has_nulall_ability       { readonly get { return this[12].get_bit(13); } set { this[12].set_bit(13, value); } }
    public bool has_attack_reels_single  { readonly get { return this[12].get_bit(14); } set { this[12].set_bit(14, value); } }
    public bool has_open_lock            { readonly get { return this[12].get_bit(15); } set { this[12].set_bit(15, value); } }

    public bool has_extra_24             { readonly get { return this[13].get_bit( 0); } set { this[13].set_bit( 0, value); } }
    public bool has_extra_25             { readonly get { return this[13].get_bit( 1); } set { this[13].set_bit( 1, value); } }
    public bool has_extra_26             { readonly get { return this[13].get_bit( 2); } set { this[13].set_bit( 2, value); } }
    public bool has_extra_27             { readonly get { return this[13].get_bit( 3); } set { this[13].set_bit( 3, value); } }
    public bool has_extra_28             { readonly get { return this[13].get_bit( 4); } set { this[13].set_bit( 4, value); } }
    public bool has_extra_29             { readonly get { return this[13].get_bit( 5); } set { this[13].set_bit( 5, value); } }
    public bool has_extra_30             { readonly get { return this[13].get_bit( 6); } set { this[13].set_bit( 6, value); } }
    public bool has_extra_31             { readonly get { return this[13].get_bit( 7); } set { this[13].set_bit( 7, value); } }

    public bool has_extra_32             { readonly get { return this[13].get_bit( 8); } set { this[13].set_bit( 8, value); } }
    public bool has_extra_33             { readonly get { return this[13].get_bit( 9); } set { this[13].set_bit( 9, value); } }
    public bool has_extra_34             { readonly get { return this[13].get_bit(10); } set { this[13].set_bit(10, value); } }
    public bool has_extra_35             { readonly get { return this[13].get_bit(11); } set { this[13].set_bit(11, value); } }
    public bool has_extra_36             { readonly get { return this[13].get_bit(12); } set { this[13].set_bit(12, value); } }
    public bool has_extra_37             { readonly get { return this[13].get_bit(13); } set { this[13].set_bit(13, value); } }
    public bool has_extra_38             { readonly get { return this[13].get_bit(14); } set { this[13].set_bit(14, value); } }
    public bool has_extra_39             { readonly get { return this[13].get_bit(15); } set { this[13].set_bit(15, value); } }
}
