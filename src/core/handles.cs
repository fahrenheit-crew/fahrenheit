// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

/// <summary>
///     Allows access to the <see cref="FhModule"/> of type
///     <typeparamref name="T"/> or its <see cref="FhModuleContext"/>.
/// </summary>
public sealed class FhModuleHandle<T>(FhModule owner) where T : FhModule {
    private readonly FhModule         _owner = owner;
    private          FhModuleContext? _match;

    /// <summary>
    ///     Attempt to get the module's context.
    /// </summary>
    /// <param name="target_context">The module's context, if it is loaded.</param>
    /// <returns>Whether the module is loaded.</returns>
    public bool try_get_context([NotNullWhen(true)] out FhModuleContext? target_context) {
        return (target_context = (_match ??= FhApi.Mods.get_module<T>())) != null;
    }

    /// <summary>
    ///     Attempt to get the module.
    /// </summary>
    /// <param name="target">The module, if it is loaded.</param>
    /// <returns>Whether the module is loaded.</returns>
    public bool try_get_module([NotNullWhen(true)] out T? target) {
        target = default;
        return try_get_context(out FhModuleContext? target_context) && (target = target_context.Module as T) != null;
    }
}

/// <summary>
///     Represents the implementation of <typeparamref name="T"/> provided by the runtime.
/// </summary>
internal sealed class FhRuntimeHandle<T> {
    private readonly Lock _impl_lock = new Lock();
    private          T?   _impl;

    public bool get([NotNullWhen(true)] out T? impl) {
        lock (_impl_lock) {
            return (impl = _impl) != null;
        }
    }

    public void set(T impl) {
        lock (_impl_lock) {
            FhInternal.Log.Info(typeof(T).Name);
            _impl = impl;
        }
    }
}

/// <summary>
///     A helper to obtain the absolute address of a given function.
/// </summary>
public readonly ref struct FhCallLocation {

    /* [fkelava 11/10/26 22:05]
     * When Stage 0 is active (not in `--extdbg` mode), it uses the debug link
     * to communicate module (un)load information to Stage 1, which Fahrenheit
     * then probes from. This allows us to cache module and export locations safely.
     */

    private readonly static Dictionary<nint,           uint> _s_sizes   = [];
    private readonly static Dictionary<string,         nint> _s_modules = [];
    private readonly static Dictionary<(nint, string), nint> _s_exports = [];
    private          static int                              _lock      = 0;

    private readonly nint _ptr_target;

    unsafe static FhCallLocation() {
        FhPInvoke.s1_dbg_link_set_load_cb  (&s1_load);
        FhPInvoke.s1_dbg_link_set_unload_cb(&s1_unload);
    }

    /* [fkelava 11/10/26 22:05]
     * Callback invokes are serialized by Stage 1, so we only need
     * to lock with respect to `get_module_addr` and `get_export`.
     */

    [UnmanagedCallersOnly(CallConvs = [ typeof(CallConvStdcall) ] )]
    private unsafe static void s1_load(FhPInvoke.DBG_MODULE_LOAD* ptr_data_load) {
        Interlocked.Increment(ref _lock);

        // The char* cast is to call the null-terminated string ctor. ROS<char> ctor would copy the \0s.
        string module_name = Path.GetFileName( new((char*) &ptr_data_load->module_name) );
        nint   module_base = ptr_data_load->module_base;

        _s_modules[module_name] = module_base;
        _s_sizes  [module_base] = ptr_data_load->module_size;
        FhInternal.Log.Info($"Module cached: {module_name} @ 0x{module_base:X8}");

        Interlocked.Decrement(ref _lock);
    }

    [UnmanagedCallersOnly(CallConvs = [ typeof(CallConvStdcall) ] )]
    private unsafe static void s1_unload(FhPInvoke.DBG_MODULE_UNLOAD* ptr_data_unload) {
        Interlocked.Increment(ref _lock);

        /* [fkelava 13/10/26 13:05]
         * https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.getenumerator?view=net-10.0#remarks
         * > The only mutating methods which do not invalidate enumerators are Remove and Clear.
         */

        nint unloaded_module_base = ptr_data_unload->dll_base;
        uint unloaded_module_size = 0;

        foreach ((string module_name, nint module_base) in _s_modules) {
            if (module_base != unloaded_module_base) continue;

            if (!_s_modules.Remove(module_name)
            ||  !_s_sizes  .Remove(module_base, out unloaded_module_size)) {
                throw new Exception($"Failed to invalidate module '{module_name}'.");
            }

            FhInternal.Log.Info($"Module invalidated: {module_name} @ 0x{module_base:X8}");
            break;
        }

        foreach ((nint module_base, string export_name) in _s_exports.Keys) {
            if (module_base != unloaded_module_base) continue;

            if (!_s_exports.Remove((module_base, export_name), out nint export_addr)) {
                throw new Exception($"Failed to invalidate export '{export_name}' in module @ 0x{module_base:X8}.");
            }

            FhInternal.Log.Info($"Export invalidated: {export_name} @ 0x{export_addr:X8}");
        }

        FhInternal.Calls.chain_invalidate(
            unloaded_module_base,
            unloaded_module_size
        );

        Interlocked.Decrement(ref _lock);
    }

    /// <summary>
    ///     Define a location of a function identical in FF X and X-2.
    /// </summary>
    /// <remarks>The correct offset is chosen based on the active game.</remarks>
    public FhCallLocation(nint offset_x, nint offset_x2) {
        bool is_ffx = FhGlobal.game_id == FhGameId.FFX;

        string module_name = is_ffx ? "FFX.exe" : "FFX-2.exe";
        nint   offset      = is_ffx ? offset_x  : offset_x2;

        _ptr_target = calc_addr(module_name, offset);
    }

    /// <summary>
    ///     Define a location of a named export in an external library.
    /// </summary>
    public FhCallLocation(string module_name, string fn_name) {
        _ptr_target = calc_addr(module_name, fn_name);
    }

    /// <summary>
    ///     Define a location of a function exclusive to FF X or X-2,
    ///     or an unexported function in an external library.
    /// </summary>
    public FhCallLocation(string module_name, nint offset) {
        _ptr_target = calc_addr(module_name, offset);
    }

    /// <summary>
    ///     Define a location of a function you have a pointer to, such as
    ///     a member function or vtable entry of an object.
    /// </summary>
    /// <remarks>Unlike other constructors, no validation is performed on the input address.</remarks>
    public FhCallLocation(nint abs_addr) {
        _ptr_target = abs_addr;
    }

    /// <inheritdoc cref="FhCallLocation(nint)" />
    unsafe public FhCallLocation(void* abs_addr) {
        _ptr_target = (nint)abs_addr;
    }

    /// <summary>
    ///     Get the base address of a module.
    /// </summary>
    /// <param name="module_name">The name of the module to get the address of.</param>
    /// <returns>The address of the specified module, or zero if it is not loaded.</returns>
    private static nint get_module_addr(string module_name) {
        return _lock == 0 && _s_modules.TryGetValue(module_name, out nint ptr_module)
            ? ptr_module
            : FhPInvoke.GetModuleHandle(module_name);
    }

    /// <summary>
    ///     Get the address of a named export in a module.
    /// </summary>
    /// <param name="module_addr">The base address of the module to search.</param>
    /// <param name="export">The name of the exported symbol.</param>
    /// <param name="ptr_fn">A pointer to the export, or zero if it does not exist.</param>
    /// <returns>Whether the export exists.</returns>
    private static bool get_export(nint module_addr, string export, out nint ptr_fn) {
        if (_lock != 0) {
            return NativeLibrary.TryGetExport(module_addr, export, out ptr_fn);
        }

        var key = (module_addr, export);

        if (_s_exports.TryGetValue(key, out ptr_fn))
            return ptr_fn != 0;

        // out-parameter is 0 if no export was found
        if (!NativeLibrary.TryGetExport(module_addr, export, out ptr_fn)) {
            FhInternal.Log.Error($"No export '{export}' in module at 0x{module_addr:X8}.");
        }

        FhInternal.Log.Info($"Export cached: {export} @ 0x{ptr_fn:X8}");
        return (_s_exports[key] = ptr_fn) != 0;
    }

    /// <summary>
    ///     Obtain the address of a named export.
    /// </summary>
    /// <param name="module_name">The name of the module to search.</param>
    /// <param name="export">The name of the exported symbol.</param>
    /// <returns>The export's address, or zero if the export or its module cannot be found.</returns>
    private static nint calc_addr(string module_name, string export) {
        nint module_addr = get_module_addr(module_name);
        return module_addr != 0 && get_export(module_addr, export, out nint fn_addr)
            ? fn_addr
            : 0;
    }

    /// <summary>
    ///     Obtain the address of an unexported function.
    /// </summary>
    /// <param name="module_name">The name of the module the function is contained in.</param>
    /// <param name="offset">The offset at which the function is located in the target module.</param>
    /// <returns>The function's address, or zero if its module is not loaded.</returns>
    private static nint calc_addr(string module_name, nint offset) {
        nint module_addr = get_module_addr(module_name);
        return module_addr != 0
            ? (module_addr + offset)
            : 0;
    }

    public bool try_resolve(out nint ptr_target) {
        return (ptr_target = _ptr_target) != 0;
    }
}

/// <summary>
///     Represents a call with signature <typeparamref name="T"/>. It may then be invoked or hooked.
/// </summary>
public ref struct FhCallHandle<T> where T : Delegate {

    private readonly nint _ptr_target;

    /// <summary>A pointer to the target function.</summary>
    /// <remarks>
    ///     By default, this includes all hooks.
    ///     To execute only part of the function's call chain, use <see cref="chain_from(T)"/>.
    /// </remarks>
    public T? fnptr {
        get {
            if (FhEnvironment.get_execution_state() != FhExecState.EXEC) {
                throw new Exception("You cannot invoke calls until all module initializers have completed.");
            }

            return field;
        }
        private set;
    }

    public FhCallHandle(FhCallLocation location) {
        if (location.try_resolve(out _ptr_target)) {
            fnptr = FhInternal.Calls.get_fnptr<T>(_ptr_target);
        }
    }

    /// <summary>
    ///     Retargets the handle to only execute hooks subsequent to the given <paramref name="hook"/>.
    /// </summary>
    public FhCallHandle<T> chain_from(T hook) {
        fnptr = FhInternal.Calls.chain_next(hook);
        return this;
    }

    /// <summary>
    ///     Attempts to insert the given <paramref name="hook"/> into the hook chain of the target call.
    /// </summary>
    public readonly bool hook(FhModule owner, T hook) {
        FhHookContext hook_info = new(owner, hook);

        return _ptr_target != 0 && FhInternal.Calls.chain_add<T>(_ptr_target, hook_info);
    }
}

/// <summary>
///     Pairs a hook with its owner. A stack of these constitutes the complete hook chain of a call.
/// </summary>
internal sealed record FhHookContext(
    FhModule owner,
    Delegate fnptr);

/// <summary>
///     The state of a given call. Used to track hook insertion.
/// </summary>
internal sealed class FhCallContext {

    internal readonly Stack<FhHookContext> stack;      // The stack of hooks inserted over this call.
    internal          nint                 ptr_insert; // The address at which the next hook in the chain is to be inserted.
    internal          bool                 tainted;    // No more hooks may be inserted.

    public FhCallContext(nint ptr_target) {
        stack      = [];
        ptr_insert = ptr_target;
    }

}

/// <summary>
///     Keeps track of the global state of all callables.
/// </summary>
internal sealed class FhCallRegistry {

    private static readonly Dictionary<nint,     Delegate>      _fnptrs   = []; // Function -> Cached delegate
    private static readonly Dictionary<nint,     FhCallContext> _contexts = []; // Original -> Context
    private static readonly Dictionary<Delegate, nint>          _chains   = []; // Hook     -> Next function in chain
    private static readonly HashSet   <nint>                    _invalid  = []; // Invalidated calls that need cleaning

    private          int  _lock_commit = 0;
    private readonly Lock _lock_chains = new();

    /// <summary>
    ///     Caches a delegate for the function of type <typeparamref name="T"/> at <paramref name="ptr_target"/>.
    /// </summary>
    /// <returns>A newly generated or cached delegate for the target function.</returns>
    public T get_fnptr<T>(nint ptr_target) where T : Delegate {
        lock (_lock_chains) {
            FhInternal.Log.Info($"get");
            if (_fnptrs.TryGetValue(ptr_target, out Delegate? fnptr) && fnptr is T t_fnptr)
                return t_fnptr;

            t_fnptr = Marshal.GetDelegateForFunctionPointer<T>(ptr_target);
            _fnptrs[ptr_target] = t_fnptr;
            return t_fnptr;
        }
    }

    /// <summary>
    ///     Invalidates all chains in a given module.
    /// </summary>
    internal void chain_invalidate(
        nint module_base,
        uint module_size
    ) {
        lock (_lock_chains) {
            FhInternal.Log.Info($"inval start");
            nint module_end = nint.CreateChecked( module_base + module_size );

            foreach ((nint call_addr, FhCallContext call_context) in _contexts) {
                if (call_addr < module_base || call_addr > module_end)
                    continue;

                bool first_in_stack = true;
                foreach (FhHookContext stack_entry in call_context.stack) {
                    if (!_chains.Remove(stack_entry.fnptr, out nint fnptr) || !_fnptrs.Remove(fnptr)) {
                        throw new Exception($"Failed to invalidate chain entry '{stack_entry.fnptr.Method.Name}'.");
                    }

                    if (!first_in_stack) {
                        FhPInvoke.MH_STATUS rm_res = FhPInvoke.MH_RemoveHook(fnptr);
                        if (rm_res != FhPInvoke.MH_STATUS.MH_OK) {
                            throw new Exception($"Failed to unwind chain entry '{stack_entry.fnptr.Method.Name}' - {rm_res}.");
                        }
                    }

                    first_in_stack = false;
                }

                if (!_contexts.Remove(call_addr) || !_invalid.Add(call_addr)) {
                    throw new Exception($"Failed to invalidate call at 0x{call_addr:X8}.");
                }
            }

            foreach ((nint fnptr_addr, _) in _fnptrs) {
                if (fnptr_addr < module_base || fnptr_addr > module_end)
                    continue;

                if (!_fnptrs.Remove(fnptr_addr)) {
                    throw new Exception($"Failed to invalidate delegate at 0x{fnptr_addr:X8}.");
                }
            }

            FhInternal.Log.Info($"inval end");
        }
    }

    /// <summary>
    ///     Obtains the next link in a <paramref name="hook"/>'s chain, if it exists.
    /// </summary>
    public T? chain_next<T>(T hook) where T : Delegate {
        lock (_lock_chains) {
            FhInternal.Log.Info($"next");
            return _chains.TryGetValue(hook, out nint ptr_chain)
                ? get_fnptr<T>(ptr_chain)
                : null;
        }
    }

    /* [fkelava 02/06/26 18:55]
     * MinHook will not install two hooks for the same function.
     *
     * `h1`, `h2` and `h3` over a function `f` must therefore be ordered as such:
     * > MH_CreateHook(&f,             &h1, &trampoline_f);
     * > MH_CreateHook(&trampoline_f,  &h2, &trampoline_h1);
     * > MH_CreateHook(&trampoline_h1, &h3, &trampoline_h2);
     *
     * Execution then follows insertion order (FIFO), while we want it to go in load order (LIFO).
     *
     * We could unwind and reapply the entire hook chain, but I did not succeed,
     * presumably due to https://github.com/TsudaKageyu/minhook/issues/78#issuecomment-485101354.
     *
     * Thus we impose the following rules:
     * - Hooks inserted at `init` time are queued for application.
     * - Hooks are inserted in load order after all modules have initialized.
     * - Hook insertion after `init` is prohibited over a function that already has any.
     * - Hooks inserted after `init` revert to executing in insertion order.
     */

    /// <summary>
    ///     Attempts to insert a given <paramref name="hook"/>
    ///     into the chain of the function at <paramref name="ptr_target"/>.
    /// </summary>
    private bool chain_insert<T>(nint ptr_target, T hook) where T : Delegate {
        // _lock_chains must be held by this method's caller.
        nint ptr_hook;
        nint ptr_original = 0;
        nint ptr_insert   = _contexts.TryGetValue(ptr_target, out FhCallContext? call_context)
            ? call_context.ptr_insert
            : ptr_target;

        try {
            ptr_hook = Marshal.GetFunctionPointerForDelegate(hook);
        }
        catch (Exception e) {
            FhInternal.Log.Error(e.ToString());
            return false;
        }

        /* [fkelava 13/10/26 15:09]
         * A problem exists when a hook exists in a subsequently unloaded DLL.
         * Even though Stage 0 will signal the unload, the DLL is already gone by the time
         * we invalidate chains. MinHook still has the entry for the hook in its tables,
         * can't remove it because it would take a memory protection error, and refuses
         * to insert a hook when the DLL is reloaded because of that extant entry.
         *
         * We have to clean that up when the DLL is reloaded.
         */

        if (_invalid.Remove(ptr_insert)) {
            FhPInvoke.MH_STATUS rv_remove = FhPInvoke.MH_RemoveHook(ptr_insert);

            if (rv_remove != FhPInvoke.MH_STATUS.MH_OK) {
                FhInternal.Log.Error($"MH_RemoveHook() failed while inserting {hook.Method.Name} - {rv_remove}");
                return false;
            }
        }

        unsafe { // SAFETY: &ptr_original is used as an out parameter and stack allocated
            FhPInvoke.MH_STATUS rv_create = FhPInvoke.MH_CreateHook(ptr_insert, ptr_hook, &ptr_original);

            if (rv_create != FhPInvoke.MH_STATUS.MH_OK) {
                FhInternal.Log.Error($"MH_CreateHook() failed for {hook.Method.Name} - {rv_create}");
                return false;
            }

            FhInternal.Log.Debug($"0x{ptr_insert:X8} -> 0x{ptr_original:X8} ({hook.Method.Name})");
        }

        FhPInvoke.MH_STATUS rv_enable = FhPInvoke.MH_EnableHook(ptr_insert);

        if (rv_enable != FhPInvoke.MH_STATUS.MH_OK) {
            FhInternal.Log.Error($"MH_EnableHook() failed for {hook.Method.Name} - {rv_enable}");
            return false;
        }

        _contexts[ptr_target].ptr_insert = ptr_original;
        _chains  [hook]                  = ptr_original;

        return true;
    }

    /// <inheritdoc cref="chain_insert{T}(nint, T)" />
    public bool chain_add<T>(nint ptr_target, FhHookContext hook) where T : Delegate {
        lock (_lock_chains) {
            if (_contexts.TryGetValue(ptr_target, out FhCallContext? target)) {
                if (target.tainted) {
                    FhInternal.Log.Error($"Late insertion rejected. (0x{ptr_target:X8}, {hook.fnptr.Method.Name})");
                    return false;
                }

                target.stack.Push(hook);
                return _lock_commit == 0 || chain_insert(ptr_target, hook.fnptr);
            }

            target = new(ptr_target);
            target.stack.Push(hook);

            _contexts[ptr_target] = target;
            return _lock_commit == 0 || chain_insert(ptr_target, hook.fnptr);
        }
    }

    /// <summary>
    ///     Applies all queued hooks and prohibits further hooking of already-hooked functions.
    /// </summary>
    public bool chain_finalize() {
        if (Interlocked.CompareExchange(ref _lock_commit, 1, 0) == 1)
            return true; // reject repeat calls

        lock (_lock_chains) {
            foreach ((nint ptr_target, FhCallContext target) in _contexts) {
                Stack<FhHookContext> target_stack = target.stack;

                foreach (FhHookContext hook in target_stack) {
                    if (!chain_insert(ptr_target, hook.fnptr)) {
                        return false;
                    }
                }

                target.tainted = true;
            }

            return true;
        }
    }

}
