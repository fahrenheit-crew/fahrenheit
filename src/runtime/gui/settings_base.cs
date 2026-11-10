// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime.Gui;

public abstract class FhSettingsUiBase : FhSettingsUi {
    public interface ICapturingRenderer {
        public string get_input_help_text();
    }

    /// <summary>Possible elements for the UI to focus on.</summary>
    protected enum UiFocus {
        /// <summary>The scrollable list of mods with settings.</summary>
        MODS,

        /// <summary>The list of modules that scrolls out when a mod is selected.</summary>
        MODULES,

        /// <summary>The list of settings that appears when a module is hovered/selected.</summary>
        SETTINGS,
    }

    protected const float FONT_SIZE_DEFAULT = 43f;

    protected const uint SOUND_X_MOVE   = FFX.Ids.SoundId.UI_ACTION;
    protected const uint SOUND_X_ACTION = FFX.Ids.SoundId.UI_ACTION;
    protected const uint SOUND_X_CANCEL = FFX.Ids.SoundId.UI_CANCEL;

    protected const uint SOUND_X2_MOVE   = FFX2.Ids.SoundId.UI_ACTION;
    protected const uint SOUND_X2_ACTION = FFX2.Ids.SoundId.UI_ACTION;
    protected const uint SOUND_X2_CANCEL = FFX2.Ids.SoundId.UI_CANCEL;

    protected const string DIR_X_MENU = "/FFX_Data/GameData/PS3Data/menu/D3D11/";
    protected const string DIR_X_HIKU = "/FFX_Data/GameData/PS3Data/map/hiku/hiku22/2d/tex/D3D11/";

    protected const string DIR_X2_MENU    = "/FFX-2_Data/GameData/PS3Data/menu/D3D11/";
    protected const string DIR_X2_MAHOJIN = "/FFX-2_Data/GameData/PS3Data/menu/menu_mahojin/tex/D3D11/";
    protected const string DIR_X2_PLATE   = "/FFX-2_Data/GameData/PS3Data/menu/menu_plate/tex/D3D11/";

    protected const uint COLOR_BLACK = 0xFF000000;
    protected const uint COLOR_TRANS = 0x00000000;

    protected const float FADE_LENGTH = 0.35f;

    protected static readonly uint SOUND_ID_MOVE   = FhUtil.select(SOUND_X_MOVE,   SOUND_X2_MOVE,   SOUND_X2_MOVE);
    protected static readonly uint SOUND_ID_ACTION = FhUtil.select(SOUND_X_ACTION, SOUND_X2_ACTION, SOUND_X2_ACTION);
    protected static readonly uint SOUND_ID_CANCEL = FhUtil.select(SOUND_X_CANCEL, SOUND_X2_CANCEL, SOUND_X2_CANCEL);

    protected static readonly float ACTION_VOLUME = FhUtil.select(1f, 0.5f, 0.5f);

    protected UiFocus focus;

    // Display
    protected readonly FadeHelper fade;

    protected int selected_mod_idx;
    protected int selected_module_idx;

    protected FhModContext          selected_mod     => settings_map.GetAt(selected_mod_idx).Key;
    protected List<FhModuleContext> selected_modules => settings_map.GetAt(selected_mod_idx).Value;
    protected FhModuleContext       selected_module  => settings_map.GetAt(selected_mod_idx).Value[selected_module_idx];

    protected FhSetting? hovered_setting;

    protected int   indent_level;
    protected float setting_y;

    protected ICapturingRenderer? capturing_renderer;
    protected FhSetting?          captured_setting;

    protected FhSettingsCategory? displayed_settings;

    protected readonly SteppedScrollable scrollable_mods     = new(visible: 11);
    protected readonly SteppedScrollable scrollable_modules  = new(visible: 10);
    protected readonly Scrollable        scrollable_settings = new();

    protected BaseScrollable? current_scrollable;

    protected virtual float font_size => FONT_SIZE_DEFAULT * font_scale;

    protected FhGui.DrawTextConfig text_config => new() {
        font_size = font_size,
    };

    private bool _loaded_all_textures;

    protected virtual FhTexture[] textures => [];

    protected override Vector2 get_ref_size() => new(1920f, 1080f);

    protected FhSettingsUiBase() {
        current_scrollable = null;

        fade = new(0, 0, FADE_LENGTH);
    }

    public override bool init(FhModContext context, FileStream global_state) {
        return base.init(context, global_state)
            && FhApi.Events.Common.GameLoop.PostOpenSettingsMenu .subscribe(post_open)
            && FhApi.Events.Common.GameLoop.PostCloseSettingsMenu.subscribe(post_close);
    }

    private void post_open(EventArgs e) {
        focus = UiFocus.MODS;

        try_load_textures();

        fade.restart(COLOR_BLACK, COLOR_TRANS, FADE_LENGTH);

        scrollable_mods.max = settings_map.Count;
    }

    private void post_close(EventArgs e) {
        unload_textures();
    }


    /* ===== Helper Methods ===== */
    protected string localize(string key) {
        return FhApi.Localization.localize($"{typeof(FhSettingsUi)}.{key}");
    }

    /// <summary>Attempt to load all of the textures the UI requires to display properly.</summary>
    /// <returns>Whether all textures have been successfully loaded.</returns>
    private bool try_load_textures() {
        _loaded_all_textures = true;
        foreach (FhTexture texture in textures) {
            if (!FhApi.Resources.load_texture(texture)) {
                _loaded_all_textures = false;
            }
        }

        return _loaded_all_textures;
    }

    /// <summary>Unload all of the textures the UI requires to display properly.</summary>
    private void unload_textures() {
        foreach (FhTexture texture in textures) {
            if (FhApi.Resources.unload_texture(texture))
                _loaded_all_textures = false;
            else
                _logger.Warning($"Failed to unload texture: {texture.path}");
        }
    }

    protected void fade_out(Action action) {
        fade.restart(
            fade.get_color(),
            COLOR_BLACK,
            FADE_LENGTH * fade.progress,
            action
        );
    }

    protected void select_mod(int mod_idx) {
        focus = UiFocus.MODULES;

        selected_mod_idx        = mod_idx;
        scrollable_mods.hovered = mod_idx;
    }

    protected void hover_module(int module_idx) {
        FhModule hovered_module = selected_modules[module_idx].Module;

        FhInternal.Settings.try_get_settings(hovered_module, out displayed_settings);
    }

    protected void select_module(int module_idx) {
        focus = UiFocus.SETTINGS;

        selected_module_idx        = module_idx;
        scrollable_modules.hovered = module_idx;

        FhInternal.Settings.try_get_settings(selected_module.Module, out displayed_settings);
    }


    /* ===== Interfacing with renderers ===== */
    public bool try_capture_setting(ICapturingRenderer renderer, FhSetting setting) {
        if (capturing_renderer != null) return false;

        capturing_renderer = renderer;
        captured_setting   = setting;

        return true;
    }

    public bool try_release_setting(ICapturingRenderer renderer) {
        if (capturing_renderer != renderer) return false;

        capturing_renderer = null;
        captured_setting   = null;

        return true;
    }

    public void indent() {
        indent_level += 1;
    }

    public void unindent() {
        indent_level -= 1;
    }


    /* ===== Input handling ===== */
    protected bool mouse_hovered(Rect rect, bool require_movement = true) {
        return FhApi.Gui.mouse_hovering(rect, require_movement);
    }

    protected bool mouse_clicked(Rect rect, ImGuiMouseButton button = ImGuiMouseButton.Left, bool repeat = false) {
        return FhApi.Gui.mouse_clicked(rect, button, repeat);
    }

    private void handle_input_mods() {
        if (scrollable_mods.handle_input())
            return;

        if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_confirm)) {
            select_mod(scrollable_mods.hovered);
            return;
        }

        if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_cancel)) {
            //TODO: Close from here
            return;
        }
    }

    private void handle_input_modules() {
        int old_hovered = scrollable_modules.hovered;

        if (scrollable_modules.handle_input()) {
            if (scrollable_modules.hovered != old_hovered)
                hover_module(scrollable_modules.hovered);

            return;
        }

        if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_confirm)) {
            select_module(scrollable_modules.hovered);
            return;
        }

        if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_cancel)) {
            focus = UiFocus.MODS;
            selected_module_idx = 0;
            return;
        }
    }

    private void handle_input_settings() {

    }

    private void handle_input() {
        switch (focus) {
            case UiFocus.MODS:     handle_input_mods();     break;
            case UiFocus.MODULES:  handle_input_modules();  break;
            case UiFocus.SETTINGS: handle_input_settings(); break;
        }
    }


    /* ===== Rendering ===== */
    internal protected override void render_ui() {
        current_scrollable = focus switch {
            UiFocus.MODS    => scrollable_mods,
            UiFocus.MODULES => scrollable_modules,

            _ => null,
        };

        if (!try_load_textures()) return;

        handle_input();

        ui_background();
        ui_help();

        if (focus == UiFocus.MODS) {
            ui_list_mods();
        }
        else {
            ui_list_modules();
            ui_list_settings();
        }

        ui_scrollbars();

        // ui_fade();
    }

    protected abstract void ui_background();
    protected abstract void ui_help();
    protected abstract void ui_list_mods();
    protected abstract void ui_list_modules();
    protected abstract void ui_list_settings();
    protected abstract void ui_scrollbars();
}
