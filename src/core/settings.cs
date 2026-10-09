// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

using UserInterfaces   = Dictionary<string, FhSettingsUi>;
using ModuleSettings   = ConcurrentDictionary<Type, FhSettingData>;
using SettingRenderers = ConcurrentDictionary<(Type, Type), FhSettingRenderer>;

/// <summary>
///     Pairs a setting category with data
///     to control its persistence and rendering.
/// </summary>
internal sealed record FhSettingData(
    string             settings_path,
    FhSettingsCategory settings
) {
    /// <summary>
    ///     Marks that a <see cref="FhSettingReference{T}"/>
    ///     has taken over displaying the associated setting category.
    /// </summary>
    internal bool ref_active = false;
}

/// <summary>
///     Declares the settings provided by <typeparamref name="T"/>.
/// </summary>
/// <remarks>
///     Only one provider may exist for any given module. It must be instantiated in that module's constructor.
/// </remarks>
public abstract class FhSettingProvider<T> where T : FhModule {
    public FhSettingProvider() {
        FhInternal.Settings.register_provider<T>(this);
    }

    /// <summary>
    ///     Returns the settings the module wishes to expose through
    ///     the mod settings UI, in display order.
    /// </summary>
    internal protected abstract IEnumerable<FhSetting> get();
}

public abstract class FhSettingRenderer {
    internal protected abstract Vector2 get_size(FhSettingsUi ui);
    internal protected abstract void render(FhSettingsUi ui, FhSetting setting, Rect max_bounds);
    internal protected abstract void handle_input(FhSettingsUi ui, FhSetting setting);
}

public abstract class FhSettingRenderer<TSetting, TUi> : FhSettingRenderer
    where TSetting : FhSetting
    where TUi      : FhSettingsUi {

    internal protected sealed override Vector2 get_size(FhSettingsUi ui) {
        return get_size((TUi)ui);
    }

    internal protected sealed override void render(FhSettingsUi ui, FhSetting setting, Rect max_bounds) {
        render((TUi)ui, (TSetting)setting, max_bounds);
    }

    internal protected sealed override void handle_input(FhSettingsUi ui, FhSetting setting) {
        handle_input((TUi)ui, (TSetting)setting);
    }

    internal protected abstract Vector2 get_size(TUi ui);
    internal protected abstract void render(TUi ui, TSetting setting, Rect max_bounds);
    internal protected abstract void handle_input(TUi ui, TSetting setting);
}

/// <summary>
///     A reference takes over setting display for <typeparamref name="T"/>.
///     Its settings will be rendered in place of the reference.
/// </summary>
/// <remarks>
///     This is used to present a unified settings panel for a number of disparate modules.
///     Only one reference can target any given module.
/// </remarks>
public sealed class FhSettingReference<T> : FhSetting<FhSettingsCategory?> where T : FhModule {
    private static readonly ConcurrentDictionary<Type, byte> _s_refs = [];

    private FhSettingsCategory? _settings = null;

    public FhSettingReference(string id) : base(id, null) {
        Type module_type = typeof(T);

        if (!_s_refs.TryAdd(module_type, 0)) {
            throw new Exception($"Only one {nameof(FhSettingReference<T>)} can exist for module {module_type}.");
        }
    }

    internal override void save(Utf8JsonWriter writer) { }
    internal override void load(Utf8JsonReader reader) { }

    public override FhSettingsCategory? get() {
        if (_settings == null) {
            FhInternal.Settings.try_bind_reference<T>(out _settings);
        }

        return _settings;
    }
}

/// <summary>
///     Carries out setting-related operations.
/// </summary>
internal sealed class FhSettings {
    private readonly UserInterfaces   _uis       = [];
    private readonly ModuleSettings   _settings  = [];
    private readonly SettingRenderers _renderers = [];

    /// <summary>Get the settings UI associated with the given ID.</summary>
    /// <param name="id">The ID of the desired UI.</param>
    /// <param name="ui">The UI with the given ID.</param>
    /// <returns>Whether the operation succeeded.</returns>
    internal bool get_ui(string id, [NotNullWhen(true)] out FhSettingsUi? ui) {
        return _uis.TryGetValue(id, out ui);
    }

    /// <summary>Register a new settings UI for selection by the user.</summary>
    /// <param name="ui">The new UI to register.</param>
    public void register_ui(FhSettingsUi ui) {
        _uis[ui.ModuleType] = ui;
    }

    public bool get_setting_renderer<TUi>(
        FhSetting setting,
        [NotNullWhen(true)] out FhSettingRenderer? renderer
    )
        where TUi      : FhSettingsUi {

        FhInternal.Log.Info($"Getting renderer for {setting.GetType()} in {typeof(TUi)}");

        return _renderers.TryGetValue((setting.GetType(), typeof(TUi)), out renderer);
    }

    /// <summary>Registers the given renderer type.</summary>
    /// <typeparam name="TSetting">The type of the setting the renderer is for.</typeparam>
    /// <typeparam name="TUi">The type of the UI the renderer is for.</typeparam>
    /// <typeparam name="TRenderer">The type of the renderer for the setting.</typeparam>
    public void register_setting_renderer<TSetting, TUi, TRenderer>()
        where TSetting  : FhSetting
        where TUi       : FhSettingsUi
        where TRenderer : FhSettingRenderer<TSetting, TUi>, new() {

        FhInternal.Log.Info($"Registering renderer for {typeof(TSetting)} in {typeof(TUi)}");

        _renderers[(typeof(TSetting), typeof(TUi))] = new TRenderer();
    }

    /// <summary>
    ///     Registers a given module's settings for display.
    /// </summary>
    /// <param name="provider">The <see cref="FhSettingProvider{T}"/> for the module.</param>
    internal void register_provider<T>(FhSettingProvider<T> provider) where T : FhModule {
        if (FhEnvironment.get_execution_state() != FhExecState.CTOR) {
            throw new Exception($"{nameof(FhSettingProvider<>)} may only be instantiated in a module constructor.");
        }

        Type          module_type     = typeof(T);
        FhSettingData module_settings = new(
            settings_path: "", // We'll fill in the path in initialize(), when all module constructors have finished running.
            settings:      new FhSettingsCategory(module_type.FullName!, [ .. provider.get() ])
        );

        if (!_settings.TryAdd(module_type, module_settings)) {
            throw new Exception($"Only one {nameof(FhSettingProvider<T>)} can be registered for module {module_type}.");
        }
    }

    /// <summary>
    ///     Prepares settings for persistence.
    /// </summary>
    internal void initialize() {
        foreach (FhModuleContext module_context in FhApi.Mods.get_modules()) {
            Type module_type = module_context.Module.GetType();

            if (!_settings.TryGetValue(module_type, out FhSettingData? sd))
                continue;

            _settings[module_type] = sd with { settings_path = module_context.Paths.GlobalConfigPath };
        }

        load_all();
    }

    /// <summary>
    ///     Attempts to retrieve the settings of the given <paramref name="module"/>.
    /// </summary>
    internal bool try_get_settings(FhModule module, [NotNullWhen(true)] out FhSettingsCategory? settings) {
        settings = null;
        if (!_settings.TryGetValue(module.GetType(), out FhSettingData? sd) || sd.ref_active)
            return false;

        settings = sd.settings;
        return true;
    }

    /// <summary>
    ///     Transfers displaying the settings for <typeparamref name="T"/> to a <see cref="FhSettingReference{T}"/>.
    /// </summary>
    internal bool try_bind_reference<T>([NotNullWhen(true)] out FhSettingsCategory? settings) where T : FhModule {
        settings = null;
        if (!_settings.TryGetValue(typeof(T), out FhSettingData? sd))
            return false;

        settings = sd.settings;
        sd.ref_active = true;

        return true;
    }

    /// <summary>Reads out all settings from disk.</summary>
    internal void load_all() {
        foreach (FhSettingData sd in _settings.Values) {
            try {
                Utf8JsonReader reader = new(File.ReadAllBytes(sd.settings_path));

                reader.enter_json_object();
                sd.settings.load(reader);
            }
            catch (FileNotFoundException) { }
        }
    }

    /// <summary>Persists all settings to disk.</summary>
    internal void save_all() {
        JsonWriterOptions opts = new() {
            Indented   = true,
            IndentSize = 4,
        };

        foreach (FhSettingData sd in _settings.Values) {
            using FileStream file = File.Open(
                sd.settings_path,
                FileMode  .OpenOrCreate,
                FileAccess.ReadWrite,
                FileShare .None
            );

            using Utf8JsonWriter writer = new(file, opts);

            writer.WriteStartObject();
            sd.settings.save(writer);
            writer.WriteEndObject();

            // Truncate the file.
            file.SetLength(writer.BytesCommitted);
        }
    }
}

/// <summary>
///     A configurable value with a unique <paramref name="id"/>.
/// </summary>
/// <remarks>
///     Settings are automatically persisted to disk and exposed through the
///     mod settings UI when provided through an <see cref="FhSettingProvider{T}"/>.
/// </remarks>
public abstract class FhSetting(string id) {
    internal string id = id;

    /// <summary>
    ///     The setting's display name.
    /// </summary>
    /// <remarks>This must be provided in localization data with ID <c>{setting_id}.name</c>.</remarks>
    public string name => FhApi.Localization.localize($"{id}.name");

    /// <summary>
    ///     The setting's description.
    /// </summary>
    /// <remarks>This must be provided in localization data with ID <c>{setting_id}.desc</c>.</remarks>
    public string desc => FhApi.Localization.localize($"{id}.desc");

    /// <summary>
    ///     Writes out the setting's value to disk through the provided <paramref name="writer"/>.
    /// </summary>
    internal abstract void save(Utf8JsonWriter writer);

    /// <summary>
    ///     Reads the setting's value from disk through the provided <paramref name="reader"/>.
    /// </summary>
    internal abstract void load(Utf8JsonReader reader);
}

/// <summary>
///     A configurable value of type <typeparamref name="T"/> with a unique <paramref name="id"/>.
/// </summary>
/// <remarks>
///     Settings are automatically persisted to disk and exposed through the
///     mod settings UI when provided through an <see cref="FhSettingProvider{T}"/>.
/// </remarks>
public abstract class FhSetting<T>(string id, T defval) : FhSetting(id) {
    protected bool disabled = false;

    protected readonly T default_ = defval;
    protected          T value    = defval;

    public   virtual T    get()            => value;
    internal virtual void set(T new_value) => value = new_value;

    internal override void save(Utf8JsonWriter writer) {
        writer.WritePropertyName(id);
        JsonSerializer.Serialize(writer, value, FhUtil.InternalJsonOpts);
    }

    internal override void load(Utf8JsonReader reader) {
        /* [fkelava 22/06/26 17:50]
         * A `Utf8JsonReader` is forward-only. Since `try_find_key_and_deserialize` will read
         * as far ahead as necessary to find the key, we need to copy the reader.
         *
         * The same is done by System.Text.Json when necessary. See dotnet/runtime,
         * Read<TValue>(ref Utf8JsonReader, JsonTypeInfo<TValue>) in JsonSerializer.Read.Utf8JsonReader.cs.
         */
        Utf8JsonReader copy = reader;
        if (!copy.try_find_key_and_deserialize(id, out T? loaded_value)) {
            FhInternal.Log.Warning($"failed to load setting {id}");
            return;
        }

        value = loaded_value;
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

    internal override void save(Utf8JsonWriter writer) {
        writer.WriteBoolean($"{id}.collapsed", collapsed);
        foreach (FhSetting setting in settings) {
            setting.save(writer);
        }
    }

    internal override void load(Utf8JsonReader reader) {
        Utf8JsonReader copy = reader;
        if (copy.try_find_key_and_deserialize($"{id}.collapsed", out bool is_collapsed)) {
            collapsed = is_collapsed;
        }

        foreach (FhSetting setting in settings) {
            setting.load(reader);
        }
    }
}

/// <summary>A text input setting, with flags.</summary>
/// <remarks>Callbacks are not currently supported.</remarks>
public sealed class FhSettingText(
    string              id,
    string              def_value,
    ImGuiInputTextFlags flags = ImGuiInputTextFlags.None
) : FhSetting<string>(id, def_value) {

    public const int MAX_LENGTH = 1024;
}

/// <summary>A numeric/spinbox input.</summary>
/// <param name="id">The setting's identifier.</param>
/// <param name="def_value">The default value of the setting.</param>
/// <param name="min">The smallest accepted number. Defaults to 0.</param>
/// <param name="max">The biggest accepted number. Defaults to 1.</param>
/// <param name="step">The amount the arrows increase/decrease the value. Defaults to 1. Set to 0 to disable the arrows.</param>
/// <typeparam name="T">The underlying numeric type for the value.</typeparam>
public class FhSettingNumber<T>(
    string id,
    T      def_value,
    T?     min,
    T?     max,
    T?     step
) : FhSetting<T>(id, def_value) where T : unmanaged, INumber<T> {

    public readonly ImGuiDataType type = get_data_type(def_value);

    public readonly T step = step ?? T.One;
    public readonly T min  = min  ?? T.Zero;
    public readonly T max  = max  ?? T.One;

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
            _      => throw new NotImplementedException($"{nameof(FhSettingNumber<T>)} expected a built-in number type, not {typeof(T).Name}"),
        };
    }

    internal override void set(T new_value) {
        value = T.Clamp(new_value, min, max);
    }
}

public class FhSettingToggle(string id, bool def_value) : FhSetting<bool>(id, def_value);
