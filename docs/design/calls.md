Functions in the game and other libraries are called and hooked using [`FhCallHandle<T>`](https://github.com/fahrenheit-crew/fahrenheit/blob/ee0d0f9dc3d8e4ca7315cec3ebc834445721ee05/src/core/handles.cs#L248). 

This page describes where to find them and how to use them.

## Call prerequisites
To obtain a call handle, two pieces of information are needed: its _signature_ and _location_.

Put simply, a signature says _what_ you are calling, and the location says _where_ that something is.

### Call signature
The function's signature is the `<T>` in `FhCallHandle<T>`. It is the conjunction of a function's:
- calling convention
- return type
- argument count and types.

A function's signature is expressed by a [delegate](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/). For example:
```cs
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal delegate nint d_CDfileSize_PC(int arg1);
```
represents a function returning `nint`, with one `int` argument, and `__cdecl` calling convention.

> [!TIP]
> A signature is typically obtained with reverse-engineering tools like [IDA](https://hex-rays.com/ida-pro) or [Ghidra](https://github.com/NationalSecurityAgency/ghidra).

### Call location
A function's location is expressed by a [`FhCallLocation`](https://github.com/fahrenheit-crew/fahrenheit/blob/ee0d0f9dc3d8e4ca7315cec3ebc834445721ee05/src/core/handles.cs#L59).

Several types of functions can be targeted depending on the constructor you use:
| `FhCallLocation` constructor | Call type                 |
| ---------------------------- | ------------------------- |
| (`nint`, `nint`)             | A pair of functions identical in both games.                |
| (`string`, `nint`)           | A function at an offset in a given library.                 |
| (`string`, `string`)         | A named function exported from a given library.             |
| (`nint`)                     | A function at an absolute address, ex. an object's vftable. |

A call location targeting an identical function pair will pick the correct function based on the currently active game.

For example:
```cs
// (nint, nint) - Identical function pair.
var loc = new FhCallLocation(0x36D660, 0x1397D0);
// (string, nint) - Function at an offset.
var loc = new FhCallLocation("FFX.exe", 0x25A620); 
// (string, string) - Named export.
var loc = new FhCallLocation("magic_0003.dll", "GetEffectOverlayTable"); 
// (nint) - Absolute address.
var loc = new FhCallLocation(ptr_swapchain->lpVtbl[8]);
```

## Call library
To avoid the need to painstakingly find this information by hand, Fahrenheit provides a _call library_, `FhCall`. 

It contains:
- location information for _all_ functions in both FF X and X-2/LM
- signature information for functions that have been manually reversed.

and has call handles for _all_ functions in both FF X and X-2/LM, divided as such:
| Call type                   | Call library namespace    |
| --------------------------- | ------------------------- |
| Identical in both games     | `Fahrenheit.FhCall`       |
| Exclusive to FF X           | `Fahrenheit.FFX.FhCall`   |
| Exclusive to FF X-2 and LM  | `Fahrenheit.FFX2.FhCall`  |

> [!IMPORTANT]
> A function is 'identical in both games' **only if** all of its instructions and argument types are identical in both games.

> [!IMPORTANT]
> If no signature information exists for a function, its delegate type is `d_UnknownFn`. 
>
> You will likely not be able to call it successfully.
>
> Raise an issue or contact us directly if you need a function to be reversed.

## Calling a function
You can call a function you have a `FhCallHandle<T>` to through its `fnptr`.

For example:
```cs
// [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
// internal delegate void d_SaveDataGetLoc(int arg1, byte* arg2);
FhCall.SaveDataGetLoc.fnptr!(0x4C1, ptr_lm_location_prefix_encoded);

// [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
// public delegate byte* d_AtelGetSaveDicName(ushort arg1, uint arg2);
byte* ptr_location_name_encoded = FhCall.AtelGetSaveDicName.fnptr!(location_id, 0);
```

> [!IMPORTANT]
> Be careful! `fnptr` can be `null` **if**:
> - the targeted library is not loaded
> - the named export cannot be found
> 
> If you want to call the method _anyway_ and crash if it is not present, use `fnptr!()`.
>
> If you want to only call if the method was found, use `fnptr?.Invoke()` instead.

## Hooking
To hook a call through a handle, use the `hook(FhModule, T hook_method)` method. 

As explained in [Startup](Startup), you **must** do so in your module's initializer. For example:
```cs
public override bool init(FhModContext mod_context, FileStream global_state_file) {
    something_else();

    return FhCall.CDfileSize_PC.hook(this, h_fsize_pc);
}
``` 
Hooks, with rare exceptions, execute in load order.

Hook methods:
- **Must** be decorated with the correct [`[UnmanagedCallConv]`](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.unmanagedcallconvattribute?view=net-10.0#examples) attribute.
- **Must** have the same signature as the call they are hooking.
- **Must** either directly return **or** forward execution to the next hook by chaining.

For example:
```cs
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal delegate nint d_CDfileSize_PC(int arg1);
internal static FhCallHandle<d_CDfileSize_PC> CDfileSize_PC => // ...

// CallConvs equal to [UnmanagedFunctionPointer] of delegate
[UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )] 
private nint h_fsize_pc(int arg1) { } // <--- Equal return and argument types to delegate
```
> [!IMPORTANT]
> There is a limited exception to the rule that hooking must be done in a module's initializer.
>
> **If and only if** there are no hooks on a function when the initialization phase of startup has finished, it may be hooked at any later point.
> **However**, such "late" hooks execute in insertion order, **not** in load order.

## Chaining from hooks
In a hook, to execute the next call in the chain, use the `chain_from(T)` method, then invoke the call. For example:
```cs
[UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
private nint h_fsize_pc(int arg1) {
    // The subsequent function will be executed- the next hook
    // if there is one, or the original if none remain.
    nint rv = FhCall.CDfileSize_PC.chain_from(h_fsize_pc).fnptr!(arg1);
}
```
> [!IMPORTANT]
> If `chain_from` is passed a function that is not in the given call's chain, `fnptr` is `null` and the behavior is undefined.