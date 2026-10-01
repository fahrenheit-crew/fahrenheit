// SPDX-License-Identifier: MIT

/* [fkelava 26/04/26 15:26]
 * This file is a continuation of `petypes.cs`. See the header comment in it for the purpose.
 *
 * Unlike `petypes.cs`, which contains manually corrected and annotated versions of Phyre types,
 * these were source-generated from the class descriptors in the game binary.
 * This is so we at least have basic types with correct shapes for further debugging.
 *
 * Sources within the executable are annotated for every type, and all are relative to FFX.exe.
 *
 * Certain types are intentionally suppressed and/or corrected:
 * - template specializations which are superseded by generics, like PArray<T> and PSharray<T>
 * - D3D11 types, for which we use TerraFX definitions instead
 * - Phyre primitives (P{U}Int{8|16|32} etc.), which are replaced by C# analogues
 * - Vector and matrix primitives, replaced by System.Numerics ones
 *
 * When you are ready to 'fix' one of these types, uncomment the fields,
 * resolve the resulting errors, then move it to `petypes.cs`.
 */

namespace Fahrenheit;

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<CD3D11_BLEND_DESC> -> +8B47E8
 */

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<D3D11_RENDER_TARGET_BLEND_DESC> -> +8B45D0
 */

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<CD3D11_DEPTH_STENCIL_DESC> -> +8B43C8
 */

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<D3D11_DEPTH_STENCILOP_DESC> -> +8B4268
 */

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<CD3D11_RASTERIZER_DESC> -> +8B4008
 */

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PMatrix4> -> +894CB0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x40, Pack = 0x4)]
internal struct PMatrix4 {
    //    [FieldOffset(0x00)] public float m_elements;
}

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<Matrix3> -> +894B00
 */

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<Point3> -> +8946C0
 */

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<Quat> -> +894988
 */

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<Vector2> -> +894448
 */

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PTimer> -> +8901B0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x1, Pack = 0x1)]
internal struct PTimer {
}

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<Vector4> -> +894810
 */

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<Vector3> -> +894570
 */

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<Matrix4> -> +894BD8
 */

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PMatrix4x3> -> +894DD8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x30, Pack = 0x4)]
internal struct PMatrix4x3 {
    //    [FieldOffset(0x00)] public float m_elements;
}

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PBase> -> +8902E8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x0, Pack = 0x1)]
internal struct PBase {
}

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsCharacterCamera> -> +8BE940
 */

[StructLayout(LayoutKind.Explicit, Size = 0x200, Pack = 0x4)]
internal struct PPhysicsCharacterCamera {
    //    [FieldOffset(0x08)] public float m_collisionRadius;
    //    [FieldOffset(0x0C)] public float m_targetDistance;
    //    [FieldOffset(0x10)] public float m_targetHeight;
    //    [FieldOffset(0x14)] public float m_contactEpsilon;
    //    [FieldOffset(0x18)] public float m_minimumCameraDistance;
    //    [FieldOffset(0x1C)] public float m_smoothingRate;
    //    [FieldOffset(0x28)] public float m_springTension;
    //    [FieldOffset(0x2C)] public float m_nearCameraYAdjust;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PScriptCallbackHandler> -> +8BE2F0
 */

[StructLayout(LayoutKind.Explicit, Size = 0xC, Pack = 0x4)]
internal struct PScriptCallbackHandler {
    //    [FieldOffset(0x00)] public PString m_entryPoint;
    //    [FieldOffset(0x04)] public PTypedObject m_handler;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PDataBlockBufferD3D11> -> +8B11B8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14, Pack = 0x4)]
internal struct PDataBlockBufferD3D11 {
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PIndexDataBlockBufferD3D11> -> +8B1088
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14, Pack = 0x4)]
internal struct PIndexDataBlockBufferD3D11 {
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PDataBlockD3D11>> -> +8B1468
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PSharray<PIndexDataBlockBufferD3D11>> -> +8B1588
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PSharray<PDataBlockBufferD3D11>> -> +8B16A8
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PStreamInputLayoutD3D11> -> +8B3610
 */

[StructLayout(LayoutKind.Explicit, Size = 0xC, Pack = 0x4)]
internal struct PStreamInputLayoutD3D11 {
    //    [FieldOffset(0x00)] public PArray<PStreamInputDescD3D11> m_streams;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PStreamInputDescD3D11>> -> +8B4C58
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PStreamInputDescD3D11> -> +8B3578
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14, Pack = 0x4)]
internal struct PStreamInputDescD3D11 {
    //    [FieldOffset(0x00)] public PString m_semantic;
    //    [FieldOffset(0x04)] public PRenderDataType m_renderType;
    //    [FieldOffset(0x08)] public PUInt32 m_semanticIndex;
    //    [FieldOffset(0x0C)] public PUInt32 m_d3dFormat;
    //    [FieldOffset(0x10)] public PUInt32 m_inputSlot;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInstanceListHeader> -> +897338
 */

[StructLayout(LayoutKind.Explicit, Size = 0x24, Pack = 0x4)]
internal struct PInstanceListHeader {
    //    [FieldOffset(0x00)] public PUInt32 m_classID;
    //    [FieldOffset(0x04)] public PUInt32 m_count;
    //    [FieldOffset(0x08)] public PUInt32 m_size;
    //    [FieldOffset(0x0C)] public PUInt32 m_objectsSize;
    //    [FieldOffset(0x10)] public PUInt32 m_arraysSize;
    //    [FieldOffset(0x14)] public PUInt32 m_pointersInArraysCount;
    //    [FieldOffset(0x18)] public PUInt32 m_arrayFixupCount;
    //    [FieldOffset(0x1C)] public PUInt32 m_pointerFixupCount;
    //    [FieldOffset(0x20)] public PUInt32 m_pointerArrayFixupCount;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PClusterHeaderBase> -> +896AA8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x48, Pack = 0x4)]
internal struct PClusterHeaderBase {
    //    [FieldOffset(0x00)] public PUInt32 m_phyreMarker;
    //    [FieldOffset(0x04)] public PUInt32 m_size;
    //    [FieldOffset(0x10)] public PUInt32 m_instanceListCount;
    //    [FieldOffset(0x08)] public PUInt32 m_packedNamespaceSize;
    //    [FieldOffset(0x14)] public PUInt32 m_arrayFixupSize;
    //    [FieldOffset(0x18)] public PUInt32 m_arrayFixupCount;
    //    [FieldOffset(0x1C)] public PUInt32 m_pointerFixupSize;
    //    [FieldOffset(0x20)] public PUInt32 m_pointerFixupCount;
    //    [FieldOffset(0x24)] public PUInt32 m_pointerArrayFixupSize;
    //    [FieldOffset(0x28)] public PUInt32 m_pointerArrayFixupCount;
    //    [FieldOffset(0x2C)] public PUInt32 m_pointersInArraysCount;
    //    [FieldOffset(0x30)] public PUInt32 m_userFixupCount;
    //    [FieldOffset(0x34)] public PUInt32 m_userFixupDataSize;
    //    [FieldOffset(0x38)] public PUInt32 m_totalDataSize;
    //    [FieldOffset(0x3C)] public PUInt32 m_headerClassInstanceCount;
    //    [FieldOffset(0x40)] public PUInt32 m_headerClassChildCount;
    //    [FieldOffset(0x0C)] public PUInt32 m_platformID;
    //    [FieldOffset(0x44)] public PUInt32 m_physicsEngineID;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PClusterHeaderD3D11> -> +8970F8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x54, Pack = 0x4)]
internal struct PClusterHeaderD3D11 {
    //    [FieldOffset(0x48)] public PUInt32 m_indexBufferSize;
    //    [FieldOffset(0x4C)] public PUInt32 m_vertexBufferSize;
    //    [FieldOffset(0x50)] public PUInt32 m_maxTextureBufferSize;
}
// this class (descriptor) has a parent; PClassDescriptor<PClusterHeaderBase (sz 0x48, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PClusterHeader> -> +896B40
 */

[StructLayout(LayoutKind.Explicit, Size = 0x54, Pack = 0x4)]
internal struct PClusterHeader {
}
// this class (descriptor) has a parent; PClassDescriptor<PClusterHeaderD3D11 (sz 0x54, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PDynamicMesh> -> +89A7D0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x1C, Pack = 0x10)]
internal struct PDynamicMesh {
    //    [FieldOffset(0x08)] public PArray<PVertexStream> m_dynamicStreams;
    //    [FieldOffset(0x10)] public PArray<PDynamicSegmentDesc> m_segments;
    //    [FieldOffset(0x04)] public PMesh m_mesh;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PModifierNetworkDynamicMesh> -> +8BB300
 */

[StructLayout(LayoutKind.Explicit, Size = 0x3C, Pack = 0x4)]
internal struct PModifierNetworkDynamicMesh {
    //    [FieldOffset(0x1C)] public PArray<PUInt8 *> m_modifierNetworkInputs;
    //    [FieldOffset(0x24)] public PArray<PRenderStream> m_modifierNetworkInputRenderStreams;
    //    [FieldOffset(0x2C)] public PArray<PInt32> m_modifierNetworkOutputs;
    //    [FieldOffset(0x34)] public PArray<PModifierNetworkDynamicMeshSegment> m_segmentInputs;
}
// this class (descriptor) has a parent; PClassDescriptor<PDynamicMesh (sz 0x1C, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<ShadowDynamicMesh> -> +8CC158
 */

[StructLayout(LayoutKind.Explicit, Size = 0x1EC, Pack = 0x4)]
internal struct ShadowDynamicMesh {
}
// this class (descriptor) has a parent; PClassDescriptor<PDynamicMesh (sz 0x1C, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<BrokenScreenPolygonDynamicMesh> -> +8CC058
 */

[StructLayout(LayoutKind.Explicit, Size = 0x20, Pack = 0x4)]
internal struct BrokenScreenPolygonDynamicMesh {
}
// this class (descriptor) has a parent; PClassDescriptor<PDynamicMesh (sz 0x1C, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<RadialLineDynamicMesh> -> +8CBE88
 */

[StructLayout(LayoutKind.Explicit, Size = 0x20, Pack = 0x4)]
internal struct RadialLineDynamicMesh {
}
// this class (descriptor) has a parent; PClassDescriptor<PDynamicMesh (sz 0x1C, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<DistortionGridDynamicMesh> -> +8CBBE0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x1C, Pack = 0x4)]
internal struct DistortionGridDynamicMesh {
}
// this class (descriptor) has a parent; PClassDescriptor<PDynamicMesh (sz 0x1C, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<ClassDynamicMesh> -> +8CBA18
 */

[StructLayout(LayoutKind.Explicit, Size = 0x1C, Pack = 0x4)]
internal struct ClassDynamicMesh {
}
// this class (descriptor) has a parent; PClassDescriptor<PDynamicMesh (sz 0x1C, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PDynamicSegmentDesc>> -> +89AA10
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PDynamicSegmentDesc> -> +89A738
 */

[StructLayout(LayoutKind.Explicit, Size = 0x10, Pack = 0x4)]
internal struct PDynamicSegmentDesc {
    //    [FieldOffset(0x00)] public PUInt32 m_startStreamIndex;
    //    [FieldOffset(0x04)] public PUInt32 m_streamCount;
    //    [FieldOffset(0x08)] public PUInt32 m_elementCount;
    //    [FieldOffset(0x0C)] public PInt32 m_indexCount;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PVertexStream> -> +898730
 */

[StructLayout(LayoutKind.Explicit, Size = 0xC, Pack = 0x4)]
internal struct PVertexStream {
    //    [FieldOffset(0x08)] public PUInt8 m_type;
    //    [FieldOffset(0x00)] public PUInt32 m_offset;
    //    [FieldOffset(0x04)] public PRenderDataType m_renderDataType;
    //    [FieldOffset(0x09)] public PUInt8 m_streamSet;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PMeshSegmentBase> -> +899298
 */

[StructLayout(LayoutKind.Explicit, Size = 0x28, Pack = 0x4)]
internal struct PMeshSegmentBase {
    //    [FieldOffset(0x00)] public PUInt32 m_materialIndex;
    //    [FieldOffset(0x04)] public PInt32 m_matrixIndex;
    //    [FieldOffset(0x08)] public PArray<PSkinBoneRemap> m_skinBones;
    //    [FieldOffset(0x10)] public PInt32 m_primitiveType;
    //    [FieldOffset(0x18)] public PInt32 m_modeIndex;
    //    [FieldOffset(0x1C)] public PInt32 m_segmentRenderOrder;
    //    [FieldOffset(0x20)] public PInt32 m_clothmodelIndex;
    //    [FieldOffset(0x24)] public PInt32 m_clothIndex;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PMeshSegmentD3D11> -> +8B0FF0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x6C, Pack = 0x4)]
internal struct PMeshSegmentD3D11 {
    //    [FieldOffset(0x28)] public PArray<PDataBlockD3D11> m_vertexData;
    //    [FieldOffset(0x30)] public PIndexDataBlockD3D11 m_indexData;
}
// this class (descriptor) has a parent; PClassDescriptor<PMeshSegmentBase (sz 0x28, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PMeshSegment> -> +899330
 */

[StructLayout(LayoutKind.Explicit, Size = 0x6C, Pack = 0x4)]
internal struct PMeshSegment {
}
// this class (descriptor) has a parent; PClassDescriptor<PMeshSegmentD3D11 (sz 0x6C, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PSkinBoneRemap>> -> +899590
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PSkinBoneRemap> -> +899200
 */

[StructLayout(LayoutKind.Explicit, Size = 0x4, Pack = 0x2)]
internal struct PSkinBoneRemap {
    //    [FieldOffset(0x00)] public PUInt16 m_hierarchyMatrixIndex;
    //    [FieldOffset(0x02)] public PUInt16 m_skeletonMatrixIndex;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PMesh> -> +899C80
 */

[StructLayout(LayoutKind.Explicit, Size = 0x38, Pack = 0x4)]
internal struct PMesh {
    //    [FieldOffset(0x00)] public PArray<PMeshSegment> m_meshSegments;
    //    [FieldOffset(0x08)] public PArray<PMatrix4> m_skeletonMatrices;
    //    [FieldOffset(0x10)] public PArray<PSkeletonJointBounds> m_skeletonBounds;
    //    [FieldOffset(0x18)] public PArray<PMatrix4> m_defaultPose;
    //    [FieldOffset(0x20)] public PArray<PString> m_matrixNames;
    //    [FieldOffset(0x28)] public PArray<PInt32> m_matrixParents;
    //    [FieldOffset(0x30)] public PMaterialSet m_defaultMaterials;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PSkeletonJointBounds>> -> +89A2D0
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PMeshSegment>> -> +89A3F0
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PSkeletonJointBounds> -> +899BE8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x20, Pack = 0x4)]
internal struct PSkeletonJointBounds {
    //    [FieldOffset(0x00)] public float m_min;
    //    [FieldOffset(0x10)] public float m_size;
    //    [FieldOffset(0x0C)] public PUInt32 m_hierarchyMatrixIndex;
    //    [FieldOffset(0x1C)] public PUInt32 m_pad;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShape> -> +89AB98
 */

[StructLayout(LayoutKind.Explicit, Size = 0x1C, Pack = 0x4)]
internal struct PShape {
    //    [FieldOffset(0x04)] public PArray<PUInt8,4> m_indices;
    //    [FieldOffset(0x19)] public PUInt8 m_indexFormat;
    //    [FieldOffset(0x00)] public PUInt32 m_indexCount;
    //    [FieldOffset(0x10)] public PArray<PUInt8,4> m_vertexData;
    //    [FieldOffset(0x18)] public PUInt8 m_vertexFormat;
    //    [FieldOffset(0x0C)] public PUInt32 m_vertexCount;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PUInt8,4>> -> +89AD48
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PMaterialSet> -> +8998E8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x8, Pack = 0x4)]
internal struct PMaterialSet {
    //    [FieldOffset(0x00)] public PSharray<PMaterial *> m_materials;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PSharray<PMaterial *>> -> +899A30
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PDynamicDataBlock> -> +898D18
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PDynamicDataBlock {
    //    [FieldOffset(0x00)] public PUInt32 m_stride;
    //    [FieldOffset(0x04)] public PUInt32 m_elementCount;
    //    [FieldOffset(0x08)] public PArray<PVertexStream> m_streams;
    //    [FieldOffset(0x10)] public PArray<PUInt8> m_data;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PIndexDataBlockBase> -> +898EC8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14, Pack = 0x4)]
internal struct PIndexDataBlockBase {
    //    [FieldOffset(0x0D)] public PUInt8 m_memoryType;
    //    [FieldOffset(0x08)] public PUInt32 m_elementCount;
    //    [FieldOffset(0x0C)] public PUInt8 m_type;
    //    [FieldOffset(0x00)] public PUInt32 m_minimumIndex;
    //    [FieldOffset(0x04)] public PUInt32 m_maximumIndex;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PIndexDataBlockD3D11> -> +8B1120
 */

[StructLayout(LayoutKind.Explicit, Size = 0x3C, Pack = 0x4)]
internal struct PIndexDataBlockD3D11 {
    //    [FieldOffset(0x14)] public PSharray<PIndexDataBlockBufferD3D11> m_buffers;
    //    [FieldOffset(0x34)] public PUInt32 m_dataSize;
    //    [FieldOffset(0x2C)] public PUInt32 m_offsetInIndexBuffer;
}
// this class (descriptor) has a parent; PClassDescriptor<PIndexDataBlockBase (sz 0x14, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PIndexDataBlock> -> +898F60
 */

[StructLayout(LayoutKind.Explicit, Size = 0x3C, Pack = 0x4)]
internal struct PIndexDataBlock {
}
// this class (descriptor) has a parent; PClassDescriptor<PIndexDataBlockD3D11 (sz 0x3C, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PDataBlockBase> -> +8988B0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PDataBlockBase {
    //    [FieldOffset(0x15)] public PUInt8 m_memoryType;
    //    [FieldOffset(0x00)] public PUInt32 m_stride;
    //    [FieldOffset(0x04)] public PUInt32 m_elementCount;
    //    [FieldOffset(0x08)] public PArray<PVertexStream> m_streams;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PDataBlockD3D11> -> +8B1250
 */

[StructLayout(LayoutKind.Explicit, Size = 0x40, Pack = 0x4)]
internal struct PDataBlockD3D11 {
    //    [FieldOffset(0x18)] public PSharray<PDataBlockBufferD3D11> m_buffers;
    //    [FieldOffset(0x38)] public PUInt32 m_dataSize;
    //    [FieldOffset(0x30)] public PUInt32 m_offsetInVertexBuffer;
}
// this class (descriptor) has a parent; PClassDescriptor<PDataBlockBase (sz 0x18, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PDataBlock> -> +898948
 */

[StructLayout(LayoutKind.Explicit, Size = 0x40, Pack = 0x4)]
internal struct PDataBlock {
}
// this class (descriptor) has a parent; PClassDescriptor<PDataBlockD3D11 (sz 0x40, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PVertexStream>> -> +898A98
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PSharray<PMeshInstanceSegmentStreamBinding *>> -> +8A79A8
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PIndirectArgsBufferBase> -> +8A9038
 */

[StructLayout(LayoutKind.Explicit, Size = 0x8, Pack = 0x4)]
internal struct PIndirectArgsBufferBase {
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PIndirectArgsBufferD3D11> -> +8B3D48
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PIndirectArgsBufferD3D11 {
}
// this class (descriptor) has a parent; PClassDescriptor<PIndirectArgsBufferBase (sz 0x8, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PIndirectArgsBuffer> -> +8A90D0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PIndirectArgsBuffer {
}
// this class (descriptor) has a parent; PClassDescriptor<PIndirectArgsBufferD3D11 (sz 0x18, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PStructuredBufferBase> -> +8A8D30
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14, Pack = 0x4)]
internal struct PStructuredBufferBase {
    //    [FieldOffset(0x00)] public PUInt32 m_elementCount;
    //    [FieldOffset(0x04)] public PUInt32 m_elementSize;
    //    [FieldOffset(0x08)] public PUInt32 m_flags;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PStructuredBufferD3D11> -> +8B3CB0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x48, Pack = 0x4)]
internal struct PStructuredBufferD3D11 {
}
// this class (descriptor) has a parent; PClassDescriptor<PStructuredBufferBase (sz 0x14, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PStructuredBuffer> -> +8A8DC8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x48, Pack = 0x4)]
internal struct PStructuredBuffer {
}
// this class (descriptor) has a parent; PClassDescriptor<PStructuredBufferD3D11 (sz 0x48, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PLight> -> +89AF80
 */

[StructLayout(LayoutKind.Explicit, Size = 0x34, Pack = 0x4)]
internal struct PLight {
    //    [FieldOffset(0x00)] public Vector4 m_color;
    //    [FieldOffset(0x10)] public PWorldMatrix m_localToWorldMatrix;
    //    [FieldOffset(0x14)] public PShadowCaster m_shadowCaster;
    //    [FieldOffset(0x18)] public PLightType m_lightType;
    //    [FieldOffset(0x1C)] public float m_innerConeAngle;
    //    [FieldOffset(0x20)] public float m_outerConeAngle;
    //    [FieldOffset(0x24)] public float m_intensity;
    //    [FieldOffset(0x28)] public float m_innerRange;
    //    [FieldOffset(0x2C)] public float m_outerRange;
    //    [FieldOffset(0x30)] public float m_scatteringIntensity;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShadowCaster> -> +8A44C8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x68, Pack = 0x4)]
internal struct PShadowCaster {
    //    [FieldOffset(0x08)] public PArray<PShadowSplit> m_splits;
    //    [FieldOffset(0x04)] public PShadowCasterType m_shadowCasterType;
    //    [FieldOffset(0x40)] public float m_zbias;
    //    [FieldOffset(0x00)] public PLight m_light;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PShadowSplit>> -> +8A4680
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShadowSplit> -> +8A4430
 */

[StructLayout(LayoutKind.Explicit, Size = 0xE0, Pack = 0x4)]
internal struct PShadowSplit {
    //    [FieldOffset(0xC4)] public float m_farPlane;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PRenderTargetBase> -> +8A4168
 */

[StructLayout(LayoutKind.Explicit, Size = 0x10, Pack = 0x10)]
internal struct PRenderTargetBase {
    //    [FieldOffset(0x05)] public PUInt8 m_msaaType;
    //    [FieldOffset(0x08)] public PInt32 m_renderTargetFlags;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PRenderTargetD3D11> -> +8B3B68
 */

[StructLayout(LayoutKind.Explicit, Size = 0x28, Pack = 0x10)]
internal struct PRenderTargetD3D11 {
}
// this class (descriptor) has a parent; PClassDescriptor<PRenderTargetBase (sz 0x10, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PRenderTarget> -> +8A4200
 */

[StructLayout(LayoutKind.Explicit, Size = 0x28, Pack = 0x4)]
internal struct PRenderTarget {
}
// this class (descriptor) has a parent; PClassDescriptor<PRenderTargetD3D11 (sz 0x28, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PMeshInstanceBounds> -> +8A89B8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x24, Pack = 0x4)]
internal struct PMeshInstanceBounds {
    //    [FieldOffset(0x1C)] public PMeshInstance m_meshInstance;
    //    [FieldOffset(0x0C)] public PWorldMatrix m_worldMatrix;
    //    [FieldOffset(0x20)] public PInt32 m_render_order;
    //    [FieldOffset(0x00)] public float m_min;
    //    [FieldOffset(0x10)] public float m_size;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PModifierNetworkInstance *>> -> +8A80F0
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PDynamicMeshInstance> -> +8A7F28
 */

[StructLayout(LayoutKind.Explicit, Size = 0xC, Pack = 0x4)]
internal struct PDynamicMeshInstance {
    //    [FieldOffset(0x04)] public PDynamicMesh m_dynamicMesh;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PModifierNetworkDynamicMeshInstance> -> +8A8020
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14, Pack = 0x4)]
internal struct PModifierNetworkDynamicMeshInstance {
    //    [FieldOffset(0x0C)] public PArray<PModifierNetworkInstance *> m_modifierNetworkInstances;
}
// this class (descriptor) has a parent; PClassDescriptor<PDynamicMeshInstance (sz 0xC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<BrokenScreenPolygonDynamicMeshInstance> -> +8CBF88
 */

[StructLayout(LayoutKind.Explicit, Size = 0xC, Pack = 0x4)]
internal struct BrokenScreenPolygonDynamicMeshInstance {
}
// this class (descriptor) has a parent; PClassDescriptor<PDynamicMeshInstance (sz 0xC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<RadialLineDynamicMeshInstance> -> +8CBDB8
 */

[StructLayout(LayoutKind.Explicit, Size = 0xC, Pack = 0x4)]
internal struct RadialLineDynamicMeshInstance {
}
// this class (descriptor) has a parent; PClassDescriptor<PDynamicMeshInstance (sz 0xC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<DistortionGridDynamicMeshInstance> -> +8CBCE0
 */

[StructLayout(LayoutKind.Explicit, Size = 0xC, Pack = 0x4)]
internal struct DistortionGridDynamicMeshInstance {
}
// this class (descriptor) has a parent; PClassDescriptor<PDynamicMeshInstance (sz 0xC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<ClassDynamicMeshInstance> -> +8CBB10
 */

[StructLayout(LayoutKind.Explicit, Size = 0x10, Pack = 0x4)]
internal struct ClassDynamicMeshInstance {
}
// this class (descriptor) has a parent; PClassDescriptor<PDynamicMeshInstance (sz 0xC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderStreamDefinition> -> +89C7E0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x10, Pack = 0x4)]
internal struct PShaderStreamDefinition {
    //    [FieldOffset(0x04)] public PString m_name;
    //    [FieldOffset(0x0E)] public PUInt8 m_dataType;
    //    [FieldOffset(0x00)] public PRenderDataType m_renderType;
    //    [FieldOffset(0x08)] public PShaderParameterCaptureBufferLocationSize m_bufferLoc;
    //    [FieldOffset(0x0C)] public PUInt16 m_nameHash;
    //    [FieldOffset(0x0F)] public PUInt8 m_index;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderProgramBase> -> +89C748
 */

[StructLayout(LayoutKind.Explicit, Size = 0x1, Pack = 0x1)]
internal struct PShaderProgramBase {
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderProgramD3D11> -> +8B36A8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x4E0, Pack = 0x4)]
internal struct PShaderProgramD3D11 {
    //    [FieldOffset(0x0C)] public PArray<PUInt8> m_compiledCode;
    //    [FieldOffset(0x14)] public PUInt32 m_constantBufferSize;
    //    [FieldOffset(0x18)] public PUInt32 m_globalConstantBufferIndex;
    //    [FieldOffset(0x4DC)] public PUInt32 m_shaderProfile;
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderProgramBase (sz 0x1, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderComputeProgramD3D11> -> +8B3908
 */

[StructLayout(LayoutKind.Explicit, Size = 0x4E4, Pack = 0x4)]
internal struct PShaderComputeProgramD3D11 {
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderProgramD3D11 (sz 0x4E0, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderComputeProgram> -> +89CA40
 */

[StructLayout(LayoutKind.Explicit, Size = 0x4E4, Pack = 0x4)]
internal struct PShaderComputeProgram {
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderComputeProgramD3D11 (sz 0x4E4, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderGeometryProgramD3D11> -> +8B3870
 */

[StructLayout(LayoutKind.Explicit, Size = 0x4E4, Pack = 0x4)]
internal struct PShaderGeometryProgramD3D11 {
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderProgramD3D11 (sz 0x4E0, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderGeometryProgram> -> +89C9A8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x4E4, Pack = 0x4)]
internal struct PShaderGeometryProgram {
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderGeometryProgramD3D11 (sz 0x4E4, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderFragmentProgramD3D11> -> +8B37D8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x4E4, Pack = 0x4)]
internal struct PShaderFragmentProgramD3D11 {
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderProgramD3D11 (sz 0x4E0, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderFragmentProgram> -> +89C910
 */

[StructLayout(LayoutKind.Explicit, Size = 0x4E4, Pack = 0x4)]
internal struct PShaderFragmentProgram {
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderFragmentProgramD3D11 (sz 0x4E4, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderVertexProgramD3D11> -> +8B3740
 */

[StructLayout(LayoutKind.Explicit, Size = 0x4F0, Pack = 0x4)]
internal struct PShaderVertexProgramD3D11 {
    //    [FieldOffset(0x4E0)] public PStreamInputLayoutD3D11 m_inputLayout;
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderProgramD3D11 (sz 0x4E0, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderVertexProgram> -> +89C878
 */

[StructLayout(LayoutKind.Explicit, Size = 0x4F0, Pack = 0x4)]
internal struct PShaderVertexProgram {
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderVertexProgramD3D11 (sz 0x4F0, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderProgramParamsAndStreams> -> +89C6B0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x10, Pack = 0x4)]
internal struct PShaderProgramParamsAndStreams {
    //    [FieldOffset(0x00)] public PArray<PShaderStreamDefinition> m_streamDefinitions;
    //    [FieldOffset(0x08)] public PArray<PShaderParameterDefinition> m_parameterDefinitions;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderSource> -> +89C618
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14, Pack = 0x4)]
internal struct PShaderSource {
    //    [FieldOffset(0x00)] public PString m_code;
    //    [FieldOffset(0x04)] public PString m_entry;
    //    [FieldOffset(0x08)] public PArray<PString> m_compileOptions;
    //    [FieldOffset(0x10)] public PUInt32 m_profile;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderParameterCaptureBufferTextureBase> -> +89BDC8
 */

[StructLayout(LayoutKind.Explicit, Size = 0xC, Pack = 0x4)]
internal struct PShaderParameterCaptureBufferTextureBase {
    //    [FieldOffset(0x00)] public PUInt32 m_parameterType;
    //    [FieldOffset(0x08)] public PSamplerState m_samplerState;
    //    [FieldOffset(0x04)] public PUInt32 m_textureBufferIndex;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderParameterCaptureBufferRWTexture3D> -> +89C580
 */

[StructLayout(LayoutKind.Explicit, Size = 0x10, Pack = 0x4)]
internal struct PShaderParameterCaptureBufferRWTexture3D {
    //    [FieldOffset(0x0C)] public PTexture3D m_texture;
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderParameterCaptureBufferTextureBase (sz 0xC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderParameterCaptureBufferRWTexture2D> -> +89C4E8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x10, Pack = 0x4)]
internal struct PShaderParameterCaptureBufferRWTexture2D {
    //    [FieldOffset(0x0C)] public PTexture2D m_texture;
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderParameterCaptureBufferTextureBase (sz 0xC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderParameterCaptureBufferIndexDataBlock> -> +89C450
 */

[StructLayout(LayoutKind.Explicit, Size = 0x10, Pack = 0x4)]
internal struct PShaderParameterCaptureBufferIndexDataBlock {
    //    [FieldOffset(0x0C)] public PIndexDataBlock m_indexDataBlock;
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderParameterCaptureBufferTextureBase (sz 0xC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderParameterCaptureBufferDataBlock> -> +89C3B8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x10, Pack = 0x4)]
internal struct PShaderParameterCaptureBufferDataBlock {
    //    [FieldOffset(0x0C)] public PDataBlock m_dataBlock;
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderParameterCaptureBufferTextureBase (sz 0xC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderParameterCaptureConstantBuffer> -> +89BE60
 */

[StructLayout(LayoutKind.Explicit, Size = 0x10, Pack = 0x4)]
internal struct PShaderParameterCaptureConstantBuffer {
    //    [FieldOffset(0x0C)] public PConstantBuffer m_constantBuffer;
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderParameterCaptureBufferTextureBase (sz 0xC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderParameterCaptureBufferRWByteAddressBuffer> -> +89C320
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PShaderParameterCaptureBufferRWByteAddressBuffer {
    //    [FieldOffset(0x0C)] public PStructuredBuffer m_rwStructuredBuffer;
    //    [FieldOffset(0x10)] public PDataBlock m_rwDataBlock;
    //    [FieldOffset(0x14)] public PIndirectArgsBuffer m_rwIndirectArgsBuffer;
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderParameterCaptureBufferTextureBase (sz 0xC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderParameterCaptureBufferByteAddressBuffer> -> +89C288
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14, Pack = 0x4)]
internal struct PShaderParameterCaptureBufferByteAddressBuffer {
    //    [FieldOffset(0x0C)] public PStructuredBuffer m_structuredBuffer;
    //    [FieldOffset(0x10)] public PDataBlock m_dataBlock;
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderParameterCaptureBufferTextureBase (sz 0xC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderParameterCaptureBufferRWStructuredBuffer> -> +89C1F0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14, Pack = 0x4)]
internal struct PShaderParameterCaptureBufferRWStructuredBuffer {
    //    [FieldOffset(0x0C)] public PStructuredBuffer m_rwStructuredBuffer;
    //    [FieldOffset(0x10)] public PDataBlock m_rwDataBlock;
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderParameterCaptureBufferTextureBase (sz 0xC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderParameterCaptureBufferStructuredBuffer> -> +89C158
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14, Pack = 0x4)]
internal struct PShaderParameterCaptureBufferStructuredBuffer {
    //    [FieldOffset(0x0C)] public PStructuredBuffer m_structuredBuffer;
    //    [FieldOffset(0x10)] public PDataBlock m_dataBlock;
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderParameterCaptureBufferTextureBase (sz 0xC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderParameterCaptureBufferSampler> -> +89C0C0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x10, Pack = 0x4)]
internal struct PShaderParameterCaptureBufferSampler {
    //    [FieldOffset(0x0C)] public PTextureCommonBase m_unusedPointer;
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderParameterCaptureBufferTextureBase (sz 0xC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderParameterCaptureBufferTextureCubeMap> -> +89C028
 */

[StructLayout(LayoutKind.Explicit, Size = 0x10, Pack = 0x4)]
internal struct PShaderParameterCaptureBufferTextureCubeMap {
    //    [FieldOffset(0x0C)] public PTextureCubeMap m_texture;
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderParameterCaptureBufferTextureBase (sz 0xC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderParameterCaptureBufferTexture3D> -> +89BF90
 */

[StructLayout(LayoutKind.Explicit, Size = 0x10, Pack = 0x4)]
internal struct PShaderParameterCaptureBufferTexture3D {
    //    [FieldOffset(0x0C)] public PTexture3D m_texture;
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderParameterCaptureBufferTextureBase (sz 0xC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderParameterCaptureBufferTexture2D> -> +89BEF8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x10, Pack = 0x4)]
internal struct PShaderParameterCaptureBufferTexture2D {
    //    [FieldOffset(0x0C)] public PTexture2D m_texture;
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderParameterCaptureBufferTextureBase (sz 0xC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderParameterCaptureBufferStream> -> +89BD30
 */

[StructLayout(LayoutKind.Explicit, Size = 0xC, Pack = 0x4)]
internal struct PShaderParameterCaptureBufferStream {
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PShaderParameterCaptureBufferLocationTypeConstantBuffer>> -> +8A19E0
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PShaderParameterCaptureBufferLocationType>> -> +8A18C0
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderPassParameterLocationTypesBase> -> +8A1338
 */

[StructLayout(LayoutKind.Explicit, Size = 0x10, Pack = 0x2)]
internal struct PShaderPassParameterLocationTypesBase {
    //    [FieldOffset(0x00)] public PUInt16 m_parameterStart;
    //    [FieldOffset(0x08)] public PUInt16 m_parameterCount;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderPassParameterLocationTypesConstantBuffer> -> +8A1468
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PShaderPassParameterLocationTypesConstantBuffer {
    //    [FieldOffset(0x10)] public PArray<PShaderParameterCaptureBufferLocationTypeConstantBuffer> m_parameterLocations;
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderPassParameterLocationTypesBase (sz 0x10, align 0x2)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderPassParameterLocationTypes> -> +8A13D0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PShaderPassParameterLocationTypes {
    //    [FieldOffset(0x10)] public PArray<PShaderParameterCaptureBufferLocationType> m_parameterLocations;
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderPassParameterLocationTypesBase (sz 0x10, align 0x2)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderParameterDefinition> -> +89B980
 */

[StructLayout(LayoutKind.Explicit, Size = 0x10, Pack = 0x4)]
internal struct PShaderParameterDefinition {
    //    [FieldOffset(0x04)] public PString m_name;
    //    [FieldOffset(0x02)] public PUInt8 m_parameterType;
    //    [FieldOffset(0x03)] public PUInt8 m_dataType;
    //    [FieldOffset(0x00)] public PUInt16 m_arrayElementCount;
    //    [FieldOffset(0x08)] public PShaderParameterCaptureBufferLocationSize m_bufferLoc;
    //    [FieldOffset(0x0C)] public PUInt32 m_constantBufferLocation;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderParameterCaptureBufferLocation> -> +89B720
 */

[StructLayout(LayoutKind.Explicit, Size = 0x2, Pack = 0x2)]
internal struct PShaderParameterCaptureBufferLocation {
    //    [FieldOffset(0x00)] public PUInt16 m_offset;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderParameterCaptureBufferLocationTypeConstantBuffer> -> +89B8E8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x10, Pack = 0x4)]
internal struct PShaderParameterCaptureBufferLocationTypeConstantBuffer {
    //    [FieldOffset(0x04)] public PUInt32 m_constantBufferLocation;
    //    [FieldOffset(0x08)] public PUInt32 m_size;
    //    [FieldOffset(0x0C)] public PUInt8 m_type;
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderParameterCaptureBufferLocation (sz 0x2, align 0x2)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderParameterCaptureBufferLocationType> -> +89B850
 */

[StructLayout(LayoutKind.Explicit, Size = 0x4, Pack = 0x2)]
internal struct PShaderParameterCaptureBufferLocationType {
    //    [FieldOffset(0x02)] public PUInt8 m_type;
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderParameterCaptureBufferLocation (sz 0x2, align 0x2)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderParameterCaptureBufferLocationSize> -> +89B7B8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x4, Pack = 0x2)]
internal struct PShaderParameterCaptureBufferLocationSize {
    //    [FieldOffset(0x02)] public PUInt16 m_size;
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderParameterCaptureBufferLocation (sz 0x2, align 0x2)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PSamplerStateBase> -> +8A2898
 */

[StructLayout(LayoutKind.Explicit, Size = 0x20, Pack = 0x4)]
internal struct PSamplerStateBase {
    //    [FieldOffset(0x00)] public PUInt8 m_minFilter;
    //    [FieldOffset(0x01)] public PUInt8 m_magFilter;
    //    [FieldOffset(0x02)] public PUInt8 m_wrapS;
    //    [FieldOffset(0x03)] public PUInt8 m_wrapT;
    //    [FieldOffset(0x04)] public PUInt8 m_wrapR;
    //    [FieldOffset(0x08)] public float m_lodBias;
    //    [FieldOffset(0x0C)] public float m_maxAnisotropy;
    //    [FieldOffset(0x10)] public PUInt32 m_borderColor;
    //    [FieldOffset(0x14)] public PUInt32 m_baseLevel;
    //    [FieldOffset(0x18)] public PUInt32 m_maxLevel;
    //    [FieldOffset(0x1C)] public PUInt32 m_flags;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PSamplerStateD3D11> -> +8B3C00
 */

[StructLayout(LayoutKind.Explicit, Size = 0x24, Pack = 0x4)]
internal struct PSamplerStateD3D11 {
}
// this class (descriptor) has a parent; PClassDescriptor<PSamplerStateBase (sz 0x20, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PSamplerState> -> +8A2930
 */

[StructLayout(LayoutKind.Explicit, Size = 0x24, Pack = 0x4)]
internal struct PSamplerState {
}
// this class (descriptor) has a parent; PClassDescriptor<PSamplerStateD3D11 (sz 0x24, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PTextureCommonBase> -> +8A3530
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PTextureCubeMapBase> -> +8A3E58
 */

[StructLayout(LayoutKind.Explicit, Size = 0x20, Pack = 0x4)]
internal struct PTextureCubeMapBase {
    //    [FieldOffset(0x1C)] public PUInt32 m_size;
}
// this class (descriptor) has a parent; PClassDescriptor<PTextureCommonBase (sz 0x1C, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PTextureCubeMapD3D11> -> +8B3AD0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x68, Pack = 0x4)]
internal struct PTextureCubeMapD3D11 {
}
// this class (descriptor) has a parent; PClassDescriptor<PTextureCubeMapBase (sz 0x20, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PTextureCubeMap> -> +8A3EF0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x68, Pack = 0x4)]
internal struct PTextureCubeMap {
}
// this class (descriptor) has a parent; PClassDescriptor<PTextureCubeMapD3D11 (sz 0x68, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PTexture3DBase> -> +8A3AC8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x28, Pack = 0x4)]
internal struct PTexture3DBase {
    //    [FieldOffset(0x1C)] public PUInt32 m_width;
    //    [FieldOffset(0x20)] public PUInt32 m_height;
    //    [FieldOffset(0x24)] public PUInt32 m_depth;
}
// this class (descriptor) has a parent; PClassDescriptor<PTextureCommonBase (sz 0x1C, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PTexture3DD3D11> -> +8B3A38
 */

[StructLayout(LayoutKind.Explicit, Size = 0x68, Pack = 0x4)]
internal struct PTexture3DD3D11 {
}
// this class (descriptor) has a parent; PClassDescriptor<PTexture3DBase (sz 0x28, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PTexture3D> -> +8A3B60
 */

[StructLayout(LayoutKind.Explicit, Size = 0x68, Pack = 0x4)]
internal struct PTexture3D {
}
// this class (descriptor) has a parent; PClassDescriptor<PTexture3DD3D11 (sz 0x68, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PTexture2DBase> -> +8A3738
 */

// this class (descriptor) has a parent; PClassDescriptor<PTextureCommonBase (sz 0x1C, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PTexture2DD3D11> -> +8B39A0
 */

// this class (descriptor) has a parent; PClassDescriptor<PTexture2DBase (sz 0x24, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PTexture2D> -> +8A37D0
 */

// this class (descriptor) has a parent; PClassDescriptor<PTexture2DD3D11 (sz 0x74, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShader> -> +8A2548
 */

[StructLayout(LayoutKind.Explicit, Size = 0x20, Pack = 0x4)]
internal struct PShader {
    //    [FieldOffset(0x04)] public PArray<PShaderPass> m_passes;
    //    [FieldOffset(0x0C)] public PArray<PShaderParameterDefinition> m_parameterDefinitionsForPasses;
    //    [FieldOffset(0x14)] public PArray<PShaderStreamDefinition> m_streamDefinitionsForPasses;
    //    [FieldOffset(0x1C)] public PUInt32 m_parameterBufferSize;
    //    [FieldOffset(0x00)] public PUInt32 m_parameterBufferFrequenciesRequired;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PShaderStreamDefinition>> -> +89D200
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PShaderPass>> -> +8A26D8
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderPassStateBase> -> +8A1630
 */

[StructLayout(LayoutKind.Explicit, Size = 0x4, Pack = 0x4)]
internal struct PShaderPassStateBase {
    //    [FieldOffset(0x00)] public PUInt32 m_importantState;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderPassStateD3D11> -> +8B34E0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x17C, Pack = 0x4)]
internal struct PShaderPassStateD3D11 {
    //    [FieldOffset(0x04)] public CD3D11_RASTERIZER_DESC m_rasterDesc;
    //    [FieldOffset(0x2C)] public CD3D11_DEPTH_STENCIL_DESC m_depthDesc;
    //    [FieldOffset(0x60)] public CD3D11_BLEND_DESC m_blendDesc;
    //    [FieldOffset(0x178)] public PUInt8 m_stencilRef;
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderPassStateBase (sz 0x4, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderPassBase> -> +8A1500
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PShaderPassBase {
    //    [FieldOffset(0x00)] public PShaderVertexProgram m_vertexProgram;
    //    [FieldOffset(0x04)] public PShaderFragmentProgram m_fragmentProgram;
    //    [FieldOffset(0x08)] public PShaderGeometryProgram m_geometryProgram;
    //    [FieldOffset(0x0C)] public PShaderComputeProgram m_computeProgram;
    //    [FieldOffset(0x10)] public PArray<PShaderParameterCaptureBufferLocation> m_streamLocations;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderPassD3D11> -> +8B3448
 */

[StructLayout(LayoutKind.Explicit, Size = 0x254, Pack = 0x4)]
internal struct PShaderPassD3D11 {
    //    [FieldOffset(0x18)] public PShaderPassStateD3D11 m_state;
    //    [FieldOffset(0x194)] public PShaderPassParameterLocationTypesConstantBuffer m_vertexParameterLocation;
    //    [FieldOffset(0x1AC)] public PShaderPassParameterLocationTypesConstantBuffer m_fragmentParameterLocation;
    //    [FieldOffset(0x1C4)] public PShaderPassParameterLocationTypesConstantBuffer m_geometryParameterLocation;
    //    [FieldOffset(0x1DC)] public PShaderPassParameterLocationTypesConstantBuffer m_computeParameterLocation;
    //    [FieldOffset(0x1F4)] public PShaderPassParameterLocationTypesConstantBuffer m_vertexTexParameterLocation;
    //    [FieldOffset(0x20C)] public PShaderPassParameterLocationTypesConstantBuffer m_fragmentTexParameterLocation;
    //    [FieldOffset(0x224)] public PShaderPassParameterLocationTypesConstantBuffer m_geometryTexParameterLocation;
    //    [FieldOffset(0x23C)] public PShaderPassParameterLocationTypesConstantBuffer m_computeTexParameterLocation;
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderPassBase (sz 0x18, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderPass> -> +8A1598
 */

[StructLayout(LayoutKind.Explicit, Size = 0x254, Pack = 0x4)]
internal struct PShaderPass {
}
// this class (descriptor) has a parent; PClassDescriptor<PShaderPassD3D11 (sz 0x254, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PShaderParameterCaptureBufferLocation>> -> +8A1B00
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PContextVariantFoldingTable> -> +8A5660
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14, Pack = 0x4)]
internal struct PContextVariantFoldingTable {
    //    [FieldOffset(0x00)] public PUInt32 m_contextVariantIndex;
    //    [FieldOffset(0x04)] public PUInt32 m_contextVariantVpIndex;
    //    [FieldOffset(0x08)] public PUInt32 m_contextVariantFpIndex;
    //    [FieldOffset(0x0C)] public PUInt32 m_contextVariantGsIndex;
    //    [FieldOffset(0x10)] public PUInt32 m_contextVariantCsIndex;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PShaderPassInfo> -> +8A55C8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x20, Pack = 0x4)]
internal struct PShaderPassInfo {
    //    [FieldOffset(0x00)] public PString m_vertexEntryPoint;
    //    [FieldOffset(0x10)] public PUInt32 m_vertexProfile;
    //    [FieldOffset(0x04)] public PString m_fragmentEntryPoint;
    //    [FieldOffset(0x14)] public PUInt32 m_fragmentProfile;
    //    [FieldOffset(0x08)] public PString m_geometryEntryPoint;
    //    [FieldOffset(0x18)] public PUInt32 m_geometryProfile;
    //    [FieldOffset(0x0C)] public PString m_computeEntryPoint;
    //    [FieldOffset(0x1C)] public PUInt32 m_computeProfile;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PSceneRenderPass> -> +8A5530
 */

[StructLayout(LayoutKind.Explicit, Size = 0x28, Pack = 0x4)]
internal struct PSceneRenderPass {
    //    [FieldOffset(0x00)] public PSceneRenderPassType m_passType;
    //    [FieldOffset(0x04)] public PArray<PShader> m_shaders;
    //    [FieldOffset(0x0C)] public PArray<PShaderPassInfo> m_entryPoints;
    //    [FieldOffset(0x14)] public PArray<PContextVariantFoldingTable> m_variantsFoldingTable;
    //    [FieldOffset(0x1C)] public PArray<PString> m_platforms;
    //    [FieldOffset(0x24)] public bool m_platformsAreInclude;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PContextVariantFoldingTable>> -> +8A5AA0
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PShaderPassInfo>> -> +8A5BC0
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PShader>> -> +8A5CE0
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PEffectVariant> -> +8A5FF0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x34, Pack = 0x4)]
internal struct PEffectVariant {
    //    [FieldOffset(0x00)] public PEffect m_effect;
    //    [FieldOffset(0x04)] public PArray<PMaterialSwitch> m_switches;
    //    [FieldOffset(0x0C)] public PArray<PSceneRenderPass> m_sceneRenderPasses;
    //    [FieldOffset(0x14)] public PArray<PSceneRenderPass *> m_sceneRenderPassLookup;
    //    [FieldOffset(0x1C)] public PUInt16 m_largestShaderPassCount;
    //    [FieldOffset(0x20)] public PArray<PShaderParameterDefinition> m_tweakableShaderParameterDefinitions;
    //    [FieldOffset(0x28)] public PArray<PShaderParameterDefinition> m_untweakableShaderParameterDefinitions;
    //    [FieldOffset(0x30)] public PUInt16 m_tweakableParameterBufferSize;
    //    [FieldOffset(0x32)] public PUInt16 m_untweakableParameterBufferSize;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PSceneRenderPass *>> -> +8A6240
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PSceneRenderPass>> -> +8A6360
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PMaterialSwitch>> -> +8A6480
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PMeshInstanceAttachPoint> -> +8A8BD8
 */

[StructLayout(LayoutKind.Explicit, Size = 0xC, Pack = 0x4)]
internal struct PMeshInstanceAttachPoint {
    //    [FieldOffset(0x00)] public PWorldMatrix m_destination;
    //    [FieldOffset(0x04)] public PMeshInstance m_source;
    //    [FieldOffset(0x08)] public PUInt32 m_sourceMatrixIndex;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PMeshInstance> -> +8A7228
 */

[StructLayout(LayoutKind.Explicit, Size = 0x84, Pack = 0x4)]
internal struct PMeshInstance {
    //    [FieldOffset(0x00)] public PMesh m_mesh;
    //    [FieldOffset(0x04)] public PWorldMatrix m_localToWorldMatrix;
    //    [FieldOffset(0x08)] public PArray<PMatrix4> m_currentPose;
    //    [FieldOffset(0x10)] public PMaterialSet m_materialSet;
    //    [FieldOffset(0x14)] public PMeshSegment m_instanceSegment;
    //    [FieldOffset(0x18)] public PDynamicMeshInstance m_dynamicMeshInstance;
    //    [FieldOffset(0x1C)] public PMeshInstanceBounds m_bounds;
    //    [FieldOffset(0x20)] public PLODLevel m_lodLevel;
    //    [FieldOffset(0x24)] public PArray<PMeshInstanceSegmentContext> m_segmentContext;
    //    [FieldOffset(0x2C)] public PString m_name;
    //    [FieldOffset(0x30)] public PInt32 m_animCt;
    //    [FieldOffset(0x34)] public PInt32 m_animID0;
    //    [FieldOffset(0x38)] public PInt32 m_animID1;
    //    [FieldOffset(0x3C)] public PInt32 m_animID2;
    //    [FieldOffset(0x40)] public PInt32 m_animID3;
    //    [FieldOffset(0x48)] public PInt32 m_groupID;
    //    [FieldOffset(0x4C)] public PInt32 m_DObjKind;
    //    [FieldOffset(0x50)] public PInt32 m_RotType;
    //    [FieldOffset(0x44)] public PInt32 m_mimeCt;
    //    [FieldOffset(0x54)] public PInt32 m_dObjFlag;
    //    [FieldOffset(0x58)] public PInt32 m_objID;
    //    [FieldOffset(0x5C)] public float m_layerz0;
    //    [FieldOffset(0x60)] public float m_layerz1;
    //    [FieldOffset(0x64)] public float m_layerz2;
    //    [FieldOffset(0x68)] public float m_layerz3;
    //    [FieldOffset(0x6C)] public PUInt32 m_flags;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PMeshInstanceSegmentContext>> -> +8A7AC8
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PMatrix4>> -> +89A1B0
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PMeshInstanceSegmentStreamBinding> -> +8A70F8
 */

[StructLayout(LayoutKind.Explicit, Size = 0xC, Pack = 0x4)]
internal struct PMeshInstanceSegmentStreamBinding {
    //    [FieldOffset(0x00)] public PRenderDataType m_renderDataType;
    //    [FieldOffset(0x04)] public PString m_name;
    //    [FieldOffset(0x08)] public PUInt16 m_nameHash;
    //    [FieldOffset(0x0A)] public PUInt8 m_index;
    //    [FieldOffset(0x0B)] public PUInt8 m_inputSet;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PMeshSegmentContext> -> +8A4E28
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14, Pack = 0x4)]
internal struct PMeshSegmentContext {
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PMeshInstanceSegmentContext> -> +8A7190
 */

[StructLayout(LayoutKind.Explicit, Size = 0x24, Pack = 0x4)]
internal struct PMeshInstanceSegmentContext {
    //    [FieldOffset(0x1C)] public PSharray<PMeshInstanceSegmentStreamBinding *> m_streamBindings;
}
// this class (descriptor) has a parent; PClassDescriptor<PMeshSegmentContext (sz 0x14, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PEffect> -> +8A66E8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x40, Pack = 0x4)]
internal struct PEffect {
    //    [FieldOffset(0x00)] public PUInt32 m_supportedLightMask;
    //    [FieldOffset(0x04)] public PUInt32 m_supportedShadowCasterMask;
    //    [FieldOffset(0x08)] public PString m_effectFile;
    //    [FieldOffset(0x34)] public PString m_effectSource;
    //    [FieldOffset(0x0C)] public PArray<PEffectVariant *> m_effectVariants;
    //    [FieldOffset(0x14)] public PArray<PLightType *> m_supportedLightTypes;
    //    [FieldOffset(0x1C)] public PArray<PShadowCasterType *> m_supportedShadowCasterTypes;
    //    [FieldOffset(0x24)] public PArray<PContextSwitch *> m_contextSwitches;
    //    [FieldOffset(0x2C)] public PArray<PNodeContext> m_contextVariantSwitches;
    //    [FieldOffset(0x38)] public PUInt32 m_maxLightCount;
    //    [FieldOffset(0x3C)] public PUInt32 m_numSupportedShaderLODLevels;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PNodeContext>> -> +8A6990
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PContextSwitch *>> -> +8A6AB0
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PShadowCasterType *>> -> +8A6BD0
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PLightType *>> -> +8A6CF0
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PEffectVariant *>> -> +8A6E10
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PNodeContext> -> +8A4BC8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x8, Pack = 0x4)]
internal struct PNodeContext {
    //    [FieldOffset(0x00)] public PSharray<PUInt32> m_packedSwitches;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PSharray<PUInt32>> -> +8A4C98
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PMaterialSwitch> -> +8A51D0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x8, Pack = 0x4)]
internal struct PMaterialSwitch {
    //    [FieldOffset(0x00)] public PString m_name;
    //    [FieldOffset(0x04)] public PString m_value;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PMaterial> -> +8A5268
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PMaterial {
    //    [FieldOffset(0x00)] public PEffectVariant m_effectVariant;
    //    [FieldOffset(0x04)] public PParameterBuffer m_parameterBuffer;
    //    [FieldOffset(0x08)] public PSceneRenderPassType m_remapFrom;
    //    [FieldOffset(0x0C)] public PSceneRenderPassType m_remapTo;
    //    [FieldOffset(0x10)] public PString m_texAnimID;
    //    [FieldOffset(0x14)] public PShaderParameterDefinition m_chLightAmt;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PConstantBufferBase> -> +89B4C8
 */

[StructLayout(LayoutKind.Explicit, Size = 0xC, Pack = 0x4)]
internal struct PConstantBufferBase {
    //    [FieldOffset(0x00)] public PUInt32 m_size;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PConstantBufferD3D11> -> +8B3DE0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x3C, Pack = 0x4)]
internal struct PConstantBufferD3D11 {
}
// this class (descriptor) has a parent; PClassDescriptor<PConstantBufferBase (sz 0xC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PConstantBuffer> -> +89B560
 */

[StructLayout(LayoutKind.Explicit, Size = 0x3C, Pack = 0x4)]
internal struct PConstantBuffer {
}
// this class (descriptor) has a parent; PClassDescriptor<PConstantBufferD3D11 (sz 0x3C, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PShaderParameterDefinition>> -> +89D0E0
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PParameterBufferBase> -> +8A4FA8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x4, Pack = 0x10)]
internal struct PParameterBufferBase {
    //    [FieldOffset(0x00)] public PUInt32 m_parameterBufferSize;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PSceneWideParameterBuffer> -> +8A8298
 */

[StructLayout(LayoutKind.Explicit, Size = 0x8, Pack = 0x4)]
internal struct PSceneWideParameterBuffer {
}
// this class (descriptor) has a parent; PClassDescriptor<PParameterBufferBase (sz 0x4, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PParameterBuffer> -> +8A5040
 */

[StructLayout(LayoutKind.Explicit, Size = 0x10, Pack = 0x4)]
internal struct PParameterBuffer {
    //    [FieldOffset(0x04)] public PEffectVariant m_effectVariant;
    //    [FieldOffset(0x08)] public PArray<PShaderParameterDefinition> m_tweakableShaderParameterDefinitions;
}
// this class (descriptor) has a parent; PClassDescriptor<PParameterBufferBase (sz 0x4, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PNode> -> +8A91F8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x54, Pack = 0x4)]
internal struct PNode {
    //    [FieldOffset(0x04)] public PNode m_parent;
    //    [FieldOffset(0x08)] public PNode m_firstChild;
    //    [FieldOffset(0x00)] public PNode m_next;
    //    [FieldOffset(0x0C)] public PWorldMatrix m_worldMatrix;
    //    [FieldOffset(0x10)] public PMatrix4 m_localMatrix;
    //    [FieldOffset(0x50)] public PString m_name;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationSlotListIndex> -> +8A9778
 */

[StructLayout(LayoutKind.Explicit, Size = 0x10, Pack = 0x4)]
internal struct PAnimationSlotListIndex {
    //    [FieldOffset(0x00)] public PAnimationKeyDataType m_animKeyType;
    //    [FieldOffset(0x04)] public PInt32 m_interp;
    //    [FieldOffset(0x08)] public PUInt32 m_targetIndex;
    //    [FieldOffset(0x0C)] public PUInt32 m_nodeCentricReIndex;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationSlotFilterDeferredLoad> -> +8AC0B8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x8, Pack = 0x4)]
internal struct PAnimationSlotFilterDeferredLoad {
    //    [FieldOffset(0x00)] public PAnimationKeyDataType m_keyType;
    //    [FieldOffset(0x04)] public PString m_nodeName;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PAnimationSlotFilterDeferredLoad>> -> +8AC2D8
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationSlotArray> -> +8A9810
 */

[StructLayout(LayoutKind.Explicit, Size = 0x4, Pack = 0x4)]
internal struct PAnimationSlotArray {
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationSet> -> +8AAEA8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PAnimationSet {
    //    [FieldOffset(0x00)] public PSharray<PAnimationClip *> m_animationClips;
    //    [FieldOffset(0x08)] public PArray<PAnimationChannelTarget> m_targets;
    //    [FieldOffset(0x10)] public PArray<PAnimationSlotListIndex> m_slotArrayIndices;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PAnimationSlotListIndex>> -> +8AB058
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PAnimationChannelTarget>> -> +8AB178
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PSharray<PAnimationClip *>> -> +8AB298
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationNetworkInstanceTarget> -> +8ACDB0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PAnimationNetworkInstanceTarget {
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationNetworkInstance> -> +8ACE48
 */

[StructLayout(LayoutKind.Explicit, Size = 0x3C, Pack = 0x4)]
internal struct PAnimationNetworkInstance {
    //    [FieldOffset(0x04)] public PArray<PAnimationDataSourceListEntry> m_animationDataSources;
    //    [FieldOffset(0x0C)] public PArray<PAnimationNetworkInstanceTarget> m_instanceTargetNodes;
    //    [FieldOffset(0x18)] public PAnimationTargetBlenderController m_targetBlender;
    //    [FieldOffset(0x1C)] public PUInt32 m_slotArrayElementsRequired;
    //    [FieldOffset(0x20)] public PArray<PUInt128> m_persistentBuffer;
    //    [FieldOffset(0x28)] public PUInt32 m_preprocessBufferSize;
    //    [FieldOffset(0x2C)] public PArray<PAnimationSlotArray> m_slotArrays;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PAnimationSlotArray>> -> +8AD158
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PAnimationNetworkInstanceTarget>> -> +8AD398
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PAnimationDataSourceListEntry>> -> +8AD4B8
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationHierarchyNode> -> +8AB858
 */

[StructLayout(LayoutKind.Explicit, Size = 0x8, Pack = 0x10)]
internal struct PAnimationHierarchyNode {
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PTimeController> -> +8AB920
 */

[StructLayout(LayoutKind.Explicit, Size = 0x8, Pack = 0x4)]
internal struct PTimeController {
}
// this class (descriptor) has a parent; PClassDescriptor<PAnimationHierarchyNode (sz 0x8, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PTimeScaleOffsetController> -> +8ABBA8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14, Pack = 0x4)]
internal struct PTimeScaleOffsetController {
    //    [FieldOffset(0x08)] public PTimeController m_parent;
    //    [FieldOffset(0x0C)] public float m_scale;
    //    [FieldOffset(0x10)] public float m_offset;
}
// this class (descriptor) has a parent; PClassDescriptor<PTimeController (sz 0x8, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PTimeIntervalController> -> +8ABA08
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PTimeIntervalController {
    //    [FieldOffset(0x08)] public PTimeController m_parent;
    //    [FieldOffset(0x0C)] public float m_parentBase;
    //    [FieldOffset(0x10)] public float m_localBase;
    //    [FieldOffset(0x14)] public float m_localRange;
}
// this class (descriptor) has a parent; PClassDescriptor<PTimeController (sz 0x8, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationDataSource> -> +8ABD28
 */

[StructLayout(LayoutKind.Explicit, Size = 0xC, Pack = 0x10)]
internal struct PAnimationDataSource {
    //    [FieldOffset(0x08)] public PAnimationSet m_animationSet;
}
// this class (descriptor) has a parent; PClassDescriptor<PAnimationHierarchyNode (sz 0x8, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationTargetBlenderController> -> +8ACC00
 */

[StructLayout(LayoutKind.Explicit, Size = 0x10, Pack = 0x4)]
internal struct PAnimationTargetBlenderController {
    //    [FieldOffset(0x0C)] public PAnimationDataSource m_animDataSource;
}
// this class (descriptor) has a parent; PClassDescriptor<PAnimationDataSource (sz 0xC, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationSpuTargetBlenderController> -> +8ACB00
 */

[StructLayout(LayoutKind.Explicit, Size = 0x10, Pack = 0x4)]
internal struct PAnimationSpuTargetBlenderController {
    //    [FieldOffset(0x0C)] public PAnimationDataSource m_animDataSource;
}
// this class (descriptor) has a parent; PClassDescriptor<PAnimationDataSource (sz 0xC, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationSlotFilter> -> +8AC150
 */

[StructLayout(LayoutKind.Explicit, Size = 0x20, Pack = 0x4)]
internal struct PAnimationSlotFilter {
    //    [FieldOffset(0x0C)] public PArray<PUInt32> m_slotBlockList;
    //    [FieldOffset(0x14)] public PAnimationDataSource m_animDataSource;
    //    [FieldOffset(0x18)] public PArray<PAnimationSlotFilterDeferredLoad> m_deferredLoad;
}
// this class (descriptor) has a parent; PClassDescriptor<PAnimationDataSource (sz 0xC, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationEventController> -> +8ABE20
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14, Pack = 0x4)]
internal struct PAnimationEventController {
    //    [FieldOffset(0x0C)] public PAnimationEventList m_animationEventList;
    //    [FieldOffset(0x10)] public PTimeController m_timeController;
}
// this class (descriptor) has a parent; PClassDescriptor<PAnimationDataSource (sz 0xC, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationController> -> +8ABF68
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14, Pack = 0x4)]
internal struct PAnimationController {
    //    [FieldOffset(0x0C)] public PAnimationClip m_animationClip;
    //    [FieldOffset(0x10)] public PTimeController m_timeController;
}
// this class (descriptor) has a parent; PClassDescriptor<PAnimationDataSource (sz 0xC, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationBlenderController> -> +8AC618
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14, Pack = 0x10)]
internal struct PAnimationBlenderController {
    //    [FieldOffset(0x0C)] public PArray<PAnimationDataSource *> m_sources;
}
// this class (descriptor) has a parent; PClassDescriptor<PAnimationDataSource (sz 0xC, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationWeightedBlenderController> -> +8AC898
 */

[StructLayout(LayoutKind.Explicit, Size = 0x1C, Pack = 0x4)]
internal struct PAnimationWeightedBlenderController {
    //    [FieldOffset(0x14)] public PArray<float> m_sourceWeights;
}
// this class (descriptor) has a parent; PClassDescriptor<PAnimationBlenderController (sz 0x14, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationAdditiveBlenderController> -> +8AC9D0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x1C, Pack = 0x4)]
internal struct PAnimationAdditiveBlenderController {
    //    [FieldOffset(0x14)] public PArray<float> m_sourceWeights;
}
// this class (descriptor) has a parent; PClassDescriptor<PAnimationBlenderController (sz 0x14, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationEventList> -> +8AB580
 */

[StructLayout(LayoutKind.Explicit, Size = 0x8, Pack = 0x4)]
internal struct PAnimationEventList {
    //    [FieldOffset(0x00)] public PArray<PAnimationEvent> m_events;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PAnimationEvent>> -> +8AB6A8
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationEvent> -> +8AB4E8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x8, Pack = 0x4)]
internal struct PAnimationEvent {
    //    [FieldOffset(0x00)] public float m_time;
    //    [FieldOffset(0x04)] public PUInt32 m_id;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationDataSourceListEntry> -> +8ACD18
 */

[StructLayout(LayoutKind.Explicit, Size = 0x94, Pack = 0x4)]
internal struct PAnimationDataSourceListEntry {
    //    [FieldOffset(0x00)] public PAnimationDataSource m_dataSource;
    //    [FieldOffset(0x04)] public PUInt32 m_inputCount;
    //    [FieldOffset(0x08)] public PUInt32 m_inputIndex;
    //    [FieldOffset(0x88)] public PUInt32 m_destSlotArray;
    //    [FieldOffset(0x8C)] public PUInt32 m_persistentBufferOffset;
    //    [FieldOffset(0x90)] public PUInt32 m_preprocessBufferOffset;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationClip> -> +8AA7E8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x20, Pack = 0x4)]
internal struct PAnimationClip {
    //    [FieldOffset(0x04)] public PArray<PAnimationChannel *> m_channels;
    //    [FieldOffset(0x0C)] public PArray<PAnimationConstantChannel> m_constantChannels;
    //    [FieldOffset(0x14)] public float m_constantChannelStartTime;
    //    [FieldOffset(0x18)] public float m_constantChannelEndTime;
    //    [FieldOffset(0x00)] public PAnimationClipBinding m_binding;
    //    [FieldOffset(0x1C)] public PString m_name;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PAnimationConstantChannel>> -> +8AAB28
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PAnimationChannel *>> -> +8AAC48
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationChannelTimes> -> +8A99B8
 */

[StructLayout(LayoutKind.Explicit, Size = 0xC, Pack = 0x4)]
internal struct PAnimationChannelTimes {
    //    [FieldOffset(0x04)] public PArray<float> m_timeKeys;
    //    [FieldOffset(0x00)] public PUInt32 m_keyCount;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationChannelTarget> -> +8A9C28
 */

[StructLayout(LayoutKind.Explicit, Size = 0x1C, Pack = 0x4)]
internal struct PAnimationChannelTarget {
    //    [FieldOffset(0x00)] public PClassDescriptor m_instanceObjectType;
    //    [FieldOffset(0x04)] public PBase m_instanceObject;
    //    [FieldOffset(0x08)] public PClassDescriptor m_baseObjectType;
    //    [FieldOffset(0x0C)] public PBase m_baseObject;
    //    [FieldOffset(0x10)] public PInt32 m_type;
    //    [FieldOffset(0x14)] public PString m_name;
    //    [FieldOffset(0x18)] public PUInt32 m_index;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationChannelBase> -> +8A9F30
 */

[StructLayout(LayoutKind.Explicit, Size = 0x24, Pack = 0x4)]
internal struct PAnimationChannelBase {
    //    [FieldOffset(0x1C)] public PAnimationKeyDataType m_keyType;
    //    [FieldOffset(0x20)] public PInt32 m_interp;
}
// this class (descriptor) has a parent; PClassDescriptor<PAnimationChannelTarget (sz 0x1C, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationConstantChannel> -> +8AA298
 */

[StructLayout(LayoutKind.Explicit, Size = 0x34, Pack = 0x4)]
internal struct PAnimationConstantChannel {
    //    [FieldOffset(0x24)] public float m_value;
}
// this class (descriptor) has a parent; PClassDescriptor<PAnimationChannelBase (sz 0x24, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationChannel> -> +8AA0B8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x34, Pack = 0x4)]
internal struct PAnimationChannel {
    //    [FieldOffset(0x24)] public PAnimationChannelTimes m_times;
    //    [FieldOffset(0x28)] public PArray<float> m_valueKeys;
    //    [FieldOffset(0x30)] public PUInt32 m_keyCount;
}
// this class (descriptor) has a parent; PClassDescriptor<PAnimationChannelBase (sz 0x24, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PAnimationDataSource *>> -> +8AC748
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationClipBindingDataBlockCache> -> +8AA4C0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x10, Pack = 0x4)]
internal struct PAnimationClipBindingDataBlockCache {
    //    [FieldOffset(0x0C)] public PUInt16 m_sourceChannelIndex;
    //    [FieldOffset(0x08)] public PUInt32 m_valueWidthAndKeyCount;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationClipBindingChannelMap> -> +8AA428
 */

[StructLayout(LayoutKind.Explicit, Size = 0x4, Pack = 0x2)]
internal struct PAnimationClipBindingChannelMap {
    //    [FieldOffset(0x00)] public PInt16 m_destSlotArrayIndex;
    //    [FieldOffset(0x02)] public PUInt16 m_interp;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimationClipBinding> -> +8AA558
 */

[StructLayout(LayoutKind.Explicit, Size = 0x8, Pack = 0x4)]
internal struct PAnimationClipBinding {
    //    [FieldOffset(0x00)] public PUInt16 m_spuBindingSize;
    //    [FieldOffset(0x02)] public PUInt16 m_channelCount;
    //    [FieldOffset(0x04)] public PUInt16 m_constantChannelCount;
    //    [FieldOffset(0x06)] public PUInt16 m_interpCountLength;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PBlendableAnimationSource>> -> +8AE060
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PBlendableAnimationSource> -> +8AD9A8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x4C, Pack = 0x4)]
internal struct PBlendableAnimationSource {
    //    [FieldOffset(0x00)] public PAnimationClip m_animationClip;
    //    [FieldOffset(0x0C)] public PTimeIntervalController m_timeIntervalController;
    //    [FieldOffset(0x24)] public PTimeScaleOffsetController m_timeScaleOffsetController;
    //    [FieldOffset(0x38)] public PAnimationController m_animationController;
    //    [FieldOffset(0x08)] public float m_speed;
    //    [FieldOffset(0x04)] public float m_weight;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PTriggerReceiverTypeCallbackData> -> +8AF1E8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14, Pack = 0x4)]
internal struct PTriggerReceiverTypeCallbackData {
    //    [FieldOffset(0x00)] public PTypedObject m_triggerReceiverComponent;
    //    [FieldOffset(0x08)] public PTrigger m_trigger;
    //    [FieldOffset(0x0C)] public PQuarryComponent m_quarryComponent;
    //    [FieldOffset(0x10)] public PUInt32 m_entryCount;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PTrigger> -> +8AED58
 */

[StructLayout(LayoutKind.Explicit, Size = 0x44, Pack = 0x4)]
internal struct PTrigger {
    //    [FieldOffset(0x00)] public PTriggerType m_type;
    //    [FieldOffset(0x0C)] public PWorldMatrix m_localToWorldMatrix;
    //    [FieldOffset(0x10)] public PWorldMatrix m_previousLocalToWorldMatrix;
    //    [FieldOffset(0x04)] public PSharray<PTriggerReceiverComponent *> m_triggerReceivers;
    //    [FieldOffset(0x40)] public bool m_enabled;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PSharray<PTriggerReceiverComponent *>> -> +8AEF78
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PSpline> -> +8AE7B8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x2C, Pack = 0x4)]
internal struct PSpline {
    //    [FieldOffset(0x04)] public PArray<Vector3> m_controlPoints;
    //    [FieldOffset(0x0C)] public PArray<Vector3> m_eulers;
    //    [FieldOffset(0x14)] public PWorldMatrix m_localToWorldMatrix;
    //    [FieldOffset(0x18)] public bool m_isLoop;
    //    [FieldOffset(0x1C)] public float m_tension;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<Vector3>> -> +8AEAA8
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PLocator> -> +8AE550
 */

[StructLayout(LayoutKind.Explicit, Size = 0xC, Pack = 0x4)]
internal struct PLocator {
    //    [FieldOffset(0x00)] public PWorldMatrix m_localToWorldMatrix;
    //    [FieldOffset(0x04)] public PString m_name;
    //    [FieldOffset(0x08)] public PLocatorShapeType m_shapeType;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PSpriteAnimationInfoInstance> -> +8B3158
 */

[StructLayout(LayoutKind.Explicit, Size = 0x28, Pack = 0x8)]
internal struct PSpriteAnimationInfoInstance {
    //    [FieldOffset(0x00)] public PWorldMatrix m_localToWorldMatrix;
    //    [FieldOffset(0x04)] public PSpriteCollection m_spriteCollection;
    //    [FieldOffset(0x08)] public PSpriteAnimationInfo m_spriteAnimInfo;
    //    [FieldOffset(0x20)] public bool m_flipX;
    //    [FieldOffset(0x21)] public bool m_flipY;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PSpriteAnimationInfo> -> +8B1EB8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14, Pack = 0x4)]
internal struct PSpriteAnimationInfo {
    //    [FieldOffset(0x00)] public PString m_name;
    //    [FieldOffset(0x04)] public PTextureAtlasInfo m_textureAtlasInfo;
    //    [FieldOffset(0x08)] public float m_timeInterval;
    //    [FieldOffset(0x0C)] public PArray<PUInt32> m_subTextureIDs;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PSpriteAnimationInfoChar> -> +8B1F50
 */

[StructLayout(LayoutKind.Explicit, Size = 0x28, Pack = 0x4)]
internal struct PSpriteAnimationInfoChar {
    //    [FieldOffset(0x14)] public PString m_groupName;
    //    [FieldOffset(0x18)] public float mUStart;
    //    [FieldOffset(0x1C)] public float mVStart;
    //    [FieldOffset(0x20)] public float mUEnd;
    //    [FieldOffset(0x24)] public float mVEnd;
}
// this class (descriptor) has a parent; PClassDescriptor<PSpriteAnimationInfo (sz 0x14, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PTextureAtlasInfo> -> +8B1E20
 */

[StructLayout(LayoutKind.Explicit, Size = 0xC, Pack = 0x4)]
internal struct PTextureAtlasInfo {
    //    [FieldOffset(0x00)] public PTexture2D m_texture;
    //    [FieldOffset(0x04)] public PArray<PSubTextureInfo> m_subTextures;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PSubTextureInfo>> -> +8B2A60
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PSubTextureInfo> -> +8B1D88
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14, Pack = 0x4)]
internal struct PSubTextureInfo {
    //    [FieldOffset(0x00)] public PString m_name;
    //    [FieldOffset(0x04)] public float m_uOrigin;
    //    [FieldOffset(0x08)] public float m_vOrigin;
    //    [FieldOffset(0x0C)] public float m_textureSizeX;
    //    [FieldOffset(0x10)] public float m_textureSizeY;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PSpriteAttributes> -> +8B1C58
 */

[StructLayout(LayoutKind.Explicit, Size = 0x30, Pack = 0x4)]
internal struct PSpriteAttributes {
    //    [FieldOffset(0x00)] public float m_posX;
    //    [FieldOffset(0x04)] public float m_posY;
    //    [FieldOffset(0x08)] public float m_sinPhi;
    //    [FieldOffset(0x0C)] public float m_cosPhi;
    //    [FieldOffset(0x10)] public float m_uOrigin;
    //    [FieldOffset(0x14)] public float m_vOrigin;
    //    [FieldOffset(0x18)] public float m_textureSizeX;
    //    [FieldOffset(0x1C)] public float m_textureSizeY;
    //    [FieldOffset(0x20)] public float m_spriteSizeX;
    //    [FieldOffset(0x24)] public float m_spriteSizeY;
    //    [FieldOffset(0x28)] public float m_depth;
    //    [FieldOffset(0x2C)] public float m_phi;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PSpriteCollection> -> +8B1CF0
 */

[StructLayout(LayoutKind.Explicit, Size = 0xDC, Pack = 0x4)]
internal struct PSpriteCollection {
    //    [FieldOffset(0x14)] public PMaterialSet m_quadMaterialSet;
    //    [FieldOffset(0x1C)] public PUInt32 m_maxSpriteCount;
    //    [FieldOffset(0x20)] public PUInt32 m_currentSpriteCount;
    //    [FieldOffset(0x24)] public PMeshSegment m_instanceSegment;
    //    [FieldOffset(0x90)] public PTexture2D m_textureAtlas;
    //    [FieldOffset(0xB8)] public PSpriteAttributes m_sprites;
    //    [FieldOffset(0xBC)] public PMaterial m_material;
    //    [FieldOffset(0xC4)] public PArray<PUInt32> m_indexMap;
    //    [FieldOffset(0xC0)] public PUInt32 m_nextAvailableSpriteID;
    //    [FieldOffset(0x0C)] public PMesh m_mesh;
    //    [FieldOffset(0x04)] public bool m_sort;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PUInt32>> -> +8AC3F8
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PModifierNetworkDynamicMeshSegment>> -> +8BB520
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PRenderStream>> -> +8BB640
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PUInt8 *>> -> +8BB760
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PModifierNetworkDynamicMeshSegment> -> +8BB268
 */

[StructLayout(LayoutKind.Explicit, Size = 0x10, Pack = 0x4)]
internal struct PModifierNetworkDynamicMeshSegment {
    //    [FieldOffset(0x00)] public PModifierNetwork m_modifierNetwork;
    //    [FieldOffset(0x04)] public PUInt32 m_segmentInputGroupStart;
    //    [FieldOffset(0x08)] public PUInt32 m_segmentOutputGroupStart;
    //    [FieldOffset(0x0C)] public PUInt32 m_modifierOutputsAreConsumed;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PModifierNetworkInstance> -> +8BBB38
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14, Pack = 0x4)]
internal struct PModifierNetworkInstance {
    //    [FieldOffset(0x00)] public PModifierNetwork m_modifierNetwork;
    //    [FieldOffset(0x04)] public PArray<PBase *> m_modifierUserData;
    //    [FieldOffset(0x0C)] public PArray<PUInt128> m_persistentState;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PUInt128>> -> +8AD278
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PBase *>> -> +8BBD20
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PModifierNetworkInstancePacketInput> -> +8BBAA0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x8, Pack = 0x4)]
internal struct PModifierNetworkInstancePacketInput {
    //    [FieldOffset(0x00)] public PUInt32 m_source;
    //    [FieldOffset(0x04)] public PUInt32 m_list;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PRenderStream> -> +8BBFF8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x8, Pack = 0x4)]
internal struct PRenderStream {
    //    [FieldOffset(0x00)] public PDynamicDataBlock m_dataBlock;
    //    [FieldOffset(0x04)] public PVertexStream m_vertexStream;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PModifierNetwork> -> +8BC420
 */

[StructLayout(LayoutKind.Explicit, Size = 0x40, Pack = 0x4)]
internal struct PModifierNetwork {
    //    [FieldOffset(0x04)] public PArray<PModifierAndInputs> m_modifiers;
    //    [FieldOffset(0x0C)] public PUInt32 m_totalInputCount;
    //    [FieldOffset(0x10)] public PArray<PModifierNetworkBuffer> m_inputs;
    //    [FieldOffset(0x18)] public PArray<PModifierNetworkBuffer> m_outputs;
    //    [FieldOffset(0x00)] public PUInt32 m_totalOutputElementSize;
    //    [FieldOffset(0x24)] public PInt32 m_streamingStrategy;
    //    [FieldOffset(0x28)] public PModifierNetworkInfoPacket m_infoPacket;
    //    [FieldOffset(0x2C)] public PUInt32 m_totalPersistentStateSize;
    //    [FieldOffset(0x30)] public PUInt32 m_totalStateBlockSize;
    //    [FieldOffset(0x34)] public bool m_isCompiled;
    //    [FieldOffset(0x38)] public PArray<PInt32> m_inputElementCountSources;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PModifierNetworkBuffer>> -> +8BCD40
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PModifierAndInputs>> -> +8BCE60
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PModifierNetworkInfoPacket> -> +8BC388
 */

[StructLayout(LayoutKind.Explicit, Size = 0x20, Pack = 0x10)]
internal struct PModifierNetworkInfoPacket {
    //    [FieldOffset(0x00)] public PUInt32 m_size;
    //    [FieldOffset(0x04)] public PUInt32 m_modifierCodeCount;
    //    [FieldOffset(0x08)] public PUInt32 m_modifierInstanceCount;
    //    [FieldOffset(0x0C)] public PUInt16 m_inputCount;
    //    [FieldOffset(0x0E)] public PUInt16 m_outputCount;
    //    [FieldOffset(0x10)] public PInt32 m_streamingStrategy;
    //    [FieldOffset(0x14)] public PUInt32 m_totalPersistentStateSize;
    //    [FieldOffset(0x18)] public PUInt32 m_totalStateBlockSize;
    //    [FieldOffset(0x1C)] public PUInt32 m_spuModifierCodeSize;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PModifierNetworkInfoPacket_Buffer> -> +8BC2F0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x2, Pack = 0x2)]
internal struct PModifierNetworkInfoPacket_Buffer {
    //    [FieldOffset(0x00)] public PUInt16 m_elementSize;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PModifierNetworkInfoPacket_ModifierInstance> -> +8BC258
 */

[StructLayout(LayoutKind.Explicit, Size = 0x20, Pack = 0x10)]
internal struct PModifierNetworkInfoPacket_ModifierInstance {
    //    [FieldOffset(0x00)] public PUInt32 m_persistentStateOffset;
    //    [FieldOffset(0x04)] public PUInt32 m_stateBlockOffset;
    //    [FieldOffset(0x08)] public PUInt8 m_codeIndex;
    //    [FieldOffset(0x09)] public PUInt8 m_inputOutputIndices;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PModifierNetworkInfoPacket_ModifierCode> -> +8BC1C0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x20, Pack = 0x10)]
internal struct PModifierNetworkInfoPacket_ModifierCode {
    //    [FieldOffset(0x00)] public PModifier m_modifier;
    //    [FieldOffset(0x04)] public PUInt32 m_inputCount;
    //    [FieldOffset(0x08)] public PUInt32 m_outputCount;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PModifierAndInputs> -> +8BC128
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14, Pack = 0x4)]
internal struct PModifierAndInputs {
    //    [FieldOffset(0x00)] public PModifier m_modifier;
    //    [FieldOffset(0x04)] public PUInt32 m_persistentStateOffset;
    //    [FieldOffset(0x08)] public PUInt32 m_stateBlockOffset;
    //    [FieldOffset(0x0C)] public PArray<PRenderStreamInput> m_inputs;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PRenderStreamInput>> -> +8BCC20
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PModifierNetworkBuffer> -> +8BC090
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14, Pack = 0x4)]
internal struct PModifierNetworkBuffer {
    //    [FieldOffset(0x00)] public PRenderDataType m_type;
    //    [FieldOffset(0x04)] public PUInt32 m_modifier;
    //    [FieldOffset(0x08)] public PUInt32 m_stream;
    //    [FieldOffset(0x0C)] public PUInt32 m_elementSize;
    //    [FieldOffset(0x10)] public PUInt32 m_info;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PRenderStreamInput> -> +8BBF60
 */

[StructLayout(LayoutKind.Explicit, Size = 0x8, Pack = 0x4)]
internal struct PRenderStreamInput {
    //    [FieldOffset(0x00)] public PInt32 m_source;
    //    [FieldOffset(0x04)] public PInt32 m_stream;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PModifierNetworkInstanceInput> -> +8BBA08
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14, Pack = 0x4)]
internal struct PModifierNetworkInstanceInput {
    //    [FieldOffset(0x08)] public PUInt32 m_elementSize;
    //    [FieldOffset(0x0C)] public PRenderStream m_renderStream;
}
// this class (descriptor) has a parent; PClassDescriptor<PRenderStreamInput (sz 0x8, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PMorphModifierWeightsUserDataObject> -> +8BD390
 */

[StructLayout(LayoutKind.Explicit, Size = 0x8, Pack = 0x4)]
internal struct PMorphModifierWeightsUserDataObject {
    //    [FieldOffset(0x00)] public PArray<float> m_weights;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<float>> -> +8A9AB0
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PLODGroup> -> +8BAB20
 */

[StructLayout(LayoutKind.Explicit, Size = 0x48, Pack = 0x4)]
internal struct PLODGroup {
    //    [FieldOffset(0x3C)] public PArray<PLODLevel> m_levels;
    //    [FieldOffset(0x00)] public Vector3 m_minBounds;
    //    [FieldOffset(0x10)] public Vector3 m_maxBounds;
    //    [FieldOffset(0x28)] public float m_blendRange;
    //    [FieldOffset(0x2C)] public float m_shaderLODLevelDistance;
    //    [FieldOffset(0x34)] public PLODMetricType m_lodMetricType;
    //    [FieldOffset(0x38)] public PLODBlendType m_lodBlendType;
    //    [FieldOffset(0x44)] public bool m_isEnabled;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PLODLevel>> -> +8BAF50
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PLODLevel> -> +8BAA88
 */

[StructLayout(LayoutKind.Explicit, Size = 0x24, Pack = 0x4)]
internal struct PLODLevel {
    //    [FieldOffset(0x00)] public PLODGroup m_lodGroup;
    //    [FieldOffset(0x04)] public PSharray<PMeshInstance *> m_meshInstances;
    //    [FieldOffset(0x0C)] public float m_minimumThreshold;
    //    [FieldOffset(0x10)] public float m_maximumThreshold;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PSharray<PMeshInstance *>> -> +8BAE30
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAsyncProcessHeader> -> +8C9910
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14, Pack = 0x4)]
internal struct PAsyncProcessHeader {
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PScheduler> -> +8BD9C8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x284, Pack = 0x4)]
internal struct PScheduler {
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PScript> -> +8BE410
 */

[StructLayout(LayoutKind.Explicit, Size = 0x28, Pack = 0x4)]
internal struct PScript {
    //    [FieldOffset(0x00)] public PArray<PUInt8> m_scriptChunk;
    //    [FieldOffset(0x08)] public PArray<PString> m_functionNames;
    //    [FieldOffset(0x10)] public PString m_onLoadEntryPoint;
    //    [FieldOffset(0x14)] public PString m_onUnloadEntryPoint;
    //    [FieldOffset(0x18)] public PString m_defaultEntryPoint;
    //    [FieldOffset(0x1C)] public PString m_sourceName;
    //    [FieldOffset(0x20)] public bool m_isPersistent;
    //    [FieldOffset(0x24)] public PUInt32 m_executionInterval;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PString>> -> +89A090
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsCallbackData> -> +8BF190
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14, Pack = 0x4)]
internal struct PPhysicsCallbackData {
    //    [FieldOffset(0x00)] public PTypedObject m_collisionObjectA;
    //    [FieldOffset(0x08)] public PTypedObject m_collisionObjectB;
    //    [FieldOffset(0x10)] public float m_amount;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PRaycastResult> -> +8BF6E8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x28, Pack = 0x4)]
internal struct PRaycastResult {
    //    [FieldOffset(0x00)] public Vector4 m_contactPoint;
    //    [FieldOffset(0x10)] public Vector4 m_contactNormal;
    //    [FieldOffset(0x20)] public PBase m_collisionObject;
    //    [FieldOffset(0x24)] public PClassDescriptor m_collisionObjectClassDescriptor;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsCharacterControllerBase> -> +8BE9D8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x10C, Pack = 0x4)]
internal struct PPhysicsCharacterControllerBase {
    //    [FieldOffset(0x24)] public Vector3 m_gravity;
    //    [FieldOffset(0xC4)] public PNode m_targetNode;
    //    [FieldOffset(0xC8)] public PWorldMatrix m_targetWorldMatrix;
    //    [FieldOffset(0xD0)] public float m_rotate;
    //    [FieldOffset(0xD4)] public float m_right;
    //    [FieldOffset(0xD8)] public float m_forward;
    //    [FieldOffset(0xDC)] public float m_velocity;
    //    [FieldOffset(0xF4)] public bool m_jump;
    //    [FieldOffset(0xF5)] public bool m_isOnGround;
    //    [FieldOffset(0xE0)] public float m_maxSlopeAngle;
    //    [FieldOffset(0x00)] public PPhysicsCharacterControllerBase m_next;
    //    [FieldOffset(0x04)] public Point3 m_startPosition;
    //    [FieldOffset(0x14)] public Vector3 m_scale;
    //    [FieldOffset(0xE4)] public float m_jumpHeight;
    //    [FieldOffset(0xE8)] public float m_height;
    //    [FieldOffset(0xEC)] public float m_radius;
    //    [FieldOffset(0xCC)] public PPhysicsWorld m_world;
    //    [FieldOffset(0x44)] public PMatrix4 m_graphicsOffset;
    //    [FieldOffset(0x84)] public PMatrix4 m_invGraphicsOffset;
    //    [FieldOffset(0x100)] public PScriptCallbackHandler m_scriptHandler;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsCharacterControllerBullet> -> +8BFE08
 */

[StructLayout(LayoutKind.Explicit, Size = 0x15C, Pack = 0x4)]
internal struct PPhysicsCharacterControllerBullet {
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsCharacterControllerBase (sz 0x10C, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsCharacterController> -> +8BEA70
 */

[StructLayout(LayoutKind.Explicit, Size = 0x15C, Pack = 0x4)]
internal struct PPhysicsCharacterController {
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsCharacterControllerBullet (sz 0x15C, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsInterfaceBase> -> +8BEC38
 */

[StructLayout(LayoutKind.Explicit, Size = 0x10, Pack = 0x4)]
internal struct PPhysicsInterfaceBase {
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsInterfaceBullet> -> +8BFD70
 */

[StructLayout(LayoutKind.Explicit, Size = 0x4C, Pack = 0x4)]
internal struct PPhysicsInterfaceBullet {
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsInterfaceBase (sz 0x10, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsInterface> -> +8BECD0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x4C, Pack = 0x10)]
internal struct PPhysicsInterface {
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsInterfaceBullet (sz 0x4C, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsRigidBodyBase> -> +8BF0F8
 */

[StructLayout(LayoutKind.Explicit, Size = 0xDC, Pack = 0x4)]
internal struct PPhysicsRigidBodyBase {
    //    [FieldOffset(0xC0)] public float m_mass;
    //    [FieldOffset(0xC4)] public PUInt8 m_rigidBodyType;
    //    [FieldOffset(0x04)] public PMatrix4x3 m_massFrameTransform;
    //    [FieldOffset(0x34)] public Vector3 m_initialPosition;
    //    [FieldOffset(0x44)] public Quat m_initialOrientation;
    //    [FieldOffset(0x54)] public Vector3 m_inertiaTensor;
    //    [FieldOffset(0x64)] public Vector3 m_initialLinearVelocity;
    //    [FieldOffset(0x74)] public Vector3 m_initialAngularVelocity;
    //    [FieldOffset(0x98)] public PNode m_targetNode;
    //    [FieldOffset(0x9C)] public PWorldMatrix m_targetWorldMatrix;
    //    [FieldOffset(0x94)] public PPhysicsMaterial m_material;
    //    [FieldOffset(0xA0)] public PSharray<PPhysicsShape *> m_shapes;
    //    [FieldOffset(0x84)] public Vector3 m_scale;
    //    [FieldOffset(0xA8)] public float m_linearDamping;
    //    [FieldOffset(0xAC)] public float m_angularDamping;
    //    [FieldOffset(0x00)] public PPhysicsRigidBody m_next;
    //    [FieldOffset(0xB0)] public PPhysicsModel m_model;
    //    [FieldOffset(0xB8)] public PPhysicsRigidBody m_nextKinematicRigidBody;
    //    [FieldOffset(0xBC)] public PUInt8 m_collisionGroup;
    //    [FieldOffset(0xC5)] public bool m_enabled;
    //    [FieldOffset(0xD0)] public PScriptCallbackHandler m_scriptHandler;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsRigidBodyBullet> -> +8BFC40
 */

[StructLayout(LayoutKind.Explicit, Size = 0xE4, Pack = 0x4)]
internal struct PPhysicsRigidBodyBullet {
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsRigidBodyBase (sz 0xDC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsRigidBody> -> +8BF228
 */

[StructLayout(LayoutKind.Explicit, Size = 0xE4, Pack = 0x4)]
internal struct PPhysicsRigidBody {
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsRigidBodyBullet (sz 0xE4, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PSharray<PPhysicsShape *>> -> +8C5558
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsWorldBase> -> +8BF650
 */

[StructLayout(LayoutKind.Explicit, Size = 0x54, Pack = 0x4)]
internal struct PPhysicsWorldBase {
    //    [FieldOffset(0x48)] public PPhysicsModel m_models;
    //    [FieldOffset(0x00)] public Vector3 m_gravity;
    //    [FieldOffset(0x10)] public float m_timeStep;
    //    [FieldOffset(0x14)] public Vector3 m_worldMin;
    //    [FieldOffset(0x24)] public Vector3 m_worldMax;
    //    [FieldOffset(0x4C)] public PPhysicsCharacterControllerBase m_characterControllers;
    //    [FieldOffset(0x50)] public PPhysicsRigidBody m_kinematicRigidBodies;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsWorldBullet> -> +8BFCD8
 */

[StructLayout(LayoutKind.Explicit, Size = 0xA8, Pack = 0x4)]
internal struct PPhysicsWorldBullet {
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsWorldBase (sz 0x54, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsWorld> -> +8BF780
 */

[StructLayout(LayoutKind.Explicit, Size = 0xA8, Pack = 0x4)]
internal struct PPhysicsWorld {
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsWorldBullet (sz 0xA8, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsShapeBase> -> +8BF2C0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x58, Pack = 0x10)]
internal struct PPhysicsShapeBase {
    //    [FieldOffset(0x04)] public bool m_hollow;
    //    [FieldOffset(0x08)] public float m_mass;
    //    [FieldOffset(0x0C)] public float m_density;
    //    [FieldOffset(0x10)] public PPhysicsMaterial m_material;
    //    [FieldOffset(0x14)] public PMatrix4x3 m_transform;
    //    [FieldOffset(0x44)] public Vector3 m_scale;
    //    [FieldOffset(0x54)] public PInt32 m_type;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsShapeBullet> -> +8BF818
 */

[StructLayout(LayoutKind.Explicit, Size = 0x5C, Pack = 0x4)]
internal struct PPhysicsShapeBullet {
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsShapeBase (sz 0x58, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsShape> -> +8BF358
 */

[StructLayout(LayoutKind.Explicit, Size = 0x5C, Pack = 0x4)]
internal struct PPhysicsShape {
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsShapeBullet (sz 0x5C, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsTaperedCylinder> -> +8BF5B8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x70, Pack = 0x4)]
internal struct PPhysicsTaperedCylinder {
    //    [FieldOffset(0x5C)] public float m_upperRadiusArray;
    //    [FieldOffset(0x64)] public float m_lowerRadiusArray;
    //    [FieldOffset(0x6C)] public float m_height;
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsShape (sz 0x5C, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsTaperedCapsule> -> +8BF520
 */

[StructLayout(LayoutKind.Explicit, Size = 0x70, Pack = 0x4)]
internal struct PPhysicsTaperedCapsule {
    //    [FieldOffset(0x5C)] public float m_upperRadiusArray;
    //    [FieldOffset(0x64)] public float m_lowerRadiusArray;
    //    [FieldOffset(0x6C)] public float m_height;
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsShape (sz 0x5C, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsSphereBase> -> +8BF3F0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x60, Pack = 0x4)]
internal struct PPhysicsSphereBase {
    //    [FieldOffset(0x5C)] public float m_radius;
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsShape (sz 0x5C, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsSphereBullet> -> +8BFBA8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x64, Pack = 0x4)]
internal struct PPhysicsSphereBullet {
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsSphereBase (sz 0x60, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsSphere> -> +8BF488
 */

[StructLayout(LayoutKind.Explicit, Size = 0x64, Pack = 0x4)]
internal struct PPhysicsSphere {
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsSphereBullet (sz 0x64, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsPlaneBase> -> +8BEFC8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x6C, Pack = 0x4)]
internal struct PPhysicsPlaneBase {
    //    [FieldOffset(0x5C)] public Vector4 m_equationCoefficient;
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsShape (sz 0x5C, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsPlaneBullet> -> +8BFB10
 */

[StructLayout(LayoutKind.Explicit, Size = 0x70, Pack = 0x4)]
internal struct PPhysicsPlaneBullet {
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsPlaneBase (sz 0x6C, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsPlane> -> +8BF060
 */

[StructLayout(LayoutKind.Explicit, Size = 0x70, Pack = 0x4)]
internal struct PPhysicsPlane {
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsPlaneBullet (sz 0x70, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsMeshBase> -> +8BEE00
 */

[StructLayout(LayoutKind.Explicit, Size = 0x60, Pack = 0x4)]
internal struct PPhysicsMeshBase {
    //    [FieldOffset(0x5C)] public PShape m_shape;
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsShape (sz 0x5C, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsMeshBullet> -> +8BFA78
 */

[StructLayout(LayoutKind.Explicit, Size = 0x6C, Pack = 0x4)]
internal struct PPhysicsMeshBullet {
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsMeshBase (sz 0x60, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsMesh> -> +8BEE98
 */

[StructLayout(LayoutKind.Explicit, Size = 0x6C, Pack = 0x4)]
internal struct PPhysicsMesh {
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsMeshBullet (sz 0x6C, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsCylinderBase> -> +8BEB08
 */

[StructLayout(LayoutKind.Explicit, Size = 0x68, Pack = 0x4)]
internal struct PPhysicsCylinderBase {
    //    [FieldOffset(0x5C)] public float m_radiusArray;
    //    [FieldOffset(0x64)] public float m_height;
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsShape (sz 0x5C, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsCylinderBullet> -> +8BF9E0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x6C, Pack = 0x4)]
internal struct PPhysicsCylinderBullet {
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsCylinderBase (sz 0x68, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsCylinder> -> +8BEBA0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x6C, Pack = 0x4)]
internal struct PPhysicsCylinder {
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsCylinderBullet (sz 0x6C, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsCapsuleBase> -> +8BE810
 */

[StructLayout(LayoutKind.Explicit, Size = 0x6C, Pack = 0x4)]
internal struct PPhysicsCapsuleBase {
    //    [FieldOffset(0x5C)] public float m_radiusArray;
    //    [FieldOffset(0x64)] public float m_height;
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsShape (sz 0x5C, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsCapsuleBullet> -> +8BF948
 */

[StructLayout(LayoutKind.Explicit, Size = 0x70, Pack = 0x4)]
internal struct PPhysicsCapsuleBullet {
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsCapsuleBase (sz 0x6C, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsCapsule> -> +8BE8A8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x70, Pack = 0x4)]
internal struct PPhysicsCapsule {
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsCapsuleBullet (sz 0x70, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsBoxBase> -> +8BE6E0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x6C, Pack = 0x4)]
internal struct PPhysicsBoxBase {
    //    [FieldOffset(0x5C)] public Vector3 m_halfExtents;
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsShape (sz 0x5C, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsBoxBullet> -> +8BF8B0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x70, Pack = 0x4)]
internal struct PPhysicsBoxBullet {
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsBoxBase (sz 0x6C, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsBox> -> +8BE778
 */

[StructLayout(LayoutKind.Explicit, Size = 0x70, Pack = 0x4)]
internal struct PPhysicsBox {
}
// this class (descriptor) has a parent; PClassDescriptor<PPhysicsBoxBullet (sz 0x70, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsModel> -> +8BEF30
 */

[StructLayout(LayoutKind.Explicit, Size = 0xC, Pack = 0x4)]
internal struct PPhysicsModel {
    //    [FieldOffset(0x04)] public PPhysicsRigidBody m_rigidBodies;
    //    [FieldOffset(0x00)] public PPhysicsModel m_next;
    //    [FieldOffset(0x08)] public PPhysicsWorld m_world;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPhysicsMaterial> -> +8BED68
 */

[StructLayout(LayoutKind.Explicit, Size = 0xC, Pack = 0x4)]
internal struct PPhysicsMaterial {
    //    [FieldOffset(0x00)] public float m_dynamicFriction;
    //    [FieldOffset(0x04)] public float m_staticFriction;
    //    [FieldOffset(0x08)] public float m_restitution;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInputMap> -> +1541910
 */

[StructLayout(LayoutKind.Explicit, Size = 0x8, Pack = 0x4)]
internal struct PInputMap {
    //    [FieldOffset(0x00)] public PArray<PInputAction *> m_inputActions;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PInputAction *>> -> +1541E98
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInputAction> -> +1541878
 */

[StructLayout(LayoutKind.Explicit, Size = 0x10, Pack = 0x4)]
internal struct PInputAction {
    //    [FieldOffset(0x04)] public PString m_name;
    //    [FieldOffset(0x08)] public PArray<PInputSource *> m_inputSources;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PInputSource *>> -> +1541D78
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInputSource> -> +1540AD0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x10)]
internal struct PInputSource {
    //    [FieldOffset(0x08)] public PInputTypeSemantic m_typeSemantic;
    //    [FieldOffset(0x0C)] public PUInt32 m_id;
    //    [FieldOffset(0x10)] public bool m_invert;
    //    [FieldOffset(0x14)] public float m_scale;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInputSourceMotionLinearAccelerationZ> -> +15417E0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PInputSourceMotionLinearAccelerationZ {
}
// this class (descriptor) has a parent; PClassDescriptor<PInputSource (sz 0x18, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInputSourceMotionLinearAccelerationY> -> +1541748
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PInputSourceMotionLinearAccelerationY {
}
// this class (descriptor) has a parent; PClassDescriptor<PInputSource (sz 0x18, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInputSourceMotionLinearAccelerationX> -> +15416B0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PInputSourceMotionLinearAccelerationX {
}
// this class (descriptor) has a parent; PClassDescriptor<PInputSource (sz 0x18, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInputSourceMotionAngularVelocityZ> -> +1541618
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PInputSourceMotionAngularVelocityZ {
}
// this class (descriptor) has a parent; PClassDescriptor<PInputSource (sz 0x18, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInputSourceMotionAngularVelocityY> -> +1541580
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PInputSourceMotionAngularVelocityY {
}
// this class (descriptor) has a parent; PClassDescriptor<PInputSource (sz 0x18, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInputSourceMotionAngularVelocityX> -> +15414E8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PInputSourceMotionAngularVelocityX {
}
// this class (descriptor) has a parent; PClassDescriptor<PInputSource (sz 0x18, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInputSourceMotionQuatW> -> +1541450
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PInputSourceMotionQuatW {
}
// this class (descriptor) has a parent; PClassDescriptor<PInputSource (sz 0x18, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInputSourceMotionQuatZ> -> +15413B8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PInputSourceMotionQuatZ {
}
// this class (descriptor) has a parent; PClassDescriptor<PInputSource (sz 0x18, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInputSourceMotionQuatY> -> +1541320
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PInputSourceMotionQuatY {
}
// this class (descriptor) has a parent; PClassDescriptor<PInputSource (sz 0x18, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInputSourceMotionQuatX> -> +1541288
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PInputSourceMotionQuatX {
}
// this class (descriptor) has a parent; PClassDescriptor<PInputSource (sz 0x18, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInputSourceTouchDragY> -> +15410C0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PInputSourceTouchDragY {
}
// this class (descriptor) has a parent; PClassDescriptor<PInputSource (sz 0x18, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInputSourceTouchDragX> -> +1541028
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PInputSourceTouchDragX {
}
// this class (descriptor) has a parent; PClassDescriptor<PInputSource (sz 0x18, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInputSourceTouchTwoFingerDragY> -> +15411F0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PInputSourceTouchTwoFingerDragY {
}
// this class (descriptor) has a parent; PClassDescriptor<PInputSource (sz 0x18, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInputSourceTouchTwoFingerDragX> -> +1541158
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PInputSourceTouchTwoFingerDragX {
}
// this class (descriptor) has a parent; PClassDescriptor<PInputSource (sz 0x18, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInputSourceTouchPinch> -> +1540F90
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PInputSourceTouchPinch {
}
// this class (descriptor) has a parent; PClassDescriptor<PInputSource (sz 0x18, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInputSourceTouchRotate> -> +1540EF8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PInputSourceTouchRotate {
}
// this class (descriptor) has a parent; PClassDescriptor<PInputSource (sz 0x18, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInputSourceMouseDeltaY> -> +1540E60
 */

[StructLayout(LayoutKind.Explicit, Size = 0x28, Pack = 0x4)]
internal struct PInputSourceMouseDeltaY {
    //    [FieldOffset(0x20)] public PInputMouseButtonSemantic m_modifierButtonSemantic;
    //    [FieldOffset(0x24)] public PInputKeySemantic m_modifierKeySemantic;
}
// this class (descriptor) has a parent; PClassDescriptor<PInputSource (sz 0x18, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInputSourceMouseDeltaX> -> +1540DC8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x28, Pack = 0x4)]
internal struct PInputSourceMouseDeltaX {
    //    [FieldOffset(0x20)] public PInputMouseButtonSemantic m_modifierButtonSemantic;
    //    [FieldOffset(0x24)] public PInputKeySemantic m_modifierKeySemantic;
}
// this class (descriptor) has a parent; PClassDescriptor<PInputSource (sz 0x18, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInputSourceMouseButton> -> +1540D30
 */

[StructLayout(LayoutKind.Explicit, Size = 0x20, Pack = 0x4)]
internal struct PInputSourceMouseButton {
    //    [FieldOffset(0x1C)] public PInputMouseButtonSemantic m_mouseButtonSemantic;
}
// this class (descriptor) has a parent; PClassDescriptor<PInputSource (sz 0x18, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInputSourceJoypadAxis> -> +1540C98
 */

[StructLayout(LayoutKind.Explicit, Size = 0x20, Pack = 0x4)]
internal struct PInputSourceJoypadAxis {
    //    [FieldOffset(0x1C)] public PInputAxisSemantic m_joypadAxisSemantic;
}
// this class (descriptor) has a parent; PClassDescriptor<PInputSource (sz 0x18, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInputSourceJoypadButton> -> +1540C00
 */

[StructLayout(LayoutKind.Explicit, Size = 0x28, Pack = 0x4)]
internal struct PInputSourceJoypadButton {
    //    [FieldOffset(0x20)] public PInputJoypadButtonSemantic m_positiveButtonSemantic;
    //    [FieldOffset(0x24)] public PInputJoypadButtonSemantic m_negativeButtonSemantic;
}
// this class (descriptor) has a parent; PClassDescriptor<PInputSource (sz 0x18, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInputSourceKey> -> +1540B68
 */

[StructLayout(LayoutKind.Explicit, Size = 0x28, Pack = 0x4)]
internal struct PInputSourceKey {
    //    [FieldOffset(0x20)] public PInputKeySemantic m_positiveKeySemantic;
    //    [FieldOffset(0x24)] public PInputKeySemantic m_negativeKeySemantic;
}
// this class (descriptor) has a parent; PClassDescriptor<PInputSource (sz 0x18, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PGameSettings> -> +16855B8
 */

[StructLayout(LayoutKind.Explicit, Size = 0xC, Pack = 0x4)]
internal struct PGameSettings {
    //    [FieldOffset(0x08)] public PInputMap m_inputMap;
    //    [FieldOffset(0x00)] public PArray<PInputMap *> m_inputMaps;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PInputMap *>> -> +16856B0
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPostEffectManager> -> +15448A0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x10, Pack = 0x4)]
internal struct PPostEffectManager {
    //    [FieldOffset(0x00)] public PArray<PPostEffectBase *> m_postEffects;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PPostEffectBase *>> -> +1545010
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PPostEffectBase> -> +15435E8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14, Pack = 0x4)]
internal struct PPostEffectBase {
    //    [FieldOffset(0x08)] public PMaterial m_effectMaterial;
    //    [FieldOffset(0x11)] public bool m_enabled;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PScreenSpaceReflectionBase> -> +15446D8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x94, Pack = 0x4)]
internal struct PScreenSpaceReflectionBase {
    //    [FieldOffset(0x14)] public float m_minReflectionDirZ;
    //    [FieldOffset(0x18)] public float m_marchStepFactor;
}
// this class (descriptor) has a parent; PClassDescriptor<PPostEffectBase (sz 0x14, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PScreenSpaceReflectionD3D11> -> +1544808
 */

[StructLayout(LayoutKind.Explicit, Size = 0x94, Pack = 0x4)]
internal struct PScreenSpaceReflectionD3D11 {
}
// this class (descriptor) has a parent; PClassDescriptor<PScreenSpaceReflectionBase (sz 0x94, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PScreenSpaceReflection> -> +1544770
 */

[StructLayout(LayoutKind.Explicit, Size = 0x94, Pack = 0x4)]
internal struct PScreenSpaceReflection {
}
// this class (descriptor) has a parent; PClassDescriptor<PScreenSpaceReflectionD3D11 (sz 0x94, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PScreenSpaceAmbientOcclusionBase> -> +1543BD8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x28, Pack = 0x4)]
internal struct PScreenSpaceAmbientOcclusionBase {
}
// this class (descriptor) has a parent; PClassDescriptor<PPostEffectBase (sz 0x14, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PScreenSpaceAmbientOcclusionD3D11> -> +1543D08
 */

[StructLayout(LayoutKind.Explicit, Size = 0x28, Pack = 0x4)]
internal struct PScreenSpaceAmbientOcclusionD3D11 {
}
// this class (descriptor) has a parent; PClassDescriptor<PScreenSpaceAmbientOcclusionBase (sz 0x28, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PScreenSpaceAmbientOcclusion> -> +1543C70
 */

[StructLayout(LayoutKind.Explicit, Size = 0x28, Pack = 0x4)]
internal struct PScreenSpaceAmbientOcclusion {
}
// this class (descriptor) has a parent; PClassDescriptor<PScreenSpaceAmbientOcclusionD3D11 (sz 0x28, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PMotionBlurBase> -> +1543A10
 */

[StructLayout(LayoutKind.Explicit, Size = 0xD4C, Pack = 0x4)]
internal struct PMotionBlurBase {
    //    [FieldOffset(0xCD4)] public float m_velocityScale;
}
// this class (descriptor) has a parent; PClassDescriptor<PPostEffectBase (sz 0x14, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PMotionBlurD3D11> -> +1543B40
 */

[StructLayout(LayoutKind.Explicit, Size = 0xD50, Pack = 0x4)]
internal struct PMotionBlurD3D11 {
}
// this class (descriptor) has a parent; PClassDescriptor<PMotionBlurBase (sz 0xD4C, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PMotionBlur> -> +1543AA8
 */

[StructLayout(LayoutKind.Explicit, Size = 0xD50, Pack = 0x4)]
internal struct PMotionBlur {
}
// this class (descriptor) has a parent; PClassDescriptor<PMotionBlurD3D11 (sz 0xD50, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PMLAABase> -> +1543DF0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x90, Pack = 0x4)]
internal struct PMLAABase {
}
// this class (descriptor) has a parent; PClassDescriptor<PPostEffectBase (sz 0x14, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PMLAAD3D11> -> +1543F20
 */

[StructLayout(LayoutKind.Explicit, Size = 0x90, Pack = 0x4)]
internal struct PMLAAD3D11 {
}
// this class (descriptor) has a parent; PClassDescriptor<PMLAABase (sz 0x90, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PMLAA> -> +1543E88
 */

[StructLayout(LayoutKind.Explicit, Size = 0x90, Pack = 0x4)]
internal struct PMLAA {
}
// this class (descriptor) has a parent; PClassDescriptor<PMLAAD3D11 (sz 0x90, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PGlowBase> -> +1544050
 */

[StructLayout(LayoutKind.Explicit, Size = 0x90, Pack = 0x4)]
internal struct PGlowBase {
    //    [FieldOffset(0x68)] public float m_glowAmountScale;
    //    [FieldOffset(0x6C)] public float m_glowLuminanceThreshold;
    //    [FieldOffset(0x70)] public float m_glowLuminanceScale;
}
// this class (descriptor) has a parent; PClassDescriptor<PPostEffectBase (sz 0x14, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PGlowGPUBase> -> +15440E8
 */

[StructLayout(LayoutKind.Explicit, Size = 0xAC, Pack = 0x4)]
internal struct PGlowGPUBase {
}
// this class (descriptor) has a parent; PClassDescriptor<PGlowBase (sz 0x90, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PGlowD3D11> -> +1544218
 */

[StructLayout(LayoutKind.Explicit, Size = 0xAC, Pack = 0x4)]
internal struct PGlowD3D11 {
}
// this class (descriptor) has a parent; PClassDescriptor<PGlowGPUBase (sz 0xAC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PGlow> -> +1544180
 */

[StructLayout(LayoutKind.Explicit, Size = 0xAC, Pack = 0x4)]
internal struct PGlow {
}
// this class (descriptor) has a parent; PClassDescriptor<PGlowD3D11 (sz 0xAC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PFXAABase> -> +1544510
 */

[StructLayout(LayoutKind.Explicit, Size = 0x70, Pack = 0x4)]
internal struct PFXAABase {
}
// this class (descriptor) has a parent; PClassDescriptor<PPostEffectBase (sz 0x14, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PFXAAD3D11> -> +1544640
 */

[StructLayout(LayoutKind.Explicit, Size = 0x70, Pack = 0x4)]
internal struct PFXAAD3D11 {
}
// this class (descriptor) has a parent; PClassDescriptor<PFXAABase (sz 0x70, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PFXAA> -> +15445A8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x70, Pack = 0x4)]
internal struct PFXAA {
}
// this class (descriptor) has a parent; PClassDescriptor<PFXAAD3D11 (sz 0x70, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PDepthOfFieldBase> -> +1543848
 */

[StructLayout(LayoutKind.Explicit, Size = 0xFB0, Pack = 0x4)]
internal struct PDepthOfFieldBase {
    //    [FieldOffset(0x14)] public float m_focusPlaneDistance;
    //    [FieldOffset(0x18)] public float m_focusRange;
    //    [FieldOffset(0x1C)] public float m_focusBlurRange;
}
// this class (descriptor) has a parent; PClassDescriptor<PPostEffectBase (sz 0x14, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PDepthOfFieldD3D11> -> +1543978
 */

[StructLayout(LayoutKind.Explicit, Size = 0xFC0, Pack = 0x4)]
internal struct PDepthOfFieldD3D11 {
}
// this class (descriptor) has a parent; PClassDescriptor<PDepthOfFieldBase (sz 0xFB0, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PDepthOfField> -> +15438E0
 */

[StructLayout(LayoutKind.Explicit, Size = 0xFC0, Pack = 0x4)]
internal struct PDepthOfField {
}
// this class (descriptor) has a parent; PClassDescriptor<PDepthOfFieldD3D11 (sz 0xFC0, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PDeferredLightingBase> -> +1543680
 */

[StructLayout(LayoutKind.Explicit, Size = 0x3F94, Pack = 0x4)]
internal struct PDeferredLightingBase {
    //    [FieldOffset(0x3E90)] public Vector4 m_ambientColor;
    //    [FieldOffset(0x3EC8)] public float m_instantLightIntensity;
    //    [FieldOffset(0x3ECC)] public float m_instantLightScatteringIntensity;
    //    [FieldOffset(0x3EB0)] public float m_fogDistance;
    //    [FieldOffset(0x3EB4)] public float m_fogNearDistance;
    //    [FieldOffset(0x3EB8)] public float m_fogAmount;
    //    [FieldOffset(0x3EA0)] public Vector4 m_fogColor;
}
// this class (descriptor) has a parent; PClassDescriptor<PPostEffectBase (sz 0x14, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PDeferredLightingD3D11> -> +15437B0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x4040, Pack = 0x4)]
internal struct PDeferredLightingD3D11 {
}
// this class (descriptor) has a parent; PClassDescriptor<PDeferredLightingBase (sz 0x3F94, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PDeferredLighting> -> +1543718
 */

[StructLayout(LayoutKind.Explicit, Size = 0x4040, Pack = 0x4)]
internal struct PDeferredLighting {
}
// this class (descriptor) has a parent; PClassDescriptor<PDeferredLightingD3D11 (sz 0x4040, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PBitmapFontCharInfo> -> +1684C88
 */

[StructLayout(LayoutKind.Explicit, Size = 0x30, Pack = 0x4)]
internal struct PBitmapFontCharInfo {
    //    [FieldOffset(0x00)] public PInt32 m_characterCode;
    //    [FieldOffset(0x04)] public PInt32 m_kernPairs;
    //    [FieldOffset(0x08)] public PInt32 m_kernOffset;
    //    [FieldOffset(0x0C)] public float m_uv;
    //    [FieldOffset(0x14)] public float m_width;
    //    [FieldOffset(0x18)] public float m_height;
    //    [FieldOffset(0x1C)] public float m_offset;
    //    [FieldOffset(0x24)] public float m_advance;
    //    [FieldOffset(0x2C)] public bool m_rotated;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PBitmapFont> -> +1684D20
 */

[StructLayout(LayoutKind.Explicit, Size = 0x24, Pack = 0x4)]
internal struct PBitmapFont {
    //    [FieldOffset(0x04)] public PUInt32 m_fontSize;
    //    [FieldOffset(0x00)] public bool m_isSDF;
    //    [FieldOffset(0x08)] public float m_lineSpacing;
    //    [FieldOffset(0x0C)] public float m_baselineOffset;
    //    [FieldOffset(0x10)] public PArray<PBitmapFontCharInfo> m_characterInfo;
    //    [FieldOffset(0x18)] public PArray<PInt32> m_kerningInfo;
    //    [FieldOffset(0x20)] public PTexture2D m_bitmapFontTexture;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PInt32>> -> +899F70
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PBitmapFontCharInfo>> -> +16850B0
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PRandomGenerator> -> +894350
 */

[StructLayout(LayoutKind.Explicit, Size = 0x1, Pack = 0x1)]
internal struct PRandomGenerator {
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PTypedObject>> -> +893C58
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PClusterDependencyList> -> +891958
 */

[StructLayout(LayoutKind.Explicit, Size = 0x8, Pack = 0x4)]
internal struct PClusterDependencyList {
    //    [FieldOffset(0x00)] public PSharray<PString> m_dependencyList;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PSharray<PString>> -> +891A28
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PTypedObject> -> +890F30
 */

[StructLayout(LayoutKind.Explicit, Size = 0x8, Pack = 0x4)]
internal struct PTypedObject {
    //    [FieldOffset(0x00)] public PBase m_object;
    //    [FieldOffset(0x04)] public PClassDescriptor m_classDescriptor;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAssetReferenceImport> -> +894030
 */

[StructLayout(LayoutKind.Explicit, Size = 0xC, Pack = 0x4)]
internal struct PAssetReferenceImport {
    //    [FieldOffset(0x04)] public PString m_id;
    //    [FieldOffset(0x00)] public PClassDescriptor m_targetAssetType;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAssetReference> -> +893F98
 */

[StructLayout(LayoutKind.Explicit, Size = 0x28, Pack = 0x4)]
internal struct PAssetReference {
    //    [FieldOffset(0x18)] public PString m_id;
    //    [FieldOffset(0x1C)] public PBase m_asset;
    //    [FieldOffset(0x20)] public PClassDescriptor m_assetType;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PString> -> +891048
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PCamera> -> +8922C0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14C, Pack = 0x4)]
internal struct PCamera {
    //    [FieldOffset(0x00)] public PMatrix4x3 m_viewMatrix;
    //    [FieldOffset(0x30)] public Matrix4 m_projectionMatrix;
    //    [FieldOffset(0x70)] public Matrix4 m_viewProjectionMatrix;
    //    [FieldOffset(0xB0)] public PWorldMatrix m_localToWorldMatrix;
    //    [FieldOffset(0xB4)] public float m_nearPlane;
    //    [FieldOffset(0xB8)] public float m_farPlane;
    //    [FieldOffset(0xBC)] public Vector3 m_ambientColor;
    //    [FieldOffset(0xCC)] public PString m_name;
    //    [FieldOffset(0xF0)] public Vector3 m_bgColor;
    //    [FieldOffset(0xD0)] public Vector3 m_fogColor;
    //    [FieldOffset(0xE0)] public float m_fogNear;
    //    [FieldOffset(0xE4)] public float m_fogFar;
    //    [FieldOffset(0xE8)] public float m_fogCurve;
    //    [FieldOffset(0xEC)] public float m_fogLimit;
    //    [FieldOffset(0x100)] public float m_glowUpperThreshold;
    //    [FieldOffset(0x104)] public float m_glowLowerThreshold;
    //    [FieldOffset(0x108)] public float m_glowGain;
    //    [FieldOffset(0x10C)] public float m_glowSpread;
    //    [FieldOffset(0x110)] public float m_dofFocusPlaneDistance;
    //    [FieldOffset(0x114)] public float m_dofFocusRange;
    //    [FieldOffset(0x118)] public float m_dofFocusBlurRange;
    //    [FieldOffset(0x11C)] public Vector4 m_charcterLightIntensity;
    //    [FieldOffset(0x12C)] public Vector4 m_charcterLightPitch;
    //    [FieldOffset(0x13C)] public Vector4 m_characterLightYaw;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PCameraProjection> -> +892358
 */

[StructLayout(LayoutKind.Explicit, Size = 0x154, Pack = 0x10)]
internal struct PCameraProjection {
    //    [FieldOffset(0x150)] public float m_aspect;
}
// this class (descriptor) has a parent; PClassDescriptor<PCamera (sz 0x14C, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PCameraOrthographic> -> +8930E8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x158, Pack = 0x4)]
internal struct PCameraOrthographic {
    //    [FieldOffset(0x154)] public float m_height;
}
// this class (descriptor) has a parent; PClassDescriptor<PCameraProjection (sz 0x154, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PCameraPerspective> -> +893608
 */

[StructLayout(LayoutKind.Explicit, Size = 0x158, Pack = 0x4)]
internal struct PCameraPerspective {
    //    [FieldOffset(0x154)] public float m_FOV;
}
// this class (descriptor) has a parent; PClassDescriptor<PCameraProjection (sz 0x154, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PWorldMatrix> -> +892090
 */

[StructLayout(LayoutKind.Explicit, Size = 0x30, Pack = 0x4)]
internal struct PWorldMatrix {
    //    [FieldOffset(0x00)] public PMatrix4x3 m_matrix;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PComponent> -> +891C18
 */

[StructLayout(LayoutKind.Explicit, Size = 0xC, Pack = 0x4)]
internal struct PComponent {
    //    [FieldOffset(0x04)] public PEntity m_entity;
    //    [FieldOffset(0x00)] public PComponent m_next;
    //    [FieldOffset(0x08)] public PClassDescriptor m_componentType;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAnimatableComponent> -> +8ADA40
 */

[StructLayout(LayoutKind.Explicit, Size = 0x84, Pack = 0x4)]
internal struct PAnimatableComponent {
    //    [FieldOffset(0x10)] public PAnimationSet m_animationSet;
    //    [FieldOffset(0x14)] public PArray<PBlendableAnimationSource> m_animationClips;
    //    [FieldOffset(0x1C)] public PAnimationWeightedBlenderController m_animationWeightedBlenderController;
    //    [FieldOffset(0x38)] public PAnimationTargetBlenderController m_animationTargetBlenderController;
    //    [FieldOffset(0x48)] public PAnimationNetworkInstance m_animationNetworkInstance;
}
// this class (descriptor) has a parent; PClassDescriptor<PComponent (sz 0xC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PAttachableComponent> -> +8B0AC0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x6C, Pack = 0x4)]
internal struct PAttachableComponent {
    //    [FieldOffset(0x0C)] public PMatrix4 m_offsetMatrix;
    //    [FieldOffset(0x4C)] public PTypedObject m_targetObject;
    //    [FieldOffset(0x54)] public PString m_targetName;
    //    [FieldOffset(0x58)] public PNode m_nodeToUpdate;
    //    [FieldOffset(0x5C)] public PInt32 m_boneIndex;
    //    [FieldOffset(0x60)] public PTypedObject m_attachedObject;
    //    [FieldOffset(0x68)] public bool m_isEnabled;
}
// this class (descriptor) has a parent; PClassDescriptor<PComponent (sz 0xC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PScriptableComponent> -> +8AF780
 */

[StructLayout(LayoutKind.Explicit, Size = 0x20, Pack = 0x10)]
internal struct PScriptableComponent {
    //    [FieldOffset(0x14)] public PScript m_script;
    //    [FieldOffset(0x10)] public PString m_entryPoint;
}
// this class (descriptor) has a parent; PClassDescriptor<PComponent (sz 0xC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PScriptedComponent> -> +8AF8D0
 */

[StructLayout(LayoutKind.Explicit, Size = 0x30, Pack = 0x4)]
internal struct PScriptedComponent {
    //    [FieldOffset(0x20)] public PString m_onLoadEntryPoint;
    //    [FieldOffset(0x24)] public PString m_onUnloadEntryPoint;
    //    [FieldOffset(0x28)] public PUInt32 m_executionInterval;
    //    [FieldOffset(0x2E)] public bool m_suspendOnInitialization;
}
// this class (descriptor) has a parent; PClassDescriptor<PScriptableComponent (sz 0x20, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PCameraControllerComponent> -> +8AFF10
 */

[StructLayout(LayoutKind.Explicit, Size = 0x2C0, Pack = 0x4)]
internal struct PCameraControllerComponent {
    //    [FieldOffset(0x30)] public PCameraProjection m_camera;
    //    [FieldOffset(0xB0)] public bool m_isMainCamera;
    //    [FieldOffset(0xB1)] public bool m_isFollowCamera;
    //    [FieldOffset(0xB4)] public PWorldMatrix m_followTarget;
    //    [FieldOffset(0xB8)] public PNode m_nodeToUpdate;
    //    [FieldOffset(0xBC)] public PPhysicsCharacterCamera m_followPhysics;
}
// this class (descriptor) has a parent; PClassDescriptor<PScriptedComponent (sz 0x30, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PTimerComponent> -> +8AFB70
 */

[StructLayout(LayoutKind.Explicit, Size = 0x2C, Pack = 0x4)]
internal struct PTimerComponent {
    //    [FieldOffset(0x20)] public float m_timeOut;
}
// this class (descriptor) has a parent; PClassDescriptor<PScriptableComponent (sz 0x20, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PQuarryComponent> -> +8AF540
 */

[StructLayout(LayoutKind.Explicit, Size = 0x5C, Pack = 0x4)]
internal struct PQuarryComponent {
    //    [FieldOffset(0x0C)] public PWorldMatrix m_previousLocalToWorldMatrix;
    //    [FieldOffset(0x3C)] public Vector3 m_min;
    //    [FieldOffset(0x4C)] public Vector3 m_size;
}
// this class (descriptor) has a parent; PClassDescriptor<PComponent (sz 0xC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PTriggerReceiverComponent> -> +8AF388
 */

[StructLayout(LayoutKind.Explicit, Size = 0x2C, Pack = 0x4)]
internal struct PTriggerReceiverComponent {
    //    [FieldOffset(0x0C)] public PScript m_script;
    //    [FieldOffset(0x14)] public PScriptCallbackHandler m_entryHandler;
    //    [FieldOffset(0x20)] public PScriptCallbackHandler m_exitHandler;
}
// this class (descriptor) has a parent; PClassDescriptor<PComponent (sz 0xC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PSplineFollowerComponent> -> +8AFD60
 */

[StructLayout(LayoutKind.Explicit, Size = 0x1C, Pack = 0x4)]
internal struct PSplineFollowerComponent {
    //    [FieldOffset(0x0C)] public PSpline m_spline;
    //    [FieldOffset(0x10)] public float m_speed;
    //    [FieldOffset(0x14)] public float m_distanceTravelled;
    //    [FieldOffset(0x18)] public bool m_autoUpdate;
}
// this class (descriptor) has a parent; PClassDescriptor<PComponent (sz 0xC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PNameComponent> -> +893E38
 */

[StructLayout(LayoutKind.Explicit, Size = 0x18, Pack = 0x4)]
internal struct PNameComponent {
    //    [FieldOffset(0x14)] public PString m_name;
}
// this class (descriptor) has a parent; PClassDescriptor<PComponent (sz 0xC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInstancesComponent> -> +893AE8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x14, Pack = 0x4)]
internal struct PInstancesComponent {
    //    [FieldOffset(0x0C)] public PArray<PTypedObject> m_instances;
}
// this class (descriptor) has a parent; PClassDescriptor<PComponent (sz 0xC, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PEntity> -> +891DD8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x1C, Pack = 0x4)]
internal struct PEntity {
    //    [FieldOffset(0x00)] public PComponent m_firstComponent;
    //    [FieldOffset(0x04)] public PWorldMatrix m_worldMatrix;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PWorld> -> +890E30
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PCluster> -> +890B90
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PArray<PUInt8>> -> +8915C8
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PClassDescriptor> -> +890760
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PClassDescriptorDynamic> -> +891330
 */

// this class (descriptor) has a parent; PClassDescriptor<PClassDescriptor (sz 0x94, align 0x10)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PClassMember> -> +890610
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PClassCallableMethodScript> -> +8C9A40
 */

[StructLayout(LayoutKind.Explicit, Size = 0x34, Pack = 0x4)]
internal struct PClassCallableMethodScript {
    //    [FieldOffset(0x28)] public PScript m_script;
    //    [FieldOffset(0x2C)] public PString m_nameAsString;
    //    [FieldOffset(0x30)] public PString m_entryPoint;
}
// this class (descriptor) has a parent; PClassDescriptor<PClassMember (sz 0x14, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PClassDataMemberDynamic> -> +891180
 */

[StructLayout(LayoutKind.Explicit, Size = 0x30, Pack = 0x4)]
internal struct PClassDataMemberDynamic {
    //    [FieldOffset(0x18)] public PType m_type;
    //    [FieldOffset(0x24)] public PUInt32 m_offset;
    //    [FieldOffset(0x2C)] public PString m_nameAsString;
}
// this class (descriptor) has a parent; PClassDescriptor<PClassMember (sz 0x14, align 0x4)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PNamespace> -> +890A18
 */

// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PInputMapper> -> +8C9E50
 */

[StructLayout(LayoutKind.Explicit, Size = 0xB8, Pack = 0x4)]
internal struct PInputMapper {
    //    [FieldOffset(0x00)] public PInputMap m_inputMap;
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>

/* [fkelava 26/04/26 14:52]
 * PClassDescriptor<PApplicationViewport> -> +8C9DB8
 */

[StructLayout(LayoutKind.Explicit, Size = 0x28, Pack = 0x4)]
internal struct PApplicationViewport {
}
// this class (descriptor) has a parent; PClassDescriptor<PBase (sz 0x0, align 0x1)>
