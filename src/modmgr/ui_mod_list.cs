// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Tools.ModManager;

internal static unsafe partial class FhModManagerUI {
    private sealed record FhPendingModToggle(FhInstalledMod Mod, bool Enable);
    private static FhPendingModToggle? _pending_mod_toggle;
    private sealed record FhPendingLoadOrderMove(FhInstalledMod Mod, int Direction);
    private static FhPendingLoadOrderMove? _pending_load_order_move;

    /// <summary>
    ///    Renders the Enabled and Disabled mod panels, each with a scrollable table of mods.
    /// </summary>
    private static void _render_mod_lists() {
        Vector2 available               = ImGui.GetContentRegionAvail();
        float   spacing                 = ImGui.GetStyle().ItemSpacing.X;
        bool stack_panels               = available.X < 760F;
        float status_bar_extra_reserve  = Math.Max(0F, _status_bar_height() - ImGui.GetStyle().WindowPadding.Y);
        float available_height          = Math.Max(0F, available.Y - status_bar_extra_reserve);
        float min_panel_height          = 150F;

        float panel_width;
        float panel_height;

        if (stack_panels) {
            panel_width  = available.X;
            panel_height = Math.Max(min_panel_height, (available_height - spacing) / 2F);
        }
        else {
            float gutter = 6F;
            panel_width  = MathF.Floor((available.X - spacing - gutter) / 2F);
            panel_height = Math.Max(min_panel_height, available_height - (gutter / 3F));
        }

        _render_mod_panel("Enabled Mods", "##EnabledMods", _catalog.Enabled, panel_width, panel_height, true);

        if (!stack_panels) {
            ImGui.SameLine();
        }

        _render_mod_panel("Disabled Mods", "##DisabledMods", _catalog.Disabled, panel_width, panel_height, false);
    }

    /// <summary>
    ///    Renders a single mod panel, which includes a header with the title and mod count,
    /// </summary>
    private static void _render_mod_panel(string title, string child_id, IReadOnlyList<FhInstalledMod> mods, float width, float height, bool show_load_order) {
        bool modPanelHeader = ImGui.BeginChild(child_id, new Vector2(width, height), ImGuiChildFlags.None, ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse);
        if (modPanelHeader) {
            _center_cursor_x(ImGui.CalcTextSize($"{title} ({mods.Count})").X);
            ImGui.TextUnformatted($"{title} ({mods.Count})");
            ImGui.Separator();

            bool modPanelList = ImGui.BeginChild($"{child_id}.List", new Vector2(width, height - ImGui.GetFrameHeightWithSpacing()), ImGuiChildFlags.None, ImGuiWindowFlags.AlwaysVerticalScrollbar);

            if (modPanelList) {
                if (mods.Count == 0) {
                    ImGui.TextDisabled(show_load_order ? "No mods are enabled." : "No disabled mods were found.");
                }
                else {
                    _render_mod_table(child_id, mods, show_load_order);
                }
            }
            ImGui.EndChild();
        }
        ImGui.EndChild();
    }

    /// <summary>
    ///    Renders a table of mods, with each mod displayed in a row with its details and controls.
    /// </summary>
    private static void _render_mod_table(string child_id, IReadOnlyList<FhInstalledMod> mods, bool show_load_order) {
        int column_count    = show_load_order ? 4 : 2;
        float table_width   = ImGui.GetContentRegionAvail().X;

        bool table = ImGui.BeginTable($"{child_id}.Table", column_count, ImGuiTableFlags.BordersInnerH | ImGuiTableFlags.RowBg, new Vector2(table_width, 0F));
        if (!table) {
            return;
        }

        float control_width = ImGui.GetFrameHeight();
        float grip_width    = ImGui.GetFrameHeight() * 1.1F + (ImGui.GetStyle().ItemSpacing.X * 2F);

        if (show_load_order) {
            ImGui.TableSetupColumn("##Order",   ImGuiTableColumnFlags.WidthFixed, control_width);
            ImGui.TableSetupColumn("##Enabled", ImGuiTableColumnFlags.WidthFixed, control_width);
            ImGui.TableSetupColumn("##Details", ImGuiTableColumnFlags.WidthStretch);
            ImGui.TableSetupColumn("##Drag",    ImGuiTableColumnFlags.WidthFixed, grip_width);
        }
        else {
            ImGui.TableSetupColumn("##Enabled", ImGuiTableColumnFlags.WidthFixed, control_width);
            ImGui.TableSetupColumn("##Details", ImGuiTableColumnFlags.WidthStretch);
        }

        for (int index = 0; index < mods.Count; index++) {
            _render_mod(mods[index], show_load_order, index, mods.Count);
        }

        ImGui.EndTable();
    }

    /// <summary>
    ///    Renders an arrow button for moving a mod up or down in the load order.
    /// </summary>
    private static void _render_load_order_arrow_button(FhInstalledMod mod, ImGuiDir direction, int move_direction, bool enabled, string tooltip) {
        if (!enabled) {
            ImGui.BeginDisabled();
        }

        if (ImGui.ArrowButton($"##Move{direction}.{mod.Manifest.Id}", direction)) {
            _pending_load_order_move = new(mod, move_direction);
        }

        if (ImGui.IsItemHovered()) {
            ImGui.SetTooltip(tooltip);
        }

        if (!enabled) {
            ImGui.EndDisabled();
        }
    }

    /// <summary>
    ///    Renders a single mod row in the mod table: checkbox, load order arrows, and mod details.
    /// </summary>
    private static void _render_mod(FhInstalledMod mod, bool show_load_order, int display_index, int mod_count) {
        ImGui.TableNextRow();

        if (show_load_order) {
            ImGui.TableSetColumnIndex(0);

            bool can_move_up   = mod.HasValidManifest && display_index > 0;
            bool can_move_down = mod.HasValidManifest && display_index < mod_count - 1;

            _render_load_order_arrow_button(mod, ImGuiDir.Up,   -1, can_move_up,   "Move up");
            _render_load_order_arrow_button(mod, ImGuiDir.Down,  1, can_move_down, "Move down");
        }

        ImGui.TableSetColumnIndex(show_load_order ? 1 : 0);

        float row_top_y = ImGui.GetCursorPosY();
        bool enabled    = mod.IsEnabled;

        if (ImGui.Checkbox($"##Enabled.{mod.Manifest.Id}", ref enabled)) {
            _pending_mod_toggle = new( mod, enabled);
        }

        float second_line_y = row_top_y + ImGui.GetFrameHeight() + ImGui.GetStyle().ItemSpacing.Y;

        if (show_load_order) {
            ImGui.SetCursorPosY(second_line_y);
            _render_centered_load_order_number(display_index + 1);
        }

        ImGui.TableSetColumnIndex(show_load_order ? 2 : 1);
        float details_start_y = ImGui.GetCursorPosY();

        if (!mod.DirectoryExists) {
            _text_colored_wrapped(ImGui.GetStyle().Colors[(int)ImGuiCol.NavCursor], $"Missing: {mod.Manifest.Id}");

            if (show_load_order) {
                ImGui.SetCursorPosY(second_line_y);
            }

            _text_disabled_wrapped(mod.DirectoryPath);
        }
        else {
            // display actual mod version or a default value
            string version = $"{(string.IsNullOrWhiteSpace(mod.Manifest.Version) ? "unknown version" : $"v{mod.Manifest.Version}")}";

            _text_wrapped(mod.Manifest.Name);
            ImGui.SameLine();
            _text_disabled_wrapped(version);

            if (show_load_order) {
                ImGui.SetCursorPosY(second_line_y);
            }

            if (!string.IsNullOrWhiteSpace(mod.Manifest.Authors)) {
                _text_disabled_wrapped($"Author: {mod.Manifest.Authors}");
            }

            if (!mod.ManifestExists) {
                _text_colored_wrapped(ImGui.GetStyle().Colors[(int)ImGuiCol.DragDropTarget], "Invalid mod: expected manifest is missing.");
            }
            else if (!string.IsNullOrWhiteSpace(mod.ManifestError)) {
                _text_colored_wrapped(ImGui.GetStyle().Colors[(int)ImGuiCol.NavCursor], $"Invalid mod: manifest error: {mod.ManifestError}");
            }
        }

        float details_height = ImGui.GetCursorPosY() - details_start_y;

        if (show_load_order) {
            ImGui.TableSetColumnIndex(3);
            _render_drag_handle(mod, display_index, mod_count, details_height);
        }
    }
}
