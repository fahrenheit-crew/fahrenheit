// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

/// <summary>
///     A disposable structure which changes the protection flags on a memory location
///     while it remains in scope, reverting to the original flags when disposed.
/// </summary>
/// <remarks>
///     This is useful for rewriting constants embedded into read-only process sections,
///     which would normally cause an access violation.
/// </remarks>
[SupportedOSPlatform("windows5.1.2600")]
public unsafe readonly ref struct FhVirtualProtectScope<T> where T : unmanaged {
    private readonly T*   _address;
    private readonly uint _protection_flags_old;

    /// <summary>
    ///     Changes the memory protection mode beginning at the given address.
    /// </summary>
    /// <remarks>
    ///     The protection of any memory page containing one or more bytes from <paramref name="address"/> to
    ///     (<paramref name="address"/> + sizeof(<typeparamref name="T"/>)) will be changed.
    /// </remarks>
    /// <param name="address">The address to start changing the memory protection mode from.</param>
    /// <param name="protection_mode">The memory protection mode to set for the targeted memory region.</param>
    public FhVirtualProtectScope(T* address, uint protection_mode) {
        uint flOldProtect;
        Windows.VirtualProtect(address, (nuint) sizeof(T), protection_mode, &flOldProtect);

        _address              = address;
        _protection_flags_old = flOldProtect;
    }

    /// <summary>
    ///     Reverts the memory protection mode of the targeted region to its previous state.
    /// </summary>
    public void Dispose() {
        uint discard;
        Windows.VirtualProtect(_address, (nuint) sizeof(T), _protection_flags_old, &discard);
    }
}

