// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

/* [fkelava 23/03/26 00:41]
 * The game's remasters all utilize the cross-platform Phyre game engine. All assets have been processed
 * in some way, resulting in ``*.phyre`` files which are not inspectable or loadable using standard tooling.
 *
 * Fahrenheit allows custom textures to be used in ImGui flows. It would be desirable to use game assets as well,
 * but we can't directly load Phyre-processed assets. While tools such as Roelin's Asset Converter
 * (https://www.nexusmods.com/finalfantasy12/mods/288) can 'un-Phyre' files, it is ABSOLUTELY PROHIBITED
 * to distribute them with mods. However, we _can_ ask the game to load them for us at runtime! These types exist to enable this.
 *
 * Phyre types are generally self-describing. That is to say, Phyre classes have 'class descriptors', which
 * contain information about the type such as its name and layout. Their constructors, destructors, and vftables
 * all remain in the executable's RTTI metadata, and that information was used to construct these interop types.
 *
 * Once the basic set of types was reversed sufficiently, the 'global' Phyre namespace was queried for all of its class
 * descriptors, and the information they yielded was used to reconstruct the rest of the type graph. See `petypes.g.cs`.
 *
 * Sources within the executable are annotated for every attribute, and all are relative to FFX.exe.
 */

namespace Fahrenheit;

/* [fkelava 19/03/26 03:01]
 * .ctor                  -> +3A170
 * RTTI inheritance graph -> +793EE8
 */

/// <summary>Describes a unique member of a Phyre class.</summary>
/// <remarks>
///     A <see cref="PClassMember"/> can be a field, function or method. Thus, it has no <see cref="PType"/>.
///     For fields, derived types <see cref="PClassData"/> and/or <see cref="PClassDataMember"/> carry type information.
/// </remarks>
[StructLayout(LayoutKind.Sequential, Size = 0x14, Pack = 4)]
internal unsafe struct PClassMember {
    public PSimpleDoubleListElement<PClassMember> base_PSimpleDoubleListElement;
    public PClassDescriptor*                      m_classDescriptor; // name: +70EA88 -> +3A362
    public nint                                   ptr_name;          // null-terminated ANSI/UTF-8 string
    public uint                                   m_flags;           // name: +70EA80 -> +3A316

    public override string ToString() {
        return $"{(*m_classDescriptor).base_PType}::{Marshal.PtrToStringAnsi(ptr_name)}";
    }
}

/* [fkelava 17/03/26 04:13]
 * ctors of this class are all inlined; name inferred from RTTI inheritance graph of PClassDataMember
 *
 * RTTI inheritance graph -> +793E88
 */

/// <summary>
///     Describes a unique member of a Phyre class which has a concrete <see cref="PType"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 0x18, Pack = 4)]
internal unsafe struct PClassData {
    public PClassMember base_PClassMember;
    public PType*       m_type; // name: +70F994 -> +465A6

    public override string ToString() {
        return $"{*m_type} {base_PClassMember}";
    }
}

/* [fkelava 19/03/26 03:01]
 * .ctor                  -> +175410
 * RTTI inheritance graph -> +793E3C
 */

/// <summary>
///     Describes a unique field of a Phyre class which has a concrete <see cref="PType"/>, offset,
///     and may have <see cref="PAnnotation"/>s associated with it.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 0x2C, Pack = 4)]
internal struct PClassDataMember {
    public nint         vftable;
    public PClassData   base_PClassData;   // 0x4
    public PAnnotatable base_PAnnotatable; // 0x1C
    public nint         m_offset;          // name: +70F99C -> +465F2
    public nint         __0x28;

    public override string ToString() {
        return $"{base_PClassData} at offset 0x{m_offset:X}";
    }
}

/* [fkelava 17/03/26 04:13]
 * ctors of this class are all inlined; name inferred from RTTI metadata of instantiations:
 *
 * +824D90 - Phyre::PNamedSemanticDescriptorForType<PAnnotationSemantic>
 * +1758A0 - Phyre::PTypeDispenser::GetType<Phyre::PAnnotationSemantic>
 *
 * size inferred from +5758D3 - PNamedSemanticDescriptor::.ctor()
 */

[StructLayout(LayoutKind.Sequential, Size = 0x8, Pack = 4)]
internal struct PAnnotationSemantic {
    public nint _0x00;
    public nint _0x04;
}

/* [fkelava 19/03/26 03:01]
 * .ctor                  -> +1759B0
 * RTTI inheritance graph -> +79459C
 */

/// <summary>
///     Annotations are arbitrary tags attached to Phyre types.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 0x18, Pack = 4)]
internal unsafe struct PAnnotation {
    public nint                                  vftable;
    public PSimpleDoubleListElement<PAnnotation> base_PSimpleDoubleListElement;
    public PAnnotationSemantic*                  _0x0C;
    public PType*                                _0x10_type;
    public nint                                  _0x14;
}

/* [fkelava 17/03/26 04:13]
 * ctors of this class are all inlined; name inferred from RTTI inheritance graph of PClassDescriptor
 *
 * RTTI inheritance graph -> +7928DC
 */

/// <summary>
///     Indicates that a Phyre type can have <see cref="PAnnotation"/>s, arbitrary tags, associated with it.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 0x8, Pack = 4)]
internal struct PAnnotatable {
    public PSimpleDoubleListElement<PAnnotation> _0x00;
}

/* [fkelava 25/03/26 14:49]
 * ctors of this class are all inlined; name inferred from RTTI inheritance graph of PType
 *
 * RTTI inheritance graph -> +792758
 */

[StructLayout(LayoutKind.Sequential, Size = 0x8, Pack = 4)]
internal struct PHashEntryConstKey {
    public nint _0x00;
    public nint _0x04;
}

/* [fkelava 25/03/26 14:49]
 * ctors of this class are all inlined; name inferred from RTTI inheritance graph of PType
 *
 * RTTI inheritance graph -> +7926C4
 */

[StructLayout(LayoutKind.Sequential, Size = 0x14, Pack = 4)]
internal struct PHashEntryConst {
    public PRedBlackTreeNode<PUnknown> base_PRedBlackTreeNode;
    public PHashEntryConstKey          base_PHashEntryConstKey;
}

/* [fkelava 25/03/26 14:49]
 * ctors of this class are all inlined; name inferred from RTTI inheritance graph of PType
 *
 * RTTI inheritance graph -> +792720
 */

[StructLayout(LayoutKind.Sequential, Size = 0xC, Pack = 4)]
internal unsafe struct PRedBlackTreeNode<T> where T : unmanaged {

    /* [fkelava 25/03/26 16:36]
     * https://www.eecs.umich.edu/courses/eecs380/ALG/red_black.html
     * These must be a 'left', 'right', and 'parent' pointer,
     * but which is which is unknown.
     */

    public PRedBlackTreeNode<T>* _0x00;
    public PRedBlackTreeNode<T>* _0x04;
    public PRedBlackTreeNode<T>* _0x08;
}

/* [fkelava 13/03/26 21:49]
 * See Phyre::TypeDispenser::GetType<Phyre::PType>
 * (+375D0)
 *
 * RTTI inheritance graph -> +79265C
 */

/// <summary>Describes a unique type in the Phyre type system- its name, size, alignment, and more.</summary>
/// <remarks>
///     Classes have a derived <see cref="PClassDescriptor"/> instead, providing information about their members, layout, and inheritance chain.
/// </remarks>
[StructLayout(LayoutKind.Sequential, Size = 0x2C, Pack = 4)]
internal struct PType {
    public nint            vftable;
    public PHashEntryConst base_PHashEntryConst;
    public nint            type_name; // null-terminated ANSI/UTF-8 string
    public nint            type_size;
    public nint            type_alignment;
    public nint            fnptr_fixup_get;
    public nint            fnptr_fixup_resolve;

    public override string ToString() {
        return $"{Marshal.PtrToStringAnsi(type_name)} (sz 0x{type_size:X}, align 0x{type_alignment:X})";
    }
}

/* [fkelava 25/03/26 22:29]
 * .ctor -> +1763A0
 *
 * We can't model this as PInstanceList<T> containing PFreeList<T> and T*,
 * because at the time we get instance lists from a cluster we have no idea what the
 * type parameter is. Instead, use FhPInstanceListIterator<T>.
 */

/// <summary>
///     Stores instances of derived asset types contained in a <see cref="PCluster"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 0x4C, Pack = 4)]
internal unsafe struct PInstanceList {
    public PSimpleDoubleListElement<PInstanceList> base_PSimpleDoubleListElement;
    public PFreeList<PUnknown>                     _0x08_free_list;
    public PSimpleDoubleListElement<PUnknown>      _0x20;
    public PCluster*                               ptr_cluster;          // 0x28
    public PClassDescriptor*                       ptr_class_descriptor; // 0x2C
    public nint                                    _0x30;
    public nint                                    _0x34;
    public nint                                    _0x38;
    public nint                                    _0x3C;
    public nint                                    _0x40;
    public nint                                    _0x44;
    public nint                                    _0x48;

    public override string ToString() {
        return $"{nameof(PInstanceList)}<{(*ptr_class_descriptor).base_PType}>";
    }
}

/* [fkelava 17/03/26 04:13]
 * .ctor                  -> +3B0A0, +3B190
 * RTTI inheritance graph -> +792834
 */

/// <summary>Describes a Phyre class- its layout, members, and inheritance chain.</summary>
/// <remarks>
///     Basic type information such as name, size and alignment is provided by the base class <see cref="PType"/>.
/// </remarks>
[StructLayout(LayoutKind.Sequential, Size = 0x94, Pack = 4)]
internal unsafe struct PClassDescriptor {

    /* [fkelava 25/03/26 17:25]
     * Plenty of things remain unclear.
     *
     * - What are the 'default', 'validation' and 'write mask' buffers?
     * - 'Offset to base' is ambiguous; which base? Most classes have multiple inheritance.
     * - How and when does 'offset to base' vary from 'offset to base in allocated block'?
     * - Why are there two function and two field doubly-linked lists?
     */

    public PType                                      base_PType;
    public PSimpleDoubleListElement<PClassDescriptor> base_PSimpleDoubleListElement;
    public PAnnotatable                               base_PAnnotatable;
    public PNamespace*                                ptr_namespace;
    public PClassDescriptor*                          m_parent;                      // 0x40 - name: +70FB5C -> +47F02
    public PSimpleDoubleListElement<PClassDataMember> _0x44;                         // seems to be used for field members?
    public PSimpleDoubleListElement<PClassMember>     _0x4C;                         // seems to be used for function members?
    public PSimpleDoubleListElement<PClassMember>     _0x54;                         // seems to be used for function members?
    public PSimpleDoubleListElement<PClassDataMember> _0x5C;                         // seems to be used for field members?
    public nint                                       ptr_buffer_default;            // 0x64 - name: +3D600 (Phyre::PClassDescriptor::setDefaultBuffer)
    public nint                                       ptr_buffer_validation;         // 0x68 - name: +3D6A0 (Phyre::PClassDescriptor::setValidationBuffer)
    public nint                                       ptr_buffer_write_mask;         // 0x6C - name: +3D6B0 (Phyre::PClassDescriptor::setWriteMaskBuffer)
    public nint                                       _0x70;
    public nint                                       _0x74;
    public nint                                       _0x78;
    public nint                                       _0x7C;
    public nint                                       _0x80;
    public nint                                       m_offsetToParent;               // 0x84 - name: +70FB68 -> +47F50
    public nint                                       m_offsetToBase;                 // 0x88 - name: +70FB7C -> +47F9F
    public nint                                       m_offsetToBaseInAllocatedBlock; // 0x8C - name: +70FB8C -> +47FEE
    public nint                                       _0x90_flags;                    // 0x90

    public override string ToString() {
        return $"{nameof(PClassDescriptor)}<{base_PType}>";
    }
}

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PClassDescriptorDynamic> -> +891330
 */

/// <inheritdoc cref="PClassDescriptor"/>
[StructLayout(LayoutKind.Sequential, Size = 0xB8, Pack = 4)]
internal struct PClassDescriptorDynamic {
    public PClassDescriptor base_PClassDescriptor;     // 0x0
    public ushort           _0x94;
    public ushort           m_currentMemberCount;      // 0x96
    public nint             m_actualSize;              // 0x98 - name: +70FBC4, +4808C
    public PArray<byte>     m_defaultBufferStorage;    // 0x9C
    public PArray<byte>     m_validationBufferStorage; // 0xA4
    public PArray<byte>     m_writeMaskBufferStorage;  // 0xAC
    public PString          m_nameAsString;            // 0xB4
}

/* [fkelava 17/03/26 04:13]
 * ctors of this class are all inlined; name inferred from RTTI metadata of numerous derivations
 *
 * +80A160, 80B180, 80B620, 80E448, 8312F8 - Phyre::PSimpleDoubleListElement<>
 */

/// <summary>An element of a doubly-linked list of <typeparamref name="T"/>.</summary>
/// <remarks>
///     Unlike typical implementations, the absence of an element is not denoted by <c>null</c>,
///     but by the next and/or previous pointer being equal to the head pointer.
/// </remarks>
[StructLayout(LayoutKind.Sequential, Size = 0x8, Pack = 4)]
internal unsafe readonly struct PSimpleDoubleListElement<T> where T : unmanaged {
    public readonly PSimpleDoubleListElement<T>* ptr_next;
    public readonly PSimpleDoubleListElement<T>* ptr_prev;

    public readonly PSimpleDoubleListElement<T>* next(PSimpleDoubleListElement<T>* head) {
        if (ptr_next == head) return null;
        return ptr_next;
    }

    public readonly PSimpleDoubleListElement<T>* prev(PSimpleDoubleListElement<T>* head) {
        if (ptr_prev == head) return null;
        return ptr_prev;
    }
}

/* [fkelava 11/03/26 20:00]
 * .ctor -> +9C550
 * .dtor -> +9C680
 */

/// <summary>
///     A free list of <typeparamref name="T"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 0x18, Pack = 4)]
internal unsafe struct PFreeList<T> where T : unmanaged {
    public T*   _0x00_head;       // inferred from various iterator methods
    public nint _0x04_size;
    public nint _0x08_block_size;
    public nint _0x0C_alignment;
    public nint _0x10_name;       // null-terminated ANSI/UTF-8 string
    public nint _0x14;

    public override string ToString() {
        return $"{nameof(PFreeList<>)}<{Marshal.PtrToStringAnsi(_0x10_name)}>";
    }
}

/* [fkelava 11/03/26 20:00]
 * .ctor -> +3DE70
 */

/// <summary>
///     A namespace is a group of <see cref="PClassDescriptor"/>s. It can contain nested namespaces as well.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 0x1C, Pack = 4)]
internal struct PNamespace {
    public PSimpleDoubleListElement<PNamespace>       base_PSimpleDoubleListElement;
    public PSimpleDoubleListElement<PClassDescriptor> _0x08_class_descriptors;
    public nint                                       m_index;              // name: +24FE9
    public PSimpleDoubleListElement<PNamespace>       _0x14_sub_namespaces;
}

/* [fkelava 19/03/26 16:20]
 * .ctor -> +3F910
 */

/// <summary>A cluster is, roughly spoken, a Phyre asset container.</summary>
/// <remarks>
///     On disk, a cluster contains the asset in question, references to other
///     assets that must be simultaneously loaded, and more.
/// </remarks>
[StructLayout(LayoutKind.Sequential, Size = 0x50, Pack = 4)]
internal unsafe struct PCluster {
    public PNamespace                              _0x00_namespace;
    public PSimpleDoubleListElement<PInstanceList> _0x1C_instance_lists;
    public nint                                    _0x24;
    public PFreeList<PInstanceList>                _0x28_free_list_PInstanceList;
    public PWorld*                                 ptr_world; // deduced from Phyre::PWorld::addInstanceList (+433F0)
    public nint                                    _0x44;
    public PSimpleDoubleListElement<PUnknown>      _0x48;
}

/* [fkelava 19/03/26 16:20]
 * .ctor -> +1A0390
 *
 * The purpose of this class is not entirely clear, because only the constructor remains
 * but _not_ the RTTI inheritance graph which would perhaps shed more light.
 */

[StructLayout(LayoutKind.Sequential, Size = 0x34, Pack = 4)]
internal struct PNamedSemanticDescriptor {
    public PType                              base_PType;
    public PSimpleDoubleListElement<PUnknown> base_PSimpleDoubleListElement;
}

/// <summary>A stub that corresponds to no Phyre type.</summary> 
/// <remarks>Used when a target Phyre type is not known, as a placeholder.</remarks>
[StructLayout(LayoutKind.Sequential, Size = 0x80, Pack = 4)]
public struct PUnknown {
    public nint _0x00;
    public nint _0x04;
    public nint _0x08;
    public nint _0x0C;
    public nint _0x10;
    public nint _0x14;
    public nint _0x18;
    public nint _0x1C;
    public nint _0x20;
    public nint _0x24;
    public nint _0x28;
    public nint _0x2C;
    public nint _0x30;
    public nint _0x34;
    public nint _0x38;
    public nint _0x3C;
    public nint _0x40;
    public nint _0x44;
    public nint _0x48;
    public nint _0x4C;
    public nint _0x50;
    public nint _0x54;
    public nint _0x58;
    public nint _0x5C;
    public nint _0x60;
    public nint _0x64;
    public nint _0x68;
    public nint _0x6C;
    public nint _0x70;
    public nint _0x74;
    public nint _0x78;
    public nint _0x7C;
}

/* [fkelava 25/03/26 17:04]
 * Types from this point onward were obtained by querying the global PNamespace for its class descriptors.
 *
 * Call Phyre::PNamespace::GetGlobalNamespace (+3E3E0), iterate over its class descriptors, then recurse over all sub-namespaces.
 */

/* [fkelava 25/03/26 18:30]
 * PClassDescriptor<PString> -> +891048
 */

/// <summary>
///     A simple null-terminated string.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 0x4)]
internal struct PString {
    public nint m_buffer;

    public override string ToString() {
        return Marshal.PtrToStringAnsi(m_buffer) ?? string.Empty;
    }
}

/* [fkelava 25/03/26 18:30]
 * Derived from all class descriptors which are template instantiations of PArray, e.g.
 *
 * +1545010 (PArray<PPostEffectBase*>)
 * +15448A0 (PArray<PPostEffectManager>)
 * +8A9AB0  (PArray<float>) etc.
 *
 * In Ghidra each specialization is presented explicitly.
 */

[StructLayout(LayoutKind.Sequential, Size = 0x8, Pack = 0x4)]
internal unsafe struct PArray<T> where T : unmanaged {
    public uint m_count; // name: +70FD3C -> +90AE0 (and many others)
    public T*   m_els;   // name: +70FD80 -> +90BC1 (and many others)

    [UnscopedRef]
    public readonly Span<T> as_span() => new Span<T>(m_els, int.CreateChecked(m_count));
}

/* [fkelava 25/03/26 18:30]
 * Derived from all class descriptors which are template instantiations of PSharray, e.g.
 *
 * +8AB298 (PSharray<PAnimationClip*>)
 *
 * In Ghidra each specialization is presented explicitly.
 */

[StructLayout(LayoutKind.Sequential, Size = 0x8, Pack = 0x4)]
internal unsafe struct PSharray<T> where T : unmanaged {
   /* [fkelava 02/05/26 02:00]
    * In reality m_u is a union of T and T*,
    * but this cannot be modeled in a generic.
    *
    * The caller must cast to T if required.
    */

    public uint m_count;
    public T*   m_u;
}

/* [fkelava 25/03/26 21:00]
 * Derived from appearance in the PClassDescriptor of PTextureCommonBase.
 *
 * Neither the constructor nor class descriptor are known at this time.
 * Field meanings were deduced by debugger inspection.
 */

[StructLayout(LayoutKind.Sequential, Size = 0x14, Pack = 0x4)]
internal unsafe struct PTextureFormatBase {
    public nint                _0x00_format_name;
    public PTextureFormatBase* _0x04;
    public nint                _0x08_flags;
    public nint                _0x0C;
    public nint                _0x10;

    public override string ToString() {
        return $"{nameof(PTextureFormatBase)} - {Marshal.PtrToStringAnsi(_0x00_format_name)}";
    }
}

/* [fkelava 25/03/26 18:30]
 * Phyre::PRendering::PTextureCommonBase::Bind() -> +C20C0
 * PClassDescriptor<PTextureCommonBase>          -> +8A3530
 */

[StructLayout(LayoutKind.Sequential, Size = 0x1C, Pack = 0x4)]
internal unsafe struct PTextureCommonBase {
    public PTextureFormatBase* m_format;       // 0x0
    public byte                m_memoryType;   // 0x5
    public byte                _0x06;
    public byte                _0x07;
    public uint                _0x08;
    public uint                m_mipmapCount;  // 0xC
    public uint                m_maxMipLevel;  // 0x10
    public int                 m_textureFlags; // 0x14
    public uint                _0x18;
}

/* [fkelava 25/03/26 18:30]
 * Phyre::PRendering::PTexture2DBase::Bind() -> +C3510
 * PClassDescriptor<PTexture2DBase>          -> +8A3738
 */

[StructLayout(LayoutKind.Sequential, Size = 0x24, Pack = 0x4)]
internal struct PTexture2DBase {
    public PTextureCommonBase base_PTextureCommonBase;
    public uint               m_width;  // 0x1C
    public uint               m_height; // 0x20
}

/* [fkelava 25/03/26 18:30]
 * PClassDescriptor<PTexture2DD3D11> -> +8B39A0
 */

[StructLayout(LayoutKind.Sequential, Size = 0x74, Pack = 0x4)]
internal unsafe struct PTexture2DD3D11 {
    public PTexture2DBase  base_PTexture2DBase;
    public uint            _0x24;
    public ID3D11Resource* ptr_d3d_resource; // discovered by Rurusachi
    public uint            _0x2C;
    public PUnknown*       _0x30;
    public uint            _0x34;
    public uint            _0x38;
    public uint            _0x3C;
    public uint            _0x40;
    public uint            _0x44;
    public uint            _0x48;
    public uint            _0x4C;
    public uint            _0x50;
    public uint            _0x54;
    public uint            _0x58;
    public uint            _0x5C;
    public uint            _0x60;
    public uint            _0x64;
    public uint            _0x68;
    public uint            _0x6C;
    public uint            is_bound; // 0x70 - name: +196120 - PTexture2DD3D11::isBound()
}

/* [fkelava 25/03/26 18:30]
 * Phyre::PRendering::PTexture2D::Bind() -> +C34F0
 * PClassDescriptor<PTexture2D>          -> +8A37D0
 */

[StructLayout(LayoutKind.Sequential, Size = 0x74, Pack = 0x4)]
internal struct PTexture2D {
    public PTexture2DD3D11 base_PTexture2DD3D11;
}

/* [fkelava 26/04/26 14:52]
 * Phyre::PWorld::Bind()    -> +42F00
 * PClassDescriptor<PWorld> -> +890E30
 */

[StructLayout(LayoutKind.Sequential, Size = 0x10, Pack = 0x4)]
internal struct PWorld { }

/* [fkelava 26/04/26 14:52]
 * Phyre::PSerialization::PStreamFile::PStreamFile() -> +207D80
 */

/// <summary>
///     A file as seen by the game. Can be backed by either a VBF or OS handle.
/// </summary>
internal unsafe struct PStreamFile {
    public HANDLE handle_os;
    public VFile* handle_vbf;
}

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PApplication> -> +8C9D20
 */

[StructLayout(LayoutKind.Explicit, Size = 0x3A0, Pack = 0x10)]
public struct PApplication { }
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>
