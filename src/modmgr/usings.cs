/* [fkelava 5/7/25 14:16]
 * Hexa duplicates some definitions for SDL3 structures. We must disambiguate and cast between them
 * because they for some reason aren't one and the same. Heaven only knows why that is so.
 */
global using SDLWindow     = Hexa.NET.SDL3.SDLWindow;
global using SDLEvent      = Hexa.NET.SDL3.SDLEvent;
global using SDLWindowB    = Hexa.NET.ImGui.Backends.SDL3.SDLWindow;
global using SDLEventB     = Hexa.NET.ImGui.Backends.SDL3.SDLEvent;
global using SDLWindowPtrB = Hexa.NET.ImGui.Backends.SDL3.SDLWindowPtr;

// C# stdlib essentials
global using System;
global using System.Collections.Generic;
global using System.Diagnostics;
global using System.Drawing;
global using System.IO;
global using System.IO.Compression;
global using System.Numerics;
global using System.Runtime.CompilerServices;
global using System.Runtime.InteropServices;
global using System.Runtime.Versioning;
global using System.Text.Json;

// ImGui C# binding
global using Hexa.NET.ImGui;

// SDL3/OGL3 typedefs
global using Hexa.NET.ImGui.Backends.OpenGL3;
global using Hexa.NET.ImGui.Backends.SDL3;
global using Hexa.NET.SDL3;
global using Hexa.NET.OpenGL;

global using HexaGen.Runtime;

// Native file dialog bindings
global using NativeFileDialogCore;
