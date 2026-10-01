// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX;

/* [fkelava 28/07/26 02:36]
 * The game stores various data in 'Excel' containers, a form of binary serialization.
 * Headers define sections consisting of an array of elements and, optionally, text.
 *
 * Most of the game's `kernel` directory consists of such files. They contain anything
 * from player command definitions to aeon stat growth curves.
 */

/// <summary>The prologue of an Excel container. Describes it in enough detail to construct a reader.</summary>
/// <remarks>To iterate over its contents, use an <see cref="ExcelReader{T}"/>.</remarks>
[StructLayout(LayoutKind.Sequential)]
public struct ExcelProlog {
    /// <summary>
    ///     The amount of headers that map out this container.
    /// </summary>
    /// <remarks>
    ///     In the games, this amount is always 1.
    ///     Both the games and Fahrenheit support amounts higher than 1.
    /// </remarks>
    public  ushort header_count;
    private ushort _0x02;
    private ushort _0x04;
    private ushort _0x06;
}

/// <summary>
///     The header that defines a section of an Excel container.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct ExcelHeader {
    /// <summary>
    ///     The index of the first element in the section.
    /// </summary>
    public ushort index_first;

    /// <summary>
    ///     The index of the last element in the section.
    /// </summary>
    public ushort index_last;

    /// <summary>
    ///     The size of one element in the section.
    /// </summary>
    public ushort element_size;

    /// <summary>The combined length, in bytes, of all the elements in the section.</summary>
    /// <remarks>This does not include any text which may follow the data.</remarks>
    public ushort data_length;

    /// <summary>The offset, in bytes, from the start of the container to the start of the data.</summary>
    /// <remarks>In vanilla, always equivalent to the size of this header.</remarks>
    public uint data_start;

    /// <summary>
    ///     The length of the array of elements defined by this header.
    /// </summary>
    public readonly int length => index_last + 1 - index_first;
}

/// <summary>
///     Allows iteration over an Excel container of <typeparamref name="T"/>.
/// </summary>
public unsafe ref struct ExcelReader<T>(ReadOnlySpan<byte> excel_bytes) where T : unmanaged {
    private readonly ReadOnlySpan<byte> _bytes = excel_bytes;

    /// <summary>
    ///     Gets the headers of this Excel container. Each defines a section of it.
    /// </summary>
    public ReadOnlySpan<ExcelHeader> get_headers() {
        int sz_prolog = sizeof(ExcelProlog);
        int sz_header = sizeof(ExcelHeader);

        return MemoryMarshal.TryRead(_bytes, out ExcelProlog prolog)
            ? MemoryMarshal.Cast<byte, ExcelHeader>(_bytes [ sz_prolog .. (sz_prolog + prolog.header_count * sz_header) ])
            : [];
    }

    /// <summary>
    ///     Gets the instances of <typeparamref name="T"/> defined in the given <paramref name="header"/>.
    /// </summary>
    public ReadOnlySpan<T> get_elements(ExcelHeader header) {
        int start  = (int)header.data_start;
        int length = (int)header.data_length;

        return MemoryMarshal.Cast<byte, T>(_bytes [ start .. (start + length) ]);
    }

    /// <summary>
    ///     Attempts to obtain the Excel header for the element at the given <paramref name="index"/>.
    /// </summary>
    private bool find_header_for_index(int index, out ExcelHeader target_header) {
        foreach (ExcelHeader header in get_headers()) {
            if (header.index_first <= index && index <= header.index_last) {
                target_header = header;
                return true;
            }
        }

        target_header = default;
        return false;
    }

    /// <summary>
    ///     Gets a span of bytes containing the text pointed to by an Excel element's <paramref name="ptr_text"/>.
    /// </summary>
    public ReadOnlySpan<byte> get_text_span(int element_index, ExcelTextOffset ptr_text) {
        // We don't really care what the end bound is. Encoding will stop at the null terminator.
        return find_header_for_index(element_index, out ExcelHeader header)
            ? _bytes [ ((int)header.data_start + (int)header.data_length + ptr_text.text_offset) .. ]
            : [ 0x00 ];
    }
}
