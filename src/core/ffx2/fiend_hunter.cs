// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX2;

/// <summary>
///     Commands deal 4x damage to the respective fiend type when these flags are set.
/// </summary>
[Flags]
public enum FiendSpecies : ushort {
    NONE    = 0,
    MACHINA = 1 << 0,
    MECH    = 1 << 1,
    LIZARD  = 1 << 2,
    ELEMENT = 1 << 3,
    DRAKE   = 1 << 4,
    DEVIL   = 1 << 5, // Imps and Evil Eyes
    FLAN    = 1 << 6,
    WOLF    = 1 << 7,
    WING    = 1 << 8, // Birds and Wasps
    HELM    = 1 << 9,
}

public static partial class FhEnumExt {
    extension(FiendSpecies flags) {
        public bool machina {
            get { return flags.HasFlag(FiendSpecies.MACHINA); }
            set { if (value) flags |= FiendSpecies.MACHINA; else flags &= ~FiendSpecies.MACHINA; }
        }

        public bool mech {
            get { return flags.HasFlag(FiendSpecies.MECH); }
            set { if (value) flags |= FiendSpecies.MECH; else flags &= ~FiendSpecies.MECH; }
        }

        public bool lizard {
            get { return flags.HasFlag(FiendSpecies.LIZARD); }
            set { if (value) flags |= FiendSpecies.LIZARD; else flags &= ~FiendSpecies.LIZARD; }
        }

        public bool element {
            get { return flags.HasFlag(FiendSpecies.ELEMENT); }
            set { if (value) flags |= FiendSpecies.ELEMENT; else flags &= ~FiendSpecies.ELEMENT; }
        }

        public bool drake {
            get { return flags.HasFlag(FiendSpecies.DRAKE); }
            set { if (value) flags |= FiendSpecies.DRAKE; else flags &= ~FiendSpecies.DRAKE; }
        }

        public bool devil {
            get { return flags.HasFlag(FiendSpecies.DEVIL); }
            set { if (value) flags |= FiendSpecies.DEVIL; else flags &= ~FiendSpecies.DEVIL; }
        }

        public bool flan {
            get { return flags.HasFlag(FiendSpecies.FLAN); }
            set { if (value) flags |= FiendSpecies.FLAN; else flags &= ~FiendSpecies.FLAN; }
        }

        public bool wolf {
            get { return flags.HasFlag(FiendSpecies.WOLF); }
            set { if (value) flags |= FiendSpecies.WOLF; else flags &= ~FiendSpecies.WOLF; }
        }

        public bool wing {
            get { return flags.HasFlag(FiendSpecies.WING); }
            set { if (value) flags |= FiendSpecies.WING; else flags &= ~FiendSpecies.WING; }
        }

        public bool helm {
            get { return flags.HasFlag(FiendSpecies.HELM); }
            set { if (value) flags |= FiendSpecies.HELM; else flags &= ~FiendSpecies.HELM; }
        }
    }
}
