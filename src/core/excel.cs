// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

/// <summary>
///     A pointer to text in an Excel container.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct ExcelTextOffset {
    /// <summary>
    ///     The offset to the text.
    /// </summary>
    public  ushort text_offset;
    private ushort __0x2; // Clearly related to the text, but unknown
}

/// <summary>
///     A pointer to text with an alternate simplified version in an Excel container.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct ExcelSimplifiableTextOffset {
    /// <summary>
    ///     The offset to the standard text.
    /// </summary>
    public ExcelTextOffset standard;

    /// <summary>
    ///     The offset to the simplified text.
    /// </summary>
    /// <remarks>
    ///     In Japanese, this would have been hiragana; 
    ///     in Western encodings, it has no effect. 
    ///     This is completely unused.
    /// </remarks>
    internal ExcelTextOffset simplified;
}
