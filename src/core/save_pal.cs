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

    private const string DIR_MAP_ICON_X  = "/FFX_Data/GameData/PS3Data/savedataicons";
    private const string DIR_MAP_ICON_X2 = "/FFX-2_Data/GameData/PS3Data/savedataicons";
    private const string DIR_CHR_ICON_X  = "/FFX_Data/GameData/PS3Data/menu/D3D11";
    private const string DIR_CHR_ICON_X2 = "/FFX-2_Data/GameData/PS3Data/menu/face_data/D3D11";

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
    ///     Returns the icon for a character in the save with the given <paramref name="header"/>.
    /// </summary>
    private static FhTexture _pal_get_icon_chr(
        in SaveHeader header,
        in SaveData   data,
           int        index
    ) {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(index, 2, nameof(index));

        if (FhGlobal.game_id is FhGameId.FFX) {
            /* [fkelava 31/10/26 00:35]
             * In FFX the player and aeon icons are grouped into atlases, _except_ for Seymour.
             * So we map the character's ID to one of these three textures until we devise FhTextureClip.
             */
            byte chr_id = data.x.party_frontline[index];
            byte key_id = chr_id switch {
                <= FFX.Ids.PlySaveId.PC_RIKKU   => 0,
                   FFX.Ids.PlySaveId.PC_SEYMOUR => 1,
                <= FFX.Ids.PlySaveId.PC_MAGUS3  => 2,
                _                               => 0
            };

            if (_cache_icon_chr.TryGetValue((key_id, 0), out FhTexture? tex_x))
                return tex_x;

            return key_id switch {
                0 => (_cache_icon_chr[(key_id, 0)] = new FhTexture($"{DIR_CHR_ICON_X}/face_ply.dds.phyre",   FhTextureType.GAME_PHYRE_2D)),
                1 => (_cache_icon_chr[(key_id, 0)] = new FhTexture($"{DIR_CHR_ICON_X2}/mface_182.dds.phyre", FhTextureType.GAME_PHYRE_2D)),
                2 => (_cache_icon_chr[(key_id, 0)] = new FhTexture($"{DIR_CHR_ICON_X}/face_smn.dds.phyre",   FhTextureType.GAME_PHYRE_2D)),
                _ => throw new UnreachableException()
            };
        }

        byte chr = data.x2.extend_party[index];

        if (chr >= FFX2.Ids.PlySaveId.PC_CREATURE1 && chr <= FFX2.Ids.PlySaveId.PC_CREATURE8) {
            int  model_id = data.x2.ply_saves[chr].creature_data.creature;
            byte icon_id  = pal_remap_creature_icon(model_id);

            (byte, byte) chr_key = (chr, icon_id);

            if (_cache_icon_chr.TryGetValue(chr_key, out FhTexture? tex_mon))
                return tex_mon;

            return (_cache_icon_chr[chr_key] = new FhTexture($"{DIR_CHR_ICON_X2}/mface_{icon_id:D3}.dds.phyre", FhTextureType.GAME_PHYRE_2D));
        }

        bool is_lm = FhGlobal.game_id is FhGameId.FFX2LM;

        byte ply_id = is_lm
            ? header.x2.lm_ply
            : data.x2.party[index];

        byte job_id = is_lm
            ? header.x2.lm_job
            : header.x2.ply_jobs[data.x2.party[index]];

        (byte, byte) ply_key = (ply_id, job_id);

        if (_cache_icon_chr.TryGetValue(ply_key, out FhTexture? tex))
            return tex;

        if (job_id == 0x21) {
            return (_cache_icon_chr[ply_key] = new FhTexture($"{DIR_CHR_ICON_X2}/mface_147.dds.phyre", FhTextureType.GAME_PHYRE_2D)); // She-Goon
        }

        string ply_name = ply_id switch {
            1 => "rikku",
            2 => "paine",
            _ => "yuna",
        };

        // Remaps Rikku and Paine's job IDs to correctly match the icon's filenames
        byte job = job_id switch {
            24 or 25       => 12, // Trainer
            26 or 27       => 14, // Mascot
            28             => 16, // Psychic
            29 or 30 or 31 => 17, // Festivalist
            32             => 18, // Freelancer

            _  => job_id,
        };

        /* We have to remap Paine's "default" icon to her "Warrior" icon
         * because her default icon is 128x128, not 256x256.
         */
        if (ply_id == FFX2.Ids.PlySaveId.PC_PAINE && job_id == 0x0) {
            job = 0x4;
        }

        return (_cache_icon_chr[ply_key] = new FhTexture($"{DIR_CHR_ICON_X2}/{ply_name}face_{job:D3}.dds.phyre", FhTextureType.GAME_PHYRE_2D));
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

    /// <summary>
    ///     Returns the icon ID for a given FF X-2 creature's model ID.
    /// </summary>
    private static byte pal_remap_creature_icon(int model_id) {
        return model_id switch {
            0x1001 =>   1, // Sallet
            0x1002 =>   2, // Bicocette
            0x1003 =>   3, // Barbuta
            0x1004 =>   4, // Armet
            0x1005 =>   5, // Bascinet
            0x1126 =>   6, // Heavy Sallet
            0x1006 =>   7, // Zurvan
            0x1007 =>   8, // Varan
            0x1008 =>   9, // Arast
            0x110F =>  10, // Elder Zurvan
            0x1009 =>  11, // Coyote
            0x100A =>  12, // Wild Wolf
            0x100B =>  13, // Killer Hound
            0x100C =>  14, // White Fang
            0x100D =>  15, // Canis Major
            0x100E =>  16, // Lupus
            0x100F =>  17, // Tindalos
            0x1010 =>  18, // Flan Azul
            0x1011 =>  19, // Flan Amarillo
            0x1012 =>  20, // Flan Palido
            0x1013 =>  21, // Flan Blanco
            0x1014 =>  22, // Flan Rojo
            0x1015 =>  23, // Flan Azabache
            0x1016 =>  24, // Gecko
            0x1017 =>  25, // Agama
            0x1018 =>  26, // Skink
            0x1019 =>  27, // Anole
            0x101A =>  28, // Archaeothyris
            0x101B =>  29, // Lacerta
            0x101C =>  30, // Divebeak
            0x101D =>  31, // Peregrine
            0x101E =>  32, // Aquila
            0x101F =>  33, // Death Dauber
            0x1020 =>  34, // Assassin Bee
            0x1021 =>  35, // Aculeate
            0x1022 =>  36, // Vespa
            0x1111 =>  37, // Wasp Queen
            0x1023 =>  38, // Ahriman
            0x1024 =>  39, // Fly Eye
            0x1025 =>  40, // Vertigo
            0x1026 =>  41, // Grim Gaze
            0x1027 =>  42, // Purpurea
            0x1028 =>  43, // Leucophylla
            0x1029 =>  44, // Cephalotus
            0x102A =>  45, // Shantak
            0x102B =>  46, // Zu
            0x10E7 =>  47, // Rukh
            0x102E =>  48, // YSLS-99
            0x102F =>  49, // YSLS-Zero
            0x1030 =>  50, // Haunt
            0x10E3 =>  51, // Wight
            0x1031 =>  52, // Phantom
            0x10E4 =>  53, // Lich
            0x1113 =>  54, // Great Haunt
            0x1032 =>  55, // Nashorn
            0x1033 =>  56, // Quadricorn
            0x1034 =>  57, // Balivarha
            0x1035 =>  58, // Lesser Drake
            0x1036 =>  59, // Spine Drake
            0x1037 =>  60, // Bolt Drake
            0x1038 =>  61, // Greater Drake
            0x1039 =>  62, // Elder Drake
            0x103A =>  63, // Malboro
            0x103B =>  64, // Great Malboro
            0x103C =>  65, // Gigas
            0x103D =>  66, // Gug
            0x103E =>  67, // Hrimthurs
            0x103F =>  68, // Yellow Elemental
            0x1040 =>  69, // White Elemental
            0x1041 =>  70, // Red Elemental
            0x1042 =>  71, // Gold Elemental
            0x1043 =>  72, // Blue Elemental
            0x1044 =>  73, // Dark Elemental
            0x1045 =>  74, // Black Elemental
            0x1046 =>  75, // Takouba
            0x1047 =>  76, // Barong
            0x1114 =>  77, // King Takouba
            0x1048 =>  78, // Behemoth
            0x1049 =>  79, // Humbaba
            0x104A =>  80, // Protochimera
            0x104B =>  81, // Rhyos
            0x104C =>  82, // Coeurl
            0x104D =>  83, // Queen Coeurl
            0x104E =>  84, // Machina Ranger
            0x104F =>  85, // Machina Hunter
            0x1050 =>  86, // Machina Soldier
            0x10DA =>  87, // Machina Leader
            0x1051 =>  88, // Tomb
            0x10DE =>  89, // Monolith
            0x10DF =>  90, // Dolmen
            0x1116 =>  91, // Epitaph
            0x1052 =>  92, // Scout Machina
            0x1053 =>  93, // Guard Machina
            0x1054 =>  94, // Assault Machina
            0x10DB =>  95, // Killer Machina
            0x1055 =>  96, // Iron Giant
            0x1056 or
            0x1057 =>  97, // Gemini
            0x1058 =>  98, // Kukulcan
            0x1059 =>  99, // Gucumatz
            0x10EF => 100, // Chac
            0x105A => 101, // Adamantoise
            0x10E5 => 102, // Adamantortoise
            0x105B => 103, // Daeva
            0x10E0 => 104, // Aeshma
            0x10E1 => 105, // Aka Manah
            0x105C => 106, // Ochu
            0x105D => 107, // Flailing Ochu
            0x10EE => 108, // Drowsy Ochu
            0x105E => 109, // YAC-13
            0x105F => 110, // YAC-62
            0x1060 => 111, // Bomb
            0x1061 => 112, // Detonator
            0x1062 => 113, // Volcano
            0x1063 => 114, // YAU-28
            0x1064 => 115, // YAU-71
            0x106A or
            0x106B => 116, // Cactuar
            0x10ED => 117, // Jumbo Cactuar
            0x106C => 118, // Stalwart
            0x10E6 => 119, // Ironside
            0x106D => 120, // Bully Cap
            0x106E => 121, // Mycotoxin
            0x106F => 122, // Mushroom Cloud
            0x1115 => 123, // Big Bully Cap
            0x1070 => 124, // Pairika
            0x1071 => 125, // Jahi
            0x10E2 => 126, // Taromaiti
            0x1072 => 127, // Tonberry
            0x1073 => 128, // Mega Tonberry
            0x1078 => 129, // Claret Dragon
            0x1079 => 130, // Zalamander
            0x107A => 131, // Flame Dragon
            0x107B => 132, // Recoil
            0x107C => 133, // Flak Python
            0x107D => 134, // Viper Sniper
            0x112E => 135, // Battlesnake
            0x107E => 136, // Creeper
            0x107F => 137, // Hug Bug
            0x10D9 => 138, // Hexapod
            0x1080 => 139, // King VERMIN!
            0x112F => 140, // Insect Matriarch
            0x1130 => 141, // Critical Bug
            0x1084 => 142, // Boris
            0x10DD => 143, // Aranea
            0x1085 => 144, // Goon
            0x1086 => 145, // Mr. Goon
            0x1087 => 146, // Dr. Goon
            0x1088 => 147, // She-Goon
            0x1089 => 148, // Ms. Goon
            0x108A => 149, // Fem-Goon
            0x1090 => 150, // Sahagin
            0x10DC => 151, // Sahagin Prince
            0x1112 => 152, // Sahagin Chief
            0x1091 => 153, // Haizhe
            0x1092 => 154, // Deep Haizhe
            0x1093 => 155, // Xiphactinus
            0x1094 => 156, // Dinictus
            0x1095 => 157, // Tentacles
            0x1096 => 158, // Ultima Weapon
            0x1097 => 159, // Omega Weapon
            0x1098 => 160, // Paragon
            0x1099 => 161, // Chocobo Eater
            0x109A => 162, // Anything Eater
            0x109B => 163, // Shell Shocker
            0x10E8 => 164, // Concheror
            0x109C => 165, // Pop Fry
            0x10EB => 166, // Machina Striker
            0x109E => 167, // Amorphous Gel
            0x109F => 168, // Protean Gel
            0x10A0 => 169, // Machina Panzer
            0x10A2 => 170, // Guardian Beast
            0x10A3 => 171, // Azi Dahaka
            0x10BC or
            0x1153 => 172, // Chocobo
            0x1081 => 173, // Ormi
            0x1082 => 174, // Logos
            0x1083 => 175, // Leblanc
            0x1134 => 176, // Brother
            0x1135 => 177, // Buddy
            0x1137 => 178, // Auron
            0x1139 => 179, // Kimahri
            0x113B => 180, // Lulu
            0x113D => 181, // Tidus
            0x113F => 182, // Seymour
            0x1141 => 183, // Baralai
            0x1142 => 184, // Nooj
            0x1143 => 185, // Gippal
            0x1144 => 186, // Garik
            0x1145 => 187, // Yaibal
            0x1146 => 188, // Lucil
            0x1147 => 189, // Elma
            0x1148 => 190, // Trema
            0x1149 => 191, // Shinra
            _      =>   0  // None
        };
    }
}
