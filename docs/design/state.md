State is arbitrary data [modules](Modules) may save between game sessions. It comes in two varieties:
- Global state, applicable at all times
- Local state, specific to each save file

> [!IMPORTANT]
> **Never** store your mod's data directly in the save file, or in arbitrary other files. 
>
> **Always** use global and local state for data storage.

## Global state
In its [`init`](Startup#Initialization) method, each module receives a `FileStream` of their global state file.

The module may store the handle and use this file as it deems fit. The format of global state is not prescribed.

> [!IMPORTANT]
> The framework will not close the file handle at any point. If the module does, it must reopen it itself.

## Local state
Local state is loaded and saved alongside a save game. It allows per-save data to be stored without modifying the save itself.

The format of local state is not prescribed. A mod may write whatever it deems fit into the file.

To use local state, a module must implement the `load_local_state` and `save_local_state` callbacks. For example:
```cs
public override void save_local_state(FileStream local_state_file) {
    MyModState state = new();
    JsonSerializer.Serialize(local_state_file, state);
    local_state_file.SetLength(local_state_file.Position);
}

public override void load_local_state(FileStream local_state_file, FhLocalStateInfo local_state_info) {
    MyModState loaded_state = JsonSerializer.Deserialize<MyModState>(local_state_file);
    // ...
}
```
These callbacks:
- **Must not** throw. Faults and exceptions **must** be handled.
- **Must not** store the `local_state_file` handle. The framework closes it after the callback completes.

## Local state versioning
When loading local state, a supplementary `FhLocalStateInfo` is provided. 

This structure contains the [manifest](Manifests) `Version` of your mod that last wrote that state file. 

> [!TIP]
> You may use this information to safely handle updating the format of your state file, or refuse to load it altogether.