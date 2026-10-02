// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime.Gui;

/// <summary>
///     Renders the modlist on the main menu.
/// </summary>
[FhLoad(FhGameId.FFX | FhGameId.FFX2 | FhGameId.FFX2LM)]
public unsafe class FhModListDisplayModule : FhModule {

    public FhModListDisplayModule() { }

    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        return true;
    }

    public override void render_imgui() {
        int curr_event_id = FhUtil.select(*FFX.Globals.event_id, *FFX2.Globals.event_id, *FFX2.Globals.event_id);

        // Do not render the mod list outside the main menu.
        if (curr_event_id != 0x17
         || FhSavePal.pal_get_screen_state() == FhSaveScreenState.OPEN
         || FhSavePal.pal_get_screen_state() == FhSaveScreenState.OPENING) {
            return;
        }

        // Create a window for the mod list and render all the mods
        ImGui.SetNextWindowPos (new Vector2(0,   0  ));
        ImGui.SetNextWindowSize(new Vector2(350, 500));

        if (ImGui.Begin("Fh.ModList", ImGuiWindowFlags.NoBackground | ImGuiWindowFlags.NoSavedSettings | ImGuiWindowFlags.NoDecoration | ImGuiWindowFlags.NoInputs)) {
            ImGui.PushFont(FhApi.Gui.FONT_DEFAULT, 18f);
            FhModContext[] mods = [ .. FhApi.Mods.get_mods() ];

            ImGui.Text($"{mods.Length} mods loaded");
            foreach (FhModContext mod_ctx in mods) {
                ImGui.Text($"{mod_ctx.Manifest.Name} v{mod_ctx.Manifest.Version}");
            }
            ImGui.PopFont();
        }
        ImGui.End();

        //ImGui.ShowDemoWindow();
    }
}
