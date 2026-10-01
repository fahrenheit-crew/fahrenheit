Ensure the folder you cloned to does not contain special characters. This can cause build errors.

Fahrenheit is generally subdivided into two parts:
- The native (C++) components. These are the Stage 0 (`src/stage0`) and Stage 1 (`src/stage1`) loaders.
- The managed (.NET) components. These are the core library, SDK, runtime and tools, and any mods.

All systems can build the managed components. Windows systems can perform full builds.

## Managed component build

One of the following is required:
- A .NET IDE such as [JetBrains Rider](https://www.jetbrains.com/rider/) or [Visual Studio](https://visualstudio.microsoft.com/).
- The CLI [.NET SDK](https://dotnet.microsoft.com/en-us/download).

To build them all:
- With a .NET IDE, build the solution.
- With the .NET SDK, run `dotnet build -c {Debug|Release} .\Fahrenheit.slnx` in the folder you cloned Fahrenheit to.

Errors relating to the 'Stage 0' and 'Stage 1' loader projects may be ignored.

You can use a copy of the Stage 0 and Stage 1 loaders from the latest [release](https://github.com/fahrenheit-crew/fahrenheit/releases) on systems that cannot build them to compose a complete build.

## Native component or full build

Requires Visual Studio 2026 on Windows (IDE or Build Tools only) with the following workloads:
- C++ desktop development
- .NET desktop development (if performing a full build)

Once you have installed Visual Studio, enable the `vcpkg` package manager
by issuing `vcpkg integrate install` at a Developer PowerShell prompt.

Restart Visual Studio if it was open during this process.

To build at a Developer PowerShell:
```
msbuild .\Fahrenheit.slnx /t:Restore /p:Configuration=Release
msbuild .\Fahrenheit.slnx /p:Configuration=Release
```
Alternatively, `Build Solution` in the IDE performs everything for you.

For a Debug build, change the `Configuration` parameter to `Debug`.

## Deploying/installing and debugging a local build

To install/test your development build:
- Create a subfolder in your game directory (where ``FFX.exe`` is) named ``fahrenheit``.
- In the directory in which you cloned Fahrenheit, navigate to ``artifacts\deploy``, then ``dbg`` or ``rel`` depending on build type (Debug or Release).
- Copy its contents (the folders ``bin``, ``mods``, etc.) to the ``fahrenheit`` subfolder in the game directory.
- Create an empty, extensionless file called `loadorder` in the `mods` directory. Add the mod IDs of mods you wish to load, each on their own line.
- Open a terminal in ``fahrenheit/bin``, then issue ``.\fhstage0.exe ..\..\FFX.exe``.
- Debugging can be performed from Visual Studio. Attach to either ``fhstage0.exe`` or ``FFX.exe``,
and make sure to enable [mixed-mode debugging](https://learn.microsoft.com/en-us/visualstudio/debugger/how-to-debug-managed-and-native-code?view=vs-2022).