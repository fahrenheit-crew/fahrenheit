// SPDX-License-Identifier: MIT

namespace Fahrenheit.Runtime;

/* [fkelava 23/02/26 16:59]
 * Mods must be able to persist some information between saves or game sessions.
 *
 * They must not store such information in empty space of the save data, lest they make the save
 * impossible to load in their absence. They should also not place such data in random places on
 * the user's system where they might be hard to find, move, or delete.
 *
 * Fahrenheit provides a callback invoked on save/load, allowing mods to read from and write to an
 * isolated file per save game. This is called 'local state'. State travels with the user's Fahrenheit folder.
 *
 * Modules also have access to an analogous 'global state' file, which is not unique per save game.
 */

/// <summary>
///     Implements the 'local state' mechanism of Fahrenheit.
///     <para/>
///     Do not interface with this module directly. Instead, implement
///     <see cref="FhModule.load_local_state(FileStream, FhLocalStateInfo)"/>
///     and <see cref="FhModule.save_local_state(FileStream)"/>.
/// </summary>
[FhLoad(FhGameId.FFX | FhGameId.FFX2 | FhGameId.FFX2LM)]
public unsafe sealed class FhLocalStateModule : FhModule {

    private readonly FhModuleHandle<FhSaveManagerModule> _handle_smm;
    private          FhSaveManagerModule?                _smm;

    public FhLocalStateModule() {
        _handle_smm = new(this);
    }

    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        return _handle_smm.try_get_module(out _smm);
    }

    /// <summary>
    ///     For the given save <paramref name="slot"/> and given module's <paramref name="module_context"/>,
    ///     returns the path of the local state directory and creates it.
    /// </summary>
    private string _get_state_dir_path(FhModContext mod_context, FhModuleContext module_context, int slot) {
        string local_state_dir = Path.Join(
            FhEnvironment.Finder.State.FullName,
            FhEnvironment.StateHash,
            _smm!.get_active_set(),
            FhSavePal.pal_get_save_subfolder(),
            FhSavePal.pal_get_save_name_for_slot(slot),
            mod_context.Manifest.Id,
            module_context.Module.ModuleType);

        Directory.CreateDirectory(local_state_dir);
        return local_state_dir;
    }

    /// <summary>
    ///     Loads all modules' local state from the selected save <paramref name="slot"/>.
    /// </summary>
    internal void state_load_slot(int slot) {
        foreach (FhModContext mod_context in FhApi.Mods.get_mods()) {
            foreach (FhModuleContext module_context in mod_context.Modules) {
                string module_type   = module_context.Module.ModuleType;
                string state_dir     = _get_state_dir_path(mod_context, module_context, slot);

                string state_fn      = Path.Join(state_dir, "state");
                string state_meta_fn = Path.Join(state_dir, "state.meta.json");

                if (!File.Exists(state_fn)) continue;

                using FileStream state_meta_fs = File.Open(state_meta_fn, FileMode.Open,         FileAccess.Read, FileShare.None);
                using FileStream state_fs      = File.Open(state_fn,      FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);

                _logger.Debug($"Reading metadata for module {module_type}.");
                FhLocalStateInfo state_meta = JsonSerializer.Deserialize<FhLocalStateInfo>(state_meta_fs, FhUtil.InternalJsonOpts)
                    ?? throw new Exception("FH_E_LOCAL_STATE_META_BLOCK_NULL");

                _logger.Debug($"{state_fn} -> {module_type}.");
                module_context.Module.load_local_state(state_fs, state_meta);
            }
        }
    }

    /// <summary>
    ///     Saves all modules' local state to the selected save <paramref name="slot"/>.
    /// </summary>
    internal void state_save_slot(int slot) {
        foreach (FhModContext mod_context in FhApi.Mods.get_mods()) {
            foreach (FhModuleContext module_context in mod_context.Modules) {
                string module_type   = module_context.Module.ModuleType;
                string state_dir     = _get_state_dir_path(mod_context, module_context, slot);

                string state_fn      = Path.Join(state_dir, "state");
                string state_meta_fn = Path.Join(state_dir, "state.meta.json");

                FhLocalStateInfo state_meta = new(mod_context.Manifest.Version);

                using FileStream state_meta_fs = File.Open(state_meta_fn, FileMode.Create,       FileAccess.Write, FileShare.None);
                using FileStream state_fs      = File.Open(state_fn,      FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);

                _logger.Debug($"Writing metadata for module {module_type}.");
                state_meta_fs.Write(JsonSerializer.SerializeToUtf8Bytes(state_meta, FhUtil.InternalJsonOpts));

                _logger.Debug($"{module_type} -> {state_fn}.");
                module_context.Module.save_local_state(state_fs);
            }
        }
    }
}
