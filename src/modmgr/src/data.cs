// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Tools.ModManager;

/// <summary>Describes a mod, including non-manifest data.</summary>
internal struct ModData {
    public FhManifest manifest;
    public bool enabled;
    public long size_on_disk;

    public string get_size_string() {
        float kilobyte = 1024f;
        float megabyte = 1024f * kilobyte;
        float gigabyte = 1024f * megabyte;

        if (size_on_disk > gigabyte) {
            return $"{size_on_disk / gigabyte:F1} GB";
        }

        if (size_on_disk > megabyte) {
            return $"{size_on_disk / megabyte:F1} MB";
        }

        if (size_on_disk > kilobyte) {
            return $"{size_on_disk / kilobyte:F1} KB";
        }

        return $"{size_on_disk} B";
    }
}
