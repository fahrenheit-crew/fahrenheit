// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime.Gui;

public abstract class FhSettingsUiBase : FhSettingsUi {
    protected class SettingsScrollable(FhSettingsUiBase ui) : Scrollable {
        protected override ImGuiKey[] keys_pg_up => [
            ImGuiKey.PageUp,
            ImGuiKey.GamepadL1,
        ];

        protected override ImGuiKey[] keys_pg_dn => [
            ImGuiKey.PageDown,
            ImGuiKey.GamepadR1,
        ];

        protected bool scroll_page_iterate(FhSetting setting, ref float total) {
            if (!ui.get_setting_renderer(setting, out SettingRenderer? renderer)) {
                throw new NotImplementedException();
            }

            total += renderer.get_size(ui).Y;

            if (total >= current) {
                ui.hovered_setting = setting;
                ui.ensure_setting_visible(ui.hovered_setting);
                return true;
            }

            total += ui.get_setting_margin(setting);

            if (setting is not FhSettingsCategory category || category.collapsed)
                return false;

            foreach (FhSetting inner_setting in category.settings) {
                if (scroll_page_iterate(inner_setting, ref total))
                    return true;
            }

            return false;
        }

        public override void scroll_page_up() {
            current -= visible;
            current = float.Clamp(current, 0f, float.Max(0f, max_current));

            float running_total = 0f;
            foreach (FhSetting setting in ui.displayed_settings!.settings) {
                if (scroll_page_iterate(setting, ref running_total))
                    return;
            }
        }

        public override void scroll_page_down() {
            current += visible;
            current = float.Clamp(current, 0f, float.Max(0f, max_current));

            float running_total = 0f;
            foreach (FhSetting setting in ui.displayed_settings!.settings) {
                if (scroll_page_iterate(setting, ref running_total))
                    return;
            }
        }

        public override void scroll_begin() {
            current = 0f;

            ui.hovered_setting = ui.displayed_settings!.settings[0];
        }

        public override void scroll_end() {
            current = max <= visible ? 0f : max_current;

            FhSetting bottom_setting = ui.displayed_settings!.settings[^1];

            while (bottom_setting is FhSettingsCategory category && !category.collapsed) {
                bottom_setting = category.settings[^1];
            }

            ui.hovered_setting = bottom_setting;
        }

        public override bool handle_input() {
            if (FhApi.Gui.is_any_pressed(keys_pg_up, repeat: true)
                && current > 0f
            ) {
                scroll_page_up();

                return true;
            }

            if (FhApi.Gui.is_any_pressed(keys_pg_dn, repeat: true)
                && current < max_current
            ) {
                float old_current = current;
                scroll_page_down();

                if (old_current == current) {
                    scroll_end();
                }

                return true;
            }

            if (FhApi.Gui.is_any_pressed(keys_home)) {
                scroll_begin();
                return true;
            }

            if (FhApi.Gui.is_any_pressed(keys_end)) {
                scroll_end();
                return true;
            }

            float mouse_wheel = ImGui.GetIO().MouseWheel;

            if (mouse_wheel > 0) {
                scroll(-scroll_step_size);
                return true;
            }

            if (mouse_wheel < 0) {
                scroll(scroll_step_size);
                return true;
            }

            return false;
        }
    }

    public interface ICapturingRenderer {
        public string get_input_help_text();
    }

    protected abstract class SettingRenderer {
        public abstract Vector2 get_size(FhSettingsUiBase ui);
        public abstract bool handle_input(FhSettingsUiBase ui, FhSetting setting);
        public abstract void render(FhSettingsUiBase ui, FhSetting setting, Rect max_bounds);
    }

    /// <summary>Possible elements for the UI to focus on.</summary>
    protected enum UiFocus {
        /// <summary>The scrollable list of mods with settings.</summary>
        MODS,

        /// <summary>The list of modules that scrolls out when a mod is selected.</summary>
        MODULES,

        /// <summary>The list of settings that appears when a module is hovered/selected.</summary>
        SETTINGS,
    }

    protected const float FONT_SIZE_DEFAULT = 43f;

    protected const uint SOUND_X_MOVE   = FFX.Ids.SoundId.UI_ACTION;
    protected const uint SOUND_X_ACTION = FFX.Ids.SoundId.UI_ACTION;
    protected const uint SOUND_X_CANCEL = FFX.Ids.SoundId.UI_CANCEL;

    protected const uint SOUND_X2_MOVE   = FFX2.Ids.SoundId.UI_ACTION;
    protected const uint SOUND_X2_ACTION = FFX2.Ids.SoundId.UI_ACTION;
    protected const uint SOUND_X2_CANCEL = FFX2.Ids.SoundId.UI_CANCEL;

    protected const string DIR_X_MENU = "/FFX_Data/GameData/PS3Data/menu/D3D11/";
    protected const string DIR_X_HIKU = "/FFX_Data/GameData/PS3Data/map/hiku/hiku22/2d/tex/D3D11/";

    protected const string DIR_X2_MENU    = "/FFX-2_Data/GameData/PS3Data/menu/D3D11/";
    protected const string DIR_X2_MAHOJIN = "/FFX-2_Data/GameData/PS3Data/menu/menu_mahojin/tex/D3D11/";
    protected const string DIR_X2_PLATE   = "/FFX-2_Data/GameData/PS3Data/menu/menu_plate/tex/D3D11/";

    protected const uint COLOR_BLACK = 0xFF000000;
    protected const uint COLOR_TRANS = 0x00000000;

    protected const float FADE_LENGTH = 0.35f;

    protected static readonly uint SOUND_ID_MOVE   = FhUtil.select(SOUND_X_MOVE,       SOUND_X2_MOVE,       SOUND_X2_MOVE);
    protected static readonly uint SOUND_ID_ACTION = FhUtil.select(SOUND_X_ACTION,     SOUND_X2_ACTION,     SOUND_X2_ACTION);
    protected static readonly uint SOUND_ID_CANCEL = FhUtil.select(SOUND_X_CANCEL,     SOUND_X2_CANCEL,     SOUND_X2_CANCEL);

    protected static readonly float ACTION_VOLUME = FhUtil.select(1f, 0.5f, 0.5f);

    protected UiFocus focus;

    // Display
    protected readonly FadeHelper fade;

    protected readonly Fahrenheit.Gui.Timer audio_fade_helper = new(30f);

    protected bool audio_fade_helper_restart = true;

    protected FhModContext          selected_mod     => settings_map.GetAt(scrollable_mods.hovered).Key;
    protected List<FhModuleContext> selected_modules => settings_map.GetAt(scrollable_mods.hovered).Value;
    protected FhModuleContext       selected_module  => settings_map.GetAt(scrollable_mods.hovered).Value[scrollable_modules.hovered];

    protected FhSetting? hovered_setting;

    protected int   indent_level;
    protected float setting_y;

    protected FhSetting?          captured_setting;

    protected FhSettingsCategory? displayed_settings;

    protected readonly SteppedScrollable  scrollable_mods     = new(visible: 11);
    protected readonly SteppedScrollable  scrollable_modules  = new(visible: 10);
    protected readonly SettingsScrollable scrollable_settings;

    protected BaseScrollable? current_scrollable;

    protected BaseScrollable? scrollbar_dragging;
    protected Vector2?        scrollbar_held_pos;

    protected struct ScrollbarData<T> where T : INumber<T> {
        public BaseScrollable<T> scrollable;
        public float             current_progress;
        public bool              at_top;
        public bool              at_bottom;

        public T current     => T.CreateChecked(scrollable.current);
        public T visible     => T.CreateChecked(scrollable.visible);
        public T max         => T.CreateChecked(scrollable.max);
        public T max_current => max - visible;

        public float visible_percentage => float.CreateChecked(visible) / float.CreateChecked(T.Max(T.One, max));

        public Rect  bounds_track;
        public float scrollable_height;   // only filled after ui_scrollbar_get_thumb_bounds()
        public Rect  bounds_thumb;        // only filled after ui_scrollbar_get_thumb_bounds()
        public Rect  bounds_thumb_hitbox; // only filled after ui_scrollbar_get_thumb_bounds()
        public Rect  bounds_arrow_begin;  // only filled after ui_scrollbar_get_arrow_bounds()
        public Rect  bounds_arrow_end;    // only filled after ui_scrollbar_get_arrow_bounds()
    }

    protected virtual float font_size => FONT_SIZE_DEFAULT * font_scale;

    protected FhGui.DrawTextConfig text_config => new() {
        font_size = font_size,
    };

    private bool _loaded_all_textures;

    protected virtual FhTexture[] textures => [];

    protected override Vector2 get_ref_size() => new(1920f, 1080f);

    protected FhSettingsUiBase() {
        scrollable_settings = new(this);

        current_scrollable = null;

        fade = new(0, 0, FADE_LENGTH);
    }

    public override bool init(FhModContext context, FileStream global_state) {
        return base.init(context, global_state)
            && FhApi.Events.Common.GameLoop.PostOpenSettingsMenu .subscribe(post_open)
            && FhApi.Events.Common.GameLoop.PostCloseSettingsMenu.subscribe(post_close);
    }

    private void post_open(EventArgs e) {
        focus = UiFocus.MODS;

        try_load_textures();

        fade.restart(COLOR_BLACK, COLOR_TRANS, FADE_LENGTH);

        scrollable_mods    .max = settings_map.Count;
        scrollable_settings.max = 0f;

        scrollable_settings.scroll_step_size = 60f * aspect_scale.Y;
    }

    private void post_close(EventArgs e) {
        unload_textures();

        hovered_setting = null;
    }


    /* ===== Helper Methods ===== */
    protected string localize(string key) {
        return FhApi.Localization.localize($"{typeof(FhSettingsUi)}.{key}");
    }

    /// <summary>Get the volume a move sound should play at.</summary>
    /// <returns>The desired volume.</returns>
    protected float get_move_volume() {
        float min_volume = ACTION_VOLUME * 0.5f;

        return min_volume + (1f - audio_fade_helper.progress) * (1f - min_volume);
    }

    /// <summary>Attempt to load all of the textures the UI requires to display properly.</summary>
    /// <returns>Whether all textures have been successfully loaded.</returns>
    private bool try_load_textures() {
        _loaded_all_textures = true;
        foreach (FhTexture texture in textures) {
            if (!FhApi.Resources.load_texture(texture)) {
                _loaded_all_textures = false;
            }
        }

        return _loaded_all_textures;
    }

    /// <summary>Unload all of the textures the UI requires to display properly.</summary>
    private void unload_textures() {
        foreach (FhTexture texture in textures) {
            if (FhApi.Resources.unload_texture(texture))
                _loaded_all_textures = false;
            else
                _logger.Warning($"Failed to unload texture: {texture.path}");
        }
    }

    protected void fade_out(Action action) {
        fade.restart(
            fade.get_color(),
            COLOR_BLACK,
            FADE_LENGTH * fade.progress,
            action
        );
    }

    protected void select_mod(int mod_idx) {
        focus = UiFocus.MODULES;

        scrollable_mods.hovered = mod_idx;

        scrollable_modules.hovered = 0;

        FhInternal.Settings.try_get_settings(selected_modules[0].Module, out displayed_settings);
    }

    protected void hover_module(int module_idx) {
        scrollable_modules.hovered = module_idx;

        FhModule hovered_module = selected_module.Module;

        FhInternal.Settings.try_get_settings(hovered_module, out displayed_settings);

        scrollable_settings.current = 0f;
        recalculate_settings_scrollable();
    }

    protected void select_module(int module_idx) {
        focus = UiFocus.SETTINGS;

        scrollable_modules.hovered = module_idx;

        FhInternal.Settings.try_get_settings(selected_module.Module, out displayed_settings);
        hovered_setting = displayed_settings!.settings[0];

        scrollable_settings.current = 0f;
        recalculate_settings_scrollable();
    }

    protected bool try_get_previous_setting(FhSetting setting, [NotNullWhen(true)] out FhSetting? prev_setting) {
        ArgumentOutOfRangeException.ThrowIfEqual(setting, displayed_settings);

        if (!try_get_setting_parent(setting, out FhSettingsCategory? parent, out int? index)) {
            throw new UnreachableException();
        }

        prev_setting = null;

        // If we have a parent and the index is zero, the parent category is guaranteed to be directly above us!
        if (index == 0) {
            // The displayed settings category is never actually displayed, only its settings
            if (parent == displayed_settings) {
                return false;
            }

            prev_setting = parent;
            return true;
        }

        FhSetting setting_one_up = parent.settings[(int)index - 1];

        // We have to dive into the previous setting if it's an expanded category
        while (setting_one_up is FhSettingsCategory category && !category.collapsed) {
            setting_one_up = category.settings[^1];
        }

        prev_setting = setting_one_up;
        return true;
    }

    protected bool try_get_next_setting(FhSetting setting, [NotNullWhen(true)] out FhSetting? next_setting) {
        ArgumentOutOfRangeException.ThrowIfEqual(setting, displayed_settings);

        if (!try_get_setting_parent(setting, out FhSettingsCategory? parent, out int? index)) {
            throw new UnreachableException();
        }

        next_setting = null;

        // If the setting is an expanded category, go into the category
        if (setting is FhSettingsCategory category
            && !category.collapsed
            && category.settings.Length > 0
        ) {
            next_setting = category.settings[0];
            return true;
        }

        // If there is no next setting in current category, go up until we find one
        while (parent.settings.Length <= index + 1) {
            // If the parent is the displayed settings and the condition still passed,
            // there is no next setting
            if (parent == displayed_settings)
                return false;

            index  = parent.index_in_parent;
            parent = parent.parent!;
        }

        next_setting = parent.settings[(int)index + 1];
        return true;
    }

    protected void hover_setting_up() {
        if (!try_get_previous_setting(hovered_setting!, out FhSetting? prev_setting)) {
            return;
        }

        hovered_setting = prev_setting;
        ensure_setting_visible(hovered_setting);
    }

    protected void hover_setting_down() {
        if (!try_get_next_setting(hovered_setting!, out FhSetting? next_setting)) {
            return;
        }

        hovered_setting = next_setting;
        ensure_setting_visible(next_setting);
    }

    protected float get_setting_height(FhSetting setting) {
        if (!get_setting_renderer(setting, out SettingRenderer? renderer)) {
            throw new NotImplementedException();
        }

        float height = 0f;

        height += renderer.get_size(this).Y;

        if (setting is not FhSettingsCategory category || category.collapsed) {
            return height;
        }

        foreach (FhSetting inner_setting in category.settings) {
            height += get_setting_height(inner_setting);
            height += get_setting_margin(inner_setting);
        }

        return height;
    }

    protected abstract float get_setting_margin(FhSetting setting);

    protected float calculate_setting_top(FhSetting setting) {
        if (setting == displayed_settings) {
            return 0f;
        }

        float top = 0f;

        if (!try_get_setting_parent(setting, out FhSettingsCategory? parent, out int? index)) {
            throw new UnreachableException();
        }

        while (parent != null) {
            for (int i = 0; i < index; i++) {
                top += get_setting_height(parent.settings[i]);
                top += get_setting_margin(parent.settings[i]);
            }

            if (parent == displayed_settings)
                break;

            if (!get_setting_renderer(parent, out SettingRenderer? renderer)) {
                throw new NotImplementedException();
            }

            top += renderer.get_size(this).Y;
            top += get_setting_margin(parent);

            try_get_setting_parent(parent, out parent, out index);
        }

        return top;
    }

    protected void ensure_setting_visible(FhSetting setting) {
        if (!get_setting_renderer(setting, out SettingRenderer? renderer)) {
            throw new NotImplementedException();
        }

        if (!try_get_setting_parent(setting, out FhSettingsCategory? parent, out int? index)) {
            throw new UnreachableException();
        }

        float top = calculate_setting_top(setting);

        Rect setting_bounds = new() {
            pos  = new Vector2(0f, top),
            size = renderer.get_size(this),
        };

        bool has_prev = try_get_previous_setting(setting, out FhSetting? prev_setting);
        bool has_next = try_get_next_setting(setting, out FhSetting? next_setting);

        float margin_top = has_prev
            ? float.Min(get_setting_margin(setting), get_setting_margin(prev_setting!))
            : get_setting_margin(setting);

        float margin_bottom = has_next
            ? float.Min(get_setting_margin(setting), get_setting_margin(next_setting!))
            : get_setting_margin(setting);

        float min_top    = setting_bounds.top.Y    - margin_top;
        float min_bottom = setting_bounds.bottom.Y + margin_bottom;

        min_top = float.Max(min_top, 0f);
        min_bottom = float.Min(min_bottom, scrollable_settings.max);

        if (scrollable_settings.get_clip_start() > min_top) {
            scrollable_settings.current = min_top;
        }
        else if (scrollable_settings.get_clip_end() < min_bottom) {
            scrollable_settings.current = min_bottom - scrollable_settings.visible;
        }
    }

    protected void recalculate_settings_scrollable() {
        float height = 0f;

        foreach (FhSetting setting in displayed_settings!.settings) {
            height += get_setting_height(setting);
            height += get_setting_margin(setting);
        }

        height -= get_setting_margin(displayed_settings.settings[^1]);

        scrollable_settings.max = height;

        float max_current = float.Max(0f, scrollable_settings.max - scrollable_settings.visible);

        scrollable_settings.current = float.Clamp(scrollable_settings.current, 0f, max_current);
    }


    /* ===== Interfacing with renderers ===== */
    public bool try_capture_setting(FhSetting setting) {
        if (captured_setting != null) return false;

        captured_setting = setting;

        return true;
    }

    public void release_setting() {
        captured_setting = null;
    }

    public void indent() {
        indent_level += 1;
    }

    public void unindent() {
        indent_level -= 1;
    }

    protected void toggle_category_collapsed(FhSettingsCategory category) {
        category.collapsed = !category.collapsed;

        recalculate_settings_scrollable();
    }


    /* ===== Input handling ===== */
    protected bool mouse_hovered(Rect rect, bool require_movement = true) {
        return FhApi.Gui.mouse_hovering(rect, require_movement);
    }

    protected bool mouse_clicked(Rect rect, ImGuiMouseButton button = ImGuiMouseButton.Left, bool repeat = false) {
        return FhApi.Gui.mouse_clicked(rect, button, repeat);
    }

    private void handle_input_mods() {
        if (scrollable_mods.handle_input())
            return;

        if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_confirm)) {
            select_mod(scrollable_mods.hovered);
            return;
        }

        if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_cancel)) {
            //TODO: Close from here
            return;
        }
    }

    private void handle_input_modules() {
        int old_hovered = scrollable_modules.hovered;

        if (scrollable_modules.handle_input()) {
            if (scrollable_modules.hovered != old_hovered)
                hover_module(scrollable_modules.hovered);

            return;
        }

        if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_confirm)) {
            select_module(scrollable_modules.hovered);
            return;
        }

        if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_cancel)) {
            focus = UiFocus.MODS;
            scrollable_modules.hovered = 0;
            return;
        }
    }

    private void handle_input_settings() {
        if (captured_setting != null) {
            if (!get_setting_renderer(captured_setting, out SettingRenderer? capturing_renderer)) {
                captured_setting = null;
                return;
            }

            capturing_renderer.handle_input(this, captured_setting);
            return;
        }

        if (hovered_setting != null) {
            if (get_setting_renderer(hovered_setting, out SettingRenderer? renderer)) {
                if (renderer.handle_input(this, hovered_setting)) {
                    return;
                }
            }
        }

        if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_up, repeat: true)) {
            hover_setting_up();
            return;
        }

        if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_down, repeat: true)) {
            hover_setting_down();
            return;
        }

        if (scrollable_settings.handle_input()) {
            return;
        }

        if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_cancel)) {
            focus = UiFocus.MODULES;
            hovered_setting = null;
            return;
        }
    }

    private void handle_input() {
        if (captured_setting != null) {
            return;
        }

        switch (focus) {
            case UiFocus.MODS:     handle_input_mods();     break;
            case UiFocus.MODULES:  handle_input_modules();  break;
            case UiFocus.SETTINGS: handle_input_settings(); break;
        }
    }


    /* ===== Rendering ===== */
    internal protected override void render_ui() {
        current_scrollable = focus switch {
            UiFocus.MODS    => scrollable_mods,
            UiFocus.MODULES => scrollable_modules,

            _ => null,
        };

        if (!try_load_textures()) return;

        handle_input();

        ui_background();
        ui_help();

        if (focus == UiFocus.MODS) {
            ui_list_mods();
        }
        else {
            ui_list_modules();
            ui_list_settings();
        }

        ui_scrollbars();

        // ui_fade();
    }

    protected abstract bool get_setting_renderer(FhSetting setting, [NotNullWhen(true)] out SettingRenderer? renderer);

    protected abstract void ui_background();
    protected abstract void ui_help();
    protected abstract void ui_list_mods();
    protected abstract void ui_list_modules();
    protected abstract void ui_list_settings();
    protected abstract void ui_scrollbars();

    /// <summary>Render a scrollbar for a given scrollable at a given position.</summary>
    /// <param name="scrollable">The scrollable to render a scrollbar for.</param>
    /// <param name="track_bounds">The bounds of the scrollbar's track.</param>
    protected void ui_scrollbar<T>(BaseScrollable<T> scrollable, Rect track_bounds)
        where T : INumber<T> {

        if (scrollable.max <= scrollable.visible) {
            return;
        }

        float current_progress = scrollable.get_progress();

        bool at_top;
        bool at_bottom;

        if (scrollable is SteppedScrollable stepped) {
            at_top    = stepped.hovered == 0;
            at_bottom = stepped.hovered >= stepped.max - 1;
        }
        else {
            at_top    = current_progress == 0f;
            at_bottom = current_progress == 1f;
        }

        ScrollbarData<T> data = new() {
            scrollable       = scrollable,
            current_progress = scrollable.get_progress(),
            at_top           = at_top,
            at_bottom        = at_bottom,
            bounds_track     = track_bounds,
        };

        ui_scrollbar_get_thumb_bounds(ref data);
        ui_scrollbar_get_arrow_bounds(ref data);

        data.bounds_track = data.bounds_track.scale_to_aspect(aspect_helper);

        // Render
        ui_scrollbar_direct(ref data);

        // Handle input
        float expanded_track_height = data.scrollable_height + (data.bounds_track.size.X - data.bounds_thumb.size.X);

        if (mouse_clicked(data.bounds_thumb_hitbox)) {
            scrollbar_dragging = scrollable;
            scrollbar_held_pos = ImGui.GetMousePos() - data.bounds_thumb_hitbox.pos;
        }

        if (ImGui.IsMouseReleased(ImGuiMouseButton.Left)) {
            scrollbar_dragging = null;
            scrollbar_held_pos = null;
        }

        if (scrollbar_dragging == scrollable) {
            Vector2 new_held_pos = ImGui.GetMousePos() - data.bounds_thumb_hitbox.pos;

            float drag_delta     = new_held_pos.Y - scrollbar_held_pos!.Value.Y;
            float progress_delta = drag_delta / expanded_track_height;

            float new_progress   = data.current_progress + progress_delta;

            if (scrollable is SteppedScrollable stepped_dragging)
                stepped_dragging.set_progress(new_progress, true);
            else
                scrollable.set_progress(new_progress);

            return;
        }

        if (scrollable is SteppedScrollable stepped_scrollable) {
            if (!data.at_top && mouse_clicked(data.bounds_arrow_begin, repeat: true)) {
                FhApi.Audio.play_sound(SOUND_ID_MOVE, volume: get_move_volume());
                audio_fade_helper.tick(1f);

                stepped_scrollable.move_hover(-1);
            }

            if (!data.at_bottom && mouse_clicked(data.bounds_arrow_end, repeat: true)) {
                FhApi.Audio.play_sound(SOUND_ID_MOVE, volume: get_move_volume());
                audio_fade_helper.tick(1f);

                stepped_scrollable.move_hover(1);
            }
        }
        else {
            if (!data.at_top && mouse_clicked(data.bounds_arrow_begin, repeat: true)) {
                scrollable.scroll(-scrollable.scroll_step_size);
            }

            if (!data.at_bottom && mouse_clicked(data.bounds_arrow_end, repeat: true)) {
                scrollable.scroll(scrollable.scroll_step_size);
            }
        }

        if ((mouse_hovered(data.bounds_arrow_begin, false) || mouse_hovered(data.bounds_arrow_end, false))
            && ImGui.IsMouseDown(ImGuiMouseButton.Left)
        ) {
            audio_fade_helper_restart = false;
        }
    }

    protected virtual void ui_scrollbar_get_thumb_bounds<T>(
        ref ScrollbarData<T> data
    ) where T : INumber<T> {
        Rect track = data.bounds_track;

        Vector2 thumb_margin = new(2f);

        float thumb_width = track.size.X - thumb_margin.X * 2f;

        float scrollable_height = track.size.Y - thumb_margin.Y * 2;

        float thumb_height_max = 0.8f * scrollable_height;
        float thumb_height_min = float.Min(20f, thumb_height_max);

        float thumb_height = float.Lerp(
            thumb_height_min,
            thumb_height_max,
            data.visible_percentage
        );

        Vector2 thumb_size = new(thumb_width, thumb_height);

        data.scrollable_height = track.size.Y - thumb_margin.Y * 2 - thumb_size.Y;
        float thumb_progress   = data.current_progress * data.scrollable_height;

        Rect thumb = new() {
            pos = new(
                track.top_left.X + thumb_margin.X,
                track.top_left.Y + thumb_margin.Y + thumb_progress
            ),
            size = thumb_size,
        };

        track = track.scale_to_aspect(aspect_helper);
        thumb = thumb.scale_to_aspect(aspect_helper);

        // Make sure that the thumb is still in the middle of the track after scaling
        thumb.size.X = float.Round(track.size.X) - float.Round(thumb_margin.X * aspect_scale.X) * 2f;
        thumb.pos.X  = float.Round(track.pos.X) + (float.Round(track.size.X) - thumb.size.X) / 2f;

        data.scrollable_height *= aspect_scale.Y;

        data.bounds_thumb = thumb;

        // Allow grabbing the thumb on the entire track width (hitbox)
        data.bounds_thumb_hitbox = thumb.expand(
            new(track.size.X - thumb.size.X),
            Alignment2D.CENTER
        );
    }

    protected virtual void ui_scrollbar_get_arrow_bounds<T>(
        ref ScrollbarData<T> data
    ) where T : INumber<T> {
        Rect track_bounds = data.bounds_track;

        float arrow_width  = 2.24f  * track_bounds.size.X;
        float arrow_height = 0.625f * arrow_width;

        Vector2 arrow_size = new(arrow_width, arrow_height);

        float triangle_gap = 13f;

        Rect arrow_top = new() {
            pos = new(
                track_bounds.top.X - arrow_size.X / 2f,
                track_bounds.top.Y - triangle_gap - arrow_size.Y
            ),
            size = arrow_size,
        };

        Rect arrow_bottom = new() {
            pos = new(
                track_bounds.bottom.X - arrow_size.X / 2f,
                track_bounds.bottom.Y + triangle_gap
            ),
            size = arrow_size,
        };

        arrow_top    = arrow_top   .scale_to_aspect(aspect_helper);
        arrow_bottom = arrow_bottom.scale_to_aspect(aspect_helper);

        data.bounds_arrow_begin = arrow_top;
        data.bounds_arrow_end   = arrow_bottom;
    }

    protected virtual void ui_scrollbar_direct<T>(
        ref ScrollbarData<T> data
    ) where T : INumber<T> {
        uint track_color = 0xFF000000;

        uint thumb_color_top    = 0xFFCCCCCC;
        uint thumb_color_bottom = 0xFF808080;

        uint arrow_color_top    = 0xFFC0C0C0;
        uint arrow_color_bottom = 0xFF808080;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

        draw.AddRectFilled(
            data.bounds_track.top_left,
            data.bounds_track.bottom_right,
            track_color
        );

        draw.AddRectFilledMultiColor(
            data.bounds_thumb.top_left,
            data.bounds_thumb.bottom_right,
            thumb_color_top,
            thumb_color_top,
            thumb_color_bottom,
            thumb_color_bottom
        );

        Rect top_arrow_outline_bounds = data.bounds_arrow_begin.expand(
            Vector2.Round(new Vector2(2f, 1f) * new Vector2(1f) * aspect_scale) * 2f,
            Alignment2D.CENTER
        );

        Rect bot_arrow_outline_bounds = data.bounds_arrow_end.expand(
            Vector2.Round(new Vector2(2f, 1f) * new Vector2(1f) * aspect_scale) * 2f,
            Alignment2D.CENTER
        );

        FhApi.Gui.draw_triangle_gradient(
            draw,
            top_arrow_outline_bounds,
            GradientDirection.UP,
            0xFF000000,
            0xFF000000,
            0xFF000000
        );

        FhApi.Gui.draw_triangle_gradient(
            draw,
            bot_arrow_outline_bounds,
            GradientDirection.DOWN,
            0xFF000000,
            0xFF000000,
            0xFF000000
        );

        FhApi.Gui.draw_triangle_gradient(
            draw,
            data.bounds_arrow_begin,
            GradientDirection.UP,
            arrow_color_bottom,
            arrow_color_bottom,
            arrow_color_top
        );

        FhApi.Gui.draw_triangle_gradient(
            draw,
            data.bounds_arrow_end,
            GradientDirection.DOWN,
            arrow_color_bottom,
            arrow_color_bottom,
            arrow_color_top
        );
    }
}
