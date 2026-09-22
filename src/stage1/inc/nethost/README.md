.NET hosting using `hostfxr` and `nethost` requires certain headers and libraries that are shipped as part of the .NET SDK.

These are:
- `hostfxr.h`
- `coreclr_delegates.h`
- `nethost.h`

and `nethost.dll`, with import library `nethost.lib`.

However, _locating_ these headers and libraries programmatically is difficult to impossible from a `.vcxproj`. The MSBuild
properties required to locate it are only available through the .NET SDK props/targets.

As a workaround for this, the official .NET samples have devised a (in their own words) "relatively complicated" build system
in which they suppress building the `.vcxproj` normally and drive the build from a .NET project target.

"Relatively complicated" is, put charitably, an understatement. We must co-opt the separate .NET project,
but use it merely to copy the headers and libraries from your local .NET SDK install to the right places. See `src/Fahrenheit.NativeDeps.csproj`.

> [!WARNING]
> Do not commit the copied-over headers or libraries to this repository.

See generally the [.NET NativeHost sample](https://github.com/dotnet/samples/blob/86ff8487361a6f32549d9c9ab8b14dde55c643cf/core/hosting/readme.md) 
and its [project file](https://github.com/dotnet/samples/blob/86ff8487361a6f32549d9c9ab8b14dde55c643cf/core/hosting/src/NativeHost/NativeHost.csproj).
