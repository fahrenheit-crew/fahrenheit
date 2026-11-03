// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

/// <summary>
///     A representation of a fully loaded mod.
///     Contains its <see cref="FhManifest"/> and a <see cref="FhModuleContext"/> for each of its modules.
/// </summary>
public sealed record FhModContext {
    public readonly FhModPaths            Paths;
    public readonly FhManifest            Manifest;
    public readonly List<FhModuleContext> Modules;

    internal FhModContext(FhManifest            manifest,
                          FhModPaths            paths,
                          List<FhModuleContext> modules)
    {
        Paths    = paths;
        Manifest = manifest;
        Modules  = modules;
    }
}

/// <summary>
///     A representation of a loaded module.
///     Contains a reference to it, and any metadata relating to it.
/// </summary>
public sealed record FhModuleContext {
    internal readonly FhModulePaths Paths;
    public   readonly FhModule      Module;

    internal FhModuleContext(FhModule      module,
                             FhModulePaths paths)
    {
        Paths  = paths;
        Module = module;
    }
}

/// <summary>
///     Contains metadata about a given local state file, such as the version of a module last used to write to it.
/// </summary>
public sealed record FhLocalStateInfo(
    string Version);

[Flags]
public enum FhManifestFlags {
    NONE           = 0,
    SEPARATE_SAVES = 1,
}

/// <summary>
///     Describes a unique Fahrenheit mod, consisting of one DLL containing any number of <see cref="FhModule"/>.
/// </summary>
public sealed record FhManifest(
    string          Id,
    string          Name,
    string          Desc,
    string          Authors,
    string          Version,
    string          Link,
    string[]        Dependencies,
    FhManifestFlags Flags);

/// <summary>
///     A module is the base unit of functionality in Fahrenheit.
///     Modules are used to logically partition your functionality.
/// </summary>
/// <remarks>
///     A mod can consist of any number of modules. Modules can link with
///     other modules at runtime using <see cref="FhModuleHandle{TTarget}"/>.
/// </remarks>
public abstract class FhModule {
    protected readonly string   _module_type_name;
    protected readonly FhLogger _logger;

    protected FhModule() {
        _module_type_name = GetType().FullName ?? throw new Exception("FH_E_MODULE_TYPE_UNIDENTIFIABLE");
        _logger           = new FhLogger($"{_module_type_name}.log");
    }

    internal string ModuleType {
        get { return _module_type_name; }
    }

    /// <summary>
    ///     Your module should perform all Fahrenheit-related initialization here. When this is called, all mods are loaded, and:
    ///     <br/>
    ///     <br/> - you may call <see cref="FhModuleHandle{T}.try_get_context(out FhModuleContext?)"/> to obtain references to other modules;
    ///     <br/> - you receive a copy of the containing mod's <see cref="FhModContext"/>;
    ///     <br/> - you receive a <see cref="FileStream"/> of the global state file for your module;
    /// </summary>
    /// <returns>Whether initialization succeeded. If <see cref="false"/>, an error is shown to the user, but execution continues.</returns>
    public abstract bool init(FhModContext mod_context, FileStream global_state_file);

    /// <summary>Called when the game saves, allowing the module to save state specific to that save game.</summary>
    /// <remarks>
    ///     Do not flush, dispose/close, or save <paramref name="local_state_file"/> as a local variable.
    ///     The stream only lasts for the duration of this call, and the framework cleans it up.
    /// </remarks>
    public virtual void save_local_state(FileStream local_state_file) { }

    /// <summary>Called when the game loads, allowing the module to load state specific to that save game.</summary>
    /// <remarks>
    ///     Do not flush, dispose/close, or save <paramref name="local_state_file"/> as a local variable.
    ///     The stream only lasts for the duration of this call, and the framework cleans it up.
    /// </remarks>
    public virtual void load_local_state(FileStream local_state_file, FhLocalStateInfo local_state_info) { }

    /// <summary>Called every frame. Invoke ImGui methods here to submit draw calls for the frame.</summary>
    public virtual void render_imgui() { }
}
