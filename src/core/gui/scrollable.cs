// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Gui;

/// <summary>
///     Represents and controls a one-dimensional scrollable list of elements.
/// </summary>
/// <remarks>
///     A Scrollable object will not render the list it represents in any way.
///     It is only meant to provide a unified way of controlling such lists in custom UI.
/// </remarks>
public class Scrollable {
    /// <summary>The index currently at the top of the scrollable.</summary>
    public int current;

    /// <summary>The maximum index of the scrollable.</summary>
    public int max;

    /// <summary>The amount of indices of the scrollable visible at once.</summary>
    public int visible;

    /// <summary>The index currently hovered over/selected by the user in the scrollable.</summary>
    public int hovered;

    /// <summary>Reset the scrollable to its initial state.</summary>
    public void reset() {
        current = 0;
        hovered = 0;
    }

    /// <summary>Get the first index currently visible in the scrollable.</summary>
    public int get_clip_start() {
        return current;
    }

    /// <summary>Get the value one after the last index currently visible in the scrollable.</summary>
    public int get_clip_end() {
        return int.Min(current + visible, max);
    }

    /// <summary>Determine whether a given index is currently visible in the scrollable.</summary>
    /// <param name="index">The index to check for visibility.</param>
    /// <returns>Whether the given index is currently visible in the scrollable.</returns>
    public bool is_within_clip(int index) {
        return get_clip_start() <= index && index < get_clip_end();
    }

    /// <summary>Calculate how far through the scrollable the user has scrolled.</summary>
    /// <returns>How far through the scrollable the currently visible range of indices is, between 0 and 1.</returns>
    /// <remarks>
    ///     When compiled in Debug, this may return values lower than 0 or greater than 1,
    ///     so as to make debugging easier.
    /// </remarks>
    public float get_progress() {
        if (max <= visible) return 0;

        // Slightly weird math: we technically only scroll through the first visible items,
        // so we must reduce the amount of max items by how many are visible at once.
        float progress = current / float.Max(0, max - visible);

        // When debugging, a visual glitch in the scrollbar may be useful for identifying an issue,
        // so we only clamp in release.
#if !DEBUG
        progress = float.Clamp(progress, 0f, 1f);
#endif

        return progress;
    }

    /// <summary>
    ///     Set what the currently visible range of indices in the scrollable is
    ///     by providing a percent value (between 0 and 1).
    /// </summary>
    /// <param name="value">
    ///     How far through the scrollable, in percent,
    ///     the new visible range of indices should be.
    /// </param>
    /// <param name="preserve_hover">
    ///     Whether the hovered index should be preserved
    ///     relative to the visible range of indices.
    /// </param>
    /// <remarks>
    ///     The new progress value will be safely clamped to between 0 and 1.
    ///     This will safely move the hovered value to fit within the new range of visible indices.
    /// </remarks>
    public void set_progress(float value, bool preserve_hover = false) {
        value = float.Clamp(value, 0f, 1f);

        int old_current = current;
        int hover_diff  = hovered - current;

        current = (int)float.Round(value * int.Max(0, max - visible));
        current = int.Clamp(current, 0, int.Max(0, max - visible));

        if (preserve_hover) {
            hovered = current + hover_diff;
            hovered = int.Clamp(hovered, 0, int.Max(0, max - 1));
        }
        else {
            // Clip the hovered index to the range of visible indices so it never goes off-screen
            if (int.Sign(current - old_current) > 0) {
                hovered = get_clip_start();
            }
            else {
                hovered = get_clip_end() - 1;
            }
        }
    }

    /// <summary>Scroll through the scrollable by the specified amount.</summary>
    /// <param name="amount">The amount to scroll by.</param>
    /// <remarks>This will safely move the hovered value to fit within the new range of visible indices.</remarks>
    public void scroll(int amount) {
        int old_current = current;

        current += amount;
        current = int.Clamp(current, 0, int.Max(0, max - visible));

        if (is_within_clip(hovered)) {
            if (current != old_current) {
                hovered += amount;
                hovered = int.Clamp(hovered, 0, int.Max(0, max - visible));
            }
        }
        else {
            // Clip the hovered index to the range of visible indices so it never goes off-screen
            if (int.Sign(amount) > 0) {
                hovered = get_clip_start();
            }
            else {
                hovered = get_clip_end() - 1;
            }
        }
    }

    /// <summary>Move the currently hovered index in the scrollable by a given amount.</summary>
    /// <param name="amount">The amount to move the hovered index by.</param>
    /// <remarks>
    ///     This will safely adjust the range of currently visible indices
    ///     to include the new hovered index.
    /// </remarks>
    public void move_hover(int amount) {
        hovered += amount;
        hovered = int.Clamp(hovered, 0, int.Max(0, max - 1));

        if (is_within_clip(hovered)) return;

        // Move the clip to the hovered index
        if (hovered < get_clip_start()) {
            current = hovered;
        }
        else {
            current = hovered - visible + 1;
        }
    }

    /// <summary>Scroll to the previous set of visible indices.</summary>
    /// <param name="just_pressed">Whether the page up button was just pressed.</param>
    /// <remarks>
    ///     If it is no longer visible, this will also set the hovered index
    ///     to the start of the visible range for improved UX.
    /// </remarks>
    public void scroll_page_up(bool just_pressed) {
        if (just_pressed && (current == 0 || max <= visible)) {
            hovered = 0;
            return;
        }

        current -= visible;
        current = int.Clamp(current, 0, int.Max(0, max - visible));

        if (!is_within_clip(hovered)) {
            hovered = get_clip_start();
        }
    }

    /// <summary>Scroll to the next set of visible indices.</summary>
    /// <param name="just_pressed">Whether the page down button was just pressed.</param>
    /// <remarks>
    ///     If it is no longer visible, this will also set the hovered index
    ///     to the end of the visible range for improved UX.
    /// </remarks>
    public void scroll_page_down(bool just_pressed) {
        if (just_pressed && (current == max - visible || max <= visible)) {
            hovered = max - 1;
            return;
        }

        current += visible;
        current = int.Clamp(current, 0, int.Max(0, max - visible));

        if (!is_within_clip(hovered)) {
            hovered = get_clip_end() - 1;
        }
    }

    /// <summary>Scroll to the beginning of the scrollable.</summary>
    /// <remarks>
    ///     This will also set the hovered index to the beginning
    ///     of the scrollable for improved UX.
    /// </remarks>
    public void scroll_begin() {
        current = hovered = 0;
    }

    /// <summary>Scroll to the end of the scrollable.</summary>
    /// <remarks>
    ///     This will also set the hovered index to the end
    ///     of the scrollable for improved UX.
    /// </remarks>
    public void scroll_end() {
        current = max <= visible ? 0 : max - visible;
        hovered = max - 1;
    }

    /// <summary>Handle scrollable input.</summary>
    /// <remarks>This method should be called at most once per ImGui frame whenever desired.</remarks>
    /// <returns>Whether Scrollable input was held, even if it was not used.</returns>
    public bool handle_input() {
        // Various scrolling methods
        ImGuiKey[] up_keys   = FhApi.Gui.keys_up;
        ImGuiKey[] down_keys = FhApi.Gui.keys_down;

        ImGuiKey[] pg_up_keys = [
            ImGuiKey.PageUp,
            ImGuiKey.GamepadL1,
            .. FhApi.Gui.keys_left,
        ];

        ImGuiKey[] pg_dn_keys = [
            ImGuiKey.PageDown,
            ImGuiKey.GamepadR1,
            .. FhApi.Gui.keys_right,
        ];

        ImGuiKey[] home_keys = [
            ImGuiKey.Home,
            ImGuiKey.GamepadL2,
        ];

        ImGuiKey[] end_keys = [
            ImGuiKey.End,
            ImGuiKey.GamepadR2,
        ];

        bool hover_up   = FhApi.Gui.is_any_pressed(up_keys,   true);
        bool hover_down = FhApi.Gui.is_any_pressed(down_keys, true);
        int  hover      = (hover_up ? 1 : 0) - (hover_down ? 1 : 0);

        float mouse_wheel = ImGui.GetIO().MouseWheel;

        bool scroll_pg_up = FhApi.Gui.is_any_pressed(pg_up_keys);
        bool scroll_pg_dn = FhApi.Gui.is_any_pressed(pg_dn_keys);

        bool scroll_pg_up_held = FhApi.Gui.is_any_pressed(pg_up_keys, true);
        bool scroll_pg_dn_held = FhApi.Gui.is_any_pressed(pg_dn_keys, true);

        bool scroll_to_start = FhApi.Gui.is_any_pressed(home_keys);
        bool scroll_to_end   = FhApi.Gui.is_any_pressed(end_keys);

        ImGui.GetIO().WantCaptureKeyboard |=
             hover_up
         ||  hover_down
         || (scroll_pg_up || scroll_pg_up_held)
         || (scroll_pg_dn || scroll_pg_dn_held)
         ||  scroll_to_start
         ||  scroll_to_end;

        if (hover > 0) {
            move_hover(-1);
        }

        if (hover < 0) {
            move_hover(1);
        }

        if (mouse_wheel > 0) {
            scroll(-1);
        }

        if (mouse_wheel < 0) {
            scroll(1);
        }

        if (scroll_pg_up || scroll_pg_up_held) {
            scroll_page_up(scroll_pg_up);
        }

        if (scroll_pg_dn || scroll_pg_dn_held) {
            scroll_page_down(scroll_pg_dn);
        }

        if (scroll_to_start) {
            scroll_begin();
        }

        if (scroll_to_end) {
            scroll_end();
        }

        return FhApi.Gui.is_any_down(
            [
                .. up_keys,
                .. down_keys,
                .. pg_up_keys,
                .. pg_dn_keys,
                .. home_keys,
                .. end_keys,
            ]
        );
    }
}
