// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

using UserInterfaces   = Dictionary<string, FhSettingsUi>;
using ModuleSettings   = ConcurrentDictionary<Type, FhSettingData>;

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

// /// <summary>
// ///     A reference takes over setting display for <typeparamref name="T"/>.
// ///     Its settings will be rendered in place of the reference.
// /// </summary>
// /// <remarks>
// ///     This is used to present a unified settings panel for a number of disparate modules.
// ///     Only one reference can target any given module.
// /// </remarks>
// public sealed class FhSettingReference<T> : FhSetting<FhSettingsCategory?> where T : FhModule {
//     private static readonly ConcurrentDictionary<Type, byte> _s_refs = [];
//
//     private FhSettingsCategory? _settings = null;
//
//     public FhSettingReference(string id) : base(id, null) {
//         Type module_type = typeof(T);
//
//         if (!_s_refs.TryAdd(module_type, 0)) {
//             throw new Exception($"Only one {nameof(FhSettingReference<T>)} can exist for module {module_type}.");
//         }
//     }
//
//     internal override void save(Utf8JsonWriter writer) { }
//     internal override void load(Utf8JsonReader reader) { }
//
//     public override FhSettingsCategory? get() {
//         if (_settings == null) {
//             FhInternal.Settings.try_bind_reference<T>(out _settings);
//         }
//
//         return _settings;
//     }
// }

/// <summary>
///     Carries out setting-related operations.
/// </summary>
internal sealed class FhSettings {
    private readonly UserInterfaces   _uis       = [];
    private readonly ModuleSettings   _settings  = [];

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
            settings:      new FhSettingsCategory($"{module_type.FullName!}.settings", [ .. provider.get() ])
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

            _settings[module_type].settings.init();
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

    internal FhSettingsCategory? parent;
    internal int                 index_in_parent;

    internal virtual void init() { }

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
    public struct SetEventArgs {
        public readonly T old_value;

        public SetEventArgs(T old_value) {
            this.old_value = old_value;
        }
    }

    public readonly FhEvent<SetEventArgs> PostSet = new();

    protected bool disabled = false;

    protected readonly T default_ = defval;
    protected          T value    = defval;

    internal T dirty_value = defval;

    public virtual T get() => value;

    internal virtual void set(T new_value) {
        SetEventArgs args = new(value);

        value = new_value;

        PostSet.invoke(args);
    }

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

        set(loaded_value);
    }
}

/// <summary>An indented group of settings.</summary>
public sealed class FhSettingsCategory : FhSetting {
    internal readonly FhSetting[] settings;
    internal          bool        collapsed;

    public FhSettingsCategory(string id, FhSetting[] settings) : base(id) {
        this.settings = settings;
        update_ids(id);
        update_parents();
    }

    /// <summary>Prepends the category's ID to all subordinate settings.</summary>
    private void update_ids(string new_prefix) {
        foreach (FhSetting setting in settings) {
            setting.id = $"{new_prefix}.{setting.id}";

            if (setting is FhSettingsCategory category)
                category.update_ids(new_prefix);
        }
    }

    private void update_parents() {
        for (int i = 0; i < settings.Length; i++) {
            settings[i].parent = this;
            settings[i].index_in_parent = i;
        }
    }

    internal override void init() {
        foreach (FhSetting setting in settings) {
            setting.init();
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
public class FhSettingNumber : FhSetting<double> {
    public readonly double min;
    public readonly double max;
    public readonly double step;

    public readonly bool integers_only;

    public FhSettingNumber(
        string id,
        double def_value,
        double min           = 0.0,
        double max           = 1.0,
        double step          = 1.0,
        bool   integers_only = false
    ) : base(id, def_value) {
        ArgumentOutOfRangeException.ThrowIfLessThan   (def_value, min);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(def_value, max);

        ArgumentOutOfRangeException.ThrowIfGreaterThan(step, max - min);

        if (integers_only) {
            ArgumentOutOfRangeException.ThrowIfEqual(double.IsInteger(min),  false);
            ArgumentOutOfRangeException.ThrowIfEqual(double.IsInteger(max),  false);
            ArgumentOutOfRangeException.ThrowIfEqual(double.IsInteger(step), false);
        }

        this.min  = min;
        this.max  = max;
        this.step = step;

        this.integers_only = integers_only;
    }

    internal override void set(double new_value) {
        SetEventArgs args = new(value);

        value = double.Clamp(new_value, min, max);

        if (integers_only) {
            value = double.Round(value);
        }

        PostSet.invoke(args);
    }
}

public class FhSettingToggle(string id, bool def_value) : FhSetting<bool>(id, def_value);

public abstract class FhSettingCarousel<T> : FhSetting<T> {
    protected FhSettingCarousel(string id, T def_value) : base(id, def_value) { }

    public abstract ReadOnlySpan<T> get_choice_values();
    public abstract string get_choice_name(T choice);
    public abstract string get_choice_desc(T choice);
}

public abstract class FhSettingChoice(string id, long def_value) : FhSettingCarousel<long>(id, def_value) {
    public override string get_choice_name(long choice) => $"{id}.{choice}.name";
    public override string get_choice_desc(long choice) => $"{id}.{choice}.desc";
}

public sealed class FhSettingChoice<T> : FhSettingChoice
    where T : struct, Enum {

    private static long[] _vals;
    public override ReadOnlySpan<long> get_choice_values() => _vals;

    static FhSettingChoice() {
        T[] values = Enum.GetValues<T>();

        _vals = new long[values.Length];

        for (int i = 0; i < values.Length; i++) {
            _vals[i] = Convert.ToInt64(values[i]);
        }
    }

    public FhSettingChoice(string id, T def_value) : base(id, Convert.ToInt64(def_value)) {
        // This will be unnecessary once we can constrain T to be 'closed'
        ArgumentOutOfRangeException.ThrowIfEqual(
            Enum.IsDefined(def_value),
            false,
            nameof(def_value)
        );
    }

    internal override void set(long new_value) {
        SetEventArgs args = new(value);

        // This will be unnecessary once we can constrain T to be 'closed'
        if (!Enum.IsDefined(typeof(T), new_value)) {
            FhInternal.Log.Error($"Value '{new_value}' rejected as invalid for setting '{id}'.");
            return;
        }

        value = new_value;

        PostSet.invoke(args);
    }
}

public abstract class FhSettingDynamicChoice(string id, object def_value) : FhSettingCarousel<object>(id, def_value) {
    internal List<object> choices;

    public sealed override ReadOnlySpan<object> get_choice_values() => CollectionsMarshal.AsSpan(choices);
    public sealed override string get_choice_name(object choice) => $"{id}.{choice}.name";
    public sealed override string get_choice_desc(object choice) => $"{id}.{choice}.desc";

    internal sealed override void set(object new_value) {
        SetEventArgs args = new(value);

        if (!choices.Contains(new_value)) {
            FhInternal.Log.Error($"Value '{new_value}' rejected as invalid for setting '{id}'.");
            return;
        }

        value = new_value;

        PostSet.invoke(args);
    }

    internal sealed override void init() {
        choices = [ ..init_choices() ];
    }

    protected abstract IReadOnlyList<object> init_choices();
}
