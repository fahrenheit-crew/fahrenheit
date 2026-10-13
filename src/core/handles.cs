// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

/// <summary>
///     Provides runtime binding to a <see cref="FhModule"/> of type <typeparamref name="T"/>.
///     You may then access the module or its <see cref="FhModuleContext"/>.
/// </summary>
public sealed class FhModuleHandle<T>(FhModule owner) where T : FhModule {
    private readonly FhModule         _owner = owner;
    private          FhModuleContext? _match;

    /// <summary>
    ///     Searches for a module of type <typeparamref name="T"/>,
    ///     caching the match if found, and returns its <see cref="FhModuleContext"/>.
    /// </summary>
    public bool try_get_context([NotNullWhen(true)] out FhModuleContext? target_context) {
        FhInternal.Log.Info($"{_owner.ModuleType} -> {typeof(T).FullName}");
        return (target_context = (_match ??= FhApi.Mods.get_module<T>())) != null;
    }

    /// <summary>
    ///     Searches for a module of type <typeparamref name="T"/>,
    ///     caching the match if found, and returns it.
    /// </summary>
    public bool try_get_module([NotNullWhen(true)] out T? target) {
        target = default;
        return try_get_context(out FhModuleContext? target_context) && (target = target_context.Module as T) != null;
    }
}

/// <summary>
///     Represents an object of type <typeparamref name="T"/> initialized at runtime.
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
public readonly ref struct FhMethodLocation {

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

    unsafe static FhMethodLocation() {
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

        string dll_name = Path.GetFileName( new(ptr_data_load->dll_name) );
        nint   dll_base = ptr_data_load->dll_base;

        _s_modules[dll_name] = dll_base;
        _s_sizes  [dll_base] = ptr_data_load->dll_size;

        Interlocked.Decrement(ref _lock);
    }

    [UnmanagedCallersOnly(CallConvs = [ typeof(CallConvStdcall) ] )]
    private unsafe static void s1_unload(FhPInvoke.DBG_MODULE_UNLOAD* ptr_data_unload) {
        Interlocked.Increment(ref _lock);

        nint unloaded_module_base = ptr_data_unload->dll_base;
        uint unloaded_module_size = 0;

        foreach ((string module_name, nint module_base) in _s_modules) {
            if (module_base != unloaded_module_base) continue;

            if (!_s_modules.Remove(module_name)
            ||  !_s_sizes  .Remove(module_base, out unloaded_module_size)) {
                throw new Exception("Failed to invalidate module records.");
            }
            break;
        }

        List<(nint, string)> invalidated_exports = [];
        foreach ((nint module_base, string export_name) in _s_exports.Keys) {
            if (module_base != unloaded_module_base) continue;

            invalidated_exports.Add((module_base, export_name));
        }

        foreach ((nint, string) export in invalidated_exports) {
            if (!_s_exports.Remove(export)) {
                throw new Exception("Failed to invalidate module exports.");
            }
        }

        Interlocked.Decrement(ref _lock);

        FhInternal.Methods.invalidate_chains(
            unloaded_module_base,
            unloaded_module_size
        );
    }

    /// <summary>
    ///     Use this constructor for functions which are analogous between FF X and X-2.
    ///     The handle implicitly targets the currently running game and selects the appropriate offset.
    /// </summary>
    public FhMethodLocation(nint offset_x, nint offset_x2) {
        bool is_ffx = FhGlobal.game_id == FhGameId.FFX;

        string module_name = is_ffx ? "FFX.exe" : "FFX-2.exe";
        nint   offset      = is_ffx ? offset_x  : offset_x2;

        _ptr_target = calc_addr(module_name, offset);
    }

    /// <summary>
    ///     Use this constructor for exported functions in external modules, such as D3D11.dll.
    /// </summary>
    public FhMethodLocation(string module_name, string fn_name) {
        _ptr_target = calc_addr(module_name, fn_name);
    }

    /// <summary>
    ///     Use this constructor for private/non-exported functions in external modules,
    ///     such as D3D11.dll, or functions exclusive to either FF X or X-2.
    /// </summary>
    public FhMethodLocation(string module_name, nint offset) {
        _ptr_target = calc_addr(module_name, offset);
    }

    /// <summary>
    ///     Use this constructor for member functions or vtable entries of objects, such as
    ///     <see cref="IDXGISwapChain.Present(uint, DXGI_PRESENT)"/>.
    /// </summary>
    /// <remarks>Unlike other constructors, no validation is performed on the input address.</remarks>
    public FhMethodLocation(nint abs_addr) {
        _ptr_target = abs_addr;
    }

    /// <inheritdoc cref="FhMethodLocation(nint)" />
    unsafe public FhMethodLocation(void* abs_addr) {
        _ptr_target = (nint)abs_addr;
    }

    /// <summary>
    ///     Gets the address of the module with the given <paramref name="module_name"/>.
    /// </summary>
    /// <returns>The address of the specified module, or zero if it is not loaded.</returns>
    private static nint get_module_addr(string module_name) {
        return _lock == 0 && _s_modules.TryGetValue(module_name, out nint ptr_module)
            ? ptr_module
            : FhPInvoke.GetModuleHandle(module_name);
    }

    /// <summary>
    ///     Gets the address of a named <paramref name="export"/>
    ///     in the module at address <paramref name="module_addr"/>.
    /// </summary>
    /// <param name="ptr_fn">The pointer to the given export, or zero if it does not exist.</param>
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
            FhInternal.Log.Error($"no export {export} in module at 0x{module_addr:X}");
        }

        return (_s_exports[key] = ptr_fn) != 0;
    }

    /// <summary>
    ///     Obtains the absolute address of a named <paramref name="export"/>
    ///     in module <paramref name="module_name"/>.
    /// </summary>
    private static nint calc_addr(string module_name, string export) {
        nint module_addr = get_module_addr(module_name);
        return module_addr != 0 && get_export(module_addr, export, out nint fn_addr)
            ? fn_addr
            : 0;
    }

    /// <summary>
    ///     Obtains the absolute address of the function at <paramref name="offset"/>
    ///     in module <paramref name="module_name"/>.
    /// </summary>
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
///     Represents a method with signature <typeparamref name="T"/>. It may then be invoked or hooked.
/// </summary>
public ref struct FhMethodHandle<T> where T : Delegate {

    private readonly nint _ptr_target;

    /// <summary>A pointer to the target function.</summary>
    /// <remarks>
    ///     By default, this includes all hooks.
    ///     To execute only part of the function's call chain, use <see cref="chain_from(T)"/>.
    /// </remarks>
    public T? fnptr {
        get {
            if (FhEnvironment.get_execution_state() != FhExecState.EXEC) {
                throw new Exception("You cannot invoke game methods until all module initializers have completed.");
            }

            return field;
        }
        private set;
    }

    public FhMethodHandle(FhMethodLocation location) {
        if (location.try_resolve(out _ptr_target)) {
            fnptr = FhInternal.Methods.get_fnptr<T>(_ptr_target);
        }
    }

    /// <summary>
    ///     Retargets the handle to only execute hooks subsequent to the given <paramref name="hook"/>.
    /// </summary>
    public FhMethodHandle<T> chain_from(T hook) {
        fnptr = FhInternal.Methods.get_fnptr_chain(hook);
        return this;
    }

    /// <summary>
    ///     Attempts to insert the given <paramref name="hook"/> into the hook chain of the target method.
    /// </summary>
    public readonly bool hook(FhModule owner, T hook) {
        FhHookContext hook_info = new(owner, hook);

        return _ptr_target != 0 && FhInternal.Methods.fnptr_chain_add<T>(_ptr_target, hook_info);
    }
}

/// <summary>
///     Pairs a hook with its owner. A stack of these constitutes the complete hook chain of a method.
/// </summary>
internal sealed record FhHookContext(
    FhModule owner,
    Delegate fnptr);

/// <summary>
///     The state of a given game method. Used to track hook insertion.
/// </summary>
internal sealed class FhMethodContext {

    internal readonly Stack<FhHookContext> stack;      // The stack of hooks inserted over this method.
    internal          nint                 ptr_insert; // The address at which the next hook in the chain is to be inserted.
    internal          bool                 tainted;    // No more hooks may be inserted.

    public FhMethodContext(nint ptr_target) {
        stack      = [];
        ptr_insert = ptr_target;
    }

}

/// <summary>
///     Keeps track of the global hook state of functions.
/// </summary>
internal sealed class FhMethods {

    private readonly static Dictionary<nint,     Delegate>        _fnptrs  = []; // Function -> Cached delegate
    private readonly static Dictionary<nint,     FhMethodContext> _methods = []; // Original -> Context
    private readonly static Dictionary<Delegate, nint>            _chain   = []; // Hook     -> Next function in chain

    private          int            _lock_commit = 0;
    private readonly AutoResetEvent _lock_chains = new AutoResetEvent(true);

    /// <summary>
    ///     Invalidates all method information in a given module.
    /// </summary>
    internal void invalidate_chains(
        nint module_base,
        uint module_size
    ) {
        _lock_chains.WaitOne();

        nint module_end = nint.CreateChecked( module_base + module_size );

        List<nint> invalidated_addrs = [];
        foreach ((nint method_addr, FhMethodContext method_context) in _methods) {
            if (method_addr < module_base || method_addr > module_end)
                continue;

            foreach (FhHookContext stack_entry in method_context.stack) {
                if (!_chain.Remove(stack_entry.fnptr, out nint fnptr) || !_fnptrs.Remove(fnptr)) {
                    throw new Exception($"Failed to purge chain entry during invalidation pass.");
                }
            }

            invalidated_addrs.Add(method_addr);
        }

        foreach (nint method_addr in invalidated_addrs) {
            if (!_methods.Remove(method_addr, out FhMethodContext? method_context)) {
                throw new Exception($"Failed to purge method during invalidation pass.");
            }
        }

        List<nint> invalidated_fnptrs = [];
        foreach ((nint method_addr, Delegate method_ptr) in _fnptrs) {
            if (method_addr < module_base || method_addr > module_end)
                continue;

            invalidated_fnptrs.Add(method_addr);
        }

        foreach (nint fnptr in invalidated_fnptrs) {
            if (!_fnptrs.Remove(fnptr)) {
                throw new Exception($"Failed to purge method during invalidation pass.");
            }
        }

        _lock_chains.Set();
    }

    /// <summary>
    ///     Caches a delegate for the function of type <typeparamref name="T"/>
    ///     at <paramref name="ptr_target"/>, or returns the cached one if it already exists.
    /// </summary>
    public T get_fnptr<T>(nint ptr_target) where T : Delegate {
        _lock_chains.WaitOne();

        if (_fnptrs.TryGetValue(ptr_target, out Delegate? fnptr) && fnptr is T t_fnptr) {
            _lock_chains.Set();
            return t_fnptr;
        }

        t_fnptr = Marshal.GetDelegateForFunctionPointer<T>(ptr_target);
        _fnptrs[ptr_target] = t_fnptr;

        _lock_chains.Set();
        return t_fnptr;
    }

    /// <summary>
    ///     For the function at <paramref name="ptr_target"/>, obtains the address at which
    ///     the next function in the chain must be inserted.
    /// </summary>
    public nint get_ptr_insert(nint ptr_target) {
        // _lock_chains must be held by this method's caller.
        return _methods.TryGetValue(ptr_target, out FhMethodContext? method_context)
            ? method_context.ptr_insert
            : ptr_target;
    }

    /// <summary>
    ///     For a given <paramref name="hook"/>, obtains the next link in its hook chain (if any exists).
    /// </summary>
    public T? get_fnptr_chain<T>(T hook) where T : Delegate {
        // _lock_chains must be held by this method's caller.
        return _chain.TryGetValue(hook, out nint ptr_chain)
            ? get_fnptr<T>(ptr_chain)
            : null;
    }

    /* [fkelava 02/06/26 18:55]
     * MinHook creates a problem for us here; it will not install two hooks for the same function.
     *
     * Given MH_CreateHook(pTarget, pDetour, &ppOriginal), we can sequence `h1`, `h2` and `h3` over a function `f` as such:
     * > MH_CreateHook(&f,             &h1, &trampoline_f);
     * > MH_CreateHook(&trampoline_f,  &h2, &trampoline_h1);
     * > MH_CreateHook(&trampoline_h1, &h3, &trampoline_h2);
     *
     * Execution follows insertion order. Earlier hooks can pre-empt later ones.
     * This goes directly against _our_ LIFO load order where we want subsequent hooks to take priority.
     *
     * One way of proceeding would be to unwind and reapply the entire hook chain, but I could not get it to work.
     * I assume this is due to https://github.com/TsudaKageyu/minhook/issues/78#issuecomment-485101354.
     *
     * Thus we impose the following rules:
     * - Hooks inserted at `init` time are queued for application.
     * - Hooks are inserted in the proper order after all modules have initialized.
     * - Hook insertion after `init` is prohibited over a function that already has any.
     * - Hooks inserted after `init` revert to executing in insertion order.
     */

    /// <summary>
    ///     Attempts to insert a given <paramref name="hook"/>
    ///     into the chain of the function at <paramref name="ptr_target"/>.
    /// </summary>
    /// <remarks><see cref="_lock_chains" /> must be held by the caller.</remarks>
    private bool fnptr_chain_insert<T>(nint ptr_target, T hook) where T : Delegate {
        // _lock_chains must be held by this method's caller.
        nint ptr_detour;
        nint ptr_insert   = get_ptr_insert(ptr_target);
        nint ptr_original = 0;

        try {
            ptr_detour = Marshal.GetFunctionPointerForDelegate(hook);
        }
        catch (Exception e) {
            FhInternal.Log.Error(e.ToString());
            return false;
        }

        // SAFETY: &ppOriginal is used as an out parameter and stack allocated
        unsafe {
            FhPInvoke.MH_STATUS rv_create = FhPInvoke.MH_CreateHook(ptr_insert, ptr_detour, &ptr_original);

            if (rv_create != FhPInvoke.MH_STATUS.MH_OK) {
                FhInternal.Log.Error($"MH_CreateHook() failed for {hook.Method.Name} - {rv_create}");
                return false;
            }
        }

        FhPInvoke.MH_STATUS rv_enable = FhPInvoke.MH_EnableHook(ptr_insert);

        if (rv_enable != FhPInvoke.MH_STATUS.MH_OK) {
            FhInternal.Log.Error($"MH_EnableHook() failed for {hook.Method.Name} - {rv_enable}");
            return false;
        }

        _methods[ptr_target].ptr_insert = ptr_original;
        _chain  [hook]                  = ptr_original;

        FhInternal.Log.Info($"(0x{ptr_target:X}) -> {hook.Method.Name}");
        return true;
    }

    /// <inheritdoc cref="fnptr_chain_insert{T}(nint, T)" />
    public bool fnptr_chain_add<T>(nint ptr_target, FhHookContext hook) where T : Delegate {
        _lock_chains.WaitOne();

        if (_methods.TryGetValue(ptr_target, out FhMethodContext? target)) {
            if (target.tainted) {
                FhInternal.Log.Error($"(0x{ptr_target:X}) - rejected late insertion of {hook.fnptr.Method.Name}");

                _lock_chains.Set();
                return false;
            }

            target.stack.Push(hook);
            _lock_chains.Set();

            return _lock_commit == 0 || fnptr_chain_insert(ptr_target, hook.fnptr);
        }

        target = new(ptr_target);
        target.stack.Push(hook);

        _methods[ptr_target] = target;
        _lock_chains.Set();

        return _lock_commit == 0 || fnptr_chain_insert(ptr_target, hook.fnptr);
    }

    /// <summary>
    ///     Applies all hooks registered at the time of calling and
    ///     prohibits further insertion over functions with any hooks registered.
    /// </summary>
    public bool commit() {
        if (Interlocked.CompareExchange(ref _lock_commit, 1, 0) == 1)
            return true; // reject repeat calls

        _lock_chains.WaitOne();

        foreach ((nint ptr_target, FhMethodContext target) in _methods) {
            Stack<FhHookContext> target_stack = target.stack;

            foreach (FhHookContext hook in target_stack) {
                if (!fnptr_chain_insert(ptr_target, hook.fnptr)) {
                    _lock_chains.Set();
                    return false;
                }
            }

            target.tainted = true;
        }

        return _lock_chains.Set();
    }

}
