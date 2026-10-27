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
[FhLoad(FhGameId.FFX | FhGameId.FFX2 | FhGameId.FFX2LM)]
public sealed class FhSaveUiX : FhRuntimeSaveUiBase {
    private readonly FhTexture _texture_bg           = new(Path.Join(DIR_X_MENU, "ffx_bg.dds.phyre"),        FhTextureType.GAME_PHYRE_2D);
    private readonly FhTexture _texture_help         = new(Path.Join(DIR_X_HIKU, "0_0_512_448_0.dds.phyre"), FhTextureType.GAME_PHYRE_2D);
    private readonly FhTexture _texture_battle_kuang = new(Path.Join(DIR_X_MENU, "battle_kuang.dds.phyre"),  FhTextureType.GAME_PHYRE_2D);
    private readonly FhTexture _texture_faces_ply    = new(Path.Join(DIR_X_MENU, "face_ply.dds.phyre"),      FhTextureType.GAME_PHYRE_2D);
    private readonly FhTexture _texture_faces_smn    = new(Path.Join(DIR_X_MENU, "face_smn.dds.phyre"),      FhTextureType.GAME_PHYRE_2D);
    private readonly FhTexture _texture_meswin       = new(Path.Join(DIR_X_MENU, "meswin.dds.phyre"),        FhTextureType.GAME_PHYRE_2D);
    private readonly FhTexture _texture_summonbg     = new(Path.Join(DIR_X_MENU, "summonbg.dds.phyre"),      FhTextureType.GAME_PHYRE_2D);

    private readonly Vector2 _tex_meswin_size = new(1024f, 1024f);

    private readonly FhTexture _texture_freetex = new(Path.Join(DIR_X2_MENU, "freetex.dds.phyre"), FhTextureType.GAME_PHYRE_2D);

    private readonly Vector2 _tex_ffx2_face_size = new(256f, 256f);

    protected override FhTexture[] textures => [
        _texture_bg,
        _texture_help,
        _texture_battle_kuang,
        _texture_faces_ply,
        _texture_faces_smn,
        _texture_meswin,
        _texture_summonbg,
        _texture_freetex,
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

    protected override void ui_background() {
        if (!_texture_bg.try_use(out ImTextureRef bg, out FhTextureMetadata? bg_meta)) {
            return;
        }

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        UV tex_uv = new Rect {
            pos  = new(   0f,    0f),
            size = new(1920f, 1024f),
        }.as_uv(bg_meta.size);

        UV screen_uv = new Rect {
            pos  = new(   0f,    0f),
            size = new(1920f, 1080f),
        }.scale_to_aspect(aspect_helper).as_uv();

        draw.AddImage(bg, screen_uv.p0, screen_uv.p1, tex_uv.p0, tex_uv.p1);
    }

    protected override void ui_help() {
        if (!_texture_help     .try_use(out ImTextureRef help,   out FhTextureMetadata? help_meta)
            || !_texture_meswin.try_use(out ImTextureRef meswin, out FhTextureMetadata? meswin_meta)
        ) {
            return;
        }

        if (!FhApi.Saves.impl_get_system_mode(out FhSaveSystemMode? save_mode)) {
            return;
        }

        UV bg_tuv = new Rect {
            pos  = new( 154f, 1762f),
            size = new(1740f,  105f),
        }.as_uv(help_meta.size);

        // Saving this one as a rect to more easily calculate the text position later
        Rect bg_screen = new Rect {
            pos  = new( 151f, 96f),
            size = new(1600f, 55f),
        }.scale_to_aspect(aspect_helper);

        UV bg_suv = bg_screen.as_uv();

        UV title_tuv = new Rect {
            pos  = new(670f, 38f),
            size = new( 74f, 21f),
        }.as_uv(meswin_meta.size);

        UV title_suv = new Rect {
            pos  = new(151f, 82f),
            size = new( 74f, 18f),
        }.scale_to_aspect(aspect_helper).as_uv();

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        draw.AddImage(help, bg_suv.p0, bg_suv.p1, bg_tuv.p0, bg_tuv.p1);

        draw.AddImage(meswin, title_suv.p0, title_suv.p1, title_tuv.p0, title_tuv.p1);

        string text_key = mode switch {
            UiMode.SET_SWAP => "help.set",

            UiMode.SAVE_LIST  or
            UiMode.SAVE_POPUP => save_mode switch {
                FhSaveSystemMode.SAVE => "help.save",
                FhSaveSystemMode.LOAD or
                FhSaveSystemMode.ALBD => "help.load",

                //TODO-C#16: Remove this, since FhSaveSystemMode should be a `closed enum`.
                _ => throw new UnreachableException(),
            },

            _ => throw new NotImplementedException(),
        };

        string text = localize(text_key);

        Vector2 text_pos = bg_screen.left + new Vector2(24f, -1f) * aspect_scale;

        FhApi.Gui.draw_text(
            draw,
            text_pos,
            text,
            text_config with { align = Alignment2D.LEFT }
        );
    }

    protected override void ui_savecount() {
        FhSaveSet set = FhApi.Saves.sets_get_active();

        string save_text = set.has_autosave
            ? $"{localize("save_count.separator")}{localize("save_count.saves")}"
            : localize("save_count.saves");

        save_text = string.Format(
            save_text,
            set.has_autosave
                ? set.count - 1
                : set.count
        );

        Vector2 text_pos = new(1722f, 144f);

        float line_height = font_size - 5f;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        Vector2 saves_text_size = FhApi.Gui.draw_text(
            draw,
            text_pos * aspect_scale + aspect_helper.pos,
            save_text,
            text_config with { align = Alignment2D.RIGHT }
        );

        if (set.has_autosave) {
            Vector2 autosave_pos = text_pos * aspect_scale + aspect_helper.pos;
            autosave_pos.X -= saves_text_size.X;

            string autosave_text = localize("save_count.autosave");

            FhApi.Gui.draw_text(
                draw,
                autosave_pos,
                autosave_text,
                text_config with {
                    align = Alignment2D.RIGHT,
                    color = COLOR_AUTOSAVE,
                }
            );
        }

        Vector2 second_line_pos = text_pos with { Y = text_pos.Y - line_height };

        FhApi.Gui.draw_text(
            draw,
            second_line_pos * aspect_scale + aspect_helper.pos,
            set.name,
            text_config with { align = Alignment2D.RIGHT }
        );
    }

    protected override void ui_cursor(Vector2 target_pos, bool overlap = true) {
        if (!_texture_meswin.try_use(out ImTextureRef meswin, out FhTextureMetadata? meswin_meta)) {
            return;
        }

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        UV cursor_tuv = new UV(
            new(157f, 244f),
            new( 90f, 200f)
        ).map_to(meswin_meta.size);

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
        if (!_texture_battle_kuang.try_use(
            out ImTextureRef battle_kuang,
            out FhTextureMetadata? battle_kuang_meta
        )) {
            return;
        }

        UV bg_tuv = new Rect {
            pos  = new(  0f, 757f),
            size = new(432f,  12f),
        }.as_uv(battle_kuang_meta.size, false);

        Rect bg_screen = new Rect {
            pos  = new(660f, 28f),
            size = new(600f, 60f),
        }.scale_to_aspect(aspect_helper);

        UV bg_suv = bg_screen.as_uv();

        if (mouse_hovered(bg_screen) && focus != UiFocus.ACTIVE_SET) {
            FhApi.Audio.play_sound(sound_id_action, ACTION_VOLUME);
            focus = UiFocus.ACTIVE_SET;
        }

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();
        draw.AddImage(battle_kuang, bg_suv.p0, bg_suv.p1, bg_tuv.p0, bg_tuv.p1);

        Vector2 text_pos = bg_screen.center;

        string text = localize("set_list.change_set");

        Vector2 text_size = FhApi.Gui.draw_text(
            draw,
            text_pos,
            text,
            text_config with { align = Alignment2D.CENTER }
        );

        if (focus == UiFocus.ACTIVE_SET) {
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

    protected override void ui_setlist() {
        if (mode != UiMode.SET_SWAP) return;

        int start_idx = scrollable_sets.get_clip_start();
        int end_idx   = scrollable_sets.get_clip_end();

        for (int set_idx = start_idx; set_idx < end_idx; set_idx++) {
            string set_name = set_list[set_idx];

            if (!FhApi.Saves.sets_get(set_name, out FhSaveSet? set))
                continue;

            ui_set(set, set_idx, set_idx - start_idx);
        }
    }

    protected override void ui_set(FhSaveSet set, int global_idx, int local_idx) {
        if (!_texture_summonbg.try_use(
            out ImTextureRef summonbg,
            out FhTextureMetadata? summonbg_meta
        )) {
            return;
        }

        // Texture coordinates
        int   tex_idx    = global_idx % 8;
        float tex_y_diff = 78f * tex_idx;

        UV button_tuv = new Rect {
            pos  = new(   0f, 954f - tex_y_diff),
            size = new(1024f,  70f),
        }.as_uv(summonbg_meta.size);

        // Screen coordinates
        Rect button = new() {
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
            if (focus != UiFocus.LIST || scrollable_sets.hovered != global_idx) {
                FhApi.Audio.play_sound(sound_id_action, ACTION_VOLUME);
            }

            focus = UiFocus.LIST;
            scrollable_sets.hovered = global_idx;
        }

        bool hovering = scrollable_sets.hovered == global_idx;

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
            text_config with { align = Alignment2D.LEFT }
        );

        string save_text = set.has_autosave
            ? localize("save_count.separator") + localize("save_count.saves")
            : localize("save_count.saves");

        save_text = string.Format(
            save_text,
            set.has_autosave
                ? set.count - 1
                : set.count
        );

        Vector2 save_count_size = FhApi.Gui.draw_text(
            draw,
            save_count_pos,
            save_text,
            text_config with { align = Alignment2D.RIGHT }
        );

        if (set.has_autosave) {
            Vector2 autosave_pos = save_count_pos;
            autosave_pos.X -= save_count_size.X;

            string autosave_text = localize("save_count.autosave");

            FhApi.Gui.draw_text(
                draw,
                autosave_pos,
                autosave_text,
                text_config with {
                    align = Alignment2D.RIGHT,
                    color = COLOR_AUTOSAVE,
                }
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

    protected override void ui_savelist() {
        FhSaveSet set = FhApi.Saves.sets_get_active();

        scrollable_saves.max = is_saving
            ? set.count + 1 - (set.has_autosave ? 1 : 0) // Add one for New Save Data button
            : set.count;

        if (!is_saving && set.count == 0) {
            ui_no_saves();
            return;
        }

        int start = scrollable_saves.get_clip_start();
        int end   = scrollable_saves.get_clip_end();

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

        if (!FhApi.Saves.impl_get_system_mode(out FhSaveSystemMode? save_mode)) {
            return;
        }

        string message_key = save_mode switch {
            FhSaveSystemMode.ALBD => "popup.no_save_data",
            FhSaveSystemMode.LOAD => "popup.no_save_data.menu",

            // When saving, we should always display "New Save Data" instead.
            _ => throw new UnreachableException(),
        };

        string message = localize(message_key);

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
            text_config with { align = Alignment2D.CENTER }
        );
    }

    protected override void ui_save(FhSaveUiData save, int global_idx, int local_idx) {
        if (!_texture_meswin.try_use(out ImTextureRef meswin, out _)) {
            return;
        }

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        Rect save_outer_bounds = new() {
            pos  = new( 160f, 176f), // Topmost position
            size = new(1561f, 152f),
        };

        float save_gap = 13f;

        save_outer_bounds.pos.Y += (save_outer_bounds.size.Y + save_gap) * local_idx;

        // We set the hovered state early to potentially use it later.
        if (mouse_hovered(save_outer_bounds.scale_to_aspect(aspect_helper))) {
            if (focus != UiFocus.LIST || scrollable_saves.hovered != global_idx) {
                FhApi.Audio.play_sound(sound_id_action, ACTION_VOLUME);
            }

            focus = UiFocus.LIST;
            scrollable_saves.hovered = global_idx;
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
        Vector2 map_icon_size = new() {
            X = save_bounds.size.Y * 1.77083f,
            Y = save_bounds.size.Y,
        };

        Rect map_bounds = new() {
            pos = save_bounds.top_right with {
                X = save_bounds.top_right.X - map_icon_size.X,
            },
            size = map_icon_size,
        };

        Rect info_bounds = new() {
            pos  = save_bounds.pos with {
                Y = header_bounds.bottom.Y,
            },

            size = save_bounds.size with {
                X = save_bounds.size.X   - map_bounds.size.X,
                Y = save_bounds.bottom.Y - header_bounds.bottom.Y,
            },
        };

        if (save.slot == 0 && is_saving)
            ui_newdata(info_bounds, map_bounds);
        else
            ui_save_details(save, save_bounds, header_bounds, info_bounds, map_bounds);

        /* ===== Cursor ===== */
        if (focus == UiFocus.LIST && scrollable_saves.hovered == global_idx) {
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

    protected override void ui_save_details(
        FhSaveUiData save,
        Rect         save_bounds,
        Rect         header_bounds,
        Rect         info_bounds,
        Rect         map_bounds
    ) {
        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        /* ===== Party Icons ===== */
        float   face_height = info_bounds.size.Y;
        float   face_gap    = FhUtil.select(6f, 3f, 25f);

        Vector2 face_size = new() {
            X = face_height * FhUtil.select(1f, 1.0625f, 1.0625f),
            Y = face_height,
        };

        float faces_width = FhUtil.select(
            face_size.X * 3f + face_gap * 2f,
            face_size.X * 3f + face_gap * 2f,
            face_size.X      + face_gap * 3f
        );

        Rect faces_bounds = new() {
            pos  = info_bounds.top_left,
            size = new(faces_width, face_size.Y),
        };

        switch (FhGlobal.game_id) {
            case FhGameId.FFX:    ui_save_faces_x (save, faces_bounds, face_size, face_gap); break;
            case FhGameId.FFX2:   ui_save_faces_x2(save, faces_bounds, face_size, face_gap); break;
            case FhGameId.FFX2LM: ui_save_faces_lm(save, faces_bounds, face_size, face_gap); break;

            default: throw new UnreachableException();
        }

        /* ===== Location Icon ===== */
        FhTexture tex_map = save.icon_map;

        if (tex_map.try_use(out ImTextureRef map_icon, out _)) {
            UV tuv = new(Vector2.Zero, Vector2.One);
            UV suv = map_bounds.scale_to_aspect(aspect_helper).as_uv();

            draw.AddImage(map_icon, suv.p0, suv.p1, tuv.p0, tuv.p1);
        }

        /* ===== Header Details ===== */
        float text_margin_left    = 12f;
        float text_margin_between = 48f;

        Vector2 header_text_pos_left = header_bounds.left;
        header_text_pos_left.X += text_margin_left;

        string slot_text   = save.slot == 0 ? localize("details.autosave") : save.slot_str;
        string create_time = save.create_time.ToString(@"yyyy\/M\/d H\:mm\:ss");

        Vector2 text_size = FhApi.Gui.draw_text(
            draw,
            header_text_pos_left * aspect_scale + aspect_helper.pos,
            slot_text,
            text_config with {
                align = Alignment2D.LEFT,
                color = save.slot == 0 ? COLOR_AUTOSAVE : COLOR_WHITE,
            }
        );

        int location_id = FhGlobal.game_id switch {
            FhGameId.FFX    => save.header.x .id_location,
            FhGameId.FFX2   => save.header.x2.id_location,
            FhGameId.FFX2LM => save.header.x2.lm_id_location,

            _ => throw new UnreachableException(),
        };

        // Don't render name for locations with missing names
        if (location_id != 0) {
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
                text_config with { align = Alignment2D.LEFT }
            );
        }

        Vector2 header_text_pos_right = header_bounds.right;
        header_text_pos_right.X -= map_bounds.size.X + text_margin_between;

        FhApi.Gui.draw_text(
            draw,
            header_text_pos_right * aspect_scale + aspect_helper.pos,
            create_time,
            text_config with { align = Alignment2D.RIGHT }
        );

        /* ===== Info Details ===== */
        float margin_face = 38f;
        float margin_map  = 24f;

        float info_margin_left  = faces_bounds.size.X + margin_face;
        float info_margin_right = margin_map;

        info_bounds.pos.X  += info_margin_left;
        info_bounds.size.X -= info_margin_left + info_margin_right;

        switch (FhGlobal.game_id) {
            case FhGameId.FFX:    ui_save_info_x (save, info_bounds); break;
            case FhGameId.FFX2:   ui_save_info_x2(save, info_bounds); break;
            case FhGameId.FFX2LM: ui_save_info_lm(save, info_bounds); break;

            default: throw new UnreachableException();
        }
    }

    protected override void ui_save_faces_x(
        FhSaveUiData save,
        Rect         bounds,
        Vector2      face_size,
        float        face_gap
    ) {
        if (!_texture_faces_ply   .try_use(out ImTextureRef faces_ply, out FhTextureMetadata? faces_ply_meta)
            || !_texture_faces_smn.try_use(out ImTextureRef faces_smn, out FhTextureMetadata? faces_smn_meta)
        ) {
            return;
        }

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        Rect face_bounds = new() {
            pos = bounds.top_left,
            size = face_size,
        };

        UV suv = face_bounds.scale_to_aspect(aspect_helper).as_uv();

        Vector2 face_screen_offset = new Vector2(face_size.X + face_gap, 0f) * aspect_scale;

        SaveHeaderX header = save.header.x;

        foreach (int chr in header.formation[..3]) {
            if (chr == 0xFF) continue;

            int id = chr;

            if (id <= 0x7) {
                ReadOnlySpan<int> ply_face_tex_xs = [ 1, 201, 400, 600 ];

                float x1 = ply_face_tex_xs[id % 4];
                float x2 = x1 + 198f;

                float y1 = id > 3 ? 311f : 511f;
                float y2 = y1 - 198f;

                UV tuv = new UV(
                    new(x1, y1),
                    new(x2, y2)
                ).map_to(faces_ply_meta.size);

                draw.AddImage(faces_ply, suv.p0, suv.p1, tuv.p0, tuv.p1);
            }
            else {
                id -= FFX.Ids.PlySaveId.PC_VALEFOR;

                float x1 = 1f + 200f * (id % 5);
                float x2 = x1 + 198f;

                float y1 = id > 5 ? 311f : 511f;
                float y2 = y1 - 198f;

                UV tuv = new UV(
                    new(x1, y1),
                    new(x2, y2)
                ).map_to(faces_smn_meta.size);

                draw.AddImage(faces_smn, suv.p0, suv.p1, tuv.p0, tuv.p1);
            }

            suv = suv.move(face_screen_offset);
        }
    }

    protected override void ui_save_faces_x2(
        FhSaveUiData save,
        Rect         bounds,
        Vector2      face_size,
        float        face_gap
    ) {
        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        UV face_tuv = new Rect {
            pos  = new(  0f,   0f),
            size = new(256f, 239f),
        }.as_uv(_tex_ffx2_face_size);

        Rect face_bounds = new() {
            pos = bounds.bottom_left with {
                Y = bounds.bottom_left.Y - face_size.Y,
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
    }

    protected override void ui_save_faces_lm(
        FhSaveUiData save,
        Rect         bounds,
        Vector2      face_size,
        float        face_gap
    ) {
        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        UV face_tuv = new Rect {
            pos  = new(  0f,   0f),
            size = new(256f, 239f),
        }.as_uv(_tex_ffx2_face_size);

        Rect face = new() {
            pos = bounds.bottom_left with {
                X = bounds.bottom_left.X + face_gap * 3,
                Y = bounds.bottom_left.Y - face_size.Y,
            },
            size = face_size,
        };

        UV face_suv = face.scale_to_aspect(aspect_helper).as_uv();

        Vector2 face_screen_offset = new Vector2(-face_gap, 0f) * aspect_scale;

        SaveHeaderX2 header = save.header.x2;

        byte job_id = header.lm_job;

        if (job_id > 0x21) return;

        FhTexture portrait = save.icon_chr[0];

        if (!portrait.try_use(out ImTextureRef faces, out _)) {
            return;
        }

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

    protected override void ui_save_info_x(FhSaveUiData save, Rect bounds)  {
        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        float top_y    = float.Lerp(bounds.top.Y, bounds.bottom.Y, 0.25f);
        float bottom_y = float.Lerp(bounds.top.Y, bounds.bottom.Y, 0.7f);

        Vector2 pos_name = new() {
            X = bounds.left.X,
            Y = top_y,
        };

        Vector2 pos_playtime = new() {
            X = bounds.left.X,
            Y = bottom_y,
        };

        FhApi.Gui.draw_text(
            draw,
            pos_name * aspect_scale + aspect_helper.pos,
            save.player_name,
            text_config with { align = Alignment2D.LEFT }
        );

        FhApi.Gui.draw_text(
            draw,
            pos_playtime * aspect_scale + aspect_helper.pos,
            save.play_time,
            text_config with { align = Alignment2D.LEFT }
        );
    }

    protected override void ui_save_info_x2(FhSaveUiData save, Rect bounds)  {
        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        float top_y    = float.Lerp(bounds.top.Y, bounds.bottom.Y, 0.25f);
        float bottom_y = float.Lerp(bounds.top.Y, bounds.bottom.Y, 0.7f);

        Vector2 pos_name = new() {
            X = bounds.left.X,
            Y = top_y,
        };

        Vector2 pos_chapter = new() {
            X = bounds.left.X,
            Y = bottom_y,
        };

        Vector2 pos_completion = new() {
            X = bounds.right.X,
            Y = top_y,
        };

        Vector2 pos_playtime = new() {
            X = bounds.right.X,
            Y = bottom_y,
        };

        // When running in CJK, we hardcode the "STORY COMPLETED" string.
        string completion = FhGlobal.lang_id.is_cjk
            ? $"STORY COMPLETED: {save.header.x2.completion}%"
            : save.completion;

        FhApi.Gui.draw_text(
            draw,
            pos_name * aspect_scale + aspect_helper.pos,
            save.player_name,
            text_config with { align = Alignment2D.LEFT }
        );

        FhApi.Gui.draw_text(
            draw,
            pos_chapter * aspect_scale + aspect_helper.pos,
            save.chapter,
            text_config with { align = Alignment2D.LEFT }
        );

        FhApi.Gui.draw_text(
            draw,
            pos_completion * aspect_scale + aspect_helper.pos,
            completion,
            text_config with { align = Alignment2D.RIGHT }
        );

        FhApi.Gui.draw_text(
            draw,
            pos_playtime * aspect_scale + aspect_helper.pos,
            save.play_time,
            text_config with { align = Alignment2D.RIGHT }
        );
    }

    protected override void ui_save_info_lm(FhSaveUiData save, Rect bounds)  {
        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        float margin_between = 48f * aspect_scale.X;

        float top_y    = float.Lerp(bounds.top.Y, bounds.bottom.Y, 0.25f);
        float bottom_y = float.Lerp(bounds.top.Y, bounds.bottom.Y, 0.7f);

        Vector2 pos_name = new() {
            X = bounds.left.X,
            Y = top_y,
        };

        Vector2 pos_job = new() {
            X = bounds.left.X,
            Y = bottom_y,
        };

        Vector2 pos_playtime = new() {
            X = bounds.right.X,
            Y = bottom_y,
        };

        FhApi.Gui.draw_text(
            draw,
            pos_name * aspect_scale + aspect_helper.pos,
            save.player_name,
            text_config with { align = Alignment2D.LEFT }
        );

        pos_job = pos_job * aspect_scale + aspect_helper.pos;

        Vector2 job_text_size = FhApi.Gui.draw_text(
            draw,
            pos_job,
            save.lm_job,
            text_config with { align = Alignment2D.LEFT }
        );

        pos_job.X += job_text_size.X + margin_between;

        FhApi.Gui.draw_text(
            draw,
            pos_job,
            save.lm_level,
            text_config with { align = Alignment2D.LEFT }
        );

        FhApi.Gui.draw_text(
            draw,
            pos_playtime * aspect_scale + aspect_helper.pos,
            save.play_time,
            text_config with { align = Alignment2D.RIGHT }
        );
    }

    protected override void ui_newdata(Rect text_bounds, Rect icon_bounds) {
        if (!_texture_freetex.try_use(out ImTextureRef freetex, out FhTextureMetadata? freetex_meta)) {
            return;
        }

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        /* ===== New Save Data Text ===== */
        string text = localize("details.new_save_data");

        float margin = 14f;

        Vector2 text_pos = text_bounds.left;
        text_pos.X += margin;

        FhApi.Gui.draw_text(
            draw,
            text_pos * aspect_scale + aspect_helper.pos,
            text,
            text_config with { align = Alignment2D.LEFT }
        );

        /* ===== "+" Icon ===== */
        GradientStep[] icon_steps = [
            new(0.0f, 0xFF252525),
            new(1.0f, 0xFF191919),
        ];

        FhApi.Gui.draw_rectangle_gradient(
            draw,
            icon_bounds.scale_to_aspect(aspect_helper),
            GradientDirection.DOWN,
            icon_steps
        );

        UV plus_tuv = new Rect {
            pos  = new(581f, 724f),
            size = new( 36f,  36f),
        }.as_uv(freetex_meta.size);

        UV plus_suv = new Rect {
            pos = icon_bounds.center,
        }
            .expand(new(45f, 47f), Alignment2D.CENTER)
            .scale_to_aspect(aspect_helper)
            .as_uv();

        draw.AddImage(
            freetex,
            plus_suv.p0,
            plus_suv.p1,
            plus_tuv.p0,
            plus_tuv.p1
        );
    }

    protected override void ui_scrollbar() {
        Rect track_bounds = new() {
            pos  = new(1753f, 209f),
            size = new(  17f, 756f),
        };

        float triangle_gap = 15f;

        ui_scrollbar(
            current_scrollable,
            track_bounds,
            triangle_gap
        );
    }
}
