// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime.Gui;

[FhLoad(FhGameId.FFX | FhGameId.FFX2 | FhGameId.FFX2LM)]
public class FhSettingsUiX2 : FhSettingsUi {
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

    private const string MENU_D3D11_DIR   = "/FFX-2_Data/GameData/PS3Data/menu/D3D11/";
    private const string MENU_MAHOJIN_DIR = "/FFX-2_Data/GameData/PS3Data/menu/menu_mahojin/tex/D3D11/";
    private const string MENU_PLATE_DIR   = "/FFX-2_Data/GameData/PS3Data/menu/menu_plate/tex/D3D11/";

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

    private readonly FhTexture _texture_menuback = new(Path.Join(MENU_D3D11_DIR,   "menuback.dds.phyre"),             FhTextureType.PHYRE);
    private readonly FhTexture _texture_mahojin  = new(Path.Join(MENU_MAHOJIN_DIR, "14336_19_0_0_512_512.dds.phyre"), FhTextureType.PHYRE);
    private readonly FhTexture _texture_black0   = new(Path.Join(MENU_PLATE_DIR,   "black0.dds.phyre"),               FhTextureType.PHYRE);
    private readonly FhTexture _texture_bk_blue  = new(Path.Join(MENU_D3D11_DIR,   "bk_blue.dds.phyre"),              FhTextureType.PHYRE);
    private readonly FhTexture _texture_freetex  = new(Path.Join(MENU_D3D11_DIR,   "freetex.dds.phyre"),              FhTextureType.PHYRE);
    private readonly FhTexture _texture_plate    = new(Path.Join(MENU_PLATE_DIR,   "12288_19_0_0_256_256.dds.phyre"), FhTextureType.PHYRE);

    private readonly Vector2 _tex_menuback_size = new( 512f,  512f);
    private readonly Vector2 _tex_mahojin_size  = new(2048f, 2048f);
    private readonly Vector2 _tex_black0_size   = new(1024f, 1024f);
    private readonly Vector2 _tex_bk_blue_size  = new( 512f,  512f);
    private readonly Vector2 _tex_freetex_size  = new(1024f,  768f);
    private readonly Vector2 _tex_plate_size    = new( 512f,  512f);

    private FhTexture[] _textures => [
        _texture_menuback,
        _texture_mahojin,
        _texture_black0,
        _texture_bk_blue,
        _texture_freetex,
        _texture_plate,
    ];

    protected override Vector2 get_ref_size() => new(1920f, 1080f);

    public FhSettingsUiX2() {
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
            _ when setting_type == typeof(FhSettingText) => "Input desired text",
            _ when setting_type == typeof(FhSettingNumber<>) => "Input desired number",

            _ => null,
        };
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

        // ui_scrollbar_mods();
        //
        // ui_fade();
    }

    /// <summary>Render the background.</summary>
    private void ui_background() {
        if (!_texture_menuback.try_use(out ImTextureRef menuback, out _)
         || !_texture_mahojin .try_use(out ImTextureRef mahojin,  out _)
         || !_texture_bk_blue .try_use(out ImTextureRef bk_blue,  out _)
         || !_texture_black0  .try_use(out ImTextureRef black0,   out _)
        ) {
            return;
        }

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        UV screen_uv = new Rect {
            pos  = new(   0f,    0f),
            size = new(1920f, 1080f),
        }.scale_to_aspect(aspect_helper).as_uv();

        // Draws a scissor over the screen to prevent the mahojin glyph drawing out of bounds
        draw.PushClipRect(screen_uv.p0, screen_uv.p1, false);

        UV menuback_tuv = new Rect {
            pos  = new(  0f,   0f),
            size = new(512f, 512f),
        }.as_uv(_tex_menuback_size);

        // Draw the background in 4 chunks, top/bottom, left/right
        Rect menuback_screen = new Rect {
            pos  = new(  0f,   0f),
            size = new(960f, 665f),
        };

        Rect[] menuback_rects = [
            menuback_screen,
            menuback_screen with { pos = new(menuback_screen.size.X, menuback_screen.pos.Y) },
            menuback_screen with { pos = new(menuback_screen.pos.X, menuback_screen.size.Y) },
            menuback_screen with { pos = menuback_screen.size },
        ];

        foreach (Rect rect in menuback_rects) {
            UV suv = rect.scale_to_aspect(aspect_helper).as_uv();
            draw.AddImage(menuback, suv.p0, suv.p1, menuback_tuv.p0, menuback_tuv.p1);
        }

        draw.AddRectFilled(
            screen_uv.p0,
            screen_uv.p1,
            0x40000000
        );

        UV mahojin_tuv = new Rect {
            pos  = new(   6f,  529f),
            size = new(1508f, 1509f),
        }.as_uv(_tex_mahojin_size);

        UV mahojin_suv = new Rect {
            pos  = new(-335f, -422f),
            size = new(1277f, 1279f),
        }.scale_to_aspect(aspect_helper).as_uv();

        // Background glyph TL
        draw.AddImage(
            mahojin,
            mahojin_suv.p0,
            mahojin_suv.p1,
            mahojin_tuv.p0,
            mahojin_tuv.p1
        );

        UV black0_tuv = new Rect {
            pos  = new(   0f,    0f),
            size = new(1024f, 1024f),
        }.as_uv(_tex_black0_size);

        draw.AddImage(
            black0,
            screen_uv.p0,
            screen_uv.p1,
            black0_tuv.p0,
            black0_tuv.p1
        );

        UV bk_blue_tuv = new Rect {
            pos  = new(  0f,   0f),
            size = new(280f, 369f),
        }.as_uv(_tex_bk_blue_size);

        UV bk_blue_suv = new Rect {
            pos  = new(1537f,  -2f),
            size = new( 383f, 503f),
        }.scale_to_aspect(aspect_helper).as_uv();

        // Background glyph TR
        draw.AddImage(
            bk_blue,
            bk_blue_suv.p0,
            bk_blue_suv.p1,
            bk_blue_tuv.p0,
            bk_blue_tuv.p1
        );

        draw.PopClipRect();
    }

    /// <summary>Render the help text.</summary>
    private void ui_help() {
        if (!_texture_freetex.try_use(out ImTextureRef freetex, out _)) {
            return;
        }

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        uint bg_grad_l = 0xFF000000; // black
        uint bg_grad_r = 0x00000000; // transparent black

        // Saving this one as a rect to more easily calculate the text position later
        Rect bg_screen = new Rect {
            pos  = new(   0f, 86f),
            size = new(1920f, 46f),
        }.scale_to_aspect(aspect_helper);

        UV bg_suv = bg_screen.as_uv();

        draw.AddRectFilledMultiColor(
            bg_suv.p0,
            bg_suv.p1,
            bg_grad_l,
            bg_grad_r,
            bg_grad_r,
            bg_grad_l
        );

        uint accent_grad_l = 0xFF006F6F; // yellow
        uint accent_grad_r = 0x00006F6F; // transparent yellow

        UV accent_suv = new Rect {
            pos  = new(   0f, 124f),
            size = new(1920f,   4f),
        }.scale_to_aspect(aspect_helper).as_uv();

        draw.AddRectFilledMultiColor(
            accent_suv.p0,
            accent_suv.p1,
            accent_grad_l,
            accent_grad_r,
            accent_grad_r,
            accent_grad_l
        );

        UV title_tuv = new Rect {
            pos  = new(641f, 576f),
            size = new(143f,  60f),
        }.as_uv(_tex_freetex_size);

        UV title_suv = new Rect {
            pos  = new(147f, 81f),
            size = new( 60f, 23f),
        }.scale_to_aspect(aspect_helper).as_uv();

        draw.AddImage(
            freetex,
            title_suv.p0,
            title_suv.p1,
            title_tuv.p0,
            title_tuv.p1
        );

        //TODO: Add localization
        string text = _focus switch {
            UiFocus.MOD_LIST      => "Select mod to configure",
            UiFocus.MODULE_LIST   => "Select module to configure",
            UiFocus.SETTINGS_LIST => FhApi.Localization.localize(_hovered_setting!.desc),
            UiFocus.SETTING_INPUT => get_input_help_text() ?? FhApi.Localization.localize(_hovered_setting!.desc),

            _ => throw new NotImplementedException(),
        };

        Vector2 text_pos = bg_screen.left;
        text_pos.X += 219f * aspect_scale.X;

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
}
