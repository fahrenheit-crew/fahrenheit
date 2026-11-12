// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

#pragma once

void bridge_ui(); // Displays the Bridge user interface.

bool bridge_check_vcredist_x86(); // Determines whether the x86 VC Redistributable is installed on this system.
bool bridge_check_vcredist_x64(); // Determines whether the x64 VC Redistributable is installed on this system.
bool bridge_check_nethost_x86();  // Determines whether the x86 .NET Runtime is installed on this system.
bool bridge_check_nethost_x64();  // Determines whether the x64 .NET Runtime is installed on this system.
