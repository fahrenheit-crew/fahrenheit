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

wchar_t path_dir_cache[MAX_PATH] = { 0 }; // The full path to the 'cache' directory, used to store symbols.
wchar_t path_dir_crash[MAX_PATH] = { 0 }; // The full path to the 'crash' directory, used to store core dumps.

// Attempts to obtain and display a symbol for a given stack frame.
static void stage0_dbg_symbolicate(
    HANDLE       h_process,  // A handle to the process the stack frame belongs to.
    STACKFRAME64 stack_frame // The stack frame to symbolicate.
) {
    DWORD64 frame_addr = stack_frame.AddrPC.Offset;

    IMAGEHLP_MODULEW64 module = { 0 };
    module.SizeOfStruct = sizeof(IMAGEHLP_MODULEW64);

    if (!SymGetModuleInfoW64(
        h_process,
        frame_addr,
        &module
    )) {
        fwprintf_s(stderr, L"SymGetModuleInfoW64() failed with code 0x%X.\n", GetLastError());
        return;
    }

    SYMSRV_INDEX_INFOW symsrv_info = { 0 };
    symsrv_info.sizeofstruct = sizeof(SYMSRV_INDEX_INFOW);

    if (!SymSrvGetFileIndexInfoW(
        module.LoadedImageName,
        &symsrv_info,
        0
    )) {
        fwprintf_s(stderr, L"SymSrvGetFileIndexInfoW() failed with code 0x%X.\n", GetLastError());
        return;
    }

    wchar_t pdb_path [1024] = { 0 };
    wchar_t frame_str[1024] = { 0 };

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
        NULL,
        symsrv_info.pdbfile,
        id,
        symsrv_info.age,
        0,
        flags,
        pdb_path,
        NULL,
        NULL
    )) {
        fwprintf_s(stderr, L"SymFindFileInPathW() failed with code 0x%X.\n", GetLastError());
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
    if (!SymFromAddrW(
        h_process,
        frame_addr,
        &sym_displacement,
        &sym.si
    )) {
        fwprintf_s(stderr, L"SymFromAddrW() failed with code 0x%X.\n", GetLastError());
        return;
    }

    fwprintf_s(stdout, L"%s!%s+%llX\n", module.ModuleName, sym.si.Name, sym_displacement);
}

// Prints the register state at the time an exception was caught.
static void stage0_dbg_print_context(
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

// Walks the faulting thread's stack, displaying a stack trace.
// If available, symbols are automatically obtained and utilized.
static void stage0_dbg_stack_walk(
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

    fwprintf_s(stdout, L"---- STACK TRACE ----\n");

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

        stage0_dbg_symbolicate(h_process, stack_frame);
    }

    fwprintf_s(stdout, L"\n");
}

// Filters objects from a core dump being created.
static BOOL CALLBACK stage0_dbg_filter_dump(
          PVOID                     ptr_callback_param, // Always null. We pass no argument to this callback.
    const PMINIDUMP_CALLBACK_INPUT  ptr_callback_input, // A pointer to a structure containing supplementary information for the callback.
          PMINIDUMP_CALLBACK_OUTPUT ptr_callback_output // A pointer to a structure containing extended return information from the callback.
) {
    if (!ptr_callback_input || !ptr_callback_output) return FALSE;

    switch (ptr_callback_input->CallbackType) {
        case CancelCallback:
            return FALSE;
    }

    return TRUE;
}

// Writes a core dump to disk.
static void stage0_dbg_create_dump(
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

    if (FAILED(StringCchCatW(crash_dump_path, MAX_PATH, path_dir_crash)) ||
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

    if (dump_handle == NULL || dump_handle == INVALID_HANDLE_VALUE) {
        fwprintf_s(stderr, L"Failed to open a file to write the core dump to.\n");
        return;
    }

    MINIDUMP_TYPE dump_type = (MINIDUMP_TYPE)(
        MiniDumpNormal
      | MiniDumpWithDataSegs
      | MiniDumpWithHandleData
      | MiniDumpWithFullMemoryInfo
      | MiniDumpWithThreadInfo
      | MiniDumpWithProcessThreadData
      | MiniDumpWithUnloadedModules);

    /* [fkelava 11/06/26 21:24]
     * MiniDumpWriteDump expects, in MINIDUMP_EXCEPTION_INFORMATION, a PEXCEPTION_POINTERS
     * (a CONTEXT and EXCEPTION_RECORD). But a debugger, in EXCEPTION_DEBUG_INFO, only gets the latter.
     *
     * GetThreadContext solves that, but there's a catch. MINIDUMP_EXCEPTION_INFORMATION has a ClientPointers field:
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

    MINIDUMP_CALLBACK_INFORMATION info_dump_callback = { 0 };
    info_dump_callback.CallbackRoutine = (MINIDUMP_CALLBACK_ROUTINE)stage0_dbg_filter_dump;
    info_dump_callback.CallbackParam   = nullptr;

    fwprintf_s(stderr, L"Dumping process core. Please wait.\n");

    if (!MiniDumpWriteDump(
        h_process,
        id_process,
        dump_handle,
        dump_type,
        &info_dump_exception,
        nullptr,
        &info_dump_callback
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
static DWORD stage0_dbg_exception(
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

        HANDLE faulting_thread_handle = OpenThread(
            THREAD_GET_CONTEXT,
            FALSE,
            id_thread
        );

        if (faulting_thread_handle == nullptr || faulting_thread_handle == INVALID_HANDLE_VALUE) {
            fwprintf_s(stderr, L"Failed to open the faulting thread for context capture.\n");
            return DBG_EXCEPTION_NOT_HANDLED;
        }

        if (!GetThreadContext(faulting_thread_handle, &faulting_thread_context)) {
            fwprintf_s(stderr, L"Failed to capture the faulting thread's context.\n");
            return DBG_EXCEPTION_NOT_HANDLED;
        }

        stage0_dbg_print_context(
            &faulting_thread_context
        );

        /* [fkelava 13/09/26 13:49]
         * https://learn.microsoft.com/en-us/windows/win32/api/dbghelp/nf-dbghelp-stackwalk64
         * > This context may be modified, so do not pass a context record that should not be modified.
         *
         * The stack walk will modify the context, so we must do that last.
         */
        stage0_dbg_create_dump(
            h_process,
            id_process,
            id_thread,
            &faulting_thread_context,
            &ptr_info_exception->ExceptionRecord
        );

        stage0_dbg_stack_walk(
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
static BOOL stage0_dbg_get_module_size(
    HANDLE h_process,       //       A handle to the process the module is being loaded into.
    LPVOID ptr_module_base, //       The base address of the target module.
    DWORD& size             // [out] The size of the module, if the call succeeds.
) {
    size = 0;

    while (true) {
        LPCVOID ptr_current = (PBYTE)ptr_module_base + size;

        MEMORY_BASIC_INFORMATION mem_info;
        if (VirtualQueryEx(h_process, ptr_current, &mem_info, sizeof(mem_info)) == 0) {
            fwprintf_s(stderr, L"[!] VirtualQueryEx() failed.\n");
            return FALSE;
        }

        if (mem_info.AllocationBase != ptr_module_base)
            break;

        size += mem_info.RegionSize;
    }

    return TRUE;
}

// Loads a module's symbols.
static BOOL stage0_dbg_process_module(
    HANDLE h_process,       //       The handle of the process the module is being loaded into.
    HANDLE h_module_file,   //       The handle to the file of the module being loaded.
    LPVOID ptr_module_base, //       A pointer to the base address of the module itself.
    DWORD& error_code       // [out] The error code to terminate the process with on failure.
) {
    if (h_module_file == nullptr || h_module_file == INVALID_HANDLE_VALUE) {
        fwprintf_s(stderr, L"Invalid DLL handle in LOAD_DLL_DEBUG_EVENT.\n");
        error_code = ERROR_INVALID_HANDLE;

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
        fwprintf_s(stderr, L"[!] GetFinalPathNameByHandleW() failed.\n");
        error_code = GetLastError();

        return FALSE;
    }

    if (sz_module_path > MAX_PATH) {
        fwprintf_s(stderr, L"[!] GetFinalPathNameByHandleW() - path length exceeded MAX_PATH.\n");
        error_code = ERROR_BUFFER_OVERFLOW;

        return FALSE;
    }

    /* [fkelava 13/09/26 16:43]
     * https://groups.google.com/forum/#!topic/comp.os.ms-windows.programmer.win32/ulkwYhM3020
     * > When deferred symbols are in use, the correct DLL size must be passed.
     */

    DWORD module_size;
    if (!stage0_dbg_get_module_size(
        h_process,
        ptr_module_base,
        module_size
    )) {
        fwprintf_s(stderr, L"Failed to get the size of module being loaded.\n");
        error_code = GetLastError();

        return FALSE;
    }

    DWORD64 module_base_addr = SymLoadModuleExW(
        h_process,
        h_module_file,
        module_path,
        NULL,
        (DWORD64) ptr_module_base,
        module_size,
        NULL,
        0
    );

    DWORD error_symload = GetLastError();
    if (module_base_addr == 0 && error_symload != ERROR_SUCCESS) {
        fwprintf_s(stderr, L"[!] SymLoadModuleExW() failed.\n");
        error_code = error_symload;

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

    if (!SymGetModuleInfo64(
        h_process,
        module_base_addr,
        &module_info
    )) {
        fwprintf_s(stderr, L"[!] SymGetModuleInfo64() failed.\n");
        error_code = GetLastError();

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

// Prepares the directories the debugger requires to operate.
static BOOL stage0_dbg_init() {
    wchar_t path_dir_base[MAX_PATH] = { 0 };

    DWORD path_base_size = GetModuleFileNameW(
        NULL,
        path_dir_base,
        sizeof(path_dir_base) / sizeof(wchar_t)
    );

    if (path_base_size == 0) {
        fwprintf_s(stderr, L"[!] GetModuleFileNameW() failed with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    /* [fkelava 15/09/26 14:54]
     * We have to remove the last path element twice to get from /bin/fhstage0.exe to the base directory.
     */
    if (PathCchRemoveFileSpec(path_dir_base, MAX_PATH) != S_OK ||
        PathCchRemoveFileSpec(path_dir_base, MAX_PATH) != S_OK
    ) {
        fwprintf_s(stderr, L"[!] PathCchRemoveFileSpec() failed for path %s.\n", path_dir_base);
        return FALSE;
    }

    if (FAILED(StringCchCatW(path_dir_cache, MAX_PATH, path_dir_base)) ||
        FAILED(StringCchCatW(path_dir_cache, MAX_PATH, L"\\cache"))    ||
        FAILED(StringCchCatW(path_dir_crash, MAX_PATH, path_dir_base)) ||
        FAILED(StringCchCatW(path_dir_crash, MAX_PATH, L"\\crash"))
    ) {
        fwprintf_s(stderr, L"[!] StringCchCatW() failed.\n");
        return FALSE;
    }

    if ((!CreateDirectoryW(path_dir_cache, NULL) && GetLastError() != ERROR_ALREADY_EXISTS) ||
        (!CreateDirectoryW(path_dir_crash, NULL) && GetLastError() != ERROR_ALREADY_EXISTS)
    ) {
        fwprintf_s(stderr, L"[!] CreateDirectoryW() failed with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    return TRUE;
}

// The main loop of the debugger. Handles incoming debug events.
void stage0_dbg_loop() {
    BOOL init_failed = FALSE;
    if (!stage0_dbg_init()) {
        fwprintf_s(stderr, L"Failed to create debugger directories. Aborting.\n");
        init_failed = TRUE;
    }

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

            if (init_failed) {
                TerminateProcess(h_process, 1);
                return;
            }

            wchar_t sym_search_path[1024] = { 0 };
            swprintf_s(
                sym_search_path,
                L"cache*%s;SRV*https://msdl.microsoft.com/download/symbols",
                path_dir_cache
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

            if (!stage0_dbg_process_module(
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
            if (!stage0_dbg_process_module(
                h_process,
                event.u.LoadDll.hFile,
                event.u.LoadDll.lpBaseOfDll,
                error_code
            )) {
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
            continue_state = stage0_dbg_exception(h_process, id_process, id_thread, &event.u.Exception);
        }

        ContinueDebugEvent(id_process, id_thread, continue_state);
    }
}
