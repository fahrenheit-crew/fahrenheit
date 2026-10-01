// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime;

/* [fkelava 11/02/26 04:19]
 * One of the essential features of any mod framework is the ability to display custom UI
 * to the user. For this, we offer the Dear ImGui toolkit (https://github.com/ocornut/imgui),
 * exposed through the Hexa.NET.ImGui bindings (https://github.com/HexaEngine/Hexa.NET.ImGui).
 *
 * On every frame presentation, a callback is invoked in every module that adds ImGui primitives
 * to the draw list for the frame, and those additions are rendered over the game.
 */

/// <summary>Provides the ability to use the ImGui GUI toolkit within the game.</summary>
/// <remarks>In your module, access this functionality by implementing <see cref="FhModule.render_imgui"/>.</remarks>
[FhLoad(FhGameId.FFX | FhGameId.FFX2 | FhGameId.FFX2LM)]
[SupportedOSPlatform("windows6.1")] // To satisfy CA1416 warning about invoking D3D/DXGI API which TerraFX annotates as supported only on Windows.
public unsafe sealed class FhImguiModule : FhModule, IFhPlatformUser {

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate void ImGui_ImplWin32_SetWindowFocus(
        ImGuiViewportPtr viewport);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate LRESULT WndProc(
        HWND   hWnd,
        uint   msg,
        WPARAM wParam,
        LPARAM lParam);

    // Win32 internals
    private          HWND    _hWnd;
    private          nint    _ptr_o_WndProc;
    private          nint    _ptr_h_WndProc;
    private readonly WndProc _h_WndProc;

    // D3D11 internals
    private IDXGISwapChain*         _ptr_swapchain;  // https://learn.microsoft.com/en-us/windows/win32/api/dxgi/nn-dxgi-idxgiswapchain
    private ID3D11Device*           _ptr_device;     // https://learn.microsoft.com/en-us/windows/win32/api/d3d11/nn-d3d11-id3d11device
    private ID3D11DeviceContext*    _ptr_device_ctx; // https://learn.microsoft.com/en-us/windows/win32/api/d3d11/nn-d3d11-id3d11devicecontext
    private ID3D11RenderTargetView* _ptr_rtv;        // https://learn.microsoft.com/en-us/windows/win32/api/d3d11/nn-d3d11-id3d11rendertargetview

    // Interlocked var for RTV regen on WM_SIZE
    private int _rtv_generated;

    // Signal that resources can be freed because the frame was rendered
    private FhResourceLoaderModule? _rlm;

    public FhImguiModule() {
        _h_WndProc = h_wndproc;
    }

    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        FhModuleHandle<FhResourceLoaderModule> rlm_handle = new(this);

        return rlm_handle.try_get_module(out _rlm)
            && FhCall.Phyre_PFramework_PInput_Update.hook(this, h_pinput);
    }

    void IFhPlatformUser.platform_bind(
        ID3D11Device*        ptr_device,
        ID3D11DeviceContext* ptr_device_context,
        IDXGISwapChain*      ptr_swapchain,
        HWND                 hWnd) {

        _hWnd          = hWnd;
        _ptr_h_WndProc = Marshal.GetFunctionPointerForDelegate(_h_WndProc);
        _ptr_o_WndProc = PInvoke.GetWindowLongW(_hWnd, WINDOW_LONG_PTR_INDEX.GWLP_WNDPROC);
        _              = PInvoke.SetWindowLongW(_hWnd, WINDOW_LONG_PTR_INDEX.GWLP_WNDPROC, (int)_ptr_h_WndProc);

        _ptr_swapchain  = ptr_swapchain;
        _ptr_device     = ptr_device;
        _ptr_device_ctx = ptr_device_context;

        ImGuiContextPtr    ctx   = ImGui.CreateContext();
        ImGuiIOPtr         io    = ImGui.GetIO();
        ImGuiPlatformIOPtr pio   = ImGui.GetPlatformIO();
        ImGuiStylePtr      style = ImGui.GetStyle();

        // Enable controls
        io.ConfigFlags |= ImGuiConfigFlags.NavEnableKeyboard;
        io.ConfigFlags |= ImGuiConfigFlags.NavEnableGamepad;

        // Enable features
        io.ConfigFlags |= ImGuiConfigFlags.DockingEnable;

        io.ConfigDpiScaleFonts     = true;
        io.ConfigDpiScaleViewports = true;

        ImGui.StyleColorsDark();

        HexaID3D11DevicePtr        hexa_p_device     = new((HexaID3D11Device*)       _ptr_device);
        HexaID3D11DeviceContextPtr hexa_p_device_ctx = new((HexaID3D11DeviceContext*)_ptr_device_ctx);

        ImGuiImplWin32.SetCurrentContext(ctx);
        ImGuiImplD3D11.SetCurrentContext(ctx);
        ImGuiImplWin32.Init((nint)_hWnd);
        ImGuiImplD3D11.Init(hexa_p_device, hexa_p_device_ctx);

        /* [fkelava 20/06/26 17:38]
         * We don't want to dump `imgui.ini` in the game's working/install directory.
         *
         * Why go through native allocation instead of just (fixed byte* = "PATH"u8)?
         * Because the spec doesn't actually _mandate_ that u8 literals be emitted in `.data`.
         * Consequently, the resulting pointer to it need not be constant.
         *
         * See https://github.com/dotnet/runtime/discussions/102949#discussioncomment-9628356.
         */

        string     path_ini        = Path.Join(FhEnvironment.Finder.State.FullName, "global", "imgui.ini");
        Span<byte> path_ini_u8     = Encoding.UTF8.GetBytes(path_ini);
        byte*      ptr_path_ini_u8 = (byte*)NativeMemory.AllocZeroed((nuint)path_ini_u8.Length);

        path_ini_u8.CopyTo(new(ptr_path_ini_u8, path_ini_u8.Length));

        io.IniFilename = ptr_path_ini_u8;

        /* [fkelava 16/02/26 15:12]
         * It would be best to use [UnmanagedCallersOnly] and &h_implw32_setwindowfocus,
         * but this is not possible because PlatformSetWindowFocus is typed `void*` and CS8812 results.
         */

        pio.PlatformSetWindowFocus = (void*)Marshal.GetFunctionPointerForDelegate
            <ImGui_ImplWin32_SetWindowFocus>(h_implw32_setwindowfocus);

        FhApi.Gui.init();

        FhCall.DXGI_IDXGISwapChain_Present      (_ptr_swapchain).hook(this, h_present);
        FhCall.DXGI_IDXGISwapChain_ResizeBuffers(_ptr_swapchain).hook(this, h_resize_buffers);
    }

    /* [fkelava 16/02/26 14:59]
     * https://github.com/ocornut/imgui/issues/9242
     *
     * The games have a separate render thread. In other words, the main message
     * pump is on a different thread from the one we call ImGui functions on.
     * (i.e. `render_imgui` and `h_present` occur on the render thread)
     *
     * When setting up the frame in ImGui, SetWindowPos will fire.
     * This may cause a message to be sent, making the render thread wait on the message pump thread.
     * If either is blocked, in a critical section, or otherwise impeded from pumping, both deadlock.
     *
     * As a workaround, we override ImGui to pass SWP_ASYNCWINDOWPOS. This is not a true fix,
     * but suffices to prevent the deadlock in this specific case.
     *
     * See https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowpos,
     * https://learn.microsoft.com/en-us/windows/win32/direct3ddxgi/d3d10-graphics-programming-guide-dxgi#multithread-considerations.
     */

    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    private static void h_implw32_setwindowfocus(ImGuiViewportPtr viewport) {
        if (viewport.PlatformHandleRaw == null)
            return;

        SET_WINDOW_POS_FLAGS flags =
            SET_WINDOW_POS_FLAGS.SWP_ASYNCWINDOWPOS |
            SET_WINDOW_POS_FLAGS.SWP_NOSIZE         |
            SET_WINDOW_POS_FLAGS.SWP_NOMOVE;

        PInvoke.SetWindowPos       ((HWND)viewport.PlatformHandleRaw, HWND.HWND_TOP, 0, 0, 0, 0, flags);
        PInvoke.SetForegroundWindow((HWND)viewport.PlatformHandleRaw);
        PInvoke.SetFocus           ((HWND)viewport.PlatformHandleRaw);
    }

    /// <summary>
    ///     Allows ImGui to intercept window messages sent to the game,
    ///     enabling mouse and keyboard input to be directed to it.
    /// </summary>
    [UnmanagedCallConv( CallConvs = [ typeof(CallConvStdcall) ] )]
    private LRESULT h_wndproc(HWND hWnd, uint msg, WPARAM wParam, LPARAM lParam) {
        return ImGuiImplWin32.WndProcHandler(hWnd, msg, wParam, lParam) == 1
             ? new LRESULT(1)
             : PInvoke.CallWindowProcW((delegate* unmanaged[Stdcall] <HWND, uint, WPARAM, LPARAM, LRESULT>)_ptr_o_WndProc, hWnd, msg, wParam, lParam);
    }

    /// <summary>
    ///     Allows interception of raw input from the game, redirecting it to ImGui if appropriate.
    /// </summary>
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvStdcall) ] )]
    private int h_pinput() {
        if (_hWnd           == 0    // if assign_devices has not yet run, bail
         || _ptr_device     == null
         || _ptr_device_ctx == null)
            return FhCall.Phyre_PFramework_PInput_Update.chain_from(h_pinput).fnptr!();

        ImGuiIOPtr io = ImGui.GetIO();
        return io.WantCaptureKeyboard || io.WantCaptureMouse
            ? 0
            : FhCall.Phyre_PFramework_PInput_Update.chain_from(h_pinput).fnptr!();
    }

    /// <summary>
    ///     Intercepts attempts to resize the game window to allow ImGui to continue drawing.
    /// </summary>
    [UnmanagedCallConv( CallConvs = [ typeof(CallConvStdcall) ] )]
    private nint h_resize_buffers(
        IDXGISwapChain* pSwapChain,
        uint            BufferCount,
        uint            Width,
        uint            Height,
        DXGI_FORMAT     NewFormat,
        uint            SwapChainFlags) {
        if (_ptr_rtv != null) {
            _ptr_device_ctx->OMSetRenderTargets(0, null, null);
            _ptr_rtv       ->Release();
            _ptr_rtv = null;
        }

        Interlocked.CompareExchange(ref _rtv_generated, 0, 1);
        return FhCall.DXGI_IDXGISwapChain_ResizeBuffers(_ptr_swapchain).
            chain_from(h_resize_buffers).fnptr!(pSwapChain, BufferCount, Width, Height, NewFormat, SwapChainFlags);
    }

    /// <summary>
    ///     Overrides the game's <see cref="IDXGISwapChain.Present(uint, DXGI_PRESENT)"/> call to display mods' user interfaces.
    /// </summary>
    [UnmanagedCallConv( CallConvs = [ typeof(CallConvStdcall) ] )]
    private nint h_present(
        IDXGISwapChain* pSwapChain,
        uint            SyncInterval,
        DXGI_PRESENT    Flags) {
        if (Interlocked.CompareExchange(ref _rtv_generated, 1, 0) == 0) {
            ID3D11Resource* ptr_backbuffer;

            fixed (ID3D11RenderTargetView** ppRTView = &_ptr_rtv)
            fixed (Guid*                    pIID     = &ID3D11Texture2D.IID_Guid) {
                _ptr_swapchain->GetBuffer(0, pIID, (void**)&ptr_backbuffer);
                _ptr_device   ->CreateRenderTargetView(ptr_backbuffer, null, ppRTView);
                ptr_backbuffer->Release();
            }
        }

        ImGuiImplD3D11.NewFrame();
        ImGuiImplWin32.NewFrame();

        ImGui.NewFrame();

        foreach (FhModuleContext module_ctx in FhApi.Mods.get_modules()) {
            module_ctx.Module.render_imgui();
        }

        ImGui.Render();

        fixed (ID3D11RenderTargetView** ppRenderTargetViews = &_ptr_rtv) {
            _ptr_device_ctx->OMSetRenderTargets(1, ppRenderTargetViews, null);
        }

        ImGuiImplD3D11.RenderDrawData(ImGui.GetDrawData());

        _rlm!.release_pending_resources();
        return FhCall.DXGI_IDXGISwapChain_Present(_ptr_swapchain).
            chain_from(h_present).fnptr!(pSwapChain, SyncInterval, Flags);
    }
}
