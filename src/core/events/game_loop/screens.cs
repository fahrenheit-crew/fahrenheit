// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Events;

public partial class GameLoopEvents {
    /// <summary>Raised after the save/load menu is opened.</summary>
    public FhEvent<EventArgs> PostOpenSaveMenu = new();

    /// <summary>Raised after the save/load menu is closed.</summary>
    public FhEvent<EventArgs> PostCloseSaveMenu = new();
}
