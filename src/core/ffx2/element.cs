// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX2;

[Flags]
public enum ElementFlags : byte {
    NONE    = 0,
    FIRE    = 1 << 0,
    ICE     = 1 << 1,
    THUNDER = 1 << 2,
    WATER   = 1 << 3,
    GRAVITY = 1 << 4,
    HOLY    = 1 << 5,
}

public static partial class FhEnumExt {
    extension(ElementFlags flags) {
        public bool fire {
            get { return flags.HasFlag(ElementFlags.FIRE); }
            set { if (value) flags |= (ElementFlags.FIRE); else flags &= ~(ElementFlags.FIRE); }
        }

        public bool ice {
            get { return flags.HasFlag(ElementFlags.ICE); }
            set { if (value) flags |= (ElementFlags.ICE); else flags &= ~(ElementFlags.ICE); }
        }

        public bool thunder {
            get { return flags.HasFlag(ElementFlags.THUNDER); }
            set { if (value) flags |= (ElementFlags.THUNDER); else flags &= ~(ElementFlags.THUNDER); }
        }

        public bool water {
            get { return flags.HasFlag(ElementFlags.WATER); }
            set { if (value) flags |= (ElementFlags.WATER); else flags &= ~(ElementFlags.WATER); }
        }

        public bool gravity {
            get { return flags.HasFlag(ElementFlags.GRAVITY); }
            set { if (value) flags |= (ElementFlags.GRAVITY); else flags &= ~(ElementFlags.GRAVITY); }
        }

        public bool holy {
            get { return flags.HasFlag(ElementFlags.HOLY); }
            set { if (value) flags |= (ElementFlags.HOLY); else flags &= ~(ElementFlags.HOLY); }
        }
    }
}
