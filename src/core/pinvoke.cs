// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

/// <summary>
///     Contains non-game, i.e. OS or library native invocations used by Fahrenheit.
/// </summary>
internal static unsafe partial class FhPInvoke {

    [StructLayout(LayoutKind.Sequential)]
    internal readonly struct DLL_LOAD_DATA {
        [InlineArray(260)]
        internal struct DllName {
            private char _c;
        }

        public readonly nint    dll_base;
        public readonly uint    dll_size;
        public readonly DllName dll_name;
    }

    [LibraryImport("fhstage1.dll", EntryPoint = "s1_bridge_register_load_cb")]
    internal static partial void s1_bridge_register_load_cb(delegate* unmanaged[Stdcall]<DLL_LOAD_DATA*, void> fnptr_cb);

    [LibraryImport("fhstage1.dll", EntryPoint = "s1_bridge_register_free_cb")]
    internal static partial void s1_bridge_register_free_cb(delegate* unmanaged[Stdcall]<nint,           void> fnptr_cb);

    /* [fkelava 07/05/26 17:10]
     * https://github.com/TsudaKageyu/minhook/blob/master/include/MinHook.h
     */

    /// <summary>
    ///     Error codes used internally by MinHook.
    /// </summary>
    internal enum MH_STATUS {
        MH_UNKNOWN = -1,
        MH_OK      = 0,
        MH_ERROR_ALREADY_INITIALIZED,
        MH_ERROR_NOT_INITIALIZED,
        MH_ERROR_ALREADY_CREATED,
        MH_ERROR_NOT_CREATED,
        MH_ERROR_ENABLED,
        MH_ERROR_DISABLED,
        MH_ERROR_NOT_EXECUTABLE,
        MH_ERROR_UNSUPPORTED_FUNCTION,
        MH_ERROR_MEMORY_ALLOC,
        MH_ERROR_MEMORY_PROTECT,
        MH_ERROR_MODULE_NOT_FOUND,
        MH_ERROR_FUNCTION_NOT_FOUND
    }

    /// <summary>
    ///     Retrieves a module handle for the specified module. The module must have been loaded by the calling process.
    /// </summary>
    [LibraryImport("kernel32.dll", EntryPoint = "GetModuleHandleW", StringMarshalling = StringMarshalling.Utf16)]
    internal static partial nint GetModuleHandle(string? lpModuleName);

#if DEBUG
    private const string hook_lib_name = "minhook.x32d.dll";
#else
    private const string hook_lib_name = "minhook.x32.dll";
#endif

    /// <summary>
    ///     Creates a hook for the specified target function, in disabled state.
    /// </summary>
    /// <param name="pTarget">A pointer to the target function, which will be overridden by the detour function.</param>
    /// <param name="pDetour">A pointer to the detour function, which will override the target function.</param>
    /// <param name="ppOriginal">A pointer to the trampoline function, which will be used to call the original target function. This parameter can be NULL.</param>
    [LibraryImport(hook_lib_name)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvStdcall) ] )]
    internal static partial MH_STATUS MH_CreateHook(
        nint  pTarget,
        nint  pDetour,
        nint* ppOriginal);

    /// <summary>
    ///     Removes an already created hook.
    /// </summary>
    /// <param name="pTarget">A pointer to the target function.</param>
    [LibraryImport(hook_lib_name)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvStdcall) ] )]
    internal static partial MH_STATUS MH_RemoveHook(nint pTarget);

    /// <summary>
    ///     Enables an already created hook.
    /// </summary>
    /// <param name="pTarget">A pointer to the target function. If this parameter is MH_ALL_HOOKS, all created hooks are enabled in one go.</param>
    [LibraryImport(hook_lib_name)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvStdcall) ] )]
    internal static partial MH_STATUS MH_EnableHook(nint pTarget);

    /// <summary>
    ///     Disables an already created hook.
    /// </summary>
    /// <param name="pTarget">A pointer to the target function. If this parameter is MH_ALL_HOOKS, all created hooks are disabled in one go.</param>
    [LibraryImport(hook_lib_name)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvStdcall) ] )]
    internal static partial MH_STATUS MH_DisableHook(nint pTarget);
}
