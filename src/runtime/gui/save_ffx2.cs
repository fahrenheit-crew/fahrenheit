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
[FhLoad(FhGameId.FFX | FhGameId.FFX2 | FhGameId.FFX2LM)]
public sealed class FhSaveUiX2 : FhSaveUiBase {
    private readonly FhTexture _texture_menuback = new(Path.Join(DIR_X2_MENU,    "menuback.dds.phyre"),             FhTextureType.GAME_PHYRE_2D);
    private readonly FhTexture _texture_black0   = new(Path.Join(DIR_X2_PLATE,   "black0.dds.phyre"),               FhTextureType.GAME_PHYRE_2D);
    private readonly FhTexture _texture_mahojin  = new(Path.Join(DIR_X2_MAHOJIN, "14336_19_0_0_512_512.dds.phyre"), FhTextureType.GAME_PHYRE_2D);
    private readonly FhTexture _texture_plate    = new(Path.Join(DIR_X2_PLATE,   "12288_19_0_0_256_256.dds.phyre"), FhTextureType.GAME_PHYRE_2D);
    private readonly FhTexture _texture_freetex  = new(Path.Join(DIR_X2_MENU,    "freetex.dds.phyre"),              FhTextureType.GAME_PHYRE_2D);
    private readonly FhTexture _texture_x2_bg    = new(Path.Join(DIR_X2_MENU,    "x2_bg.dds.phyre"),                FhTextureType.GAME_PHYRE_2D);
    private readonly FhTexture _texture_b_menu1  = new(Path.Join(DIR_X2_MENU,    "b_menu1.dds.phyre"),              FhTextureType.GAME_PHYRE_2D);

    private readonly Vector2 _tex_face_size = new(256f, 256f);

    private readonly FhTexture _texture_ffx_faces_ply = new(Path.Join(DIR_X_MENU, "face_ply.dds.phyre"), FhTextureType.GAME_PHYRE_2D);
    private readonly FhTexture _texture_ffx_faces_smn = new(Path.Join(DIR_X_MENU, "face_smn.dds.phyre"), FhTextureType.GAME_PHYRE_2D);

    protected override FhTexture[] textures => [
        _texture_menuback,
        _texture_black0,
        _texture_mahojin,
        _texture_plate,
        _texture_freetex,
        _texture_x2_bg,
        _texture_b_menu1,
        _texture_ffx_faces_ply,
        _texture_ffx_faces_smn,
    ];

    protected override void ui_background() {
        if (!_texture_menuback  .try_use(out ImTextureRef menuback, out FhTextureMetadata? menuback_meta)
            || !_texture_mahojin.try_use(out ImTextureRef mahojin,  out FhTextureMetadata? mahojin_meta)
            || !_texture_black0 .try_use(out ImTextureRef black0,   out FhTextureMetadata? black0_meta)
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
        }.as_uv(menuback_meta.size);

        // Draw the background in 6 left->right chunks, top row, bottom row
        Rect menuback_screen = new() {
            pos  = new(  0f,   0f),
            size = new(948f, 947f),
        };

        ReadOnlySpan<Rect> menuback_rects = [
            menuback_screen with { pos = new(-1f, menuback_screen.pos.Y) },
            menuback_screen with { pos = new(menuback_screen.size.X - 1f, menuback_screen.pos.Y) },
            menuback_screen with { pos = new((menuback_screen.size.X - 1f) * 2f, menuback_screen.pos.Y) },
            menuback_screen with { pos = new(-1f, menuback_screen.size.Y) },
            menuback_screen with { pos = new(menuback_screen.size.X - 1f, menuback_screen.size.Y) },
            menuback_screen with { pos = new((menuback_screen.size.X - 1f) * 2f, menuback_screen.size.Y) },
        ];

        foreach (Rect rect in menuback_rects) {
            UV suv = rect.scale_to_aspect(aspect_helper).as_uv();
            draw.AddImage(menuback, suv.p0, suv.p1, menuback_tuv.p0, menuback_tuv.p1);
        }

        UV mahojin_tuv = new Rect {
            pos  = new(   6f,  529f),
            size = new(1508f, 1509f),
        }.as_uv(mahojin_meta.size);

        UV mahojin_suv = new Rect {
            pos  = new( 374f,  -57f),
            size = new(1225f, 1224f),
        }.scale_to_aspect(aspect_helper).as_uv();

        // Background glyph
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
        }.as_uv(black0_meta.size);

        draw.AddImage(
            black0,
            screen_uv.p0,
            screen_uv.p1,
            black0_tuv.p0,
            black0_tuv.p1
        );

        draw.AddImage(
            black0,
            screen_uv.p0,
            screen_uv.p1,
            black0_tuv.p1,
            black0_tuv.p0
        );

        draw.PopClipRect();
    }

    protected override void ui_help() {
        if (!_texture_freetex.try_use(out ImTextureRef freetex, out FhTextureMetadata? freetex_meta)) {
            return;
        }

        if (!FhApi.Saves.impl_get_system_mode(out FhSaveSystemMode? save_mode)) {
            return;
        }

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        uint bg_grad_l = 0xFF000000; // black
        uint bg_grad_r = 0x00000000; // transparent black

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
        uint accent_grad_r = 0x0000BFB5; // transparent yellow

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
        }.as_uv(freetex_meta.size);

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

        string text_key = mode switch {
            UiMode.SET_CHANGE => "help.set",

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

        Vector2 text_pos = bg_screen.left;
        text_pos.X += 241f * aspect_scale.X;
        text_pos.Y -=   7f * aspect_scale.Y;

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

        Vector2 text_pos = new(1717f, 146f);

        float line_height = FONT_SIZE_SMALL - 3f;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        FhGui.DrawTextConfig config = text_config with {
            align     = Alignment2D.RIGHT,
            font_size = font_size_small,
        };

        Vector2 saves_text_size = FhApi.Gui.draw_text(
            draw,
            text_pos * aspect_scale + aspect_helper.pos,
            save_text,
            config
        );

        if (set.has_autosave) {
            Vector2 autosave_pos = text_pos * aspect_scale + aspect_helper.pos;
            autosave_pos.X -= saves_text_size.X;

            string autosave_text = localize("save_count.autosave");

            FhApi.Gui.draw_text(
                draw,
                autosave_pos,
                autosave_text,
                config with { color = COLOR_AUTOSAVE }
            );
        }

        Vector2 second_line_pos = text_pos with { Y = text_pos.Y - line_height };

        FhApi.Gui.draw_text(
            draw,
            second_line_pos * aspect_scale + aspect_helper.pos,
            set.name,
            config
        );
    }

    protected override void ui_cursor(Vector2 target_pos, bool overlap = true) {
        if (!_texture_freetex.try_use(out ImTextureRef freetex, out FhTextureMetadata? freetex_meta)) return;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        UV cursor_tuv = new UV(
            new(  9f, 572f),
            new(180f, 478f)
        ).map_to(freetex_meta.size);

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

    protected override void ui_change_set() {
        if (!_texture_x2_bg.try_use(out ImTextureRef x2_bg, out FhTextureMetadata? x2_bg_meta)) return;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        Rect bg_screen = new Rect {
            pos  = new(735f, 28f),
            size = new(450f, 60f),
        }.scale_to_aspect(aspect_helper);

        UV bg_suv = bg_screen.as_uv();

        if (mouse_hovered(bg_screen) && focus != UiFocus.ACTIVE_SET) {
            FhApi.Audio.play_sound(SOUND_ID_ACTION, ACTION_VOLUME);
            focus = UiFocus.ACTIVE_SET;
        }

        draw.AddRectFilled(
            bg_suv.p0,
            bg_suv.p1,
            0xA5000000
        );

        UV accent_tuv = new Rect {
            pos  = new(1469f, 733f),
            size = new( 286f, 287f),
        }.as_uv(x2_bg_meta.size);

        Vector2 accent_size = 60f * aspect_scale;

        Rect accent_left = new() {
            pos  = bg_screen.bottom_left + new Vector2(-2f, -58f) * aspect_scale,
            size = accent_size,
        };

        Rect accent_right = new() {
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

        Vector2 text_pos  =  bg_screen.center;
        text_pos.Y        -= 2f * aspect_scale.Y;

        string text = localize("set_list.change_set");

        Vector2 text_size = FhApi.Gui.draw_text(
            draw,
            text_pos,
            text,
            text_config with { align = Alignment2D.CENTER }
        );

        if (focus == UiFocus.ACTIVE_SET) {
            float text_left   = text_pos.X - text_size.X / 2f;
            float text_margin = 33f * aspect_scale.X;

            Vector2 cursor_target = bg_screen.center with {
                X = text_left - text_margin,
            };

            ui_cursor(cursor_target, false);
        }

        // Input handling
        if (mouse_clicked(bg_screen)) {
            FhApi.Audio.play_sound(SOUND_ID_SET_CHANGE);
            change_mode(UiMode.SET_CHANGE);
        }
    }

    protected override void ui_setlist() {
        if (mode != UiMode.SET_CHANGE) return;

        if (!_texture_plate.try_use(out ImTextureRef plate, out FhTextureMetadata? plate_meta)) return;

        // Shadows behind slots
        Vector2 shadow_offset = 20f * aspect_scale;

        Rect header_bounds = new Rect {
            pos  = new( 400f, 199f),
            size = new(1120f,  32f),
        };

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        float border_width = 5f;
        uint  highlight    = 0x28FFFFFF;
        uint  shadow       = 0x80000000;

        FhApi.Gui.X2.draw_plate(
            draw,
            plate,
            header_bounds.scale_to_aspect(aspect_helper),
            2,
            0xFFAEBEC3,
            border_width * aspect_scale.Y,
            highlight,
            shadow,
            shadow_offset,
            true
        );

        Vector2 text_pos = header_bounds.left;
        text_pos.X += 30f;
        text_pos.Y -= 2f;
        text_pos = text_pos * aspect_scale + aspect_helper.pos;

        string text = localize("set_list.list_title");

        FhGui.DrawTextConfig config = text_config with {
            align     = Alignment2D.LEFT,
            font_size = font_size_tiny,
            color = 0xFFF7D19C,
        };

        FhApi.Gui.draw_text(
            draw,
            text_pos,
            text,
            config
        );

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
        if (!_texture_plate.try_use(out ImTextureRef plate, out FhTextureMetadata? plate_meta)) return;

        int start_idx = scrollable_sets.get_clip_start();

        // Screen coordinates
        Rect plate_bounds = new() {
            pos  = new( 400f, 232f),
            size = new(1120f,  64f),
        };

        plate_bounds.pos.Y += (plate_bounds.size.Y + 1f) * local_idx;

        Vector2 name_pos = plate_bounds.left;
        name_pos.X += 30f;
        name_pos.Y -= 1f;
        name_pos = name_pos * aspect_scale + aspect_helper.pos;

        Vector2 save_count_pos = plate_bounds.right;
        save_count_pos.X -= 40f;
        save_count_pos.Y -= 1f;
        save_count_pos = save_count_pos * aspect_scale + aspect_helper.pos;

        plate_bounds = plate_bounds.scale_to_aspect(aspect_helper);

        // Drawing time
        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();
        ImGuiIOPtr    io   = ImGui.GetIO();

        if (mouse_hovered(plate_bounds)) {
            if (focus != UiFocus.LIST || scrollable_sets.hovered != global_idx) {
                FhApi.Audio.play_sound(SOUND_ID_ACTION, ACTION_VOLUME);
            }

            focus = UiFocus.LIST;
            scrollable_sets.hovered = global_idx;
        }

        bool hovering = scrollable_sets.hovered == global_idx;

        // Shadows behind slots
        Vector2 shadow_offset = 20f * aspect_scale;

        float border_width = 6f;
        uint  highlight    = 0x28FFFFFF;
        uint  shadow       = 0x80000000;

        FhApi.Gui.X2.draw_plate(
            draw,
            plate,
            plate_bounds,
            global_idx,
            0xFFE4F0F1,
            border_width * aspect_scale.Y,
            highlight,
            shadow,
            shadow_offset
        );

        // TODO: Highlight for active set
        /*if (set.name == FhApi.Saves.sets_get_active().name) {

        }*/

        FhApi.Gui.draw_text(
            draw,
            name_pos,
            set.name,
            text_config with { align = Alignment2D.LEFT }
        );

        string save_text = set.has_autosave
            ? $"{localize("save_count.separator")}{localize("save_count.saves")}"
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
            ui_cursor(plate_bounds.left);
        }

        // Handle input

        if (mouse_clicked(plate_bounds)) {
            io.WantCaptureMouse = true;
            FhApi.Audio.play_sound(SOUND_ID_SET_CHANGE);
            change_set(set.name);
        }
    }

    protected override void ui_no_saves() {
        if (!_texture_x2_bg.try_use(out ImTextureRef x2_bg, out FhTextureMetadata? x2_bg_meta)) {
            return;
        }

        string message = localize("popup.no_save_data.menu");

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        ImGui.PushFont(null, font_size);
        Vector2 text_size = ImGui.CalcTextSize(message);
        ImGui.PopFont();

        Vector2 text_margin = new Vector2(60f, 40f) * aspect_scale;
        Vector2 window_size = text_margin * 2 + text_size;

        Rect window = new Rect {
            pos  = FhApi.Gui.display_size / 2f,
            size = Vector2.Zero,
        }.expand(window_size, Alignment2D.CENTER);

        draw.AddRectFilled(
            window.top_left,
            window.bottom_right,
            0xA5000000
        );

        UV accent_tuv = new Rect {
            pos  = new(1469f, 733f),
            size = new( 286f, 287f),
        }.as_uv(x2_bg_meta.size);

        Vector2 accent_size = 125f * aspect_scale;

        Rect accent_left = new() {
            pos  = window.bottom_left + new Vector2(-4f, -121f) * aspect_scale,
            size = accent_size,
        };

        Rect accent_right = new() {
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
            text_config with { align = Alignment2D.CENTER }
        );
    }

    protected override void ui_save(FhSaveUiData save, int global_idx, int local_idx) {
        if (!_texture_plate.try_use(out ImTextureRef plate, out FhTextureMetadata? plate_meta)) {
            return;
        }

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        Rect save_outer_bounds = new() {
            pos  = new( 157f, 172f), // Topmost position
            size = new(1565f, 155f),
        };

        float save_gap = 10f;

        save_outer_bounds.pos.Y += (save_outer_bounds.size.Y + save_gap) * local_idx;

        // We set the hovered state early to potentially use it later.
        if (mouse_hovered(save_outer_bounds.scale_to_aspect(aspect_helper))) {
            if (focus != UiFocus.LIST || scrollable_saves.hovered != global_idx) {
                FhApi.Audio.play_sound(SOUND_ID_ACTION, ACTION_VOLUME);
            }

            focus = UiFocus.LIST;
            scrollable_saves.hovered = global_idx;
        }

        /* ===== Plate Texture ===== */
        float header_size    = 47f;
        float vertical_space =  4f;

        Rect header_outer_bounds = new() {
            pos  = save_outer_bounds.pos,
            size = save_outer_bounds.size with {
                Y = header_size,
            },
        };

        Rect info_outer_bounds = new() {
            pos = new() {
                X = save_outer_bounds.pos.X,
                Y = header_outer_bounds.bottom.Y + vertical_space,
            },
            size = new() {
                X = save_outer_bounds.size.X,
                Y = save_outer_bounds.size.Y - header_size - vertical_space,
            },
        };

        // Texture UV changes for each slot
        float slice_size = 64f;
        float max_y = 512f - (((global_idx + 1) % 8) * slice_size);

        float uv_y_start = max_y;
        float uv_y_end   = max_y - slice_size;
        float uv_y_split = uv_y_start + (uv_y_end - uv_y_start) * (47f / 151f);

        UV header_tuv = new Rect {
            pos  = new(  0f,              uv_y_split),
            size = new(512f, uv_y_start - uv_y_split),
        }.as_uv(plate_meta.size);

        UV info_tuv = new Rect {
            pos  = new(  0f,              uv_y_end),
            size = new(512f, uv_y_split - uv_y_end),
        }.as_uv(plate_meta.size);

        UV header_suv = header_outer_bounds.scale_to_aspect(aspect_helper).as_uv();
        UV info_suv   = info_outer_bounds  .scale_to_aspect(aspect_helper).as_uv();

        // Shadows behind saves
        Vector2 shadow_offset = 10f * aspect_scale;

        draw.AddRectFilled(
            header_suv.p0 + shadow_offset,
            header_suv.p1 + shadow_offset,
            0xA5000000
        );

        draw.AddRectFilled(
            info_suv.p0 + shadow_offset,
            info_suv.p1 + shadow_offset,
            0xA5000000
        );

        // Draw Autosave darker
        bool is_autosave = save.slot == 0 && !is_saving;

        uint header_clr   = is_autosave ? 0xFFB6A19F  : 0xFFBECED3;
        uint info_clr     = is_autosave ? 0xFFC6B1AF  : 0xFFD3EEF2;
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

        // Border highlights/shadows
        float border_width = 5f;
        uint  highlight    = 0x28FFFFFF;
        uint  shadow       = 0x80000000;

        FhApi.Gui.X2.draw_plate_edges(
            draw,
            header_outer_bounds.scale_to_aspect(aspect_helper),
            border_width * aspect_scale.Y,
            highlight,
            shadow
        );

        FhApi.Gui.X2.draw_plate_edges(
            draw,
            info_outer_bounds.scale_to_aspect(aspect_helper),
            border_width * aspect_scale.Y,
            highlight,
            shadow
        );

        draw.AddRectFilled(
            header_suv.p0,
            header_suv.p1,
            header_shade
        );

        // Details
        Vector2 shrink_size = new(border_width * 2f);

        Rect save_bounds   = save_outer_bounds  .expand(-shrink_size, Alignment2D.CENTER);
        Rect header_bounds = header_outer_bounds.expand(-shrink_size, Alignment2D.CENTER);
        Rect info_bounds   = info_outer_bounds  .expand(-shrink_size, Alignment2D.CENTER);

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

        info_bounds.size.X -= map_bounds.size.X;

        if (save.slot == 0 && is_saving)
            ui_newdata(info_bounds, map_bounds);
        else
            ui_save_details(save, save_bounds, header_bounds, info_bounds, map_bounds);

        /* ===== Cursor ===== */
        if (focus == UiFocus.LIST && scrollable_saves.hovered == global_idx) {
            ui_cursor(info_outer_bounds.scale_to_aspect(aspect_helper).left);
        }

        // Handle input
        if (mouse_clicked(save_outer_bounds.scale_to_aspect(aspect_helper))) {
            FhApi.Audio.play_sound(SOUND_ID_ACTION, ACTION_VOLUME);
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
        float   face_height = 96f;
        float   face_gap    = FhUtil.select(3f, 3f, 25f);

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
            pos = info_bounds.bottom_left with {
                Y = info_bounds.bottom_left.Y - face_size.Y,
            },
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
        float text_offset      = 13f;
        float text_margin      = 66f;
        float autosave_offset  = 10f;

        Vector2 header_text_pos_l = header_bounds.left + new Vector2 {
            X = text_offset,
        };

        string slot_text   = save.slot == 0 ? localize("details.autosave") : save.slot_str;
        string create_time = save.create_time.ToString(@"yyyy\/M\/d H\:mm\:ss");

        Vector2 text_size = FhApi.Gui.draw_text(
            draw,
            header_text_pos_l * aspect_scale + aspect_helper.pos,
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
            header_text_pos_l.X -= 1f;
            header_text_pos_l.Y -= 2f;
            header_text_pos_l = header_text_pos_l * aspect_scale + aspect_helper.pos;
            header_text_pos_l.X += text_size.X + text_margin * aspect_scale.X;

            // If the slot text is "Autosave", increase the margin
            if (save.slot == 0) header_text_pos_l.X += autosave_offset * aspect_scale.X;

            FhApi.Gui.draw_text(
                draw,
                header_text_pos_l,
                save.location,
                text_config with { align = Alignment2D.LEFT }
            );
        }

        text_margin = 59f;

        Vector2 header_text_pos_r = header_bounds.right - new Vector2 {
            X = map_bounds.size.X + text_margin,
        };

        FhApi.Gui.draw_text(
            draw,
            header_text_pos_r * aspect_scale + aspect_helper.pos,
            create_time,
            text_config with { align = Alignment2D.RIGHT }
        );

        /* ===== Info Details ===== */
        float margin_face = FhUtil.select(38f, 133f, 86f);
        float margin_map  = 57f;

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
        if (!_texture_ffx_faces_ply   .try_use(out ImTextureRef faces_ply, out FhTextureMetadata? faces_ply_meta)
            || !_texture_ffx_faces_smn.try_use(out ImTextureRef faces_smn, out FhTextureMetadata? faces_smn_meta)
        ) {
            return;
        }

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        Rect face_bounds = new() {
            pos  = bounds.top_left,
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
        }.as_uv(_tex_face_size);

        Rect face = new() {
            pos = bounds.bottom_left with {
                Y = bounds.bottom_left.Y - face_size.Y,
            },
            size = face_size,
        };

        UV face_suv = face.scale_to_aspect(aspect_helper).as_uv();

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
        }.as_uv(_tex_face_size);

        Rect face = new() {
            pos = bounds.bottom_left with {
                X = bounds.bottom_left.X + (3 * face_gap),
                Y = bounds.bottom_left.Y - face_size.Y,
            },
            size = face_size,
        };

        UV face_suv = face.scale_to_aspect(aspect_helper).as_uv();

        Vector2 face_screen_offset = new Vector2(-face_gap, 0f) * aspect_scale;

        SaveHeaderX2 header = save.header.x2;

        byte job_id = header.lm_job;
        if (job_id <= 0x21 && save.icon_chr[0].try_use(out ImTextureRef faces, out _)) {
            for (int i = 3; i >= 0; i--) {
                uint alpha = ImGui.GetColorU32(
                    Vector4.One with {
                        W = 1 - (i * 0.25f),
                    }
                );

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

    protected override void ui_save_info_x(FhSaveUiData save, Rect bounds) {
        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        float top_y    = float.Lerp(bounds.top.Y, bounds.bottom.Y, 0.25f);
        float bottom_y = float.Lerp(bounds.top.Y, bounds.bottom.Y, 0.75f);

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

    protected override void ui_save_info_x2(FhSaveUiData save, Rect bounds) {
        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        // When running in CJK, we slightly reduce the text margin and hardcode
        // the "STORY COMPLETED" string to shorten it and prevent overlap.
        string completion = FhGlobal.lang_id.is_cjk
            ? $"STORY COMPLETED: {save.header.x2.completion}%"
            : save.completion;

        if (FhGlobal.lang_id.is_cjk) {
            float extra_offset = 92f;

            bounds.pos.X  -= extra_offset;
            bounds.size.X += extra_offset;
        }

        float top_y    = float.Lerp(bounds.top.Y, bounds.bottom.Y, 0.25f);
        float bottom_y = float.Lerp(bounds.top.Y, bounds.bottom.Y, 0.75f);

        Vector2 pos_name = new() {
            X = bounds.left.X,
            Y = top_y,
        };

        Vector2 pos_chapter = new() {
            X = bounds.left.X,
            Y = bottom_y,
        };

        Vector2 pos_completion = new() {
            X = bounds.right.X + 2f,
            Y = top_y,
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

    protected override void ui_save_info_lm(FhSaveUiData save, Rect bounds) {
        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        float top_y    = float.Lerp(bounds.top.Y, bounds.bottom.Y, 0.25f);
        float bottom_y = float.Lerp(bounds.top.Y, bounds.bottom.Y, 0.75f);

        float level_offset = 79f;

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

        Vector2 job_text_size = FhApi.Gui.draw_text(
            draw,
            pos_job * aspect_scale + aspect_helper.pos,
            save.lm_job,
            text_config with { align = Alignment2D.LEFT }
        );

        Vector2 pos_level = pos_job * aspect_scale + aspect_helper.pos;
        pos_level.X += job_text_size.X + level_offset * aspect_scale.X;

        FhApi.Gui.draw_text(
            draw,
            pos_level,
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
        if (!_texture_freetex.try_use(out ImTextureRef freetex, out FhTextureMetadata? freetex_meta)) return;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        /* ===== New Save Data Text ===== */
        string text = localize("details.new_save_data");

        Vector2 text_pos = text_bounds.left;
        text_pos.X += 17f;

        FhApi.Gui.draw_text(
            draw,
            text_pos * aspect_scale + aspect_helper.pos,
            text,
            text_config with { align = Alignment2D.LEFT }
        );

        /* ===== "+" Icon ===== */
        ReadOnlySpan<GradientStep> icon_steps = [
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
            pos  = new(1753f, 205f),
            size = new(  17f, 753f),
        };

        float triangle_gap = 13f;

        ui_scrollbar(
            current_scrollable,
            track_bounds,
            triangle_gap
        );
    }
}
