// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

/* [fkelava 10/10/26 17:25]
 * Not every game has a high quality error reporting system. When using Fahrenheit,
 * we want to provide a consistent baseline across all systems.
 *
 * Some systems, like Dalamud, have a dedicated crash handler. We do it differently-
 * Stage 0 is a tiny Win32 "debugger" that handles exception events, dumps core,
 * and surfaces error information to the end user.
 *
 * If a proper external debugger is connected, this functionality is disabled.
*/

#include <fhstage0.h>
#include <fhstage1.h>

// STL
#include <map>

// Win32 debugging
#include <dbghelp.h>

std::map<LPVOID, DLL_LOAD_DATA> g_modules;         // A map containing information about loaded modules.
std::map<LPVOID, bool>          g_checked_symbols; // Whether we performed symbol file lookup for a given module.

LPVOID g_addr_except; // The originating address of the last handled exception. Used to suppress dumping as it is rethrown during unwind.

// Processes a stack frame, returning its type and preparing its symbol, if native.
static S0_FRAME_DATA s0_dbg_w32_stack_frame(
    HANDLE       h_process,  // A handle to the process the stack frame belongs to.
    STACKFRAME64 stack_frame // The stack frame to symbolicate.
) {
    S0_FRAME_DATA frame      = { };
    DWORD64       frame_addr = stack_frame.AddrPC.Offset;

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
            return frame;
        }

        frame.frame_type = FRAME_MANAGED;
        return frame;
    }

    LPVOID ptr_module_base = (LPVOID) module.BaseOfImage;

    if (!g_checked_symbols.contains(ptr_module_base)) {
        g_checked_symbols[ptr_module_base] = true;

        SYMSRV_INDEX_INFOW symsrv_info = { 0 };
        symsrv_info.sizeofstruct = sizeof(SYMSRV_INDEX_INFOW);

        if (!SymSrvGetFileIndexInfoW(module.LoadedImageName, &symsrv_info, 0)) {
            fwprintf_s(stderr, L"SymSrvGetFileIndexInfoW() failed with code 0x%X.\n", GetLastError());
            return frame;
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
    }

    /* [fkelava 16/09/26 18:57]
     * This struct is not documented anywhere.
     * It is a SYMBOL_INFOW whose symbol name buffer is of size MAX_SYM_NAME, for ease of use.
     * https://learn.microsoft.com/en-us/windows/win32/api/dbghelp/ns-dbghelp-symbol_infow
     */
    SYMBOL_INFO_PACKAGEW sym = { 0 };
    sym.si.SizeOfStruct = sizeof(SYMBOL_INFOW);
    sym.si.MaxNameLen   = MAX_SYM_NAME;

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
            return frame;
        }

        swprintf_s(frame.frame_name, MAX_SYM_NAME, L"%s+0x%llX\n", module.ModuleName, frame_addr - module.BaseOfImage);
    }
    else {
        swprintf_s(frame.frame_name, MAX_SYM_NAME, L"%s!%s+0x%llX\n", module.ModuleName, sym.si.Name, sym_displacement);
    }

    return frame;
}

// Prints the register state at the time an exception was caught.
static void s0_dbg_w32_print_context(
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
static void s0_dbg_w32_print_stack_trace() {
    fwprintf_s(stdout, L"---- STACK TRACE ----\n");

    for (const S0_FRAME_DATA& frame : g_frames) {
        const wchar_t* frame_name = frame.frame_name;

        fwrite(frame_name, sizeof(wchar_t), lstrlenW(frame_name), stdout);
    }

    fwprintf_s(stdout, L"\n");
}

// Walks the faulting thread's stack, displaying a stack trace.
// If available, symbols are automatically obtained and utilized.
static void s0_dbg_w32_stack_walk(
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
        if (!StackWalk64(
            IMAGE_FILE_MACHINE_I386,
            h_process,
            h_thread,
            &stack_frame,
            ptr_context,
            NULL,
            SymFunctionTableAccess64,
            SymGetModuleBase64,
            NULL
        )) break;

        if (stack_frame.AddrPC.Offset == 0)
            break;

        g_frames.emplace_back(
            s0_dbg_w32_stack_frame(h_process, stack_frame)
        );
    }
}

// Writes a core dump to disk.
static void s0_dbg_w32_create_dump(
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

// Returns whether to treat exception as fatal or not.
static BOOL s0_dbg_w32_exception_filter(
    EXCEPTION_RECORD* ptr_exception_record // The record of the thrown exception.
) {
    /* [fkelava 04/10/26 02:28]
     * We only checked exception flags for EXCEPTION_NONCONTINUABLE until Square Enix began
     * making AV with all flags unset. We must honor that flag for .NET to avoid dumping on
     * 1st chance exceptions that will be handled by `try` block, but ignore it otherwise.
     *
     * See `ntstatus.h` from the Windows SDK. The high two bits of an exception code
     * represent the severity of an exception, and they're both set if it is an error.
     * Thus we capture any exception code above C000_0000.
     */
    DWORD code  = ptr_exception_record->ExceptionCode;
    DWORD flags = ptr_exception_record->ExceptionFlags;

    return (code == 0xE0434352 && (flags & EXCEPTION_NONCONTINUABLE) != 0)
        || (code != 0xE0434352 && code > 0xC0000000);
}

// Handles exception events, returning whether to continue or treat the exception as unhandled.
static DWORD s0_dbg_w32_exception(
    HANDLE                h_process,         // The handle to the process that encountered an exception.
    DWORD                 id_process,        // The ID of the process that encountered an exception.
    DWORD                 id_thread,         // The ID of the faulting thread in the process that encountered an exception.
    EXCEPTION_DEBUG_INFO* ptr_info_exception // A pointer to information about the exception.
) {
    EXCEPTION_RECORD* ptr_exception_record = &ptr_info_exception->ExceptionRecord;

    /* [fkelava 12/09/26 23:50]
     * https://learn.microsoft.com/en-us/windows/win32/api/minwinbase/ns-minwinbase-exception_debug_info#members
     * > If this member is zero, the debugger has previously encountered the exception.
     *
     * We return NOT_HANDLED so other EH (WER, .NET) can proceed. We don't want to override user WER options.
     * Checking the address is required because the same exception may be rethrown as it unwinds.
     */
    if (ptr_info_exception->dwFirstChance == 0 || ptr_exception_record->ExceptionAddress == g_addr_except)
        return DBG_EXCEPTION_NOT_HANDLED;

    /* [fkelava 12/09/26 23:50]
     * https://learn.microsoft.com/en-us/windows-hardware/drivers/debugger/initial-breakpoint
     * > {...} an initial breakpoint automatically occurs after the main image and all
     * > statically-linked DLLs are loaded before any DLL initialization routines are called.
     *
     * The process will begin execution when we handle the BP. Now we spin up a thread that
     * prepares the debug pipe for Stage 1 to connect to. The function is a no-op on subsequent BPs.
     */
    if (ptr_exception_record->ExceptionCode == STATUS_BREAKPOINT) {
        if (!s0_dbg_bridge_init()) {
            fwprintf_s(stderr, L"[!] Failed to initialize debug bridge.\n");
            TerminateProcess(h_process, 1);
        }

        return DBG_EXCEPTION_HANDLED;
    }

    if (s0_dbg_w32_exception_filter(ptr_exception_record)) {
        g_addr_except = ptr_exception_record->ExceptionAddress;

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

        s0_dbg_w32_print_context(&faulting_thread_context);

        /* [fkelava 13/09/26 13:49]
         * https://learn.microsoft.com/en-us/windows/win32/api/dbghelp/nf-dbghelp-stackwalk64
         * > This context may be modified, so do not pass a context record that should not be modified.
         *
         * The stack walk will modify the context, so we must do that last.
         */
        s0_dbg_w32_create_dump(
            h_process,
            id_process,
            id_thread,
            &faulting_thread_context,
            &ptr_info_exception->ExceptionRecord
        );

        g_frames.clear();

        s0_dbg_w32_stack_walk(
            h_process,
            faulting_thread_handle,
            &faulting_thread_context
        );

        s0_dbg_cor_stack_walk(
            h_process,
            id_thread
        );

        s0_dbg_w32_print_stack_trace();

        return DBG_EXCEPTION_NOT_HANDLED;
    }

    return DBG_CONTINUE;
}

/* [fkelava 16/09/26 18:44]
 * Original: https://github.com/jrfonseca/drmingw/blob/6824862b34b288524ed6e92806479bb3ec6fab07/src/common/debugger.cpp#L251-L268
 */

// Determines the size of a loaded/mapped-in module.
static BOOL s0_dbg_w32_module_size(
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
static BOOL s0_dbg_w32_module_load(
    HANDLE h_process,      // The handle of the process the module is being loaded into.
    HANDLE h_module_file,  // The handle to the file of the module being loaded.
    LPVOID ptr_module_base // A pointer to the base address of the module itself.
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
     * To engage CLR debugging later, we need to track the loaded `coreclr.dll`.
     */
    if (wcsstr(module_path, L"coreclr.dll") != nullptr) {
        if (!s0_dbg_cor_init(ptr_module_base, module_path)) {
            fwprintf_s(stderr, L"Failed to prepare for CLR debugging.\n");
            return FALSE;
        }
    }

    /* [fkelava 13/09/26 16:43]
     * https://groups.google.com/forum/#!topic/comp.os.ms-windows.programmer.win32/ulkwYhM3020
     * > When deferred symbols are in use, the correct DLL size must be passed.
     */
    DWORD module_size;
    if (!s0_dbg_w32_module_size(h_process, ptr_module_base, module_size))
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

    DLL_LOAD_DATA load_data = { 0 };
    load_data.dll_base = ptr_module_base;
    load_data.dll_size = module_size;

    HRESULT hr = StringCchCopyW(
        load_data.dll_name,
        MAX_PATH,
        module_path
    );

    g_modules        [ptr_module_base] = load_data;
    g_checked_symbols[ptr_module_base] = false;

    if (hr != S_OK) {
        fwprintf_s(stderr, L"[!] StringCchCopyW() failed with code 0x%X.\n", hr);
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

// Unloads a module's symbols.
static BOOL s0_dbg_w32_module_unload(
    HANDLE h_process,      // The handle of the process the module is being unloaded from.
    LPVOID ptr_module_base // A pointer to the base address of the module itself.
) {
    if (!SymUnloadModule64(
        h_process,
        (DWORD64) ptr_module_base
    )) {
        fwprintf_s(stderr, L"[!] SymUnloadModule64() failed with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    if (g_checked_symbols.erase(ptr_module_base) != 1 ||
        g_modules        .erase(ptr_module_base) != 1) {
        fwprintf_s(stderr, L"[!] Failed to erase module records during unload.\n");
        return FALSE;
    }

    return TRUE;
}

// The main loop of the debugger. Handles incoming debug events.
void s0_dbg_w32_main() {
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
                fwprintf_s(stderr, L"[!] SymInitializeW() failed.\n");
                TerminateProcess(h_process, GetLastError());

                return;
            }

            if (!s0_dbg_w32_module_load(
                h_process,
                event.u.CreateProcessInfo.hFile,
                event.u.CreateProcessInfo.lpBaseOfImage
            )) {
                TerminateProcess(h_process, GetLastError());
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
            if (!s0_dbg_w32_module_load(h_process, event.u.LoadDll.hFile, event.u.LoadDll.lpBaseOfDll)) {
                TerminateProcess(h_process, GetLastError());
                return;
            }
        }

        if (event_code == UNLOAD_DLL_DEBUG_EVENT) {
            if (!s0_dbg_w32_module_unload(h_process, event.u.UnloadDll.lpBaseOfDll)) {
                TerminateProcess(h_process, GetLastError());
                return;
            }
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
            continue_state = s0_dbg_w32_exception(h_process, id_process, id_thread, &event.u.Exception);
        }

        ContinueDebugEvent(id_process, id_thread, continue_state);
    }
}
