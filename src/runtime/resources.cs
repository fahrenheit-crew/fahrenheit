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
    private ID3D11Device*        _ptr_device;  // https://learn.microsoft.com/en-us/windows/win32/api/d3d11/nn-d3d11-id3d11device
    private ID3D11DeviceContext* _ptr_context; // https://learn.microsoft.com/en-us/windows/win32/api/d3d11/nn-d3d11-id3d11devicecontext

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
        HWND                 hWnd
    ) {
        _ptr_device  = ptr_device;
        _ptr_context = ptr_device_context;
    }

    bool IFhResourceLoaderImpl.load_texture(FhTexture texture) {
        return texture.type switch {
            FhTextureType.DISK            => load_texture_disk(texture),
            FhTextureType.GAME            => load_texture_game(texture),

            FhTextureType.GAME_PHYRE_2D   => load_texture_game<PTexture2D>     (texture),
            FhTextureType.GAME_PHYRE_3D   => load_texture_game<PTexture3D>     (texture),
            FhTextureType.GAME_PHYRE_CUBE => load_texture_game<PTextureCubeMap>(texture),
            _                             => false,
        };
    }

    /// <summary>
    ///     Attempts to load the given <paramref name="texture"/> from disk.
    /// </summary>
    /// <remarks>
    ///     Any format supported by the system's Windows Imaging Component codecs can be loaded.
    /// </remarks>
    /// <returns>Whether the operation succeeded and <paramref name="texture"/> can be passed to ImGui for rendering.</returns>
    private bool load_texture_disk(FhTexture texture) {
        if (_ptr_device == null || _ptr_context == null) {
            _logger.Error($"{texture.path} -> system not ready");
            return false;
        }

        if (!texture.try_lock())
            return false;

        ID3D11Texture2D*          ptr_texture;
        ID3D11ShaderResourceView* ptr_srv;

        HRESULT hr = FhImaging.CreateWICTextureFromFile(
            _ptr_device,
            _ptr_context,
            texture.path,
            0,
            (ID3D11Resource**) &ptr_texture,
            &ptr_srv
        );

        if (hr.FAILED) {
            _logger.Error($"{texture.path} -> load failed with HRESULT 0x{hr:X8}");

            texture.unlock();
            return false;
        }

        D3D11_TEXTURE2D_DESC     desc;
        D3D11_RESOURCE_DIMENSION dimension;

        ptr_texture->GetDesc(&desc);
        ptr_texture->GetType(&dimension);

        FhTextureMetadata metadata = new(
            desc.Width,
            desc.Height,
            0,
            desc.ArraySize,
            desc.MipLevels,
            desc.MiscFlags,
            0,
            desc.Format,
            dimension
        );

        texture.load(
            new ImTextureRef(null, ptr_srv),
            metadata
        );

        texture.unlock();
        return true;
    }

    /// <summary>
    ///     Attempts to load the given non-Phyre <paramref name="texture"/> from the VBF archive.
    /// </summary>
    /// <remarks>
    ///     Any format supported by the system's Windows Imaging Component codecs can be loaded.
    ///     <see cref="FhTexture.path"/> must be a valid VBF path.
    /// </remarks>
    /// <returns>Whether the operation succeeded and <paramref name="texture"/> can be passed to ImGui for rendering.</returns>
    private bool load_texture_game(FhTexture texture) {
        if (_ptr_device == null || _ptr_context == null) {
            _logger.Error($"{texture.path} -> system not ready");
            return false;
        }

        if (!texture.try_lock())
            return false;

        using FhVbfFileScope scope = new(texture.path);

        byte[] temp = new byte[ scope.get_file_size() ];
        scope.read(temp);

        ID3D11Texture2D*          ptr_texture;
        ID3D11ShaderResourceView* ptr_srv;

        fixed (byte* ptr_temp = temp) {
            HRESULT hr = FhImaging.CreateWICTextureFromMemory(
                _ptr_device,
                _ptr_context,
                ptr_temp,
                (nuint) temp.Length,
                0,
                (ID3D11Resource**) &ptr_texture,
                &ptr_srv
            );

            if (hr.FAILED) {
                _logger.Error($"{texture.path} -> load failed with HRESULT 0x{hr:X8}");

                texture.unlock();
                return false;
            }
        }

        D3D11_TEXTURE2D_DESC     desc;
        D3D11_RESOURCE_DIMENSION dimension;

        ptr_texture->GetDesc(&desc);
        ptr_texture->GetType(&dimension);

        FhTextureMetadata metadata = new(
            desc.Width,
            desc.Height,
            0,
            desc.ArraySize,
            desc.MipLevels,
            desc.MiscFlags,
            0,
            desc.Format,
            dimension
        );

        texture.load(
            new ImTextureRef(null, ptr_srv),
            metadata
        );

        texture.unlock();
        return true;
    }

    /// <summary>
    ///     Attempts to load the given Phyre <paramref name="texture"/> from the VBF archive.
    /// </summary>
    /// <remarks>
    ///     The <paramref name="texture"/> must have the correct <see cref="FhTextureType"/> selected.
    ///     <see cref="FhTexture.path"/> must be a valid VBF path.
    /// </remarks>
    /// <returns>Whether the operation succeeded and <paramref name="texture"/> can be passed to ImGui for rendering.</returns>
    private bool load_texture_game<T>(FhTexture texture) where T : unmanaged {
        string tex_path = texture.path;

        if (_ptr_device == null || _ptr_context == null) {
            _logger.Info($"{tex_path} -> system not ready");
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

        FhPDoubleListIterator<PInstanceList> iter_instances = new(&ptr_cluster->__0x1C_instance_lists);

        while (iter_instances.next(out PInstanceList* ptr_instance_list)) {
            if (Marshal.PtrToStringAnsi(ptr_instance_list->__0x08_free_list.__0x10_name) != typeof(T).Name)
                continue;

            FhPFilteredInstanceList        <T> texture_filter = new(ptr_instance_list);
            FhPFilteredInstanceListIterator<T> texture_iter   = new(texture_filter);

            // We don't know (or even care) what happens in the case that the instance list returns multiple texture objects.
            if (!texture_iter.next(out T* ptr_texture)) {
                _logger.Error($"{tex_path} - no texture object found in filtered instance list");

                texture.unlock();
                return false;
            }

            /* [fkelava 24/10/26 21:59]
             * It would be easier to go from the resource- if we knew where it was in each respective texture type.
             * When we do, this can be simplified.
             */

            ID3D11ShaderResourceView* srv = default;

            bool is_2d = *ptr_texture is PTexture2D;
            bool is_3d = *ptr_texture is PTexture3D;
            bool is_cm = *ptr_texture is PTextureCubeMap;

            HRESULT hr = 0 switch {
                0 when is_2d => _ptr_device->CreateShaderResourceView( ((PTexture2D*) ptr_texture)->base_PTexture2DD3D11.ptr_d3d_resource, null, &srv),
                0 when is_3d => E.E_NOINTERFACE,
                0 when is_cm => E.E_NOINTERFACE,
                _            => E.E_INVALIDARG
            };

            if (hr.FAILED) {
                _logger.Error($"{tex_path} - SRV instantiation failed");

                texture.unlock();
                return false;
            }

            nuint width  = is_2d || is_3d ? ((PTexture2DBase*) ptr_texture)->m_width  : 0;
            nuint height = is_2d || is_3d ? ((PTexture2DBase*) ptr_texture)->m_height : 0;
            uint  mips   = is_2d || is_3d ? ((PTexture2DBase*) ptr_texture)->base_PTextureCommonBase.m_maxMipLevel : 0;

            D3D11_RESOURCE_DIMENSION dimension = D3D11_RESOURCE_DIMENSION.D3D11_RESOURCE_DIMENSION_UNKNOWN;
            if      (is_2d) dimension = D3D11_RESOURCE_DIMENSION.D3D11_RESOURCE_DIMENSION_TEXTURE2D;
            else if (is_3d) dimension = D3D11_RESOURCE_DIMENSION.D3D11_RESOURCE_DIMENSION_TEXTURE3D;

            // TODO: Can we obtain the rest of the image metadata?
            FhTextureMetadata tex_metadata = new(
                width,
                height,
                0,
                1,
                mips,
                0,
                0,
                DXGI_FORMAT.DXGI_FORMAT_UNKNOWN,
                dimension
            );

            texture.load(
                new ImTextureRef(null, srv),
                tex_metadata
            );

            texture.unlock();
            return true;
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
