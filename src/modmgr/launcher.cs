// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Tools.ModManager;

/// <summary>
///    Validates the Fahrenheit install and launches FFX/FFX-2 through fhstage0.exe.
///    Supports passing in optional command-line arguments to fhstage0.exe 
/// </summary>
internal static class FhGameLauncher {
    internal static ResultsMessage launch(FhGameId target, string configured_game_directory, string[] args) {
        if (!OperatingSystem.IsWindows()) {
            return new(false, "Launching Final Fantasy X/X-2 is currently supported only on Windows.");
        }

        string game_directory;

        try {
            game_directory = FhModManagerSettingsStore.normalize_path(configured_game_directory);
        }
        catch (Exception exception) {
            return new(false, $"The configured game directory is invalid.\n\n{exception.Message}");
        }

        string game_executable = target switch {
            FhGameId.FFX    => "FFX.exe",
            FhGameId.FFX2   or
            FhGameId.FFX2LM => "FFX-2.exe",
            _               => throw new ArgumentOutOfRangeException(nameof(target))
        };

        string fahrenheit_bin       = AppContext.BaseDirectory;
        string stage0_path          = Path.Join(fahrenheit_bin, "fhstage0.exe");
        string stage1_path          = Path.Join(fahrenheit_bin, "fhstage1.dll");
        string game_path            = Path.Join(game_directory, game_executable);
        List<string> missing_files  = [];

        foreach (string required_path in new[] { stage0_path, stage1_path, game_path }) {
            if (!File.Exists(required_path)) {
                missing_files.Add(required_path);
            }
        }

        if (missing_files.Count > 0) {
            return new(false, $"Fahrenheit installation is incomplete.\n\nMissing files:\n{string.Join('\n', missing_files)}");
        }

        string relative_game_path = Path.GetRelativePath(fahrenheit_bin, game_path);

        ProcessStartInfo start_info = new() {
            FileName         = stage0_path,
            WorkingDirectory = fahrenheit_bin,
            UseShellExecute  = true,
        };

        start_info.ArgumentList.Add(relative_game_path);

        foreach (string arg in args) {
            start_info.ArgumentList.Add(arg);
        }
        
        try {
            Process? process = Process.Start(start_info);

            if (process == null) {
                return new(false, "Windows did not create the Fahrenheit Stage 0 process.");
            }

            string msg = args.Contains("--debug", StringComparer.OrdinalIgnoreCase)
                ? $"Started {game_executable} through Fahrenheit in debug mode.\n\nAttach a debugger, then press any key in the Stage 0 console window."
                : $"Started {game_executable} through Fahrenheit.";

            return new(true, msg);
        }
        catch (Exception exception) {
            return new(false, $"Fahrenheit could not start {game_executable}.\n\n{exception.Message}");
        }
    }
}

/// <summary>
///    Provides a method to open an arbitrary folder in the OS's file browser (Explorer on Windows, xdg-open elsewhere).
/// </summary>
internal static class FhShell {
    internal static bool try_open_folder(string path, out string error) {
        error = "";

        if (!Directory.Exists(path)) {
            error = $"The folder does not exist:\n{path}";
            return false;
        }

        try {
            ProcessStartInfo start_info = OperatingSystem.IsWindows()
                ? new ProcessStartInfo { FileName = path, UseShellExecute = true }
                : new ProcessStartInfo { FileName = "xdg-open" };

            if (!OperatingSystem.IsWindows()) {
                start_info.ArgumentList.Add(path);
            }

            Process.Start(start_info);

            return true;
        }
        catch (Exception exception) {
            error = $"The folder could not be opened.\n\n{exception.Message}";
            return false;
        }
    }
}
