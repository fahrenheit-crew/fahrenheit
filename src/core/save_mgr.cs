// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

using SaveCounts     = Dictionary<FhGameId, Dictionary<string, int>>;
using UserInterfaces = Dictionary<string, FhSaveUi>;

/// <summary>Represents the current operating mode of the save system.</summary>
public enum FhSaveSystemMode {
    /// <summary>The save system is not active.</summary>
    NULL = 0,

    /// <summary>The save system wants to load a save file.</summary>
    LOAD = 1,

    /// <summary>The save system wants to save to a slot.</summary>
    SAVE = 2,

    /// <summary>The save system wants to compile Al Bhed information from a save file to the current save data.</summary>
    /// <remarks>This mode is only valid and entered in Final Fantasy X.</remarks>
    ALBD = 3,
}

internal interface IFhSaveSystemImpl {
    /// <summary>Retrieve the current save system mode.</summary>
    /// <returns>The current mode of the save system.</returns>
    internal FhSaveSystemMode get_system_mode();

    /// <summary>Save to the specified slot.</summary>
    /// <param name="slot">The slot to save to. Set this to <c>0</c> if saving to a new slot.</param>
    internal void save(int slot);

    /// <summary>Load the save file in the specified slot.</summary>
    /// <param name="slot">The slot to load from.</param>
    internal void load(int slot);

    /// <summary>Compile Al Bhed primers from a specified slot.</summary>
    /// <remarks>
    ///     Compilation fills the currently loaded save data's obtained
    ///     Al Bhed primers with ones from the chosen save file.
    /// </remarks>
    /// <param name="slot">The slot to compile primers from.</param>
    internal void copy_albd(int slot);

    /// <summary>Exit the save screen through cancellation or error.</summary>
    internal void exit_cancel();

    /// <summary>Exit the save screen through successfully loading, saving, or compiling a save.</summary>
    internal void exit_success();
}

/// <summary>
///     Allows multiple sets of saves to exist.
/// </summary>
public sealed class FhSaves {

    internal readonly FhRuntimeHandle<IFhSaveSystemImpl> impl_handle = new();

    /* [fkelava 07/11/25 15:01]
     * Fh computes a load-order sensitive hash over all mods that declare 'separate saves'.
     * It creates a 'default' save set for that hash. The user may create manual sets.
     * At runtime, you may swap between all sets for the hash.
     */

    private          int                     _sm_lock;
    private readonly HashSet<string>         _sm_sets;
    private readonly HashSet<string>         _sm_sets_with_autosaves;
    private readonly SaveCounts              _sm_set_save_counts;
    private readonly HashSet<int>            _sm_occupied_slots;
    private          string                  _sm_active_set;
    private readonly string                  _sm_path_base;
    private readonly string                  _sm_path_default_set;
    private readonly List<FhSaveDisplayData> _sm_display_data;
    private readonly UserInterfaces          _sm_uis;

    /// <summary>The name of the active set.</summary>
    public string active_set => _sm_active_set;

    /// <summary>The list of display data of saves in the active set.</summary>
    public List<FhSaveDisplayData> display_data => _sm_display_data;

    public FhSaves() {
        _sm_path_base           = Path.Join(FhEnvironment.Finder.Saves.FullName, FhInternal.Hasher.SaveSetHash);
        _sm_path_default_set    = Path.Join(_sm_path_base, FhSavePal.DEFAULT_SET_NAME, FhSavePal.pal_get_save_subfolder());
        _sm_occupied_slots      = [];
        _sm_sets                = [];
        _sm_sets_with_autosaves = [];
        _sm_active_set          = FhSavePal.DEFAULT_SET_NAME;
        _sm_display_data        = [];
        _sm_uis                 = [];

        _sm_set_save_counts  = new() {
            { FhGameId.FFX,    [] },
            { FhGameId.FFX2,   [] },
            { FhGameId.FFX2LM, [] },
        };

        /* [fkelava 19/01/26 18:13]
         * Save PAL is not ready to perform indexing operations at 'init' time because
         * AtelGetSaveDicName & co. are not usable before game initialization has run.
         *
         * Set indexing is performed just in time when SEM transitions to save/load mode.
         */

        _sm_create_default_set();
        _sm_query_sets();
    }

    /* [fkelava 19/01/26 12:03]
     * The save manager is considered essential, as is any part of the runtime. We throw
     * if we cannot do basic file I/O; there is no point trying to handle it gracefully.
     */

    /// <summary>
    ///     Ensures the default set exists. A default set must exist for every state hash.
    /// </summary>
    private void _sm_create_default_set() {
        _ = Directory.CreateDirectory(_sm_path_default_set);
    }

    /// <summary>
    ///     Indexes the active save set's directory. This function must be called under lock.
    /// </summary>
    private void _sm_index_active_set() {
        _sm_display_data  .Clear();
        _sm_occupied_slots.Clear();

        /* [fkelava 08/02/26 15:00]
         * Save sets mirror the base game save directory's structure, i.e. the subfolders
         * 'FINAL FANTASY X', 'FINAL FANTASY X-2', 'FINAL FANTASY X-2 LAST MISSION' exist.
         *
         * When the user creates a set manually (not through the Fh API or mod manager),
         * it is possible they forgot to create these subdirectories. When loading this fails
         * safely because the saves will simply never be found. When saving this is lethal
         * because SMM assumes the path it is writing to exists for simplicity.
         *
         * In this case we silently correct their error. I/O faults are still propagated
         * because that is not something we can gracefully handle.
         */

        string path_set_folder = Path.Join(
            _sm_path_base,
            _sm_active_set,
            FhSavePal.pal_get_save_subfolder());

        _ = Directory.CreateDirectory(path_set_folder);

        foreach (var save_file in Directory.EnumerateFiles(path_set_folder, FhUtil.select("ffx_*", "ffx2_*", "ffx2_*"))) {
            FhSaveDisplayData  display_data = new();
            ReadOnlySpan<char> slot_str     = save_file[ (save_file.LastIndexOf('_') + 1) .. ];

            if (!int.TryParse(slot_str, out int slot))
                continue;

            using (FileStream save_file_stream = File.OpenRead(save_file)) {
                save_file_stream.ReadExactly(display_data.header);
            }

            FhSavePal.pal_get_location   (display_data.header, display_data.location);
            FhSavePal.pal_get_icon_map   (display_data.header, display_data.icon_map);
            FhSavePal.pal_get_player_name(display_data.header, display_data.player_name);
            FhSavePal.pal_get_playtime   (display_data.header, display_data.play_time);
            FhSavePal.pal_get_chapter    (display_data.header, display_data.chapter);
            FhSavePal.pal_get_completion (display_data.header, display_data.completion);
            FhSavePal.pal_get_lm_job     (display_data.header, display_data.lm_job);
            FhSavePal.pal_get_lm_level   (display_data.header, display_data.lm_level);

            display_data.slot        = slot;
            display_data.slot_str    = slot.ToString();
            display_data.create_time = File.GetLastWriteTimeUtc(save_file);

            _sm_occupied_slots.Add(slot);
            _sm_display_data  .Add(display_data);
        }
    }

    /// <summary>
    ///     Queries the disk for available save sets.
    /// </summary>
    private void _sm_query_sets() {
        _sm_sets               .Clear();
        _sm_sets_with_autosaves.Clear();

        _sm_set_save_counts[FhGameId.FFX]   .Clear();
        _sm_set_save_counts[FhGameId.FFX2]  .Clear();
        _sm_set_save_counts[FhGameId.FFX2LM].Clear();

        foreach (string dir in Directory.EnumerateDirectories(_sm_path_base)) {
            string set_name = Path.GetFileName(dir);
            _sm_sets.Add(set_name);

            if (File.Exists(get_save_path_for_slot(set_name, 0))) {
                _sm_sets_with_autosaves.Add(set_name);
            }

            DirectoryInfo x_dir  = Directory.CreateDirectory(Path.Join(dir, "FINAL FANTASY X"));
            DirectoryInfo x2_dir = Directory.CreateDirectory(Path.Join(dir, "FINAL FANTASY X-2"));
            DirectoryInfo lm_dir = Directory.CreateDirectory(Path.Join(dir, "FINAL FANTASY X-2 LAST MISSION"));

            _sm_set_save_counts[FhGameId.FFX   ][set_name] = x_dir .GetFiles("ffx_*") .Length;
            _sm_set_save_counts[FhGameId.FFX2  ][set_name] = x2_dir.GetFiles("ffx2_*").Length;
            _sm_set_save_counts[FhGameId.FFX2LM][set_name] = lm_dir.GetFiles("ffx2_*").Length;
        }

        /* [fkelava 19/01/26 14:50]
         * Sets can be modified on the disk under us. The user can create a new one, delete the
         * active one, or even delete the _default_ set, which is meant to be a system invariant.
         *
         * We can't stop that, but we can disregard their nonsense. The default set is forcibly
         * regenerated if it does not exist, and we fall back to it if the active set was torched.
         */

        if (!_sm_sets.Contains(_sm_active_set)) {
            FhInternal.Log.Warning($"Active set {_sm_active_set} was deleted; falling back to default.");
            _sm_active_set = FhSavePal.DEFAULT_SET_NAME;
        }

        if (!_sm_sets.Contains(FhSavePal.DEFAULT_SET_NAME)) {
            FhInternal.Log.Error("Default set was deleted; forcibly re-generating.");

            _sm_create_default_set();
            _sm_query_sets();
        }
    }

    /// <summary>
    ///     Reindexes the active save set.
    /// </summary>
    internal void index_active_set() {
        if (Interlocked.CompareExchange(ref _sm_lock, 1, 0) != 0)
            return;

        _sm_index_active_set();

        Interlocked.Decrement(ref _sm_lock);
    }

    /// <summary>
    ///     For a given <paramref name="slot"/> in the given <paramref name="set"/>, gets the full path of the corresponding save file.
    /// </summary>
    internal string get_save_path_for_slot(string set, int slot) {
        return Path.Join(
            _sm_path_base,
            set,
            FhSavePal.pal_get_save_subfolder(),
            FhSavePal.pal_get_save_name_for_slot(slot));
    }

    /// <summary>
    ///     For a given <paramref name="slot"/>, gets the full path of the corresponding save file.
    /// </summary>
    internal string get_save_path_for_slot(int slot) {
        return Path.Join(
            _sm_path_base,
            _sm_active_set,
            FhSavePal.pal_get_save_subfolder(),
            FhSavePal.pal_get_save_name_for_slot(slot));
    }

    /// <summary>
    ///     For a given <paramref name="slot"/>, returns the slot number being saved to.
    /// </summary>
    internal int remap_slot(int slot) {
        // This method is not re-entrant.
        if (slot != 0) return slot;

        int target_slot = 1;
        while (_sm_occupied_slots.Contains(target_slot)) { target_slot++; }

        _sm_occupied_slots.Add(target_slot);
        return target_slot;
    }

    /// <summary>Get the save UI associated with the given ID.</summary>
    /// <param name="id">The ID of the desired UI.</param>
    /// <param name="ui">The UI with the given ID.</param>
    /// <returns>Whether the operation succeeded.</returns>
    internal bool get_ui(string id, [NotNullWhen(true)] out FhSaveUi? ui) {
        return _sm_uis.TryGetValue(id, out ui);
    }

    /// <summary>Register a new save UI for selection by the user.</summary>
    /// <param name="ui">The new UI to register.</param>
    public void register_ui(FhSaveUi ui) {
        _sm_uis[ui.ModuleType] = ui;
    }

    /// <summary>Regenerate and retrieve the loadable sets.</summary>
    /// <remarks>
    ///     Regenerating sets involves expensive file operations,
    ///     so only do this when you have to.
    /// </remarks>
    /// <returns>A set of the available set names.</returns>
    public IReadOnlySet<string> get_sets() {
        _sm_query_sets();
        return _sm_sets;
    }

    /// <summary>Retrieve the save counts for each set for the current game.</summary>
    /// <returns>A dictionary of save counts keyed by set names.</returns>
    public IReadOnlyDictionary<string, int> get_save_counts() {
        return _sm_set_save_counts[FhGlobal.game_id];
    }

    /// <summary>Determine whether a set contains an autosave.</summary>
    /// <param name="set_name">The name of the set to check for an autosave.</param>
    /// <returns>Whether the specified set contains an autosave.</returns>
    public bool set_has_autosave(string set_name) {
        return _sm_sets_with_autosaves.Contains(set_name);
    }

    /// <summary>Switch to the specified save set, and index it.</summary>
    /// <remarks>
    ///     Indexing a set involves expensive file operations,
    ///     so only do this when you have to.
    /// </remarks>
    /// <param name="set_name">The name of the set to switch to.</param>
    public void switch_active_set(string set_name) {
        if (Interlocked.CompareExchange(ref _sm_lock, 1, 0) != 0)
            return;

        _sm_active_set = set_name;
        _sm_index_active_set();

        Interlocked.Decrement(ref _sm_lock);
    }

    /// <summary>Get the number of slots used in the current set, not including the autosave.</summary>
    /// <returns>The number of slots used.</returns>
    public int get_slots_used() {
        return _sm_occupied_slots.Contains(0)
            ? _sm_display_data.Count - 1
            : _sm_display_data.Count;
    }

    /// <summary>Retrieve the current save system mode.</summary>
    /// <param name="mode">The current mode of the system.</param>
    /// <returns>Whether the operation succeeded.</returns>
    public bool get_system_mode([NotNullWhen(true)] out FhSaveSystemMode? mode) {
        mode = null;
        if (!impl_handle.get(out IFhSaveSystemImpl? impl))
            return false;

        mode = impl.get_system_mode();
        return true;
    }

    /// <inheritdoc cref="IFhSaveSystemImpl.save"/>
    /// <returns>Whether the operation succeeded.</returns>
    public bool save(int slot) {
        if (!impl_handle.get(out IFhSaveSystemImpl? impl))
            return false;

        impl.save(slot);
        return true;
    }

    /// <inheritdoc cref="IFhSaveSystemImpl.load"/>
    /// <returns>Whether the operation succeeded.</returns>
    public bool load(int slot) {
        if (!impl_handle.get(out IFhSaveSystemImpl? impl))
            return false;

        impl.load(slot);
        return true;
    }

    /// <inheritdoc cref="IFhSaveSystemImpl.copy_albd"/>
    /// <returns>Whether the operation succeeded.</returns>
    public bool copy_albd(int slot) {
        if (!impl_handle.get(out IFhSaveSystemImpl? impl))
            return false;

        impl.copy_albd(slot);
        return true;
    }

    /// <inheritdoc cref="IFhSaveSystemImpl.exit_cancel"/>
    /// <returns>Whether the operation succeeded.</returns>
    public bool exit_cancel() {
        if (!impl_handle.get(out IFhSaveSystemImpl? impl))
            return false;

        impl.exit_cancel();
        return true;
    }

    /// <inheritdoc cref="IFhSaveSystemImpl.exit_success"/>
    /// <returns>Whether the operation succeeded.</returns>
    public bool exit_success() {
        if (!impl_handle.get(out IFhSaveSystemImpl? impl))
            return false;

        impl.exit_success();
        return true;
    }
}
