// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Tests;

[TestFixture]
public class FhEncodingTests {

    /* [fkelava 30/10/25 16:35]
     * This is to ensure that every encodable character 'survives' a round-trip such that no meaning is lost in the plaintext.
     * This has to be tested because there are multiple encodings for some characters in the Shift-JIS tables.
     * Charset does not preserve them; it will always emit the first (i.e. numerically lowest) possible encoding in such cases.
     */

    [TestCase(FhLangId.US, FhGameId.FFX)]
    [TestCase(FhLangId.CH, FhGameId.FFX)]
    [TestCase(FhLangId.JP, FhGameId.FFX)]
    [TestCase(FhLangId.KR, FhGameId.FFX)]
    [TestCase(FhLangId.US, FhGameId.FFX2)]
    [TestCase(FhLangId.CH, FhGameId.FFX2, Ignore = "CJK encodings for FF X-2 are not yet supported.")]
    [TestCase(FhLangId.JP, FhGameId.FFX2, Ignore = "CJK encodings for FF X-2 are not yet supported.")]
    [TestCase(FhLangId.KR, FhGameId.FFX2, Ignore = "CJK encodings for FF X-2 are not yet supported.")]
    public void roundtrip_shift_jis_tables(FhLangId lang, FhGameId game) {
        ReadOnlySpan<byte> sjistbl      = FhShiftJisTables.get_table(lang, game);
        string             sjistbl_utf8 = Encoding.UTF8.GetString(sjistbl);

        byte[] sjistbl_to_game = new byte[ FhEncoding.compute_encode_buffer_size(sjistbl, lang, game, FhEncodingFlags.IGNORE_EXPRESSIONS) ];
        FhEncoding.encode(sjistbl, sjistbl_to_game, lang, game, FhEncodingFlags.IGNORE_EXPRESSIONS);

        byte[] game_to_utf8 = new byte[ FhEncoding.compute_decode_buffer_size(sjistbl_to_game, lang, game, FhEncodingFlags.IGNORE_EXPRESSIONS) ];
        FhEncoding.decode(sjistbl_to_game, game_to_utf8, lang, game);

        string sjistbl_utf8_roundtripped = Encoding.UTF8.GetString(game_to_utf8);

        Assert.That(sjistbl_utf8.Equals(sjistbl_utf8_roundtripped, StringComparison.CurrentCulture));
    }

}

public class FhEncodingRegressionTests {

    /* [fkelava 30/10/25 18:46]
     * Discovered during Archipelago testing.
     * Wrong offsets in 'select_correction_we' resulted in:
     *
     * [Amulet[ x 90
     * [Lv. 1 Key Sphere x 10[
     * xCOLOR:88vParty Member: Magus SisterxCOLOR:41v
     */

    [TestCase("[Amulet] x 90")]
    public void ffx_we_square_brackets(string input) {
        ReadOnlySpan<byte> utf8_bytes_input = Encoding.UTF8.GetBytes(input);

        byte[] encoded = new byte[ FhEncoding.compute_encode_buffer_size(utf8_bytes_input, FhLangId.US, FhGameId.FFX) ];
        FhEncoding.encode(utf8_bytes_input, encoded, FhLangId.US, FhGameId.FFX);

        ReadOnlySpan<byte> expected_result =
            [ 0x6A, 0x50, 0x7C, 0x84, 0x7B, 0x74, 0x83, 0x6C, 0x3A, 0x87, 0x3A, 0x39, 0x30 ];

        Assert.That(expected_result.SequenceEqual(encoded));
    }

    [TestCase("Amulet x 90", FhLangId.CH, FhGameId.FFX)]
    [TestCase("Amulet x 90", FhLangId.JP, FhGameId.FFX)]
    [TestCase("Amulet x 90", FhLangId.KR, FhGameId.FFX)]
    [TestCase("Amulet x 90", FhLangId.CH, FhGameId.FFX2, Ignore = "CJK encodings for FF X-2 are not yet supported.")]
    [TestCase("Amulet x 90", FhLangId.JP, FhGameId.FFX2, Ignore = "CJK encodings for FF X-2 are not yet supported.")]
    [TestCase("Amulet x 90", FhLangId.KR, FhGameId.FFX2, Ignore = "CJK encodings for FF X-2 are not yet supported.")]
    public void ffx_cjk_extension(string input, FhLangId lang, FhGameId game) {
        ReadOnlySpan<byte> utf8_bytes_input = Encoding.UTF8.GetBytes(input);
        FhEncodingFlags    flags            = FhEncodingFlags.IMPLICIT_CJK_EXTENSION;

        byte[] encoded = new byte[ FhEncoding.compute_encode_buffer_size(
            utf8_bytes_input,
            lang,
            game,
            flags) ];

        FhEncoding.encode(utf8_bytes_input, encoded, lang, game, flags);

        ReadOnlySpan<byte> expected_result = lang switch {
            FhLangId.CH or
            FhLangId.KR => [ 0x5A, 0x80, 0x88, 0x7F, 0x78, 0x87, 0x3A, 0x8B, 0x3A, 0x39, 0x30 ],
            FhLangId.JP => [ 0x2C, 0x30, 0x2C, 0x56, 0x2C, 0x5E, 0x2C, 0x55, 0x2C, 0x4E, 0x2C, 0x5D, 0x3A, 0x2C, 0x61, 0x3A, 0x39, 0x30 ],
            _           => [ ]
        };

        Assert.That(expected_result.SequenceEqual(encoded));
    }

    [TestCase("{COLOR:88}Party Member: Magus Sisters{COLOR:41}")]
    public void ffx_we_curly_brackets_in_ignore_expr(string input) {
        ReadOnlySpan<byte> utf8_bytes_input = Encoding.UTF8.GetBytes(input);

        byte[] encoded = new byte[ FhEncoding.compute_encode_buffer_size(utf8_bytes_input, FhLangId.US, FhGameId.FFX, FhEncodingFlags.IGNORE_EXPRESSIONS) ];
        FhEncoding.encode(utf8_bytes_input, encoded, FhLangId.US, FhGameId.FFX, FhEncodingFlags.IGNORE_EXPRESSIONS);

        ReadOnlySpan<byte> expected_result =
            [ 0x8A, 0x52, 0x5E, 0x5B, 0x5E, 0x61, 0x4A, 0x38, 0x38, 0x8C, 0x5F, 0x70, 0x81,
              0x83, 0x88, 0x3A, 0x5C, 0x74, 0x7C, 0x71, 0x74, 0x81, 0x4A, 0x3A, 0x5C, 0x70,
              0x76, 0x84, 0x82, 0x3A, 0x62, 0x78, 0x82, 0x83, 0x74, 0x81, 0x82, 0x8A, 0x52,
              0x5E, 0x5B, 0x5E, 0x61, 0x4A, 0x34, 0x31, 0x8C ];

        Assert.That(expected_result.SequenceEqual(encoded));
    }

}
