// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime;

/// <summary>
///     Contains a number of miscellaneous fixes for game defects.
/// </summary>
[FhLoad(FhGameId.FFX | FhGameId.FFX2 | FhGameId.FFX2LM)]
public sealed class FhMiscFixesModule : FhModule {

    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        return FhCall.FUN_0022A460_006B9D30.hook(this, h_focus);
    }

    /* [fkelava 06/11/26 13:37]
     * The game likes to continue accepting input when it's unfocused.
     * We stop that by simply asserting our input lock.
     */

    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    private void h_focus(byte focused) {
        if (focused == 1) FhApi.Input.lock_off();
        else              FhApi.Input.lock_on();

        FhCall.FUN_0022A460_006B9D30.chain_from(h_focus).fnptr!(focused);
    }

}
