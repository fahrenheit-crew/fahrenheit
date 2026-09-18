// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

/* [fkelava 26/04/26 15:26]
 * Unlike `call.g.cs`, which contains source-generated delegates with no guarantee of accuracy,
 * this file contains manually annotated calls with proper Fahrenheit types that are vetted for functionality.
 *
 * This file is for calls which are exclusive to FF X-2/LM and not shared with X.
 */

using Fahrenheit.FFX2.Battle;

namespace Fahrenheit.FFX2;

/// <summary>
///     An accessor for game function calls exclusive to FF X-2/LM.
/// </summary>
public static unsafe partial class FhCall {

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public unsafe delegate void d_FUN_00534A70(int* ptr_this, int arg2, int arg3, int arg4, int arg5, int arg6, int* arg7, int* arg8, int* arg9);
    public static FhMethodHandle<d_FUN_00534A70> FUN_00534A70
        => new( new FhMethodLocation("FFX-2.exe", 0x134A70) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_kySetHelpJob2(uint job_id);
    public static FhMethodHandle<d_kySetHelpJob2> kySetHelpJob2
        => new( new FhMethodLocation("FFX-2.exe", 0x1E59B0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_kyAddPoint3D(int x, int y, int icon, uint arg4);
    public static FhMethodHandle<d_kyAddPoint3D> kyAddPoint3D
        => new( new FhMethodLocation("FFX-2.exe", 0x1E7580) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte d_kyGetCursorPoint(uint chr_id, uint plate_id);
    public static FhMethodHandle<d_kyGetCursorPoint> kyGetCursorPoint
        => new( new FhMethodLocation("FFX-2.exe", 0x1EA770) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ushort d_kyGetJobNum();
    public static FhMethodHandle<d_kyGetJobNum> kyGetJobNum
        => new( new FhMethodLocation("FFX-2.exe", 0x1EA7B0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ushort d_kyGetJobNum2();
    public static FhMethodHandle<d_kyGetJobNum2> kyGetJobNum2
        => new( new FhMethodLocation("FFX-2.exe", 0x1EA810) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ushort d_kyGetJobNum3();
    public static FhMethodHandle<d_kyGetJobNum3> kyGetJobNum3
        => new( new FhMethodLocation("FFX-2.exe", 0x1EA8B0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_kyGetResultPlateNum();
    public static FhMethodHandle<d_kyGetResultPlateNum> kyGetResultPlateNum
        => new( new FhMethodLocation("FFX-2.exe", 0x1EB2E0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_kyGetUsedPoint();
    public static FhMethodHandle<d_kyGetUsedPoint> kyGetUsedPoint
        => new( new FhMethodLocation("FFX-2.exe", 0x1EB480) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_kyIsUsedPoint(uint plate_id, uint plate_slot);
    public static FhMethodHandle<d_kyIsUsedPoint> kyIsUsedPoint
        => new( new FhMethodLocation("FFX-2.exe", 0x1EB9E0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_FUN_006083B0(uint arg1);
    public static FhMethodHandle<d_FUN_006083B0> FUN_006083B0
        => new( new FhMethodLocation("FFX-2.exe", 0x2083B0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsAddSaveDreSphere(uint job_id, int amount);
    public static FhMethodHandle<d_MsAddSaveDreSphere> MsAddSaveDreSphere
        => new( new FhMethodLocation("FFX-2.exe", 0x20B260) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetChrNum(uint chr_id);
    public static FhMethodHandle<d_MsGetChrNum> MsGetChrNum
        => new( new FhMethodLocation("FFX-2.exe", 0x20C1A0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetSaveAp(uint chr_id, uint ability_id);
    public static FhMethodHandle<d_MsGetSaveAp> MsGetSaveAp
        => new( new FhMethodLocation("FFX-2.exe", 0x20C2E0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate byte* d_MsGetSaveChrName(uint chr_id);
    public static FhMethodHandle<d_MsGetSaveChrName> MsGetSaveChrName
        => new( new FhMethodLocation("FFX-2.exe", 0x20C4A0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetSaveCommand(uint chr_id, uint ability_id);
    public static FhMethodHandle<d_MsGetSaveCommand> MsGetSaveCommand
        => new( new FhMethodLocation("FFX-2.exe", 0x20C500) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetSaveConfigChangeEffect();
    public static FhMethodHandle<d_MsGetSaveConfigChangeEffect> MsGetSaveConfigChangeEffect
        => new( new FhMethodLocation("FFX-2.exe", 0x20C650) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsGetSaveDreSphere(uint job_id);
    public static FhMethodHandle<d_MsGetSaveDreSphere> MsGetSaveDreSphere
        => new( new FhMethodLocation("FFX-2.exe", 0x20C710) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetSaveDressUpCount(uint chr_id, uint arg2);
    public static FhMethodHandle<d_MsGetSaveDressUpCount> MsGetSaveDressUpCount
        => new( new FhMethodLocation("FFX-2.exe", 0x20C730) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetSaveJob(uint chr_id);
    public static FhMethodHandle<d_MsGetSaveJob> MsGetSaveJob
        => new( new FhMethodLocation("FFX-2.exe", 0x20C950) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetSaveLearn(uint chr_id, uint job_id);
    public static FhMethodHandle<d_MsGetSaveLearn> MsGetSaveLearn
        => new( new FhMethodLocation("FFX-2.exe", 0x20CA70) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetSaveNeedAp(uint chr_id, uint ability_id);
    public static FhMethodHandle<d_MsGetSaveNeedAp> MsGetSaveNeedAp
        => new( new FhMethodLocation("FFX-2.exe", 0x20CB20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetSavePlate(uint plate_id);
    public static FhMethodHandle<d_MsGetSavePlate> MsGetSavePlate
        => new( new FhMethodLocation("FFX-2.exe", 0x20CC00) );

    // Unofficial name
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate uint d_CalculateStats(uint chr_id, int chr_level, uint job_id, PlySave* ptr_ply_save, int* ptr_stats);
    public static FhMethodHandle<d_CalculateStats> CalculateStats
        => new( new FhMethodLocation("FFX-2.exe", 0x20D720) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsSetSaveLearn(uint chr_id, uint job_id, ushort ability_id);
    public static FhMethodHandle<d_MsSetSaveLearn> MsSetSaveLearn
        => new( new FhMethodLocation("FFX-2.exe", 0x20E270) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsBtlChrGetMem();
    public static FhMethodHandle<d_MsBtlChrGetMem> MsBtlChrGetMem
        => new( new FhMethodLocation("FFX-2.exe", 0x20FE10) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsBtlChrNumCheck(uint chr_id);
    public static FhMethodHandle<d_MsBtlChrNumCheck> MsBtlChrNumCheck
        => new( new FhMethodLocation("FFX-2.exe", 0x20FF90) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsBtlMonsterSaveNumCheck(uint chr_id);
    public static FhMethodHandle<d_MsBtlMonsterSaveNumCheck> MsBtlMonsterSaveNumCheck
        => new( new FhMethodLocation("FFX-2.exe", 0x210440) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsBtlPlayerSaveNumCheck(uint chr_id);
    public static FhMethodHandle<d_MsBtlPlayerSaveNumCheck> MsBtlPlayerSaveNumCheck
        => new( new FhMethodLocation("FFX-2.exe", 0x210460) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate Chr* d_MsGetChr(uint chr_id);
    public static FhMethodHandle<d_MsGetChr> MsGetChr
        => new( new FhMethodLocation("FFX-2.exe", 0x211450) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetChrStatDeathStone(uint chr_id);
    public static FhMethodHandle<d_MsGetChrStatDeathStone> MsGetChrStatDeathStone
        => new( new FhMethodLocation("FFX-2.exe", 0x213360) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsCalcChrLevel(uint chr_id);
    public static FhMethodHandle<d_MsCalcChrLevel> MsCalcChrLevel
        => new( new FhMethodLocation("FFX-2.exe", 0x217140) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsCalcFirstAttack();
    public static FhMethodHandle<d_MsCalcFirstAttack> MsCalcFirstAttack
        => new( new FhMethodLocation("FFX-2.exe", 0x218B80) ) ;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsGetRndChr(uint chr_id, int arg2);
    public static FhMethodHandle<d_MsGetRndChr> MsGetRndChr
        =>new( new FhMethodLocation("FFX-2.exe", 0x21ADD0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsSetChrWeak(uint chr_id, int arg2);
    public static FhMethodHandle<d_MsSetChrWeak> MsSetChrWeak
        => new( new FhMethodLocation("FFX-2.exe", 0x21B080) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsCheckMonsterOversoul(uint chr_id);
    public static FhMethodHandle<d_MsCheckMonsterOversoul> MsCheckMonsterOversoul
        => new( new FhMethodLocation("FFX-2.exe", 0x21C290) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetJobNumBasic(uint chr_id);
    public static FhMethodHandle<d_MsGetJobNumBasic> MsGetJobNumBasic
        => new( new FhMethodLocation("FFX-2.exe", 0x21DE30) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate Job* d_MsGetRomJob(uint chr_id, uint job_id, byte* out_data_end);
    public static FhMethodHandle<d_MsGetRomJob> MsGetRomJob
        => new( new FhMethodLocation("FFX-2.exe", 0x21DEB0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsCheckStatCount(uint arg1);
    public static FhMethodHandle<d_MsCheckStatCount> MsCheckStatCount
        => new( new FhMethodLocation("FFX-2.exe", 0x2218E0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsStatusEffectCheck(uint chr_id);
    public static FhMethodHandle<d_MsStatusEffectCheck> MsStatusEffectCheck
        => new( new FhMethodLocation("FFX-2.exe", 0x223290) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetChrID(uint chr_id);
    public static FhMethodHandle<d_MsGetChrID> MsGetChrID
        => new( new FhMethodLocation("FFX-2.exe", 0x224F90) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetRamChrMonster(uint chr_id);
    public static FhMethodHandle<d_MsGetRamChrMonster> MsGetRamChrMonster
        => new( new FhMethodLocation("FFX-2.exe", 0x225BF0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsGetRamConfigChangeEffect();
    public static FhMethodHandle<d_MsGetRamConfigChangeEffect> MsGetRamConfigChangeEffect
        => new( new FhMethodLocation("FFX-2.exe", 0x225C90) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsSetRamChrParam(uint chr_id);
    public static FhMethodHandle<d_MsSetRamChrParam> MsSetRamChrParam
        => new( new FhMethodLocation("FFX-2.exe", 0x2275C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsSetRamMotionChrData(uint chr_id, uint job_id);
    public static FhMethodHandle<d_MsSetRamMotionChrData> MsSetRamMotionChrData
        => new( new FhMethodLocation("FFX-2.exe", 0x227A20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsCheckAbility(uint arg1, int arg2, int arg3);
    public static FhMethodHandle<d_MsCheckAbility> MsCheckAbility
        => new( new FhMethodLocation("FFX-2.exe", 0x229280) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_FUN_006294f0(uint arg1, int arg2, int arg3);
    public static FhMethodHandle<d_FUN_006294f0> FUN_006294f0
        => new( new FhMethodLocation("FFX-2.exe", 0x2294F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate ushort* d_MsGetJobAbilityList(uint chr_id, uint job_id, uint* ptr_list_length, int arg4);
    public static FhMethodHandle<d_MsGetJobAbilityList> MsGetJobAbilityList
        => new( new FhMethodLocation("FFX-2.exe", 0x229AF0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsStructClear(void* arg1, uint arg2);
    public static FhMethodHandle<d_MsStructClear> MsStructClear
        => new( new FhMethodLocation("FFX-2.exe", 0x22A0F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate uint d_FUN_0062AB30(uint chr_id, uint sound_id);
    public static FhMethodHandle<d_FUN_0062AB30> FUN_0062AB30
        => new( new FhMethodLocation("FFX-2.exe", 0x22AB30) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsMotionRecoverExe(uint chr_id, int arg2);
    public static FhMethodHandle<d_MsMotionRecoverExe> MsMotionRecoverExe
        => new( new FhMethodLocation("FFX-2.exe", 0x2330E0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsATBActiveCheck(uint chr_id, uint arg2);
    public static FhMethodHandle<d_MsATBActiveCheck> MsATBActiveCheck
        => new( new FhMethodLocation("FFX-2.exe", 0x233F90) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsATBgetRestTime(byte chr_id, uint command_id);
    public static FhMethodHandle<d_MsATBgetRestTime> MsATBgetRestTime
        => new( new FhMethodLocation("FFX-2.exe", 0x234140) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsATBgetThinkingTime(uint chr_id);
    public static FhMethodHandle<d_MsATBgetThinkingTime> MsATBgetThinkingTime
        => new( new FhMethodLocation("FFX-2.exe", 0x2341A0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsChrATBprocess();
    public static FhMethodHandle<d_MsChrATBprocess> MsChrATBprocess
        => new( new FhMethodLocation("FFX-2.exe", 0x2343D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsChrAtbInit(Chr* chr, int arg2, int arg3);
    public static FhMethodHandle<d_MsChrAtbInit> MsChrAtbInit
        => new( new FhMethodLocation("FFX-2.exe", 0x234730) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsChrAtbReset(uint chr_id, int arg2);
    public static FhMethodHandle<d_MsChrAtbReset> MsChrAtbReset
        => new( new FhMethodLocation("FFX-2.exe", 0x2348A0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsChrSetDecTime(uint chr_id, Chr* chr, uint rom_atb_speed);
    public static FhMethodHandle<d_MsChrSetDecTime> MsChrSetDecTime
        => new( new FhMethodLocation("FFX-2.exe", 0x234A20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsSetATBwait(sbyte target_value);
    public static FhMethodHandle<d_MsSetATBwait> MsSetATBwait
        => new( new FhMethodLocation("FFX-2.exe", 0x234AE0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_FUN_00634B00(Chr* chr);
    public static FhMethodHandle<d_FUN_00634B00> FUN_00634B00
        => new( new FhMethodLocation("FFX-2.exe", 0x234B00) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsActionRequest(uint chr_id, int arg2, int arg3, int arg4);
    public static FhMethodHandle<d_MsActionRequest> MsActionRequest
        => new( new FhMethodLocation("FFX-2.exe", 0x235300) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsCheckLearnCommand(uint chr_id, int ability_id);
    public static FhMethodHandle<d_MsCheckLearnCommand> MsCheckLearnCommand
        => new( new FhMethodLocation("FFX-2.exe", 0x235790) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsCheckDanceStatus(uint chr_id);
    public static FhMethodHandle<d_MsCheckDanceStatus> MsCheckDanceStatus
        => new( new FhMethodLocation("FFX-2.exe", 0x236360) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsClearDanceStatusMotion(uint chr_id);
    public static FhMethodHandle<d_MsClearDanceStatusMotion> MsClearDanceStatusMotion
        => new( new FhMethodLocation("FFX-2.exe", 0x236400) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_FUN_00636690(uint chr_id, Chr* chr, byte arg3);
    public static FhMethodHandle<d_FUN_00636690> FUN_00636690
        => new( new FhMethodLocation("FFX-2.exe", 0x236690) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsResetDefenseStatus(uint chr_id);
    public static FhMethodHandle<d_MsResetDefenseStatus> MsResetDefenseStatus
        => new( new FhMethodLocation("FFX-2.exe", 0x236900) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsSetStatus(uint chr_id, uint command_id, int arg3, int arg4);
    public static FhMethodHandle<d_MsSetStatus> MsSetStatus
        => new( new FhMethodLocation("FFX-2.exe", 0x236CA0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsStatusProcess();
    public static FhMethodHandle<d_MsStatusProcess> MsStatusProcess
        => new( new FhMethodLocation("FFX-2.exe", 0x236EB0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsCommandComplete(uint chr_id, int arg2, int arg3);
    public static FhMethodHandle<d_MsCommandComplete> MsCommandComplete
        => new( new FhMethodLocation("FFX-2.exe", 0x2401C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsDamageBufferExe(uint user_id, uint target_id, DamageBuffer* dmg_buffer);
    public static FhMethodHandle<d_MsDamageBufferExe> MsDamageBufferExe
        => new( new FhMethodLocation("FFX-2.exe", 0x2422D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsStatCheckStop(byte chr_id, int arg2);
    public static FhMethodHandle<d_MsStatCheckStop> MsStatCheckStop
        => new( new FhMethodLocation("FFX-2.exe", 0x2430F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte d_MsMagicCheckCommandExe(int arg1, uint arg2, int* arg3, int* arg4);
    public static FhMethodHandle<d_MsMagicCheckCommandExe> MsMagicCheckCommandExe
        => new( new FhMethodLocation("FFX-2.exe", 0x244BB0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsActionAI(uint chr_id, int arg2, int arg3);
    public static FhMethodHandle<d_MsActionAI> MsActionAI
        => new( new FhMethodLocation("FFX-2.exe", 0x248520) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsAutoBerserkProcess(uint chr_id, Chr* chr);
    public static FhMethodHandle<d_MsAutoBerserkProcess> MsAutoBerserkProcess
        => new( new FhMethodLocation("FFX-2.exe", 0x249100) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_Ch_SetMotionSpeed(uint ptr_actor, ushort speed);
    public static FhMethodHandle<d_Ch_SetMotionSpeed> Ch_SetMotionSpeed
        => new( new FhMethodLocation("FFX-2.exe", 0x2E63B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt> CT_RetInt_0172_fillPartyMemberMp
        => new( new FhMethodLocation("FFX-2.exe", 0x319360) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOBtlDrawATBGaude(int arg1, int arg2, int arg3);
    public static FhMethodHandle<d_TOBtlDrawATBGaude> TOBtlDrawATBGaude
        => new( new FhMethodLocation("FFX-2.exe", 0x356590) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte* d_TOBtlGetComName(uint ability_id);
    public static FhMethodHandle<d_TOBtlGetComName> TOBtlGetComName
        => new( new FhMethodLocation("FFX-2.exe", 0x359FD0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOBtlSetATBChr(byte chr_id);
    public static FhMethodHandle<d_TOBtlSetATBChr> TOBtlSetATBChr
        => new( new FhMethodLocation("FFX-2.exe", 0x35D0C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOCtrlATBChr();
    public static FhMethodHandle<d_TOCtrlATBChr> TOCtrlATBChr
        => new( new FhMethodLocation("FFX-2.exe", 0x35E2C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMenuSetHelpMes(byte* ptr_text);
    public static FhMethodHandle<d_TOMenuSetHelpMes> TOMenuSetHelpMes
        => new( new FhMethodLocation("FFX-2.exe", 0x363970) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte d_TkMenuGetTimer();
    public static FhMethodHandle<d_TkMenuGetTimer> TkMenuGetTimer
        => new( new FhMethodLocation("FFX-2.exe", 0x364680) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TkMenuSetHelpMessage(byte* ptr_text);
    public static FhMethodHandle<d_TkMenuSetHelpMessage> TkMenuSetHelpMessage
        => new( new FhMethodLocation("FFX-2.exe", 0x365B20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate byte* d_GetLastMissionJobName(byte arg1, byte arg2);
    internal static FhMethodHandle<d_GetLastMissionJobName> GetLastMissionJobName
        => new( new FhMethodLocation("FFX-2.exe", 0x368570) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_TOGetRtcRatio(uint arg1);
    public static FhMethodHandle<d_TOGetRtcRatio> TOGetRtcRatio
        => new( new FhMethodLocation("FFX-2.exe", 0x3730C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_TOGetRtcValue(uint arg1);
    public static FhMethodHandle<d_TOGetRtcValue> TOGetRtcValue
        => new( new FhMethodLocation("FFX-2.exe", 0x3730E0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate float d_offsetAdjust_X(int arg1);
    public static FhMethodHandle<d_offsetAdjust_X> offsetAdjust_X
        => new( new FhMethodLocation("FFX-2.exe", 0x3764C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate float d_offsetAdjust_Y(int arg1);
    public static FhMethodHandle<d_offsetAdjust_Y> offsetAdjust_Y
        => new( new FhMethodLocation("FFX-2.exe", 0x3764E0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMenuOpenPkt();
    public static FhMethodHandle<d_TOMenuOpenPkt> TOMenuOpenPkt
        => new( new FhMethodLocation("FFX-2.exe", 0x376910) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_FUN_776EC0(uint arg1, uint ability_slot);
    public static FhMethodHandle<d_FUN_776EC0> FUN_776EC0
        => new( new FhMethodLocation("FFX-2.exe", 0x376EC0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_777270(uint arg1);
    public static FhMethodHandle<d_FUN_777270> FUN_777270
        => new( new FhMethodLocation("FFX-2.exe", 0x377270) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_777C60(uint arg1);
    public static FhMethodHandle<d_FUN_777C60> FUN_777C60
        => new( new FhMethodLocation("FFX-2.exe", 0x377C60) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_778160(int arg1, int arg2, int arg3, int arg4);
    public static FhMethodHandle<d_FUN_778160> FUN_778160
        => new( new FhMethodLocation("FFX-2.exe", 0x378160) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_TOMenuGetJobLearnedRate(uint chr_id, uint job_id);
    public static FhMethodHandle<d_TOMenuGetJobLearnedRate> TOMenuGetJobLearnedRate
        => new( new FhMethodLocation("FFX-2.exe", 0x3786B0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMenuMakeJobAbilityList(uint chr_id, uint job_id);
    public static FhMethodHandle<d_TOMenuMakeJobAbilityList> TOMenuMakeJobAbilityList
        => new( new FhMethodLocation("FFX-2.exe", 0x3788D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMenuMakeJobList(uint chr_id);
    public static FhMethodHandle<d_TOMenuMakeJobList> TOMenuMakeJobList
        => new( new FhMethodLocation("FFX-2.exe", 0x378B00) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_TOMenuNextJobList();
    public static FhMethodHandle<d_TOMenuNextJobList> TOMenuNextJobList
        => new( new FhMethodLocation("FFX-2.exe", 0x378CD0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_TOMenuPrevJobList();
    public static FhMethodHandle<d_TOMenuPrevJobList> TOMenuPrevJobList
        => new( new FhMethodLocation("FFX-2.exe", 0x378E80) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMenuSetSaveLearn(uint chr_id, uint job_id, uint slot);
    public static FhMethodHandle<d_TOMenuSetSaveLearn> TOMenuSetSaveLearn
        => new( new FhMethodLocation("FFX-2.exe", 0x378F40) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMenuStartJobAbilityWindow(uint chr_id, uint job_id);
    public static FhMethodHandle<d_TOMenuStartJobAbilityWindow> TOMenuStartJobAbilityWindow
        => new( new FhMethodLocation("FFX-2.exe", 0x378F70) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte* d_TOGetMenuText(uint arg1);
    public static FhMethodHandle<d_TOGetMenuText> TOGetMenuText
        => new( new FhMethodLocation("FFX-2.exe", 0x379250) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMenuDrawRotPlate(int arg1, int arg2, int arg3, int arg4, int arg5, int arg6, int arg7);
    public static FhMethodHandle<d_TOMenuDrawRotPlate> TOMenuDrawRotPlate
        => new( new FhMethodLocation("FFX-2.exe", 0x379DC0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMkpExPlateParam(int arg1, int arg2, int arg3, int arg4, int arg5);
    public static FhMethodHandle<d_TOMkpExPlateParam> TOMkpExPlateParam
        => new( new FhMethodLocation("FFX-2.exe", 0x37AA20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TODVDFileReadNonBlock(int arg1, int arg2, int arg3);
    public static FhMethodHandle<d_TODVDFileReadNonBlock> TODVDFileReadNonBlock
        => new( new FhMethodLocation("FFX-2.exe", 0x391610) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_TOGetFFXLang();
    public static FhMethodHandle<d_TOGetFFXLang> TOGetFFXLang
        => new( new FhMethodLocation("FFX-2.exe", 0x393010) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_TOGetFaceIndex2(uint chr_id, uint arg2);
    public static FhMethodHandle<d_TOGetFaceIndex2> TOGetFaceIndex2
        => new( new FhMethodLocation("FFX-2.exe", 0x393190) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte* d_TOGetRomHelp(int arg1);
    public static FhMethodHandle<d_TOGetRomHelp> TOGetRomHelp
        => new( new FhMethodLocation("FFX-2.exe", 0x394500) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte* d_TOGetSaveJobName(uint chr_id);
    public static FhMethodHandle<d_TOGetSaveJobName> TOGetSaveJobName
        => new( new FhMethodLocation("FFX-2.exe", 0x394600) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMenuSetMacroCommandType(int arg1, int arg2, byte arg3);
    public static FhMethodHandle<d_TOMenuSetMacroCommandType> TOMenuSetMacroCommandType
        => new( new FhMethodLocation("FFX-2.exe", 0x396330) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMenuSetMacroCommandValue(int arg1, int arg2, byte* arg3);
    public static FhMethodHandle<d_TOMenuSetMacroCommandValue> TOMenuSetMacroCommandValue
        => new( new FhMethodLocation("FFX-2.exe", 0x396360) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FFX2_Reset_UI_Scale();
    public static FhMethodHandle<d_FFX2_Reset_UI_Scale> FFX2_Reset_UI_Scale
        => new( new FhMethodLocation("FFX-2.exe", 0x3A0030) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FFX2_Set_UI_Scale(float arg1, float arg2);
    public static FhMethodHandle<d_FFX2_Set_UI_Scale> FFX2_Set_UI_Scale
        => new( new FhMethodLocation("FFX-2.exe", 0x3A0060) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOKickPacket();
    public static FhMethodHandle<d_TOKickPacket> TOKickPacket
        => new( new FhMethodLocation("FFX-2.exe", 0x3ADD10) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_FUN_007AE430(byte* arg1, int arg2, int arg3, int arg4, int arg5, int arg6, int arg7);
    public static FhMethodHandle<d_FUN_007AE430> FUN_007AE430
        => new( new FhMethodLocation("FFX-2.exe", 0x3AE430) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMenuChangeFrameAccPlate(int arg1);
    public static FhMethodHandle<d_TOMenuChangeFrameAccPlate> TOMenuChangeFrameAccPlate
        => new( new FhMethodLocation("FFX-2.exe", 0x3AE7E0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMkpComIconNameClut(uint arg1, int arg2, int arg3, int arg4);
    public static FhMethodHandle<d_TOMkpComIconNameClut> TOMkpComIconNameClut
        => new( new FhMethodLocation("FFX-2.exe", 0x3AE9B0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_007AEDA0(byte* arg1, float arg2, float arg3);
    public static FhMethodHandle<d_FUN_007AEDA0> FUN_007AEDA0
        => new( new FhMethodLocation("FFX-2.exe", 0x3AEDA0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMkpResetFrameAcc();
    public static FhMethodHandle<d_TOMkpResetFrameAcc> TOMkpResetFrameAcc
        => new( new FhMethodLocation("FFX-2.exe", 0x3B0A60) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMkpScrollWaveXYWH(int x, int y, int w, int h, int colour);
    public static FhMethodHandle<d_TOMkpScrollWaveXYWH> TOMkpScrollWaveXYWH
        => new( new FhMethodLocation("FFX-2.exe", 0x3B0F50) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMkpShape2dMenu(int x, int y, int arg3, int arg4);
    public static FhMethodHandle<d_TOMkpShape2dMenu> TOMkpShape2dMenu
        => new( new FhMethodLocation("FFX-2.exe", 0x3B1250) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_rcefObjProc(RcEffectObj* ptr_rcef_obj);
    public static FhMethodHandle<d_rcefObjProc> rcefObjProc
        => new( new FhMethodLocation("FFX-2.exe", 0x3EA6C0) );

}
