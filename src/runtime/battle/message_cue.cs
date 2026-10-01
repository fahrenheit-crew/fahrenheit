// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

using Fahrenheit.FFX.Battle;

using static Fahrenheit.FFX.Globals.Battle;

namespace Fahrenheit.Runtime.Battle;

/// <summary>
/// Fahrenheit module used to implement custom text in message cues.
/// </summary>
//[FhLoad(FhGameId.FFX)]
public unsafe class CustomMessageCueModule : FhModule {

    public CustomMessageCueModule() { }

    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        return FFX.FhCall.MsMessageCueProcess.hook(this, _h_MsMessageCueProcess);
    }

    private int _draw_custom_message_window(byte* message) {
        FFX.FhCall.FUN_0089db10.fnptr!(0, message);
        return 7;
    }

    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ])]
    public byte _h_MsMessageCueProcess() {
        if (btl->__0x2076 > 0) {
            btl->__0x2076 -= 1;
        }

        byte bVar2 = btl->__0x2076;

        if (btl->message_cue_count == 0) {
            return 0;
        }

        byte bVar3 = btl->__0x2077;
        while (true) {
            if (btl->__0x2040 != 0) {
                if (btl->__0x2040 > -1) {
                    if ( bVar2 != 0
                      && ((FhUtil.get_at<byte>((nint)btl + 0x2034) & 0x20) == 0
                       || (FhUtil.get_at<byte>((nint)btl + 0x2038) & 0x20) == 0
                       ||  bVar3 <= bVar2)) {
                        return btl->message_cue_count;
                    }
                    FFX.FhCall.TOBtlCloseSimpleHelpMes.fnptr!();
                }

                bVar2 = 0;
                bVar3 = 0;
                btl->__0x2040 = 0;
                btl->__0x2076 = 0;
                btl->__0x2077 = 0;
                btl->__0x2075 = (byte)((btl->__0x2075 + 1) & 3);
                btl->message_cue_count -= 1;

                if (btl->message_cue_count == 0) {
                    return 0;
                }

                continue;
            }

            int cue_idx = btl->__0x2075 & 3;
            MessageCue cue = btl->message_cues[cue_idx];

            if (cue.type is MessageCueType.LEARN_COMMAND or MessageCueType.LEARN_LIMIT_TYPE) {
                FFX.FhCall.MsRegSEplay.fnptr!(0xFF, 0x39);
            }

            int draw_ret = cue.type switch {
                MessageCueType.PLY_NAME         => FFX.FhCall.TOBtlDrawStdChrNameMessageWindow       .fnptr!(cue.arg1, cue.arg2),
                MessageCueType.PREEMPTIVE       => FFX.FhCall.TOBtlDrawFirstStrikePlayerMessageWindow.fnptr!(),
                MessageCueType.AMBUSH           => FFX.FhCall.TOBtlDrawFirstStrikeEnemyMessageWindow .fnptr!(),
                MessageCueType.STEAL_ITEM       => FFX.FhCall.TOBtlDrawGetItemMessageWindow          .fnptr!((byte*)cue.arg1, cue.arg2),
                MessageCueType.CAPTURE_MONSTER  => FFX.FhCall.TOBtlDrawCaptureMonsterMessageWindow   .fnptr!(cue.arg1, cue.arg2),
                MessageCueType.LEARN_COMMAND    => FFX.FhCall.TOBtlDrawLearningMessageWindow         .fnptr!(cue.arg1, cue.arg2),
                MessageCueType.LEARN_LIMIT_TYPE => FFX.FhCall.TOBtlDrawGetLimitTypeMessageWindow     .fnptr!(cue.arg1, cue.arg2),
                MessageCueType.STEAL_MONEY      => FFX.FhCall.TOBtlDrawGetMoneyMessageWindow         .fnptr!(cue.arg1),
                MessageCueType.FH_CUSTOM        => _draw_custom_message_window((byte*)cue.arg1),
                _                               => -1,
            };

            bVar2 = cue.__0x2;
            bVar3 = cue.__0x1;
            btl->__0x2040 = draw_ret;
            btl->__0x2076 = bVar2;
            btl->__0x2077 = bVar3;
        }
    }
}
