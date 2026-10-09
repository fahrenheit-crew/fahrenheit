// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

/* [fkelava 12/09/26 23:26]
 * Fahrenheit's design is to leave no trace on the game binary or folder whatsoever.
 * To do so, instead of modifying one of the game's imports as UnX, ASI, ffgriever EFL etc.
 * do, it has a launcher system- the Stage 0 and 1 loaders.
 *
 * The method of choice is reversible IAT patching using MS Detours.
 * Stage 0 creates the game process and rewrites the IAT to load Stage 1 first,
 * then serves as a debugger, crash handler and standard I/O pipe for the target.
 * Stage 1 reverses that modification, then bootstraps .NET and Fahrenheit.
 */

#include <fhstage0.h>

// Win32
#include <conio.h>

// IAT patching
#include <detours/detours.h>

void s0_dbg_loop(); // Forward declaration of debugger loop function.

// Separates Stage0 args from those which will be passed through to the target.
static HRESULT s0_main_process_args(
    int      argc,  // The number of arguments passed to the executable.
    wchar_t* argv[] // The arguments passed to the executable.
) {
    /* [fkelava 17/09/26 15:29]
     * Stage 0 args are separated from target's args with a '--'.
     *
     * The user could have passed a relative or absolute path to the target binary.
     * Methods from this point on expect an absolute path, so we normalize it here.
     */
    wchar_t target_rel_or_abs[MAX_PATH] = { 0 };

    HRESULT hr = StringCchCopyW(target_rel_or_abs, MAX_PATH, argv[1]);
    if (hr != S_OK) {
        fwprintf_s(stderr, L"[!] StringCchCopyW(%s) failed\n", argv[1]);
        return hr;
    }

    DWORD rc = GetFullPathNameW(target_rel_or_abs, MAX_PATH, g_target, nullptr);
    if (rc == 0) {
        fwprintf_s(stderr, L"[!] GetFullPathNameW() failed with code 0x%X.\n", GetLastError());
        return E_FAIL;
    }

    if (rc > MAX_PATH) {
        fwprintf_s(stderr, L"[!] GetFullPathNameW() failed - buffer was too small. (%u > %u)\n", rc, MAX_PATH);
        return E_FAIL;
    }

    wchar_t* dest = g_args_self;

    for (int i = 2; i < argc; i++) {
        if (wcscmp(argv[i], L"--") == 0) {
            dest = g_args_target;

            if (FAILED(StringCchCatW(dest, 1024, L"\"" )) ||
                FAILED(StringCchCatW(dest, 1024, g_target)) ||
                FAILED(StringCchCatW(dest, 1024, L"\"" ))
            ) {
                fwprintf_s(stderr, L"[!] Failed to copy target process name.\n");
                return hr;
            }

            continue;
        }

        hr = StringCchCatW(dest, 1024, L" ");
        if (hr != S_OK) {
            fwprintf_s(stderr, L"[!] StringCchCatW(%s, %s) failed\n", dest, L" ");
            return hr;
        }

        hr = StringCchCatW(dest, 1024, argv[i]);
        if (hr != S_OK) {
            fwprintf_s(stderr, L"[!] StringCchCatW(%s, %s) failed\n", dest, argv[i]);
            return hr;
        }
    }

    return hr;
}

// Gets the directory of the target binary. This will be used as its working directory.
static HRESULT stage0_main_dir_target() {
    HRESULT hr = StringCchCopyW(g_dir_target, MAX_PATH, g_target);
    if (hr != S_OK) {
        fwprintf_s(stderr, L"[!] StringCchCopyW(%s, %s) failed.\n", g_dir_target, g_target);
        return hr;
    }

    hr = PathCchRemoveFileSpec(g_dir_target, MAX_PATH);
    if (hr != S_OK) {
        fwprintf_s(stderr, L"[!] PathCchRemoveFileSpec(%s) failed.\n", g_dir_target);
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
    size_t sz_self = sizeof(g_dir_self) / sizeof(char);

    DWORD rc = GetCurrentDirectoryA(sz_self, g_dir_self);

    if (rc == 0) {
        fwprintf_s(stderr, L"[!] GetCurrentDirectoryA() failed with code 0x%X.\n", GetLastError());
        return E_FAIL;
    }

    if (rc > sz_self) {
        fwprintf_s(stderr, L"[!] GetCurrentDirectoryA() failed - buffer was too small. (%u > %u)\n", rc, sz_self);
        return E_FAIL;
    }

    HRESULT hr = StringCchCatA(g_dir_self, sz_self, "\\");
    if (hr != S_OK) {
        fprintf_s(stderr, "[!] StringCchCatA(%s, %s) failed.\n", g_dir_self, "\\");
    }

    return hr;
}

// Given the name of a dependency DLL, obtains its full path.
static HRESULT s0_main_get_dependency_path(
    LPSTR  dep_path, // A pointer to a buffer for the full path string.
    LPCSTR dep_name  // The file name of the DLL to obtain the full path of.
) {
    HRESULT hr = StringCchCatA(dep_path, MAX_PATH, g_dir_self);
    if (hr != S_OK) {
        fprintf_s(stderr, "[!] StringCchCatA(%s, %s) failed.\n", dep_path, g_dir_self);
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

    bool external_debug  = wcsstr(g_args_self, L"--extdbg") != nullptr;
    bool wait_for_attach = wcsstr(g_args_self, L"--wait")   != nullptr;

    DWORD creation_flags = external_debug
        ? CREATE_SUSPENDED
        : DEBUG_ONLY_THIS_PROCESS; // A debugged process is implicitly suspended until debug events are handled/pumped.

    PROCESS_INFORMATION pi;
    STARTUPINFOW        si = { 0 };

    si.cb = sizeof(STARTUPINFOW);

    // Create target process in suspended or debugged state.
    if (!CreateProcessW(
        g_target,
        g_args_target,
        nullptr,
        nullptr,
        FALSE,
        creation_flags,
        nullptr,
        g_dir_target,
        &si,
        &pi
    )) {
        fwprintf_s(stderr, L"Failed to create target process.\n");
        return 1;
    }

    // Pause for external debugger attach if `--wait` arg is passed.
    if (wait_for_attach) {
        fwprintf_s(stdout, L"You can now attach a debugger; press any key to continue.\n");
        int i = _getch();
    }

    /* [fkelava 17/09/26 17:11]
     * The target binary may use relative path addressing. It must therefore start with the
     * working directory set to its own. However, Stage 1 has dependencies stored alongside
     * itself, which isn't on the target binary's search path.
     *
     * For loading to succeed, we inject all of its dependencies too. The order is not incidental;
     * a DLL must be preceded by all its dependencies. nethost and MinHook thankfully only
     * depend on system libraries, which are on the PATH.
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
