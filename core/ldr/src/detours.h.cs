// SPDX-License-Identifier: MIT

namespace Fahrenheit.Core.Stage0;

[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal struct _DETOUR_SECTION_HEADER {
    internal uint cbHeaderSize;
    internal uint nSignature;
    internal uint nDataOffset;
    internal uint cbDataSize;

    internal uint nOriginalImportVirtualAddress;
    internal uint nOriginalImportSize;
    internal uint nOriginalBoundImportVirtualAddress;
    internal uint nOriginalBoundImportSize;

    internal uint nOriginalIatVirtualAddress;
    internal uint nOriginalIatSize;
    internal uint nOriginalSizeOfImage;
    internal uint cbPrePE;

    internal uint nOriginalClrFlags;
    internal uint reserved1;
    internal uint reserved2;
    internal uint reserved3;

    // Followed by cbPrePE bytes of data.
} // *PDETOUR_SECTION_HEADER;

[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal struct _DETOUR_SECTION_RECORD {
    internal uint cbBytes;
    internal uint nReserved;
    internal Guid guid;
} // *PDETOUR_SECTION_RECORD;

[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal struct _DETOUR_CLR_HEADER {
    // Header versioning
    internal ulong                 cb;
    internal ushort                MajorRuntimeVersion;
    internal ushort                MinorRuntimeVersion;

    // Symbol table and startup information
    internal IMAGE_DATA_DIRECTORY  MetaData;
    internal ulong                 Flags;

    // Followed by the rest of the IMAGE_COR20_HEADER
} // *PDETOUR_CLR_HEADER;

/* [fkelava 08/02/26 17:02]
 * https://github.com/microsoft/Detours/blob/main/src/detours.h#L463
 *
 * > #ifdef IMAGE_NT_OPTIONAL_HDR64_MAGIC
 * > BYTE raw[sizeof(IMAGE_NT_HEADERS64) + sizeof(IMAGE_SECTION_HEADER) * 32]
 * > #else
 * > BYTE raw[0x108 + sizeof(IMAGE_SECTION_HEADER) * 32]
 */
[InlineArray(1544)]
public struct InlineArray1544<T> {
    private T _b;
}

[StructLayout(LayoutKind.Explicit, Pack = 8)]
internal unsafe struct _DETOUR_EXE_RESTORE_UNION {
    [FieldOffset(0x0)] internal IMAGE_NT_HEADERS32    inh32;
    [FieldOffset(0x0)] internal IMAGE_NT_HEADERS64    inh64;
    [FieldOffset(0x0)] internal InlineArray1544<byte> raw;
}

[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal unsafe struct _DETOUR_EXE_RESTORE {
    internal uint                      cb;
    internal uint                      cbidh;
    internal uint                      cbinh;
    internal uint                      cbclr;

    internal byte*                     pidh;
    internal byte*                     pinh;
    internal byte*                     pclr;

    internal IMAGE_DOS_HEADER          idh;
    internal _DETOUR_EXE_RESTORE_UNION union;
    internal _DETOUR_CLR_HEADER        clr;
}

[StructLayout(LayoutKind.Sequential, Pack = 8)]
internal struct _DETOUR_EXE_HELPER {
    internal uint               cb;
    internal uint               pid;
    internal uint               nDlls;
    internal InlineArray4<byte> rDlls;
}
