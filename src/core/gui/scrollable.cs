// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Gui;

/// <summary>
///     Represents and controls a one-dimensional scrollable list of positions.
/// </summary>
/// <remarks>
///     A BaseScrollable object will not render the list it represents in any way.
///     It is only meant to provide a unified way of controlling such lists in custom UI.
/// </remarks>
public abstract class BaseScrollable;

/// <inheritdoc cref="BaseScrollable"/>
public abstract class BaseScrollable<T> : BaseScrollable
    where T : INumber<T> {

    /// <summary>The position at the top of the visible range of the scrollable.</summary>
    public T current { get; set; } = T.Zero;

    /// <summary>The maximum position of the scrollable.</summary>
    public T max { get; set; } = T.Zero;

    /// <summary>The length of the range of positions the scrollable can display at once.</summary>
    public T visible { get; set; } = T.Zero;

    /// <summary>The amount a single-step scroll input should scroll.</summary>
    public T scroll_step_size { get; set; } = T.One;

    /// <summary>The maximum position of the top of the visible range of the scrollable.</summary>
    protected T max_current => max - visible;

    protected ImGuiKey[] keys_pg_up => [
        ImGuiKey.PageUp,
        ImGuiKey.GamepadL1,
        .. FhApi.Gui.keys_left,
    ];

    protected ImGuiKey[] keys_pg_dn => [
        ImGuiKey.PageDown,
        ImGuiKey.GamepadR1,
        .. FhApi.Gui.keys_right,
    ];

    protected ImGuiKey[] keys_home => [
        ImGuiKey.Home,
        ImGuiKey.GamepadL2,
    ];

    protected ImGuiKey[] keys_end => [
        ImGuiKey.End,
        ImGuiKey.GamepadR2,
    ];

    /// <summary>Resets the scrollable to its initial state.</summary>
    public virtual void reset() {
        current = T.Zero;
    }

    /// <summary>Get the first visible position of the scrollable.</summary>
    public T get_clip_start() {
        return current;
    }

    /// <summary>Get the last visible position of the scrollable.</summary>
    /// <remarks>This is the <em>exclusive</em> end of the scrollable.</remarks>
    public T get_clip_end() {
        return T.Min(current + visible, max);
    }

    /// <summary>Determine whether a given position is visible in the scrollable.</summary>
    /// <param name="position">The position to check for visibility.</param>
    /// <returns>Whether the given position is visible in the scrollable.</returns>
    public bool is_within_clip(T position) {
        return get_clip_start() <= position && position < get_clip_end();
    }

    /// <summary>Calculate how far through the scrollable the user has scrolled.</summary>
    /// <returns>How far through the scrollable the visible range is, between 0 and 1.</returns>
    /// <remarks>
    ///     When compiled in Debug, this may return values lower than 0 or greater than 1,
    ///     so as to make debugging easier.
    /// </remarks>
    public virtual float get_progress() {
        if (max <= visible) return 0;

        if (current > max) return 1f;

        float f_current = float.CreateChecked(current);
        float f_max     = float.CreateChecked(max_current);

        float progress = f_current / float.Max(1f, f_max);

        // When debugging, a visual glitch in the scrollbar may be useful for identifying an issue,
        // so we only clamp in release.
#if !DEBUG
        progress = float.Clamp(progress, 0f, 1f);
#endif

        return progress;
    }

    /// <summary>
    ///     Set what the visible range in the scrollable is
    ///     by providing a percent value (between 0 and 1).
    /// </summary>
    /// <param name="value">
    ///     How far through the scrollable, in percent,
    ///     the new visible range should be.
    /// </param>
    /// <remarks>
    ///     The new progress value will be safely clamped to between 0 and 1.
    /// </remarks>
    public virtual void set_progress(float value) {
        value = float.Clamp(value, 0f, 1f);

        current = T.CreateChecked(float.Round(value * float.CreateChecked(T.Max(T.Zero, max_current))));
        current = T.Clamp(current, T.Zero, T.Max(T.Zero, max_current));
    }

    /// <summary>Scroll through the scrollable by the specified amount.</summary>
    /// <param name="amount">The amount to scroll by.</param>
    public virtual void scroll(T amount) {
        current += amount;
        current = T.Clamp(current, T.Zero, T.Max(T.Zero, max_current));
    }

    /// <summary>Scroll to the previous range of visible position.</summary>
    public virtual void scroll_page_up() {
        current -= visible;
        current = T.Clamp(current, T.Zero, T.Max(T.Zero, max_current));
    }

    /// <summary>Scroll to the next set of visible positions.</summary>
    public virtual void scroll_page_down() {
        current += visible;
        current = T.Clamp(current, T.Zero, T.Max(T.Zero, max_current));
    }

    /// <summary>Scroll to the beginning of the scrollable.</summary>
    public virtual void scroll_begin() {
        current = T.Zero;
    }

    /// <summary>Scroll to the end of the scrollable.</summary>
    public virtual void scroll_end() {
        current = max <= visible ? T.Zero : max_current;
    }

    /// <summary>Handle scrollable input.</summary>
    /// <remarks>This method should be called at most once per ImGui frame whenever desired.</remarks>
    /// <returns>Whether Scrollable input was held, even if it was not used.</returns>
    public virtual bool handle_input() {
        // Various scrolling methods
        float mouse_wheel = ImGui.GetIO().MouseWheel;

        bool scroll_pg_up = FhApi.Gui.is_any_pressed(keys_pg_up);
        bool scroll_pg_dn = FhApi.Gui.is_any_pressed(keys_pg_dn);

        bool scroll_pg_up_held = FhApi.Gui.is_any_pressed(keys_pg_up, true);
        bool scroll_pg_dn_held = FhApi.Gui.is_any_pressed(keys_pg_dn, true);

        bool scroll_to_start = FhApi.Gui.is_any_pressed(keys_home);
        bool scroll_to_end   = FhApi.Gui.is_any_pressed(keys_end);

        ImGui.GetIO().WantCaptureKeyboard |=
               scroll_pg_up || scroll_pg_up_held
            || scroll_pg_dn || scroll_pg_dn_held
            || scroll_to_start
            || scroll_to_end;

        if (mouse_wheel > 0) {
            scroll(-scroll_step_size);
        }

        if (mouse_wheel < 0) {
            scroll(scroll_step_size);
        }

        if (scroll_pg_up || scroll_pg_up_held) {
            scroll_page_up();
        }

        if (scroll_pg_dn || scroll_pg_dn_held) {
            scroll_page_down();
        }

        if (scroll_to_start) {
            scroll_begin();
        }

        if (scroll_to_end) {
            scroll_end();
        }

        return FhApi.Gui.is_any_down([
            .. keys_pg_up,
            .. keys_pg_dn,
            .. keys_home,
            .. keys_end,
        ]);
    }
}

/// <summary>
///     Represents and controls a one-dimensional scrollable list of positions.
/// </summary>
/// <remarks>
///     A Scrollable object will not render the list it represents in any way.
///     It is only meant to provide a unified way of controlling such lists in custom UI.
/// </remarks>
public class Scrollable : BaseScrollable<float> {
    public Scrollable(float max = 0, float visible = 0) {
        this.max     = max;
        this.visible = visible;
    }
}

/// <summary>
///     Represents and controls a one-dimensional scrollable list of positions
///     with hard steps between elements.
/// </summary>
/// <remarks>
///     A SteppedScrollable object will not render the list it represents in any way.
///     It is only meant to provide a unified way of controlling such lists in custom UI.
/// </remarks>
public class SteppedScrollable : BaseScrollable<int> {
    public SteppedScrollable(int max = 0, int visible = 0) {
        this.max     = max;
        this.visible = visible;
    }

    /// <summary>The currently hovered position in the scrollable.</summary>
    public int hovered { get; set; }

    public override void reset() {
        current = 0;
        hovered = 0;
    }

    /// <summary>
    ///     Set what the visible range in the scrollable is
    ///     by providing a percent value (between 0 and 1).
    /// </summary>
    /// <param name="value">
    ///     How far through the scrollable, in percent,
    ///     the new visible range should be.
    /// </param>
    /// <param name="preserve_hover">
    ///     Whether the hovered position should be preserved
    ///     relative to the visible range.
    /// </param>
    /// <remarks>
    ///     The new progress value will be safely clamped to between 0 and 1.
    ///     This will safely move the hovered value to fit within the new visible range.
    /// </remarks>
    public void set_progress(float value, bool preserve_hover = false) {
        int old_current = current;
        int hover_diff  = hovered - current;

        base.set_progress(value);

        if (preserve_hover) {
            hovered = current + hover_diff;
            hovered = int.Clamp(hovered, 0, int.Max(0, max - 1));
        }
        else {
            // Clip the hovered position to the visible range so it never goes off-screen
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
    /// <remarks>This will safely move the hovered value to fit within the new range visible of positions.</remarks>
    public override void scroll(int amount) {
        int old_current = current;

        base.scroll(amount);

        if (is_within_clip(hovered)) {
            if (current != old_current) {
                hovered += amount;
                hovered = int.Clamp(hovered, 0, int.Max(0, max - visible));
            }
        }
        else {
            // Clip the hovered position to the visible range so it never goes off-screen
            if (int.Sign(amount) > 0) {
                hovered = get_clip_start();
            }
            else {
                hovered = get_clip_end() - 1;
            }
        }
    }

    /// <summary>Move the currently hovered position by a given amount.</summary>
    /// <param name="amount">The amount to move the hovered position by.</param>
    /// <remarks>
    ///     This will safely adjust the visible range
    ///     to include the new hovered positions.
    /// </remarks>
    public void move_hover(int amount) {
        hovered += amount;
        hovered = int.Clamp(hovered, 0, int.Max(0, max - 1));

        if (is_within_clip(hovered)) return;

        // Move the clip to the hovered position
        if (hovered < get_clip_start()) {
            current = hovered;
        }
        else {
            current = hovered - visible + 1;
        }
    }

    /// <summary>Scroll to the previous visible range.</summary>
    /// <param name="just_pressed">Whether the page up button was just pressed.</param>
    /// <remarks>
    ///     If it is no longer visible, this will also set the hovered position
    ///     to the start of the visible range for improved UX.
    /// </remarks>
    public override void scroll_page_up() {
        bool just_pressed = FhApi.Gui.is_any_pressed(keys_pg_up);
        if (just_pressed && (current == 0 || max <= visible)) {
            hovered = 0;
            return;
        }

        base.scroll_page_up();

        if (!is_within_clip(hovered)) {
            hovered = get_clip_start();
        }
    }

    /// <summary>Scroll to the next visible range.</summary>
    /// <param name="just_pressed">Whether the page down button was just pressed.</param>
    /// <remarks>
    ///     If it is no longer visible, this will also set the hovered index
    ///     to the end of the visible range for improved UX.
    /// </remarks>
    public override void scroll_page_down() {
        bool just_pressed = FhApi.Gui.is_any_pressed(keys_pg_dn);
        if (just_pressed && (current == max - visible || max <= visible)) {
            hovered = max - 1;
            return;
        }

        base.scroll_page_down();

        if (!is_within_clip(hovered)) {
            hovered = get_clip_end() - 1;
        }
    }

    /// <summary>Scroll to the beginning of the scrollable.</summary>
    /// <remarks>
    ///     This will also set the hovered index to the beginning
    ///     of the scrollable for improved UX.
    /// </remarks>
    public override void scroll_begin() {
        base.scroll_begin();
        hovered = 0;
    }

    /// <summary>Scroll to the end of the scrollable.</summary>
    /// <remarks>
    ///     This will also set the hovered index to the end
    ///     of the scrollable for improved UX.
    /// </remarks>
    public override void scroll_end() {
        base.scroll_end();
        hovered = max - 1;
    }

    /// <summary>Handle scrollable input.</summary>
    /// <remarks>This method should be called at most once per ImGui frame whenever desired.</remarks>
    /// <returns>Whether Scrollable input was held, even if it was not used.</returns>
    public override bool handle_input() {
        // Various scrolling methods
        ImGuiKey[] keys_up   = FhApi.Gui.keys_up;
        ImGuiKey[] keys_down = FhApi.Gui.keys_down;

        bool hover_up   = FhApi.Gui.is_any_pressed(keys_up,   true);
        bool hover_down = FhApi.Gui.is_any_pressed(keys_down, true);
        int  hover      = (hover_up ? 1 : 0) - (hover_down ? 1 : 0);

        ImGui.GetIO().WantCaptureKeyboard |= hover_up || hover_down;

        if (hover > 0) {
            move_hover(-1);
        }

        if (hover < 0) {
            move_hover(1);
        }

        bool handled_base = base.handle_input();

        return handled_base || FhApi.Gui.is_any_down([
            .. keys_up,
            .. keys_down,
        ]);
    }
}
