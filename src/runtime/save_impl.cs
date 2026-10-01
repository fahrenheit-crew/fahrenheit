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
public unsafe sealed class FhSaveExtensionModule : FhModule, IFhSaveSystemImpl {

    private int              _load_pending_slot;
    private FhSaveSystemMode _mode;

    public FhSaveExtensionModule() { }

    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        FhApi.Saves.impl_handle.set(this);

        bool is_ffx = FhGlobal.game_id is FhGameId.FFX;

        return FhCall.SaveDataManager_debugSave_Internal_6F0650.hook(this, impl_autosave)
            && FhCall.TkMenuJumpToLoadedScene                  .hook(this, impl_copy)
            && FhCall.SaveDataToSave                           .hook(this, signal_enter_save)
            && FhCall.SaveDataToLoad                           .hook(this, signal_enter_load)
            && (!is_ffx || FFX.FhCall.FUN_2EFFF0.hook(this, signal_enter_albd));
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
        FhApi.Saves.index_active_set();
        _mode = FhSaveSystemMode.SAVE;
        FhSavePal.pal_set_system_state(FhSaveSystemState.SAVE);

        FhApi.Events.Common.GameLoop.PostOpenSaveMenu.invoke(EventArgs.Empty);
    }

    /// <summary>
    ///     Signals to both Fahrenheit and the game that the next save/load operation will be a load.
    /// </summary>
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    private void signal_enter_load() {
        FhApi.Saves.index_active_set();
        _mode = FhSaveSystemMode.LOAD;
        FhSavePal.pal_set_system_state(FhSaveSystemState.LOAD);

        FhApi.Events.Common.GameLoop.PostOpenSaveMenu.invoke(EventArgs.Empty);
    }

    /// <summary>
    ///     Signals to both Fahrenheit and the game that the next save/load operation will be
    ///     an Al Bhed Compilation Sphere load. Only valid in FF-X.
    /// </summary>
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl ) ] )]
    private void signal_enter_albd() {
        FhApi.Saves.index_active_set();
        _mode = FhSaveSystemMode.ALBD;
        FhSavePal.pal_set_system_state(FhSaveSystemState.LOAD);

        FhApi.Events.Common.GameLoop.PostOpenSaveMenu.invoke(EventArgs.Empty);
    }

    /// <summary>
    ///     Signals to the game that the pending save/load operation ended in failure.
    /// </summary>
    internal void signal_exit_abort() {
        FhSavePal.pal_set_cancel_state(1);
        FhCall.SaveDataSaveLoadSucceed.fnptr!(_mode is FhSaveSystemMode.SAVE
            ? FhSaveSystemState.SAVE
            : FhSaveSystemState.LOAD);
        FhSavePal.pal_set_dialog_state(FhSaveDialogState.CLOSED);

        FhApi.Events.Common.GameLoop.PostCloseSaveMenu.invoke(EventArgs.Empty);
    }

    /// <summary>
    ///     Signals to the game that the pending save/load operation ended in success.
    /// </summary>
    internal void signal_exit_success() {
        FhSavePal.pal_set_cancel_state(0);
        FhCall.SaveDataSaveLoadSucceed.fnptr!(_mode is FhSaveSystemMode.SAVE
            ? FhSaveSystemState.SAVE
            : FhSaveSystemState.LOAD);
        FhSavePal.pal_set_dialog_state(FhSaveDialogState.CLOSED);

        FhApi.Events.Common.GameLoop.PostCloseSaveMenu.invoke(EventArgs.Empty);
    }

    /* [fkelava 16/01/26 14:29]
     * https://github.com/fahrenheit-crew/fahrenheit/issues/70
     *
     * Loading in the game is a multi-step process:
     * 1) Data from the disk is loaded to `SaveDataManager::getRefBuffer`.
     * 2) CRC checking and other validation is performed on the ref buffer.
     * 3) If verification passed, the save data is later copied to `save_ram`.
     * 4) Fahrenheit's SaveData accessors (and the rest of the game) only look at `save_ram`.
     *
     * with the following constraints:
     * - Between 1) and 3) there is at least one main loop iteration of delay.
     * - SaveData must be inspectable at `load_local_state` time.
     * - SaveExtensionModule must load into the ref buffer for correctness, because
     *   `SaveDataCheckCrc` et al. implicitly operate on the ref buffer.
     * - Information about the slot being loaded is lost after 1).
     *
     * Thus we defer `load_local_state` to after 3), storing the pending load slot at 1).
     *
     * As the actual function performing the copy to `save_ram` is inlined on some platforms,
     * we hook its parent function, TkMenuJumpToLoadedScene.
     */

    [UnmanagedCallConv(CallConvs = [ typeof(CallConvStdcall) ] )]
    private void impl_copy() {
        FhCall.TkMenuJumpToLoadedScene.chain_from(impl_copy).fnptr!();
        FhInternal.State.state_load_slot(_load_pending_slot);

        FhApi.Events.Common.GameLoop.PostLoadGame.invoke(new() { save_slot_idx = _load_pending_slot });
    }

    /// <summary>
    ///     Creates the autosave, the save game in the reserved slot 0.
    /// </summary>
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    private void impl_autosave(int size, byte* ptr) {
        FhCall.SaveDataWriteCrc       .fnptr!(ptr);
        FhCall._SetUpDefaultSaveFolder.fnptr!();

        string             save_path = FhApi.Saves.get_save_path_for_slot(0);
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
    void IFhSaveSystemImpl.save(int slot) {
               slot      = FhApi.Saves.remap_slot(slot);
        string save_path = FhApi.Saves.get_save_path_for_slot(slot);

        ReadOnlySpan<byte> save = new(FhSavePal.pal_addr_buf_save(), FhSavePal.pal_sz_buf_save());
        FhCall.SaveDataWriteCrc.fnptr!(FhSavePal.pal_addr_buf_save());

        // TODO: add popups on success/failure

        using (FileStream save_stream = File.OpenWrite(save_path)) {
            save_stream.Write(save);
        }

        FhInternal.State.state_save_slot(slot);
        signal_exit_success();
    }

    /// <inheritdoc cref="IFhSaveSystemImpl.load"/>
    void IFhSaveSystemImpl.load(int slot) {
        string     save_name = FhApi.Saves.get_save_path_for_slot(slot);
        Span<byte> save      = new(FhSavePal.pal_addr_buf_save(), FhSavePal.pal_sz_buf_save());

        // TODO: add popups on success/failure
        using (FileStream save_stream = File.OpenRead(save_name)) {
            save_stream.ReadExactly(save);
        }

        // From Iggy state handler 0xF
        if (FhCall.SaveDataCheckCrc.fnptr!() == 0) {
            save.Clear();

            signal_exit_abort();
            return;
        }

        bool player_needs_rename = FhCall.isNeedRenamePlayer.fnptr!(
            save[ FhSavePal.pal_header_offset_playerrename() ]);

        FhUtil.set_at(FhSavePal.pal_addr_force_player_rename(), player_needs_rename);

        _load_pending_slot = slot;
        signal_exit_success(); // TODO: popup if success
    }

    /// <inheritdoc cref="IFhSaveSystemImpl.copy_albd"/>
    void IFhSaveSystemImpl.copy_albd(int slot) {
        string save_name = FhApi.Saves.get_save_path_for_slot(slot);

        //Span<byte> save = new(
        //    FhUtil.ptr_at<byte>(pal_addr_buf_save()),
        //    pal_buf_save_size()
        //    );

        //using (FileStream save_stream = File.OpenRead(save_name)) {
        //    save_stream.ReadExactly(save);
        //}

        signal_exit_success();
    }

    /// <inheritdoc cref="IFhSaveSystemImpl.exit_cancel"/>
    void IFhSaveSystemImpl.exit_cancel() {
        signal_exit_abort();
    }

    /// <inheritdoc cref="IFhSaveSystemImpl.exit_success"/>
    void IFhSaveSystemImpl.exit_success() {
        signal_exit_success();
    }
}
