This guide will ensure you are ready to build Fahrenheit mods by guiding you through building the official mod template.

> [!IMPORTANT]
> If you get stuck or confused, don't worry! Contact us in the [Cid's Salvage Ship](https://discord.gg/AGx2grw9nD) Discord for assistance.

> [!IMPORTANT]
> Always read notes marked 'Important'. They are critical to your understanding of a page's subject.

## Preparation
Follow the 'Managed component prerequisites' section of the [build and deployment guide](Build-and-deployment-guide#managed-component-prerequisites).

## Building the template mod
Navigate to the [template mod](https://github.com/fahrenheit-crew/fh-mods-template), then:
- Click 'Use this template'.
- Clone the repository you created.
- Navigate to the folder you cloned to.

> [!IMPORTANT]
> **Do not** click 'Fork' instead of 'Use this template'.

> [!IMPORTANT]
> You can build in two configurations: **Debug** or **Release**.
> 
> - **Debug** is used when testing, and allows you to use a debugger.
> - **Release** is used when preparing a mod for distribution.
>
> Paths and commands depend on your choice, and are marked `{D|R}` in this guide. The `D` option applies when building in Debug, and the `R` option when building in Release.

What follows next depends on the tool you chose in the build and deployment guide.
| Build tool      | Instructions |
| --------------  | ------------ |
| CLI .NET SDK    | At a terminal, `dotnet build -c {Debug\|Release} .\Fahrenheit.Mods.Template.slnx`.  |
| Visual Studio   | Open `Fahrenheit.Mods.Template.slnx`, then `Build > Build Solution` or (Ctrl+Shift+B). |
| JetBrains Rider | Open `Fahrenheit.Mods.Template.slnx`, then `Build > Build Solution` or (Ctrl+Shift+B). |

The build should succeed. You will find the result in `artifacts/deploy/{dbg|rel}/mods`.

> [!IMPORTANT]
> Read [Mod structure](Mod-structure) at this point to familiarize yourself with what each file does.
>
> At the very least, you should know what the 'mod ID' is to proceed. In this example, it is `fhtemplate`.

## Testing the template mod

### Installing Fahrenheit
Open `src/Fahrenheit.Mods.Template.csproj`, and take note of the `Project` line. For example:
```xml
<Project Sdk="Fahrenheit.Sdk/1.0.0-alpha11">
```
Note the version of Fahrenheit you built for. It is the string that follows the forward slash- in this case, `1.0.0-alpha11`.

Install the corresponding version of Fahrenheit according to the [installation guide](Installation-guide#installing).

### Installing the mod
Copy the `mods` folder under `artifacts/deploy/{dbg|rel}` to the Fahrenheit [installation directory](Install-structure).

Create an empty file called `loadorder` under `mods` if it does not already exist.

In it, add the mod ID on an empty line, then save.

### Launching Fahrenheit
Run Fahrenheit according to the [installation guide](Installation-guide#running).

If you see `Template Mod` in the loaded list in the top left of the screen, you've successfully built and launched your first mod.

## Next steps
Now that you know how to build a Fahrenheit mod, you can begin customizing the template!

Begin by changing its mod ID and manifest properties. See [Manifests](Manifests). After that, the world is your oyster. Have fun!