// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX2;

[StructLayout(LayoutKind.Sequential, Size = 0xBC)]
public struct RcEffectTask { }

[StructLayout(LayoutKind.Sequential, Size = 0xD0)]
public struct RcEffectModel { }

[StructLayout(LayoutKind.Sequential, Size = 0x90)]
public struct RcEffectAnimation { }

[StructLayout(LayoutKind.Sequential, Size = 0x130)]
public struct RcEffectParticle { }

[StructLayout(LayoutKind.Explicit, Size = 0x510)]
public struct RcEffectObj {
    [FieldOffset(0x280)] RcEffectParticle  particle;
    [FieldOffset(0x3B0)] RcEffectAnimation animation;
    [FieldOffset(0x440)] RcEffectModel     model;
}
