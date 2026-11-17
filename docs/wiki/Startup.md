In Fahrenheit, startup is a two-stage process from a module's perspective. First comes _construction_, then _initialization_.

It is important to understand which actions are valid to take at which time.

## Construction
Construction is the stage at which a module's constructor runs. These restrictions apply to it, and anything it calls.

During construction, you **must**:
- Initialize your module's local variables.
- If your module has [settings](Settings), create an instance of its `FhSettingProvider<T>`.

During construction, you **cannot**:
- Use module handles, and hence interact with other modules.
- Use [call handles](Calls), and hence call or hook game functions.
- Call any member or method of `FhApi`.

## Initialization
Initialization is the stage at which a module's `init` method runs. These restrictions apply to it, and anything it calls.

**Exclusively** during initialization, you **can**:
- Subscribe to any events you wish to respond to.
- Use call handles to hook, but not call, game functions.

During and after initialization, you **can**:
- Use module handles, and hence interact with other modules.
- Call any member or method of `FhApi`.
- Access your module's [global state](State#Global-state).

After initialization, you **can**:
- Use call handles to call game functions.

> [!IMPORTANT]
> All modules have loaded by initialization time. If a module is not loaded by then, it never will be.
>
> If you want to store a module for later use, you should obtain it at initialization time and keep it.

> [!IMPORTANT]
> Remember to store the global state file handle you receive in `init` if you intend to use it.
>
> The framework will not provide it at any other point.
