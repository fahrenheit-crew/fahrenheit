// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Runtime;

/* [fkelava 21/03/26 22:55]
 * The game utilizes the Phyre engine as a middleware for modern assets.
 * All are stored in pre-processed, platform-specific form.
 *
 * We would like to load them at runtime, that they need not be distributed
 * with mods. We have the game load them, since it can unwrap the Phyre containers.
 *
 * Regardless of what derived asset type is at hand (a texture, shader, model...),
 * all are loaded the same way, in the same base form- a Phyre 'cluster',
 * containing 'instance lists' of specialized objects.
 *
 * In Phyre, an asset/cluster can only be loaded once until released. The game
 * thus has a ref-counting system, the 'cluster manager'. This module interfaces with it.
 *
 * See:
 * - src/runtime/resload.cs
 * - src/core/petypes.cs
 * - src/core/petypes.g.cs
 * - src/core/peinterop.cs
 */

/// <summary>
///     A disposable structure returned when loading a <see cref="PCluster"/>
///     which ensures its release once the scope is exited.
/// </summary>
internal readonly unsafe ref struct FhPClusterScope(PCluster* ptr_cluster, FhPhyreLoaderModule owner) {
    private readonly PCluster*           _ptr_cluster = ptr_cluster;
    private readonly FhPhyreLoaderModule _owner       = owner;

    public bool enter(out PCluster* ptr_cluster) {
        return (ptr_cluster = _ptr_cluster) != null;
    }

    /* [fkelava 29/04/26 15:09]
     * Remember that structs always have an implicit parameterless constructor.
     * default(FhPClusterScope) will result in _both_ pointers being null, so both must be guarded.
     */

    public void Dispose() {
        if (_ptr_cluster == null) return;
        _owner?.cluster_release(_ptr_cluster);
    }
}

/// <summary>
///     Provides Phyre asset load services to the Fahrenheit runtime.
/// </summary>
[FhLoad(FhGameId.FFX | FhGameId.FFX2 | FhGameId.FFX2LM)]
public unsafe sealed class FhPhyreLoaderModule : FhModule {

    private readonly uint* _pp_cluster_mgr;

    public FhPhyreLoaderModule() {
        _pp_cluster_mgr = FhUtil.ptr_at<uint>(FhUtil.select(0x8CCA44, 0x9CFE48, 0x9CFE48));
    }

    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        return true;
    }

    /// <summary>
    ///     Attempts to load a Phyre asset at <paramref name="file_path"/>.
    ///     If successful, returns a scope over the resulting cluster.
    /// </summary>
    internal FhPClusterScope cluster_load(string file_path) {
        byte[] file_path_u8    = Encoding.UTF8.GetBytes(file_path);
        uint   ptr_cluster_mgr = *_pp_cluster_mgr;

        if (ptr_cluster_mgr == 0) {
            _logger.Warning($"ClusterManager not ready - {file_path}");
            return new FhPClusterScope(null, this);
        }

        PCluster* ptr_cluster;
        fixed (byte* ptr_path_u8 = file_path_u8) {
            ptr_cluster = FhCall.ClusterManager_loadPCluster.fnptr!(ptr_cluster_mgr, ptr_path_u8);
        }

        if (ptr_cluster == null) {
            _logger.Warning($"ClusterManager::loadPCluster failed for {file_path}");
            return new FhPClusterScope(null, this);
        }

        int rv_fixup = FhCall.PApplication_FixupClusters.fnptr!(&ptr_cluster, 1);

        if (rv_fixup != 0) {
            _logger.Warning($"PApplication::FixupClusters returned {rv_fixup} for {file_path}");
            return new FhPClusterScope(null, this);
        }

        return new FhPClusterScope(ptr_cluster, this);
    }

    /// <summary>
    ///     Releases a cluster attained by a previous call to <see cref="cluster_load"/>.
    /// </summary>
    internal void cluster_release(PCluster* ptr_cluster) {
        uint ptr_cluster_mgr = *_pp_cluster_mgr;
        FhCall.ClusterManager_releasePCluster.fnptr!(ptr_cluster_mgr, ptr_cluster);
    }
}
