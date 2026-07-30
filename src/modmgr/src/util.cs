// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

using System.IO;

namespace Fahrenheit.Tools.ModManager;

public static class ModManagerUtil {
    public static long get_folder_size(DirectoryInfo dir) {
        long size = 0;

        foreach (FileInfo file in dir.GetFiles()) {
            size += file.Length;
        }

        foreach (DirectoryInfo subdir in dir.GetDirectories()) {
            size += get_folder_size(subdir);
        }

        return size;
    }
}
