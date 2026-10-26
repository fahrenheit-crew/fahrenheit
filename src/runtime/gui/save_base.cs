// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime.Gui;

public abstract class FhRuntimeSaveUiBase : FhSaveUi {
    /// <summary>Possible open windows of the save/load menu.</summary>
    protected enum UiMode {
        /// <summary>The list of saves to save/load/compile from.</summary>
        SAVE_LIST = 0,

        /// <summary>The save set selection window.</summary>
        SET_SWAP = 1,

        /// <summary>The popup for displaying save/load errors and notifications.</summary>
        SAVE_POPUP = 2,
    }

    /// <summary>Targets the player can put their cursor on.</summary>
    protected enum UiFocus {
        /// <summary>The scrollable list of either saves or sets.</summary>
        LIST = 0,

        /// <summary>The active set button that opens the set list.</summary>
        ACTIVE_SET = 1,
    }

    protected const uint COLOR_BLACK = 0xFF000000;
    protected const uint COLOR_TRANS = 0x00000000;

    private const string DIR_X_PS3  = "/FFX_Data/GameData/PS3Data";
    private const string DIR_X2_PS3 = "/FFX-2_Data/GameData/PS3Data";

    protected const string DIR_X_MENU      = $"{DIR_X_PS3}/menu/D3D11/";
    protected const string DIR_X_HIKU      = $"{DIR_X_PS3}/map/hiku/hiku22/2d/tex/D3D11/";
    protected const string DIR_X_MAP_ICONS = $"{DIR_X_PS3}/savedataicons/";

    protected const string DIR_X2_MENU      = $"{DIR_X2_PS3}/menu/D3D11/";
    protected const string DIR_X2_MAHOJIN   = $"{DIR_X2_PS3}/menu/menu_mahojin/tex/D3D11/";
    protected const string DIR_X2_PLATE     = $"{DIR_X2_PS3}/menu/menu_plate/tex/D3D11/";
    protected const string DIR_X2_FACE_DATA = $"{DIR_X2_PS3}/menu/face_data/D3D11/";
    protected const string DIR_X2_MAP_ICONS = $"{DIR_X2_PS3}/savedataicons/";

    protected static readonly float ACTION_VOLUME =
        FhGlobal.game_id is FhGameId.FFX ? 1f : 0.5f;

    protected UiMode  _mode;
    protected UiFocus _focus;

    protected readonly float      _fade_length = FhUtil.select(0.5f, 0.35f, 0.0f);
    protected readonly FadeHelper _fade;

    protected readonly Fahrenheit.Gui.Timer _audio_fade_helper = new(30f);

    protected bool _audio_fade_helper_restart = true;

    protected readonly List<string> _set_list = [ ];

    protected bool _loaded_all_textures;

    protected bool is_saving =>
        FhApi.Saves.impl_get_system_mode(out FhSaveSystemMode? mode) && mode is FhSaveSystemMode.SAVE;

    protected readonly SteppedScrollable _scrollable_saves = new(visible: 5);
    protected readonly SteppedScrollable _scrollable_sets  = new(visible: 9);

    protected SteppedScrollable _current_scrollable;

    protected bool     _scrollbar_dragging;
    protected Vector2? _scrollbar_held_pos;

    // We block input when fading out, for example so you can't requests two loads of a file.
    // We block input when fading in as well, but only for a short time to make the UI feel snappy.
    protected bool is_fade_blocking_input => !(_fade.is_done || (_fade.color_to == COLOR_TRANS && _fade.progress > 0.3f));
    protected bool should_handle_input    => !_scrollbar_dragging && !is_fade_blocking_input;

    protected virtual uint sound_id_move       => FFX.Ids.SoundId.UI_ACTION;
    protected virtual uint sound_id_action     => FFX.Ids.SoundId.UI_ACTION;
    protected virtual uint sound_id_cancel     => FFX.Ids.SoundId.UI_CANCEL;
    protected virtual uint sound_id_set_switch => FFX.Ids.SoundId.PAGE_TURN;

    protected float font_size => 43f * font_scale;

    protected virtual FhTexture[] textures => [];

    protected FhRuntimeSaveUiBase() {
        _fade = new(0, 0, _fade_length);

        _current_scrollable = _scrollable_saves;
    }

    public override bool init(FhModContext context, FileStream global_state) {
        return base.init(context, global_state)
            && FhApi.Events.Common.GameLoop.PostOpenSaveMenu .subscribe(post_open)
            && FhApi.Events.Common.GameLoop.PostCloseSaveMenu.subscribe(post_close);
    }

    protected override Vector2 get_ref_size() => new(1920f, 1080f);

    /// <summary>Initialize data when the UI is opened.</summary>
    private void post_open(EventArgs e) {
        _mode  = UiMode .SAVE_LIST;
        _focus = UiFocus.LIST;

        try_load_textures();

        _set_list.Clear();
        _set_list.AddRange(FhApi.Saves.sets_enumerate());

        FhSaveSet set = FhApi.Saves.sets_get_active();

        _scrollable_sets .max = _set_list.Count;
        _scrollable_saves.max = is_saving
            ? set.count + 1 - (set.has_autosave ? 1 : 0) // Add one for New Save Data button
            : set.count;

        _fade.restart(COLOR_BLACK, COLOR_TRANS, _fade_length);
    }

    /// <summary>Clean-up data after the UI is closed.</summary>
    private void post_close(EventArgs e) {
        _scrollable_saves.reset();
        _scrollable_sets .reset();

        unload_textures(textures);
    }


    /* ===== Helper methods ===== */

    /// <summary>Get localized save UI text with the given key.</summary>
    /// <param name="key">The key of the save UI text to localize.</param>
    /// <returns>The localized text.</returns>
    protected string localize(string key) {
        return FhApi.Localization.localize($"{typeof(FhSaveUi)}.{key}");
    }

    /// <summary>Begin a fade-out.</summary>
    /// <param name="action">The action to end the fade-out with.</param>
    protected void fade_out(Action action) {
        _fade.restart(
            _fade.get_color(),
            COLOR_BLACK,
            _fade_length * _fade.progress,
            action
        );
    }

    /// <summary>Get the volume a move sound should play at.</summary>
    /// <returns>The desired volume.</returns>
    protected float get_move_volume() {
        float min_volume = ACTION_VOLUME * 0.5f;

        return min_volume + (1f - _audio_fade_helper.progress) * (1f - min_volume);
    }

    /// <summary>Change the UI mode.</summary>
    /// <param name="new_mode">The UI mode to switch to.</param>
    protected void change_mode(UiMode new_mode) {
        _scrollable_saves.reset();
        _scrollable_sets .reset();
        _focus = UiFocus.LIST;
        _mode  = new_mode;
    }

    /// <summary>Change the active set to the one with the given name.</summary>
    /// <param name="set_name"></param>
    protected void switch_set(string set_name) {
        FhApi.Saves.sets_switch(set_name);

        try_load_textures();

        change_mode(UiMode.SAVE_LIST);
    }


    /* ===== Textures ===== */

    /// <summary>Attempt to load all of the textures the UI requires to display properly.</summary>
    /// <returns>Whether all textures have been successfully loaded.</returns>
    protected bool try_load_textures() {
        _loaded_all_textures = true;
        foreach (FhTexture texture in textures) {
            if (!FhApi.Resources.load_texture(texture)) {
                _loaded_all_textures = false;
            }
        }

        return _loaded_all_textures;
    }

    /// <summary>Unload all of the textures the UI requires to display properly.</summary>
    protected void unload_textures(Span<FhTexture> texture_list) {
        foreach (FhTexture texture in texture_list) {
            if (FhApi.Resources.unload_texture(texture))
                _loaded_all_textures = false;
            else
                _logger.Warning($"Failed to unload texture: {texture.path}");
        }
    }


    /* ===== Input handling ===== */

    /// <summary>Detect whether the player hovered an on-screen area with the mouse.</summary>
    /// <param name="bounds">The area to detect hovering over.</param>
    /// <param name="require_movement">Whether the method should return <c>false</c> if the mouse is not moving.</param>
    /// <returns>Whether the specified area was hovered.</returns>
    protected bool mouse_hovered(Rect bounds, bool require_movement = true) {
        return should_handle_input
            && FhApi.Gui.mouse_hovering(bounds, require_movement);
    }

    /// <summary>Detect whether the player clicked an on-screen area with the mouse.</summary>
    /// <param name="bounds">The area to detect clicks in.</param>
    /// <param name="button">The mouse button to detect clicks from.</param>
    /// <param name="repeat">Whether the method should repeatedly return <c>true</c> for held inputs.</param>
    /// <returns>Whether the specified click was performed.</returns>
    protected bool mouse_clicked(
        Rect             bounds,
        ImGuiMouseButton button = ImGuiMouseButton.Left,
        bool             repeat = false
    ) {
        return should_handle_input
            && FhApi.Gui.mouse_clicked(bounds, button, repeat);
    }

    /// <summary>Handle input for the save and set lists.</summary>
    /// <returns>Whether input handling should be interrupted.</returns>
    protected bool handle_input_list() {
        if (_mode == UiMode.SAVE_LIST) {
            if (_scrollable_saves.max == 0) {
                _focus = UiFocus.ACTIVE_SET;
                return false;
            }

            if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_up)
             && _current_scrollable.hovered == 0
            ) {
                FhApi.Audio.play_sound(sound_id_action, ACTION_VOLUME);
                _focus = UiFocus.ACTIVE_SET;
                return true;
            }
        }

        int old_hovered = _current_scrollable.hovered;

        bool scrollable_input_held = _current_scrollable.handle_input();

        if (_current_scrollable.hovered != old_hovered) {
            FhApi.Audio.play_sound(sound_id_move, volume: get_move_volume());
            _audio_fade_helper.tick(1f);
        }

        if (scrollable_input_held) {
            _audio_fade_helper_restart = false;
        }

        if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_confirm)) {
            int hovered = _current_scrollable.hovered;

            if (_mode == UiMode.SAVE_LIST) {
                FhApi.Audio.play_sound(sound_id_action, ACTION_VOLUME);

                if (is_saving && hovered == 0) {
                    fade_out(() => FhApi.Saves.impl_execute(0));
                }
                else {
                    if (FhApi.Saves.sets_get_ui_data(hovered, out FhSaveUiData data)) {
                        fade_out(() => FhApi.Saves.impl_execute(data.slot));
                    }
                }

                return true;
            }

            if (_mode == UiMode.SET_SWAP) {
                FhApi.Audio.play_sound(sound_id_set_switch);

                string hovered_set = _set_list[hovered];
                switch_set(hovered_set);

                return true;
            }
        }

        return false;
    }

    /// <summary>Handle input for the Change Set button</summary>
    /// <returns>Whether input handling should be interrupted.</returns>
    protected bool handle_input_change_set() {
        if (_mode == UiMode.SAVE_LIST && FhApi.Gui.is_any_pressed(FhApi.Gui.keys_down) && _current_scrollable.max > 0) {
            FhApi.Audio.play_sound(sound_id_action, ACTION_VOLUME);

            _focus = UiFocus.LIST;
            _current_scrollable.hovered = _current_scrollable.current;

            return true;
        }

        if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_confirm)) {
            FhApi.Audio.play_sound(sound_id_set_switch);
            change_mode(UiMode.SET_SWAP);
            return true;
        }

        return false;
    }

    /// <summary>Handle non-mouse input.</summary>
    /// <remarks>The scroll wheel is handled here as well.</remarks>
    protected void handle_input() {
        if (!should_handle_input) return;

        if (_focus == UiFocus.LIST       && handle_input_list())       return;
        if (_focus == UiFocus.ACTIVE_SET && handle_input_change_set()) return;

        if (FhApi.Gui.is_any_pressed(FhApi.Gui.keys_cancel)) {
            FhApi.Audio.play_sound(sound_id_cancel);

            if (_mode == UiMode.SET_SWAP)
                change_mode(UiMode.SAVE_LIST);
            else
                fade_out(() => FhApi.Saves.impl_abort());
        }
    }


    /* ===== Rendering ===== */

    /// <summary>Render the UI.</summary>
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

    /// <summary>Render the background.</summary>
    protected abstract void ui_background();

    /// <summary>Render the help bar and help text.</summary>
    protected abstract void ui_help();

    /// <summary>Render the active set name and save count.</summary>
    protected abstract void ui_savecount();

    /// <summary>Render the cursor.</summary>
    /// <param name="target_pos">The position the cursor should point at.</param>
    /// <param name="overlap">Whether the cursor should overlap its target a little.</param>
    protected abstract void ui_cursor(Vector2 target_pos, bool overlap = true);

    /// <summary>Render the Change Set button.</summary>
    protected abstract void ui_change_set();

    /// <summary>Render the list of available sets.</summary>
    protected abstract void ui_setlist();

    /// <summary>Render a set in the list of available sets.</summary>
    /// <param name="set">The set to render a button for.</param>
    /// <param name="global_idx">The index of the set in the set list.</param>
    /// <param name="local_idx">The index of the set on screen.</param>
    protected abstract void ui_set(FhSaveSet set, int global_idx, int local_idx);

    /// <summary>Render the list of saves in the active set.</summary>
    protected abstract void ui_savelist();

    /// <summary>Render the message displayed when no saves exist in the active set.</summary>
    protected abstract void ui_no_saves();

    /// <summary>Render a save in the list of saves.</summary>
    /// <param name="save">The save to render a button for.</param>
    /// <param name="global_idx">The index of the save in the save list.</param>
    /// <param name="local_idx">The index of the save on screen.</param>
    protected abstract void ui_save(FhSaveUiData save, int global_idx, int local_idx);

    /// <summary>Render details for a given FFX save.</summary>
    /// <param name="save">The save to render details for.</param>
    /// <param name="save_bounds">The inner bounds of the save.</param>
    /// <param name="header_bounds">The bounds to fit the header details in.</param>
    /// <param name="info_bounds">The bounds to fit the info details in.</param>
    protected abstract void ui_save_details_x(
        FhSaveUiData save,
        Rect save_bounds,
        Rect header_bounds,
        Rect info_bounds
    );

    /// <summary>Render details for a given FFX-2 save.</summary>
    /// <param name="save">The save to render details for.</param>
    /// <param name="save_bounds">The inner bounds of the save.</param>
    /// <param name="header_bounds">The bounds to fit the header details in.</param>
    /// <param name="info_bounds">The bounds to fit the info details in.</param>
    protected abstract void ui_save_details_x2(
        FhSaveUiData save,
        Rect save_bounds,
        Rect header_bounds,
        Rect info_bounds
    );

    /// <summary>Render details for a given FFX-2 LAST MISSION save.</summary>
    /// <param name="save">The save to render details for.</param>
    /// <param name="save_bounds">The inner bounds of the save.</param>
    /// <param name="header_bounds">The bounds to fit the header details in.</param>
    /// <param name="info_bounds">The bounds to fit the info details in.</param>
    protected abstract void ui_save_details_lm(
        FhSaveUiData save,
        Rect save_bounds,
        Rect header_bounds,
        Rect info_bounds
    );

    /// <summary>Render the New Save Data button.</summary>
    /// <param name="bounds">The desired bounds of the button.</param>
    protected abstract void ui_newdata(Rect bounds);

    /// <summary>Render a full-screen fade as provided by <see cref="_fade"/>.</summary>
    protected virtual void ui_fade() {
        if (_fade.is_done) return;

        ImDrawListPtr draw = ImGui.GetBackgroundDrawList();
        draw.AddRectFilled(Vector2.Zero, FhApi.Gui.display_size, _fade.get_color());

        _fade.tick(ImGui.GetIO().DeltaTime);
    }

    /// <summary>Render the scrollbar.</summary>
    protected abstract void ui_scrollbar();

    /// <summary>Render a scrollbar for a given scrollable at a given position.</summary>
    /// <param name="scrollable">The scrollable to render a scrollbar for.</param>
    /// <param name="track_bounds">The bounds of the scrollbar's track.</param>
    /// <param name="triangle_gap">The gap between the scrollbar's track and its arrows.</param>
    protected virtual void ui_scrollbar(
        SteppedScrollable scrollable,
        Rect track_bounds,
        float triangle_gap
    ) {
        if (scrollable.max <= scrollable.visible) {
            return;
        }

        Rect track = track_bounds;

        uint track_color = 0xFF000000;

        Vector2 thumb_margin = new(2f);

        float scrollable_height  = track.size.Y - thumb_margin.Y * 2;
        float visible_percentage = scrollable.visible / (float)scrollable.max;

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

        float progress = scrollable.get_progress();

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

            scrollable.set_progress(progress + progress_delta, true);

            if (ImGui.IsMouseReleased(ImGuiMouseButton.Left)) {
                _scrollbar_dragging = false;
                _scrollbar_held_pos = null;
            }

            return;
        }

        if (mouse_clicked(triangle_top, repeat: true)) {
            FhApi.Audio.play_sound(sound_id_move, volume: get_move_volume());
            _audio_fade_helper.tick(1f);

            scrollable.move_hover(-1);
        }

        if (mouse_clicked(triangle_bottom, repeat: true)) {
            FhApi.Audio.play_sound(sound_id_move, volume: get_move_volume());
            _audio_fade_helper.tick(1f);

            scrollable.move_hover(1);
        }

        if ((mouse_hovered(triangle_top, false) || mouse_hovered(triangle_bottom, false))
         && ImGui.IsMouseDown(ImGuiMouseButton.Left)
           ) {
            _audio_fade_helper_restart = false;
        }
    }
}
