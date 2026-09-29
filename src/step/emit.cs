// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Tools.STEP;

/// <summary>
///     Generates C# code from Ghidra data exports.
/// </summary>
internal abstract class FhStepGenerator(
    DirectoryInfo output_dir,
    RejectData    reject,
    RemapData     remap,
    FhGameId      game) {

    /* [fkelava 30/07/26 03:03]
     * These structures are used for internal record-keeping.
     */

    protected ref struct FhFuncSignatureData {
        public ReadOnlySpan<char>    ReturnType;
        public ReadOnlySpan<char>    FunctionName;
        public List<FhFuncParameter> Parameters;
    }

    protected record FhFuncParameter(
        string ParameterType,
        string ParameterName);

    /* [fkelava 01/06/26 13:47]
     * Generating all functions for either game results in about a 400,000 line file.
     * IDEs and IntelliSense absolutely cannot handle files that large, not even on
     * top-end machines. We thus split the file at roughly the 50,000 line mark.
     */

    protected const int LINES_PER_FILE = 50_000;

    // Input data
    protected readonly DirectoryInfo _out_dir = output_dir;
    protected readonly FhGameId      _game    = game;
    protected readonly int[]         _reject  = reject;
    protected readonly RemapData     _remap   = remap;

    // Emitter state
    protected int           _file_count  = 1;
    protected int           _line_count  = 0;
    protected string        _path_output = "";
    protected StringBuilder _output      = new();

    /* [fkelava 29/09/26 18:15]
     * We special-case 'undefined unknown FN(void)`. Such a function has not has its parameters
     * committed in Ghidra, and is thus likely unsafe to call. We assign them a Fahrenheit delegate,
     * `Fahrenheit.FhCall.d_UnknownFn` to warn users that this function requires manual attention.
     */

    /// <summary>
    ///     Determines whether a function is devoid of manual information,
    ///     and should thus be assigned to a single "unknown function" delegate.
    /// </summary>
    protected static bool should_map_to_unknownfn(FhFuncDecl function, FhFuncSignatureData signature_data) {
        return signature_data.Parameters.Count == 0
            && signature_data.ReturnType       == "void"
            && function.CallConv               == "unknown";
    }

    /// <summary>
    ///     Determines whether a specific function declaration provided by Ghidra should be interpreted.
    /// </summary>
    /// <param name="function">The function declaration to be checked.</param>
    /// <returns>Whether the provided function declaration should be interpreted.</returns>
    protected static bool should_interpret(FhFuncDecl function) {
        return function is {
                Type:      "Function",
                Namespace: "Global", // Exclude potentially proprietary symbols
            } &&
            !function.Name.Contains("operator") && // ignore operator.new, operator.delete
            !function.Name.Contains('@')        && // ignore Unwind@{ADDR}, Catch_All@{ADDR} thunks
            !function.Signature.Contains('.')   && // ignore vararg functions
            !function.Signature.Contains(':')   && // ignore anything that even vaguely resembles a C++ namespace
            !function.Signature.Contains('`')   && // ignore vector ctors/dtors
            !function.Signature.Contains('<')   && // ignore template specializations
            !function.Signature.Contains('-')   &&
            !function.Signature.Contains('+');     // ignore descriptively labeled but not authoritatively named functions
    }

    /// <summary>
    ///     Determines whether a specific global declaration provided by Ghidra should be interpreted.
    /// </summary>
    /// <param name="data_label">The global declaration to be checked.</param>
    /// <returns>Whether the provided global declaration should be interpreted.</returns>
    protected static bool should_interpret(FhDataLabelDecl data_label) {
        return !data_label.Name.Contains('+'); // ignore descriptively labeled but not authoritatively named globals
    }

    /// <summary>
    ///     Maps a Ghidra-provided type using the user-defined typemap.
    /// </summary>
    /// <example>
    ///     Assuming Ghidra's <c>undefined4</c> type is mapped to C#'s <c>uint</c>,
    ///     calling this function with <c>"undefined4"</c> will return <c>"uint"</c>.
    /// </example>
    /// <param name="type">The string representation of a Ghidra parameter type.</param>
    /// <returns>The mapped parameter type.<br/>Returns <c>"nint"</c> if the given Ghidra type isn't mapped.</returns>
    protected ReadOnlySpan<char> remap_type(string type) {
        return _remap.GetValueOrDefault(type, "nint");
    }

    /// <summary>
    ///     Applies the user-defined type map to a parameter type provided by Ghidra.<br/>
    ///     Unlike <see cref="remap_type"/>, accounts for <c>void</c>.
    /// </summary>
    /// <param name="return_type">The string representation of a Ghidra return type.</param>
    /// <returns>The mapped return type.</returns>
    protected ReadOnlySpan<char> remap_return_type(string return_type) {
        return return_type switch {
            "void"      => "void",
            "undefined" => "void",
            _           => remap_type(return_type),
        };
    }

    /// <summary>
    ///     Modifies a <paramref name="param_name"/> to not conflict with C# keywords.
    /// </summary>
    /// <returns>The modified parameter name.</returns>
    protected static ReadOnlySpan<char> get_param_name(string param_name) {
        bool is_language_reserved =
            SyntaxFacts.GetKeywordKind          (param_name) != SyntaxKind.None
         || SyntaxFacts.GetContextualKeywordKind(param_name) != SyntaxKind.None;

        return is_language_reserved ? $"_{param_name}" : param_name;
    }

    /// <summary>
    ///     Translates a function's parameter list to a string with types and names mapped.
    /// </summary>
    /// <param name="parameters">The list of parameters</param>
    /// <returns>A string representation of the parameter list, valid as C# code.</returns>
    protected string get_params_string(List<FhFuncParameter> parameters) {
        List<string> param_str = [];

        foreach (FhFuncParameter param in parameters) {
            param_str.Add($"{remap_type(param.ParameterType)} {get_param_name(param.ParameterName)}");
        }

        return $"({string.Join(", ", param_str)})";
    }

    /// <summary>
    ///     Converts an offset back into its Ghidra equivalent.
    /// </summary>
    protected static int addr_to_ghidra(int address) => address + 0x400000;

    /// <summary>
    ///     Convert from a C++/Ghidra calling convention specifier to the equivalent C# attribute for delegates.
    /// </summary>
    /// <param name="call_conv">The C++/Ghidra-style calling convention specifier.</param>
    /// <returns>An equivalent C# attribute applicable to delegates.</returns>
    /// <exception cref="ArgumentException">Thrown if the C++/Ghidra-style calling convention specifier is not recognized.</exception>
    protected static ReadOnlySpan<char> emit_callconv_attr(ReadOnlySpan<char> call_conv) {
        return call_conv switch {
            "__thiscall" => "[UnmanagedFunctionPointer(CallingConvention.ThisCall)]",
            "__cdecl"    => "[UnmanagedFunctionPointer(CallingConvention.Cdecl)]",
            "__stdcall"  => "[UnmanagedFunctionPointer(CallingConvention.StdCall)]",
            "__fastcall" => "[UnmanagedFunctionPointer(CallingConvention.FastCall)]",
            "unknown"    => "[UnmanagedFunctionPointer(CallingConvention.Cdecl)]",
            _            => throw new ArgumentException($"Encountered an unknown calling convention `{call_conv}` while parsing functions."),
        };
    }

    /// <summary>
    ///     Return FhCall's introductory comment.
    /// </summary>
    protected string emit_prologue() {
        string ns = _game switch {
            FhGameId.FFX    => "namespace Fahrenheit.FFX;",
            FhGameId.FFX2   or
            FhGameId.FFX2LM => "namespace Fahrenheit.FFX2;",
            _               => "namespace Fahrenheit;",
        };

        return $$"""
            /* [STEP {{DateTime.UtcNow:dd/M/yy HH:mm}}]
             * This file was generated by Fahrenheit's STEP tool (https://github.com/fahrenheit-crew/fh-tools-step/).
             *
             * Its purpose is to provide auto-generated delegates to allow you to call or hook game functions without having
             * to go through an extensive reverse-engineering process. These are, for the time being, quite rudimentary;
             * many parameters whose types are known to us are still mapped only to `nint`.
             *
             * The presence of a delegate or function signature in this file does not imply it has been tested. You have been warned.
             *
             * To improve the call map quality, add new entries to `typemap.json` in the STEP source code or annotate further
             * functions in Ghidra. Every so often, STEP generation will be rerun and Fahrenheit updated with the result.
             */

            {{ns}}

            public static unsafe partial class FhCall {

            """;
    }

    /// <summary>
    ///     Opens a new symbol table for writing.
    /// </summary>
    protected void file_open() {
        _path_output = Path.Join(_out_dir.FullName, $"call_{_file_count++}.g.cs");
        _output      = new(emit_prologue());
        _line_count  = 0;
    }

    /// <summary>
    ///     Flushes a written out symbol table to disk.
    /// </summary>
    protected void file_close() {
        _output.AppendLine("}");
        File.WriteAllText(_path_output, _output.ToString());

        Console.WriteLine(_path_output);
    }
}

/// <summary>
///     Generates C# code from Ghidra data exports for a specific <paramref name="game"/>.
/// </summary>
internal sealed class FhGameSpecificGenerator(
    DirectoryInfo output_dir,
    RejectData    reject,
    RemapData     remap,
    FhGameId      game,
    FuncData      funcs,
    GlobalData    globals) : FhStepGenerator(output_dir, reject, remap, game) {

    private readonly FuncData   _funcs   = funcs;
    private readonly GlobalData _globals = globals;

    /// <summary>
    ///     Converts a Ghidra function declaration and the associated signature data into valid C# code.
    /// </summary>
    /// <param name="function">A Ghidra-provided function declaration.</param>
    /// <param name="signature_data">The signature data associated with the function.</param>
    /// <returns>A valid C# delegate declaration and associated function address constant.</returns>
    private void emit_function(FhFuncDecl function, FhFuncSignatureData signature_data) {
        int addr_label = addr_to_ghidra(function.Location);

        string module = _game switch {
            FhGameId.FFX    => "FFX.exe",
            FhGameId.FFX2   or
            FhGameId.FFX2LM => "FFX-2.exe",
            _               => throw new NotImplementedException($"invalid game id {_game} - cannot generate function"),
        };

        if (_reject.Contains(function.Location))
            return;

        if (should_map_to_unknownfn(function, signature_data)) {
            _output.AppendLine($"""
                 // Unannotated function:
                 // {function.CallConv} {function.Signature} at {addr_label:x8}

                 public static FhMethodHandle<Fahrenheit.FhCall.d_UnknownFn> {function.Name} => new( new FhMethodLocation("{module}", 0x{function.Location:X}) );

             """);
            _line_count += 5;
            return;
        }

        _output.AppendLine($"""
             // Original after pruning:
             // {function.CallConv} {function.Signature} at {addr_label:x8}

             {emit_callconv_attr(function.CallConv)}
             public unsafe delegate {signature_data.ReturnType} d_{function.FuncName}{get_params_string(signature_data.Parameters)};
             public static FhMethodHandle<d_{function.FuncName}> {function.Name} => new( new FhMethodLocation("{module}", 0x{function.Location:X}) );

         """);
        _line_count += 7;
    }

    /// <summary>
    ///     Converts a global symbol provided by Ghidra into valid C# code.
    /// </summary>
    /// <param name="global">A global symbol provided by Ghidra</param>
    /// <returns>A valid C# const declaration for the given global</returns>
    private void emit_global(FhDataLabelDecl global) {
        int                addr_label = addr_to_ghidra(global.Location);
        ReadOnlySpan<char> type       = remap_type    (global.DataType);

        if (_reject.Contains(global.Location))
            return;

        //TODO: Make sure C# doesn't have issues with the pointer when the global is an array.
        _output.AppendLine($"""
             // Original after pruning:
             // {global.DataType} {global.Name} at {addr_label:x8}

             public const nint __addr_{global.Name} = 0x{global.Location:X};
             public static {type}* {global.Name} => FhUtil.ptr_at<{type}>(__addr_{global.Name});

         """);
        _line_count += 7;
    }

        /// <summary>
    ///     Emits C# code files for functions specific to the generator's configured game.
    /// </summary>
    internal void generate_code() {
        file_open();

        // This local is reused in the loop
        FhFuncSignatureData signature_data = new FhFuncSignatureData {
            Parameters = [ ],
        };

        foreach ((int _, FhFuncDecl func) in _funcs) {
            if (_line_count >= LINES_PER_FILE) {
                file_close();
                file_open ();
            }

            if (!should_interpret(func)) {
                _output.AppendLine($"    // Symbol skipped (deemed uninterpretable):");
                _output.AppendLine($"    // {func.CallConv} {func.Signature} at {addr_to_ghidra(func.Location):x8}");
                _output.AppendLine();

                _line_count += 3;
                continue;
            }

            // We lex the function signature in the form {RETURN_TYPE} {NAME}({PARAMETER_TYPE} {PARAMETER_NAME} ... );
            string[] tokens = func.Signature.Split(
                [ ' ', '(', ',', ')' ],
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
            );

            /* Tokens:
             * [0] -> Return type
             * [1] -> Function name
             * [2] -> Type of parameter 1
             * [3] -> Name of parameter 1
             * [4] -> Type of parameter 2
             * [5] -> Name of parameter 2
             * ... and so on
             */

            signature_data.ReturnType   = remap_return_type(tokens[0]);
            signature_data.FunctionName = tokens[1]; //TODO: Add cleanup of function name (remove the '+' prefix)

            // Parse parameters
            for (int i = 2; i < tokens.Length - 1; i += 2) {
                string type = tokens[i];
                string name = tokens[i + 1];

                signature_data.Parameters.Add(new (type, name));
            }

            emit_function(func, signature_data);
            signature_data.Parameters.Clear();
        }

        foreach ((int _, FhDataLabelDecl global) in _globals) {
            if (_line_count >= LINES_PER_FILE) {
                file_close();
                file_open ();
            }

            if (!should_interpret(global)) {
                _output.AppendLine($"    // Global skipped (deemed uninterpretable):");
                _output.AppendLine($"    // {global.DataType} {global.Name} at {addr_to_ghidra(global.Location):x8}");
                _output.AppendLine();

                _line_count += 3;
                continue;
            }

            emit_global(global);
        }

        file_close();
    }
}

/// <summary>
///     Generates C# code from Ghidra data exports for functions shared between both games.
/// </summary>
internal sealed class FhCommonGenerator(
    DirectoryInfo output_dir,
    RejectData    reject,
    RemapData     remap,
    FhGameId      game,
    FuncData      funcs,
    CommonData    common) : FhStepGenerator(output_dir, reject, remap, game) {

    private readonly FuncData   _funcs  = funcs;
    private readonly CommonData _common = common;

    /// <summary>
    ///     Emits valid C# code for a fused handle which permits access to a function identical in both binaries.
    /// </summary>
    /// <param name="function">A Ghidra-provided function declaration.</param>
    /// <param name="signature_data">The signature data associated with the function.</param>
    /// <param name="common_data">Data describing which two functions are being fused.</param>
    /// <returns>A valid C# delegate declaration and associated function address constant.</returns>
    private void emit_common_function(FhFuncDecl function, FhFuncSignatureData signature_data, FhCommonFuncDecl common_data) {
        int addr_label_src = addr_to_ghidra(common_data.SourceAddress);
        int addr_label_dst = addr_to_ghidra(common_data.DestAddress);

        string fused_label = $"FUN_{addr_label_src:X8}_{addr_label_dst:X8}";

        if (_reject.Contains(common_data.SourceAddress))
            return;

        if (should_map_to_unknownfn(function, signature_data)) {
            _output.AppendLine($"""
                 // Fused unannotated identical entry: {function.CallConv} {function.Signature}
                 // at (FFX.exe+{addr_label_src:X}, FFX-2.exe+{addr_label_dst:X})

                 public static FhMethodHandle<Fahrenheit.FhCall.d_UnknownFn> {function.Name} => new( new FhMethodLocation(0x{common_data.SourceAddress:X}, 0x{common_data.DestAddress:X}) );

             """);
            _line_count += 5;
            return;
        }

        _output.AppendLine($"""
             // Fused identical entry: {function.CallConv} {function.Signature}
             // at (FFX.exe+{addr_label_src:X}, FFX-2.exe+{addr_label_dst:X})

             {emit_callconv_attr(function.CallConv)}
             public unsafe delegate {signature_data.ReturnType} d_{fused_label}{get_params_string(signature_data.Parameters)};
             public static FhMethodHandle<d_{fused_label}> {fused_label} => new( new FhMethodLocation(0x{common_data.SourceAddress:X}, 0x{common_data.DestAddress:X}) );

         """);
         _line_count += 7;
    }

    /// <summary>
    ///     Emits C# code files for functions common between the games.
    /// </summary>
    internal void generate_code() {
        file_open();

        // This local is reused in the loop
        FhFuncSignatureData signature_data = new FhFuncSignatureData {
            Parameters = [ ],
        };

        foreach ((int addr, FhCommonFuncDecl common_data) in _common) {
            if (!_funcs.TryGetValue(addr, out FhFuncDecl func)) {
                throw new Exception($"No funcdef for {addr:x} with common def");
            }

            // We lex the function signature in the form {RETURN_TYPE} {NAME}({PARAMETER_TYPE} {PARAMETER_NAME} ... );
            string[] tokens = func.Signature.Split(
                [ ' ', '(', ',', ')' ],
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
            );

            /* Tokens:
             * [0] -> Return type
             * [1] -> Function name
             * [2] -> Type of parameter 1
             * [3] -> Name of parameter 1
             * [4] -> Type of parameter 2
             * [5] -> Name of parameter 2
             * ... and so on
             */

            signature_data.ReturnType   = remap_return_type(tokens[0]);
            signature_data.FunctionName = tokens[1]; //TODO: Add cleanup of function name (remove the '+' prefix)

            // Parse parameters
            for (int i = 2; i < tokens.Length - 1; i += 2) {
                string type = tokens[i];
                string name = tokens[i + 1];

                signature_data.Parameters.Add(new (type, name));
            }

            emit_common_function(func, signature_data, common_data);
            signature_data.Parameters.Clear();
        }

        file_close();
    }

}
