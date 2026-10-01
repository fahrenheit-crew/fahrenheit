The "Stage 1" loader is responsible for initializing .NET and subsequently Fahrenheit.

In order, it:
- Undoes all changes to the IAT and PE header made by the ["Stage 0" loader](https://github.com/fahrenheit-crew/fahrenheit/tree/main/src/stage0).
    - This is done using MS Detours' [``DetourRestoreAfterWith``](https://github.com/microsoft/detours/wiki/DetourRestoreAfterWith).
- If necessary, overrides the target binary's default exception handler.
- Hooks the entry-point of the target binary.
- Attaches the target process to the "Stage 0" loader's console.
- Using the [.NET Hosting API](https://learn.microsoft.com/en-us/dotnet/core/tutorials/netcore-hosting), loads the .NET Runtime into the process.
- Jumps to Fahrenheit's boot function, loading Fahrenheit mods.
- After returning to native code, calls the original entry-point of the target, proceeding as normal.
