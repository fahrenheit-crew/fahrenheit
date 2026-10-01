// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Tools.STEP;

/// <summary>
///     Represents a function identical in both games.
/// </summary>
internal struct FhCommonFuncDecl {
    [Name("Source Namespace")]
    public string SourceNamespace      { get; set; }
    [Name("Source Label")]
    public string SourceLabel          { get; set; }
    [Name("Dest Namespace")]
    public string DestNamespace        { get; set; }
    [Name("Dest Label")]
    public string DestLabel            { get; set; }

    [Name         ("Source Address")]
    [TypeConverter(typeof(AddressConverter))]
    public int    SourceAddress        { get; set; }
    [Name         ("Dest Address")]
    [TypeConverter(typeof(AddressConverter))]
    public int    DestAddress          { get; set; }

    [Name   ("Multiple Source Labels?")]
    [Default(0)]
    public int    SourceLabelCount     { get; set; }
    [Name   ("Multiple Dest Labels?")]
    [Default(0)]
    public int    DestLabelCount       { get; set; }
}

/// <summary>
///     Represents a function declaration exported from Ghidra.
/// </summary>
internal struct FhFuncDecl {
    public string Name      { get; set; }
    [TypeConverter(typeof(AddressConverter))]
    public int    Location  { get; set; }
    [Name("Function Signature")]
    public string Signature { get; set; }
    [Name("Symbol Source")]
    public string Source    { get; set; }
    [Name("Symbol Type")]
    public string Type      { get; set; }
    [Name("Function Name")]
    public string FuncName  { get; set; }
    [Name("Function Calling Convention")]
    public string CallConv  { get; set; }
    public string Namespace { get; set; }
}

/// <summary>
///     Represents a global/data symbol exported from Ghidra.
/// </summary>
internal struct FhDataLabelDecl {
    public string Name      { get; set; }
    [TypeConverter(typeof(AddressConverter))]
    public int    Location  { get; set; }
    public string Type      { get; set; }
    [Name("Data Type")]
    public string DataType  { get; set; }
    public string Namespace { get; set; }
    public string Source    { get; set; }
}

/* [fkelava 29/07/26 20:53]
 * Ghidra exports source and destination addresses as ex. '00400F90'.
 * Thus we need to convert from hex and subtract the image base to obtain what we actually need, the offset from imagebase.
 */

public class AddressConverter : DefaultTypeConverter {
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData) {
        return int.TryParse(text, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out int i)
            ? i - 0x400000
            : base.ConvertFromString(text, row, memberMapData);
    }
}
