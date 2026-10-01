// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX;

[StructLayout(LayoutKind.Sequential, Size = 0xBC)]
public struct RcEffectTask { }

[StructLayout(LayoutKind.Sequential, Size = 0xD0)]
public struct RcEffectModel { }

[StructLayout(LayoutKind.Sequential, Size = 0x90)]
public struct RcEffectAnimation { }

[StructLayout(LayoutKind.Sequential, Size = 0xB0)]
public struct RcEffectParticle { }

[StructLayout(LayoutKind.Explicit, Size = 0x490)]
public struct RcEffectObj {
    [FieldOffset(0x1B0)] Matrix4x4 mat_local;
    [FieldOffset(0x1F0)] Matrix4x4 mat_world;
    [FieldOffset(0x230)] Matrix4x4 mat_parent;

    [FieldOffset(0x280)] RcEffectParticle  particle;
    [FieldOffset(0x330)] RcEffectAnimation animation;
    [FieldOffset(0x3C0)] RcEffectModel     model;
}
