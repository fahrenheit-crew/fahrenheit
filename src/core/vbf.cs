// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

/// <summary>
///     The base class handling all VBF-related operations.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct BigFileStream {
    public uint              ptr_hcryptprov; // https://learn.microsoft.com/en-us/windows/win32/seccrypto/hcryptprov
    public uint              _0x04;
    public uint              _0x08;
    public uint              _0x0C;
    public BigFileHandle*    ptr_handle_0x10;
    public BigFileHandle*    ptr_handle_0x14;
    public BigFileHandle*    ptr_handle_0x18;
    public BigFileHandle*    ptr_handle_0x1C;
    public BigFileHandle*    ptr_handle_0x20;
    public CRITICAL_SECTION* ptr_crit_sec;
    public uint              _0x28;
    public uint              _0x2C;
    public uint              _0x30;
    public uint              _0x34;
    public uint              _0x38;
    public uint              len_stream_prefix;
    public byte*             ptr_stream_prefix;
}

/// <summary>
///     A handle to an individual VBF archive.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct BigFileHandle {
    public uint              _0x00;
    public uint              _0x04;
    public uint              _0x08;
    public uint              _0x0C;
    public uint              _0x10;
    public uint              _0x14;
    public uint              _0x18;
    public PFreeList<nint>   PFreeList_PMapPair;
    public uint              _0x34;
    public uint              _0x38;
    public uint              _0x3C;
    public byte*             ptr_file_path;
    public CRITICAL_SECTION* ptr_crit_sec;
    public uint              _0x48;
    public uint              _0x4C;
    public uint              _0x50;
    public uint              _0x54;
    public uint              _0x58;
    public VFile*            ptr_vfiles; // [32]
}

/// <summary>
///     A descriptor of an individual object in a VBF archive.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct BigFileContent {
    public uint _0x00;
    public uint _0x04;
    public uint size_low_part;
    public uint size_high_part;
}

/// <summary>
///    A individual file in a VBF archive.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct VFile {
    public byte            is_assigned;
    public uint            ptr_file;
    public BigFileHandle*  ptr_originating_handle;
    public BigFileContent* ptr_content;
    public uint            _0x10;
    public uint            _0x14;
    public byte*           ptr_buf_0x100000;
    public uint            _0x1C;
}

/// <summary>
///     A disposable structure returned when loading a file from the VBF
///     archive which ensures its release once the scope is exited.
/// </summary>
public ref struct FhVbfFileScope {

    private PStreamFile _file;
    private uint        _file_size;

    /// <summary>Attempts to open a file at the given relative <paramref name="path"/> in the VBF.</summary>
    /// <remarks>
    ///     If the path is constant, make it a UTF-8 literal and call the
    ///     <see cref="FhVbfFileScope(ReadOnlySpan{byte})"/> constructor instead.
    /// </remarks>
    public FhVbfFileScope(string path) : this(Encoding.UTF8.GetBytes(path)) { }

    /// <summary>Attempts to open a file at the given relative path in the VBF.</summary>
    /// <remarks>
    ///     <paramref name="path_u8"/> must be a UTF-8 literal or byte span.
    /// </remarks>
    public FhVbfFileScope(ReadOnlySpan<byte> path_u8) {
        unsafe { // SAFETY: P/Invoke with verified signature
            fixed (PStreamFile* ptr_file    = &_file)
            fixed (byte*        ptr_path_u8 = path_u8) {
                _ = FhCall.Phyre_PSerialization_PStreamFile_ctor.fnptr!(
                    ptr_file,
                    ptr_path_u8,
                    true,
                    0,
                    0,
                    true
                );
            }
        }
    }

    /// <summary>Returns the size, in bytes, of the referred-to file.</summary>
    public uint get_file_size() {
         unsafe { // SAFETY: P/Invoke with verified signature
             fixed (PStreamFile* ptr_file = &_file) {
                 return _file_size = FhCall.Phyre_PSerialization_PStreamFile_getFileSize.fnptr!(ptr_file);
             }
         }
    }

    /// <summary>Reads the contents of the file into a user-supplied <paramref name="buffer"/>.</summary>
    /// <returns>The number of bytes read into <paramref name="buffer"/>.</returns>
    public uint read(Span<byte> buffer) {
        unsafe { // SAFETY: P/Invoke with verified signature
            fixed (PStreamFile* ptr_file = &_file)
            fixed (byte*        ptr_fbuf = &buffer[0]) {
                return FhCall.Phyre_PSerialization_PStreamFile_read.fnptr!(
                    ptr_file,
                    ptr_fbuf,
                    uint.CreateChecked(buffer.Length)
                );
            }
        }
    }

    /// <summary>Closes the referred-to file.</summary>
    public void Dispose() {
        unsafe { // SAFETY: P/Invoke with verified signature
            fixed (PStreamFile* ptr_file = &_file) {
                FhCall.Phyre_PSerialization_PStreamFile_closeFile.fnptr!(ptr_file);
            }
        }
    }
}
