// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

/// <summary>Internal setting-related utilities and constants.</summary>
internal sealed class FhSettings {

    internal const float TOOLTIP_SIZE = 40;
    internal const float NAME_WIDTH   = 300;

    /// <summary>Reads out all settings from disk.</summary>
    public void load() {
        foreach (FhModuleContext module_ctx in FhApi.Mods.get_modules()) {
            try {
                Span<byte>     config = File.ReadAllBytes(module_ctx.Paths.GlobalConfigPath);
                Utf8JsonReader reader = new(config);

                module_ctx.Module.load_settings(reader);
            }
            catch (FileNotFoundException) { }
        }
    }

    /// <summary>Persists all settings to disk.</summary>
    public void save() {
        JsonWriterOptions opts = new() {
            Indented   = true,
            IndentSize = 4
        };

        foreach (FhModuleContext module_ctx in FhApi.Mods.get_modules()) {
            using FileStream file = File.Open(
                module_ctx.Paths.GlobalConfigPath,
                FileMode  .OpenOrCreate,
                FileAccess.ReadWrite,
                FileShare .None);
            using Utf8JsonWriter writer = new Utf8JsonWriter(file, opts);

            module_ctx.Module.save_settings(writer);
        }
    }
}

/// <summary>
///     A persistent, configurable value associated with a given module, with a unique <paramref name="id"/>.
/// </summary>
public abstract class FhSetting(string id) {

    internal string id   =  id;
    public   string name => FhApi.Localization.localize($"{id}.name");
    public   string desc => FhApi.Localization.localize($"{id}.desc");

    /// <summary>
    ///     Writes out the setting's value to disk through the provided <paramref name="writer"/>.
    /// </summary>
    internal abstract void save(Utf8JsonWriter writer);

    /// <summary>
    ///     Reads the setting's value from disk through the provided <paramref name="reader"/>.
    /// </summary>
    internal abstract void load(Utf8JsonReader reader);

    internal abstract void render();

    public virtual void render_name() {
        ImGui.AlignTextToFramePadding();
        ImGui.Text(name);
    }

    protected static void render_tooltip(string tooltip) {
        if (!ImGui.BeginItemTooltip()) return;

        ImGui.PushTextWrapPos(ImGui.GetFontSize() * FhSettings.TOOLTIP_SIZE);
        ImGui.TextUnformatted(tooltip);
        ImGui.PopTextWrapPos ();
        ImGui.EndTooltip     ();
    }
}

/// <summary>
///     A persistent, configurable value of type <typeparamref name="T"/>
///     associated with a given module, with a unique <paramref name="id"/>.
/// </summary>
public abstract class FhSetting<T>(string id, T defval) : FhSetting(id) where T : notnull {
    protected T    _default  = defval;
    protected T    _value    = defval;
    protected bool _disabled = false;

    public           T    get()        => _value;
    internal virtual void set(T value) => _value = value;

    /* [fkelava 22/06/26 18:42]
     * There are catches with these that make implementing them correctly non-trivial.
     * Hence they are currently sealed, and we may unseal them at a later date.
     */

    internal sealed override void save(Utf8JsonWriter writer) {
        writer.WritePropertyName(id);
        JsonSerializer.Serialize<T>(writer, _value, FhUtil.InternalJsonOpts);
    }

    internal sealed override void load(Utf8JsonReader reader) {
        /* [fkelava 22/06/26 17:50]
         * A `Utf8JsonReader` is forward-only. Since `try_find_key_and_deserialize` will read
         * as far ahead as necessary to find the key, we need to copy the reader.
         *
         * The same is done by System.Text.Json when necessary. See dotnet/runtime,
         * Read<TValue>(ref Utf8JsonReader, JsonTypeInfo<TValue>) in JsonSerializer.Read.Utf8JsonReader.cs.
         */
        Utf8JsonReader copy = reader;
        if (!copy.try_find_key_and_deserialize(id, out T? value)) {
            FhInternal.Log.Error($"failed to load setting {id}");
            return;
        }

        _value = (value ?? _default);
    }
}

/// <summary>An indented group of settings.</summary>
public sealed class FhSettingsCategory : FhSetting {

    internal readonly FhSetting[] settings;
    internal          bool        collapsed;

    public FhSettingsCategory(string id, FhSetting[] settings) : base(id) {
        this.settings = settings;
        update_ids();
    }

    /// <summary>Prepends the category's ID to all subordinate settings.</summary>
    private void update_ids() {
        foreach (FhSetting setting in settings) {
            setting.id = $"{id}.{setting.id}";

            if (setting is FhSettingsCategory category)
                category.update_ids();
        }
    }

    /// <inheritdoc cref="FhSetting.save" />
    internal override void save(Utf8JsonWriter writer) {
        writer.WriteBoolean($"{id}.collapsed", collapsed);
        foreach (FhSetting setting in settings) {
            setting.save(writer);
        }
    }

    /// <inheritdoc cref="FhSetting.load" />
    internal override void load(Utf8JsonReader reader) {
        Utf8JsonReader copy = reader;
        if (copy.try_find_key_and_deserialize($"{id}.collapsed", out bool is_collapsed)) {
            collapsed = is_collapsed;
        }

        foreach (FhSetting setting in settings) {
            setting.load(reader);
        }
    }

    public override void render_name() {
        //TODO: Render extra ArrowButton with Down/Right arrow for collapsing the category
        ImGui.SeparatorText(name);
        render_tooltip(desc);
    }

    internal override void render() {
        ImGui.Dummy(Vector2.Zero); // Get rid of the SameLine from modconfig

        if (collapsed)
            return;

        ImGui.Indent(ImGui.GetTreeNodeToLabelSpacing());

        foreach (FhSetting setting in settings) {
            setting.render_name();
            ImGui.SameLine(FhSettings.NAME_WIDTH);
            setting.render();
        }

        ImGui.Unindent(ImGui.GetTreeNodeToLabelSpacing());
    }
}

/// <summary>A text input setting, with flags.</summary>
/// <remarks>Callbacks are not currently supported.</remarks>
public sealed class FhSettingText(string id, string def_value, ImGuiInputTextFlags flags = ImGuiInputTextFlags.None) : FhSetting<string>(id, def_value) {
    internal const int MAX_LENGTH = 1024;

    private readonly ImGuiInputTextFlags _flags = flags;

    internal override void render() {
        ImGui.InputText($"##setting.{id}", ref _value, MAX_LENGTH, _disabled ? _flags | ImGuiInputTextFlags.ReadOnly : _flags);
    }
}

/// <summary>A numeric/spinbox input.</summary>
/// <param name="id">The setting's identifier.</param>
/// <param name="def_value">The default value of the setting.</param>
/// <param name="min">The smallest accepted number. Defaults to 0.</param>
/// <param name="max">The biggest accepted number. Defaults to 1.</param>
/// <param name="step">The amount the arrows increase/decrease the value. Defaults to 1. Set to 0 to disable the arrows.</param>
/// <typeparam name="T">The underlying numeric type for the value.</typeparam>
public class FhSettingNumber<T>(string id, T def_value, T? min, T? max, T? step) : FhSetting<T>(id, def_value) where T : unmanaged, INumber<T> {
    private readonly ImGuiDataType _type = get_data_type(def_value);
    private readonly T             _step = step ?? T.One;
    private readonly T             _min  = min  ?? T.Zero;
    private readonly T             _max  = max  ?? T.One;

    internal override void set(T value) => _value = T.Clamp(value, _min, _max);

    internal override void render() {
        unsafe {
            fixed (T* ptr_value = &_value)
            fixed (T* ptr_step  = &_step) {
                if (ImGui.InputScalar($"##setting.{id}", _type, ptr_value, ptr_step)) {
                    set(_value);
                }
            }
        }
    }

    private static ImGuiDataType get_data_type(T value) {
        return value switch {
            byte   => ImGuiDataType.U8,
            ushort => ImGuiDataType.U16,
            uint   => ImGuiDataType.U32,
            ulong  => ImGuiDataType.U64,
            sbyte  => ImGuiDataType.S8,
            short  => ImGuiDataType.S16,
            int    => ImGuiDataType.S32,
            long   => ImGuiDataType.S64,
            float  => ImGuiDataType.Float,
            double => ImGuiDataType.Double,
            _      => throw new NotImplementedException($"FhSettingNumber<T> expected a built-in number type, not {typeof(T).Name}"),
        };
    }
}

public class FhSettingToggle(string id, bool def_value) : FhSetting<bool>(id, def_value) {
    internal override void render() {
        ImGui.Checkbox($"##setting.{id}", ref _value);
    }
}
