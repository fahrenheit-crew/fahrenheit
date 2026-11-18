# Mod structure

A Fahrenheit mod is contained in its own folder, stored under [`mods`](Install-structure#mods) in the installation directory.

This entry describes its basic building blocks.

## Mod ID

The most basic property of any Fahrenheit mod is its ID. A mod's ID:
- **Must** be globally unique.
- **Must not** be changed once chosen.
- **Must not** contain spaces or characters that would be invalid in a Windows path string.

A few examples of valid IDs:
- `myname.mymod`
- `mymod5000`
- `my-very-creative-name`

> [!IMPORTANT]
> Where-ever the documentation refers to `{mod_id}` or "the mod's ID", this is what is called for.
>
> The mod's folder name and manifest `Id` **must** be equal to the mod ID.

## Mod files

> [!IMPORTANT]
> **Do not** modify or delete any mod files or subfolders unless explicitly instructed to by the mod's developer.
>
> **Modifying mod files is strictly unsupported**. Each is necessary for a mod to properly load and function.

### `{mod_id}.manifest.json`
Describes the mod to Fahrenheit, allowing it to be loaded.
- Each mod, whether purely file-based or not, **must** contain a manifest. See [Manifests](Manifests).

### `{mod_id}.dll`
Contains the mod's executable code. If the mod changes game logic in any way, this is where it is done.

### `{mod_id}.deps.json`
This file is a build artifact that encodes information about the mod DLL's dependencies.

### `{mod_id}.runtimeconfig.json`
This file is a build artifact that encodes information required to load the mod's DLL.

### `{mod_id}.pdb`
Contains debugging information for the mod's DLL. This is required to properly analyze crashes.

There may be supplementary files and folders beyond this, such as the mod's dependencies. 

These do not belong to Fahrenheit, but are still required.

## Mod subfolders

### `efl`
Keeps data files which will override the base game. Fahrenheit will load them with its built-in external file loader.

It can have two subfolders:
- `x`, for FF X (`FFX_Data.vbf`)
- `x2`, for FF X-2/X-2 LM (`FFX2_Data.vbf`)

Each of these is taken as the root of their respective VBF. From them, mirror the folder and file paths of the VBF. For example: 
- `ffx_ps2\ffx\master\jppc\battle\kernel\takara.bin` -> `efl\x\ffx_ps2\ffx\master\jppc\battle\kernel\takara.bin`
- `ffx_ps2\ffx2\master\new_uspc\menu\menutb.bin` -> `efl\x2\ffx_ps2\ffx2\master\new_uspc\menu\menutb.bin`


### `lang`
Keeps localization data for your mod, to be used with Fahrenheit's localization API.
- It contains **one folder per module**, named after its full type name. Each contains one or more localization data files.
- See generally [Localization](Localization) for more details.

### `resources`
This folder is provided for the mod to use as it deems fit. Its contents are not prescribed in any way or form.
