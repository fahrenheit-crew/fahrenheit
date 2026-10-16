// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Gui;

/// <summary>The base class for custom save/load user interfaces.</summary>
public abstract class FhSaveUi : FhFullscreenUi {
    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        _logger.Info($"Registering new save UI: {ModuleType}");
        FhApi.Saves.register_ui(this);

        FhApi.Events.Common.GameLoop.PostOpenSaveMenu.subscribe(post_open_menu);

        return true;
    }
}
