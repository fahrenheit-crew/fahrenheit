// SPDX-License-Identifier: MIT

namespace Fahrenheit;

/// <summary>
///     Provides hashes which uniquely identify mod sets with respect to various properties.
/// </summary>
internal sealed class FhHasher {

    internal const string DEFAULT_HASH_VALUE = "0";

    internal readonly string ModSetHash;
    internal readonly string SaveSetHash;

    public FhHasher() {
        ModSetHash  = _init_mod_set_hash ();
        SaveSetHash = _init_save_set_hash();
    }

    /// <summary>
    ///     Computes a hash which uniquely identifies the current mod set, accounting for load order.
    /// </summary>
    private static string _init_mod_set_hash() {
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
    ///     Computes the hash which uniquely identifies the current save set. That is,
    ///     mods that do not define <see cref="FhManifestFlags.SEPARATE_SAVES"/> are disregarded.
    /// </summary>
    private static string _init_save_set_hash() {
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
