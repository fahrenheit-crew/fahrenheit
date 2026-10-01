// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX;

/// <summary>
///     Factors in the calculation of aeon stat boosts based on Yuna's stats.<br/>
///     The "individual" factors affect only the boost from the same stat. That is, the Aeon's HP being based on Yuna's HP.<br/>
///     The "total" factors affect the boost from all of Yuna's stats, except Luck.<br/>
///     In vanilla, this total is calculated with the formula<br/>
///     <c>hp/100 + mp/10 + strength + defense + magic + magic_defense + agility + evasion + accuracy</c>.
/// </summary>
public struct AeonStatBoostsScaling {
    /// <summary>
    ///     Contributes <c>yuna_total * hp_total</c> to the Aeon's HP.
    /// </summary>
    public byte hp_from_total_stats;

    /// <summary>
    ///     Contributes <c>yuna_hp * hp_individual / 100</c> to the Aeon's HP.
    /// </summary>
    public byte hp_from_specific_stat;

    /// <summary>
    ///     Contributes <c>yuna_total * mp_total / 10</c> to the Aeon's MP.
    /// </summary>
    public byte mp_from_total_stats;

    /// <summary>
    ///     Contributes <c>yuna_mp * mp_individual / 100</c> to the Aeon's MP.
    /// </summary>
    public byte mp_from_specific_stat;

    /// <summary>
    ///     Contributes <c>yuna_total / strength_total</c> to the Aeon's strength.
    /// </summary>
    public byte strength_from_total_stats;

    /// <summary>
    ///     Contributes <c>yuna_strength * strength_individual / 10</c> to the Aeon's strength.
    /// </summary>
    public byte strength_from_specific_stat;

    /// <summary>
    ///     Contributes <c>yuna_total / defense_total</c> to the Aeon's defense.
    /// </summary>
    public byte defense_from_total_stats;

    /// <summary>
    ///     Contributes <c>yuna_defense * defense_individual / 10</c> to the Aeon's defense.
    /// </summary>
    public byte defense_from_specific_stat;

    /// <summary>
    ///     Contributes <c>yuna_total / magic_total</c> to the Aeon's magic.
    /// </summary>
    public byte magic_from_total_stats;

    /// <summary>
    ///     Contributes <c>yuna_magic * magic_individual / 10</c> to the Aeon's magic.
    /// </summary>
    public byte magic_from_specific_stat;

    /// <summary>
    ///     Contributes <c>yuna_total / magic_defense_total</c> to the Aeon's magic_defense.
    /// </summary>
    public byte magic_defense_from_total_stats;

    /// <summary>
    ///     Contributes <c>yuna_magic_defense * magic_defense_individual / 10</c> to the Aeon's magic_defense.
    /// </summary>
    public byte magic_defense_from_specific_stat;

    /// <summary>
    ///     Contributes <c>yuna_total / agility_total</c> to the Aeon's agility.
    /// </summary>
    public byte agility_from_total_stats;

    /// <summary>
    ///     Contributes <c>yuna_agility * agility_individual / 10</c> to the Aeon's agility.
    /// </summary>
    public byte agility_from_specific_stat;

    /// <summary>
    ///     Contributes <c>yuna_total / evasion_total</c> to the Aeon's evasion.
    /// </summary>
    public byte evasion_from_total_stats;

    /// <summary>
    ///     Contributes <c>yuna_evasion * evasion_individual / 10</c> to the Aeon's evasion.
    /// </summary>
    public byte evasion_from_specific_stat;

    /// <summary>
    ///     Contributes <c>yuna_total / accuracy_total</c> to the Aeon's accuracy.
    /// </summary>
    public byte accuracy_from_total_stats;

    /// <summary>
    ///     Contributes <c>yuna_accuracy * accuracy_individual / 10</c> to the Aeon's accuracy.
    /// </summary>
    public byte accuracy_from_specific_stat;
}

/// <summary>
///     Data detailing the minimum stat boost for an Aeon based on the battle count.<br/>
///     In vanilla, this boost increases every 30 battles, starting at 60.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct AeonStatBoostsMinimum {
    /// <summary>
    ///     The boost to the Aeon's HP.
    /// </summary>
    public ushort hp;

    /// <summary>
    ///     The boost to the Aeon's MP.
    /// </summary>
    public ushort mp;

    /// <summary>
    ///     The boost to the Aeon's strength.
    /// </summary>
    public byte strength;

    /// <summary>
    ///     The boost to the Aeon's defense.
    /// </summary>
    public byte defense;

    /// <summary>
    ///     The boost to the Aeon's magic.
    /// </summary>
    public byte magic;

    /// <summary>
    ///     The boost to the Aeon's magic defense.
    /// </summary>
    public byte magic_defense;

    /// <summary>
    ///     The boost to the Aeon's accuracy.
    /// </summary>
    public byte accuracy;

    /// <summary>
    ///     The boost to the Aeon's evasion.
    /// </summary>
    public byte evasion;

    /// <summary>
    ///     The boost to the Aeon's agility.
    /// </summary>
    public byte agility;

    /// <summary>
    ///     The boost to the Aeon's luck.
    /// </summary>
    public byte luck;
}

/// <summary>
///     Recipe for teaching an aeon an ability using a set amount of an item.
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = 0x8)]
public struct AeonAbilityRecipe {
    [FieldOffset(0x0)] internal byte __0x0; // Always 0x7F

    /// <summary>
    ///     The command to be taught.
    ///     <remarks>
    ///         If __0x7 is true, this is not a command id, but something else.
    ///     </remarks>
    /// </summary>
    [FieldOffset(0x2)] public short command;

    /// <summary>
    ///     The item to be spent in teaching the command.
    /// </summary>
    [FieldOffset(0x4)] public T_XCommandId item;

    /// <summary>
    ///     The amount of the item that is needed.
    /// </summary>
    [FieldOffset(0x6)] public byte item_cost;

    [FieldOffset(0x7)] internal bool __0x7;
}
