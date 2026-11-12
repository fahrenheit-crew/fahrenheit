// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

#include <fhsetup.h>

// Win32
#include <windows.h>

// (U)CRT
#include <stdio.h>

// .NET hosting API
#include <nethost.h>
#include <coreclr_delegates.h>
#include <hostfxr.h>

hostfxr_initialize_for_runtime_config_fn g_fnptr_hostfxr_init;
hostfxr_close_fn                         g_fnptr_hostfxr_close;

/* [fkelava 12/11/26 17:43]
 * There are many ways out there of checking for VC++ presence,
 * but we use the only semi-official one. Per MSDN:
 *
 * > The current {...} version {...} stored in HKLM\SOFTWARE\Wow6432Node\Microsoft\VisualStudio\14.0\VC\Runtimes\{x86|x64|arm64}.
 * > (You need to check {...} Wow6432Node {...} only {...} to view the version of the x86 package on an x64 platform.)
 *
 * https://learn.microsoft.com/en-us/cpp/windows/redistributing-visual-cpp-files?view=msvc-170#install-the-redistributable-packages
 */

// TODO: CHECK FOR PROPER MINOR/MAJOR REVISION OF VC REDIST

bool bridge_check_vcredist_x86() {
    HKEY    key;
    LSTATUS ret = RegOpenKeyExW(
        HKEY_LOCAL_MACHINE,
        L"SOFTWARE\\Microsoft\\VisualStudio\\14.0\\VC\\Runtimes\\X86",
        0,
        KEY_READ | KEY_WOW64_32KEY,
        &key
    );

    if (ret != ERROR_SUCCESS)
        return false;

    DWORD type;
    DWORD value = 0;
    DWORD size  = sizeof(value);

    ret = RegGetValueW(
        key,
        nullptr,
        L"Installed",
        RRF_RT_REG_DWORD,
        &type,
        &value,
        &size
    );

    if (ret != ERROR_SUCCESS)
        return false;

    return value == 1;
}

bool bridge_check_vcredist_x64() {
    HKEY    key;
    LSTATUS ret = RegOpenKeyExW(
        HKEY_LOCAL_MACHINE,
        L"SOFTWARE\\Microsoft\\VisualStudio\\14.0\\VC\\Runtimes\\X64",
        0,
        KEY_READ | KEY_WOW64_64KEY,
        &key
    );

    if (ret != ERROR_SUCCESS)
        return false;

    DWORD type;
    DWORD value = 0;
    DWORD size  = sizeof(value);

    ret = RegGetValueW(
        key,
        nullptr,
        L"Installed",
        RRF_RT_REG_DWORD,
        &type,
        &value,
        &size
    );

    if (ret != ERROR_SUCCESS)
        return false;

    return value == 1;
}

// TODO: BORROW STAGE 1 HOSTFXR CODE

bool bridge_check_nethost_x86() {
    return false;
}

bool bridge_check_nethost_x64() {
    return false;
}
