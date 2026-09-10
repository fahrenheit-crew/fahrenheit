// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime.Gui;

/*
 * Some conventions are assumed across the various methods in this file:
 * Variables named '*_tuv' are texture UVs
 * Variables named '*_suv' are on-screen UVs
 *
 * Notably, we lie about on-screen UVs being UVs; they are, in fact, pixel coordinates.
 * This is because usage of the UV type is still beneficial, and the distinction
 * may as well not exist past the actual calculation.
 */

/// <summary>The default save UI renderer for Final Fantasy X.</summary>
[FhLoad(FhGameId.FFX)]
public sealed class FhSaveUiX : FhSaveUi {
    /// <summary>Possible open windows of the save/load menu.</summary>
    private enum UiMode {
        /// <summary>The list of saves to save/load/compile from.</summary>
        SAVE_LIST = 0,

        /// <summary>The save set selection window.</summary>
        SET_SWAP = 1,

        /// <summary>The popup for displaying save/load errors and notifications.</summary>
        SAVE_POPUP = 2,
    }

    /// <summary>Targets the player can put their cursor on.</summary>
    private enum UiFocus {
        /// <summary>The scrollable list of either saves or sets.</summary>
        LIST = 0,

        /// <summary>The active set button that opens the set list.</summary>
        ACTIVE_SET = 1,
    }

    private const string MAP_ICONS_DIR  = "/FFX_Data/GameData/PS3Data/savedataicons/";
    private const string MENU_D3D11_DIR = "/FFX_Data/GameData/PS3Data/menu/D3D11/";

    private const uint COLOR_BLACK = 0xFF000000;
    private const uint COLOR_TRANS = 0x00000000;

    private UiMode  _mode;
    private UiFocus _focus;

    private          float      _fade_length = FhUtil.select(0.5f, 0.35f, 0.01f);
    private readonly FadeHelper _fade;

    private readonly List<string> _set_list = [ ];

    private bool _loaded_all_textures;

    private readonly Dictionary<string, FhTexture> _map_icon_textures = [ ];

    private readonly FhTexture _texture_map_icon_default = new(Path.Join(MAP_ICONS_DIR, "0.png"), FhTextureType.PNG);

    private readonly FhTexture _texture_bg           = new(Path.Join(MENU_D3D11_DIR, "ffx_bg.dds.phyre"),       FhTextureType.PHYRE);
    private readonly FhTexture _texture_battle       = new(Path.Join(MENU_D3D11_DIR, "battle.dds.phyre"),       FhTextureType.PHYRE);
    private readonly FhTexture _texture_battle_kuang = new(Path.Join(MENU_D3D11_DIR, "battle_kuang.dds.phyre"), FhTextureType.PHYRE);
    private readonly FhTexture _texture_faces        = new(Path.Join(MENU_D3D11_DIR, "face_ply.dds.phyre"),     FhTextureType.PHYRE);
    private readonly FhTexture _texture_meswin       = new(Path.Join(MENU_D3D11_DIR, "meswin.dds.phyre"),       FhTextureType.PHYRE);
    private readonly FhTexture _texture_summonbg     = new(Path.Join(MENU_D3D11_DIR, "summonbg.dds.phyre"),     FhTextureType.PHYRE);

    private readonly Vector2 _tex_map_icon_size     = new(310f, 210f);

    private readonly Vector2 _tex_bg_size           = new(2048f, 1024f);
    private readonly Vector2 _tex_battle_size       = new(1024f, 1024f);
    private readonly Vector2 _tex_battle_kuang_size = new(2048f, 1024f);
    private readonly Vector2 _tex_faces_size        = new(1024f,  512f);
    private readonly Vector2 _tex_meswin_size       = new(1024f, 1024f);
    private readonly Vector2 _tex_summonbg_size     = new(1024f, 1024f);

    private bool is_saving => FhApi.Saves.get_system_mode(out FhSaveSystemMode? system_mode) && system_mode is FhSaveSystemMode.SAVE;

    private readonly NineSliceHelper  _savefile_border_helper;

    private readonly Scrollable _scrollable_saves = new() {
        visible = 5,
    };

    private readonly Scrollable _scrollable_sets = new() {
        visible = 9,
    };

    private Scrollable _current_scrollable;

    private bool     _scrollbar_dragging;
    private Vector2? _scrollbar_held_pos;

    // We block input when fading out, for example so you can't requests two loads of a file.
    // We block input when fading in as well, but only for a short time to make the UI feel snappy.
    private bool is_fade_blocking_input => !(_fade.is_done || (_fade.color_to == COLOR_TRANS && _fade.progress > 0.3f));
    private bool should_handle_input => !_scrollbar_dragging && !is_fade_blocking_input;

    public FhSaveUiX() {
        _current_scrollable     = _scrollable_saves;
        _savefile_border_helper = NineSliceHelper.create(
            _tex_meswin_size,
            new(385f, 544f),
            new(450f, 609f),
            new(  9f,   9f)
        );

        _fade = new(0, 0, _fade_length);
    }

    public override bool init(FhModContext context, FileStream global_state) {
        return base.init(context, global_state)
            && FhApi.Events.Common.GameLoop.PostOpenSaveMenu .subscribe(post_open)
            && FhApi.Events.Common.GameLoop.PostCloseSaveMenu.subscribe(post_close);
    }

    protected override Vector2 get_ref_size() => new(1600f, 900f);

    protected internal override void render_ui() {
        _current_scrollable = _mode switch {
            UiMode.SET_SWAP => _scrollable_sets,
            _               => _scrollable_saves,
        };

        if (!try_load_textures()) return;

        handle_input();

        ui_background();
        ui_help();

        if (_mode == UiMode.SET_SWAP) {
            ui_setswap();
        }
        else {
            ui_savecount();
            ui_savelist();
            ui_change_set();
        }

        ui_scrollbar();

        ui_fade();
    }

    private void fade_out(Action action) {
        _fade.restart(
            _fade.get_color(),
            COLOR_BLACK,
            _fade_length * _fade.progress,
            action
        );
    }

    private bool handle_input_list() {
        if (_mode == UiMode.SAVE_LIST) {
            if (_scrollable_saves.max == 0) {
                _focus = UiFocus.ACTIVE_SET;
                return false;
            }

            if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_up)
             && _current_scrollable.hovered == 0
            ) {
                _focus = UiFocus.ACTIVE_SET;
                return true;
            }
        }

        _current_scrollable.handle_input();

        if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_confirm)) {
            int hovered = _current_scrollable.hovered;

            if (_mode == UiMode.SAVE_LIST) {
                if (is_saving && hovered == 0) {
                    FhApi.Saves.save(0);
                }
                else {
                    FhSaveDisplayData save = FhApi.Saves.display_data[hovered];
                    fade_out(() => execute(save.slot));
                }

                return true;
            }

            if (_mode == UiMode.SET_SWAP) {
                string hovered_set = _set_list[hovered];
                switch_set(hovered_set);

                return true;
            }
        }

        return false;
    }

    private bool handle_input_active_set() {
        if (_mode == UiMode.SAVE_LIST && FhApi.Gui.is_any_pressed(FhApi.Gui.keys_down) && _current_scrollable.max > 0) {
            _focus = UiFocus.LIST;
            _current_scrollable.hovered = _current_scrollable.current;

            return true;
        }

        if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_confirm)) {
            change_mode(UiMode.SET_SWAP);
            return true;
        }

        return false;
    }

    private void handle_input() {
        if (!should_handle_input) return;

        switch (_focus) {
            case UiFocus.LIST:
                if (handle_input_list())
                    return;
                break;

            case UiFocus.ACTIVE_SET:
                if (handle_input_active_set())
                    return;
                break;

            default: throw new NotImplementedException();
        }

        if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_cancel)) {
            if (_mode == UiMode.SET_SWAP)
                change_mode(UiMode.SAVE_LIST);
            else
                fade_out(() => FhApi.Saves.exit_cancel());
        }
    }

    private void post_open(EventArgs e) {
        _mode  = UiMode.SAVE_LIST;
        _focus = UiFocus.LIST;

        populate_map_icons();
        try_load_textures();

        _set_list.Clear();
        _set_list.AddRange(FhApi.Saves.get_sets());

        _scrollable_sets .max = _set_list.Count;
        _scrollable_saves.max = is_saving
            ? FhApi.Saves.get_slots_used() + 1 // Add one for New Save Data button
            : FhApi.Saves.display_data.Count;

        _fade.restart(COLOR_BLACK, COLOR_TRANS, _fade_length);
    }

    private void post_close(EventArgs e) {
        _scrollable_saves.reset();
        _scrollable_sets .reset();

        unload_textures();

        _map_icon_textures.Clear();
    }

    private void change_mode(UiMode new_mode) {
        _scrollable_saves.reset();
        _scrollable_sets .reset();
        _focus = UiFocus.LIST;
        _mode  = new_mode;
    }

    private void execute(int slot) {
        FhApi.Saves.get_system_mode(out FhSaveSystemMode? system_mode);
        switch (system_mode) {
            case FhSaveSystemMode.SAVE: FhApi.Saves.save(slot);      break;
            case FhSaveSystemMode.LOAD: FhApi.Saves.load(slot);      break;
            case FhSaveSystemMode.ALBD: FhApi.Saves.copy_albd(slot); break;

            default: throw new InvalidOperationException();
        }
    }

    private void switch_set(string set_name) {
        FhApi.Saves.switch_active_set(set_name);

        populate_map_icons();
        try_load_textures();

        change_mode(UiMode.SAVE_LIST);
    }

    private void populate_map_icons() {
        _map_icon_textures.Clear();

        foreach (FhSaveDisplayData save in FhApi.Saves.display_data) {
            string map = Encoding.UTF8.GetString(save.icon_map);
            if (_map_icon_textures.TryGetValue(map, out _)) continue;

            FhTexture map_icon = new(Path.Join(MAP_ICONS_DIR, $"{map}.png"), FhTextureType.PNG);
            _map_icon_textures.Add(map, map_icon);
        }
    }

    /// <summary>Attempt to load all of the textures the renderer requires to display properly.</summary>
    /// <returns>Whether all textures have been successfully loaded.</returns>
    private bool try_load_textures() {
        Span<FhTexture> textures = [
            _texture_bg,
            _texture_battle,
            _texture_battle_kuang,
            _texture_faces,
            _texture_meswin,
            _texture_summonbg,
            // _texture_map_icon_default,
            // .. _map_icon_textures.Values,
        ];

        _loaded_all_textures = true;
        foreach (FhTexture texture in textures) {
            if (!FhApi.Resources.load_game_texture_2d(texture)) {
                _loaded_all_textures = false;
            }
        }

        return _loaded_all_textures;
    }

    /// <summary>Unload all of the textures the renderer requires to display properly.</summary>
    private void unload_textures() {
        Span<FhTexture> textures = [
            _texture_bg,
            _texture_battle,
            _texture_battle_kuang,
            _texture_faces,
            _texture_meswin,
            _texture_summonbg,
            // _texture_map_icon_default,
            // .. _map_icon_textures.Values,
        ];

        foreach (FhTexture texture in textures) {
            if (FhApi.Resources.unload_texture(texture))
                _loaded_all_textures = false;
            else
                _logger.Warning($"Failed to unload texture: {texture.path}");
        }
    }

    private bool mouse_hovered(Rect rect) {
        return should_handle_input
            && !ImGui.GetIO().WantCaptureMouse
            && ImGui.GetIO().MouseDelta.LengthSquared() > 0
            && FhApi.Gui.mouse_hovering(rect);
    }

    private bool mouse_clicked(Rect rect, ImGuiMouseButton button = ImGuiMouseButton.Left, bool repeat = false) {
        return should_handle_input
            && !ImGui.GetIO().WantCaptureMouse
            && FhApi.Gui.mouse_clicked(rect, button, repeat);
    }

    /// <summary>Render the background for the save/load screen.</summary>
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
            pos  = new(   0f,   0f),
            size = new(1600f, 900f),
        }.scale_to_aspect(aspect_helper).as_uv();

        draw.AddImage(bg, screen_uv.p0, screen_uv.p1, tex_uv.p0, tex_uv.p1);
    }

    /// <summary>Render the help text for the save/load screen.</summary>
    private unsafe void ui_help() {
        if (!_texture_battle_kuang.try_use(out ImTextureRef battle_kuang, out _)
         || !_texture_meswin      .try_use(out ImTextureRef meswin,       out _)
        ) {
            return;
        }

        if (!FhApi.Saves.get_system_mode(out FhSaveSystemMode? system_mode)) {
            return;
        }

        UV bg_tuv = new Rect {
            pos  = new(   0f, 425f),
            size = new(1600f,  55f),
        }.as_uv(_tex_battle_kuang_size);

        // Saving this one as a rect to more easily calculate the text position later
        Rect bg_screen = new Rect {
            pos  = new( 126f, 82f),
            size = new(1348f, 43f),
        }.scale_to_aspect(aspect_helper);

        UV bg_suv = bg_screen.as_uv();

        UV title_tuv = new Rect {
            pos  = new(670f, 37f),
            size = new( 74f, 22f),
        }.as_uv(_tex_meswin_size);

        UV title_suv = new Rect {
            pos  = new(126f, 67f),
            size = new( 62f, 18f),
        }.scale_to_aspect(aspect_helper).as_uv();

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        draw.AddImage(battle_kuang, bg_suv.p0, bg_suv.p1, bg_tuv.p0, bg_tuv.p1);

        /*
         * Shenanigans time!
         *
         * In the vanilla game, the help background is rendered with
         * a 100% -> 0% alpha gradient going from left to right.
         *
         * We cannot replicate this with just ImGui / DrawList calls.
         * However, `.AddImage()` calls into `.PrimRectUV()`,
         * which reveals potential for shenanigans in its code:
         *
         *     _VtxWritePtr[0].pos = a; _VtxWritePtr[0].uv = uv_a; _VtxWritePtr[0].col = col;
         *     _VtxWritePtr[1].pos = b; _VtxWritePtr[1].uv = uv_b; _VtxWritePtr[1].col = col;
         *     _VtxWritePtr[2].pos = c; _VtxWritePtr[2].uv = uv_c; _VtxWritePtr[2].col = col;
         *     _VtxWritePtr[3].pos = d; _VtxWritePtr[3].uv = uv_d; _VtxWritePtr[3].col = col;
         *     _VtxWritePtr += 4;
         *
         * Armed with this knowledge, we can exploit it by looking directly
         * at the draw commands queued by `.AddImage()`, and manually
         * changing the colors of points B (topright) and C (bottomright) to be transparent!
         */

        ImDrawVert* vtx_ptr = draw.VtxWritePtr.Handle;
        vtx_ptr -= 4;
        vtx_ptr[1].Col = 0;
        vtx_ptr[2].Col = 0;

        draw.AddImage(meswin, title_suv.p0, title_suv.p1, title_tuv.p0, title_tuv.p1);

        //TODO: Add localization
        string text = _mode switch {
            UiMode.SET_SWAP => "Select save set",

            UiMode.SAVE_LIST  or
            UiMode.SAVE_POPUP => system_mode switch {
                FhSaveSystemMode.SAVE => "Select save area",
                FhSaveSystemMode.LOAD or
                FhSaveSystemMode.ALBD => "Select save data",

                //TODO-C#16: Remove this, since FhSaveSystemMode should be a `closed enum`.
                _ => throw new UnreachableException(),
            },

            _ => throw new NotImplementedException(),
        };

        Vector2 text_pos = bg_screen.left;
        text_pos.X += 20f * aspect_scale.X;

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

    /// <summary>Render the active set name and set count.</summary>
    private void ui_savecount() {
        string active_set   = FhApi.Saves.active_set;
        bool   has_autosave = FhApi.Saves.set_has_autosave(active_set);

        int save_count = FhApi.Saves.get_slots_used();

        //TODO: Add localization
        string save_text = has_autosave
            ? $" + {save_count} saves"
            : $"{save_count} saves";

        Vector2 text_pos = new(1435f, 120f);

        float line_height = 32f;
        float font_size   = 36f * font_scale;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        Vector2 saves_text_size = FhApi.Gui.draw_text(
            draw,
            text_pos * aspect_scale + aspect_helper.pos,
            save_text,
            font_size,
            true,
            new(Alignment.END, Alignment.CENTER)
        );

        if (has_autosave) {
            Vector2 autosave_pos = text_pos * aspect_scale + aspect_helper.pos;
            autosave_pos.X -= saves_text_size.X;

            FhApi.Gui.draw_text(
                draw,
                autosave_pos,
                "A",
                font_size,
                true,
                new(Alignment.END, Alignment.CENTER),
                0xFF19D8FF
            );
        }

        Vector2 second_line_pos = text_pos with { Y = text_pos.Y - line_height };

        FhApi.Gui.draw_text(
            draw,
            second_line_pos * aspect_scale + aspect_helper.pos,
            active_set,
            font_size,
            true,
            new(Alignment.END, Alignment.CENTER)
        );
    }

    /// <summary>Render the cursor.</summary>
    /// <param name="target_pos">The position the cursor should point at.</param>
    /// <param name="overlap">Whether the cursor should overlap the target like the vanilla game.</param>
    private void ui_cursor(Vector2 target_pos, bool overlap = true) {
        if (!_texture_meswin.try_use(out ImTextureRef meswin, out _)) {
            return;
        }

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        UV cursor_tuv = new UV(
            new(157f, 244f),
            new( 90f, 200f)
        ).map_to(_tex_meswin_size);

        Vector2 cursor_size = new Vector2(76f, 51f) * aspect_scale;

        float max_offset   = 13f * aspect_scale.X;
        float cycle_length = 0.38f;

        // Multiply by pi so cycle_length can be expressed in seconds.
        float cycle_t = MathF.Sin((float)ImGui.GetTime() * MathF.PI / cycle_length) / 2f + 0.5f;
        float offset  = -(max_offset * cycle_t);

        float overlap_amount = overlap ? cursor_size.X * 0.1f : 0f;

        Vector2 cursor_center = new(
            target_pos.X - cursor_size.X / 2f + overlap_amount - offset,
            target_pos.Y
        );

        UV cursor_suv = new Rect { pos = cursor_center }
            .expand(cursor_size, new(Alignment.CENTER, Alignment.CENTER))
            .as_uv();

        draw.AddImage(
            meswin,
            cursor_suv.p0,
            cursor_suv.p1,
            cursor_tuv.p0,
            cursor_tuv.p1
        );
    }

    /// <summary>Render the set name.</summary>
    private void ui_change_set() {
        if (!_texture_battle_kuang.try_use(out ImTextureRef battle_kuang, out _)) {
            return;
        }

        UV bg_tuv = new Rect {
            pos  = new(  0f, 757f),
            size = new(432f,  12f),
        }.as_uv(_tex_battle_kuang_size, false);

        Rect bg_screen = new Rect {
            pos  = new(550f, 23f),
            size = new(500f, 50f),
        }.scale_to_aspect(aspect_helper);

        UV bg_suv = bg_screen.as_uv();

        if (mouse_hovered(bg_screen)) {
            _focus = UiFocus.ACTIVE_SET;
        }

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();
        draw.AddImage(battle_kuang, bg_suv.p0, bg_suv.p1, bg_tuv.p0, bg_tuv.p1);

        float   font_size = 36f * font_scale;
        Vector2 text_pos  = bg_screen.center;

        //TODO: Add localization
        string text = "Change set";

        Vector2 text_size = FhApi.Gui.draw_text(
            draw,
            text_pos,
            text,
            font_size,
            true,
            new(Alignment.CENTER, Alignment.CENTER)
        );

        if (_focus == UiFocus.ACTIVE_SET) {
            float text_left   = text_pos.X - text_size.X / 2f;
            float text_margin = 33f * aspect_scale.X;

            Vector2 cursor_target = bg_screen.center with {
                X = text_left - text_margin,
            };

            ui_cursor(cursor_target, false);
        }

        // Input handling
        if (mouse_clicked(bg_screen)) {
            change_mode(UiMode.SET_SWAP);
        }
    }

    private void ui_set(int set_idx, string name, int save_count) {
        if (!_texture_summonbg.try_use(out ImTextureRef summonbg, out _)) {
            return;
        }

        int start_idx = _scrollable_sets.get_clip_start();

        // Texture coordinates
        int   tex_idx    = set_idx % 8;
        float tex_y_diff = 78f * tex_idx;

        UV button_tuv = new Rect {
            pos  = new(   0f, 954f - tex_y_diff),
            size = new(1024f,  70f),
        }.as_uv(_tex_summonbg_size);

        // Screen coordinates
        Rect button = new Rect {
            pos  = new( 290f, 148f),
            size = new(1024f,  70f),
        };

        button.pos.Y += (button.size.Y + 8f) * (set_idx - start_idx);

        Rect button_no_shadow = button.expand(new Vector2(-6f, -5f), new(Alignment.BEGIN, Alignment.BEGIN));
        Rect button_scaled    = button.scale_to_aspect(aspect_helper);

        UV button_suv = button_scaled.as_uv();

        Vector2 name_pos = button_no_shadow.left;
        name_pos.X += 30f;
        name_pos = name_pos * aspect_scale + aspect_helper.pos;

        Vector2 save_count_pos = button_no_shadow.right;
        save_count_pos.X -= 40f;
        save_count_pos = save_count_pos * aspect_scale + aspect_helper.pos;

        // Drawing time
        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();
        ImGuiIOPtr    io   = ImGui.GetIO();

        float font_size = 36f * font_scale;

        if (mouse_hovered(button_scaled)) {
            _focus = UiFocus.LIST;
            _scrollable_sets.hovered = set_idx;
        }

        bool hovering = _scrollable_sets.hovered == set_idx;

        draw.AddImage(
            summonbg,
            button_suv.p0,
            button_suv.p1,
            button_tuv.p0,
            button_tuv.p1,
            hovering ? 0xFFFDF5E6 : 0xFFFFFFFF
        );

        FhApi.Gui.draw_text(
            draw,
            name_pos,
            name,
            font_size,
            true,
            new(Alignment.BEGIN, Alignment.CENTER)
        );

        bool has_autosave = FhApi.Saves.set_has_autosave(name);

        Vector2 save_count_size = FhApi.Gui.draw_text(
            draw,
            save_count_pos,
            has_autosave ? $" + {save_count - 1} saves" : $"{save_count} saves",
            font_size,
            true,
            new(Alignment.END, Alignment.CENTER)
        );

        if (has_autosave) {
            Vector2 autosave_pos = save_count_pos;
            autosave_pos.X -= save_count_size.X;

            FhApi.Gui.draw_text(
                draw,
                autosave_pos,
                "A",
                font_size,
                true,
                new(Alignment.END, Alignment.CENTER),
                0xFF19D8FF
            );
        }

        if (hovering) {
            ui_cursor(button_no_shadow.scale_to_aspect(aspect_helper).left);
        }

        // Handle input
        if (mouse_clicked(button_scaled)) {
            io.WantCaptureMouse = true;
            switch_set(name);
        }
    }

    private void ui_setswap() {
        if (_mode != UiMode.SET_SWAP) return;

        int start_idx = _scrollable_sets.get_clip_start();
        int end_idx   = _scrollable_sets.get_clip_end();

        for (int set_idx = start_idx; set_idx < end_idx; set_idx++) {
            string set_name   = _set_list[set_idx];
            int    save_count = FhApi.Saves.get_save_counts()[set_name];

            ui_set(set_idx, set_name, save_count);
        }
    }

    private void ui_savelist() {
        List<FhSaveDisplayData> display_data = FhApi.Saves.display_data;
        if (is_saving && FhApi.Saves.set_has_autosave(FhApi.Saves.active_set)) {
            display_data = display_data[1..];
        }

        _scrollable_saves.max = is_saving
            ? display_data.Count + 1
            : display_data.Count;

         if (!is_saving && display_data.Count == 0) {
            ui_no_saves();
            return;
        }

        int start = _scrollable_saves.get_clip_start();
        int end   = _scrollable_saves.get_clip_end();

        for (int i = start; i < end; i++) {
            bool is_new_data = is_saving && i == 0;

            FhSaveDisplayData save = is_new_data
                ? new FhSaveDisplayData { slot = 0 }
                : display_data[is_saving ? i - 1 : i];

            ui_savefile(i, save);
        }
    }

    private void ui_no_saves() {
        if (!_texture_meswin.try_use(out ImTextureRef meswin, out _)) {
            return;
        }

        if (!FhApi.Saves.get_system_mode(out FhSaveSystemMode? system_mode)) {
            return;
        }

        //TODO: Add localization
        string message =  system_mode switch {
            FhSaveSystemMode.LOAD => "No saved data. Change set or return to the main menu.",
            FhSaveSystemMode.ALBD => "No saved data. Change set or return.",

            // When saving, we should always display "New Save Data" instead.
            _ => throw new UnreachableException(),
        };

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        float font_size = 36f * font_scale;

        ImGui.PushFont(null, font_size);
        Vector2 text_size = ImGui.CalcTextSize(message);
        ImGui.PopFont();

        Vector2 text_margin = new Vector2(30f, 40f) * aspect_scale;
        Vector2 window_size = text_margin * 2 + text_size;

        Rect window = new Rect {
            pos  = FhApi.Gui.display_size / 2f,
            size = Vector2.Zero,
        }.expand(window_size, new(Alignment.CENTER, Alignment.CENTER));

        uint gradient_tl = 0xFF9E4541;
        uint gradient_tr = 0xFF91264F;
        uint gradient_bl = 0xFF8F3950;
        uint gradient_br = 0xFF822856;

        draw.AddRectFilledMultiColor(
            window.top_left,
            window.bottom_right,
            gradient_tl,
            gradient_tr,
            gradient_br,
            gradient_bl
        );

        NineSliceHelper screen_border_helper = NineSliceHelper.create(
            Vector2.One,
            window,
            new Vector2(9f, 9f) * aspect_scale
        );

        for (int slice_idx = 0; slice_idx < 9; slice_idx++) {
            Vector2[] tex_uv    = _savefile_border_helper.get_uvs(slice_idx);
            Vector2[] screen_uv =    screen_border_helper.get_uvs(slice_idx);

            draw.AddImage(meswin, screen_uv[0], screen_uv[3], tex_uv[0], tex_uv[3]);
        }

        FhApi.Gui.draw_text(
            draw,
            window.center,
            message,
            font_size,
            true,
            new(Alignment.CENTER, Alignment.CENTER)
        );
    }

    private void ui_savefile(int index, FhSaveDisplayData save) {
        if (save.slot == 0 && is_saving) {
            ui_newdata();
            return;
        }

        if (!_scrollable_saves.is_within_clip(index)) {
            return;
        }

        if (!_texture_meswin.try_use(out ImTextureRef meswin, out _)
         || !_texture_faces .try_use(out ImTextureRef faces , out _)
        ) {
            return;
        }

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        Rect save_rect = new Rect {
            pos  = new( 133f, 146f), // Topmost position
            size = new(1302f, 127f),
        };

        float save_gap = 10f;

        save_rect.pos.Y += (save_rect.size.Y + save_gap) * (index - _scrollable_saves.current);

        // We set the hovered state early to potentially use it later.
        if (mouse_hovered(save_rect.scale_to_aspect(aspect_helper))) {
            _focus = UiFocus.LIST;
            _scrollable_saves.hovered = index;
        }

        /* ===== Autosave Background ===== */
        if (save.slot == 0) {
            UV save_suv = save_rect.scale_to_aspect(aspect_helper).as_uv();

            draw.AddRectFilled(save_suv.p0, save_suv.p1, 0x25FFFFFF);
        }

        /* ===== Border ===== */
        NineSliceHelper screen_border_helper = NineSliceHelper.create(
            Vector2.One,
            save_rect.scale_to_aspect(aspect_helper),
            new Vector2(9f, 9f) * aspect_scale
        );

        for (int slice_idx = 0; slice_idx < 9; slice_idx++) {
            Vector2[] tex_uv    = _savefile_border_helper.get_uvs(slice_idx);
            Vector2[] screen_uv =    screen_border_helper.get_uvs(slice_idx);

            draw.AddImage(meswin, screen_uv[0], screen_uv[3], tex_uv[0], tex_uv[3]);
        }

        // The border effectively has two sizes: 4f and 9f.
        // We only want to respect the outermost border, so we use 4f.
        Vector2 save_border_size = new(4f);

        /* ===== Header Gradient ===== */
        float header_size = 35f;

        uint header_grad_l = 0xFF000000; // black
        uint header_grad_r = 0x00800000; // transparent blue

        UV header_uv = new Rect {
            pos  = save_rect.pos + save_border_size,
            size = save_rect.size with { Y = header_size },
        }.scale_to_aspect(aspect_helper).as_uv();

        draw.AddRectFilledMultiColor(
            header_uv.p0,
            header_uv.p1,
            header_grad_l,
            header_grad_r,
            header_grad_r,
            header_grad_l
        );

        /* ===== Party Icons ===== */
        Span<UV> face_tuvs = stackalloc UV[8];

        for (int i = 0; i < 8; i++) {
            float x1 = 200f * (i % 4);
            float x2 = x1 + 200f;

            float y1 = i > 3 ? 312f : 512f;
            float y2 = y1 - 200f;

            face_tuvs[i] = new UV(
                new(x1, y1),
                new(x2, y2)
            ).map_to(_tex_faces_size);
        }

        Vector2 face_size = new(83f);
        float   face_gap  = 5f;

        Rect face = new Rect {
            pos = new(
                save_rect.left.X + face_gap,
                save_rect.bottom.Y - save_border_size.Y - face_size.Y
            ),
            size = face_size,
        };

        UV face_suv = face.scale_to_aspect(aspect_helper).as_uv();

        Vector2 face_screen_offset = new Vector2(face_size.X + face_gap, 0f) * aspect_scale;

        FhSaveHeader header = MemoryMarshal.Read<FhSaveHeader>(save.header);

        foreach (int chr in header.formation[..3]) {
            if (chr == 0xFF) continue;

            UV tuv = face_tuvs[chr];

            draw.AddImage(faces, face_suv.p0, face_suv.p1, tuv.p0, tuv.p1);

            face_suv = face_suv.move(face_screen_offset);
        }

        /* ===== Location Icon ===== */
        string    map     = Encoding.UTF8.GetString(save.icon_map);
        FhTexture tex_map = _map_icon_textures.GetValueOrDefault(map, _texture_map_icon_default);

        // if (tex_map.try_use(out ImTextureRef map_icon, out _)) {
        //     UV tuv = new(new(0f, 1f), new(1f, 0f));
        //
        //     UV suv = new Rect {
        //         pos  = save_rect.pos + new Vector2(1085f, 4f),
        //         size = new(212f, 120f),
        //     }.scale_to_aspect(_aspect_helper).as_uv();
        //
        //     draw.AddImage(map_icon, suv.p0, suv.p1, tuv.p0, tuv.p1);
        // }

        /* ===== Text ===== */

        /* ===== Header ===== */
        float text_margin_left    = 10f;
        float text_margin_between = 40f;

        Vector2 text_pos_left =
              save_rect.pos
            + save_border_size
            + new Vector2( text_margin_left, header_size / 2f);

        float font_size = 36f * font_scale;

        //TODO: Add localization
        string slot_text   = save.slot == 0 ? "Autosave" : save.slot_str;
        string create_time = save.create_time.ToString(@"yyyy\/M\/d H\:mm\:ss");

        Vector2 text_size = FhApi.Gui.draw_text(
            draw,
            text_pos_left * aspect_scale + aspect_helper.pos,
            slot_text,
            font_size,
            true,
            new(Alignment.BEGIN, Alignment.CENTER),
            save.slot == 0 ? 0xFF19D8FF : 0xFFFFFFFF // Autosave text is yellow
        );

        text_pos_left = text_pos_left * aspect_scale + aspect_helper.pos;
        text_pos_left.X += text_size.X + text_margin_between * aspect_scale.X;

        // For some reason, if the slot text is "Autosave", the margin is doubled
        if (save.slot == 0) {
            text_pos_left.X += text_margin_between * aspect_scale.X;
        }

        FhApi.Gui.draw_text(
            draw,
            text_pos_left,
            save.location,
            font_size,
            true,
            new(Alignment.BEGIN, Alignment.CENTER)
        );

        float map_icon_size = 212f;

        Vector2 text_pos_right =
              save_rect.top_right
            + new Vector2(-save_border_size.X, save_border_size.Y)
            + new Vector2(-(map_icon_size + text_margin_between), header_size / 2f);

        FhApi.Gui.draw_text(
            draw,
            text_pos_right * aspect_scale + aspect_helper.pos,
            create_time,
            font_size,
            true,
            new(Alignment.END, Alignment.CENTER)
        );

        /* ===== Details ===== */
        float margin_from_header    =  5f;
        float margin_from_last_face = 31f;
        float line_height           = 40f;

        Vector2 text_pos = save_rect.pos + new Vector2(
            face_size.X * 3 + face_gap * 2 + margin_from_last_face,
            header_size + margin_from_header + line_height / 2f
        );

        FhApi.Gui.draw_text(
            draw,
            text_pos * aspect_scale + aspect_helper.pos,
            save.player_name,
            font_size,
            true,
            new(Alignment.BEGIN, Alignment.CENTER)
        );

        text_pos.Y += line_height;

        FhApi.Gui.draw_text(
            draw,
            text_pos * aspect_scale + aspect_helper.pos,
            save.play_time,
            font_size,
            true,
            new(Alignment.BEGIN, Alignment.CENTER)
        );

        /* ===== Cursor ===== */
        if (_focus == UiFocus.LIST && _scrollable_saves.hovered == index) {
            ui_cursor(save_rect.scale_to_aspect(aspect_helper).left);
        }

        // Handle input
        if (mouse_clicked(save_rect.scale_to_aspect(aspect_helper))) {
            fade_out(() => execute(save.slot));
        }
    }

    private void ui_newdata() {
        if (!_texture_meswin.try_use(out ImTextureRef meswin, out _)) {
            return;
        }

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        Rect save_rect = new Rect {
            pos  = new( 133f, 146f),
            size = new(1302f, 127f),
        };

        // The border effectively has two sizes: 4f and 9f.
        // We only want to respect the outermost border, so we use 4f.
        Vector2 save_border_size = new(4f);

        if (mouse_hovered(save_rect.scale_to_aspect(aspect_helper))) {
            _focus = UiFocus.LIST;
            _scrollable_saves.hovered = 0;
        }

        /* ===== Border ===== */
        NineSliceHelper screen_border_helper = NineSliceHelper.create(
            Vector2.One,
            save_rect.scale_to_aspect(aspect_helper),
            new Vector2(9f, 9f) * aspect_scale
        );

        for (int slice_idx = 0; slice_idx < 9; slice_idx++) {
            Vector2[] tex_uv    = _savefile_border_helper.get_uvs(slice_idx);
            Vector2[] screen_uv =    screen_border_helper.get_uvs(slice_idx);

            draw.AddImage(meswin, screen_uv[0], screen_uv[3], tex_uv[0], tex_uv[3]);
        }

        /* ===== Header Gradient ===== */
        uint grad_l = 0xFF000000; // black
        uint grad_r = 0x00800000; // transparent blue

        UV header_uv = new Rect {
            pos  = save_rect.pos + save_border_size,
            size = save_rect.size with { Y = 35f },
        }.scale_to_aspect(aspect_helper).as_uv();

        draw.AddRectFilledMultiColor(header_uv.p0, header_uv.p1, grad_l, grad_r, grad_r, grad_l);

        /* ===== New Save Data Text ===== */
        //TODO: Add localization
        string text = "New Save Data";

        float header_height = 35f;

        Vector2 text_pos = save_rect.top_left + new Vector2(
            12f,
            header_height + (save_rect.size.Y - header_height) / 2f
        );

        float font_size = 36f * font_scale;

        FhApi.Gui.draw_text(
            draw,
            text_pos * aspect_scale + aspect_helper.pos,
            text,
            font_size,
            true,
            new(Alignment.BEGIN, Alignment.CENTER)
        );

        /* ===== Cursor ===== */
        if (_focus == UiFocus.LIST && _scrollable_saves.hovered == 0) {
            ui_cursor(save_rect.scale_to_aspect(aspect_helper).left);
        }

        // Handle input
        if (mouse_clicked(save_rect.scale_to_aspect(aspect_helper))) {
            fade_out(() => FhApi.Saves.save(0));
        }
    }

    private void ui_scrollbar() {
        if (_current_scrollable.max <= _current_scrollable.visible) {
            return;
        }

        Rect track = new() {
            pos  = new(1461f, 174f),
            size = new(  14f, 630f),
        };

        uint track_color = 0xFF000000;

        Vector2 thumb_margin = new(1f);
        Vector2 thumb_size   = new(12f, 50f);

        uint thumb_color_top    = 0xFFCBCBCB;
        uint thumb_color_bottom = 0xFF808080;

        Vector2 triangle_size = new(30f, 15f);
        float   triangle_gap  = 13f;

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

        // In the vanilla game, the triangles are a linear gradient
        // #c0c0c3 at the tip to #838383 at the base.
        uint triangle_color = 0xFFA0A0A0;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        float progress = _current_scrollable.get_progress();

        float scrollable_track_height = track.size.Y - thumb_margin.Y * 2 - thumb_size.Y;
        float thumb_progress          = progress * scrollable_track_height;

        Rect thumb = new() {
            pos = new(
                track.top_left.X + thumb_margin.X,
                track.top_left.Y + thumb_margin.Y + thumb_progress
            ),
            size = thumb_size,
        };

        track = track.scale_to_aspect(aspect_helper);
        thumb = thumb.scale_to_aspect(aspect_helper);

        triangle_top    = triangle_top   .scale_to_aspect(aspect_helper);
        triangle_bottom = triangle_bottom.scale_to_aspect(aspect_helper);

        scrollable_track_height *= aspect_scale.Y;

        draw.AddRectFilled(
            track.top_left,
            track.bottom_right,
            track_color
        );

        draw.AddRectFilledMultiColor(
            thumb.top_left,
            thumb.bottom_right,
            thumb_color_top,
            thumb_color_top,
            thumb_color_bottom,
            thumb_color_bottom
        );

        draw.AddTriangleFilled(
            triangle_top.top,
            triangle_top.bottom_left,
            triangle_top.bottom_right,
            triangle_color
        );

        draw.AddTriangleFilled(
            triangle_bottom.bottom,
            triangle_bottom.top_left,
            triangle_bottom.top_right,
            triangle_color
        );

        // Handle input

        // Allow grabbing the thumb on the entire track width
        Rect expanded_thumb = thumb.expand(
            new(track.size.X - thumb.size.X),
            new(Alignment.CENTER, Alignment.CENTER)
        );

        float expanded_track_height = scrollable_track_height + (track.size.X - thumb.size.X);

        if (mouse_clicked(expanded_thumb)) {
            _scrollbar_dragging = true;
            _scrollbar_held_pos = ImGui.GetMousePos() - expanded_thumb.pos;
        }

        if (_scrollbar_dragging) {
            Vector2 new_held_pos = ImGui.GetMousePos() - expanded_thumb.pos;

            float drag_delta     = new_held_pos.Y - _scrollbar_held_pos!.Value.Y;
            float progress_delta = drag_delta / expanded_track_height;

            _current_scrollable.set_progress(progress + progress_delta, true);

            if (ImGui.IsMouseReleased(ImGuiMouseButton.Left)) {
                _scrollbar_dragging = false;
                _scrollbar_held_pos = null;
            }

            return;
        }

        if (mouse_clicked(triangle_top, repeat: true)) {
            _current_scrollable.move_hover(-1);
        }

        if (mouse_clicked(triangle_bottom, repeat: true)) {
            _current_scrollable.move_hover(1);
        }
    }

    private void ui_fade() {
        if (_fade.is_done) return;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();
        draw.AddRectFilled(Vector2.Zero, FhApi.Gui.display_size, _fade.get_color());

        _fade.tick(ImGui.GetIO().DeltaTime);
    }
}
