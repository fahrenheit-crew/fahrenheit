// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

using System.Diagnostics;
using System.Text;

namespace Fahrenheit.Tools.ModManager;

internal static partial class ModManager {
    private static void _render_menu_bar() {
        if (!ImGui.BeginMenuBar()) return;

        if (ImGui.BeginMenu("Mods")) {
            ImGui.MenuItem("Install External Mod");
            ImGui.MenuItem("Import EFL Mod");
            ImGui.Separator();

            ImGui.MenuItem("Import Mod Pack");
            ImGui.MenuItem("Export Mod Pack");

            ImGui.EndMenu();
        }

        if (ImGui.BeginMenu("Play")) {
            if (ImGui.MenuItem("Final Fantasy X"))
                play(FhGameId.FFX);

            if (ImGui.MenuItem("Final Fantasy X-2"))
                play(FhGameId.FFX2);

            if (ImGui.MenuItem("Final Fantasy X-2 Last Mission"))
                play(FhGameId.FFX2LM);

            if (Config.show_advanced_options) {
                ImGui.Separator();

                if (ImGui.MenuItem("Final Fantasy X (Debug)"))
                    play(FhGameId.FFX, debug: true);

                if (ImGui.MenuItem("Final Fantasy X-2 (Debug)"))
                    play(FhGameId.FFX2, debug: true);

                if (ImGui.MenuItem("Final Fantasy X-2 Last Mission (Debug)"))
                    play(FhGameId.FFX2LM, debug: true);
            }

            ImGui.EndMenu();
        }

        if (ImGui.BeginMenu("Manager")) {
            ImGui.MenuItem("About");

            if (ImGui.MenuItem("Settings") && !_show_config_window) {
                show_config();
            }

            ImGui.EndMenu();
        }

        ImGui.EndMenuBar();
    }

    private static List<string> get_args_for_play(FhGameId game, bool debug) {
        List<string> args = [ ];

        string exe = FhUtil.select(
            $"{Config.path_game}/FFX.exe",
            $"{Config.path_game}/FFX-2.exe",
            $"{Config.path_game}/FFX-2.exe"
        );

        args.Add(exe);

        if (debug) {
            args.Add("--extdbg");
            args.Add("--wait");
        }

        if (game == FhGameId.FFX2LM) {
            args.Add("-- FFX2_LASTMISSION");
        }

        return args;
    }

    [SupportedOSPlatform("windows")]
    private static void play_windows(FhGameId game, bool debug) {
        ProcessStartInfo info = new() {
            WorkingDirectory = FhEnvironment.Finder.Binaries.FullName,
            FileName         = "fhstage0.exe",
        };

        foreach (string arg in get_args_for_play(game, debug)) {
            info.ArgumentList.Add(arg);
        }

        debug_log.Add($"Starting {game}...");

        try {
            Process.Start(info);
        }
        catch (Exception e) {
            debug_log.Add($"Game launch failed: {e.Message}");
        }
    }

    [SupportedOSPlatform("linux")]
    private static void play_linux(FhGameId game, bool debug) {
        ProcessStartInfo info = new() {
            WorkingDirectory = FhEnvironment.Finder.Binaries.FullName,
            FileName         = Config.path_proton_exe,
            Environment = {
                { "STEAM_COMPAT_DATA_PATH",           Config.path_proton_prefix },
                { "STEAM_COMPAT_CLIENT_INSTALL_PATH", Config.path_proton_install },
                { "WINEDLLOVERRIDES", "dbghelp=n,b" },
            },
        };

        info.ArgumentList.Add("run");
        info.ArgumentList.Add("fhstage0.exe");

        string fh_args = string.Join(' ', get_args_for_play(game, debug));

        info.ArgumentList.Add(fh_args);

        debug_log.Add($"Starting {game}...");

        try {
            Process.Start(info);
        }
        catch (Exception e) {
            debug_log.Add($"Game launch failed: {e.Message}");
        }
    }

    private static void play(FhGameId game, bool debug = false) {
        if (OperatingSystem.IsWindows()) {
            play_windows(game, debug);
            return;
        }

        if (OperatingSystem.IsLinux()) {
            play_linux(game, debug);
            return;
        }

        debug_log.Add("No launch method found for current operating system.");
    }
}
