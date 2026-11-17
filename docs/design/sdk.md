# SDK

Fahrenheit mods are built on the [Fahrenheit SDK](https://github.com/fahrenheit-crew/fahrenheit/tree/main/src/sdk), which ensures all necessary build properties and dependencies are set.

Depending on your exact needs, you will build your mod against either a global (official) or local (unofficial) SDK.
| SDK type                     | Use case                  |
| ---------------------------- | ------------------------- |
| Global (official)            | Building against a stable, official Fahrenheit. For modders. |
| Local (unofficial)           | Building against a local, modified Fahrenheit. For research/testing/Fahrenheit developers. |

> [!IMPORTANT]
> Use a local SDK **if, and only if**:
> - you are testing your mod against a pre-release of Fahrenheit.
> - you are preparing changes to Fahrenheit itself. 
>
> Such changes **must** be submitted as a pull request to become part of Fahrenheit and included in the next global SDK.

> [!IMPORTANT]
> Publicly distributed mods **must**, without exception, use the global SDK.
>
> **Only a mod built against the global, official SDK of a given Fahrenheit version is compatible with it.**

## Global/official SDKs
For each [official release](https://github.com/fahrenheit-crew/fahrenheit/releases) of Fahrenheit, the corresponding SDK is published as [`Fahrenheit.Sdk`](https://www.nuget.org/packages/Fahrenheit.Sdk) on NuGet.

To build a valid Fahrenheit mod, in your C# project file, you need only specify:
```xml
<Project Sdk="Fahrenheit.Sdk/1.0.0-alpha12">
    <!-- ... -->
</Project>
```
where `1.0.0-alpha12` is replaced by the version of Fahrenheit you wish to build against.

## Local/unofficial SDKs
Every time you build Fahrenheit locally, a matching SDK will be produced in `artifacts/pkg/{dbg,rel}`.

To enable use of local SDKs, set that folder as a local NuGet feed.
| Build environment            | Setting location          |
| ---------------------------- | ------------------------- |
| [Visual Studio](https://visualstudio.microsoft.com/) | `Tools > NuGet Package Manager > Package Manager Settings > Source` |
| [JetBrains Rider](https://www.jetbrains.com/rider/)  | `NuGet > Sources > Add feed` |

When you rebuild Fahrenheit (and thus the SDK), you **must** clear the local NuGet cache for the new SDK to be recognized.
| Build environment            | Setting location          |
| ---------------------------- | ------------------------- |
| [Visual Studio](https://visualstudio.microsoft.com/) | `Tools > NuGet Package Manager > Package Manager Settings > General > Clear NuGet local resources` |
| [JetBrains Rider](https://www.jetbrains.com/rider/)  | `NuGet > Folders > {Global Packages, Package Cache} > Clear Selected Caches` |

You may additionally have to restart your IDE after doing so.

> [!IMPORTANT]
> Ensure `<Version>` in `Directory.Build.props` when building locally is higher than any published official SDK.
>
> When building for distribution, you **must** ensure that the local SDK source is disabled in your IDE.
