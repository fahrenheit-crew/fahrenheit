// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Tools.ModManager;

/* [fkelava 15/8/25 17:41]
 * This is the generic tool program stub. Do not edit this unless you encounter a bug.
 * To edit the tool's UI, please open `ui_*.cs`.
 */

/* [fkelava 19/07/26 23:30]
 * Previously, we used the same Win32+D3D11 support as we used inside the game process.
 * Now, in the interests of our tools working across platforms, we do OGL3+SDL3 for tools.
 *
 * For reasons which are not known, the 'backend' package and the OGL3/SDL3 wrappers from Hexa.NET
 * _do not_ use the same types. This necessitates a lot of ugly casting and/or wrapper types.
 * Legibility is thus rather poor. You have been warned.
 */

internal sealed unsafe class Program {
    // Fahrenheit Theme Properties
    internal static readonly FhImGuiHelper ImGuiHelper = new();

    // SDL3
    private static SDLWindow*   _sdl_window;
    private static uint         _sdl_window_id;
    private static SDLGLContext _sdl_context;

    // OGL3
    private static GL?          _gl_context;

    /* [fkelava 19/07/26 23:56]
     * https://github.com/ocornut/imgui/blob/81c008f90d488d18370dbe6741115e126d67f539/examples/example_sdl3_opengl3/main.cpp#L28
     * This is a straight rewrite. Comments appear as in the original, where they are in the original; with some extras.
     */

    private static void Main(string[] args) {

        // Setup SDL.
        SDLInitFlags init_flags = (
            SDLInitFlags.Video
          | SDLInitFlags.Events);

        if (!SDL.Init(init_flags)) {
            Console.WriteLine($"Fault in SDL_Init() - {SDL.GetErrorS()}");
            return;
        }

        /* [fkelava 20/07/26 00:12]
         * The example goes on to select a GL version. Since we don't have access to
         * the IMGUI_IMPL_OPENGL_* defines, we can't do that. Apparently we just let
         * the backend do its thing by passing nothing to ImGui_ImplOpenGL3_Init.
         */

        // Create window with graphics content
        SDL.GLSetAttribute(SDLGLAttr.Doublebuffer, 1);
        SDL.GLSetAttribute(SDLGLAttr.DepthSize,    24);
        SDL.GLSetAttribute(SDLGLAttr.StencilSize,  8);

        uint  main_display_id    = SDL.GetPrimaryDisplay();
        float main_display_scale = SDL.GetDisplayContentScale(main_display_id);

        SDLWindowFlags window_flags =
            SDLWindowFlags.Opengl
          | SDLWindowFlags.Resizable
          | SDLWindowFlags.Hidden
          | SDLWindowFlags.HighPixelDensity;

        _sdl_window = SDL.CreateWindow(
            "Fahrenheit Mod Manager",
            int.CreateChecked(1280 * main_display_scale),
            int.CreateChecked(800  * main_display_scale),
            window_flags);

        if (_sdl_window == null) {
            Console.WriteLine($"Fault in SDL_CreateWindow() - {SDL.GetErrorS()}");
            return;
        }

        _sdl_window_id = SDL.GetWindowID    (_sdl_window);
        _sdl_context   = SDL.GLCreateContext(_sdl_window);

        if (_sdl_context == SDLGLContext.Null) {
            Console.WriteLine($"Fault in SDL_GL_CreateContext() - {SDL.GetErrorS()}");
            return;
        }

        /* [fkelava 20/07/26 00:31]
         * For some reason we do not get SDL_WINDOWPOS_CENTERED defined. We improvise.
         */

        SDL.GLMakeCurrent    (_sdl_window, _sdl_context);
        SDL.GLSetSwapInterval(1); // Enable VSync
        SDL.SetWindowPosition(_sdl_window,
            (int)SDL.SDL_WINDOWPOS_CENTERED_MASK,
            (int)SDL.SDL_WINDOWPOS_CENTERED_MASK);
        SDL.ShowWindow       (_sdl_window);

        // Setup Dear ImGui context
        ImGuiContextPtr ctx   = ImGui.CreateContext();
        ImGuiIOPtr      io    = ImGui.GetIO();
        ImGuiStylePtr   style = ImGui.GetStyle();

        io.ConfigFlags |= ImGuiConfigFlags.NavEnableKeyboard; // Enable Keyboard Controls
        io.ConfigFlags |= ImGuiConfigFlags.NavEnableGamepad;  // Enable Gamepad Controls
        io.ConfigFlags |= ImGuiConfigFlags.DockingEnable;     // Enable Docking
        io.ConfigFlags |= ImGuiConfigFlags.ViewportsEnable;   // Enable Multi-Viewport / Platform Windows

        // Setup Dear ImGui style
        ImGui.StyleColorsDark();
        //FhImGuiHelper.init(); // Apply Fahrenheit theme. // todo uncomment when fahrenheit theme is accessible

        // Setup scaling
        style.ScaleAllSizes(main_display_scale); // Bake a fixed style scale. (Changing this requires resetting Style + calling this again).
        style.FontScaleDpi = main_display_scale; // Set initial font scale.
        io.ConfigDpiScaleFonts     = true;       // [Experimental] Automatically overwrite style.FontScaleDpi in Begin() when Monitor DPI changes. This will scale fonts but _NOT_ scale sizes/padding for now.
        io.ConfigDpiScaleViewports = true;       // [Experimental] Scale Dear ImGui and Platform Windows when Monitor DPI changes.

        // When viewports are enabled we tweak WindowRounding/WindowBg so platform windows can look identical to regular ones.
        if (io.ConfigFlags.HasFlag(ImGuiConfigFlags.ViewportsEnable))
        {
            style.WindowRounding                   = 0.0F;
            style.Colors[(int)ImGuiCol.WindowBg].W = 1.0F;
        }

        // Setup Platform/Renderer backends
        ImGuiImplSDL3.SetCurrentContext(ctx);
        ImGuiImplSDL3.InitForOpenGL    (new SDLWindowPtrB((SDLWindowB*)_sdl_window), (void*)_sdl_context.Handle);

        ImGuiImplOpenGL3.SetCurrentContext(ctx);
        ImGuiImplOpenGL3.Init             ((byte*)null);

        /* [fkelava 20/07/26 00:17]
         * See comment beneath main loop body.
         */

        _gl_context = new (new FhGlContext(_sdl_window, _sdl_context));

        // Main loop
        bool     quit = false;
        SDLEvent msg  = default;

        while (!quit) {
            // Poll and handle events (inputs, window resize, etc.)
            while (SDL.PollEvent(ref msg)) {
                ImGuiImplSDL3.ProcessEvent((SDLEventB*)&msg);

                quit = (SDLEventType)msg.Type switch {
                    SDLEventType.Quit                                                            => true,
                    SDLEventType.WindowCloseRequested when msg.Window.WindowID == _sdl_window_id => true,
                    _                                                                            => false
                };
            }

            SDLWindowFlags current_window_flags = SDL.GetWindowFlags(_sdl_window);

            if (current_window_flags.HasFlag(SDLWindowFlags.Minimized)) {
                SDL.Delay(10);
                continue;
            }

            // Start the Dear ImGui frame
            ImGuiImplOpenGL3.NewFrame();
            ImGuiImplSDL3   .NewFrame();
            ImGui           .NewFrame();

            // Draw the mod manager UI
            FhModManagerUI.UI();

            // Rendering
            ImGui.Render();

            _gl_context.Viewport  (0, 0, int.CreateChecked( io.DisplaySize.X ), int.CreateChecked( io.DisplaySize.Y ));
            _gl_context.ClearColor(0.45F, 0.55F, 0.60F, 1F);
            _gl_context.Clear     (GLClearBufferMask.ColorBufferBit);

            ImGuiImplOpenGL3.RenderDrawData(ImGui.GetDrawData());

            // Update and Render additional Platform Windows
            // (Platform functions may change the current OpenGL context, so we save/restore it to make it easier to paste this code elsewhere.)
            if (io.ConfigFlags.HasFlag(ImGuiConfigFlags.ViewportsEnable)) {
                SDLWindow*   current_window  = SDL.GLGetCurrentWindow();
                SDLGLContext current_context = SDL.GLGetCurrentContext();

                ImGui.UpdatePlatformWindows();
                ImGui.RenderPlatformWindowsDefault();

                SDL.GLMakeCurrent(current_window, current_context);
            }

            SDL.GLSwapWindow(_sdl_window);
        }

        // Cleanup
        ImGuiImplOpenGL3.Shutdown();
        ImGuiImplSDL3   .Shutdown();

        ImGui.DestroyContext();

        SDL.GLDestroyContext(_sdl_context);
        SDL.DestroyWindow   (_sdl_window);
        SDL.Quit();
    }
}

/* [fkelava 20/07/26 00:22]
 * For whatever reason we don't get to directly use GL functions. We have to go
 * through this intermediary Hexa type. The relevant parts were straight copy-pasted from
 * https://github.com/HexaEngine/Hexa.NET.ImGui/blob/0ec233064d0dcd565b9ce9b40d9762d94ef30a1b/Examples/ExampleSDL3OpenGL3/Program.cs
 */

internal sealed unsafe class FhGlContext(SDLWindow* window, SDLGLContext context) : IGLContext {
    public nint Handle    => (nint)window;
    public bool IsCurrent => SDL.GLGetCurrentContext() == context;

    public void Dispose() { }

    public nint GetProcAddress      (string procName)      => (nint)SDL.GLGetProcAddress    (procName);
    public bool IsExtensionSupported(string extensionName) =>       SDL.GLExtensionSupported(extensionName);

    public void MakeCurrent ()             => SDL.GLMakeCurrent(window, context);
    public void SwapBuffers ()             => SDL.GLSwapWindow (window);
    public void SwapInterval(int interval) => SDL.GLSetSwapInterval(interval);

    public bool TryGetProcAddress(string procName, out nint procAddress) {
        return (procAddress = (nint)SDL.GLGetProcAddress(procName)) != 0;
    }
}
