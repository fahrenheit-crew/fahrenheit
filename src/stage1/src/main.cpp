// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

/* [fkelava 29/5/23 18:15]
 * Uses code from the .NET NativeHost sample, used under the MIT license.
 *
 * See THIRD-PARTY-NOTICES.
 *
 * For the HostFXR bits, see https://github.com/dotnet/samples/blob/main/core/hosting/src/NativeHost/nativehost.cpp.
 */

#include "fhstage1.h"

typedef void (CORECLR_DELEGATE_CALLTYPE* fh_init)(); // Function pointer to managed delegate with our own signature

using main_fn = int(*)(void);

main_fn g_fnptr_main_original = nullptr; // A function pointer to the game's original entrypoint.
main_fn g_fnptr_main_target   = nullptr; // A function pointer to our modified Stage 1 entrypoint.

wchar_t g_path_fh_dir[MAX_PATH]; // The path to the `fahrenheit/bin` directory we were started in.

hostfxr_initialize_for_runtime_config_fn g_fnptr_hostfxr_init;
hostfxr_set_runtime_property_value_fn    g_fnptr_hostfxr_set_runtime_property;
hostfxr_get_runtime_delegate_fn          g_fnptr_hostfxr_get_delegate;
hostfxr_close_fn                         g_fnptr_hostfxr_close;

FILE* g_stdout;
FILE* g_stderr;

BOOL s1_eh_suppress(); // Forward declaration of EH suppressor function

// Uses the `nethost` library to discover the location of the .NET hosting library,
// `hostfxr`, and obtains the necessary function pointers from it.
static BOOL s1_load_hostfxr(
    DWORD& error_code // [out] The error code, if the method returns FALSE.
) {
    wchar_t path_hostfxr_buf[MAX_PATH];
    size_t  path_hostfxr_size = sizeof(path_hostfxr_buf) / sizeof(wchar_t);

    int rc = get_hostfxr_path(
        path_hostfxr_buf,
        &path_hostfxr_size,
        nullptr
    );

    if (rc != 0) {
        fwprintf_s(stderr, L"[!] get_hostfxr_path() failed.\n");
        error_code = rc;

        return FALSE;
    }

    HMODULE lib_hostfxr = LoadLibraryW(path_hostfxr_buf);

    if (lib_hostfxr == nullptr) {
        fwprintf_s(stderr, L"[!] LoadLibraryW() failed.\n");
        error_code = GetLastError();

        return FALSE;
    }

    g_fnptr_hostfxr_init                 = (hostfxr_initialize_for_runtime_config_fn)GetProcAddress(lib_hostfxr, "hostfxr_initialize_for_runtime_config");
    g_fnptr_hostfxr_set_runtime_property = (hostfxr_set_runtime_property_value_fn)   GetProcAddress(lib_hostfxr, "hostfxr_set_runtime_property_value");
    g_fnptr_hostfxr_get_delegate         = (hostfxr_get_runtime_delegate_fn)         GetProcAddress(lib_hostfxr, "hostfxr_get_runtime_delegate");
    g_fnptr_hostfxr_close                = (hostfxr_close_fn)                        GetProcAddress(lib_hostfxr, "hostfxr_close");

    return g_fnptr_hostfxr_init
        && g_fnptr_hostfxr_set_runtime_property
        && g_fnptr_hostfxr_get_delegate
        && g_fnptr_hostfxr_close;
}

// Runs before the program's own entrypoint, setting up Fahrenheit.
static int s1_main(void) {
    /* [fkelava 16/09/26 17:07]
     * Sometimes Visual Studio is obstinate and won't honor breakpoints in Stage 1.
     *
     * If that happens on your system, block here and attach with WinDbg.
     */

    // If necessary, suppress the game's SEH filters, so Stage 0
    // takes over exception handling and core dumping.
    if (!s1_eh_suppress()) {
        fwprintf_s(stderr, L"Failed to suppress SEH filter.\n");
        return 1;
    }

    // Declare the name, type, and location of the bootstrap method to invoke.
    wchar_t path_fh_runtimeconfig[MAX_PATH] = { 0 };
    wchar_t path_fh_dll          [MAX_PATH] = { 0 };

    if (FAILED(StringCchCatW(path_fh_runtimeconfig, MAX_PATH, g_path_fh_dir))              ||
        FAILED(StringCchCatW(path_fh_runtimeconfig, MAX_PATH, L"\\fh.runtimeconfig.json")) ||
        FAILED(StringCchCatW(path_fh_dll,           MAX_PATH, g_path_fh_dir))              ||
        FAILED(StringCchCatW(path_fh_dll,           MAX_PATH, L"\\fh.dll"))
    ) {
        fwprintf_s(stderr, L"[!] StringCchCatW() failed.\n");
        return 1;
    }

    const wchar_t* fh_init_type   = L"Fahrenheit.FhEnvironment, fh";
    const wchar_t* fh_init_method = L"boot";

    // Load HostFxr. This library will locate the .NET runtime for us.
    DWORD load_hostfxr_rc = 0;
    if (!s1_load_hostfxr(load_hostfxr_rc)) {
        fwprintf_s(stderr, L"Fahrenheit failed to load the .NET Runtime. Ensure it is installed as per the setup guide.\n");
        return load_hostfxr_rc;
    }

    // Initialize and start the .NET runtime.
    void*          ptr_hostfxr_load_assembly        = nullptr;
    void*          ptr_hostfxr_get_function_pointer = nullptr;
    hostfxr_handle cxt                              = nullptr;

    int rc = g_fnptr_hostfxr_init(path_fh_runtimeconfig, nullptr, &cxt);
    if (rc != 0 || cxt == nullptr) {
        fwprintf_s(stderr, L"hostfxr: initialize_for_runtime_config() failed\n");
        fwprintf_s(stderr, L"This is an uncommon error. Please contact the Fahrenheit developers at https://github.com/fahrenheit-crew/fahrenheit.\n");

        g_fnptr_hostfxr_close(cxt);
        return rc;
    }

    // Set up AppContext.BaseDirectory so we can use it to find runtime dependencies.
    rc = g_fnptr_hostfxr_set_runtime_property(
        cxt,
        L"APP_CONTEXT_BASE_DIRECTORY",
        g_path_fh_dir);

    if (rc != 0) {
        fwprintf_s(stderr, L"hostfxr: failed to set APP_CONTEXT_BASE_DIRECTORY\n");
        fwprintf_s(stderr, L"This is an uncommon error. Please contact the Fahrenheit developers at https://github.com/fahrenheit-crew/fahrenheit.\n");
        return rc;
    }

    // Get function pointers to HostFxr's `load_assembly()` and `get_function_pointer()`.
    rc = g_fnptr_hostfxr_get_delegate(
        cxt,
        hdt_load_assembly,
        &ptr_hostfxr_load_assembly);

    if (rc != 0 || ptr_hostfxr_load_assembly == nullptr) {
        fwprintf_s(stderr, L"hostfxr: failed to obtain fnptr (hdt_load_assembly)\n");
        fwprintf_s(stderr, L"This is an uncommon error. Please contact the Fahrenheit developers at https://github.com/fahrenheit-crew/fahrenheit.\n");
        return rc;
    }

    rc = g_fnptr_hostfxr_get_delegate(
        cxt,
        hdt_get_function_pointer,
        &ptr_hostfxr_get_function_pointer);

    if (rc != 0 || ptr_hostfxr_get_function_pointer == nullptr) {
        fwprintf_s(stderr, L"hostfxr: failed to obtain fnptr (hdt_get_function_pointer)\n");
        fwprintf_s(stderr, L"This is an uncommon error. Please contact the Fahrenheit developers at https://github.com/fahrenheit-crew/fahrenheit.\n");
        return rc;
    }

    g_fnptr_hostfxr_close(cxt);

    load_assembly_fn        fnptr_hostfxr_load_assembly        = (load_assembly_fn)       ptr_hostfxr_load_assembly;
    get_function_pointer_fn fnptr_hostfxr_get_function_pointer = (get_function_pointer_fn)ptr_hostfxr_get_function_pointer;

    // Load managed assembly and get function pointer to bootstrap function.
    fh_init fnptr_fh_init = nullptr;

    rc = fnptr_hostfxr_load_assembly(
        path_fh_dll,
        nullptr,
        nullptr);

    if (rc != 0) {
        fwprintf_s(stderr, L"hostfxr: load_assembly() failed\n");
        fwprintf_s(stderr, L"Could not load the Fahrenheit DLL. It is in an unexpected place, or does not exist. Double-check your install.\n");
        return rc;
    }

    rc = fnptr_hostfxr_get_function_pointer(
        fh_init_type,
        fh_init_method,
        UNMANAGEDCALLERSONLY_METHOD,
        nullptr,
        nullptr,
        (void**)&fnptr_fh_init);

    if (rc != 0 || fnptr_fh_init == nullptr) {
        fwprintf_s(stderr, L"hostfxr: get_function_pointer() failed\n");
        fwprintf_s(stderr, L"Failed to locate the Fahrenheit boot function. You made a change to the bootloader, but forgot to update Stage1.\n");
        return rc;
    }

    // Boot Fahrenheit by invoking the boot function in `fh.dll`.
    fnptr_fh_init();

    // Finally, invoke the original program entrypoint.
    fwprintf_s(stdout, L"Stage 1 Loader complete. The game is now executing.\n");
    return g_fnptr_main_original();
}


// Records the directory we started from and hooks the target's entrypoint.
static BOOL s1_init(
    HMODULE h_self
) {
    // Attach to the Stage 0 console and forward stdout/stderr to it.
    if (!AttachConsole(ATTACH_PARENT_PROCESS)) {
        fwprintf_s(stderr, L"Failed to attach to the Stage 0 console with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    if (freopen_s(&g_stdout, "CONOUT$", "w", stdout) != 0 ||
        freopen_s(&g_stderr, "CONOUT$", "w", stderr) != 0
    ) {
        fwprintf_s(stderr, L"Failed to redirect standard output and error to Stage 0 console.\n");
        return FALSE;
    }

    DWORD path_fh_dir_size = GetModuleFileNameW(
        h_self,
        g_path_fh_dir,
        sizeof(g_path_fh_dir) / sizeof(wchar_t)
    );

    if (path_fh_dir_size == 0) {
        fwprintf_s(stderr, L"[!] GetModuleFileNameW() failed.\n");
        return FALSE;
    }

    HRESULT hr = PathCchRemoveFileSpec(g_path_fh_dir, MAX_PATH);
    if (hr != S_OK) {
        fwprintf_s(stderr, L"PathCchRemoveFileSpec() failed for path %s, error code: %X\n", g_path_fh_dir, hr);
        return FALSE;
    }

    // Override the program entrypoint. We need to run Fahrenheit initialization first.
    HMODULE h_target        = GetModuleHandleW(nullptr);
    LPBYTE  ptr_target_base = reinterpret_cast<LPBYTE>(h_target);

    PIMAGE_DOS_HEADER ptr_dos_headers = reinterpret_cast<PIMAGE_DOS_HEADER>(h_target);
    if (ptr_dos_headers->e_magic != IMAGE_DOS_SIGNATURE)
        return FALSE;

    PIMAGE_NT_HEADERS ptr_nt_headers  = reinterpret_cast<PIMAGE_NT_HEADERS>((ptr_target_base + ptr_dos_headers->e_lfanew));
    if (ptr_nt_headers->Signature != IMAGE_NT_SIGNATURE)
        return FALSE;

    g_fnptr_main_target = reinterpret_cast<main_fn>(ptr_target_base + ptr_nt_headers->OptionalHeader.AddressOfEntryPoint);

    if (MH_Initialize() != MH_OK
    ||  MH_CreateHook(g_fnptr_main_target, &s1_main, reinterpret_cast<void**>(&g_fnptr_main_original)) != MH_OK
    ||  MH_EnableHook(g_fnptr_main_target) != MH_OK)
        return FALSE;

    return TRUE;
}

BOOL APIENTRY DllMain(
    HMODULE h_self,
    DWORD   reason,
    LPVOID  ptr_reserved
) {
    switch (reason) {
        case DLL_PROCESS_ATTACH: {
            // Now that we're in, restore the original IAT.
            if (!DetourRestoreAfterWith())
                return FALSE;

            if (!s1_init(h_self))
                return FALSE;
        }
        case DLL_THREAD_ATTACH:
        case DLL_THREAD_DETACH:
        case DLL_PROCESS_DETACH:
            break;
    }
    return TRUE;
}
