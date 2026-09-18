// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

/* [fkelava 08/11/25 21:45]
 * These are direct mappings of game structures.
 * We reuse them for simplicity, ignoring the stored creation date/time logic in favor of OS API calls.
 */

/// <summary>
///     Represents the possible states of the game's default save data manager.
/// </summary>
public enum FhSaveSystemState : int {
    IDLE           = 0x00,
    SAVE           = 0x01,
    SAVE_SUCCEED   = 0x02,
    LOAD           = 0x03,
    LOAD_SUCCEED   = 0x04,
    DELETE         = 0x05, // Effectively unused on PC
    DELETE_SUCCEED = 0x06  // Effectively unused on PC
}

/// <summary>
///     Represents the possible states of the dialog box displayed in the save/load screen.
/// </summary>
public enum FhSaveDialogState {
    CLOSED = 0x00,
    UNK5   = 0x05
}

/// <summary>
///     Represents the possible states of the game's save data screen.
/// </summary>
public enum FhSaveScreenState {
    CLOSED  = 0x00,
    OPENING = 0x01,
    OPEN    = 0x02,
    UNK3    = 0x03,
    UNK4    = 0x04
}

/// <summary>
///     The game's default save data manager structure for FF X.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 0x488)]
public unsafe struct FhSaveDataManager {
    public int                    save_enabled;
    public int                    cb_result;
    public FhSaveSystemState      state;
    public int                    __0xC;
    public InlineArray64 <byte>   game_name;
    public InlineArray128<byte>   description;
    public InlineArray512<byte>   description_detailed;
    public InlineArray64 <byte>   path_icon_1;
    public InlineArray64 <byte>   path_icon_2;
    public byte*                  ref_buffer;
    public int                    ref_buffer_size;
    public int                    operation_canceled;
}

/// <summary>
///     The game's default save data manager structure for FF X-2.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 0x4B0)]
public unsafe struct FhSaveDataManager2 {
    public FhSaveSystemState      state;
    public int                    save_enabled;
    public int                    cb_result;
    public InlineArray64 <byte>   game_name;
    public InlineArray128<byte>   description;
    public InlineArray512<byte>   description_detailed;
    public InlineArray64 <byte>   path_icon_1;
    public InlineArray64 <byte>   path_icon_2;
    public byte*                  ref_buffer;
    public int                    ref_buffer_size;
    public int                    operation_canceled;
}

/// <summary>
///     The save game header for FF X.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct FhSaveHeader {
    [InlineArray(0x20)]
    public struct FhSavePlayerName {
        private byte _b;
    }

    [InlineArray(0x7)]
    public struct Formation {
        private byte _e0;
    }

    public uint             _0x00;
    public byte             _0x04;
    public Formation        formation;
    public FhLangId         lang_id;
    public byte             _0x0D;
    public ushort           _0x0E;
    public uint             playtime_secs;
    public uint             gil;
    public ushort           id_location;
    public ushort           _0x1a;
    public ushort           _0x1c;
    public ushort           _0x1e;
    public FhSavePlayerName player_name;
};

/// <summary>
///     The save game header for FF X-2.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct FhSaveHeader2 {
    [InlineArray(0x3)]
    public struct Party {
        private byte _e0;
    }

    public uint     _0x00;
    public byte     _0x04;
    public Party    ply;
    public Party    ply_levels;
    public byte     chapter;
    public byte     completion;
    public Party    ply_jobs;
    public uint     playtime_secs;
    public uint     gil;
    public ushort   id_location;
    public ushort   _0x1A;
    public uint     _0x1C;
    public byte     times_played;
    public byte     lm_ply;
    public byte     lm_ply_level;
    public byte     lm_job;
    public byte     lm_job_level;
    public byte     lm_id_location;
    public byte     lm_retry;
    public byte     _0x27;
    public FhLangId lang_id;
    public byte     _0x29;
    public ushort   id_map_icon;
    public byte     _0x2C;

    public bool is_new_game_plus => times_played > 1;
}

/// <summary>
///     Contains the fields the game shows as part of its standard save game display.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct FhSaveDisplayData {

    /* [fkelava 19/01/26 13:14]
     * An array of these of size DEFAULT_SET_SIZE is allocated by the save UI module on boot.
     * These instances are continually reused. To prevent garbage from being displayed when a slot
     * occupied in the previous set becomes empty, the save manager module (un)sets 'valid'.
     *
     * These inline arrays are in reality UTF-8 strings, since both Iggy
     * and ImGui accept them as input. The sizes are taken from the base game.
     */

    public int    slot;
    public string slot_str;

    public DateTimeOffset create_time;

    public InlineArray64 <byte> header;
    public InlineArray128<byte> location;
    public InlineArray128<byte> play_time;
    public InlineArray32 <byte> player_name;
    public InlineArray16 <byte> icon_map;
    public InlineArray128<byte> chapter;
    public InlineArray128<byte> completion;
    public InlineArray64 <byte> lm_level;
    public InlineArray64 <byte> lm_job;
}

/* [fkelava 10/01/26 16:53]
 * The save PAL, being a binding to implementation details of each game,
 * is virtually illegible without consulting the original method bodies.
 *
 * For your convenience, most PAL methods are annotated with a source line you can look up.
 */

/// <summary>
///     Abstracts the game's save data system.
/// </summary>
internal unsafe static class FhSavePal {

    /* [fkelava 01/01/26 15:04]
     * TODO:
     * When FhCall is improved, this can be drastically simplified and
     * all the delegates and pal_addr_* functions can be removed.
     */

    internal const string DEFAULT_SET_NAME = "default";

    internal static FhSaveDialogState pal_get_dialog_state()                        => FhUtil.get_at<FhSaveDialogState>(pal_addr_dialog_state());
    internal static void              pal_set_dialog_state(FhSaveDialogState value) => FhUtil.set_at(pal_addr_dialog_state(), value);

    internal static FhSaveScreenState pal_get_screen_state()                        => FhUtil.get_at<FhSaveScreenState>(pal_addr_screen_state());
    internal static void              pal_set_screen_state(FhSaveScreenState value) => FhUtil.set_at(pal_addr_screen_state(), value);

    /// <summary>
    ///     Sets the state of the in-game save manager to the given <paramref name="state"/>.
    /// </summary>
    internal static void pal_set_system_state(FhSaveSystemState state) {
        FhSaveDataManager*  mgr_x  = *(FhSaveDataManager **)pal_addr_save_mgr();
        FhSaveDataManager2* mgr_x2 = *(FhSaveDataManager2**)pal_addr_save_mgr();

        if (FhGlobal.game_id is FhGameId.FFX) {
            mgr_x->state = state;
            return;
        }

        mgr_x2->state = state;
    }

    /// <summary>
    ///     Signals to the game whether a save/load operation has been canceled by the user.
    /// </summary>
    internal static void pal_set_cancel_state(int cancel_state) {
        FhSaveDataManager*  mgr_x  = *(FhSaveDataManager **)pal_addr_save_mgr();
        FhSaveDataManager2* mgr_x2 = *(FhSaveDataManager2**)pal_addr_save_mgr();

        if (FhGlobal.game_id is FhGameId.FFX) {
            mgr_x->operation_canceled = cancel_state;
            return;
        }

        mgr_x2->operation_canceled = cancel_state;
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
    internal static void pal_fill_template(Span<byte> template, int fill) {
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

    /* [fkelava 18/11/25 21:27]
     * Both Iggy and ImGui take null-terminated UTF-8 strings. Neither FhEncoding nor Encoding.UTF8
     * emit terminating null bytes, so we take care to do so manually in the PAL.
     */

    /* [fkelava 13/11/25 21:59]
     * FFX.exe  +2F0DA0, L66-80   (X)
     * FFX-2.exe+11DC50, L83-97   (X-2)
     * FFX-2.exe+11DC50, L161-165 (X-2 LM)
     */

    /// <summary>
    ///     Writes the icon ID of the current map in the save with the given
    ///     <paramref name="header"/> to <paramref name="dest"/> as a UTF-8 string.
    /// </summary>
    internal static void pal_get_icon_map(in ReadOnlySpan<byte> header, in Span<byte> dest) {
        bool not_lm      = FhGlobal.game_id is not FhGameId.FFX2LM;
        int  id_icon_map = not_lm
            ? FhCall.fix_mappic.fnptr!(BinaryPrimitives.ReadUInt16LittleEndian(header[ pal_header_offset_locationid() .. ]))
            : ((int.Clamp(header[0x25] >> 1, 0, 0x50) - 1) / 0x14) + 1;

        if (not_lm && id_icon_map == pal_id_map_icon_clear() && FhCall.isNeedShowJapanLogo.fnptr!() != 0) {
            id_icon_map = 999;
        }

        string str_icon_map = id_icon_map < 0x3E9
            ? $"m{id_icon_map}"
            : $"m{id_icon_map - 1000}_l";

        int len_icon_map = Encoding.UTF8.GetBytes(str_icon_map, dest);
        dest [ len_icon_map ] = 0x00;
    }

    /* [fkelava 13/11/25 22:08]
     * FFX-2.exe+11DC50, L66-69 (X-2)
     */

    /// <summary>
    ///     Writes the story chapter in the FF X-2 save with the given
    ///     <paramref name="header_bytes"/> to <paramref name="dest"/> as a UTF-8 string.
    /// </summary>
    internal static void pal_get_chapter(in ReadOnlySpan<byte> header_bytes, in Span<byte> dest) {
        if (FhGlobal.game_id is not FhGameId.FFX2) {
            dest[0] = 0x00;
            return;
        }

        FhSaveHeader2 header = MemoryMarshal.Read<FhSaveHeader2>(header_bytes);

        byte*      ptr_chapter_encoded = FhUtil.ptr_at<byte>(0x9ED648);
        Span<byte> chapter_encoded     = new(ptr_chapter_encoded, 0x80);

        FhCall.SaveDataGetLoc.fnptr!(0x4D8, ptr_chapter_encoded);
        pal_fill_template(chapter_encoded, header.chapter);

        int len_chapter = FhEncoding.decode(chapter_encoded, dest, flags: FhEncodingFlags.IMPLICIT_END);
        dest[ len_chapter ] = 0x00;
    }

    /* [fkelava 13/11/25 22:08]
     * FFX-2.exe+11DC50, L70-73 (X-2)
     */

    /// <summary>
    ///     Writes the story completion in the FF X-2 save with the given
    ///     <paramref name="header_bytes"/> to <paramref name="dest"/> as a UTF-8 string.
    /// </summary>
    internal static void pal_get_completion(in ReadOnlySpan<byte> header_bytes, in Span<byte> dest) {
        if (FhGlobal.game_id is not FhGameId.FFX2) {
            dest[0] = 0x00;
            return;
        }

        FhSaveHeader2 header = MemoryMarshal.Read<FhSaveHeader2>(header_bytes);

        byte*      ptr_completion_encoded = FhUtil.ptr_at<byte>(0x9ED7C8);
        Span<byte> completion_encoded     = new(ptr_completion_encoded, 0x80);

        FhCall.SaveDataGetLoc.fnptr!(0x39A, ptr_completion_encoded);
        pal_fill_template(completion_encoded, header.completion);

        int len_completion = FhEncoding.decode(completion_encoded, dest, flags: FhEncodingFlags.IMPLICIT_END);
        dest [ len_completion ] = 0x00;
    }

    /* [fkelava 13/11/25 22:08]
     * FFX.exe  +2F0DA0, L57-61   (X)
     * FFX-2.exe+11DC50, L74-78   (X-2)
     * FFX-2.exe+11DC50, L139-144 (X-2 LM)
     */

    /// <summary>
    ///     Writes the total play time in the save with the given
    ///     <paramref name="header"/> to <paramref name="dest"/> as a UTF-8 string.
    /// </summary>
    internal static void pal_get_playtime(in ReadOnlySpan<byte> header, in Span<byte> dest) {
        uint playtime_secs = BinaryPrimitives.ReadUInt32LittleEndian(header [ 0x10 .. ]);
        uint playtime_mins = playtime_secs / 60;

        byte*              ptr_playtime_prefix_encoded = FhUtil.ptr_at<byte>(pal_addr_buf_playtime_prefix_encoded());
        ReadOnlySpan<byte> playtime_prefix_encoded     = new(ptr_playtime_prefix_encoded, int.MaxValue);

        FhCall.SaveDataGetLoc.fnptr!(pal_id_playtime_prefix_SaveDataGetLoc(), ptr_playtime_prefix_encoded);

        int len_playtime_prefix = FhEncoding.decode(playtime_prefix_encoded, dest, flags: FhEncodingFlags.IMPLICIT_END);
        int len_playtime        = len_playtime_prefix + Encoding.UTF8.GetBytes($"  {playtime_mins / 60:D3}:{playtime_mins % 60:D2}:{playtime_secs % 60:D2}", dest[ len_playtime_prefix .. ]);

        dest [ len_playtime ] = 0x00;
    }

    /* [fkelava 13/11/25 22:08]
     * FFX.exe  +2F0DA0, L55      (X)
     * FFX-2.exe+11DC50, L64-65   (X-2)
     * FFX-2.exe+11DC50, L122-132 (X-2 LM)
     */

    /// <summary>
    ///     Writes the player character's name in the save with the given
    ///     <paramref name="header_bytes"/> to <paramref name="dest"/> as a UTF-8 string.
    /// </summary>
    internal static void pal_get_player_name(in ReadOnlySpan<byte> header_bytes, in Span<byte> dest) {
        int len_player_name;

        if (FhGlobal.game_id is FhGameId.FFX) {
            FhSaveHeader header = MemoryMarshal.Read<FhSaveHeader>(header_bytes);

            len_player_name = FhEncoding.decode(header.player_name, dest, header.lang_id, flags: FhEncodingFlags.IMPLICIT_END);
            dest [ len_player_name ] = 0x00;
            return;
        }

        byte*              ptr_player_name_encoded = FhUtil.ptr_at<byte>(pal_addr_buf_player_name_encoded());
        ReadOnlySpan<byte> player_name_encoded     = new(ptr_player_name_encoded, int.MaxValue);

        FhCall.SaveDataGetLoc.fnptr!(0xDD + header_bytes[0x21], ptr_player_name_encoded);

        len_player_name = FhEncoding.decode(player_name_encoded, dest, flags: FhEncodingFlags.IMPLICIT_END);
        dest [ len_player_name ] = 0x00;
    }

    /* [fkelava 13/11/25 22:08]
     * FFX-2.exe+11DC50, L137-138 (X-2 LM)
     */

    /// <summary>
    ///     Writes the player's job in the FF X-2 LM save with the given
    ///     <paramref name="header"/> to <paramref name="dest"/> as a UTF-8 string.
    /// </summary>
    internal static void pal_get_lm_job(in ReadOnlySpan<byte> header, in Span<byte> dest) {
        if (FhGlobal.game_id is not FhGameId.FFX2LM) {
            dest[0] = 0x00;
            return;
        }

        byte*              ptr_lm_job_encoded = FFX2.FhCall.GetLastMissionJobName.fnptr!(header[0x21], header[0x23]);
        ReadOnlySpan<byte> lm_job_encoded     = new(ptr_lm_job_encoded, int.MaxValue);

        int len_lm_job = FhEncoding.decode(lm_job_encoded, dest, flags: FhEncodingFlags.IMPLICIT_END);
        dest [ len_lm_job ] = 0x00;
    }

    /* [fkelava 13/11/25 22:08]
     * FFX-2.exe+11DC50, L133-136 (X-2 LM)
     */

    /// <summary>
    ///     Writes the player's level in the FF X-2 LM save with the given
    ///     <paramref name="header"/> to <paramref name="dest"/> as a UTF-8 string.
    /// </summary>
    internal static void pal_get_lm_level(in ReadOnlySpan<byte> header, in Span<byte> dest) {
        if (FhGlobal.game_id is not FhGameId.FFX2LM) {
            dest[0] = 0x00;
            return;
        }

        byte*              ptr_player_level_prefix_encoded = FhUtil.ptr_at<byte>(0x9ED378);
        ReadOnlySpan<byte> player_level_prefix_encoded     = new(ptr_player_level_prefix_encoded, int.MaxValue);

        FhCall.SaveDataGetLoc.fnptr!(0x36B, ptr_player_level_prefix_encoded);

        int len_player_level_prefix = FhEncoding.decode(player_level_prefix_encoded, dest, flags: FhEncodingFlags.IMPLICIT_END);
        int len_player_level        = len_player_level_prefix + Encoding.UTF8.GetBytes($" {header[0x22]}", dest[ len_player_level_prefix .. ]);

        dest [ len_player_level ] = 0x00;
    }

    /// <summary>
    ///     Writes the current location in the save with the given <paramref name="header"/>
    ///     to <paramref name="dest"/> as a UTF-8 string.
    /// </summary>
    internal static void pal_get_location(in ReadOnlySpan<byte> header, in Span<byte> dest) {
        /* [fkelava 05/11/25 00:44]
         * Strings from AtelGetSaveDicName and SaveDataGetLoc are null-terminated. You can pass
         * a span with a bogus length to FhEncoding and it will properly handle it.
         *
         * Decodes like these (UTF-8 that is directly consumed by the game)
         * MUST specify the IMPLICIT_END flag to suppress unwanted {END} on every line.
         */
        if (FhGlobal.game_id is not FhGameId.FFX2LM) {
            ushort location_id = BinaryPrimitives.ReadUInt16LittleEndian(header[ 0x18 .. ]);

            byte*              ptr_location_name_encoded = FhCall.AtelGetSaveDicName.fnptr!(location_id, 0);
            ReadOnlySpan<byte> location_name_encoded     = new(ptr_location_name_encoded, int.MaxValue);

            int len_location = FhEncoding.decode(location_name_encoded, dest, flags: FhEncodingFlags.IMPLICIT_END);
            dest [ len_location ] = 0x00;
            return;
        }

        byte*      ptr_lm_location_prefix_encoded = FhUtil.ptr_at<byte>(0x9ED058);
        byte*      ptr_lm_location_suffix_encoded = FhUtil.ptr_at<byte>(0x9ED158);
        Span<byte> lm_location_prefix_encoded     = new(ptr_lm_location_prefix_encoded, int.MaxValue);
        Span<byte> lm_location_suffix_encoded     = new(ptr_lm_location_suffix_encoded, 0x40);

        FhCall.SaveDataGetLoc.fnptr!(0x4C1, ptr_lm_location_prefix_encoded);
        FhCall.SaveDataGetLoc.fnptr!(0x4C2, ptr_lm_location_suffix_encoded);

        pal_fill_template(lm_location_suffix_encoded, (byte)(header[0x25] >> 1));

        int len_lm_location_prefix = FhEncoding.decode(lm_location_prefix_encoded, dest, flags: FhEncodingFlags.IMPLICIT_END);
        int len_lm_location        = len_lm_location_prefix + Encoding.UTF8.GetBytes(" ", dest[ len_lm_location_prefix .. ]);

        len_lm_location += FhEncoding.decode(lm_location_suffix_encoded, dest[ len_lm_location .. ], flags: FhEncodingFlags.IMPLICIT_END);
        dest [ len_lm_location ] = 0x00;
    }

    /// <summary>
    ///     Gets the currently executing game's default save game folder.
    /// </summary>
    internal static string pal_get_save_default_folder() {
        return Path.Join(
            Environment.GetFolderPath(Environment.SpecialFolder.Personal),
            "SQUARE ENIX",
            "FINAL FANTASY X&X-2 HD Remaster");
    }

    /// <summary>
    ///     Gets the filename prefix of a save game for the currently loaded game.
    /// </summary>
    internal static string pal_get_save_name_prefix() {
        return FhUtil.select("ffx", "ffx2", "ffx2");
    }

    /// <summary>
    ///     Gets the name of the save game folder for the currently loaded game.
    /// </summary>
    internal static string pal_get_save_subfolder() {
        return FhUtil.select(
            "FINAL FANTASY X",
            "FINAL FANTASY X-2",
            "FINAL FANTASY X-2 LAST MISSION");
    }

    /// <summary>
    ///     Gets the file name of the save file in the given <paramref name="slot"/>.
    /// </summary>
    internal static string pal_get_save_name_for_slot(int slot) {
        string prefix = pal_get_save_name_prefix();
        return $"{prefix}_{slot:D3}";
    }

    /* [fkelava 14/11/25 01:52]
     * The rest of the PAL are address or struct offset mappings between the same calls in
     * different games. You can go to these addresses in Ghidra and navigate up the XREFs/call graph.
     */

    internal static nint pal_addr_save_mgr() {
        return FhEnvironment.BaseAddr + FhUtil.select(0x8E81E4, 0x9EDABC, 0x9EDABC);
    }

    internal static nint pal_addr_buf_player_name_encoded() {
        return FhGlobal.game_id switch {
            FhGameId.FFX2   => 0x9ED628,
            FhGameId.FFX2LM => 0x9ED358,
            _               => throw new NotImplementedException("Invalid game type"),
        };
    }

    internal static nint pal_addr_buf_playtime_prefix_encoded() {
        return FhUtil.select(0x8E8058, 0x9ED948, 0x9ED480);
    }

    internal static nint pal_addr_screen_state() {
        return FhUtil.select(0x8CB994, 0x9CEA50, 0x9CEA50);
    }

    internal static nint pal_addr_dialog_state() {
        return FhUtil.select(0x8CB998, 0x9CEA54, 0x9CEA54);
    }

    internal static byte* pal_addr_buf_save() {
        return FhUtil.ptr_at<byte>(FhUtil.select(0x1197F30, 0xF9E500, 0xF9E500));
    }

    internal static int pal_sz_buf_save() {
        return FhUtil.select(0x6900, 0x166A0, 0x166A0);
    }

    internal static nint pal_addr_force_player_rename() {
        return FhUtil.select(0xD33350, 0xA0FB70, 0xA0FB70);
    }

    internal static int pal_header_offset_playerrename() {
        // FFX.exe+2F022E, FFX-2.exe+11D0BE
        return FhUtil.select(0x0C, 0x28, 0x28);
    }

    internal static int pal_header_offset_locationid() {
        return FhGlobal.game_id switch {
            FhGameId.FFX  => 0x18, // FFX.exe+2F0E8D
            FhGameId.FFX2 => 0x2A, // FFX-2.exe+11E2A3
            _             => throw new NotImplementedException("Invalid game type"),
        };
    }

    internal static int pal_id_playtime_prefix_SaveDataGetLoc() {
        // FFX.exe+2F0F8E, FFX-2.exe+11E203, FFX-2.exe+11DF4D
        return FhUtil.select(0x52, 0x5C, 0x5C);
    }

    internal static int pal_id_map_icon_clear() {
        return FhGlobal.game_id switch {
            FhGameId.FFX  => 0x00, // FFX.exe+2F1039
            FhGameId.FFX2 => 0x17, // FFX-2.exe+11E2AD
            _             => throw new NotImplementedException("Invalid game type"),
        };
    }
}
