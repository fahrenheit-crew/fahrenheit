// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Tools.EEdit;

/* [fkelava 16/02/26 00:36]
 * I couldn't be arsed to implement this whole thing from scratch like I did the Win32 stub.
 *
 * For file dialogs I opted for slightly modernized bindings for NativeFileDialog.
 * https://github.com/lofcz/NativeFileDialogCore
 */

internal static partial class UI {
    /// <summary>
    ///     Handles a file open request using a native file dialog. If successful,
    ///     <paramref name="opened_file"/> contains the user-selected file.
    /// </summary>
    private static bool _dialog_fopen([NotNullWhen(true)] out FileStream? opened_file) {
        opened_file = default;

        DialogResult result = Dialog.FileOpenEx(
            filterList:        "*.bin",
            defaultPath:       null,
            dialogTitle:       "Open Excel File",
            fileNameLabel:     null,
            selectButtonLabel: null,
            cancelButtonLabel: null,
            parentWindow:      null);

        if (!result.IsOk || result.Path == null)
            return false;

        try {
            opened_file = File.OpenRead(result.Path);
            return true;
        }
        catch (Exception e) { // TODO: unfuck
            Console.WriteLine(e.ToString());
            return false;
        }
    }

    /// <summary>
    ///     Handles a file save request using a native file dialog. If successful,
    ///     the file is written to the path the user selects.
    /// </summary>
    private static bool _dialog_fsave(FileStream file_to_save) {
        DialogResult result = Dialog.FileSave(
            filterList:  "*.bin",
            defaultPath: null);

        if (!result.IsOk || result.Path == null)
            return false;

        try {
            using (FileStream target = File.OpenWrite(result.Path)) {
                file_to_save.CopyTo(target);
            }

            return true;
        }
        catch (Exception e) { // TODO: unfuck
            Console.WriteLine(e.ToString());
            return false;
        }
    }
}
