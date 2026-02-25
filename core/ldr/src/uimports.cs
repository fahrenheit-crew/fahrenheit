// SPDX-License-Identifier: MIT

using System.Runtime.Versioning;

namespace Fahrenheit.Core.Stage0;

[SupportedOSPlatform("windows")]
internal unsafe static partial class Detours {

    public static BOOL UPDATE_IMPORTS_XX(HANDLE hProcess, HMODULE hModule, byte* plpDlls, uint nDlls) {


        return BOOL.TRUE;
    }

}
