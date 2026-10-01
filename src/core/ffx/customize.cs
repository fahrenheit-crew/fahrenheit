// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX;

[Flags]
public enum GearType : byte {
    NONE   = 0,
    WEAPON = 1,
    ARMOR  = 2,
}

public static partial class FhEnumExt {
    extension(GearType gear_type) {
        public bool is_weapon => gear_type.HasFlag(GearType.WEAPON);
        public bool is_armor  => gear_type.HasFlag(GearType.ARMOR);
    }
}

/// <summary>
///     Recipe for customizing an auto-ability onto gear using a set amount of an item.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct CustomizationRecipe {
    /// <summary>
    ///     The gear type that can be customized using this recipe.
    /// </summary>
    public GearType target_gear_type;

    /// <summary>
    ///     The auto-ability that results from this recipe.
    /// </summary>
    public T_XAutoAbilityId auto_ability;

    /// <summary>
    ///     The item to be spent on the customization.
    /// </summary>
    public T_XCommandId item;

    /// <summary>
    ///     The amount of the item that is needed.
    /// </summary>
    public ushort item_cost;
}
