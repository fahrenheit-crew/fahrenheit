// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Atel;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct AtelCallTarget {
    public delegate* unmanaged[Cdecl]<AtelBasicWorker*, int*, AtelStack*, void>  fnptr_init;
    public delegate* unmanaged[Cdecl]<AtelBasicWorker*, int*,             int>   fnptr_exec;
    public delegate* unmanaged[Cdecl]<AtelBasicWorker*, int*, AtelStack*, float> fnptr_retf;
    public delegate* unmanaged[Cdecl]<AtelBasicWorker*, int*, AtelStack*, int>   fnptr_reti;
}
