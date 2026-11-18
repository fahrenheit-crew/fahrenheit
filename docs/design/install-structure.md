# Install structure

Fahrenheit is a fully self-contained modding system. That is to say:
- all of its executables and data are contained in its installation directory.
- it does not write any information or leave any leftovers on your system outside of its installation directory.

It is therefore useful to understand how this directory is laid out.

## Installation directory layout
The installation directory contains the following subdirectories:

### `bin`
Keeps the Fahrenheit libraries and binaries, and its dependencies.

> [!IMPORTANT]
> **Do not** modify any part of this folder. This is strictly unsupported and will cause errors.
>
> When updating, you must delete this folder before extracting a new release. Mixing releases will cause errors.

### `cache`
Keeps temporary data that Fahrenheit keeps around to avoid continually re-fetching it.
- You may delete this folder at any time, but doing so may increase waiting times for certain operations.
- It is reasonable to delete this folder when updating Fahrenheit.

### `config`
Keeps mod(ule) configuration files; both user-visible and private mod settings.

> [!IMPORTANT]
> Deleting this folder will reset all entries in the mod settings UI to their default.

### `crash`
Keeps crash dumps, which contain essential debugging information about game crashes.
- Contains **one core dump per crash** named after the time the crash occurred. 
- When reporting an error, you will likely be expected to **submit the relevant core dump**.

### `logs`
Keeps module logging output.
- Contains **one folder for each game session**.
- In each folder, contains **one log file per loaded module** named after the module, **plus framework logs**.
- To provide logs with a bug report, simply ZIP up the folder for the session in which you experienced the issue.
- Logs are **not** automatically cleaned. You can delete them as and when you see fit.

### `mods`
Keeps your mods.
- Contains **one folder per mod**, plus a **load order file**.
- For an individual mod directory's structure, see [Mod structure](mod-structure).
> [!IMPORTANT]
> No mods will load if the load order file is missing. **Do not** delete it.

### `saves`
Keeps your save sets, which keep your save games.
- Contains **one folder for each unique save set hash**, each of which contains **one folder per save set**.
- Beneath the set folder, the layout is identical to the game's default save directory.

### `state`
Keeps your global and local state, which is per-module supplementary information. See [State](state).
- Contains **a global state folder** and **one folder for each unique save set hash**, each containing **one folder per save set**.

> [!IMPORTANT]
> Saves and state are intrinsically paired. Deleting or modifying one without the other can cause unexpected behavior.
