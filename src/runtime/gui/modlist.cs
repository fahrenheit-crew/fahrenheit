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
    private FhSettingsUiSelector? _settings_ui;

    private class Settings : FhSettingProvider<FhModListDisplayModule> {
        internal protected override IEnumerable<FhSetting> get() {
            FhSettingToggle setting_a = new("setting_a", false);
            FhSettingToggle setting_b = new("setting_b", true);

            FhSettingNumber setting_c = new("setting_c", 3.0, 0.0, 5.0, 0.2, precision: 1);
            FhSettingNumber setting_d = new("setting_d", 10, 5, 20, 1, integers_only: true);

            FhSettingChoice<Alignment> setting_e = new("setting_e", Alignment.CENTER);
            FhSettingChoice<GradientDirection> setting_f = new("setting_f", GradientDirection.DOWN);

            FhSettingsCategory sub_a = new("sub_a", [
                setting_c,
                setting_d,
            ]);

            FhSettingsCategory sub_b = new("sub_b", [
                setting_e,
                setting_f,
            ]);

            return [
                setting_a,
                setting_b,
                sub_a,
                sub_b,
                setting_a,
                setting_b,
                sub_a,
                sub_b,
                setting_a,
                setting_b,
                sub_a,
                sub_b,
                setting_a,
                setting_b,
            ];
        }
    }

    private Settings _settings = new();

    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        return new FhModuleHandle<FhSettingsUiSelector>(this).try_get_module(out _settings_ui);
    }

    public override void render_imgui() {
        int curr_event_id = FhUtil.select(*FFX.Globals.event_id, *FFX2.Globals.event_id, *FFX2.Globals.event_id);

        // Do not render the mod list outside the main menu.
        if (curr_event_id != 0x17
         || FhSaves.pal_state_screen == SaveScreenState.OPEN
         || FhSaves.pal_state_screen == SaveScreenState.OPENING
         || _settings_ui!.is_open) {
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
