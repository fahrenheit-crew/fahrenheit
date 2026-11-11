// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime.Gui;

[FhLoad(FhGameId.FFX | FhGameId.FFX2 | FhGameId.FFX2LM)]
public partial class FhSettingsUiX2 : FhSettingsUiBase {

    public const int   INDENT_MAX  = 5;
    public const float INDENT_SIZE = 40f;

    public const float SETTING_MARGIN_SIZE  = 22f;
    public const float CATEGORY_MARGIN_SIZE = 10f;

    private int setting_idx; // Used for plate texture UV

    // Textures
    public readonly FhTexture _texture_menuback = new(Path.Join(DIR_X2_MENU,    "menuback.dds.phyre"),             FhTextureType.GAME_PHYRE_2D);
    public readonly FhTexture _texture_black0   = new(Path.Join(DIR_X2_PLATE,   "black0.dds.phyre"),               FhTextureType.GAME_PHYRE_2D);
    public readonly FhTexture _texture_mahojin  = new(Path.Join(DIR_X2_MAHOJIN, "14336_19_0_0_512_512.dds.phyre"), FhTextureType.GAME_PHYRE_2D);
    public readonly FhTexture _texture_bk_blue  = new(Path.Join(DIR_X2_MENU,    "bk_blue.dds.phyre"),              FhTextureType.GAME_PHYRE_2D);
    public readonly FhTexture _texture_freetex  = new(Path.Join(DIR_X2_MENU,    "freetex.dds.phyre"),              FhTextureType.GAME_PHYRE_2D);
    public readonly FhTexture _texture_plate    = new(Path.Join(DIR_X2_PLATE,   "12288_19_0_0_256_256.dds.phyre"), FhTextureType.GAME_PHYRE_2D);
    public readonly FhTexture _texture_window   = new(Path.Join(DIR_X2_MENU,    "window.dds.phyre"),               FhTextureType.GAME_PHYRE_2D);
    public readonly FhTexture _texture_wave     = new(Path.Join(DIR_X2_MENU,    "texture.dds.phyre"),              FhTextureType.GAME_PHYRE_2D);
    public readonly FhTexture _texture_b_menu1  = new(Path.Join(DIR_X2_MENU,    "b_menu1.dds.phyre"),              FhTextureType.GAME_PHYRE_2D);

    private readonly Vector2 _tex_window_size = new(1024f, 256f);

    public readonly FhTexture _texture_meswin = new(Path.Join(DIR_X_MENU, "meswin.dds.phyre"), FhTextureType.GAME_PHYRE_2D);

    protected override FhTexture[] textures => [
        _texture_menuback,
        _texture_black0,
        _texture_mahojin,
        _texture_bk_blue,
        _texture_freetex,
        _texture_plate,
        _texture_window,
        _texture_wave,
        _texture_b_menu1,

        _texture_meswin,
    ];

    FhGui.DrawPlateConfig plate_config => new(_texture_plate) {
        color = 0xFFD3EEF2,
        border_width = 5f * aspect_scale.Y,
        shadow_offset = new Vector2(7f) * aspect_scale,
    };

    FhGui.DrawCursorConfig cursor_config => new(_texture_freetex) {
        cursor_size = new Vector2(92f, 55f) * aspect_scale,
        max_offset = 18f * aspect_scale.X,
    };

    private readonly NineSliceHelper window_helper;

    public FhSettingsUiX2() {
        window_helper = NineSliceHelper.create(
            _tex_window_size,
            new(  -2f,  -1f),
            new(1026f, 257f),
            new(   3f,   3f)
        );

        FhInternal.Settings.register_setting_renderer<FhSettingsUiX2>(typeof(FhSettingsCategory), typeof(Renderer_Category));
        FhInternal.Settings.register_setting_renderer<FhSettingsUiX2>(typeof(FhSettingToggle),    typeof(Renderer_Toggle));
        FhInternal.Settings.register_setting_renderer<FhSettingsUiX2>(typeof(FhSettingChoice<>),  typeof(Renderer_Choice<>));
    }

    // Rendering

    /// <summary>Render the background.</summary>
    protected override void ui_background() {
        if (!_texture_menuback  .try_use(out ImTextureRef menuback, out FhTextureMetadata? menuback_meta)
            || !_texture_black0 .try_use(out ImTextureRef black0,   out FhTextureMetadata? black0_meta)
            || !_texture_mahojin.try_use(out ImTextureRef mahojin,  out FhTextureMetadata? mahojin_meta)
            || !_texture_bk_blue.try_use(out ImTextureRef bk_blue,  out FhTextureMetadata? bk_blue_meta)
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
        }.as_uv(black0_meta.size);

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
        }.as_uv(mahojin_meta.size);

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
        }.as_uv(bk_blue_meta.size);

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
    protected override void ui_help() {
        if (!_texture_freetex.try_use(out ImTextureRef freetex, out FhTextureMetadata? freetex_meta)) {
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
            size = new(142f,  60f),
        }.as_uv(freetex_meta.size);

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

        string text = focus switch {
            UiFocus.MODS    => localize("help.mod_list"),
            UiFocus.MODULES => localize("help.module_list"),

            UiFocus.SETTINGS when capturing_renderer != null
                => capturing_renderer.get_input_help_text(),

            UiFocus.SETTINGS => hovered_setting!.desc,

            _ => throw new NotImplementedException(),
        };

        Vector2 text_pos = bg_screen.left + new Vector2(219f, 0f) * aspect_scale;

        FhApi.Gui.draw_text(
            draw,
            text_pos,
            text,
            text_config with { align = Alignment2D.LEFT }
        );
    }

    /// <summary>Renders a mod plate.</summary>
    /// <param name="mod_ctx">The context of the mod to render the plate for.</param>
    /// <param name="global_index">The index of the mod in the mod list.</param>
    /// <param name="local_index">The index of the plate on the screen.</param>
    private void ui_mod(FhModContext mod_ctx, int global_index, int local_index) {
        if (!_texture_plate.try_use(out ImTextureRef plate, out FhTextureMetadata? plate_meta)) {
            return;
        }

        Vector2 plate_screen_size = new(475f, 60f);

        float plate_screen_dy = plate_screen_size.Y + 18f;

        Rect plate_screen = new Rect {
            pos  = new(35f, 174f + plate_screen_dy * local_index),
            size = plate_screen_size,
        }.scale_to_aspect(aspect_helper);

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        FhApi.Gui.draw_x2_plate(
            draw,
            plate_screen,
            local_index,
            plate_config with {
                border_width = 6f * aspect_scale.Y,
            }
        );

        Vector2 text_pos = plate_screen.center;
        text_pos.Y -= 1f;

        Vector2 text_size = FhApi.Gui.draw_text(
            draw,
            text_pos,
            mod_ctx.Manifest.Name,
            text_config with { align = Alignment2D.CENTER }
        );

        if (mouse_hovered(plate_screen)) {
            scrollable_mods.hovered = global_index;
        }

        if (global_index == scrollable_mods.hovered) {
            Vector2 cursor_target = text_pos;
            cursor_target.X -= text_size.X / 2f;
            cursor_target.X -= 10f * aspect_scale.X;

            if (cursor_target.X < 60f * aspect_scale.X) {
                cursor_target.X = 60f * aspect_scale.X;
            }

            FhApi.Gui.draw_x2_cursor(
                draw,
                cursor_target,
                cursor_config with {
                    overlap = false
                }
            );
        }

        if (mouse_clicked(plate_screen)) {
            select_mod(global_index);
        }
    }

    /// <summary>Renders all mod plates.</summary>
    protected override void ui_list_mods() {
        int max_i = int.Min(scrollable_mods.max, scrollable_mods.visible);

        for (int i = 0, mod_i = scrollable_mods.current; i < max_i; i++, mod_i++) {
            ui_mod(settings_map.GetAt(mod_i).Key, mod_i, i);
        }
    }

    /// <summary>Renders a module plate.</summary>
    /// <param name="module_ctx">The context of the module to render the plate for.</param>
    /// <param name="global_index">The index of the module in the mod list.</param>
    /// <param name="local_index">The index of the plate on the screen.</param>
    private void ui_module(FhModuleContext module_ctx, int global_index, int local_index) {
        if (!_texture_plate.try_use(out ImTextureRef plate, out FhTextureMetadata? plate_meta)) {
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

        FhApi.Gui.draw_x2_plate(
            draw,
            plate_screen,
            local_index + 1,
            plate_config
        );

        Vector2 text_pos = plate_screen.center;
        text_pos.Y -= 1f;

        Vector2 text_size = FhApi.Gui.draw_text(
            draw,
            text_pos,
            module_ctx.Module.ModuleType.Split('.')[^1],
            text_config with { align = Alignment2D.CENTER }
        );

        if (mouse_hovered(plate_screen)) {
            scrollable_modules.hovered = global_index;
        }

        if (global_index == scrollable_modules.hovered) {
            Vector2 cursor_target = text_pos;
            cursor_target.X -= text_size.X / 2f;
            cursor_target.X -= 10f * aspect_scale.X;

            if (cursor_target.X < 60f * aspect_scale.X) {
                cursor_target.X = 60f * aspect_scale.X;
            }

            FhApi.Gui.draw_x2_cursor(
                draw,
                cursor_target,
                cursor_config with {
                    overlap = false
                }
            );
        }

        if (mouse_clicked(plate_screen)) {
            select_module(global_index);
        }
    }

    /// <summary>Renders all module plates.</summary>
    protected override void ui_list_modules() {
        ui_mod(selected_mod, selected_mod_idx, 0);

        //TODO: Move this line to when a mod is selected
        scrollable_modules.max = selected_modules.Count;

        int max_i = int.Min(scrollable_modules.max, scrollable_modules.visible);

        for (int i = 1, module_i = scrollable_modules.current; i < max_i + 1; i++, module_i++) {
            ui_module(selected_modules[module_i], module_i, i);
        }
    }

    private void ui_setting(FhSetting setting, Rect outer_bounds) {
        if (!setting.get_renderer<FhSettingsUiX2>(out FhSettingRenderer? renderer)) {
            Rect debug_bounds = new() {
                pos  = new(610f, 204f + setting_y),
                size = new(outer_bounds.size.X, 40f),
            };

            FhApi.Gui.draw_text(
                ImGui.GetBackgroundDrawList(),
                debug_bounds.scale_to_aspect(aspect_helper).center,
                $"Renderer not found for {setting.GetType()}.",
                text_config with { align = Alignment2D.CENTER }
            );

            setting_y += 30f;

            return;
        }

        float indent_size = indent_level * INDENT_SIZE;

        Vector2 pos = new(610f, 204f);
        pos.X += indent_size;
        pos.Y += setting_y;

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

            draw.PushClipRect(scaled_bounds.top_left, scaled_bounds.bottom_right);

            renderer.render(this, setting, bounds);

            draw.PopClipRect();
        }

        setting_idx++;
        setting_y += size.Y + SETTING_MARGIN_SIZE;

        if (setting is FhSettingsCategory category && !category.collapsed) {
            indent();

            foreach (FhSetting inner_setting in category.settings) {
                ui_setting(inner_setting, outer_bounds);
            }

            unindent();
            setting_y += CATEGORY_MARGIN_SIZE;
        }
    }

    protected override void ui_list_settings() {
        // Debugging
        if (displayed_settings == null) {
            FhInternal.Settings.try_get_settings(selected_module.Module, out displayed_settings);
        }

        if (displayed_settings == null) return;

        indent_level = 0;
        setting_y    = 0f;
        setting_idx  = 0;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        Rect bounds = new() {
            pos  = new( 610f, 204f),
            size = new(1100f, 769f),
        };

        Rect scaled_bounds = bounds.scale_to_aspect(aspect_helper);

        draw.PushClipRect(scaled_bounds.top_left, scaled_bounds.bottom_right);

        foreach (FhSetting setting in displayed_settings.settings) {
            ui_setting(setting, bounds);
        }

        indent_level += 1;

        foreach (FhSetting setting in displayed_settings.settings) {
            ui_setting(setting, bounds);
        }

        indent_level += 1;

        foreach (FhSetting setting in displayed_settings.settings) {
            ui_setting(setting, bounds);
        }

        indent_level -= 2;

        foreach (FhSetting setting in displayed_settings.settings) {
            ui_setting(setting, bounds);
        }

        indent_level += 1;

        foreach (FhSetting setting in displayed_settings.settings) {
            ui_setting(setting, bounds);
        }

        indent_level += 1;

        foreach (FhSetting setting in displayed_settings.settings) {
            ui_setting(setting, bounds);
        }

        draw.PopClipRect();
    }

    /// <summary>Renders a scrollbar.</summary>
    /// <param name="scrollable">The scrollable to draw the scrollbar for.</param>
    /// <param name="track_bounds">The bounding rectangle of the scrollbar's track.</param>
    private void ui_scrollbar<T>(BaseScrollable<T> scrollable, Rect track_bounds)
        where T : INumber<T> {

        if (scrollable.max <= scrollable.visible) {
            return;
        }

        if (!_texture_b_menu1.try_use(out ImTextureRef b_menu1, out FhTextureMetadata? b_menu1_meta)) {
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
        }.as_uv(b_menu1_meta.size);

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
        if (scrollable != current_scrollable) return;

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
    protected override void ui_scrollbars() {
        Rect scrollbar_mods = new() {
            pos  = new(530f, 202f),
            size = new(  8f, 784f),
        };

        if (focus == UiFocus.MODS)
            ui_scrollbar(scrollable_mods, scrollbar_mods);
        else
            ui_scrollbar(scrollable_modules, scrollbar_mods);
    }
}
