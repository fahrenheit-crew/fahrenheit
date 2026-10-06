// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

/// <summary>
///     Stores all loaded mods' <see cref="FhModContext"/> for runtime lookup,
///     and exposes facilities to find <see cref="FhModule"/>s at runtime.
/// </summary>
public sealed class FhMods {

    /* [fkelava 23/06/26 14:27]
     * While module contexts are always accessible through the mod contexts,
     * they are also separately cached because users often care about only one or the other.
     */
    private readonly ImmutableArray<FhModContext>    _mods;
    private readonly ImmutableArray<FhModuleContext> _modules;

    internal FhMods() {
        FhEnvironment.set_execution_state(FhExecState.CTOR);

        FhModContext[]        mods    = new FhModContext[FhEnvironment.Manifests.Length];
        List<FhModuleContext> modules = [];

        FhModPaths[] mod_paths = FhEnvironment.ModPaths;
        FhManifest[] manifests = FhEnvironment.Manifests;

        FhInternal.Loader.prepare(manifests);

        for (int i = 0; i < manifests.Length; i++) {
            var mod_context = new FhModContext(
                manifests[i],
                mod_paths[i],
                [ .. FhInternal.Loader.load_mod(manifests[i]) ]);

            mods[i] = mod_context;
            modules.AddRange(mod_context.Modules);
        }

        _mods    = ImmutableArray.Create     (mods);
        _modules = ImmutableArray.CreateRange(modules);

        FhEnvironment.set_execution_state(FhExecState.PREINIT);
    }

    /// <summary>
    ///     Invokes the initializer callback for all loaded modules.
    /// </summary>
    internal void initialize() {
        FhEnvironment.set_execution_state(FhExecState.INIT);

        foreach (FhModContext mod_ctx in _mods) {
            foreach (FhModuleContext module_ctx in mod_ctx.Modules) {
                FhModule   fm       = module_ctx.Module;
                FileStream fm_state = File.Open(
                    module_ctx.Paths.GlobalStatePath,
                    FileMode  .OpenOrCreate,
                    FileAccess.ReadWrite,
                    FileShare .ReadWrite);

                if (!fm.init(mod_ctx, fm_state)) {
                    FhInternal.Log.Warning($"Module {fm.ModuleType} initializer callback failed.");
                    continue;
                }

                FhInternal.Log.Info($"Initialized module {fm.ModuleType}.");
            }
        }

        FhEnvironment.set_execution_state(FhExecState.EXEC);
    }

    /// <summary>
    ///     Returns the <see cref="FhModContext"/>s of all loaded mods for this session.
    /// </summary>
    public IEnumerable<FhModContext> get_mods() => _mods;

    /// <summary>
    ///     Returns the <see cref="FhModuleContext"/>s of all loaded modules for this session.
    /// </summary>
    public IEnumerable<FhModuleContext> get_modules() => _modules;

    /// <summary>
    ///     Searches all modules for one of type <typeparamref name="TModule"/>,
    ///     then returns its <see cref="FhModuleContext"/> if it was found.
    /// </summary>
    public FhModuleContext? get_module<TModule>() where TModule : FhModule {
        foreach (FhModuleContext module_ctx in _modules) {
            if (module_ctx.Module is TModule) return module_ctx;
        }
        return null;
    }
}
