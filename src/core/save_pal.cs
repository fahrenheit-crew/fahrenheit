// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

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

[StructLayout(LayoutKind.Explicit)]
internal struct SaveDataManager {
    [FieldOffset(0x0)] public SaveDataManagerX  x;
    [FieldOffset(0x0)] public SaveDataManagerX2 x2;
}

/// <summary>
///     The game's default save data manager structure for FF X.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 0x488)]
internal unsafe struct SaveDataManagerX {
    public int                  save_enabled;
    public int                  cb_result;
    public SaveSystemState      state;
    public int                  __0xC;
    public InlineArray64 <byte> game_name;
    public InlineArray128<byte> description;
    public InlineArray512<byte> description_detailed;
    public InlineArray64 <byte> path_icon_1;
    public InlineArray64 <byte> path_icon_2;
    public byte*                ref_buffer;
    public int                  ref_buffer_size;
    public int                  operation_canceled;
}

/// <summary>
///     The game's default save data manager structure for FF X-2.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 0x4B0)]
internal unsafe struct SaveDataManagerX2 {
    public SaveSystemState      state;
    public int                  save_enabled;
    public int                  cb_result;
    public InlineArray64 <byte> game_name;
    public InlineArray128<byte> description;
    public InlineArray512<byte> description_detailed;
    public InlineArray64 <byte> path_icon_1;
    public InlineArray64 <byte> path_icon_2;
    public byte*                ref_buffer;
    public int                  ref_buffer_size;
    public int                  operation_canceled;
}

[StructLayout(LayoutKind.Explicit)]
public struct SaveHeader {
    [FieldOffset(0x0)] public SaveHeaderX  x;
    [FieldOffset(0x0)] public SaveHeaderX2 x2;
}

/// <summary>
///     The save game header for FF X.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SaveHeaderX {
    [InlineArray(0x20)]
    public struct FhSavePlayerName {
        private byte _b;
    }

    [InlineArray(0x7)]
    public struct Formation {
        private byte _e0;
    }

    public uint             __0x00;
    public byte             __0x04;
    public Formation        formation;
    public FhLangId         lang_id;
    public byte             __0x0D;
    public ushort           __0x0E;
    public uint             playtime_secs;
    public uint             gil;
    public ushort           id_location;
    public ushort           __0x1A;
    public ushort           __0x1C;
    public ushort           __0x1E;
    public FhSavePlayerName player_name;
};

/// <summary>
///     The save game header for FF X-2.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SaveHeaderX2 {
    [InlineArray(0x3)]
    public struct Party {
        private byte _e0;
    }

    public uint     __0x00;
    public byte     __0x04;
    public Party    ply;
    public Party    ply_levels;
    public byte     chapter;
    public byte     completion;
    public Party    ply_jobs;
    public uint     playtime_secs;
    public uint     gil;
    public ushort   id_location;
    public ushort   __0x1A;
    public uint     __0x1C;
    public byte     times_played;
    public byte     lm_ply;
    public byte     lm_ply_level;
    public byte     lm_job;
    public byte     lm_job_level;
    public byte     lm_id_location;
    public byte     lm_retry;
    public byte     __0x27;
    public FhLangId lang_id;
    public byte     __0x29;
    public ushort   id_map_icon;
    public byte     __0x2C;

    public bool is_new_game_plus => times_played > 1;
}

/// <summary>Sorts save games by their creation time.</summary>
public sealed class FhSaveTimeComparer : Comparer<FhSaveUiData> {
    private static FhSaveTimeComparer? _instance;

    public override int Compare(FhSaveUiData x, FhSaveUiData y) {
        if (x.slot == 0) return -1; // The autosave must precede any other save.
        if (y.slot == 0) return  1;

        if      (x.create_time > y.create_time) return -1;
        else if (x.create_time < y.create_time) return  1;

        return x.slot - y.slot;
    }

    public static FhSaveTimeComparer Instance => _instance ??= new();
}

/// <summary>Sorts save games by their slot index.</summary>
public sealed class FhSaveSlotComparer : Comparer<FhSaveUiData> {
    private static FhSaveSlotComparer? _instance;

    public override int Compare(FhSaveUiData x, FhSaveUiData y) {
        return x.slot - y.slot;
    }

    public static FhSaveSlotComparer Instance => _instance ??= new();
}

/// <summary>
///     Contains the fields the game shows as part of its standard save game display.
/// </summary>
public struct FhSaveUiData {
    [InlineArray(3)]
    public struct IconChr {
        private FhTexture _e0;
    }

    public int            slot        { get; internal init; }
    public SaveHeader     header      { get; internal init; }
    public string         slot_str    { get; internal init; }
    public string         play_time   { get; internal init; }
    public string         player_name { get; internal init; }
    public string         chapter     { get; internal init; }
    public string         completion  { get; internal init; }
    public string         location    { get; internal init; }
    public string         lm_level    { get; internal init; }
    public string         lm_job      { get; internal init; }
    public IconChr        icon_chr    { get; internal init; }
    public FhTexture      icon_map    { get; internal init; }
    public DateTimeOffset create_time { get; internal init; }
}
