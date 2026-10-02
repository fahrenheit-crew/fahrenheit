// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime.Gui;

[FhLoad(FhGameId.FFX | FhGameId.FFX2 | FhGameId.FFX2LM)]
public class FhSettingsUiX : FhSettingsUi {
    /// <summary>Possible elements for the UI to focus on.</summary>
    private enum UiFocus {
        /// <summary>The scrollable list of mods with settings.</summary>
        MOD_LIST,

        /// <summary>The list of modules that scrolls out when a mod is selected.</summary>
        MODULE_LIST,

        /// <summary>The list of settings that appears when a module is hovered/selected.</summary>
        SETTINGS_LIST,

        /// <summary>Inputting a setting took over input handling.</summary>
        SETTING_INPUT,
    }

    private const string MENU_D3D11_DIR = "/FFX_Data/GameData/PS3Data/menu/D3D11/";
    private const string HIKU_D3D11_DIR = "/FFX_Data/GameData/PS3Data/map/hiku/hiku22/2d/tex/D3D11/";

    private const uint COLOR_BLACK = 0xFF000000;
    private const uint COLOR_TRANS = 0x00000000;

    private const float FADE_LENGTH = 0.35f;

    // Display
    private readonly FadeHelper _fade;

    private UiFocus _focus;

    private int _selected_mod_idx;
    private int _selected_module_idx;
    private FhSetting? _hovered_setting;

    private FhSettingsCategory? _displayed_settings;

    private Scrollable _scrollable_mods;
    private Scrollable _scrollable_modules;
    // private ContinuousScrollable _scrollable_settings;
    private Scrollable _current_scrollable;

    // Textures
    private bool _loaded_all_textures;

    private readonly FhTexture _texture_help     = new(Path.Join(HIKU_D3D11_DIR, "0_0_512_448_0.dds.phyre"), FhTextureType.PHYRE);
    private readonly FhTexture _texture_bg       = new(Path.Join(MENU_D3D11_DIR, "ffx_bg.dds.phyre"),        FhTextureType.PHYRE);
    private readonly FhTexture _texture_menu_new = new(Path.Join(MENU_D3D11_DIR, "menu_new.dds.phyre"),      FhTextureType.PHYRE);

    private readonly Vector2 _tex_help_size     = new(2048f, 2048f);
    private readonly Vector2 _tex_bg_size       = new(2048f, 1024f);
    private readonly Vector2 _tex_menu_new_size = new(2048f, 1024f);

    private FhTexture[] _textures => [
        _texture_help,
        _texture_bg,
        _texture_menu_new,
    ];

    protected override Vector2 get_ref_size() => new(1920f, 1080f);

    public FhSettingsUiX() {
        _current_scrollable = null;

        _fade = new(0, 0, FADE_LENGTH);
    }

    public override bool init(FhModContext context, FileStream global_state) {
        return base.init(context, global_state)
            && FhApi.Events.Common.GameLoop.PostOpenSettingsMenu.subscribe(post_open)
            && FhApi.Events.Common.GameLoop.PostCloseSettingsMenu.subscribe(post_close);
    }

    private void post_open(EventArgs e) {
        _focus = UiFocus.MOD_LIST;

        try_load_textures();

        _fade.restart(COLOR_BLACK, COLOR_TRANS, FADE_LENGTH);
    }

    private void post_close(EventArgs e) {
        unload_textures();
    }

    /// <summary>Attempt to load all of the textures the UI requires to display properly.</summary>
    /// <returns>Whether all textures have been successfully loaded.</returns>
    private bool try_load_textures() {
        Span<FhTexture> textures = _textures;

        _loaded_all_textures = true;
        foreach (FhTexture texture in textures) {
            if (!FhApi.Resources.load_game_texture_2d(texture)) {
                _loaded_all_textures = false;
            }
        }

        return _loaded_all_textures;
    }

    /// <summary>Unload all of the textures the UI requires to display properly.</summary>
    private void unload_textures() {
        Span<FhTexture> textures = _textures;

        foreach (FhTexture texture in textures) {
            if (FhApi.Resources.unload_texture(texture))
                _loaded_all_textures = false;
            else
                _logger.Warning($"Failed to unload texture: {texture.path}");
        }
    }


    // Helper functions
    private void fade_out(Action action) {
        _fade.restart(
            _fade.get_color(),
            COLOR_BLACK,
            FADE_LENGTH * _fade.progress,
            action
        );
    }

    private bool mouse_hovered(Rect rect) {
        return !ImGui.GetIO().WantCaptureMouse
            && ImGui.GetIO().MouseDelta.LengthSquared() > 0
            && FhApi.Gui.mouse_hovering(rect);
    }

    private bool mouse_clicked(Rect rect, ImGuiMouseButton button = ImGuiMouseButton.Left, bool repeat = false) {
        return !ImGui.GetIO().WantCaptureMouse
            && FhApi.Gui.mouse_clicked(rect, button, repeat);
    }

    private string? get_input_help_text() {
        Type setting_type = _hovered_setting!.GetType();
        return setting_type switch {
            _ when setting_type == typeof(FhSettingText)     => "Input desired text",
            _ when setting_type == typeof(FhSettingNumber<>) => "Input desired number",

            _ => null,
        };
    }

    private bool has_settings(FhModuleContext module_ctx) {
        return FhInternal.Settings.try_get(module_ctx.Module, out _);
    }

    private bool has_settings(FhModContext mod_ctx) {
        foreach (FhModuleContext module_ctx in mod_ctx.Modules) {
            if(has_settings(module_ctx))
                return true;
        }

        return false;
    }

    // Input handling
    private void handle_input() {

    }

    // Rendering
    internal protected override void render_ui() {
        _current_scrollable = _focus switch {
            _ => null,
        };

        if (!try_load_textures()) return;

        handle_input();

        ui_background();
        ui_help();

        ui_modlist();

        // ui_scrollbar_mods();
        //
        // ui_fade();
    }

    /// <summary>Render the background.</summary>
    private void ui_background() {
        if (!_texture_bg.try_use(out ImTextureRef bg, out _)) {
            return;
        }

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        UV tex_uv = new Rect {
            pos  = new(   0f,    0f),
            size = new(1920f, 1024f),
        }.as_uv(_tex_bg_size);

        UV screen_uv = new Rect {
            pos  = new(   0f,    0f),
            size = new(1920f, 1080f),
        }.scale_to_aspect(aspect_helper).as_uv();

        draw.AddImage(bg, screen_uv.p0, screen_uv.p1, tex_uv.p0, tex_uv.p1);
    }

    /// <summary>Render the help text.</summary>
    private void ui_help() {
        if (!_texture_help.try_use(out ImTextureRef help, out _)) {
            return;
        }

        UV bg_tuv = new Rect {
            pos  = new( 154f, 1761f),
            size = new(1740f,  134f),
        }.as_uv(_tex_help_size);

        // Saving this one as a rect to more easily calculate the text position later
        Rect bg_screen = new Rect {
            pos  = new( 151f, 80f),
            size = new(1618f, 70f),
        }.scale_to_aspect(aspect_helper);

        UV bg_suv = bg_screen.as_uv();

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        draw.AddImage(help, bg_suv.p0, bg_suv.p1, bg_tuv.p0, bg_tuv.p1);

        //TODO: Add localization
        string text = _focus switch {
            UiFocus.MOD_LIST      => "Select mod to configure",
            UiFocus.MODULE_LIST   => "Select module to configure",
            UiFocus.SETTINGS_LIST => FhApi.Localization.localize(_hovered_setting!.desc),
            UiFocus.SETTING_INPUT => get_input_help_text() ?? FhApi.Localization.localize(_hovered_setting!.desc),

            _ => throw new NotImplementedException(),
        };

        Vector2 text_pos = bg_screen.left + new Vector2(24f, 5f) * aspect_scale;

        float font_size = 36f * font_scale;

        FhApi.Gui.draw_text(
            draw,
            text_pos,
            text,
            font_size,
            true,
            new(Alignment.BEGIN, Alignment.CENTER)
        );
    }

    private void ui_mod(FhModContext mod_ctx, int global_index, int local_index) {
        if (!_texture_menu_new.try_use(out ImTextureRef menu_new, out _)) {
            return;
        }

        Vector2 plate_tex_size    = new(477f, 65f);
        Vector2 plate_screen_size = new(477f, 65f);

        float[] plate_tex_dy_array =
            [ 0f, 75f, 150f, 224f, 299f, 374f, 449f, 523f, 598f, 673f, 748f ];

        float plate_tex_dy    = plate_tex_dy_array[global_index % 11];
        float plate_screen_dy = plate_screen_size.Y + 12f;

        float plate_shadow_height = 5f * aspect_scale.Y;

        UV plate_tuv = new Rect {
            pos  = new(1120f, 959f - plate_tex_dy),
            size = plate_tex_size,
        }.as_uv(_tex_menu_new_size);

        Rect plate_screen = new Rect {
            pos  = new(32f, 182f + plate_screen_dy * local_index),
            size = plate_screen_size,
        }.scale_to_aspect(aspect_helper);

        UV plate_suv = plate_screen.as_uv();

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        draw.AddImage(menu_new, plate_suv.p0, plate_suv.p1, plate_tuv.p0, plate_tuv.p1);

        Vector2 text_pos = plate_screen.center;
        text_pos.Y -= plate_shadow_height;
        text_pos.Y += 1f * aspect_scale.Y;

        float font_size = 36f * font_scale;

        FhApi.Gui.draw_text(
            draw,
            text_pos,
            mod_ctx.Manifest.Name,
            font_size,
            true,
            new(Alignment.CENTER, Alignment.CENTER)
        );
    }

    private static FhModContext? _dbg_mod_ctx;
    private void ui_modlist() {
        if (_dbg_mod_ctx is null) {
            foreach (FhModContext ctx in FhApi.Mods.get_mods()) {
                if (ctx.Manifest.Id != "fhr") continue;

                _dbg_mod_ctx = ctx;
                break;
            }
        }

        for (int i = 0; i < 11; i++) {
            ui_mod(_dbg_mod_ctx!, i, i);
        }
    }
}
