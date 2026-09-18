// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

/* [fkelava 23/6/25 13:47]
 * This is exclusively permitted to the runtime library so it can fulfill the contracts specified
 * in the Fahrenheit API. If you need access to something currently marked internal, open an issue
 * or contact the developers and explain the use case instead of extending IVT to your mod.
 */

[assembly: InternalsVisibleTo("fhr")]

namespace Fahrenheit;

/// <summary>
///     The accessor for objects and helpers that compose the public Fahrenheit API.
/// </summary>
public static class FhApi {
    public static readonly FhMods         Mods         = new();
    public static readonly FhLocalization Localization = new();
    public static readonly FhResources    Resources    = new();
    public static readonly FhGui          Gui          = new();
    public static readonly FhAudio        Audio        = new();
    public static readonly FhInput        Input        = new();
    public static readonly FhEvents       Events       = new();
    public static readonly FhSaves        Saves        = new();
}

/// <summary>
///     An accessor for objects private to the Fahrenheit core and runtime libraries.
/// </summary>
internal static class FhInternal {
    // The initialization order here is not incidental. Objects higher in the list may not rely on objects below them in their constructor.
    public static readonly FhLogger   Log      = new("core.log");
    public static readonly FhLoader   Loader   = new();
    public static readonly FhMethods  Methods  = new();
    public static readonly FhHasher   Hasher   = new();
    public static readonly FhState    State    = new();
    public static readonly FhSettings Settings = new();
}
