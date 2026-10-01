// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX.Battle;

[StructLayout(LayoutKind.Sequential)]
public struct MonStats {
    public  ExcelTextOffset             name;
    public  ExcelSimplifiableTextOffset sensor_text;
    public  ExcelSimplifiableTextOffset scan_text;
    public  uint                        hp;
    public  uint                        mp;
    public  uint                        overkill_threshold;
    public  byte                        strength;
    public  byte                        defense;
    public  byte                        magic;
    public  byte                        magic_defense;
    public  byte                        agility;
    public  byte                        luck;
    public  byte                        evasion;
    public  byte                        accuracy;
    public  ChrResistFlags              special_resistances;
    public  byte                        poison_dmg;
    public  ElementFlags                elem_absorb;
    public  ElementFlags                elem_ignore;
    public  ElementFlags                elem_resist;
    public  ElementFlags                elem_weak;
    public  StatusMap                   status_resist;
    public  StatusPermanentFlags        status_auto_permanent;
    public  StatusTemporalFlags         status_auto_temporal;
    public  StatusExtraFlags            status_auto_extra;
    public  StatusExtraFlags            status_resist_extra;
    public  InlineArray16<ushort>       command_list;
    public  ushort                      forced_move;
    public  ushort                      monster_idx;
    public  ushort                      model_idx;
    public  byte                        ctb_icon_type;
    public  byte                        doom_counter;
    public  ushort                      monster_arena_idx;
    public  ushort                      sound_bank_ref;
    private uint                        always_zero;
}
