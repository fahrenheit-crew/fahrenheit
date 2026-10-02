// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime.Gui;

[FhLoad(FhGameId.FFX | FhGameId.FFX2 | FhGameId.FFX2LM)]
public sealed class FhModConfigModule : FhModule {
    //private bool _dockbuilder_initialized = false;
    private bool _open;
    private int  _selected_mod_idx;

    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        return true;
    }

    internal void open() {
        _open = true;
        //TODO: Prevent the game from playing the Zanarkand scene while the config menu is open
    }

    internal void close() {
        _open             = false;
        _selected_mod_idx = 0;

        FhInternal.Settings.save_all();
    }

    public override void render_imgui() {
        //TODO: Add a proper open/close button in the topright corner
        if (ImGui.IsKeyPressed(ImGuiKey.F7)) {
            if (_open) close();
            else       open();
        }

        if (!_open) return;

        ImGuiViewportPtr viewport = ImGui.GetMainViewport();

        ImGui.SetNextWindowPos     (viewport.WorkPos);
        ImGui.SetNextWindowSize    (viewport.WorkSize);
        ImGui.SetNextWindowViewport(viewport.ID);

        ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding,   0f);
        ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0f);
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding,    Vector2.Zero);

        ImGui.PushStyleColor(ImGuiCol.WindowBg, new Vector4(0, 0, 0, 1));
        ImGui.PushFont(FhApi.Gui.FONT_DEFAULT, 20f);

        if (ImGui.Begin("ModConfig", FhApi.Gui.WINDOW_FLAGS_FULLSCREEN)) {
            // TODO: Make the docking code in https://gist.github.com/fkelava/6c6ab0089a63280fdfb4bea4a9cdf9b0 work
            // Docking code here

            ImGui.SetNextWindowPos (viewport.WorkPos);
            ImGui.SetNextWindowSize(new (viewport.WorkSize.X * 0.16f, viewport.WorkSize.Y));

            if (ImGui.Begin("ModTabs", ImGuiWindowFlags.NoDecoration | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove)) {
                int   mod_idx   = 0;
                float tab_width = ImGui.GetContentRegionAvail().X;

                foreach (FhModContext mod in FhApi.Mods.get_mods()) {
                    if (has_settings(mod)) {
                        render_mod_tab(mod, mod_idx, tab_width);
                    }

                    mod_idx++;
                }
            }
            ImGui.End();

            ImGui.SetNextWindowPos (new Vector2(viewport.WorkPos.X + viewport.WorkSize.X * 0.17f, viewport.WorkPos.Y));
            ImGui.SetNextWindowSize(new Vector2(viewport.WorkSize.X * 0.83f, viewport.WorkSize.Y));

            if (ImGui.Begin("ModSettings", ImGuiWindowFlags.NoDecoration | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove)) {
                FhModContext[] mods = [ .. FhApi.Mods.get_mods() ];
                foreach (FhModuleContext module in mods[_selected_mod_idx].Modules) {
                    if (!FhInternal.Settings.try_get(module.Module, out FhSettingsCategory? settings))
                        continue;

                    settings.render_name();
                    settings.render();
                }
            }

            ImGui.End();
        }

        // ImGui uses the style var in `Begin()` so we're free to pop it before `End()`
        // See: https://github.com/ocornut/imgui/issues/1797#issuecomment-644131003
        ImGui.PopStyleVar(3);
        ImGui.PopStyleColor();
        ImGui.PopFont();

        ImGui.End(); // Closing the fullscreen window!
    }

    private void render_mod_tab(FhModContext mod, int mod_idx, float tab_width) {
        if (ImGui.Button($"{mod.Manifest.Name}##mod{mod_idx}", new Vector2(tab_width, 0)))
            _selected_mod_idx = mod_idx;
    }

    private static bool has_settings(FhModContext mod_context) {
        foreach (FhModuleContext module_context in mod_context.Modules) {
            if (FhInternal.Settings.try_get(module_context.Module, out _))
                return true;
        }

        return false;
    }
}
