// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

/* [fkelava 12/09/26 23:26]
 * As Fahrenheit is a .NET modding system for native binaries, it follows that debugging
 * and stack walking must be carried out in "mixed" mode; errors must include both
 * managed and native frames for proper debugging.
 *
 * This file contains a very rudimentary implementation of a managed stack walk.
 */

#include <fhstage0.h>
#include <fhstage1.h>

// .NET debugging
#include <cor.h>
#include <cordebug.h>
#include <dbgshim.h>

wchar_t g_path_coreclr     [MAX_PATH] = { 0 };   // The full path to the loaded CoreCLR.
wchar_t g_path_mscordbi    [MAX_PATH] = { 0 };   // The full path to the `mscordbi` module for the given CoreCLR.
wchar_t g_path_mscordacwks [MAX_PATH] = { 0 };   // The full path to the `mscordacwks` module for the given CoreCLR.
wchar_t g_path_mscordaccore[MAX_PATH] = { 0 };   // The full path to the `mscordaccore` module for the given CoreCLR.
LPVOID  g_ptr_coreclr                 = nullptr; // The pointer to `coreclr.dll` in memory.

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
BOOL s0_dbg_cor_init(
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
HRESULT s0_dbg_cor_stack_walk(
    HANDLE h_process, // A handle to the process the fault occurred in.
    DWORD  id_thread  // The ID of the faulting thread in the process that encountered an exception.
) {
    ICLRDebugging*     ptr_ICLRDebugging      = nullptr;
    ICorDebugProcess*  ptr_ICorDebugProcess   = nullptr;
    ICorDebugThread*   ptr_ICorDebugThread    = nullptr;
    ICorDebugThread2*  ptr_ICorDebugThread2   = nullptr;
    ICorDebugFunction* ptr_ICorDebugFunction  = nullptr;
    ICorDebugModule*   ptr_ICorDebugModule    = nullptr;
    IMetaDataImport*   ptr_IMetaDataImport    = nullptr;

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

    hr = ptr_ICorDebugThread->QueryInterface(IID_ICorDebugThread2, (LPVOID*) &ptr_ICorDebugThread2);
    if (hr != S_OK) {
        fwprintf_s(stderr, L"[!] ICorDebugThread::QI(ICorDebugThread2) failed with code 0x%X.\n", hr);
        return hr;
    }

    ULONG32 nb_managed_fns   = 0;
    ULONG32 nb_managed_fns_2 = 0;

    hr = ptr_ICorDebugThread2->GetActiveFunctions(0, &nb_managed_fns, nullptr);
    if (hr != S_OK) {
        fwprintf_s(stderr, L"[!] ICorDebugThread2::GetActiveFunctions failed with code 0x%X.\n", hr);
        return hr;
    }

    COR_ACTIVE_FUNCTION* managed_fns = (COR_ACTIVE_FUNCTION*) malloc(nb_managed_fns * sizeof(COR_ACTIVE_FUNCTION));

    if (managed_fns == nullptr) {
        fwprintf_s(stderr, L"[!] Failed to allocate memory for ICorDebugThread2::GetActiveFunctions.\n");
        return hr;
    }

    hr = ptr_ICorDebugThread2->GetActiveFunctions(nb_managed_fns, &nb_managed_fns_2, managed_fns);
    if (hr != S_OK) {
        fwprintf_s(stderr, L"[!] ICorDebugThread2::GetActiveFunctions failed with code 0x%X.\n", hr);
        return hr;
    }

    mdMethodDef method_token;
    ULONG32     managed_fn_index = 0;

    for (size_t frame_index = 0; frame_index < g_frames.size(); frame_index++) {
        S0_FRAME_DATA* ptr_frame = &g_frames[frame_index];

        if (ptr_frame->frame_type != FRAME_MANAGED)
            continue;

        if (managed_fn_index >= nb_managed_fns) {
            fwprintf_s(stderr, L"[!] Fewer active managed functions than managed frames; the stack trace may be incorrect. Aborting.\n");
            break;
        }

        hr = managed_fns[managed_fn_index++].pFunction->QueryInterface(IID_ICorDebugFunction, (LPVOID*) &ptr_ICorDebugFunction);
        if (hr != S_OK) {
            fwprintf_s(stderr, L"[!] ICorDebugFunction2::QI(ICorDebugFunction) failed with code 0x%X.\n", hr);
            return hr;
        }

        hr = ptr_ICorDebugFunction->GetModule(&ptr_ICorDebugModule);
        if (hr != S_OK) {
            fwprintf_s(stderr, L"[!] ICorDebugFunction::GetModule failed with code 0x%X.\n", hr);
            break;
        }

        wchar_t module_name[MAX_PATH] = { 0 };
        ULONG32 module_name_sz;

        hr = ptr_ICorDebugModule->GetName(MAX_PATH, &module_name_sz, module_name);
        if (hr != S_OK) {
            fwprintf_s(stderr, L"[!] ICorDebugModule::GetName failed with code 0x%X.\n", hr);
            break;
        }

        wchar_t* module_file_name = wcsrchr(module_name, L'\\');
        if (module_file_name == nullptr || PathCchRemoveExtension(module_file_name, MAX_PATH) != S_OK) {
            fwprintf_s(stderr, L"[!] Failed to get file name from full module path.\n");
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
            swprintf_s(
                ptr_frame->frame_name,
                sizeof(ptr_frame->frame_name) / sizeof(wchar_t),
                L"Unknown managed frame.\n"
            );

            continue;
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
            swprintf_s(
                ptr_frame->frame_name,
                sizeof(ptr_frame->frame_name) / sizeof(wchar_t),
                L"Unknown managed frame.\n"
            );

            continue;
        }

        swprintf_s(
            ptr_frame->frame_name,
            sizeof(ptr_frame->frame_name) / sizeof(wchar_t),
            L"%s!%s.%s\n",
            module_file_name + 1,
            type_name,
            method_name
        );
    }

    return hr;
}
