// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

#pragma comment(lib, "pathcch")
#pragma comment(lib, "nethost")

#include <fhsetup.h>

#define WIN32_LEAN_AND_MEAN // Exclude rarely-used stuff from Windows headers

// Win32
#include <windows.h>
#include <strsafe.h>
#include <pathcch.h>

// (U)CRT
#include <stdio.h>

// .NET hosting API
#include <nethost.h>
#include <coreclr_delegates.h>
#include <hostfxr.h>

hostfxr_initialize_for_runtime_config_fn g_fnptr_hostfxr_init;
hostfxr_close_fn                         g_fnptr_hostfxr_close;

/* [fkelava 12/11/26 17:43]
 * There are many ways of checking for VC++ presence, but we use the only semi-official one. Per MSDN:
 *
 * https://learn.microsoft.com/en-us/cpp/windows/redistributing-visual-cpp-files?view=msvc-170#install-the-redistributable-packages
 * > The current {...} version {...} stored in HKLM\SOFTWARE\Wow6432Node\Microsoft\VisualStudio\14.0\VC\Runtimes\{x86|x64|arm64}.
 * > (You need to check {...} Wow6432Node {...} only {...} to view the version of the x86 package on an x64 platform.)
 *
 * We use KEY_WOW64_32KEY to automatically redirect us to Wow6432Node as necessary.
 */

bool bridge_check_vcredist(BRIDGE_REDIST_TYPE type) {
    LPCWSTR key_name;
    if      (type == X86) key_name = L"SOFTWARE\\Microsoft\\VisualStudio\\14.0\\VC\\Runtimes\\X86";
    else if (type == X64) key_name = L"SOFTWARE\\Microsoft\\VisualStudio\\14.0\\VC\\Runtimes\\X64";
    else return false;

    HKEY    key;
    LSTATUS ret = RegOpenKeyExW(
        HKEY_LOCAL_MACHINE,
        key_name,
        0,
        KEY_READ | KEY_WOW64_32KEY,
        &key
    );

    if (ret != ERROR_SUCCESS) {
        fwprintf_s(stderr, L"[!] RegOpenKeyExW() failed with code 0x%X.\n", ret);
        return false;
    }

    DWORD reg_type;
    DWORD reg_value     = 0;
    DWORD reg_type_size = sizeof(reg_value);

    ret = RegGetValueW(
        key,
        nullptr,
        L"Installed",
        RRF_RT_REG_DWORD,
        &reg_type,
        &reg_value,
        &reg_type_size
    );

    if (ret != ERROR_SUCCESS) {
        fwprintf_s(stderr, L"[!] RegGetValueW() failed with code 0x%X.\n", ret);
        return false;
    }

    return reg_value == 1;
}

/* [fkelava 13/11/26 13:40]
 * This is adapted from Stage 1. It should be kept in sync with `s1_load_hostfxr`.
 */

// Uses the `nethost` library to discover the location of the .NET hosting library,
// `hostfxr`, and obtains the necessary function pointers from it.
static bool bridge_check_hostfxr() {
    if (g_fnptr_hostfxr_init && g_fnptr_hostfxr_close)
        return true;

    wchar_t path_hostfxr_buf[MAX_PATH];
    size_t  path_hostfxr_size = sizeof(path_hostfxr_buf) / sizeof(wchar_t);

    int rc = get_hostfxr_path(
        path_hostfxr_buf,
        &path_hostfxr_size,
        nullptr
    );

    if (rc != 0) {
        fwprintf_s(stderr, L"[!] get_hostfxr_path() failed with code 0x%X.\n", rc);
        return false;
    }

    HMODULE lib_hostfxr = LoadLibraryW(path_hostfxr_buf);

    if (lib_hostfxr == nullptr) {
        fwprintf_s(stderr, L"[!] LoadLibraryW() failed with code 0x%X.\n", GetLastError());
        return false;
    }

    g_fnptr_hostfxr_init  = (hostfxr_initialize_for_runtime_config_fn) GetProcAddress(lib_hostfxr, "hostfxr_initialize_for_runtime_config");
    g_fnptr_hostfxr_close = (hostfxr_close_fn)                         GetProcAddress(lib_hostfxr, "hostfxr_close");

    return g_fnptr_hostfxr_init && g_fnptr_hostfxr_close;
}

bool bridge_check_nethost(BRIDGE_REDIST_TYPE type) {
    if (!bridge_check_hostfxr())
        return false;

    LPCWSTR rtcfg_name;
    if      (type == X86) rtcfg_name = L"\\fhr.runtimeconfig.json";      // Or anything else known to be x86 only.
    else if (type == X64) rtcfg_name = L"\\fhmodmgr.runtimeconfig.json"; // Or anything else known to be x64 only.
    else return false;

    wchar_t path_fh_dir       [MAX_PATH] = { 0 };
    wchar_t path_runtimeconfig[MAX_PATH] = { 0 };

    DWORD path_fh_dir_size = GetModuleFileNameW(
        NULL,
        path_fh_dir,
        sizeof(path_fh_dir) / sizeof(wchar_t)
    );

    if (path_fh_dir_size == 0) {
        fwprintf_s(stderr, L"[!] GetModuleFileNameW() failed with code 0x%X.\n", GetLastError());
        return false;
    }

    HRESULT hr = PathCchRemoveFileSpec(path_fh_dir, MAX_PATH);
    if (hr != S_OK) {
        fwprintf_s(stderr, L"PathCchRemoveFileSpec() failed for path %s, error code: %X\n", path_fh_dir, hr);
        return false;
    }

    if (FAILED(StringCchCatW(path_runtimeconfig, MAX_PATH, path_fh_dir)) ||
        FAILED(StringCchCatW(path_runtimeconfig, MAX_PATH, rtcfg_name))
    ) {
        fwprintf_s(stderr, L"[!] StringCchCatW() failed.\n");
        return false;
    }

    hostfxr_handle cxt = nullptr;

    int rc = g_fnptr_hostfxr_init(
        path_runtimeconfig,
        nullptr,
        &cxt
    );

    if (rc != 0 || cxt == nullptr) {
        fwprintf_s(stderr, L"hostfxr: initialize_for_runtime_config() failed\n");

        if (g_fnptr_hostfxr_close(cxt) != 0) {
            fwprintf_s(stderr, L"hostfxr: failed to close.\n");
        }

        return false;
    }

    if (g_fnptr_hostfxr_close(cxt) != 0) {
        fwprintf_s(stderr, L"hostfxr: failed to close.\n");
    }

    return true;
}
