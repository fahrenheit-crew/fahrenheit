// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime.Gui;

[FhLoad(FhGameId.FFX | FhGameId.FFX2 | FhGameId.FFX2LM)]
public partial class FhSettingsUiX : FhSettingsUiBase {

    public const int   INDENT_MAX  = 5;
    public const float INDENT_SIZE = 40f;

    public const float SETTINGS_LIST_HEIGHT = 769f;

    public const float SETTING_MARGIN_SIZE  = 22f;
    public const float CATEGORY_MARGIN_SIZE = 10f;

    // Textures
    public readonly FhTexture _texture_help         = new(Path.Join(DIR_X_HIKU, "0_0_512_448_0.dds.phyre"), FhTextureType.GAME_PHYRE_2D);
    public readonly FhTexture _texture_bg           = new(Path.Join(DIR_X_MENU, "ffx_bg.dds.phyre"),        FhTextureType.GAME_PHYRE_2D);
    public readonly FhTexture _texture_battle       = new(Path.Join(DIR_X_MENU, "battle.dds.phyre"),        FhTextureType.GAME_PHYRE_2D);
    public readonly FhTexture _texture_meswin       = new(Path.Join(DIR_X_MENU, "meswin.dds.phyre"),        FhTextureType.GAME_PHYRE_2D);
    public readonly FhTexture _texture_menu_new     = new(Path.Join(DIR_X_MENU, "menu_new.dds.phyre"),      FhTextureType.GAME_PHYRE_2D);
    public readonly FhTexture _texture_battle_kuang = new(Path.Join(DIR_X_MENU, "battle_kuang.dds.phyre"),  FhTextureType.GAME_PHYRE_2D);

    protected override FhTexture[] textures => [
        _texture_help,
        _texture_bg,
        _texture_battle,
        _texture_meswin,
        _texture_menu_new,
        _texture_battle_kuang,
    ];

    FhGui.DrawCursorConfig cursor_config => new(_texture_meswin) {
        cursor_size = new Vector2(92f, 61f) * aspect_scale,
        max_offset  = 16f * aspect_scale.X,
    };

    public FhSettingsUiX() {
        scrollable_settings.visible = SETTINGS_LIST_HEIGHT;
    }

    protected override float get_setting_margin(FhSetting setting) {
        ArgumentOutOfRangeException.ThrowIfEqual(setting, displayed_settings);

        if (!try_get_setting_parent(setting, out FhSettingsCategory? parent, out int? index)) {
            throw new UnreachableException();
        }

        return index == parent.settings.Length - 1
            ? SETTING_MARGIN_SIZE + CATEGORY_MARGIN_SIZE
            : SETTING_MARGIN_SIZE;
    }

    // Rendering

    /// <summary>Render the background.</summary>
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

    /// <summary>Render the help text.</summary>
    protected override void ui_help() {
        if (!_texture_help.try_use(out ImTextureRef help, out FhTextureMetadata? help_meta)) {
            return;
        }

        UV bg_tuv = new Rect {
            pos  = new( 154f, 1761f),
            size = new(1740f,  134f),
        }.as_uv(help_meta.size);

        // Saving this one as a rect to more easily calculate the text position later
        Rect bg_screen = new Rect {
            pos  = new( 151f, 80f),
            size = new(1618f, 70f),
        }.scale_to_aspect(aspect_helper);

        UV bg_suv = bg_screen.as_uv();

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        draw.AddImage(help, bg_suv.p0, bg_suv.p1, bg_tuv.p0, bg_tuv.p1);

        string text = focus switch {
            UiFocus.MODS     => localize("help.mod_list"),
            UiFocus.MODULES  => localize("help.module_list"),
            UiFocus.SETTINGS => hovered_setting!.desc,

            _ => throw new NotImplementedException(),
        };

        if (focus == UiFocus.SETTINGS && captured_setting != null) {
            if (get_setting_renderer(captured_setting, out SettingRenderer? renderer)) {
                text = ((ICapturingRenderer)renderer).get_input_help_text();
            }
        }

        Vector2 text_pos = bg_screen.left + new Vector2(24f, 5f) * aspect_scale;

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
        if (!_texture_menu_new.try_use(out ImTextureRef menu_new, out FhTextureMetadata? menu_new_meta)) {
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
        }.as_uv(menu_new_meta.size);

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

            FhApi.Gui.draw_x_cursor(
                draw,
                cursor_target,
                cursor_config with { overlap = false }
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
        if (!_texture_menu_new.try_use(out ImTextureRef menu_new, out FhTextureMetadata? menu_new_meta)) {
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
        }.as_uv(menu_new_meta.size);

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

        draw.PushClipRect(plate_screen.top_left, plate_screen.bottom_right);

        Vector2 text_pos = plate_screen.center;
        text_pos.Y -= plate_shadow_height;
        text_pos.Y += 1f * aspect_scale.Y;

        if (!FhInternal.Settings.try_get_settings(module_ctx.Module, out FhSettingsCategory? settings)) {
            return;
        }

        string module_name = settings.name;

        Vector2 text_size = FhApi.Gui.draw_text(
            draw,
            text_pos,
            module_name,
            text_config with { align = Alignment2D.CENTER }
        );

        draw.PopClipRect();

        if (focus == UiFocus.MODULES && mouse_hovered(plate_screen)) {
            hover_module(global_index);
        }

        if (global_index == scrollable_modules.hovered) {
            Vector2 cursor_target = text_pos;
            cursor_target.X -= text_size.X / 2f;
            cursor_target.X -= 10f * aspect_scale.X;

            if (cursor_target.X < 60f * aspect_scale.X) {
                cursor_target.X = 60f * aspect_scale.X;
            }

            FhApi.Gui.draw_x_cursor(
                draw,
                cursor_target,
                cursor_config with { overlap = false }
            );
        }

        if (focus == UiFocus.MODULES && mouse_clicked(plate_screen)) {
            select_module(global_index);
        }
    }

    /// <summary>Renders all module plates.</summary>
    protected override void ui_list_modules() {
        ui_mod(selected_mod, scrollable_mods.hovered, 0);

        //TODO: Move this line to when a mod is selected
        scrollable_modules.max = selected_modules.Count;

        int max_i = int.Min(scrollable_modules.max, scrollable_modules.visible);

        for (int i = 1, module_i = scrollable_modules.current; i < max_i + 1; i++, module_i++) {
            ui_module(selected_modules[module_i], module_i, i);
        }
    }

    protected override bool get_setting_renderer(FhSetting setting, [NotNullWhen(true)] out SettingRenderer? renderer) {
        renderer = 0 switch {
            _ when setting is FhSettingsCategory     => Renderer_Category     .instance,
            _ when setting is FhSettingToggle        => Renderer_Toggle       .instance,
            _ when setting is FhSettingNumber        => Renderer_Number       .instance,
            _ when setting is FhSettingChoice        => Renderer_Choice       .instance,
            _ when setting is FhSettingDynamicChoice => Renderer_DynamicChoice.instance,
            _ => null,
        };

        return renderer != null;
    }

    private void ui_setting(FhSetting setting, Rect outer_bounds) {
        if (!get_setting_renderer(setting, out SettingRenderer? renderer)) {
            Rect debug_bounds = new() {
                pos  = new(610f, 202f + setting_y),
                size = new(outer_bounds.size.X, 40f),
            };

            FhApi.Gui.draw_text(
                ImGui.GetBackgroundDrawList(),
                debug_bounds.scale_to_aspect(aspect_helper).center,
                $"Renderer not found for {setting.GetType()}.",
                text_config with { align = Alignment2D.CENTER }
            );

            setting_y += 40f;

            return;
        }

        float indent_size = int.Min(indent_level, INDENT_MAX) * INDENT_SIZE;

        Vector2 pos = new(610f, 202f);
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

        if (bounds.bottom.Y >= outer_bounds.top.Y) {
            ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

            Rect scaled_bounds = bounds.scale_to_aspect(aspect_helper);
            Rect scaled_outer_bounds = outer_bounds.scale_to_aspect(aspect_helper);

            draw.PushClipRect(scaled_bounds.top_left, scaled_bounds.bottom_right);
            draw.PushClipRect(scaled_outer_bounds.top_left, scaled_outer_bounds.bottom_right);

            renderer.render(this, setting, bounds);

            draw.PopClipRect();
            draw.PopClipRect();

            if (focus == UiFocus.SETTINGS && mouse_hovered(scaled_bounds)) {
                hovered_setting = setting;
                ensure_setting_visible(hovered_setting);
            }

            if (setting == hovered_setting) {
                FhApi.Gui.draw_x_cursor(
                    draw,
                    scaled_bounds.left,
                    cursor_config
                );
            }
        }

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

        Rect bounds = new() {
            pos  = new( 610f, 202f),
            size = new(1100f, SETTINGS_LIST_HEIGHT),
        };

        float clip_start = scrollable_settings.get_clip_start();
        float clip_end   = scrollable_settings.get_clip_end();

        foreach (FhSetting setting in displayed_settings.settings) {
            float top    = calculate_setting_top(setting);
            float height = get_setting_height(setting);

            // off-screen on the top
            if (top + height < clip_start)
                continue;

            // off-screen on the bottom
            if (top > clip_end)
                continue;

            if (setting_y == 0f && top < scrollable_settings.get_clip_start()) {
                setting_y = top - scrollable_settings.get_clip_start();
            }

            ui_setting(setting, bounds);
        }
    }

    /// <summary>Renders all scrollbars.</summary>
    protected override void ui_scrollbars() {
        Rect scrollbar_mods = new() {
            pos  = new(527f, 208f),
            size = new( 17f, 777f),
        };

        Rect scrollbar_settings = new() {
            pos  = new(1730f, 202f),
            size = new(  17f, SETTINGS_LIST_HEIGHT),
        };

        if (focus == UiFocus.MODS) {
            ui_scrollbar(scrollable_mods, scrollbar_mods);
            return;
        }

        ui_scrollbar(scrollable_modules,  scrollbar_mods);
        ui_scrollbar(scrollable_settings, scrollbar_settings);
    }

    protected override void ui_scrollbar_direct<T>(
        ref ScrollbarData<T> data
    ) {
        if (!_texture_battle.try_use(out ImTextureRef battle, out FhTextureMetadata? battle_meta)) {
            return;
        }

        uint track_color = 0xFF1A1A1A;

        uint thumb_color_left  = 0xFFC9ADAD;
        uint thumb_color_right = 0xFFAA8082;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        draw.AddRectFilled(
            data.bounds_track.top_left,
            data.bounds_track.bottom_right,
            track_color
        );

        draw.AddRectFilledMultiColor(
            data.bounds_thumb.top_left,
            data.bounds_thumb.bottom_right,
            thumb_color_left,
            thumb_color_right,
            thumb_color_right,
            thumb_color_left
        );

        UV triangle_top_suv    = data.bounds_arrow_begin.as_uv();
        UV triangle_bottom_suv = data.bounds_arrow_end  .as_uv();

        UV triangle_tuv = new Rect {
            pos  = new(708f, 425f),
            size = new( 45f,  26f),
        }.as_uv(battle_meta.size);

        draw.AddImage(
            battle,
            triangle_top_suv.p0,
            triangle_top_suv.p1,
            triangle_tuv.p1,
            triangle_tuv.p0,
            !data.at_top ? 0xFFFFFFFF : 0x90D0D0D0
        );

        draw.AddImage(
            battle,
            triangle_bottom_suv.p0,
            triangle_bottom_suv.p1,
            triangle_tuv.p0,
            triangle_tuv.p1,
            !data.at_bottom ? 0xFFFFFFFF : 0x90D0D0D0
        );
    }
}
