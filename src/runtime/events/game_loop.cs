// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime.Events;

[FhLoad(FhGameId.FFX | FhGameId.FFX2 | FhGameId.FFX2LM)]
public class GameLoopEventsImplModule : FhModule {

    public GameLoopEventsImplModule() { }

    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        // TODO: Support `PostReturnToTitle` in FFX-2
        bool is_ffx = FhGlobal.game_id is FhGameId.FFX;

        return FhCall.Sg_MainLoop.hook(this, raise_update_events)
            && (!is_ffx || FFX.FhCall.AtelSetEventJump2.hook(this, handle_warp));
    }

    /// <summary>
    ///     Runs around the game's main loop to raise the
    ///     <see cref="Fahrenheit.Events.GameLoopEvents.PreUpdate">PreUpdate</see>
    ///     and <see cref="Fahrenheit.Events.GameLoopEvents.PostUpdate">PostUpdate</see>
    ///     events before and after every iteration, respectively.
    /// </summary>
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    private void raise_update_events(float delta) {
        FhApi.Events.Common.GameLoop.PreUpdate.invoke(new() { delta = delta });

        FhCall.Sg_MainLoop.chain_from(raise_update_events).fnptr!(delta);

        FhApi.Events.Common.GameLoop.PostUpdate.invoke(new() { delta = delta });
    }

    /// <summary>
    ///     Runs after the player warps to a new room to raise the
    ///     <see cref="Fahrenheit.Events.GameLoopEvents.PostReturnToTitle">PostReturnToTitle</see>
    ///     event.
    /// </summary>
    /// <param name="room">The room we are warping to</param>
    /// <param name="entrance">The entrance the main character will spawn at</param>
    /// <param name="do_fade">Non-zero if we should fade, zero if not</param>
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    private void handle_warp(int room, int entrance, int do_fade) {
        FFX.FhCall.AtelSetEventJump2.chain_from(handle_warp).fnptr!(room, entrance, do_fade);

        if (room == 23 && entrance == 0) {
            // Warping to the title screen
            FhApi.Events.Common.GameLoop.PostReturnToTitle.invoke(EventArgs.Empty);
        }
    }
}
