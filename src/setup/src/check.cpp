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

    REGSAM sam = KEY_READ;
    if      (type == X86) sam |= KEY_WOW64_32KEY;
    else if (type == X64) sam |= KEY_WOW64_64KEY;

    HKEY    key;
    LSTATUS ret = RegOpenKeyExW(
        HKEY_LOCAL_MACHINE,
        key_name,
        0,
        sam,
        &key
    );

    if (ret != ERROR_SUCCESS) {
        fwprintf_s(stderr, L"[!] RegOpenKeyExW() failed with code 0x%X.\n", ret);
        return false;
    }

    DWORD val_installed = 0;
    DWORD val_major     = 0;
    DWORD val_minor     = 0;
    DWORD val_bld       = 0;

    DWORD reg_type_sz = sizeof(DWORD);
    DWORD reg_flags   = RRF_RT_REG_DWORD;

    if (RegGetValueW(key, nullptr, L"Installed", reg_flags, nullptr, &val_installed, &reg_type_sz) != ERROR_SUCCESS ||
        RegGetValueW(key, nullptr, L"Major",     reg_flags, nullptr, &val_major,     &reg_type_sz) != ERROR_SUCCESS ||
        RegGetValueW(key, nullptr, L"Minor",     reg_flags, nullptr, &val_minor,     &reg_type_sz) != ERROR_SUCCESS ||
        RegGetValueW(key, nullptr, L"Bld",       reg_flags, nullptr, &val_bld,       &reg_type_sz) != ERROR_SUCCESS
    ) {
        fwprintf_s(stderr, L"[!] RegGetValueW() failed with code 0x%X.\n", ret);
        return false;
    }

    /* [fkelava 13/11/26 15:35]
     * https://devblogs.microsoft.com/oldnewthing/20221219-00/?p=107601/
     * > If the compiler’s self-reported version is `aa.bb.ccccc.dd`, then _MSC_VER is `aabb` and _MSC_FULL_VER is `aabbccccc`.
     * > In other words, `_MSC_VER = aa * 100 + bb` and `_MSC_FULL_VER = aa * 10000000 + bb * 100000 + cc`.
     */

    const auto MSC_MAJOR = _MSC_FULL_VER / 10000000;
    const auto MSC_MINOR = _MSC_VER - MSC_MAJOR * 100;
    const auto MSC_BUILD = _MSC_FULL_VER % 100000;

    /* [fkelava 13/11/26 15:35]
     * https://learn.microsoft.com/en-us/cpp/overview/compiler-versions?view=msvc-170#visual-studio-2026-and-later
     * > The MSVC build tools that ship with Visual Studio 2026 and later start at version 14.50, and _MSC_VER starts at 1950.
     */

    return val_installed == 1
        && val_major     >= (MSC_MAJOR - 5)
        && val_minor     >=  MSC_MINOR
        && val_bld       >=  MSC_BUILD;
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
