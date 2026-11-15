// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

#include <fhsetup.h>

// Win32
#include <strsafe.h>
#include <pathcch.h>
#include <msi.h>
#include <shlwapi.h>
#include <ShObjIdl.h>
#include <ShlGuid.h>

// (U)CRT
#include <stdio.h>

// .NET hosting API
#include <nethost.h>
#include <coreclr_delegates.h>
#include <hostfxr.h>

hostfxr_initialize_for_runtime_config_fn g_fnptr_hostfxr_init;
hostfxr_close_fn                         g_fnptr_hostfxr_close;

/* [fkelava 13/11/26 13:40]
 * This is adapted from Stage 1. It should be kept in sync with `s1_load_hostfxr`.
 */

// Uses the `nethost` library to discover the location of the .NET hosting library,
// `hostfxr`, and obtains the necessary function pointers from it.
static bool setup_check_hostfxr() {
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

// Create a shortcut to a given object.
static HRESULT setup_create_link(
    LPCWSTR path_target,  // [in] The absolute path to the target.
    LPCWSTR path_link,    // [in] The absolute path where the shortcut is to be saved.
    LPCWSTR path_workdir, // [in] The absolute path to the working directory of the shortcut's target.
    LPCWSTR args          // [in] The command-line arguments for the shortcut.
) {
    IShellLinkW*  ptr_IShellLinkW  = nullptr;
    IPersistFile* ptr_IPersistFile = nullptr;

    HRESULT hr = CoCreateInstance(
        CLSID_ShellLink,
        NULL,
        CLSCTX_INPROC_SERVER,
        IID_IShellLinkW,
        (LPVOID*) &ptr_IShellLinkW
    );

    if (FAILED(hr))
        return hr;

    hr = ptr_IShellLinkW->SetWorkingDirectory(path_workdir);

    if (FAILED(hr)) {
        fwprintf_s(stderr, L"IShellLinkW::SetWorkingDirectory() failed with code 0x%X.\n", hr);

        ptr_IShellLinkW->Release();
        return hr;
    }

    hr = ptr_IShellLinkW->SetPath(path_target);

    if (FAILED(hr)) {
        fwprintf_s(stderr, L"IShellLinkW::SetPath() failed with code 0x%X.\n", hr);

        ptr_IShellLinkW->Release();
        return hr;
    }

    hr = ptr_IShellLinkW->SetArguments(args);

    if (FAILED(hr)) {
        fwprintf_s(stderr, L"IShellLinkW::SetArguments() failed with code 0x%X.\n", hr);

        ptr_IShellLinkW->Release();
        return hr;
    }

    hr = ptr_IShellLinkW->QueryInterface(
        IID_IPersistFile,
        (LPVOID*) &ptr_IPersistFile
    );

    if (FAILED(hr)) {
        fwprintf_s(stderr, L"IShellLinkW::QI(IPersistFile) failed with code 0x%X.\n", hr);

        ptr_IShellLinkW->Release();
        return hr;
    }

    hr = ptr_IPersistFile->Save(path_link, TRUE);
    ptr_IPersistFile->Release();

    return hr;
}

static bool setup_create_links(
    wchar_t* path_ffx, // [in] The absolute path to `FFX.exe`.
    wchar_t* path_ffx2 // [in] The absolute path to `FFX-2.exe`.
) {
    const wchar_t* name_link_ffx    = L"\\Final Fantasy X (Fahrenheit).lnk";
    const wchar_t* name_link_ffx2   = L"\\Final Fantasy X-2 (Fahrenheit).lnk";
    const wchar_t* name_link_ffx2lm = L"\\Final Fantasy X-2 LM (Fahrenheit).lnk";

    wchar_t path_fhstage0   [MAX_PATH] = { 0 }; // The absolute path to `fhstage0.exe`.
    wchar_t path_workdir    [MAX_PATH] = { 0 }; // The absolute path of the game's working directory.
    wchar_t path_linkdir    [MAX_PATH] = { 0 }; // The absolute path of the directory the shortcuts shall be placed in.
    wchar_t path_link_ffx   [MAX_PATH] = { 0 }; // The absolute path of the shortcut for launching FF X.
    wchar_t path_link_ffx2  [MAX_PATH] = { 0 }; // The absolute path of the shortcut for launching FF X-2.
    wchar_t path_link_ffx2lm[MAX_PATH] = { 0 }; // The absolute path of the shortcut for launching FF X-2 LM.
    wchar_t args_ffx2lm     [MAX_PATH] = { 0 }; // The arguments to launch FF X-2 LM with.

    PathQuoteSpacesW(path_ffx);
    PathQuoteSpacesW(path_ffx2);

    // Link directory prep
    DWORD path_linkdir_size = GetModuleFileNameW(
        NULL,
        path_linkdir,
        sizeof(path_linkdir) / sizeof(wchar_t)
    );

    if (path_linkdir_size == 0) {
        fwprintf_s(stderr, L"[!] GetModuleFileNameW() failed with code 0x%X.\n", GetLastError());
        return false;
    }

    HRESULT hr;
    if ((hr = PathCchRemoveFileSpec(path_linkdir, MAX_PATH)) != S_OK) {
        fwprintf_s(stderr, L"PathCchRemoveFileSpec() failed for path %s, error code: %X\n", path_linkdir, hr);
        return false;
    }

    // Working directory prep
    if (FAILED(StringCchCatW(path_workdir, MAX_PATH, path_linkdir))) {
        fwprintf_s(stderr, L"[!] Failed to prepare path of game working directory.\n");
        return false;
    }

    if (FAILED(StringCchCatW(path_fhstage0, MAX_PATH, path_linkdir     )) ||
        FAILED(StringCchCatW(path_fhstage0, MAX_PATH, L"\\fhstage0.exe"))
    ) {
        fwprintf_s(stderr, L"[!] Failed to prepare path to Stage 0.\n");
        return false;
    }

    PathQuoteSpacesW(path_fhstage0);

    // Link path prep
    if ((hr = PathCchRemoveFileSpec(path_linkdir, MAX_PATH)) != S_OK) {
        fwprintf_s(stderr, L"PathCchRemoveFileSpec() failed for path %s, error code: %X\n", path_linkdir, hr);
        return false;
    }

    if (FAILED(StringCchCatW(path_link_ffx,    MAX_PATH, path_linkdir    )) ||
        FAILED(StringCchCatW(path_link_ffx2,   MAX_PATH, path_linkdir    )) ||
        FAILED(StringCchCatW(path_link_ffx2lm, MAX_PATH, path_linkdir    )) ||
        FAILED(StringCchCatW(path_link_ffx,    MAX_PATH, name_link_ffx   )) ||
        FAILED(StringCchCatW(path_link_ffx2,   MAX_PATH, name_link_ffx2  )) ||
        FAILED(StringCchCatW(path_link_ffx2lm, MAX_PATH, name_link_ffx2lm))
    ) {
        fwprintf_s(stderr, L"[!] Failed to prepare link paths.\n");
        return false;
    }

    // X-2 LM args prep
    if (FAILED(StringCchCatW(args_ffx2lm, MAX_PATH, path_ffx2              )) ||
        FAILED(StringCchCatW(args_ffx2lm, MAX_PATH, L" -- FFX2_LASTMISSION"))
    ) {
        fwprintf_s(stderr, L"[!] Failed to prepare target paths.\n");
        return false;
    }

    // Link creation
    hr = setup_create_link(
        path_fhstage0,
        path_link_ffx,
        path_workdir,
        path_ffx
    );

    if (FAILED(hr))
        return false;

    hr = setup_create_link(
        path_fhstage0,
        path_link_ffx2,
        path_workdir,
        path_ffx2
    );

    if (FAILED(hr))
        return false;

    hr = setup_create_link(
        path_fhstage0,
        path_link_ffx2lm,
        path_workdir,
        args_ffx2lm
    );

    return SUCCEEDED(hr);
}

bool setup_check_vcredist(FH_SETUP_REDIST_TYPE type) {
    /* [fkelava 12/11/26 17:43]
     * There are many ways of checking for VC++ presence, but we use the only semi-official one. Per MSDN:
     *
     * https://learn.microsoft.com/en-us/cpp/windows/redistributing-visual-cpp-files?view=msvc-170#install-the-redistributable-packages
     * > The current {...} version {...} stored in HKLM\SOFTWARE\Wow6432Node\Microsoft\VisualStudio\14.0\VC\Runtimes\{x86|x64|arm64}.
     * > (You need to check {...} Wow6432Node {...} only {...} to view the version of the x86 package on an x64 platform.)
     *
     * We use KEY_WOW64_32KEY to automatically redirect us to Wow6432Node as necessary.
     */

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

    const int MSC_MAJOR = _MSC_FULL_VER / 10000000;
    const int MSC_MINOR = _MSC_VER - MSC_MAJOR * 100;
    const int MSC_BUILD = _MSC_FULL_VER % 100000;

    /* [fkelava 13/11/26 15:35]
     * https://learn.microsoft.com/en-us/cpp/overview/compiler-versions?view=msvc-170#visual-studio-2026-and-later
     * > The MSVC build tools that ship with Visual Studio 2026 and later start at version 14.50, and _MSC_VER starts at 1950.
     */

    return val_installed == 1
        && val_major     >= (MSC_MAJOR - 5)
        && val_minor     >=  MSC_MINOR
        && val_bld       >=  MSC_BUILD;
}

bool setup_check_nethost(FH_SETUP_REDIST_TYPE type) {
    if (!setup_check_hostfxr())
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

bool setup_check_gamedir(
    const wchar_t*        ptr_path, // [in]  A user-supplied directory to check.
          FH_SETUP_ERROR& error     // [out] The detailed error code, if the function fails.
) {
    wchar_t path_ffx [MAX_PATH] = { 0 };
    wchar_t path_ffx2[MAX_PATH] = { 0 };
    wchar_t path_vbf [MAX_PATH] = { 0 };
    wchar_t path_vbf2[MAX_PATH] = { 0 };

    if (FAILED(StringCchCatW(path_ffx,  MAX_PATH, ptr_path                )) ||
        FAILED(StringCchCatW(path_ffx2, MAX_PATH, ptr_path                )) ||
        FAILED(StringCchCatW(path_vbf,  MAX_PATH, ptr_path                )) ||
        FAILED(StringCchCatW(path_vbf2, MAX_PATH, ptr_path                )) ||
        FAILED(StringCchCatW(path_ffx,  MAX_PATH, L"\\FFX.exe"            )) ||
        FAILED(StringCchCatW(path_ffx2, MAX_PATH, L"\\FFX-2.exe"          )) ||
        FAILED(StringCchCatW(path_vbf,  MAX_PATH, L"\\data\\FFX_Data.vbf" )) ||
        FAILED(StringCchCatW(path_vbf2, MAX_PATH, L"\\data\\FFX2_Data.vbf"))
    ) {
        fwprintf_s(stderr, L"[!] Failed to prepare target names.\n");

        error = INTERNAL_ERROR;
        return false;
    }

    DWORD attr_ffx  = GetFileAttributesW(path_ffx );
    DWORD attr_ffx2 = GetFileAttributesW(path_ffx2);
    DWORD attr_vbf  = GetFileAttributesW(path_vbf );
    DWORD attr_vbf2 = GetFileAttributesW(path_vbf2);

    if ((attr_ffx  == INVALID_FILE_ATTRIBUTES || attr_ffx  & FILE_ATTRIBUTE_DIRECTORY) ||
        (attr_ffx2 == INVALID_FILE_ATTRIBUTES || attr_ffx2 & FILE_ATTRIBUTE_DIRECTORY)
    ) {
        error = MISSING_BIN;
        return false;
    }

    if ((attr_vbf  == INVALID_FILE_ATTRIBUTES || attr_vbf  & FILE_ATTRIBUTE_DIRECTORY) ||
        (attr_vbf2 == INVALID_FILE_ATTRIBUTES || attr_vbf2 & FILE_ATTRIBUTE_DIRECTORY)
    ) {
        error = MISSING_VBF;
        return false;
    }

    MSIFILEHASHINFO hash_info = { };
    hash_info.dwFileHashInfoSize = sizeof(MSIFILEHASHINFO);

    UINT hash_rc = MsiGetFileHashW(path_ffx, 0, &hash_info);

    if (hash_rc != ERROR_SUCCESS) {
        fwprintf_s(stderr, L"[!] MsiGetFileHashW(FFX.exe) failed with code 0x%X.\n", hash_rc);

        error = INTERNAL_ERROR;
        return false;
    }

    if (hash_info.dwData[0] != 0x67239580 ||
        hash_info.dwData[1] != 0xA48DCCE2 ||
        hash_info.dwData[2] != 0xD9DFAFCB ||
        hash_info.dwData[3] != 0xB2ED8427
    ) {
        error = INVALID_BIN;
        return false;
    }

    hash_rc = MsiGetFileHashW(path_ffx2, 0, &hash_info);

    if (hash_rc != ERROR_SUCCESS) {
        fwprintf_s(stderr, L"[!] MsiGetFileHashW(FFX-2.exe) failed with code 0x%X.\n", hash_rc);

        error = INTERNAL_ERROR;
        return false;
    }

    if (hash_info.dwData[0] != 0xF2059BB3 ||
        hash_info.dwData[1] != 0xC780DCA9 ||
        hash_info.dwData[2] != 0x49F12153 ||
        hash_info.dwData[3] != 0xFB9E85E1
    ) {
        error = INVALID_BIN;
        return false;
    }

    if (!setup_create_links(path_ffx, path_ffx2)) {
        error = INTERNAL_ERROR;
        return false;
    }

    return true;
}
