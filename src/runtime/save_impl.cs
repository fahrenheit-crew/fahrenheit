// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime;

/* [fkelava 13/11/25 22:03]
 * The game's original save system is a basic manager operating a Iggy-based Flash UI.
 * It is fairly limiting. A global limit of 200 saves exist, which cannot be separated or logically organized.
 *
 * In Fahrenheit, this creates several problems for us:
 * - We would like to let mods read and write to some local state at save/load time.
 * - We would like to offer the ability to have multiple sets of saves.
 * - We would like to offer the ability to save more than 200 saves per set.
 * - We would like to offer the ability to customize the save/load user interface.
 *
 * Instead of going the roundabout way, we simply reimplement the entire save system. Other modules
 * can then query the system's state and actuate it to accomplish the above tasks,
 * and we build in local state directly into the save/load actions.
 */

/// <summary>
///     Implements Fahrenheit's extended save system.
/// </summary>
[FhLoad(FhGameId.FFX | FhGameId.FFX2 | FhGameId.FFX2LM)]
public unsafe sealed class FhSaveSystemModule : FhModule, IFhSaveSystemImpl {

    private int              _load_pending_slot = -1;
    private FhSaveSystemMode _mode;

    public FhSaveSystemModule() { }

    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        FhApi.Saves.impl_handle.set(this);

        bool is_ffx = FhGlobal.game_id is FhGameId.FFX;

        return FhCall.SaveDataManager_debugSave_Internal_6f0650.hook(this, impl_autosave)
            && FhCall.TkRefreshHdd                             .hook(this, impl_copy)
            && FhCall.SaveDataToSave                           .hook(this, signal_enter_save)
            && FhCall.SaveDataToLoad                           .hook(this, signal_enter_load)
            && (!is_ffx || FFX.FhCall.FUN_002efeb0.hook(this, signal_enter_albd));
    }

    /* [fkelava 27/11/25 02:15]
     * These five functions are the transition points to and from the save system UI.
     *
     * The entry points correspond to real game functions:
     * 'signal_enter_save' <-> 'SaveDataToSave'
     * 'signal_enter_load' <-> 'SaveDataToLoad'
     * 'signal_enter_albd' <-> CT {TBD} in FFX
     *
     * while the exit transitions are 'constructed' from bits and pieces that would normally occur
     * in Iggy state handlers 0x06, 0x11, and abort bits in HandleSaveDataScreen.
     */

    /// <summary>
    ///     Signals to both Fahrenheit and the game that the next save/load operation will be a save.
    /// </summary>
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    private void signal_enter_save() {
        FhApi.Input.lock_on();

        FhApi.Saves.sets_query();
        FhApi.Saves.sets_index();

        _mode = FhSaveSystemMode.SAVE;
        FhSaves.pal_state_system = SaveSystemState.SAVE;

        FhApi.Events.Common.GameLoop.PostOpenSaveMenu.invoke(EventArgs.Empty);
    }

    /// <summary>
    ///     Signals to both Fahrenheit and the game that the next save/load operation will be a load.
    /// </summary>
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    private void signal_enter_load() {
        FhApi.Input.lock_on();

        FhApi.Saves.sets_query();
        FhApi.Saves.sets_index();

        _mode = FhSaveSystemMode.LOAD;
        FhSaves.pal_state_system = SaveSystemState.LOAD;

        FhApi.Events.Common.GameLoop.PostOpenSaveMenu.invoke(EventArgs.Empty);
    }

    /// <summary>
    ///     Signals to both Fahrenheit and the game that the next save/load operation will be
    ///     an Al Bhed Compilation Sphere load. Only valid in FF-X.
    /// </summary>
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl ) ] )]
    private void signal_enter_albd() {
        FhApi.Input.lock_on();

        FhApi.Saves.sets_query();
        FhApi.Saves.sets_index();

        _mode = FhSaveSystemMode.ALBD;
        FhSaves.pal_state_system = SaveSystemState.LOAD;

        FhApi.Events.Common.GameLoop.PostOpenSaveMenu.invoke(EventArgs.Empty);
    }

    /// <summary>
    ///     Signals to the game that the pending save/load operation ended in failure.
    /// </summary>
    internal void signal_exit_abort() {
        FhSaves.pal_state_cancel = 1;
        FhSaves.pal_state_dialog = SaveDialogState.CLOSED;

        FhCall.SaveDataSaveLoadSucceed.fnptr!(_mode is FhSaveSystemMode.SAVE
            ? SaveSystemState.SAVE
            : SaveSystemState.LOAD);

        FhApi.Input.lock_off();
        FhApi.Events.Common.GameLoop.PostCloseSaveMenu.invoke(EventArgs.Empty);
    }

    /// <summary>
    ///     Signals to the game that the pending save/load operation ended in success.
    /// </summary>
    internal void signal_exit_success() {
        FhSaves.pal_state_cancel = 0;
        FhSaves.pal_state_dialog = SaveDialogState.CLOSED;

        FhCall.SaveDataSaveLoadSucceed.fnptr!(_mode is FhSaveSystemMode.SAVE
            ? SaveSystemState.SAVE
            : SaveSystemState.LOAD);

        FhApi.Input.lock_off();
        FhApi.Events.Common.GameLoop.PostCloseSaveMenu.invoke(EventArgs.Empty);
    }

    /* [fkelava 16/01/26 14:29]
     * https://github.com/fahrenheit-crew/fahrenheit/issues/70
     *
     * Before being copied into global `save_ram`, save data is loaded into a temporary buffer
     * where it's validated and CRC-checked. We want the save data to be inspectable at state
     * load time, so we wait for that copy to occur before proceeding.
     */

    [UnmanagedCallConv(CallConvs = [ typeof(CallConvStdcall) ] )]
    private void impl_copy() {
        FhCall.TkRefreshHdd.chain_from(impl_copy).fnptr!();

        int load_pending_slot = Interlocked.Exchange(ref _load_pending_slot, -1);
        if (load_pending_slot == -1)
            return;

        FhInternal.State.state_load_slot(load_pending_slot);
        FhApi.Events.Common.GameLoop.PostLoadGame.invoke(new() { save_slot_idx = load_pending_slot });
    }

    /// <summary>
    ///     Creates the autosave, the save game in the reserved slot 0.
    /// </summary>
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    private void impl_autosave(int size, byte* ptr) {
        FhCall.SaveDataWriteCrc       .fnptr!(ptr);
        FhCall._SetUpDefaultSaveFolder.fnptr!();

        string             save_path = FhApi.Saves.pal_get_path_for_slot(0);
        ReadOnlySpan<byte> save      = new(ptr, size);

        using (FileStream save_stream = File.OpenWrite(save_path)) {
            save_stream.Write(save);
        }

        FhInternal.State.state_save_slot(0);
    }

    /* [fkelava 19/01/26 16:12]
     * The game exhibits no-throw behavior on most violations of save system invariants. Load fails
     * with 'Save data is corrupt'. Autosave no-ops without indicating fault. Saving crashes.
     *
     * We do not follow this. Attempting to suppress failure to perform I/O in Fahrenheit dirs is
     * just kicking the ball down the curb to mod authors, who can do nothing in such cases.
     */

    FhSaveSystemMode IFhSaveSystemImpl.get_system_mode() {
        return _mode;
    }

    /// <inheritdoc cref="IFhSaveSystemImpl.save"/>
    bool IFhSaveSystemImpl.save(int slot) {
        if (slot == 0) { // This is a new save, not an overwrite.
            slot = FhApi.Saves.pal_get_open_slot();
        }

        string             save_path = FhApi.Saves.pal_get_path_for_slot(slot);
        ReadOnlySpan<byte> save      = FhSaves.pal_get_buf_save();

        try {
            FhCall.SaveDataWriteCrc.fnptr!(FhSaves.pal_get_addr_save());

            using FileStream save_stream = File.OpenWrite(save_path);
            save_stream.Write(save);
        }
        catch { return false; }

        FhInternal.State.state_save_slot(slot);

        signal_exit_success();
        return true;
    }

    /// <inheritdoc cref="IFhSaveSystemImpl.load"/>
    bool IFhSaveSystemImpl.load(int slot) {
        string     save_path = FhApi.Saves.pal_get_path_for_slot(slot);
        Span<byte> save      = FhSaves.pal_get_buf_save();

        try {
            using FileStream save_stream = File.OpenRead(save_path);
            save_stream.ReadExactly(save);
        }
        catch { return false; }

        // From Iggy state handler 0xF
        if (FhCall.SaveDataCheckCrc.fnptr!() == 0) {
            save.Clear();

            signal_exit_abort();
            return false;
        }

        // FFX.exe+2F022E, FFX-2.exe+11D0BE
        bool player_needs_rename = FhCall.isNeedRenamePlayer.fnptr!(
            save[ FhUtil.select(0x0C, 0x28, 0x28) ]
        );

        FhUtil.set_at(FhUtil.select(0xD33350, 0xA0FB70, 0xA0FB70), player_needs_rename);
        _load_pending_slot = slot;

        signal_exit_success();
        return true;
    }

    /// <inheritdoc cref="IFhSaveSystemImpl.copy_albd"/>
    bool IFhSaveSystemImpl.copy_albd(int slot) {
        string     save_name = FhApi.Saves.pal_get_path_for_slot(slot);
        Span<byte> save      = FhSaves.pal_get_buf_save();

        //using (FileStream save_stream = File.OpenRead(save_name)) {
        //    save_stream.ReadExactly(save);
        //}

        signal_exit_success();
        return true;
    }

    /// <inheritdoc cref="IFhSaveSystemImpl.abort"/>
    void IFhSaveSystemImpl.abort() {
        signal_exit_abort();
    }
}
