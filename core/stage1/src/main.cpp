/* [fkelava 29/5/23 18:15]
 * Uses code from the .NET NativeHost sample, used under the MIT license.
 *
 * See THIRD-PARTY-NOTICES.
 */

#include "fhstage1.h"

typedef void (CORECLR_DELEGATE_CALLTYPE* fh_ldr_managed_init)();

using string_t = std::basic_string<char_t>;
using main_t   = int(*)(void);

main_t pMainOriginal = nullptr; // Original program entrypoint.
main_t pMainTarget   = nullptr; // Detour of program entrypoint.

namespace {
    // Globals to hold hostfxr exports
    hostfxr_initialize_for_runtime_config_fn init_fptr;
    hostfxr_get_runtime_delegate_fn          get_delegate_fptr;
    hostfxr_close_fn                         close_fptr;

    // Forward declarations
    bool load_hostfxr();
}

/********************************************************************************************
 * Function used to load and activate .NET Core
 ********************************************************************************************/

namespace {
    // Forward declarations
    void *load_library(const char_t *);
    void *get_export(void *, const char *);

#ifdef _WIN32
    void *load_library(const char_t *path)
    {
        HMODULE h = ::LoadLibraryW(path);
        assert(h != nullptr);
        return (void*)h;
    }
    void *get_export(void *h, const char *name)
    {
        void *f = ::GetProcAddress((HMODULE)h, name);
        assert(f != nullptr);
        return f;
    }
#else
    void *load_library(const char_t *path)
    {
        void *h = dlopen(path, RTLD_LAZY | RTLD_LOCAL);
        assert(h != nullptr);
        return h;
    }
    void *get_export(void *h, const char *name)
    {
        void *f = dlsym(h, name);
        assert(f != nullptr);
        return f;
    }
#endif

    // Using the nethost library, discover the location of hostfxr and get exports
    bool load_hostfxr() {
        // Pre-allocate a large buffer for the path to hostfxr
        char_t buffer[MAX_PATH];
        size_t buffer_size = sizeof(buffer) / sizeof(char_t);

        int rc = get_hostfxr_path(buffer, &buffer_size, nullptr);
        if (rc != 0)
            return false;

        // Load hostfxr and get desired exports
        void* lib         = load_library(buffer);
        init_fptr         = (hostfxr_initialize_for_runtime_config_fn)get_export(lib, "hostfxr_initialize_for_runtime_config");
        get_delegate_fptr = (hostfxr_get_runtime_delegate_fn)         get_export(lib, "hostfxr_get_runtime_delegate");
        close_fptr        = (hostfxr_close_fn)                        get_export(lib, "hostfxr_close");

        return (init_fptr && get_delegate_fptr && close_fptr);
    }
}

static int DetourMain(void) {
    //
    // STEP 1:
    // Attach to the Stage0 console and forward stdout/stderr to it.
    //
    AttachConsole(ATTACH_PARENT_PROCESS);

    FILE* parent_stdout;
    FILE* parent_stderr;

    if (freopen_s(&parent_stdout, "CONOUT$", "w", stdout) != 0 ||
        freopen_s(&parent_stderr, "CONOUT$", "w", stderr) != 0) {
        exit(EXIT_FAILURE);
    }

    //
    // STEP 2:
    // Determine the current working directory and the location
    // of the executable being launched, to which we will swap
    // the working directory later.
    //
    char_t host_path_buf[MAX_PATH]; // where is the game?
    char_t cwd_path_buf [MAX_PATH]; // where are _we_?

    auto size     = ::GetModuleFileName  (NULL, host_path_buf, sizeof(host_path_buf) / sizeof(char_t));
    auto cwd_size = ::GetCurrentDirectory(sizeof(cwd_path_buf) / sizeof(char_t), cwd_path_buf);

    if (size == 0 || cwd_size == 0) {
        std::wcout << "Cannot determine game directory.\n";
        exit(EXIT_FAILURE);
    }

    if (cwd_size == 0) {
        std::wcout << "Cannot determine working directory.\n";
        exit(EXIT_FAILURE);
    }

    string_t host_path = host_path_buf;
    string_t cwd_path  = cwd_path_buf;

    //
    // STEP 3:
    // Declare the name, type, and location of the bootstrap method to invoke.
    //
    const string_t clrhost_config_path = cwd_path + STR("\\fh.runtimeconfig.json");
    const string_t clrhost_lib_path    = cwd_path + STR("\\fh.dll");
    const char_t*  clrhost_type        = STR("Fahrenheit.FhEnvironment, fh");
    const char_t*  clrhost_init_method = STR("boot");

    auto host_dirsep_pos = host_path.find_last_of(DIR_SEPARATOR);

    if (host_dirsep_pos == string_t::npos) {
        std::wcout << "Cannot normalize game directory path.\n";
        exit(EXIT_FAILURE);
    }

    std::wcout << "Stage 1 Loader executing for: " << host_path << std::endl;

    host_path = host_path.substr(0, host_dirsep_pos + 1);

    //
    // STEP 4:
    // Load HostFxr.
    //
    if (!load_hostfxr()) {
        std::cout << "load_hostfxr() failed\n";
        exit(EXIT_FAILURE);
    }

    //
    // STEP 5:
    // Initialize and start the .NET Core runtime.
    //
    void*          load_assembly_fptr        = nullptr;
    void*          get_function_pointer_fptr = nullptr;
    hostfxr_handle cxt                       = nullptr;

    int rc = init_fptr(clrhost_config_path.c_str(), nullptr, &cxt);
    if (rc != 0 || cxt == nullptr) {
        std::wcerr << "Init failed: " << std::hex << std::showbase << rc << std::endl;
        close_fptr(cxt);
        exit(EXIT_FAILURE);
    }

    //
    // STEP 6:
    // Get function pointers to HostFxr's `load_assembly()` and `get_function_pointer()`.
    //
    rc = get_delegate_fptr(
        cxt,
        hdt_load_assembly,
        &load_assembly_fptr);

    if (rc != 0 || load_assembly_fptr == nullptr)
        std::wcerr << "Get delegate failed: " << std::hex << std::showbase << rc << std::endl;

    rc = get_delegate_fptr(
        cxt,
        hdt_get_function_pointer,
        &get_function_pointer_fptr);

    if (rc != 0 || get_function_pointer_fptr == nullptr)
        std::wcerr << "Get delegate failed: " << std::hex << std::showbase << rc << std::endl;

    close_fptr(cxt);

    load_assembly_fn        load_assembly        = (load_assembly_fn)       load_assembly_fptr;
    get_function_pointer_fn get_function_pointer = (get_function_pointer_fn)get_function_pointer_fptr;

    if (load_assembly == nullptr) {
        std::wcout << "get_dotnet_load_assembly() failed\n";
        exit(EXIT_FAILURE);
    }

    //
    // STEP 7:
    // Load managed assembly and get function pointer to bootstrap function.
    //
    fh_ldr_managed_init fh_init = nullptr;

    rc = load_assembly(
        clrhost_lib_path.c_str(),
        nullptr,
        nullptr);

    if (rc != 0) {
        std::wcout << "Failed to load managed assembly.\n";
        exit(EXIT_FAILURE);
    }

    rc = get_function_pointer(
        clrhost_type,
        clrhost_init_method,
        UNMANAGEDCALLERSONLY_METHOD,
        nullptr,
        nullptr,
        (void**)&fh_init);

    if (rc != 0 || fh_init == nullptr) {
        std::wcout << "Failed to get pointer to bootstrap function.\n";
        exit(EXIT_FAILURE);
    }

    // TRANSITION: NATIVE -> MANAGED
    fh_init();
    // TRANSITION: MANAGED -> NATIVE

    //
    // STEP 8:
    // Change the working directory to the targeted executable's location,
    // now that we have finished initialization.
    //
    int chrc = _wchdir(host_path.c_str());
    if (chrc != 0) {
        std::wcout << "_wchdir failed, rc:" << chrc << std::endl;
        exit(EXIT_FAILURE);
    }

    std::wcout << "Stage 1 Loader complete: execution will now resume.\n";
    return pMainOriginal();
}

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     ) {
    switch (ul_reason_for_call) {
        case DLL_PROCESS_ATTACH: {
            if (!DetourRestoreAfterWith()) exit(GetLastError());

            auto hMainModule    = reinterpret_cast<HMODULE>          (NtCurrentTeb()->ProcessEnvironmentBlock->Reserved3[1]);
            auto pImgDosHeaders = reinterpret_cast<PIMAGE_DOS_HEADER>(hMainModule);
            if (pImgDosHeaders->e_magic  != IMAGE_DOS_SIGNATURE) return TRUE;

            auto pImgNTHeaders = reinterpret_cast<PIMAGE_NT_HEADERS> ((reinterpret_cast<LPBYTE>(pImgDosHeaders) + pImgDosHeaders->e_lfanew));
            if (pImgNTHeaders->Signature != IMAGE_NT_SIGNATURE) return TRUE;

            pMainTarget = reinterpret_cast<main_t>(pImgNTHeaders->OptionalHeader.AddressOfEntryPoint + reinterpret_cast<LPBYTE>(hMainModule));

            if (MH_Initialize()                                                                   != MH_OK) return TRUE;
            if (MH_CreateHook(pMainTarget, &DetourMain, reinterpret_cast<void**>(&pMainOriginal)) != MH_OK) return TRUE;
            if (MH_EnableHook(pMainTarget)                                                        != MH_OK) return TRUE;
        }
        case DLL_THREAD_ATTACH:
        case DLL_THREAD_DETACH:
        case DLL_PROCESS_DETACH:
            break;
    }
    return TRUE;
}

