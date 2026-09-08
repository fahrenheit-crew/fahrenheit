// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

/* [fkelava 26/04/26 15:26]
 * Unlike `call.g.cs`, which contains source-generated delegates with no guarantee of accuracy,
 * this file contains manually annotated calls with proper Fahrenheit types that are vetted for functionality.
 *
 * This file is for calls which are exclusive to FF X and not shared with X-2/LM.
 */

using Fahrenheit.Atel;
using Fahrenheit.FFX.Battle;
using Fahrenheit.FFX.Ids;

namespace Fahrenheit.FFX;

/// <summary>
///     An accessor for game function calls exclusive to FF X.
/// </summary>
public static partial class FhCall {

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> CT_0000_Init
        => new( new FhMethodLocation("FFX.exe", 0x45C3E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt> CT_0001_RetInt
        => new( new FhMethodLocation("FFX.exe", 0x45CE70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> CT_5010_Init
        => new( new FhMethodLocation("FFX.exe", 0x679820) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt> CT_5021_RetInt
        => new( new FhMethodLocation("FFX.exe", 0x679510) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt> CT_504C_RetInt
        => new( new FhMethodLocation("FFX.exe", 0x6786A0) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public unsafe delegate void d_PhyreScene_doDeleteMeshInstances(uint ptr_this);
    public static FhMethodHandle<d_PhyreScene_doDeleteMeshInstances> PhyreScene_doDeleteMeshInstances
        => new( new FhMethodLocation("FFX.exe", 0x25A7B0) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public unsafe delegate void d_DynGeoMemManager_clearDynGeoMemory(uint ptr_this);
    public static FhMethodHandle<d_DynGeoMemManager_clearDynGeoMemory> DynGeoMemManager_clearDynGeoMemory
        => new( new FhMethodLocation("FFX.exe", 0x2DE830) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_JobSchedule_pppPart_fillBuffer_Kick(uint param_1);
    public static FhMethodHandle<d_JobSchedule_pppPart_fillBuffer_Kick> JobSchedule_pppPart_fillBuffer_Kick
        => new( new FhMethodLocation("FFX.exe", 0x31EE90) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_JobSchedule_pppPart_clearFillBufferReques();
    public static FhMethodHandle<d_JobSchedule_pppPart_clearFillBufferReques> JobSchedule_pppPart_clearFillBufferRequest
        => new( new FhMethodLocation("FFX.exe", 0x31EDB0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_ClassVFXRenderDataTable_Clear(ClassVFXRenderDataTable* ptr_this);
    public static FhMethodHandle<d_ClassVFXRenderDataTable_Clear> ClassVFXRenderDataTable_Clear
        => new( new FhMethodLocation("FFX.exe", 0x29F140) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public unsafe delegate void d_ClassVFXRenderDataTable_ClearVFXDrawData(ClassVFXRenderDataTable* ptr_this);
    public static FhMethodHandle<d_ClassVFXRenderDataTable_ClearVFXDrawData> ClassVFXRenderDataTable_ClearVFXDrawData
        => new( new FhMethodLocation("FFX.exe", 0x29F3F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_graphicDynGeoMemManagerSwapBuffer(uint param_1);
    public static FhMethodHandle<d_graphicDynGeoMemManagerSwapBuffer> graphicDynGeoMemManagerSwapBuffer
        => new( new FhMethodLocation("FFX.exe", 0x240120) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_graphicVFXUpdateWithType(uint param_1, uint param_2, uint type);
    public static FhMethodHandle<d_graphicVFXUpdateWithType> graphicVFXUpdateWithType
        => new( new FhMethodLocation("FFX.exe", 0x245D50) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_graphicVFXDestroy(uint param_1, uint param_2);
    public static FhMethodHandle<d_graphicVFXDestroy> graphicVFXDestroy
        => new( new FhMethodLocation("FFX.exe", 0x245430) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_graphicVFXCreate(uint param_1, uint param_2, uint flags);
    public static FhMethodHandle<d_graphicVFXCreate> graphicVFXCreate
        => new( new FhMethodLocation("FFX.exe", 0x244F90) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_graphicVFXSetVisible(uint param_1, uint param_2, uint visible);
    public static FhMethodHandle<d_graphicVFXSetVisible> graphicVFXSetVisible
        => new( new FhMethodLocation("FFX.exe", 0x2458C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_graphicVFXUpdate(uint param_1, uint param_2);
    public static FhMethodHandle<d_graphicVFXUpdate> graphicVFXUpdate
        => new( new FhMethodLocation("FFX.exe", 0x245A50) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_hideDisplayOffParticle(uint ptr_par);
    public static FhMethodHandle<d_hideDisplayOffParticle> hideDisplayOffParticle
        => new( new FhMethodLocation("FFX.exe", 0x328F30) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d__pppRunPartFp(uint ptr_par, byte flags);
    public static FhMethodHandle<d__pppRunPartFp> _pppRunPartFp
        => new( new FhMethodLocation("FFX.exe", 0x3123D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d__pppRunPart(uint ptr_par, byte flags);
    public static FhMethodHandle<d__pppRunPart> _pppRunPart
        => new( new FhMethodLocation("FFX.exe", 0x312330) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_op_loop_ffx_after();
    public static FhMethodHandle<d_op_loop_ffx_after> op_loop_ffx_after
        => new( new FhMethodLocation("FFX.exe", 0x3FF5A0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_op_loop_ffx_before();
    public static FhMethodHandle<d_op_loop_ffx_before> op_loop_ffx_before
        => new( new FhMethodLocation("FFX.exe", 0x3FF690) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsEffectStart();
    public static FhMethodHandle<d_MsEffectStart> MsEffectStart
        => new( new FhMethodLocation("FFX.exe", 0x388540) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_op_ot_draw();
    public static FhMethodHandle<d_op_ot_draw> op_ot_draw
        => new( new FhMethodLocation("FFX.exe", 0x3EC390) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_pppPartLoop();
    public static FhMethodHandle<d_pppPartLoop> pppPartLoop
        => new( new FhMethodLocation("FFX.exe", 0x362330) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsSetChrStatInfo(uint chr_id, uint stat_id, uint target_id, uint value);
    public static FhMethodHandle<d_MsSetChrStatInfo> MsSetChrStatInfo
        => new( new FhMethodLocation("FFX.exe", 0x3B4B80) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_003B4AA0(uint chr_id, uint stat_id, float value);
    public static FhMethodHandle<d_FUN_003B4AA0> FUN_003B4AA0
        => new( new FhMethodLocation("FFX.exe", 0x3B4AA0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_Ch_SetKeepFps(Actor* ptr_actor, uint keep_fps);
    public static FhMethodHandle<d_Ch_SetKeepFps> Ch_SetKeepFps
        => new( new FhMethodLocation("FFX.exe", 0x439BD0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_rcefObjProc(RcEffectObj* ptr_rcef_obj);
    public static FhMethodHandle<d_rcefObjProc> rcefObjProc
        => new( new FhMethodLocation("FFX.exe", 0x530060) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_Sg_AccSetAlpha(ushort alpha, ushort frames);
    public static FhMethodHandle<d_Sg_AccSetAlpha> Sg_AccSetAlpha
        => new( new FhMethodLocation("FFX.exe", 0x42BD90) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_Sg_GetCurExecFrames();
    public static FhMethodHandle<d_Sg_GetCurExecFrames> Sg_GetCurExecFrames
        => new( new FhMethodLocation("FFX.exe", 0x42D860) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_graphicVideoUpdate();
    public static FhMethodHandle<d_graphicVideoUpdate> graphicVideoUpdate
        => new( new FhMethodLocation("FFX.exe", 0x245FA0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsEffectSetSpeed(byte chr_id, ushort speed);
    public static FhMethodHandle<d_MsEffectSetSpeed> MsEffectSetSpeed
        => new( new FhMethodLocation("FFX.exe", 0x3884F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_graphicDrawMainMenuWaterEffect();
    public static FhMethodHandle<d_graphicDrawMainMenuWaterEffect> graphicDrawMainMenuWaterEffect
        => new( new FhMethodLocation("FFX.exe", 0x23EAD0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_Sg_EffectRoutineEnable(byte arg1);
    public static FhMethodHandle<d_Sg_EffectRoutineEnable> Sg_EffectRoutineEnable
        => new( new FhMethodLocation("FFX.exe", 0x420490) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate Actor* d_AtelGetCharHandle(AtelBasicWorker* ptr_worker);
    public static FhMethodHandle<d_AtelGetCharHandle> AtelGetCharHandle
        => new( new FhMethodLocation("FFX.exe", 0x46AE10) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_Ch_CalcAnim(float delta);
    public static FhMethodHandle<d_Ch_CalcAnim> Ch_CalcMain
        => new( new FhMethodLocation("FFX.exe", 0x432E90) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_Ch_SetMotionSpeed(Actor* ptr_actor, ushort speed);
    public static FhMethodHandle<d_Ch_SetMotionSpeed> Ch_SetMotionSpeed
        => new( new FhMethodLocation("FFX.exe", 0x42B400) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOBtlCtrlLimitTimer();
    public static FhMethodHandle<d_TOBtlCtrlLimitTimer> TOBtlCtrlLimitTimer
        => new( new FhMethodLocation("FFX.exe", 0x491A30) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate PlySave* d_MsGetSavePlayerPtr(int ply_id);
    public static FhMethodHandle<d_MsGetSavePlayerPtr> MsGetSavePlayerPtr
        => new ( new FhMethodLocation("FFX.exe", 0x3853F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_TkMenuGetPlayerListMax2();
    public static FhMethodHandle<d_TkMenuGetPlayerListMax2> TkMenuGetPlayerListMax2
        => new ( new FhMethodLocation("FFX.exe", 0x4A9B00) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMenuOpenPktBuffTmp();
    public static FhMethodHandle<d_TOMenuOpenPktBuffTmp> TOMenuOpenPktBuffTmp
        => new ( new FhMethodLocation("FFX.exe", 0x4BEF00) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TODrawMenuBG();
    public static FhMethodHandle<d_TODrawMenuBG> TODrawMenuBG
        => new ( new FhMethodLocation("FFX.exe", 0x4F5C10) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte d_TkMenuGetPlayerFromIndex2(int ply_idx);
    public static FhMethodHandle<d_TkMenuGetPlayerFromIndex2> TkMenuGetPlayerFromIndex2
        => new ( new FhMethodLocation("FFX.exe", 0x4A9AB0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate float d_graphicUiRemapX2(float x);
    public static FhMethodHandle<d_graphicUiRemapX2> graphicUiRemapX2
        => new ( new FhMethodLocation("FFX.exe", 0x244990) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate float d_graphicUiRemapY2(float y);
    public static FhMethodHandle<d_graphicUiRemapY2> graphicUiRemapY2
        => new ( new FhMethodLocation("FFX.exe", 0x2449D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_FUN_008a9b20();
    public static FhMethodHandle<d_FUN_008a9b20> FUN_008a9b20
        => new ( new FhMethodLocation("FFX.exe", 0x4A9B20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_DrawWaterWaveShapeC2(float x, float y, float w, float h, float start_x, float start_y, float param_7, float param_8, float move_x, float move_y, uint color_start, uint color_end);
    public static FhMethodHandle<d_DrawWaterWaveShapeC2> DrawWaterWaveShapeC2
        => new ( new FhMethodLocation("FFX.exe", 0x4E7D30) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte d_FUN_008a9a20(int ply_id);
    public static FhMethodHandle<d_FUN_008a9a20> FUN_008a9a20
        => new ( new FhMethodLocation("FFX.exe", 0x4A9A20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_ToMakeBtlEasyEdgeFont(byte* text, float x, float y, byte color_id, float scale, float param_6);
    public static FhMethodHandle<d_ToMakeBtlEasyEdgeFont> ToMakeBtlEasyEdgeFont
        => new ( new FhMethodLocation("FFX.exe", 0x505930) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate FhLangId d_TOGetFFXLang();
    public static FhMethodHandle<d_TOGetFFXLang> TOGetFFXLang
        => new ( new FhMethodLocation("FFX.exe", 0x4AC2A0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_TkMenuGetMaxHP(int ply_id);
    public static FhMethodHandle<d_TkMenuGetMaxHP> TkMenuGetMaxHP
        => new ( new FhMethodLocation("FFX.exe", 0x4A9940) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_TkMenuGetHP(int ply_id);
    public static FhMethodHandle<d_TkMenuGetHP> TkMenuGetHP
        => new ( new FhMethodLocation("FFX.exe", 0x4A9870) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_TOMkpCrossEasyStrFontSClut(byte* text, float param_2, float param_3, byte param_4, float param_5, float param_6);
    public static FhMethodHandle<d_TOMkpCrossEasyStrFontSClut> TOMkpCrossEasyStrFontSClut
        => new ( new FhMethodLocation("FFX.exe", 0x501660) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_TkMenuGetMaxMP(int ply_id);
    public static FhMethodHandle<d_TkMenuGetMaxMP> TkMenuGetMaxMP
        => new ( new FhMethodLocation("FFX.exe", 0x4A9960) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_TkMenuGetMP(int ply_id);
    public static FhMethodHandle<d_TkMenuGetMP> TkMenuGetMP
        => new ( new FhMethodLocation("FFX.exe", 0x4A9920) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_FUN_008a9b30(byte ply_id);
    public static FhMethodHandle<d_FUN_008a9b30> FUN_008a9b30
        => new ( new FhMethodLocation("FFX.exe", 0x4A9B30) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_FUN_00905230(int param_1, float* param_2, float param_3, float param_4);
    public static FhMethodHandle<d_FUN_00905230> FUN_00905230
        => new ( new FhMethodLocation("FFX.exe", 0x505230) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_00905820(int param_1, float param_2, float param_3, byte param_4, float param_5, float param_6);
    public static FhMethodHandle<d_FUN_00905820> FUN_00905820
        => new ( new FhMethodLocation("FFX.exe", 0x505820) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_FUN_008a9c10();
    public static FhMethodHandle<d_FUN_008a9c10> FUN_008a9c10
        => new ( new FhMethodLocation("FFX.exe", 0x4A9C10) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_008c13b0(float x, float y, int param_3);
    public static FhMethodHandle<d_FUN_008c13b0> FUN_008c13b0
        => new ( new FhMethodLocation("FFX.exe", 0x4C13B0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TODrawCrossBoxXYWHC2(float x, float y, float w, float h, uint color_start, uint color_end);
    public static FhMethodHandle<d_TODrawCrossBoxXYWHC2> TODrawCrossBoxXYWHC2
        => new ( new FhMethodLocation("FFX.exe", 0x4F4B20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_MsGetGIL();
    public static FhMethodHandle<d_MsGetGIL> MsGetGIL
        => new ( new FhMethodLocation("FFX.exe", 0x384F40) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_008c09f0(float x, float y, float w, float h, int param_5);
    public static FhMethodHandle<d_FUN_008c09f0> FUN_008c09f0
        => new ( new FhMethodLocation("FFX.exe", 0x4C09F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_FUN_008a9c00();
    public static FhMethodHandle<d_FUN_008a9c00> FUN_008a9c00
        => new ( new FhMethodLocation("FFX.exe", 0x4A9C00) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_008e19f0(uint param_1, float param_2, float param_3, int param_4, int param_5);
    public static FhMethodHandle<d_FUN_008e19f0> FUN_008e19f0
        => new ( new FhMethodLocation("FFX.exe", 0x4E19F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMkpShapeXYWHUVC2(uint param_1, float x, float y, float w, float h, float param_6, float param_7, float param_8, float param_9, uint color_start, uint color_end);
    public static FhMethodHandle<d_TOMkpShapeXYWHUVC2> TOMkpShapeXYWHUVC2
        => new ( new FhMethodLocation("FFX.exe", 0x503EE0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsGetSaveConfigEnglish();
    public static FhMethodHandle<d_MsGetSaveConfigEnglish> MsGetSaveConfigEnglish
        => new ( new FhMethodLocation("FFX.exe", 0x385290) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate byte* d_TkMenuDraw1612Width(byte* text);
    public static FhMethodHandle<d_TkMenuDraw1612Width> TkMenuDraw1612Width
        => new ( new FhMethodLocation("FFX.exe", 0x4DC9C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMenuDrawKickTmp();
    public static FhMethodHandle<d_TOMenuDrawKickTmp> TOMenuDrawKickTmp
        => new ( new FhMethodLocation("FFX.exe", 0x4BE9F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate byte* d_TOGetShapTextureName(int param_1);
    public static FhMethodHandle<d_TOGetShapTextureName> TOGetShapTextureName
        => new ( new FhMethodLocation("FFX.exe", 0x4AC870) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_TOGetImageWH(int param_1, float* out_width, float* out_height);
    public static FhMethodHandle<d_TOGetImageWH> TOGetImageWH
        => new ( new FhMethodLocation("FFX.exe", 0x4AC3B0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte d_AtelGetAlbhedRikku();
    public static FhMethodHandle<d_AtelGetAlbhedRikku> AtelGetAlbhedRikku
        => new ( new FhMethodLocation("FFX.exe", 0x46A770) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate SaveData* d_MsGetSaveEventAddress();
    public static FhMethodHandle<d_MsGetSaveEventAddress> MsGetSaveEventAddress
        => new ( new FhMethodLocation("FFX.exe", 0x385300) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetSavePlyJoin(byte ply_id);
    public static FhMethodHandle<d_MsGetSavePlyJoin> MsGetSavePlyJoin
        => new ( new FhMethodLocation("FFX.exe", 0x385440) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsSetSavePlyJoin(int ply_id, int enable);
    public static FhMethodHandle<d_MsSetSavePlyJoin> MsSetSavePlyJoin
        => new ( new FhMethodLocation("FFX.exe", 0x386A70) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_FUN_008bd9d0(int ply_id);
    public static FhMethodHandle<d_FUN_008bd9d0> FUN_008bd9d0
        => new ( new FhMethodLocation("FFX.exe", 0x4BD9D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_ToMakeBtlEasyDigitRight(int param_1, float param_2, float param_3, int param_4, float param_5, float param_6);
    public static FhMethodHandle<d_ToMakeBtlEasyDigitRight> ToMakeBtlEasyDigitRight
        => new ( new FhMethodLocation("FFX.exe", 0x5055C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_ToMakeBtlEasyDigit2(int param_1, float param_2, float param_3, byte param_4, float param_5);
    public static FhMethodHandle<d_ToMakeBtlEasyDigit2> ToMakeBtlEasyDigit2
        => new ( new FhMethodLocation("FFX.exe", 0x505550) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte d_FUN_008bda10(byte param_1);
    public static FhMethodHandle<d_FUN_008bda10> FUN_008bda10
        => new ( new FhMethodLocation("FFX.exe", 0x4BDA10) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsGetNextAP(int ply_id);
    public static FhMethodHandle<d_MsGetNextAP> MsGetNextAP
        => new ( new FhMethodLocation("FFX.exe", 0x384F50) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_FUN_00785370(byte ply_id);
    public static FhMethodHandle<d_FUN_00785370> FUN_00785370
        => new ( new FhMethodLocation("FFX.exe", 0x385370) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate byte* d_MsGetSaveWeaponName(uint inv_idx);
    public static FhMethodHandle<d_MsGetSaveWeaponName> MsGetSaveWeaponName
        => new ( new FhMethodLocation("FFX.exe", 0x3ABE10) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_DrawCrossMenuIconXYWHRGBA(float x, float y, float w, float h, byte icon_idx, byte r, byte g, byte b, byte a);
    public static FhMethodHandle<d_DrawCrossMenuIconXYWHRGBA> DrawCrossMenuIconXYWHRGBA
        => new ( new FhMethodLocation("FFX.exe", 0x4E6AF0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public delegate bool d_MsGetSaveConfigHiragana();
    public static FhMethodHandle<d_MsGetSaveConfigHiragana> MsGetSaveConfigHiragana
        => new ( new FhMethodLocation("FFX.exe", 0x3852B0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate double d_graphicGetTime();
    public static FhMethodHandle<d_graphicGetTime> graphicGetTime
        => new ( new FhMethodLocation("FFX.exe", 0x2415C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMakePktScissor(int param_1, int param_2, int param_3, int param_4);
    public static FhMethodHandle<d_TOMakePktScissor> TOMakePktScissor
        => new ( new FhMethodLocation("FFX.exe", 0x4FDEE0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_TOCheckBtlCommandUse(int chr_id, uint com_id);
    public static FhMethodHandle<d_TOCheckBtlCommandUse> TOCheckBtlCommandUse
        => new ( new FhMethodLocation("FFX.exe", 0x49AC10) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_MsGetCommandMP(int chr_id, Command* command);
    public static FhMethodHandle<d_MsGetCommandMP> MsGetCommandMP
        => new ( new FhMethodLocation("FFX.exe", 0x38D030) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsGetRamChrHP(int chr_id);
    public static FhMethodHandle<d_MsGetRamChrHP> MsGetRamChrHP
        => new ( new FhMethodLocation("FFX.exe", 0x39ADE0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsGetRamChrMP(int chr_id);
    public static FhMethodHandle<d_MsGetRamChrMP> MsGetRamChrMP
        => new ( new FhMethodLocation("FFX.exe", 0x39AE60) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_FUN_00904ba0(byte* param_1, float param_2, float param_3, float param_4, byte param_5, float param_6, uint param_7, int param_8, int param_9, int param_10);
    public static FhMethodHandle<d_FUN_00904ba0> FUN_00904ba0
        => new ( new FhMethodLocation("FFX.exe", 0x504BA0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_TkMenuGetCurrentPlayerPos();
    public static FhMethodHandle<d_TkMenuGetCurrentPlayerPos> TkMenuGetCurrentPlayerPos
        => new ( new FhMethodLocation("FFX.exe", 0x4A9820) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public delegate bool d_FUN_008cfc00();
    public static FhMethodHandle<d_FUN_008cfc00> FUN_008cfc00
        => new ( new FhMethodLocation("FFX.exe", 0x4CFC00) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_008cfcf0(int param_1, int param_2);
    public static FhMethodHandle<d_FUN_008cfcf0> FUN_008cfcf0
        => new ( new FhMethodLocation("FFX.exe", 0x4CFCF0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_FUN_008c2bd0(byte* param_1);
    public static FhMethodHandle<d_FUN_008c2bd0> FUN_008c2bd0
        => new ( new FhMethodLocation("FFX.exe", 0x4C2BD0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_FUN_00798be0(BtlRewardData* get_data);
    public static FhMethodHandle<d_FUN_00798be0> FUN_00798be0
        => new ( new FhMethodLocation("FFX.exe", 0x398BE0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public delegate bool d_MsGetSavePlyJoined(byte ply_id);
    public static FhMethodHandle<d_MsGetSavePlyJoined> MsGetSavePlyJoined
        => new( new FhMethodLocation("FFX.exe", 0x385460) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_FUN_00798aa0(uint aability_id);
    public static FhMethodHandle<d_FUN_00798aa0> FUN_00798aa0
        => new ( new FhMethodLocation("FFX.exe", 0x398AA0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void* d_FUN_008d9140(uint param_1);
    public static FhMethodHandle<d_FUN_008d9140> FUN_008d9140
        => new ( new FhMethodLocation("FFX.exe", 0x4D9140) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte d_FUN_008a9c20(int ply_id);
    public static FhMethodHandle<d_FUN_008a9c20> FUN_008a9c20
        => new ( new FhMethodLocation("FFX.exe", 0x4A9C20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte d_FUN_008a97d0(int ply_id);
    public static FhMethodHandle<d_FUN_008a97d0> FUN_008a97d0
        => new ( new FhMethodLocation("FFX.exe", 0x4A97D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate byte* d_FUN_008bee40(uint param_1);
    public static FhMethodHandle<d_FUN_008bee40> FUN_008bee40
        => new ( new FhMethodLocation("FFX.exe", 0x4BEE40) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_ToGetBtlEasyFontWidth(byte* text, float* out_width, int param_3, float scale);
    public static FhMethodHandle<d_ToGetBtlEasyFontWidth> ToGetBtlEasyFontWidth
        => new ( new FhMethodLocation("FFX.exe", 0x505290) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_FUN_008d8a70(float param_1, float param_2, Equipment* gear);
    public static FhMethodHandle<d_FUN_008d8a70> FUN_008d8a70
        => new ( new FhMethodLocation("FFX.exe", 0x4D8A70) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public delegate bool d_MsGetRamChrMonster(int chr_id);
    public static FhMethodHandle<d_MsGetRamChrMonster> MsGetRamChrMonster
        => new ( new FhMethodLocation("FFX.exe", 0x39AF00) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_MsLimitUp(int chr_id, Chr* chr, int inc_amount);
    public static FhMethodHandle<d_MsLimitUp> MsLimitUp
        => new ( new FhMethodLocation("FFX.exe", 0x3B15A0) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int d_MsCalcWeakLevel(int hp, int max_hp);
    public static FhMethodHandle<d_MsCalcWeakLevel> MsCalcWeakLevel
        => new ( new FhMethodLocation("FFX.exe", 0x38BFC0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate PCommand* d_MsGetRomPlyCommand(uint com_id, int* out_text);
    public static FhMethodHandle<d_MsGetRomPlyCommand> MsGetRomPlyCommand
        => new ( new FhMethodLocation("FFX.exe", 0x390AE0) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate byte d_TkMenuGetCurrentPlayer();
    public static FhMethodHandle<d_TkMenuGetCurrentPlayer> TkMenuGetCurrentPlayer
        => new ( new FhMethodLocation("FFX.exe", 0x4A9810) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ushort d_getScenerioFlag();
    public static FhMethodHandle<d_getScenerioFlag> getScenerioFlag
        => new ( new FhMethodLocation("FFX.exe", 0x387420) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt> CT_RetInt_0171_fillPartyMemberHp
        => new ( new FhMethodLocation("FFX.exe", 0x45C4F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt> CT_RetInt_0172_fillPartyMemberMp
        => new ( new FhMethodLocation("FFX.exe", 0x45C6B0) );

    // Unofficial naming
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_TkMenuDrawMain(void* menu);
    public static FhMethodHandle<d_TkMenuDrawMain> TkMenuDrawMain
        => new ( new FhMethodLocation("FFX.exe", 0x4E0BA0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_008c0220(uint param_1, float param_2, float param_3, float param_4, float param_5);
    public static FhMethodHandle<d_FUN_008c0220> FUN_008c0220
        => new ( new FhMethodLocation("FFX.exe", 0x4C0220) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_008bc300(int param_1);
    public static FhMethodHandle<d_FUN_008bc300> FUN_008bc300
        => new ( new FhMethodLocation("FFX.exe", 0x4BC300) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_008e67f0(uint gear_idx, float x, float y, byte color_id);
    public static FhMethodHandle<d_FUN_008e67f0> FUN_008e67f0
        => new ( new FhMethodLocation("FFX.exe", 0x4E67F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_DrawCrossMenuIconWeaponName2(void* param_1, float x, float y, byte color_id);
    public static FhMethodHandle<d_DrawCrossMenuIconWeaponName2> DrawCrossMenuIconWeaponName2
        => new ( new FhMethodLocation("FFX.exe", 0x4E6970) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_TOBtlDrawCommandWindow(void* param_1);
    public static FhMethodHandle<d_TOBtlDrawCommandWindow> TOBtlDrawCommandWindow
        => new ( new FhMethodLocation("FFX.exe", 0x49F300) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_FUN_008cf800(void* param_1);
    public static FhMethodHandle<d_FUN_008cf800> FUN_008cf800
        => new ( new FhMethodLocation("FFX.exe", 0x4CF800) );

    // Unofficial naming
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_BattleRewards_AddGear(int chr_id, ChrLoot* loot, BtlRewardData* rewards);
    public static FhMethodHandle<d_BattleRewards_AddGear> BattleRewards_AddGear
        => new ( new FhMethodLocation("FFX.exe", 0x398C20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsChangeWeaponInvisible(int ply_id, [MarshalAs(UnmanagedType.U1)] bool enable);
    public static FhMethodHandle<d_MsChangeWeaponInvisible> MsChangeWeaponInvisible
        => new ( new FhMethodLocation("FFX.exe", 0x3AD5F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_FUN_008d85f0(void* param_1, int param_2);
    public static FhMethodHandle<d_FUN_008d85f0> FUN_008d85f0
        => new ( new FhMethodLocation("FFX.exe", 0x4D85F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_MsLimitTypeDeathCheck(int attacker_id, Chr* attacker, int target_id, Chr* target);
    public static FhMethodHandle<d_MsLimitTypeDeathCheck> MsLimitTypeDeathCheck
        => new ( new FhMethodLocation("FFX.exe", 0x3B0F90) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_FUN_007b10d0(int chr_id, uint limit_mode, int param_3);
    public static FhMethodHandle<d_FUN_007b10d0> FUN_007b10d0
        => new ( new FhMethodLocation("FFX.exe", 0x3B10D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_MsLimitTypeTurnCheck(int chr_id, Chr* chr);
    public static FhMethodHandle<d_MsLimitTypeTurnCheck> MsLimitTypeTurnCheck
        => new ( new FhMethodLocation("FFX.exe", 0x3B13D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsLimitTypeWinCheck();
    public static FhMethodHandle<d_MsLimitTypeWinCheck> MsLimitTypeWinCheck
        => new ( new FhMethodLocation("FFX.exe", 0x3B1550) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsSetSaveStartGame();
    public static FhMethodHandle<d_MsSetSaveStartGame> MsSetSaveStartGame
        => new ( new FhMethodLocation("FFX.exe", 0x386BC0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public delegate bool d_FUN_00635c20(uint param_1);
    public static FhMethodHandle<d_FUN_00635c20> FUN_00635c20
        => new ( new FhMethodLocation("FFX.exe", 0x235C20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_MsParseCommand(byte* param_1);
    public static FhMethodHandle<d_MsParseCommand> MsParseCommand
        => new ( new FhMethodLocation("FFX.exe", 0x3AE380) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOBtlCtrlHelpWin();
    public static FhMethodHandle<d_TOBtlCtrlHelpWin> TOBtlCtrlHelpWin
        => new ( new FhMethodLocation("FFX.exe", 0x491250) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate ushort* d_TOGetSaveWindow(int chr_id, BtlWindowType window_type, int* out_length);
    public static FhMethodHandle<d_TOGetSaveWindow> TOGetSaveWindow
        => new ( new FhMethodLocation("FFX.exe", 0x49B510) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_TkMenuSummonEnableMask();
    public static FhMethodHandle<d_TkMenuSummonEnableMask> TkMenuSummonEnableMask
        => new ( new FhMethodLocation("FFX.exe", 0x4AB190) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void* d_MsGetChrAbilityMap(int chr_id, void* save_param);
    public static FhMethodHandle<d_MsGetChrAbilityMap> MsGetChrAbilityMap
        => new ( new FhMethodLocation("FFX.exe", 0x385C20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_sceClose(void* arg1);
    public static FhMethodHandle<d_sceClose> sceClose
        => new( new FhMethodLocation("FFX.exe", 0x22F7C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_sceLseek(void* arg1, int arg2, int arg3);
    public static FhMethodHandle<d_sceLseek> sceLseek
        => new( new FhMethodLocation("FFX.exe", 0x22FA90) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void* d_sceOpen(byte* arg1, int arg2);
    public static FhMethodHandle<d_sceOpen> sceOpen
        => new( new FhMethodLocation("FFX.exe", 0x22FBE0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_sceRead(void* arg1, void* dst, int amount);
    public static FhMethodHandle<d_sceRead> sceRead
        => new( new FhMethodLocation("FFX.exe", 0x22FDB0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool d_graphicInitFMVPlayer(int movie_id, int arg2);
    public static FhMethodHandle<d_graphicInitFMVPlayer> graphicInitFMVPlayer
        => new( new FhMethodLocation("FFX.exe", 0x241840) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void d_FUN_00656c90(int arg1, int arg2, char* fileName);
    public static FhMethodHandle<d_FUN_00656c90> FUN_00656c90
        => new( new FhMethodLocation("FFX.exe", 0x256C90) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public unsafe delegate void d_LocalizationManager_Initialize(LocalizationManager* ptr_this);
    public static FhMethodHandle<d_LocalizationManager_Initialize> LocalizationManager_Initialize
        => new( new FhMethodLocation("FFX.exe", 0x2DB1C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void d_FUN_2EFFF0();
    internal static FhMethodHandle<d_FUN_2EFFF0> FUN_2EFFF0
        => new( new FhMethodLocation("FFX.exe", 0x2EFFF0) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public delegate void d_FfxFmod_soundInit(nint ptr_this);
    public static FhMethodHandle<d_FfxFmod_soundInit> FfxFmod_soundInit
        => new( new FhMethodLocation("FFX.exe", 0x307170) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public delegate int d_FmodVoice_dataChange(nint ptr_this, int event_id, nint arg3);
    public static FhMethodHandle<d_FmodVoice_dataChange> FmodVoice_dataChange
        => new( new FhMethodLocation("FFX.exe", 0x30A720) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public delegate void d_FmodVoice_initList(nint ptr_this);
    public static FhMethodHandle<d_FmodVoice_initList> FmodVoice_initList
        => new( new FhMethodLocation("FFX.exe", 0x30AC80) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public delegate uint d_FUN_0070aec0(nint ptr_this, uint voice_id, uint arg3);
    public static FhMethodHandle<d_FUN_0070aec0> FUN_0070aec0
        => new( new FhMethodLocation("FFX.exe", 0x30AEC0) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public delegate void d_FfxFmod_soundInit_setLang(nint ptr_this, int lang);
    public static FhMethodHandle<d_FfxFmod_soundInit_setLang> FfxFmod_soundInit_setLang
        => new( new FhMethodLocation("FFX.exe", 0x30B4E0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsBattleEncountExe(int field_id, int group_idx, float walked_delta);
    public static FhMethodHandle<d_MsBattleEncountExe> MsBattleEncountExe
        => new( new FhMethodLocation("FFX.exe", 0x380DE0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_ResetEncountExe(int arg1);
    public static FhMethodHandle<d_ResetEncountExe> ResetEncountExe
        => new( new FhMethodLocation("FFX.exe", 0x3810C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsBattleExe(uint arg1, int field_idx, int group_idx, int formation_idx);
    public static FhMethodHandle<d_MsBattleExe> MsBattleExe
        => new( new FhMethodLocation("FFX.exe", 0x3810F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsBattleLabelExe(uint encounter_id, byte arg2, byte screen_transition);
    public static FhMethodHandle<d_MsBattleLabelExe> MsBattleLabelExe
        => new( new FhMethodLocation("FFX.exe", 0x381D60) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsBtlReadManage();
    public static FhMethodHandle<d_MsBtlReadManage> MsBtlReadManage
        => new( new FhMethodLocation("FFX.exe", 0x3830D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_00783bb0(byte mon_idx);
    public static FhMethodHandle<d_FUN_00783bb0> FUN_00783bb0
        => new( new FhMethodLocation("FFX.exe", 0x383BB0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte d_MsBtlReadSetScene();
    public static FhMethodHandle<d_MsBtlReadSetScene> MsBtlReadSetScene
        => new( new FhMethodLocation("FFX.exe", 0x383ED0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsGetSaveCommand(int chr_id, uint com_id);
    public static FhMethodHandle<d_MsGetSaveCommand> MsGetSaveCommand
        => new( new FhMethodLocation("FFX.exe", 0x3850E0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_MsGetSavePartyMember(uint* arg1, uint* arg2, uint* arg3);
    public static FhMethodHandle<d_MsGetSavePartyMember> MsGetSavePartyMember
        => new( new FhMethodLocation("FFX.exe", 0x3853B0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsPayGIL(int arg1);
    public static FhMethodHandle<d_MsPayGIL> MsPayGIL
        => new( new FhMethodLocation("FFX.exe", 0x385A60) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsSetSaveCommand(int chr_id, uint arg2, int arg3);
    public static FhMethodHandle<d_MsSetSaveCommand> MsSetSaveCommand
        => new( new FhMethodLocation("FFX.exe", 0x385D10) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsSetSaveParam(uint chr_id);
    public static FhMethodHandle<d_MsSetSaveParam> MsSetSaveParam
        => new( new FhMethodLocation("FFX.exe", 0x3861B0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsSetSaveParamAll();
    public static FhMethodHandle<d_MsSetSaveParamAll> MsSetSaveParamAll
        => new( new FhMethodLocation("FFX.exe", 0x3869C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_MsAliveProcess(int chr_id, Chr* chr);
    public static FhMethodHandle<d_MsAliveProcess> MsAliveProcess
        => new( new FhMethodLocation("FFX.exe", 0x389220) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_MsBlowProcess(int chr_id, Chr* chr);
    public static FhMethodHandle<d_MsBlowProcess> MsBlowProcess
        => new( new FhMethodLocation("FFX.exe", 0x389270) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_MsCalcCommand(AttackCue* arg1, int arg2);
    public static FhMethodHandle<d_MsCalcCommand> MsCalcCommand
        => new( new FhMethodLocation("FFX.exe", 0x3893A0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate uint d_FUN_0078bb30(int arg1, byte* arg2, byte* arg3, Command* arg4, uint arg5, uint* arg6, int* arg7);
    public static FhMethodHandle<d_FUN_0078bb30> FUN_0078bb30
        => new( new FhMethodLocation("FFX.exe", 0x38BB30) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsDamageCheckDeath(int attacker_id, int target_id, int arg3, int targeting_self);
    public static FhMethodHandle<d_MsDamageCheckDeath> MsDamageCheckDeath
        => new( new FhMethodLocation("FFX.exe", 0x38C800) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsDamageSetMotion(int chr_id, int arg2, int targeting_self);
    public static FhMethodHandle<d_MsDamageSetMotion> MsDamageSetMotion
        => new( new FhMethodLocation("FFX.exe", 0x38CAE0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate Command* d_MsGetCommand(int chr_id, int unused, int quit_on_idx, AttackCommandInfo* arg4, uint* arg5);
    public static FhMethodHandle<d_MsGetCommand> MsGetCommand
        => new( new FhMethodLocation("FFX.exe", 0x38CF10) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate uint d_FUN_0078d100(Chr* chr);
    public static FhMethodHandle<d_FUN_0078d100> FUN_0078d100
        => new( new FhMethodLocation("FFX.exe", 0x38D100) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_MsLimitStatusProcess(int chr_id, Chr* chr, uint arg3);
    public static FhMethodHandle<d_MsLimitStatusProcess> MsLimitStatusProcess
        => new( new FhMethodLocation("FFX.exe", 0x38D330) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsSetChrWeak(int chr_id, int new_weak_level);
    public static FhMethodHandle<d_MsSetChrWeak> MsSetChrWeak
        => new( new FhMethodLocation("FFX.exe", 0x38D8B0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate bool d_MsAutoRelifeProcess(int attacker_id, Chr* attacker, int target_id, Chr* target);
    public static FhMethodHandle<d_MsAutoRelifeProcess> MsAutoRelifeProcess
        => new( new FhMethodLocation("FFX.exe", 0x38D990) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_MsStoneProcess(int chr_id, Chr* chr);
    public static FhMethodHandle<d_MsStoneProcess> MsStoneProcess
        => new( new FhMethodLocation("FFX.exe", 0x38E210) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_MsSubCTB(int chr_id, Chr* chr, int arg3, int arg4, uint arg5, uint arg6);
    public static FhMethodHandle<d_MsSubCTB> MsSubCTB
        => new( new FhMethodLocation("FFX.exe", 0x38E2A0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_MsSubHP(int chr_id, Chr* chr, int arg3, int arg4, int arg5, uint arg6, uint arg7);
    public static FhMethodHandle<d_MsSubHP> MsSubHP
        => new( new FhMethodLocation("FFX.exe", 0x38E2F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_MsSubMP(int chr_id, Chr* chr, int arg3, int arg4, int arg5, uint arg6, uint arg7);
    public static FhMethodHandle<d_MsSubMP> MsSubMP
        => new( new FhMethodLocation("FFX.exe", 0x38E400) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_MsThreatProcess(int chr_id, Chr* chr);
    public static FhMethodHandle<d_MsThreatProcess> MsThreatProcess
        => new( new FhMethodLocation("FFX.exe", 0x38E4B0) );

    // Unofficial naming
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate uint d_AfterDamageProcess(int attacker_id, uint arg2, int target_id, uint* arg4, uint arg5);
    public static FhMethodHandle<d_AfterDamageProcess> AfterDamageProcess
        => new( new FhMethodLocation("FFX.exe", 0x38F0B0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate byte* d_MsGetRomBtlText(int arg1, int arg2);
    public static FhMethodHandle<d_MsGetRomBtlText> MsGetRomBtlText
        => new( new FhMethodLocation("FFX.exe", 0x38F940) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsMenuCloseTitleWindow(int arg1);
    public static FhMethodHandle<d_MsMenuCloseTitleWindow> MsMenuCloseTitleWindow
        => new( new FhMethodLocation("FFX.exe", 0x38FA80) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsSaveItemUse(uint item_id, int amount);
    public static FhMethodHandle<d_MsSaveItemUse> MsSaveItemUse
        => new( new FhMethodLocation("FFX.exe", 0x3905A0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate byte* d_MsImportantName(uint key_item_idx);
    public static FhMethodHandle<d_MsImportantName> MsImportantName
        => new( new FhMethodLocation("FFX.exe", 0x3908B0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate AutoAbility* d_MsGetRomAbility(uint a_ability_id, int* ref_data_end);
    public static FhMethodHandle<d_MsGetRomAbility> MsGetRomAbility
        => new( new FhMethodLocation("FFX.exe", 0x3909C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate CustomizationRecipe* d_MsGetRomKaizou(int *size);
    public static FhMethodHandle<d_MsGetRomKaizou> MsGetRomKaizou
        => new( new FhMethodLocation("FFX.exe", 0x390A60) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate AeonAbilityRecipe* d_MsGetRomSummonGrow(int* size);
    public static FhMethodHandle<d_MsGetRomSummonGrow> MsGetRomSummonGrow
        => new( new FhMethodLocation("FFX.exe", 0x390B00) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool d_MsMonsterCapture(int target_id, int arena_idx);
    public static FhMethodHandle<d_MsMonsterCapture> MsMonsterCapture
        => new( new FhMethodLocation("FFX.exe", 0x390B80) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_00791820();
    public static FhMethodHandle<d_FUN_00791820> FUN_00791820
        => new( new FhMethodLocation("FFX.exe", 0x391820) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetBattleEndStatus();
    public static FhMethodHandle<d_MsGetBattleEndStatus> MsGetBattleEndStatus
        => new( new FhMethodLocation("FFX.exe", 0x3928F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate Chr* d_MsGetChr(int chr_id);
    public static FhMethodHandle<d_MsGetChr> MsGetChr
        => new( new FhMethodLocation("FFX.exe", 0x394030) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate Chr* d_MsGetMon(byte mon_idx);
    public static FhMethodHandle<d_MsGetMon> MsGetMon
        => new( new FhMethodLocation("FFX.exe", 0x395AB0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate uint d_MsApUp(int chr_id, Chr* chr, int base_ap_add, uint arg4);
    public static FhMethodHandle<d_MsApUp> MsApUp
        => new( new FhMethodLocation("FFX.exe", 0x398A10) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsFieldItemGet(int treasure_id);
    public static FhMethodHandle<d_MsFieldItemGet> MsFieldItemGet
        => new( new FhMethodLocation("FFX.exe", 0x398FE0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_MsSetWeaponName(Equipment* gear);
    public static FhMethodHandle<d_MsSetWeaponName> MsSetWeaponName
        => new( new FhMethodLocation("FFX.exe", 0x3993C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_FUN_007993f0(BtlRewardData* arg1, int arg2);
    public static FhMethodHandle<d_FUN_007993f0> FUN_007993f0
        => new( new FhMethodLocation("FFX.exe", 0x3993F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate Command* d_MsGetComData(int com_id, byte** arg2);
    public static FhMethodHandle<d_MsGetComData> MsGetComData
        => new( new FhMethodLocation("FFX.exe", 0x39A4C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_0079b480(int chr_id, int com_id, int is_disabled);
    public static FhMethodHandle<d_FUN_0079b480> FUN_0079b480
        => new( new FhMethodLocation("FFX.exe", 0x39B480) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_MsSetRamChrAbility(int chr_id, Chr* chr);
    public static FhMethodHandle<d_MsSetRamChrAbility> MsSetRamChrAbility
        => new( new FhMethodLocation("FFX.exe", 0x39BB70) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsSetRamChrParam(uint chr_id);
    public static FhMethodHandle<d_MsSetRamChrParam> MsSetRamChrParam
        => new( new FhMethodLocation("FFX.exe", 0x39C610) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte d_MsMessageCueProcess();
    public static FhMethodHandle<d_MsMessageCueProcess> MsMessageCueProcess
        => new( new FhMethodLocation("FFX.exe", 0x39CE10) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsMessageCueRegist(uint type, int arg2, int arg3, byte arg4, byte arg5);
    public static FhMethodHandle<d_MsMessageCueRegist> MsMessageCueRegist
        => new( new FhMethodLocation("FFX.exe", 0x39CFF0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate BtlBinEncounter* d_MsBtlListEncount(int field_idx);
    public static FhMethodHandle<d_MsBtlListEncount> MsBtlListEncount
        => new( new FhMethodLocation("FFX.exe", 0x39D190) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate BtlBinField* d_MsBtlListField(int field_idx);
    public static FhMethodHandle<d_MsBtlListField> MsBtlListField
        => new( new FhMethodLocation("FFX.exe", 0x39D1B0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsBtlListFieldNum(int field_id);
    public static FhMethodHandle<d_MsBtlListFieldNum> MsBtlListFieldNum
        => new( new FhMethodLocation("FFX.exe", 0x39D1E0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate BtlBinGroup* d_MsBtlListGroup(int field_idx, int group_idx);
    public static FhMethodHandle<d_MsBtlListGroup> MsBtlListGroup
        => new( new FhMethodLocation("FFX.exe", 0x39D230) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsSetStealEffect(int arg1, int arg2);
    public static FhMethodHandle<d_MsSetStealEffect> MsSetStealEffect
        => new( new FhMethodLocation("FFX.exe", 0x39ED20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsSetStealGillEffect(int arg1, int arg2);
    public static FhMethodHandle<d_MsSetStealGillEffect> MsSetStealGillEffect
        => new( new FhMethodLocation("FFX.exe", 0x39ED40) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsStatusDefenseEffect(int attacker_id, int target_id, int dmg_calc_flags);
    public static FhMethodHandle<d_MsStatusDefenseEffect> MsStatusDefenseEffect
        => new( new FhMethodLocation("FFX.exe", 0x39EE40) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsStatusEffectCheck(int chr_id);
    public static FhMethodHandle<d_MsStatusEffectCheck> MsStatusEffectCheck
        => new( new FhMethodLocation("FFX.exe", 0x39F010) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsNumberRegist(int arg1, int arg2, int arg3, int arg4, int arg5, uint arg6, uint arg7);
    public static FhMethodHandle<d_MsNumberRegist> MsNumberRegist
        => new( new FhMethodLocation("FFX.exe", 0x39FA20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsRegSEplay(byte arg1, int arg2);
    public static FhMethodHandle<d_MsRegSEplay> MsRegSEplay
        => new( new FhMethodLocation("FFX.exe", 0x3A0120) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsRegSEplay2(int arg1, uint arg2);
    public static FhMethodHandle<d_MsRegSEplay2> MsRegSEplay2
        => new( new FhMethodLocation("FFX.exe", 0x3A0160) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_MsWeaponName(ushort name_id, byte owner, [MarshalAs(UnmanagedType.Bool)] bool simplified, ushort* out_model_id);
    public static FhMethodHandle<d_MsWeaponName> MsWeaponName
        => new( new FhMethodLocation("FFX.exe", 0x3A0C70) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate ushort d_MsWeaponNameNum(Equipment* arg1);
    public static FhMethodHandle<d_MsWeaponNameNum> MsWeaponNameNum
        => new( new FhMethodLocation("FFX.exe", 0x3A0D10) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate nint d_MsGetExcelData(int req_elem_idx, nint excel_data_ptr, int* ref_data_end);
    public static FhMethodHandle<d_MsGetExcelData> MsGetExcelData
        => new( new FhMethodLocation("FFX.exe", 0x3AB890) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_FUN_007ab930(Equipment* arg1);
    public static FhMethodHandle<d_FUN_007ab930> FUN_007ab930
        => new( new FhMethodLocation("FFX.exe", 0x3AB930) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate Equipment* d_MsGetSaveWeapon(uint gear_inv_idx, nint ref_name);
    public static FhMethodHandle<d_MsGetSaveWeapon> MsGetSaveWeapon
        => new( new FhMethodLocation("FFX.exe", 0x3ABBF0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_MsBtlGetPos(int arg1, Chr* chr, int btl_pos_a, int btl_pos_b, int btl_pos_c, Vector4* out_pos);
    public static FhMethodHandle<d_MsBtlGetPos> MsBtlGetPos
        => new( new FhMethodLocation("FFX.exe", 0x3AC000) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_MsPopBtlPos(Chr* chr);
    public static FhMethodHandle<d_MsPopBtlPos> MsPopBtlPos
        => new( new FhMethodLocation("FFX.exe", 0x3AC620) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_MsActionRequest(int target_id, int attacker_id, int arg3, int arg4, int arg5, void* arg6);
    public static FhMethodHandle<d_MsActionRequest> MsActionRequest
        => new( new FhMethodLocation("FFX.exe", 0x3ACEC0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsLimitTidusLearn(int chr_id);
    public static FhMethodHandle<d_MsLimitTidusLearn> MsLimitTidusLearn
        => new( new FhMethodLocation("FFX.exe", 0x3B0CE0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_MsLimitTypeDamageCheck(int attacker_id, Chr* attacker, int target_id, Chr* target, int arg5, int arg6, int arg7);
    public static FhMethodHandle<d_MsLimitTypeDamageCheck> MsLimitTypeDamageCheck
        => new( new FhMethodLocation("FFX.exe", 0x3B0D60) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_MsLimitTypeStatusCheck(int attacker_id, Chr* attacker, int target_id, Chr* target, int arg5, uint arg6);
    public static FhMethodHandle<d_MsLimitTypeStatusCheck> MsLimitTypeStatusCheck
        => new( new FhMethodLocation("FFX.exe", 0x3B12D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_MsAutoCureProcess(int target_id, Chr* target, int attacker_id, int poison, int zombie, int darkness, int silence);
    public static FhMethodHandle<d_MsAutoCureProcess> MsAutoCureProcess
        => new( new FhMethodLocation("FFX.exe", 0x3B2520) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_MsAutoPotionProcess(int target_id, Chr* target, int attacker_id);
    public static FhMethodHandle<d_MsAutoPotionProcess> MsAutoPotionProcess
        => new( new FhMethodLocation("FFX.exe", 0x3B2860) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_achievementUnlockAchievement(int acid);
    public static FhMethodHandle<d_achievementUnlockAchievement> achievementUnlockAchievement
        => new( new FhMethodLocation("FFX.exe", 0x422410) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_Sg_FadeInW(int arg1);
    public static FhMethodHandle<d_Sg_FadeInW> Sg_FadeInW
        => new( new FhMethodLocation("FFX.exe", 0x42CC20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_UpgradeBrotherhood(int level);
    public static FhMethodHandle<d_UpgradeBrotherhood> UpgradeBrotherhood
        => new( new FhMethodLocation("FFX.exe", 0x4596A0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_FUN_008671d0(byte opcode, AtelWorkThread* thread, AtelBasicWorker* work, AtelStack* stack);
    public static FhMethodHandle<d_FUN_008671d0> FUN_008671d0
        => new( new FhMethodLocation("FFX.exe", 0x4671D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_FUN_00867370(byte opcode, AtelBasicWorker* work, AtelWorkThread* thread, AtelStack* stack, uint arg5);
    public static FhMethodHandle<d_FUN_00867370> FUN_00867370
        => new( new FhMethodLocation("FFX.exe", 0x467370) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate nint d_AtelGetCurCtrlWork();
    public static FhMethodHandle<d_AtelGetCurCtrlWork> AtelGetCurCtrlWork
        => new( new FhMethodLocation("FFX.exe", 0x46AF80) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate short d_FUN_0086bea0(int arg1);
    public static FhMethodHandle<d_FUN_0086bea0> FUN_0086bea0
        => new( new FhMethodLocation("FFX.exe", 0x46BEA0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate byte* d_FUN_0086bec0(int arg1);
    public static FhMethodHandle<d_FUN_0086bec0> FUN_0086bec0
        => new( new FhMethodLocation("FFX.exe", 0x46BEC0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_AtelInitTotal();
    public static FhMethodHandle<d_AtelInitTotal> AtelInitTotal
        => new( new FhMethodLocation("FFX.exe", 0x46D660) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_AtelPopStackInteger(AtelBasicWorker* work, AtelStack* stack);
    public static FhMethodHandle<d_AtelPopStackInteger> AtelPopStackInteger
        => new( new FhMethodLocation("FFX.exe", 0x46DE90) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_AtelSetEventJump2(int room, int entrance, int do_fade);
    public static FhMethodHandle<d_AtelSetEventJump2> AtelSetEventJump2
        => new( new FhMethodLocation("FFX.exe", 0x46FED0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_AtelEventSetUp(int event_id);
    public static FhMethodHandle<d_AtelEventSetUp> AtelEventSetUp
        => new( new FhMethodLocation("FFX.exe", 0x472E90) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsSetSaveCommandWithPrefix(int chr_id, int com_id, int arg3);
    public static FhMethodHandle<d_MsSetSaveCommandWithPrefix> MsSetSaveCommandWithPrefix
        => new( new FhMethodLocation("FFX.exe", 0x474190) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_AtelSetUpCallFunc(int nameSpaceId, nint nameSpacePtr);
    public static FhMethodHandle<d_AtelSetUpCallFunc> AtelSetUpCallFunc
        => new( new FhMethodLocation("FFX.exe", 0x477800) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate char* d_AtelGetEventName(uint event_id);
    public static FhMethodHandle<d_AtelGetEventName> AtelGetEventName
        => new( new FhMethodLocation("FFX.exe", 0x4796E0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_SndSepPlaySimple(uint arg1);
    public static FhMethodHandle<d_SndSepPlaySimple> SndSepPlaySimple
        => new( new FhMethodLocation("FFX.exe", 0x486DE0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TkMsImportantSet(uint arg1);
    public static FhMethodHandle<d_TkMsImportantSet> TkMsImportantSet
        => new( new FhMethodLocation("FFX.exe", 0x48E700) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TkVU1SyncPath();
    public static FhMethodHandle<d_TkVU1SyncPath> TkVU1SyncPath
        => new( new FhMethodLocation("FFX.exe", 0x48EBD0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOBtlCloseSimpleHelpMes();
    public static FhMethodHandle<d_TOBtlCloseSimpleHelpMes> TOBtlCloseSimpleHelpMes
        => new( new FhMethodLocation("FFX.exe", 0x490E60) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_TOBtlDrawCaptureMonsterMessageWindow(int mon_id, int text_id);
    public static FhMethodHandle<d_TOBtlDrawCaptureMonsterMessageWindow> TOBtlDrawCaptureMonsterMessageWindow
        => new( new FhMethodLocation("FFX.exe", 0x4927E0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_TOBtlDrawFirstStrikeEnemyMessageWindow();
    public static FhMethodHandle<d_TOBtlDrawFirstStrikeEnemyMessageWindow> TOBtlDrawFirstStrikeEnemyMessageWindow
        => new( new FhMethodLocation("FFX.exe", 0x493440) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_TOBtlDrawFirstStrikePlayerMessageWindow();
    public static FhMethodHandle<d_TOBtlDrawFirstStrikePlayerMessageWindow> TOBtlDrawFirstStrikePlayerMessageWindow
        => new( new FhMethodLocation("FFX.exe", 0x493460) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_TOBtlDrawGetItemMessageWindow(byte* item_name, int amount);
    public static FhMethodHandle<d_TOBtlDrawGetItemMessageWindow> TOBtlDrawGetItemMessageWindow
        => new( new FhMethodLocation("FFX.exe", 0x493480) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_TOBtlDrawGetLimitTypeMessageWindow(int ply_id, int limit_mode);
    public static FhMethodHandle<d_TOBtlDrawGetLimitTypeMessageWindow> TOBtlDrawGetLimitTypeMessageWindow
        => new( new FhMethodLocation("FFX.exe", 0x493560) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_TOBtlDrawGetMoneyMessageWindow(int amount);
    public static FhMethodHandle<d_TOBtlDrawGetMoneyMessageWindow> TOBtlDrawGetMoneyMessageWindow
        => new( new FhMethodLocation("FFX.exe", 0x4935D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_TOBtlDrawLearningMessageWindow(int ply_id, int com_id);
    public static FhMethodHandle<d_TOBtlDrawLearningMessageWindow> TOBtlDrawLearningMessageWindow
        => new( new FhMethodLocation("FFX.exe", 0x495290) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_TOBtlDrawStdChrNameMessageWindow(int chr_id, int text_id);
    public static FhMethodHandle<d_TOBtlDrawStdChrNameMessageWindow> TOBtlDrawStdChrNameMessageWindow
        => new( new FhMethodLocation("FFX.exe", 0x497170) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_FUN_0089db10(int arg1, byte* text);
    public static FhMethodHandle<d_FUN_0089db10> FUN_0089db10
        => new( new FhMethodLocation("FFX.exe", 0x49DB10) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte d_TkMenuGetCurrentSummon();
    public static FhMethodHandle<d_TkMenuGetCurrentSummon> TkMenuGetCurrentSummon
        => new( new FhMethodLocation("FFX.exe", 0x4A9830) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate TkWindow* d_TkMenuMainAllocWindow();
    public static FhMethodHandle<d_TkMenuMainAllocWindow> TkMenuMainAllocWindow
        => new( new FhMethodLocation("FFX.exe", 0x4AA150) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate TkWindow* d_TkMenuMainRegistWindow(TkWindow* window);
    public static FhMethodHandle<d_TkMenuMainRegistWindow> TkMenuMainRegistWindow
        => new( new FhMethodLocation("FFX.exe", 0x4AAAB0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate PCommand* d_TkMsGetRomItem(uint arg1, int* arg2);
    public static FhMethodHandle<d_TkMsGetRomItem> TkMsGetRomItem
        => new( new FhMethodLocation("FFX.exe", 0x4AB230) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate byte* d_TOGetSaveChrName(int chr_id);
    public static FhMethodHandle<d_TOGetSaveChrName> TOGetSaveChrName
        => new( new FhMethodLocation("FFX.exe", 0x4AC800) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_FUN_008b4460(TkWindow* window);
    public static FhMethodHandle<d_FUN_008b4460> FUN_008b4460
        => new( new FhMethodLocation("FFX.exe", 0x4B4460) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOBtlSetMacroCommandType(int arg1, int arg2, byte arg3);
    public static FhMethodHandle<d_TOBtlSetMacroCommandType> TOBtlSetMacroCommandType
        => new( new FhMethodLocation("FFX.exe", 0x4B5770) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_TOBtlSetMacroCommandValue(int arg1, int arg2, byte* arg3);
    public static FhMethodHandle<d_TOBtlSetMacroCommandValue> TOBtlSetMacroCommandValue
        => new( new FhMethodLocation("FFX.exe", 0x4B57A0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_008b8910(int window_idx, int variable_idx, int type); // setMessageWindowVariableType (0: text*, 1: int)
    public static FhMethodHandle<d_FUN_008b8910> FUN_008b8910
        => new( new FhMethodLocation("FFX.exe", 0x4B8910) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate byte* d_TkBtlEndGetText(uint window_idx); // getMenuText
    public static FhMethodHandle<d_TkBtlEndGetText> TkBtlEndGetText
        => new( new FhMethodLocation("FFX.exe", 0x4BDA20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_008b8930(int window_idx, int variable_idx, int value); // setMessageWindowVariable
    public static FhMethodHandle<d_FUN_008b8930> FUN_008b8930
        => new( new FhMethodLocation("FFX.exe", 0x4B8930) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_0086a0c0();
    public static FhMethodHandle<d_FUN_0086a0c0> FUN_0086a0c0
        => new( new FhMethodLocation("FFX.exe", 0x46A0C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate TOMesWinWork* d_AtelGetMesWinWork(int idx);
    public static FhMethodHandle<d_AtelGetMesWinWork> AtelGetMesWinWork
        => new( new FhMethodLocation("FFX.exe", 0x46BE20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate byte* d_FUN_008bee80(uint arg1);
    public static FhMethodHandle<d_FUN_008bee80> FUN_008bee80
        => new( new FhMethodLocation("FFX.exe", 0x4BEE80) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TkMn2DrawCrossCursor(float x, float y, int arg3);
    public static FhMethodHandle<d_TkMn2DrawCrossCursor> TkMn2DrawCrossCursor
        => new( new FhMethodLocation("FFX.exe", 0x4C0640) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TkMn2DrawKickSyncPacket();
    public static FhMethodHandle<d_TkMn2DrawKickSyncPacket> TkMn2DrawKickSyncPacket
        => new( new FhMethodLocation("FFX.exe", 0x4C0C90) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_008c0f40(int arg1, int arg2, int arg3, int arg4);
    public static FhMethodHandle<d_FUN_008c0f40> FUN_008c0f40
        => new( new FhMethodLocation("FFX.exe", 0x4C0F40) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_008c1350_DrawScissor512x416();
    public static FhMethodHandle<d_FUN_008c1350_DrawScissor512x416> FUN_008c1350_DrawScissor512x416
        => new( new FhMethodLocation("FFX.exe", 0x4C1350) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_TkMn2GetSummonGrowMax();
    public static FhMethodHandle<d_TkMn2GetSummonGrowMax> TkMn2GetSummonGrowMax
        => new( new FhMethodLocation("FFX.exe", 0x4C1C20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_008c1c70(int arg1, int arg2, uint arg3, int arg4);
    public static FhMethodHandle<d_FUN_008c1c70> FUN_008c1c70
        => new( new FhMethodLocation("FFX.exe", 0x4C1C70) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_FUN_008c2370(TkMenuItemListId menu_list_id, Equipment* gear);
    public static FhMethodHandle<d_FUN_008c2370> FUN_008c2370 // PrepareMenuList
        => new( new FhMethodLocation("FFX.exe", 0x4C2370) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_FUN_008c2c40(int arg1, int arg2, byte* arg3);
    public static FhMethodHandle<d_FUN_008c2c40> FUN_008c2c40
        => new( new FhMethodLocation("FFX.exe", 0x4C2C40) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_TkSetLegendAbility(int chr_id, int level);
    public static FhMethodHandle<d_TkSetLegendAbility> TkSetLegendAbility
        => new( new FhMethodLocation("FFX.exe", 0x4C3150) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_008cc120(int arg1);
    public static FhMethodHandle<d_FUN_008cc120> FUN_008cc120
        => new( new FhMethodLocation("FFX.exe", 0x4CC120) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_TkMenuCtrl(TkMenu* menu, int arg);
    public static FhMethodHandle<d_TkMenuCtrl> TkMenuCtrlSummon
        => new( new FhMethodLocation("FFX.exe", 0x4CC300) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_FUN_008cd960(TkWindow* window, int arg2, int arg3, float arg4, float arg5);
    public static FhMethodHandle<d_FUN_008cd960> FUN_008cd960
        => new( new FhMethodLocation("FFX.exe", 0x4CD960) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_FUN_008cd9f0(TkWindow* window, int arg2, int arg3);
    public static FhMethodHandle<d_FUN_008cd9f0> FUN_008cd9f0
        => new( new FhMethodLocation("FFX.exe", 0x4CD9F0) );

    // DrawAeonCustomizationMenu
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_FUN_008cdb70(TkWindow* window);
    public static FhMethodHandle<d_FUN_008cdb70> FUN_008cdb70
        => new( new FhMethodLocation("FFX.exe", 0x4CDB70) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_008d4140(uint arg1, int arg2);
    public static FhMethodHandle<d_FUN_008d4140> FUN_008d4140
        => new( new FhMethodLocation("FFX.exe", 0x4D4140) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_FUN_008d48e0();
    public static FhMethodHandle<d_FUN_008d48e0> FUN_008d48e0
        => new( new FhMethodLocation("FFX.exe", 0x4D48E0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool d_FUN_008d5720(uint gear_id, int arg2);
    public static FhMethodHandle<d_FUN_008d5720> FUN_008d5720
        => new( new FhMethodLocation("FFX.exe", 0x4D5720) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_UpdateGearCustomizationMenuState(TkWindow* window);
    public static FhMethodHandle<d_UpdateGearCustomizationMenuState> UpdateGearCustomizationMenuState
        => new( new FhMethodLocation("FFX.exe", 0x4D5800) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_FUN_008d5d20(TkWindow* window, int arg2, int arg3, int arg4, int arg5);
    public static FhMethodHandle<d_FUN_008d5d20> FUN_008d5d20
        => new( new FhMethodLocation("FFX.exe", 0x4D5D20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_FUN_008d5dc0(TkWindow* window, int arg2, int arg3);
    public static FhMethodHandle<d_FUN_008d5dc0> FUN_008d5dc0
        => new( new FhMethodLocation("FFX.exe", 0x4D5DC0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_DrawGearCustomizationMenu(TkWindow* window);
    public static FhMethodHandle<d_DrawGearCustomizationMenu> DrawGearCustomizationMenu
        => new( new FhMethodLocation("FFX.exe", 0x4D5F30) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_008d6630(int arg1, int arg2, int arg3);
    public static FhMethodHandle<d_FUN_008d6630> FUN_008d6630
        => new( new FhMethodLocation("FFX.exe", 0x4D6630) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TkMenuAppearMainCmdWindow(int arg1, int arg2);
    public static FhMethodHandle<d_TkMenuAppearMainCmdWindow> TkMenuAppearMainCmdWindow
        => new( new FhMethodLocation("FFX.exe", 0x4E1C60) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_008e2de0();
    public static FhMethodHandle<d_FUN_008e2de0> FUN_008e2de0
        => new( new FhMethodLocation("FFX.exe", 0x4E2DE0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_FUN_008e33a0(byte* text, byte* arg2, byte* arg3);
    public static FhMethodHandle<d_FUN_008e33a0> FUN_008e33a0
        => new( new FhMethodLocation("FFX.exe", 0x4E33A0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_DrawCrossMenuScrollParts(float arg1, float arg2, float arg3, float arg4, int arg5, int arg6, int arg7);
    public static FhMethodHandle<d_DrawCrossMenuScrollParts> DrawCrossMenuScrollParts
        => new( new FhMethodLocation("FFX.exe", 0x4E6CC0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_008e71d0(int arg1);
    public static FhMethodHandle<d_FUN_008e71d0> FUN_008e71d0
        => new( new FhMethodLocation("FFX.exe", 0x4E71D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TODrawMenuPlateXYWHType(float x, float y, float w, float h, int type);
    public static FhMethodHandle<d_TODrawMenuPlateXYWHType> TODrawMenuPlateXYWHType
        => new( new FhMethodLocation("FFX.exe", 0x4F5F70) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_008f8bb0(int arg1, float arg2, float arg3, float arg4, float arg5);
    public static FhMethodHandle<d_FUN_008f8bb0> FUN_008f8bb0
        => new( new FhMethodLocation("FFX.exe", 0x4F8BB0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TODrawScissorXYWH(int x, int y, int w, int h);
    public static FhMethodHandle<d_TODrawScissorXYWH> TODrawScissorXYWH
        => new( new FhMethodLocation("FFX.exe", 0x4F9230) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_008ff490(uint arg1, float arg2, float arg3);
    public static FhMethodHandle<d_FUN_008ff490> FUN_008ff490
        => new( new FhMethodLocation("FFX.exe", 0x4FF490) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_TOMkpCrossExtMesFontLClut(int arg1, byte* text, float x, float y, byte color, float scale, float p7_unused);
    public static FhMethodHandle<d_TOMkpCrossExtMesFontLClut> TOMkpCrossExtMesFontLClut
        => new( new FhMethodLocation("FFX.exe", 0x5016B0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_TOMkpCrossExtMesFontLClutTypeRGBA(uint arg1, byte* text, float x, float y, byte color, byte arg6, byte tint_r, byte tint_g, byte tint_b, byte tint_a, float scale, float _);
    public static FhMethodHandle<d_TOMkpCrossExtMesFontLClutTypeRGBA> TOMkpCrossExtMesFontLClutTypeRGBA
        => new( new FhMethodLocation("FFX.exe", 0x501700) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMkpShapeXYWHUV(int arg1, float x, float y, float w, float h, float uv_x1, float uv_y1, float uv_x2, float uv_y2);
    public static FhMethodHandle<d_TOMkpShapeXYWHUV> TOMkpShapeXYWHUV
        => new( new FhMethodLocation("FFX.exe", 0x503BB0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_ToGetCrossExtMesFontWidth(int arg1, byte* arg2, float* arg3, float arg4, float arg5);
    public static FhMethodHandle<d_ToGetCrossExtMesFontWidth> ToGetCrossExtMesFontWidth
        => new( new FhMethodLocation("FFX.exe", 0x505320) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_ToMakeBtlEasyFont(byte* text, float x, float y, byte color_id, float scale);
    public static FhMethodHandle<d_ToMakeBtlEasyFont> ToMakeBtlEasyFont
        => new( new FhMethodLocation("FFX.exe", 0x505AB0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_abmap_get_panel(int ply_id, int node_idx);
    public static FhMethodHandle<d_abmap_get_panel> abmap_get_panel
        => new( new FhMethodLocation("FFX.exe", 0x6458A0) );

    // Sphere-grid state-machine entry points
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_abmap_ctrl();
    public static FhMethodHandle<d_abmap_ctrl> AbmapState_ChangingNode
        => new( new FhMethodLocation("FFX.exe", 0x647D50) );
    public static FhMethodHandle<d_abmap_ctrl> AbmapState_Warping
        => new( new FhMethodLocation("FFX.exe", 0x647F00) );
    public static FhMethodHandle<d_abmap_ctrl> AbmapState_MovingToTarget
        => new( new FhMethodLocation("FFX.exe", 0x659990) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_00a48910(uint chr_id, int node_idx);
    public static FhMethodHandle<d_FUN_00a48910> FUN_00a48910
        => new( new FhMethodLocation("FFX.exe", 0x648910) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_eiAbmParaGet();
    public static FhMethodHandle<d_eiAbmParaGet> eiAbmParaGet
        => new( new FhMethodLocation("FFX.exe", 0x654860) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_00a56160(int arg1, int arg2, int arg3);
    public static FhMethodHandle<d_FUN_00a56160> FUN_00a56160
        => new( new FhMethodLocation("FFX.exe", 0x656160) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate nint d_FMOD_EventSystem_load(nint arg1, nint file_path, nint arg3, nint bank);
    public static FhMethodHandle<d_FMOD_EventSystem_load> FMOD_EventSystem_load
        => new( new FhMethodLocation("fmod_event.dll", "?load@EventSystem@FMOD@@QAG?AW4FMOD_RESULT@@PBDPAUFMOD_EVENT_LOADINFO@@PAPAVEventProject@2@@Z") );

}
