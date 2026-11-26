using System.Numerics;

using TableFlags = Hexa.NET.ImGui.ImGuiTableFlags;
using ColumnFlags = Hexa.NET.ImGui.ImGuiTableColumnFlags;

namespace Fahrenheit.Tools.ModManager;

internal static partial class ModManager {
    public static void show_mod_failures() {

    }

    public static void render_modlist() {
        TableFlags modlist_flags =
            // Features
            TableFlags.Resizable
          | TableFlags.Reorderable
          | TableFlags.Hideable
          | TableFlags.Sortable
          | TableFlags.ContextMenuInBody

            // Decoration
          | TableFlags.RowBg
          | TableFlags.Borders

            // Misc
          | TableFlags.SizingFixedFit
          | TableFlags.ScrollY
          | TableFlags.SortMulti;

        if (ImGui.BeginTable("Modlist", 7, modlist_flags)) {
            setup_modlist_headers();

            int idx = 0;

            for (LinkedListNode<ModData>? node = installed_mods.First; node is not null; node = node.Next, idx++) {
                ModData mod = node.Value;

                ImGui.TableNextRow();

                ImGui.PushID($"##mod.{mod.manifest.Id}");

                render_mod(node, idx);

                ImGui.PopID();
            }

            ImGui.EndTable();
        }
    }

    public static void setup_modlist_headers() {
        ImGui.TableSetupColumn("Icon",    ColumnFlags.WidthFixed | ColumnFlags.NoResize);
        ImGui.TableSetupColumn("Name",    ColumnFlags.WidthStretch | ColumnFlags.NoHide);
        ImGui.TableSetupColumn("Version", ColumnFlags.DefaultHide);
        ImGui.TableSetupColumn("Size",    ColumnFlags.DefaultHide);
        ImGui.TableSetupColumn("Enabled", ColumnFlags.WidthFixed | ColumnFlags.NoResize | ColumnFlags.NoHide);
        ImGui.TableSetupColumn("Order",   ColumnFlags.DefaultSort | ColumnFlags.WidthFixed | ColumnFlags.NoResize);

        ImGui.TableSetupColumn(
            "##Controls",
            ColumnFlags.WidthFixed
          | ColumnFlags.NoResize
          | ColumnFlags.NoReorder
          | ColumnFlags.NoHide
          | ColumnFlags.NoSort
          | ColumnFlags.NoHeaderLabel
        );

        ImGui.TableHeadersRow();
    }

    public static void render_mod(LinkedListNode<ModData> node, int index) {
        FhManifest manifest = node.Value.manifest;
        ModData mod = node.Value;

        // Icon
        {
            ImGui.TableSetColumnIndex(0);
            ImGui.Text("IC\nON");
        }

        // Name
        {
            ImGui.TableSetColumnIndex(1);
            ImGui.Text(manifest.Name);
        }

        // Version
        {
            ImGui.TableSetColumnIndex(2);
            ImGui.Text($"v{manifest.Version}");
        }

        // Size
        {
            ImGui.TableSetColumnIndex(3);
            ImGui.Text(mod.get_size_string());
        }

        // Enabled
        {
            ImGui.TableSetColumnIndex(4);

            ImGui.PushStyleVar(ImGuiStyleVar.FrameBorderSize, 1.0f);

            float width = ImGui.GetColumnWidth();
            float half_size = ImGui.GetFrameHeight() / 2.0f;
            float half_item_spacing = ImGui.GetStyle().ItemSpacing.Y / 2.0f;

            Vector2 cursor_pos = new(
                width / 2.0f - half_size,
                ImGui.GetTextLineHeightWithSpacing() - half_size + half_item_spacing
            );

            ImGui.SetCursorPos(ImGui.GetCursorPos() + cursor_pos);

            ImGui.Checkbox("##enabled", ref node.ValueRef.enabled);

            ImGui.PopStyleVar();
        }

        // Order
        {
            ImGui.TableSetColumnIndex(5);
            ImGui.Text((index+1).ToString());
        }

        // ##Controls
        {
            ImGui.TableSetColumnIndex(6);

            ImGui.PushStyleVar(ImGuiStyleVar.FrameBorderSize, 1.0f);

            if (node.Previous is { } prev) {
                if (ImGui.ArrowButton($"Up##{index}", ImGuiDir.Up)) {
                    (prev.Value, node.Value) = (node.Value, prev.Value);
                }
            } else {
                ImGui.BeginDisabled();
                ImGui.ArrowButton($"Up##{index}.disabled", ImGuiDir.Up);
                ImGui.EndDisabled();
            }

            if (node.Next is { } next) {
                if (ImGui.ArrowButton($"Down##{index}", ImGuiDir.Down)) {
                    (next.Value, node.Value) = (node.Value, next.Value);
                }
            } else {
                ImGui.BeginDisabled();
                ImGui.ArrowButton($"Down##{index}.disabled", ImGuiDir.Down);
                ImGui.EndDisabled();
            }

            ImGui.PopStyleVar();
        }
    }
}
