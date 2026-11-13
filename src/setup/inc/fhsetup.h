// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

#pragma once

void bridge_ui(); // Displays the Bridge user interface.

enum BRIDGE_REDIST_TYPE {
    X86   = 1,
    X64   = 2,
    ARM64 = 3
};

bool bridge_check_vcredist(BRIDGE_REDIST_TYPE type); // Determines whether the specified VC Redistributable is installed on this system.
bool bridge_check_nethost (BRIDGE_REDIST_TYPE type); // Determines whether the specified .NET Runtime is installed on this system.
