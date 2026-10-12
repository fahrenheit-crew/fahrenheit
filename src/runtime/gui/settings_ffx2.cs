// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime.Gui;

[FhLoad(FhGameId.FFX | FhGameId.FFX2 | FhGameId.FFX2LM)]
public partial class FhSettingsUiX2 : FhSettingsUi {
    /// <summary>Possible elements for the UI to focus on.</summary>
    private enum UiFocus {
        /// <summary>The scrollable list of mods with settings.</summary>
        MODS,

        /// <summary>The list of modules that scrolls out when a mod is selected.</summary>
        MODULES,

        /// <summary>The list of settings that appears when a module is hovered/selected.</summary>
        SETTINGS,
    }

    private const string MENU_D3D11_DIR   = "/FFX-2_Data/GameData/PS3Data/menu/D3D11/";
    private const string MENU_MAHOJIN_DIR = "/FFX-2_Data/GameData/PS3Data/menu/menu_mahojin/tex/D3D11/";
    private const string MENU_PLATE_DIR   = "/FFX-2_Data/GameData/PS3Data/menu/menu_plate/tex/D3D11/";

    private const uint COLOR_BLACK = 0xFF000000;
    private const uint COLOR_TRANS = 0x00000000;

    private const float FADE_LENGTH = 0.35f;

    public const float INDENT_MAX           = 10;
    public const float INDENT_SIZE          = 40f;
    public const float SETTING_MARGIN_SIZE  = 18f;
    public const float CATEGORY_MARGIN_SIZE = 10f;

    // Display
    private readonly FadeHelper _fade;

    private UiFocus _focus;

    private int _selected_mod_idx;
    private int _selected_module_idx;

    private FhModContext          _selected_mod     => settings_map.GetAt(_selected_mod_idx).Key;
    private FhModuleContext       _selected_module  => settings_map.GetAt(_selected_mod_idx).Value[_selected_module_idx];
    private List<FhModuleContext> _selected_modules => settings_map.GetAt(_selected_mod_idx).Value;

    private FhSetting? _hovered_setting;

    private int   _indent_level;
    private float _setting_y;

    private int     _setting_idx; // Used for plate texture UV
    private Vector2 _shadow_pos = new(0f, 0f); // TODO: Properly implement

    private ICapturingRenderer? _capturing_renderer;
    private FhSetting?          _captured_setting;

    private FhSettingsCategory? _displayed_settings;

    private readonly SteppedScrollable _scrollable_mods     = new(visible: 11);
    private readonly SteppedScrollable _scrollable_modules  = new(visible: 10);
    private readonly Scrollable        _scrollable_settings = new();

    private BaseScrollable? _current_scrollable;

    public float font_size => 36f * font_scale;

    private readonly NineSliceHelper  _message_window;

    // Textures
    private bool _loaded_all_textures;

    public readonly FhTexture _texture_menuback = new(Path.Join(MENU_D3D11_DIR,   "menuback.dds.phyre"),             FhTextureType.PHYRE);
    public readonly FhTexture _texture_black0   = new(Path.Join(MENU_PLATE_DIR,   "black0.dds.phyre"),               FhTextureType.PHYRE);
    public readonly FhTexture _texture_mahojin  = new(Path.Join(MENU_MAHOJIN_DIR, "14336_19_0_0_512_512.dds.phyre"), FhTextureType.PHYRE);
    public readonly FhTexture _texture_bk_blue  = new(Path.Join(MENU_D3D11_DIR,   "bk_blue.dds.phyre"),              FhTextureType.PHYRE);
    public readonly FhTexture _texture_freetex  = new(Path.Join(MENU_D3D11_DIR,   "freetex.dds.phyre"),              FhTextureType.PHYRE);
    public readonly FhTexture _texture_plate    = new(Path.Join(MENU_PLATE_DIR,   "12288_19_0_0_256_256.dds.phyre"), FhTextureType.PHYRE);
    public readonly FhTexture _texture_window   = new(Path.Join(MENU_D3D11_DIR,   "window.dds.phyre"),               FhTextureType.PHYRE);
    public readonly FhTexture _texture_wave     = new(Path.Join(MENU_D3D11_DIR,   "texture.dds.phyre"),              FhTextureType.PHYRE);
    public readonly FhTexture _texture_b_menu1  = new(Path.Join(MENU_D3D11_DIR,   "b_menu1.dds.phyre"),              FhTextureType.PHYRE);

    public readonly Vector2 _tex_menuback_size = new( 512f,  512f);
    public readonly Vector2 _tex_black0_size   = new(1024f, 1024f);
    public readonly Vector2 _tex_mahojin_size  = new(2048f, 2048f);
    public readonly Vector2 _tex_bk_blue_size  = new( 512f,  512f);
    public readonly Vector2 _tex_freetex_size  = new(1024f,  768f);
    public readonly Vector2 _tex_plate_size    = new( 512f,  512f);
    public readonly Vector2 _tex_window_size   = new(1024f,  256f);
    public readonly Vector2 _tex_wave_size     = new(1024f, 1024f);
    public readonly Vector2 _tex_b_menu1_size  = new(1024f, 1024f);

    private FhTexture[] _textures => [
        _texture_menuback,
        _texture_black0,
        _texture_mahojin,
        _texture_bk_blue,
        _texture_freetex,
        _texture_plate,
        _texture_window,
        _texture_wave,
        _texture_b_menu1,
    ];

    protected override Vector2 get_ref_size() => new(1920f, 1080f);

    public FhSettingsUiX2() {
        _current_scrollable = null;
        _message_window = NineSliceHelper.create(
            _tex_window_size,
            new(  -1f,  -1f),
            new(1025f, 257f),
            new(   4f,   4f)
        );

        _fade = new(0, 0, FADE_LENGTH);

        FhInternal.Settings.register_setting_renderer<FhSettingsCategory, FhSettingsUiX2, Renderer_Category>();
        FhInternal.Settings.register_setting_renderer<FhSettingToggle, FhSettingsUiX2, Renderer_Toggle>();
    }

    public override bool init(FhModContext context, FileStream global_state) {
        return base.init(context, global_state)
            && FhApi.Events.Common.GameLoop.PostOpenSettingsMenu .subscribe(post_open)
            && FhApi.Events.Common.GameLoop.PostCloseSettingsMenu.subscribe(post_close);
    }

    private void post_open(EventArgs e) {
        _focus = UiFocus.MODS;

        try_load_textures();

        _fade.restart(COLOR_BLACK, COLOR_TRANS, FADE_LENGTH);

        _scrollable_mods.max = settings_map.Count;
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

    private bool mouse_hovered(Rect rect, bool require_movement = true) {
        return FhApi.Gui.mouse_hovering(rect, require_movement);
    }

    private bool mouse_clicked(Rect rect, ImGuiMouseButton button = ImGuiMouseButton.Left, bool repeat = false) {
        return FhApi.Gui.mouse_clicked(rect, button, repeat);
    }

    private void select_mod(int mod_idx) {
        _focus = UiFocus.MODULES;

        _selected_mod_idx = mod_idx;
        _scrollable_mods.hovered = mod_idx;
    }

    private void hover_module(int module_idx) {
        FhModule hovered_module = _selected_modules[module_idx].Module;

        FhInternal.Settings.try_get_settings(hovered_module, out _displayed_settings);
    }

    private void select_module(int module_idx) {
        _focus = UiFocus.SETTINGS;

        _selected_module_idx = module_idx;
        _scrollable_modules.hovered = module_idx;

        FhInternal.Settings.try_get_settings(_selected_module.Module, out _displayed_settings);
    }

    // Functions for interfacing with renderers
    public bool try_capture_setting(ICapturingRenderer renderer, FhSetting setting) {
        if (_capturing_renderer != null) return false;

        _capturing_renderer = renderer;
        _captured_setting = setting;

        return true;
    }

    public bool try_release_setting(ICapturingRenderer renderer) {
        if (_capturing_renderer != renderer) return false;

        _capturing_renderer = null;
        _captured_setting = null;

        return true;
    }

    public void indent() {
        _indent_level += 1;
    }

    public void unindent() {
        _indent_level -= 1;
    }

    // Input handling
    private void handle_input_mods() {
        if (_scrollable_mods.handle_input())
            return;

        if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_confirm)) {
            select_mod(_scrollable_mods.hovered);
            return;
        }

        if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_cancel)) {
            //TODO: Close from here
            return;
        }
    }

    private void handle_input_modules() {
        int old_hovered = _scrollable_modules.hovered;

        if (_scrollable_modules.handle_input()) {
            if (_scrollable_modules.hovered != old_hovered)
                hover_module(_scrollable_modules.hovered);

            return;
        }

        if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_confirm)) {
            select_module(_scrollable_modules.hovered);
            return;
        }

        if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_cancel)) {
            _focus = UiFocus.MODS;
            _selected_module_idx = 0;
            return;
        }
    }

    private void handle_input_settings() {

    }

    private void handle_input() {
        if (_captured_setting != null) {
            if (!FhInternal.Settings.get_setting_renderer<FhSettingsUiX2>(_captured_setting, out FhSettingRenderer? renderer)) {
                _capturing_renderer = null;
                _captured_setting = null;
                return;
            }

            renderer.handle_input(this, _captured_setting);
            return;
        }

        switch (_focus) {
            case UiFocus.MODS:     handle_input_mods();     break;
            case UiFocus.MODULES:  handle_input_modules();  break;
            case UiFocus.SETTINGS: handle_input_settings(); break;
        }
    }

    // Rendering
    internal protected override void render_ui() {
        _current_scrollable = _focus switch {
            UiFocus.MODS => _scrollable_mods,
            UiFocus.MODULES => _scrollable_modules,

            _ => null,
        };

        if (!try_load_textures()) return;

        handle_input();

        ui_background();
        ui_help();

        if (_focus == UiFocus.MODS) {
            ui_list_mods();
        }
        else {
            ui_list_modules();
            ui_settings();
        }

        ui_scrollbars();

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

        UV mahojin_tuv = new Rect {
            pos  = new(   6f,  529f),
            size = new(1508f, 1509f),
        }.as_uv(_tex_mahojin_size);

        UV mahojin_suv = new Rect {
            pos  = new(-335f, -422f),
            size = new(1277f, 1279f),
        }.scale_to_aspect(aspect_helper).as_uv();

        // Background glyph (top left)
        draw.AddImage(
            mahojin,
            mahojin_suv.p0,
            mahojin_suv.p1,
            mahojin_tuv.p0,
            mahojin_tuv.p1,
            0x80FFFFFF
        );

        UV bk_blue_tuv = new Rect {
            pos  = new(  0f,   0f),
            size = new(280f, 369f),
        }.as_uv(_tex_bk_blue_size);

        UV bk_blue_suv = new Rect {
            pos  = new(1537f,  -2f),
            size = new( 383f, 503f),
        }.scale_to_aspect(aspect_helper).as_uv();

        // Background glyph (top right)
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
            pos  = new(   0f, 125f),
            size = new(1920f,   3f),
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

        string text = _focus switch {
            UiFocus.MODS    => FhApi.Localization.localize($"{typeof(FhSettingsUiBase).FullName}.help.mod_list"),
            UiFocus.MODULES => FhApi.Localization.localize($"{typeof(FhSettingsUiBase).FullName}.help.module_list"),

            UiFocus.SETTINGS when _capturing_renderer != null
                => _capturing_renderer.get_input_help_text(),

            UiFocus.SETTINGS => _hovered_setting!.desc,

            _ => throw new NotImplementedException(),
        };

        Vector2 text_pos = bg_screen.left + new Vector2(219f, 0f) * aspect_scale;

        FhApi.Gui.draw_text(
            draw,
            text_pos,
            text,
            font_size,
            true,
            new(Alignment.BEGIN, Alignment.CENTER)
        );
    }

    /// <summary>Render the cursor.</summary>
    /// <param name="target_pos">The position the cursor should point at.</param>
    /// <param name="overlap">Whether the cursor should overlap the target like the vanilla game.</param>
    private void ui_cursor(Vector2 target_pos, bool overlap = true) {
        if (!_texture_freetex.try_use(out ImTextureRef freetex, out _)) {
            return;
        }

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        UV cursor_tuv = new UV(
            new(  9f, 572f),
            new(180f, 478f)
        ).map_to(_tex_freetex_size);

        Vector2 cursor_size = new Vector2(92f, 55f) * aspect_scale;

        float overlap_amount = overlap ? cursor_size.X * 0.1f : 0f;

        Vector2 cursor_top_left = new(
            target_pos.X - cursor_size.X + overlap_amount,
            target_pos.Y - cursor_size.Y / 2f + (4f * aspect_scale.Y) // "+ 4f" Centers the cursor properly
        );

        float loop_time     = (float)(ImGui.GetTime() % 0.53f);
        float loop_progress = loop_time / 0.53f;
        float travel_dist   = 18f * aspect_scale.X;

        // Draw trailing/fade out effect for cursor
        // Ghost Cursor 1
        if (loop_progress >= 0.12f) {
            float offset = -5f * aspect_scale.X;
            float alpha  = 0.25f;

            // Sync movement with other cursors
            if (loop_progress >= 0.48f) {
                float fade_time = (loop_progress - 0.48f) / 0.52f;
                offset += fade_time * (travel_dist * 0.55f);
                alpha *= 1f - fade_time;
            }

            UV ghost_suv = new Rect {
                pos  = cursor_top_left + new Vector2(offset, 0f),
                size = cursor_size,
            }.as_uv();

            uint color = ImGui.ColorConvertFloat4ToU32(new Vector4(1f, 1f, 1f, alpha));

            draw.AddImage(
                freetex,
                ghost_suv.p0,
                ghost_suv.p1,
                cursor_tuv.p0,
                cursor_tuv.p1,
                color
            );
        }

        // Ghost Cursor 2
        if (loop_progress >= 0.25f) {
            float offset = -4f * aspect_scale.X;
            float alpha  = 0.85f;

            // Start moving
            if (loop_progress >= 0.38f && loop_progress < 0.48f) {
                float slide_progress = (loop_progress - 0.38f) / 0.10f;
                offset += slide_progress * (travel_dist * 0.20f);
            }
            // Sync movement with other cursors
            else if (loop_progress >= 0.48f) {
                float fade_time = (loop_progress - 0.48f) / 0.52f;
                offset += (travel_dist * 0.20f) + (fade_time * (travel_dist * 0.55f));
                alpha *= 1f - fade_time;
            }

            UV ghost_suv = new Rect {
                pos  = cursor_top_left + new Vector2(offset, 0f),
                size = cursor_size,
            }.as_uv();

            uint color = ImGui.ColorConvertFloat4ToU32(new Vector4(1f, 1f, 1f, alpha));

            draw.AddImage(
                freetex,
                ghost_suv.p0,
                ghost_suv.p1,
                cursor_tuv.p0,
                cursor_tuv.p1,
                color
            );
        }

        // Main Cursor
        UV main_suv = new Rect {
            pos  = cursor_top_left + new Vector2(loop_progress * travel_dist, 0f),
            size = cursor_size,
        }.as_uv();

        draw.AddImage(
            freetex,
            main_suv.p0,
            main_suv.p1,
            cursor_tuv.p0,
            cursor_tuv.p1
        );
    }

    /// <summary>Renders a mod plate.</summary>
    /// <param name="mod_ctx">The context of the mod to render the plate for.</param>
    /// <param name="global_index">The index of the mod in the mod list.</param>
    /// <param name="local_index">The index of the plate on the screen.</param>
    private void ui_mod(FhModContext mod_ctx, int global_index, int local_index) {
        if (!_texture_plate.try_use(out ImTextureRef plate, out _)) {
            return;
        }

        Vector2 plate_screen_size = new(475f, 60f);

        float plate_screen_dy = plate_screen_size.Y + 18f;

        Rect plate_screen = new Rect {
            pos  = new(35f, 174f + plate_screen_dy * local_index),
            size = plate_screen_size,
        }.scale_to_aspect(aspect_helper);

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        float slice_size  = 64f;
        float plate_max_y = 512f - ((local_index % 8) * slice_size);

        UV plate_tuv = new Rect {
            pos  = new(  0f, plate_max_y - slice_size),
            size = new(512f,               slice_size),
        }.as_uv(_tex_plate_size);

        uint color = 0xFFFFFFFF;

        float border_width = 5f * aspect_scale.Y;

        FhApi.Gui.X2.draw_plate(
            draw,
            plate_screen,
            plate,
            plate_tuv,
            color,
            border_width,
            _shadow_pos
        );

        Vector2 text_pos = plate_screen.center;
        text_pos.Y -= 1f;

        Vector2 text_size = FhApi.Gui.draw_text(
            draw,
            text_pos,
            mod_ctx.Manifest.Name,
            font_size,
            true,
            new(Alignment.CENTER, Alignment.CENTER)
        );

        if (mouse_hovered(plate_screen)) {
            _scrollable_mods.hovered = global_index;
        }

        if (global_index == _scrollable_mods.hovered) {
            Vector2 cursor_target = text_pos;
            cursor_target.X -= text_size.X / 2f;
            cursor_target.X -= 10f * aspect_scale.X;

            if (cursor_target.X < 60f * aspect_scale.X) {
                cursor_target.X = 60f * aspect_scale.X;
            }

            ui_cursor(cursor_target, false);
        }

        if (mouse_clicked(plate_screen)) {
            select_mod(global_index);
        }
    }

    /// <summary>Renders all mod plates.</summary>
    private void ui_list_mods() {
        int max_i = int.Min(_scrollable_mods.max, _scrollable_mods.visible);

        for (int i = 0, mod_i = _scrollable_mods.current; i < max_i; i++, mod_i++) {
            ui_mod(settings_map.GetAt(mod_i).Key, mod_i, i);
        }
    }

    /// <summary>Renders a module plate.</summary>
    /// <param name="module_ctx">The context of the module to render the plate for.</param>
    /// <param name="global_index">The index of the module in the mod list.</param>
    /// <param name="local_index">The index of the plate on the screen.</param>
    private void ui_module(FhModuleContext module_ctx, int global_index, int local_index) {
        if (!_texture_plate.try_use(out ImTextureRef plate, out _)) {
            return;
        }

        Vector2 plate_screen_size = new(415f, 60f);

        float plate_screen_dy = plate_screen_size.Y + 18f;

        float x_pos = 95f;
        // TODO: insert slide in/out animation here when a mod is selected

        Rect plate_screen = new Rect {
            pos  = new(x_pos, 174f + plate_screen_dy * local_index),
            size = plate_screen_size,
        }.scale_to_aspect(aspect_helper);

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        float slice_size  = 64f;
        float plate_max_y = 512f - (((local_index + 1) % 8) * slice_size);

        UV plate_tuv = new Rect {
            pos  = new(  0f, plate_max_y - slice_size),
            size = new(512f,               slice_size),
        }.as_uv(_tex_plate_size);

        uint color = 0xFFFFFFFF;

        float border_width = 5f * aspect_scale.Y;

        FhApi.Gui.X2.draw_plate(
            draw,
            plate_screen,
            plate,
            plate_tuv,
            color,
            border_width,
            _shadow_pos
        );

        Vector2 text_pos = plate_screen.center;
        text_pos.Y -= 1f;

        Vector2 text_size = FhApi.Gui.draw_text(
            draw,
            text_pos,
            module_ctx.Module.ModuleType.Split('.')[^1],
            font_size,
            true,
            new(Alignment.CENTER, Alignment.CENTER)
        );

        if (mouse_hovered(plate_screen)) {
            _scrollable_modules.hovered = global_index;
        }

        if (global_index == _scrollable_modules.hovered) {
            Vector2 cursor_target = text_pos;
            cursor_target.X -= text_size.X / 2f;
            cursor_target.X -= 10f * aspect_scale.X;

            if (cursor_target.X < 60f * aspect_scale.X) {
                cursor_target.X = 60f * aspect_scale.X;
            }

            ui_cursor(cursor_target, false);
        }

        if (mouse_clicked(plate_screen)) {
            select_module(global_index);
        }
    }

    /// <summary>Renders all module plates.</summary>
    private void ui_list_modules() {
        ui_mod(_selected_mod, _selected_mod_idx, 0);

        //TODO: Move this line to when a mod is selected
        _scrollable_modules.max = _selected_modules.Count;

        int max_i = int.Min(_scrollable_modules.max, _scrollable_modules.visible);

        for (int i = 1, module_i = _scrollable_modules.current; i < max_i + 1; i++, module_i++) {
            ui_module(_selected_modules[module_i], module_i, i);
        }
    }

    private void ui_setting(FhSetting setting, Rect outer_bounds) {
        if (!FhInternal.Settings.get_setting_renderer<FhSettingsUiX2>(setting, out FhSettingRenderer? renderer)) {
            Rect debug_bounds = new() {
                pos  = new(610f, 252f + _setting_y),
                size = new(outer_bounds.size.X, 30f),
            };

            FhApi.Gui.draw_text(
                ImGui.GetBackgroundDrawList(),
                debug_bounds.scale_to_aspect(aspect_helper).center,
                $"Renderer not found for {setting.GetType()}.",
                30f * font_scale,
                true,
                new(Alignment.CENTER, Alignment.CENTER)
            );

            _setting_y += 30f;

            return;
        }

        float indent_size = _indent_level * INDENT_SIZE;

        Vector2 pos = new(610f, 252f);
        pos.X += indent_size;
        pos.Y += _setting_y;

        if (pos.Y > outer_bounds.bottom.Y) {
            return;
        }

        Vector2 size = renderer.get_size(this);

        if (size.X == 0f || size.X + pos.X > outer_bounds.right.X) {
            size.X = outer_bounds.size.X - indent_size;
        }

        Rect bounds = new() {
            pos  = pos,
            size = size,
        };

        if (bounds.bottom.Y > outer_bounds.top.Y) {
            ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

            Rect scaled_bounds = bounds.scale_to_aspect(aspect_helper);

            //draw.PushClipRect(scaled_bounds.top_left, scaled_bounds.bottom_right);

            renderer.render(this, setting, bounds);

            //draw.PopClipRect();
        }

        _setting_idx++;
        _setting_y += size.Y + SETTING_MARGIN_SIZE;

        if (setting is FhSettingsCategory category && !category.collapsed) {
            indent();

            foreach (FhSetting inner_setting in category.settings) {
                ui_setting(inner_setting, outer_bounds);
            }

            unindent();
            _setting_y += CATEGORY_MARGIN_SIZE;
        }
    }

    private void ui_settings() {
        // Debugging
        if (_displayed_settings == null) {
            FhInternal.Settings.try_get_settings(_selected_module.Module, out _displayed_settings);
        }

        if (_displayed_settings == null) return;

        _indent_level = 0;
        _setting_y    = 0f;
        _setting_idx  = 0;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        Rect bounds = new() {
            pos  = new( 610f, 202f),
            size = new(1100f, 769f),
        };

        Rect scaled_bounds = bounds.scale_to_aspect(aspect_helper);

        //draw.PushClipRect(scaled_bounds.top_left, scaled_bounds.bottom_right);

        foreach (FhSetting setting in _displayed_settings.settings) {
            ui_setting(setting, bounds);
        }
    }

    /// <summary>Renders a scrollbar.</summary>
    /// <param name="scrollable">The scrollable to draw the scrollbar for.</param>
    /// <param name="track_bounds">The bounding rectangle of the scrollbar's track.</param>
    private void ui_scrollbar<T>(BaseScrollable<T> scrollable, Rect track_bounds)
        where T : INumber<T> {

        if (scrollable.max <= scrollable.visible) {
            return;
        }

        if (!_texture_b_menu1.try_use(out ImTextureRef b_menu1, out _)) {
            return;
        }

        float progress = scrollable.get_progress();

        float visible_percentage = float.CreateChecked(scrollable.visible)
            / float.CreateChecked(scrollable.max);

        float scrollable_height = track_bounds.size.Y;

        float thumb_height_max = 0.8f * scrollable_height;
        float thumb_height_min = float.Min(15f * aspect_scale.Y, thumb_height_max);
        float thumb_height     = float.Clamp(
            scrollable_height * visible_percentage,
            thumb_height_min,
            thumb_height_max
        );

        float thumb_y = scrollable.get_progress() * scrollable_height;

        Rect track = track_bounds;

        Rect thumb = new() {
            pos  = track.top_left + new Vector2(0f, thumb_y),
            size = new(track.size.X, thumb_height),
        };

        Vector2 arrow_size = new(32f, 22f);
        float   arrow_gap  = (arrow_size.Y * 0.6f) + 0.8f;

        Rect arrow_top = new() {
            pos = new(
                track.top.X - arrow_size.X / 2f,
                track.top.Y - arrow_gap - arrow_size.Y
            ),
            size = arrow_size,
        };

        Rect arrow_bottom = new() {
            pos = new(
                track.bottom.X - arrow_size.X / 2f,
                track.bottom.Y + arrow_gap
            ),
            size = arrow_size,
        };

        UV arrow_tuv = new Rect {
            pos  = new(965f, 913f),
            size = new( 53f,  30f),
        }.as_uv(_tex_b_menu1_size);

        uint track_color = 0xFF010101;
        uint thumb_color = 0xFFFCFCFC;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        float thumb_max_y    = scrollable_height - thumb.size.Y;
        float thumb_progress = progress * thumb_max_y;

        Rect scaled_track = track.scale_to_aspect(aspect_helper);
        Rect scaled_thumb = thumb.scale_to_aspect(aspect_helper);

        // Make sure that the thumb is still in the middle of the track after scaling
        scaled_thumb.size.X = float.Round(scaled_track.size.X)
            - (float.Round(scaled_thumb.pos.X) - float.Round(scaled_track.pos.X)) * 2f;

        UV scaled_arrow_top    = arrow_top   .scale_to_aspect(aspect_helper).as_uv();
        UV scaled_arrow_bottom = arrow_bottom.scale_to_aspect(aspect_helper).as_uv();

        float scaled_thumb_max_y = thumb_max_y * aspect_scale.Y;

        draw.AddRectFilled(
            scaled_track.top_left,
            scaled_track.bottom_right,
            track_color
        );

        draw.AddRectFilled(
            scaled_thumb.top_left,
            scaled_thumb.bottom_right,
            thumb_color
        );

        draw.AddImage(
            b_menu1,
            scaled_arrow_top.p0,
            scaled_arrow_top.p1,
            arrow_tuv.p1,
            arrow_tuv.p0
        );

        draw.AddImage(
            b_menu1,
            scaled_arrow_bottom.p0,
            scaled_arrow_bottom.p1,
            arrow_tuv.p0,
            arrow_tuv.p1
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

        if (scrollable is SteppedScrollable stepped) {
            if (mouse_clicked(arrow_top, repeat: true)) {
                stepped.move_hover(-1);
            }

            if (mouse_clicked(arrow_bottom, repeat: true)) {
                stepped.move_hover(1);
            }
        }
        else {
            if (mouse_clicked(arrow_top, repeat: true)) {
                scrollable.scroll(-scrollable.scroll_step_size);
            }
            if (mouse_clicked(arrow_bottom, repeat: true)) {
                scrollable.scroll(scrollable.scroll_step_size);
            }
        }
    }

    /// <summary>Renders all scrollbars.</summary>
    private void ui_scrollbars() {
        Rect scrollbar_mods = new() {
            pos  = new(530f, 202f),
            size = new(  8f, 784f),
        };

        ui_scrollbar(_scrollable_mods, scrollbar_mods);
    }
}
