// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX;

[ExtendedLayout(ExtendedLayoutKind.CStruct)]
public struct HelpText {
    public ExcelSimplifiableTextOffset help;
}

[ExtendedLayout(ExtendedLayoutKind.CStruct)]
public struct NameHelpText {
    public ExcelSimplifiableTextOffset command;
    public ExcelSimplifiableTextOffset help;
}
