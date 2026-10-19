// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

using Fahrenheit.FFX.Ids;

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
[FhLoad(FhGameId.FFX | FhGameId.FFX2 | FhGameId.FFX2LM)]
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

    private const string DIR_MAP_ICONS  = "/FFX_Data/GameData/PS3Data/savedataicons/";
    private const string DIR_MENU_D3D11 = "/FFX_Data/GameData/PS3Data/menu/D3D11/";
    private const string DIR_HIKU_D3D11 = "/FFX_Data/GameData/PS3Data/map/hiku/hiku22/2d/tex/D3D11/";

    private const string FFX2_DIR_MENU_D3D11 = "/FFX-2_Data/GameData/PS3Data/menu/D3D11/";
    private const string FFX2_DIR_MAP_ICONS  = "/FFX-2_Data/GameData/PS3Data/savedataicons/";
    private const string FFX2_DIR_FACE_DATA  = "/FFX-2_Data/GameData/PS3Data/menu/face_data/D3D11/";

    private const uint COLOR_BLACK = 0xFF000000;
    private const uint COLOR_TRANS = 0x00000000;

    private static readonly float ACTION_VOLUME =
        FhGlobal.game_id is FhGameId.FFX
            ? 1.0f
            : 0.5f;

    private UiMode  _mode;
    private UiFocus _focus;

    private readonly float      _fade_length = FhUtil.select(0.5f, 0.35f, 0.0f);
    private readonly FadeHelper _fade;

    private readonly Fahrenheit.Gui.Timer _audio_fade_helper = new(30f);

    private bool _audio_fade_helper_restart = true;

    private readonly List<string> _set_list = [ ];

    private bool _loaded_all_textures;

    // FFX textures
    private readonly Dictionary<string, FhTexture> _map_icon_textures = [ ];

    private readonly FhTexture _texture_map_icon_default = new(Path.Join(DIR_MAP_ICONS, "0.png"), FhTextureType.PNG);

    private readonly FhTexture _texture_bg           = new(Path.Join(DIR_MENU_D3D11, "ffx_bg.dds.phyre"),        FhTextureType.PHYRE);
    private readonly FhTexture _texture_help         = new(Path.Join(DIR_HIKU_D3D11, "0_0_512_448_0.dds.phyre"), FhTextureType.PHYRE);
    private readonly FhTexture _texture_battle_kuang = new(Path.Join(DIR_MENU_D3D11, "battle_kuang.dds.phyre"),  FhTextureType.PHYRE);
    private readonly FhTexture _texture_faces        = new(Path.Join(DIR_MENU_D3D11, "face_ply.dds.phyre"),      FhTextureType.PHYRE);
    private readonly FhTexture _texture_meswin       = new(Path.Join(DIR_MENU_D3D11, "meswin.dds.phyre"),        FhTextureType.PHYRE);
    private readonly FhTexture _texture_summonbg     = new(Path.Join(DIR_MENU_D3D11, "summonbg.dds.phyre"),      FhTextureType.PHYRE);

    private readonly Vector2 _tex_map_icon_size  = new(310f, 210f);

    private readonly Vector2 _tex_bg_size           = new(2048f, 1024f);
    private readonly Vector2 _tex_help_size         = new(2048f, 2048f);
    private readonly Vector2 _tex_battle_kuang_size = new(2048f, 1024f);
    private readonly Vector2 _tex_faces_size        = new(1024f,  512f);
    private readonly Vector2 _tex_meswin_size       = new(1024f, 1024f);
    private readonly Vector2 _tex_summonbg_size     = new(1024f, 1024f);

    // FFX-2 textures
    private readonly Dictionary<string, FhTexture> _ffx2_face_icon_textures = [ ];

    private readonly FhTexture _texture_ffx2_face_icon_default = new(Path.Join(FFX2_DIR_FACE_DATA, "mface_000.dds.phyre"), FhTextureType.PHYRE);
    private readonly FhTexture _texture_ffx2_map_icon_default  = new(Path.Join(FFX2_DIR_MAP_ICONS, "logo_j.png"),          FhTextureType.PNG);

    private readonly FhTexture _texture_freetex = new(Path.Join(FFX2_DIR_MENU_D3D11, "freetex.dds.phyre"), FhTextureType.PHYRE);

    private readonly Vector2 _tex_ffx2_face_size     = new(256f, 256f);
    private readonly Vector2 _tex_ffx2_map_icon_size = new(320f, 176f);

    private readonly Vector2 _tex_freetex_size = new(1024f,  768f);

    private FhTexture[] textures => [
        _texture_bg,
        _texture_help,
        _texture_battle_kuang,
        _texture_faces,
        _texture_meswin,
        _texture_summonbg,

        // _texture_map_icon_default,
        // .. _map_icon_textures.Values,

        _texture_freetex,

        _texture_ffx2_face_icon_default,
        .. _ffx2_face_icon_textures.Values,
    ];

    private bool is_saving => FhApi.Saves.get_system_mode(out FhSaveSystemMode? system_mode) && system_mode is FhSaveSystemMode.SAVE;

    private readonly NineSliceHelper  _savefile_border_helper;

    private readonly SteppedScrollable _scrollable_saves = new(visible: 5);
    private readonly SteppedScrollable _scrollable_sets  = new(visible: 9);

    private SteppedScrollable _current_scrollable;

    private bool     _scrollbar_dragging;
    private Vector2? _scrollbar_held_pos;

    // We block input when fading out, for example so you can't requests two loads of a file.
    // We block input when fading in as well, but only for a short time to make the UI feel snappy.
    private bool is_fade_blocking_input => !(_fade.is_done || (_fade.color_to == COLOR_TRANS && _fade.progress > 0.3f));
    private bool should_handle_input    => !_scrollbar_dragging && !is_fade_blocking_input;

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

    protected override Vector2 get_ref_size() => new(1920f, 1080f);

    protected internal override void render_ui() {
        _current_scrollable = _mode switch {
            UiMode.SET_SWAP => _scrollable_sets,
            _               => _scrollable_saves,
        };

        if (!try_load_textures()) return;

        _audio_fade_helper_restart = true;

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

        if (_audio_fade_helper_restart) {
            _audio_fade_helper.restart();
        }
    }

    private void fade_out(Action action) {
        _fade.restart(
            _fade.get_color(),
            COLOR_BLACK,
            _fade_length * _fade.progress,
            action
        );
    }

    private float get_move_volume() {
        return FhGlobal.game_id is FhGameId.FFX
            ? 0.5f  + (1f - _audio_fade_helper.progress) * 0.5f
            : 0.25f + (1f - _audio_fade_helper.progress) * 0.25f;
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
                FhApi.Audio.play_sound(SoundId.UI_ACTION, ACTION_VOLUME);
                _focus = UiFocus.ACTIVE_SET;
                return true;
            }
        }

        int old_hovered = _current_scrollable.hovered;

        bool scrollable_input_held = _current_scrollable.handle_input();

        if (_current_scrollable.hovered != old_hovered) {
            FhApi.Audio.play_sound(SoundId.UI_ACTION, volume: get_move_volume());
            _audio_fade_helper.tick(1f);
        }

        if (scrollable_input_held) {
            _audio_fade_helper_restart = false;
        }

        if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_confirm)) {
            int hovered = _current_scrollable.hovered;

            if (_mode == UiMode.SAVE_LIST) {
                FhApi.Audio.play_sound(SoundId.UI_ACTION, ACTION_VOLUME);

                if (is_saving && hovered == 0) {
                    fade_out(() => FhApi.Saves.save(0));
                }
                else {
                    FhSaveDisplayData save = FhApi.Saves.display_data[hovered];
                    fade_out(() => execute(save.slot));
                }

                return true;
            }

            if (_mode == UiMode.SET_SWAP) {
                FhApi.Audio.play_sound(SoundId.PAGE_TURN);

                string hovered_set = _set_list[hovered];
                switch_set(hovered_set);

                return true;
            }
        }

        return false;
    }

    private bool handle_input_active_set() {
        if (_mode == UiMode.SAVE_LIST && FhApi.Gui.is_any_pressed(FhApi.Gui.keys_down) && _current_scrollable.max > 0) {
            FhApi.Audio.play_sound(SoundId.UI_ACTION, ACTION_VOLUME);

            _focus = UiFocus.LIST;
            _current_scrollable.hovered = _current_scrollable.current;

            return true;
        }

        if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_confirm)) {
            FhApi.Audio.play_sound(SoundId.PAGE_TURN);
            change_mode(UiMode.SET_SWAP);
            return true;
        }

        return false;
    }

    private void handle_input() {
        if (!should_handle_input) return;

        if (_focus == UiFocus.LIST       && handle_input_list())       return;
        if (_focus == UiFocus.ACTIVE_SET && handle_input_active_set()) return;

        if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_cancel)) {
            FhApi.Audio.play_sound(SoundId.UI_CANCEL);

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
        populate_ffx2_job_icons();

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
        populate_ffx2_job_icons();

        try_load_textures();

        change_mode(UiMode.SAVE_LIST);
    }

    private void populate_map_icons() {
        _map_icon_textures.Clear();

        foreach (FhSaveDisplayData save in FhApi.Saves.display_data) {
            string map = Encoding.UTF8.GetString(save.icon_map);
            if (_map_icon_textures.TryGetValue(map, out _)) continue;

            FhTexture map_icon = new(Path.Join(DIR_MAP_ICONS, $"{map}.png"), FhTextureType.PNG);
            _map_icon_textures.Add(map, map_icon);
        }
    }

    private void populate_ffx2_job_icons() {
        _ffx2_face_icon_textures.Clear();

        if (FhGlobal.game_id == FhGameId.FFX) {
            return;
        }

        foreach (FhSaveDisplayData save in FhApi.Saves.display_data) {
            FhSaveHeader2 header2 = MemoryMarshal.Read<FhSaveHeader2>(save.header);

            bool is_lm     = FhGlobal.game_id is FhGameId.FFX2LM;
            int  num_faces = is_lm ? 1 : 3;

            for (int i = 0; i < num_faces; i++) {
                byte job_id = is_lm ? header2.lm_job : header2.ply_jobs[i];

                if (job_id > 0x21) continue;

                string face = is_lm ? save.lm_job_filename : save.job_filenames[i];

                if (_ffx2_face_icon_textures.TryGetValue(face, out _)) continue;

                FhTexture face_icon = new(Path.Join(FFX2_DIR_FACE_DATA, face), FhTextureType.PHYRE);

                _ffx2_face_icon_textures.Add(
                    face,
                    face_icon
                );
            }
        }
    }

    /// <summary>Attempt to load all of the textures the renderer requires to display properly.</summary>
    /// <returns>Whether all textures have been successfully loaded.</returns>
    private bool try_load_textures() {
        Span<FhTexture> texture_list = textures;

        _loaded_all_textures = true;
        foreach (FhTexture texture in texture_list) {
            if (!FhApi.Resources.load_game_texture_2d(texture)) {
                _loaded_all_textures = false;
            }
        }

        return _loaded_all_textures;
    }

    /// <summary>Unload all of the textures the renderer requires to display properly.</summary>
    private void unload_textures() {
        Span<FhTexture> texture_list = textures;

        foreach (FhTexture texture in texture_list) {
            if (FhApi.Resources.unload_texture(texture))
                _loaded_all_textures = false;
            else
                _logger.Warning($"Failed to unload texture: {texture.path}");
        }
    }

    private bool mouse_hovered(Rect rect, bool require_movement = true) {
        return should_handle_input
            && FhApi.Gui.mouse_hovering(rect, require_movement);
    }

    private bool mouse_clicked(Rect rect, ImGuiMouseButton button = ImGuiMouseButton.Left, bool repeat = false) {
        return should_handle_input
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
            pos  = new(   0f,    0f),
            size = new(1920f, 1080f),
        }.scale_to_aspect(aspect_helper).as_uv();

        draw.AddImage(bg, screen_uv.p0, screen_uv.p1, tex_uv.p0, tex_uv.p1);
    }

    /// <summary>Render the help text for the save/load screen.</summary>
    private void ui_help() {
        if (!_texture_help  .try_use(out ImTextureRef help,   out _)
         || !_texture_meswin.try_use(out ImTextureRef meswin, out _)
        ) {
            return;
        }

        if (!FhApi.Saves.get_system_mode(out FhSaveSystemMode? system_mode)) {
            return;
        }

        UV bg_tuv = new Rect {
            pos  = new( 154f, 1762f),
            size = new(1740f,  105f),
        }.as_uv(_tex_help_size);

        // Saving this one as a rect to more easily calculate the text position later
        Rect bg_screen = new Rect {
            pos  = new( 151f, 96f),
            size = new(1600f, 55f),
        }.scale_to_aspect(aspect_helper);

        UV bg_suv = bg_screen.as_uv();

        UV title_tuv = new Rect {
            pos  = new(670f, 38f),
            size = new( 74f, 21f),
        }.as_uv(_tex_meswin_size);

        UV title_suv = new Rect {
            pos  = new(151f, 82f),
            size = new( 74f, 18f),
        }.scale_to_aspect(aspect_helper).as_uv();

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        draw.AddImage(help, bg_suv.p0, bg_suv.p1, bg_tuv.p0, bg_tuv.p1);

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
        text_pos.X += 24f * aspect_scale.X;
        text_pos.Y -=  1f * aspect_scale.Y;

        float font_size = 43f * font_scale;

        FhApi.Gui.draw_text(
            draw,
            text_pos,
            text,
            font_size,
            true,
            Alignment2D.LEFT
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

        Vector2 text_pos = new(1722f, 144f);

        float line_height = 38f;
        float font_size   = 43f * font_scale;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        Vector2 saves_text_size = FhApi.Gui.draw_text(
            draw,
            text_pos * aspect_scale + aspect_helper.pos,
            save_text,
            font_size,
            true,
            Alignment2D.RIGHT
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
                Alignment2D.RIGHT,
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
            Alignment2D.RIGHT
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

        Vector2 cursor_size = new Vector2(92f, 61f) * aspect_scale;

        float max_offset   = 16f * aspect_scale.X;
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
            .expand(cursor_size, Alignment2D.CENTER)
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
            pos  = new(660f, 28f),
            size = new(600f, 60f),
        }.scale_to_aspect(aspect_helper);

        UV bg_suv = bg_screen.as_uv();

        if (mouse_hovered(bg_screen) && _focus != UiFocus.ACTIVE_SET) {
            FhApi.Audio.play_sound(SoundId.UI_ACTION, ACTION_VOLUME);
            _focus = UiFocus.ACTIVE_SET;
        }

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();
        draw.AddImage(battle_kuang, bg_suv.p0, bg_suv.p1, bg_tuv.p0, bg_tuv.p1);

        float   font_size = 43f * font_scale;
        Vector2 text_pos  = bg_screen.center;

        //TODO: Add localization
        string text = "Change set";

        Vector2 text_size = FhApi.Gui.draw_text(
            draw,
            text_pos,
            text,
            font_size,
            true,
            Alignment2D.CENTER
        );

        if (_focus == UiFocus.ACTIVE_SET) {
            float text_left   = text_pos.X - text_size.X / 2f;
            float text_margin = 40f * aspect_scale.X;

            Vector2 cursor_target = bg_screen.center with {
                X = text_left - text_margin,
            };

            ui_cursor(cursor_target, false);
        }

        // Input handling
        if (mouse_clicked(bg_screen)) {
            FhApi.Audio.play_sound(SoundId.PAGE_TURN);
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
            pos  = new( 348f, 177f),
            size = new(1229f,  84f),
        };

        button.pos.Y += (button.size.Y + 10f) * (set_idx - start_idx);

        Rect button_no_shadow = button.expand(new Vector2(-6f, -5f), Alignment2D.TOP_LEFT);
        Rect button_scaled    = button.scale_to_aspect(aspect_helper);

        UV button_suv = button_scaled.as_uv();

        Vector2 name_pos = button_no_shadow.left;
        name_pos.X += 36f;
        name_pos = name_pos * aspect_scale + aspect_helper.pos;

        Vector2 save_count_pos = button_no_shadow.right;
        save_count_pos.X -= 48f;
        save_count_pos = save_count_pos * aspect_scale + aspect_helper.pos;

        // Drawing time
        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();
        ImGuiIOPtr    io   = ImGui.GetIO();

        float font_size = 43f * font_scale;

        if (mouse_hovered(button_scaled)) {
            if (_focus != UiFocus.LIST || _scrollable_sets.hovered != set_idx) {
                FhApi.Audio.play_sound(SoundId.UI_ACTION, ACTION_VOLUME);
            }

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
            Alignment2D.LEFT
        );

        bool has_autosave = FhApi.Saves.set_has_autosave(name);

        Vector2 save_count_size = FhApi.Gui.draw_text(
            draw,
            save_count_pos,
            has_autosave ? $" + {save_count - 1} saves" : $"{save_count} saves",
            font_size,
            true,
            Alignment2D.RIGHT
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
                Alignment2D.RIGHT,
                0xFF19D8FF
            );
        }

        if (hovering) {
            ui_cursor(button_no_shadow.scale_to_aspect(aspect_helper).left);
        }

        // Handle input
        if (mouse_clicked(button_scaled)) {
            io.WantCaptureMouse = true;
            FhApi.Audio.play_sound(SoundId.PAGE_TURN);
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

        float font_size = 43f * font_scale;

        ImGui.PushFont(null, font_size);
        Vector2 text_size = ImGui.CalcTextSize(message);
        ImGui.PopFont();

        Vector2 text_margin = new Vector2(36f, 48f) * aspect_scale;
        Vector2 window_size = text_margin * 2 + text_size;

        Rect window = new Rect {
            pos  = FhApi.Gui.display_size / 2f,
            size = Vector2.Zero,
        }.expand(window_size, Alignment2D.CENTER);

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
            Alignment2D.CENTER
        );
    }

    private void ui_save_details_x(FhSaveDisplayData save, Rect save_bounds, Rect header_bounds) {
        if (!_texture_faces.try_use(out ImTextureRef faces, out _)) {
            return;
        }

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        /* ===== Party Icons ===== */
        Span<UV> face_tuvs = stackalloc UV[8];

        ReadOnlySpan<int> face_pos_xs = [ 1, 201, 400, 600 ];

        for (int i = 0; i < 8; i++) {
            float x1 = face_pos_xs[i % 4];
            float x2 = x1 + 198f;

            float y1 = i > 3 ? 311f : 511f;
            float y2 = y1 - 198f;

            face_tuvs[i] = new UV(
                new(x1, y1),
                new(x2, y2)
            ).map_to(_tex_faces_size);
        }

        Vector2 face_size = new(99f);
        float   face_gap  = 6f;

        Rect face = new() {
            pos = save_bounds.bottom_left with {
                Y = save_bounds.bottom_left.Y - face_size.Y,
            },
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

        Vector2 map_icon_size = new(255f, 144f);

        // if (tex_map.try_use(out ImTextureRef map_icon, out _)) {
        //     UV tuv = new(new(0f, 1f), new(1f, 0f));
        //
        //     UV suv = new Rect {
        //         pos  = new() {
        //             X = save_bounds.right.X - map_icon_size.X,
        //             Y = save_bounds.top.Y,
        //         },
        //         size = map_icon_size,
        //     }.scale_to_aspect(aspect_helper).as_uv();
        //
        //     draw.AddImage(map_icon, suv.p0, suv.p1, tuv.p0, tuv.p1);
        // }

        /* ===== Header Details ===== */
        float text_margin_left    = 12f;
        float text_margin_between = 48f;

        Vector2 header_text_pos_left = header_bounds.left;
        header_text_pos_left.X += text_margin_left;

        float font_size = 43f * font_scale;

        //TODO: Add localization
        string slot_text   = save.slot == 0 ? "Autosave" : save.slot_str;
        string create_time = save.create_time.ToString(@"yyyy\/M\/d H\:mm\:ss");

        Vector2 text_size = FhApi.Gui.draw_text(
            draw,
            header_text_pos_left * aspect_scale + aspect_helper.pos,
            slot_text,
            font_size,
            true,
            Alignment2D.LEFT,
            save.slot == 0 ? 0xFF19D8FF : 0xFFFFFFFF // Autosave text is yellow
        );

        // If location string is "None", don't render
        if (header.id_location != 0) {
            header_text_pos_left = header_text_pos_left * aspect_scale + aspect_helper.pos;
            header_text_pos_left.X += text_size.X + text_margin_between * aspect_scale.X;

            // For some reason, if the slot text is "Autosave", the margin is doubled
            if (save.slot == 0) {
                header_text_pos_left.X += text_margin_between * aspect_scale.X;
            }

            FhApi.Gui.draw_text(
                draw,
                header_text_pos_left,
                save.location,
                font_size,
                true,
                Alignment2D.LEFT
            );
        }

        Vector2 header_text_pos_right = header_bounds.right;
        header_text_pos_right.X -= map_icon_size.X + text_margin_between;

        FhApi.Gui.draw_text(
            draw,
            header_text_pos_right * aspect_scale + aspect_helper.pos,
            create_time,
            font_size,
            true,
            Alignment2D.RIGHT
        );

        /* ===== Info Details ===== */
        float margin_from_header    =  6f;
        float margin_from_last_face = 38f;
        float line_height           = 48f;

        Vector2 info_text_pos = new() {
            X = save_bounds.left.X + face_size.X * 3 + face_gap * 2 + margin_from_last_face,
            Y = header_bounds.bottom.Y + margin_from_header + line_height / 2f,
        };

        FhApi.Gui.draw_text(
            draw,
            info_text_pos * aspect_scale + aspect_helper.pos,
            save.player_name,
            font_size,
            true,
            Alignment2D.LEFT
        );

        info_text_pos.Y += line_height;

        FhApi.Gui.draw_text(
            draw,
            info_text_pos * aspect_scale + aspect_helper.pos,
            save.play_time,
            font_size,
            true,
            Alignment2D.LEFT
        );
    }

    private void ui_save_details_x2(FhSaveDisplayData save, Rect save_bounds, Rect header_bounds) {
        if (save.slot == 0 && is_saving) return;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        /* ===== Party Icons ===== */
        UV face_tuv = new Rect {
            pos  = new(  0f,   0f),
            size = new(256f, 239f),
        }.as_uv(_tex_ffx2_face_size);

        float   face_height = save_bounds.size.Y - header_bounds.size.Y;
        Vector2 face_size   = new(1.0625f * face_height, face_height);
        float   face_gap    = 3f;

        Rect face_bounds = new() {
            pos = save_bounds.bottom_left with {
                Y = save_bounds.bottom_left.Y - face_size.Y,
            },
            size = face_size,
        };

        UV face_suv = face_bounds.scale_to_aspect(aspect_helper).as_uv();

        Vector2 face_screen_offset = new Vector2(face_size.X + face_gap, 0f) * aspect_scale;

        FhSaveHeader2 header2 = MemoryMarshal.Read<FhSaveHeader2>(save.header);

        for (byte i = 0; i < 3; i++) {
            byte ply_id = header2.ply[i];
            byte job_id = header2.ply_jobs[i];

            if (job_id > 0x21) {
                face_suv = face_suv.move(face_screen_offset);
                continue;
            }

            string    filename = save.job_filenames[i];
            FhTexture portrait = _ffx2_face_icon_textures.GetValueOrDefault(filename, _texture_ffx2_face_icon_default);

            uint alpha = job_id == 0x0 ? 0xA0C0C0C0 : 0xFFFFFFFF;

            if (portrait.try_use(out ImTextureRef face, out _)) {
                draw.AddImage(
                    face,
                    face_suv.p0,
                    face_suv.p1,
                    face_tuv.p0,
                    face_tuv.p1,
                    alpha
                );
            }

            face_suv = face_suv.move(face_screen_offset);
        }

        /* ===== Location Icon ===== */
        string    map     = Encoding.UTF8.GetString(save.icon_map);
        FhTexture tex_map = _map_icon_textures.GetValueOrDefault(map, _texture_map_icon_default);

        Vector2 map_icon_size = new(255f, save_bounds.size.Y);

        // if (tex_map.try_use(out ImTextureRef map_icon, out _)) {
        //     UV tuv = new(new(0f, 1f), new(1f, 0f));
        //
        //     UV suv = new Rect {
        //         pos  = new() {
        //             X = save_bounds.right.X - map_icon_size.X,
        //             Y = save_bounds.top.Y,
        //         },
        //         size = map_icon_size,
        //     }.scale_to_aspect(aspect_helper).as_uv();
        //
        //     draw.AddImage(map_icon, suv.p0, suv.p1, tuv.p0, tuv.p1);
        // }

        /* ===== Header Details ===== */
        float text_margin_left    = 12f;
        float text_margin_between = 48f;

        Vector2 header_text_pos_left = header_bounds.left;
        header_text_pos_left.X += text_margin_left;

        float font_size = 43f * font_scale;

        //TODO: Add localization
        string slot_text   = save.slot == 0 ? "Autosave" : save.slot_str;
        string create_time = save.create_time.ToString(@"yyyy\/M\/d H\:mm\:ss");

        Vector2 text_size = FhApi.Gui.draw_text(
            draw,
            header_text_pos_left * aspect_scale + aspect_helper.pos,
            slot_text,
            font_size,
            true,
            Alignment2D.LEFT,
            save.slot == 0 ? 0xFF19D8FF : 0xFFFFFFFF // Autosave text is yellow
        );

        // If location string is "NONAME", don't render
        if (header2.id_location != 0) {
            header_text_pos_left = header_text_pos_left * aspect_scale + aspect_helper.pos;
            header_text_pos_left.X += text_size.X + text_margin_between * aspect_scale.X;

            // If the slot text is "Autosave", double the margin
            if (save.slot == 0) {
                header_text_pos_left.X += text_margin_between * aspect_scale.X;
            }

            FhApi.Gui.draw_text(
                draw,
                header_text_pos_left,
                save.location,
                font_size,
                true,
                Alignment2D.LEFT
            );
        }

        Vector2 header_text_pos_right = header_bounds.right;
        header_text_pos_right.X -= map_icon_size.X + text_margin_between;

        FhApi.Gui.draw_text(
            draw,
            header_text_pos_right * aspect_scale + aspect_helper.pos,
            create_time,
            font_size,
            true,
            Alignment2D.RIGHT
        );

        /* ===== Info Details ===== */
        float info_margin_left  = face_size.X * 3 + face_gap * 2f;
        float info_margin_right = map_icon_size.X;

        Rect info_bounds = new() {
            pos = new() {
                X = save_bounds.left.X + info_margin_left,
                Y = header_bounds.bottom.Y,
            },
            size = new() {
                X = save_bounds.size.X - info_margin_left - info_margin_right,
                Y = save_bounds.size.Y - header_bounds.size.Y,
            },
        };

        float info_text_left_x  = info_bounds.left.X  + text_margin_between;
        float info_text_right_x = info_bounds.right.X - text_margin_between;

        float info_text_top_y    = float.Lerp(info_bounds.top.Y, info_bounds.bottom.Y, 0.25f);
        float info_text_bottom_y = float.Lerp(info_bounds.top.Y, info_bounds.bottom.Y, 0.7f);

        Vector2 info_text_pos_name = new() {
            X = info_text_left_x,
            Y = info_text_top_y,
        };

        Vector2 info_text_pos_chapter = new() {
            X = info_text_left_x,
            Y = info_text_bottom_y,
        };

        Vector2 info_text_pos_completion = new() {
            X = info_text_right_x,
            Y = info_text_top_y,
        };

        Vector2 info_text_pos_playtime = new() {
            X = info_text_right_x,
            Y = info_text_bottom_y,
        };

        // When running in CJK, we hardcode the "STORY COMPLETED" string.
        string completion = FhGlobal.lang_id.is_cjk
            ? $"STORY COMPLETED: {header2.completion}%"
            : Encoding.UTF8.GetString(save.completion);

        FhApi.Gui.draw_text(
            draw,
            info_text_pos_name * aspect_scale + aspect_helper.pos,
            save.player_name,
            font_size,
            true,
            Alignment2D.LEFT
        );

        FhApi.Gui.draw_text(
            draw,
            info_text_pos_chapter * aspect_scale + aspect_helper.pos,
            save.chapter,
            font_size,
            true,
            Alignment2D.LEFT
        );

        FhApi.Gui.draw_text(
            draw,
            info_text_pos_completion * aspect_scale + aspect_helper.pos,
            completion,
            font_size,
            true,
            Alignment2D.RIGHT
        );

        FhApi.Gui.draw_text(
            draw,
            info_text_pos_playtime * aspect_scale + aspect_helper.pos,
            save.play_time,
            font_size,
            true,
            Alignment2D.RIGHT
        );
    }

    private void ui_save_details_lm(FhSaveDisplayData save, Rect save_bounds, Rect header_bounds) {
        if (save.slot == 0 && is_saving) return;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        /* ===== Party Icons ===== */
        UV face_tuv = new Rect {
            pos  = new(  0f,   0f),
            size = new(256f, 239f),
        }.as_uv(_tex_ffx2_face_size);

        float   face_height = save_bounds.size.Y - header_bounds.size.Y;
        Vector2 face_size   = new(1.0625f * face_height, face_height);
        float   face_gap    = 25f;

        Rect face = new() {
            pos = save_bounds.bottom_left with {
                X = save_bounds.bottom_left.X + face_gap * 3,
                Y = save_bounds.bottom_left.Y - face_size.Y,
            },
            size = face_size,
        };

        UV face_suv = face.scale_to_aspect(aspect_helper).as_uv();

        Vector2 face_screen_offset = new Vector2(-face_gap, 0f) * aspect_scale;

        FhSaveHeader2 header2 = MemoryMarshal.Read<FhSaveHeader2>(save.header);

        byte ply_id = header2.lm_ply;
        byte job_id = header2.lm_job;

        if (job_id <= 0x21) {
            string    filename = save.lm_job_filename;
            FhTexture portrait = _ffx2_face_icon_textures.GetValueOrDefault(filename, _texture_ffx2_face_icon_default);

            if (portrait.try_use(out ImTextureRef faces, out _)) {
                for (int i = 3; i >= 0; i--) {
                    uint alpha = ImGui.GetColorU32(new Vector4(1f, 1f, 1f, 1f - (i * 0.25f)));

                    draw.AddImage(
                        faces,
                        face_suv.p0,
                        face_suv.p1,
                        face_tuv.p0,
                        face_tuv.p1,
                        alpha
                    );

                    face_suv = face_suv.move(face_screen_offset);
                }
            }
        }

        /* ===== Location Icon ===== */
        string    map     = Encoding.UTF8.GetString(save.icon_map);
        FhTexture tex_map = _map_icon_textures.GetValueOrDefault(map, _texture_map_icon_default);

        Vector2 map_icon_size = new(255f, save_bounds.size.Y);

        // if (tex_map.try_use(out ImTextureRef map_icon, out _)) {
        //     UV tuv = new(new(0f, 1f), new(1f, 0f));
        //
        //     UV suv = new Rect {
        //         pos  = new() {
        //             X = save_bounds.right.X - map_icon_size.X,
        //             Y = save_bounds.top.Y,
        //         },
        //         size = map_icon_size,
        //     }.scale_to_aspect(aspect_helper).as_uv();
        //
        //     draw.AddImage(map_icon, suv.p0, suv.p1, tuv.p0, tuv.p1);
        // }

        /* ===== Header Details ===== */
        float text_margin_left    = 12f;
        float text_margin_between = 48f;

        Vector2 header_text_pos_left = header_bounds.left;
        header_text_pos_left.X += text_margin_left;

        float font_size = 43f * font_scale;

        //TODO: Add localization
        string slot_text   = save.slot == 0 ? "Autosave" : save.slot_str;
        string create_time = save.create_time.ToString(@"yyyy\/M\/d H\:mm\:ss");

        Vector2 text_size = FhApi.Gui.draw_text(
            draw,
            header_text_pos_left * aspect_scale + aspect_helper.pos,
            slot_text,
            font_size,
            true,
            Alignment2D.LEFT,
            save.slot == 0 ? 0xFF19D8FF : 0xFFFFFFFF // Autosave text is yellow
        );

        // If location string is "NONAME", don't render
        if (header2.lm_id_location != 0) {
            header_text_pos_left = header_text_pos_left * aspect_scale + aspect_helper.pos;
            header_text_pos_left.X += text_size.X + text_margin_between * aspect_scale.X;

            // If the slot text is "Autosave", double the margin
            if (save.slot == 0) {
                header_text_pos_left.X += text_margin_between * aspect_scale.X;
            }

            FhApi.Gui.draw_text(
                draw,
                header_text_pos_left,
                save.location,
                font_size,
                true,
                Alignment2D.LEFT
            );
        }

        Vector2 header_text_pos_right = header_bounds.right;
        header_text_pos_right.X -= map_icon_size.X + text_margin_between;

        FhApi.Gui.draw_text(
            draw,
            header_text_pos_right * aspect_scale + aspect_helper.pos,
            create_time,
            font_size,
            true,
            Alignment2D.RIGHT
        );

        /* ===== Info Details ===== */
        float info_margin_left  = face_size.X + face_gap * 3f;
        float info_margin_right = map_icon_size.X;

        Rect info_bounds = new() {
            pos = new() {
                X = save_bounds.left.X + info_margin_left,
                Y = header_bounds.bottom.Y,
            },
            size = new() {
                X = save_bounds.size.X - info_margin_left - info_margin_right,
                Y = save_bounds.size.Y - header_bounds.size.Y,
            },
        };

        float top_line_y    = float.Lerp(info_bounds.top.Y, info_bounds.bottom.Y, 0.25f);
        float bottom_line_y = float.Lerp(info_bounds.top.Y, info_bounds.bottom.Y, 0.7f);

        Vector2 info_text_pos_name = new() {
            X = info_bounds.left.X + text_margin_between,
            Y = top_line_y,
        };

        Vector2 info_text_pos_job = new() {
            X = info_bounds.left.X + text_margin_between,
            Y = bottom_line_y,
        };

        Vector2 info_text_pos_playtime = new() {
            X = info_bounds.right.X - text_margin_between,
            Y = bottom_line_y,
        };

        FhApi.Gui.draw_text(
            draw,
            info_text_pos_name * aspect_scale + aspect_helper.pos,
            save.player_name,
            font_size,
            true,
            Alignment2D.LEFT
        );

        info_text_pos_job = info_text_pos_job * aspect_scale + aspect_helper.pos;

        Vector2 job_text_size = FhApi.Gui.draw_text(
            draw,
            info_text_pos_job,
            save.lm_job,
            font_size,
            true,
            Alignment2D.LEFT
        );

        info_text_pos_job.X += job_text_size.X + text_margin_between;

        FhApi.Gui.draw_text(
            draw,
            info_text_pos_job,
            save.lm_level,
            font_size,
            true,
            Alignment2D.LEFT
        );

        FhApi.Gui.draw_text(
            draw,
            info_text_pos_playtime * aspect_scale + aspect_helper.pos,
            save.play_time,
            font_size,
            true,
            Alignment2D.RIGHT
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

        if (!_texture_meswin.try_use(out ImTextureRef meswin, out _)) {
            return;
        }

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        Rect save_outer_bounds = new Rect {
            pos  = new( 160f, 176f), // Topmost position
            size = new(1561f, 152f),
        };

        float save_gap = 13f;

        save_outer_bounds.pos.Y += (save_outer_bounds.size.Y + save_gap) * (index - _scrollable_saves.current);

        // We set the hovered state early to potentially use it later.
        if (mouse_hovered(save_outer_bounds.scale_to_aspect(aspect_helper))) {
            if (_focus != UiFocus.LIST || _scrollable_saves.hovered != index) {
                FhApi.Audio.play_sound(SoundId.UI_ACTION, ACTION_VOLUME);
            }

            _focus = UiFocus.LIST;
            _scrollable_saves.hovered = index;
        }

        /* ===== Autosave Background ===== */
        if (save.slot == 0) {
            UV save_suv = save_outer_bounds.scale_to_aspect(aspect_helper).as_uv();

            draw.AddRectFilled(save_suv.p0, save_suv.p1, 0x30F0F0F0);
        }

        /* ===== Border ===== */
        NineSliceHelper screen_border_helper = NineSliceHelper.create(
            Vector2.One,
            save_outer_bounds.scale_to_aspect(aspect_helper),
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

        Rect save_bounds = save_outer_bounds.expand(-save_border_size * 2f, Alignment2D.CENTER);

        /* ===== Header Gradient ===== */
        float header_height = 43f;

        uint header_grad_l = 0xFF000000; // black
        uint header_grad_r = 0x00800000; // transparent blue

        Rect header_bounds = new() {
            pos  = save_bounds.pos,
            size = save_bounds.size with { Y = header_height },
        };

        UV header_uv = header_bounds
            .scale_to_aspect(aspect_helper)
            .as_uv();

        draw.AddRectFilledMultiColor(
            header_uv.p0,
            header_uv.p1,
            header_grad_l,
            header_grad_r,
            header_grad_r,
            header_grad_l
        );

        /* ===== Separator ===== */
        Rect separator_bounds = new Rect {
            pos  = save_bounds.pos  with { Y = header_bounds.bottom.Y - 1f },
            size = save_bounds.size with { Y = 3f },
        }.scale_to_aspect(aspect_helper);

        GradientStep[] separator_steps = [
            new(0.0f, 0x00FBF1F3),
            new(0.2f, 0x40FBF1F3),
            new(0.6f, 0x40FBF1F3),
            new(0.8f, 0x00FBF1F3),
        ];

        FhApi.Gui.draw_rectangle_gradient(
            draw,
            separator_bounds,
            GradientDirection.RIGHT,
            separator_steps
        );

        /* ===== Details ===== */

        switch (FhGlobal.game_id) {
            case FhGameId.FFX:    ui_save_details_x (save, save_bounds, header_bounds); break;
            case FhGameId.FFX2:   ui_save_details_x2(save, save_bounds, header_bounds); break;
            case FhGameId.FFX2LM: ui_save_details_lm(save, save_bounds, header_bounds); break;
        }

        /* ===== Cursor ===== */
        if (_focus == UiFocus.LIST && _scrollable_saves.hovered == index) {
            Rect save_rect_scaled = save_outer_bounds.scale_to_aspect(aspect_helper);

            Vector2 cursor_target = save_rect_scaled.left with {
                Y = save_rect_scaled.left.Y + 4f,
            };

            ui_cursor(cursor_target);
        }

        // Handle input
        if (mouse_clicked(save_outer_bounds.scale_to_aspect(aspect_helper))) {
            FhApi.Audio.play_sound(SoundId.UI_ACTION, ACTION_VOLUME);
            fade_out(() => execute(save.slot));
        }
    }

    private void ui_newdata() {
        if (!_texture_meswin .try_use(out ImTextureRef meswin,  out _)
         || !_texture_freetex.try_use(out ImTextureRef freetex, out _)
        ) {
            return;
        }

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        Rect save_rect = new Rect {
            pos  = new( 160f, 176f),
            size = new(1561f, 152f),
        };

        // The border effectively has two sizes: 4f and 9f.
        // We only want to respect the outermost border, so we use 4f.
        Vector2 save_border_size = new(4f);

        if (mouse_hovered(save_rect.scale_to_aspect(aspect_helper))) {
            if (_focus != UiFocus.LIST || _scrollable_saves.hovered != 0) {
                FhApi.Audio.play_sound(SoundId.UI_ACTION, ACTION_VOLUME);
            }

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
        float header_height = 43f;

        uint grad_l = 0xFF000000; // black
        uint grad_r = 0x00800000; // transparent blue

        UV header_uv = new Rect {
            pos  = save_rect.pos + save_border_size,
            size = save_rect.size with { Y = header_height },
        }.scale_to_aspect(aspect_helper).as_uv();

        draw.AddRectFilledMultiColor(header_uv.p0, header_uv.p1, grad_l, grad_r, grad_r, grad_l);

        // Separator
        Rect separator_bounds = new Rect {
            pos = new(
                save_rect.pos.X + save_border_size.X,
                save_rect.pos.Y + (header_height - 1f) + save_border_size.Y
            ),
            size = save_rect.size with { Y = 3f },
        }.scale_to_aspect(aspect_helper);

        GradientStep[] separator_steps = [
            new(0.0f, 0x00FBF1F3),
            new(0.2f, 0x40FBF1F3),
            new(0.6f, 0x40FBF1F3),
            new(0.8f, 0x00FBF1F3),
        ];

        FhApi.Gui.draw_rectangle_gradient(
            draw,
            separator_bounds,
            GradientDirection.RIGHT,
            separator_steps
        );

        /* ===== New Save Data Text ===== */
        //TODO: Add localization
        string text = "New Save Data";

        Vector2 text_pos = save_rect.top_left + new Vector2(
            14f,
            header_height + (save_rect.size.Y - header_height) / 2f
        );

        float font_size = 43f * font_scale;

        FhApi.Gui.draw_text(
            draw,
            text_pos * aspect_scale + aspect_helper.pos,
            text,
            font_size,
            true,
            Alignment2D.LEFT
        );

        // TODO: Uncomment once map icons can be loaded/rendered.
        /* ===== "+" Icon ===== */
        /*grad_l = 0xFF191919; // grey
        grad_r = 0xFF252525; // black

        UV suv = new Rect {
            pos  = save_rect.pos + new Vector2(1302f, 4f),
            size = new(255f, 144f),
        }.scale_to_aspect(aspect_helper).as_uv();

        draw.AddRectFilledMultiColor(
            suv.p0,
            suv.p1,
            grad_r,
            grad_r,
            grad_l,
            grad_l
        );

        UV plus_tuv = new Rect {
            pos  = new(581f, 724f),
            size = new( 36f,  36f),
        }.as_uv(_tex_freetex_size);

        UV plus_suv = new Rect {
            pos  = save_rect.pos + new Vector2(1405f, 56f),
            size = new(45f, 47f),
        }.scale_to_aspect(aspect_helper).as_uv();

        draw.AddImage(
            freetex,
            plus_suv.p0,
            plus_suv.p1,
            plus_tuv.p0,
            plus_tuv.p1
        );*/

        /* ===== Cursor ===== */
        if (_focus == UiFocus.LIST && _scrollable_saves.hovered == 0) {
            Rect save_rect_scaled = save_rect.scale_to_aspect(aspect_helper);

            Vector2 cursor_target = save_rect_scaled.left with {
                Y = save_rect_scaled.left.Y + 4f,
            };

            ui_cursor(cursor_target);
        }

        // Handle input
        if (mouse_clicked(save_rect.scale_to_aspect(aspect_helper))) {
            FhApi.Audio.play_sound(SoundId.UI_ACTION, ACTION_VOLUME);
            fade_out(() => FhApi.Saves.save(0));
        }
    }

    private void ui_scrollbar() {
        if (_current_scrollable.max <= _current_scrollable.visible) {
            return;
        }

        Rect track = new() {
            pos  = new(1753f, 209f),
            size = new(  17f, 756f),
        };

        uint track_color = 0xFF000000;

        Vector2 thumb_margin = new(2f);

        float scrollable_height  = track.size.Y - thumb_margin.Y * 2;
        float visible_percentage = _current_scrollable.visible / (float)_current_scrollable.max;

        float thumb_height_max = 0.8f * scrollable_height;
        float thumb_height_min = float.Min(20f, thumb_height_max);

        float thumb_height = float.Lerp(
            thumb_height_min,
            thumb_height_max,
            visible_percentage
        );

        Vector2 thumb_size = new(13f, thumb_height);

        uint thumb_color_top    = 0xFFCCCCCC;
        uint thumb_color_bottom = 0xFF808080;

        Vector2 triangle_size = new(38f, 19f);
        float   triangle_gap  = 15f;

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

        uint triangle_color_top    = 0xFFC0C0C0;
        uint triangle_color_bottom = 0xFF808080;

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

        Rect scaled_track = track.scale_to_aspect(aspect_helper);
        Rect scaled_thumb = thumb.scale_to_aspect(aspect_helper);

        // Make sure that the thumb is still in the middle of the track after scaling
        scaled_thumb.size.X = float.Round(scaled_track.size.X) - float.Round(thumb_margin.X * aspect_scale.X) * 2f;
        scaled_thumb.pos.X  = float.Round(scaled_track.pos.X) + (float.Round(scaled_track.size.X) - scaled_thumb.size.X) / 2f;

        triangle_top    = triangle_top   .scale_to_aspect(aspect_helper);
        triangle_bottom = triangle_bottom.scale_to_aspect(aspect_helper);

        scrollable_track_height *= aspect_scale.Y;

        draw.AddRectFilled(
            scaled_track.top_left,
            scaled_track.bottom_right,
            track_color
        );

        draw.AddRectFilledMultiColor(
            scaled_thumb.top_left,
            scaled_thumb.bottom_right,
            thumb_color_top,
            thumb_color_top,
            thumb_color_bottom,
            thumb_color_bottom
        );

        Rect triangle_top_outline_bounds = triangle_top.expand(
            Vector2.Round(new Vector2(2f, 1f) * new Vector2(1f) * aspect_scale) * 2f,
            Alignment2D.CENTER
        );

        Rect triangle_bot_outline_bounds = triangle_bottom.expand(
            Vector2.Round(new Vector2(2f, 1f) * new Vector2(1f) * aspect_scale) * 2f,
            Alignment2D.CENTER
        );

        FhApi.Gui.draw_triangle_gradient(
            draw,
            triangle_top_outline_bounds,
            GradientDirection.UP,
            0xFF000000,
            0xFF000000,
            0xFF000000
        );

        FhApi.Gui.draw_triangle_gradient(
            draw,
            triangle_bot_outline_bounds,
            GradientDirection.DOWN,
            0xFF000000,
            0xFF000000,
            0xFF000000
        );

        FhApi.Gui.draw_triangle_gradient(
            draw,
            triangle_top,
            GradientDirection.UP,
            triangle_color_bottom,
            triangle_color_bottom,
            triangle_color_top
        );

        FhApi.Gui.draw_triangle_gradient(
            draw,
            triangle_bottom,
            GradientDirection.DOWN,
            triangle_color_bottom,
            triangle_color_bottom,
            triangle_color_top
        );

        // Handle input

        // Allow grabbing the thumb on the entire track width
        Rect expanded_thumb = scaled_thumb.expand(
            new(scaled_track.size.X - scaled_thumb.size.X),
            Alignment2D.CENTER
        );

        float expanded_track_height = scrollable_track_height + (scaled_track.size.X - scaled_thumb.size.X);

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
            FhApi.Audio.play_sound(SoundId.UI_ACTION, volume: get_move_volume());
            _audio_fade_helper.tick(1f);

            _current_scrollable.move_hover(-1);
        }

        if (mouse_clicked(triangle_bottom, repeat: true)) {
            FhApi.Audio.play_sound(SoundId.UI_ACTION, volume: get_move_volume());
            _audio_fade_helper.tick(1f);

            _current_scrollable.move_hover(1);
        }

        if ((mouse_hovered(triangle_top) || mouse_hovered(triangle_bottom))
         && ImGui.IsMouseDown(ImGuiMouseButton.Left)
           ) {
            _audio_fade_helper_restart = false;
        }
    }

    private void ui_fade() {
        if (_fade.is_done) return;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();
        draw.AddRectFilled(Vector2.Zero, FhApi.Gui.display_size, _fade.get_color());

        _fade.tick(ImGui.GetIO().DeltaTime);
    }
}
