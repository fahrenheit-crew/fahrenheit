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

    private Scrollable _scrollable_mods = new() {
        visible = 11,
        max     = 50,
    };
    private Scrollable _scrollable_modules;
    // private ContinuousScrollable _scrollable_settings;

    private Scrollable? _current_scrollable;

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

    private bool has_settings(FhModuleContext module_ctx) {
        return FhInternal.Settings.try_get(module_ctx.Module, out _);
    }

    private bool has_settings(FhModContext mod_ctx) {
        foreach (FhModuleContext module_ctx in mod_ctx.Modules) {
            if (has_settings(module_ctx))
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

        ui_scrollbars();

        // ui_fade();
    }

    /// <summary>Draws the highlights/shadows for the save slot texture.</summary>
    private void draw_highlight_shadow(ImDrawListPtr draw, Rect bounds, float thickness, uint highlight, uint shadow) {
        // Top Highlight
        draw.AddRectFilled(
            bounds.top_left,
            new Vector2(bounds.top_right.X, bounds.top_left.Y + thickness),
            highlight
        );

        // Left Highlight
        draw.AddRectFilled(
            new Vector2(bounds.top_left.X, bounds.top_left.Y + thickness),
            new Vector2(bounds.top_left.X + thickness, bounds.bottom_left.Y),
            highlight
        );

        // Bottom Shadow - intentional overlap with left highlight to mimic the vanilla game!
        draw.AddRectFilled(
            new Vector2(bounds.top_left.X, bounds.bottom_left.Y - thickness),
            bounds.bottom_right,
            shadow
        );

        // Right Shadow
        draw.AddRectFilled(
            bounds.top_right + new Vector2(-thickness, thickness),
            new Vector2(bounds.bottom_right.X, bounds.bottom_right.Y - thickness),
            shadow
        );
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

        Vector2 text_pos = bg_screen.left + new Vector2(219f, 0f) * aspect_scale;

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
        if (!_texture_plate.try_use(out ImTextureRef plate, out _)) {
            return;
        }

        // Texture coordinates
        int   tex_idx     = (local_index % 8) + 1;
        float slice_size  = 64f;
        float plate_max_y = slice_size * tex_idx;

        Vector2 plate_screen_size = new(477f, 65f);

        float plate_screen_dy = plate_screen_size.Y + 12f;

        UV plate_tuv = new Rect {
            pos  = new(  0f, plate_max_y - slice_size),
            size = new(512f, slice_size),
        }.as_uv(_tex_plate_size);

        Rect plate_screen = new Rect {
            pos  = new(32f, 182f + plate_screen_dy * local_index),
            size = plate_screen_size,
        }.scale_to_aspect(aspect_helper);

        UV plate_suv = plate_screen.as_uv();

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        Vector2 shadow_offset = 7f * aspect_scale;

        draw.AddRectFilled(
            plate_suv.p0 + shadow_offset,
            plate_suv.p1 + shadow_offset,
            0xA5000000
        );

        draw.AddImage(
            plate,
            plate_suv.p0,
            plate_suv.p1,
            plate_tuv.p0,
            plate_tuv.p1,
            0xFFE4F0F1
        );

        // Draw shading/edges on the plate texture for definition
        float border_thickness = 5f * aspect_scale.Y;
        uint  highlight        = 0x28FFFFFF;
        uint  shadow           = 0x80000000;

        draw_highlight_shadow(
            draw,
            plate_screen,
            border_thickness,
            highlight,
            shadow
        );

        Vector2 text_pos = plate_screen.center * aspect_scale;

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

    private void ui_scrollbar(Scrollable scrollable, Rect track_bounds) {
        if (scrollable.max <= scrollable.visible) {
            return;
        }

        float progress = scrollable.get_progress();

        float visible_percentage = scrollable.visible / (float)scrollable.max;

        float thumb_margin = 2f * aspect_scale.X;

        float scrollable_height = track_bounds.size.Y - thumb_margin * 2f;

        float thumb_height_max = 0.8f * scrollable_height;
        float thumb_height_min = float.Min(60f * aspect_scale.Y, thumb_height_max);
        float thumb_height     = float.Clamp(
            scrollable_height * visible_percentage,
            thumb_height_min,
            thumb_height_max
        );

        float thumb_y = scrollable.get_progress() * scrollable_height;

        Rect track = track_bounds;

        Rect thumb = new() {
            pos  = track.top_left + new Vector2(thumb_margin, thumb_margin + thumb_y),
            size = new(track.size.X - thumb_margin * 2f, thumb_height),
        };

        Vector2 triangle_size = new(36f, 18f);
        float   triangle_gap  = triangle_size.Y * 0.8f;

        Rect triangle_top = new() {
            pos = new(
                track.top.X - triangle_size.X / 2f,
                track.top.Y - triangle_gap - triangle_size.Y
            ),
            size = triangle_size,
        };

        Rect triangle_bottom = new() {
            pos = new(
                track.bottom.X - triangle_size.X / 2f,
                track.bottom.Y + triangle_gap
            ),
            size = triangle_size,
        };

        uint track_color = 0xFF000000;

        uint gradient_top    = 0xFFCCCCCC;
        uint gradient_bottom = 0xFF818181;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        float thumb_max_y    = scrollable_height - thumb.size.Y;
        float thumb_progress = progress * thumb_max_y;

        Rect scaled_track = track.scale_to_aspect(aspect_helper);
        Rect scaled_thumb = thumb.scale_to_aspect(aspect_helper);

        // Make sure that the thumb is still in the middle of the track after scaling
        scaled_thumb.size.X = float.Round(scaled_track.size.X)
            - (float.Round(scaled_thumb.pos.X) - float.Round(scaled_track.pos.X)) * 2f;

        Rect scaled_triangle_top    = triangle_top   .scale_to_aspect(aspect_helper);
        Rect scaled_triangle_bottom = triangle_bottom.scale_to_aspect(aspect_helper);

        float scaled_thumb_max_y = thumb_max_y * aspect_scale.Y;

        draw.AddRectFilled(
            scaled_track.top_left,
            scaled_track.bottom_right,
            track_color
        );

        draw.AddRectFilledMultiColor(
            scaled_thumb.top_left,
            scaled_thumb.bottom_right,
            gradient_top,
            gradient_top,
            gradient_bottom,
            gradient_bottom
        );

        FhApi.Gui.draw_triangle_filled_multi_color(
            draw,
            scaled_triangle_top,
            0,
            gradient_bottom,
            gradient_bottom,
            0xFFBEBEBE
        );

        FhApi.Gui.draw_triangle_filled_multi_color(
            draw,
            scaled_triangle_bottom,
            2,
            gradient_bottom,
            gradient_bottom,
            0xFFBEBEBE
        );

        // Handle input
        if (scrollable != _current_scrollable) return;

        // Allow grabbing the thumb on the entire track width
        Rect expanded_thumb = thumb.expand(
            new(track.size.X - thumb.size.X),
            new(Alignment.CENTER, Alignment.CENTER)
        );

        float expanded_track_height = thumb_max_y + (track.size.X - thumb.size.X);

        // if (mouse_clicked(expanded_thumb)) {
        //     _scrollbar_dragging = true;
        //     _scrollbar_held_pos = ImGui.GetMousePos() - expanded_thumb.pos;
        // }
        //
        // if (_scrollbar_dragging) {
        //     Vector2 new_held_pos = ImGui.GetMousePos() - expanded_thumb.pos;
        //
        //     float drag_delta     = new_held_pos.Y - _scrollbar_held_pos!.Value.Y;
        //     float progress_delta = drag_delta / expanded_track_height;
        //
        //     _current_scrollable.set_progress(progress + progress_delta, true);
        //
        //     if (ImGui.IsMouseReleased(ImGuiMouseButton.Left)) {
        //         _scrollbar_dragging = false;
        //         _scrollbar_held_pos = null;
        //     }
        //
        //     return;
        // }

        if (mouse_clicked(triangle_top, repeat: true)) {
            scrollable.move_hover(-1);
        }

        if (mouse_clicked(triangle_bottom, repeat: true)) {
            scrollable.move_hover(1);
        }
    }

    private void ui_scrollbars() {
        Rect scrollbar_mods = new() {
            pos  = new(527f, 208f),
            size = new( 17f, 757f),
        };

        ui_scrollbar(_scrollable_mods, scrollbar_mods);
    }
}
