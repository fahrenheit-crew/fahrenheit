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

/// <summary>The default save UI renderer for Final Fantasy X-2/Last Mission.</summary>
[FhLoad(FhGameId.FFX2 | FhGameId.FFX2LM)]
public sealed class FhSaveUiX2 : FhSaveUi {
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

    private const string MAP_ICONS_DIR      = "/FFX-2_Data/GameData/PS3Data/savedataicons/";
    private const string MENU_FACE_DATA_DIR = "/FFX-2_Data/GameData/PS3Data/menu/face_data/D3D11/";
    private const string MENU_D3D11_DIR     = "/FFX-2_Data/GameData/PS3Data/menu/D3D11/";
    private const string MENU_MAHOJIN_DIR   = "/FFX-2_Data/GameData/PS3Data/menu/menu_mahojin/tex/D3D11/";
    private const string MENU_PLATE_DIR     = "/FFX-2_Data/GameData/PS3Data/menu/menu_plate/tex/D3D11/";

    private const uint COLOR_BLACK = 0xFF000000;
    private const uint COLOR_TRANS = 0x00000000;

    private UiMode  _mode;
    private UiFocus _focus;

    private          float      _fade_length = FhUtil.select(0.5f, 0.35f, 0.01f);
    private readonly FadeHelper _fade;

    private readonly List<string> _set_list = [ ];

    private bool _loaded_all_textures;

    private readonly Dictionary<string, FhTexture> _face_icon_textures = [ ];
    private readonly Dictionary<string, FhTexture> _map_icon_textures  = [ ];

    private readonly FhTexture _texture_face_icon_default = new(Path.Join(MENU_FACE_DATA_DIR, "mface_000.dds.phyre"), FhTextureType.PHYRE);
    private readonly FhTexture _texture_map_icon_default  = new(Path.Join(MAP_ICONS_DIR,      "logo_j.png"),          FhTextureType.PNG);

    private readonly FhTexture _texture_menuback = new(Path.Join(MENU_D3D11_DIR,   "menuback.dds.phyre"),             FhTextureType.PHYRE);
    private readonly FhTexture _texture_mahojin  = new(Path.Join(MENU_MAHOJIN_DIR, "14336_19_0_0_512_512.dds.phyre"), FhTextureType.PHYRE);
    private readonly FhTexture _texture_plate    = new(Path.Join(MENU_PLATE_DIR,   "12288_19_0_0_256_256.dds.phyre"), FhTextureType.PHYRE);
    private readonly FhTexture _texture_freetex  = new(Path.Join(MENU_D3D11_DIR,   "freetex.dds.phyre"),              FhTextureType.PHYRE);
    private readonly FhTexture _texture_x2_bg    = new(Path.Join(MENU_D3D11_DIR,   "x2_bg.dds.phyre"),                FhTextureType.PHYRE);

    private readonly Vector2 _tex_face_size     = new(256f, 256f);
    private readonly Vector2 _tex_map_icon_size = new(320f, 176f);

    private readonly Vector2 _tex_menuback_size = new( 512f,  512f);
    private readonly Vector2 _tex_mahojin_size  = new(2048f, 2048f);
    private readonly Vector2 _tex_plate_size    = new( 512f,  512f);
    private readonly Vector2 _tex_freetex_size  = new(1024f,  768f);
    private readonly Vector2 _tex_x2_bg_size    = new(2048f, 2048f);

    private bool is_saving => FhApi.Saves.get_system_mode(out FhSaveSystemMode? system_mode) && system_mode is FhSaveSystemMode.SAVE;

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
    private bool is_fade_blocking_input => _fade.is_done || (_fade.color_to == COLOR_TRANS && _fade.progress > 0.1f);
    private bool should_handle_input => !_scrollbar_dragging && is_fade_blocking_input;

    public FhSaveUiX2() {
        _current_scrollable = _scrollable_saves;

        _fade = new FadeHelper(0, 0, _fade_length);
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
            _fade_length * (1f - _fade.progress),
            action
        );
    }

    private void handle_input_list() {
        if (_mode == UiMode.SAVE_LIST) {
            if (_scrollable_saves.max == 0) {
                _focus = UiFocus.ACTIVE_SET;
                return;
            }

            if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_up)
             && _current_scrollable.hovered == 0
            ) {
                _focus = UiFocus.ACTIVE_SET;
                return;
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

                return;
            }

            if (_mode == UiMode.SET_SWAP) {
                string hovered_set = _set_list[hovered];
                switch_set(hovered_set);

                return;
            }
        }
    }

    private void handle_input_active_set() {
        if (_mode == UiMode.SAVE_LIST && FhApi.Gui.is_any_pressed(FhApi.Gui.keys_down) && _current_scrollable.max > 0) {
            _focus = UiFocus.LIST;
            _current_scrollable.hovered = _current_scrollable.current;

            return;
        }

        if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_confirm)) {
            change_mode(UiMode.SET_SWAP);
        }
    }

    private void handle_input() {
        if (!should_handle_input) return;

        switch (_focus) {
            case UiFocus.LIST:       handle_input_list();       break;
            case UiFocus.ACTIVE_SET: handle_input_active_set(); break;

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
        _mode  = UiMode .SAVE_LIST;
        _focus = UiFocus.LIST;

        populate_face_icons();
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
    }

    private void change_mode(UiMode new_mode) {
        _scrollable_saves.reset();
        _scrollable_sets .reset();
        _focus = UiFocus.LIST;
        _mode  = new_mode;
    }

    private void execute(int slot) {
        if (!FhApi.Saves.get_system_mode(out FhSaveSystemMode? system_mode)) return;

        switch (system_mode) {
            case FhSaveSystemMode.SAVE: FhApi.Saves.save(slot); break;
            case FhSaveSystemMode.LOAD: FhApi.Saves.load(slot); break;

            default: throw new InvalidOperationException();
        }
    }

    private void switch_set(string set_name) {
        FhApi.Saves.switch_active_set(set_name);

        populate_face_icons();
        populate_map_icons();
        try_load_textures();

        change_mode(UiMode.SAVE_LIST);
    }

    /// <summary>Draws the highlights/shadows for the save slot texture.</summary>
    private void draw_highlight_shadow(ImDrawListPtr draw, Vector2 pos_topleft, Vector2 size, float thickness, uint highlight, uint shadow) {
        // Top Highlight
        draw.AddRectFilled(
            pos_topleft,
            new Vector2(pos_topleft.X + size.X, pos_topleft.Y + thickness),
            highlight
        );

        // Left Highlight
        draw.AddRectFilled(
            new Vector2(pos_topleft.X, pos_topleft.Y + thickness),
            new Vector2(pos_topleft.X + thickness, pos_topleft.Y + size.Y),
            highlight
        );

        // Bottom Shadow - intentional overlap with left highlight to mimic the vanilla game!
        draw.AddRectFilled(
            new Vector2(pos_topleft.X, pos_topleft.Y + size.Y - thickness),
            pos_topleft + size,
            shadow
        );

        // Right Shadow
        draw.AddRectFilled(
            new Vector2(pos_topleft.X + size.X - thickness, pos_topleft.Y + thickness),
            new Vector2(pos_topleft.X + size.X, pos_topleft.Y + size.Y - thickness),
            shadow
        );
    }

    /// <returns>The correct filename for the face icon.</returns>
    private static string remap_job(byte ply_id, byte job_id) {
        if (job_id == 0x21) return "mface_147.dds.phyre"; // She-Goon

        string ply = ply_id switch {
            0 => "yuna",
            1 => "rikku",
            2 => "paine",
            _ => "m"
        };

        // Rewires Rikku's and Paine's job IDs to match original filenames
        byte job = job_id switch {
            24 or
            25 => 12, // Trainer
            26 or
            27 => 14, // Mascot
            28 => 16, // Psychic
            29 or
            30 or
            31 => 17, // Festivalist
            32 => 18, // Freelancer
            _  => job_id
        };

        return $"{ply}face_{job:D3}.dds.phyre";
    }

    private void populate_face_icons() {
        _face_icon_textures.Clear();

        foreach (FhSaveDisplayData save in FhApi.Saves.display_data) {
            FhSaveHeader2 header2 = MemoryMarshal.Read<FhSaveHeader2>(save.header);

            bool is_lm     = FhGlobal.game_id is FhGameId.FFX2LM;
            int  num_faces = is_lm ? 1 : 3;

            for (int i = 0; i < num_faces; i++) {
                byte ply_id = is_lm ? header2.lm_ply : header2.ply[i];
                byte job_id = is_lm ? header2.lm_job : header2.ply_jobs[i];

                if (job_id > 0x21) continue;

                string face = remap_job(ply_id, job_id);

                if (_face_icon_textures.TryGetValue(face, out _)) continue;

                FhTexture face_icon = new(Path.Join(MENU_FACE_DATA_DIR, face), FhTextureType.PHYRE);

                _face_icon_textures.Add(
                    face,
                    face_icon
                );
            }
        }
    }

    private void populate_map_icons() {
        _map_icon_textures.Clear();

        foreach (FhSaveDisplayData save in FhApi.Saves.display_data) {
            string map = Encoding.UTF8.GetString(save.icon_map);
            if (_map_icon_textures.TryGetValue(map, out _)) continue;

            FhTexture map_icon = new(Path.Join(MAP_ICONS_DIR, $"{map}_0.png"), FhTextureType.PNG);
            _map_icon_textures.Add(
                map,
                map_icon
            );
        }
    }

    /// <summary>Attempt to load all of the textures the save/load screen requires to display properly.</summary>
    /// <returns>Whether all textures have been successfully loaded.</returns>
    private bool try_load_textures() {
        Span<FhTexture> textures = [
            _texture_menuback,
            _texture_mahojin,
            _texture_plate,
            _texture_freetex,
            _texture_x2_bg,
            _texture_face_icon_default,
            .. _face_icon_textures.Values,
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
            _texture_menuback,
            _texture_mahojin,
            _texture_plate,
            _texture_freetex,
            _texture_x2_bg,
            _texture_face_icon_default,
            .. _face_icon_textures.Values,
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
        if (!_texture_menuback.try_use(out ImTextureRef bg,      out _)
         || !_texture_mahojin .try_use(out ImTextureRef mahojin, out _)
        ) {
            return;
        }

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        UV tex_uv = new Rect {
            pos  = new(  0f,   0f),
            size = new(512f, 512f),
        }.as_uv(_tex_menuback_size);

        UV screen_uv = new Rect {
            pos  = new(   0f,    0f),
            size = new(1920f, 1080f),
        }.scale_to_aspect(aspect_helper).as_uv();

        draw.AddImage(
            bg,
            screen_uv.p0,
            screen_uv.p1,
            tex_uv.p0,
            tex_uv.p1
        );

        UV mahojin_tuv = new Rect {
            pos  = new(   6f,  529f),
            size = new(1508f, 1509f),
        }.as_uv(_tex_mahojin_size);

        UV mahojin_suv = new Rect {
            pos  = new( 374f,  -53f),
            size = new(1226f, 1218f),
        }.scale_to_aspect(aspect_helper).as_uv();

        // Draws a scissor over the screen to prevent the mahojin glyph drawing out of bounds
        draw.PushClipRect(screen_uv.p0, screen_uv.p1, false);

        // Background glyph
        draw.AddImage(
            mahojin,
            mahojin_suv.p0,
            mahojin_suv.p1,
            mahojin_tuv.p0,
            mahojin_tuv.p1
        );

        draw.PopClipRect();

        // Draw a shadow over the corners of the background to match the vanilla menu
        uint grad_l = 0xD0000000;
        uint grad_r = 0x40000000;

        float mid_x = (screen_uv.p0.X + screen_uv.p1.X) * 0.5f;

        UV left_half  = new(screen_uv.p0, screen_uv.p1 with { X = mid_x });
        UV right_half = new(screen_uv.p0 with { X = mid_x }, screen_uv.p1);

        draw.AddRectFilledMultiColor(
            left_half.p0,
            left_half.p1,
            grad_l,
            grad_r,
            grad_r,
            grad_l
        );

        draw.AddRectFilledMultiColor(
            right_half.p0,
            right_half.p1,
            grad_r,
            grad_l,
            grad_l,
            grad_r
        );
    }

    /// <summary>Render the help text for the save/load screen.</summary>
    private void ui_help() {
        if (!_texture_freetex.try_use(out ImTextureRef freetex, out _)) return;

        if (!FhApi.Saves.get_system_mode(out FhSaveSystemMode? system_mode)) return;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        uint bg_grad_l = 0xFF000000; // black
        uint bg_grad_r = 0x10000000; // transparent black

        // Saving this one as a rect to more easily calculate the text position later
        Rect bg_screen = new Rect {
            pos  = new(   0f, 100f),
            size = new(1920f,  49f),
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

        uint accent_grad_l = 0xFF00BFB5; // yellow
        uint accent_grad_r = 0x1000BFB5; // transparent yellow

        UV accent_suv = new Rect {
            pos  = new(   0f, 138f),
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
            pos  = new(643f, 576f),
            size = new(140f,  58f),
        }.as_uv(_tex_freetex_size);

        UV title_suv = new Rect {
            pos  = new(151f, 94f),
            size = new( 78f, 32f),
        }.scale_to_aspect(aspect_helper).as_uv();

        draw.AddImage(
            freetex,
            title_suv.p0,
            title_suv.p1,
            title_tuv.p0,
            title_tuv.p1
        );

        //TODO: Add localization
        string text = _mode switch {
            UiMode.SET_SWAP => "Select save set",

            UiMode.SAVE_LIST  or
            UiMode.SAVE_POPUP => system_mode switch {
                FhSaveSystemMode.LOAD => "Select save data",
                FhSaveSystemMode.SAVE => "Select save area",

                //TODO-C#16: Remove this, since FhSaveSystemMode should be a `closed enum`.
                _ => throw new UnreachableException(),
            },

            _ => throw new NotImplementedException(),
        };

        Vector2 text_pos = bg_screen.left;
        text_pos.X += 240f * aspect_scale.X;
        text_pos.Y -=   6f * aspect_scale.Y;

        float font_size = 38f * font_scale;

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

        Vector2 text_pos = new(1717f, 146f);

        float line_height = 36f;
        float font_size   = 38f * font_scale;

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
        if (!_texture_freetex.try_use(out ImTextureRef freetex, out _)) return;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        UV cursor_tuv = new UV(
            new(  9f, 572f),
            new(180f, 478f)
        ).map_to(_tex_freetex_size);

        Vector2 cursor_size = new Vector2(103f, 58f) * aspect_scale;

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
            float offset = -6f * aspect_scale.X;
            float alpha  = 0.25f;

            // Sync movement with other cursors
            if (loop_progress >= 0.48f) {
                float fade_time = (loop_progress - 0.48f) / 0.52f;
                offset += fade_time * (travel_dist * 0.55f);
                alpha  *= 1f - fade_time;
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
                alpha  *= 1f - fade_time;
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

    /// <summary>Render the Change set button.</summary>
    private void ui_change_set() {
        if (!_texture_x2_bg.try_use(out ImTextureRef x2_bg, out _)) return;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        Rect bg_screen = new Rect {
            pos  = new(735f, 28f),
            size = new(450f, 60f),
        }.scale_to_aspect(aspect_helper);

        UV bg_suv = bg_screen.as_uv();

        if (mouse_hovered(bg_screen)) {
            _focus = UiFocus.ACTIVE_SET;
        }

        draw.AddRectFilled(
            bg_suv.p0,
            bg_suv.p1,
            0x80000000
        );

        UV accent_tuv = new Rect {
            pos  = new(1469f, 733f),
            size = new( 286f, 287f),
        }.as_uv(_tex_x2_bg_size);

        Vector2 accent_size = 60f * aspect_scale;

        Rect accent_left = new Rect {
            pos  = bg_screen.bottom_left + new Vector2(-2f, -58f) * aspect_scale,
            size = accent_size,
        };

        Rect accent_right = new Rect {
            pos  = bg_screen.top_right + new Vector2(2f, 58f) * aspect_scale,
            size = -accent_size,
        };

        UV accentl_suv = accent_left .as_uv();
        UV accentr_suv = accent_right.as_uv();

        draw.AddImage(
            x2_bg,
            accentl_suv.p0,
            accentl_suv.p1,
            accent_tuv.p0,
            accent_tuv.p1
        );

        draw.AddImage(
            x2_bg,
            accentr_suv.p0,
            accentr_suv.p1,
            accent_tuv.p0,
            accent_tuv.p1
        );

        float   font_size = 38f * font_scale;
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
        if (!_texture_plate.try_use(out ImTextureRef plate, out _)) return;

        int start_idx = _scrollable_sets.get_clip_start();

        // Texture coordinates
        int   tex_idx     = (set_idx % 8) + 1;
        float slice_size  = 64f;
        float plate_max_y = slice_size * tex_idx;

        UV plate_tuv = new Rect {
            pos  = new(  0f, plate_max_y - slice_size),
            size = new(512f, slice_size),
        }.as_uv(_tex_plate_size);

        // Screen coordinates
        Rect button = new Rect {
            pos  = new( 347f, 178f),
            size = new(1216f,  84f),
        };

        button.pos.Y += (button.size.Y + 10f) * (set_idx - start_idx);

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

        float font_size = 38f * font_scale;

        if (mouse_hovered(button_scaled)) {
            _focus = UiFocus.LIST;
            _scrollable_sets.hovered = set_idx;
        }

        bool hovering = _scrollable_sets.hovered == set_idx;

        // Shadows behind slots
        Vector2 shadow_offset = 10f * aspect_scale;

        draw.AddRectFilled(
            button_suv.p0 + shadow_offset,
            button_suv.p1 + shadow_offset,
            0x88000000
        );

        draw.AddImage(
            plate,
            button_suv.p0,
            button_suv.p1,
            plate_tuv.p0,
            plate_tuv.p1,
            hovering ? 0xFFC6B1AF : 0xFFE4F0F1
        );

        // Draw shading/edges on the save texture for definition
        float border_thickness = 5f * aspect_scale.Y;
        uint  highlight        = 0x28FFFFFF;
        uint  shadow           = 0x80000000;

        draw_highlight_shadow(
            draw,
            button_scaled.pos,
            button_scaled.size,
            border_thickness,
            highlight,
            shadow
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
        if (!_texture_x2_bg.try_use(out ImTextureRef x2_bg, out _)) return;

        //TODO: Add localization
        string message = "No saved data. Change set or return to the main menu.";

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        float font_size = 38f * font_scale;

        ImGui.PushFont(null, font_size);
        Vector2 text_size = ImGui.CalcTextSize(message);
        ImGui.PopFont();

        Vector2 text_margin = new Vector2(60f, 40f) * aspect_scale;
        Vector2 window_size = text_margin * 2 + text_size;

        Rect window = new Rect {
            pos  = FhApi.Gui.display_size / 2f,
            size = Vector2.Zero,
        }.expand(window_size, new(Alignment.CENTER, Alignment.CENTER));

        draw.AddRectFilled(
            window.top_left,
            window.bottom_right,
            0x80000000
        );

        UV accent_tuv = new Rect {
            pos  = new(1469f, 733f),
            size = new( 286f, 287f),
        }.as_uv(_tex_x2_bg_size);

        Vector2 accent_size = 125f * aspect_scale;

        Rect accent_left = new Rect {
            pos  = window.bottom_left + new Vector2(-4f, -121f) * aspect_scale,
            size = accent_size,
        };

        Rect accent_right = new Rect {
            pos  = window.top_right + new Vector2(3f, 121f) * aspect_scale,
            size = -accent_size,
        };

        UV accentl_suv = accent_left .as_uv();
        UV accentr_suv = accent_right.as_uv();

        draw.AddImage(
            x2_bg,
            accentl_suv.p0,
            accentl_suv.p1,
            accent_tuv.p0,
            accent_tuv.p1
        );

        draw.AddImage(
            x2_bg,
            accentr_suv.p0,
            accentr_suv.p1,
            accent_tuv.p0,
            accent_tuv.p1
        );

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
        if (!_scrollable_saves.is_within_clip(index)) return;

        if (!_texture_plate.try_use(out ImTextureRef plate, out _)) return;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        Rect save_rect = new Rect {
            pos  = new( 157f, 172f), // Topmost position
            size = new(1565f, 155f),
        };

        float save_gap = 10f;

        save_rect.pos.Y += (save_rect.size.Y + save_gap) * (index - _scrollable_saves.current);

        // We set the hovered state early to potentially use it later.
        if (mouse_hovered(save_rect.scale_to_aspect(aspect_helper))) {
            _focus = UiFocus.LIST;
            _scrollable_saves.hovered = index;
        }

        /* ===== Save Texture ===== */
        float header_size    = 47f;
        float vertical_space =  4f;

        Rect header_rect = new Rect {
            pos  = save_rect.pos,
            size = new(save_rect.size.X, header_size),
        };

        Rect info_rect = new Rect {
            pos  = new(save_rect.pos.X, header_rect.bottom.Y + vertical_space),
            size = new(save_rect.size.X, save_rect.size.Y - (header_size + vertical_space)),
        };

        // Texture UV changes for each slot
        float slice_size = 64f;
        float max_y = slice_size * ((save.slot % 8) + 1);

        float uv_y_start = max_y;
        float uv_y_end   = max_y - slice_size;
        float uv_y_split = uv_y_start + (uv_y_end - uv_y_start) * (47f / 151f);

        UV header_tuv = new Rect {
            pos  = new(  0f,              uv_y_start),
            size = new(512f, uv_y_split - uv_y_start),
        }.as_uv(_tex_plate_size);

        UV info_tuv = new Rect {
            pos  = new(  0f,            uv_y_split),
            size = new(512f, uv_y_end - uv_y_split),
        }.as_uv(_tex_plate_size);

        UV header_suv = header_rect.scale_to_aspect(aspect_helper).as_uv();
        UV info_suv   = info_rect  .scale_to_aspect(aspect_helper).as_uv();

        // Shadows behind saves
        Vector2 shadow_offset = 10f * aspect_scale;

        draw.AddRectFilled(
            header_suv.p0 + shadow_offset,
            header_suv.p1 + shadow_offset,
            0x88000000
        );

        draw.AddRectFilled(
            info_suv.p0 + shadow_offset,
            info_suv.p1 + shadow_offset,
            0x88000000
        );

        // Draw Autosave darker
        bool is_autosave = save.slot == 0 && !is_saving;

        uint header_clr   = is_autosave ? 0xFFB6A19F  : 0xFFC4D0D1;
        uint info_clr     = is_autosave ? 0xFFC6B1AF  : 0xFFE4F0F1;
        uint header_shade = is_autosave ? 0x63000000U : 0x33000000U;

        draw.AddImage(
            plate,
            header_suv.p0,
            header_suv.p1,
            header_tuv.p0,
            header_tuv.p1,
            header_clr
        );

        draw.AddImage(
            plate,
            info_suv.p0,
            info_suv.p1,
            info_tuv.p0,
            info_tuv.p1,
            info_clr
        );

        draw.AddRectFilled(
            header_suv.p0,
            header_suv.p1,
            header_shade
        );

        // Border highlights/shadows
        float border_thickness = 5f * aspect_scale.Y;
        uint  highlight        = 0x28FFFFFF;
        uint  shadow           = 0x80000000;

        draw_highlight_shadow(
            draw,
            header_suv.p0,
            header_rect.size * aspect_scale,
            border_thickness,
            highlight,
            shadow
        );

        draw_highlight_shadow(
            draw,
            info_suv.p0,
            info_rect.size * aspect_scale,
            border_thickness,
            highlight,
            shadow
        );

        switch (FhGlobal.game_id) {
            case FhGameId.FFX2:   ui_savefile_details_x2(save_rect, save); break;
            case FhGameId.FFX2LM: ui_savefile_details_lm(save_rect, save); break;
            default:                                                       break;
        }

        if (save.slot == 0 && is_saving) {
            ui_newdata(save_rect);
        }

        /* ===== Cursor ===== */
        if (_focus == UiFocus.LIST && _scrollable_saves.hovered == index) {
            save_rect.pos.Y += 28f;
            ui_cursor(save_rect.scale_to_aspect(aspect_helper).left);
        }

        // Handle input
        if (mouse_clicked(save_rect.scale_to_aspect(aspect_helper))) {
            fade_out(() => execute(save.slot));
        }
    }

    private void ui_savefile_details_x2(Rect save_rect, FhSaveDisplayData save) {
        if (save.slot == 0 && is_saving) return;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        /* ===== Party Icons ===== */
        UV face_tuv = new Rect {
            pos  = new(  0f,   0f),
            size = new(256f, 239f),
        }.as_uv(_tex_face_size);

        Vector2 face_size   = new(102f, 96f);
        float   face_gap    = 3f;
        float   face_offset = 5f;

        Rect face = new Rect {
            pos = new(
                save_rect.left.X + face_offset,
                save_rect.bottom.Y - 6f - face_size.Y
            ),
            size = face_size,
        };

        UV face_suv = face.scale_to_aspect(aspect_helper).as_uv();

        Vector2 face_screen_offset = new Vector2(face_size.X + face_gap, 0f) * aspect_scale;

        FhSaveHeader2 header2 = MemoryMarshal.Read<FhSaveHeader2>(save.header);

        for (byte i = 0; i < 3; i++) {
            byte ply_id = header2.ply[i];
            byte job_id = header2.ply_jobs[i];

            if (job_id > 0x21) {
                face_suv = face_suv.move(face_screen_offset);
                continue;
            }

            string    filename = remap_job(ply_id, job_id);
            FhTexture portrait = _face_icon_textures.GetValueOrDefault(filename, _texture_face_icon_default);

            uint alpha = job_id == 0x0 ? 0xA0C0C0C0 : 0xFFFFFFFF;

            if (portrait.try_use(out ImTextureRef faces, out _)) {
                draw.AddImage(
                    faces,
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

        // if (tex_map.try_use(out ImTextureRef map_icon, out _)) {
        //     UV tuv = new(new(0f, 1f), new(1f, 0f));
        //
        //     UV suv = new Rect {
        //         pos  = save_rect.top_right + new Vector2(-260f, 6f),
        //         size = new(255f, 144f),
        //     }.scale_to_aspect(aspect_helper).as_uv();
        //
        //     draw.AddImage(
        //         map_icon,
        //         suv.p0,
        //         suv.p1,
        //         tuv.p0,
        //         tuv.p1
        //     );
        // }

        /* ===== Text ===== */

        /* ===== Header Details ===== */
        float border_thickness =  5f;
        float header_size      = 47f;
        float text_offset      = 12f;
        float text_margin      = 71f;
        float autosave_offset  = 16f;

        Vector2 header_text_pos_l = save_rect.top_left + new Vector2(
            border_thickness + text_offset,
            (header_size / 2) - 2f // - 2f to center
        );

        float font_size = 38f * font_scale;

        //TODO: Add localization
        string slot_text   = save.slot == 0 ? "Autosave" : save.slot_str;
        string create_time = save.create_time.ToString(@"yyyy\/M\/d H\:mm\:ss");

        Vector2 text_size = FhApi.Gui.draw_text(
            draw,
            header_text_pos_l * aspect_scale + aspect_helper.pos,
            slot_text,
            font_size,
            true,
            new(Alignment.BEGIN, Alignment.CENTER),
            save.slot == 0 ? 0xFF19D8FF : 0xFFFFFFFF // Autosave text is yellow
        );

        header_text_pos_l = header_text_pos_l * aspect_scale + aspect_helper.pos;
        header_text_pos_l.X += text_size.X + text_margin * aspect_scale.X;

        // If the slot text is "Autosave", increase the margin
        if (save.slot == 0) header_text_pos_l.X += autosave_offset * aspect_scale.X;

        FhApi.Gui.draw_text(
            draw,
            header_text_pos_l,
            save.location,
            font_size,
            true,
            new(Alignment.BEGIN, Alignment.CENTER)
        );

        float map_icon_size = 255f;
        text_margin = 57f;

        Vector2 header_text_pos_r = save_rect.top_right + new Vector2(
            border_thickness - map_icon_size - text_margin,
            (header_size / 2) - 2f
        );

        FhApi.Gui.draw_text(
            draw,
            header_text_pos_r * aspect_scale + aspect_helper.pos,
            create_time,
            font_size,
            true,
            new(Alignment.END, Alignment.CENTER)
        );

        /* ===== Info Details ===== */
        float margin_from_header    =   4f;
        float margin_from_last_face = 132f;
        float margin_from_map       =  57f;
        float ck_offset             =  92f;
        float line_height           =  49f;

        Vector2 info_text_pos_l = save_rect.top_left + new Vector2(
            face_offset + face_size.X * 3 + face_gap * 2 + margin_from_last_face,
            header_size + margin_from_header + line_height / 2f
        );

        Vector2 info_text_pos_r = save_rect.top_right + new Vector2(
            border_thickness - map_icon_size - margin_from_map,
            header_size + margin_from_header + line_height / 2f
        );

        // When running in CJK, we slightly reduce the text margin and hardcode
        // the "STORY COMPLETED" string to shorten it and prevent overlap.
        bool cjk = FhGlobal.lang_id == FhLangId.Chinese
                || FhGlobal.lang_id == FhLangId.Japanese
                || FhGlobal.lang_id == FhLangId.Korean;

        string completion;
        if (cjk) {
            info_text_pos_l.X -= ck_offset;
            completion = $"STORY COMPLETED: {header2.completion}%";
        }
        else {
            completion = Encoding.UTF8.GetString(save.completion);
        }

        FhApi.Gui.draw_text(
            draw,
            info_text_pos_l * aspect_scale + aspect_helper.pos,
            save.player_name,
            font_size,
            true,
            new(Alignment.BEGIN, Alignment.CENTER)
        );

        FhApi.Gui.draw_text(
            draw,
            new Vector2(info_text_pos_r.X + 2f, info_text_pos_r.Y) * aspect_scale + aspect_helper.pos,
            completion,
            font_size,
            true,
            new(Alignment.END, Alignment.CENTER)
        );

        info_text_pos_l.Y += line_height;
        info_text_pos_r.Y += line_height;

        FhApi.Gui.draw_text(
            draw,
            info_text_pos_l * aspect_scale + aspect_helper.pos,
            save.chapter,
            font_size,
            true,
            new(Alignment.BEGIN, Alignment.CENTER)
        );

        FhApi.Gui.draw_text(
            draw,
            info_text_pos_r * aspect_scale + aspect_helper.pos,
            save.play_time,
            font_size,
            true,
            new(Alignment.END, Alignment.CENTER)
        );
    }

    private void ui_savefile_details_lm(Rect save_rect, FhSaveDisplayData save) {
        if (save.slot == 0 && is_saving) return;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        /* ===== Party Icons ===== */
        UV face_tuv = new Rect {
            pos  = new(  0f,   0f),
            size = new(256f, 239f),
        }.as_uv(_tex_face_size);

        Vector2 face_size   = new(102f, 96f);
        float   face_gap    = 25f;
        float   face_offset =  6f;

        Rect face = new Rect {
            pos = new(
                save_rect.left.X + face_offset + (3 * face_gap),
                save_rect.bottom.Y - 6f - face_size.Y
            ),
            size = face_size,
        };

        UV face_suv = face.scale_to_aspect(aspect_helper).as_uv();

        Vector2 face_screen_offset = new Vector2(-face_gap, 0f) * aspect_scale;

        FhSaveHeader2 header2 = MemoryMarshal.Read<FhSaveHeader2>(save.header);

        for (int i = 3; i >= 0; i--) {
            byte ply_id = header2.lm_ply;
            byte job_id = header2.lm_job;

            if (job_id > 0x21) {
                face_suv = face_suv.move(face_screen_offset);
                continue;
            }

            string    filename = remap_job(ply_id, job_id);
            FhTexture portrait = _face_icon_textures.GetValueOrDefault(filename, _texture_face_icon_default);

            if (portrait.try_use(out ImTextureRef faces, out _)) {
                uint alpha = ImGui.GetColorU32(new Vector4(1f, 1f, 1f, 1f - (i * 0.25f)));

                draw.AddImage(
                    faces,
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

        // if (tex_map.try_use(out ImTextureRef map_icon, out _)) {
        //     UV tuv = new(new(0f, 1f), new(1f, 0f));
        //
        //     UV suv = new Rect {
        //         pos  = save_rect.top_right + new Vector2(-260f, 6f),
        //         size = new(255f, 144f),
        //     }.scale_to_aspect(aspect_helper).as_uv();
        //
        //     draw.AddImage(
        //         map_icon,
        //         suv.p0,
        //         suv.p1,
        //         tuv.p0,
        //         tuv.p1
        //     );
        // }

        /* ===== Text ===== */

        /* ===== Header Details ===== */
        float border_thickness =  5f;
        float header_size      = 47f;
        float text_offset      = 12f;
        float text_margin      = 81f;
        float autosave_offset  = 16f;

        Vector2 header_text_pos_l = save_rect.top_left + new Vector2(
            border_thickness + text_offset,
            (header_size / 2) - 2f // - 2f to center
        );

        float font_size = 38f * font_scale;

        //TODO: Add localization
        string slot_text   = save.slot == 0 ? "Autosave" : save.slot_str;
        string create_time = save.create_time.ToString(@"yyyy\/M\/d H\:mm\:ss");

        Vector2 text_size = FhApi.Gui.draw_text(
            draw,
            header_text_pos_l * aspect_scale + aspect_helper.pos,
            slot_text,
            font_size,
            true,
            new(Alignment.BEGIN, Alignment.CENTER),
            save.slot == 0 ? 0xFF19D8FF : 0xFFFFFFFF // Autosave text is yellow
        );

        header_text_pos_l = header_text_pos_l * aspect_scale + aspect_helper.pos;
        header_text_pos_l.X += text_size.X + text_margin * aspect_scale.X;

        // If the slot text is "Autosave", increase the margin
        if (save.slot == 0) header_text_pos_l.X += autosave_offset * aspect_scale.X;

        FhApi.Gui.draw_text(
            draw,
            header_text_pos_l,
            save.location,
            font_size,
            true,
            new(Alignment.BEGIN, Alignment.CENTER)
        );

        float map_icon_size = 255f;
        text_margin = 57f;

        Vector2 header_text_pos_r = save_rect.top_right + new Vector2(
            border_thickness - map_icon_size - text_margin,
            (header_size / 2) - 2f
        );

        FhApi.Gui.draw_text(
            draw,
            header_text_pos_r * aspect_scale + aspect_helper.pos,
            create_time,
            font_size,
            true,
            new(Alignment.END, Alignment.CENTER)
        );

        /* ===== Info Details ===== */
        float margin_from_header    =  4f;
        float margin_from_last_face = 86f;
        float margin_from_map       = 57f;
        float level_offset          = 85f;
        float line_height           = 49f;

        Vector2 info_text_pos_l = save_rect.top_left + new Vector2(
            face_offset + (face_gap * 3) + face_size.X + margin_from_last_face,
            header_size + margin_from_header + line_height / 2f
        );

        Vector2 info_text_pos_r = save_rect.top_right + new Vector2(
            border_thickness - map_icon_size - margin_from_map,
            header_size + margin_from_header + line_height / 2f
        );

        FhApi.Gui.draw_text(
            draw,
            info_text_pos_l * aspect_scale + aspect_helper.pos,
            save.player_name,
            font_size,
            true,
            new(Alignment.BEGIN, Alignment.CENTER)
        );

        info_text_pos_l.Y += line_height;
        info_text_pos_r.Y += line_height;

        text_size = FhApi.Gui.draw_text(
            draw,
            info_text_pos_l * aspect_scale + aspect_helper.pos,
            save.lm_job,
            font_size,
            true,
            new(Alignment.BEGIN, Alignment.CENTER)
        );

        info_text_pos_l = info_text_pos_l * aspect_scale + aspect_helper.pos;
        info_text_pos_l.X += text_size.X + level_offset * aspect_scale.X;

        FhApi.Gui.draw_text(
            draw,
            info_text_pos_l,
            save.lm_level,
            font_size,
            true,
            new(Alignment.BEGIN, Alignment.CENTER)
        );

        FhApi.Gui.draw_text(
            draw,
            info_text_pos_r * aspect_scale + aspect_helper.pos,
            save.play_time,
            font_size,
            true,
            new(Alignment.END, Alignment.CENTER)
        );
    }

    private void ui_newdata(Rect save_rect) {
        if (!_texture_freetex.try_use(out ImTextureRef freetex, out _)) return;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        /* ===== New Save Data Text ===== */
        //TODO: Add localization
        string text = "New Save Data";

        float header_height = 47f;

        Vector2 text_pos = save_rect.top_left + new Vector2(
            17f,
            header_height + (save_rect.size.Y - header_height) / 2f + 3f
        );

        float font_size = 38f * font_scale;

        FhApi.Gui.draw_text(
            draw,
            text_pos * aspect_scale + aspect_helper.pos,
            text,
            font_size,
            true,
            new(Alignment.BEGIN, Alignment.CENTER)
        );

        /* ===== "+" Icon ===== */
        uint grad_l = 0xFF191919; // grey
        uint grad_r = 0xFF252525; // black

        UV suv = new Rect {
            pos  = save_rect.top_right + new Vector2(-260f, 6f),
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
            pos  = save_rect.top_right + new Vector2(-156f, 56f),
            size = new(45f, 47f),
        }.scale_to_aspect(aspect_helper).as_uv();

        draw.AddImage(
            freetex,
            plus_suv.p0,
            plus_suv.p1,
            plus_tuv.p0,
            plus_tuv.p1
        );
    }

    private void ui_scrollbar() {
        if (_current_scrollable.max <= _current_scrollable.visible) return;

        Rect track = new() {
            pos  = new(1753f, 205f),
            size = new(  17f, 754f),
        };

        uint track_color = 0xFF000000;

        Vector2 thumb_margin = new(2f);
        Vector2 thumb_size   = new(13f, 60f);

        uint thumb_color_top    = 0xFFCBCBCB;
        uint thumb_color_bottom = 0xFF808080;

        Vector2 triangle_size = new(35f, 17f);
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
