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
public sealed class FhSaveUiX : FhRuntimeSaveUiBase {
    private readonly FhTexture _texture_map_icon_default = new(Path.Join(DIR_X_MAP_ICONS, "0.png"), FhTextureType.PNG);

    private readonly FhTexture _texture_bg           = new(Path.Join(DIR_X_MENU, "ffx_bg.dds.phyre"),        FhTextureType.PHYRE);
    private readonly FhTexture _texture_help         = new(Path.Join(DIR_X_HIKU, "0_0_512_448_0.dds.phyre"), FhTextureType.PHYRE);
    private readonly FhTexture _texture_battle_kuang = new(Path.Join(DIR_X_MENU, "battle_kuang.dds.phyre"),  FhTextureType.PHYRE);
    private readonly FhTexture _texture_faces        = new(Path.Join(DIR_X_MENU, "face_ply.dds.phyre"),      FhTextureType.PHYRE);
    private readonly FhTexture _texture_meswin       = new(Path.Join(DIR_X_MENU, "meswin.dds.phyre"),        FhTextureType.PHYRE);
    private readonly FhTexture _texture_summonbg     = new(Path.Join(DIR_X_MENU, "summonbg.dds.phyre"),      FhTextureType.PHYRE);

    private readonly Vector2 _tex_map_icon_size  = new(310f, 210f);

    private readonly Vector2 _tex_bg_size           = new(2048f, 1024f);
    private readonly Vector2 _tex_help_size         = new(2048f, 2048f);
    private readonly Vector2 _tex_battle_kuang_size = new(2048f, 1024f);
    private readonly Vector2 _tex_faces_size        = new(1024f,  512f);
    private readonly Vector2 _tex_meswin_size       = new(1024f, 1024f);
    private readonly Vector2 _tex_summonbg_size     = new(1024f, 1024f);

    private readonly FhTexture _texture_ffx2_face_icon_default = new(Path.Join(DIR_X2_FACE_DATA, "mface_000.dds.phyre"), FhTextureType.PHYRE);
    private readonly FhTexture _texture_ffx2_map_icon_default  = new(Path.Join(DIR_X2_MAP_ICONS, "logo_j.png"),          FhTextureType.PNG);

    private readonly FhTexture _texture_freetex = new(Path.Join(DIR_X2_MENU, "freetex.dds.phyre"), FhTextureType.PHYRE);

    private readonly Vector2 _tex_ffx2_face_size     = new(256f, 256f);
    private readonly Vector2 _tex_ffx2_map_icon_size = new(320f, 176f);

    private readonly Vector2 _tex_freetex_size = new(1024f,  768f);

    protected override FhTexture[] textures => [
        _texture_bg,
        _texture_help,
        _texture_battle_kuang,
        _texture_faces,
        _texture_meswin,
        _texture_summonbg,
        _texture_freetex,

        // _texture_map_icon_default,
        _texture_ffx2_face_icon_default,
    ];

    private readonly NineSliceHelper  _savefile_border_helper;

    public FhSaveUiX() {
        _savefile_border_helper = NineSliceHelper.create(
            _tex_meswin_size,
            new(385f, 544f),
            new(450f, 609f),
            new(  9f,   9f)
        );
    }

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
            ui_setlist();
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

    protected override void ui_background() {
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

    protected override void ui_help() {
        if (!_texture_help  .try_use(out ImTextureRef help,   out _)
         || !_texture_meswin.try_use(out ImTextureRef meswin, out _)
        ) {
            return;
        }

        if (!FhApi.Saves.impl_get_system_mode(out FhSaveSystemMode? system_mode)) {
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

        FhApi.Gui.draw_text(
            draw,
            text_pos,
            text,
            font_size,
            true,
            Alignment2D.LEFT
        );
    }

    protected override void ui_savecount() {
        FhSaveSet set = FhApi.Saves.sets_get_active();

        //TODO: Add localization
        string save_text = set.has_autosave
            ? $" + {set.count - 1} saves"
            : $"{set.count} saves";

        Vector2 text_pos = new(1722f, 144f);

        float line_height = 38f;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        Vector2 saves_text_size = FhApi.Gui.draw_text(
            draw,
            text_pos * aspect_scale + aspect_helper.pos,
            save_text,
            font_size,
            true,
            Alignment2D.RIGHT
        );

        if (set.has_autosave) {
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
            set.name,
            font_size,
            true,
            Alignment2D.RIGHT
        );
    }

    protected override void ui_cursor(Vector2 target_pos, bool overlap = true) {
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

    protected override void ui_change_set() {
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
            FhApi.Audio.play_sound(sound_id_action, ACTION_VOLUME);
            _focus = UiFocus.ACTIVE_SET;
        }

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();
        draw.AddImage(battle_kuang, bg_suv.p0, bg_suv.p1, bg_tuv.p0, bg_tuv.p1);

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
            FhApi.Audio.play_sound(sound_id_set_switch);
            change_mode(UiMode.SET_SWAP);
        }
    }

    protected override void ui_set(FhSaveSet set, int global_idx, int local_idx) {
        if (!_texture_summonbg.try_use(out ImTextureRef summonbg, out _)) {
            return;
        }

        // Texture coordinates
        int   tex_idx    = global_idx % 8;
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

        button.pos.Y += (button.size.Y + 10f) * local_idx;

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

        if (mouse_hovered(button_scaled)) {
            if (_focus != UiFocus.LIST || _scrollable_sets.hovered != global_idx) {
                FhApi.Audio.play_sound(sound_id_action, ACTION_VOLUME);
            }

            _focus = UiFocus.LIST;
            _scrollable_sets.hovered = global_idx;
        }

        bool hovering = _scrollable_sets.hovered == global_idx;

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
            set.name,
            font_size,
            true,
            Alignment2D.LEFT
        );

        Vector2 save_count_size = FhApi.Gui.draw_text(
            draw,
            save_count_pos,
            set.has_autosave
                ? $" + {set.count - 1} saves"
                : $"{set.count} saves",
            font_size,
            true,
            Alignment2D.RIGHT
        );

        if (set.has_autosave) {
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
            FhApi.Audio.play_sound(sound_id_set_switch);
            switch_set(set.name);
        }
    }

    protected override void ui_setlist() {
        if (_mode != UiMode.SET_SWAP) return;

        int start_idx = _scrollable_sets.get_clip_start();
        int end_idx   = _scrollable_sets.get_clip_end();

        for (int set_idx = start_idx; set_idx < end_idx; set_idx++) {
            string set_name = _set_list[set_idx];

            if (!FhApi.Saves.sets_get(set_name, out FhSaveSet? set))
                continue;

            ui_set(set, set_idx, set_idx - start_idx);
        }
    }

    protected override void ui_savelist() {
        FhSaveSet set = FhApi.Saves.sets_get_active();

        _scrollable_saves.max = is_saving
            ? set.count + 1 - (set.has_autosave ? 1 : 0) // Add one for New Save Data button
            : set.count;

        if (!is_saving && set.count == 0) {
            ui_no_saves();
            return;
        }

        int start = _scrollable_saves.get_clip_start();
        int end   = _scrollable_saves.get_clip_end();

        int ui_data_start = is_saving && set.has_autosave ? 1 : 0;

        for (int save_idx = start; save_idx < end; save_idx++) {
            if (is_saving && save_idx == 0) { // New Save Data
                ui_save(new FhSaveUiData { slot = 0 }, 0, 0);
                continue;
            }

            int ui_data_index = is_saving
                ? ui_data_start + save_idx - 1
                : ui_data_start + save_idx;

            if (FhApi.Saves.sets_get_ui_data(ui_data_index, out FhSaveUiData save)) {
                ui_save(save, save_idx, save_idx - start);
            }
        }
    }

    protected override void ui_no_saves() {
        if (!_texture_meswin.try_use(out ImTextureRef meswin, out _)) {
            return;
        }

        if (!FhApi.Saves.impl_get_system_mode(out FhSaveSystemMode? system_mode)) {
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

    protected override void ui_save(FhSaveUiData save, int global_idx, int local_idx) {
        if (!_texture_meswin.try_use(out ImTextureRef meswin, out _)) {
            return;
        }

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        Rect save_outer_bounds = new Rect {
            pos  = new( 160f, 176f), // Topmost position
            size = new(1561f, 152f),
        };

        if (save.slot == 0 && is_saving) {
            ui_newdata(save_outer_bounds);
            return;
        }

        float save_gap = 13f;

        save_outer_bounds.pos.Y += (save_outer_bounds.size.Y + save_gap) * local_idx;

        // We set the hovered state early to potentially use it later.
        if (mouse_hovered(save_outer_bounds.scale_to_aspect(aspect_helper))) {
            if (_focus != UiFocus.LIST || _scrollable_saves.hovered != global_idx) {
                FhApi.Audio.play_sound(sound_id_action, ACTION_VOLUME);
            }

            _focus = UiFocus.LIST;
            _scrollable_saves.hovered = global_idx;
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

        Rect info_bounds = new() {
            pos  = save_bounds.pos with {
                Y = header_bounds.bottom.Y,
            },

            size = save_bounds.size with {
                Y = save_bounds.bottom.Y - header_bounds.bottom.Y,
            },
        };

        switch (FhGlobal.game_id) {
            case FhGameId.FFX:    ui_save_details_x (save, save_bounds, header_bounds, info_bounds); break;
            case FhGameId.FFX2:   ui_save_details_x2(save, save_bounds, header_bounds, info_bounds); break;
            case FhGameId.FFX2LM: ui_save_details_lm(save, save_bounds, header_bounds, info_bounds); break;
        }

        /* ===== Cursor ===== */
        if (_focus == UiFocus.LIST && _scrollable_saves.hovered == global_idx) {
            Rect save_rect_scaled = save_outer_bounds.scale_to_aspect(aspect_helper);

            Vector2 cursor_target = save_rect_scaled.left with {
                Y = save_rect_scaled.left.Y + 4f,
            };

            ui_cursor(cursor_target);
        }

        // Handle input
        if (mouse_clicked(save_outer_bounds.scale_to_aspect(aspect_helper))) {
            FhApi.Audio.play_sound(sound_id_action, ACTION_VOLUME);
            fade_out(() => FhApi.Saves.impl_execute(save.slot));
        }
    }

    protected override void ui_save_details_x(
        FhSaveUiData save,
        Rect save_bounds,
        Rect header_bounds,
        Rect info_bounds
    ) {
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

        Vector2 face_size = new(info_bounds.size.Y);
        float   face_gap  = 6f;

        Rect face = new() {
            pos = info_bounds.bottom_left with {
                Y = info_bounds.bottom_left.Y - face_size.Y,
            },
            size = face_size,
        };

        UV face_suv = face.scale_to_aspect(aspect_helper).as_uv();

        Vector2 face_screen_offset = new Vector2(face_size.X + face_gap, 0f) * aspect_scale;

        SaveHeaderX header = save.header.x;

        foreach (int chr in header.formation[..3]) {
            if (chr == 0xFF) continue;

            UV tuv = face_tuvs[chr];

            draw.AddImage(faces, face_suv.p0, face_suv.p1, tuv.p0, tuv.p1);

            face_suv = face_suv.move(face_screen_offset);
        }

        /* ===== Location Icon ===== */
        FhTexture tex_map       = save.icon_map;
        Vector2   map_icon_size = new(255f, 144f);

        // if (tex_map.try_use(out ImTextureRef map_icon, out _)) {
        //     UV tuv = new(new(0f, 1f), new(1f, 0f));
        //
        //     UV suv = new Rect {
        //         pos = save_bounds.top_right with {
        //             X = save_bounds.top_right.X - map_icon_size.X,
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
        float margin_face = 38f;
        float margin_map  = 24f;

        float info_margin_left  = face_size.X * 3f + face_gap * 2f + margin_face;
        float info_margin_right = map_icon_size.X + margin_map;

        info_bounds.pos.X  += info_margin_left;
        info_bounds.size.X -= info_margin_left + info_margin_right;

        float info_text_top_y    = float.Lerp(info_bounds.top.Y, info_bounds.bottom.Y, 0.25f);
        float info_text_bottom_y = float.Lerp(info_bounds.top.Y, info_bounds.bottom.Y, 0.7f);

        Vector2 info_text_pos_name = new() {
            X = info_bounds.left.X,
            Y = info_text_top_y,
        };

        Vector2 info_text_pos_playtime = new() {
            X = info_bounds.left.X,
            Y = info_text_bottom_y,
        };

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
            info_text_pos_playtime * aspect_scale + aspect_helper.pos,
            save.play_time,
            font_size,
            true,
            Alignment2D.LEFT
        );
    }

    protected override void ui_save_details_x2(
        FhSaveUiData save,
        Rect save_bounds,
        Rect header_bounds,
        Rect info_bounds
    ) {
        if (save.slot == 0 && is_saving) return;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        /* ===== Party Icons ===== */
        UV face_tuv = new Rect {
            pos  = new(  0f,   0f),
            size = new(256f, 239f),
        }.as_uv(_tex_ffx2_face_size);

        float   face_height = info_bounds.size.Y;
        Vector2 face_size   = new(1.0625f * face_height, face_height);
        float   face_gap    = 3f;

        Rect face_bounds = new() {
            pos = info_bounds.bottom_left with {
                Y = info_bounds.bottom_left.Y - face_size.Y,
            },
            size = face_size,
        };

        UV face_suv = face_bounds.scale_to_aspect(aspect_helper).as_uv();

        Vector2 face_screen_offset = new Vector2(face_size.X + face_gap, 0f) * aspect_scale;

        SaveHeaderX2 header = save.header.x2;

        for (byte i = 0; i < 3; i++) {
            byte job_id = header.ply_jobs[i];

            if (job_id > 0x21) {
                face_suv = face_suv.move(face_screen_offset);
                continue;
            }

            FhTexture portrait = save.icon_chr[i];

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
        FhTexture tex_map       = save.icon_map;
        Vector2   map_icon_size = new(255f, save_bounds.size.Y);

        // if (tex_map.try_use(out ImTextureRef map_icon, out _)) {
        //     UV tuv = new(new(0f, 1f), new(1f, 0f));
        //
        //     UV suv = new Rect {
        //         pos  = save_bounds.top_right with {
        //             X = save_bounds.top_right.X - map_icon_size.X,
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
        if (header.id_location != 0) {
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
        float info_margin_left  = face_size.X * 3 + face_gap * 2f + text_margin_between;
        float info_margin_right = map_icon_size.X + text_margin_between;

        info_bounds.pos.X  += info_margin_left;
        info_bounds.size.X -= info_margin_left + info_margin_right;

        float info_text_top_y    = float.Lerp(info_bounds.top.Y, info_bounds.bottom.Y, 0.25f);
        float info_text_bottom_y = float.Lerp(info_bounds.top.Y, info_bounds.bottom.Y, 0.7f);

        Vector2 info_text_pos_name = new() {
            X = info_bounds.left.X,
            Y = info_text_top_y,
        };

        Vector2 info_text_pos_chapter = new() {
            X = info_bounds.left.X,
            Y = info_text_bottom_y,
        };

        Vector2 info_text_pos_completion = new() {
            X = info_bounds.right.X,
            Y = info_text_top_y,
        };

        Vector2 info_text_pos_playtime = new() {
            X = info_bounds.right.X,
            Y = info_text_bottom_y,
        };

        // When running in CJK, we hardcode the "STORY COMPLETED" string.
        string completion = FhGlobal.lang_id.is_cjk
            ? $"STORY COMPLETED: {header.completion}%"
            : save.completion;

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

    protected override void ui_save_details_lm(
        FhSaveUiData save,
        Rect save_bounds,
        Rect header_bounds,
        Rect info_bounds
    ) {
        if (save.slot == 0 && is_saving) return;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        /* ===== Party Icons ===== */
        UV face_tuv = new Rect {
            pos  = new(  0f,   0f),
            size = new(256f, 239f),
        }.as_uv(_tex_ffx2_face_size);

        float   face_height = info_bounds.size.Y;
        Vector2 face_size   = new(1.0625f * face_height, face_height);
        float   face_gap    = 25f;

        Rect face = new() {
            pos = info_bounds.bottom_left with {
                X = info_bounds.bottom_left.X + face_gap * 3,
                Y = info_bounds.bottom_left.Y - face_size.Y,
            },
            size = face_size,
        };

        UV face_suv = face.scale_to_aspect(aspect_helper).as_uv();

        Vector2 face_screen_offset = new Vector2(-face_gap, 0f) * aspect_scale;

        SaveHeaderX2 header = save.header.x2;

        byte job_id = header.lm_job;

        if (job_id <= 0x21) {
            FhTexture portrait = save.icon_chr[0];

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
        FhTexture tex_map       = save.icon_map;
        Vector2   map_icon_size = new(255f, save_bounds.size.Y);

        // if (tex_map.try_use(out ImTextureRef map_icon, out _)) {
        //     UV tuv = new(new(0f, 1f), new(1f, 0f));
        //
        //     UV suv = new Rect {
        //         pos = save_bounds.top_right with {
        //             X = save_bounds.top_right.X - map_icon_size.X,
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
        if (header.lm_id_location != 0) {
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
        float info_margin_left  = face_size.X + face_gap * 3f + text_margin_between;
        float info_margin_right = map_icon_size.X + text_margin_between;

        info_bounds.pos.X  += info_margin_left;
        info_bounds.size.X -= info_margin_left + info_margin_right;

        float top_line_y    = float.Lerp(info_bounds.top.Y, info_bounds.bottom.Y, 0.25f);
        float bottom_line_y = float.Lerp(info_bounds.top.Y, info_bounds.bottom.Y, 0.7f);

        Vector2 info_text_pos_name = new() {
            X = info_bounds.left.X,
            Y = top_line_y,
        };

        Vector2 info_text_pos_job = new() {
            X = info_bounds.left.X,
            Y = bottom_line_y,
        };

        Vector2 info_text_pos_playtime = new() {
            X = info_bounds.right.X,
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

    protected override void ui_newdata(Rect bounds) {
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
                FhApi.Audio.play_sound(sound_id_action, ACTION_VOLUME);
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
        // grad_l = 0xFF191919; // grey
        // grad_r = 0xFF252525; // black
        //
        // UV suv = new Rect {
        //     pos  = save_rect.pos + new Vector2(1302f, 4f),
        //     size = new(255f, 144f),
        // }.scale_to_aspect(aspect_helper).as_uv();
        //
        // draw.AddRectFilledMultiColor(
        //     suv.p0,
        //     suv.p1,
        //     grad_r,
        //     grad_r,
        //     grad_l,
        //     grad_l
        // );
        //
        // UV plus_tuv = new Rect {
        //     pos  = new(581f, 724f),
        //     size = new( 36f,  36f),
        // }.as_uv(_tex_freetex_size);
        //
        // UV plus_suv = new Rect {
        //     pos  = save_rect.pos + new Vector2(1405f, 56f),
        //     size = new(45f, 47f),
        // }.scale_to_aspect(aspect_helper).as_uv();
        //
        // draw.AddImage(
        //     freetex,
        //     plus_suv.p0,
        //     plus_suv.p1,
        //     plus_tuv.p0,
        //     plus_tuv.p1
        // );

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
            FhApi.Audio.play_sound(sound_id_action, ACTION_VOLUME);
            fade_out(() => FhApi.Saves.impl_execute(0));
        }
    }

    private void ui_scrollbar() {
        Rect track_bounds = new() {
            pos  = new(1753f, 209f),
            size = new(  17f, 756f),
        };

        float triangle_gap = 15f;

        ui_scrollbar(
            _current_scrollable,
            track_bounds,
            triangle_gap
        );
    }
}
