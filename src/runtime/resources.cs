// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime;

/// <summary>Loads textures and other resources at runtime.</summary>
/// <remarks>In your module, access this functionality through <see cref="FhApi.Resources"/>.</remarks>
[FhLoad(FhGameId.FFX | FhGameId.FFX2 | FhGameId.FFX2LM)]
[SupportedOSPlatform("windows6.1")] // To satisfy CA1416 warning about invoking D3D/DXGI API which TerraFX annotates as supported only on Windows.
public unsafe sealed class FhResourceLoaderModule : FhModule, IFhResourceLoaderImpl, IFhPlatformUser {
    private ID3D11Device* _p_device; // https://learn.microsoft.com/en-us/windows/win32/api/d3d11/nn-d3d11-id3d11device

    private          FhPhyreLoaderModule? _plm;
    private readonly HashSet<FhTexture>   _release_queue;
    private readonly Lock                 _release_lock;

    public FhResourceLoaderModule() {
        _release_queue = [];
        _release_lock  = new Lock();
    }

    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        FhApi.Resources.impl_handle.set(this);
        FhModuleHandle<FhPhyreLoaderModule> plm_handle = new(this);

        return plm_handle.try_get_module(out _plm);
    }

    void IFhPlatformUser.platform_bind(
        ID3D11Device*        ptr_device,
        ID3D11DeviceContext* ptr_device_context,
        IDXGISwapChain*      ptr_swapchain,
        HWND                 hWnd) {
        _p_device = ptr_device;
    }

    /// <summary>
    ///     Creates a <see cref="ID3D11ShaderResourceView"/> from a given texture, then assigns it to a <see cref="FhTexture"/>.
    /// </summary>
    private bool _helper_create_srv(Hexa_ScratchImage hexa_image, Hexa_TexMetadata hexa_metadata, FhTexture texture) {
        Hexa_ID3D11SRV* srv;
        Hexa_HRESULT    rc = DirectXTex.CreateShaderResourceView(
            (Hexa_ID3D11Device*)_p_device,
            Hexa_Extensions.GetImages    (hexa_image),
            Hexa_Extensions.GetImageCount(hexa_image),
            &hexa_metadata,
            &srv);

        if (rc.IsFailure) {
            _logger.Info($"0x{rc:X}");
            return false;
        }

        FhTextureMetadata image_metadata = new(
            hexa_metadata.Width,
            hexa_metadata.Height,
            hexa_metadata.Depth,
            hexa_metadata.ArraySize,
            hexa_metadata.MipLevels,
            hexa_metadata.MiscFlags,
            hexa_metadata.MiscFlags2,
            (DXGI_FORMAT)             hexa_metadata.Format,
            (D3D11_RESOURCE_DIMENSION)hexa_metadata.Dimension);
        ImTextureRef imgui_ref = new ImTextureRef(null, srv);

        texture.load(imgui_ref, image_metadata);
        return true;
    }

    bool IFhResourceLoaderImpl.load_texture_from_disk(FhTexture texture) {
        string file_path = texture.path;

        if (_p_device == null) {
            _logger.Info($"{file_path} -> device not ready");
            return false;
        }

        if (!texture.try_lock())
            return false;

        Hexa_TexMetadata  image_metadata = default;
        Hexa_ScratchImage image = DirectXTex.CreateScratchImage();
        Hexa_HRESULT      rc    = texture.type switch {
            FhTextureType.DDS  => DirectXTex.LoadFromDDSFile (file_path, Hexa_DDSFlags.None, &image_metadata, &image),
            FhTextureType.TGA  => DirectXTex.LoadFromTGAFile (file_path, Hexa_TGAFlags.None, &image_metadata, &image),
            FhTextureType.JPEG => DirectXTex.LoadFromJPEGFile(file_path,                     &image_metadata, &image),
            FhTextureType.PNG  => DirectXTex.LoadFromPNGFile (file_path,                     &image_metadata, &image),
            FhTextureType.WIC  => DirectXTex.LoadFromWICFile (file_path, Hexa_WICFlags.None, &image_metadata, &image, null),
            _                  => -1
        };

        if (rc.IsFailure) {
            _logger.Info($"{file_path} -> 0x{rc.Value:X}");
            return false;
        }

        bool rv = _helper_create_srv(image, image_metadata, texture);
        Hexa_Extensions.Release(image);

        texture.unlock();
        return rv;
    }

    /* [fkelava 01/05/26 18:47]
     * These functions deal with game textures and call through the Phyre asset load system.
     * They have a special rule that the 'file path' parameter has to be in the game's canonical form.
     */

    bool IFhResourceLoaderImpl.load_game_texture_2d(FhTexture texture) {
        string tex_path = texture.path;

        if (_p_device == null) {
            _logger.Info($"{tex_path} -> device not ready");
            return false;
        }

        if (!texture.try_lock())
            return false;

        using FhPClusterScope cluster_scope = _plm!.cluster_load(tex_path);

        if (!cluster_scope.enter(out PCluster* ptr_cluster)) {
            _logger.Info($"{tex_path} -> cluster load failed");

            texture.unlock();
            return false;
        }

        FhPDoubleListIterator<PInstanceList> iter_instances = new(&ptr_cluster->_0x1C_instance_lists);

        while (iter_instances.next(out PInstanceList* ptr_instance_list)) {
            if (Marshal.PtrToStringAnsi(ptr_instance_list->_0x08_free_list._0x10_name) != nameof(PTexture2D))
                continue;

            FhPFilteredInstanceList        <PTexture2D> texture_filter = new(ptr_instance_list);
            FhPFilteredInstanceListIterator<PTexture2D> texture_iter   = new(texture_filter);

            // We don't know (or even care) what happens in the case that the instance list returns multiple texture objects.
            if (!texture_iter.next(out PTexture2D* ptr_texture)) {
                _logger.Error($"{tex_path} - no texture object found in filtered instance list");

                texture.unlock();
                return false;
            }

            /* [fkelava 08/05/26 23:41]
             * https://learn.microsoft.com/en-us/windows/win32/api/d3d11/nf-d3d11-id3d11device-createshaderresourceview
             * CreateShaderResourceView is meant to return HRESULT. However, the only public overload CsWin32 provides
             * is one which returns `void` and throws if the HRESULT indicates error, necessitating this ugly workaround.
             */

            ID3D11ShaderResourceView* srv;
            try {
                _p_device->CreateShaderResourceView(ptr_texture->base_PTexture2DD3D11.ptr_d3d_resource, null, &srv);
            }
            catch {
                _logger.Error($"{tex_path} - SRV instantiation failed");

                texture.unlock();
                return false;
            }

            // TODO: Can we obtain the rest of the image metadata?
            FhTextureMetadata tex_metadata = new(
                ptr_texture->base_PTexture2DD3D11.base_PTexture2DBase.m_width,
                ptr_texture->base_PTexture2DD3D11.base_PTexture2DBase.m_height,
                0,
                0,
                ptr_texture->base_PTexture2DD3D11.base_PTexture2DBase.base_PTextureCommonBase.m_maxMipLevel,
                0,
                0,
                DXGI_FORMAT             .DXGI_FORMAT_UNKNOWN,
                D3D11_RESOURCE_DIMENSION.D3D11_RESOURCE_DIMENSION_TEXTURE2D);
            ImTextureRef tex_ref = new ImTextureRef(null, srv);

            texture.load   (tex_ref, tex_metadata);
            texture.unlock();
            return true;
        }

        texture.unlock();
        return false;
    }

    bool IFhResourceLoaderImpl.load_game_texture_3d(FhTexture texture) {
        string tex_path = texture.path;

        if (_p_device == null) {
            _logger.Info($"{tex_path} -> device not ready");
            return false;
        }

        if (!texture.try_lock())
            return false;

        using FhPClusterScope cluster_scope = _plm!.cluster_load(tex_path);

        if (!cluster_scope.enter(out PCluster* ptr_cluster)) {
            _logger.Info($"{tex_path} -> cluster load failed");

            texture.unlock();
            return false;
        }

        FhPDoubleListIterator<PInstanceList> iter_instances = new(&ptr_cluster->_0x1C_instance_lists);

        while (iter_instances.next(out PInstanceList* ptr_instance_list)) {
            if (Marshal.PtrToStringAnsi(ptr_instance_list->_0x08_free_list._0x10_name) != nameof(PTexture3D))
                continue;

            FhPFilteredInstanceList        <PTexture3D> texture_filter = new(ptr_instance_list);
            FhPFilteredInstanceListIterator<PTexture3D> texture_iter   = new(texture_filter);

            // We don't know (or even care) what happens in the case that the instance list returns multiple texture objects.
            if (!texture_iter.next(out PTexture3D* ptr_texture)) {
                _logger.Error($"{tex_path} - no texture object found in filtered instance list");

                texture.unlock();
                return false;
            }

            // TODO: impl after reversing PTexture3D
        }

        texture.unlock();
        return false;
    }

    bool IFhResourceLoaderImpl.load_game_texture_cubemap(FhTexture texture) {
        string tex_path = texture.path;

        if (_p_device == null) {
            _logger.Info($"{tex_path} -> device not ready");
            return false;
        }

        if (!texture.try_lock())
            return false;

        using FhPClusterScope cluster_scope = _plm!.cluster_load(tex_path);

        if (!cluster_scope.enter(out PCluster* ptr_cluster)) {
            _logger.Info($"{tex_path} -> cluster load failed");

            texture.unlock();
            return false;
        }

        FhPDoubleListIterator<PInstanceList> iter_instances = new(&ptr_cluster->_0x1C_instance_lists);

        while (iter_instances.next(out PInstanceList* ptr_instance_list)) {
            if (Marshal.PtrToStringAnsi(ptr_instance_list->_0x08_free_list._0x10_name) != nameof(PTextureCubeMap))
                continue;

            FhPFilteredInstanceList        <PTextureCubeMap> texture_filter = new(ptr_instance_list);
            FhPFilteredInstanceListIterator<PTextureCubeMap> texture_iter   = new(texture_filter);

            // We don't know (or even care) what happens in the case that the instance list returns multiple texture objects.
            if (!texture_iter.next(out PTextureCubeMap* ptr_texture)) {
                _logger.Error($"{tex_path} - no texture object found in filtered instance list");

                texture.unlock();
                return false;
            }

            // TODO: impl after reversing PTextureCubeMap
        }

        texture.unlock();
        return false;
    }

   /* [fkelava 29/04/26 14:41]
    * Releases are deferred to after the next frame presentation.
    *
    * This is because `render_imgui` does not take effect instantly. Each module's callback
    * adds to the draw list for the current frame, but the actual act of _rendering_
    * only occurs once all have run. Thus, resources which modules use
    * _must survive an unspecified duration past the end of the callback_.
    *
    * If a user issues an ImGui call with a resource, then releases it immediately thereafter,
    * by the time the ImGui module actually attempts to render the frame the resource is freed
    * and an access violation results.
    *
    * Users are unlikely to intuitively understand and obey this rule, so we do this
    * to ensure resources are valid at all points during the current frame.
    */

    bool IFhResourceLoaderImpl.unload_texture(FhTexture texture) {
        lock (_release_lock) {
            return texture.try_lock() && _release_queue.Add(texture);
        }
    }

    /* [fkelava 01/05/26 23:58]
     * The release lock protects from a new release being enregistered during enumeration,
     * which would cause a throw. This is because this method is only invoked from `h_present` in
     * the ImGui module, which executes on the Phyre render thread.
     *
     * No `render_imgui` callback will thus run concurrently with this method. This leaves only the case
     * where a release is attempted from a _different_ thread, and the lock suffices.
     */

    /// <summary>
    ///     Flushes the queue of pending resource releases.
    /// </summary>
    /// <remarks>
    ///     It is only valid to call this method on the Phyre render thread, and only
    ///     after draw data for the frame has been rendered. Failing to observe this
    ///     will result in access violations at rendering time or exceptions at release time.
    /// </remarks>
    internal void release_pending_resources() {
        lock (_release_lock) {
            foreach (FhTexture texture in _release_queue) {
                texture.unload();
                texture.unlock();
            }

            _release_queue.Clear();
        }
    }
}
