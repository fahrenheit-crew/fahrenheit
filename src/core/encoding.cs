// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

/* [fkelava 03/09/26 16:30]
 * The game uses a custom text encoding to save space on disk. Text may contain
 * embedded 'ops' or commands which modify its display, reference script variables,
 * or expand macros. This is an encoder and decoder for all languages of FF X and
 * non-CJK languages of FF X-2/LM; it round-trips all original game dialogue byte-perfectly,
 * with some additional bugfixes for things which never worked in the original.
 *
 * The frontend for this functionality is DEdit.
 *
 * Input to this encoder is 'DEdit' or 'Fahrenheit' text syntax. It is similar, but not identical,
 * to text syntax in the FFXDataParser. Commands are given between curly brackets, ex. {END}.
 *
 * Consult the tests and `_op_*` literals in the source below for all supported syntax elements.
 */

/// <summary>
///     The game's supported languages.
/// </summary>
public enum FhLangId : byte {
    JP  = 0,
    US  = 1,
    FR  = 2,
    SP  = 3,
    DE  = 4,
    IT  = 5,
    KR  = 9,
    CH  = 10,
    DBG = 11
}

/* [fkelava 16/10/25 19:54]
 * Raw text lines are preceded by an array of indices, which embed line offsets,
 * option count for multiple-choice selections, and so on.
 */

public enum FhTextIndexType {
    COM_X2 = 1, // composite: u16 offset, u8 option_count, u8 unknown
    COM_X1 = 2,
    I16_X2 = 3,
    I16_X1 = 4,
    I32_X1 = 5
}

/* [fkelava 16/10/25 20:13]
 * A macro dictionary is a concatenation of up to sixteen text files with a 64-byte header
 * that indicates the start of each 'section', or individual text file.
 * In text, the op {MACRO:X:Y} then gets the Y'th line of section/file X.
 */

[InlineArray(16)]
public struct FhMacroDictSections {
    private int _i;
}

public struct FhMacroDictHeader {
    public FhMacroDictSections sections;
}

/// <summary>
///     Specifies a variety of non-standard behavior in the text encoder and decoder.
/// </summary>
[Flags]
public enum FhEncodingFlags {
    IGNORE_DEST_BUFFER     = 1,      // `dest` is not iterated through in `encode`/`decode`. Allows size to be calculated with cheap stackalloc buffer.
    IGNORE_EXPRESSIONS     = 1 << 1, // U+007B and U+007D are no longer considered expression opener and closer, respectively.
    IMPLICIT_END           = 1 << 2, // Do not emit the {END} mark on phrase termination. This output style is not roundtrippable.
    IMPLICIT_CJK_EXTENSION = 1 << 3, // In a CJK language, implicitly extend supported characters to their fullwidth forms to allow the display of English text.
}

/// <summary>
///     Exposes low-level transforms between UTF-8 and game encoding.
/// </summary>
public static class FhEncoding {

    /* [fkelava 14/10/25 14:58]
     * These literals and methods control the plaintext representation of
     * text ops when decoding, and how the encoder shall identify them.
     * Changing them constitutes a breaking change.
     */

    private static ReadOnlySpan<byte> _newline    => "\r\n"u8;
    private static ReadOnlySpan<byte> _op_end     => "{END}"u8;     // 0x00
    private static ReadOnlySpan<byte> _op_break   => "{BREAK}"u8;   // 0x01
    private static ReadOnlySpan<byte> _op_newline => "{LF}"u8;      // 0x03
    private static ReadOnlySpan<byte> _op_space   => "{SPACE:"u8;   // 0x07
    private static ReadOnlySpan<byte> _op_time    => "{TIME:"u8;    // 0x09
    private static ReadOnlySpan<byte> _op_color   => "{COLOR:"u8;   // 0x0A
    private static ReadOnlySpan<byte> _op_btn     => "{BTN:"u8;     // 0x0B
    private static ReadOnlySpan<byte> _op_choice  => "{CHOICE:"u8;  // 0x10
    private static ReadOnlySpan<byte> _op_var     => "{VAR:"u8;     // 0x12
    private static ReadOnlySpan<byte> _op_macro   => "{MACRO:"u8;   // 0x13-0x22
    private static ReadOnlySpan<byte> _op_key     => "{KEY:"u8;     // 0x23
    private static ReadOnlySpan<byte> _op_unknown => "{UNKNOWN:"u8; // any other

    /// <summary>
    ///     Returns the UTF-8 plaintext literal corresponding to a given text <paramref name="op_code"/>.
    /// </summary>
    private static ReadOnlySpan<byte> _select_op_literal(
        byte op_code
    ) {
        return op_code switch {
            0x00                => _op_end,
            0x01                => _op_break,
            0x03                => _op_newline,
            0x07                => _op_space,
            0x09                => _op_time,
            0x0A                => _op_color,
            0x0B                => _op_btn,
            0x10                => _op_choice,
            0x12                => _op_var,
            >= 0x13 and <= 0x22 => _op_macro,
            0x23                => _op_key,
            _                   => _op_unknown,
        };
    }

    /// <summary>
    ///     Returns the op code corresponding to a UTF-8 text op <paramref name="expression"/>.
    /// </summary>
    private static byte _select_op_code(
        ReadOnlySpan<byte> expression
    ) {
        return 0 switch {
            _ when expression.IndexOf(_op_end    ) != -1 => 0x00,
            _ when expression.IndexOf(_op_break  ) != -1 => 0x01,
            _ when expression.IndexOf(_op_newline) != -1 => 0x03,
            _ when expression.IndexOf(_op_space  ) != -1 => 0x07,
            _ when expression.IndexOf(_op_time   ) != -1 => 0x09,
            _ when expression.IndexOf(_op_color  ) != -1 => 0x0A,
            _ when expression.IndexOf(_op_btn    ) != -1 => 0x0B,
            _ when expression.IndexOf(_op_choice ) != -1 => 0x10,
            _ when expression.IndexOf(_op_var    ) != -1 => 0x12,
            _ when expression.IndexOf(_op_macro  ) != -1 => byte.CreateChecked(0x13 + _get_op_arg1(expression, _op_macro)),
            _ when expression.IndexOf(_op_key    ) != -1 => 0x23,
            _                                            => _get_op_arg1(expression, _op_unknown),
        };
    }

    /// <summary>
    ///     Gets the first argument of a text <paramref name="op"/> from an UTF-8 plaintext <paramref name="expression"/>.
    /// </summary>
    private static byte _get_op_arg1(
        in ReadOnlySpan<byte> expression,
        in ReadOnlySpan<byte> op
    ) {
        return byte.Parse(expression[ op.Length .. (op.Length + 2) ], NumberStyles.HexNumber); // inverse of $"{arg1:X2}"
    }

    /// <summary>
    ///     Gets the second argument of a text <paramref name="op"/> from an UTF-8 plaintext <paramref name="expression"/>.
    /// </summary>
    private static byte _get_op_arg2(
        in ReadOnlySpan<byte> expression,
        in ReadOnlySpan<byte> op
    ) {
        return byte.Parse(expression[ (op.Length + 3) .. (op.Length + 5) ], NumberStyles.HexNumber); // inverse of $"{arg1:X2}:{arg2:X2}"
    }

    /// <summary>
    ///     Emits to <paramref name="dest"/> the UTF-8 plaintext literal of the op
    ///     defined by <paramref name="op_code"/>.
    /// </summary>
    /// <returns>The amount of bytes written to <paramref name="dest"/>.</returns>
    private static int _decode_op(
        in Span<byte> dest,
           byte       op_code
    ) {
        ReadOnlySpan<byte> op = _select_op_literal(op_code);
        op.CopyTo(dest);

        return op.Length;
    }

    /// <summary>
    ///     Emits to <paramref name="dest"/> the UTF-8 plaintext literal of the op
    ///     defined by <paramref name="op_code"/> with argument <paramref name="op_arg1"/>.
    /// </summary>
    /// <returns>The amount of bytes written to <paramref name="dest"/>.</returns>
    private static int _decode_op(
        in Span<byte> dest,
           byte       op_code,
           byte       op_arg1
    ) {
        ReadOnlySpan<byte> op = _select_op_literal(op_code);
        op.CopyTo(dest);

        return op.Length + Encoding.UTF8.GetBytes($"{op_arg1:X2}}}", dest[ op.Length .. ]);
    }

    /// <summary>
    ///     Emits to <paramref name="dest"/> the UTF-8 plaintext literal of the op
    ///     defined by <paramref name="op_code"/> with arguments <paramref name="op_arg1"/> and <paramref name="op_arg2"/>.
    /// </summary>
    /// <returns>The amount of bytes written to <paramref name="dest"/>.</returns>
    private static int _decode_op(
        in Span<byte> dest,
           byte       op_code,
           byte       op_arg1,
           byte       op_arg2
    ) {
        ReadOnlySpan<byte> op = _select_op_literal(op_code);
        op.CopyTo(dest);

        return op.Length + Encoding.UTF8.GetBytes($"{op_arg1:X2}:{op_arg2:X2}}}", dest[ op.Length .. ]);
    }

    /// <summary>
    ///     Reads the next game encoding character for the specified <paramref name="lang"/> and <paramref name="game"/>
    ///     from <paramref name="src"/> and emits its UTF-8 equivalent, if any, to <paramref name="dest"/>.
    /// </summary>
    /// <returns>The amount of bytes written to <paramref name="dest"/>.</returns>
    private static int _decode_char(
        in ReadOnlySpan<byte> src,
                   Span<byte> dest,
           FhLangId           lang,
           FhGameId           game
    ) {
        return _is_cjk(lang)
            ? _decode_cjk(src, dest, lang, game)
            : _decode_we (src, dest, lang, game);
    }

    /// <summary>
    ///     Writes to <paramref name="dest"/> the game encoding character at <paramref name="index"/> in the Shift-JIS table.
    /// </summary>
    /// <returns>The amount of bytes written to <paramref name="dest"/>.</returns>
    private static int _encode_char(
        in Span<byte> dest,
           int        index
    ) {
        int i = 0;

        if (index >= 0x410) {
            index -= 0x410;
            dest[i++] = 0x04;
        }

        if ((index + 0x30) > 0xFF) {
            dest[i++] = byte.CreateChecked((index / 0xD0) + 0x2B);
            dest[i++] = byte.CreateChecked((index % 0xD0) + 0x30);
        }
        else {
            dest[i++] = byte.CreateChecked(index + 0x30);
        }

        return i;
    }

    /// <summary>
    ///     Attempts to extend an eligible character in the Shift-JIS table to its fullwidth form.
    /// </summary>
    /// <remarks>Only valid for Chinese, Japanese, and Korean game languages.</remarks>
    /// <returns>Whether the character was rewritten.</returns>
    private static bool _extend_cjk(
        ref Rune code_point
    ) {
        /* [fkelava 14/07/26 17:23]
         * See issue #200, https://www.unicode.org/charts/PDF/UFF00.pdf, https://www.unicode.org/charts/PDF/U0000.pdf,
         * https://www.compart.com/en/unicode/decomposition/%3Cwide%3E.
         *
         * We only permit extension if the extended character strictly decomposes into its non-extended form.
         */
        int unicode_scalar  = code_point.Value;
        int extended_scalar = unicode_scalar switch {
            0x0020                  => 0x3000,                  // U+0020 'Space'  -> U+3000 'Ideographic Space'
            >= 0x0021 and <= 0x007E => unicode_scalar + 0xFEE0, // U+0021 - U+007E -> U+FF01 - U+FF5E
            _                       => unicode_scalar
        };

        code_point = new Rune(extended_scalar);
        return extended_scalar != unicode_scalar;
    }

    /// <summary>
    ///     Determines whether the given <paramref name="code_point"/> is invalid and should be ignored.
    /// </summary>
    private static bool _is_invalid(
        Rune code_point
    ) {
        return code_point.Value is 0x000D  // U+000D - <Carriage Return> (CR)
                                or 0x000A  // U+000A - <End of Line> (EOL, LF, NL)
                                or 0xFEFF; // U+FEFF - Zero Width No-Break Space (BOM, ZWNBSP)
    }

    /// <summary>
    ///     Determines whether the given <paramref name="lang"/> is a CJK language.
    /// </summary>
    private static bool _is_cjk(
        FhLangId lang
    ) {
        return lang is FhLangId.CH
                    or FhLangId.JP
                    or FhLangId.KR;
    }

    /// <summary>
    ///     Decodes an expression at the start of <paramref name="src"/> and
    ///     writes the resulting UTF-8 character into <paramref name="dest"/>.
    /// </summary>
    /// <remarks>Only valid for Chinese, Japanese, and Korean game languages.</remarks>
    /// <returns>The amount of bytes written to <paramref name="dest"/>.</returns>
    private static int _decode_cjk(
        in ReadOnlySpan<byte> src,
                   Span<byte> dest,
           FhLangId           lang,
           FhGameId           game
    ) {
        // FFX.exe+646250 (DecodingGamecodeJPCHKR) - cleaned up rewrite
        ReadOnlySpan<byte> sjistbl     = FhShiftJisTables.get_table(lang, game);
        int                sjistbl_idx = 0; // iVar4

        byte e0 = src[0];             // bVar2
        int  i  = e0 == 0x04 ? 1 : 0; // simulates increment of `src` pointer
        byte e1 = src[i];             // bVar1

        /* [fkelava 30/10/25 19:19]
         * This function has three nonstandard behaviors compared to the game:
         * 1) In Korean FF X substitutes U+0020 (Space) for U+3000 (Ideographic Space) - we do not.
         *
         * 2) FF X treats all CJK characters past byte offset 0x78 as 3-byte, but final character in JP table
         * is 2-byte U+00AE (Registered Sign), so it reads garbage past the end. We do not.
         *
         * 3) PS2 Int and HD refuse to decode 5C, 5D and 5E in JP. These are U+247{1|2|3} (Circled Number 18/19/20).
         * Ideographic Space (U+3000) is substituted. We allow it so text can properly round-trip.
         */

        if (e1 < 0x30) {
            sjistbl_idx = (e1 - 0x2B) * 0xD0;
            i++;
        }

        byte e2 = src[i];
        sjistbl_idx += (e2 - 0x30);

        if (e0 == 0x04) {
            sjistbl_idx += 0x410;
        }

        sjistbl_idx *= 3;

        if (sjistbl_idx > sjistbl.Length) {
            throw new Exception($"lang {lang}: invalid expr {e0:X2} {e1:X2} {e2:X2}");
        }

        Range copy_range = sjistbl_idx switch {
         <= 0x32                                                     => sjistbl_idx       .. (sjistbl_idx + 3),
            0x33                                                     => sjistbl_idx       .. (sjistbl_idx + 2),
          > 0x33 when lang is FhLangId.CH || sjistbl_idx < 0x78 => (sjistbl_idx - 1) .. (sjistbl_idx + 2),
            0x78                                                     => (sjistbl_idx - 1) .. (sjistbl_idx + 1),
            0xBBB when lang is FhLangId.JP                     => (sjistbl_idx - 2) .. sjistbl_idx,
            _                                                        => (sjistbl_idx - 2) .. (sjistbl_idx + 1)
        };

        sjistbl[copy_range].CopyTo(dest);
        return copy_range.End.Value - copy_range.Start.Value;
    }

    /// <summary>
    ///     Decodes an expression at the start of <paramref name="src"/> and
    ///     writes the resulting UTF-8 character into <paramref name="dest"/>.
    /// </summary>
    /// <remarks>Only valid for Western game languages, i.e. not Chinese, Japanese, or Korean.</remarks>
    /// <returns>The amount of bytes written to <paramref name="dest"/>.</returns>
    private static int _decode_we(
        in ReadOnlySpan<byte> src,
                   Span<byte> dest,
           FhLangId           lang,
           FhGameId           game
    ) {
        // FFX.exe+6463a0 (DecodingGamecodeUK) - cleaned up rewrite
        ReadOnlySpan<byte> sjistbl     = FhShiftJisTables.get_table(lang, game);
        int                sjistbl_idx = 0; // iVar2

        int  i  = 0; // simulates increment of `src` pointer
        byte e0 = src[0];

        if (e0 < 0x30) {
            sjistbl_idx = (e0 - 0x2B) * 0xD0;
            i++;
        }

        byte e1 = src[i];
        sjistbl_idx += (e1 - 0x30);

        Range? copy_range = sjistbl_idx switch {
          < 0xC  => sjistbl_idx                .. (sjistbl_idx + 0x01),
            0xC  => 0xC                        .. 0xF,
          < 0x11 => (sjistbl_idx + 0x02)       .. (sjistbl_idx + 0x03),
            0x11 => 0x13                       .. 0x16,
          < 0x3F => (sjistbl_idx + 0x04)       .. (sjistbl_idx + 0x05),
            0x3F => 0x43                       .. 0x46,
          < 0x5E => (sjistbl_idx + 0x06)       .. (sjistbl_idx + 0x07),
            0x5E => (sjistbl_idx + 0x06)       .. (sjistbl_idx + 0x08),
            0x5F => (sjistbl_idx + 0x07)       .. (sjistbl_idx + 0x0A),
            0x60 => (sjistbl_idx + 0x09)       .. (sjistbl_idx + 0x0C),
            0x61 => (sjistbl_idx + 0x0B)       .. (sjistbl_idx + 0x0E),
            0x62 => (sjistbl_idx + 0x0D)       .. (sjistbl_idx + 0x10),
            0x63 => (sjistbl_idx + 0x0F)       .. (sjistbl_idx + 0x10),
            0x64 => (sjistbl_idx + 0x0F)       .. (sjistbl_idx + 0x12),
            0x65 => (sjistbl_idx + 0x11)       .. (sjistbl_idx + 0x14),
            0x66 => (sjistbl_idx + 0x13)       .. (sjistbl_idx + 0x16),
            0x67 => (sjistbl_idx + 0x15)       .. (sjistbl_idx + 0x16),
            0x68 => (sjistbl_idx + 0x15)       .. (sjistbl_idx + 0x17),
            0x69 => (sjistbl_idx + 0x16)       .. (sjistbl_idx + 0x19),
            0x6A => (sjistbl_idx + 0x18)       .. (sjistbl_idx + 0x1B),
            0x6B => (sjistbl_idx + 0x1A)       .. (sjistbl_idx + 0x1D),
            0x6C => (sjistbl_idx + 0x1C)       .. (sjistbl_idx + 0x1F),
          < 0x70 => ((sjistbl_idx * 2) - 0x4F) .. ((sjistbl_idx * 2) - 0x4F + 2),
            0x70 => 0x91                       .. 0x92,
         <= 0x9F => ((sjistbl_idx * 2) - 0x50) .. ((sjistbl_idx * 2) - 0x50 + 2),
            0xA0 => (sjistbl_idx + 0x50)       .. (sjistbl_idx + 0x51),
            0xA1 => (sjistbl_idx + 0x50)       .. (sjistbl_idx + 0x52),
            0xA2 => (sjistbl_idx + 0x51)       .. (sjistbl_idx + 0x54),
            0xA3 => (sjistbl_idx + 0x53)       .. (sjistbl_idx + 0x56),
            0xA4 => (sjistbl_idx + 0x55)       .. (sjistbl_idx + 0x56),
            0xA5 => (sjistbl_idx + 0x55)       .. (sjistbl_idx + 0x58),
            0xA6 => (sjistbl_idx + 0x57)       .. (sjistbl_idx + 0x5A),
            0xA7 or
            0xA8 => (sjistbl_idx + 0x59)       .. (sjistbl_idx + 0x5A),
            0xA9 => (sjistbl_idx + 0x59)       .. (sjistbl_idx + 0x5C),
            0xAA => (sjistbl_idx + 0x5B)       .. (sjistbl_idx + 0x5C),
            0xAB => (sjistbl_idx + 0x5B)       .. (sjistbl_idx + 0x5E),
            0xAC => (sjistbl_idx + 0x5D)       .. (sjistbl_idx + 0x5F),
            0xAD => (sjistbl_idx + 0x5E)       .. (sjistbl_idx + 0x60),
            0xAE => (sjistbl_idx + 0x5F)       .. (sjistbl_idx + 0x61),
            0xAF => (sjistbl_idx + 0x60)       .. (sjistbl_idx + 0x62),
            0xB0 => (sjistbl_idx + 0x61)       .. (sjistbl_idx + 0x63),
            0xB1 => (sjistbl_idx + 0x62)       .. (sjistbl_idx + 0x64),
            0xB2 => (sjistbl_idx + 0x63)       .. (sjistbl_idx + 0x65),
            0xB3 => (sjistbl_idx + 0x64)       .. (sjistbl_idx + 0x66),
            0xB4 => (sjistbl_idx + 0x65)       .. (sjistbl_idx + 0x67),
            0xB5 => (sjistbl_idx + 0x66)       .. (sjistbl_idx + 0x68),
            0xB6 => (sjistbl_idx + 0x67)       .. (sjistbl_idx + 0x69),
            0xB7 => (sjistbl_idx + 0x68)       .. (sjistbl_idx + 0x6A),
            0xB8 => (sjistbl_idx + 0x69)       .. (sjistbl_idx + 0x6C),
            0xB9 => (sjistbl_idx + 0x6B)       .. (sjistbl_idx + 0x6E),
            0xBA => (sjistbl_idx + 0x6D)       .. (sjistbl_idx + 0x6E),
            0xBB => (sjistbl_idx + 0x6D)       .. (sjistbl_idx + 0x6F),
            0xBC => (sjistbl_idx + 0x6E)       .. (sjistbl_idx + 0x71),
            0xBD => (sjistbl_idx + 0x70)       .. (sjistbl_idx + 0x73),
            0xBE => (sjistbl_idx + 0x72)       .. (sjistbl_idx + 0x75),
            0xBF => (sjistbl_idx + 0x74)       .. (sjistbl_idx + 0x77),
            _    => null,
        };

        if (copy_range is not Range valid_range)
            return 0;

        sjistbl[valid_range].CopyTo(dest);
        return valid_range.End.Value - valid_range.Start.Value;
    }

    /* [fkelava 20/10/25 14:03]
     * Shift-JIS tables can arbitrarily combine ASCII and UTF-8 characters, most prominently
     * in the Western encoding tables. In the original game they 'account' for this by
     * manually fixing up the byte offset, as seen above in _decode_cjk() and _decode_us().
     *
     * These functions apply the inverse, for encoding purposes.
     */

    private static int _select_correction_cjk(int index, FhLangId lang) {
        index += index switch {
            <= 0x33                                                => 0,
             > 0x33 when lang is FhLangId.CH || index <= 0x78 => 1,
               _                                                   => 2
        };

        return index /= 3;
    }

    private static int _select_correction_we(int index) {
        return index switch {
            <= 0xC   => index,
            <= 0x13  => index - 0x02,
            <= 0x43  => index - 0x04,
             < 0x64  => index - 0x06,
               0x64  => index - 0x06,
               0x66  => index - 0x07,
               0x69  => index - 0x09,
               0x6C  => index - 0x0B,
               0x6F  => index - 0x0D,
               0x73  => index - 0x0F,
               0x76  => index - 0x11,
               0x79  => index - 0x13,
               0x7D  => index - 0x15,
               0x7F  => index - 0x16,
               0x82  => index - 0x18,
               0x85  => index - 0x1A,
               0x88  => index - 0x1C,
             < 0xBF  => (index + 0x4F + 1) / 2, // +1 to round up
            <= 0xEF  => (index + 0x50 + 1) / 2,
               0xF0  => index - 0x50,
               0xF1  => index - 0x50,
               0xF3  => index - 0x51,
               0xF6  => index - 0x53,
               0xF9  => index - 0x55,
               0xFA  => index - 0x55,
               0xFD  => index - 0x57,
               0x100 or
               0x101 => index - 0x59,
               0x102 => index - 0x59,
               0x106 => index - 0x5B,
               0x109 => index - 0x5D,
               0x10B => index - 0x5E,
               0x10D => index - 0x5F,
               0x10F => index - 0x60,
               0x111 => index - 0x61,
               0x113 => index - 0x62,
               0x115 => index - 0x63,
               0x117 => index - 0x64,
               0x119 => index - 0x65,
               0x11B => index - 0x66,
               0x11D => index - 0x67,
               0x11F => index - 0x68,
               0x121 => index - 0x69,
               0x124 => index - 0x6B,
               0x128 => index - 0x6D,
               0x12A => index - 0x6E,
               0x12D => index - 0x70,
               0x130 => index - 0x72,
               0x133 => index - 0x74,
               _     => throw new Exception($"UNREACHABLE ({index:X2})")
        };
    }

    /// <summary>
    ///     Given some text in <paramref name="src"/>, computes the size of the buffer required to write all indices
    ///     of a given <paramref name="index_type"/> necessary for the game to access that text once encoded.
    /// </summary>
    /// <remarks>
    ///     Such a buffer is given as the second argument to <see cref="write_indices(in ReadOnlySpan{byte}, Span{byte}, FhTextIndexType)"/>.
    /// </remarks>
    public static int compute_index_buffer_size(
        in ReadOnlySpan<byte> src,
           FhTextIndexType    index_type
    ) {
        int offset = 0;
        int size   = 0;

        while ((offset += src[offset .. ].IndexOf(_op_end) + _op_end.Length) < src.Length) {
            size += index_type switch {
                FhTextIndexType.COM_X2 => 8,
                FhTextIndexType.COM_X1 => 4,
                FhTextIndexType.I16_X2 => 4,
                FhTextIndexType.I16_X1 => 2,
                _                      => throw new NotImplementedException($"Unknown index type {index_type}"),
            };
        }

        return size;
    }

    /// <summary>
    ///     Given some game-encoded text in <paramref name="src"/>, writes to <paramref name="dest"/> the indices
    ///     of a given <paramref name="index_type"/> necessary for the game to access that text.
    /// </summary>
    /// <remarks>
    ///     <paramref name="dest"/> must be properly sized. To obtain the correct size, perform
    ///     <see cref="compute_index_buffer_size(in ReadOnlySpan{byte}, FhTextIndexType)"/> on the source text.
    /// </remarks>
    public static void write_indices(
        in ReadOnlySpan<byte> src,
                   Span<byte> dest,
           FhTextIndexType    index_type
    ) {
        int src_offset  = 0;
        int dest_offset = 0;
        int index       = short.CreateChecked(dest.Length);

        ReadOnlySpan<byte> nul = "\0"u8;

        while (dest_offset < dest.Length) {
            int                index_next_nul = src[src_offset .. ].IndexOf(nul) + nul.Length;
            ReadOnlySpan<byte> line           = src[src_offset .. (src_offset + index_next_nul)];

            int option_count = 0;
            foreach (byte b in line) {
                if (b is 0x10) // {CHOICE}
                    option_count++;
            }

            src_offset  += index_next_nul;
            dest_offset += _write_index(dest[dest_offset .. ], index, option_count, index_type);
            index       += index_next_nul;
        }
    }

    /// <summary>
    ///     Reads a text file index of a given <paramref name="index_type"/>.
    /// </summary>
    public static int read_index(
        in  ReadOnlySpan<byte> src,
            FhTextIndexType    index_type,
        out int                consumed
    ) {
        consumed = index_type switch {
            FhTextIndexType.COM_X2 => 8,
            FhTextIndexType.COM_X1 or
            FhTextIndexType.I32_X1 or
            FhTextIndexType.I16_X2 => 4,
            FhTextIndexType.I16_X1 => 2,
            _                      => throw new Exception("UNREACHABLE")
        };

        if (index_type is FhTextIndexType.I32_X1) {
            return BinaryPrimitives.ReadInt32LittleEndian(src);
        }

        ushort e0 = index_type switch {
            FhTextIndexType.COM_X2 or
            FhTextIndexType.COM_X1 => BinaryPrimitives.ReadUInt16LittleEndian(src),
            FhTextIndexType.I16_X2 or
            FhTextIndexType.I16_X1 => BinaryPrimitives.ReadUInt16LittleEndian(src),
            _                      => throw new Exception("UNREACHABLE")
        };

        byte option_count = index_type is FhTextIndexType.COM_X2 or FhTextIndexType.COM_X1
            ? src[3]
            : (byte)0;

        ushort e1 = index_type switch {
            FhTextIndexType.COM_X2 => BinaryPrimitives.ReadUInt16LittleEndian(src[sizeof(int)   .. ]),
            FhTextIndexType.COM_X1 => e0,
            FhTextIndexType.I16_X2 => BinaryPrimitives.ReadUInt16LittleEndian(src[sizeof(short) .. ]),
            FhTextIndexType.I16_X1 => e0,
            _                      => throw new Exception("UNREACHABLE")
        };

        ArgumentOutOfRangeException.ThrowIfNotEqual(e0, e1);
        return e0;
    }

    /// <summary>
    ///     Writes to <paramref name="dest"/> an index of a given <paramref name="index_type"/> with the literal
    ///     offset <paramref name="value"/> and <paramref name="option_count"/>.
    /// </summary>
    /// <returns>The amount of bytes written to <paramref name="dest"/>.</returns>
    private static int _write_index(
        Span<byte>      dest,
        int             value,
        int             option_count,
        FhTextIndexType index_type
    ) {
        if (index_type is FhTextIndexType.I32_X1) {
            BinaryPrimitives.WriteInt32LittleEndian(dest, value);
            return 4;
        }

        short index = short.CreateChecked(value);

        BinaryPrimitives.WriteInt16LittleEndian(dest, index);
        if (index_type is FhTextIndexType.I16_X1) {
            return 2;
        }

        if (index_type is FhTextIndexType.I16_X2) {
            BinaryPrimitives.WriteInt16LittleEndian(dest, index);
            return 4;
        }

        if (index_type is FhTextIndexType.COM_X1 or FhTextIndexType.COM_X2) {
            dest[2] = 0x00; // TODO: check actual purpose
            dest[3] = byte.CreateChecked(option_count);

            if (index_type is FhTextIndexType.COM_X1) return 4;

            dest[0 .. 4].CopyTo(dest[4 .. 8]);
            return 8;
        }

        throw new Exception("UNREACHABLE");
    }

    /// <summary>
    ///     Converts a given <paramref name="expression"/>- a UTF-8 byte sequence starting with
    ///     U+007B and ending with U+007D- to game encoding and writes it to <paramref name="dest"/>.
    /// </summary>
    /// <returns>The number of bytes written to <paramref name="dest"/>.</returns>
    private static int _encode_expr(
        in ReadOnlySpan<byte> expression,
                   Span<byte> dest
    ) {
        byte               op_code = _select_op_code   (expression);
        ReadOnlySpan<byte> op      = _select_op_literal(op_code);

        dest[0] = op_code;

        // {(COLOR|BTN|KEY):X2}
        if (op_code is 0x0A or 0x0B or 0x23) {
            dest[1] = _get_op_arg1(expression, op);
            return 2;
        }

        // {(SPACE|TIME|CHOICE|VAR):(X2 - 0x30)}
        if (op_code is 0x07 or 0x09 or 0x10 or 0x12) {
            dest[1] = byte.CreateChecked(0x30 + _get_op_arg1(expression, op));
            return 2;
        }

        // {MACRO:(X2 - 0x13):(X2 - 0x30)}
        if (op_code is (>= 0x13 and <= 0x22)) {
            dest[1] = byte.CreateChecked(0x30 + _get_op_arg2(expression, op));
            return 2;
        }

        // {(END|PAUSE|LF)} or {UNKNOWN:ARG1}
        if (op_code is 0x00 or 0x01 or 0x02 or 0x03 or 0x05) {
            return 1;
        }

        // {UNKNOWN:ARG1:ARG2}
        if (op_code is 0x06 or 0x08 or (>= 0x0C and <= 0x0F) or 0x11 or (>= 0x24 and <= 0x2B)) {
            dest[1] = _get_op_arg2(expression, op);
            return 2;
        }

        throw new Exception($"unreachable - illegal expr {Convert.ToHexString(expression)}");
    }

    /// <summary>
    ///     Computes, in bytes, how large a buffer should be to store the encoded contents of <paramref name="src"/>.
    /// </summary>
    public static int compute_encode_buffer_size(
        in ReadOnlySpan<byte> src,
           FhLangId?          lang  = default,
           FhGameId?          game  = default,
           FhEncodingFlags    flags = default
    ) {
        Span<byte> dest = stackalloc byte[64];
        return encode(src, dest, lang, game, flags | FhEncodingFlags.IGNORE_DEST_BUFFER);
    }

    /// <summary>
    ///     Writes into <paramref name="dest"/> the game encoding representation of a
    ///     UTF-8 string contained in <paramref name="src"/>.
    /// </summary>
    /// <remarks>
    ///     If <paramref name="lang"/> or <paramref name="game"/>
    ///     are not specified, the active game type and language are used.
    ///     If calling outside the game process, they MUST be specified.
    /// </remarks>
    /// <returns>The number of bytes written to <paramref name="dest"/>.</returns>
    public static int encode(
        in ReadOnlySpan<byte> src,
                   Span<byte> dest,
           FhLangId?          lang  = default,
           FhGameId?          game  = default,
           FhEncodingFlags    flags = default
    ) {
        FhLangId lang_id = lang ?? FhGlobal.lang_id;
        FhGameId game_id = game ?? FhGlobal.game_id;

        ReadOnlySpan<byte> sjistbl = FhShiftJisTables.get_table(lang_id, game_id);

        int src_offset, dest_offset;

        for (src_offset = 0, dest_offset = 0; src_offset < src.Length; ) {
            /* [fkelava 14/10/25 19:27]
             * Obtain the next complete UTF-8 code point in the input buffer.
             */
            OperationStatus decode_status = Rune.DecodeFromUtf8(src[ src_offset .. ], out Rune code_point, out int consumed);

            if (decode_status != OperationStatus.Done) {
                FhInternal.Log.Info($"failed to get complete UTF8 codepoint in input; aborting (pos {src_offset}, R {decode_status})");
                return dest_offset;
            }

            if (_is_invalid(code_point)) {
                src_offset += consumed;
                continue;
            }

            /* [fkelava 16/07/26 13:10]
             * When doing implicit CJK extension, we will have to rewrite `bytes` later with a `stackalloc` buffer.
             * `bytes` is returnable, but a `stackalloc` buffer is not, so the compiler complains.
             * We mark this span scoped so the compiler knows that such reassignment is valid and `bytes` is nonreturnable.
             *
             * See generally https://github.com/dotnet/csharplang/discussions/1130.
             */
            scoped ReadOnlySpan<byte> bytes     = src[ src_offset .. (src_offset + consumed) ];
                   Span        <byte> dst_slice = flags.HasFlag(FhEncodingFlags.IGNORE_DEST_BUFFER) ? dest : dest[ dest_offset .. ];

            /* [fkelava 14/10/25 19:27]
             * If we have encountered a op statement opener (U+007B '{'), process the op by
             * consuming complete UTF-8 code points until a statement terminator (U+007D '}'),
             * then encoding the complete expression. The usual form is {CMD:ARG1:ARG2}.
             */
            if (code_point.Value == 0x007B && !flags.HasFlag(FhEncodingFlags.IGNORE_EXPRESSIONS)) { // '{' (U+007B) - dialogue op statement opener
                // check for '{{' sequence - escape '{'
                if (Rune.DecodeFromUtf8(src[ (src_offset + consumed) .. ], out code_point, out consumed) == OperationStatus.Done && code_point.Value == 0x007B) {
                    src_offset += consumed;
                    goto encode;
                }

                int expr_start_offset = src_offset;
                src_offset += consumed;

                while (code_point.Value != 0x007D) { // '}' (U+007D) - dialogue op statement terminator
                    decode_status = Rune.DecodeFromUtf8(src[ src_offset .. ], out code_point, out consumed);

                    if (decode_status != OperationStatus.Done) {
                        FhInternal.Log.Info($"failed to get complete UTF8 codepoint while parsing op statement; aborting (pos {src_offset}, R {decode_status})");
                        return dest_offset;
                    }

                    src_offset += consumed;
                }

                dest_offset += _encode_expr(src[ expr_start_offset .. src_offset ], dst_slice);
                continue;
            }

            /* [fkelava 14/10/25 19:27]
             * If the character is NOT an op statement opener, find it in the Shift-JIS table and emit its index.
             */
        encode:
            if (flags.HasFlag(FhEncodingFlags.IMPLICIT_CJK_EXTENSION) && _is_cjk(lang_id) && _extend_cjk(ref code_point)) {
                Span<byte> extend_buf = stackalloc byte[code_point.Utf8SequenceLength];
                code_point.EncodeToUtf8(extend_buf);
                bytes = extend_buf;
            }

            int index = sjistbl.IndexOf(bytes);

            if (index == -1) {
                FhInternal.Log.Error($"sequence {Convert.ToHexString(bytes)} not in sjistbl of lang {lang_id}; aborting (pos {src_offset})");
                return dest_offset;
            }

            index = lang_id is FhLangId.CH or FhLangId.JP or FhLangId.KR
                ? _select_correction_cjk(index, lang_id)
                : _select_correction_we (index);

            src_offset  += consumed;
            dest_offset += _encode_char(dst_slice, index);
        }

        return dest_offset;
    }

    /// <summary>
    ///     If <paramref name="lang"/> or <paramref name="game"/>
    ///     are not specified, the active game type and language are used.
    ///     If calling outside the game process, they MUST be specified.
    /// </summary>
    /// <returns>The size of a buffer that can store the decoded contents of <paramref name="src"/>.</returns>
    public static int compute_decode_buffer_size(
        in ReadOnlySpan<byte> src,
           FhLangId?          lang  = default,
           FhGameId?          game  = default,
           FhEncodingFlags    flags = default
    ) {
        Span<byte> dest = stackalloc byte[64];
        return decode(src, dest, lang, game, flags | FhEncodingFlags.IGNORE_DEST_BUFFER);
    }

    /// <summary>
    ///     Writes into <paramref name="dest"/> the UTF-8 representation of a
    ///     game encoding string contained in <paramref name="src"/>.
    /// </summary>
    /// <remarks>
    ///     If <paramref name="lang"/> or <paramref name="game"/>
    ///     are not specified, the active game type and language are used.
    ///     If calling outside the game process, they MUST be specified.
    /// </remarks>
    /// <returns>The number of bytes written to <paramref name="dest"/>.</returns>
    public static int decode(
        in ReadOnlySpan<byte> src,
           Span        <byte> dest,
           FhLangId?          lang  = default,
           FhGameId?          game  = default,
           FhEncodingFlags    flags = default
    ) {
        FhLangId lang_id = lang ?? FhGlobal.lang_id;
        FhGameId game_id = game ?? FhGlobal.game_id;

        int src_offset, dest_offset;

        for (src_offset = 0, dest_offset = 0; src_offset < src.Length; ) {
            byte op = src[src_offset];

            ReadOnlySpan<byte> src_slice = src[ src_offset .. ];
            Span        <byte> dst_slice = flags.HasFlag(FhEncodingFlags.IGNORE_DEST_BUFFER) ? dest : dest[ dest_offset .. ];

            if (op == 0x00) { // {END} - statement terminator
                if (flags.HasFlag(FhEncodingFlags.IMPLICIT_END))
                    return dest_offset;

                _newline.CopyTo(dst_slice[ _decode_op(dst_slice, op) .. ]);
                return dest_offset + _op_end.Length + _newline.Length;
            }

            (int src_increment, int dest_increment) = op switch {
                // 0-arg ops - {PAUSE|LF}
                0x01 or 0x03          => (1, _decode_op(dst_slice, op)),
                // 1-arg ops - {COLOR|BTN|KEY}
                0x0A or 0x0B or 0x23  => (2, _decode_op(dst_slice, op, src[src_offset + 1])),
                // 1-arg ops with 0x30-based indexing - {SPACE|TIME|CHOICE}
                0x07 or 0x09 or 0x10  => (2, _decode_op(dst_slice, op, byte.CreateChecked(src[src_offset + 1] - 0x30))),
                // 2-arg ops where the op itself is transformed to an argument - {MACRO}
                >= 0x13 and <= 0x22   => (2, _decode_op(dst_slice, op, byte.CreateChecked(op - 0x13), byte.CreateChecked(src[src_offset + 1] - 0x30))),
                // Multi-byte character expressions - 0x04 is CJK-only, 0x2C-2F for any charset
                0x04                  => (src[src_offset + 1] < 0x30 ? 3 : 2, _decode_cjk (src[ src_offset .. ], dst_slice, lang_id, game_id)),
                >= 0x2C and <= 0x2F   => (2,                                  _decode_char(src[ src_offset .. ], dst_slice, lang_id, game_id)),
                // Unknown 1-arg ops, embedded literally
                0x02 or 0x05          => (1, _decode_op(dst_slice, op, src[src_offset + 1])),
                0x06 or
                0x08 or
                (>= 0x0C and <= 0x0F) or
                0x11 or
                0x12 or
                (>= 0x24 and <= 0x2B) => (2, _decode_op(dst_slice, op, op, src[src_offset + 1])),
                // fall through: regular character
                _                     => (1, _decode_char(src[ src_offset .. ], dst_slice, lang_id, game_id))
            };

            src_offset  += src_increment;
            dest_offset += dest_increment;
        }

        return dest_offset;
    }
}
