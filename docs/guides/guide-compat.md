# Compatibility guide

This page lists a number of commonly encountered issues when running Fahrenheit standalone, or with other tools.

## Game version support

Fahrenheit only works with **genuine, original Steam copies** of the [Final Fantasy X/X-2 HD Remaster](https://store.steampowered.com/app/359870/).

We offer **no support**, and provide **no guarantee of functionality** for any other variants of this game.

`v1.0.0-alpha11` and previous **only** support the March 27th, 2019 version.
- The Steam manifest ID is `5168047039601173807`.

`v1.0.0-alpha12` and onwards **only** support the latest Steam version (October 1st, 2026).
- The Steam manifest ID is `1235377778411650212`.

> [!IMPORTANT]
> Fahrenheit does not check the game's version or provenance. However, it relies on the game binary's _exact_ layout.
>
> Thus, any modified binaries will naturally cause errors that we cannot and will not fix.

## Windows systems

Do not run either the game or Fahrenheit as an administrator. This does not work and is **strictly unsupported**.

If you see a UAC icon (little blue shield) over your game executable:
- Right click it and go to `Properties > Compatibility`.
- Ensure `Run this program as an administrator` is **not checked**.

## Linux systems

`PROTON_USE_WINED3D=1` is not supported. Use DXVK instead.

## Other tools

### ffgriever's [External File Loader for FFX/FFX-2](https://www.nexusmods.com/finalfantasyxx2hdremaster/mods/150)
If Fahrenheit is in use, its integrated external file loader will override the ffgriever EFL.

Standalone EFL mods are trivially convertible to Fahrenheit format. We strongly recommend that you do so.

### [Untitled Project X](https://github.com/Kaldaien/UnX)
To the best of our knowledge, this mod is no longer supported by its creator.

We therefore do not offer support. Use mainline Special K instead, which we ensure compatibility with.

UnX has no functionality which is not replicable by a Fahrenheit mod.

### [Special K](https://github.com/SpecialKO/SpecialK)
You **must** pass `--extdbg` in the SK(IF) launch options when launching Fahrenheit.

If you do not, the game will deadlock on boot because both SK and Fahrenheit attempt to act as symbol and crash handlers.

### [RivaTuner Statistics Server](https://www.guru3d.com/download/rtss-rivatuner-statistics-server-download/)
You **must** enable 'Use Microsoft Detours API hooking' in `Setup > General > Compatibility properties`.

If you do not, Fahrenheit's user interface will not display, making you unable to save/load and interact with mods.

### [Malwarebytes Anti-Malware](https://www.malwarebytes.com/)
Malwarebytes' AMSI provider (`mbamsi32.dll`) may, in certain scenarios, bring the process down. This typically manifests as:
```
[!] SymUnloadModule64() failed with code 0x57.
```
The only currently known recourse is to disable it.
