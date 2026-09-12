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

char_t path_target_buf[MAX_PATH]; // The path to the binary we're being loaded into.
char_t path_fh_buf    [MAX_PATH]; // The path to the `fahrenheit/bin` directory we were started in.

hostfxr_initialize_for_runtime_config_fn g_fnptr_hostfxr_init;
hostfxr_set_runtime_property_value_fn    g_fnptr_hostfxr_set_runtime_property;
hostfxr_get_runtime_delegate_fn          g_fnptr_hostfxr_get_delegate;
hostfxr_close_fn                         g_fnptr_hostfxr_close;

FILE* g_stdout;
FILE* g_stderr;

BOOL stage1_eh_suppress(LPBYTE ptr_main_module); // Forward declaration of EH suppressor function

/*
 * Uses the `nethost` library to discover the location of the .NET hosting library,
 * `hostfxr`, and obtains the necessary function pointers from it.
 */

static bool load_hostfxr() {
    char_t path_hostfxr_buf[MAX_PATH];
    size_t path_hostfxr_size = sizeof(path_hostfxr_buf) / sizeof(char_t);

    int rc = get_hostfxr_path(path_hostfxr_buf, &path_hostfxr_size, nullptr);
    if (rc != 0) {
        std::wcerr << "get_hostfxr_path() failed, error code: " << rc << std::endl;
        return false;
    }

    HMODULE lib_hostfxr = ::LoadLibraryW(path_hostfxr_buf);

    if (lib_hostfxr == nullptr)
        return FALSE;

    g_fnptr_hostfxr_init                 = (hostfxr_initialize_for_runtime_config_fn)::GetProcAddress(lib_hostfxr, "hostfxr_initialize_for_runtime_config");
    g_fnptr_hostfxr_set_runtime_property = (hostfxr_set_runtime_property_value_fn)   ::GetProcAddress(lib_hostfxr, "hostfxr_set_runtime_property_value");
    g_fnptr_hostfxr_get_delegate         = (hostfxr_get_runtime_delegate_fn)         ::GetProcAddress(lib_hostfxr, "hostfxr_get_runtime_delegate");
    g_fnptr_hostfxr_close                = (hostfxr_close_fn)                        ::GetProcAddress(lib_hostfxr, "hostfxr_close");

    return g_fnptr_hostfxr_init
        && g_fnptr_hostfxr_set_runtime_property
        && g_fnptr_hostfxr_get_delegate
        && g_fnptr_hostfxr_close;
}

// Runs before the program's own entrypoint, setting up Fahrenheit.
static int stage1_main(void) {
    // STEP 5:
    // If necessary, suppress the game's SEH filters, so Stage 0
    // takes over exception handling and core dumping.
    HMODULE hMainModule = GetModuleHandleW(nullptr);
    LPBYTE  pMainModule = reinterpret_cast<LPBYTE>(hMainModule);

    if (!stage1_eh_suppress(pMainModule)) {
        std::wcerr << "Failed to suppress SEH filter." << std::endl;
        exit(EXIT_FAILURE);
    }

    // STEP 6:
    // Declare the name, type, and location of the bootstrap method to invoke.
    std::basic_string<char_t> path_cwd = path_fh_buf;

    const std::basic_string<char_t> path_fh_runtimeconfig = path_cwd + L"\\fh.runtimeconfig.json";
    const std::basic_string<char_t> path_fh_dll           = path_cwd + L"\\fh.dll";

    const char_t* fh_init_type   = L"Fahrenheit.FhEnvironment, fh";
    const char_t* fh_init_method = L"boot";

    // STEP 7:
    // Load HostFxr. This library will locate the .NET runtime for us.
    if (!load_hostfxr()) {
        std::wcerr << "hostfxr: failed to load" << std::endl;
        std::wcerr << "Fahrenheit failed to load the .NET Runtime. Ensure it is installed as per the setup guide." << std::endl;
        exit(EXIT_FAILURE);
    }

    // STEP 8:
    // Initialize and start the .NET runtime.
    void*          ptr_hostfxr_load_assembly        = nullptr;
    void*          ptr_hostfxr_get_function_pointer = nullptr;
    hostfxr_handle cxt                              = nullptr;

    int rc = g_fnptr_hostfxr_init(path_fh_runtimeconfig.c_str(), nullptr, &cxt);
    if (rc != 0 || cxt == nullptr) {
        std::wcerr << "hostfxr: initialize_for_runtime_config() failed" << std::endl;
        std::wcerr << "This is an uncommon error. Please contact the Fahrenheit developers at https://github.com/fahrenheit-crew/fahrenheit." << std::endl;

        g_fnptr_hostfxr_close(cxt);
        exit(rc);
    }

    // STEP 9:
    // Set up AppContext.BaseDirectory so we can use it to find runtime dependencies.
    rc = g_fnptr_hostfxr_set_runtime_property(
        cxt,
        L"APP_CONTEXT_BASE_DIRECTORY",
        path_cwd.c_str());

    if (rc != 0) {
        std::wcerr << "hostfxr: failed to set APP_CONTEXT_BASE_DIRECTORY" << std::endl;
        std::wcerr << "This is an uncommon error. Please contact the Fahrenheit developers at https://github.com/fahrenheit-crew/fahrenheit." << std::endl;
        exit(rc);
    }

    // STEP 10:
    // Get function pointers to HostFxr's `load_assembly()` and `get_function_pointer()`.
    rc = g_fnptr_hostfxr_get_delegate(
        cxt,
        hdt_load_assembly,
        &ptr_hostfxr_load_assembly);

    if (rc != 0 || ptr_hostfxr_load_assembly == nullptr) {
        std::wcerr << "hostfxr: failed to obtain fnptr (hdt_load_assembly)" << std::endl;
        std::wcerr << "This is an uncommon error. Please contact the Fahrenheit developers at https://github.com/fahrenheit-crew/fahrenheit." << std::endl;
        exit(rc);
    }

    rc = g_fnptr_hostfxr_get_delegate(
        cxt,
        hdt_get_function_pointer,
        &ptr_hostfxr_get_function_pointer);

    if (rc != 0 || ptr_hostfxr_get_function_pointer == nullptr) {
        std::wcerr << "hostfxr: failed to obtain fnptr (hdt_get_function_pointer)"  << std::endl;
        std::wcerr << "This is an uncommon error. Please contact the Fahrenheit developers at https://github.com/fahrenheit-crew/fahrenheit." << std::endl;
        exit(rc);
    }

    g_fnptr_hostfxr_close(cxt);

    load_assembly_fn        fnptr_hostfxr_load_assembly        = (load_assembly_fn)       ptr_hostfxr_load_assembly;
    get_function_pointer_fn fnptr_hostfxr_get_function_pointer = (get_function_pointer_fn)ptr_hostfxr_get_function_pointer;

    // STEP 11:
    // Load managed assembly and get function pointer to bootstrap function.
    fh_init fnptr_fh_init = nullptr;

    rc = fnptr_hostfxr_load_assembly(
        path_fh_dll.c_str(),
        nullptr,
        nullptr);

    if (rc != 0) {
        std::wcerr << "hostfxr: load_assembly() failed" << std::endl;
        std::wcerr << "Could not load the Fahrenheit DLL. It is in an unexpected place, or does not exist. Double-check your install." << std::endl;
        exit(rc);
    }

    rc = fnptr_hostfxr_get_function_pointer(
        fh_init_type,
        fh_init_method,
        UNMANAGEDCALLERSONLY_METHOD,
        nullptr,
        nullptr,
        (void**)&fnptr_fh_init);

    if (rc != 0 || fnptr_fh_init == nullptr) {
        std::wcerr << "hostfxr: get_function_pointer() failed" << std::endl;
        std::wcerr << "Failed to locate the Fahrenheit boot function. You made a change to the bootloader, but forgot to update Stage1." << std::endl;
        exit(rc);
    }

    // STEP 12:
    // Boot Fahrenheit by invoking the boot function in `fh.dll`.

    // TRANSITION: NATIVE -> MANAGED
    fnptr_fh_init();
    // TRANSITION: MANAGED -> NATIVE

    // STEP 13:
    // Let the game run. Enjoy!
    std::wcout << "Stage 1 Loader complete. The game is now executing." << std::endl;
    return g_fnptr_main_original();
}

/* [fkelava 21/08/26 14:09]
 * Records the name of the module we're being loaded into and the directory we started from,
 * switches the current working directory to the target's, and hooks its entrypoint.
 *
 * This is required because certain other tools expect the game's working directory to be
 * unmodified when they load into the process, which occurs immediately after Stage 1 exits `DllMain`.
 */
static BOOL stage1_init() {
    // STEP 2:
    // Attach to the Stage0 console and forward stdout/stderr to it.
    if (!AttachConsole(ATTACH_PARENT_PROCESS)) {
        std::wcerr << "Failed to attach to the Stage0 console." << std::endl;
        exit(GetLastError());
    }

    if (freopen_s(&g_stdout, "CONOUT$", "w", stdout) != 0 ||
        freopen_s(&g_stderr, "CONOUT$", "w", stderr) != 0) {
        std::wcerr << "Failed to redirect standard input, output and error to Stage0 console." << std::endl;
        exit(EXIT_FAILURE);
    }

    auto path_target_size = ::GetModuleFileNameW(
        NULL,
        path_target_buf,
        sizeof(path_target_buf) / sizeof(char_t)
    );

    auto path_cwd_size = ::GetCurrentDirectoryW(
        sizeof(path_fh_buf) / sizeof(char_t),
        path_fh_buf
    );

    if (path_target_size == 0) {
        std::wcerr << "GetModuleFileName() failed." << std::endl;
        return FALSE;
    }

    if (path_cwd_size == 0) {
        std::wcerr << "GetCurrentDirectory() failed." << std::endl;
        return FALSE;
    }

    std::basic_string<char_t> target_path = path_target_buf;
    std::wcout << "Stage 1 Loader executing for: " << target_path << std::endl;

    HRESULT hr = PathCchRemoveFileSpec(path_target_buf, MAX_PATH);
    if (hr != S_OK) {
        std::wcerr << "PathCchRemoveFileSpec() failed, error code: " << hr << std::endl;
        return FALSE;
    }

    target_path = path_target_buf;

    // STEP 3:
    // Change the working directory to the targeted executable's location.
    int rc = _wchdir(target_path.c_str());
    if (rc != 0) {
        std::wcerr << "Failed to switch to the game's working directory." << std::endl;
        return FALSE;
    }

    // STEP 4:
    // Override the program entrypoint. We need to run Fahrenheit initialization first.
    HMODULE hMainModule = GetModuleHandleW(nullptr);
    LPBYTE  pMainModule = reinterpret_cast<LPBYTE>(hMainModule);

    PIMAGE_DOS_HEADER pImgDosHeaders = reinterpret_cast<PIMAGE_DOS_HEADER>(hMainModule);
    if (pImgDosHeaders->e_magic != IMAGE_DOS_SIGNATURE)
        return FALSE;

    PIMAGE_NT_HEADERS pImgNTHeaders = reinterpret_cast<PIMAGE_NT_HEADERS>((pMainModule + pImgDosHeaders->e_lfanew));
    if (pImgNTHeaders->Signature != IMAGE_NT_SIGNATURE)
        return FALSE;

    g_fnptr_main_target = reinterpret_cast<main_fn>(pMainModule + pImgNTHeaders->OptionalHeader.AddressOfEntryPoint);

    if (MH_Initialize() != MH_OK
    ||  MH_CreateHook(g_fnptr_main_target, &stage1_main, reinterpret_cast<void**>(&g_fnptr_main_original)) != MH_OK
    ||  MH_EnableHook(g_fnptr_main_target) != MH_OK)
        return FALSE;

    return TRUE;
}

BOOL APIENTRY DllMain(
    HMODULE hdll,
    DWORD   reason,
    LPVOID  ptr_reserved
) {
    switch (reason) {
        case DLL_PROCESS_ATTACH: {
            // STEP 1:
            // Return the IAT to its original self.
            if (!DetourRestoreAfterWith())
                return FALSE;

            if (!stage1_init())
                return FALSE;
        }
        case DLL_THREAD_ATTACH:
        case DLL_THREAD_DETACH:
        case DLL_PROCESS_DETACH:
            break;
    }
    return TRUE;
}
