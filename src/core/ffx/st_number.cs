// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX;

/// <summary>
///     Responsible for displaying commands the player has unlocked in menus.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct StNumber {
    /// <summary>
    ///     Can either be a valid <see cref="Ids.PlySaveId"> or submenu ID.
    /// </summary>
    public byte   category;
    public byte   type;
    public ushort command_id; // Can also be an Aeon ID if category is 0x1 (Yuna)
}
