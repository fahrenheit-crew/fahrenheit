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

static LPTOP_LEVEL_EXCEPTION_FILTER WINAPI stage1_eh_set_filter(LPTOP_LEVEL_EXCEPTION_FILTER fnptr_exception_filter) {
    return NULL;
}

BOOL stage1_eh_suppress(LPBYTE ptr_main_module) {
    char_t exe_full_name_buf[MAX_PATH];
    auto size = ::GetModuleFileNameW(NULL, exe_full_name_buf, sizeof(exe_full_name_buf) / sizeof(char_t));

    std::basic_string<char_t> exe_full_name       = exe_full_name_buf;
    size_t                    exe_name_dirsep_pos = exe_full_name.find_last_of(L'\\') + 1;

    if (exe_name_dirsep_pos == std::basic_string<char_t>::npos) {
        std::wcerr << "The path to the target binary is invalid." << std::endl;
        return FALSE;
    }

    std::basic_string<char_t> exe_name = exe_full_name.substr(exe_name_dirsep_pos, exe_full_name.length());

    // This can be generalized for other games in the future.
    if (exe_name.compare(L"FFX.exe")   != 0
    &&  exe_name.compare(L"FFX-2.exe") != 0)
        return TRUE;

    SetUnhandledExceptionFilter(NULL);

    // We don't care about the original SEH filter in the slightest, so we don't keep it.
    void* fnptr_eh_original = NULL;

    if (MH_CreateHookApi(L"kernel32.dll", "SetUnhandledExceptionFilter", &stage1_eh_set_filter, &fnptr_eh_original) != MH_OK
    ||  MH_EnableHook   (&SetUnhandledExceptionFilter)                                                              != MH_OK) {
        std::wcerr << "Failed to suppress SEH filter install for " << exe_name << std::endl;
        return FALSE;
    }

    std::wcout << "Suppressed SEH filter install for " << exe_name << std::endl;
    return TRUE;
}
