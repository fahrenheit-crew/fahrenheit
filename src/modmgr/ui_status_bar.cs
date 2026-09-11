// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Tools.ModManager;

internal static unsafe partial class FhModManagerUI {
    private static string _status = "";
    private static bool   _status_is_error;
    private static double _status_set_at;

    // How long a status message stays fully visible before it starts fading.
    private const double STATUS_BAR_VISIBLE_SECONDS = 4.0;

    // How long the fade-out takes.
    private const double STATUS_BAR_FADE_SECONDS = 0.6;
    private static float STATUS_BAR_VERTICAL_PADDING => 5F;

    private static float _status_bar_height() {
        return ImGui.GetTextLineHeight() + (STATUS_BAR_VERTICAL_PADDING * 2F);
    }

    /// <summary>
    ///    Renders the status bar at the bottom of the main window, with a toast effect for messages.
    /// </summary>
    private static void _render_status_bar() {
        float height       = _status_bar_height();
        float window_width = ImGui.GetWindowWidth();

        ImGui.SetCursorPos(new Vector2(0F, ImGui.GetWindowHeight() - height));
        ImGui.PushStyleColor(ImGuiCol.ChildBg, ImGui.GetStyle().Colors[(int)ImGuiCol.PopupBg]);

        bool statusBar = ImGui.BeginChild("##StatusBar", new Vector2(window_width, height), ImGuiChildFlags.None, ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse);
        if (statusBar) {
            float alpha = _status_alpha();

            if (alpha > 0F) {
                Vector4 color = ImGui.GetStyle().Colors[(int)(_status_is_error ? ImGuiCol.NavCursor : ImGuiCol.CheckMark)];
                color.W *= alpha;

                ImGui.SetCursorPos(new Vector2(ImGui.GetStyle().WindowPadding.X,STATUS_BAR_VERTICAL_PADDING));
                ImGui.PushStyleColor(ImGuiCol.Text, color);
                ImGui.TextUnformatted(_status);
                ImGui.PopStyleColor();
            }
        }

        ImGui.EndChild();
        ImGui.PopStyleColor();
    }

    /// <summary>
    ///    Returns the current alpha value for the status bar, based on how long it's been since the last message was set.
    /// </summary>
    private static float _status_alpha() {
        if (string.IsNullOrWhiteSpace(_status)) {
            return 0F;
        }

        double elapsed = ImGui.GetTime() - _status_set_at;

        if (elapsed < STATUS_BAR_VISIBLE_SECONDS) {
            return 1F;
        }

        double fade_elapsed = elapsed - STATUS_BAR_VISIBLE_SECONDS;

        if (fade_elapsed >= STATUS_BAR_FADE_SECONDS) {
            return 0F;
        }

        return 1F - (float)(fade_elapsed / STATUS_BAR_FADE_SECONDS);
    }
}
