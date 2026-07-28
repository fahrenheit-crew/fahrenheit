namespace Fahrenheit.Tools.ModManager;

internal static partial class ModManager {
    public static void show_mod_failures() {

    }

    public static void render_modlist() {
        for (LinkedListNode<ModData>? node = installed_mods.First; node is not null; node = node.Next) {
            ModData mod = node!.Value;
            FhManifest manifest = mod.manifest;

            ImGui.Text($"{manifest.Name} v{manifest.Version} ({(mod.enabled ? "Enabled" : "Disabled")})");
        }
    }

    public static void render_mod(ModData mod) {
        // if (ImGui.BeginChild($"###Mod{manifest.Id}", ImGuiWindowFlags.NoDecoration | ImGuiWindowFlags.NoSavedSettings)) {
        //     ImGui.
        // }
        // ImGui.EndChild();
    }
}
