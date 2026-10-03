// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

/* [fkelava 26/04/26 15:26]
 * Unlike `call.g.cs`, which contains source-generated delegates with no guarantee of accuracy,
 * this file contains manually annotated calls with proper Fahrenheit types that are vetted for functionality.
 *
 * This file contains calls which are analogous between both games. Unlike the per-game `call.cs` files,
 * functions included here are expected to also provide a 'select' helper for automatic .
 */

using Fahrenheit.Atel;

namespace Fahrenheit;

/// <summary>
///     An accessor for game function calls available in both titles.
/// </summary>
public static unsafe partial class FhCall {

    /// <summary>
    ///     This delegate is assigned to functions for which Fahrenheit
    ///     has no meaningful information. This function requires manual attention.
    /// </summary>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_UnknownFn();

    // INTERNAL/RESTRICTED - BEGIN

    /* [fkelava 28/05/26 13:40]
     * Some methods are `restricted` - only meant to be overridden by the runtime.
     * We neither permit nor support any other mod tampering with them.
     *
     * Since the runtime (and only the runtime) has IVT into the core,
     * we provide for this by marking such methods' delegates `internal`.
     *
     * Attempting to actively prohibit method handles being constructed
     * over `restricted` methods is pointless; the user always has a means to
     * circumvent it. We simply refuse to support any such scenario.
     */

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_FUN_0048E750_0034C250(int param_1);
    public static FhMethodHandle<d_FUN_0048E750_0034C250> FUN_0048E750_0034C250 =>
        new( new FhMethodLocation(0x48E750, 0x34C250) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ushort d_AtelGetSaveDic();
    public static FhMethodHandle<d_AtelGetSaveDic> AtelGetSaveDic =>
        new( new FhMethodLocation(0x46C410, 0x326B00) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_MsSetSavePartyMember(uint param_1, uint param_2, uint param_3);
    public static FhMethodHandle<d_MsSetSavePartyMember> MsSetSavePartyMember =>
        new( new FhMethodLocation(0x386950, 0x20EAF0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_FUN_0048E730_0034C230(int param_1);
    public static FhMethodHandle<d_FUN_0048E730_0034C230> FUN_0048E730_0034C230 =>
        new( new FhMethodLocation(0x48E730, 0x34C230) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate uint d_MsGetSaveItemNum(uint param_1);
    public static FhMethodHandle<d_MsGetSaveItemNum> MsGetSaveItemNum =>
        new( new FhMethodLocation(0x3904B0, 0x220BA0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte d_AtelPushMember();
    public static FhMethodHandle<d_AtelPushMember> AtelPushMember =>
        new( new FhMethodLocation(0x46E310, 0x328D40) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte d_AtelPopMember();
    public static FhMethodHandle<d_AtelPopMember> AtelPopMember =>
        new( new FhMethodLocation(0x46DDB0, 0x328780) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_AtelJumpGameOver();
    public static FhMethodHandle<d_AtelJumpGameOver> AtelJumpGameOver
        => new( new FhMethodLocation(0x46DA10, 0x328340) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate uint d_MsBattleCheck();
    public static FhMethodHandle<d_MsBattleCheck> MsBattleCheck
        => new( new FhMethodLocation(0x380C90, 0x207230) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_graphicDestroyFmv();
    public static FhMethodHandle<d_graphicDestroyFmv> graphicDestroyFmv
        => new( new FhMethodLocation(0x23DF30, 0x4D170) );

    // RT - File cross-loader

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_FUN_00607E00_00890EC0(byte* ptr_path, byte* arg2, byte* arg3);
    public static FhMethodHandle<d_FUN_00607E00_00890EC0> FUN_00607E00_00890EC0 =>
        new( new FhMethodLocation(0x207E00, 0x490EC0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_FUN_00607D50_00890FA0(byte* ptr_path);
    public static FhMethodHandle<d_FUN_00607D50_00890FA0> FUN_00607D50_00890FA0 =>
        new( new FhMethodLocation(0x207D50, 0x490FA0) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    internal unsafe delegate PCluster* d_ClusterManager_getPClusterByName(uint ptr_this, byte* ptr_name);
    internal static FhMethodHandle<d_ClusterManager_getPClusterByName> ClusterManager_getPClusterByName =>
        new( new FhMethodLocation(0x29B4B0, 0x09E360) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate BigFileStream* d_BigFileStream_get();
    internal static FhMethodHandle<d_BigFileStream_get> BigFileStream_get =>
        new( new FhMethodLocation(0x21BDB0, 0x5428A0) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    internal unsafe delegate void d_BigFileStream_ctor(BigFileStream* ptr_this);
    internal static FhMethodHandle<d_BigFileStream_ctor> BigFileStream_ctor =>
        new( new FhMethodLocation(0x21BDD0, 0x5428C0) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    internal unsafe delegate void d_BigFileStream_setStreamPrefix(BigFileStream* ptr_this, byte* ptr_stream_prefix);
    internal static FhMethodHandle<d_BigFileStream_setStreamPrefix> BigFileStream_setStreamPrefix =>
        new( new FhMethodLocation(0x21C3A0, 0x542E90) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    internal unsafe delegate int d_BigFileStream_registerBigFile(BigFileStream* ptr_this, byte* ptr_vbf_name);
    internal static FhMethodHandle<d_BigFileStream_registerBigFile> BigFileStream_registerBigFile =>
        new( new FhMethodLocation(0x21C150, 0x542C40) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate byte* d_Phyre_PSerialization_PStreamFile_GetStreamPrefix();
    internal static FhMethodHandle<d_Phyre_PSerialization_PStreamFile_GetStreamPrefix> Phyre_PSerialization_PStreamFile_GetStreamPrefix =>
        new( new FhMethodLocation(0x207D30, 0x490EB0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void d_Phyre_PSerialization_PStreamFile_SetStreamPrefix(byte* ptr_stream_prefix);
    internal static FhMethodHandle<d_Phyre_PSerialization_PStreamFile_SetStreamPrefix> Phyre_PSerialization_PStreamFile_SetStreamPrefix =>
        new( new FhMethodLocation(0x207D40, 0x490F90) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    internal unsafe delegate VFile* d_BigFileStream_openFile(BigFileStream* ptr_this, byte* ptr_file_name);
    internal static FhMethodHandle<d_BigFileStream_openFile> BigFileStream_openFile =>
        new( new FhMethodLocation(0x21BF10, 0x542A00) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void d_fiosUnifyFilename(byte* src, byte* dest, int size);
    internal static FhMethodHandle<d_fiosUnifyFilename> fiosUnifyFilename =>
        new( new FhMethodLocation(0x279820, 0x94F10) );

    // RT - Allocator fix

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void* d__VirtualAlloc_Commit_RW(void* ptr, uint size);
    internal static FhMethodHandle<d__VirtualAlloc_Commit_RW> _VirtualAlloc_Commit_RW =>
       new( new FhMethodLocation(0x5438B0, 0x4781C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate bool d__VirtualFree_Decommit(void* ptr, uint size);
    internal static FhMethodHandle<d__VirtualFree_Decommit> _VirtualFree_Decommit =>
        new( new FhMethodLocation(0x5438E0, 0x4781F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void* d__VirtualAlloc_Reserve_NA(uint size);
    internal static FhMethodHandle<d__VirtualAlloc_Reserve_NA> _VirtualAlloc_Reserve_NA =>
        new( new FhMethodLocation(0x5439A0, 0x4782B0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void* d__VirtualAlloc_ReserveCommit_TopDown_RW(uint size);
    internal static FhMethodHandle<d__VirtualAlloc_ReserveCommit_TopDown_RW> _VirtualAlloc_ReserveCommit_TopDown_RW =>
        new( new FhMethodLocation(0x2EBBC0, 0x1133C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void d__malloc_pool_init();
    internal static FhMethodHandle<d__malloc_pool_init> _malloc_pool_init =>
        new( new FhMethodLocation(0x2FB9B0, 0x122040) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void d_FUN_005428A0_008771A0();
    internal static FhMethodHandle<d_FUN_005428A0_008771A0> FUN_005428A0_008771A0 =>
        new( new FhMethodLocation(0x5428A0, 0x4771A0) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    internal delegate __ALLOC_DATA* d_FUN_00542A40_00477340(__ALLOC_DATA* ptr_this, uint size, uint p2, uint p3);
    internal static FhMethodHandle<d_FUN_00542A40_00477340> FUN_00542A40_00477340 =>
        new( new FhMethodLocation(0x542A40, 0x477340) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    internal delegate void* d_FUN_00542B60_00477460(__ALLOC_DATA* ptr_this, uint arg2);
    internal static FhMethodHandle<d_FUN_00542B60_00477460> FUN_00542B60_00477460 =>
        new( new FhMethodLocation(0x542B60, 0x477460) );

    // Frame limiter

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public unsafe delegate VFXDynamicGeometry* d_ClassVFXRenderDataTable_GetDynamicGeometryByInstance(ClassVFXRenderDataTable* ptr_this, uint param_1, uint param_2);
    public static FhMethodHandle<d_ClassVFXRenderDataTable_GetDynamicGeometryByInstance> ClassVFXRenderDataTable_GetDynamicGeometryByInstance
        => new( new FhMethodLocation(0x29F760, 0x0B3050) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_TkSetFadeOut(uint frame_count);
    public static FhMethodHandle<d_TkSetFadeOut> TkSetFadeOut
        => new( new FhMethodLocation(0x48EAC0, 0x34C780) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_PhyreScene_updateTextureVideoCallback(uint param_1);
    public static FhMethodHandle<d_PhyreScene_updateTextureVideoCallback> PhyreScene_updateTextureVideoCallback
        => new( new FhMethodLocation(0x272210, 0x085F70) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsEffectProcess(uint param_1);
    public static FhMethodHandle<d_MsEffectProcess> MsEffectProcess
        => new( new FhMethodLocation(0x387EC0, 0x216680) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate uint* d_Sg_GetDBuffDC(uint* out_sg_count);
    public static FhMethodHandle<d_Sg_GetDBuffDC> Sg_GetDBuffDC
        => new( new FhMethodLocation(0x420640, 0x204B00) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_yiCallFieldParticle();
    public static FhMethodHandle<d_yiCallFieldParticle> yiCallFieldParticle
        => new( new FhMethodLocation(0x5083E0, 0x3B9220) );

    // Unofficial name
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_rcefTaskRetSeqCont_Inner(uint* ptr_task);
    public static FhMethodHandle<d_rcefTaskRetSeqCont_Inner> rcefTaskRetSeqCont_Inner
        => new( new FhMethodLocation(0x52EDE0, 0x3E92F0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_rcefTaskRetSeqCont(uint* ptr_task);
    public static FhMethodHandle<d_rcefTaskRetSeqCont> rcefTaskRetSeqCont
        => new( new FhMethodLocation(0x52EE00, 0x3E9310) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_enableGameControlTextureAnimation(uint enable);
    public static FhMethodHandle<d_enableGameControlTextureAnimation> enableGameControlTextureAnimation
        => new( new FhMethodLocation(0x436790, 0x2E56A0) );

    // Unofficial name
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_Sg_Fade_Common(ushort frame_count, uint mode_in, uint mode_w);
    public static FhMethodHandle<d_Sg_Fade_Common> Sg_Fade_Common
        => new( new FhMethodLocation(0x42CE40, 0x2D4980) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_Sg_Flash(ushort frame_count, byte arg2, byte arg3, byte arg4);
    public static FhMethodHandle<d_Sg_Flash> Sg_Flash
        => new( new FhMethodLocation(0x42CD20, 0x2D4810) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public delegate void d_PhyFMVPlayerManager_UpdateTexture(uint ptr_this);
    public static FhMethodHandle<d_PhyFMVPlayerManager_UpdateTexture> PhyFMVPlayerManager_UpdateTexture
        => new( new FhMethodLocation(0x2D77B0, 0x035600) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public unsafe delegate ulong d_Phyre_PVideo_PVideoPlaybackWin32_getCurrentTime(uint* ptr_this);
    public static FhMethodHandle<d_Phyre_PVideo_PVideoPlaybackWin32_getCurrentTime> Phyre_PVideo_PVideoPlaybackWin32_getCurrentTime
        => new( new FhMethodLocation(0x627BD0, 0x50F740) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public unsafe delegate ulong d_Phyre_PVideo_PVideoPlaybackWin32_getEndTime(uint* ptr_this);
    public static FhMethodHandle<d_Phyre_PVideo_PVideoPlaybackWin32_getEndTime> Phyre_PVideo_PVideoPlaybackWin32_getEndTime
        => new( new FhMethodLocation(0x627C40, 0x50F7C0) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public unsafe delegate void d_PhyreScene_UpdateTextureVideo(uint* ptr_this);
    public static FhMethodHandle<d_PhyreScene_UpdateTextureVideo> PhyreScene_UpdateTextureVideo
        => new( new FhMethodLocation(0x254B10, 0x064C00) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_graphicTextureVideoPlay(uint arg1);
    public static FhMethodHandle<d_graphicTextureVideoPlay> graphicTextureVideoPlay
        => new( new FhMethodLocation(0x244430, 0x055460) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_graphicTextureVideoUpdate();
    public static FhMethodHandle<d_graphicTextureVideoUpdate> graphicTextureVideoUpdate
        => new( new FhMethodLocation(0x244470, 0x055490) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_pppFpStopStatus(uint arg1);
    public static FhMethodHandle<d_pppFpStopStatus> pppFpStopStatus
        => new( new FhMethodLocation(0x32A840, 0x411010) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate sbyte d_Sg_GetKeepFps();
    public static FhMethodHandle<d_Sg_GetKeepFps> Sg_GetKeepFps
        => new( new FhMethodLocation(0x4206B0, 0x204B70) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate sbyte d_Sg_SetKeepFps(sbyte arg1);
    public static FhMethodHandle<d_Sg_SetKeepFps> Sg_SetKeepFps
        => new( new FhMethodLocation(0x421C00, 0x2065A0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_Phyre_PFramework_PWindowWin32Base_SetFlipVSyncInterval(uint param_1);
    public static FhMethodHandle<d_Phyre_PFramework_PWindowWin32Base_SetFlipVSyncInterval> Phyre_PFramework_PWindowWin32Base_SetFlipVSyncInterval
        => new( new FhMethodLocation(0x225250, 0x6B4B00) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    public unsafe delegate uint d_Phyre_PFramework_PApplication_frame(PApplication* ptr_this);
    public static FhMethodHandle<d_Phyre_PFramework_PApplication_frame> Phyre_PFramework_PApplication_frame
        => new( new FhMethodLocation(0x227AF0, 0x6B7390) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint d_rnd();
    public static FhMethodHandle<d_rnd> rnd
        => new( new FhMethodLocation(0x3989B0, 0x21E360) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_00821F90_00606930(float delta);
    public static FhMethodHandle<d_FUN_00821F90_00606930> FUN_00821F90_00606930
        => new( new FhMethodLocation(0x421F90, 0x206930) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte d_graphicIsVideoPlaying();
    public static FhMethodHandle<d_graphicIsVideoPlaying> graphicIsVideoPlaying
        => new( new FhMethodLocation(0x241EA0, 0x052CD0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsCameraMoveFrame(uint camera_id, uint arg2, uint arg3, uint frame_count, uint arg5);
    public static FhMethodHandle<d_MsCameraMoveFrame> MsCameraMoveFrame
        => new( new FhMethodLocation(0x3BDDD0, 0x251D20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_MsCameraMoveAcc(uint camera_id, uint mode_non_ref, uint mode_polar, uint arg4, uint arg5, uint arg6, uint arg7);
    public static FhMethodHandle<d_MsCameraMoveAcc> MsCameraMoveAcc
        => new( new FhMethodLocation(0x3BD7E0, 0x251720) );

    // RT - Input tracking

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void d_AtelExec_Internal_871D10();
    internal static FhMethodHandle<d_AtelExec_Internal_871D10> AtelExec_Internal_871D10 =>
        new( new FhMethodLocation(0x471D10, 0x32CE90) );

    // RT - Platform bind

    /* [fkelava 25/4/24 17:51]
     * https://github.com/terrafx/terrafx.interop.windows/blob/55590efae0f77f4c8db465a80d18b4f5b679696c/sources/Interop/Windows/DirectX/um/d3d11/DirectX.cs#L25
     */
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate HRESULT d_D3D11_D3D11CreateDeviceAndSwapChain(
        IDXGIAdapter*         pAdapter,
        D3D_DRIVER_TYPE       DriverType,
        HMODULE               Software,
        uint                  Flags,
        D3D_FEATURE_LEVEL*    pFeatureLevels,
        uint                  FeatureLevels,
        uint                  SDKVersion,
        DXGI_SWAP_CHAIN_DESC* pSwapChainDesc,
        IDXGISwapChain**      ppSwapChain,
        ID3D11Device**        ppDevice,
        D3D_FEATURE_LEVEL*    pFeatureLevel,
        ID3D11DeviceContext** ppImmediateContext);
    internal static FhMethodHandle<d_D3D11_D3D11CreateDeviceAndSwapChain> D3D11_D3D11CreateDeviceAndSwapChain =>
        new( new FhMethodLocation("D3D11.dll", "D3D11CreateDeviceAndSwapChain") );

    // RT - Game UI

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void d_TODrawMessageWindow();
    public static FhMethodHandle<d_TODrawMessageWindow> TODrawMessageWindow
        => new( new FhMethodLocation(0x4ABCE0, 0x391D00) );

    // RT - ImGui

    /* [fkelava 6/10/24 01:54]
     * See src/core/native/Windows.Win32.IDXGISwapChain.g.cs for swapchain method signatures.
     */
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate nint d_DXGI_IDXGISwapChain_Present(
        IDXGISwapChain* pSwapChain,
        uint            SyncInterval,
        DXGI_PRESENT    Flags);
    internal static FhMethodHandle<d_DXGI_IDXGISwapChain_Present> DXGI_IDXGISwapChain_Present
        (IDXGISwapChain* ptr_swapchain)
        => new( new FhMethodLocation(ptr_swapchain->lpVtbl[8]) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate nint d_DXGI_IDXGISwapChain_ResizeBuffers(
        IDXGISwapChain* pSwapChain,
        uint            BufferCount,
        uint            Width,
        uint            Height,
        DXGI_FORMAT     NewFormat,
        uint            SwapChainFlags);
    internal static FhMethodHandle<d_DXGI_IDXGISwapChain_ResizeBuffers> DXGI_IDXGISwapChain_ResizeBuffers
        (IDXGISwapChain* ptr_swapchain)
        => new( new FhMethodLocation(ptr_swapchain->lpVtbl[13]) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate int d_Phyre_PFramework_PInput_Update();
    internal static FhMethodHandle<d_Phyre_PFramework_PInput_Update> Phyre_PFramework_PInput_Update
        => new( new FhMethodLocation(0x225770, 0x6B50A0) );

    // RT - Game loop

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void d_Sg_MainLoop(float delta);
    internal static FhMethodHandle<d_Sg_MainLoop> Sg_MainLoop
        => new( new FhMethodLocation(0x420AE0, 0x205130) );

    // RT - EFL

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    internal delegate PStreamFile* d_Phyre_PSerialization_PStreamFile_ctor(
        PStreamFile* ptr_this,
        byte*        ptr_path,
        bool         read_only,
        uint         p3,  // unused
        uint         p4,  // unused
        bool         p5); // unused
    internal static FhMethodHandle<d_Phyre_PSerialization_PStreamFile_ctor> Phyre_PSerialization_PStreamFile_ctor
        => new( new FhMethodLocation(0x207BC0, 0x490D40) );

    // RT - VBF loader

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    internal delegate bool d_Phyre_PSerialization_PStreamFileWin32_openFile(
        PStreamFile* ptr_this,
        byte*        ptr_path,
        bool         read_only,
        uint         p3,  // unused
        uint         p4,  // unused
        bool         p5); // unused
    internal static FhMethodHandle<d_Phyre_PSerialization_PStreamFileWin32_openFile> Phyre_PSerialization_PStreamFileWin32_openFile
        => new( new FhMethodLocation(0x207F40, 0x4911A0) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    internal delegate uint d_Phyre_PSerialization_PStreamFile_getFileSize(PStreamFile* ptr_this);
    internal static FhMethodHandle<d_Phyre_PSerialization_PStreamFile_getFileSize> Phyre_PSerialization_PStreamFile_getFileSize
        => new( new FhMethodLocation(0x207DC0, 0x491010) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    internal delegate uint d_Phyre_PSerialization_PStreamFile_read(PStreamFile* ptr_this, void* buffer, uint size);
    internal static FhMethodHandle<d_Phyre_PSerialization_PStreamFile_read> Phyre_PSerialization_PStreamFile_read
        => new( new FhMethodLocation(0x208090, 0x4912F0) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    internal delegate uint d_Phyre_PSerialization_PStreamFile_closeFile(PStreamFile* ptr_this);
    internal static FhMethodHandle<d_Phyre_PSerialization_PStreamFile_closeFile> Phyre_PSerialization_PStreamFile_closeFile
        => new( new FhMethodLocation(0x207D80, 0x490FD0) );

    // RT - Phyre loader

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    internal delegate PCluster* d_ClusterManager_loadPCluster(uint ptr_this, byte* ptr_file_name);
    internal static FhMethodHandle<d_ClusterManager_loadPCluster> ClusterManager_loadPCluster
        => new( new FhMethodLocation(0x29B940, 0x9E900) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int d_PApplication_FixupClusters(PCluster** ptr_clusters, int nb_clusters);
    internal static FhMethodHandle<d_PApplication_FixupClusters> PApplication_FixupClusters
        => new( new FhMethodLocation(0x223580, 0x6B2EE0) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    internal delegate void d_ClusterManager_releasePCluster(uint ptr_this, PCluster* ptr_cluster);
    internal static FhMethodHandle<d_ClusterManager_releasePCluster> ClusterManager_releasePCluster
        => new( new FhMethodLocation(0x29BDB0, 0x9ED80) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate PNamespace* d_Phyre_PNamespace_GetGlobalNamespace();
    internal static FhMethodHandle<d_Phyre_PNamespace_GetGlobalNamespace> Phyre_PNamespace_GetGlobalNamespace
        => new( new FhMethodLocation(0x3E2F0, 0x53D0A0) );

    // RT - CD

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate nint d_CDfileSize_PC(int arg1);
    internal static FhMethodHandle<d_CDfileSize_PC> CDfileSize_PC
        => new( new FhMethodLocation(0x6428A0, 0x74E840) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate nint d_check_ex_file_size(int arg1, int arg2);
    internal static FhMethodHandle<d_check_ex_file_size> check_ex_file_size
        => new( new FhMethodLocation(0x36D660, 0x1397D0) );

    // Save PAL
    // RT - Save impl

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void d_SaveDataManager_debugSave_Internal_6F0650(int size, byte* ptr);
    internal static FhMethodHandle<d_SaveDataManager_debugSave_Internal_6F0650> SaveDataManager_debugSave_Internal_6F0650
        => new( new FhMethodLocation(0x2F0510, 0x11D5C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void d_SaveDataToSave();
    internal static FhMethodHandle<d_SaveDataToSave> SaveDataToSave
        => new( new FhMethodLocation(0x2487A0, 0x88540) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void d_SaveDataToLoad();
    internal static FhMethodHandle<d_SaveDataToLoad> SaveDataToLoad
        => new( new FhMethodLocation(0x248760, 0x88510) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void d_TkRefreshHdd();
    internal static FhMethodHandle<d_TkRefreshHdd> TkRefreshHdd
        => new( new FhMethodLocation(0x4724A0, 0x32D950) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int d_fix_mappic(ushort arg1);
    internal static FhMethodHandle<d_fix_mappic> fix_mappic
        => new( new FhMethodLocation(0x2EF6F0, 0x11CA70) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int d_isNeedShowJapanLogo();
    internal static FhMethodHandle<d_isNeedShowJapanLogo> isNeedShowJapanLogo
        => new( new FhMethodLocation(0x387390, 0x20F4D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte* d_AtelGetSaveDicName(ushort arg1, uint arg2);
    public static FhMethodHandle<d_AtelGetSaveDicName> AtelGetSaveDicName
        => new( new FhMethodLocation(0x46C430, 0x326B20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void d_SaveDataGetLoc(int arg1, byte* arg2);
    internal static FhMethodHandle<d_SaveDataGetLoc> SaveDataGetLoc
        => new( new FhMethodLocation(0x2412D0, 0x87D20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate nint d_SaveDataWriteCrc(byte* arg1);
    internal static FhMethodHandle<d_SaveDataWriteCrc> SaveDataWriteCrc
        => new( new FhMethodLocation(0x248F20, 0x88A30) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int d_SaveDataCheckCrc();
    internal static FhMethodHandle<d_SaveDataCheckCrc> SaveDataCheckCrc
        => new( new FhMethodLocation(0x247D70, 0x87B80) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void d__SetUpDefaultSaveFolder();
    internal static FhMethodHandle<d__SetUpDefaultSaveFolder> _SetUpDefaultSaveFolder
        => new( new FhMethodLocation(0x2F0330, 0x11D3C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate bool d_isNeedRenamePlayer(byte arg1);
    internal static FhMethodHandle<d_isNeedRenamePlayer> isNeedRenamePlayer
        => new( new FhMethodLocation(0x387370, 0x20F4B0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void d_SaveDataSaveLoadSucceed(FhSaveSystemState arg1);
    internal static FhMethodHandle<d_SaveDataSaveLoadSucceed> SaveDataSaveLoadSucceed
        => new( new FhMethodLocation(0x248540, 0x88300) );

    // INTERNAL/RESTRICTED - END

    // PUBLIC/UNRESTRICTED - BEGIN

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsCheckRange(int value, int min, int max);
    public static FhMethodHandle<d_MsCheckRange> MsCheckRange
        => new( new FhMethodLocation(0x39A0D0, 0x224CD0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_brnd(int rng_idx);
    public static FhMethodHandle<d_brnd> brnd
        => new( new FhMethodLocation(0x3988F0, 0x21E270) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_CT_Init(AtelBasicWorker* work, int* storage, AtelStack* stack);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_CT_Exec(AtelBasicWorker* work, int* storage);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_CT_RetI(AtelBasicWorker* work, int* storage, AtelStack* stack);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate float d_CT_RetF(AtelBasicWorker* work, int* storage, AtelStack* stack);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_SndSepPlay(uint sound_id, uint pan, uint volume);
    public static FhMethodHandle<d_SndSepPlay> SndSepPlay
        => new( new FhMethodLocation(0x486E50, 0x3446D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_SndSepPlaySimple(uint sound_id);
    public static FhMethodHandle<d_SndSepPlaySimple> SndSepPlaySimple
        => new( new FhMethodLocation(0x486DE0, 0x344760) );

    // `printf` and similar methods for use by the debug mod

    /* [fkelava 17/7/25 02:33]
     * For vararg functions the delegate signature should have an argument count >=
     * the argument count of the invocation with the most varargs in the executable.
     *
     * For now we assume sixteen. If you crash with a buffer/stack overrun, increase it.
     */

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d__Printf(string fmt,
        nint va0,  nint va1,  nint va2,  nint va3,
        nint va4,  nint va5,  nint va6,  nint va7,
        nint va8,  nint va9,  nint va10, nint va11,
        nint va12, nint va13, nint va14, nint va15);

    public static FhMethodHandle<d__Printf> dbgPrintf =>
        new( new FhMethodLocation(0x22F6B0, 0x9ADD0) );
    public static FhMethodHandle<d__Printf> scePrintf =>
        new( new FhMethodLocation(0x22FDA0, 0x9B4B0) );
    public static FhMethodHandle<d__Printf> AtelPs2DebugString =>
        new( new FhMethodLocation(0x473C10, 0x30E9E0) );
    public static FhMethodHandle<d__Printf> AtelPs2DebugString2 =>
        new( new FhMethodLocation(0x473C20, 0x30E9F0) );
    public static FhMethodHandle<d__Printf> rcPrint =>
        new( new FhMethodLocation(0x527550, 0x3D9690) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_Phyre_PhyrePrintf(int rc, string fmt,
        nint va0,  nint va1,  nint va2,  nint va3,
        nint va4,  nint va5,  nint va6,  nint va7,
        nint va8,  nint va9,  nint va10, nint va11,
        nint va12, nint va13, nint va14, nint va15);

    public static FhMethodHandle<d_Phyre_PhyrePrintf> Phyre_PhyrePrintf =>
        new ( new FhMethodLocation(0x0353F0, 0x48CC60) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_FUN_0046A130_00324820();
    public static FhMethodHandle<d_FUN_0046A130_00324820> FUN_0046A130_00324820 =>
        new( new FhMethodLocation(0x46A130, 0x324820) );

    // PUBLIC/UNRESTRICTED - END

}
