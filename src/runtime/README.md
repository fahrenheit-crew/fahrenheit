The Runtime implements Fahrenheit's API. It is a special mod that is always present in any Fahrenheit installation.

It is an inseparable part of Fahrenheit. Interfering with any aspect of its operation is strictly unsupported.

**Bear in mind the following:**
- Never link against the Runtime. It has no public API surface, nor may you depend on any implementation detail of it.
- The Runtime always loads first. This is enforced by the framework. It does not need to be specified in `loadorder`.

The Runtime is broadly subdivided into:
- `gui/*.cs` - user interfaces for the save/load system, settings system, and more
- `events/*.cs` - fires events to make modules aware of various occurrences in the game(s)
- `cd.cs` - overrides the game's fixed size tables to allow patching of files loaded using "CD" semantics
- `fileloader.cs` - a load-order-aware external file loader
- `imgui.cs` - provides for [Dear ImGui](https://github.com/ocornut/imgui) overlays over the game
- `malloc.cs` - fixes the game's native allocator to behave more responsibly
- `peloader.cs` - interfaces with the Phyre asset system to load game assets
- `platform.cs` - provides binding to any required native API (in our case, Win32+D3D11)
- `reset.cs` - an improved soft-reset function
- `resloader.cs` - loads resources such as textures from disk, or through the game
- `save_impl.cs` - a full reimplementation of the game's save system
