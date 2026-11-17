Fahrenheit is known to run at least on Windows and Linux systems.

## Prerequisites

On Windows systems, you require:
- The [.NET 10 runtime](https://dotnet.microsoft.com/en-us/download/dotnet/10.0), in both x86 and x64 versions.
- The [latest supported Visual C++ Redistributable](https://learn.microsoft.com/en-us/cpp/windows/latest-supported-vc-redist?view=msvc-170#latest-supported-redistributable-version), in both x86 and x64 versions.

On Linux systems, you require:
- To run the tools, the [.NET 10 runtime](https://dotnet.microsoft.com/en-us/download/dotnet/10.0), x64 version.
    - `dotnet {TOOL_NAME}.dll` will start a Linux-native version of the tool.
- Proton and Protontricks.
    - Use Protontricks to install the prerequisites for Windows systems into the game's Proton prefix.

## Installing

Extract the contents of a [release](https://github.com/fahrenheit-crew/fahrenheit/releases) into a new folder somewhere on your disk.

## Updating

Delete the `bin` folder from your current installation, then extract the contents of a release into it.

## Running

**Read the [compatibility guide](https://github.com/fahrenheit-crew/fahrenheit/wiki/Compatibility-guide) before running.**

Start a terminal in `bin`, and execute `.\fhstage0.exe {PATH_TO_GAME_EXECUTABLE}`.
- On Linux, use Proton to do the same.
- For Last Mission, `.\fhstage0.exe {PATH_TO_FFX-2.exe} FFX2_LASTMISSION`.

> [!TIP]
> For faster launching, create a shortcut or add Fahrenheit as a non-Steam game.

## Uninstalling

Just delete the folder you extracted the release to.

> [!IMPORTANT]
> Remember that your configuration, save games and mod state are saved under that folder!
>
> Make a backup of `config`, `saves` and `state` before deleting if you want to reinstall.