global using System;                           // primitives
global using System.Buffers;                   // OperationStatus for Rune decoding, et al.
global using System.Buffers.Binary;            // BinaryPrimitives, et al.
global using System.Collections.Concurrent;    // ConcurrentDictionary<T>
global using System.Collections.Generic;       // List<T>, Dictionary<T,U> and others
global using System.Collections.Immutable;     // ImmutableArray<T>
global using System.Diagnostics;               // [Conditional] et al.
global using System.Diagnostics.CodeAnalysis;  // [NotNullWhen()] and other nullability static analysis attributes
global using System.Globalization;             // CultureInfo.InvariantCulture et al.
global using System.IO;                        // Path, File, and similar
global using System.IO.MemoryMappedFiles;
global using System.Numerics;                  // generic math - INumber<T>, IBinaryInteger<T>
global using System.Reflection;                // Assembly
global using System.Runtime.CompilerServices;  // [InlineArray]
global using System.Runtime.ExceptionServices; // First-chance EH
global using System.Runtime.InteropServices;   // [DllImport], [LibraryImport], et al.
global using System.Runtime.Loader;            // AssemblyLoadContext, AssemblyDependencyResolver
global using System.Runtime.Versioning;        // [SupportedOSPlatform]
global using System.Security.Cryptography;     // SHA-256 for state hashing
global using System.Text;                      // Encoding
global using System.Text.Json;                 // For JSON (de)serialization, we use STJ.
global using System.Text.Json.Serialization;
global using System.Threading;                 // Lock
global using System.Threading.Tasks;           // Task<T>

global using Fahrenheit.Events;
global using Fahrenheit.Gui;

global using Hexa.NET.ImGui;

// Win32/D3D11 typedefs
global using TerraFX.Interop.Windows;
global using TerraFX.Interop.DirectX;
