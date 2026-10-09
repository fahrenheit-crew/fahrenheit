// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime.Gui;

[FhLoad(FhGameId.FFX | FhGameId.FFX2 | FhGameId.FFX2LM)]
public partial class FhSettingsUiX : FhSettingsUi {
    /// <summary>Possible elements for the UI to focus on.</summary>
    private enum UiFocus {
        /// <summary>The scrollable list of mods with settings.</summary>
        MOD_LIST,

        /// <summary>The list of modules that scrolls out when a mod is selected.</summary>
        MODULE_LIST,

        /// <summary>The list of settings that appears when a module is hovered/selected.</summary>
        SETTINGS_LIST,
    }

    private const string MENU_D3D11_DIR = "/FFX_Data/GameData/PS3Data/menu/D3D11/";
    private const string HIKU_D3D11_DIR = "/FFX_Data/GameData/PS3Data/map/hiku/hiku22/2d/tex/D3D11/";

    private const uint COLOR_BLACK = 0xFF000000;
    private const uint COLOR_TRANS = 0x00000000;

    private const float FADE_LENGTH = 0.35f;

    public const float INDENT_MAX          = 10;
    public const float INDENT_SIZE         = 40f;
    public const float MAX_SETTING_SIZE    = 1100f;
    public const float SETTING_MARGIN_SIZE = 22f;

    // Display
    private readonly FadeHelper _fade;

    private UiFocus _focus;

    private int _selected_mod_idx;
    private int _selected_module_idx;
    private FhSetting? _hovered_setting;

    private int   _indent_level;
    private float _setting_y;

    private ICapturingRenderer? _capturing_renderer;
    private FhSetting?          _captured_setting;

    private FhSettingsCategory? _displayed_settings;

    private readonly Scrollable _scrollable_mods = new() {
        visible = 11,
        max     = 50,
    };

    private readonly Scrollable _scrollable_modules = new(){
        visible = 10,
    };
    // private ContinuousScrollable _scrollable_settings;

    private Scrollable? _current_scrollable;

    // Textures
    private bool _loaded_all_textures;

    public readonly FhTexture _texture_help         = new(Path.Join(HIKU_D3D11_DIR, "0_0_512_448_0.dds.phyre"), FhTextureType.PHYRE);
    public readonly FhTexture _texture_bg           = new(Path.Join(MENU_D3D11_DIR, "ffx_bg.dds.phyre"),        FhTextureType.PHYRE);
    public readonly FhTexture _texture_meswin       = new(Path.Join(MENU_D3D11_DIR, "meswin.dds.phyre"),        FhTextureType.PHYRE);
    public readonly FhTexture _texture_menu_new     = new(Path.Join(MENU_D3D11_DIR, "menu_new.dds.phyre"),      FhTextureType.PHYRE);
    public readonly FhTexture _texture_battle_kuang = new(Path.Join(MENU_D3D11_DIR, "battle_kuang.dds.phyre"),  FhTextureType.PHYRE);

    public readonly Vector2 _tex_help_size         = new(2048f, 2048f);
    public readonly Vector2 _tex_bg_size           = new(2048f, 1024f);
    public readonly Vector2 _tex_meswin_size       = new(1024f, 1024f);
    public readonly Vector2 _tex_menu_new_size     = new(2048f, 1024f);
    public readonly Vector2 _tex_battle_kuang_size = new(2048f, 1024f);

    private FhTexture[] _textures => [
        _texture_help,
        _texture_bg,
        _texture_meswin,
        _texture_menu_new,
        _texture_battle_kuang,
    ];

    protected override Vector2 get_ref_size() => new(1920f, 1080f);

    public FhSettingsUiX() {
        _current_scrollable = null;

        _fade = new(0, 0, FADE_LENGTH);

        FhInternal.Settings.register_setting_renderer<FhSettingToggle, FhSettingsUiX, Renderer_Toggle>();
    }

    public override bool init(FhModContext context, FileStream global_state) {
        return base.init(context, global_state)
            && FhApi.Events.Common.GameLoop.PostOpenSettingsMenu.subscribe(post_open)
            && FhApi.Events.Common.GameLoop.PostCloseSettingsMenu.subscribe(post_close);
    }

    private void post_open(EventArgs e) {
        _focus = UiFocus.MODULE_LIST;

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

    private bool has_settings(FhModuleContext module_ctx) {
        return FhInternal.Settings.try_get_settings(module_ctx.Module, out _);
    }

    private bool has_settings(FhModContext mod_ctx) {
        foreach (FhModuleContext module_ctx in mod_ctx.Modules) {
            if(has_settings(module_ctx))
                return true;
        }

        return false;
    }

    // Functions for interfacing with renderers
    public bool try_capture_setting(ICapturingRenderer renderer, FhSetting setting) {
        if (_capturing_renderer != null) return false;

        _capturing_renderer = renderer;
        _captured_setting   = setting;

        return true;
    }

    public bool try_release_setting(ICapturingRenderer renderer) {
        if (_capturing_renderer != renderer) return false;

        _capturing_renderer = null;
        _captured_setting   = null;

        return true;
    }

    public void indent() {
        _indent_level += 1;
    }

    public void unindent() {
        _indent_level -= 1;
    }

    // Input handling
    private void handle_input() {
        if (_captured_setting != null) {
            if (!FhInternal.Settings.get_setting_renderer<FhSettingsUiX>(_captured_setting, out FhSettingRenderer? renderer)) {
                _capturing_renderer = null;
                _captured_setting   = null;
                return;
            }

            renderer.handle_input(this, _captured_setting);
            return;
        }
    }

    // Rendering
    internal protected override void render_ui() {
        _current_scrollable = _focus switch {
            UiFocus.MOD_LIST    => _scrollable_mods,
            UiFocus.MODULE_LIST => _scrollable_modules,

            _ => null,
        };

        if (!try_load_textures()) return;

        handle_input();

        ui_background();
        ui_help();

        if (_focus == UiFocus.MOD_LIST)
            ui_list_mods();
        else
            ui_list_modules();

        ui_settings();

        ui_scrollbars();

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
            UiFocus.MOD_LIST    => FhApi.Localization.localize($"{typeof(FhSettingsUiBase).FullName}.help.mod_list"),
            UiFocus.MODULE_LIST => FhApi.Localization.localize($"{typeof(FhSettingsUiBase).FullName}.help.module_list"),

            UiFocus.SETTINGS_LIST when _capturing_renderer != null
                => _capturing_renderer.get_input_help_text(),

            UiFocus.SETTINGS_LIST => _hovered_setting!.desc,

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

    /// <summary>Renders a mod plate.</summary>
    /// <param name="mod_ctx">The context of the mod to render the plate for.</param>
    /// <param name="global_index">The index of the mod in the mod list.</param>
    /// <param name="local_index">The index of the plate on the screen.</param>
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
    /// <summary>Renders all mod plates.</summary>
    private void ui_list_mods() {
        if (_dbg_mod_ctx is null) {
            foreach (FhModContext ctx in FhApi.Mods.get_mods()) {
                if (ctx.Manifest.Id != "fhr") continue;

                _dbg_mod_ctx = ctx;
                break;
            }
        }

        int max_i = int.Min(_scrollable_mods.max, _scrollable_mods.visible);

        for (int i = 0, mod_i = _scrollable_mods.current; i < max_i; i++, mod_i++) {
            ui_mod(_dbg_mod_ctx!, mod_i, i);
        }
    }

    /// <summary>Renders a module plate.</summary>
    /// <param name="module_ctx">The context of the module to render the plate for.</param>
    /// <param name="global_index">The index of the module in the mod list.</param>
    /// <param name="local_index">The index of the plate on the screen.</param>
    private void ui_module(FhModuleContext module_ctx, int global_index, int local_index) {
        if (!_texture_menu_new.try_use(out ImTextureRef menu_new, out _)) {
            return;
        }

        Vector2 plate_tex_size    = new(477f, 65f);
        Vector2 plate_screen_size = new(417f, 65f);

        float[] plate_tex_dy_array =
            [ 0f, 75f, 150f, 224f, 299f, 374f, 449f, 523f, 598f, 673f, 748f ];

        float plate_tex_dy    = plate_tex_dy_array[global_index % 11];
        float plate_screen_dy = plate_screen_size.Y + 12f;

        float plate_shadow_height = 5f * aspect_scale.Y;

        uint plate_color_mult = 0xFF9EA3A7;

        UV plate_tuv = new Rect {
            pos  = new(1120f, 959f - plate_tex_dy),
            size = plate_tex_size,
        }.as_uv(_tex_menu_new_size);

        Rect plate_screen = new Rect {
            pos  = new(92f, 182f + plate_screen_dy * local_index),
            size = plate_screen_size,
        }.scale_to_aspect(aspect_helper);

        UV plate_suv = plate_screen.as_uv();

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        draw.AddImage(
            menu_new,
            plate_suv.p0,
            plate_suv.p1,
            plate_tuv.p0,
            plate_tuv.p1,
            plate_color_mult
        );

        Vector2 text_pos = plate_screen.center;
        text_pos.Y -= plate_shadow_height;
        text_pos.Y += 1f * aspect_scale.Y;

        float font_size = 36f * font_scale;

        FhApi.Gui.draw_text(
            draw,
            text_pos,
            module_ctx.Module.ModuleType.Split('.')[^1],
            font_size,
            true,
            new(Alignment.CENTER, Alignment.CENTER)
        );
    }

    /// <summary>Renders all module plates.</summary>
    private void ui_list_modules() {
        if (_dbg_mod_ctx is null) {
            foreach (FhModContext ctx in FhApi.Mods.get_mods()) {
                if (ctx.Manifest.Id != "fhr") continue;

                _dbg_mod_ctx = ctx;
                break;
            }
        }

        ui_mod(_dbg_mod_ctx!, _selected_mod_idx, 0);

        _scrollable_modules.max = _dbg_mod_ctx!.Modules.Count;

        int max_i = int.Min(_scrollable_modules.max, _scrollable_modules.visible);
        List<FhModuleContext> modules = _dbg_mod_ctx.Modules;

        for (int i = 1, module_i = _scrollable_modules.current; i < max_i + 1; i++, module_i++) {
            ui_module(modules[module_i], module_i, i);
        }
    }

    private void ui_setting(FhSetting setting) {
        if (!FhInternal.Settings.get_setting_renderer<FhSettingsUiX>(setting, out FhSettingRenderer? renderer)) {
            Rect debug_bounds = new() {
                pos  = new(610f, 202f + _setting_y),
                size = new(MAX_SETTING_SIZE, 30f),
            };

            FhApi.Gui.draw_text(
                ImGui.GetBackgroundDrawList(),
                debug_bounds.scale_to_aspect(aspect_helper).center,
                $"Renderer not found for {setting.GetType()}.",
                22f * font_scale,
                true,
                new(Alignment.CENTER, Alignment.CENTER)
            );

            _setting_y += 30f;

            return;
        }

        float indent_size = _indent_level * INDENT_SIZE;

        Vector2 pos = new(610f, 202f);
        pos.X += indent_size;
        pos.Y += _setting_y;

        Vector2 size = renderer.get_size(this);

        if (size.X == 0f || size.X + pos.X > get_ref_size().X) {
            size.X = MAX_SETTING_SIZE - indent_size;
        }

        Rect bounds = new() {
            pos  = pos,
            size = size,
        };

        renderer.render(this, setting, bounds);

        _setting_y += size.Y + SETTING_MARGIN_SIZE;
    }

    private void ui_settings() {
        // Debugging
        if (_displayed_settings == null) {
            FhModule modlist = FhApi.Mods.get_module<FhModListDisplayModule>()!.Module;

            FhInternal.Settings.try_get_settings(modlist, out _displayed_settings);
        }

        if (_displayed_settings == null) return;

        _indent_level = 0;
        _setting_y    = 0f;

        foreach (FhSetting setting in _displayed_settings.settings) {
            ui_setting(setting);
        }
    }

    /// <summary>Renders a scrollbar.</summary>
    /// <param name="scrollable">The scrollable to draw the scrollbar for.</param>
    /// <param name="track_bounds">The bounding rectangle of the scrollbar's track.</param>
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

        uint gradient_top    = 0xFFCBCBCB;
        uint gradient_bottom = 0xFF808080;

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

        FhApi.Gui.draw_triangle_gradient(
            draw,
            scaled_triangle_top,
            GradientDirection.UP,
            gradient_bottom,
            gradient_bottom,
            gradient_top
        );

        FhApi.Gui.draw_triangle_gradient(
            draw,
            scaled_triangle_bottom,
            GradientDirection.DOWN,
            gradient_bottom,
            gradient_bottom,
            gradient_top
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

    /// <summary>Renders all scrollbars.</summary>
    private void ui_scrollbars() {
        Rect scrollbar_mods = new() {
            pos  = new(527f, 208f),
            size = new( 17f, 777f),
        };

        ui_scrollbar(_scrollable_mods, scrollbar_mods);
    }
}
