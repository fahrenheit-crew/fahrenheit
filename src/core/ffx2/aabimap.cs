// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX2;

[InlineArray(3)]
public struct AutoAbilityEffectsMap {
    private ushort _u;

    // Auto-Abilities
    public bool has_first_strike      { readonly get { return this[0].get_bit( 0); } set { this[0].set_bit( 0, value); } }
    public bool has_initiative        { readonly get { return this[0].get_bit( 1); } set { this[0].set_bit( 1, value); } }
    public bool has_counter_attack    { readonly get { return this[0].get_bit( 2); } set { this[0].set_bit( 2, value); } }
    public bool has_evade_and_counter { readonly get { return this[0].get_bit( 3); } set { this[0].set_bit( 3, value); } }
    public bool has_magic_counter     { readonly get { return this[0].get_bit( 4); } set { this[0].set_bit( 4, value); } }
    public bool has_magic_booster     { readonly get { return this[0].get_bit( 5); } set { this[0].set_bit( 5, value); } }
    public bool has_more_encounters   { readonly get { return this[0].get_bit( 6); } set { this[0].set_bit( 6, value); } }
    public bool has_magic             { readonly get { return this[0].get_bit( 7); } set { this[0].set_bit( 7, value); } } // No idea what this is, maybe unused?
    public bool has_chemist           { readonly get { return this[0].get_bit( 8); } set { this[0].set_bit( 8, value); } }
    public bool has_elementalist      { readonly get { return this[0].get_bit( 9); } set { this[0].set_bit( 9, value); } }
    public bool has_physicist         { readonly get { return this[0].get_bit(10); } set { this[0].set_bit(10, value); } }
    public bool has_status_master     { readonly get { return this[0].get_bit(11); } set { this[0].set_bit(11, value); } }
    public bool has_double_ap         { readonly get { return this[0].get_bit(12); } set { this[0].set_bit(12, value); } }
    public bool has_triple_ap         { readonly get { return this[0].get_bit(13); } set { this[0].set_bit(13, value); } }
    public bool has_break_hp_limit    { readonly get { return this[0].get_bit(14); } set { this[0].set_bit(14, value); } }
    public bool has_break_mp_limit    { readonly get { return this[0].get_bit(15); } set { this[0].set_bit(15, value); } }

    public bool has_break_damage_limit { readonly get { return this[1].get_bit( 0); } set { this[1].set_bit( 0, value); } }
    public bool has_butterfingers      { readonly get { return this[1].get_bit( 1); } set { this[1].set_bit( 1, value); } }
    public bool has_drain_sword        { readonly get { return this[1].get_bit( 2); } set { this[1].set_bit( 2, value); } } // No idea what this is, maybe unused?
    public bool has_aspil_sword        { readonly get { return this[1].get_bit( 3); } set { this[1].set_bit( 3, value); } } // No idea what this is, maybe unused?
    public bool has_gillionaire        { readonly get { return this[1].get_bit( 4); } set { this[1].set_bit( 4, value); } }
    public bool has_double_items       { readonly get { return this[1].get_bit( 5); } set { this[1].set_bit( 5, value); } }
    public bool has_double_exp         { readonly get { return this[1].get_bit( 6); } set { this[1].set_bit( 6, value); } }
    public bool has_auto_hp            { readonly get { return this[1].get_bit( 7); } set { this[1].set_bit( 7, value); } }
    public bool has_auto_mp            { readonly get { return this[1].get_bit( 8); } set { this[1].set_bit( 8, value); } }
    public bool has_item_hunter        { readonly get { return this[1].get_bit( 9); } set { this[1].set_bit( 9, value); } }
    public bool has_piercing_magic     { readonly get { return this[1].get_bit(10); } set { this[1].set_bit(10, value); } }
    public bool has_hp_stroll          { readonly get { return this[1].get_bit(11); } set { this[1].set_bit(11, value); } }
    public bool has_mp_stroll          { readonly get { return this[1].get_bit(12); } set { this[1].set_bit(12, value); } }
    public bool has_no_encounters      { readonly get { return this[1].get_bit(13); } set { this[1].set_bit(13, value); } }
    public bool has_trigger_happy_lv2  { readonly get { return this[1].get_bit(14); } set { this[1].set_bit(14, value); } }
    public bool has_trigger_happy_lv3  { readonly get { return this[1].get_bit(15); } set { this[1].set_bit(15, value); } }

    public bool has_scan_lv2     { readonly get { return this[2].get_bit(0); } set { this[2].set_bit(0, value); } }
    public bool has_scan_lv3     { readonly get { return this[2].get_bit(1); } set { this[2].set_bit(1, value); } }
    public bool has_half_mp_cost { readonly get { return this[2].get_bit(2); } set { this[2].set_bit(2, value); } }
    public bool has_one_mp_cost  { readonly get { return this[2].get_bit(3); } set { this[2].set_bit(3, value); } } 

}
