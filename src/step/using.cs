// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

global using System;
global using System.Collections.Generic;
global using System.CommandLine;
global using System.Diagnostics;
global using System.Globalization;
global using System.IO;
global using System.Text;
global using System.Text.Json;
global using System.Threading.Tasks;

global using CsvHelper;
global using CsvHelper.Configuration;
global using CsvHelper.Configuration.Attributes;
global using CsvHelper.TypeConversion;

global using Microsoft.CodeAnalysis.CSharp;

global using CommonData = System.Collections.Generic.Dictionary<int, Fahrenheit.Tools.STEP.FhCommonFuncDecl>;
global using FuncData   = System.Collections.Generic.Dictionary<int, Fahrenheit.Tools.STEP.FhFuncDecl>;
global using GlobalData = System.Collections.Generic.Dictionary<int, Fahrenheit.Tools.STEP.FhDataLabelDecl>;
global using RejectData = int[];
global using RemapData  = System.Collections.Generic.Dictionary<string, string>;

