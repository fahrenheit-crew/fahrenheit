// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

/// <summary>
///     Provides hashes which uniquely identify mod sets with respect to various properties.
/// </summary>
internal sealed class FhHasher {

    internal const string DEFAULT_HASH_VALUE = "0";

    public FhHasher() { }

    /// <summary>
    ///     Gets the hash which uniquely identifies the mod set for this session.
    /// </summary>
    internal string ModSetHash  => field ??= _init_hash_mods();

    /// <summary>
    ///     Gets the hash which uniquely identifies the current save set.
    /// </summary>
    internal string SaveSetHash => field ??= _init_hash_saves();

    /// <summary>
    ///     Computes a hash which uniquely identifies the current mod set.
    /// </summary>
    /// <remarks>
    ///     The hash is sensitive to load order.
    /// </remarks>
    private static string _init_hash_mods() {
        StringBuilder mods = new();

        foreach (FhManifest manifest in FhEnvironment.Manifests) {
            mods.Append(manifest.Id);
        }

        if (mods.Length == 0)
            return DEFAULT_HASH_VALUE;

        ReadOnlySpan<byte> input = Encoding.UTF8.GetBytes(mods.ToString());
                Span<byte> hash  = stackalloc byte[16];

        hash.Clear();
        Blake3.Hasher.Hash(input, hash);

        return Convert.ToHexString(hash);
    }

    /// <summary>
    ///     Computes the hash which uniquely identifies the current save set.
    /// </summary>
    /// <remarks>
    ///     The hash ignores mods that do not define <see cref="FhManifestFlags.SEPARATE_SAVES"/>.
    ///     In all other respects, it is identical to the mod set hash.
    /// </remarks>
    private static string _init_hash_saves() {
        StringBuilder stateful_mods = new();

        foreach (FhManifest manifest in FhEnvironment.Manifests) {
            if (manifest.Flags.HasFlag(FhManifestFlags.SEPARATE_SAVES))
                stateful_mods.Append(manifest.Id);
        }

        if (stateful_mods.Length == 0)
            return DEFAULT_HASH_VALUE;

        ReadOnlySpan<byte> input = Encoding.UTF8.GetBytes(stateful_mods.ToString());
                Span<byte> hash  = stackalloc byte[16];

        hash.Clear();
        Blake3.Hasher.Hash(input, hash);

        return Convert.ToHexString(hash);
    }

}
