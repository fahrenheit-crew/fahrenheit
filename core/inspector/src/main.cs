// SPDX-License-Identifier: MIT

namespace Fahrenheit.Tools.Inspector;

public class FhInspectorWriter {
    private readonly TextWriterTraceListener _console;
    private readonly TextWriterTraceListener _file;

    public FhInspectorWriter(string log_file_path) {
        _console = new TextWriterTraceListener(Console.Out);
        _file    = new TextWriterTraceListener(File.Open(log_file_path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite));
    }

    public void Log(string msg) {
        _console.WriteLine(msg);
        _file   .WriteLine(msg);
        _file   .Flush();
    }
}

internal sealed class Program {

    private static void _inspect(string target_path, string dest_path) {
        Stopwatch         perf   = Stopwatch.StartNew();
        FhInspectorWriter writer = new FhInspectorWriter(dest_path);
        Assembly          target = AssemblyLoadContext.Default.LoadFromAssemblyPath(target_path);

        foreach (Type type in target.GetExportedTypes()) {
            // We ignore classes, delegates, and such.
            if (!type.IsValueType) {
                Console.WriteLine($"Skipped type {type.FullName}.");
                continue;
            }

            TypeLayout layout = TypeLayout.GetLayout(type);

            writer.Log(type.FullName ?? string.Empty);
            writer.Log(layout.ToString(true));
        }

        Console.WriteLine($"Inspector emitted to {dest_path} in {perf.Elapsed}.");
    }

    private static void Main(string[] args) {
        Option<string> opt_tgt_path = new Option<string>("--target") {
            Description = "The path to the DLL Inspector should evaluate.",
            Required    = true
        };
        Option<string> opt_dest_path = new Option<string>("--dest") {
            Description = "Set the folder where the Inspector output file should be written.",
            Required    = true
        };

        RootCommand root_cmd = new RootCommand("Inspects the layout of all structures in a .NET library and dumps them to disk.") {
            opt_tgt_path,
            opt_dest_path,
        };

        ParseResult argparse_result = root_cmd.Parse(args);

        string tgt_path_raw  = argparse_result.GetValue(opt_tgt_path)!;
        string dest_path_raw = argparse_result.GetValue(opt_dest_path)!;

        string tgt_path  = Path.GetFullPath(tgt_path_raw);
        string dest_path = Path.Join(dest_path_raw, $"inspector-{Guid.NewGuid()}.txt");

        _inspect(tgt_path, dest_path);
    }
}
