// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.FFX;

/// <summary>
///     An accessor for game function calls exclusive to FF X.
/// </summary>
public static partial class FhCall {

    // Common (0000h-0267h)

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_000_Init => new( new FhMethodLocation("FFX.exe", 0x45C4E0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_000_Exec => new( new FhMethodLocation("FFX.exe", 0x45C6B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_001_RetI => new( new FhMethodLocation("FFX.exe", 0x45D000) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_002_RetI => new( new FhMethodLocation("FFX.exe", 0x45E920) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_003_RetI => new( new FhMethodLocation("FFX.exe", 0x45EBD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_004_RetI => new( new FhMethodLocation("FFX.exe", 0x45ECB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_005_RetI => new( new FhMethodLocation("FFX.exe", 0x45CDC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_006_RetI => new( new FhMethodLocation("FFX.exe", 0x45E620) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_007_RetI => new( new FhMethodLocation("FFX.exe", 0x45C8C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_008_RetI => new( new FhMethodLocation("FFX.exe", 0x45EEF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_009_RetI => new( new FhMethodLocation("FFX.exe", 0x45F160) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_00A_RetI => new( new FhMethodLocation("FFX.exe", 0x45F2D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_00B_RetI => new( new FhMethodLocation("FFX.exe", 0x45F360) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_00C_RetI => new( new FhMethodLocation("FFX.exe", 0x45F640) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_00D_RetI => new( new FhMethodLocation("FFX.exe", 0x4565F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_00E_RetI => new( new FhMethodLocation("FFX.exe", 0x4566A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_00F_RetI => new( new FhMethodLocation("FFX.exe", 0x456790) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_010_RetI => new( new FhMethodLocation("FFX.exe", 0x456910) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_011_RetI => new( new FhMethodLocation("FFX.exe", 0x4581C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_012_RetI => new( new FhMethodLocation("FFX.exe", 0x45F800) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_013_RetI => new( new FhMethodLocation("FFX.exe", 0x45FAA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_014_RetI => new( new FhMethodLocation("FFX.exe", 0x460120) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_015_RetI => new( new FhMethodLocation("FFX.exe", 0x4602F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_016_RetF => new( new FhMethodLocation("FFX.exe", 0x457DA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_017_RetF => new( new FhMethodLocation("FFX.exe", 0x45F040) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_018_RetI => new( new FhMethodLocation("FFX.exe", 0x456C30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_019_RetI => new( new FhMethodLocation("FFX.exe", 0x457520) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_01A_Exec => new( new FhMethodLocation("FFX.exe", 0x4579A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_01B_Exec => new( new FhMethodLocation("FFX.exe", 0x457B30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_01C_RetF => new( new FhMethodLocation("FFX.exe", 0x455F10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_01D_RetF => new( new FhMethodLocation("FFX.exe", 0x4560D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_01E_RetF => new( new FhMethodLocation("FFX.exe", 0x456490) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_01F_RetF => new( new FhMethodLocation("FFX.exe", 0x4587A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_020_RetF => new( new FhMethodLocation("FFX.exe", 0x458D30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_021_RetF => new( new FhMethodLocation("FFX.exe", 0x459430) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_022_RetF => new( new FhMethodLocation("FFX.exe", 0x459510) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_023_RetF => new( new FhMethodLocation("FFX.exe", 0x4591A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_024_RetF => new( new FhMethodLocation("FFX.exe", 0x4592F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_025_RetF => new( new FhMethodLocation("FFX.exe", 0x459D80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_026_RetF => new( new FhMethodLocation("FFX.exe", 0x459E50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_027_RetF => new( new FhMethodLocation("FFX.exe", 0x459F30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_028_RetF => new( new FhMethodLocation("FFX.exe", 0x459640) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_029_RetF => new( new FhMethodLocation("FFX.exe", 0x459880) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_02A_RetF => new( new FhMethodLocation("FFX.exe", 0x459BB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_02B_RetF => new( new FhMethodLocation("FFX.exe", 0x45A0B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_02C_RetF => new( new FhMethodLocation("FFX.exe", 0x45A1A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_02D_RetF => new( new FhMethodLocation("FFX.exe", 0x458020) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_02E_RetF => new( new FhMethodLocation("FFX.exe", 0x45A2A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_02F_RetF => new( new FhMethodLocation("FFX.exe", 0x45A5C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_030_RetF => new( new FhMethodLocation("FFX.exe", 0x45A7C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_031_RetI => new( new FhMethodLocation("FFX.exe", 0x456BA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_032_RetI => new( new FhMethodLocation("FFX.exe", 0x456DA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_033_RetI => new( new FhMethodLocation("FFX.exe", 0x45BA50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_034_RetI => new( new FhMethodLocation("FFX.exe", 0x45C060) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_035_RetI => new( new FhMethodLocation("FFX.exe", 0x45C280) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_036_RetI => new( new FhMethodLocation("FFX.exe", 0x45B4E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_037_RetI => new( new FhMethodLocation("FFX.exe", 0x45B780) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_038_RetF => new( new FhMethodLocation("FFX.exe", 0x45BB80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_039_RetF => new( new FhMethodLocation("FFX.exe", 0x45BD40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_03A_RetF => new( new FhMethodLocation("FFX.exe", 0x45BF10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_03B_RetI => new( new FhMethodLocation("FFX.exe", 0x4604A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_03C_RetI => new( new FhMethodLocation("FFX.exe", 0x455D50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_03D_RetI => new( new FhMethodLocation("FFX.exe", 0x45C3E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_03E_RetI => new( new FhMethodLocation("FFX.exe", 0x45C470) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_03F_RetF => new( new FhMethodLocation("FFX.exe", 0x45C590) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_040_RetF => new( new FhMethodLocation("FFX.exe", 0x45C740) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_041_RetF => new( new FhMethodLocation("FFX.exe", 0x45C840) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_042_RetI => new( new FhMethodLocation("FFX.exe", 0x45CB40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_043_RetI => new( new FhMethodLocation("FFX.exe", 0x45C9A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_044_RetI => new( new FhMethodLocation("FFX.exe", 0x45D4E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_045_RetI => new( new FhMethodLocation("FFX.exe", 0x45D760) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_046_RetI => new( new FhMethodLocation("FFX.exe", 0x45D9B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_047_RetI => new( new FhMethodLocation("FFX.exe", 0x45DB70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_048_RetI => new( new FhMethodLocation("FFX.exe", 0x45DCC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_049_RetI => new( new FhMethodLocation("FFX.exe", 0x45DDC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_04A_RetI => new( new FhMethodLocation("FFX.exe", 0x45DFF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_04B_RetI => new( new FhMethodLocation("FFX.exe", 0x45E0D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_04C_RetI => new( new FhMethodLocation("FFX.exe", 0x45E130) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_04D_RetI => new( new FhMethodLocation("FFX.exe", 0x45E820) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_04E_RetI => new( new FhMethodLocation("FFX.exe", 0x45EA80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_04F_RetI => new( new FhMethodLocation("FFX.exe", 0x45E3C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_050_RetI => new( new FhMethodLocation("FFX.exe", 0x45EC60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_051_RetI => new( new FhMethodLocation("FFX.exe", 0x45F0E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_052_RetI => new( new FhMethodLocation("FFX.exe", 0x45F2A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_053_RetI => new( new FhMethodLocation("FFX.exe", 0x45EDA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_054_RetI => new( new FhMethodLocation("FFX.exe", 0x45F370) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_055_RetI => new( new FhMethodLocation("FFX.exe", 0x45FA20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_056_RetI => new( new FhMethodLocation("FFX.exe", 0x45FBF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_057_RetI => new( new FhMethodLocation("FFX.exe", 0x4603E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_058_RetI => new( new FhMethodLocation("FFX.exe", 0x455F80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_059_RetI => new( new FhMethodLocation("FFX.exe", 0x455CD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_05A_RetI => new( new FhMethodLocation("FFX.exe", 0x456170) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_05B_RetI => new( new FhMethodLocation("FFX.exe", 0x456600) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_05C_RetI => new( new FhMethodLocation("FFX.exe", 0x4600C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_05D_RetI => new( new FhMethodLocation("FFX.exe", 0x4566B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_05E_RetI => new( new FhMethodLocation("FFX.exe", 0x4567C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_05F_Exec => new( new FhMethodLocation("FFX.exe", 0x45C420) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_060_RetI => new( new FhMethodLocation("FFX.exe", 0x456920) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_061_RetF => new( new FhMethodLocation("FFX.exe", 0x457180) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_062_RetF => new( new FhMethodLocation("FFX.exe", 0x4572E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_063_RetF => new( new FhMethodLocation("FFX.exe", 0x4574F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_064_RetI => new( new FhMethodLocation("FFX.exe", 0x457870) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_065_RetI => new( new FhMethodLocation("FFX.exe", 0x4580C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_066_RetI => new( new FhMethodLocation("FFX.exe", 0x458360) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_067_RetI => new( new FhMethodLocation("FFX.exe", 0x458850) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_068_RetI => new( new FhMethodLocation("FFX.exe", 0x458640) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_069_RetI => new( new FhMethodLocation("FFX.exe", 0x4589D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_06A_RetI => new( new FhMethodLocation("FFX.exe", 0x458B50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_06B_RetI => new( new FhMethodLocation("FFX.exe", 0x459060) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_06C_RetF => new( new FhMethodLocation("FFX.exe", 0x457F40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_06D_RetF => new( new FhMethodLocation("FFX.exe", 0x45AB80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_06E_RetF => new( new FhMethodLocation("FFX.exe", 0x45ACD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_06F_RetF => new( new FhMethodLocation("FFX.exe", 0x45AF20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_070_RetF => new( new FhMethodLocation("FFX.exe", 0x45B230) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_071_RetF => new( new FhMethodLocation("FFX.exe", 0x45B480) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_072_RetI => new( new FhMethodLocation("FFX.exe", 0x45BB50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_073_RetI => new( new FhMethodLocation("FFX.exe", 0x45BC90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_074_RetI => new( new FhMethodLocation("FFX.exe", 0x45BDC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_075_RetI => new( new FhMethodLocation("FFX.exe", 0x45BF40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_076_RetI => new( new FhMethodLocation("FFX.exe", 0x45F5A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_077_RetI => new( new FhMethodLocation("FFX.exe", 0x45B620) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_078_RetI => new( new FhMethodLocation("FFX.exe", 0x45B950) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_079_RetI => new( new FhMethodLocation("FFX.exe", 0x45C040) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_07A_RetI => new( new FhMethodLocation("FFX.exe", 0x45C130) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_07B_RetI => new( new FhMethodLocation("FFX.exe", 0x457EA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_07C_Init => new( new FhMethodLocation("FFX.exe", 0x45B7C0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_07C_Exec => new( new FhMethodLocation("FFX.exe", 0x45B980) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_07C_RetI => new( new FhMethodLocation("FFX.exe", 0x45BA80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_07D_Init => new( new FhMethodLocation("FFX.exe", 0x459710) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_07D_Exec => new( new FhMethodLocation("FFX.exe", 0x459C50) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_07D_RetI => new( new FhMethodLocation("FFX.exe", 0x45A6F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_07E_RetI => new( new FhMethodLocation("FFX.exe", 0x45C580) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_07F_RetI => new( new FhMethodLocation("FFX.exe", 0x45C320) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_080_RetF => new( new FhMethodLocation("FFX.exe", 0x45C7B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_081_RetF => new( new FhMethodLocation("FFX.exe", 0x45C8F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_082_RetF => new( new FhMethodLocation("FFX.exe", 0x45CB20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_083_RetF => new( new FhMethodLocation("FFX.exe", 0x45CCE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_084_Init => new( new FhMethodLocation("FFX.exe", 0x45ABD0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_084_Exec => new( new FhMethodLocation("FFX.exe", 0x45AE90) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_084_RetI => new( new FhMethodLocation("FFX.exe", 0x45B6D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_085_RetI => new( new FhMethodLocation("FFX.exe", 0x456460) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_086_RetI => new( new FhMethodLocation("FFX.exe", 0x45CDB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_087_RetI => new( new FhMethodLocation("FFX.exe", 0x45CF90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_088_RetI => new( new FhMethodLocation("FFX.exe", 0x45D160) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_089_RetI => new( new FhMethodLocation("FFX.exe", 0x45D280) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_08A_RetI => new( new FhMethodLocation("FFX.exe", 0x45D3B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_08B_RetI => new( new FhMethodLocation("FFX.exe", 0x45D410) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_08C_RetI => new( new FhMethodLocation("FFX.exe", 0x45D8B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_08D_RetF => new( new FhMethodLocation("FFX.exe", 0x4577C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_08E_RetF => new( new FhMethodLocation("FFX.exe", 0x45DB40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_08F_RetI => new( new FhMethodLocation("FFX.exe", 0x45DE10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_090_RetF => new( new FhMethodLocation("FFX.exe", 0x45E270) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_091_RetF => new( new FhMethodLocation("FFX.exe", 0x45E0B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_092_RetF => new( new FhMethodLocation("FFX.exe", 0x45EAC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_093_RetF => new( new FhMethodLocation("FFX.exe", 0x45E8F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_094_RetF => new( new FhMethodLocation("FFX.exe", 0x45F0B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_095_RetF => new( new FhMethodLocation("FFX.exe", 0x45F170) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_096_RetF => new( new FhMethodLocation("FFX.exe", 0x45F460) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_097_RetI => new( new FhMethodLocation("FFX.exe", 0x45FA80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_098_RetI => new( new FhMethodLocation("FFX.exe", 0x45FB50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_099_RetI => new( new FhMethodLocation("FFX.exe", 0x45FD80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_09A_RetI => new( new FhMethodLocation("FFX.exe", 0x460390) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_09B_RetI => new( new FhMethodLocation("FFX.exe", 0x45F750) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_09C_RetI => new( new FhMethodLocation("FFX.exe", 0x460000) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_09D_RetI => new( new FhMethodLocation("FFX.exe", 0x4584D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_09E_RetI => new( new FhMethodLocation("FFX.exe", 0x455C70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_09F_RetI => new( new FhMethodLocation("FFX.exe", 0x456090) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0A0_RetI => new( new FhMethodLocation("FFX.exe", 0x455ED0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0A1_RetI => new( new FhMethodLocation("FFX.exe", 0x456220) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0A2_RetI => new( new FhMethodLocation("FFX.exe", 0x456570) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0A3_RetI => new( new FhMethodLocation("FFX.exe", 0x456E20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_0A4_RetF => new( new FhMethodLocation("FFX.exe", 0x4566C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_0A5_RetF => new( new FhMethodLocation("FFX.exe", 0x456990) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0A6_RetI => new( new FhMethodLocation("FFX.exe", 0x457400) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0A7_RetI => new( new FhMethodLocation("FFX.exe", 0x457820) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0A8_RetI => new( new FhMethodLocation("FFX.exe", 0x457950) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0A9_RetI => new( new FhMethodLocation("FFX.exe", 0x457680) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0AA_RetI => new( new FhMethodLocation("FFX.exe", 0x457A60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0AB_RetI => new( new FhMethodLocation("FFX.exe", 0x458330) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0AC_RetI => new( new FhMethodLocation("FFX.exe", 0x458050) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0AD_RetI => new( new FhMethodLocation("FFX.exe", 0x4581F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0AE_RetI => new( new FhMethodLocation("FFX.exe", 0x458320) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0AF_RetI => new( new FhMethodLocation("FFX.exe", 0x458440) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0B0_RetI => new( new FhMethodLocation("FFX.exe", 0x4585C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_0B1_RetF => new( new FhMethodLocation("FFX.exe", 0x456A50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0B2_RetI => new( new FhMethodLocation("FFX.exe", 0x4586B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0B3_RetI => new( new FhMethodLocation("FFX.exe", 0x458810) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0B4_RetI => new( new FhMethodLocation("FFX.exe", 0x458A20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0B5_RetI => new( new FhMethodLocation("FFX.exe", 0x458CF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0B6_RetI => new( new FhMethodLocation("FFX.exe", 0x458E20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0B7_RetI => new( new FhMethodLocation("FFX.exe", 0x459080) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0B8_RetI => new( new FhMethodLocation("FFX.exe", 0x459220) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0B9_RetI => new( new FhMethodLocation("FFX.exe", 0x4593E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0BA_RetI => new( new FhMethodLocation("FFX.exe", 0x4594A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0BB_RetI => new( new FhMethodLocation("FFX.exe", 0x4596B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0BC_RetI => new( new FhMethodLocation("FFX.exe", 0x459590) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0BD_RetI => new( new FhMethodLocation("FFX.exe", 0x459840) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0BE_RetI => new( new FhMethodLocation("FFX.exe", 0x459950) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_0BF_RetF => new( new FhMethodLocation("FFX.exe", 0x459D30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_0C0_RetF => new( new FhMethodLocation("FFX.exe", 0x459E00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_0C1_RetF => new( new FhMethodLocation("FFX.exe", 0x459EF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_0C2_RetF => new( new FhMethodLocation("FFX.exe", 0x45A070) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0C3_RetI => new( new FhMethodLocation("FFX.exe", 0x45A130) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0C4_RetI => new( new FhMethodLocation("FFX.exe", 0x45A340) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0C5_RetI => new( new FhMethodLocation("FFX.exe", 0x45A4E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0C6_RetI => new( new FhMethodLocation("FFX.exe", 0x45A670) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0C7_RetI => new( new FhMethodLocation("FFX.exe", 0x45ABB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0C8_RetI => new( new FhMethodLocation("FFX.exe", 0x45AD00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_0C9_RetF => new( new FhMethodLocation("FFX.exe", 0x45B560) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0CA_RetI => new( new FhMethodLocation("FFX.exe", 0x45B700) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0CB_RetI => new( new FhMethodLocation("FFX.exe", 0x45B820) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0CC_RetI => new( new FhMethodLocation("FFX.exe", 0x45B9F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0CD_RetI => new( new FhMethodLocation("FFX.exe", 0x45BBE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0CE_RetI => new( new FhMethodLocation("FFX.exe", 0x45C2B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0CF_RetI => new( new FhMethodLocation("FFX.exe", 0x45C400) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0D0_RetI => new( new FhMethodLocation("FFX.exe", 0x45C4B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0D1_RetI => new( new FhMethodLocation("FFX.exe", 0x45C5B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0D2_RetI => new( new FhMethodLocation("FFX.exe", 0x45C6F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0D3_RetI => new( new FhMethodLocation("FFX.exe", 0x45C7D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0D4_RetI => new( new FhMethodLocation("FFX.exe", 0x45C910) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_0D5_Init => new( new FhMethodLocation("FFX.exe", 0x45CD00) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_0D5_Exec => new( new FhMethodLocation("FFX.exe", 0x45CEC0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0D5_RetI => new( new FhMethodLocation("FFX.exe", 0x45D360) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_0D6_Init => new( new FhMethodLocation("FFX.exe", 0x45D6B0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_0D6_Exec => new( new FhMethodLocation("FFX.exe", 0x45D9D0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0D6_RetI => new( new FhMethodLocation("FFX.exe", 0x45DE80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0D7_RetI => new( new FhMethodLocation("FFX.exe", 0x45E090) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0D8_RetI => new( new FhMethodLocation("FFX.exe", 0x45E1D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_0D9_Exec => new( new FhMethodLocation("FFX.exe", 0x45E4A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0DA_RetI => new( new FhMethodLocation("FFX.exe", 0x457C30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_0DB_Init => new( new FhMethodLocation("FFX.exe", 0x45E9E0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_0DB_Exec => new( new FhMethodLocation("FFX.exe", 0x45EAF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_0DC_Init => new( new FhMethodLocation("FFX.exe", 0x45EE00) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_0DC_Exec => new( new FhMethodLocation("FFX.exe", 0x45EE70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0DD_RetI => new( new FhMethodLocation("FFX.exe", 0x45F070) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_0DE_Exec => new( new FhMethodLocation("FFX.exe", 0x45F280) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0DF_RetI => new( new FhMethodLocation("FFX.exe", 0x45F310) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0E0_RetI => new( new FhMethodLocation("FFX.exe", 0x455F60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0E1_RetI => new( new FhMethodLocation("FFX.exe", 0x457FB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0E2_RetI => new( new FhMethodLocation("FFX.exe", 0x457DD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_0E3_RetF => new( new FhMethodLocation("FFX.exe", 0x45A480) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_0E4_RetF => new( new FhMethodLocation("FFX.exe", 0x45B030) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0E5_RetI => new( new FhMethodLocation("FFX.exe", 0x456130) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0E6_RetI => new( new FhMethodLocation("FFX.exe", 0x45BF90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0E7_RetI => new( new FhMethodLocation("FFX.exe", 0x45BDF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0E8_RetI => new( new FhMethodLocation("FFX.exe", 0x456270) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0E9_RetI => new( new FhMethodLocation("FFX.exe", 0x456660) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0EA_RetI => new( new FhMethodLocation("FFX.exe", 0x4567D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0EB_RetI => new( new FhMethodLocation("FFX.exe", 0x456980) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0EC_RetI => new( new FhMethodLocation("FFX.exe", 0x456BC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0ED_RetI => new( new FhMethodLocation("FFX.exe", 0x4571C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0EE_RetI => new( new FhMethodLocation("FFX.exe", 0x4572B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0EF_RetI => new( new FhMethodLocation("FFX.exe", 0x457470) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0F0_RetI => new( new FhMethodLocation("FFX.exe", 0x4576A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0F1_RetI => new( new FhMethodLocation("FFX.exe", 0x45D620) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0F2_RetI => new( new FhMethodLocation("FFX.exe", 0x457850) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0F3_RetI => new( new FhMethodLocation("FFX.exe", 0x457C40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_0F4_RetF => new( new FhMethodLocation("FFX.exe", 0x458970) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_0F5_RetF => new( new FhMethodLocation("FFX.exe", 0x458F40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_0F6_Init => new( new FhMethodLocation("FFX.exe", 0x457DF0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_0F6_Exec => new( new FhMethodLocation("FFX.exe", 0x457FF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_0F7_Init => new( new FhMethodLocation("FFX.exe", 0x458290) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_0F7_Exec => new( new FhMethodLocation("FFX.exe", 0x458400) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0F8_RetI => new( new FhMethodLocation("FFX.exe", 0x458520) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0F9_RetI => new( new FhMethodLocation("FFX.exe", 0x458780) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0FA_RetI => new( new FhMethodLocation("FFX.exe", 0x458920) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0FB_RetI => new( new FhMethodLocation("FFX.exe", 0x458AC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0FC_RetI => new( new FhMethodLocation("FFX.exe", 0x458DD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0FD_RetI => new( new FhMethodLocation("FFX.exe", 0x45FF30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0FE_RetI => new( new FhMethodLocation("FFX.exe", 0x460090) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_0FF_RetI => new( new FhMethodLocation("FFX.exe", 0x4602C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_100_RetI => new( new FhMethodLocation("FFX.exe", 0x460460) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_101_RetI => new( new FhMethodLocation("FFX.exe", 0x455D90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_102_Init => new( new FhMethodLocation("FFX.exe", 0x458E50) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_102_Exec => new( new FhMethodLocation("FFX.exe", 0x459040) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_102_RetI => new( new FhMethodLocation("FFX.exe", 0x459250) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_103_RetI => new( new FhMethodLocation("FFX.exe", 0x4593A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_104_RetI => new( new FhMethodLocation("FFX.exe", 0x4596D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_105_Init => new( new FhMethodLocation("FFX.exe", 0x459D70) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_105_Exec => new( new FhMethodLocation("FFX.exe", 0x459DF0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_105_RetI => new( new FhMethodLocation("FFX.exe", 0x459EE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_106_RetI => new( new FhMethodLocation("FFX.exe", 0x45A010) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_107_RetI => new( new FhMethodLocation("FFX.exe", 0x45A1F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_108_RetI => new( new FhMethodLocation("FFX.exe", 0x45A370) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_109_RetI => new( new FhMethodLocation("FFX.exe", 0x45A870) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_10A_RetI => new( new FhMethodLocation("FFX.exe", 0x45A630) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_10B_RetI => new( new FhMethodLocation("FFX.exe", 0x458460) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_10C_RetI => new( new FhMethodLocation("FFX.exe", 0x458610) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_10D_RetI => new( new FhMethodLocation("FFX.exe", 0x45AC90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_10E_RetI => new( new FhMethodLocation("FFX.exe", 0x45AF50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_10F_RetI => new( new FhMethodLocation("FFX.exe", 0x45AFD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_110_RetI => new( new FhMethodLocation("FFX.exe", 0x45B260) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_111_RetI => new( new FhMethodLocation("FFX.exe", 0x45B4B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_112_RetI => new( new FhMethodLocation("FFX.exe", 0x45B520) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_113_RetI => new( new FhMethodLocation("FFX.exe", 0x45B5C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_114_RetI => new( new FhMethodLocation("FFX.exe", 0x45B670) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_115_RetI => new( new FhMethodLocation("FFX.exe", 0x45B8C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_116_RetI => new( new FhMethodLocation("FFX.exe", 0x45B9B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_117_RetI => new( new FhMethodLocation("FFX.exe", 0x45BC50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_118_RetI => new( new FhMethodLocation("FFX.exe", 0x45BAE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_119_RetI => new( new FhMethodLocation("FFX.exe", 0x45BD70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_11A_RetI => new( new FhMethodLocation("FFX.exe", 0x45C0A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_11B_RetI => new( new FhMethodLocation("FFX.exe", 0x45C370) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_11C_RetI => new( new FhMethodLocation("FFX.exe", 0x45C4C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_11D_RetI => new( new FhMethodLocation("FFX.exe", 0x45C650) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_11E_RetI => new( new FhMethodLocation("FFX.exe", 0x45CAF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_11F_RetI => new( new FhMethodLocation("FFX.exe", 0x45CC50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_120_RetI => new( new FhMethodLocation("FFX.exe", 0x45CDE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_121_RetI => new( new FhMethodLocation("FFX.exe", 0x45D1D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_122_RetI => new( new FhMethodLocation("FFX.exe", 0x45D310) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_123_RetI => new( new FhMethodLocation("FFX.exe", 0x45D440) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_124_RetI => new( new FhMethodLocation("FFX.exe", 0x45D530) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_125_RetI => new( new FhMethodLocation("FFX.exe", 0x45FC50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_126_RetI => new( new FhMethodLocation("FFX.exe", 0x45FEA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_127_RetI => new( new FhMethodLocation("FFX.exe", 0x45DB10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_128_RetI => new( new FhMethodLocation("FFX.exe", 0x45D7B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_129_RetI => new( new FhMethodLocation("FFX.exe", 0x45C760) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_12A_RetI => new( new FhMethodLocation("FFX.exe", 0x45C890) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_12B_RetI => new( new FhMethodLocation("FFX.exe", 0x45DC50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_12C_RetI => new( new FhMethodLocation("FFX.exe", 0x45DDD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_12D_RetI => new( new FhMethodLocation("FFX.exe", 0x45DF80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_12E_RetI => new( new FhMethodLocation("FFX.exe", 0x45E040) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_12F_RetI => new( new FhMethodLocation("FFX.exe", 0x45E170) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_130_RetI => new( new FhMethodLocation("FFX.exe", 0x45E410) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_131_RetI => new( new FhMethodLocation("FFX.exe", 0x45E760) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_132_RetI => new( new FhMethodLocation("FFX.exe", 0x45FE30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_133_RetI => new( new FhMethodLocation("FFX.exe", 0x460290) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_134_RetI => new( new FhMethodLocation("FFX.exe", 0x45D3C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_135_RetI => new( new FhMethodLocation("FFX.exe", 0x45ED00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_136_RetI => new( new FhMethodLocation("FFX.exe", 0x45EE40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_137_RetI => new( new FhMethodLocation("FFX.exe", 0x45EF80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_138_RetI => new( new FhMethodLocation("FFX.exe", 0x45F140) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_139_Init => new( new FhMethodLocation("FFX.exe", 0x45F730) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_139_Exec => new( new FhMethodLocation("FFX.exe", 0x45F850) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_139_RetI => new( new FhMethodLocation("FFX.exe", 0x45F900) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_13A_Init => new( new FhMethodLocation("FFX.exe", 0x45FB30) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_13A_Exec => new( new FhMethodLocation("FFX.exe", 0x45FCD0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_13A_RetI => new( new FhMethodLocation("FFX.exe", 0x45FD40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_13B_Init => new( new FhMethodLocation("FFX.exe", 0x4602A0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_13B_Exec => new( new FhMethodLocation("FFX.exe", 0x4603D0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_13B_RetI => new( new FhMethodLocation("FFX.exe", 0x460760) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_13C_Init => new( new FhMethodLocation("FFX.exe", 0x455D70) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_13C_Exec => new( new FhMethodLocation("FFX.exe", 0x455EC0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_13C_RetI => new( new FhMethodLocation("FFX.exe", 0x456030) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_13D_Init => new( new FhMethodLocation("FFX.exe", 0x4561B0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_13D_Exec => new( new FhMethodLocation("FFX.exe", 0x456300) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_13E_RetI => new( new FhMethodLocation("FFX.exe", 0x456890) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_13F_RetI => new( new FhMethodLocation("FFX.exe", 0x456A00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_140_RetI => new( new FhMethodLocation("FFX.exe", 0x456F20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_141_RetI => new( new FhMethodLocation("FFX.exe", 0x457A00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_142_RetI => new( new FhMethodLocation("FFX.exe", 0x4578C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_143_RetI => new( new FhMethodLocation("FFX.exe", 0x457AD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_144_RetI => new( new FhMethodLocation("FFX.exe", 0x457D40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_145_RetI => new( new FhMethodLocation("FFX.exe", 0x457FC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_146_RetI => new( new FhMethodLocation("FFX.exe", 0x4581A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_147_RetI => new( new FhMethodLocation("FFX.exe", 0x458250) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_148_RetI => new( new FhMethodLocation("FFX.exe", 0x4583B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_149_RetI => new( new FhMethodLocation("FFX.exe", 0x458580) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_14A_RetI => new( new FhMethodLocation("FFX.exe", 0x458740) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_14B_RetI => new( new FhMethodLocation("FFX.exe", 0x4588E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_14C_RetI => new( new FhMethodLocation("FFX.exe", 0x458AF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_14D_RetI => new( new FhMethodLocation("FFX.exe", 0x458E10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_14E_RetF => new( new FhMethodLocation("FFX.exe", 0x458EA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_14F_RetI => new( new FhMethodLocation("FFX.exe", 0x459120) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_150_RetI => new( new FhMethodLocation("FFX.exe", 0x459380) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_151_RetI => new( new FhMethodLocation("FFX.exe", 0x459410) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_152_RetI => new( new FhMethodLocation("FFX.exe", 0x4594F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_153_RetI => new( new FhMethodLocation("FFX.exe", 0x459600) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_154_RetI => new( new FhMethodLocation("FFX.exe", 0x4597A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_155_RetI => new( new FhMethodLocation("FFX.exe", 0x459D00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_156_RetI => new( new FhMethodLocation("FFX.exe", 0x459920) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_157_RetI => new( new FhMethodLocation("FFX.exe", 0x459DC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_158_RetI => new( new FhMethodLocation("FFX.exe", 0x45CFA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_159_RetI => new( new FhMethodLocation("FFX.exe", 0x45D290) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_15A_RetI => new( new FhMethodLocation("FFX.exe", 0x457C60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_15B_Init => new( new FhMethodLocation("FFX.exe", 0x45A8A0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_15B_Exec => new( new FhMethodLocation("FFX.exe", 0x45AF70) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_15B_RetI => new( new FhMethodLocation("FFX.exe", 0x45B4C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_15C_RetI => new( new FhMethodLocation("FFX.exe", 0x45B530) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_15D_RetI => new( new FhMethodLocation("FFX.exe", 0x457A80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_15E_RetI => new( new FhMethodLocation("FFX.exe", 0x457980) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_15F_RetI => new( new FhMethodLocation("FFX.exe", 0x45B650) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_160_RetI => new( new FhMethodLocation("FFX.exe", 0x45B900) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_161_RetI => new( new FhMethodLocation("FFX.exe", 0x45B760) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_162_RetI => new( new FhMethodLocation("FFX.exe", 0x459E90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_163_RetI => new( new FhMethodLocation("FFX.exe", 0x45A0F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_164_RetI => new( new FhMethodLocation("FFX.exe", 0x459F70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_165_RetI => new( new FhMethodLocation("FFX.exe", 0x45A1D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_166_RetI => new( new FhMethodLocation("FFX.exe", 0x4598F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_167_RetI => new( new FhMethodLocation("FFX.exe", 0x459C40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_168_RetI => new( new FhMethodLocation("FFX.exe", 0x45B990) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_169_RetI => new( new FhMethodLocation("FFX.exe", 0x45BAB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_16A_RetI => new( new FhMethodLocation("FFX.exe", 0x45BBB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_16B_RetI => new( new FhMethodLocation("FFX.exe", 0x45BE70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_16C_RetI => new( new FhMethodLocation("FFX.exe", 0x4594C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_16D_RetI => new( new FhMethodLocation("FFX.exe", 0x4595B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_16E_RetI => new( new FhMethodLocation("FFX.exe", 0x45C2D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_16F_RetI => new( new FhMethodLocation("FFX.exe", 0x45C430) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_170_RetI => new( new FhMethodLocation("FFX.exe", 0x45C530) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_171_RetI => new( new FhMethodLocation("FFX.exe", 0x45C680) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_172_RetI => new( new FhMethodLocation("FFX.exe", 0x45C860) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_173_RetF => new( new FhMethodLocation("FFX.exe", 0x45E7A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_174_RetF => new( new FhMethodLocation("FFX.exe", 0x45E3A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_175_RetF => new( new FhMethodLocation("FFX.exe", 0x45EEC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_176_RetF => new( new FhMethodLocation("FFX.exe", 0x45EC30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_177_RetI => new( new FhMethodLocation("FFX.exe", 0x45C9C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_178_RetI => new( new FhMethodLocation("FFX.exe", 0x45CC30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_179_Init => new( new FhMethodLocation("FFX.exe", 0x45CD90) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_179_Exec => new( new FhMethodLocation("FFX.exe", 0x45CEA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_17A_RetI => new( new FhMethodLocation("FFX.exe", 0x45D210) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_17B_RetI => new( new FhMethodLocation("FFX.exe", 0x45D3D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_17C_RetI => new( new FhMethodLocation("FFX.exe", 0x45D480) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_17D_RetI => new( new FhMethodLocation("FFX.exe", 0x45D680) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_17E_RetI => new( new FhMethodLocation("FFX.exe", 0x45D830) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_17F_RetI => new( new FhMethodLocation("FFX.exe", 0x45BD10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_180_RetI => new( new FhMethodLocation("FFX.exe", 0x45C000) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_181_RetF => new( new FhMethodLocation("FFX.exe", 0x45E550) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_182_RetF => new( new FhMethodLocation("FFX.exe", 0x45ED10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_183_RetF => new( new FhMethodLocation("FFX.exe", 0x45F690) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_184_RetI => new( new FhMethodLocation("FFX.exe", 0x45DAC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_185_RetI => new( new FhMethodLocation("FFX.exe", 0x45DBB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_186_RetI => new( new FhMethodLocation("FFX.exe", 0x45DD50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_187_RetI => new( new FhMethodLocation("FFX.exe", 0x45DF00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_188_RetI => new( new FhMethodLocation("FFX.exe", 0x45E290) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_189_Init => new( new FhMethodLocation("FFX.exe", 0x45E890) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_189_Exec => new( new FhMethodLocation("FFX.exe", 0x45EA30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_18A_RetI => new( new FhMethodLocation("FFX.exe", 0x45EBB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_18B_RetI => new( new FhMethodLocation("FFX.exe", 0x45EC90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_18C_RetI => new( new FhMethodLocation("FFX.exe", 0x45ED60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_18D_RetI => new( new FhMethodLocation("FFX.exe", 0x45EF00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_18E_RetI => new( new FhMethodLocation("FFX.exe", 0x45F110) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_18F_RetI => new( new FhMethodLocation("FFX.exe", 0x45F240) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_190_RetI => new( new FhMethodLocation("FFX.exe", 0x45F330) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_191_RetI => new( new FhMethodLocation("FFX.exe", 0x45F5C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_192_RetF => new( new FhMethodLocation("FFX.exe", 0x45F700) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_193_RetI => new( new FhMethodLocation("FFX.exe", 0x45F4E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_194_RetI => new( new FhMethodLocation("FFX.exe", 0x45F8B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_195_Exec => new( new FhMethodLocation("FFX.exe", 0x45FB00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_196_RetI => new( new FhMethodLocation("FFX.exe", 0x45FCE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_197_RetI => new( new FhMethodLocation("FFX.exe", 0x45EDD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_198_RetI => new( new FhMethodLocation("FFX.exe", 0x45FFA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_199_RetI => new( new FhMethodLocation("FFX.exe", 0x460200) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_19A_RetI => new( new FhMethodLocation("FFX.exe", 0x456040) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_19B_RetI => new( new FhMethodLocation("FFX.exe", 0x4563E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_19C_RetI => new( new FhMethodLocation("FFX.exe", 0x456630) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_19D_RetI => new( new FhMethodLocation("FFX.exe", 0x45CA00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_19E_RetI => new( new FhMethodLocation("FFX.exe", 0x456950) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_19F_RetI => new( new FhMethodLocation("FFX.exe", 0x4567A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1A0_RetI => new( new FhMethodLocation("FFX.exe", 0x45F940) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1A1_RetI => new( new FhMethodLocation("FFX.exe", 0x456B10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1A2_RetI => new( new FhMethodLocation("FFX.exe", 0x456E90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1A3_RetI => new( new FhMethodLocation("FFX.exe", 0x457210) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1A4_RetI => new( new FhMethodLocation("FFX.exe", 0x457360) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1A5_RetI => new( new FhMethodLocation("FFX.exe", 0x4574C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1A6_RetI => new( new FhMethodLocation("FFX.exe", 0x4576D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_1A7_Init => new( new FhMethodLocation("FFX.exe", 0x457B70) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_1A7_Exec => new( new FhMethodLocation("FFX.exe", 0x457EF0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1A7_RetI => new( new FhMethodLocation("FFX.exe", 0x458230) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1A8_RetI => new( new FhMethodLocation("FFX.exe", 0x458390) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1A9_RetI => new( new FhMethodLocation("FFX.exe", 0x458500) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1AA_RetI => new( new FhMethodLocation("FFX.exe", 0x458690) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1AB_RetI => new( new FhMethodLocation("FFX.exe", 0x458800) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1AC_RetI => new( new FhMethodLocation("FFX.exe", 0x458950) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1AD_RetI => new( new FhMethodLocation("FFX.exe", 0x458B30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1AE_RetI => new( new FhMethodLocation("FFX.exe", 0x458DF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1AF_RetI => new( new FhMethodLocation("FFX.exe", 0x458E70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1B0_RetI => new( new FhMethodLocation("FFX.exe", 0x4590A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1B1_RetI => new( new FhMethodLocation("FFX.exe", 0x459240) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1B2_RetI => new( new FhMethodLocation("FFX.exe", 0x459360) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1B4_RetI => new( new FhMethodLocation("FFX.exe", 0x459490) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1B5_RetI => new( new FhMethodLocation("FFX.exe", 0x459580) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1B6_RetI => new( new FhMethodLocation("FFX.exe", 0x459630) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1B7_RetI => new( new FhMethodLocation("FFX.exe", 0x459800) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1B8_RetI => new( new FhMethodLocation("FFX.exe", 0x459BA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1B9_RetI => new( new FhMethodLocation("FFX.exe", 0x459D20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1BA_RetI => new( new FhMethodLocation("FFX.exe", 0x45BEB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1BB_RetI => new( new FhMethodLocation("FFX.exe", 0x459DE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1BC_RetI => new( new FhMethodLocation("FFX.exe", 0x459EC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1BD_RetI => new( new FhMethodLocation("FFX.exe", 0x459FF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1BE_RetI => new( new FhMethodLocation("FFX.exe", 0x45A0E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1BF_RetI => new( new FhMethodLocation("FFX.exe", 0x45A190) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1C0_RetI => new( new FhMethodLocation("FFX.exe", 0x45A250) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1C1_RetI => new( new FhMethodLocation("FFX.exe", 0x45A420) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1C2_RetI => new( new FhMethodLocation("FFX.exe", 0x45A570) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1C3_RetI => new( new FhMethodLocation("FFX.exe", 0x45A750) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1C4_RetI => new( new FhMethodLocation("FFX.exe", 0x45A840) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_1C5_Init => new( new FhMethodLocation("FFX.exe", 0x45B1A0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_1C5_Exec => new( new FhMethodLocation("FFX.exe", 0x45B590) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1C5_RetI => new( new FhMethodLocation("FFX.exe", 0x45D8F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1C6_RetI => new( new FhMethodLocation("FFX.exe", 0x45DE70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1C7_RetI => new( new FhMethodLocation("FFX.exe", 0x45E000) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1C8_RetI => new( new FhMethodLocation("FFX.exe", 0x455E50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1C9_RetI => new( new FhMethodLocation("FFX.exe", 0x460730) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1CA_RetI => new( new FhMethodLocation("FFX.exe", 0x45E340) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1CB_RetI => new( new FhMethodLocation("FFX.exe", 0x45E0E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1CC_RetI => new( new FhMethodLocation("FFX.exe", 0x45E400) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1CD_RetI => new( new FhMethodLocation("FFX.exe", 0x45E5D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1CE_RetI => new( new FhMethodLocation("FFX.exe", 0x45E7C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1CF_RetI => new( new FhMethodLocation("FFX.exe", 0x45EA40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1D0_RetI => new( new FhMethodLocation("FFX.exe", 0x45FBB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_1D1_Init => new( new FhMethodLocation("FFX.exe", 0x45FD20) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_1D1_Exec => new( new FhMethodLocation("FFX.exe", 0x45FF70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1D2_RetI => new( new FhMethodLocation("FFX.exe", 0x460180) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1D3_RetI => new( new FhMethodLocation("FFX.exe", 0x460720) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1D4_RetI => new( new FhMethodLocation("FFX.exe", 0x455DD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1D5_RetI => new( new FhMethodLocation("FFX.exe", 0x4561E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1D6_RetI => new( new FhMethodLocation("FFX.exe", 0x4564B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1D7_RetI => new( new FhMethodLocation("FFX.exe", 0x456740) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1D8_RetI => new( new FhMethodLocation("FFX.exe", 0x4567E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1D9_RetI => new( new FhMethodLocation("FFX.exe", 0x4582E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1DA_RetI => new( new FhMethodLocation("FFX.exe", 0x456A80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1DB_RetI => new( new FhMethodLocation("FFX.exe", 0x456E00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1DC_RetI => new( new FhMethodLocation("FFX.exe", 0x4571D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_1DD_Init => new( new FhMethodLocation("FFX.exe", 0x457660) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_1DD_Exec => new( new FhMethodLocation("FFX.exe", 0x4577F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_1DE_Init => new( new FhMethodLocation("FFX.exe", 0x457920) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_1DE_Exec => new( new FhMethodLocation("FFX.exe", 0x4579D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1DF_RetI => new( new FhMethodLocation("FFX.exe", 0x45F8D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1E0_RetI => new( new FhMethodLocation("FFX.exe", 0x458490) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1E1_RetI => new( new FhMethodLocation("FFX.exe", 0x4586E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1E2_RetI => new( new FhMethodLocation("FFX.exe", 0x458940) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1E3_RetI => new( new FhMethodLocation("FFX.exe", 0x458AB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1E4_RetI => new( new FhMethodLocation("FFX.exe", 0x458C90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1E5_RetI => new( new FhMethodLocation("FFX.exe", 0x459000) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1E6_RetI => new( new FhMethodLocation("FFX.exe", 0x4591D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1E7_RetI => new( new FhMethodLocation("FFX.exe", 0x459320) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1E8_RetI => new( new FhMethodLocation("FFX.exe", 0x459470) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_1E9_Init => new( new FhMethodLocation("FFX.exe", 0x4590F0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_1E9_Exec => new( new FhMethodLocation("FFX.exe", 0x4592C0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1E9_RetI => new( new FhMethodLocation("FFX.exe", 0x4595F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1EA_RetI => new( new FhMethodLocation("FFX.exe", 0x457AA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1EB_RetI => new( new FhMethodLocation("FFX.exe", 0x458070) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1EC_RetI => new( new FhMethodLocation("FFX.exe", 0x457CF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1ED_RetI => new( new FhMethodLocation("FFX.exe", 0x457F70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1EE_RetI => new( new FhMethodLocation("FFX.exe", 0x457440) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1EF_RetI => new( new FhMethodLocation("FFX.exe", 0x457320) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_1F0_RetF => new( new FhMethodLocation("FFX.exe", 0x459550) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_1F1_RetF => new( new FhMethodLocation("FFX.exe", 0x459620) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1F2_RetI => new( new FhMethodLocation("FFX.exe", 0x4597D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1F3_RetI => new( new FhMethodLocation("FFX.exe", 0x459990) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1F4_RetI => new( new FhMethodLocation("FFX.exe", 0x459FA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1F5_RetI => new( new FhMethodLocation("FFX.exe", 0x45A160) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1F6_RetI => new( new FhMethodLocation("FFX.exe", 0x45A2D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1F7_RetI => new( new FhMethodLocation("FFX.exe", 0x45A800) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1F8_RetI => new( new FhMethodLocation("FFX.exe", 0x45E570) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1F9_RetI => new( new FhMethodLocation("FFX.exe", 0x45AC50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1FA_RetI => new( new FhMethodLocation("FFX.exe", 0x45AF60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1FB_RetI => new( new FhMethodLocation("FFX.exe", 0x45AF10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1FC_RetI => new( new FhMethodLocation("FFX.exe", 0x45B170) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1FD_RetI => new( new FhMethodLocation("FFX.exe", 0x45BA20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1FE_RetI => new( new FhMethodLocation("FFX.exe", 0x45BB20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_1FF_RetI => new( new FhMethodLocation("FFX.exe", 0x45A300) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_200_RetI => new( new FhMethodLocation("FFX.exe", 0x45A430) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_201_RetI => new( new FhMethodLocation("FFX.exe", 0x45A580) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_202_RetI => new( new FhMethodLocation("FFX.exe", 0x45A770) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_203_Init => new( new FhMethodLocation("FFX.exe", 0x45D460) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_203_Exec => new( new FhMethodLocation("FFX.exe", 0x45D550) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_203_RetI => new( new FhMethodLocation("FFX.exe", 0x45DC10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_204_RetF => new( new FhMethodLocation("FFX.exe", 0x45DC90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_205_RetI => new( new FhMethodLocation("FFX.exe", 0x45EC00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_206_RetI => new( new FhMethodLocation("FFX.exe", 0x45EE10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_207_RetI => new( new FhMethodLocation("FFX.exe", 0x45F010) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_208_RetI => new( new FhMethodLocation("FFX.exe", 0x45F210) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_209_RetI => new( new FhMethodLocation("FFX.exe", 0x45F430) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_20A_RetI => new( new FhMethodLocation("FFX.exe", 0x45F660) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_20B_RetI => new( new FhMethodLocation("FFX.exe", 0x45BE30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_20C_RetI => new( new FhMethodLocation("FFX.exe", 0x45BCC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_20D_RetI => new( new FhMethodLocation("FFX.exe", 0x45BF70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_20E_RetI => new( new FhMethodLocation("FFX.exe", 0x45C090) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_20F_RetI => new( new FhMethodLocation("FFX.exe", 0x45C100) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_210_RetI => new( new FhMethodLocation("FFX.exe", 0x45C3B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_211_RetI => new( new FhMethodLocation("FFX.exe", 0x45C810) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_212_RetI => new( new FhMethodLocation("FFX.exe", 0x45C950) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_213_RetI => new( new FhMethodLocation("FFX.exe", 0x45CE30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_214_RetI => new( new FhMethodLocation("FFX.exe", 0x45CBE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_215_RetI => new( new FhMethodLocation("FFX.exe", 0x45D170) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_216_Init => new( new FhMethodLocation("FFX.exe", 0x45B280) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_216_Exec => new( new FhMethodLocation("FFX.exe", 0x45B5D0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_216_RetI => new( new FhMethodLocation("FFX.exe", 0x45B920) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_217_RetI => new( new FhMethodLocation("FFX.exe", 0x45D3A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_218_RetI => new( new FhMethodLocation("FFX.exe", 0x45D3F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_219_RetI => new( new FhMethodLocation("FFX.exe", 0x45D500) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_21A_RetI => new( new FhMethodLocation("FFX.exe", 0x45D780) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_21B_RetI => new( new FhMethodLocation("FFX.exe", 0x45D8C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_21C_RetI => new( new FhMethodLocation("FFX.exe", 0x45DB90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_21D_RetI => new( new FhMethodLocation("FFX.exe", 0x45DCD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_21E_RetI => new( new FhMethodLocation("FFX.exe", 0x458E40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_21F_RetI => new( new FhMethodLocation("FFX.exe", 0x45DFC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_220_RetI => new( new FhMethodLocation("FFX.exe", 0x45E010) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_221_RetI => new( new FhMethodLocation("FFX.exe", 0x45E110) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_222_RetI => new( new FhMethodLocation("FFX.exe", 0x45E380) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_223_RetI => new( new FhMethodLocation("FFX.exe", 0x45E460) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_224_RetI => new( new FhMethodLocation("FFX.exe", 0x45E800) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_225_RetI => new( new FhMethodLocation("FFX.exe", 0x45EA10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_226_RetI => new( new FhMethodLocation("FFX.exe", 0x45EB80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_227_RetI => new( new FhMethodLocation("FFX.exe", 0x45AC20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_228_RetI => new( new FhMethodLocation("FFX.exe", 0x45B060) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_229_RetI => new( new FhMethodLocation("FFX.exe", 0x45ECE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_22A_RetI => new( new FhMethodLocation("FFX.exe", 0x45ED40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_22B_Init => new( new FhMethodLocation("FFX.exe", 0x45EEA0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_22B_Exec => new( new FhMethodLocation("FFX.exe", 0x45EFB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_22C_RetI => new( new FhMethodLocation("FFX.exe", 0x45F2E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_22D_Init => new( new FhMethodLocation("FFX.exe", 0x45F4C0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_22D_Exec => new( new FhMethodLocation("FFX.exe", 0x45F600) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_22E_RetI => new( new FhMethodLocation("FFX.exe", 0x45F860) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_22F_RetI => new( new FhMethodLocation("FFX.exe", 0x455FB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_230_RetI => new( new FhMethodLocation("FFX.exe", 0x45FB10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_231_RetI => new( new FhMethodLocation("FFX.exe", 0x45FCA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_232_Init => new( new FhMethodLocation("FFX.exe", 0x45A4B0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_232_Exec => new( new FhMethodLocation("FFX.exe", 0x45A5F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_233_RetI => new( new FhMethodLocation("FFX.exe", 0x45FE10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_234_RetI => new( new FhMethodLocation("FFX.exe", 0x45FFF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_235_RetI => new( new FhMethodLocation("FFX.exe", 0x460170) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_236_RetI => new( new FhMethodLocation("FFX.exe", 0x460350) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_237_RetI => new( new FhMethodLocation("FFX.exe", 0x4604C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_238_RetI => new( new FhMethodLocation("FFX.exe", 0x45EB30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_239_RetI => new( new FhMethodLocation("FFX.exe", 0x45E8B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_23A_RetI => new( new FhMethodLocation("FFX.exe", 0x456250) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_23B_RetI => new( new FhMethodLocation("FFX.exe", 0x45C490) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_23C_RetI => new( new FhMethodLocation("FFX.exe", 0x45C600) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_23D_RetI => new( new FhMethodLocation("FFX.exe", 0x456520) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_23E_RetI => new( new FhMethodLocation("FFX.exe", 0x456640) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_23F_RetI => new( new FhMethodLocation("FFX.exe", 0x456770) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_240_RetI => new( new FhMethodLocation("FFX.exe", 0x4568D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_241_RetI => new( new FhMethodLocation("FFX.exe", 0x456AD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_242_RetI => new( new FhMethodLocation("FFX.exe", 0x457200) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_243_RetI => new( new FhMethodLocation("FFX.exe", 0x457350) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_244_RetI => new( new FhMethodLocation("FFX.exe", 0x4574B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_245_RetI => new( new FhMethodLocation("FFX.exe", 0x457690) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_246_RetI => new( new FhMethodLocation("FFX.exe", 0x457810) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_247_RetI => new( new FhMethodLocation("FFX.exe", 0x457910) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_248_RetI => new( new FhMethodLocation("FFX.exe", 0x4579F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_249_RetI => new( new FhMethodLocation("FFX.exe", 0x457B60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_24A_RetI => new( new FhMethodLocation("FFX.exe", 0x457CB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_24B_RetI => new( new FhMethodLocation("FFX.exe", 0x457E30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_24C_RetI => new( new FhMethodLocation("FFX.exe", 0x4580A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_24D_RetI => new( new FhMethodLocation("FFX.exe", 0x456DC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_24E_RetI => new( new FhMethodLocation("FFX.exe", 0x45D4B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_24F_RetI => new( new FhMethodLocation("FFX.exe", 0x45D890) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_250_RetI => new( new FhMethodLocation("FFX.exe", 0x45DE60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_251_RetI => new( new FhMethodLocation("FFX.exe", 0x45E5E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_252_RetI => new( new FhMethodLocation("FFX.exe", 0x45EF50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_253_RetI => new( new FhMethodLocation("FFX.exe", 0x458210) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_254_RetI => new( new FhMethodLocation("FFX.exe", 0x4585D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_255_RetI => new( new FhMethodLocation("FFX.exe", 0x458430) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_256_RetI => new( new FhMethodLocation("FFX.exe", 0x458710) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_257_RetI => new( new FhMethodLocation("FFX.exe", 0x4588B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_258_Init => new( new FhMethodLocation("FFX.exe", 0x458A60) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_258_Exec => new( new FhMethodLocation("FFX.exe", 0x458DA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Std_259_RetF => new( new FhMethodLocation("FFX.exe", 0x458FC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_25A_RetI => new( new FhMethodLocation("FFX.exe", 0x4590E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_25B_RetI => new( new FhMethodLocation("FFX.exe", 0x459270) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_25C_RetI => new( new FhMethodLocation("FFX.exe", 0x459400) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_25D_RetI => new( new FhMethodLocation("FFX.exe", 0x459C20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_25E_RetI => new( new FhMethodLocation("FFX.exe", 0x459D50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_25F_RetI => new( new FhMethodLocation("FFX.exe", 0x459E20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_260_RetI => new( new FhMethodLocation("FFX.exe", 0x459F10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_261_RetI => new( new FhMethodLocation("FFX.exe", 0x45A090) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_262_RetI => new( new FhMethodLocation("FFX.exe", 0x45A110) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_263_RetI => new( new FhMethodLocation("FFX.exe", 0x45A120) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_264_RetI => new( new FhMethodLocation("FFX.exe", 0x45A230) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_265_RetI => new( new FhMethodLocation("FFX.exe", 0x45A7F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_266_RetI => new( new FhMethodLocation("FFX.exe", 0x45A410) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Std_267_Init => new( new FhMethodLocation("FFX.exe", 0x4602A0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Std_267_Exec => new( new FhMethodLocation("FFX.exe", 0x45A540) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Std_267_RetI => new( new FhMethodLocation("FFX.exe", 0x45A620) );

    // Math (1000h-101Dh)

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Math_000_RetI => new( new FhMethodLocation("FFX.exe", 0x4779C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Math_001_RetF => new( new FhMethodLocation("FFX.exe", 0x477A70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Math_002_RetF => new( new FhMethodLocation("FFX.exe", 0x477AA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Math_003_RetF => new( new FhMethodLocation("FFX.exe", 0x477AD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Math_004_RetF => new( new FhMethodLocation("FFX.exe", 0x477B00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Math_005_RetF => new( new FhMethodLocation("FFX.exe", 0x477B30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Math_006_RetF => new( new FhMethodLocation("FFX.exe", 0x477B70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Math_007_RetF => new( new FhMethodLocation("FFX.exe", 0x477BA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Math_008_RetI => new( new FhMethodLocation("FFX.exe", 0x477BD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Math_009_RetI => new( new FhMethodLocation("FFX.exe", 0x477BF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Math_00A_RetI => new( new FhMethodLocation("FFX.exe", 0x4778B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Math_00B_RetI => new( new FhMethodLocation("FFX.exe", 0x4778D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Math_00C_RetI => new( new FhMethodLocation("FFX.exe", 0x4778F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Math_00D_RetI => new( new FhMethodLocation("FFX.exe", 0x477920) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Math_00E_RetI => new( new FhMethodLocation("FFX.exe", 0x477950) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Math_00F_RetF => new( new FhMethodLocation("FFX.exe", 0x477980) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Math_010_RetF => new( new FhMethodLocation("FFX.exe", 0x4779A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Math_011_RetF => new( new FhMethodLocation("FFX.exe", 0x477C10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Math_012_RetF => new( new FhMethodLocation("FFX.exe", 0x477C80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Math_013_RetF => new( new FhMethodLocation("FFX.exe", 0x477D30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Math_014_RetF => new( new FhMethodLocation("FFX.exe", 0x477D60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Math_015_RetI => new( new FhMethodLocation("FFX.exe", 0x477DC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Math_016_RetI => new( new FhMethodLocation("FFX.exe", 0x477E50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Math_017_RetF => new( new FhMethodLocation("FFX.exe", 0x477EF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Math_018_RetF => new( new FhMethodLocation("FFX.exe", 0x477F60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Math_019_RetF => new( new FhMethodLocation("FFX.exe", 0x477FD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Math_01A_RetF => new( new FhMethodLocation("FFX.exe", 0x477FF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Math_01B_RetF => new( new FhMethodLocation("FFX.exe", 0x478060) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Math_01C_RetI => new( new FhMethodLocation("FFX.exe", 0x4780F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Math_01D_RetI => new( new FhMethodLocation("FFX.exe", 0x478110) );

    // SgEvent (4000h-4046h)

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_000_RetI => new( new FhMethodLocation("FFX.exe", 0x677D20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_001_RetI => new( new FhMethodLocation("FFX.exe", 0x677B90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_003_RetI => new( new FhMethodLocation("FFX.exe", 0x677D30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_004_RetI => new( new FhMethodLocation("FFX.exe", 0x677D80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_005_RetI => new( new FhMethodLocation("FFX.exe", 0x677DA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_006_RetI => new( new FhMethodLocation("FFX.exe", 0x677DC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_007_RetI => new( new FhMethodLocation("FFX.exe", 0x677DE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_008_RetI => new( new FhMethodLocation("FFX.exe", 0x677E30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_009_RetI => new( new FhMethodLocation("FFX.exe", 0x677E80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_00A_RetI => new( new FhMethodLocation("FFX.exe", 0x677E00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_00B_RetI => new( new FhMethodLocation("FFX.exe", 0x677E20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_00C_RetI => new( new FhMethodLocation("FFX.exe", 0x677ED0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Sg_00D_Init => new( new FhMethodLocation("FFX.exe", 0x677EE0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Sg_00D_Exec => new( new FhMethodLocation("FFX.exe", 0x677EF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_00E_RetI => new( new FhMethodLocation("FFX.exe", 0x677F00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_00F_RetI => new( new FhMethodLocation("FFX.exe", 0x677F20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_010_RetI => new( new FhMethodLocation("FFX.exe", 0x677F40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_011_RetI => new( new FhMethodLocation("FFX.exe", 0x677F70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_012_RetI => new( new FhMethodLocation("FFX.exe", 0x677FD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_013_RetI => new( new FhMethodLocation("FFX.exe", 0x678030) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_014_RetI => new( new FhMethodLocation("FFX.exe", 0x6780A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_015_RetI => new( new FhMethodLocation("FFX.exe", 0x6780C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_016_RetI => new( new FhMethodLocation("FFX.exe", 0x6780F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_017_RetI => new( new FhMethodLocation("FFX.exe", 0x678130) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_018_RetI => new( new FhMethodLocation("FFX.exe", 0x678160) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_019_RetI => new( new FhMethodLocation("FFX.exe", 0x678180) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_01A_RetI => new( new FhMethodLocation("FFX.exe", 0x6781A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_01B_RetI => new( new FhMethodLocation("FFX.exe", 0x6781D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_01C_RetI => new( new FhMethodLocation("FFX.exe", 0x6781F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Sg_01D_Init => new( new FhMethodLocation("FFX.exe", 0x678210) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Sg_01D_Exec => new( new FhMethodLocation("FFX.exe", 0x678270) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_01D_RetI => new( new FhMethodLocation("FFX.exe", 0x6782A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_01E_RetI => new( new FhMethodLocation("FFX.exe", 0x6782C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_01F_RetI => new( new FhMethodLocation("FFX.exe", 0x6782E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_020_RetI => new( new FhMethodLocation("FFX.exe", 0x6782F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_021_RetI => new( new FhMethodLocation("FFX.exe", 0x678300) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_022_RetI => new( new FhMethodLocation("FFX.exe", 0x678320) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_023_RetI => new( new FhMethodLocation("FFX.exe", 0x678340) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_024_RetI => new( new FhMethodLocation("FFX.exe", 0x678360) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_025_RetI => new( new FhMethodLocation("FFX.exe", 0x678370) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_026_RetI => new( new FhMethodLocation("FFX.exe", 0x678450) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_027_RetI => new( new FhMethodLocation("FFX.exe", 0x6784C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_028_RetI => new( new FhMethodLocation("FFX.exe", 0x678510) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_029_RetI => new( new FhMethodLocation("FFX.exe", 0x678560) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_02A_RetI => new( new FhMethodLocation("FFX.exe", 0x6785B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_02B_RetI => new( new FhMethodLocation("FFX.exe", 0x677880) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_02C_RetI => new( new FhMethodLocation("FFX.exe", 0x6778A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Sg_02D_RetF => new( new FhMethodLocation("FFX.exe", 0x6778E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_02E_RetI => new( new FhMethodLocation("FFX.exe", 0x677900) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_02F_RetI => new( new FhMethodLocation("FFX.exe", 0x677930) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_030_RetI => new( new FhMethodLocation("FFX.exe", 0x677950) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_031_RetI => new( new FhMethodLocation("FFX.exe", 0x677970) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_032_RetI => new( new FhMethodLocation("FFX.exe", 0x677980) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Sg_033_RetF => new( new FhMethodLocation("FFX.exe", 0x6779C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Sg_034_RetF => new( new FhMethodLocation("FFX.exe", 0x6779E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Sg_035_RetF => new( new FhMethodLocation("FFX.exe", 0x677A00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_036_RetI => new( new FhMethodLocation("FFX.exe", 0x677A10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_037_RetI => new( new FhMethodLocation("FFX.exe", 0x677A70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_038_RetI => new( new FhMethodLocation("FFX.exe", 0x677AB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Sg_039_RetF => new( new FhMethodLocation("FFX.exe", 0x677AC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_03A_RetI => new( new FhMethodLocation("FFX.exe", 0x677B10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_03B_RetI => new( new FhMethodLocation("FFX.exe", 0x677B30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_03C_RetI => new( new FhMethodLocation("FFX.exe", 0x677B60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_03D_RetI => new( new FhMethodLocation("FFX.exe", 0x677BC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_03E_RetI => new( new FhMethodLocation("FFX.exe", 0x677BE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_03F_RetI => new( new FhMethodLocation("FFX.exe", 0x677C20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_040_RetI => new( new FhMethodLocation("FFX.exe", 0x677C30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_041_RetI => new( new FhMethodLocation("FFX.exe", 0x677C60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_042_RetI => new( new FhMethodLocation("FFX.exe", 0x677C70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_043_RetI => new( new FhMethodLocation("FFX.exe", 0x677C80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_044_RetI => new( new FhMethodLocation("FFX.exe", 0x677CB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_045_RetI => new( new FhMethodLocation("FFX.exe", 0x677CE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Sg_046_RetI => new( new FhMethodLocation("FFX.exe", 0x677D00) );

    // ChEvent (5000h-5090h)

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_000_RetI => new( new FhMethodLocation("FFX.exe", 0x678770) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Ch_001_Init => new( new FhMethodLocation("FFX.exe", 0x6799B0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Ch_001_Exec => new( new FhMethodLocation("FFX.exe", 0x679A10) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_001_RetI => new( new FhMethodLocation("FFX.exe", 0x679A20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_002_RetI => new( new FhMethodLocation("FFX.exe", 0x6787A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Ch_003_Init => new( new FhMethodLocation("FFX.exe", 0x6787D0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Ch_003_Exec => new( new FhMethodLocation("FFX.exe", 0x6788A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_004_RetI => new( new FhMethodLocation("FFX.exe", 0x6788D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_005_RetI => new( new FhMethodLocation("FFX.exe", 0x678940) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_006_RetI => new( new FhMethodLocation("FFX.exe", 0x678A40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_007_RetI => new( new FhMethodLocation("FFX.exe", 0x6789A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_008_RetI => new( new FhMethodLocation("FFX.exe", 0x678AC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Ch_009_RetF => new( new FhMethodLocation("FFX.exe", 0x678B30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_00A_RetI => new( new FhMethodLocation("FFX.exe", 0x678C20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_00B_RetI => new( new FhMethodLocation("FFX.exe", 0x678B90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_00C_RetI => new( new FhMethodLocation("FFX.exe", 0x678D30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_00D_RetI => new( new FhMethodLocation("FFX.exe", 0x678DF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_00E_RetI => new( new FhMethodLocation("FFX.exe", 0x678CB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_00F_RetI => new( new FhMethodLocation("FFX.exe", 0x678DA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Ch_010_Init => new( new FhMethodLocation("FFX.exe", 0x679820) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Ch_010_Exec => new( new FhMethodLocation("FFX.exe", 0x679970) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_010_RetI => new( new FhMethodLocation("FFX.exe", 0x679980) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_011_RetI => new( new FhMethodLocation("FFX.exe", 0x678E50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_013_RetI => new( new FhMethodLocation("FFX.exe", 0x678EC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_014_RetI => new( new FhMethodLocation("FFX.exe", 0x678F30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_015_RetI => new( new FhMethodLocation("FFX.exe", 0x678FB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_016_RetI => new( new FhMethodLocation("FFX.exe", 0x6790B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_017_RetI => new( new FhMethodLocation("FFX.exe", 0x679130) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_018_RetI => new( new FhMethodLocation("FFX.exe", 0x6791E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_019_RetI => new( new FhMethodLocation("FFX.exe", 0x679220) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_01A_RetI => new( new FhMethodLocation("FFX.exe", 0x679270) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_01B_RetI => new( new FhMethodLocation("FFX.exe", 0x6792D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_01D_RetI => new( new FhMethodLocation("FFX.exe", 0x679310) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Ch_01E_Init => new( new FhMethodLocation("FFX.exe", 0x679370) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Ch_01E_Exec => new( new FhMethodLocation("FFX.exe", 0x6793C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_01F_RetI => new( new FhMethodLocation("FFX.exe", 0x679450) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_020_RetI => new( new FhMethodLocation("FFX.exe", 0x6794B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_021_RetI => new( new FhMethodLocation("FFX.exe", 0x679510) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_022_RetI => new( new FhMethodLocation("FFX.exe", 0x679550) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_023_RetI => new( new FhMethodLocation("FFX.exe", 0x6795B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_024_RetI => new( new FhMethodLocation("FFX.exe", 0x679620) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_025_RetI => new( new FhMethodLocation("FFX.exe", 0x679670) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_026_RetI => new( new FhMethodLocation("FFX.exe", 0x6796E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_027_RetI => new( new FhMethodLocation("FFX.exe", 0x679860) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Ch_028_RetF => new( new FhMethodLocation("FFX.exe", 0x6799D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_029_RetI => new( new FhMethodLocation("FFX.exe", 0x679A40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_02A_RetI => new( new FhMethodLocation("FFX.exe", 0x679AF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_02B_RetI => new( new FhMethodLocation("FFX.exe", 0x679B70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_02C_RetI => new( new FhMethodLocation("FFX.exe", 0x679BC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_02D_RetI => new( new FhMethodLocation("FFX.exe", 0x679C50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_02E_RetI => new( new FhMethodLocation("FFX.exe", 0x679D10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_02F_RetI => new( new FhMethodLocation("FFX.exe", 0x679D60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_030_RetI => new( new FhMethodLocation("FFX.exe", 0x679DB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_031_RetI => new( new FhMethodLocation("FFX.exe", 0x679E00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_032_RetI => new( new FhMethodLocation("FFX.exe", 0x679E60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_033_RetI => new( new FhMethodLocation("FFX.exe", 0x679EA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_034_RetI => new( new FhMethodLocation("FFX.exe", 0x679F40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_035_RetI => new( new FhMethodLocation("FFX.exe", 0x679FA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_036_RetI => new( new FhMethodLocation("FFX.exe", 0x67A120) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_037_RetI => new( new FhMethodLocation("FFX.exe", 0x67A140) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_038_RetI => new( new FhMethodLocation("FFX.exe", 0x67A250) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Ch_039_RetF => new( new FhMethodLocation("FFX.exe", 0x67A340) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Ch_03A_RetF => new( new FhMethodLocation("FFX.exe", 0x67A3D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_03B_RetI => new( new FhMethodLocation("FFX.exe", 0x67A450) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_03C_RetI => new( new FhMethodLocation("FFX.exe", 0x67A4D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_03D_RetI => new( new FhMethodLocation("FFX.exe", 0x67A520) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_03E_RetI => new( new FhMethodLocation("FFX.exe", 0x67A570) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_03F_RetI => new( new FhMethodLocation("FFX.exe", 0x67A5A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_040_RetI => new( new FhMethodLocation("FFX.exe", 0x67A610) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_041_RetI => new( new FhMethodLocation("FFX.exe", 0x67A740) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_042_RetI => new( new FhMethodLocation("FFX.exe", 0x67A780) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_043_RetI => new( new FhMethodLocation("FFX.exe", 0x67A7C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_044_RetI => new( new FhMethodLocation("FFX.exe", 0x67A810) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_045_RetI => new( new FhMethodLocation("FFX.exe", 0x67A830) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_046_RetI => new( new FhMethodLocation("FFX.exe", 0x67A880) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_047_RetI => new( new FhMethodLocation("FFX.exe", 0x67A900) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_048_RetI => new( new FhMethodLocation("FFX.exe", 0x67A940) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Ch_049_Init => new( new FhMethodLocation("FFX.exe", 0x6785F0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Ch_049_Exec => new( new FhMethodLocation("FFX.exe", 0x678620) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_04A_RetI => new( new FhMethodLocation("FFX.exe", 0x678640) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_04B_RetI => new( new FhMethodLocation("FFX.exe", 0x678670) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_04C_RetI => new( new FhMethodLocation("FFX.exe", 0x6786A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Ch_04D_RetF => new( new FhMethodLocation("FFX.exe", 0x6786E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_04E_RetI => new( new FhMethodLocation("FFX.exe", 0x679800) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Ch_04F_Init => new( new FhMethodLocation("FFX.exe", 0x679340) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Ch_04F_Exec => new( new FhMethodLocation("FFX.exe", 0x679380) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_04F_RetI => new( new FhMethodLocation("FFX.exe", 0x679410) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_050_RetI => new( new FhMethodLocation("FFX.exe", 0x678710) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_051_RetI => new( new FhMethodLocation("FFX.exe", 0x678750) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_052_RetI => new( new FhMethodLocation("FFX.exe", 0x6787E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_053_RetI => new( new FhMethodLocation("FFX.exe", 0x678900) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_054_RetI => new( new FhMethodLocation("FFX.exe", 0x678960) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_055_RetI => new( new FhMethodLocation("FFX.exe", 0x678A10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_056_RetI => new( new FhMethodLocation("FFX.exe", 0x678A80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_057_RetI => new( new FhMethodLocation("FFX.exe", 0x678AF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_058_RetI => new( new FhMethodLocation("FFX.exe", 0x678B70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_059_RetI => new( new FhMethodLocation("FFX.exe", 0x678BD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_05A_RetI => new( new FhMethodLocation("FFX.exe", 0x678C60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_05B_RetI => new( new FhMethodLocation("FFX.exe", 0x678CE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_05C_RetI => new( new FhMethodLocation("FFX.exe", 0x678D70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_05D_RetI => new( new FhMethodLocation("FFX.exe", 0x678DD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Ch_05E_RetF => new( new FhMethodLocation("FFX.exe", 0x678E20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_05F_RetI => new( new FhMethodLocation("FFX.exe", 0x678E90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_060_RetI => new( new FhMethodLocation("FFX.exe", 0x678F00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_061_RetI => new( new FhMethodLocation("FFX.exe", 0x678F80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Ch_062_Init => new( new FhMethodLocation("FFX.exe", 0x679030) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Ch_062_Exec => new( new FhMethodLocation("FFX.exe", 0x679070) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_062_RetI => new( new FhMethodLocation("FFX.exe", 0x679120) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Ch_063_Init => new( new FhMethodLocation("FFX.exe", 0x679160) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Ch_063_Exec => new( new FhMethodLocation("FFX.exe", 0x6791A0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_063_RetI => new( new FhMethodLocation("FFX.exe", 0x679210) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Ch_064_Init => new( new FhMethodLocation("FFX.exe", 0x679240) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Ch_064_Exec => new( new FhMethodLocation("FFX.exe", 0x679290) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_064_RetI => new( new FhMethodLocation("FFX.exe", 0x679300) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_065_RetI => new( new FhMethodLocation("FFX.exe", 0x679420) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Ch_066_Init => new( new FhMethodLocation("FFX.exe", 0x679480) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Ch_066_Exec => new( new FhMethodLocation("FFX.exe", 0x6794D0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_066_RetI => new( new FhMethodLocation("FFX.exe", 0x679540) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_067_RetI => new( new FhMethodLocation("FFX.exe", 0x679580) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Ch_068_Exec => new( new FhMethodLocation("FFX.exe", 0x6795D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_069_RetI => new( new FhMethodLocation("FFX.exe", 0x6795E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_06A_RetI => new( new FhMethodLocation("FFX.exe", 0x679640) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_06B_RetI => new( new FhMethodLocation("FFX.exe", 0x6796C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_06C_RetI => new( new FhMethodLocation("FFX.exe", 0x6797B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_06D_RetI => new( new FhMethodLocation("FFX.exe", 0x679A80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_06E_RetI => new( new FhMethodLocation("FFX.exe", 0x679B30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_06F_RetI => new( new FhMethodLocation("FFX.exe", 0x679B90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_070_RetI => new( new FhMethodLocation("FFX.exe", 0x679C30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_071_RetI => new( new FhMethodLocation("FFX.exe", 0x679CC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_072_RetI => new( new FhMethodLocation("FFX.exe", 0x679CF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_073_RetI => new( new FhMethodLocation("FFX.exe", 0x679D30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_074_RetI => new( new FhMethodLocation("FFX.exe", 0x679D90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_075_RetI => new( new FhMethodLocation("FFX.exe", 0x679DD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_076_RetI => new( new FhMethodLocation("FFX.exe", 0x679E30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_077_RetI => new( new FhMethodLocation("FFX.exe", 0x679E80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_078_RetI => new( new FhMethodLocation("FFX.exe", 0x679EF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_079_RetI => new( new FhMethodLocation("FFX.exe", 0x679F70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_07A_RetI => new( new FhMethodLocation("FFX.exe", 0x679FC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_07B_RetI => new( new FhMethodLocation("FFX.exe", 0x67A000) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_07C_RetI => new( new FhMethodLocation("FFX.exe", 0x67A070) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_07D_RetI => new( new FhMethodLocation("FFX.exe", 0x67A090) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_07E_RetI => new( new FhMethodLocation("FFX.exe", 0x67A210) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_07F_RetI => new( new FhMethodLocation("FFX.exe", 0x67A380) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_080_RetI => new( new FhMethodLocation("FFX.exe", 0x67A410) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_081_RetI => new( new FhMethodLocation("FFX.exe", 0x67A4A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_082_RetI => new( new FhMethodLocation("FFX.exe", 0x67A4F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_083_RetI => new( new FhMethodLocation("FFX.exe", 0x67A540) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_084_RetI => new( new FhMethodLocation("FFX.exe", 0x67A5D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_085_RetI => new( new FhMethodLocation("FFX.exe", 0x67A640) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_086_RetI => new( new FhMethodLocation("FFX.exe", 0x67A680) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_087_RetI => new( new FhMethodLocation("FFX.exe", 0x67A6A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_088_RetI => new( new FhMethodLocation("FFX.exe", 0x67A6B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_089_RetI => new( new FhMethodLocation("FFX.exe", 0x67A700) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_08A_RetI => new( new FhMethodLocation("FFX.exe", 0x67A720) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_08B_RetI => new( new FhMethodLocation("FFX.exe", 0x67A760) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_08C_RetI => new( new FhMethodLocation("FFX.exe", 0x67A7A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_08D_RetI => new( new FhMethodLocation("FFX.exe", 0x67A7E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_08E_RetI => new( new FhMethodLocation("FFX.exe", 0x67A850) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_08F_RetI => new( new FhMethodLocation("FFX.exe", 0x67A8A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Ch_090_RetI => new( new FhMethodLocation("FFX.exe", 0x67A8D0) );

    // Camera (6000h-6089h)

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_000_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8EA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_001_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8F30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_002_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9220) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_003_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9260) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_004_RetI => new( new FhMethodLocation("FFX.exe", 0x3B92E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_005_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9320) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_006_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9360) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_007_RetI => new( new FhMethodLocation("FFX.exe", 0x3B93A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_008_RetI => new( new FhMethodLocation("FFX.exe", 0x3B93C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_009_RetI => new( new FhMethodLocation("FFX.exe", 0x3B93E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_00A_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9440) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_00B_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9480) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_00C_RetI => new( new FhMethodLocation("FFX.exe", 0x3B94C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_00D_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9500) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_00E_RetI => new( new FhMethodLocation("FFX.exe", 0x3B95A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_00F_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9520) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_010_RetI => new( new FhMethodLocation("FFX.exe", 0x3B96C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_011_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9730) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_012_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9800) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_013_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9870) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_014_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9940) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_015_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9980) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_016_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9B40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_017_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9A00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_018_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9A80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_019_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9AC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Cam_01A_Exec => new( new FhMethodLocation("FFX.exe", 0x3B9BE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_01B_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9CA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_01C_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8330) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_01D_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8470) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_01E_RetI => new( new FhMethodLocation("FFX.exe", 0x3B83D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_01F_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8530) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_020_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9D00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_021_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9D40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_022_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9E10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_023_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9E90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_024_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9F20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_025_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9FA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_026_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9FC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_027_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9FE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_028_RetI => new( new FhMethodLocation("FFX.exe", 0x3BA0E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_029_RetI => new( new FhMethodLocation("FFX.exe", 0x3BA1D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_02A_RetI => new( new FhMethodLocation("FFX.exe", 0x3BA1F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_02B_RetI => new( new FhMethodLocation("FFX.exe", 0x3BA2D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_02C_RetI => new( new FhMethodLocation("FFX.exe", 0x3BA350) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_02D_RetI => new( new FhMethodLocation("FFX.exe", 0x3BA2F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_02E_RetI => new( new FhMethodLocation("FFX.exe", 0x3B7F70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_02F_RetI => new( new FhMethodLocation("FFX.exe", 0x3B7FE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_030_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8050) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_031_RetI => new( new FhMethodLocation("FFX.exe", 0x3B80C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_032_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8130) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_033_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8150) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_034_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8210) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_035_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8170) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_036_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8190) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_037_RetI => new( new FhMethodLocation("FFX.exe", 0x3B81B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Cam_038_Exec => new( new FhMethodLocation("FFX.exe", 0x3B8270) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_039_RetI => new( new FhMethodLocation("FFX.exe", 0x3B82D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_03A_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9010) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_03B_RetI => new( new FhMethodLocation("FFX.exe", 0x3B90B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_03C_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9400) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_03D_RetI => new( new FhMethodLocation("FFX.exe", 0x3BA000) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_03E_RetI => new( new FhMethodLocation("FFX.exe", 0x3B86D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_03F_RetI => new( new FhMethodLocation("FFX.exe", 0x3B86F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_040_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8710) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_041_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8730) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_042_RetI => new( new FhMethodLocation("FFX.exe", 0x3B85F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_043_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8660) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_044_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8750) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_045_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8770) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_046_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9D20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_047_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9D60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_048_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9DA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_049_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9E30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_04A_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9EB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_04B_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9F40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_04C_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8790) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_04D_RetI => new( new FhMethodLocation("FFX.exe", 0x3B87B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_04E_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8AD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_04F_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8B80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_050_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8BA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_051_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8C30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_052_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8C00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_053_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8C60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_054_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8C90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_055_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8CC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_056_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8D10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_057_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8D40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_058_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8D70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_059_RetI => new( new FhMethodLocation("FFX.exe", 0x3BA020) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_05A_RetI => new( new FhMethodLocation("FFX.exe", 0x3BA100) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_05B_RetI => new( new FhMethodLocation("FFX.exe", 0x3BA210) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_05C_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8E80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_05D_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8F10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_05E_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9420) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_05F_RetI => new( new FhMethodLocation("FFX.exe", 0x3B97A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_060_RetI => new( new FhMethodLocation("FFX.exe", 0x3B98E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_061_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9960) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Cam_062_Exec => new( new FhMethodLocation("FFX.exe", 0x3B9A20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Cam_063_Exec => new( new FhMethodLocation("FFX.exe", 0x3B99A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Cam_064_Init => new( new FhMethodLocation("FFX.exe", 0x3B9AA0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Cam_064_Exec => new( new FhMethodLocation("FFX.exe", 0x3B9B20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_065_RetI => new( new FhMethodLocation("FFX.exe", 0x3B87D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_066_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8FA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_067_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8FC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_068_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9460) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_069_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8FF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_06A_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9090) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_06B_RetI => new( new FhMethodLocation("FFX.exe", 0x3B94A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_06C_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9150) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_06D_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9170) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_06E_RetI => new( new FhMethodLocation("FFX.exe", 0x3B94E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_06F_RetI => new( new FhMethodLocation("FFX.exe", 0x3B91D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_070_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9200) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_071_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9580) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_072_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9240) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_073_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9280) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_074_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9610) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_075_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8B50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_076_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8FE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Cam_077_RetF => new( new FhMethodLocation("FFX.exe", 0x3B9190) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Cam_078_RetF => new( new FhMethodLocation("FFX.exe", 0x3B91F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_079_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8DC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_07A_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8DE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Cam_07B_Init => new( new FhMethodLocation("FFX.exe", 0x3B8E40) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Cam_07B_Exec => new( new FhMethodLocation("FFX.exe", 0x3B8E60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Cam_07C_Init => new( new FhMethodLocation("FFX.exe", 0x3B8E00) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Cam_07C_Exec => new( new FhMethodLocation("FFX.exe", 0x3B8E20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_07D_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8840) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_07E_RetI => new( new FhMethodLocation("FFX.exe", 0x3B88B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_07F_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8920) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_080_RetI => new( new FhMethodLocation("FFX.exe", 0x3B89A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_081_RetI => new( new FhMethodLocation("FFX.exe", 0x3B92C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_082_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9300) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_083_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9340) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_084_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9380) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_085_RetI => new( new FhMethodLocation("FFX.exe", 0x3B92A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_086_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9D80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_087_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8A10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_088_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8A70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Cam_089_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9630) );

    // Battle (7000h-7127h)

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_000_RetI => new( new FhMethodLocation("FFX.exe", 0x3A8620) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_001_RetI => new( new FhMethodLocation("FFX.exe", 0x3A39D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Btl_002_Init => new( new FhMethodLocation("FFX.exe", 0x3A3530) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Btl_002_Exec => new( new FhMethodLocation("FFX.exe", 0x3A35C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_003_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3D90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_004_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3AA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_005_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4550) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_006_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3F50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_007_RetI => new( new FhMethodLocation("FFX.exe", 0x3A35A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Btl_008_Exec => new( new FhMethodLocation("FFX.exe", 0x3A3980) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_009_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3B20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_00A_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4100) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_00B_RetI => new( new FhMethodLocation("FFX.exe", 0x3A44B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_00C_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4310) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_00D_RetI => new( new FhMethodLocation("FFX.exe", 0x3A63F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_00E_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5690) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_00F_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4D50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_010_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5AF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_011_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4280) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_012_RetI => new( new FhMethodLocation("FFX.exe", 0x3A29A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_013_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5840) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_014_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4430) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_015_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4910) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_016_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2A20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_017_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4050) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_018_RetI => new( new FhMethodLocation("FFX.exe", 0x3A50C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_019_RetI => new( new FhMethodLocation("FFX.exe", 0x3A62D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_01A_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6390) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_01B_RetI => new( new FhMethodLocation("FFX.exe", 0x3A66A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_01C_RetI => new( new FhMethodLocation("FFX.exe", 0x3A59D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_01D_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5BF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_01E_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6870) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_01F_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5F80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_020_RetI => new( new FhMethodLocation("FFX.exe", 0x3A53B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_021_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6A80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_022_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4CD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_023_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4940) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_024_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4E90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_025_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5CF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_026_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5D80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_027_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5E90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_028_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6970) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_029_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3FB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_02A_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5BC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_02B_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3C50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_02C_RetI => new( new FhMethodLocation("FFX.exe", 0x3A45C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_02D_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4E60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Btl_02E_RetF => new( new FhMethodLocation("FFX.exe", 0x3A6D70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_02F_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5140) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_030_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5090) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_031_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2D00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_032_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6F70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_033_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3CC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_034_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5330) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_035_RetI => new( new FhMethodLocation("FFX.exe", 0x3A53A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_036_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7180) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_037_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6D30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_038_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6DD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_039_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2B10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_03A_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7360) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_03B_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6EF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Btl_03C_Init => new( new FhMethodLocation("FFX.exe", 0x3A5540) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Btl_03C_Exec => new( new FhMethodLocation("FFX.exe", 0x3A5680) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_03D_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5980) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Btl_03E_Exec => new( new FhMethodLocation("FFX.exe", 0x3A5A10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Btl_03F_Init => new( new FhMethodLocation("FFX.exe", 0x3A5DF0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Btl_03F_Exec => new( new FhMethodLocation("FFX.exe", 0x3A5EC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_040_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5610) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_041_RetI => new( new FhMethodLocation("FFX.exe", 0x3A56D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_042_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6060) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Btl_043_Init => new( new FhMethodLocation("FFX.exe", 0x3A6A10) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Btl_043_Exec => new( new FhMethodLocation("FFX.exe", 0x3A6A40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_044_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6E10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_045_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4DA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_046_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5220) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_047_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5270) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_048_RetI => new( new FhMethodLocation("FFX.exe", 0x3A53F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_049_RetI => new( new FhMethodLocation("FFX.exe", 0x3A55A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_04A_RetI => new( new FhMethodLocation("FFX.exe", 0x3A58D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Btl_04B_Init => new( new FhMethodLocation("FFX.exe", 0x3A7640) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Btl_04B_Exec => new( new FhMethodLocation("FFX.exe", 0x3A7850) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Btl_04C_Init => new( new FhMethodLocation("FFX.exe", 0x3A7BF0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Btl_04C_Exec => new( new FhMethodLocation("FFX.exe", 0x3A7CF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_04D_RetI => new( new FhMethodLocation("FFX.exe", 0x3A74E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_04E_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7530) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_04F_RetI => new( new FhMethodLocation("FFX.exe", 0x3A70C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Btl_050_Init => new( new FhMethodLocation("FFX.exe", 0x3A7400) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Btl_050_Exec => new( new FhMethodLocation("FFX.exe", 0x3A74F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Btl_051_Exec => new( new FhMethodLocation("FFX.exe", 0x3A5890) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_052_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7E10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_053_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4470) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_054_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7FE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_055_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3EB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_056_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6E80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_057_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4220) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_058_RetI => new( new FhMethodLocation("FFX.exe", 0x3A8160) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Btl_059_RetF => new( new FhMethodLocation("FFX.exe", 0x3A6E40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_05A_RetI => new( new FhMethodLocation("FFX.exe", 0x3A49F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Btl_05B_RetF => new( new FhMethodLocation("FFX.exe", 0x3A2D80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Btl_05C_RetF => new( new FhMethodLocation("FFX.exe", 0x3A2DF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_05D_RetI => new( new FhMethodLocation("FFX.exe", 0x3A51E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_05E_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5310) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_05F_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4C30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_060_RetI => new( new FhMethodLocation("FFX.exe", 0x3A62A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_061_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6300) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_062_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4580) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Btl_063_Exec => new( new FhMethodLocation("FFX.exe", 0x3A5DC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Btl_064_Init => new( new FhMethodLocation("FFX.exe", 0x3A63D0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Btl_064_Exec => new( new FhMethodLocation("FFX.exe", 0x3A64C0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_064_RetI => new( new FhMethodLocation("FFX.exe", 0x3A65D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Btl_065_Exec => new( new FhMethodLocation("FFX.exe", 0x3A6760) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_065_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6950) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_066_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6960) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_067_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6A30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Btl_068_Exec => new( new FhMethodLocation("FFX.exe", 0x3A6AD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_069_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6170) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_06A_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7580) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_06B_RetI => new( new FhMethodLocation("FFX.exe", 0x3A84C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_06C_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6BC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Btl_06D_Init => new( new FhMethodLocation("FFX.exe", 0x3A6D00) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Btl_06D_Exec => new( new FhMethodLocation("FFX.exe", 0x3A6DB0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_06D_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6EA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Btl_06E_Init => new( new FhMethodLocation("FFX.exe", 0x3A7020) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Btl_06E_Exec => new( new FhMethodLocation("FFX.exe", 0x3A71F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_06F_RetI => new( new FhMethodLocation("FFX.exe", 0x3A36C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_070_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7010) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_071_RetI => new( new FhMethodLocation("FFX.exe", 0x3A70B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_072_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6330) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_073_RetI => new( new FhMethodLocation("FFX.exe", 0x3A85F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_074_RetI => new( new FhMethodLocation("FFX.exe", 0x3A29F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Btl_075_Init => new( new FhMethodLocation("FFX.exe", 0x3A78F0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Btl_075_Exec => new( new FhMethodLocation("FFX.exe", 0x3A7AA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_076_RetI => new( new FhMethodLocation("FFX.exe", 0x3A56F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_077_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5450) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_078_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6540) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_079_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7200) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_07A_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2A80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_07B_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7470) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Btl_07C_Exec => new( new FhMethodLocation("FFX.exe", 0x3A7260) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_07D_RetI => new( new FhMethodLocation("FFX.exe", 0x3A81F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_07E_RetI => new( new FhMethodLocation("FFX.exe", 0x3A27E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_07F_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5E50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_080_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2C70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Btl_081_Exec => new( new FhMethodLocation("FFX.exe", 0x3A5D60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_082_RetI => new( new FhMethodLocation("FFX.exe", 0x3A41E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_083_RetI => new( new FhMethodLocation("FFX.exe", 0x3A42A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_084_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4440) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_085_RetI => new( new FhMethodLocation("FFX.exe", 0x3A43A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_086_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2CE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_087_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7880) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_088_RetI => new( new FhMethodLocation("FFX.exe", 0x3A79C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_089_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7C40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_08A_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3710) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_08B_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3900) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_08C_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3920) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_08D_RetI => new( new FhMethodLocation("FFX.exe", 0x3A76E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_08E_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5C20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_08F_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6700) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_090_RetI => new( new FhMethodLocation("FFX.exe", 0x3A49C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_091_RetI => new( new FhMethodLocation("FFX.exe", 0x3A77C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_092_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5F10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_093_RetI => new( new FhMethodLocation("FFX.exe", 0x3A60F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_094_RetI => new( new FhMethodLocation("FFX.exe", 0x3A72C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_095_RetI => new( new FhMethodLocation("FFX.exe", 0x3A61D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_096_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7B30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Btl_097_Init => new( new FhMethodLocation("FFX.exe", 0x3A5780) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Btl_097_Exec => new( new FhMethodLocation("FFX.exe", 0x3A58C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_098_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2DC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_099_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7340) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_09A_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2EB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Btl_09B_RetF => new( new FhMethodLocation("FFX.exe", 0x3A3170) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Btl_09C_RetF => new( new FhMethodLocation("FFX.exe", 0x3A3330) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Btl_09D_Exec => new( new FhMethodLocation("FFX.exe", 0x3A3AE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_09E_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2FE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Btl_09F_RetF => new( new FhMethodLocation("FFX.exe", 0x3A3460) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0A0_RetI => new( new FhMethodLocation("FFX.exe", 0x3A73D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0A1_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6B40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Btl_0A2_Init => new( new FhMethodLocation("FFX.exe", 0x3A7440) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Btl_0A2_Exec => new( new FhMethodLocation("FFX.exe", 0x3A7500) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0A3_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3730) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0A4_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7630) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0A5_RetI => new( new FhMethodLocation("FFX.exe", 0x3A76F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0A6_RetI => new( new FhMethodLocation("FFX.exe", 0x3A77A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Btl_0A7_RetF => new( new FhMethodLocation("FFX.exe", 0x3A54F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0A8_RetI => new( new FhMethodLocation("FFX.exe", 0x3A57E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Btl_0A9_Init => new( new FhMethodLocation("FFX.exe", 0x3A6C50) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Btl_0A9_Exec => new( new FhMethodLocation("FFX.exe", 0x3A6CB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0AA_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4EA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0AB_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5360) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Btl_0AC_RetF => new( new FhMethodLocation("FFX.exe", 0x3A5650) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0AD_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2CD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0AE_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7790) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0AF_RetI => new( new FhMethodLocation("FFX.exe", 0x3A79A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0B0_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4F80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0B1_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4A60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0B2_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5930) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0B3_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7DF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0B4_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7130) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0B5_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2F40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0B6_RetI => new( new FhMethodLocation("FFX.exe", 0x3A52B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0B7_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6640) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0B8_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7860) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Btl_0B9_RetF => new( new FhMethodLocation("FFX.exe", 0x3A2EE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Btl_0BA_RetF => new( new FhMethodLocation("FFX.exe", 0x3A3040) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0BB_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4CF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Btl_0BC_Init => new( new FhMethodLocation("FFX.exe", 0x3A5990) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Btl_0BC_Exec => new( new FhMethodLocation("FFX.exe", 0x3A5B90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Btl_0BD_Init => new( new FhMethodLocation("FFX.exe", 0x3A5ED0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Btl_0BD_Exec => new( new FhMethodLocation("FFX.exe", 0x3A6030) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Btl_0BE_RetF => new( new FhMethodLocation("FFX.exe", 0x3A3780) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0BF_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6AE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Btl_0C0_RetF => new( new FhMethodLocation("FFX.exe", 0x3A6C70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0C1_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4B90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Btl_0C2_RetF => new( new FhMethodLocation("FFX.exe", 0x3A31D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Btl_0C3_RetF => new( new FhMethodLocation("FFX.exe", 0x3A3650) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0C4_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2BA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0C5_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7C20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0C6_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3950) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Btl_0C7_RetF => new( new FhMethodLocation("FFX.exe", 0x3A39F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0C8_RetI => new( new FhMethodLocation("FFX.exe", 0x3A75E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0C9_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7740) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0CA_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7AB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0CB_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7B40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0CC_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7CB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0CD_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7D60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0CE_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7F20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0CF_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7F70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0D0_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4380) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0D1_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5CB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0D2_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6130) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0D3_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4A90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0D4_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4F20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0D5_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7FA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0D6_RetI => new( new FhMethodLocation("FFX.exe", 0x3A80B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0D7_RetI => new( new FhMethodLocation("FFX.exe", 0x3A30F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0D8_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3400) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0D9_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3500) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0DA_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4DF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0DB_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3450) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0DC_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7A10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0DD_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7F80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0DE_RetI => new( new FhMethodLocation("FFX.exe", 0x3A79B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0DF_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4EE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0E0_RetI => new( new FhMethodLocation("FFX.exe", 0x3A8450) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0E1_RetI => new( new FhMethodLocation("FFX.exe", 0x3A85A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0E2_RetI => new( new FhMethodLocation("FFX.exe", 0x3A32A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0E3_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7DA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0E4_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2B50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0E5_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4C70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0E6_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4E00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0E7_RetI => new( new FhMethodLocation("FFX.exe", 0x3A8680) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0E8_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7A90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0E9_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7B00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0EA_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7B10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0EB_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7D10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0EC_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2C50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0ED_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2CA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0EE_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7B90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0EF_RetI => new( new FhMethodLocation("FFX.exe", 0x3A64F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Btl_0F0_RetF => new( new FhMethodLocation("FFX.exe", 0x3A3AC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0F1_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2D30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0F2_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2E60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0F3_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3300) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0F4_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2AD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0F5_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2F80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0F6_RetI => new( new FhMethodLocation("FFX.exe", 0x3A34E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0F7_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3560) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0F8_RetI => new( new FhMethodLocation("FFX.exe", 0x3A36E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0F9_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5FC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0FA_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3420) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0FB_RetI => new( new FhMethodLocation("FFX.exe", 0x3A37E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0FC_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3440) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0FD_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3A10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0FE_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3B80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_0FF_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3D00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_100_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7D20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_101_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7F60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_102_RetI => new( new FhMethodLocation("FFX.exe", 0x3A35D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_103_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3880) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_104_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6600) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Btl_105_Exec => new( new FhMethodLocation("FFX.exe", 0x3A6730) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_106_RetI => new( new FhMethodLocation("FFX.exe", 0x3A30A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_107_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6800) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_108_RetI => new( new FhMethodLocation("FFX.exe", 0x3A40A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_109_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3DF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_10A_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3970) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Btl_10B_Exec => new( new FhMethodLocation("FFX.exe", 0x3A3A80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_10C_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2C10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_10D_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3C20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_10E_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3C00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_10F_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3C90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_110_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3D30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_111_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3FF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_112_RetI => new( new FhMethodLocation("FFX.exe", 0x3A43D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_113_RetI => new( new FhMethodLocation("FFX.exe", 0x3A8090) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_114_RetI => new( new FhMethodLocation("FFX.exe", 0x3A80A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_115_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4560) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_116_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6930) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_117_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4B00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_118_RetI => new( new FhMethodLocation("FFX.exe", 0x3A81D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_119_RetI => new( new FhMethodLocation("FFX.exe", 0x3A8430) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_11A_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4160) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_11B_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3DD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_11C_RetI => new( new FhMethodLocation("FFX.exe", 0x3A8530) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_11D_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4330) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_11E_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3F40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_11F_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6200) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_120_RetI => new( new FhMethodLocation("FFX.exe", 0x3A65A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_121_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6790) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_122_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3EF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_123_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2980) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_124_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7EE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_125_RetI => new( new FhMethodLocation("FFX.exe", 0x3A8650) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_126_RetI => new( new FhMethodLocation("FFX.exe", 0x3A8550) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Btl_127_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5240) );


    // MapFunc (8000h-806Bh)

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_000_RetI => new( new FhMethodLocation("FFX.exe", 0x51C530) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_001_RetI => new( new FhMethodLocation("FFX.exe", 0x51C5B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_002_RetI => new( new FhMethodLocation("FFX.exe", 0x51C670) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_003_RetI => new( new FhMethodLocation("FFX.exe", 0x51C7A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Map_004_Init => new( new FhMethodLocation("FFX.exe", 0x51AF60) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Map_004_Exec => new( new FhMethodLocation("FFX.exe", 0x51AFA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Map_005_Init => new( new FhMethodLocation("FFX.exe", 0x51C7E0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Map_005_Exec => new( new FhMethodLocation("FFX.exe", 0x51C860) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Map_006_Init => new( new FhMethodLocation("FFX.exe", 0x51AFF0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Map_006_Exec => new( new FhMethodLocation("FFX.exe", 0x51B040) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_007_RetI => new( new FhMethodLocation("FFX.exe", 0x51B050) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_008_RetI => new( new FhMethodLocation("FFX.exe", 0x51B060) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_009_RetI => new( new FhMethodLocation("FFX.exe", 0x51B070) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_00A_RetI => new( new FhMethodLocation("FFX.exe", 0x51B0A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_00B_RetI => new( new FhMethodLocation("FFX.exe", 0x51B130) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_00C_RetI => new( new FhMethodLocation("FFX.exe", 0x51B160) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_00D_RetI => new( new FhMethodLocation("FFX.exe", 0x51C740) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_00E_RetI => new( new FhMethodLocation("FFX.exe", 0x51B190) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_00F_RetI => new( new FhMethodLocation("FFX.exe", 0x51B1A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_010_RetI => new( new FhMethodLocation("FFX.exe", 0x51B1E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_011_RetI => new( new FhMethodLocation("FFX.exe", 0x51B200) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_012_RetI => new( new FhMethodLocation("FFX.exe", 0x51B240) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_013_RetI => new( new FhMethodLocation("FFX.exe", 0x51B260) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_014_RetI => new( new FhMethodLocation("FFX.exe", 0x51B280) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Map_015_RetF => new( new FhMethodLocation("FFX.exe", 0x51B2C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_016_RetI => new( new FhMethodLocation("FFX.exe", 0x51B2E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_017_RetI => new( new FhMethodLocation("FFX.exe", 0x51B320) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_018_RetI => new( new FhMethodLocation("FFX.exe", 0x51B340) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_019_RetI => new( new FhMethodLocation("FFX.exe", 0x51B360) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_01A_RetI => new( new FhMethodLocation("FFX.exe", 0x51B3A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_01B_RetI => new( new FhMethodLocation("FFX.exe", 0x51B3C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_01C_RetI => new( new FhMethodLocation("FFX.exe", 0x51B3E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_01D_RetI => new( new FhMethodLocation("FFX.exe", 0x51B400) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_01E_RetI => new( new FhMethodLocation("FFX.exe", 0x51B430) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_01F_RetI => new( new FhMethodLocation("FFX.exe", 0x51B450) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_020_RetI => new( new FhMethodLocation("FFX.exe", 0x51B470) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_021_RetI => new( new FhMethodLocation("FFX.exe", 0x51B490) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_022_RetI => new( new FhMethodLocation("FFX.exe", 0x51B4B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_023_RetI => new( new FhMethodLocation("FFX.exe", 0x51B4E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Map_024_RetF => new( new FhMethodLocation("FFX.exe", 0x51B530) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Map_025_RetF => new( new FhMethodLocation("FFX.exe", 0x51B540) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_026_RetI => new( new FhMethodLocation("FFX.exe", 0x51B550) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_027_RetI => new( new FhMethodLocation("FFX.exe", 0x51B5C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_028_RetI => new( new FhMethodLocation("FFX.exe", 0x51B5D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Map_029_RetF => new( new FhMethodLocation("FFX.exe", 0x51B600) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_02A_RetI => new( new FhMethodLocation("FFX.exe", 0x51B610) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_02B_RetI => new( new FhMethodLocation("FFX.exe", 0x51B670) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_02C_RetI => new( new FhMethodLocation("FFX.exe", 0x51B680) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_02D_RetI => new( new FhMethodLocation("FFX.exe", 0x51B690) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_02E_RetI => new( new FhMethodLocation("FFX.exe", 0x51B6A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_02F_RetI => new( new FhMethodLocation("FFX.exe", 0x51B6D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_030_RetI => new( new FhMethodLocation("FFX.exe", 0x51B6F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_031_RetI => new( new FhMethodLocation("FFX.exe", 0x51B720) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_032_RetI => new( new FhMethodLocation("FFX.exe", 0x51B740) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Map_033_RetF => new( new FhMethodLocation("FFX.exe", 0x51B770) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_034_RetI => new( new FhMethodLocation("FFX.exe", 0x51B780) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_035_RetI => new( new FhMethodLocation("FFX.exe", 0x51B790) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_036_RetI => new( new FhMethodLocation("FFX.exe", 0x51B7D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_037_RetI => new( new FhMethodLocation("FFX.exe", 0x51B810) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_038_RetI => new( new FhMethodLocation("FFX.exe", 0x51B8B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Map_039_RetF => new( new FhMethodLocation("FFX.exe", 0x51B930) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_03A_RetI => new( new FhMethodLocation("FFX.exe", 0x51B960) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Map_03B_RetF => new( new FhMethodLocation("FFX.exe", 0x51B9E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_03C_RetI => new( new FhMethodLocation("FFX.exe", 0x51BA10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Map_03D_RetF => new( new FhMethodLocation("FFX.exe", 0x51BAA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_03E_RetI => new( new FhMethodLocation("FFX.exe", 0x51BAD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_03F_RetI => new( new FhMethodLocation("FFX.exe", 0x51BBB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_040_RetI => new( new FhMethodLocation("FFX.exe", 0x51BC80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_041_RetI => new( new FhMethodLocation("FFX.exe", 0x51BD50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_042_RetI => new( new FhMethodLocation("FFX.exe", 0x51BE20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_043_RetI => new( new FhMethodLocation("FFX.exe", 0x51BE50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_044_RetI => new( new FhMethodLocation("FFX.exe", 0x51BE80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_045_RetI => new( new FhMethodLocation("FFX.exe", 0x51BEB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_046_RetI => new( new FhMethodLocation("FFX.exe", 0x51BF10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_047_RetI => new( new FhMethodLocation("FFX.exe", 0x51BF60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_048_RetI => new( new FhMethodLocation("FFX.exe", 0x51BFB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_049_RetI => new( new FhMethodLocation("FFX.exe", 0x51C000) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_04A_RetI => new( new FhMethodLocation("FFX.exe", 0x51C030) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_04B_RetI => new( new FhMethodLocation("FFX.exe", 0x51C060) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_04C_RetI => new( new FhMethodLocation("FFX.exe", 0x51C090) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_04D_RetI => new( new FhMethodLocation("FFX.exe", 0x51C0D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_04E_RetI => new( new FhMethodLocation("FFX.exe", 0x51C110) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_04F_RetI => new( new FhMethodLocation("FFX.exe", 0x51C150) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Map_050_RetF => new( new FhMethodLocation("FFX.exe", 0x51C1A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Map_051_RetF => new( new FhMethodLocation("FFX.exe", 0x51C1C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_052_RetI => new( new FhMethodLocation("FFX.exe", 0x51C1E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_053_RetI => new( new FhMethodLocation("FFX.exe", 0x51C230) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_054_RetI => new( new FhMethodLocation("FFX.exe", 0x51C280) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_055_RetI => new( new FhMethodLocation("FFX.exe", 0x51C2D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Map_056_RetF => new( new FhMethodLocation("FFX.exe", 0x51C330) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Map_057_RetF => new( new FhMethodLocation("FFX.exe", 0x51C350) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_058_RetI => new( new FhMethodLocation("FFX.exe", 0x51C370) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_059_RetI => new( new FhMethodLocation("FFX.exe", 0x51C3C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_05A_RetI => new( new FhMethodLocation("FFX.exe", 0x51C3F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_05B_RetI => new( new FhMethodLocation("FFX.exe", 0x51C410) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_05C_RetI => new( new FhMethodLocation("FFX.exe", 0x51C470) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_05D_RetI => new( new FhMethodLocation("FFX.exe", 0x51C4A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_05E_RetI => new( new FhMethodLocation("FFX.exe", 0x51C4D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_05F_RetI => new( new FhMethodLocation("FFX.exe", 0x51C4F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_060_RetI => new( new FhMethodLocation("FFX.exe", 0x51C510) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_061_RetI => new( new FhMethodLocation("FFX.exe", 0x51C580) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_062_RetI => new( new FhMethodLocation("FFX.exe", 0x51C5A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_063_RetI => new( new FhMethodLocation("FFX.exe", 0x51C600) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_064_RetI => new( new FhMethodLocation("FFX.exe", 0x51C6B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_065_RetI => new( new FhMethodLocation("FFX.exe", 0x51C780) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_066_RetI => new( new FhMethodLocation("FFX.exe", 0x51C7C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_067_RetI => new( new FhMethodLocation("FFX.exe", 0x51C810) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_068_RetI => new( new FhMethodLocation("FFX.exe", 0x51C890) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_069_RetI => new( new FhMethodLocation("FFX.exe", 0x51AF30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_06A_RetI => new( new FhMethodLocation("FFX.exe", 0x51AF80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Map_06B_RetI => new( new FhMethodLocation("FFX.exe", 0x51AFC0) );

    // MnFunc (9000h)

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Mn_000_RetI => new( new FhMethodLocation("FFX.exe", 0x507E60) );

    // MovieFunc (B000h-B00Fh)

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Fmv_000_Init => new( new FhMethodLocation("FFX.exe", 0x36E910) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Fmv_000_Exec => new( new FhMethodLocation("FFX.exe", 0x36E980) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Fmv_000_RetI => new( new FhMethodLocation("FFX.exe", 0x36E990) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Fmv_001_Init => new( new FhMethodLocation("FFX.exe", 0x36E9B0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Fmv_001_Exec => new( new FhMethodLocation("FFX.exe", 0x36E9C0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Fmv_001_RetI => new( new FhMethodLocation("FFX.exe", 0x36E9F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Fmv_002_RetI => new( new FhMethodLocation("FFX.exe", 0x36EA30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Fmv_003_RetI => new( new FhMethodLocation("FFX.exe", 0x36EA40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Fmv_004_Init => new( new FhMethodLocation("FFX.exe", 0x36EA50) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Fmv_004_Exec => new( new FhMethodLocation("FFX.exe", 0x36EA60) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Fmv_004_RetI => new( new FhMethodLocation("FFX.exe", 0x36EAC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Fmv_005_RetI => new( new FhMethodLocation("FFX.exe", 0x36EAD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Fmv_006_RetI => new( new FhMethodLocation("FFX.exe", 0x36EAE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Fmv_007_RetI => new( new FhMethodLocation("FFX.exe", 0x36EAF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Fmv_008_Init => new( new FhMethodLocation("FFX.exe", 0x36EB00) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Fmv_008_Exec => new( new FhMethodLocation("FFX.exe", 0x36EB10) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Fmv_008_RetI => new( new FhMethodLocation("FFX.exe", 0x36EB20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Fmv_009_Init => new( new FhMethodLocation("FFX.exe", 0x36EB30) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Fmv_009_Exec => new( new FhMethodLocation("FFX.exe", 0x36EB40) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Fmv_009_RetI => new( new FhMethodLocation("FFX.exe", 0x36EB60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Fmv_00A_Init => new( new FhMethodLocation("FFX.exe", 0x36EB80) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Fmv_00A_Exec => new( new FhMethodLocation("FFX.exe", 0x36EBE0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Fmv_00A_RetI => new( new FhMethodLocation("FFX.exe", 0x36EC10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Fmv_00B_Init => new( new FhMethodLocation("FFX.exe", 0x36EC20) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Fmv_00B_Exec => new( new FhMethodLocation("FFX.exe", 0x36EC30) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Fmv_00B_RetI => new( new FhMethodLocation("FFX.exe", 0x36EC40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Fmv_00C_RetI => new( new FhMethodLocation("FFX.exe", 0x36EC90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Fmv_00D_RetI => new( new FhMethodLocation("FFX.exe", 0x36ECB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Fmv_00E_RetI => new( new FhMethodLocation("FFX.exe", 0x36ECC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Fmv_00F_RetI => new( new FhMethodLocation("FFX.exe", 0x36ECD0) );

    // Debug (C000h-C05Dh)

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_000_RetI => new( new FhMethodLocation("FFX.exe", 0x478420) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_001_RetI => new( new FhMethodLocation("FFX.exe", 0x478490) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_002_RetI => new( new FhMethodLocation("FFX.exe", 0x4784E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_003_RetI => new( new FhMethodLocation("FFX.exe", 0x478510) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_004_RetI => new( new FhMethodLocation("FFX.exe", 0x478570) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_005_RetI => new( new FhMethodLocation("FFX.exe", 0x4785B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_006_RetI => new( new FhMethodLocation("FFX.exe", 0x4785E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_007_RetI => new( new FhMethodLocation("FFX.exe", 0x478600) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_008_RetI => new( new FhMethodLocation("FFX.exe", 0x478390) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_009_RetI => new( new FhMethodLocation("FFX.exe", 0x478630) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_00B_RetI => new( new FhMethodLocation("FFX.exe", 0x478690) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_00C_RetI => new( new FhMethodLocation("FFX.exe", 0x478650) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_00D_RetI => new( new FhMethodLocation("FFX.exe", 0x4786D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_00E_RetI => new( new FhMethodLocation("FFX.exe", 0x478700) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_00F_RetI => new( new FhMethodLocation("FFX.exe", 0x478750) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_010_RetI => new( new FhMethodLocation("FFX.exe", 0x478770) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_011_RetI => new( new FhMethodLocation("FFX.exe", 0x478790) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_012_RetI => new( new FhMethodLocation("FFX.exe", 0x4787A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_013_RetI => new( new FhMethodLocation("FFX.exe", 0x4787B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_014_RetI => new( new FhMethodLocation("FFX.exe", 0x4787C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_015_RetI => new( new FhMethodLocation("FFX.exe", 0x4787D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_016_RetI => new( new FhMethodLocation("FFX.exe", 0x478860) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_017_RetI => new( new FhMethodLocation("FFX.exe", 0x478880) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_018_RetI => new( new FhMethodLocation("FFX.exe", 0x4788A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_019_RetI => new( new FhMethodLocation("FFX.exe", 0x478550) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_01A_RetI => new( new FhMethodLocation("FFX.exe", 0x4788E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_01B_RetI => new( new FhMethodLocation("FFX.exe", 0x478920) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_01C_RetI => new( new FhMethodLocation("FFX.exe", 0x478960) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_01D_RetI => new( new FhMethodLocation("FFX.exe", 0x4789A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_01E_RetI => new( new FhMethodLocation("FFX.exe", 0x4789E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_01F_RetI => new( new FhMethodLocation("FFX.exe", 0x478A20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_020_RetI => new( new FhMethodLocation("FFX.exe", 0x478AC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_021_RetI => new( new FhMethodLocation("FFX.exe", 0x478AE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_022_RetI => new( new FhMethodLocation("FFX.exe", 0x478B00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_023_RetI => new( new FhMethodLocation("FFX.exe", 0x478B40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Dbg_024_Init => new( new FhMethodLocation("FFX.exe", 0x478C10) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Dbg_024_Exec => new( new FhMethodLocation("FFX.exe", 0x478C60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_025_RetI => new( new FhMethodLocation("FFX.exe", 0x478C70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_026_RetI => new( new FhMethodLocation("FFX.exe", 0x478CE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_027_RetI => new( new FhMethodLocation("FFX.exe", 0x478D20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_028_RetI => new( new FhMethodLocation("FFX.exe", 0x478D60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_029_RetI => new( new FhMethodLocation("FFX.exe", 0x478D90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_02A_RetI => new( new FhMethodLocation("FFX.exe", 0x478E10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_02B_RetI => new( new FhMethodLocation("FFX.exe", 0x478E50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_02C_RetI => new( new FhMethodLocation("FFX.exe", 0x478E60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_02D_RetI => new( new FhMethodLocation("FFX.exe", 0x478A60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_02E_RetI => new( new FhMethodLocation("FFX.exe", 0x478A90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_02F_RetI => new( new FhMethodLocation("FFX.exe", 0x478E80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_030_RetI => new( new FhMethodLocation("FFX.exe", 0x478ED0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_031_RetI => new( new FhMethodLocation("FFX.exe", 0x478530) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_032_RetI => new( new FhMethodLocation("FFX.exe", 0x478EF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_033_RetI => new( new FhMethodLocation("FFX.exe", 0x478F10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_034_RetI => new( new FhMethodLocation("FFX.exe", 0x478F20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_035_RetI => new( new FhMethodLocation("FFX.exe", 0x478F50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_036_RetI => new( new FhMethodLocation("FFX.exe", 0x478F70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_037_RetI => new( new FhMethodLocation("FFX.exe", 0x478FA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_038_RetI => new( new FhMethodLocation("FFX.exe", 0x478FE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_039_RetI => new( new FhMethodLocation("FFX.exe", 0x479040) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_03A_RetI => new( new FhMethodLocation("FFX.exe", 0x479070) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_03B_RetI => new( new FhMethodLocation("FFX.exe", 0x4790B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_03C_RetI => new( new FhMethodLocation("FFX.exe", 0x4790C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_03D_RetI => new( new FhMethodLocation("FFX.exe", 0x4790D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_03E_RetI => new( new FhMethodLocation("FFX.exe", 0x479110) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_03F_RetI => new( new FhMethodLocation("FFX.exe", 0x479130) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_040_RetI => new( new FhMethodLocation("FFX.exe", 0x479150) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_041_RetI => new( new FhMethodLocation("FFX.exe", 0x479160) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_042_RetI => new( new FhMethodLocation("FFX.exe", 0x479180) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_043_RetI => new( new FhMethodLocation("FFX.exe", 0x479190) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_044_RetI => new( new FhMethodLocation("FFX.exe", 0x4791A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_045_RetI => new( new FhMethodLocation("FFX.exe", 0x4791C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_046_RetI => new( new FhMethodLocation("FFX.exe", 0x4791E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Dbg_047_RetF => new( new FhMethodLocation("FFX.exe", 0x479200) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetF> AtelFn_Dbg_048_RetF => new( new FhMethodLocation("FFX.exe", 0x479280) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_049_RetI => new( new FhMethodLocation("FFX.exe", 0x479220) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_04A_RetI => new( new FhMethodLocation("FFX.exe", 0x4792C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_04B_RetI => new( new FhMethodLocation("FFX.exe", 0x479240) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_04C_RetI => new( new FhMethodLocation("FFX.exe", 0x479300) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_04D_RetI => new( new FhMethodLocation("FFX.exe", 0x479260) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_04E_RetI => new( new FhMethodLocation("FFX.exe", 0x479340) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_04F_RetI => new( new FhMethodLocation("FFX.exe", 0x479380) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_050_RetI => new( new FhMethodLocation("FFX.exe", 0x479390) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_051_RetI => new( new FhMethodLocation("FFX.exe", 0x4793B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_052_RetI => new( new FhMethodLocation("FFX.exe", 0x4793E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_053_RetI => new( new FhMethodLocation("FFX.exe", 0x4793F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_054_RetI => new( new FhMethodLocation("FFX.exe", 0x4781B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_055_RetI => new( new FhMethodLocation("FFX.exe", 0x4781D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_056_RetI => new( new FhMethodLocation("FFX.exe", 0x478230) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_057_RetI => new( new FhMethodLocation("FFX.exe", 0x4781F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_058_RetI => new( new FhMethodLocation("FFX.exe", 0x478280) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_059_RetI => new( new FhMethodLocation("FFX.exe", 0x4782A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_05A_RetI => new( new FhMethodLocation("FFX.exe", 0x4782F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_05B_RetI => new( new FhMethodLocation("FFX.exe", 0x478330) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_05C_RetI => new( new FhMethodLocation("FFX.exe", 0x4783C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Dbg_05D_RetI => new( new FhMethodLocation("FFX.exe", 0x478470) );

    // AbilityMap (D000h)

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetI> AtelFn_Abm_000_RetI => new( new FhMethodLocation("FFX.exe", 0x6445F0) );

    // Default (?000h-?007h)

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init> AtelFn_Nul_XXX_Init => new( new FhMethodLocation("FFX.exe", 0x477710) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec> AtelFn_Nul_XXX_Exec => new( new FhMethodLocation("FFX.exe", 0x477720) );

}
