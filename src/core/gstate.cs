// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

[Flags]
public enum FhGameId {
    NULL   = 0,
    FFX    = 1,
    FFX2   = 2,
    FFX2LM = 4,
}

/// <summary>
///     Contains runtime constants appropriate regardless of the currently executing game.
/// </summary>
public static class FhGlobal {

    static FhGlobal() {
        game_id = _init_game_id();
        lang_id = _init_lang_id();
    }

    public static readonly FhLangId lang_id;
    public static readonly FhGameId game_id;

    /// <summary>
    ///     Determines the game Fahrenheit was loaded into for the session.
    /// </summary>
    private static FhGameId _init_game_id() {
        FhGameId rv = FhGameId.NULL;

        if      (FhPInvoke.GetModuleHandle("FFX.exe")   != 0) rv = FhGameId.FFX;
        else if (FhPInvoke.GetModuleHandle("FFX-2.exe") != 0) rv = FhGameId.FFX2;

        if (rv is FhGameId.FFX2) {
            Span<string> args = Environment.GetCommandLineArgs();
            if (args.Contains("FFX2_LASTMISSION")) rv = FhGameId.FFX2LM;
        }

        return rv;
    }

    /// <summary>
    ///     Determines the language the game will use for the session.
    /// </summary>
    private static FhLangId _init_lang_id() {
        string ini_path     = FhEnvironment.Finder.get_path_settings();
        string ini_lang_key = "Language=";
        // linebreaks can be inconsistent depending on whether INI edited by hand or by launcher
        char[] line_breaks  = [ '\r', '\n' ];

        if (!File.Exists(ini_path)) {
            FhInternal.Log.Warning($"no game INI available to probe language from; fallback to JP");
            return FhLangId.JP;
        }

        using FileStream   ini_stream = File.OpenRead(ini_path);
        using StreamReader ini_reader = new StreamReader(ini_stream);

        string ini = ini_reader.ReadToEnd();

        int ini_lang_s = ini.IndexOf(ini_lang_key) + ini_lang_key.Length;
        int ini_lang_e = ini[ ini_lang_s .. ].IndexOfAny(line_breaks) + ini_lang_s;

        /* [fkelava 12/09/26 01:17]
         * Some versions of the remaster have separate locales for
         * Traditional and Simplified Chinese. Steam, notably, does not.
         */

        return ini[ ini_lang_s .. ini_lang_e ] switch {
            "en"         => FhLangId.US,
            "ch" or "cn" => FhLangId.CH,
            "jp"         => FhLangId.JP,
            "kr"         => FhLangId.KR,
            "fr"         => FhLangId.FR,
            "es"         => FhLangId.SP,
            "it"         => FhLangId.IT,
            "de"         => FhLangId.DE,
            _            => FhLangId.JP, // mirror game default behavior
        };
    }
}
