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
 * This file contains the "PAL", a set of functions that return common save attributes in a game-agnostic manner.
 * For the other half, see `save_mgr`.
 */

public sealed partial class FhSaves {

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

    private unsafe static SaveDataManager* _pal_get_save_mgr() => *(SaveDataManager**)
        (FhEnvironment.BaseAddr + FhUtil.select(0x8E81F4, 0x9ECABC, 0x9ECABC));

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

    /// <summary>
    ///     Gets the address of the temporary buffer the save/load system uses for the pending save game.
    /// </summary>
    internal unsafe static byte* pal_get_addr_save() {
        return FhUtil.ptr_at<byte>(FhUtil.select(0x1197F70, 0xF9D500, 0xF9D500));
    }

    /// <summary>
    ///     Gets the size of a complete save game for the currently active game.
    /// </summary>
    internal static int pal_get_sz_save() {
        return FhUtil.select(0x6900, 0x166A0, 0x166A0);
    }

    /// <summary>
    ///     Gets the temporary buffer the save/load system uses for the pending save game.
    /// </summary>
    internal unsafe static Span<byte> pal_get_buf_save() {
        return new(pal_get_addr_save(), pal_get_sz_save());
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
        return FhUtil.select("ffx_???", "ffx2_???", "ffx2_???");
    }

    /* [fkelava 18/11/25 21:27]
     * Both Iggy and ImGui take null-terminated UTF-8 strings. Neither FhEncoding nor Encoding.UTF8
     * emit terminating null bytes, so we take care to do so manually in the PAL.
     */

    /// <summary>
    ///     Prepares runtime-constant strings used by other PAL methods.
    /// </summary>
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
     *
     * Some (icon_{map|chr}) are substantially modified and improved over the base game.
     */

    /// <summary>
    ///     Returns whether the map with the given ID has multiple map icons.
    /// </summary>
    private static bool _pal_map_has_multiple_icons(int id_map) {
        if (FhGlobal.game_id is not FhGameId.FFX)
            return false;

        ReadOnlySpan<int> maps = [ 31, 129, 216, 244, 245, 246, 247, 257, 284, 348 ];
        return maps.Contains(id_map) || maps.Contains(id_map - 1000);
    }

    /// <summary>
    ///     Returns the map icon for the save with the given <paramref name="header"/>.
    /// </summary>
    private static FhTexture _pal_get_icon_map(in SaveHeader header) {
        string map_dir  = FhGlobal.game_id is FhGameId.FFX
            ? DIR_MAP_ICON_X
            : DIR_MAP_ICON_X2;

        if (FhGlobal.game_id is FhGameId.FFX2LM) {
            int id_lm_icon = ((int.Clamp(header.x2.lm_id_location / 2, 0, 80) - 1) / 20) + 1;

            if (_cache_icon_map.TryGetValue(id_lm_icon, out FhTexture? lm_icon))
                return lm_icon;

            return (_cache_icon_map[id_lm_icon] = new FhTexture($"{map_dir}/logo_j_lm.png", FhTextureType.GAME));
        }

        int id_logo_icon = FhUtil.select(0, 23, 23);
        int id_map       = FhGlobal.game_id is FhGameId.FFX
            ? header.x .id_location
            : header.x2.id_map_icon;

        id_map = FhCall.fix_mappic.fnptr!((ushort)id_map);

        if (_cache_icon_map.TryGetValue(id_map, out FhTexture? icon_texture))
            return icon_texture;

        if (id_map == id_logo_icon && FhCall.isNeedShowJapanLogo.fnptr!() != 0) {
            return (_cache_icon_map[id_map] = new FhTexture($"{map_dir}/logo_j.png", FhTextureType.GAME));
        }

        int id_icon = id_map;

        string suffix = FhGlobal.game_id switch {
            FhGameId.FFX when _pal_map_has_multiple_icons(id_icon) => id_icon < 1000 ? "_0" : "_1",
            FhGameId.FFX2                                          => id_icon < 1000 ? "_0" : "_1",

            _ => string.Empty,
        };

        if (suffix != string.Empty && id_icon > 1000) {
            id_icon -= 1000;
        }

        string icon_name = $"{id_icon}{suffix}";

        return (_cache_icon_map[id_map] = new FhTexture($"{map_dir}/{icon_name}.png", FhTextureType.GAME));
    }

    /// <summary>
    ///     Returns the icon for a character in the save
    ///     with the given <paramref name="header"/>.
    /// </summary>
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

        return (_cache_play_time[t_sec] = $"{_cache_prefix_play_time}  {t_min / 60:D3}:{t_min % 60:D2}:{t_sec % 60:D2}");
    }

    /// <summary>
    ///     Returns the player character's name in the save with the given <paramref name="header"/>.
    /// </summary>
    private static unsafe string _pal_get_player_name(in SaveHeader header) {
        string? str_loc;

        Span<byte> dest = stackalloc byte[0x10];
        dest.Clear();

        if (FhGlobal.game_id is FhGameId.FFX) {
            if (_cache_player_name.TryGetValue(0, out str_loc))
                return str_loc;

            _ = FhEncoding.decode(header.x.player_name, dest, header.x.lang_id, flags: FhEncodingFlags.IMPLICIT_END);
            return (_cache_player_name[0] = Encoding.UTF8.GetString(dest));
        }

        byte*              ptr_player_name_encoded = stackalloc byte[0x10];
        ReadOnlySpan<byte> player_name_encoded     = new(ptr_player_name_encoded, 0x10);

        FhCall.SaveDataGetLoc.fnptr!(0xDD + header.x2.lm_ply, ptr_player_name_encoded);

        _ = FhEncoding.decode(player_name_encoded, dest, flags: FhEncodingFlags.IMPLICIT_END);
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

        Span<byte> dest = stackalloc byte[0x80];
        dest.Clear();

        if (FhGlobal.game_id is not FhGameId.FFX2LM) {
            byte*              ptr_location_name = FhCall.AtelGetSaveDicName.fnptr!(location_id, 0);
            ReadOnlySpan<byte> location_name     = new(ptr_location_name, int.MaxValue);

            _ = FhEncoding.decode(location_name, dest, flags: FhEncodingFlags.IMPLICIT_END);
            return (_cache_location[location_id] = Encoding.UTF8.GetString(dest));
        }

        byte*      ptr_lm_location_suffix = stackalloc byte[0x40];
        Span<byte> lm_location_suffix     = new(ptr_lm_location_suffix, 0x40);

        FhCall.SaveDataGetLoc.fnptr!(0x4C2, ptr_lm_location_suffix);

        _pal_fill_template(lm_location_suffix, (byte)(location_id >> 1));

        _ = FhEncoding.decode(lm_location_suffix, dest, flags: FhEncodingFlags.IMPLICIT_END);
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

        _ = FhEncoding.decode(chapter, dest, flags: FhEncodingFlags.IMPLICIT_END);
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

        _ = FhEncoding.decode(completion, dest, flags: FhEncodingFlags.IMPLICIT_END);
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

        _ = FhEncoding.decode(lm_job, dest, flags: FhEncodingFlags.IMPLICIT_END);
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

        return (_cache_lm_level[lm_level] = $"{_cache_prefix_lm_level} {lm_level}");
    }

    /* [fkelava 28/10/26 14:42]
     * https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.comparer-1.compare?view=net-10.0#notes-to-implementers
     * > Comparing null with any reference type is allowed and does not generate an exception.
     * > A null reference is considered to be less than any reference that is not null.
     */

    /// <summary>Sorts save games by their creation time.</summary>
    private static int _pal_sort_create_time(FhSaveUiData? x, FhSaveUiData? y) {
        if (ReferenceEquals(x, y)) return  0;
        if (x == null)             return -1;
        if (y == null)             return  1;

        if (x.valid && !y.valid) return -1;
        if (y.valid && !x.valid) return  1;

        if (x.slot == 0) return -1; // The autosave must precede any other save.
        if (y.slot == 0) return  1;

        if      (x.create_time > y.create_time) return -1;
        else if (x.create_time < y.create_time) return  1;

        return x.slot - y.slot;
    }

    /// <summary>Sorts save games by their slot index.</summary>
    private static int _pal_sort_slot(FhSaveUiData? x, FhSaveUiData? y) {
        if (ReferenceEquals(x, y)) return  0;
        if (x == null)             return -1;
        if (y == null)             return  1;

        if (x.valid && !y.valid) return -1;
        if (y.valid && !x.valid) return  1;

        return x.slot - y.slot;
    }

    /// <summary>
    ///     For a given <paramref name="slot"/> in the given <paramref name="set"/>,
    ///     gets the full path of the corresponding save file.
    /// </summary>
    internal string pal_get_path_for_slot(int slot, string? set = null) {
        return Path.Join(
            _sets_path_base,
            set ?? _sets_name_active,
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

}
