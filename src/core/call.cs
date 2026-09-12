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
    public unsafe delegate int d_FUN_0088E6C0_0074C2B0(int param_1);
    public static FhMethodHandle<d_FUN_0088E6C0_0074C2B0> FUN_0088E6C0_0074C2B0 =>
        new( new FhMethodLocation(0x48E6C0, 0x34C2B0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ushort d_AtelGetSaveDic();
    public static FhMethodHandle<d_AtelGetSaveDic> AtelGetSaveDic =>
        new( new FhMethodLocation(0x46C3A0, 0x326B60) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_MsSetSavePartyMember(uint param_1, uint param_2, uint param_3);
    public static FhMethodHandle<d_MsSetSavePartyMember> MsSetSavePartyMember =>
        new( new FhMethodLocation(0x386A10, 0x20EB20) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_FUN_0088E6A0_0074C290(int param_1);
    public static FhMethodHandle<d_FUN_0088E6A0_0074C290> FUN_0088E6A0_0074C290 =>
        new( new FhMethodLocation(0x48E6A0, 0x34C290) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate uint d_MsGetSaveItemNum(uint param_1);
    public static FhMethodHandle<d_MsGetSaveItemNum> MsGetSaveItemNum =>
        new( new FhMethodLocation(0x390500, 0x220BC0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte d_AtelPushMember();
    public static FhMethodHandle<d_AtelPushMember> AtelPushMember =>
        new( new FhMethodLocation(0x46E2A0, 0x328DA0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte d_AtelPopMember();
    public static FhMethodHandle<d_AtelPopMember> AtelPopMember =>
        new( new FhMethodLocation(0x46DD40, 0x3287E0) );
  
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_AtelJumpGameOver();
    public static FhMethodHandle<d_AtelJumpGameOver> AtelJumpGameOver 
        => new( new FhMethodLocation(0x46D9A0, 0x3283A0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate uint d_MsBattleCheck();
    public static FhMethodHandle<d_MsBattleCheck> MsBattleCheck
        => new( new FhMethodLocation(0x380D60, 0x207260) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_graphicDestroyFmv();
    public static FhMethodHandle<d_graphicDestroyFmv> graphicDestroyFmv
        => new( new FhMethodLocation(0x23E0E0, 0x04D170) );

    // RT - File cross-loader

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int d_FUN_00607E00_00890EC0(byte* ptr_path, byte* arg2, byte* arg3);
    public static FhMethodHandle<d_FUN_00607E00_00890EC0> FUN_00607E00_00890EC0 =>
        new( new FhMethodLocation(0x207E00, 0x490EC0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void d_FUN_00607F10_008910A0(byte* ptr_path);
    public static FhMethodHandle<d_FUN_00607F10_008910A0> FUN_00607F10_008910A0 =>
        new( new FhMethodLocation(0x207F10, 0x4910A0) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    internal unsafe delegate PCluster* d_ClusterManager_getPClusterByName(uint ptr_this, byte* ptr_name);
    internal static FhMethodHandle<d_ClusterManager_getPClusterByName> ClusterManager_getPClusterByName => 
        new( new FhMethodLocation(0x29B5F0, 0x09E2E0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate BigFileStream* d_BigFileStream_get();
    internal static FhMethodHandle<d_BigFileStream_get> BigFileStream_get => 
        new( new FhMethodLocation(0x21BF70, 0x542A40) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    internal unsafe delegate void d_BigFileStream_ctor(BigFileStream* ptr_this);
    internal static FhMethodHandle<d_BigFileStream_ctor> BigFileStream_ctor =>
        new( new FhMethodLocation(0x21BF90, 0x542A60) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    internal unsafe delegate void d_BigFileStream_setStreamPrefix(BigFileStream* ptr_this, byte* ptr_stream_prefix);
    internal static FhMethodHandle<d_BigFileStream_setStreamPrefix> BigFileStream_setStreamPrefix => 
        new( new FhMethodLocation(0x21C560, 0x543030) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    internal unsafe delegate int d_BigFileStream_registerBigFile(BigFileStream* ptr_this, byte* ptr_vbf_name);
    internal static FhMethodHandle<d_BigFileStream_registerBigFile> BigFileStream_registerBigFile =>
        new( new FhMethodLocation(0x21C310, 0x542DE0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate byte* d_Phyre_PSerialization_PStreamFile_GetStreamPrefix();
    internal static FhMethodHandle<d_Phyre_PSerialization_PStreamFile_GetStreamPrefix> Phyre_PSerialization_PStreamFile_GetStreamPrefix => 
        new( new FhMethodLocation(0x207EF0, 0x490FB0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void d_Phyre_PSerialization_PStreamFile_SetStreamPrefix(byte* ptr_stream_prefix);
    internal static FhMethodHandle<d_Phyre_PSerialization_PStreamFile_SetStreamPrefix> Phyre_PSerialization_PStreamFile_SetStreamPrefix => 
        new( new FhMethodLocation(0x207F00, 0x491090) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    internal unsafe delegate VFile* d_BigFileStream_openFile(BigFileStream* ptr_this, byte* ptr_file_name);
    internal static FhMethodHandle<d_BigFileStream_openFile> BigFileStream_openFile => 
        new( new FhMethodLocation(0x21C0D0, 0x542BA0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void d_fiosUnifyFilename(byte* src, byte* dest, int size);
    internal static FhMethodHandle<d_fiosUnifyFilename> fiosUnifyFilename => 
        new( new FhMethodLocation(0x2799D0, 0x094E90) );

    // RT - Allocator fix

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void* d__VirtualAlloc_Commit_RW(void* ptr, uint size);
    internal static FhMethodHandle<d__VirtualAlloc_Commit_RW> _VirtualAlloc_Commit_RW =>
       new( new FhMethodLocation(0x5438B0, 0x4782B0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate bool d__VirtualFree_Decommit(void* ptr, uint size);
    internal static FhMethodHandle<d__VirtualFree_Decommit> _VirtualFree_Decommit =>
        new( new FhMethodLocation(0x5438E0, 0x4782E0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void* d__VirtualAlloc_Reserve_NA(uint size);
    internal static FhMethodHandle<d__VirtualAlloc_Reserve_NA> _VirtualAlloc_Reserve_NA =>
        new( new FhMethodLocation(0x5439A0, 0x4783A0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void* d__VirtualAlloc_ReserveCommit_TopDown_RW(uint size);
    internal static FhMethodHandle<d__VirtualAlloc_ReserveCommit_TopDown_RW> _VirtualAlloc_ReserveCommit_TopDown_RW =>
        new( new FhMethodLocation(0x2EBD00, 0x113340) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void d__malloc_pool_init();
    internal static FhMethodHandle<d__malloc_pool_init> _malloc_pool_init =>
        new( new FhMethodLocation(0x2FBA90, 0x121F90) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void d_FUN_009428A0_008772A0();
    internal static FhMethodHandle<d_FUN_009428A0_008772A0> FUN_009428A0_008772A0 =>
        new( new FhMethodLocation(0x5428A0, 0x4772A0) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    internal delegate __ALLOC_DATA* d_FUN_00942A40_00877440(__ALLOC_DATA* ptr_this, uint size, uint p2, uint p3);
    internal static FhMethodHandle<d_FUN_00942A40_00877440> FUN_00942A40_00877440 =>
        new( new FhMethodLocation(0x542A40, 0x477440) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    internal delegate void* d_FUN_00942B60_00877560(__ALLOC_DATA* ptr_this, uint arg2);
    internal static FhMethodHandle<d_FUN_00942B60_00877560> FUN_00942B60_00877560 =>
        new( new FhMethodLocation(0x542B60, 0x477560) );

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
        => new( new FhMethodLocation(0x225930, 0x6B51E0) );

    // RT - Game loop

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void d_Sg_MainLoop(float delta);
    internal static FhMethodHandle<d_Sg_MainLoop> Sg_MainLoop
        => new( new FhMethodLocation(0x420C00, 0x205150) );

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
        => new( new FhMethodLocation(0x207D80, 0x490E40) );

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
        => new( new FhMethodLocation(0x208100, 0x4912A0) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    internal delegate uint d_Phyre_PSerialization_PStreamFile_getFileSize(PStreamFile* ptr_this);
    internal static FhMethodHandle<d_Phyre_PSerialization_PStreamFile_getFileSize> Phyre_PSerialization_PStreamFile_getFileSize
        => new( new FhMethodLocation(0x207F80, 0x491110) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    internal delegate uint d_Phyre_PSerialization_PStreamFile_read(PStreamFile* ptr_this, void* buffer, uint size);
    internal static FhMethodHandle<d_Phyre_PSerialization_PStreamFile_read> Phyre_PSerialization_PStreamFile_read
        => new( new FhMethodLocation(0x208250, 0x4913F0) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    internal delegate uint d_Phyre_PSerialization_PStreamFile_closeFile(PStreamFile* ptr_this);
    internal static FhMethodHandle<d_Phyre_PSerialization_PStreamFile_closeFile> Phyre_PSerialization_PStreamFile_closeFile
        => new( new FhMethodLocation(0x207F40, 0x4910D0) );

    // RT - Phyre loader

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    internal delegate PCluster* d_ClusterManager_loadPCluster(uint ptr_this, byte* ptr_file_name);
    internal static FhMethodHandle<d_ClusterManager_loadPCluster> ClusterManager_loadPCluster
        => new( new FhMethodLocation(0x29BA80, 0x9E880) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int d_PApplication_FixupClusters(PCluster** ptr_clusters, int nb_clusters);
    internal static FhMethodHandle<d_PApplication_FixupClusters> PApplication_FixupClusters
        => new( new FhMethodLocation(0x223740, 0x6B3020) );

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    internal delegate void d_ClusterManager_releasePCluster(uint ptr_this, PCluster* ptr_cluster);
    internal static FhMethodHandle<d_ClusterManager_releasePCluster> ClusterManager_releasePCluster
        => new( new FhMethodLocation(0x29BEF0, 0x9ED00) );

    // RT - CD

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate nint d_CDfileSize_PC(int arg1);
    internal static FhMethodHandle<d_CDfileSize_PC> CDfileSize_PC
        => new( new FhMethodLocation(0x6428A0, 0x74E9A0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate nint d_check_ex_file_size(int arg1, int arg2);
    internal static FhMethodHandle<d_check_ex_file_size> check_ex_file_size
        => new( new FhMethodLocation(0x36D770, 0x1396A0) );

    // Save PAL
    // RT - Save impl

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void d_SaveDataManager_debugSave_Internal_6F0650(int size, byte* ptr);
    internal static FhMethodHandle<d_SaveDataManager_debugSave_Internal_6F0650> SaveDataManager_debugSave_Internal_6F0650
        => new( new FhMethodLocation(0x2F0650, 0x11D510) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void d_SaveDataToSave();
    internal static FhMethodHandle<d_SaveDataToSave> SaveDataToSave
        => new( new FhMethodLocation(0x248950, 0x884D0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void d_SaveDataToLoad();
    internal static FhMethodHandle<d_SaveDataToLoad> SaveDataToLoad
        => new( new FhMethodLocation(0x248910, 0x884A0) );

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void d_TkMenuJumpToLoadedScene();
    internal static FhMethodHandle<d_TkMenuJumpToLoadedScene> TkMenuJumpToLoadedScene
        => new( new FhMethodLocation(0x4B4E70, 0x36AD50 ) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int d_fix_mappic(ushort arg1);
    internal static FhMethodHandle<d_fix_mappic> fix_mappic
        => new( new FhMethodLocation(0x2EF830, 0x11C9B0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int d_isNeedShowJapanLogo();
    internal static FhMethodHandle<d_isNeedShowJapanLogo> isNeedShowJapanLogo
        => new( new FhMethodLocation(0x387450, 0x20F500) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte* d_AtelGetSaveDicName(ushort arg1, uint arg2);
    public static FhMethodHandle<d_AtelGetSaveDicName> AtelGetSaveDicName
        => new( new FhMethodLocation(0x46C3C0, 0x326B80) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void d_SaveDataGetLoc(int arg1, byte* arg2);
    internal static FhMethodHandle<d_SaveDataGetLoc> SaveDataGetLoc
        => new( new FhMethodLocation(0x2480E0, 0x87CB0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate nint d_SaveDataWriteCrc(byte* arg1);
    internal static FhMethodHandle<d_SaveDataWriteCrc> SaveDataWriteCrc
        => new( new FhMethodLocation(0x2490D0, 0x889C0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int d_SaveDataCheckCrc();
    internal static FhMethodHandle<d_SaveDataCheckCrc> SaveDataCheckCrc
        => new( new FhMethodLocation(0x247F20, 0x87B10) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void d__SetUpDefaultSaveFolder();
    internal static FhMethodHandle<d__SetUpDefaultSaveFolder> _SetUpDefaultSaveFolder
        => new( new FhMethodLocation(0x2F0470, 0x11D310) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate bool d_isNeedRenamePlayer(byte arg1);
    internal static FhMethodHandle<d_isNeedRenamePlayer> isNeedRenamePlayer
        => new( new FhMethodLocation(0x387430, 0x20F4E0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void d_SaveDataSaveLoadSucceed(FhSaveSystemState arg1);
    internal static FhMethodHandle<d_SaveDataSaveLoadSucceed> SaveDataSaveLoadSucceed
        => new( new FhMethodLocation(0x2486F0, 0x88290) );

    // INTERNAL/RESTRICTED - END

    // PUBLIC/UNRESTRICTED - BEGIN

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_MsCheckRange(int value, int min, int max);
    public static FhMethodHandle<d_MsCheckRange> MsCheckRange
        => new( new FhMethodLocation(0x39A0D0, 0x224CD0) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_brnd(int rng_idx);
    public static FhMethodHandle<d_brnd> brnd
        => new( new FhMethodLocation(0x398900, 0x21E290) );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void d_CT_Init(AtelBasicWorker* work, int* storage, AtelStack* stack);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_CT_Exec(AtelBasicWorker* work, int* storage);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int d_CT_RetInt(AtelBasicWorker* work, int* storage, AtelStack* stack);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate float d_CT_RetFloat(AtelBasicWorker* work, int* storage, AtelStack* stack);

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

    // PUBLIC/UNRESTRICTED - END

}
