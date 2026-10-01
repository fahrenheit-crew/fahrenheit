// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

/// <summary>
///     Indicates the type of texture the input to <see cref="IFhResourceLoaderImpl"/> should be interpreted as.
/// </summary>
public enum FhTextureType {
    NULL  = 0,
    DDS   = 1,
    TGA   = 2,
    JPEG  = 3,
    PNG   = 4,
    WIC   = 5,
    PHYRE = 6  // Game texture
}

/* [fkelava 9/8/25 01:19]
 * A direct reimplementation of TexMetadata (https://github.com/microsoft/DirectXTex/wiki/TexMetadata)
 * to abstract Hexa implementation details from consumers and use more correct TerraFX defs.
 */

/// <summary>
///     Describes the properties of a <see cref="FhTexture"/>, such as its dimensions and format.
/// </summary>
public sealed record FhTextureMetadata(
    nuint                    width,
    nuint                    height,
    nuint                    depth,
    nuint                    arraySize,
    nuint                    mipLevels,
    uint                     miscFlags,
    uint                     miscFlags2,
    DXGI_FORMAT              format,
    D3D11_RESOURCE_DIMENSION dimension);

/// <summary>
///     A handle to an image on disk that can be passed to ImGui for rendering.
/// </summary>
public sealed record FhTexture(string path, FhTextureType type) {
    private ImTextureRef       _tex_ref;
    private FhTextureMetadata? _tex_metadata;
    private int                _tex_lock;
    private int                _tex_loaded;

    internal bool is_loaded() {
        return Interlocked.CompareExchange(ref _tex_loaded, 0, 0) == 1;
    }

    /* [fkelava 05/05/26 22:22]
     * For the end user's safety, no two actions may be performed simultaneously on a texture.
     *
     * Locking is purely co-operative (advisory?). The resource loader which operates it
     * is actually responsible for enforcing mutual exclusion based on the lock state.
     */

    /// <summary>
    ///     Attempts to lock the texture. If successful, operations on the texture
    ///     may proceed- otherwise, they must immediately abort.
    /// </summary>
    internal bool try_lock() {
        if (Interlocked.CompareExchange(ref _tex_lock, 1, 0) != 0) {
            FhInternal.Log.Warning($"texture already locked - {path}");
            return false;
        }

        return true;
    }

    /// <summary>
    ///     Unlocks the texture, enabling other operations on it to proceed.
    /// </summary>
    /// <remarks>
    ///     Only valid if <see cref="try_lock"/> has succeeded.
    ///     A spurious unlock will throw.
    /// </remarks>
    internal void unlock() {
        if (Interlocked.CompareExchange(ref _tex_lock, 0, 1) != 1) {
            throw new Exception($"unbalanced unlock call - {path}");
        }
    }

    /// <summary>
    ///     Writes platform-specific data and metadata into the texture,
    ///     which can then be passed to ImGui for rendering.
    /// </summary>
    /// <remarks>
    ///     Only valid if the texture is not already loaded.
    ///     A spurious load will throw.
    /// </remarks>
    internal void load(ImTextureRef texture_ref, FhTextureMetadata texture_metadata) {
        _tex_ref      = texture_ref;
        _tex_metadata = texture_metadata;

        if (Interlocked.Exchange(ref _tex_loaded, 1) != 0) throw new Exception($"unbalanced load call - {path}");
    }

    /// <summary>
    ///     Releases platform-specific data and metadata from the texture,
    ///     disabling its use in ImGui rendering.
    /// </summary>
    /// <remarks>
    ///     Only valid if the texture is not already unloaded.
    ///     A spurious unload will throw.
    /// </remarks>
    [SupportedOSPlatform("windows6.1")]
    internal void unload() {
        /* [fkelava 01/05/26 18:51]
         * SAFETY: cast of opaque 'ImTextureID' to true underlying type 'ID3D11ShaderResourceView' is well defined
         * see: https://github.com/ocornut/imgui/blob/da137cbbb066e57f4b44f3f331c36e1e30e1cbe2/backends/imgui_impl_dx11.cpp#L5
         * > {...} Use 'ID3D11ShaderResourceView*' as texture identifier. {...}
         */
        unsafe {
            /* [fkelava 14/07/26 18:42]
             * Testing the return value of Release is meaningless because it is not guaranteed to be precise.
             * If you believe you're leaking textures, turn on the D3D debug layer instead.
             */
            ((ID3D11ShaderResourceView*)(void*)_tex_ref.TexID)->Release();
        }

        _tex_ref      = default;
        _tex_metadata = default;

        if (Interlocked.Exchange(ref _tex_loaded, 0) != 1) throw new Exception($"unbalanced unload call - {path}");
    }

    /// <summary>
    ///     Obtains platform-specific data and metadata which can be passed to ImGui for rendering.
    /// </summary>
    public bool try_use([NotNullWhen(true)] out ImTextureRef texture_ref, [NotNullWhen(true)] out FhTextureMetadata? texture_metadata) {
        texture_ref      = _tex_ref;
        texture_metadata = _tex_metadata;

        return is_loaded();
    }
}

/* [fkelava 9/8/25 01:52]
 * The internal contract between Core and RT is abstracted here to allow us to arrange it
 * differently from FhResourceLoader's public API, if need be.
 */
internal interface IFhResourceLoaderImpl {
    /// <summary>
    ///     Attempts to load the user-requested <paramref name="texture"/> from disk.
    ///     If successful, it can be passed to ImGui for rendering.
    /// </summary>
    internal bool load_texture_from_disk(FhTexture texture);

    /// <summary>
    ///     Attempts to load the user-requested Phyre 2D <paramref name="texture"/>.
    ///     If successful, it can be passed to ImGui for rendering.
    /// </summary>
    internal bool load_game_texture_2d(FhTexture texture);

    /// <summary>
    ///     Attempts to load the user-requested Phyre 3D <paramref name="texture"/>.
    ///     If successful, it can be passed to ImGui for rendering.
    /// </summary>
    internal bool load_game_texture_3d(FhTexture texture);

    /// <summary>
    ///     Attempts to load the user-requested Phyre cubemap <paramref name="texture"/>.
    ///     If successful, it can be passed to ImGui for rendering.
    /// </summary>
    internal bool load_game_texture_cubemap(FhTexture texture);

    /// <summary>
    ///     Unloads a texture attained through any previous load call.
    /// </summary>
    internal bool unload_texture(FhTexture texture);
}

/// <summary>
///     Allows for loading of resources such as images for use in ImGui code.
/// </summary>
public sealed class FhResources {

    internal readonly FhRuntimeHandle<IFhResourceLoaderImpl> impl_handle = new(); // RT connects here.

    /// <summary>
    ///     Attempts to load the given <paramref name="texture"/> from disk.
    /// </summary>
    /// <remarks>
    ///     Supported formats are JPEG, DDS, TGA, WIC, and PNG.
    ///     The <paramref name="texture"/> must have the correct <see cref="FhTextureType"/> selected.
    /// </remarks>
    /// <returns>Whether the operation succeeded and <paramref name="texture"/> can be passed to ImGui for rendering.</returns>
    public bool load_texture_from_disk(FhTexture texture) {
        return texture.is_loaded() || (impl_handle.get(out IFhResourceLoaderImpl? impl) && impl.load_texture_from_disk(texture));
    }

    /// <summary>
    ///     Attempts to load the given 2D Phyre game <paramref name="texture"/>.
    /// </summary>
    /// <returns>Whether the operation succeeded and <paramref name="texture"/> can be passed to ImGui for rendering.</returns>
    public bool load_game_texture_2d(FhTexture texture) {
        return texture.is_loaded() || (impl_handle.get(out IFhResourceLoaderImpl? impl) && impl.load_game_texture_2d(texture));
    }

    /// <summary>
    ///     Attempts to load the given 3D Phyre game <paramref name="texture"/>.
    /// </summary>
    /// <returns>Whether the operation succeeded and <paramref name="texture"/> can be passed to ImGui for rendering.</returns>
    public bool load_game_texture_3d(FhTexture texture) {
        return texture.is_loaded() || (impl_handle.get(out IFhResourceLoaderImpl? impl) && impl.load_game_texture_3d(texture));
    }

    /// <summary>
    ///     Attempts to load the given cubemap Phyre game <paramref name="texture"/>.
    /// </summary>
    /// <returns>Whether the operation succeeded and <paramref name="texture"/> can be passed to ImGui for rendering.</returns>
    public bool load_game_texture_cubemap(FhTexture texture) {
        return texture.is_loaded() || (impl_handle.get(out IFhResourceLoaderImpl? impl) && impl.load_game_texture_cubemap(texture));
    }

    /// <summary>
    ///     Unloads a texture attained through any previous load call.
    /// </summary>
    /// <returns>
    ///     <c>false</c> if the texture is locked or pending unload, otherwise <c>true</c>.
    /// </returns>
    public bool unload_texture(FhTexture texture) {
        return !texture.is_loaded() || (impl_handle.get(out IFhResourceLoaderImpl? impl) && impl.unload_texture(texture));
    }
}
