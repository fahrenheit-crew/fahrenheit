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
    public unsafe delegate void d_FUN_00134bd0(int* ptr_this, int arg2, int arg3, int arg4, int arg5, int arg6, int* arg7, int* arg8, int* arg9);
    public static FhMethodHandle<d_FUN_00134bd0> FUN_00134bd0
        => new( new FhMethodLocation("FFX-2.exe", 0x134BD0) );

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
    public delegate int d_FUN_00208380(uint arg1);
    public static FhMethodHandle<d_FUN_00208380> FUN_00208380
        => new( new FhMethodLocation("FFX-2.exe", 0x208380) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsAddSaveDreSphere(uint job_id, int amount);
    public static FhMethodHandle<d_MsAddSaveDreSphere> MsAddSaveDreSphere
        => new( new FhMethodLocation("FFX-2.exe", 0x20B230) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetChrNum(uint chr_id);
    public static FhMethodHandle<d_MsGetChrNum> MsGetChrNum
        => new( new FhMethodLocation("FFX-2.exe", 0x20C170) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetSaveAp(uint chr_id, uint ability_id);
    public static FhMethodHandle<d_MsGetSaveAp> MsGetSaveAp
        => new( new FhMethodLocation("FFX-2.exe", 0x20C2B0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate byte* d_MsGetSaveChrName(uint chr_id);
    public static FhMethodHandle<d_MsGetSaveChrName> MsGetSaveChrName
        => new( new FhMethodLocation("FFX-2.exe", 0x20C470) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetSaveCommand(uint chr_id, uint ability_id);
    public static FhMethodHandle<d_MsGetSaveCommand> MsGetSaveCommand
        => new( new FhMethodLocation("FFX-2.exe", 0x20C4D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetSaveConfigChangeEffect();
    public static FhMethodHandle<d_MsGetSaveConfigChangeEffect> MsGetSaveConfigChangeEffect
        => new( new FhMethodLocation("FFX-2.exe", 0x20C620) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsGetSaveDreSphere(uint job_id);
    public static FhMethodHandle<d_MsGetSaveDreSphere> MsGetSaveDreSphere
        => new( new FhMethodLocation("FFX-2.exe", 0x20C6E0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetSaveDressUpCount(uint chr_id, uint arg2);
    public static FhMethodHandle<d_MsGetSaveDressUpCount> MsGetSaveDressUpCount
        => new( new FhMethodLocation("FFX-2.exe", 0x20C700) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetSaveJob(uint chr_id);
    public static FhMethodHandle<d_MsGetSaveJob> MsGetSaveJob
        => new( new FhMethodLocation("FFX-2.exe", 0x20C920) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetSaveLearn(uint chr_id, uint job_id);
    public static FhMethodHandle<d_MsGetSaveLearn> MsGetSaveLearn
        => new( new FhMethodLocation("FFX-2.exe", 0x20CA40) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetSaveNeedAp(uint chr_id, uint ability_id);
    public static FhMethodHandle<d_MsGetSaveNeedAp> MsGetSaveNeedAp
        => new( new FhMethodLocation("FFX-2.exe", 0x20CAF0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetSavePlate(uint plate_id);
    public static FhMethodHandle<d_MsGetSavePlate> MsGetSavePlate
        => new( new FhMethodLocation("FFX-2.exe", 0x20CBD0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate uint d_CalculateStats(uint chr_id, int chr_level, uint job_id, PlySave* ptr_ply_save, int* ptr_stats);
    public static FhMethodHandle<d_CalculateStats> CalculateStats
        => new( new FhMethodLocation("FFX-2.exe", 0x20D6F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsSetSaveLearn(uint chr_id, uint job_id, ushort ability_id);
    public static FhMethodHandle<d_MsSetSaveLearn> MsSetSaveLearn
        => new( new FhMethodLocation("FFX-2.exe", 0x20E240) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsBtlChrGetMem();
    public static FhMethodHandle<d_MsBtlChrGetMem> MsBtlChrGetMem
        => new( new FhMethodLocation("FFX-2.exe", 0x20FDE0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsBtlChrNumCheck(uint chr_id);
    public static FhMethodHandle<d_MsBtlChrNumCheck> MsBtlChrNumCheck
        => new( new FhMethodLocation("FFX-2.exe", 0x20FF60) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsBtlMonsterSaveNumCheck(uint chr_id);
    public static FhMethodHandle<d_MsBtlMonsterSaveNumCheck> MsBtlMonsterSaveNumCheck
        => new( new FhMethodLocation("FFX-2.exe", 0x210410) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsBtlPlayerSaveNumCheck(uint chr_id);
    public static FhMethodHandle<d_MsBtlPlayerSaveNumCheck> MsBtlPlayerSaveNumCheck
        => new( new FhMethodLocation("FFX-2.exe", 0x210430) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate Chr* d_MsGetChr(uint chr_id);
    public static FhMethodHandle<d_MsGetChr> MsGetChr
        => new( new FhMethodLocation("FFX-2.exe", 0x211420) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetChrStatDeathStone(uint chr_id);
    public static FhMethodHandle<d_MsGetChrStatDeathStone> MsGetChrStatDeathStone
        => new( new FhMethodLocation("FFX-2.exe", 0x213340) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsCalcChrLevel(uint chr_id);
    public static FhMethodHandle<d_MsCalcChrLevel> MsCalcChrLevel
        => new( new FhMethodLocation("FFX-2.exe", 0x217120) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsCalcFirstAttack();
    public static FhMethodHandle<d_MsCalcFirstAttack> MsCalcFirstAttack
        => new( new FhMethodLocation("FFX-2.exe", 0x218B60) ) ;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsGetRndChr(uint chr_id, int arg2);
    public static FhMethodHandle<d_MsGetRndChr> MsGetRndChr
        =>new( new FhMethodLocation("FFX-2.exe", 0x21ADB0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsSetChrWeak(uint chr_id, int arg2);
    public static FhMethodHandle<d_MsSetChrWeak> MsSetChrWeak
        => new( new FhMethodLocation("FFX-2.exe", 0x21B060) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsCheckMonsterOversoul(uint chr_id);
    public static FhMethodHandle<d_MsCheckMonsterOversoul> MsCheckMonsterOversoul
        => new( new FhMethodLocation("FFX-2.exe", 0x21C270) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetJobNumBasic(uint chr_id);
    public static FhMethodHandle<d_MsGetJobNumBasic> MsGetJobNumBasic
        => new( new FhMethodLocation("FFX-2.exe", 0x21DE10) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate Job* d_MsGetRomJob(uint chr_id, uint job_id, byte* out_data_end);
    public static FhMethodHandle<d_MsGetRomJob> MsGetRomJob
        => new( new FhMethodLocation("FFX-2.exe", 0x21DE90) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsCheckStatCount(uint arg1);
    public static FhMethodHandle<d_MsCheckStatCount> MsCheckStatCount
        => new( new FhMethodLocation("FFX-2.exe", 0x2218C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsStatusEffectCheck(uint chr_id);
    public static FhMethodHandle<d_MsStatusEffectCheck> MsStatusEffectCheck
        => new( new FhMethodLocation("FFX-2.exe", 0x223260) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetChrID(uint chr_id);
    public static FhMethodHandle<d_MsGetChrID> MsGetChrID
        => new( new FhMethodLocation("FFX-2.exe", 0x224F60) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsGetRamChrMonster(uint chr_id);
    public static FhMethodHandle<d_MsGetRamChrMonster> MsGetRamChrMonster
        => new( new FhMethodLocation("FFX-2.exe", 0x225BC0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsGetRamConfigChangeEffect();
    public static FhMethodHandle<d_MsGetRamConfigChangeEffect> MsGetRamConfigChangeEffect
        => new( new FhMethodLocation("FFX-2.exe", 0x225C60) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsSetRamChrParam(uint chr_id);
    public static FhMethodHandle<d_MsSetRamChrParam> MsSetRamChrParam
        => new( new FhMethodLocation("FFX-2.exe", 0x227590) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsSetRamMotionChrData(uint chr_id, uint job_id);
    public static FhMethodHandle<d_MsSetRamMotionChrData> MsSetRamMotionChrData
        => new( new FhMethodLocation("FFX-2.exe", 0x2279F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsCheckAbility(uint arg1, int arg2, int arg3);
    public static FhMethodHandle<d_MsCheckAbility> MsCheckAbility
        => new( new FhMethodLocation("FFX-2.exe", 0x229260) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_FUN_002294d0(uint arg1, int arg2, int arg3);
    public static FhMethodHandle<d_FUN_002294d0> FUN_002294d0
        => new( new FhMethodLocation("FFX-2.exe", 0x2294D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate ushort* d_MsGetJobAbilityList(uint chr_id, uint job_id, uint* ptr_list_length, int arg4);
    public static FhMethodHandle<d_MsGetJobAbilityList> MsGetJobAbilityList
        => new( new FhMethodLocation("FFX-2.exe", 0x229AD0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsStructClear(void* arg1, uint arg2);
    public static FhMethodHandle<d_MsStructClear> MsStructClear
        => new( new FhMethodLocation("FFX-2.exe", 0x22A0D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate uint d_FUN_0022ab10(uint chr_id, uint sound_id);
    public static FhMethodHandle<d_FUN_0022ab10> FUN_0022ab10
        => new( new FhMethodLocation("FFX-2.exe", 0x22AB10) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsMotionRecoverExe(uint chr_id, int arg2);
    public static FhMethodHandle<d_MsMotionRecoverExe> MsMotionRecoverExe
        => new( new FhMethodLocation("FFX-2.exe", 0x2330C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsATBActiveCheck(uint chr_id, uint arg2);
    public static FhMethodHandle<d_MsATBActiveCheck> MsATBActiveCheck
        => new( new FhMethodLocation("FFX-2.exe", 0x233F60) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsATBgetRestTime(byte chr_id, uint command_id);
    public static FhMethodHandle<d_MsATBgetRestTime> MsATBgetRestTime
        => new( new FhMethodLocation("FFX-2.exe", 0x234110) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsATBgetThinkingTime(uint chr_id);
    public static FhMethodHandle<d_MsATBgetThinkingTime> MsATBgetThinkingTime
        => new( new FhMethodLocation("FFX-2.exe", 0x234170) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsChrATBprocess();
    public static FhMethodHandle<d_MsChrATBprocess> MsChrATBprocess
        => new( new FhMethodLocation("FFX-2.exe", 0x2343A0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsChrAtbInit(Chr* chr, int arg2, int arg3);
    public static FhMethodHandle<d_MsChrAtbInit> MsChrAtbInit
        => new( new FhMethodLocation("FFX-2.exe", 0x234700) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsChrAtbReset(uint chr_id, int arg2);
    public static FhMethodHandle<d_MsChrAtbReset> MsChrAtbReset
        => new( new FhMethodLocation("FFX-2.exe", 0x234870) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsChrSetDecTime(uint chr_id, Chr* chr, uint rom_atb_speed);
    public static FhMethodHandle<d_MsChrSetDecTime> MsChrSetDecTime
        => new( new FhMethodLocation("FFX-2.exe", 0x2349F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsSetATBwait(sbyte target_value);
    public static FhMethodHandle<d_MsSetATBwait> MsSetATBwait
        => new( new FhMethodLocation("FFX-2.exe", 0x234AB0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_FUN_00234ad0(Chr* chr);
    public static FhMethodHandle<d_FUN_00234ad0> FUN_00234ad0
        => new( new FhMethodLocation("FFX-2.exe", 0x234AD0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsActionRequest(uint chr_id, int arg2, int arg3, int arg4);
    public static FhMethodHandle<d_MsActionRequest> MsActionRequest
        => new( new FhMethodLocation("FFX-2.exe", 0x2352D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsCheckLearnCommand(uint chr_id, int ability_id);
    public static FhMethodHandle<d_MsCheckLearnCommand> MsCheckLearnCommand
        => new( new FhMethodLocation("FFX-2.exe", 0x235760) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsCheckDanceStatus(uint chr_id);
    public static FhMethodHandle<d_MsCheckDanceStatus> MsCheckDanceStatus
        => new( new FhMethodLocation("FFX-2.exe", 0x236330) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsClearDanceStatusMotion(uint chr_id);
    public static FhMethodHandle<d_MsClearDanceStatusMotion> MsClearDanceStatusMotion
        => new( new FhMethodLocation("FFX-2.exe", 0x2363D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_FUN_00236660(uint chr_id, Chr* chr, byte arg3);
    public static FhMethodHandle<d_FUN_00236660> FUN_00236660
        => new( new FhMethodLocation("FFX-2.exe", 0x236660) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsResetDefenseStatus(uint chr_id);
    public static FhMethodHandle<d_MsResetDefenseStatus> MsResetDefenseStatus
        => new( new FhMethodLocation("FFX-2.exe", 0x2368D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsSetStatus(uint chr_id, uint command_id, int arg3, int arg4);
    public static FhMethodHandle<d_MsSetStatus> MsSetStatus
        => new( new FhMethodLocation("FFX-2.exe", 0x236C70) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsStatusProcess();
    public static FhMethodHandle<d_MsStatusProcess> MsStatusProcess
        => new( new FhMethodLocation("FFX-2.exe", 0x236E80) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsCommandComplete(uint chr_id, int arg2, int arg3);
    public static FhMethodHandle<d_MsCommandComplete> MsCommandComplete
        => new( new FhMethodLocation("FFX-2.exe", 0x240190) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsDamageBufferExe(uint user_id, uint target_id, DamageBuffer* dmg_buffer);
    public static FhMethodHandle<d_MsDamageBufferExe> MsDamageBufferExe
        => new( new FhMethodLocation("FFX-2.exe", 0x2422A0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsStatCheckStop(byte chr_id, int arg2);
    public static FhMethodHandle<d_MsStatCheckStop> MsStatCheckStop
        => new( new FhMethodLocation("FFX-2.exe", 0x2430C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte d_MsMagicCheckCommandExe(int arg1, uint arg2, int* arg3, int* arg4);
    public static FhMethodHandle<d_MsMagicCheckCommandExe> MsMagicCheckCommandExe
        => new( new FhMethodLocation("FFX-2.exe", 0x244B80) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsActionAI(uint chr_id, int arg2, int arg3);
    public static FhMethodHandle<d_MsActionAI> MsActionAI
        => new( new FhMethodLocation("FFX-2.exe", 0x2484F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_MsAutoBerserkProcess(uint chr_id, Chr* chr);
    public static FhMethodHandle<d_MsAutoBerserkProcess> MsAutoBerserkProcess
        => new( new FhMethodLocation("FFX-2.exe", 0x2490D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_Ch_SetMotionSpeed(uint ptr_actor, ushort speed);
    public static FhMethodHandle<d_Ch_SetMotionSpeed> Ch_SetMotionSpeed
        => new( new FhMethodLocation("FFX-2.exe", 0x2E62D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> CT_RetInt_0172_fillPartyMemberMp
        => new( new FhMethodLocation("FFX-2.exe", 0x319300) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOBtlDrawATBGaude(int arg1, int arg2, int arg3);
    public static FhMethodHandle<d_TOBtlDrawATBGaude> TOBtlDrawATBGaude
        => new( new FhMethodLocation("FFX-2.exe", 0x3564C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte* d_TOBtlGetComName(uint ability_id);
    public static FhMethodHandle<d_TOBtlGetComName> TOBtlGetComName
        => new( new FhMethodLocation("FFX-2.exe", 0x359EF0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOBtlSetATBChr(byte chr_id);
    public static FhMethodHandle<d_TOBtlSetATBChr> TOBtlSetATBChr
        => new( new FhMethodLocation("FFX-2.exe", 0x35D000) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOCtrlATBChr();
    public static FhMethodHandle<d_TOCtrlATBChr> TOCtrlATBChr
        => new( new FhMethodLocation("FFX-2.exe", 0x35E200) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMenuSetHelpMes(byte* ptr_text);
    public static FhMethodHandle<d_TOMenuSetHelpMes> TOMenuSetHelpMes
        => new( new FhMethodLocation("FFX-2.exe", 0x3638A0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte d_TkMenuGetTimer();
    public static FhMethodHandle<d_TkMenuGetTimer> TkMenuGetTimer
        => new( new FhMethodLocation("FFX-2.exe", 0x3645B0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TkMenuSetHelpMessage(byte* ptr_text);
    public static FhMethodHandle<d_TkMenuSetHelpMessage> TkMenuSetHelpMessage
        => new( new FhMethodLocation("FFX-2.exe", 0x365A50) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate byte* d_GetLastMissionJobName(byte arg1, byte arg2);
    internal static FhMethodHandle<d_GetLastMissionJobName> GetLastMissionJobName
        => new( new FhMethodLocation("FFX-2.exe", 0x3684A0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_TOGetRtcRatio(uint arg1);
    public static FhMethodHandle<d_TOGetRtcRatio> TOGetRtcRatio
        => new( new FhMethodLocation("FFX-2.exe", 0x372FF0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_TOGetRtcValue(uint arg1);
    public static FhMethodHandle<d_TOGetRtcValue> TOGetRtcValue
        => new( new FhMethodLocation("FFX-2.exe", 0x373010) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate float d_offsetAdjust_X(int arg1);
    public static FhMethodHandle<d_offsetAdjust_X> offsetAdjust_X
        => new( new FhMethodLocation("FFX-2.exe", 0x3763E0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate float d_offsetAdjust_Y(int arg1);
    public static FhMethodHandle<d_offsetAdjust_Y> offsetAdjust_Y
        => new( new FhMethodLocation("FFX-2.exe", 0x376400) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMenuOpenPkt();
    public static FhMethodHandle<d_TOMenuOpenPkt> TOMenuOpenPkt
        => new( new FhMethodLocation("FFX-2.exe", 0x376830) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_FUN_00376de0(uint arg1, uint ability_slot);
    public static FhMethodHandle<d_FUN_00376de0> FUN_00376de0
        => new( new FhMethodLocation("FFX-2.exe", 0x376DE0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_00377190(uint arg1);
    public static FhMethodHandle<d_FUN_00377190> FUN_00377190
        => new( new FhMethodLocation("FFX-2.exe", 0x377190) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_00377b80(uint arg1);
    public static FhMethodHandle<d_FUN_00377b80> FUN_00377b80
        => new( new FhMethodLocation("FFX-2.exe", 0x377B80) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_00378080(int arg1, int arg2, int arg3, int arg4);
    public static FhMethodHandle<d_FUN_00378080> FUN_00378080
        => new( new FhMethodLocation("FFX-2.exe", 0x378080) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_TOMenuGetJobLearnedRate(uint chr_id, uint job_id);
    public static FhMethodHandle<d_TOMenuGetJobLearnedRate> TOMenuGetJobLearnedRate
        => new( new FhMethodLocation("FFX-2.exe", 0x3785D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMenuMakeJobAbilityList(uint chr_id, uint job_id);
    public static FhMethodHandle<d_TOMenuMakeJobAbilityList> TOMenuMakeJobAbilityList
        => new( new FhMethodLocation("FFX-2.exe", 0x3787F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMenuMakeJobList(uint chr_id);
    public static FhMethodHandle<d_TOMenuMakeJobList> TOMenuMakeJobList
        => new( new FhMethodLocation("FFX-2.exe", 0x378A20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_TOMenuNextJobList();
    public static FhMethodHandle<d_TOMenuNextJobList> TOMenuNextJobList
        => new( new FhMethodLocation("FFX-2.exe", 0x378BF0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_TOMenuPrevJobList();
    public static FhMethodHandle<d_TOMenuPrevJobList> TOMenuPrevJobList
        => new( new FhMethodLocation("FFX-2.exe", 0x378DA0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMenuSetSaveLearn(uint chr_id, uint job_id, uint slot);
    public static FhMethodHandle<d_TOMenuSetSaveLearn> TOMenuSetSaveLearn
        => new( new FhMethodLocation("FFX-2.exe", 0x378E60) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMenuStartJobAbilityWindow(uint chr_id, uint job_id);
    public static FhMethodHandle<d_TOMenuStartJobAbilityWindow> TOMenuStartJobAbilityWindow
        => new( new FhMethodLocation("FFX-2.exe", 0x378E90) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte* d_TOGetMenuText(uint arg1);
    public static FhMethodHandle<d_TOGetMenuText> TOGetMenuText
        => new( new FhMethodLocation("FFX-2.exe", 0x379170) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMenuDrawRotPlate(int arg1, int arg2, int arg3, int arg4, int arg5, int arg6, int arg7);
    public static FhMethodHandle<d_TOMenuDrawRotPlate> TOMenuDrawRotPlate
        => new( new FhMethodLocation("FFX-2.exe", 0x379CE0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMkpExPlateParam(int arg1, int arg2, int arg3, int arg4, int arg5);
    public static FhMethodHandle<d_TOMkpExPlateParam> TOMkpExPlateParam
        => new( new FhMethodLocation("FFX-2.exe", 0x37A940) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TODVDFileReadNonBlock(int arg1, int arg2, int arg3);
    public static FhMethodHandle<d_TODVDFileReadNonBlock> TODVDFileReadNonBlock
        => new( new FhMethodLocation("FFX-2.exe", 0x391530) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_TOGetFFXLang();
    public static FhMethodHandle<d_TOGetFFXLang> TOGetFFXLang
        => new( new FhMethodLocation("FFX-2.exe", 0x392EE0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_TOGetFaceIndex2(uint chr_id, uint arg2);
    public static FhMethodHandle<d_TOGetFaceIndex2> TOGetFaceIndex2
        => new( new FhMethodLocation("FFX-2.exe", 0x393060) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte* d_TOGetRomHelp(int arg1);
    public static FhMethodHandle<d_TOGetRomHelp> TOGetRomHelp
        => new( new FhMethodLocation("FFX-2.exe", 0x3943D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte* d_TOGetSaveJobName(uint chr_id);
    public static FhMethodHandle<d_TOGetSaveJobName> TOGetSaveJobName
        => new( new FhMethodLocation("FFX-2.exe", 0x3944D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMenuSetMacroCommandType(int arg1, int arg2, byte arg3);
    public static FhMethodHandle<d_TOMenuSetMacroCommandType> TOMenuSetMacroCommandType
        => new( new FhMethodLocation("FFX-2.exe", 0x396200) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMenuSetMacroCommandValue(int arg1, int arg2, byte* arg3);
    public static FhMethodHandle<d_TOMenuSetMacroCommandValue> TOMenuSetMacroCommandValue
        => new( new FhMethodLocation("FFX-2.exe", 0x396230) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FFX2_Reset_UI_Scale();
    public static FhMethodHandle<d_FFX2_Reset_UI_Scale> FFX2_Reset_UI_Scale
        => new( new FhMethodLocation("FFX-2.exe", 0x39FF00) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FFX2_Set_UI_Scale(float arg1, float arg2);
    public static FhMethodHandle<d_FFX2_Set_UI_Scale> FFX2_Set_UI_Scale
        => new( new FhMethodLocation("FFX-2.exe", 0x39FF30) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOKickPacket();
    public static FhMethodHandle<d_TOKickPacket> TOKickPacket
        => new( new FhMethodLocation("FFX-2.exe", 0x3ADC00) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_FUN_003ae330(byte* arg1, int arg2, int arg3, int arg4, int arg5, int arg6, int arg7);
    public static FhMethodHandle<d_FUN_003ae330> FUN_003ae330
        => new( new FhMethodLocation("FFX-2.exe", 0x3AE330) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMenuChangeFrameAccPlate(int arg1);
    public static FhMethodHandle<d_TOMenuChangeFrameAccPlate> TOMenuChangeFrameAccPlate
        => new( new FhMethodLocation("FFX-2.exe", 0x3AE6E0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMkpComIconNameClut(uint arg1, int arg2, int arg3, int arg4);
    public static FhMethodHandle<d_TOMkpComIconNameClut> TOMkpComIconNameClut
        => new( new FhMethodLocation("FFX-2.exe", 0x3AE8B0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_003aeca0(byte* arg1, float arg2, float arg3);
    public static FhMethodHandle<d_FUN_003aeca0> FUN_003aeca0
        => new( new FhMethodLocation("FFX-2.exe", 0x3AECA0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMkpResetFrameAcc();
    public static FhMethodHandle<d_TOMkpResetFrameAcc> TOMkpResetFrameAcc
        => new( new FhMethodLocation("FFX-2.exe", 0x3B0970) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMkpScrollWaveXYWH(int x, int y, int w, int h, int colour);
    public static FhMethodHandle<d_TOMkpScrollWaveXYWH> TOMkpScrollWaveXYWH
        => new( new FhMethodLocation("FFX-2.exe", 0x3B0E60) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TOMkpShape2dMenu(int x, int y, int arg3, int arg4);
    public static FhMethodHandle<d_TOMkpShape2dMenu> TOMkpShape2dMenu
        => new( new FhMethodLocation("FFX-2.exe", 0x3B1160) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_rcefObjProc(RcEffectObj* ptr_rcef_obj);
    public static FhMethodHandle<d_rcefObjProc> rcefObjProc
        => new( new FhMethodLocation("FFX-2.exe", 0x3EA5E0) );

}
