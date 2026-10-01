# Fahrenheit Symbol Table Parser (STEP)

STEP is a standalone utility that processes CSV output from Ghidra,
alongside supplementary information, to generate `FhCall` delegates
for all functions in the game executables.

It is not shipped alongside Fahrenheit releases; it is only used for,
and therefore only available during, development.

### Usage

The following commands and arguments are available:
```
Description:
  Process a Ghidra symbol table and create C# code files.

Usage:
  fhstep [options]

Options:
  -o, --output <o> (REQUIRED)            Set the folder where the C# files should be written.
  -gx, --global-x <gx> (REQUIRED)        Set the path to the file containing globals for FF X.
  -gx2, --global-x2 <gx2> (REQUIRED)     Set the path to the file containing globals for FF X-2.
  -f, --functions <f> (REQUIRED)         Set the path to the file containing common function definitions.
  -fx, --functions-x <fx> (REQUIRED)     Set the path to the file containing function definitions for FF X.
  -fx2, --functions-x2 <fx2> (REQUIRED)  Set the path to the file containing function definitions for FF X-2.
  -r, --remap <r>                        Set the path to the file containing Ghidra -> Fh remappings for common functions.
  -rx, --remap-x <rx>                    Set the path to the file containing Ghidra -> Fh remappings for FF X.
  -rx2, --remap-x2 <rx2>                 Set the path to the file containing Ghidra -> Fh remappings for FF X-2.
  -re, --reject <re> (REQUIRED)          Set the path to the file specifying addresses not to emit common calls for.
  -rex, --reject-x <rex> (REQUIRED)      Set the path to the file specifying addresses not to emit calls for in FF X.
  -rex2, --reject-x2 <rex2> (REQUIRED)   Set the path to the file specifyin addresses not to emit calls for in FF X-2.
  -?, -h, --help                         Show help and usage information
  --version                              Show version information
```

The simplest and recommended way to use STEP is to place arguments
into a response file, one argument per line, then invoke STEP as such:
```
PS> cat test.rsp
-gx
"data/globals.ffx.csv"
-gx2
"data/globals.ffx2.csv"
-f
"data/functions.common.csv"
-fx
"data/functions.ffx.csv"
-fx2
"data/functions.ffx2.csv"
-r
"data/remap.common.json"
-rx
"data/remap.ffx.json"
-rx2
"data/remap.ffx2.json"
-re
"data/reject.common.rsp"
-rex
"data/reject.ffx.rsp"
-rex2
"data/reject.ffx2.rsp"
-o
.
```
```
.\fhstep.exe '@test.rsp'
```

You must specify:
- Input functions for common functions (`-f`), FF X (`-fx`), and X-2 (`-fx2`)
- Input globals for FF X (`-gx`) and X-2 (`-gx2`)
- Addresses to reject for common functions (`-re`), FF X (`-rex`) and X-2 (`-rex2`)

You may optionally specify type remappings for the common block (`-r`), FF X (`-rx`), and X-2 (`-rx2`).

## Input file formats

See [`using.cs`](https://github.com/fahrenheit-crew/fahrenheit/blob/main/src/step/using.cs)
and [`input.cs`](https://github.com/fahrenheit-crew/fahrenheit/blob/main/src/step/input.cs)
for a description of all input structures.
