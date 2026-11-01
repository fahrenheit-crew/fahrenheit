// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

/* [fkelava 30/10/26 13:13]
 * Fahrenheit reimplements the base game's save/load system to allow for customizable user interfaces,
 * save set capability, etc. It also fixes a number of bugs, such as invalid display of player
 * names in other languages, inability to distinguish wetsuit Rikku from Seymour, and more.
 *
 * This file contains the save set and implementation controls. For the PAL, see `save_pal`.
 */

/// <summary>
///     The possible states of the game's default save data manager.
/// </summary>
public enum SaveSystemState : int {
    IDLE           = 0x00,
    SAVE           = 0x01,
    SAVE_SUCCEED   = 0x02,
    LOAD           = 0x03,
    LOAD_SUCCEED   = 0x04,
    DELETE         = 0x05, // Effectively unused on PC
    DELETE_SUCCEED = 0x06  // Effectively unused on PC
}

/// <summary>
///     The possible states of the game's save/load screen dialog box.
/// </summary>
public enum SaveDialogState {
    CLOSED = 0x00,
    UNK5   = 0x05
}

/// <summary>
///     The possible states of the game's save data screen.
/// </summary>
public enum SaveScreenState {
    CLOSED  = 0x00,
    OPENING = 0x01,
    OPEN    = 0x02,
    UNK3    = 0x03,
    UNK4    = 0x04
}

/// <summary>
///     Combines with save data to form a save file.
/// </summary>
/// <remarks>This is a union type. Only the active game's accessors are valid.</remarks>
[StructLayout(LayoutKind.Explicit)]
public struct SaveHeader {
    [FieldOffset(0x0)] public FFX .SaveHeader x;
    [FieldOffset(0x0)] public FFX2.SaveHeader x2;
}

/// <summary>
///     Combines with a save header to form a save file.
/// </summary>
/// <remarks>This is a union type. Only the active game's accessors are valid.</remarks>
[StructLayout(LayoutKind.Explicit)]
public struct SaveData {
    [FieldOffset(0x0)] public FFX .SaveData x;
    [FieldOffset(0x0)] public FFX2.SaveData x2;
}

/// <summary>
///     The core of the game's default save/load system.
/// </summary>
/// <remarks>This is a union type. Only the active game's accessors are valid.</remarks>
[StructLayout(LayoutKind.Explicit)]
internal struct SaveDataManager {
    [FieldOffset(0x0)] public FFX .SaveDataManager x;
    [FieldOffset(0x0)] public FFX2.SaveDataManager x2;
}

/// <summary>
///     Contains per-save information for save/load UIs.
/// </summary>
/// <remarks>The properties are automatically localized to the active game language.</remarks>
public class FhSaveUiData {
    [InlineArray(3)]
    public struct IconChr {
        private FhTexture _e0;
    }

    internal FhSaveUiData() { }

    /// <summary>
    ///     Marks this UI data as temporarily invalidated.
    /// </summary>
    internal bool valid;

    /// <summary>
    ///     A copy of the save's header.
    /// </summary>
    /// <remarks>Do not modify its contents.</remarks>
    public SaveHeader header;

    /// <summary>
    ///     A copy of the save's data.
    /// </summary>
    /// <remarks>Do not modify its contents.</remarks>
    public SaveData data;

    /// <summary>
    ///     The slot that this save occupies.
    /// </summary>
    public int    slot         { get; internal set; } = 0xFFFF;

    /// <summary>
    ///     The slot that this save occupies, as a string.
    /// </summary>
    public string slot_str     { get; internal set; } = "";

    /// <summary>
    ///     The time spent in this save.
    /// </summary>
    public string play_time    { get; internal set; } = "";

    /// <summary>
    ///     The name of the primary player character in this save.
    /// </summary>
    public string player_name  { get; internal set; } = "";

    /// <summary>
    ///     The active story chapter in this save.
    /// </summary>
    /// <remarks>Only valid in FF X-2.</remarks>
    public string chapter      { get; internal set; } = "";

    /// <summary>
    ///     The story completion, as a percentage, in this save.
    /// </summary>
    /// <remarks>Only valid in FF X-2.</remarks>
    public string completion   { get; internal set; } = "";

    /// <summary>
    ///     The name of the current location in this save.
    /// </summary>
    public string location     { get; internal set; } = "";

    /// <summary>
    ///     The level of the player character in this save.
    /// </summary>
    /// <remarks>Only valid in FF X-2 LM.</remarks>
    public string lm_level     { get; internal set; } = "";

    /// <summary>
    ///     The job of the player character in this save.
    /// </summary>
    /// <remarks>Only valid in FF X-2 LM.</remarks>
    public string lm_job       { get; internal set; } = "";

    /// <summary>
    ///     A set of icons for the player party in this save.
    /// </summary>
    public IconChr icon_chr    { get; internal set; } = new();

    /// <summary>
    ///    The icon for the current location in this save.
    /// </summary>
    public FhTexture icon_map  { get; internal set; } = new("", FhTextureType.NULL);

    /// <summary>
    ///     The local time at which this save was last written to.
    /// </summary>
    public DateTimeOffset create_time { get; internal set; } = DateTimeOffset.MinValue;
}

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
    internal bool save(int slot);

    /// <summary>Load the save file in the specified slot.</summary>
    /// <param name="slot">The slot to load from.</param>
    internal bool load(int slot);

    /// <summary>Compile Al Bhed primers from a specified slot.</summary>
    /// <remarks>
    ///     Compilation fills the currently loaded save data's obtained
    ///     Al Bhed primers with ones from the chosen save file.
    /// </remarks>
    /// <param name="slot">The slot to compile primers from.</param>
    internal bool copy_albd(int slot);

    /// <summary>Exit the save screen through cancellation or error.</summary>
    internal void abort();
}

/// <summary>
///     A named group of save files.
/// </summary>
public sealed class FhSaveSet {
    public string name         { get; internal init; } = "";
    public bool   has_autosave { get; internal set;  }
    public int    count        { get; internal set;  }
    public bool   is_full      { get; internal set;  }
}

/* [fkelava 07/11/25 15:01]
 * Fh computes a load-order sensitive hash over all mods that declare 'separate saves'.
 * It creates a 'default' save set for that hash. The user may create other sets manually.
 * At runtime, you may swap between all sets for the hash.
 */

/// <summary>
///     The interface to the game's save system,
///     and a manager that allows multiple sets of saves to exist.
/// </summary>
public sealed partial class FhSaves {

    internal readonly FhRuntimeHandle<IFhSaveSystemImpl> impl_handle = new();

    private const int MAX_SAVES_PER_SET = 101;

    private static readonly string _sets_path_base;
    private static readonly string _sets_path_default;
    private static readonly string _sets_name_default = "default";
    private static          string _sets_name_active; // The active set's name.

    private readonly Dictionary<string, FhSaveSet> _sets; // The available save sets.
    private readonly Dictionary<string, FhSaveUi>  _ui;   // The available save user interfaces.
    private readonly FhSaveUiData[]                _ui_data;

    private          int          _lock;
    private readonly HashSet<int> _slots; // The slots occupied in the active set.

    static FhSaves() {
        _sets_path_base    = Path.Join(FhEnvironment.Finder.Saves.FullName, FhInternal.Hasher.SaveSetHash);
        _sets_path_default = Path.Join(_sets_path_base, _sets_name_default, pal_get_save_subfolder());
        _sets_name_active  = _sets_name_default;
    }

    public FhSaves() {
        _slots   = [];
        _sets    = [];
        _ui      = [];
        _ui_data = new FhSaveUiData[MAX_SAVES_PER_SET];

        /* [fkelava 19/01/26 18:13]
         * Indexing cannot be done here because game functions
         * are not callable before module initialization has run.
         *
         * It is instead performed just in time when we switch to save/load mode.
         */

        _sets_create_default();
    }

    /// <summary>
    ///     Ensures the default save set exists.
    /// </summary>
    private void _sets_create_default() {
        _ = Directory.CreateDirectory(_sets_path_default);
    }

    /// <summary>
    ///     Reads a save file and prepares its display data.
    /// </summary>
    private Task _sets_index_save(int index) {
        string             save_file_path = pal_get_path_for_slot(index);
        ReadOnlySpan<char> slot_str       = save_file_path[ ^3 .. ];

        /* [fkelava 30/10/26 16:38]
         * UI data is sorted at end of indexing. Invalid UI data moves to the end,
         * i.e. the first `set.count` UI data are the ones for the valid saves.
         */

        FhSaveUiData ui_data = _ui_data[index] ??= new();
        ui_data.valid = false;

        if (!int.TryParse(slot_str, out int slot))
            return Task.CompletedTask;

        try {
            using MemoryMappedFile         save_file     = MemoryMappedFile.CreateFromFile(save_file_path);
            using MemoryMappedViewAccessor save_accessor = save_file.CreateViewAccessor(0, pal_get_sz_save());

            save_accessor.Read(0, out ui_data.header);

            switch (FhGlobal.game_id) {
                case FhGameId.FFX:                     save_accessor.Read(0x40, out ui_data.data.x ); break;
                case FhGameId.FFX2 or FhGameId.FFX2LM: save_accessor.Read(0x40, out ui_data.data.x2); break;

                default: break;
            }
        }
        catch { return Task.CompletedTask; }

        FhSaveUiData.IconChr icon_chr = ui_data.icon_chr;
        icon_chr[0] = _pal_get_icon_chr(ui_data.header, ui_data.data, 0);
        icon_chr[1] = _pal_get_icon_chr(ui_data.header, ui_data.data, 1);
        icon_chr[2] = _pal_get_icon_chr(ui_data.header, ui_data.data, 2);

        ui_data.slot        = slot;
        ui_data.slot_str    = slot.ToString();
        ui_data.create_time = File.GetLastWriteTime(save_file_path);
        ui_data.play_time   = _pal_get_play_time  (ui_data.header);
        ui_data.player_name = _pal_get_player_name(ui_data.header);
        ui_data.chapter     = _pal_get_chapter    (ui_data.header);
        ui_data.completion  = _pal_get_completion (ui_data.header);
        ui_data.location    = _pal_get_location   (ui_data.header);
        ui_data.lm_level    = _pal_get_lm_level   (ui_data.header);
        ui_data.lm_job      = _pal_get_lm_job     (ui_data.header);
        ui_data.icon_map    = _pal_get_icon_map   (ui_data.header);
        ui_data.icon_chr    = icon_chr;
        ui_data.valid       = true;

        _slots.Add(slot);
        return Task.CompletedTask;
    }

    /// <summary>
    ///     Indexes the active save set. This function must be called under lock.
    /// </summary>
    private void _sets_index() {
        _slots.Clear();

        string path_set_folder = Path.Join(
            _sets_path_base,
            _sets_name_active,
            pal_get_save_subfolder());

        /* [fkelava 08/02/26 15:00]
         * Save sets mirror the base game save directory's structure, i.e. the subfolders
         * 'FINAL FANTASY X', 'FINAL FANTASY X-2', 'FINAL FANTASY X-2 LAST MISSION' exist.
         *
         * When the user creates a set manually, they may forget to create these subdirectories.
         * When loading, this fails safely. When saving this is lethal because we assume
         * the path exists for simplicity. In this case we silently correct the error.
         */
        _ = Directory.CreateDirectory(path_set_folder);

        Task[] tasks = new Task[MAX_SAVES_PER_SET];

        for (int i = 0; i < MAX_SAVES_PER_SET; i++) {
            tasks[i] = _sets_index_save(i);
        }

        Task.WaitAll(tasks);

        _sets[_sets_name_active].has_autosave = File.Exists(pal_get_path_for_slot(0));
        _sets[_sets_name_active].count        = _slots.Count;

        _ui_data.Sort(_pal_sort_create_time);
    }

    /// <summary>
    ///     Registers basic information about a given save set directory.
    /// </summary>
    /// <param name="set_dir_path">The absolute path to the save set directory.</param>
    private Task _sets_query(string set_dir_path) {
        string set_name        = Path.GetFileName(set_dir_path);
        string set_subdir_path = Path.Join       (set_dir_path, pal_get_save_subfolder());

        _ = Directory.CreateDirectory(set_subdir_path);

        ReadOnlySpan<string> files = [ .. Directory.EnumerateFiles(set_subdir_path, _pal_get_save_name_glob()) ];

        _sets[set_name] = new FhSaveSet() {
            name         = set_name,
            has_autosave = File.Exists(pal_get_path_for_slot(0, set_name)),
            count        = int.Min(MAX_SAVES_PER_SET, files.Length)
        };

        return Task.CompletedTask;
    }

    /// <summary>
    ///     Queries the disk for available save sets.
    /// </summary>
    private void _sets_query_all() {
        _sets.Clear();

        string[] sets  = [ .. Directory.EnumerateDirectories(_sets_path_base) ];
        Task  [] tasks = new Task[sets.Length];

        for (int i = 0; i < sets.Length; i++) {
            tasks[i] = _sets_query(sets[i]);
        }

        Task.WaitAll(tasks);

        /* [fkelava 19/01/26 14:50]
         * The default set is forcibly regenerated if it does not exist,
         * and we fall back to it if the active set was torched.
         */

        if (!_sets.TryGetValue(_sets_name_active, out _)) {
            FhInternal.Log.Warning($"Active set {_sets_name_active} was deleted; falling back to default.");

            if (!_sets.TryGetValue(_sets_name_default, out _)) {
                FhInternal.Log.Error("Default set was deleted; forcibly re-generating.");

                _sets_create_default();
                _sets_query_all();
            }

            _sets_name_active = _sets_name_default;
        }
    }

    /// <summary>
    ///     Reindexes the active save set.
    /// </summary>
    internal void sets_index() {
        if (Interlocked.CompareExchange(ref _lock, 1, 0) != 0)
            return;

        _pal_cache_init();
        _pal_cache_tex_unload();

        _sets_index();

        _pal_cache_tex_load();

        Interlocked.Decrement(ref _lock);
    }

    /// <summary>
    ///     Regenerates the available save sets.
    /// </summary>
    /// <remarks>
    ///     Regenerating sets involves expensive file operations,
    ///     so only do this when you have to.
    /// </remarks>
    internal void sets_query() {
        if (Interlocked.CompareExchange(ref _lock, 1, 0) != 0)
            return;

        _sets_query_all();

        Interlocked.Decrement(ref _lock);
    }

    /// <summary>Retrieve the loadable sets.</summary>
    /// <returns>A set of the available set names.</returns>
    public IEnumerable<string> sets_enumerate() {
        return _sets.Keys;
    }

    /// <summary>Switch to the specified save set, and index it.</summary>
    /// <remarks>
    ///     Indexing a set involves expensive file operations,
    ///     so only do this when you have to.
    /// </remarks>
    /// <param name="set_name">The name of the set to switch to.</param>
    public void sets_change(string set_name) {
        if (Interlocked.CompareExchange(ref _lock, 1, 0) != 0)
            return;

        _pal_cache_tex_unload();

        _sets_name_active = set_name;
        _sets_index();

        _pal_cache_tex_load();

        Interlocked.Decrement(ref _lock);
    }

    /// <summaryRetrieve the active set.</summary>
    public FhSaveSet sets_get_active() => _sets[_sets_name_active];

    /// <summaryRetrieve a specific set.</summary>
    public bool sets_get(string set_name, [NotNullWhen(true)] out FhSaveSet? set) {
        return _sets.TryGetValue(set_name, out set);
    }

    /// <summary>
    ///     Retrieves the UI data at a specific index in the active set.
    /// </summary>
    public bool ui_get_data(int index, [NotNullWhen(true)] out FhSaveUiData? data) {
        data = default;
        if (index >= _sets[_sets_name_active].count)
            return false;

        data = _ui_data[index];
        return true;
    }

    /// <summary>Get the save UI associated with the given ID.</summary>
    /// <param name="id">The ID of the desired UI.</param>
    /// <param name="ui">The UI with the given ID.</param>
    /// <returns>Whether the operation succeeded.</returns>
    internal bool ui_get(string id, [NotNullWhen(true)] out FhSaveUi? ui) {
        return _ui.TryGetValue(id, out ui);
    }

    /// <summary>Register a new save UI for selection by the user.</summary>
    /// <param name="ui">The new UI to register.</param>
    internal void ui_register(FhSaveUi ui) {
        _ui[ui.ModuleType] = ui;
    }

    /// <summary>Retrieve the current save system mode.</summary>
    /// <param name="mode">The current mode of the system.</param>
    /// <returns>Whether the operation succeeded.</returns>
    public bool impl_get_system_mode([NotNullWhen(true)] out FhSaveSystemMode? mode) {
        mode = null;
        if (!impl_handle.get(out IFhSaveSystemImpl? impl))
            return false;

        mode = impl.get_system_mode();
        return true;
    }

    /// <summary>Executes the action for a given slot based on the save system mode.</summary>
    /// <param name="slot">The slot to perform the action for.</param>
    /// <returns>Whether the action succeeded.</returns>
    public bool impl_execute(int slot) {
        if (!impl_handle.get(out IFhSaveSystemImpl? impl))
            return false;

        return impl.get_system_mode() switch {
            FhSaveSystemMode.LOAD => impl.load     (slot),
            FhSaveSystemMode.SAVE => impl.save     (slot),
            FhSaveSystemMode.ALBD => impl.copy_albd(slot),
            _                     => false,
        };
    }

    /// <inheritdoc cref="IFhSaveSystemImpl.abort"/>
    /// <returns>Whether the operation succeeded.</returns>
    public bool impl_abort() {
        if (!impl_handle.get(out IFhSaveSystemImpl? impl))
            return false;

        impl.abort();
        return true;
    }
}
