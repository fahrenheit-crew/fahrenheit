/* [fkelava 5/7/25 14:16]
 * Hexa bundles some definitions for D3D11 structures that we need to use when interfacing
 * with its API. They are defined this way because we prefer the CsWin32 definitions in all other cases.
 */
global using HexaID3D11Device           = Hexa.NET.ImGui.Backends.D3D11.ID3D11Device;
global using HexaID3D11DeviceContext    = Hexa.NET.ImGui.Backends.D3D11.ID3D11DeviceContext;
global using HexaID3D11DeviceContextPtr = Hexa.NET.ImGui.Backends.D3D11.ID3D11DeviceContextPtr;
global using HexaID3D11DevicePtr        = Hexa.NET.ImGui.Backends.D3D11.ID3D11DevicePtr;

global using ImGuiImplD3D11             = Hexa.NET.ImGui.Backends.D3D11.ImGuiImplD3D11;
global using ImGuiImplWin32             = Hexa.NET.ImGui.Backends.Win32.ImGuiImplWin32;

global using System;                          // primitives
global using System.Buffers.Binary;           // BinaryPrimitives et al.
global using System.Collections.Generic;      // List<T>, Dictionary<T,U> and others
global using System.Diagnostics;              // UnreachableException
global using System.Diagnostics.CodeAnalysis; // [NotNullWhen] contract
global using System.IO;                       // Path, File, and similar
global using System.Numerics;                 // Vector2, et al.
global using System.Runtime.CompilerServices; // [InlineArray], [CallConvStdcall] et al.
global using System.Runtime.InteropServices;  // [UnmanagedFunctionPointer], [DllImport], [LibraryImport] et al.
global using System.Runtime.Versioning;       // [SupportedOSPlatform] guard
global using System.Text;                     // Encoding.UTF8 et al.
global using System.Text.Json;                // For JSON (de)serialization, we use STJ.
global using System.Threading;                // Interlocked, Lock, et al.

global using Fahrenheit.Atel;
global using Fahrenheit.Gui;

global using Hexa.NET.ImGui;                  // ImGui is required to render large parts of the runtime UI.

// Win32/D3D11 typedefs
global using TerraFX.Interop.Windows;
global using TerraFX.Interop.DirectX;
