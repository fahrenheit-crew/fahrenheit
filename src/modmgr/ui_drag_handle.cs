// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Tools.ModManager;

internal static unsafe partial class FhModManagerUI {
    private static FhInstalledMod? _dragging_mod;

    private sealed record FhPendingPreviewMove(int FromIndex, int ToIndex);

    private static FhPendingPreviewMove? _pending_preview_move;

    private sealed record FhPendingLoadOrderDrop(FhInstalledMod Mod, int TargetIndex);

    private static FhPendingLoadOrderDrop? _pending_load_order_drop;

    // How long the "pop" on grab takes to reach full size.
    private const double    DRAG_HANDLE_GRAB_POP_SECONDS = 0.12;
    private static double   _drag_handle_grabbed_at;
    private static int      _drag_start_index;

    /// <summary>
    ///    Renders the drag handle for a mod in the mod list, allowing the user to reorder mods by dragging and dropping.
    /// </summary>
    private static void _render_drag_handle(FhInstalledMod mod, int display_index, int mod_count, float row_height) {
        Vector2 size = new(ImGui.GetFrameHeight() * 1.1F, ImGui.GetFrameHeight() * 0.9F);

        float vertical_offset   = Math.Max(0F, (row_height - size.Y) / 2F);
        float horizontal_offset = Math.Max(0F, (ImGui.GetContentRegionAvail().X - size.X) / 2F);

        ImGui.SetCursorPos(ImGui.GetCursorPos() + new Vector2(horizontal_offset, vertical_offset));

        _drag_mod(mod, display_index, mod_count, row_height, size, out Vector2 center);
        _visual_feedback(mod, size, center);

        static void _drag_mod(FhInstalledMod mod, int display_index, int mod_count, float row_height, Vector2 size, out Vector2 center) {
            Vector2 top_left = ImGui.GetCursorScreenPos();
            center           = top_left + (size / 2F);
            ImGui.InvisibleButton($"##DragHandle.{mod.Manifest.Id}", size);

            if (mod.HasValidManifest) {
                if (ImGui.IsItemActivated()) {
                    _dragging_mod = mod;
                    _drag_start_index = display_index;
                    _drag_handle_grabbed_at = ImGui.GetTime();
                }

                bool is_dragging_this = _dragging_mod?.Manifest.Id == mod.Manifest.Id;

                if (is_dragging_this && ImGui.IsItemActive()) {
                    float drag_delta_y = ImGui.GetMouseDragDelta(ImGuiMouseButton.Left).Y;
                    int   row_offset   = (int)MathF.Round(drag_delta_y / row_height);
                    int   target_index = Math.Clamp(_drag_start_index + row_offset, 0, mod_count - 1);

                    if (target_index != display_index) {
                        _pending_preview_move = new(display_index, target_index);
                    }
                }

                if (is_dragging_this && ImGui.IsItemDeactivated()) {
                    _pending_load_order_drop = new(mod, display_index);
                    _dragging_mod = null;
                }
            }
        }

        static void _visual_feedback(FhInstalledMod mod, Vector2 size, Vector2 center) {
            bool hovered = ImGui.IsItemHovered();
            bool active  = ImGui.IsItemActive();

            float grab_t = 0F;
            if (active) {
                double grabbed_elapsed = ImGui.GetTime() - _drag_handle_grabbed_at;
                float  grabbed_linear  = Math.Clamp((float)(grabbed_elapsed / DRAG_HANDLE_GRAB_POP_SECONDS), 0F, 1F);
                grab_t = 1F - MathF.Pow(1F - grabbed_linear, 3F);
            }

            float visual_scale      = 1F + (0.25F * grab_t);
            ImGuiStylePtr style     = ImGui.GetStyle();
            Vector4 text            = style.Colors[(int)ImGuiCol.Text];
            Vector4 text_muted      = style.Colors[(int)ImGuiCol.TextDisabled];
            ImDrawListPtr draw_list = ImGui.GetWindowDrawList();
            Vector4 bar_color       = mod.HasValidManifest && (hovered || active) ? text : text_muted;
            uint bar_color_u32      = ImGui.GetColorU32(bar_color);
            float scaled_height     = size.Y * visual_scale;
            float bar_width         = size.X * visual_scale * 0.7F;
            float bar_x             = center.X - (bar_width / 2F);
            float bar_step          = scaled_height / 4F;
            float bars_top          = center.Y - (scaled_height / 2F);

            for (int i = 1; i <= 3; i++) {
                float y = bars_top + (bar_step * i);
                draw_list.AddLine(new Vector2(bar_x, y), new Vector2(bar_x + bar_width, y), bar_color_u32, 2F);
            }

            if (hovered) {
                ImGui.SetTooltip(mod.HasValidManifest ? "Drag to reorder" : "This mod cannot be reordered");
            }
        }
    }
}
