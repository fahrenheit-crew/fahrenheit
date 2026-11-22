// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime.Gui;

/// <summary>
///     Implements Fahrenheit's replacement opening sequence.
/// </summary>
[FhLoad(FhGameId.FFX | FhGameId.FFX2 | FhGameId.FFX2LM)]
public sealed unsafe class FhOpeningSequenceModule : FhModule {

    private static readonly FhTexture _tex_se           = new("/FFX_Data/GameData/PS3Data/map/titl/titl02/2d/tex/D3D11/0_0_512_448_0.dds.phyre",        FhTextureType.GAME_PHYRE_2D);
    private static readonly FhTexture _tex_proj_x       = new("/FFX_Data/GameData/PS3Data/map/titl/titl02/2d/tex/D3D11/0_1_512_448_0.dds.phyre",        FhTextureType.GAME_PHYRE_2D);
    private static readonly FhTexture _tex_proj_x2_logo = new("/FFX-2_Data/GameData/PS3Data/magic/magic_0353/tex/D3D11/13120_19_0_0_128_256.dds.phyre", FhTextureType.GAME_PHYRE_2D);
    private static readonly FhTexture _tex_proj_x2_text = new("/FFX-2_Data/GameData/PS3Data/magic/magic_0353/tex/D3D11/8064_19_0_0_512_64.dds.phyre",   FhTextureType.GAME_PHYRE_2D);
    private static readonly FhTexture _tex_proj_x2_year = new("/FFX-2_Data/GameData/PS3Data/magic/magic_0353/tex/D3D11/14464_19_0_0_256_256.dds.phyre", FhTextureType.GAME_PHYRE_2D);

    private static FhTexture? _tex_fh_x;
    private static FhTexture? _tex_fh_x2;

    private static float _time   = 4F;
    private static bool  _active = false;

    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        string path_x  = Path.Join(mod_context.Paths.ResourcesDir.FullName, "fh_base_500.png");
        string path_x2 = Path.Join(mod_context.Paths.ResourcesDir.FullName, "fh_base_500.png");

        _tex_fh_x  = new(path_x,  FhTextureType.DISK);
        _tex_fh_x2 = new(path_x2, FhTextureType.DISK);

        return FhCall.PhyreScene_beginRenderOpeningScreen.hook(this, h_open_begin)
            && FhCall.PhyreScene_isOpeningScreenPlaying  .hook(this, h_open_active);
    }

    [UnmanagedCallConv(CallConvs = [ typeof(CallConvThiscall) ] )]
    private void h_open_begin(byte* ptr_this) {
        PApplication* ptr_pa = FhCall.Phyre_PFramework_PApplication_GetApplication.fnptr!();
        FhCall.Phyre_PRendering_PRendererBase_setClearColor.fnptr!((byte*) ptr_pa + 0x124, 0, 0, 0, 1);

        /* [fkelava 06/11/26 17:09]
         * There is no opening screen Iggy UI anymore. We have to force the corresponding
         * pointer to null or the game will take an access violation.
         */

        *(uint*) (ptr_this + FhUtil.select(0x10DC8, 0xFFF0, 0xFFF0)) = 0;

        if (!FhApi.Resources.load_texture(_tex_se)           ||
            !FhApi.Resources.load_texture(_tex_proj_x)       ||
            !FhApi.Resources.load_texture(_tex_proj_x2_logo) ||
            !FhApi.Resources.load_texture(_tex_proj_x2_text) ||
            !FhApi.Resources.load_texture(_tex_proj_x2_year) ||
            !FhApi.Resources.load_texture(_tex_fh_x!)        ||
            !FhApi.Resources.load_texture(_tex_fh_x2!)
        ) {
            _logger.Warning("Failed to load opening sequence textures.");
        }

        _active = true;
    }

    [UnmanagedCallConv(CallConvs = [ typeof(CallConvThiscall) ] )]
    private byte h_open_active(byte* ptr_this) {
        byte rv = (byte)(_time > 0 ? 1 : 0);

        if (rv == 0) {
            _active = false;

            if (!FhApi.Resources.unload_texture(_tex_se)           ||
                !FhApi.Resources.unload_texture(_tex_proj_x)       ||
                !FhApi.Resources.unload_texture(_tex_proj_x2_logo) ||
                !FhApi.Resources.unload_texture(_tex_proj_x2_text) ||
                !FhApi.Resources.unload_texture(_tex_proj_x2_year) ||
                !FhApi.Resources.unload_texture(_tex_fh_x!)        ||
                !FhApi.Resources.unload_texture(_tex_fh_x2!)
            ) {
                _logger.Warning("Failed to unload opening sequence textures.");
            }
        }

        return rv;
    }

    public static void ui_logo_se() {
        if (!_tex_se.try_use(out ImTextureRef tex_sqex, out FhTextureMetadata? tex_sqex_data))
            return;

        ImGuiViewportPtr ptr_viewport = ImGui.GetMainViewport();
        ImDrawListPtr    ptr_draw     = ImGui.GetBackgroundDrawList();

        Vector2 logo_sqex_pos         = new(676f, 719f);
        Vector2 logo_sqex_size        = new(697f,  97f);
        Vector2 logo_sqex_scaled_size = new(654f,  68f);

        Rect screen    = new() { pos = ptr_viewport.WorkSize / 2f };
        UV   screen_uv = screen
            .expand(logo_sqex_scaled_size, Alignment2D.CENTER)
            .as_uv ();

        UV tex_uv = new Rect {
            pos  = logo_sqex_pos,
            size = logo_sqex_size,
        }.as_uv(tex_sqex_data.size);

        ptr_draw.AddImage(
            tex_sqex,
            screen_uv.p0,
            screen_uv.p1,
            tex_uv.p0,
            tex_uv.p1
        );
    }

    public static void ui_logo_fh() {
        if (!_tex_fh_x !.try_use(out ImTextureRef tex_x,  out FhTextureMetadata? data_x)
        ||  !_tex_fh_x2!.try_use(out ImTextureRef tex_x2, out FhTextureMetadata? data_x2))
            return;

        ImTextureRef      tex  = FhUtil.select(tex_x,  tex_x2,  tex_x2);
        FhTextureMetadata data = FhUtil.select(data_x, data_x2, data_x2);

        ImGuiViewportPtr ptr_viewport = ImGui.GetMainViewport();
        ImDrawListPtr    ptr_draw     = ImGui.GetBackgroundDrawList();

        Rect screen    = new() { pos = ptr_viewport.WorkSize / 2f };
        UV   screen_uv = screen
            .expand(data.size, Alignment2D.CENTER)
            .as_uv ();

        UV tex_uv = new Rect {
            pos  = Vector2.Zero,
            size = data.size,
        }.as_uv(data.size, false);

        ptr_draw.AddImage(
            tex,
            screen_uv.p0,
            screen_uv.p1,
            tex_uv.p0,
            tex_uv.p1
        );
    }

    public override void render_imgui() {
        if (!_active)
            return;

        ImGuiIOPtr io = ImGui.GetIO();

        _time = float.Max(0F, _time - io.DeltaTime);

        if (_time > 2F) ui_logo_se();
        else            ui_logo_fh();
    }

}
