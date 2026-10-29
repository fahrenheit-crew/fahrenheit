// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

/* [fkelava 28/10/26 14:42]
 * https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.comparer-1.compare?view=net-10.0#notes-to-implementers
 * > Comparing null with any reference type is allowed and does not generate an exception.
 * > A null reference is considered to be less than any reference that is not null.
 */

/// <summary>
///     Contains methods for sorting save games.
/// </summary>
public static class FhSaveComparers {
    /// <summary>Sorts save games by their creation time.</summary>
    public static int compare_create_time(FhSaveUiData? x, FhSaveUiData? y) {
        if (ReferenceEquals(x, y)) return  0;
        if (x == null)             return -1;
        if (y == null)             return  1;

        if (x.slot == 0) return -1; // The autosave must precede any other save.
        if (y.slot == 0) return  1;

        if      (x.create_time > y.create_time) return -1;
        else if (x.create_time < y.create_time) return  1;

        return x.slot - y.slot;
    }

    /// <summary>Sorts save games by their slot index.</summary>
    public static int compare_slot(FhSaveUiData? x, FhSaveUiData? y) {
        if (ReferenceEquals(x, y)) return  0;
        if (x == null)             return -1;
        if (y == null)             return  1;

        return x.slot - y.slot;
    }
}

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
///     Combines with save data to form a save file.
/// </summary>
/// <remarks>This is a union type. Only the active game's accessors are valid.</remarks>
[StructLayout(LayoutKind.Explicit)]
public struct SaveHeader {
    [FieldOffset(0x0)] public FFX .SaveHeader x;
    [FieldOffset(0x0)] public FFX2.SaveHeader x2;
}

/// <summary>
///     Contains per-save information for save/load UIs.
/// </summary>
/// <remarks>
///     The associated save's header and save data fields are provided for your convenience.
///     Do not modify them.
/// </remarks>
public class FhSaveUiData {
    [InlineArray(3)]
    public struct IconChr {
        private FhTexture _e0;
    }

    internal FhSaveUiData() { }

    public SaveHeader     header;
    public SaveData       file;
    public DateTimeOffset create_time  { get; internal set; } = DateTimeOffset.MinValue;
    public int            slot         { get; internal set; } = 0xFFFF;
    public string         slot_str     { get; internal set; } = "";
    public string         play_time    { get; internal set; } = "";
    public string         player_name  { get; internal set; } = "";
    public string         chapter      { get; internal set; } = "";
    public string         completion   { get; internal set; } = "";
    public string         location     { get; internal set; } = "";
    public string         lm_level     { get; internal set; } = "";
    public string         lm_job       { get; internal set; } = "";
    public IconChr        icon_chr     { get; internal set; } = new();
    public FhTexture      icon_map     { get; internal set; } = new("", FhTextureType.NULL);
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
    internal void abort();
}

/// <summary>
///     A named group of save files.
/// </summary>
public sealed class FhSaveSet {
    public string name         { get; internal init; } = "";
    public bool   has_autosave { get; internal set;  }
    public int    count        { get; internal set;  }
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
public sealed class FhSaves {

    internal readonly FhRuntimeHandle<IFhSaveSystemImpl> impl_handle = new();

    private const string DIR_MAP_ICON_X  = "/FFX_Data/GameData/PS3Data/savedataicons";
    private const string DIR_MAP_ICON_X2 = "/FFX-2_Data/GameData/PS3Data/savedataicons";
    private const string DIR_CHR_ICON_X  = "/FFX_Data/GameData/PS3Data/menu/D3D11";
    private const string DIR_CHR_ICON_X2 = "/FFX-2_Data/GameData/PS3Data/menu/face_data/D3D11";

    private static readonly string _s_set_path_base;
    private static readonly string _s_set_path_default;
    private static readonly string _s_set_name_default = "default";

    // Sets defining which location IDs have multiple icons
    private static readonly HashSet<int> _multi_icon_locations_x = [
         31, 129, 216, 244,
        245, 246, 247, 257,
        284, 348,
    ];

    // Caches for immutable portions of certain templated game strings.
    private static int    _cache_init             = 0;
    private static string _cache_prefix_lm_level  = "";
    private static string _cache_prefix_lm_loc    = "";
    private static string _cache_prefix_play_time = "";

    // Caches for display data strings.
    private static readonly Dictionary<uint,         string>    _cache_player_name = [];
    private static readonly Dictionary<uint,         string>    _cache_play_time   = [];
    private static readonly Dictionary<ushort,       string>    _cache_location    = [];
    private static readonly Dictionary<byte,         string>    _cache_completion  = [];
    private static readonly Dictionary<byte,         string>    _cache_chapter     = [];
    private static readonly Dictionary<byte,         string>    _cache_lm_level    = [];
    private static readonly Dictionary<(byte, byte), string>    _cache_lm_job      = [];
    private static readonly Dictionary<(byte, byte), FhTexture> _cache_icon_chr    = [];
    private static readonly Dictionary<int,          FhTexture> _cache_icon_map    = [];

    private readonly Dictionary<string, FhSaveSet> _sets; // The available save sets.
    private readonly Dictionary<string, FhSaveUi>  _ui;   // The available save user interfaces.

    private const int SET_COUNT = 101;

    private          int            _lock;
    private          string         _set_active; // The active set's name.
    private readonly FhSaveUiData[] _ui_data;
    private readonly HashSet<int>   _slots;      // The slots occupied in the active set.

    static FhSaves() {
        _s_set_path_base    = Path.Join(FhEnvironment.Finder.Saves.FullName, FhInternal.Hasher.SaveSetHash);
        _s_set_path_default = Path.Join(_s_set_path_base, _s_set_name_default, pal_get_save_subfolder());
    }

    public FhSaves() {
        _set_active = _s_set_name_default;
        _sets       = [];
        _slots      = [];
        _ui         = [];
        _ui_data    = new FhSaveUiData[SET_COUNT];

        /* [fkelava 19/01/26 18:13]
         * Indexing cannot be done here because game functions
         * are not callable before module initialization has run.
         *
         * It is instead performed just in time when we switch to save/load mode.
         */
        _sets_query();
    }

    private unsafe static SaveDataManager* _pal_get_save_mgr()
        => *(SaveDataManager**)(FhEnvironment.BaseAddr + FhUtil.select(0x8E81F4, 0x9ECABC, 0x9ECABC));

    private static nint _pal_addr_screen_state() => FhUtil.select(0x8CB994, 0x9CDA50, 0x9CDA50);
    private static nint _pal_addr_dialog_state() => FhUtil.select(0x8CB998, 0x9CDA54, 0x9CDA54);

    /// <summary>
    ///     The state of the game's save/load dialog.
    /// </summary>
    internal static SaveDialogState pal_state_dialog {
        get => FhUtil.get_at<SaveDialogState>(_pal_addr_dialog_state());
        set => FhUtil.set_at(_pal_addr_dialog_state(), value);
    }

    /// <summary>
    ///     The state of the game's save/load screen.
    /// </summary>
    internal static SaveScreenState pal_state_screen {
        get => FhUtil.get_at<SaveScreenState>(_pal_addr_screen_state());
        set => FhUtil.set_at(_pal_addr_screen_state(), value);
    }

    /// <summary>
    ///     The state of the game's save/load system.
    /// </summary>
    internal unsafe static SaveSystemState pal_state_system {
        get {
            return FhGlobal.game_id switch {
                FhGameId.FFX => _pal_get_save_mgr()->x .state,
                _            => _pal_get_save_mgr()->x2.state
            };
        }
        set {
            switch (FhGlobal.game_id) {
                case FhGameId.FFX: _pal_get_save_mgr()->x .state = value; return;
                default:           _pal_get_save_mgr()->x2.state = value; return;
            }
        }
    }

    /// <summary>
    ///     The cancellation state of the current save/load operation.
    /// </summary>
    internal unsafe static int pal_state_cancel {
        get {
            return FhGlobal.game_id switch {
                FhGameId.FFX => _pal_get_save_mgr()->x .operation_canceled,
                _            => _pal_get_save_mgr()->x2.operation_canceled
            };
        }
        set {
            switch (FhGlobal.game_id) {
                case FhGameId.FFX: _pal_get_save_mgr()->x .operation_canceled = value; return;
                default:           _pal_get_save_mgr()->x2.operation_canceled = value; return;
            }
        }
    }

    internal unsafe static byte* pal_addr_buf_save() {
        return FhUtil.ptr_at<byte>(FhUtil.select(0x1197F70, 0xF9D500, 0xF9D500));
    }

    internal unsafe static Span<byte> pal_get_buf_save() {
        return new(pal_addr_buf_save(), pal_get_sz_save());
    }

    internal static int pal_get_sz_save() {
        return FhUtil.select(0x6900, 0x166A0, 0x166A0);
    }

    /// <summary>
    ///     Gets the name of the save game folder for the currently loaded game.
    /// </summary>
    internal static string pal_get_save_subfolder() {
        return FhUtil.select("FINAL FANTASY X", "FINAL FANTASY X-2", "FINAL FANTASY X-2 LAST MISSION");
    }

    /// <summary>
    ///     Gets the file name of the save file in the given <paramref name="slot"/>.
    /// </summary>
    internal static string pal_get_save_name_for_slot(int slot) {
        string prefix = FhUtil.select("ffx", "ffx2", "ffx2");
        return $"{prefix}_{slot:D3}";
    }

    /// <summary>
    ///     Gets a glob string for save game names for the currently loaded game.
    /// </summary>
    private static string _pal_get_save_name_glob() {
        return FhUtil.select("ffx_*", "ffx2_*", "ffx2_*");
    }

    /* [fkelava 18/11/25 21:27]
     * Both Iggy and ImGui take null-terminated UTF-8 strings. Neither FhEncoding nor Encoding.UTF8
     * emit terminating null bytes, so we take care to do so manually in the PAL.
     */

    private static unsafe void _pal_cache_init() {
        if (Interlocked.CompareExchange(ref _cache_init, 0, 0) == 1)
            return;

        Span<byte> dest    = stackalloc byte[0x100];
        Span<byte> scratch = stackalloc byte[0x100];

        dest   .Clear();
        scratch.Clear();

        fixed (byte* ptr_scratch = scratch) {
            // Player level/location prefix, X-2 LM
            if (FhGlobal.game_id is FhGameId.FFX2LM) {
                FhCall.SaveDataGetLoc.fnptr!(0x36B, ptr_scratch);

                int len_lm_level_prefix = FhEncoding.decode(scratch, dest, flags: FhEncodingFlags.IMPLICIT_END);
                _cache_prefix_lm_level = Encoding.UTF8.GetString(dest[ .. len_lm_level_prefix ]);

                scratch.Clear();

                // Location prefix, X-2 LM
                FhCall.SaveDataGetLoc.fnptr!(0x4C1, ptr_scratch);

                int len_lm_loc_prefix = FhEncoding.decode(scratch, dest, flags: FhEncodingFlags.IMPLICIT_END);
                _cache_prefix_lm_loc = Encoding.UTF8.GetString(dest[ .. len_lm_loc_prefix ]);

                scratch.Clear();
            }

            // Playtime prefix, all games
            FhCall.SaveDataGetLoc.fnptr!(FhUtil.select(0x52, 0x5C, 0x5C), ptr_scratch);

            int len_playtime_prefix = FhEncoding.decode(scratch, dest, flags: FhEncodingFlags.IMPLICIT_END);
            _cache_prefix_play_time = Encoding.UTF8.GetString(dest[ .. len_playtime_prefix ]);

            scratch.Clear();
        }

        _cache_init = 1;
    }

    /// <summary>
    ///     Attempt to load all character and map icons for the current set.
    /// </summary>
    private static void _pal_cache_tex_load() {
        foreach ((_, FhTexture icon_chr) in _cache_icon_chr) {
            if (!FhApi.Resources.load_texture(icon_chr)) {
                FhInternal.Log.Warning($"Failed to load texture {icon_chr}.");
            }
        }

        foreach ((_, FhTexture icon_map) in _cache_icon_map) {
            if (!FhApi.Resources.load_texture(icon_map)) {
                FhInternal.Log.Warning($"Failed to load texture {icon_map}.");
            }
        }
    }

    /// <summary>
    ///     Attempt to unload all character and map icons for the current set.
    /// </summary>
    private static void _pal_cache_tex_unload() {
        foreach (var tex in _cache_icon_chr) {
            if (!_cache_icon_chr.Remove(tex.Key, out FhTexture? icon_chr))
                throw new Exception("Failed to clear texture cache.");

            if (!FhApi.Resources.unload_texture(icon_chr))
                FhInternal.Log.Warning($"Failed to unload texture {icon_chr}.");
        }

        foreach (var tex in _cache_icon_map) {
            if (!_cache_icon_map.Remove(tex.Key, out FhTexture? icon_map))
                throw new Exception("Failed to clear texture cache.");

            if (!FhApi.Resources.unload_texture(icon_map))
                FhInternal.Log.Warning($"Failed to unload texture {icon_map}.");
        }
    }

    /* [fkelava 12/11/25 16:51]
     * To show `Tower {X}F` in LM or `Chapter {X}` and `Story Completion: {X}%` in X-2, the game gets
     * a template from SaveDataGetLoc(), with a [ 0x05, 0x30 ] sequence marking a fill point.
     * The marker must be removed after completing the fill.
     *
     * It is unclear whether 0x05 is a dialogue op code in this context or simply a random choice.
     */

    /* [fkelava 13/11/25 22:05]
     * FFX-2.exe+88000 (X-2, X-2 LM)
     */

    /// <summary>
    ///     Inserts <paramref name="fill"/> into the empty space
    ///     in a game-encoded <paramref name="template"/> string.
    /// </summary>
    private static void _pal_fill_template(Span<byte> template, int fill) {
        ReadOnlySpan<byte> marker  = [ 0x05, 0x30 ];
                Span<byte> scratch = stackalloc byte[8];

        scratch.Clear();

        int length = Encoding.UTF8.GetBytes($"{fill}", scratch);
        int target = template.IndexOf(marker);

        ReadOnlySpan<byte> post = [ .. template [ (target + 2) .. template.IndexOf((byte)0) ] ];

        /* [fkelava 04/08/26 14:16]
         * The fill-byte will always be encoded as its Basic Latin block representation,
         * but the game in CJK modes expects the fullwidth equivalent. This is thus the one case
         * where implicit extension is not merely handy, but required for proper functionality.
         *
         * See generally #200. Step through this function without the flag and the issue should be clear.
         */

        target += FhEncoding.encode(
            scratch [ .. length ],
            template[ target .. ],
            flags: FhEncodingFlags.IMPLICIT_CJK_EXTENSION
        );

        post.CopyTo(template [ target .. ]);
        template[ (target + post.Length) ] = 0;
    }

    /* [fkelava 13/11/25 21:59]
     * The following functions are excerpts from FFX.exe+2F0C60 and FFX-2.exe+11DD00.
     */

    /// <summary>
    ///     Returns the icon filename for the current map in the save with the given <paramref name="header"/>.
    /// </summary>
    private static FhTexture _pal_get_icon_map(in SaveHeader header) {
        string map_dir  = FhGlobal.game_id is FhGameId.FFX
            ? DIR_MAP_ICON_X
            : DIR_MAP_ICON_X2;

        if (FhGlobal.game_id is FhGameId.FFX2LM) {
            int lm_icon_id = ((int.Clamp(header.x2.lm_id_location / 2, 0, 80) - 1) / 20) + 1;

            if (_cache_icon_map.TryGetValue(lm_icon_id, out FhTexture? icon_texture_lm)) {
                return icon_texture_lm;
            }

            return (_cache_icon_map[lm_icon_id] = new FhTexture($"{map_dir}/logo_j_lm.png", FhTextureType.GAME));
        }

        int logo_icon_id = FhUtil.select(0, 23, 23);

        int map_icon_id = FhGlobal.game_id is FhGameId.FFX
            ? header.x .id_location
            : header.x2.id_map_icon;

        map_icon_id = FhCall.fix_mappic.fnptr!((ushort)map_icon_id);

        if (_cache_icon_map.TryGetValue(map_icon_id, out FhTexture? icon_texture)) {
            return icon_texture;
        }

        bool is_jp_logo = map_icon_id == logo_icon_id && FhCall.isNeedShowJapanLogo.fnptr!() != 0;

        if (is_jp_logo) {
            return (_cache_icon_map[map_icon_id] = new FhTexture($"{map_dir}/logo_j.png", FhTextureType.GAME));
        }

        int icon_id = map_icon_id;

        bool has_multiple_icons = FhGlobal.game_id == FhGameId.FFX
            && (_multi_icon_locations_x.Contains(icon_id)
            ||  _multi_icon_locations_x.Contains(icon_id - 1000));

        string suffix = FhGlobal.game_id switch {
            FhGameId.FFX when has_multiple_icons => icon_id < 1000 ? "_0" : "_1",
            FhGameId.FFX2                        => icon_id < 1000 ? "_0" : "_1",

            _ => string.Empty,
        };

        if (suffix != string.Empty && icon_id > 1000) {
            icon_id -= 1000;
        }

        string icon_name = $"{icon_id}{suffix}";

        return (_cache_icon_map[map_icon_id] = new FhTexture($"{map_dir}/{icon_name}.png", FhTextureType.GAME));
    }

    /// <summary>Compute the correct filename of a face icon.</summary>
    /// <returns>The filename for the face icon.</returns>
    private static FhTexture _pal_get_icon_chr(in SaveHeader header, int index) {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(index, 2, nameof(index));

        if (FhGlobal.game_id is FhGameId.FFX) {
            if (_cache_icon_chr.TryGetValue((0, 0), out FhTexture? tex_x))
                return tex_x;

            return (_cache_icon_chr[(0, 0)] = new FhTexture($"{DIR_CHR_ICON_X}/face_ply.dds.phyre", FhTextureType.GAME_PHYRE_2D));
        }

        bool is_lm = FhGlobal.game_id is FhGameId.FFX2LM;

        byte job_id = is_lm
            ? header.x2.lm_job
            : header.x2.ply_jobs[index];

        byte ply_id = is_lm
            ? header.x2.lm_ply
            : header.x2.ply[index];

        (byte, byte) key = (ply_id, job_id);

        if (_cache_icon_chr.TryGetValue(key, out FhTexture? tex))
            return tex;

        if (job_id == 0x21) {
            return (_cache_icon_chr[key] = new FhTexture($"{DIR_CHR_ICON_X2}/mface_147.dds.phyre", FhTextureType.GAME_PHYRE_2D)); // She-Goon
        }

        string ply = ply_id switch {
            0 => "yuna",
            1 => "rikku",
            2 => "paine",
            _ => "m",
        };

        // Rewires Rikku's and Paine's job IDs to match original filenames
        byte job = job_id switch {
            24 or 25       => 12, // Trainer
            26 or 27       => 14, // Mascot
            28             => 16, // Psychic
            29 or 30 or 31 => 17, // Festivalist
            32             => 18, // Freelancer

            _  => job_id,
        };

        return (_cache_icon_chr[key] = new FhTexture($"{DIR_CHR_ICON_X2}/{ply}face_{job:D3}.dds.phyre", FhTextureType.GAME_PHYRE_2D));
    }

    /// <summary>
    ///     Returns the total play time in the save with the given <paramref name="header"/>.
    /// </summary>
    private unsafe static string _pal_get_play_time(in SaveHeader header) {
        uint t_sec = header.x.playtime_secs; // The offset of the location is the same in X and X-2.
        uint t_min = t_sec / 60;

        if (_cache_play_time.TryGetValue(t_sec, out string? str_loc))
            return str_loc;

        return (_cache_play_time[t_sec] = $"{_cache_prefix_play_time}  {t_min / 60:D3}:{t_min % 60:D2}:{t_sec % 60:D2}\0");
    }

    /// <summary>
    ///     Returns the player character's name in the save with the given <paramref name="header"/>.
    /// </summary>
    private static unsafe string _pal_get_player_name(in SaveHeader header) {
        int     len_player_name;
        string? str_loc;

        Span<byte> dest = stackalloc byte[0x10];
        dest.Clear();

        if (FhGlobal.game_id is FhGameId.FFX) {
            if (_cache_player_name.TryGetValue(0, out str_loc))
                return str_loc;

            len_player_name = FhEncoding.decode(header.x.player_name, dest, header.x.lang_id, flags: FhEncodingFlags.IMPLICIT_END);
            dest [ len_player_name ] = 0x00;

            return (_cache_player_name[0] = Encoding.UTF8.GetString(dest));
        }

        byte*              ptr_player_name_encoded = stackalloc byte[0x10];
        ReadOnlySpan<byte> player_name_encoded     = new(ptr_player_name_encoded, 0x10);

        FhCall.SaveDataGetLoc.fnptr!(0xDD + header.x2.lm_ply, ptr_player_name_encoded);

        len_player_name = FhEncoding.decode(player_name_encoded, dest, flags: FhEncodingFlags.IMPLICIT_END);
        dest [ len_player_name ] = 0x00;

        return (_cache_player_name[header.x2.lm_ply] = Encoding.UTF8.GetString(dest));
    }

    /// <summary>
    ///     Returns the current location in the save with the given <paramref name="header"/>.
    /// </summary>
    private static unsafe string _pal_get_location(in SaveHeader header) {
        /* [fkelava 05/11/25 00:44]
         * Strings from AtelGetSaveDicName and SaveDataGetLoc are null-terminated. You can pass
         * a span with a bogus length to FhEncoding and it will properly handle it.
         *
         * Decodes like these (UTF-8 that is directly consumed by the game)
         * MUST specify the IMPLICIT_END flag to suppress unwanted {END} on every line.
         */
        ushort location_id = FhGlobal.game_id is not FhGameId.FFX2LM
            ? header.x .id_location // The offset of the location is the same in X and X-2.
            : header.x2.lm_id_location;

        if (_cache_location.TryGetValue(location_id, out string? str_loc))
            return str_loc;

        Span<byte> dest = stackalloc byte[128];
        dest.Clear();

        if (FhGlobal.game_id is not FhGameId.FFX2LM) {
            byte*              ptr_location_name = FhCall.AtelGetSaveDicName.fnptr!(location_id, 0);
            ReadOnlySpan<byte> location_name     = new(ptr_location_name, int.MaxValue);

            int len_location = FhEncoding.decode(location_name, dest, flags: FhEncodingFlags.IMPLICIT_END);
            dest [ len_location ] = 0x00;

            return (_cache_location[location_id] = Encoding.UTF8.GetString(dest));
        }

        byte*      ptr_lm_location_suffix = stackalloc byte[0x40];
        Span<byte> lm_location_suffix     = new(ptr_lm_location_suffix, 0x40);

        FhCall.SaveDataGetLoc.fnptr!(0x4C2, ptr_lm_location_suffix);

        _pal_fill_template(lm_location_suffix, (byte)(location_id >> 1));

        int len_lm_location = FhEncoding.decode(lm_location_suffix, dest, flags: FhEncodingFlags.IMPLICIT_END);
        dest [ len_lm_location ] = 0x00;

        return (_cache_location[location_id] = $"{_cache_prefix_lm_loc} {Encoding.UTF8.GetString(dest)}");
    }

    /// <summary>
    ///     Returns the story chapter in the FF X-2 save with the given <paramref name="header"/>.
    /// </summary>
    private static unsafe string _pal_get_chapter(in SaveHeader header) {
        if (FhGlobal.game_id is not FhGameId.FFX2)
            return "";

        byte chapter_id = header.x2.chapter;

        if (_cache_chapter.TryGetValue(chapter_id, out string? str_loc))
            return str_loc;

        Span<byte> dest = stackalloc byte[0x80];
        dest.Clear();

        byte*      ptr_chapter = stackalloc byte[0x80];
        Span<byte> chapter     = new(ptr_chapter, 0x80);

        FhCall.SaveDataGetLoc.fnptr!(0x4D8, ptr_chapter);
        _pal_fill_template(chapter, chapter_id);

        int len_chapter = FhEncoding.decode(chapter, dest, flags: FhEncodingFlags.IMPLICIT_END);
        dest[ len_chapter ] = 0x00;

        return (_cache_chapter[chapter_id] = Encoding.UTF8.GetString(dest));
    }

    /// <summary>
    ///     Returns the story completion in the FF X-2 save with the given <paramref name="header"/>.
    /// </summary>
    private static unsafe string _pal_get_completion(in SaveHeader header) {
        if (FhGlobal.game_id is not FhGameId.FFX2)
            return "";

        byte completion_pct = header.x2.completion;

        if (_cache_completion.TryGetValue(completion_pct, out string? str_loc))
            return str_loc;

        Span<byte> dest = stackalloc byte[0x80];
        dest.Clear();

        byte*      ptr_completion = stackalloc byte[0x80];
        Span<byte> completion     = new(ptr_completion, 0x80);

        FhCall.SaveDataGetLoc.fnptr!(0x39A, ptr_completion);
        _pal_fill_template(completion, header.x2.completion);

        int len_completion = FhEncoding.decode(completion, dest, flags: FhEncodingFlags.IMPLICIT_END);
        dest[ len_completion ] = 0x00;

        return (_cache_completion[completion_pct] = Encoding.UTF8.GetString(dest));
    }

    /// <summary>
    ///     Returns the player's job in the FF X-2 LM save with the given <paramref name="header"/>.
    /// </summary>
    private static unsafe string _pal_get_lm_job(in SaveHeader header) {
        if (FhGlobal.game_id is not FhGameId.FFX2LM)
            return "";

        (byte, byte) key = (header.x2.lm_ply, header.x2.lm_job);

        if (_cache_lm_job.TryGetValue(key, out string? str_loc))
            return str_loc;

        Span<byte> dest = stackalloc byte[0x80];
        dest.Clear();

        byte*              ptr_lm_job = FFX2.FhCall.GetLastMissionJobName.fnptr!(header.x2.lm_ply, header.x2.lm_job);
        ReadOnlySpan<byte> lm_job     = new(ptr_lm_job, int.MaxValue);

        int len_lm_job = FhEncoding.decode(lm_job, dest, flags: FhEncodingFlags.IMPLICIT_END);
        dest [ len_lm_job ] = 0x00;

        return (_cache_lm_job[key] = Encoding.UTF8.GetString(dest));
    }

    /// <summary>
    ///     Returns the player's level in the FF X-2 LM save with the given <paramref name="header"/>.
    /// </summary>
    private static unsafe string _pal_get_lm_level(in SaveHeader header) {
        if (FhGlobal.game_id is not FhGameId.FFX2LM)
            return "";

        byte lm_level = header.x2.lm_ply_level;
        if (_cache_lm_level.TryGetValue(lm_level, out string? str_loc))
            return str_loc;

        return (_cache_lm_level[lm_level] = $"{_cache_prefix_lm_level} {lm_level}\0");
    }

    /// <summary>
    ///     For a given <paramref name="slot"/> in the given <paramref name="set"/>,
    ///     gets the full path of the corresponding save file.
    /// </summary>
    internal string pal_get_path_for_slot(int slot, string? set = null) {
        return Path.Join(
            _s_set_path_base,
            set ?? _set_active,
            pal_get_save_subfolder(),
            pal_get_save_name_for_slot(slot));
    }

    /// <summary>
    ///     Returns the first slot number free for saving.
    /// </summary>
    internal int pal_get_open_slot() {
        int target_slot = 1;
        while (_slots.Contains(target_slot)) { target_slot++; }

        _slots.Add(target_slot);
        return target_slot;
    }

    /* [fkelava 19/01/26 12:03]
     * The save manager is considered essential, as is any part of the runtime. We throw
     * if we cannot do basic file I/O; there is no point trying to handle it gracefully.
     */

    /// <summary>
    ///     Ensures the default save set exists.
    /// </summary>
    private void _sets_create_default() {
        _ = Directory.CreateDirectory(_s_set_path_default);
    }

    /// <summary>
    ///     Reads a save file and prepares its display data.
    /// </summary>
    private async Task _sets_index_single(int index) {
        string             save_file_path = pal_get_path_for_slot(index);
        ReadOnlySpan<char> slot_str       = save_file_path[ ^3 .. ];

        if (!int.TryParse(slot_str, out int slot))
            return;

        FhSaveUiData ui_data = _ui_data[index] ??= new();

        try {
            using MemoryMappedFile         save_file     = MemoryMappedFile.CreateFromFile(save_file_path, FileMode.Open, null, 0, MemoryMappedFileAccess.Read);
            using MemoryMappedViewAccessor save_accessor = save_file.CreateViewAccessor(0, pal_get_sz_save(), MemoryMappedFileAccess.Read);

            save_accessor.Read(0, out ui_data.header);

            switch (FhGlobal.game_id) {
                case FhGameId.FFX:                     save_accessor.Read(0x40, out ui_data.file.x ); break;
                case FhGameId.FFX2 or FhGameId.FFX2LM: save_accessor.Read(0x40, out ui_data.file.x2); break;

                default: break;
            }
        }
        catch { return; }

        FhSaveUiData.IconChr icon_chr = ui_data.icon_chr;
        icon_chr[0] = _pal_get_icon_chr(ui_data.header, 0);
        icon_chr[1] = _pal_get_icon_chr(ui_data.header, 1);
        icon_chr[2] = _pal_get_icon_chr(ui_data.header, 2);

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

        _slots.Add(slot);
    }

    /// <summary>
    ///     Indexes the active save set. This function must be called under lock.
    /// </summary>
    private void _sets_index() {
        _slots.Clear();

        string path_set_folder = Path.Join(
            _s_set_path_base,
            _set_active,
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

        Task[] tasks = new Task[SET_COUNT];

        for (int i = 0; i < SET_COUNT; i++) {
            tasks[i] = _sets_index_single(i);
        }

        Task.WaitAll(tasks);

        _sets[_set_active].has_autosave = File.Exists(pal_get_path_for_slot(0));
        _sets[_set_active].count        = _slots.Count;

        _ui_data.Sort(FhSaveComparers.compare_create_time);
    }

    /// <summary>
    ///     Registers basic information about a given save set directory.
    /// </summary>
    /// <param name="set_dir_path">The absolute path to the save set directory.</param>
    private async Task _sets_query_single(string set_dir_path) {
        string set_name        = Path.GetFileName(set_dir_path);
        string set_subdir_path = Path.Join       (set_dir_path, pal_get_save_subfolder());

        _ = Directory.CreateDirectory(set_subdir_path);

        int count = 0;
        for (int i = 0; i < SET_COUNT; i++) {
            if (File.Exists(pal_get_path_for_slot(i, set_name))) count++;
        }

        _sets[set_name] = new FhSaveSet() {
            name         = set_name,
            has_autosave = File.Exists(pal_get_path_for_slot(0, set_name)),
            count        = count
        };
    }

    /// <summary>
    ///     Queries the disk for available save sets.
    /// </summary>
    private void _sets_query() {
        _sets.Clear();

        string[] sets  = [ .. Directory.EnumerateDirectories(_s_set_path_base) ];
        Task  [] tasks = new Task[sets.Length];

        for (int i = 0; i < sets.Length; i++) {
            tasks[i] = _sets_query_single(sets[i]);
        }

        Task.WaitAll(tasks);

        /* [fkelava 19/01/26 14:50]
         * The default set is forcibly regenerated if it does not exist,
         * and we fall back to it if the active set was torched.
         */

        if (!_sets.TryGetValue(_set_active, out _)) {
            FhInternal.Log.Warning($"Active set {_set_active} was deleted; falling back to default.");

            if (!_sets.TryGetValue(_s_set_name_default, out _)) {
                FhInternal.Log.Error("Default set was deleted; forcibly re-generating.");

                _sets_create_default();
                _sets_query();
            }

            _set_active = _s_set_name_default;
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

        _sets_query();

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

        _set_active = set_name;
        _sets_index();

        _pal_cache_tex_load();

        Interlocked.Decrement(ref _lock);
    }

    /// <summaryRetrieve the active set.</summary>
    public FhSaveSet sets_get_active() => _sets[_set_active];

    /// <summaryRetrieve a specific set.</summary>
    public bool sets_get(string set_name, [NotNullWhen(true)] out FhSaveSet? set) {
        return _sets.TryGetValue(set_name, out set);
    }

    public bool sets_get_ui_data(int index, [NotNullWhen(true)] out FhSaveUiData? data) {
        data = default;
        if (index >= _sets[_set_active].count)
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

        switch (impl.get_system_mode()) {
            case FhSaveSystemMode.NULL: return false;
            case FhSaveSystemMode.LOAD: impl.load     (slot); break;
            case FhSaveSystemMode.ALBD: impl.copy_albd(slot); break;
            case FhSaveSystemMode.SAVE: impl.save     (slot); break;
        }

        return true;
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
