// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX;

[StructLayout(LayoutKind.Sequential)]
public struct SphereGridNodeType {
    public ExcelSimplifiableTextOffset name;
    public ExcelSimplifiableTextOffset help;
    public SphereTargets               sphere_effect;
    public T_XCommandId                ability_id;
    public ushort                      amount;
    public ushort                      icon_id;
}
