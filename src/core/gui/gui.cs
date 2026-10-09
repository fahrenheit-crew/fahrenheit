// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Gui;

/// <summary>
///     Fonts, style, and helper functions for user interfaces in Fahrenheit.
/// </summary>
public unsafe class FhGui {

    // Fonts for standardized style across Fahrenheit
    public ImFontPtr FONT_DEFAULT;
    public ImFontPtr FONT_JP;
    public ImFontPtr FONT_KR;
    public ImFontPtr FONT_CH_S;
    public ImFontPtr FONT_CH_T;

    public enum FhImGuiThemes {
        CLASSIC_FF = 0,
    }

    private void _init_fonts() {
        ImGuiIOPtr io        = ImGui.GetIO();
        string     dir_fonts = Path.Join(FhEnvironment.Finder.Binaries.FullName, "resources", "fonts");

        /* [fkelava 04/08/26 00:33]
         * https://github.com/HexaEngine/Hexa.NET.ImGui/issues/118
         * `new ImFontConfig()` does not work as expected due to technical limitations.
         *
         * The workaround is to call the native constructor and explicitly destroy after use.
         */

        ImFontConfigPtr font_config = ImGui.ImFontConfig();
        font_config.MergeMode = true;

        FONT_DEFAULT = io.Fonts.AddFontFromFileTTF(Path.Join(dir_fonts, "NotoSans-Regular.ttf"),   20f);
        FONT_JP      = io.Fonts.AddFontFromFileTTF(Path.Join(dir_fonts, "NotoSansJP-Regular.ttf"), 20f, font_config);
        FONT_KR      = io.Fonts.AddFontFromFileTTF(Path.Join(dir_fonts, "NotoSansKR-Regular.ttf"), 20f, font_config);
        FONT_CH_S    = io.Fonts.AddFontFromFileTTF(Path.Join(dir_fonts, "NotoSansSC-Regular.ttf"), 20f, font_config);
        FONT_CH_T    = io.Fonts.AddFontFromFileTTF(Path.Join(dir_fonts, "NotoSansTC-Regular.ttf"), 20f, font_config);

        font_config.Destroy();
    }

    private static void _init_style(FhImGuiThemes theme) {
        // Fahrenheit style from ImThemes
        ImGuiStylePtr style = ImGui.GetStyle();

        style.Alpha                          = 1.0f;
        style.DisabledAlpha                  = 0.5f;
        style.WindowPadding                  = new Vector2(11.0f, 11.1f);
        style.WindowRounding                 = 3.0f;
        style.WindowBorderSize               = 1.0f;
        style.WindowMinSize                  = new Vector2(20.0f, 20.0f);
        style.WindowTitleAlign               = new Vector2(0.5f, 0.5f);
        style.WindowMenuButtonPosition       = ImGuiDir.Right;
        style.ChildRounding                  = 5.0f;
        style.ChildBorderSize                = 1.0f;
        style.PopupRounding                  = 5.0f;
        style.PopupBorderSize                = 1.0f;
        style.FramePadding                   = new Vector2(4.0f, 3.0f);
        style.FrameRounding                  = 3.0f;
        style.FrameBorderSize                = 0.0f;
        style.ItemSpacing                    = new Vector2(5.0f, 7.0f);
        style.ItemInnerSpacing               = new Vector2(4.0f, 4.0f);
        style.CellPadding                    = new Vector2(5.0f, 3.0f);
        style.IndentSpacing                  = 16.0f;
        style.ColumnsMinSpacing              = 10.0f;
        style.ScrollbarSize                  = 13.0f;
        style.ScrollbarRounding              = 3.0f;
        style.GrabMinSize                    = 9.0f;
        style.GrabRounding                   = 3.0f;
        style.TabRounding                    = 3.0f;
        style.TabBorderSize                  = 0.0f;
        style.TabCloseButtonMinWidthSelected = 0.0f;
        style.ColorButtonPosition            = ImGuiDir.Left;
        style.ButtonTextAlign                = new Vector2(0.5f, 0.5f);
        style.SelectableTextAlign            = new Vector2(0.0f, 0.5f);

        switch (theme) {
            case FhImGuiThemes.CLASSIC_FF:
                _set_colors_ff_classic();
                break;
            default:
                throw new NotImplementedException($"Unknown theme: {theme}");
        }
    }

    private static void _set_colors_ff_classic() {
        ImGuiStylePtr style = ImGui.GetStyle();

        style.Colors[(int)ImGuiCol.Text]                  = new Vector4(1.0f  , 1.0f  , 1.0f  , 1.0f  );
        style.Colors[(int)ImGuiCol.TextDisabled]          = new Vector4(0.729f, 0.729f, 0.729f, 1.0f  );
        style.Colors[(int)ImGuiCol.WindowBg]              = new Vector4(0.114f, 0.196f, 0.412f, 1.0f  );
        style.Colors[(int)ImGuiCol.ChildBg]               = new Vector4(0.227f, 0.427f, 0.604f, 0.108f);
        style.Colors[(int)ImGuiCol.PopupBg]               = new Vector4(0.118f, 0.169f, 0.290f, 0.953f);
        style.Colors[(int)ImGuiCol.Border]                = new Vector4(0.153f, 0.153f, 0.153f, 0.349f);
        style.Colors[(int)ImGuiCol.BorderShadow]          = new Vector4(0.0f  , 0.0f  , 0.0f  , 0.129f);
        style.Colors[(int)ImGuiCol.FrameBg]               = new Vector4(0.196f, 0.263f, 0.4f  , 1.0f  );
        style.Colors[(int)ImGuiCol.FrameBgHovered]        = new Vector4(0.242f, 0.309f, 0.454f, 1.0f  );
        style.Colors[(int)ImGuiCol.FrameBgActive]         = new Vector4(0.303f, 0.384f, 0.569f, 1.0f  );
        style.Colors[(int)ImGuiCol.TitleBg]               = new Vector4(0.252f, 0.393f, 0.714f, 1.0f  );
        style.Colors[(int)ImGuiCol.TitleBgActive]         = new Vector4(0.252f, 0.405f, 0.753f, 1.0f  );
        style.Colors[(int)ImGuiCol.TitleBgCollapsed]      = new Vector4(0.253f, 0.383f, 0.678f, 0.694f);
        style.Colors[(int)ImGuiCol.MenuBarBg]             = new Vector4(0.159f, 0.294f, 0.635f, 1.0f  );
        style.Colors[(int)ImGuiCol.ScrollbarBg]           = new Vector4(0.131f, 0.152f, 0.282f, 0.196f);
        style.Colors[(int)ImGuiCol.ScrollbarGrab]         = new Vector4(0.463f, 0.569f, 0.765f, 1.0f  );
        style.Colors[(int)ImGuiCol.ScrollbarGrabHovered]  = new Vector4(0.542f, 0.648f, 0.843f, 1.0f  );
        style.Colors[(int)ImGuiCol.ScrollbarGrabActive]   = new Vector4(0.543f, 0.69f , 0.961f, 1.0f  );
        style.Colors[(int)ImGuiCol.CheckMark]             = new Vector4(0.674f, 0.758f, 0.914f, 1.0f  );
        style.Colors[(int)ImGuiCol.SliderGrab]            = new Vector4(0.607f, 0.684f, 0.824f, 1.0f  );
        style.Colors[(int)ImGuiCol.SliderGrabActive]      = new Vector4(0.640f, 0.733f, 0.902f, 1.0f  );
        style.Colors[(int)ImGuiCol.Button]                = new Vector4(0.140f, 0.307f, 0.557f, 1.0f  );
        style.Colors[(int)ImGuiCol.ButtonHovered]         = new Vector4(0.190f, 0.376f, 0.655f, 1.0f  );
        style.Colors[(int)ImGuiCol.ButtonActive]          = new Vector4(0.184f, 0.404f, 0.733f, 1.0f  );
        style.Colors[(int)ImGuiCol.Header]                = new Vector4(0.183f, 0.375f, 0.765f, 1.0f  );
        style.Colors[(int)ImGuiCol.HeaderHovered]         = new Vector4(0.240f, 0.445f, 0.863f, 1.0f  );
        style.Colors[(int)ImGuiCol.HeaderActive]          = new Vector4(0.225f, 0.461f, 0.941f, 1.0f  );
        style.Colors[(int)ImGuiCol.Separator]             = new Vector4(0.417f, 0.566f, 0.902f, 1.0f  );
        style.Colors[(int)ImGuiCol.SeparatorHovered]      = new Vector4(0.753f, 0.753f, 0.753f, 1.0f  );
        style.Colors[(int)ImGuiCol.SeparatorActive]       = new Vector4(1.0f  , 1.0f  , 1.0f  , 1.0f  );
        style.Colors[(int)ImGuiCol.ResizeGrip]            = new Vector4(0.535f, 0.659f, 0.920f, 0.514f);
        style.Colors[(int)ImGuiCol.ResizeGripHovered]     = new Vector4(0.633f, 0.741f, 0.961f, 0.784f);
        style.Colors[(int)ImGuiCol.ResizeGripActive]      = new Vector4(0.541f, 0.692f, 1.0f  , 0.784f);
        style.Colors[(int)ImGuiCol.Tab]                   = new Vector4(0.25f , 0.344f, 0.580f, 0.863f);
        style.Colors[(int)ImGuiCol.TabHovered]            = new Vector4(0.319f, 0.421f, 0.678f, 0.863f);
        style.Colors[(int)ImGuiCol.TabSelected]           = new Vector4(0.335f, 0.46f , 0.776f, 1.0f  );
        style.Colors[(int)ImGuiCol.TabDimmed]             = new Vector4(0.098f, 0.232f, 0.408f, 0.785f);
        style.Colors[(int)ImGuiCol.TabDimmedSelected]     = new Vector4(0.165f, 0.319f, 0.519f, 1.0f  );
        style.Colors[(int)ImGuiCol.DockingPreview]        = new Vector4(0.831f, 0.684f, 0.564f, 0.345f);
        style.Colors[(int)ImGuiCol.DockingEmptyBg]        = new Vector4(0.2f  , 0.2f  , 0.2f  , 1.0f  );
        style.Colors[(int)ImGuiCol.PlotLines]             = new Vector4(0.8f  , 0.863f, 0.984f, 1.0f  );
        style.Colors[(int)ImGuiCol.PlotLinesHovered]      = new Vector4(1.0f  , 0.965f, 0.648f, 1.0f  );
        style.Colors[(int)ImGuiCol.PlotHistogram]         = new Vector4(0.789f, 0.843f, 0.953f, 0.85f );
        style.Colors[(int)ImGuiCol.PlotHistogramHovered]  = new Vector4(0.974f, 0.983f, 1.0f  , 0.784f);
        style.Colors[(int)ImGuiCol.TableHeaderBg]         = new Vector4(0.180f, 0.361f, 0.729f, 1.0f  );
        style.Colors[(int)ImGuiCol.TableBorderStrong]     = new Vector4(0.565f, 0.663f, 0.863f, 0.776f);
        style.Colors[(int)ImGuiCol.TableBorderLight]      = new Vector4(0.452f, 0.562f, 0.784f, 0.502f);
        style.Colors[(int)ImGuiCol.TableRowBg]            = new Vector4(0.508f, 0.548f, 0.631f, 0.031f);
        style.Colors[(int)ImGuiCol.TableRowBgAlt]         = new Vector4(0.657f, 0.686f, 0.745f, 0.11f );
        style.Colors[(int)ImGuiCol.TextLink]              = new Vector4(0.98f,  0.683f, 0.26f , 1.0f  );
        style.Colors[(int)ImGuiCol.TextSelectedBg]        = new Vector4(0.570f, 0.744f, 0.948f, 0.485f);
        style.Colors[(int)ImGuiCol.DragDropTarget]        = new Vector4(0.910f, 0.79f , 0.574f, 0.648f);
        style.Colors[(int)ImGuiCol.NavCursor]             = new Vector4(0.991f, 0.605f, 0.362f, 1.0f  );
        style.Colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1.0f  , 1.0f  , 1.0f  , 0.7f  );
        style.Colors[(int)ImGuiCol.NavWindowingDimBg]     = new Vector4(0.8f  , 0.8f  , 0.8f  , 0.2f  );
        style.Colors[(int)ImGuiCol.ModalWindowDimBg]      = new Vector4(0.8f  , 0.8f  , 0.8f  , 0.35f );
    }

    //TODO: Reorganize the helpers into multiple sections/files:
    //      - Input
    //      - DrawList
    //      - High-level

    public readonly ImGuiWindowFlags WINDOW_FLAGS_FULLSCREEN =
            ImGuiWindowFlags.NoDecoration
          | ImGuiWindowFlags.NoMove
          | ImGuiWindowFlags.NoResize
          | ImGuiWindowFlags.NoBringToFrontOnFocus
          | ImGuiWindowFlags.NoNavFocus;

    /// <summary>The current display size.</summary>
    public Vector2 display_size => ImGui.GetMainViewport().WorkSize;

    //TODO: Change these keys arrays to private and add ReadOnlySpan accessors
    public readonly ImGuiKey[] keys_up = [
        ImGuiKey.W,
        ImGuiKey.UpArrow,
        ImGuiKey.GamepadDpadUp,
        ImGuiKey.GamepadLStickUp,
    ];

    public readonly ImGuiKey[] keys_down = [
        ImGuiKey.S,
        ImGuiKey.DownArrow,
        ImGuiKey.GamepadDpadDown,
        ImGuiKey.GamepadLStickDown,
    ];

    public readonly ImGuiKey[] keys_left = [
        ImGuiKey.A,
        ImGuiKey.LeftArrow,
        ImGuiKey.GamepadDpadLeft,
        ImGuiKey.GamepadLStickLeft,
    ];

    public readonly ImGuiKey[] keys_right = [
        ImGuiKey.D,
        ImGuiKey.RightArrow,
        ImGuiKey.GamepadDpadRight,
        ImGuiKey.GamepadLStickRight,
    ];

    public readonly ImGuiKey[] keys_confirm = [
        ImGuiKey.Enter,
        FhGlobal.lang_id == FhLangId.Japanese
            ? ImGuiKey.GamepadFaceRight
            : ImGuiKey.GamepadFaceDown,
    ];

    public readonly ImGuiKey[] keys_cancel = [
        ImGuiKey.Escape,
        ImGuiKey.Backspace,
        FhGlobal.lang_id == FhLangId.Japanese
            ? ImGuiKey.GamepadFaceDown
            : ImGuiKey.GamepadFaceRight,
    ];

    /// <summary>
    /// Initialize values that require ImGui to be running. Called by Runtime.
    /// </summary>
    internal void init(FhImGuiThemes? theme = null) {
        _init_fonts();
        _init_style(theme ?? FhImGuiThemes.CLASSIC_FF);
    }

    /// <summary>Check whether any of the specified keys were pressed.</summary>
    /// <param name="keys">Set of keys to be checked.</param>
    /// <param name="repeat">Whether the method should repeatedly return <c>true</c> for held inputs.</param>
    /// <returns>Whether any of the keys were pressed.</returns>
    public bool is_any_pressed(IEnumerable<ImGuiKey> keys, bool repeat = false) {
        foreach (ImGuiKey key in keys) {
            if (ImGui.IsKeyPressed(key, repeat)) {
                return true;
            }
        }

        return false;
    }

    /// <summary>Check whether any of the specified keys were held down.</summary>
    /// <param name="keys">Set of keys to be checked.</param>
    /// <returns>Whether any of the keys were held down.</returns>
    public bool is_any_down(IEnumerable<ImGuiKey> keys) {
        foreach (ImGuiKey key in keys) {
            if (ImGui.IsKeyDown(key)) {
                return true;
            }
        }

        return false;
    }

    /// <summary>Check whether any of the specified keys were released.</summary>
    /// <param name="keys">Set of keys to be checked.</param>
    /// <returns>Whether any of the keys were released.</returns>
    public bool is_any_released(IEnumerable<ImGuiKey> keys) {
        foreach (ImGuiKey key in keys) {
            if (ImGui.IsKeyReleased(key)) {
                return true;
            }
        }

        return false;
    }

    /// <summary>Detect whether the mouse cursor is hovering over a specified rectangle.</summary>
    /// <param name="rect">The rect describing an area of the game window to detect the mouse cursor over.</param>
    /// <param name="require_movement">
    ///     Whether the method should return <c>false</c> if the mouse is not moving.
    /// </param>
    /// <param name="respect_want_capture">
    ///     Whether the method should return <c>false</c> if something else is capturing mouse input.
    /// </param>
    /// <returns>Whether the mouse cursor is hovering the specified rectangle.</returns>
    public bool mouse_hovering(
        Rect rect,
        bool require_movement = false,
        bool respect_want_capture = true
    ) {
        return (!respect_want_capture || !ImGui.GetIO().WantCaptureMouse)
            && (!require_movement || ImGui.GetIO().MouseDelta.LengthSquared() > 0)
            && ImGui.IsMouseHoveringRect(rect.pos, rect.pos + rect.size, false);
    }

    /// <summary>Detect whether the user clicked on a specified rectangle.</summary>
    /// <param name="rect">The rect describing an area of the game window to detect mouse clicks on.</param>
    /// <param name="button">The button of the mouse to detect clicks of.</param>
    /// <param name="repeat">Whether the method should repeatedly return <c>true</c> for held inputs.</param>
    /// <param name="respect_want_capture">
    ///     Whether the method should return <c>false</c> if something else is capturing mouse input.
    /// </param>
    /// <returns>Whether the user clicked with the button on the specified rectangle.</returns>
    public bool mouse_clicked(
        Rect rect,
        ImGuiMouseButton button = ImGuiMouseButton.Left,
        bool repeat = false,
        bool respect_want_capture = true
    ) {
        return (!respect_want_capture || !ImGui.GetIO().WantCaptureMouse)
            && ImGui.IsMouseHoveringRect(rect.pos, rect.pos + rect.size, false)
            && ImGui.IsMouseClicked(button, repeat);
    }

    /// <summary>
    ///     Draw text with a given size, alignment, and color.
    ///     Optionally, draws a shadow under the text.
    /// </summary>
    /// <remarks>
    ///     The alignment will use the entire available content region to determine center and end positions.
    /// </remarks>
    /// <param name="text">The text to be drawn.</param>
    /// <param name="font_size">The font size to draw the text at.</param>
    /// <param name="draw_shadow">Whether a shadow should be drawn under the text.</param>
    /// <param name="align">The alignment of the text inside the available content region.</param>
    /// <param name="color">The color to draw the text using in the format 0xAABBGGRR, i.e. RGBA8.</param>
    public void draw_text(
        string      text,
        float       font_size,
        bool        draw_shadow = true,
        Alignment2D align       = default,
        uint        color       = 0xFFFFFFFF
    ) {
        Vector2 position = ImGui.GetContentRegionAvail();

        position.X = align.h switch {
            Alignment.BEGIN  => 0f,
            Alignment.CENTER => position.X / 2f,
            Alignment.END    => position.X,

            _ => throw new NotImplementedException(),
        };

        position.Y = align.v switch {
            Alignment.BEGIN  => 0f,
            Alignment.CENTER => position.Y / 2f,
            Alignment.END    => position.Y,

            _ => throw new NotImplementedException(),
        };

        position += ImGui.GetCursorPos();

        // Instead of repeating the text alignment logic here using ImGui.SetCursorPos,
        // we call through to the draw list variant with the window's draw list.
        draw_text(
            ImGui.GetWindowDrawList(),
            position,
            text,
            font_size,
            draw_shadow,
            align,
            color
        );
    }

    /// <inheritdoc cref="draw_text(string, float, bool, Alignment2D, uint)"/>
    public void draw_text(
        ReadOnlySpan<byte> text,
        float              font_size,
        bool               draw_shadow = true,
        Alignment2D        align       = default,
        uint               color       = 0xFFFFFFFF
    ) {
        Vector2 position = ImGui.GetContentRegionAvail();

        position.X = align.h switch {
            Alignment.BEGIN  => 0f,
            Alignment.CENTER => position.X / 2f,
            Alignment.END    => position.X,

            _ => throw new NotImplementedException(),
        };

        position.Y = align.v switch {
            Alignment.BEGIN  => 0f,
            Alignment.CENTER => position.Y / 2f,
            Alignment.END    => position.Y,

            _ => throw new NotImplementedException(),
        };

        position += ImGui.GetCursorPos();

        // Instead of repeating the text alignment logic here using ImGui.SetCursorPos,
        // we call through to the draw list variant with the window's draw list.
        draw_text(
            ImGui.GetWindowDrawList(),
            position,
            text,
            font_size,
            draw_shadow,
            align,
            color
        );
    }

    /// <summary>
    ///     Draw text with a given size, alignment, and color at some position in a draw list.
    ///     Optionally, draws a shadow under the text.
    /// </summary>
    /// <remarks>
    ///     The text will be aligned relative to the position. For example:
    ///     <ul>
    ///         <li>Text aligned to (BEGIN, BEGIN) will be drawn with its top-left corner at the position.</li>
    ///         <li>Text aligned to (CENTER, CENTER) will be drawn with its center at the position.</li>
    ///         <li>Text aligned to (END, END) will be drawn with its bottom-right corner at the position.</li>
    ///     </ul>
    /// </remarks>
    /// <param name="draw_list">The draw list to add the text to.</param>
    /// <param name="position">The position to draw the text at.</param>
    /// <param name="text">The text to be drawn.</param>
    /// <param name="font_size">The font size to draw the text at.</param>
    /// <param name="draw_shadow">Whether a shadow should be drawn under the text.</param>
    /// <param name="align">The alignment of the text relative to the position.</param>
    /// <param name="color">The color to draw the text using in the format <c>0xAABBGGRR</c>, i.e. RGBA8.</param>
    /// <returns>The size of the drawn text.</returns>
    /// <seealso cref="ImGui.GetWindowDrawList()"/>
    /// <seealso cref="ImGui.GetBackgroundDrawList()"/>
    /// <seealso cref="ImGui.GetForegroundDrawList()"/>
    public Vector2 draw_text(
        ImDrawListPtr draw_list,
        Vector2       position,
        string        text,
        float         font_size,
        bool          draw_shadow = false,
        Alignment2D   align       = default,
        uint          color       = 0xFFFFFFFF
    ) {
        ImGui.PushFont(null, font_size);

        Vector2 text_size = ImGui.CalcTextSize(text);

        position.X -= align.h switch {
            Alignment.BEGIN  => 0f,
            Alignment.CENTER => text_size.X / 2f,
            Alignment.END    => text_size.X,

            _ => throw new NotImplementedException(),
        };

        position.Y -= align.v switch {
            Alignment.BEGIN  => 0f,
            Alignment.CENTER => text_size.Y / 2f,
            Alignment.END    => text_size.Y,

            _ => throw new NotImplementedException(),
        };

        if (draw_shadow) {
            // Use the alpha from the provided color
            uint shadow_color = 0 | (color & 0xFF000000);

            draw_list.AddText(position + new Vector2(2f), shadow_color, text);
        }

        draw_list.AddText(position, color, text);

        ImGui.PopFont();

        return text_size;
    }

    /// <inheritdoc cref="draw_text(ImDrawListPtr, Vector2, string, float, bool, Alignment2D, uint)"/>
    public Vector2 draw_text(
        ImDrawListPtr      draw_list,
        Vector2            position,
        ReadOnlySpan<byte> text,
        float              font_size,
        bool               draw_shadow = false,
        Alignment2D        align       = default,
        uint               color       = 0xFFFFFFFF
    ) {
        ImGui.PushFont(null, font_size);

        Vector2 text_size = ImGui.CalcTextSize(text);

        position.X -= align.h switch {
            Alignment.BEGIN  => 0f,
            Alignment.CENTER => text_size.X / 2f,
            Alignment.END    => text_size.X,

            _ => throw new NotImplementedException(),
        };

        position.Y -= align.v switch {
            Alignment.BEGIN  => 0f,
            Alignment.CENTER => text_size.Y / 2f,
            Alignment.END    => text_size.Y,

            _ => throw new NotImplementedException(),
        };

        if (draw_shadow) {
            // Use the alpha from the provided color
            uint shadow_color = 0 | (color & 0xFF000000);

            draw_list.AddText(position + new Vector2(2f), shadow_color, text);
        }

        draw_list.AddText(position, color, text);

        ImGui.PopFont();

        return text_size;
    }

    /// <summary>Draw a filled triangle with a gradient to the given draw list.</summary>
    /// <param name="draw_list">The draw list to draw the triangle to.</param>
    /// <param name="bounds">The bounding box of the desired triangle.</param>
    /// <param name="direction">The cardinal direction the triangle should point in.</param>
    /// <param name="color_base1">The color of the left point of the base of the triangle.</param>
    /// <param name="color_base2">The color of the right point of the base of the triangle.</param>
    /// <param name="color_peak">The color of the top point of the triangle.</param>
    /// <seealso cref="ImGui.GetWindowDrawList()"/>
    /// <seealso cref="ImGui.GetBackgroundDrawList()"/>
    /// <seealso cref="ImGui.GetForegroundDrawList()"/>
    public void draw_triangle_gradient(
        ImDrawListPtr draw_list,
        Rect          bounds,
        GradientDirection     direction,
        uint          color_base1,
        uint          color_base2,
        uint          color_peak
    ) {
        if (((color_base1 | color_base2 | color_peak) & 0xFF000000) == 0) {
            return;
        }

        Vector2 uv = draw_list.Data.TexUvWhitePixel;

        draw_list.PrimReserve(3, 3);

        draw_list.PrimWriteIdx((ushort)draw_list.VtxCurrentIdx);
        draw_list.PrimWriteIdx((ushort)(draw_list.VtxCurrentIdx + 1));
        draw_list.PrimWriteIdx((ushort)(draw_list.VtxCurrentIdx + 2));

        Vector2 peak = direction switch {
            GradientDirection.UP    => bounds.top,
            GradientDirection.RIGHT => bounds.right,
            GradientDirection.DOWN  => bounds.bottom,
            GradientDirection.LEFT  => bounds.left,

            _ => throw new UnreachableException(),
        };

        Vector2 base1 = direction switch {
            GradientDirection.UP    => bounds.bottom_left,
            GradientDirection.RIGHT => bounds.top_left,
            GradientDirection.DOWN  => bounds.top_right,
            GradientDirection.LEFT  => bounds.bottom_right,

            _ => throw new UnreachableException(),
        };

        Vector2 base2 = direction switch {
            GradientDirection.UP    => bounds.bottom_right,
            GradientDirection.RIGHT => bounds.bottom_left,
            GradientDirection.DOWN  => bounds.top_left,
            GradientDirection.LEFT  => bounds.top_right,

            _ => throw new UnreachableException(),
        };

        draw_list.PrimWriteVtx(peak , uv, color_peak);
        draw_list.PrimWriteVtx(base1, uv, color_base1);
        draw_list.PrimWriteVtx(base2, uv, color_base2);
    }

    private void draw_rectangle_gradient_up(
        ImDrawListPtr draw_list,
        Rect bounds,
        List<GradientStep> steps
    ) {
        for (int i = 1; i < steps.Count; i++)  {
            GradientStep step1 = steps[i - 1];
            GradientStep step2 = steps[i];

            Rect step_bounds = new() {
                pos  = Vector2.Lerp(bounds.bottom_left, bounds.top_left, step1.progress),
                size = bounds.size with {
                    Y = bounds.size.Y * (step2.progress - step1.progress),
                },
            };

            draw_list.AddRectFilledMultiColor(
                step_bounds.top_left,
                step_bounds.bottom_right,
                step2.color_a,
                step2.color_b,
                step1.color_b,
                step1.color_a
            );
        }
    }

    private void draw_rectangle_gradient_right(
        ImDrawListPtr draw_list,
        Rect bounds,
        List<GradientStep> steps
    ) {
        for (int i = 1; i < steps.Count; i++)  {
            GradientStep step1 = steps[i - 1];
            GradientStep step2 = steps[i];

            Rect step_bounds = new() {
                pos  = Vector2.Lerp(bounds.top_left, bounds.top_right, step1.progress),
                size = bounds.size with {
                    X = bounds.size.X * (step2.progress - step1.progress),
                },
            };

            draw_list.AddRectFilledMultiColor(
                step_bounds.top_left,
                step_bounds.bottom_right,
                step1.color_a,
                step2.color_a,
                step2.color_b,
                step1.color_b
            );
        }
    }

    private void draw_rectangle_gradient_down(
        ImDrawListPtr draw_list,
        Rect bounds,
        List<GradientStep> steps
    ) {
        for (int i = 1; i < steps.Count; i++)  {
            GradientStep step1 = steps[i - 1];
            GradientStep step2 = steps[i];

            Rect step_bounds = new() {
                pos  = Vector2.Lerp(bounds.top_left, bounds.bottom_left, step1.progress),
                size = bounds.size with {
                    Y = bounds.size.Y * (step2.progress - step1.progress),
                },
            };

            draw_list.AddRectFilledMultiColor(
                step_bounds.top_left,
                step_bounds.bottom_right,
                step1.color_b,
                step1.color_a,
                step2.color_a,
                step2.color_b
            );
        }
    }

    private void draw_rectangle_gradient_left(
        ImDrawListPtr draw_list,
        Rect bounds,
        List<GradientStep> steps
    ) {
        for (int i = 1; i < steps.Count; i++)  {
            GradientStep step1 = steps[i - 1];
            GradientStep step2 = steps[i];

            Rect step_bounds = new() {
                pos  = Vector2.Lerp(bounds.top_right, bounds.top_left, step1.progress),
                size = bounds.size with {
                    X = bounds.size.X * (step2.progress - step1.progress),
                },
            };

            draw_list.AddRectFilledMultiColor(
                step_bounds.top_left,
                step_bounds.bottom_right,
                step2.color_b,
                step1.color_b,
                step1.color_a,
                step2.color_a
            );
        }
    }

    public void draw_rectangle_gradient(
        ImDrawListPtr draw_list,
        Rect bounds,
        GradientDirection direction,
        ReadOnlySpan<GradientStep> steps
    ) {
        ArgumentOutOfRangeException.ThrowIfLessThan(steps.Length, 1);

        List<GradientStep> full_steps = [ .. steps ];

        if (steps[0].progress != 0f)
            full_steps.Insert(0, steps[0] with { progress = 0f });

        if (steps[^1].progress != 1f)
            full_steps.Add(steps[^1] with { progress = 1f });

        switch (direction) {
            case GradientDirection.UP:    draw_rectangle_gradient_up   (draw_list, bounds, full_steps); break;
            case GradientDirection.RIGHT: draw_rectangle_gradient_right(draw_list, bounds, full_steps); break;
            case GradientDirection.DOWN:  draw_rectangle_gradient_down (draw_list, bounds, full_steps); break;
            case GradientDirection.LEFT:  draw_rectangle_gradient_left (draw_list, bounds, full_steps); break;

            default: throw new UnreachableException();
        }
    }

    public void draw_quad_gradient(
        ImDrawListPtr draw_list,
        Vector2[] points,
        uint[] colors
    ) {
        ArgumentOutOfRangeException.ThrowIfNotEqual(points.Length, 4);
        ArgumentOutOfRangeException.ThrowIfNotEqual(colors.Length, 4);

        // Return early if all colors are transparent
        for (int i = 0; i < 4; i++) {
            if ((colors[i] & 0xFF000000) != 0) break;
            if (i == 3) return;
        }

        Vector2 uv = draw_list.Data.TexUvWhitePixel;

        draw_list.PrimReserve(6, 4);

        /*       0 ----- 1
         *      /       /
         *     /       /
         *    /       /
         *   2 ----- 3
         */

        draw_list.PrimWriteIdx((ushort)(draw_list.VtxCurrentIdx + 0));
        draw_list.PrimWriteIdx((ushort)(draw_list.VtxCurrentIdx + 1));
        draw_list.PrimWriteIdx((ushort)(draw_list.VtxCurrentIdx + 2));

        draw_list.PrimWriteIdx((ushort)(draw_list.VtxCurrentIdx + 1));
        draw_list.PrimWriteIdx((ushort)(draw_list.VtxCurrentIdx + 2));
        draw_list.PrimWriteIdx((ushort)(draw_list.VtxCurrentIdx + 3));

        for (int i = 0; i < 4; i++) {
            draw_list.PrimWriteVtx(points[i], uv, colors[i]);
        }
    }
}
