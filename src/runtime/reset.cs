// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime;

/* [fkelava 31/08/26 12:43]
 * This may be removed out to EFP or become a standalone mod in a future update.
 */

/// <summary>
///     Provides a 'soft reset' function.
/// </summary>
[FhLoad(FhGameId.FFX | FhGameId.FFX2 | FhGameId.FFX2LM)]
public sealed class FhSoftResetModule : FhModule {

    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        return true;
    }

    public override void render_imgui() {
        bool activated = (ImGui.IsKeyDown(ImGuiKey.GamepadL1) && ImGui.IsKeyDown(ImGuiKey.GamepadR1) && ImGui.IsKeyPressed(ImGuiKey.GamepadStart))
                      || (ImGui.IsKeyDown(ImGuiKey.R)         && ImGui.IsKeyDown(ImGuiKey.S)         && ImGui.IsKeyPressed(ImGuiKey.T));

        if (!activated)
            return;

        if (FhCall.MsBattleCheck.fnptr!() != 0) {
            FhUtil.set_at<ushort>(FhUtil.select(0xD2C9F1, 0x9F94B5, 0x9F94B5), 1); // btl.battle_end_type = GAME_OVER
            FhUtil.set_at<byte>  (FhUtil.select(0xD2A8E0, 0x9F78A0, 0x9F78A0), 2); // btl.battle_state    = END
            return;
        }
          
        FhCall.graphicDestroyFmv.fnptr!();
        FhCall.AtelJumpGameOver .fnptr!();
    }

}
