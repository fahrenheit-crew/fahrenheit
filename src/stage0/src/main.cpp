// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

/* [fkelava 12/09/26 23:26]
 * One of Fahrenheit's primary design tenets is that it should apply no permanent
 * modifications to the game binary or folder whatsoever. In keeping with that,
 * instead of modifying one of the DLLs the game imports as UnX and ffgriever EFL do,
 * it has an explicit launcher system- the Stage 0 and 1 loaders. If the game is not
 * launched using it, you get a pristine, unmodified original game.
 *
 * The method of choice applied here is reversible IAT patching using MS Detours.
 * Stage 0 creates the game process and rewrites the IAT to load Stage 1 first,
 * then serves as the standard output/error pipe for the game.
 * Stage 1 reverses that modification, then bootstraps .NET and Fahrenheit.
 *
 * Stage 0 also acts as a crash handler/debugger for the target binary. See `dbg.cpp`.
 */

#include "fhstage0.h"

void s0_dbg_loop(); // Forward declaration of debugger loop function.

wchar_t target     [MAX_PATH] = { 0 }; // The path to the target binary.
wchar_t args_target[1024]     = { 0 }; // The command-line arguments to pass to the target.
wchar_t args_self  [1024]     = { 0 }; // The command-line arguments to Stage 0.
wchar_t dir_target [MAX_PATH] = { 0 }; // The directory the target binary is in.
char    dir_self   [MAX_PATH] = { 0 }; // The directory `fhstage0` is in.

// Separates Stage0 args from those which will be passed through to the target.
static HRESULT s0_main_process_args(
    int      argc,  // The number of arguments passed to the executable.
    wchar_t* argv[] // The arguments passed to the executable.
) {
    /* [fkelava 17/09/26 15:29]
     * Stage 0 args are separated from ones to be passed through to the target with a '--'.
     *
     * We can't know whether the user passed a relative or absolute path to the target binary.
     * Methods from this point on expect an absolute path, so we normalize it here.
     */
    wchar_t target_rel_or_abs[MAX_PATH] = { 0 };

    HRESULT hr = StringCchCopyW(target_rel_or_abs, MAX_PATH, argv[1]);
    if (hr != S_OK) {
        fwprintf_s(stderr, L"[!] StringCchCopyW(%s) failed\n", argv[1]);
        return hr;
    }

    DWORD rc = GetFullPathNameW(target_rel_or_abs, MAX_PATH, target, nullptr);
    if (rc == 0) {
        fwprintf_s(stderr, L"[!] GetFullPathNameW() failed with code 0x%X.\n", GetLastError());
        return E_FAIL;
    }

    if (rc > MAX_PATH) {
        fwprintf_s(stderr, L"[!] GetFullPathNameW() failed - buffer was too small. (%u > %u)\n", rc, MAX_PATH);
        return E_FAIL;
    }

    wchar_t* dest = args_self;

    for (int i = 2; i < argc; i++) {
        if (wcscmp(argv[i], L"--") == 0) {
            dest = args_target;
            continue;
        }

        hr = StringCchCatW(dest, 1024, argv[i]);
        if (hr != S_OK) {
            fwprintf_s(stderr, L"[!] StringCchCatW(%s, %s) failed\n", dest, argv[i]);
            return hr;
        }

        hr = StringCchCatW(dest, 1024, L" ");
        if (hr != S_OK) {
            fwprintf_s(stderr, L"[!] StringCchCatW(%s, %s) failed\n", dest, L" ");
            return hr;
        }
    }

    return hr;
}

// Gets the directory of the target binary. This will be used as its working directory.
static HRESULT stage0_main_dir_target() {
    HRESULT hr = StringCchCopyW(dir_target, MAX_PATH, target);
    if (hr != S_OK) {
        fwprintf_s(stderr, L"[!] StringCchCopyW(%s, %s) failed.\n", dir_target, target);
        return hr;
    }

    hr = PathCchRemoveFileSpec(dir_target, MAX_PATH);
    if (hr != S_OK) {
        fwprintf_s(stderr, L"[!] PathCchRemoveFileSpec(%s) failed.\n", dir_target);
    }

    return hr;
}

/* [fkelava 17/09/26 16:30]
 * We normally always use the wide/Unicode versions of Win32 API, but as
 * DetourUpdateProcessWithDll only takes narrow strings (LP{C}STR), we
 * have no choice but to use ANSI versions in the following functions.
 */

// Gets the directory `fhstage0` was started in. This will be used to locate dependencies.
static HRESULT s0_main_dir_self() {
    size_t sz_self = sizeof(dir_self) / sizeof(char);

    DWORD rc = GetCurrentDirectoryA(sz_self, dir_self);

    if (rc == 0) {
        fwprintf_s(stderr, L"[!] GetCurrentDirectoryA() failed with code 0x%X.\n", GetLastError());
        return E_FAIL;
    }

    if (rc > sz_self) {
        fwprintf_s(stderr, L"[!] GetCurrentDirectoryA() failed - buffer was too small. (%u > %u)\n", rc, sz_self);
        return E_FAIL;
    }

    HRESULT hr = StringCchCatA(dir_self, sz_self, "\\");
    if (hr != S_OK) {
        fprintf_s(stderr, "[!] StringCchCatA(%s, %s) failed.\n", dir_self, "\\");
    }

    return hr;
}

// Given the name of a dependency DLL, obtains its full path.
static HRESULT s0_main_get_dependency_path(
    LPSTR  dep_path, // A pointer to a buffer for the full path string.
    LPCSTR dep_name  // The file name of the DLL to obtain the full path of.
) {
    HRESULT hr = StringCchCatA(dep_path, MAX_PATH, dir_self);
    if (hr != S_OK) {
        fprintf_s(stderr, "[!] StringCchCatA(%s, %s) failed.\n", dep_path, dir_self);
        return hr;
    }

    hr = StringCchCatA(dep_path, MAX_PATH, dep_name);
    if (hr != S_OK) {
        fprintf_s(stderr, "[!] StringCchCatA(%s, %s) failed.\n", dep_path, dep_name);
    }

    return hr;
}

// Prepares the directories Stage 0 requires to operate.
static BOOL s0_main_init() {
    wchar_t path_dir_base[MAX_PATH] = { 0 };

    DWORD path_base_size = GetModuleFileNameW(
        nullptr,
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

    if (FAILED(StringCchCatW(g_path_dir_cache, MAX_PATH, path_dir_base)) ||
        FAILED(StringCchCatW(g_path_dir_cache, MAX_PATH, L"\\cache"))    ||
        FAILED(StringCchCatW(g_path_dir_crash, MAX_PATH, path_dir_base)) ||
        FAILED(StringCchCatW(g_path_dir_crash, MAX_PATH, L"\\crash"))
    ) {
        fwprintf_s(stderr, L"[!] StringCchCatW() failed.\n");
        return FALSE;
    }

    if ((!CreateDirectoryW(g_path_dir_cache, nullptr) && GetLastError() != ERROR_ALREADY_EXISTS) ||
        (!CreateDirectoryW(g_path_dir_crash, nullptr) && GetLastError() != ERROR_ALREADY_EXISTS)
    ) {
        fwprintf_s(stderr, L"[!] CreateDirectoryW() failed with code 0x%X.\n", GetLastError());
        return FALSE;
    }

    /* [fkelava 21/09/26 13:24]
     * Instead of maintaining our own core dumping machinery,
     * we can ask .NET to handle it for us... in theory. I haven't been able to get it working yet.
     *
     * See generally https://github.com/dotnet/runtime/tree/35423f17d6ebe715711a907badda2a505633daf2/src/coreclr/debug/createdump.
     */

    //wchar_t dump_path[MAX_PATH] = { 0 };
    //if (FAILED(StringCchCopyW(dump_path, MAX_PATH, g_path_dir_crash)) ||
    //    FAILED(StringCchCatW (dump_path, MAX_PATH, L"\\%e.%p.%t.dmp"))
    //) {
    //    fwprintf_s(stderr, L"[!] Failed to prepare core dump path.\n");
    //    return FALSE;
    //}
    //
    //if (!SetEnvironmentVariableW(L"DOTNET_DbgEnableMiniDump",     L"1")      ||
    //    !SetEnvironmentVariableW(L"DOTNET_DbgMiniDumpType",       L"2")      ||
    //    !SetEnvironmentVariableW(L"DOTNET_DbgMiniDumpName",       dump_path) ||
    //    !SetEnvironmentVariableW(L"DOTNET_CreateDumpDiagnostics", L"1")
    //) {
    //    fwprintf_s(stderr, L"[!] SetEnvironmentVariableW() failed with code 0x%X.\n", GetLastError());
    //    return FALSE;
    //}

    return TRUE;
}

int __cdecl wmain(
    int      argc,
    wchar_t* argv[]
) {
    if (argc < 2) {
        fwprintf_s(stdout, L"Invalid call. You must specify an executable to launch.\n");
        fwprintf_s(stdout, L"\n");
        fwprintf_s(stdout, L"Usage:\n");
        fwprintf_s(stdout, L"    fhstage0.exe [target] [options] -- [target_options]\n");
        fwprintf_s(stdout, L"\n");
        fwprintf_s(stdout, L"Options:\n");
        fwprintf_s(stdout, L"    --debug | Allows for an external debugger to be attached to the target.\n");
        fwprintf_s(stdout, L"\n");

        return 1;
    }

    if (!s0_main_init()) {
        fwprintf_s(stderr, L"Stage 0 failed to initialize.\n");
        return 1;
    }

    HRESULT hr;
    hr = s0_main_process_args(argc, argv);
    if (hr != S_OK)
        return hr;

    hr = stage0_main_dir_target();
    if (hr != S_OK)
        return hr;

    hr = s0_main_dir_self();
    if (hr != S_OK)
        return hr;

    bool  external_debug = wcsstr(args_self, L"--debug") != nullptr;
    DWORD creation_flags = external_debug
        ? CREATE_SUSPENDED
        : DEBUG_ONLY_THIS_PROCESS; // A debugged process is implicitly suspended until debug events are handled/pumped.

    PROCESS_INFORMATION pi;
    STARTUPINFOW        si = { 0 };

    si.cb = sizeof(STARTUPINFOW);

    // Create target process in suspended or debugged state.
    if (!CreateProcessW(
        target,
        args_target,
        nullptr,
        nullptr,
        FALSE,
        creation_flags,
        nullptr,
        dir_target,
        &si,
        &pi
    )) {
        fwprintf_s(stderr, L"Failed to create target process.\n");
        return 1;
    }

    // Pause for external debugger attach if `--debug` arg is passed.
    if (external_debug) {
        fwprintf_s(stdout, L"You can now attach a debugger; press any key to attempt launch.\n");
        int i = _getch();
    }

    /* [fkelava 17/09/26 17:11]
     * We have to ensure that the target binary starts with the working directory set to
     * its containing directory, so it can use relative path addressing without breaking.
     *
     * However, this creates a problem for us; Stage 1 has dependencies (nethost and MinHook),
     * and they are stored alongside Stage 1- which is _not_ on the target binary's search path.
     *
     * So we have to make sure we've injected all of Stage 1's dependencies too. The order
     * isn't incidental either; they have to be available by the time Stage 1 has run, to avoid
     * a LoadLibrary call that would fail. So Stage 1 has to come last in the injection order.
     *
     * Note that if nethost and MinHook had any non-system dependencies (thankfully, they don't),
     * you'd have to make sure those are loaded and properly ordered too.
     */

    char path_stage1 [MAX_PATH] = { 0 };
    char path_nethost[MAX_PATH] = { 0 };
    char path_minhook[MAX_PATH] = { 0 };

    hr = s0_main_get_dependency_path(path_stage1, "fhstage1.dll");
    if (hr != S_OK)
        return hr;

    hr = s0_main_get_dependency_path(path_nethost, "nethost.dll");
    if (hr != S_OK)
        return hr;

    hr = s0_main_get_dependency_path(path_minhook, MINHOOK_DLL);
    if (hr != S_OK)
        return hr;

    LPCSTR deps[3] = {
        path_nethost,
        path_minhook,
        path_stage1
    };

    // Patch IAT of suspended process to inject Stage 1 and dependencies.
    if (!DetourUpdateProcessWithDll(pi.hProcess, deps, 3)) {
        fwprintf_s(stderr, L"Failed to inject Stage 1 into the target.\n");
        TerminateProcess(pi.hProcess, 1U);

        return 1;
    }

    fwprintf_s(stdout, L"Stage 0 Loader complete. Moving to Stage 1.\n");

    /* [fkelava 17/09/26 00:15]
     * Stage 0 acts as a crash handler and standard I/O pipe for the game.
     * It does so by acting as a stub Win32 debugger that handles exception events.
     *
     * If an external debugger is connected, that functionality must be disabled.
     */

    if (external_debug) {
        ResumeThread       (pi.hThread);
        WaitForSingleObject(pi.hProcess, INFINITE);
    }
    else {
        s0_dbg_loop();
    }

    DWORD exit_code;
    BOOL  result = GetExitCodeProcess(pi.hProcess, &exit_code);

    CloseHandle(pi.hProcess);
    CloseHandle(pi.hThread);

    if (exit_code != 0) {
        fwprintf_s(stdout, L"Process exited with code 0x%X.\n", exit_code);
        fwprintf_s(stdout, L"If reporting an issue, please include the core dump (*.dmp) from the location mentioned above.\n");
    }
    else {
        fwprintf_s(stdout, L"Process ended by user.\n");
    }

    return exit_code;
}
