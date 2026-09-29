// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX2;

[Flags]
public enum DamageClass : byte {
    HP  = 1 << 0,
    MP  = 1 << 1,
    ATB = 1 << 2,
}

public static partial class FhEnumExt {
    extension(DamageClass dmg_class) {
        public bool hp {
            get { return dmg_class.HasFlag(DamageClass.HP); }
            set { if (value) dmg_class |= (DamageClass.HP); else dmg_class &= ~(DamageClass.HP); }
        }

        public bool mp {
            get { return dmg_class.HasFlag(DamageClass.MP); }
            set { if (value) dmg_class |= (DamageClass.MP); else dmg_class &= ~(DamageClass.MP); }
        }

        public bool atb {
            get { return dmg_class.HasFlag(DamageClass.ATB); }
            set { if (value) dmg_class |= (DamageClass.ATB); else dmg_class &= ~(DamageClass.ATB); }
        }
    }
}
