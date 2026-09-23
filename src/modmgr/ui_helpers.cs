// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Tools.ModManager;

/// <summary>
///    A simple struct for returning a success/failure result with an optional message.
/// </summary>
internal readonly record struct ResultsMessage(bool Success, string Message);

internal static unsafe partial class FhModManagerUI {
    /// <summary>
    ///    Sets the status bar message and whether it's an error, and records the time it was set for fade-out purposes.
    /// </summary>
    private static void _set_status(string message, bool is_error = false) {
        _status = message;
        _status_is_error = is_error;
        _status_set_at = ImGui.GetTime();
    }

    /// <summary>
    ///    Rescans the game and Fahrenheit directories for mods, updating the catalog. 
    ///    This is called whenever the mod list requires updating.
    /// </summary>
    private static void _rescan_mods() {
        _catalog = FhModScanner.scan(_settings.GameDirectory);
    }

    /// <summary>
    ///    Centers the next ImGui window on the main viewport, typically modals.
    ///    Width based on a fraction of the viewport's width.
    ///    Clamped between min and max values, and an optional height.
    /// </summary>
    private static void _center_next_window(
        float width_fraction,
        float min_width,
        float max_width,
        float height = 0F) {
        ImGuiViewportPtr viewport = ImGui.GetMainViewport();

        float width = Math.Clamp(viewport.Size.X * width_fraction, min_width, max_width);

        Vector2 center = viewport.Pos + viewport.Size / 2F;

        ImGui.SetNextWindowPos(center, pivot: new Vector2(0.5F, 0.5F));
        ImGui.SetNextWindowSize(new Vector2(width, height));
    }

    /// <summary>
    ///    Returns the width of a button based on its label, including padding and a small extra margin.
    /// </summary>
    private static float _get_button_width(string label) {
        ImGuiStylePtr style = ImGui.GetStyle();
        return ImGui.CalcTextSize(label).X + (style.FramePadding.X * 2F) + 4F;
    }

    /// <summary>
    ///    Centers the cursor horizontally within the available content region, based on the width of the content to be rendered.
    /// </summary>
    private static void _center_cursor_x(float content_width) {
        float available_width = ImGui.GetContentRegionAvail().X;

        if (content_width < available_width) {
            ImGui.SetCursorPosX(ImGui.GetCursorPosX() + (available_width - content_width) / 2F);
        }
    }

    /// <summary>
    ///    Renders the load order number for a mod, centered in its column.
    /// </summary>
    private static void _render_centered_load_order_number(int position) {
        string text = position.ToString();

        _center_cursor_x(ImGui.CalcTextSize(text).X);
        ImGui.TextDisabled(text);
    }

    /// <summary>
    ///    Renders a block of text that wraps within the available content region, without breaking words.
    /// </summary>
    private static void _text_wrapped(string text) {
        float wrap_position = ImGui.GetCursorPosX() + ImGui.GetContentRegionAvail().X;

        ImGui.PushTextWrapPos(wrap_position);
        ImGui.TextUnformatted(text);
        ImGui.PopTextWrapPos();
    }

    /// <summary>
    ///    Calls <see cref='_text_wrapped'/> in a colored style.
    /// </summary>
    private static void _text_colored_wrapped(Vector4 color, string text) {
        ImGui.PushStyleColor(ImGuiCol.Text, color);
        _text_wrapped(text);
        ImGui.PopStyleColor();
    }

    /// <summary>
    ///    Calls <see cref='_text_colored_wrapped'/> with a disabled (grayed-out) style.
    /// </summary>
    private static void _text_disabled_wrapped(string text) {
        _text_colored_wrapped(ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled], text);
    }

    /// <summary>
    ///    Renders a small status icon (checkmark or X) with an optional tooltip, based on the validity of a setting.
    /// </summary>
    private static void _status_icon(bool is_valid, string? tooltip) {
        float extent = ImGui.GetTextLineHeight();
        Vector2 size = new(extent, extent);

        Vector2 top_left = ImGui.GetCursorScreenPos();
        Vector2 center    = top_left + (size / 2F);

        ImGui.Dummy(size);

        ImDrawListPtr draw_list  = ImGui.GetWindowDrawList();
        uint          color      = ImGui.GetColorU32(is_valid ? ImGuiCol.CheckMark : ImGuiCol.NavCursor);
        float         glyph      = extent * 0.5F;
        float         thickness  = MathF.Max(1F, glyph * 0.3F);

        if (is_valid) {
            Vector2 p1 = center + new Vector2(-glyph * 0.5F, 0F);
            Vector2 p2 = center + new Vector2(-glyph * 0.05F, glyph * 0.45F);
            Vector2 p3 = center + new Vector2(glyph * 0.55F, -glyph * 0.5F);

            draw_list.AddLine(p1, p2, color, thickness);
            draw_list.AddLine(p2, p3, color, thickness);
        }
        else {
            Vector2 half = new(glyph * 0.5F, glyph * 0.5F);

            draw_list.AddLine(center - half, center + half, color, thickness);
            draw_list.AddLine(
                center + new Vector2(half.X, -half.Y),
                center + new Vector2(-half.X, half.Y),
                color,
                thickness);
        }

        if (!string.IsNullOrWhiteSpace(tooltip) && ImGui.IsItemHovered()) {
            ImGui.SetTooltip(tooltip);
        }
    }
}
