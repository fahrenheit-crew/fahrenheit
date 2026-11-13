// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

#pragma once

void setup_ui_main(); // Displays the setup user interface.

enum FH_SETUP_REDIST_TYPE {
    X86   = 1,
    X64   = 2,
    ARM64 = 3
};

bool setup_check_vcredist(FH_SETUP_REDIST_TYPE type); // Determines whether the specified VC Redistributable is installed on this system.
bool setup_check_nethost (FH_SETUP_REDIST_TYPE type); // Determines whether the specified .NET Runtime is installed on this system.
