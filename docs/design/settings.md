# Settings

Settings are configurable, persistent, strongly typed values a mod can refer to for configuration.

## Settings API
Settings derive from [`FhSetting<T>`](https://github.com/fahrenheit-crew/fahrenheit/blob/ee0d0f9dc3d8e4ca7315cec3ebc834445721ee05/src/core/settings.cs#L240), where `T` is the type of value they contain.

Fahrenheit offers several basic derivations out of the box.
| Type name                    | Primitive type            |
| ---------------------------- | ------------------------- |
| [`FhSettingToggle`](https://github.com/fahrenheit-crew/fahrenheit/blob/ee0d0f9dc3d8e4ca7315cec3ebc834445721ee05/src/core/settings.cs#L415)              | `bool` |
| [`FhSettingNumber<T>`](https://github.com/fahrenheit-crew/fahrenheit/blob/ee0d0f9dc3d8e4ca7315cec3ebc834445721ee05/src/core/settings.cs#L369)           | Any [`INumber<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.numerics.inumber-1?view=net-10.0) |

## Setting providers
To use settings, create a class inheriting [`FhSettingProvider<T>`](https://github.com/fahrenheit-crew/fahrenheit/blob/ee0d0f9dc3d8e4ca7315cec3ebc834445721ee05/src/core/settings.cs#L29), where `T` is the type of your module. See [Modules](modules).

You may arrange this class as you deem fit and control the visibility of both it and its individual settings.

For example:
```cs
public class Settings : FhSettingProvider<MyModule> {
    public FhSettingToggle show_some_stuff = new("show_stuff", true);

    internal protected override IEnumerable<FhSetting> get() {
        return [ show_some_stuff ];
    }
}

[FhLoad(FhGameId.FFX)]
public sealed class MyModule : FhModule
// ...
```
There are three requirements for setting providers:
- `FhSettingProvider` **must** be derived by only one class for a given module.
- An instance of that class **must** be created in the module's constructor. See [Startup](startup).
- In the `get` method, it **must** return the settings it wishes to expose through the mod configuration UI, in display order.

## The mod configuration UI
At any point, the user may raise the mod configuration UI by pressing F7.

[This section will be added when PR #332 and the configuration visuals are available.]
