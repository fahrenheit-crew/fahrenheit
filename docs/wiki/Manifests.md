A manifest describes a mod and any specific behaviors it demands from the framework.

## Manifest format
The manifest is a [JSON file](https://www.json.org/json-en.html) containing one instance of [`FhManifest`](https://github.com/fahrenheit-crew/fahrenheit/blob/main/src/core/module.cs). For example:
```json
{
    "Id":"fhefp",
    "Name":"Extensions, Fixes, and Patches",
    "Desc":"Provides universal fixes for common base game issues so mods do not need to reimplement them.",
    "Authors":"The Fahrenheit contributors",
    "Version":"1.0.0-alpha+4a034aea570583ecda7ff74df17944570594139d",
    "Link":"https://github.com/fahrenheit-crew/fh-mods-efp",
    "Dependencies":[],
    "Flags":"NONE"
}
```
> [!IMPORTANT]
> Every Fahrenheit mod **must** have a manifest. The game will not launch otherwise.
>
> A mod's manifest **must** be stored directly under its folder, with the exact name `{mod_id}.manifest.json`.
>
> The manifest's `Id` value **must** be equal to the mod's ID.

## Manifests for code mods

The [Fahrenheit SDK](SDK) automatically generates your mod's manifest based on details from your [C# project file](https://learn.microsoft.com/en-us/dotnet/core/project-sdk/overview#project-files) at build time.

The following manifest properties map to the following project file properties:
- `Id` -> `AssemblyName`
- `Name` -> `AssemblyTitle`
- `Desc` -> `Description`
- `Authors` -> `Authors`
- `Version` -> `Version`
- `Link` -> `FhManifestHomePage` if specified, otherwise `RepositoryUrl`
- `Flags` -> `FhManifestFlags`

> [!IMPORTANT]
> It is advisable that you provide all attributes, but the only mandatory ones are `AssemblyName` and `FhManifestFlags`.
>
> The `AssemblyName` **must** be equal to the mod's ID.

For example, the manifest above was generated from the following project file:
```xml
<Project Sdk="Fahrenheit.Sdk/1.0.0-alpha12">
    <PropertyGroup>
        <AssemblyName>fhefp</AssemblyName>
        <AssemblyTitle>Extensions, Fixes, and Patches</AssemblyTitle>
        <Product>Extensions, Fixes, and Patches</Product>
        <Description>Provides universal fixes for common base game issues so mods do not need to reimplement them.</Description>
        <Version>1.0.0-alpha</Version>
        <Authors>The Fahrenheit contributors</Authors>
        <RepositoryType>git</RepositoryType>
        <RepositoryUrl>https://github.com/fahrenheit-crew/fh-mods-efp</RepositoryUrl>
        <FhManifestFlags>NONE</FhManifestFlags>
    </PropertyGroup>
</Project>
```

## Manifests for file-based mods

For file-based or existing mods, manifests **must** be created by hand.
