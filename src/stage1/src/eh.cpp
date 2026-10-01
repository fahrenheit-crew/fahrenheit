// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

/* [fkelava 11/06/26 16:27]
 * FF X/X-2 HD have a rather poor SEH filter (ex. FFX.exe+226A90).
 * It will sometimes not generate dumps, and they don't include as much information as they ought to.
 *
 * We would also like to present a more informative error description to the user.
 * We therefore stub out the games' SEH filters, and Stage0 takes the role of crash handler and core dumper.
 */

#include "fhstage1.h"

static LPTOP_LEVEL_EXCEPTION_FILTER WINAPI s1_eh_set_filter(LPTOP_LEVEL_EXCEPTION_FILTER fnptr_exception_filter) {
    return NULL;
}

BOOL s1_eh_suppress() {
    wchar_t exe_path[MAX_PATH] = { 0 };
    wchar_t exe_name[MAX_PATH] = { 0 };

    DWORD size_exe_full_name = GetModuleFileNameW(
        NULL,
        exe_path,
        sizeof(exe_path) / sizeof(wchar_t)
    );

    if (size_exe_full_name == 0) {
        fwprintf_s(stderr, L"[!] GetModuleFileNameW failed with error code 0x%X.\n", GetLastError());
        return FALSE;
    }

    wchar_t* ptr_dirsep = wcsrchr(exe_path, L'\\');

    if (ptr_dirsep == NULL) {
        fwprintf_s(stderr, L"The path to the target binary is invalid.\n");
        return FALSE;
    }

    if (FAILED(StringCchCopyW(exe_name, MAX_PATH, ptr_dirsep + 1))) {
        fwprintf_s(stderr, L"[!] StringCchCopyW failed.\n");
        return FALSE;
    }

    // This can be generalized for other games in the future.
    if (wcscmp(exe_name, L"FFX.exe")   != 0
    &&  wcscmp(exe_name, L"FFX-2.exe") != 0)
        return TRUE;

    SetUnhandledExceptionFilter(NULL);

    /* [fkelava 16/09/26 18:29]
     * We don't care about the original SEH filter in the slightest, so we don't keep it.
     * This is safe to do because MinHook checks that ppOriginal is not NULL before assigning it.
     *
     * https://github.com/TsudaKageyu/minhook/blob/8af6b4acae5a9388fd742b56fa79ece89d96f823/src/hook.c#L633
     */
    if (MH_CreateHookApi(L"kernel32.dll", "SetUnhandledExceptionFilter", &s1_eh_set_filter, NULL) != MH_OK ||
        MH_EnableHook   (&SetUnhandledExceptionFilter)                                                != MH_OK
    ) {
        fwprintf_s(stderr, L"Failed to suppress SEH filter install for %s.\n", exe_name);
        return FALSE;
    }

    fwprintf_s(stdout, L"Suppressed SEH filter install for %s.\n", exe_name);
    return TRUE;
}
