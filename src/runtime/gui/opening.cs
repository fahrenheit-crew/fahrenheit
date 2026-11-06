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

    private static readonly FhTexture _tex_sqex         = new("/FFX_Data/PS3Data/GameData/map/titl/titl02/2d/tex/D3D11/0_0_512_448_0.dds.phyre",        FhTextureType.GAME_PHYRE_2D);
    private static readonly FhTexture _tex_proj_x       = new("/FFX_Data/PS3Data/GameData/map/titl/titl02/2d/tex/D3D11/0_1_512_448_0.dds.phyre",        FhTextureType.GAME_PHYRE_2D);
    private static readonly FhTexture _tex_proj_x2_logo = new("/FFX-2_Data/PS3Data/GameData/magic/magic_0353/tex/D3D11/13120_19_0_0_128_256.dds.phyre", FhTextureType.GAME_PHYRE_2D);
    private static readonly FhTexture _tex_proj_x2_text = new("/FFX-2_Data/PS3Data/GameData/magic/magic_0353/tex/D3D11/8064_19_0_0_512_64.dds.phyre",   FhTextureType.GAME_PHYRE_2D);
    private static readonly FhTexture _tex_proj_x2_year = new("/FFX-2_Data/PS3Data/GameData/magic/magic_0353/tex/D3D11/14464_19_0_0_256_256.dds.phyre", FhTextureType.GAME_PHYRE_2D);

    private static readonly Stopwatch _stopwatch  = new();
    private static readonly TimeSpan  _duration   = TimeSpan.FromSeconds(1);

    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        return FhCall.PhyreScene_beginRenderOpeningScreen.hook(this, h_open_begin)
            && FhCall.PhyreScene_isOpeningScreenPlaying  .hook(this, h_open_active);
    }

    private static bool _is_active => _stopwatch.IsRunning && _stopwatch.Elapsed < _duration;

    [UnmanagedCallConv(CallConvs = [ typeof(CallConvThiscall) ] )]
    private void h_open_begin(byte* ptr_this) {
        PApplication* ptr_pa = FhCall.Phyre_PFramework_PApplication_GetApplication.fnptr!();
        FhCall.Phyre_PRendering_PRendererBase_setClearColor.fnptr!((byte*) ptr_pa + 0x124, 0, 0, 0, 1);

        /* [fkelava 06/11/26 17:09]
         * There is no opening screen Iggy UI anymore. We have to force the corresponding
         * pointer to null or the game will take an access violation.
         */

        *(uint*)(ptr_this + FhUtil.select(0x10DC8, 0xFFF0, 0xFFF0)) = 0;

        if (!FhApi.Resources.load_texture(_tex_sqex)         ||
            !FhApi.Resources.load_texture(_tex_proj_x)       ||
            !FhApi.Resources.load_texture(_tex_proj_x2_logo) ||
            !FhApi.Resources.load_texture(_tex_proj_x2_text) ||
            !FhApi.Resources.load_texture(_tex_proj_x2_year)
        ) {
            _logger.Warning("Failed to load opening sequence textures.");
        }

        _stopwatch.Start();
    }

    [UnmanagedCallConv(CallConvs = [ typeof(CallConvThiscall) ] )]
    private byte h_open_active(byte* ptr_this) {
        byte rv = (byte)(_is_active ? 1 : 0);

        if (rv == 0) {
            _stopwatch.Stop();

            if (!FhApi.Resources.unload_texture(_tex_sqex)         ||
                !FhApi.Resources.unload_texture(_tex_proj_x)       ||
                !FhApi.Resources.unload_texture(_tex_proj_x2_logo) ||
                !FhApi.Resources.unload_texture(_tex_proj_x2_text) ||
                !FhApi.Resources.unload_texture(_tex_proj_x2_year)
            ) {
                _logger.Warning("Failed to unload opening sequence textures.");
            }
        }

        return rv;
    }

    public override void render_imgui() {
        if (!_is_active)
            return;

        if (_tex_sqex.try_use(out ImTextureRef tex_ref, out FhTextureMetadata? tex_data)) {
            // UI
        }
    }

}
