// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime;

internal sealed class FhPrintControlSettings : FhSettingProvider<FhPrintControlModule> {
    protected internal override IEnumerable<FhSetting> get() => [
        enable,
    ];

    public readonly FhSettingToggle enable = new("enable", false);
}

/// <summary>
///     Controls the game's emission of debug prints.
/// </summary>
[FhLoad(FhGameId.FFX | FhGameId.FFX2 | FhGameId.FFX2LM)]
public unsafe sealed class FhPrintControlModule : FhModule {

    private readonly FhPrintControlSettings _settings = new();

    public FhPrintControlModule() { }

    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        return FhCall.FUN_000354f0_0048cd70.hook(this, h_pprintf) &&
               FhCall.rcPrint              .hook(this, h_printf)  &&
               FhCall.dbgPrintf            .hook(this, h_printf)  &&
               FhCall.scePrintf            .hook(this, h_printf)  &&
               FhCall.AtelPs2DebugString   .hook(this, h_printf)  &&
               FhCall.AtelPs2DebugString2  .hook(this, h_printf);
    }

    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    private void h_pprintf(int sev, byte* ptr_str) {
        if (!_settings.enable.get())
            return;

        string str = Marshal.PtrToStringAnsi((nint)ptr_str)!;
        str        = str.ReplaceLineEndings("");

        _logger.Log(FhLogLevel.Info, $"[{sev}] {str}");
    }

    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    private void h_printf(byte* fmt,
        nint va0,  nint va1,  nint va2,  nint va3,
        nint va4,  nint va5,  nint va6,  nint va7,
        nint va8,  nint va9,  nint va10, nint va11,
        nint va12, nint va13, nint va14, nint va15
    ) {
        if (!_settings.enable.get())
            return;

        FhCall.Phyre_PhyrePrintf.fnptr!(2, fmt, va0, va1, va2, va3, va4, va5, va6, va7, va8, va9, va10, va11, va12, va13, va14, va15);
    }

}
