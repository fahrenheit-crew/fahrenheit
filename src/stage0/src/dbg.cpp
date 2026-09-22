// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

/* [fkelava 12/09/26 23:26]
 * As Fahrenheit is a .NET modding system for native binaries, it follows that debugging
 * and stack walking must be carried out in "mixed" mode. Any errors that occur should
 * ideally include both managed and native frames for the developer's convenience.
 * Some systems, like Dalamud, implement this using a dedicated crash handler process.
 *
 * We go the other way around- Stage 0 is repurposed as a stub debugger that "handles"
 * exception events, triggers core dumping, and surfaces exception information to the end user.
 *
 * If a proper external debugger is connected, this functionality is disabled.
 */

#include "fhstage0.h"

enum S0_FRAME_TYPE {
    FRAME_UNKNOWN,
    FRAME_NATIVE,
    FRAME_MANAGED
};

wchar_t g_path_dir_cache[MAX_PATH] = { 0 }; // The full path to the 'cache' directory, used to store symbols.
wchar_t g_path_dir_crash[MAX_PATH] = { 0 }; // The full path to the 'crash' directory, used to store core dumps.

wchar_t g_path_coreclr     [MAX_PATH] = { 0 }; // The full path to the loaded CoreCLR.
wchar_t g_path_mscordbi    [MAX_PATH] = { 0 }; // The full path to the `mscordbi` module for the given CoreCLR.
wchar_t g_path_mscordacwks [MAX_PATH] = { 0 }; // The full path to the `mscordacwks` module for the given CoreCLR.
wchar_t g_path_mscordaccore[MAX_PATH] = { 0 }; // The full path to the `mscordaccore` module for the given CoreCLR.

std::set   <std::wstring>  g_map_checked_symbol; // Whether we performed symbol file lookup for a given module.
std::vector<std::wstring>  g_frames_managed;     // A list of managed frame strings. Used to fill the gaps in the native stack walk.
std::vector<std::wstring>  g_frames_native;      // A list of native frame strings.
std::vector<S0_FRAME_TYPE> g_frames_type;        // A list of frames, indicating the type of any given frame.

LPVOID g_ptr_coreclr; // The pointer to `coreclr.dll` in memory.

/* [fkelava 19/09/26 00:50]
 * Here we simultaneously borrow a bit and yet diverge from Dalamud.
 * The different choice of interface seems more cosmetic than anything, though.
 *
 * https://github.com/goatcorp/Dalamud/blob/e81744f6aea94bb6781affdd0d0b9319592f95d9/DalamudCrashHandler/DalamudCrashHandler.cpp#L306
 *
 * Due to the almost _nonexistent_ documentation, any difference in approach is to be taken
 * as the unfortunate result of a goat and a donkey having to stumble around in the dark.
 */

// https://learn.microsoft.com/en-us/dotnet/framework/unmanaged-api/debugging/iclrdebugginglibraryprovider-interface
class S0_ICLRDebuggingLibraryProvider : public ICLRDebuggingLibraryProvider {
    /* [fkelava 19/09/26 01:36]
     * https://learn.microsoft.com/en-us/windows/win32/api/unknwn/nf-unknwn-iunknown-addref
     * > The internal reference counter that AddRef maintains should be a 32-bit unsigned integer.
     */
    ULONG _refs;

public:
    S0_ICLRDebuggingLibraryProvider() {
        _refs = 1;
    }

    virtual ~S0_ICLRDebuggingLibraryProvider() = default;

    // https://learn.microsoft.com/en-us/windows/win32/api/unknwn/nf-unknwn-iunknown-queryinterface(refiid_void)
    HRESULT __stdcall QueryInterface(REFIID riid, LPVOID* ppvObj) override {
        if (ppvObj == nullptr)
            return E_POINTER;

        *ppvObj = nullptr;
        if (riid != IID_IUnknown && riid != IID_ICLRDebuggingLibraryProvider)
            return E_NOINTERFACE;

        *ppvObj = (LPVOID) this;
        AddRef();

        return S_OK;
    }

    // https://learn.microsoft.com/en-us/windows/win32/api/unknwn/nf-unknwn-iunknown-addref
    ULONG __stdcall AddRef() override {
        return InterlockedIncrement(&_refs);
    }

    // https://learn.microsoft.com/en-us/windows/win32/api/unknwn/nf-unknwn-iunknown-release
    ULONG __stdcall Release() override {
        ULONG remaining_refs = InterlockedDecrement(&_refs);

        if (remaining_refs == 0)
            delete this;

        return remaining_refs;
    }

    // https://learn.microsoft.com/en-us/dotnet/framework/unmanaged-api/debugging/iclrdebugginglibraryprovider-providelibrary-method
    HRESULT __stdcall ProvideLibrary(
        const WCHAR*   pwszFileName,
              DWORD    dwTimestamp,
              DWORD    dwSizeOfImage,
              HMODULE* hModule
    ) {
        /* [fkelava 21/09/26 02:17]
         * This interface exists to supply the debugging engine the
         * correct version of DBI and DAC when the debugger is operating
         * on a CoreCLR version it does not have locally installed.
         *
         * But since we only "debug" live processes, we know the correct
         * DAC/DBI exists and is right next to `coreclr.dll`.
         */
        if (wcscmp(pwszFileName, L"mscordbi.dll") == 0) {
            *hModule = LoadLibraryW(g_path_mscordbi);
            return S_OK;
        }

        if (wcscmp(pwszFileName, L"mscordaccore.dll") == 0) {
            *hModule = LoadLibraryW(g_path_mscordaccore);
            return S_OK;
        }

        if (wcscmp(pwszFileName, L"mscordacwks.dll") == 0) {
            *hModule = LoadLibraryW(g_path_mscordacwks);
            return S_OK;
        }

        return E_FAIL;
    }
};

// https://learn.microsoft.com/en-us/dotnet/core/unmanaged-api/debugging/icordebug/icordebugdatatarget-interface
class S0_ICorDebugDataTarget : public ICorDebugDataTarget {
    ULONG  _refs;
    HANDLE _h_process;

public:
    S0_ICorDebugDataTarget(HANDLE h_process) {
        _refs      = 1;
        _h_process = h_process;
    }

    virtual ~S0_ICorDebugDataTarget() = default;

    // https://learn.microsoft.com/en-us/windows/win32/api/unknwn/nf-unknwn-iunknown-queryinterface(refiid_void)
    HRESULT __stdcall QueryInterface(REFIID riid, LPVOID* ppvObj) override {
        if (ppvObj == nullptr)
            return E_POINTER;

        *ppvObj = nullptr;
        if (riid != IID_IUnknown && riid != IID_ICorDebugDataTarget)
            return E_NOINTERFACE;

        *ppvObj = (LPVOID) this;
        AddRef();

        return S_OK;
    }

    // https://learn.microsoft.com/en-us/windows/win32/api/unknwn/nf-unknwn-iunknown-addref
    ULONG __stdcall AddRef() override {
        return InterlockedIncrement(&_refs);
    }

    // https://learn.microsoft.com/en-us/windows/win32/api/unknwn/nf-unknwn-iunknown-release
    ULONG __stdcall Release() override {
        ULONG remaining_refs = InterlockedDecrement(&_refs);

        if (remaining_refs == 0)
            delete this;

        return remaining_refs;
    }

    // https://learn.microsoft.com/en-us/dotnet/core/unmanaged-api/debugging/icordebug/icordebugdatatarget-getplatform-method
    HRESULT __stdcall GetPlatform(CorDebugPlatform* pTargetPlatform) override {
        *pTargetPlatform = CORDB_PLATFORM_WINDOWS_X86;
        return S_OK;
    }

    // https://learn.microsoft.com/en-us/dotnet/core/unmanaged-api/debugging/icordebug/icordebugdatatarget-readvirtual-method
    HRESULT __stdcall ReadVirtual(
        CORDB_ADDRESS address,
        BYTE*         pBuffer,
        ULONG32       bytesRequested,
        ULONG32*      pBytesRead
    ) override {
        return ReadProcessMemory(_h_process, (LPCVOID) address, pBuffer, bytesRequested, (SIZE_T*) pBytesRead)
            ? S_OK
            : HRESULT_FROM_WIN32(GetLastError());
    }

    // https://learn.microsoft.com/en-us/dotnet/core/unmanaged-api/debugging/icordebug/icordebugdatatarget-getthreadcontext-method
    HRESULT __stdcall GetThreadContext(
        DWORD   dwThreadID,
        ULONG32 contextFlags,
        ULONG32 contextSize,
        BYTE*   pContext
    ) override {
        if (contextSize < sizeof(CONTEXT))
            return E_INVALIDARG;

        CONTEXT* ptr_context = (CONTEXT*) pContext;
        ptr_context->ContextFlags = contextFlags;

        HANDLE h_thread = OpenThread(THREAD_GET_CONTEXT, FALSE, dwThreadID);

        if (h_thread == nullptr || h_thread == INVALID_HANDLE_VALUE) {
            fwprintf_s(stderr, L"[!] OpenThread failed for thread 0x%X.\n", dwThreadID);
            return HRESULT_FROM_WIN32(GetLastError());
        }

        if (!::GetThreadContext(h_thread, ptr_context)) {
            fwprintf_s(stderr, L"[!] GetThreadContext failed for thread 0x%X.\n", dwThreadID);
            return HRESULT_FROM_WIN32(GetLastError());
        }

        return S_OK;
    }

};

// Prepares the necessary DLL paths for CLR debugging.
static BOOL s0_dbg_clr_init(
    LPVOID ptr_coreclr, // The pointer to the image base of the `coreclr.dll` for this session.
    LPWSTR path_coreclr // The full path to `coreclr.dll` for this session.
) {
    /* [fkelava 20/09/26 23:06]
     * If we were using the CLR debugging function CreateVersionStringFromModule,
     * we would have to strip the \\?\ prefix from paths.
     */
    g_ptr_coreclr = ptr_coreclr;

    HRESULT hr = StringCchCopyW(g_path_coreclr, MAX_PATH, path_coreclr);
    if (hr != S_OK) {
        fwprintf_s(stderr, L"[!] StringCchCopyW(%s) failed with code 0x%X.\n", path_coreclr, hr);
        return FALSE;
    }

    wchar_t folder_coreclr[MAX_PATH] = { 0 };

    hr = StringCchCopyW(folder_coreclr, MAX_PATH, g_path_coreclr);
    if (hr != S_OK) {
        fwprintf_s(stderr, L"[!] StringCchCopyW(%s) failed with code 0x%X.\n", g_path_coreclr, hr);
        return FALSE;
    }

    hr = PathCchRemoveFileSpec(folder_coreclr, MAX_PATH);
    if (hr != S_OK) {
        fwprintf_s(stderr, L"[!] PathCchRemoveFileSpec(%s) failed with code 0x%X.\n", folder_coreclr, hr);
        return FALSE;
    }

    if (FAILED(StringCchCatW(g_path_mscordbi,     MAX_PATH, folder_coreclr))        ||
        FAILED(StringCchCatW(g_path_mscordbi,     MAX_PATH, L"\\mscordbi.dll"))     ||
        FAILED(StringCchCatW(g_path_mscordaccore, MAX_PATH, folder_coreclr))        ||
        FAILED(StringCchCatW(g_path_mscordaccore, MAX_PATH, L"\\mscordaccore.dll")) ||
        FAILED(StringCchCatW(g_path_mscordacwks,  MAX_PATH, folder_coreclr))        ||
        FAILED(StringCchCatW(g_path_mscordacwks,  MAX_PATH, L"\\mscordacwks.dll"))
    ) {
        fwprintf_s(stderr, L"[!] StringCchCatW() failed.\n");
        return FALSE;
    }

    return TRUE;
}

// Performs a managed stack walk, gathering any available symbols.
static HRESULT s0_dbg_stack_walk_managed(
    HANDLE h_process, // A handle to the process the fault occurred in.
    DWORD  id_thread  // The ID of the faulting thread in the process that encountered an exception.
) {
    ICLRDebugging*      ptr_ICLRDebugging      = nullptr;
    ICorDebugProcess*   ptr_ICorDebugProcess   = nullptr;
    ICorDebugThread*    ptr_ICorDebugThread    = nullptr;
    ICorDebugThread3*   ptr_ICorDebugThread3   = nullptr;
    ICorDebugStackWalk* ptr_ICorDebugStackWalk = nullptr;
    ICorDebugFrame*     ptr_ICorDebugFrame     = nullptr;
    ICorDebugFunction*  ptr_ICorDebugFunction  = nullptr;
    ICorDebugModule*    ptr_ICorDebugModule    = nullptr;
    IMetaDataImport*    ptr_IMetaDataImport    = nullptr;

    HRESULT hr = CLRCreateInstance(CLSID_CLRDebugging, IID_ICLRDebugging, (LPVOID*) &ptr_ICLRDebugging);
    if (hr != S_OK) {
        fwprintf_s(stderr, L"[!] CLRCreateInstance failed with code 0x%X.\n", hr);
        return hr;
    }

    /* [fkelava 19/09/26 00:02]
     * https://learn.microsoft.com/en-us/dotnet/framework/unmanaged-api/debugging/iclrdebugging-openvirtualprocess-method
     * > You should specify the major, minor, and build versions from the
     * > latest CLR version this debugger supports, and set the revision
     * > number to 65535 to accommodate future in-place CLR servicing releases.
     *
     * The major version is whichever .NET we compiled against.
     * The user may run with a newer point release; our `dbgshim` should work
     * anyway or at least fail safely, so we don't specify a build.
     */
    CLR_DEBUGGING_VERSION clr_ver_supported = { 0 };
    CLR_DEBUGGING_VERSION clr_ver_actual    = { 0 };

    clr_ver_actual   .wStructVersion = 0;
    clr_ver_supported.wStructVersion = 0;
    clr_ver_supported.wMajor         = 10;
    clr_ver_supported.wMinor         = 0;
    clr_ver_supported.wBuild         = 65535;
    clr_ver_supported.wRevision      = 65535;

    CLR_DEBUGGING_PROCESS_FLAGS clr_dbg_flags;

    S0_ICorDebugDataTarget          impl_CorDebugDataTarget           = S0_ICorDebugDataTarget(h_process);
    S0_ICLRDebuggingLibraryProvider impl_CLRDebuggingLibraryProvider  = S0_ICLRDebuggingLibraryProvider();

    hr = ptr_ICLRDebugging->OpenVirtualProcess(
        (ULONG64) g_ptr_coreclr,
        &impl_CorDebugDataTarget,
        &impl_CLRDebuggingLibraryProvider,
        &clr_ver_supported,
        IID_ICorDebugProcess,
        (IUnknown**) &ptr_ICorDebugProcess,
        &clr_ver_actual,
        &clr_dbg_flags
    );

    if (hr != S_OK) {
        fwprintf_s(stderr, L"[!] ICLRDebugging::OpenVirtualProcess failed with code 0x%X.\n", hr);
        return hr;
    }

    hr = ptr_ICorDebugProcess->GetThread(id_thread, &ptr_ICorDebugThread);
    if (hr != S_OK) {
        fwprintf_s(stderr, L"[!] ICorDebugProcess::GetThread failed with code 0x%X.\n", hr);
        return hr;
    }

    hr = ptr_ICorDebugThread->QueryInterface(IID_ICorDebugThread3, (LPVOID*) &ptr_ICorDebugThread3);
    if (hr != S_OK) {
        fwprintf_s(stderr, L"[!] ICorDebugThread::QI(ICorDebugThread3) failed with code 0x%X.\n", hr);
        return hr;
    }

    hr = ptr_ICorDebugThread3->CreateStackWalk(&ptr_ICorDebugStackWalk);
    if (hr != S_OK) {
        fwprintf_s(stderr, L"[!] ICorDebugThread3::CreateStackWalk failed with code 0x%X.\n", hr);
        return hr;
    }

    hr = ptr_ICorDebugStackWalk->Next();
    if (hr != S_OK)
        return hr;

    mdMethodDef method_token;

    while (true) {
        hr = ptr_ICorDebugStackWalk->GetFrame(&ptr_ICorDebugFrame);
        if (hr != S_OK) {
            fwprintf_s(stderr, L"[!] ICorDebugStackWalk::GetFrame failed with code 0x%X.\n", hr);
            break;
        }

        hr = ptr_ICorDebugFrame->GetFunction(&ptr_ICorDebugFunction);
        if (hr != S_OK) {
            // TODO: https://github.com/dotnet/runtime/blob/b1e5bd9585e4463137ba03a63856461084e8d182/src/coreclr/debug/di/shimstackwalk.cpp
            // to handle IL/native frames like P/Invoke stubs
            g_frames_managed.emplace_back(L"Unknown managed frame.\n");

            hr = ptr_ICorDebugStackWalk->Next();
            if (hr != S_OK)
                break;

            continue;
        }

        hr = ptr_ICorDebugFunction->GetModule(&ptr_ICorDebugModule);
        if (hr != S_OK) {
            fwprintf_s(stderr, L"[!] ICorDebugFunction::GetModule failed with code 0x%X.\n", hr);
            break;
        }

        wchar_t module_name[MAX_PATH] = { 0 };
        ULONG32 module_name_sz;

        hr = ptr_ICorDebugModule->GetName(
            MAX_PATH,
            &module_name_sz,
            module_name
        );

        wchar_t* module_file_name = wcsrchr(module_name, L'\\');
        if (module_file_name == nullptr || PathCchRemoveExtension(module_file_name, MAX_PATH) != S_OK) {
            fwprintf_s(stderr, L"[!] Failed to get file name from full module path.\n");
            break;
        }

        if (hr != S_OK) {
            fwprintf_s(stderr, L"[!] ICorDebugModule::GetName failed with code 0x%X.\n", hr);
            break;
        }

        hr = ptr_ICorDebugFunction->GetToken(&method_token);
        if (hr != S_OK) {
            fwprintf_s(stderr, L"[!] ICorDebugFunction::GetToken failed with code 0x%X.\n", hr);
            break;
        }

        hr = ptr_ICorDebugModule->GetMetaDataInterface(IID_IMetaDataImport, (IUnknown**) &ptr_IMetaDataImport);
        if (hr != S_OK) {
            fwprintf_s(stderr, L"[!] ICorDebugModule::GetMetaDataInterface failed with code 0x%X.\n", hr);
            break;
        }

        wchar_t       method_name[MAX_SYM_NAME] = { 0 };
        ULONG         method_name_sz;
        ULONG         method_name_sz_req = MAX_SYM_NAME;
        DWORD         method_flags;
        DWORD         method_flags_impl;
        COR_SIGNATURE method_signature[1024] = { 0 };
        ULONG         method_signature_sz;
        ULONG         method_rva;
        mdTypeDef     type_token;

        hr = ptr_IMetaDataImport->GetMethodProps(
            method_token,
            &type_token,
            method_name,
            method_name_sz_req,
            &method_name_sz,
            &method_flags,
            (PCCOR_SIGNATURE*) &method_signature,
            &method_signature_sz,
            &method_rva,
            &method_flags_impl
        );

        if (hr != S_OK) {
            fwprintf_s(stderr, L"[!] IMetaDataImport::GetMethodProps failed with code 0x%X.\n", hr);
            break;
        }

        wchar_t type_name[MAX_SYM_NAME] = { 0 };
        ULONG   type_name_sz;
        ULONG   type_name_sz_req = MAX_SYM_NAME;
        DWORD   type_flags;
        mdToken extends_token;

        hr = ptr_IMetaDataImport->GetTypeDefProps(
            type_token,
            type_name,
            type_name_sz_req,
            &type_name_sz,
            &type_flags,
            &extends_token
        );

        if (hr != S_OK) {
            fwprintf_s(stderr, L"[!] IMetaDataImport::GetTypeDefProps failed with code 0x%X.\n", hr);
            break;
        }

        wchar_t sym_managed[MAX_SYM_NAME] = { 0 };

        swprintf_s(
            sym_managed,
            MAX_SYM_NAME,
            L"%s!%s.%s\n",
            module_file_name + 1,
            type_name,
            method_name
        );

        g_frames_managed.emplace_back(sym_managed);

        hr = ptr_ICorDebugStackWalk->Next();
        if (hr != S_OK)
            break;
    }

    return hr;
}

// Processes a stack frame, returning its type and preparing its symbol, if native.
static S0_FRAME_TYPE s0_dbg_process_frame(
    HANDLE       h_process,  // A handle to the process the stack frame belongs to.
    STACKFRAME64 stack_frame // The stack frame to symbolicate.
) {
    DWORD64 frame_addr = stack_frame.AddrPC.Offset;

    IMAGEHLP_MODULEW64 module = { 0 };
    module.SizeOfStruct = sizeof(IMAGEHLP_MODULEW64);

    if (!SymGetModuleInfoW64(h_process, frame_addr, &module)) {
        /* [fkelava 21/09/26 16:08]
         * We use a very primitive heuristic here. We assume that if a given IP
         * can't be mapped to a module, it must be JITted code and therefore managed.
         *
         * We will therefore fill that frame out with data from the managed stack walk.
         */
        DWORD error = GetLastError();
        if (error != ERROR_MOD_NOT_FOUND) {
            fwprintf_s(stderr, L"SymGetModuleInfoW64() failed with code 0x%X.\n", error);
            return FRAME_UNKNOWN;
        }

        return FRAME_MANAGED;
    }

    bool checked_symbols = g_map_checked_symbol.contains(module.ImageName);

    if (!checked_symbols) {
        SYMSRV_INDEX_INFOW symsrv_info = { 0 };
        symsrv_info.sizeofstruct = sizeof(SYMSRV_INDEX_INFOW);

        if (!SymSrvGetFileIndexInfoW(module.LoadedImageName, &symsrv_info, 0)) {
            fwprintf_s(stderr, L"SymSrvGetFileIndexInfoW() failed with code 0x%X.\n", GetLastError());
            return FRAME_UNKNOWN;
        }

        wchar_t pdb_path[MAX_PATH + 1] = { 0 };

        /* [fkelava 16/09/26 18:53]
         * https://learn.microsoft.com/en-us/windows/win32/api/dbghelp/ns-dbghelp-symsrv_index_info
         * Older PDBs have a DWORD signature. Newer ones have a GUID. We must be prepared for either case.
         */
        GUID guid_0  = { 0 };
        bool use_sig = (memcmp(&symsrv_info.guid, &guid_0, sizeof(guid_0)) == 0);

        PVOID id    = use_sig
            ? (PVOID) &symsrv_info.sig
            : (PVOID) &symsrv_info.guid;
        DWORD flags = use_sig
            ? SSRVOPT_DWORDPTR
            : SSRVOPT_GUIDPTR;

        /* [fkelava 16/09/26 18:53]
         * https://learn.microsoft.com/en-us/windows/win32/api/dbghelp/nf-dbghelp-symfindfileinpathw#remarks
         * > If DbgHelp is looking for a `.pdb` file, the `id` parameter specifies the
         * > PDB signature as found in the codeview debug directory of the original image.
         * > Parameter two specifies the PDB age. Parameter three is unused and set to zero.
         *
         * This function will trigger download of symbols from the MS server if possible.
         * The symbols are stored in the 'cache' directory for reuse.
         */
        if (!SymFindFileInPathW(
            h_process,
            nullptr,
            symsrv_info.pdbfile,
            id,
            symsrv_info.age,
            0,
            flags,
            pdb_path,
            nullptr,
            nullptr
        )) {
            fwprintf_s(stderr, L"SymFindFileInPathW() failed with code 0x%X for module %s.\n", GetLastError(), module.ImageName);
        }

        g_map_checked_symbol.emplace(module.ImageName);
    }

    /* [fkelava 16/09/26 18:57]
     * This struct is not documented anywhere.
     * It is a SYMBOL_INFOW whose symbol name buffer is of size MAX_SYM_NAME, for ease of use.
     * https://learn.microsoft.com/en-us/windows/win32/api/dbghelp/ns-dbghelp-symbol_infow
     */
    SYMBOL_INFO_PACKAGEW sym = { 0 };
    sym.si.SizeOfStruct = sizeof(SYMBOL_INFOW);
    sym.si.MaxNameLen   = MAX_SYM_NAME;

    wchar_t sym_native[MAX_SYM_NAME] = { 0 };
    DWORD64 sym_displacement = 0;

    if (!SymFromAddrW(h_process, frame_addr, &sym_displacement, &sym.si)) {
        /* [fkelava 21/09/26 14:53]
         * We may not have the PDB or any other symbols for the target binary.
         * In this case SymFromAddrW seems to return ERROR_INVALID_ADDRESS.
         *
         * But, e.g., FFX+0x193912 is suitable and useful, so we display that instead.
         */
        DWORD sym_error = GetLastError();
        if (sym_error != ERROR_INVALID_ADDRESS) {
            fwprintf_s(stderr, L"[!] SymFromAddrW() failed with code 0x%X.\n", GetLastError());
            return FRAME_UNKNOWN;
        }

        swprintf_s(sym_native, MAX_SYM_NAME, L"%s+0x%llX\n", module.ModuleName, frame_addr - module.BaseOfImage);
    }
    else {
        swprintf_s(sym_native, MAX_SYM_NAME, L"%s!%s+0x%llX\n", module.ModuleName, sym.si.Name, sym_displacement);
    }

    g_frames_native.emplace_back(sym_native);
    return FRAME_NATIVE;
}

// Prints the register state at the time an exception was caught.
static void s0_dbg_print_context(
    CONTEXT* ptr_context // A pointer to the faulting thread's context.
) {
    fwprintf_s(stdout, L"---- EXCEPTION CONTEXT ----\n");

    fwprintf_s(
        stdout,
        L"eax=%08X ebx=%08X ecx=%08X edx=%08X\n",
        ptr_context->Eax,
        ptr_context->Ebx,
        ptr_context->Ecx,
        ptr_context->Edx
    );

    fwprintf_s(
        stdout,
        L"esi=%08X edi=%08X ebp=%08X eip=%08X esp=%08X\n",
        ptr_context->Esi,
        ptr_context->Edi,
        ptr_context->Ebp,
        ptr_context->Eip,
        ptr_context->Esp
    );

    fwprintf_s(
        stdout,
        L" cs=    %04hX  ds=    %04hX  es=    %04hX  fs=    %04hX  gs=    %04hX ss=    %04hX efl=%04hX\n",
        ptr_context->SegCs,
        ptr_context->SegDs,
        ptr_context->SegEs,
        ptr_context->SegFs,
        ptr_context->SegGs,
        ptr_context->SegSs,
        ptr_context->EFlags
    );

    fwprintf_s(stdout, L"\n");
}

// Prints the stack trace with any available data we have.
static void s0_dbg_print_stack_trace() {
    fwprintf_s(stdout, L"---- STACK TRACE ----\n");

    DWORD count_managed = 0;
    DWORD count_native  = 0;
    DWORD count_frames  = g_frames_type.size();

    for (DWORD i = 0; i < count_frames; i++) {
        std::wstring frame;

        switch (g_frames_type[i]) {
            case FRAME_UNKNOWN:
                frame = L"Unknown frame.\n";
                break;

            case FRAME_MANAGED:
                /* [fkelava 21/09/26 16:30]
                 * We might not have data for all managed frames.
                 * Currently, we don't handle things like forward and reverse P/Invoke stubs.
                 */
                frame = count_managed >= g_frames_managed.size()
                    ? L"Unknown managed frame.\n"
                    : g_frames_managed[count_managed++];
                break;

            case FRAME_NATIVE:
                frame = count_native >= g_frames_native.size()
                    ? L"Unknown native frame.\n"
                    : g_frames_native[count_native++];
                break;
        }

        fwrite(frame.c_str(), sizeof(wchar_t), frame.size(), stdout);
    }

    fwprintf_s(stdout, L"\n");
}

// Walks the faulting thread's stack, displaying a stack trace.
// If available, symbols are automatically obtained and utilized.
static void s0_dbg_stack_walk_native(
    HANDLE   h_process,  // A handle to the process the fault occurred in.
    HANDLE   h_thread,   // A handle to the faulting thread.
    CONTEXT* ptr_context // A pointer to the faulting thread's context.
) {
    STACKFRAME64 stack_frame = { 0 };
    stack_frame.AddrPC   .Offset = ptr_context->Eip;
    stack_frame.AddrPC   .Mode   = AddrModeFlat;
    stack_frame.AddrFrame.Offset = ptr_context->Ebp;
    stack_frame.AddrFrame.Mode   = AddrModeFlat;
    stack_frame.AddrStack.Offset = ptr_context->Esp;
    stack_frame.AddrStack.Mode   = AddrModeFlat;

    while (true) {
        BOOL rv = StackWalk64(
            IMAGE_FILE_MACHINE_I386,
            h_process,
            h_thread,
            &stack_frame,
            ptr_context,
            NULL,
            SymFunctionTableAccess64,
            SymGetModuleBase64,
            NULL
        );

        if (!rv)
            break;

        if (stack_frame.AddrPC.Offset == 0)
            break;

        g_frames_type.emplace_back(
            s0_dbg_process_frame(h_process, stack_frame)
        );
    }

    s0_dbg_print_stack_trace();
}

// Writes a core dump to disk.
static void s0_dbg_create_dump(
    HANDLE            h_process,           // The handle to the process being dumped.
    DWORD             id_process,          // The ID of the process being dumped.
    DWORD             id_thread,           // The ID of the faulting thread in the process being dumped.
    CONTEXT*          ptr_context,         // A pointer to the context of the faulting thread.
    EXCEPTION_RECORD* ptr_exception_record // A pointer to the record of the exception bringing the process down.
) {
    wchar_t crash_dump_name[128     ] = { 0 };
    wchar_t crash_dump_path[MAX_PATH] = { 0 };

    SYSTEMTIME time = { 0 };
    GetSystemTime(&time);

    swprintf_s(
        crash_dump_name,
        L"\\%02hu%02hu%02hu_%02hu%02hu%02hu.dmp",
        time.wDay,
        time.wMonth,
        time.wYear,
        time.wHour,
        time.wMinute,
        time.wSecond
    );

    if (FAILED(StringCchCatW(crash_dump_path, MAX_PATH, g_path_dir_crash)) ||
        FAILED(StringCchCatW(crash_dump_path, MAX_PATH, crash_dump_name))
    ) {
        fwprintf_s(stderr, L"[!] StringCchCatW() failed.\n");
        return;
    }

    HANDLE dump_handle = CreateFileW(
        crash_dump_path,
        GENERIC_READ | GENERIC_WRITE,
        0,
        nullptr,
        CREATE_ALWAYS,
        FILE_ATTRIBUTE_NORMAL,
        nullptr);

    if (dump_handle == nullptr || dump_handle == INVALID_HANDLE_VALUE) {
        fwprintf_s(stderr, L"Failed to open a file to write the core dump to.\n");
        return;
    }

    MINIDUMP_TYPE dump_type = (MINIDUMP_TYPE)(
        MiniDumpNormal
      | MiniDumpWithHandleData
      | MiniDumpWithFullMemoryInfo
      | MiniDumpWithThreadInfo
      | MiniDumpWithProcessThreadData
      | MiniDumpWithUnloadedModules);

    /* [fkelava 11/06/26 21:24]
     * MiniDumpWriteDump expects, in MINIDUMP_EXCEPTION_INFORMATION, a PEXCEPTION_POINTERS
     * (a CONTEXT and EXCEPTION_RECORD). EXCEPTION_DEBUG_INFO only gets the latter.
     *
     * GetThreadContext solves that, but there's a catch. MINIDUMP_EXCEPTION_INFORMATION has a ClientPointers field that:
     * > Determines where to get the memory regions pointed to by the ExceptionPointers member.
     * > Set to TRUE if the memory resides in the process being debugged {...} Otherwise, set to FALSE {...}
     *
     * You'd think TRUE is correct. Not so: the dump then has 'no exception context stored'.
     * Because the context is created _here_, FALSE solves that problem. But that, _too_, cannot be correct;
     * the context resides in the debugger, but the pointers in the exception record certainly do not.
     *
     * What then? The docs do not say. We use FALSE as the lesser evil. We are not alone in this: see
     * https://github.com/jrfonseca/drmingw/blob/6824862b34b288524ed6e92806479bb3ec6fab07/src/common/debugger.cpp#L577.
     *
     * See also:
     * - https://learn.microsoft.com/en-us/windows/win32/api/minwinbase/ns-minwinbase-exception_debug_info
     * - https://learn.microsoft.com/en-us/windows/win32/api/winnt/ns-winnt-exception_pointers
     * - https://learn.microsoft.com/en-us/windows/win32/api/minidumpapiset/ns-minidumpapiset-minidump_exception_information
     */
    EXCEPTION_POINTERS exception_pointers = { 0 };
    exception_pointers.ContextRecord   = ptr_context;
    exception_pointers.ExceptionRecord = ptr_exception_record;

    MINIDUMP_EXCEPTION_INFORMATION info_dump_exception = { 0 };
    info_dump_exception.ThreadId          = id_thread;
    info_dump_exception.ExceptionPointers = &exception_pointers;
    info_dump_exception.ClientPointers    = FALSE;

    fwprintf_s(stderr, L"Dumping process core. Please wait.\n");

    if (!MiniDumpWriteDump(
        h_process,
        id_process,
        dump_handle,
        dump_type,
        &info_dump_exception,
        nullptr,
        nullptr
    )) {
        fwprintf_s(stderr, L"Failed to dump core.\n");
    }
    else {
        fwprintf_s(stdout, L"Core dumped to %s.\n", crash_dump_path);
    }

    fwprintf_s(stdout, L"\n");
    CloseHandle(dump_handle);
}

// Handles exception events, returning whether to continue or treat the exception as unhandled.
static DWORD s0_dbg_exception(
    HANDLE                h_process,         // The handle to the process that encountered an exception.
    DWORD                 id_process,        // The ID of the process that encountered an exception.
    DWORD                 id_thread,         // The ID of the faulting thread in the process that encountered an exception.
    EXCEPTION_DEBUG_INFO* ptr_info_exception // A pointer to information about the exception.
) {
    /* [fkelava 12/09/26 23:50]
     * https://learn.microsoft.com/en-us/windows/win32/api/minwinbase/ns-minwinbase-exception_debug_info#members
     * > If this member is zero, the debugger has previously encountered the exception.
     *
     * We only "handle" exceptions (i.e. dump core) in the first instance.
     * Note that we intentionally return DBG_EXCEPTION_NOT_HANDLED so WER, .NET EH et al. function unimpeded.
     */
    if (ptr_info_exception->dwFirstChance == 0)
        return DBG_EXCEPTION_NOT_HANDLED;

    if ((ptr_info_exception->ExceptionRecord.ExceptionFlags & EXCEPTION_NONCONTINUABLE) == EXCEPTION_NONCONTINUABLE) {
        CONTEXT faulting_thread_context = { 0 };
        faulting_thread_context.ContextFlags = CONTEXT_ALL;

        HANDLE faulting_thread_handle = OpenThread(THREAD_GET_CONTEXT, FALSE, id_thread);

        if (faulting_thread_handle == nullptr || faulting_thread_handle == INVALID_HANDLE_VALUE) {
            fwprintf_s(stderr, L"[!] OpenThread failed for thread 0x%X with code 0x%X.\n", id_thread, GetLastError());
            return DBG_EXCEPTION_NOT_HANDLED;
        }

        if (!GetThreadContext(faulting_thread_handle, &faulting_thread_context)) {
            fwprintf_s(stderr, L"[!] GetThreadContext failed for thread 0x%X with code 0x%X.\n", id_thread, GetLastError());
            return DBG_EXCEPTION_NOT_HANDLED;
        }

        s0_dbg_print_context(&faulting_thread_context);

        /* [fkelava 13/09/26 13:49]
         * https://learn.microsoft.com/en-us/windows/win32/api/dbghelp/nf-dbghelp-stackwalk64
         * > This context may be modified, so do not pass a context record that should not be modified.
         *
         * The stack walk will modify the context, so we must do that last.
         */
        s0_dbg_create_dump(
            h_process,
            id_process,
            id_thread,
            &faulting_thread_context,
            &ptr_info_exception->ExceptionRecord
        );

        g_frames_native .clear();
        g_frames_managed.clear();
        g_frames_type   .clear();

        s0_dbg_stack_walk_managed(
            h_process,
            id_thread
        );

        s0_dbg_stack_walk_native(
            h_process,
            faulting_thread_handle,
            &faulting_thread_context
        );

        return DBG_EXCEPTION_NOT_HANDLED;
    }

    return DBG_CONTINUE;
}

/* [fkelava 16/09/26 18:44]
 * Original: https://github.com/jrfonseca/drmingw/blob/6824862b34b288524ed6e92806479bb3ec6fab07/src/common/debugger.cpp#L251-L268
 */

// Determines the size of a loaded/mapped-in module.
static BOOL s0_dbg_get_module_size(
    HANDLE h_process,       //       A handle to the process the module is being loaded into.
    LPVOID ptr_module_base, //       The base address of the target module.
    DWORD& size             // [out] The size of the module, if the call succeeds.
) {
    size = 0;

    while (true) {
        LPCVOID ptr_current = (PBYTE)ptr_module_base + size;

        MEMORY_BASIC_INFORMATION mem_info;
        if (VirtualQueryEx(h_process, ptr_current, &mem_info, sizeof(mem_info)) == 0) {
            fwprintf_s(stderr, L"[!] VirtualQueryEx() failed with code 0x%X.\n", GetLastError());
            return FALSE;
        }

        if (mem_info.AllocationBase != ptr_module_base)
            break;

        size += mem_info.RegionSize;
    }

    return TRUE;
}

// Loads a module's symbols.
static BOOL s0_dbg_process_module(
    HANDLE h_process,       //       The handle of the process the module is being loaded into.
    HANDLE h_module_file,   //       The handle to the file of the module being loaded.
    LPVOID ptr_module_base, //       A pointer to the base address of the module itself.
    DWORD& error_code       // [out] The error code to terminate the process with on failure.
) {
    if (h_module_file == nullptr || h_module_file == INVALID_HANDLE_VALUE) {
        fwprintf_s(stderr, L"[!] LOAD_DLL_DEBUG_EVENT: Invalid DLL handle.\n");
        return FALSE;
    }

    /* [fkelava 13/09/26 16:33]
     * `drmingw` has a fallback path in case this API doesn't work,
     * such as people running on RAM disks. We do not support this for our own sanity.
     *
     * See generally https://learn.microsoft.com/en-us/windows/win32/memory/obtaining-a-file-name-from-a-file-handle,
     * https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-getfinalpathnamebyhandlew#remarks.
     */
    wchar_t module_path[MAX_PATH] = { 0 };

    DWORD sz_module_path = GetFinalPathNameByHandleW(
        h_module_file,
        module_path,
        sizeof(module_path) / sizeof(wchar_t),
        FILE_NAME_OPENED
    );

    if (sz_module_path == 0) {
        fwprintf_s(stderr, L"[!] GetFinalPathNameByHandleW() failed with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    if (sz_module_path > MAX_PATH) {
        fwprintf_s(stderr, L"[!] GetFinalPathNameByHandleW() - path length exceeded MAX_PATH.\n");
        return FALSE;
    }

    /* [fkelava 19/09/26 02:26]
     * A bit of a hack. To engage CLR debugging later, we need to track
     * a few .NET DLLs, starting from `coreclr.dll`.
     */
    if (wcsstr(module_path, L"coreclr.dll") != nullptr) {
        if (!s0_dbg_clr_init(ptr_module_base, module_path)) {
            fwprintf_s(stderr, L"Failed to prepare for CLR debugging.\n");
            return FALSE;
        }
    }

    /* [fkelava 13/09/26 16:43]
     * https://groups.google.com/forum/#!topic/comp.os.ms-windows.programmer.win32/ulkwYhM3020
     * > When deferred symbols are in use, the correct DLL size must be passed.
     */
    DWORD module_size;
    if (!s0_dbg_get_module_size(h_process, ptr_module_base, module_size))
        return FALSE;

    DWORD64 module_base_addr = SymLoadModuleExW(
        h_process,
        h_module_file,
        module_path,
        nullptr,
        (DWORD64) ptr_module_base,
        module_size,
        nullptr,
        0
    );

    DWORD error_symload = GetLastError();
    if (module_base_addr == 0 && error_symload != ERROR_SUCCESS) {
        fwprintf_s(stderr, L"[!] SymLoadModuleExW() failed with code 0x%X.\n", error_symload);
        return FALSE;
    }

    IMAGEHLP_MODULE64 module_info = { 0 };
    module_info.SizeOfStruct = sizeof(IMAGEHLP_MODULE64);

    /* [fkelava 13/09/26 14:19]
     * https://learn.microsoft.com/en-us/windows/win32/api/dbghelp/nf-dbghelp-symloadmoduleex#remarks
     * > If deferred symbol loading is enabled, the module is marked as deferred and the
     * > symbols are not loaded until a reference is made to a symbol in the module.
     * > Therefore, you should always call SymGetModuleInfo64 after calling SymLoadModuleEx.
     */
    if (!SymGetModuleInfo64(h_process, module_base_addr, &module_info)) {
        fwprintf_s(stderr, L"[!] SymGetModuleInfo64() failed with code 0x%X.\n", GetLastError());
        return FALSE;
    }

#if _DEBUG
    fwprintf_s(stdout, L"Module loaded: %s\n", module_path);
#endif
    /* [fkelava 13/09/26 02:03]
     * https://learn.microsoft.com/en-us/windows/win32/debug/debugging-events:
     * > The debugger should close the handle to the DLL while processing LOAD_DLL_DEBUG_EVENT.
     *
     * We deviate from the guidelines. Since we pass the handle to SymLoadModuleExW
     * and use deferred symbol loading, closing it would AV at stack-walking time.
     */
    return TRUE;
}

// The main loop of the debugger. Handles incoming debug events.
void s0_dbg_loop() {
    /* [fkelava 13/09/26 02:39]
     * See https://learn.microsoft.com/en-us/windows/win32/debug/debugging-events,
     * https://learn.microsoft.com/en-us/windows/win32/debug/writing-the-debugger-s-main-loop.
     *
     * The relevant passages are given in comments.
     */
    HANDLE h_process  = { 0 };
    DWORD  error_code = ERROR_SUCCESS;

    while (true) {
        DEBUG_EVENT event;
        DWORD       continue_state = DBG_EXCEPTION_NOT_HANDLED;

        WaitForDebugEventEx(&event, INFINITE);

        DWORD event_code = event.dwDebugEventCode;
        DWORD id_thread  = event.dwThreadId;
        DWORD id_process = event.dwProcessId;

        if (event_code == CREATE_PROCESS_DEBUG_EVENT) {
            h_process = event.u.CreateProcessInfo.hProcess;

            wchar_t sym_search_path[1024] = { 0 };
            swprintf_s(
                sym_search_path,
                L"cache*%s;SRV*https://msdl.microsoft.com/download/symbols",
                g_path_dir_cache
            );

            SymSetOptions(
                SYMOPT_UNDNAME                // Undecorate/demangle names where possible.
              | SYMOPT_DEFERRED_LOADS         // Only load symbols at point of use, i.e. the stack walk.
              | SYMOPT_FAIL_CRITICAL_ERRORS); // Fail silently, without prompting.

            if (!SymInitializeW(h_process, sym_search_path, FALSE)) {
                fwprintf_s(stderr, L"[!] SymInitializeW() failed\n");
                TerminateProcess(h_process, GetLastError());

                return;
            }

            if (!s0_dbg_process_module(
                h_process,
                event.u.CreateProcessInfo.hFile,
                event.u.CreateProcessInfo.lpBaseOfImage,
                error_code
            )) {
                TerminateProcess(h_process, error_code);
                return;
            }

            /* [fkelava 13/09/26 02:03]
             * > The handle to the process's image file has GENERIC_READ access and is opened for read-sharing.
             * > The debugger should close this handle while processing CREATE_PROCESS_DEBUG_EVENT.
             *
             * We deviate from the guidelines. Since we pass the handle to SymLoadModuleExW
             * and use deferred symbol loading, closing it would AV at stack-walking time.
             */
        }

        // To proceed past this point, we need CREATE_PROCESS_DEBUG_EVENT to arrive first.
        if (h_process == nullptr || h_process == INVALID_HANDLE_VALUE) {
            ContinueDebugEvent(id_process, id_thread, continue_state);
            continue;
        }

        /* [fkelava 13/09/26 16:18]
         * To say that there is a dearth of documentation about how to properly
         * handle LOAD_DLL_DEBUG_EVENT would be an understatement. The call that a debugger
         * _should_ make is SymLoadModuleEx{W}, but the debug event requires a lot of
         * wrangling to get the right parameters for that call.
         *
         * The relevant parts are simplified slightly from https://github.com/jrfonseca/drmingw.
         */
        if (event_code == LOAD_DLL_DEBUG_EVENT) {
            DWORD error_code;
            if (!s0_dbg_process_module(h_process, event.u.LoadDll.hFile, event.u.LoadDll.lpBaseOfDll, error_code)) {
                TerminateProcess(h_process, error_code);
                return;
            }
        }

        if (event_code == UNLOAD_DLL_DEBUG_EVENT) {
            SymUnloadModule64(h_process, (DWORD64) event.u.UnloadDll.lpBaseOfDll);
        }

        if (event_code == EXIT_PROCESS_DEBUG_EVENT) {
            SymCleanup(h_process);

            /* [fkelava 13/09/26 02:03]
             * > The kernel-mode portion of process shutdown cannot be completed
             * > until the debugger that receives this event calls ContinueDebugEvent.
             * >
             * > The system closes the debugger's handle to the exiting process
             * > and all of the process's threads. The debugger should not close these handles.
             */
            ContinueDebugEvent(id_process, id_thread, continue_state);
            return;
        }

        if (event_code == EXCEPTION_DEBUG_EVENT) {
            continue_state = s0_dbg_exception(h_process, id_process, id_thread, &event.u.Exception);
        }

        ContinueDebugEvent(id_process, id_thread, continue_state);
    }
}
