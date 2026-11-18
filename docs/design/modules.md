# Modules

A module is a class deriving from [`FhModule`](https://github.com/fahrenheit-crew/fahrenheit/blob/ee0d0f9dc3d8e4ca7315cec3ebc834445721ee05/src/core/module.cs#L76). They are the basic unit of functionality and organization in Fahrenheit mods.

## Basic properties and purpose
Mods, unless purely file-based, consist of anywhere from one to an unlimited number of modules. They allow you to split the combined functionality of your mods into smaller, legible, reusable pieces.

Modules may retrieve, and interact with, each other at runtime.

Beyond being organizational scopes, modules possess the following properties:
- Each gets its own global and local state. See [State](state).
- Each gets its own log file.
- Each gets to specify the games it supports.

## Basic requirements
A valid module has three basic requirements.
- It **must** have exactly one, public, parameterless constructor.
- It **must** implement the `init` method. See [Startup](startup).
- It **must** have the [`FhLoad`](https://github.com/fahrenheit-crew/fahrenheit/blob/6a077ffdf3d9888bad66c74a2740effca4acb36e/src/core/bootloader.cs#L157) attribute applied declaring which games it supports.

For example:
```cs
// A perfectly valid, minimal module that does nothing, loadable in all three games.
[FhLoad(FhGameId.FFX | FhGameId.FFX2 | FhGameId.FFX2LM)]
public sealed class ExampleModule : FhModule {
    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        return true;
    }
}

// The same, but loadable only in FF X.
[FhLoad(FhGameId.FFX)]
public sealed class ExampleModule2 : FhModule {
    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        return true;
    }
}
```
Your module will be automatically loaded and constructed when a supported game is started.

> [!IMPORTANT]
> You **must not** construct your own instances of modules. 
>
> From Fahrenheit's perspective, such modules do not exist, thus none of their callbacks execute.
> 
> Read below for how to obtain instances of other modules at runtime.

## Module handles
To retrieve an instance of another module at runtime safely, use a [`FhModuleHandle<T>`](https://github.com/fahrenheit-crew/fahrenheit/blob/ee0d0f9dc3d8e4ca7315cec3ebc834445721ee05/src/core/handles.cs#L12). For example:

```cs
FhModuleHandle<ModuleThatDoesThings>     h_one = new(this);
FhModuleHandle<ModuleThatAlsoDoesThings> h_two = new(this);

if (h_one.try_get_module(out ModuleThatDoesThings?     m_one) && 
    h_two.try_get_module(out ModuleThatAlsoDoesThings? m_two)) 
{
    // If the call succeeds, you may store the returned
    // modules and use them as you see fit.
}
```

> [!IMPORTANT]
> **Always** use handles to refer to other modules. They allow you to deal with their potential absence gracefully.
>
> Due to the aforementioned ability of modules to only load in selected games, you **must not** assume that a mod being loaded means all of its modules are as well.
