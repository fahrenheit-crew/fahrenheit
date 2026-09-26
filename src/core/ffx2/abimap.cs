// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX2;

[InlineArray(2)]
public struct AbilityMap {
    private byte _u;

    public bool has_item        { readonly get { return this[0].get_bit(0); } set { this[0].set_bit(0, value); } }
    public bool has_escape      { readonly get { return this[0].get_bit(1); } set { this[0].set_bit(1, value); } }
    public bool has_dresspheres { readonly get { return this[0].get_bit(2); } set { this[0].set_bit(2, value); } } // Debug Command
}
