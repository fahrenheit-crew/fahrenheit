// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX;

/* [fkelava 03/08/26 00:52]
 * TODO: Decide whether this should be dissected into a separate 'item' and 'gear' shop.
 */

[StructLayout(LayoutKind.Sequential)]
public struct Shop {

    [InlineArray(0x10)]
    public struct ShopOffers {
        private ushort _e0;
    }

    /// <summary>
    ///     The percentage the base price of each offer should be adjusted by.
    ///     <example>
    ///         A value of 150 would set prices to 1.5 times their base amounts.
    ///     </example>
    /// </summary>
    public ushort     price_percentage;
    public ShopOffers offers;
}
