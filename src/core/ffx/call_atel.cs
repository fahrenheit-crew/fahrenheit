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

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_000_Init => new( new FhMethodLocation("FFX.exe", 0x45C3E0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_000_Exec => new( new FhMethodLocation("FFX.exe", 0x45C570) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_001_RetI => new( new FhMethodLocation("FFX.exe", 0x45CE70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_002_RetI => new( new FhMethodLocation("FFX.exe", 0x45E790) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_003_RetI => new( new FhMethodLocation("FFX.exe", 0x45EA10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_004_RetI => new( new FhMethodLocation("FFX.exe", 0x45EB10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_005_RetI => new( new FhMethodLocation("FFX.exe", 0x45CC80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_006_RetI => new( new FhMethodLocation("FFX.exe", 0x45E4D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_007_RetI => new( new FhMethodLocation("FFX.exe", 0x45C750) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_008_RetI => new( new FhMethodLocation("FFX.exe", 0x45EDB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_009_RetI => new( new FhMethodLocation("FFX.exe", 0x45F040) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_00A_RetI => new( new FhMethodLocation("FFX.exe", 0x45F110) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_00B_RetI => new( new FhMethodLocation("FFX.exe", 0x45F260) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_00C_RetI => new( new FhMethodLocation("FFX.exe", 0x45F520) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_00D_RetI => new( new FhMethodLocation("FFX.exe", 0x4564C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_00E_RetI => new( new FhMethodLocation("FFX.exe", 0x4565D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_00F_RetI => new( new FhMethodLocation("FFX.exe", 0x456660) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_010_RetI => new( new FhMethodLocation("FFX.exe", 0x456810) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_011_RetI => new( new FhMethodLocation("FFX.exe", 0x4580C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_012_RetI => new( new FhMethodLocation("FFX.exe", 0x45F650) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_013_RetI => new( new FhMethodLocation("FFX.exe", 0x45F8E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_014_RetI => new( new FhMethodLocation("FFX.exe", 0x45FF60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_015_RetI => new( new FhMethodLocation("FFX.exe", 0x4601B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_016_RetF => new( new FhMethodLocation("FFX.exe", 0x457C60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_017_RetF => new( new FhMethodLocation("FFX.exe", 0x45EEF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_018_RetI => new( new FhMethodLocation("FFX.exe", 0x456AF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_019_RetI => new( new FhMethodLocation("FFX.exe", 0x4573C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_01A_Exec => new( new FhMethodLocation("FFX.exe", 0x457860) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_01B_Exec => new( new FhMethodLocation("FFX.exe", 0x457AA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_01C_RetF => new( new FhMethodLocation("FFX.exe", 0x455DB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_01D_RetF => new( new FhMethodLocation("FFX.exe", 0x455FF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_01E_RetF => new( new FhMethodLocation("FFX.exe", 0x456330) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_01F_RetF => new( new FhMethodLocation("FFX.exe", 0x458640) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_020_RetF => new( new FhMethodLocation("FFX.exe", 0x458C00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_021_RetF => new( new FhMethodLocation("FFX.exe", 0x459300) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_022_RetF => new( new FhMethodLocation("FFX.exe", 0x459410) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_023_RetF => new( new FhMethodLocation("FFX.exe", 0x459090) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_024_RetF => new( new FhMethodLocation("FFX.exe", 0x459210) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_025_RetF => new( new FhMethodLocation("FFX.exe", 0x459C40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_026_RetF => new( new FhMethodLocation("FFX.exe", 0x459D20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_027_RetF => new( new FhMethodLocation("FFX.exe", 0x459E70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_028_RetF => new( new FhMethodLocation("FFX.exe", 0x459500) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_029_RetF => new( new FhMethodLocation("FFX.exe", 0x459720) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_02A_RetF => new( new FhMethodLocation("FFX.exe", 0x459B30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_02B_RetF => new( new FhMethodLocation("FFX.exe", 0x459F80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_02C_RetF => new( new FhMethodLocation("FFX.exe", 0x45A0A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_02D_RetF => new( new FhMethodLocation("FFX.exe", 0x457F10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_02E_RetF => new( new FhMethodLocation("FFX.exe", 0x45A1E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_02F_RetF => new( new FhMethodLocation("FFX.exe", 0x45A490) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_030_RetF => new( new FhMethodLocation("FFX.exe", 0x45A660) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_031_RetI => new( new FhMethodLocation("FFX.exe", 0x456A40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_032_RetI => new( new FhMethodLocation("FFX.exe", 0x456AD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_033_RetI => new( new FhMethodLocation("FFX.exe", 0x45B920) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_034_RetI => new( new FhMethodLocation("FFX.exe", 0x45BF70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_035_RetI => new( new FhMethodLocation("FFX.exe", 0x45C140) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_036_RetI => new( new FhMethodLocation("FFX.exe", 0x45B380) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_037_RetI => new( new FhMethodLocation("FFX.exe", 0x45B620) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_038_RetF => new( new FhMethodLocation("FFX.exe", 0x45BAC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_039_RetF => new( new FhMethodLocation("FFX.exe", 0x45BC30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_03A_RetF => new( new FhMethodLocation("FFX.exe", 0x45BDE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_03B_RetI => new( new FhMethodLocation("FFX.exe", 0x460590) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_03C_RetI => new( new FhMethodLocation("FFX.exe", 0x4606F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_03D_RetI => new( new FhMethodLocation("FFX.exe", 0x45C250) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_03E_RetI => new( new FhMethodLocation("FFX.exe", 0x45C340) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_03F_RetF => new( new FhMethodLocation("FFX.exe", 0x45C4A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_040_RetF => new( new FhMethodLocation("FFX.exe", 0x45C5B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_041_RetF => new( new FhMethodLocation("FFX.exe", 0x45C6E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_042_RetI => new( new FhMethodLocation("FFX.exe", 0x45CA00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_043_RetI => new( new FhMethodLocation("FFX.exe", 0x45C810) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_044_RetI => new( new FhMethodLocation("FFX.exe", 0x45D350) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_045_RetI => new( new FhMethodLocation("FFX.exe", 0x45D5D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_046_RetI => new( new FhMethodLocation("FFX.exe", 0x45D910) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_047_RetI => new( new FhMethodLocation("FFX.exe", 0x45DA00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_048_RetI => new( new FhMethodLocation("FFX.exe", 0x45DBB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_049_RetI => new( new FhMethodLocation("FFX.exe", 0x45DCC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_04A_RetI => new( new FhMethodLocation("FFX.exe", 0x45DE60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_04B_RetI => new( new FhMethodLocation("FFX.exe", 0x45DF40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_04C_RetI => new( new FhMethodLocation("FFX.exe", 0x45E0C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_04D_RetI => new( new FhMethodLocation("FFX.exe", 0x45E6B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_04E_RetI => new( new FhMethodLocation("FFX.exe", 0x45E8C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_04F_RetI => new( new FhMethodLocation("FFX.exe", 0x45E290) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_050_RetI => new( new FhMethodLocation("FFX.exe", 0x45EAA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_051_RetI => new( new FhMethodLocation("FFX.exe", 0x45EF20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_052_RetI => new( new FhMethodLocation("FFX.exe", 0x45F0E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_053_RetI => new( new FhMethodLocation("FFX.exe", 0x45EBE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_054_RetI => new( new FhMethodLocation("FFX.exe", 0x45F1A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_055_RetI => new( new FhMethodLocation("FFX.exe", 0x45F860) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_056_RetI => new( new FhMethodLocation("FFX.exe", 0x45FA30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_057_RetI => new( new FhMethodLocation("FFX.exe", 0x460220) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_058_RetI => new( new FhMethodLocation("FFX.exe", 0x455E20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_059_RetI => new( new FhMethodLocation("FFX.exe", 0x460610) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_05A_RetI => new( new FhMethodLocation("FFX.exe", 0x455FB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_05B_RetI => new( new FhMethodLocation("FFX.exe", 0x456490) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_05C_RetI => new( new FhMethodLocation("FFX.exe", 0x45FF00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_05D_RetI => new( new FhMethodLocation("FFX.exe", 0x456540) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_05E_RetI => new( new FhMethodLocation("FFX.exe", 0x456650) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_05F_Exec => new( new FhMethodLocation("FFX.exe", 0x45C320) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_060_RetI => new( new FhMethodLocation("FFX.exe", 0x4567B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_061_RetF => new( new FhMethodLocation("FFX.exe", 0x457020) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_062_RetF => new( new FhMethodLocation("FFX.exe", 0x457180) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_063_RetF => new( new FhMethodLocation("FFX.exe", 0x457390) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_064_RetI => new( new FhMethodLocation("FFX.exe", 0x457710) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_065_RetI => new( new FhMethodLocation("FFX.exe", 0x457F60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_066_RetI => new( new FhMethodLocation("FFX.exe", 0x4581D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_067_RetI => new( new FhMethodLocation("FFX.exe", 0x4586F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_068_RetI => new( new FhMethodLocation("FFX.exe", 0x4584E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_069_RetI => new( new FhMethodLocation("FFX.exe", 0x458810) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_06A_RetI => new( new FhMethodLocation("FFX.exe", 0x4589F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_06B_RetI => new( new FhMethodLocation("FFX.exe", 0x458F00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_06C_RetF => new( new FhMethodLocation("FFX.exe", 0x457E30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_06D_RetF => new( new FhMethodLocation("FFX.exe", 0x45AA40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_06E_RetF => new( new FhMethodLocation("FFX.exe", 0x45AD00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_06F_RetF => new( new FhMethodLocation("FFX.exe", 0x45ADD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_070_RetF => new( new FhMethodLocation("FFX.exe", 0x45B0D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_071_RetF => new( new FhMethodLocation("FFX.exe", 0x45B320) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_072_RetI => new( new FhMethodLocation("FFX.exe", 0x45B9F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_073_RetI => new( new FhMethodLocation("FFX.exe", 0x45BB30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_074_RetI => new( new FhMethodLocation("FFX.exe", 0x45BC60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_075_RetI => new( new FhMethodLocation("FFX.exe", 0x45BDB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_076_RetI => new( new FhMethodLocation("FFX.exe", 0x45F420) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_077_RetI => new( new FhMethodLocation("FFX.exe", 0x45B4E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_078_RetI => new( new FhMethodLocation("FFX.exe", 0x45B7F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_079_RetI => new( new FhMethodLocation("FFX.exe", 0x45BEE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_07A_RetI => new( new FhMethodLocation("FFX.exe", 0x45BFD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_07B_RetI => new( new FhMethodLocation("FFX.exe", 0x457D40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_07C_Init => new( new FhMethodLocation("FFX.exe", 0x45B660) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_07C_Exec => new( new FhMethodLocation("FFX.exe", 0x45B820) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_07C_RetI => new( new FhMethodLocation("FFX.exe", 0x45B8F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_07D_Init => new( new FhMethodLocation("FFX.exe", 0x4595B0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_07D_Exec => new( new FhMethodLocation("FFX.exe", 0x459A80) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_07D_RetI => new( new FhMethodLocation("FFX.exe", 0x45A590) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_07E_RetI => new( new FhMethodLocation("FFX.exe", 0x45C3D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_07F_RetI => new( new FhMethodLocation("FFX.exe", 0x45C270) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_080_RetF => new( new FhMethodLocation("FFX.exe", 0x45C620) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_081_RetF => new( new FhMethodLocation("FFX.exe", 0x45C730) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_082_RetF => new( new FhMethodLocation("FFX.exe", 0x45C8A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_083_RetF => new( new FhMethodLocation("FFX.exe", 0x45CB50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_084_Init => new( new FhMethodLocation("FFX.exe", 0x45AA70) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_084_Exec => new( new FhMethodLocation("FFX.exe", 0x45AD30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_084_RetI => new( new FhMethodLocation("FFX.exe", 0x45B570) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_085_RetI => new( new FhMethodLocation("FFX.exe", 0x456300) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_086_RetI => new( new FhMethodLocation("FFX.exe", 0x45CC20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_087_RetI => new( new FhMethodLocation("FFX.exe", 0x45CE00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_088_RetI => new( new FhMethodLocation("FFX.exe", 0x45CFD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_089_RetI => new( new FhMethodLocation("FFX.exe", 0x45D0F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_08A_RetI => new( new FhMethodLocation("FFX.exe", 0x45D220) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_08B_RetI => new( new FhMethodLocation("FFX.exe", 0x45D280) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_08C_RetI => new( new FhMethodLocation("FFX.exe", 0x45D720) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_08D_RetF => new( new FhMethodLocation("FFX.exe", 0x457660) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_08E_RetF => new( new FhMethodLocation("FFX.exe", 0x45D9B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_08F_RetI => new( new FhMethodLocation("FFX.exe", 0x45DC70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_090_RetF => new( new FhMethodLocation("FFX.exe", 0x45E0A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_091_RetF => new( new FhMethodLocation("FFX.exe", 0x45DF20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_092_RetF => new( new FhMethodLocation("FFX.exe", 0x45E900) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_093_RetF => new( new FhMethodLocation("FFX.exe", 0x45E730) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_094_RetF => new( new FhMethodLocation("FFX.exe", 0x45EEC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_095_RetF => new( new FhMethodLocation("FFX.exe", 0x45EFA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_096_RetF => new( new FhMethodLocation("FFX.exe", 0x45F2A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_097_RetI => new( new FhMethodLocation("FFX.exe", 0x45F8C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_098_RetI => new( new FhMethodLocation("FFX.exe", 0x45F990) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_099_RetI => new( new FhMethodLocation("FFX.exe", 0x45FBC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_09A_RetI => new( new FhMethodLocation("FFX.exe", 0x460170) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_09B_RetI => new( new FhMethodLocation("FFX.exe", 0x45F590) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_09C_RetI => new( new FhMethodLocation("FFX.exe", 0x45FE40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_09D_RetI => new( new FhMethodLocation("FFX.exe", 0x458340) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_09E_RetI => new( new FhMethodLocation("FFX.exe", 0x4605B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_09F_RetI => new( new FhMethodLocation("FFX.exe", 0x455F30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0A0_RetI => new( new FhMethodLocation("FFX.exe", 0x455D70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0A1_RetI => new( new FhMethodLocation("FFX.exe", 0x4560C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0A2_RetI => new( new FhMethodLocation("FFX.exe", 0x456410) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0A3_RetI => new( new FhMethodLocation("FFX.exe", 0x456CC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_0A4_RetF => new( new FhMethodLocation("FFX.exe", 0x456550) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_0A5_RetF => new( new FhMethodLocation("FFX.exe", 0x456830) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0A6_RetI => new( new FhMethodLocation("FFX.exe", 0x4572A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0A7_RetI => new( new FhMethodLocation("FFX.exe", 0x4576C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0A8_RetI => new( new FhMethodLocation("FFX.exe", 0x4577F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0A9_RetI => new( new FhMethodLocation("FFX.exe", 0x457520) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0AA_RetI => new( new FhMethodLocation("FFX.exe", 0x457900) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0AB_RetI => new( new FhMethodLocation("FFX.exe", 0x458220) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0AC_RetI => new( new FhMethodLocation("FFX.exe", 0x457EC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0AD_RetI => new( new FhMethodLocation("FFX.exe", 0x458060) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0AE_RetI => new( new FhMethodLocation("FFX.exe", 0x4581C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0AF_RetI => new( new FhMethodLocation("FFX.exe", 0x4582E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0B0_RetI => new( new FhMethodLocation("FFX.exe", 0x458460) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_0B1_RetF => new( new FhMethodLocation("FFX.exe", 0x456940) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0B2_RetI => new( new FhMethodLocation("FFX.exe", 0x458550) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0B3_RetI => new( new FhMethodLocation("FFX.exe", 0x4586B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0B4_RetI => new( new FhMethodLocation("FFX.exe", 0x4588C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0B5_RetI => new( new FhMethodLocation("FFX.exe", 0x458B90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0B6_RetI => new( new FhMethodLocation("FFX.exe", 0x458CC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0B7_RetI => new( new FhMethodLocation("FFX.exe", 0x458F20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0B8_RetI => new( new FhMethodLocation("FFX.exe", 0x4590C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0B9_RetI => new( new FhMethodLocation("FFX.exe", 0x459280) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0BA_RetI => new( new FhMethodLocation("FFX.exe", 0x459340) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0BB_RetI => new( new FhMethodLocation("FFX.exe", 0x4594E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0BC_RetI => new( new FhMethodLocation("FFX.exe", 0x4593F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0BD_RetI => new( new FhMethodLocation("FFX.exe", 0x4596E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0BE_RetI => new( new FhMethodLocation("FFX.exe", 0x4597F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_0BF_RetF => new( new FhMethodLocation("FFX.exe", 0x459BD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_0C0_RetF => new( new FhMethodLocation("FFX.exe", 0x459CA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_0C1_RetF => new( new FhMethodLocation("FFX.exe", 0x459D90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_0C2_RetF => new( new FhMethodLocation("FFX.exe", 0x459F10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0C3_RetI => new( new FhMethodLocation("FFX.exe", 0x459FD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0C4_RetI => new( new FhMethodLocation("FFX.exe", 0x45A1B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0C5_RetI => new( new FhMethodLocation("FFX.exe", 0x45A350) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0C6_RetI => new( new FhMethodLocation("FFX.exe", 0x45A510) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0C7_RetI => new( new FhMethodLocation("FFX.exe", 0x45AA20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0C8_RetI => new( new FhMethodLocation("FFX.exe", 0x45AB70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_0C9_RetF => new( new FhMethodLocation("FFX.exe", 0x45B400) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0CA_RetI => new( new FhMethodLocation("FFX.exe", 0x45B5A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0CB_RetI => new( new FhMethodLocation("FFX.exe", 0x45B6C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0CC_RetI => new( new FhMethodLocation("FFX.exe", 0x45B890) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0CD_RetI => new( new FhMethodLocation("FFX.exe", 0x45BA50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0CE_RetI => new( new FhMethodLocation("FFX.exe", 0x45C120) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0CF_RetI => new( new FhMethodLocation("FFX.exe", 0x45C230) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0D0_RetI => new( new FhMethodLocation("FFX.exe", 0x45C330) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0D1_RetI => new( new FhMethodLocation("FFX.exe", 0x45C400) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0D2_RetI => new( new FhMethodLocation("FFX.exe", 0x45C520) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0D3_RetI => new( new FhMethodLocation("FFX.exe", 0x45C640) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0D4_RetI => new( new FhMethodLocation("FFX.exe", 0x45C780) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_0D5_Init => new( new FhMethodLocation("FFX.exe", 0x45CB70) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_0D5_Exec => new( new FhMethodLocation("FFX.exe", 0x45CD30) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0D5_RetI => new( new FhMethodLocation("FFX.exe", 0x45D150) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_0D6_Init => new( new FhMethodLocation("FFX.exe", 0x45D520) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_0D6_Exec => new( new FhMethodLocation("FFX.exe", 0x45D820) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0D6_RetI => new( new FhMethodLocation("FFX.exe", 0x45DCF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0D7_RetI => new( new FhMethodLocation("FFX.exe", 0x45DF00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0D8_RetI => new( new FhMethodLocation("FFX.exe", 0x45E000) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_0D9_Exec => new( new FhMethodLocation("FFX.exe", 0x45E310) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0DA_RetI => new( new FhMethodLocation("FFX.exe", 0x457AD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_0DB_Init => new( new FhMethodLocation("FFX.exe", 0x45E760) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_0DB_Exec => new( new FhMethodLocation("FFX.exe", 0x45E930) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_0DC_Init => new( new FhMethodLocation("FFX.exe", 0x45EC10) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_0DC_Exec => new( new FhMethodLocation("FFX.exe", 0x45ECB0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0DD_RetI => new( new FhMethodLocation("FFX.exe", 0x45EE80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_0DE_Exec => new( new FhMethodLocation("FFX.exe", 0x45F0C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0DF_RetI => new( new FhMethodLocation("FFX.exe", 0x45F150) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0E0_RetI => new( new FhMethodLocation("FFX.exe", 0x455E00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0E1_RetI => new( new FhMethodLocation("FFX.exe", 0x457E20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0E2_RetI => new( new FhMethodLocation("FFX.exe", 0x457C40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_0E3_RetF => new( new FhMethodLocation("FFX.exe", 0x45A3B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_0E4_RetF => new( new FhMethodLocation("FFX.exe", 0x45AED0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0E5_RetI => new( new FhMethodLocation("FFX.exe", 0x455F70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0E6_RetI => new( new FhMethodLocation("FFX.exe", 0x45BE30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0E7_RetI => new( new FhMethodLocation("FFX.exe", 0x45BC90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0E8_RetI => new( new FhMethodLocation("FFX.exe", 0x456110) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0E9_RetI => new( new FhMethodLocation("FFX.exe", 0x456500) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0EA_RetI => new( new FhMethodLocation("FFX.exe", 0x456670) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0EB_RetI => new( new FhMethodLocation("FFX.exe", 0x456820) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0EC_RetI => new( new FhMethodLocation("FFX.exe", 0x456A60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0ED_RetI => new( new FhMethodLocation("FFX.exe", 0x457060) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0EE_RetI => new( new FhMethodLocation("FFX.exe", 0x457150) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0EF_RetI => new( new FhMethodLocation("FFX.exe", 0x457310) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0F0_RetI => new( new FhMethodLocation("FFX.exe", 0x457540) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0F1_RetI => new( new FhMethodLocation("FFX.exe", 0x45D3C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0F2_RetI => new( new FhMethodLocation("FFX.exe", 0x4576F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0F3_RetI => new( new FhMethodLocation("FFX.exe", 0x457AE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_0F4_RetF => new( new FhMethodLocation("FFX.exe", 0x458860) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_0F5_RetF => new( new FhMethodLocation("FFX.exe", 0x458DE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_0F6_Init => new( new FhMethodLocation("FFX.exe", 0x457C90) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_0F6_Exec => new( new FhMethodLocation("FFX.exe", 0x457E90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_0F7_Init => new( new FhMethodLocation("FFX.exe", 0x458130) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_0F7_Exec => new( new FhMethodLocation("FFX.exe", 0x4582A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0F8_RetI => new( new FhMethodLocation("FFX.exe", 0x4583C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0F9_RetI => new( new FhMethodLocation("FFX.exe", 0x458620) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0FA_RetI => new( new FhMethodLocation("FFX.exe", 0x4587C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0FB_RetI => new( new FhMethodLocation("FFX.exe", 0x458960) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0FC_RetI => new( new FhMethodLocation("FFX.exe", 0x458C70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0FD_RetI => new( new FhMethodLocation("FFX.exe", 0x45FCE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0FE_RetI => new( new FhMethodLocation("FFX.exe", 0x45FED0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_0FF_RetI => new( new FhMethodLocation("FFX.exe", 0x460100) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_100_RetI => new( new FhMethodLocation("FFX.exe", 0x4602A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_101_RetI => new( new FhMethodLocation("FFX.exe", 0x4606B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_102_Init => new( new FhMethodLocation("FFX.exe", 0x458CF0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_102_Exec => new( new FhMethodLocation("FFX.exe", 0x458EE0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_102_RetI => new( new FhMethodLocation("FFX.exe", 0x4590F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_103_RetI => new( new FhMethodLocation("FFX.exe", 0x459240) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_104_RetI => new( new FhMethodLocation("FFX.exe", 0x459570) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_105_Init => new( new FhMethodLocation("FFX.exe", 0x459C10) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_105_Exec => new( new FhMethodLocation("FFX.exe", 0x459C90) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_105_RetI => new( new FhMethodLocation("FFX.exe", 0x459D80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_106_RetI => new( new FhMethodLocation("FFX.exe", 0x459EB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_107_RetI => new( new FhMethodLocation("FFX.exe", 0x45A060) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_108_RetI => new( new FhMethodLocation("FFX.exe", 0x45A210) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_109_RetI => new( new FhMethodLocation("FFX.exe", 0x45A710) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_10A_RetI => new( new FhMethodLocation("FFX.exe", 0x45A4D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_10B_RetI => new( new FhMethodLocation("FFX.exe", 0x458370) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_10C_RetI => new( new FhMethodLocation("FFX.exe", 0x4584B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_10D_RetI => new( new FhMethodLocation("FFX.exe", 0x45AB30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_10E_RetI => new( new FhMethodLocation("FFX.exe", 0x45ADC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_10F_RetI => new( new FhMethodLocation("FFX.exe", 0x45AE70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_110_RetI => new( new FhMethodLocation("FFX.exe", 0x45B100) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_111_RetI => new( new FhMethodLocation("FFX.exe", 0x45B350) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_112_RetI => new( new FhMethodLocation("FFX.exe", 0x45B3C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_113_RetI => new( new FhMethodLocation("FFX.exe", 0x45B460) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_114_RetI => new( new FhMethodLocation("FFX.exe", 0x45B510) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_115_RetI => new( new FhMethodLocation("FFX.exe", 0x45B760) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_116_RetI => new( new FhMethodLocation("FFX.exe", 0x45B850) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_117_RetI => new( new FhMethodLocation("FFX.exe", 0x45BAF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_118_RetI => new( new FhMethodLocation("FFX.exe", 0x45B980) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_119_RetI => new( new FhMethodLocation("FFX.exe", 0x45BBE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_11A_RetI => new( new FhMethodLocation("FFX.exe", 0x45BF10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_11B_RetI => new( new FhMethodLocation("FFX.exe", 0x45C1C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_11C_RetI => new( new FhMethodLocation("FFX.exe", 0x45C360) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_11D_RetI => new( new FhMethodLocation("FFX.exe", 0x45C4C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_11E_RetI => new( new FhMethodLocation("FFX.exe", 0x45C870) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_11F_RetI => new( new FhMethodLocation("FFX.exe", 0x45CAC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_120_RetI => new( new FhMethodLocation("FFX.exe", 0x45CC30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_121_RetI => new( new FhMethodLocation("FFX.exe", 0x45D040) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_122_RetI => new( new FhMethodLocation("FFX.exe", 0x45D100) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_123_RetI => new( new FhMethodLocation("FFX.exe", 0x45D2B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_124_RetI => new( new FhMethodLocation("FFX.exe", 0x45D3A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_125_RetI => new( new FhMethodLocation("FFX.exe", 0x45FB10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_126_RetI => new( new FhMethodLocation("FFX.exe", 0x45FD20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_127_RetI => new( new FhMethodLocation("FFX.exe", 0x45D980) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_128_RetI => new( new FhMethodLocation("FFX.exe", 0x45D620) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_129_RetI => new( new FhMethodLocation("FFX.exe", 0x45C5D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_12A_RetI => new( new FhMethodLocation("FFX.exe", 0x45C700) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_12B_RetI => new( new FhMethodLocation("FFX.exe", 0x45DA80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_12C_RetI => new( new FhMethodLocation("FFX.exe", 0x45DC30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_12D_RetI => new( new FhMethodLocation("FFX.exe", 0x45DDF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_12E_RetI => new( new FhMethodLocation("FFX.exe", 0x45DEB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_12F_RetI => new( new FhMethodLocation("FFX.exe", 0x45DFA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_130_RetI => new( new FhMethodLocation("FFX.exe", 0x45E240) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_131_RetI => new( new FhMethodLocation("FFX.exe", 0x45E490) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_132_RetI => new( new FhMethodLocation("FFX.exe", 0x45FC70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_133_RetI => new( new FhMethodLocation("FFX.exe", 0x4600D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_134_RetI => new( new FhMethodLocation("FFX.exe", 0x45D250) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_135_RetI => new( new FhMethodLocation("FFX.exe", 0x45EB40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_136_RetI => new( new FhMethodLocation("FFX.exe", 0x45EC80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_137_RetI => new( new FhMethodLocation("FFX.exe", 0x45EDC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_138_RetI => new( new FhMethodLocation("FFX.exe", 0x45EF80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_139_Init => new( new FhMethodLocation("FFX.exe", 0x45F570) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_139_Exec => new( new FhMethodLocation("FFX.exe", 0x45F640) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_139_RetI => new( new FhMethodLocation("FFX.exe", 0x45F740) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_13A_Init => new( new FhMethodLocation("FFX.exe", 0x45F970) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_13A_Exec => new( new FhMethodLocation("FFX.exe", 0x45FAC0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_13A_RetI => new( new FhMethodLocation("FFX.exe", 0x45FB80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_13B_Init => new( new FhMethodLocation("FFX.exe", 0x4600E0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_13B_Exec => new( new FhMethodLocation("FFX.exe", 0x460210) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_13B_RetI => new( new FhMethodLocation("FFX.exe", 0x460580) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_13C_Init => new( new FhMethodLocation("FFX.exe", 0x460690) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_13C_Exec => new( new FhMethodLocation("FFX.exe", 0x455D60) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_13C_RetI => new( new FhMethodLocation("FFX.exe", 0x455ED0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_13D_Init => new( new FhMethodLocation("FFX.exe", 0x456050) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_13D_Exec => new( new FhMethodLocation("FFX.exe", 0x4561A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_13E_RetI => new( new FhMethodLocation("FFX.exe", 0x456730) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_13F_RetI => new( new FhMethodLocation("FFX.exe", 0x4568A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_140_RetI => new( new FhMethodLocation("FFX.exe", 0x456DC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_141_RetI => new( new FhMethodLocation("FFX.exe", 0x4578A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_142_RetI => new( new FhMethodLocation("FFX.exe", 0x457760) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_143_RetI => new( new FhMethodLocation("FFX.exe", 0x457970) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_144_RetI => new( new FhMethodLocation("FFX.exe", 0x457BE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_145_RetI => new( new FhMethodLocation("FFX.exe", 0x457E60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_146_RetI => new( new FhMethodLocation("FFX.exe", 0x458040) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_147_RetI => new( new FhMethodLocation("FFX.exe", 0x4580F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_148_RetI => new( new FhMethodLocation("FFX.exe", 0x458250) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_149_RetI => new( new FhMethodLocation("FFX.exe", 0x458420) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_14A_RetI => new( new FhMethodLocation("FFX.exe", 0x4585E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_14B_RetI => new( new FhMethodLocation("FFX.exe", 0x458780) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_14C_RetI => new( new FhMethodLocation("FFX.exe", 0x458990) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_14D_RetI => new( new FhMethodLocation("FFX.exe", 0x458CB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_14E_RetF => new( new FhMethodLocation("FFX.exe", 0x458D40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_14F_RetI => new( new FhMethodLocation("FFX.exe", 0x458FC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_150_RetI => new( new FhMethodLocation("FFX.exe", 0x4591F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_151_RetI => new( new FhMethodLocation("FFX.exe", 0x4592B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_152_RetI => new( new FhMethodLocation("FFX.exe", 0x459390) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_153_RetI => new( new FhMethodLocation("FFX.exe", 0x4594A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_154_RetI => new( new FhMethodLocation("FFX.exe", 0x459640) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_155_RetI => new( new FhMethodLocation("FFX.exe", 0x459BA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_156_RetI => new( new FhMethodLocation("FFX.exe", 0x4597C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_157_RetI => new( new FhMethodLocation("FFX.exe", 0x459C20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_158_RetI => new( new FhMethodLocation("FFX.exe", 0x45CE10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_159_RetI => new( new FhMethodLocation("FFX.exe", 0x45D190) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_15A_RetI => new( new FhMethodLocation("FFX.exe", 0x457B00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_15B_Init => new( new FhMethodLocation("FFX.exe", 0x45A740) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_15B_Exec => new( new FhMethodLocation("FFX.exe", 0x45AE10) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_15B_RetI => new( new FhMethodLocation("FFX.exe", 0x45B360) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_15C_RetI => new( new FhMethodLocation("FFX.exe", 0x45B3D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_15D_RetI => new( new FhMethodLocation("FFX.exe", 0x457920) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_15E_RetI => new( new FhMethodLocation("FFX.exe", 0x457820) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_15F_RetI => new( new FhMethodLocation("FFX.exe", 0x45B4C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_160_RetI => new( new FhMethodLocation("FFX.exe", 0x45B7A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_161_RetI => new( new FhMethodLocation("FFX.exe", 0x45B600) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_162_RetI => new( new FhMethodLocation("FFX.exe", 0x459CF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_163_RetI => new( new FhMethodLocation("FFX.exe", 0x459F60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_164_RetI => new( new FhMethodLocation("FFX.exe", 0x459DD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_165_RetI => new( new FhMethodLocation("FFX.exe", 0x45A040) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_166_RetI => new( new FhMethodLocation("FFX.exe", 0x459790) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_167_RetI => new( new FhMethodLocation("FFX.exe", 0x459A70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_168_RetI => new( new FhMethodLocation("FFX.exe", 0x45B830) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_169_RetI => new( new FhMethodLocation("FFX.exe", 0x45B950) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_16A_RetI => new( new FhMethodLocation("FFX.exe", 0x45BA20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_16B_RetI => new( new FhMethodLocation("FFX.exe", 0x45BD10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_16C_RetI => new( new FhMethodLocation("FFX.exe", 0x459360) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_16D_RetI => new( new FhMethodLocation("FFX.exe", 0x459450) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_16E_RetI => new( new FhMethodLocation("FFX.exe", 0x45C170) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_16F_RetI => new( new FhMethodLocation("FFX.exe", 0x45C2C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_170_RetI => new( new FhMethodLocation("FFX.exe", 0x45C380) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_171_RetI => new( new FhMethodLocation("FFX.exe", 0x45C4F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_172_RetI => new( new FhMethodLocation("FFX.exe", 0x45C6B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_173_RetF => new( new FhMethodLocation("FFX.exe", 0x45E610) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_174_RetF => new( new FhMethodLocation("FFX.exe", 0x45E210) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_175_RetF => new( new FhMethodLocation("FFX.exe", 0x45ED00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_176_RetF => new( new FhMethodLocation("FFX.exe", 0x45EA70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_177_RetI => new( new FhMethodLocation("FFX.exe", 0x45C830) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_178_RetI => new( new FhMethodLocation("FFX.exe", 0x45CAA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_179_Init => new( new FhMethodLocation("FFX.exe", 0x45CC00) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_179_Exec => new( new FhMethodLocation("FFX.exe", 0x45CD10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_17A_RetI => new( new FhMethodLocation("FFX.exe", 0x45D080) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_17B_RetI => new( new FhMethodLocation("FFX.exe", 0x45D230) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_17C_RetI => new( new FhMethodLocation("FFX.exe", 0x45D2D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_17D_RetI => new( new FhMethodLocation("FFX.exe", 0x45D420) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_17E_RetI => new( new FhMethodLocation("FFX.exe", 0x45D6A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_17F_RetI => new( new FhMethodLocation("FFX.exe", 0x45BBB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_180_RetI => new( new FhMethodLocation("FFX.exe", 0x45BEA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_181_RetF => new( new FhMethodLocation("FFX.exe", 0x45E3C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_182_RetF => new( new FhMethodLocation("FFX.exe", 0x45EB50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_183_RetF => new( new FhMethodLocation("FFX.exe", 0x45F4B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_184_RetI => new( new FhMethodLocation("FFX.exe", 0x45D930) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_185_RetI => new( new FhMethodLocation("FFX.exe", 0x45DA20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_186_RetI => new( new FhMethodLocation("FFX.exe", 0x45DBC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_187_RetI => new( new FhMethodLocation("FFX.exe", 0x45DD70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_188_RetI => new( new FhMethodLocation("FFX.exe", 0x45E100) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_189_Init => new( new FhMethodLocation("FFX.exe", 0x45E690) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_189_Exec => new( new FhMethodLocation("FFX.exe", 0x45E870) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_18A_RetI => new( new FhMethodLocation("FFX.exe", 0x45E9F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_18B_RetI => new( new FhMethodLocation("FFX.exe", 0x45EAD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_18C_RetI => new( new FhMethodLocation("FFX.exe", 0x45EBA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_18D_RetI => new( new FhMethodLocation("FFX.exe", 0x45ED30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_18E_RetI => new( new FhMethodLocation("FFX.exe", 0x45EF50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_18F_RetI => new( new FhMethodLocation("FFX.exe", 0x45F080) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_190_RetI => new( new FhMethodLocation("FFX.exe", 0x45F170) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_191_RetI => new( new FhMethodLocation("FFX.exe", 0x45F3E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_192_RetF => new( new FhMethodLocation("FFX.exe", 0x45F540) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_193_RetI => new( new FhMethodLocation("FFX.exe", 0x45F320) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_194_RetI => new( new FhMethodLocation("FFX.exe", 0x45F6F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_195_Exec => new( new FhMethodLocation("FFX.exe", 0x45F940) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_196_RetI => new( new FhMethodLocation("FFX.exe", 0x45FAD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_197_RetI => new( new FhMethodLocation("FFX.exe", 0x45EC20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_198_RetI => new( new FhMethodLocation("FFX.exe", 0x45FDE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_199_RetI => new( new FhMethodLocation("FFX.exe", 0x460040) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_19A_RetI => new( new FhMethodLocation("FFX.exe", 0x455EE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_19B_RetI => new( new FhMethodLocation("FFX.exe", 0x456280) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_19C_RetI => new( new FhMethodLocation("FFX.exe", 0x4564D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_19D_RetI => new( new FhMethodLocation("FFX.exe", 0x45C8C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_19E_RetI => new( new FhMethodLocation("FFX.exe", 0x4567E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_19F_RetI => new( new FhMethodLocation("FFX.exe", 0x456630) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1A0_RetI => new( new FhMethodLocation("FFX.exe", 0x45F780) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1A1_RetI => new( new FhMethodLocation("FFX.exe", 0x4569B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1A2_RetI => new( new FhMethodLocation("FFX.exe", 0x456D30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1A3_RetI => new( new FhMethodLocation("FFX.exe", 0x4570B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1A4_RetI => new( new FhMethodLocation("FFX.exe", 0x457200) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1A5_RetI => new( new FhMethodLocation("FFX.exe", 0x457360) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1A6_RetI => new( new FhMethodLocation("FFX.exe", 0x457570) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_1A7_Init => new( new FhMethodLocation("FFX.exe", 0x4579E0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_1A7_Exec => new( new FhMethodLocation("FFX.exe", 0x457D90) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1A7_RetI => new( new FhMethodLocation("FFX.exe", 0x4580A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1A8_RetI => new( new FhMethodLocation("FFX.exe", 0x458200) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1A9_RetI => new( new FhMethodLocation("FFX.exe", 0x4583A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1AA_RetI => new( new FhMethodLocation("FFX.exe", 0x458530) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1AB_RetI => new( new FhMethodLocation("FFX.exe", 0x4586A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1AC_RetI => new( new FhMethodLocation("FFX.exe", 0x4587F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1AD_RetI => new( new FhMethodLocation("FFX.exe", 0x4589D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1AE_RetI => new( new FhMethodLocation("FFX.exe", 0x458C90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1AF_RetI => new( new FhMethodLocation("FFX.exe", 0x458D10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1B0_RetI => new( new FhMethodLocation("FFX.exe", 0x458F40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1B1_RetI => new( new FhMethodLocation("FFX.exe", 0x4590E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1B2_RetI => new( new FhMethodLocation("FFX.exe", 0x4591D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1B4_RetI => new( new FhMethodLocation("FFX.exe", 0x4592F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1B5_RetI => new( new FhMethodLocation("FFX.exe", 0x4593E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1B6_RetI => new( new FhMethodLocation("FFX.exe", 0x4594D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1B7_RetI => new( new FhMethodLocation("FFX.exe", 0x4596A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1B8_RetI => new( new FhMethodLocation("FFX.exe", 0x459A40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1B9_RetI => new( new FhMethodLocation("FFX.exe", 0x459BC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1BA_RetI => new( new FhMethodLocation("FFX.exe", 0x45BD50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1BB_RetI => new( new FhMethodLocation("FFX.exe", 0x459C80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1BC_RetI => new( new FhMethodLocation("FFX.exe", 0x459D60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1BD_RetI => new( new FhMethodLocation("FFX.exe", 0x459E50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1BE_RetI => new( new FhMethodLocation("FFX.exe", 0x459F50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1BF_RetI => new( new FhMethodLocation("FFX.exe", 0x45A030) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1C0_RetI => new( new FhMethodLocation("FFX.exe", 0x45A0F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1C1_RetI => new( new FhMethodLocation("FFX.exe", 0x45A2C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1C2_RetI => new( new FhMethodLocation("FFX.exe", 0x45A410) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1C3_RetI => new( new FhMethodLocation("FFX.exe", 0x45A5F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1C4_RetI => new( new FhMethodLocation("FFX.exe", 0x45A6E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_1C5_Init => new( new FhMethodLocation("FFX.exe", 0x45B040) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_1C5_Exec => new( new FhMethodLocation("FFX.exe", 0x45B430) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1C5_RetI => new( new FhMethodLocation("FFX.exe", 0x45D760) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1C6_RetI => new( new FhMethodLocation("FFX.exe", 0x45DCE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1C7_RetI => new( new FhMethodLocation("FFX.exe", 0x45DE70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1C8_RetI => new( new FhMethodLocation("FFX.exe", 0x455CF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1C9_RetI => new( new FhMethodLocation("FFX.exe", 0x460550) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1CA_RetI => new( new FhMethodLocation("FFX.exe", 0x45E1B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1CB_RetI => new( new FhMethodLocation("FFX.exe", 0x45DF50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1CC_RetI => new( new FhMethodLocation("FFX.exe", 0x45E230) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1CD_RetI => new( new FhMethodLocation("FFX.exe", 0x45E440) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1CE_RetI => new( new FhMethodLocation("FFX.exe", 0x45E630) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1CF_RetI => new( new FhMethodLocation("FFX.exe", 0x45E880) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1D0_RetI => new( new FhMethodLocation("FFX.exe", 0x45F9F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_1D1_Init => new( new FhMethodLocation("FFX.exe", 0x45FB60) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_1D1_Exec => new( new FhMethodLocation("FFX.exe", 0x45FDB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1D2_RetI => new( new FhMethodLocation("FFX.exe", 0x45FFC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1D3_RetI => new( new FhMethodLocation("FFX.exe", 0x460540) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1D4_RetI => new( new FhMethodLocation("FFX.exe", 0x455C70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1D5_RetI => new( new FhMethodLocation("FFX.exe", 0x456080) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1D6_RetI => new( new FhMethodLocation("FFX.exe", 0x456350) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1D7_RetI => new( new FhMethodLocation("FFX.exe", 0x4565E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1D8_RetI => new( new FhMethodLocation("FFX.exe", 0x456680) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1D9_RetI => new( new FhMethodLocation("FFX.exe", 0x458180) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1DA_RetI => new( new FhMethodLocation("FFX.exe", 0x4568F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1DB_RetI => new( new FhMethodLocation("FFX.exe", 0x456CA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1DC_RetI => new( new FhMethodLocation("FFX.exe", 0x457070) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_1DD_Init => new( new FhMethodLocation("FFX.exe", 0x457500) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_1DD_Exec => new( new FhMethodLocation("FFX.exe", 0x457690) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_1DE_Init => new( new FhMethodLocation("FFX.exe", 0x4577C0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_1DE_Exec => new( new FhMethodLocation("FFX.exe", 0x457840) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1DF_RetI => new( new FhMethodLocation("FFX.exe", 0x45F710) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1E0_RetI => new( new FhMethodLocation("FFX.exe", 0x458300) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1E1_RetI => new( new FhMethodLocation("FFX.exe", 0x458580) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1E2_RetI => new( new FhMethodLocation("FFX.exe", 0x4587E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1E3_RetI => new( new FhMethodLocation("FFX.exe", 0x458950) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1E4_RetI => new( new FhMethodLocation("FFX.exe", 0x458B30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1E5_RetI => new( new FhMethodLocation("FFX.exe", 0x458EA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1E6_RetI => new( new FhMethodLocation("FFX.exe", 0x459040) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1E7_RetI => new( new FhMethodLocation("FFX.exe", 0x459190) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1E8_RetI => new( new FhMethodLocation("FFX.exe", 0x4592D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_1E9_Init => new( new FhMethodLocation("FFX.exe", 0x458F90) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_1E9_Exec => new( new FhMethodLocation("FFX.exe", 0x459160) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1E9_RetI => new( new FhMethodLocation("FFX.exe", 0x459490) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1EA_RetI => new( new FhMethodLocation("FFX.exe", 0x457940) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1EB_RetI => new( new FhMethodLocation("FFX.exe", 0x457EE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1EC_RetI => new( new FhMethodLocation("FFX.exe", 0x457B90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1ED_RetI => new( new FhMethodLocation("FFX.exe", 0x457DE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1EE_RetI => new( new FhMethodLocation("FFX.exe", 0x4572E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1EF_RetI => new( new FhMethodLocation("FFX.exe", 0x4571C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_1F0_RetF => new( new FhMethodLocation("FFX.exe", 0x4593B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_1F1_RetF => new( new FhMethodLocation("FFX.exe", 0x4594C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1F2_RetI => new( new FhMethodLocation("FFX.exe", 0x459670) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1F3_RetI => new( new FhMethodLocation("FFX.exe", 0x459830) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1F4_RetI => new( new FhMethodLocation("FFX.exe", 0x459E00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1F5_RetI => new( new FhMethodLocation("FFX.exe", 0x45A000) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1F6_RetI => new( new FhMethodLocation("FFX.exe", 0x45A140) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1F7_RetI => new( new FhMethodLocation("FFX.exe", 0x45A6A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1F8_RetI => new( new FhMethodLocation("FFX.exe", 0x45E3E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1F9_RetI => new( new FhMethodLocation("FFX.exe", 0x45AAF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1FA_RetI => new( new FhMethodLocation("FFX.exe", 0x45AE00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1FB_RetI => new( new FhMethodLocation("FFX.exe", 0x45ADB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1FC_RetI => new( new FhMethodLocation("FFX.exe", 0x45B010) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1FD_RetI => new( new FhMethodLocation("FFX.exe", 0x45B8C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1FE_RetI => new( new FhMethodLocation("FFX.exe", 0x45B9C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_1FF_RetI => new( new FhMethodLocation("FFX.exe", 0x45A170) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_200_RetI => new( new FhMethodLocation("FFX.exe", 0x45A2D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_201_RetI => new( new FhMethodLocation("FFX.exe", 0x45A420) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_202_RetI => new( new FhMethodLocation("FFX.exe", 0x45A610) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_203_Init => new( new FhMethodLocation("FFX.exe", 0x45D330) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_203_Exec => new( new FhMethodLocation("FFX.exe", 0x45D450) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_203_RetI => new( new FhMethodLocation("FFX.exe", 0x45DAF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_204_RetF => new( new FhMethodLocation("FFX.exe", 0x45DAC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_205_RetI => new( new FhMethodLocation("FFX.exe", 0x45EA40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_206_RetI => new( new FhMethodLocation("FFX.exe", 0x45EC50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_207_RetI => new( new FhMethodLocation("FFX.exe", 0x45EE50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_208_RetI => new( new FhMethodLocation("FFX.exe", 0x45F050) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_209_RetI => new( new FhMethodLocation("FFX.exe", 0x45F270) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_20A_RetI => new( new FhMethodLocation("FFX.exe", 0x45F480) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_20B_RetI => new( new FhMethodLocation("FFX.exe", 0x45BCD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_20C_RetI => new( new FhMethodLocation("FFX.exe", 0x45BB60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_20D_RetI => new( new FhMethodLocation("FFX.exe", 0x45BE10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_20E_RetI => new( new FhMethodLocation("FFX.exe", 0x45BF00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_20F_RetI => new( new FhMethodLocation("FFX.exe", 0x45BFA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_210_RetI => new( new FhMethodLocation("FFX.exe", 0x45C200) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_211_RetI => new( new FhMethodLocation("FFX.exe", 0x45C680) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_212_RetI => new( new FhMethodLocation("FFX.exe", 0x45C7C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_213_RetI => new( new FhMethodLocation("FFX.exe", 0x45CCA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_214_RetI => new( new FhMethodLocation("FFX.exe", 0x45C9B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_215_RetI => new( new FhMethodLocation("FFX.exe", 0x45CFE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_216_Init => new( new FhMethodLocation("FFX.exe", 0x45B120) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_216_Exec => new( new FhMethodLocation("FFX.exe", 0x45B470) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_216_RetI => new( new FhMethodLocation("FFX.exe", 0x45B7C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_217_RetI => new( new FhMethodLocation("FFX.exe", 0x45D210) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_218_RetI => new( new FhMethodLocation("FFX.exe", 0x45D260) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_219_RetI => new( new FhMethodLocation("FFX.exe", 0x45D370) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_21A_RetI => new( new FhMethodLocation("FFX.exe", 0x45D5F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_21B_RetI => new( new FhMethodLocation("FFX.exe", 0x45D730) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_21C_RetI => new( new FhMethodLocation("FFX.exe", 0x45D9E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_21D_RetI => new( new FhMethodLocation("FFX.exe", 0x45DB30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_21E_RetI => new( new FhMethodLocation("FFX.exe", 0x458CE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_21F_RetI => new( new FhMethodLocation("FFX.exe", 0x45DE30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_220_RetI => new( new FhMethodLocation("FFX.exe", 0x45DE80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_221_RetI => new( new FhMethodLocation("FFX.exe", 0x45DF80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_222_RetI => new( new FhMethodLocation("FFX.exe", 0x45E1F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_223_RetI => new( new FhMethodLocation("FFX.exe", 0x45E2D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_224_RetI => new( new FhMethodLocation("FFX.exe", 0x45E670) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_225_RetI => new( new FhMethodLocation("FFX.exe", 0x45E850) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_226_RetI => new( new FhMethodLocation("FFX.exe", 0x45E9C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_227_RetI => new( new FhMethodLocation("FFX.exe", 0x45AAC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_228_RetI => new( new FhMethodLocation("FFX.exe", 0x45AF00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_229_RetI => new( new FhMethodLocation("FFX.exe", 0x45EAF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_22A_RetI => new( new FhMethodLocation("FFX.exe", 0x45EB80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_22B_Init => new( new FhMethodLocation("FFX.exe", 0x45ECE0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_22B_Exec => new( new FhMethodLocation("FFX.exe", 0x45EDF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_22C_RetI => new( new FhMethodLocation("FFX.exe", 0x45F120) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_22D_Init => new( new FhMethodLocation("FFX.exe", 0x45F300) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_22D_Exec => new( new FhMethodLocation("FFX.exe", 0x45F440) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_22E_RetI => new( new FhMethodLocation("FFX.exe", 0x45F6A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_22F_RetI => new( new FhMethodLocation("FFX.exe", 0x455E50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_230_RetI => new( new FhMethodLocation("FFX.exe", 0x45F950) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_231_RetI => new( new FhMethodLocation("FFX.exe", 0x45FA90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_232_Init => new( new FhMethodLocation("FFX.exe", 0x45A320) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_232_Exec => new( new FhMethodLocation("FFX.exe", 0x45A460) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_233_RetI => new( new FhMethodLocation("FFX.exe", 0x45FC50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_234_RetI => new( new FhMethodLocation("FFX.exe", 0x45FE30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_235_RetI => new( new FhMethodLocation("FFX.exe", 0x45FFB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_236_RetI => new( new FhMethodLocation("FFX.exe", 0x460130) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_237_RetI => new( new FhMethodLocation("FFX.exe", 0x4602E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_238_RetI => new( new FhMethodLocation("FFX.exe", 0x45E970) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_239_RetI => new( new FhMethodLocation("FFX.exe", 0x45E6F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_23A_RetI => new( new FhMethodLocation("FFX.exe", 0x4560F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_23B_RetI => new( new FhMethodLocation("FFX.exe", 0x45C300) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_23C_RetI => new( new FhMethodLocation("FFX.exe", 0x45C450) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_23D_RetI => new( new FhMethodLocation("FFX.exe", 0x4563C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_23E_RetI => new( new FhMethodLocation("FFX.exe", 0x4564E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_23F_RetI => new( new FhMethodLocation("FFX.exe", 0x456610) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_240_RetI => new( new FhMethodLocation("FFX.exe", 0x456770) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_241_RetI => new( new FhMethodLocation("FFX.exe", 0x456970) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_242_RetI => new( new FhMethodLocation("FFX.exe", 0x4570A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_243_RetI => new( new FhMethodLocation("FFX.exe", 0x4571F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_244_RetI => new( new FhMethodLocation("FFX.exe", 0x457350) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_245_RetI => new( new FhMethodLocation("FFX.exe", 0x457530) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_246_RetI => new( new FhMethodLocation("FFX.exe", 0x4576B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_247_RetI => new( new FhMethodLocation("FFX.exe", 0x4577B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_248_RetI => new( new FhMethodLocation("FFX.exe", 0x457890) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_249_RetI => new( new FhMethodLocation("FFX.exe", 0x4579D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_24A_RetI => new( new FhMethodLocation("FFX.exe", 0x457B50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_24B_RetI => new( new FhMethodLocation("FFX.exe", 0x457CD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_24C_RetI => new( new FhMethodLocation("FFX.exe", 0x457F40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_24D_RetI => new( new FhMethodLocation("FFX.exe", 0x456C60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_24E_RetI => new( new FhMethodLocation("FFX.exe", 0x45D300) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_24F_RetI => new( new FhMethodLocation("FFX.exe", 0x45D700) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_250_RetI => new( new FhMethodLocation("FFX.exe", 0x45DCD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_251_RetI => new( new FhMethodLocation("FFX.exe", 0x45E450) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_252_RetI => new( new FhMethodLocation("FFX.exe", 0x45ED80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_253_RetI => new( new FhMethodLocation("FFX.exe", 0x458080) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_254_RetI => new( new FhMethodLocation("FFX.exe", 0x458470) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_255_RetI => new( new FhMethodLocation("FFX.exe", 0x4582D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_256_RetI => new( new FhMethodLocation("FFX.exe", 0x4585B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_257_RetI => new( new FhMethodLocation("FFX.exe", 0x458750) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_258_Init => new( new FhMethodLocation("FFX.exe", 0x458900) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_258_Exec => new( new FhMethodLocation("FFX.exe", 0x458BD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Std_259_RetF => new( new FhMethodLocation("FFX.exe", 0x458E60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_25A_RetI => new( new FhMethodLocation("FFX.exe", 0x458F80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_25B_RetI => new( new FhMethodLocation("FFX.exe", 0x459110) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_25C_RetI => new( new FhMethodLocation("FFX.exe", 0x4592A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_25D_RetI => new( new FhMethodLocation("FFX.exe", 0x459A50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_25E_RetI => new( new FhMethodLocation("FFX.exe", 0x459BF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_25F_RetI => new( new FhMethodLocation("FFX.exe", 0x459CC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_260_RetI => new( new FhMethodLocation("FFX.exe", 0x459DB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_261_RetI => new( new FhMethodLocation("FFX.exe", 0x459F30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_262_RetI => new( new FhMethodLocation("FFX.exe", 0x459FB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_263_RetI => new( new FhMethodLocation("FFX.exe", 0x459FC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_264_RetI => new( new FhMethodLocation("FFX.exe", 0x45A0D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_265_RetI => new( new FhMethodLocation("FFX.exe", 0x45A690) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_266_RetI => new( new FhMethodLocation("FFX.exe", 0x45A2B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Std_267_Init => new( new FhMethodLocation("FFX.exe", 0x4600E0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Std_267_Exec => new( new FhMethodLocation("FFX.exe", 0x45A3E0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Std_267_RetI => new( new FhMethodLocation("FFX.exe", 0x45A4C0) );

    // Math (1000h-101Dh)

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Math_000_RetI => new( new FhMethodLocation("FFX.exe", 0x477940) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Math_001_RetF => new( new FhMethodLocation("FFX.exe", 0x4779F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Math_002_RetF => new( new FhMethodLocation("FFX.exe", 0x477A20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Math_003_RetF => new( new FhMethodLocation("FFX.exe", 0x477A50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Math_004_RetF => new( new FhMethodLocation("FFX.exe", 0x477A80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Math_005_RetF => new( new FhMethodLocation("FFX.exe", 0x477AB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Math_006_RetF => new( new FhMethodLocation("FFX.exe", 0x477AF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Math_007_RetF => new( new FhMethodLocation("FFX.exe", 0x477B20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Math_008_RetI => new( new FhMethodLocation("FFX.exe", 0x477B50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Math_009_RetI => new( new FhMethodLocation("FFX.exe", 0x477B70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Math_00A_RetI => new( new FhMethodLocation("FFX.exe", 0x477830) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Math_00B_RetI => new( new FhMethodLocation("FFX.exe", 0x477850) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Math_00C_RetI => new( new FhMethodLocation("FFX.exe", 0x477870) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Math_00D_RetI => new( new FhMethodLocation("FFX.exe", 0x4778A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Math_00E_RetI => new( new FhMethodLocation("FFX.exe", 0x4778D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Math_00F_RetF => new( new FhMethodLocation("FFX.exe", 0x477900) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Math_010_RetF => new( new FhMethodLocation("FFX.exe", 0x477920) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Math_011_RetF => new( new FhMethodLocation("FFX.exe", 0x477B90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Math_012_RetF => new( new FhMethodLocation("FFX.exe", 0x477C00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Math_013_RetF => new( new FhMethodLocation("FFX.exe", 0x477CB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Math_014_RetF => new( new FhMethodLocation("FFX.exe", 0x477CE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Math_015_RetI => new( new FhMethodLocation("FFX.exe", 0x477D40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Math_016_RetI => new( new FhMethodLocation("FFX.exe", 0x477DD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Math_017_RetF => new( new FhMethodLocation("FFX.exe", 0x477E70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Math_018_RetF => new( new FhMethodLocation("FFX.exe", 0x477EE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Math_019_RetF => new( new FhMethodLocation("FFX.exe", 0x477F50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Math_01A_RetF => new( new FhMethodLocation("FFX.exe", 0x477F70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Math_01B_RetF => new( new FhMethodLocation("FFX.exe", 0x477FE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Math_01C_RetI => new( new FhMethodLocation("FFX.exe", 0x478070) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Math_01D_RetI => new( new FhMethodLocation("FFX.exe", 0x478090) );

    // SgEvent (4000h-404h)

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_000_RetI => new( new FhMethodLocation("FFX.exe", 0x677D20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_001_RetI => new( new FhMethodLocation("FFX.exe", 0x677B90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_003_RetI => new( new FhMethodLocation("FFX.exe", 0x677D30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_004_RetI => new( new FhMethodLocation("FFX.exe", 0x677D80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_005_RetI => new( new FhMethodLocation("FFX.exe", 0x677DA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_006_RetI => new( new FhMethodLocation("FFX.exe", 0x677DC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_007_RetI => new( new FhMethodLocation("FFX.exe", 0x677DE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_008_RetI => new( new FhMethodLocation("FFX.exe", 0x677E30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_009_RetI => new( new FhMethodLocation("FFX.exe", 0x677E80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_00A_RetI => new( new FhMethodLocation("FFX.exe", 0x677E00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_00B_RetI => new( new FhMethodLocation("FFX.exe", 0x677E20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_00C_RetI => new( new FhMethodLocation("FFX.exe", 0x677ED0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Sg_00D_Init => new( new FhMethodLocation("FFX.exe", 0x677EE0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Sg_00D_Exec => new( new FhMethodLocation("FFX.exe", 0x677EF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_00E_RetI => new( new FhMethodLocation("FFX.exe", 0x677F00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_00F_RetI => new( new FhMethodLocation("FFX.exe", 0x677F20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_010_RetI => new( new FhMethodLocation("FFX.exe", 0x677F40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_011_RetI => new( new FhMethodLocation("FFX.exe", 0x677F70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_012_RetI => new( new FhMethodLocation("FFX.exe", 0x677FD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_013_RetI => new( new FhMethodLocation("FFX.exe", 0x678030) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_014_RetI => new( new FhMethodLocation("FFX.exe", 0x6780A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_015_RetI => new( new FhMethodLocation("FFX.exe", 0x6780C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_016_RetI => new( new FhMethodLocation("FFX.exe", 0x6780F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_017_RetI => new( new FhMethodLocation("FFX.exe", 0x678130) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_018_RetI => new( new FhMethodLocation("FFX.exe", 0x678160) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_019_RetI => new( new FhMethodLocation("FFX.exe", 0x678180) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_01A_RetI => new( new FhMethodLocation("FFX.exe", 0x6781A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_01B_RetI => new( new FhMethodLocation("FFX.exe", 0x6781D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_01C_RetI => new( new FhMethodLocation("FFX.exe", 0x6781F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Sg_01D_Init => new( new FhMethodLocation("FFX.exe", 0x678210) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Sg_01D_Exec => new( new FhMethodLocation("FFX.exe", 0x678270) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_01D_RetI => new( new FhMethodLocation("FFX.exe", 0x6782A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_01E_RetI => new( new FhMethodLocation("FFX.exe", 0x6782C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_01F_RetI => new( new FhMethodLocation("FFX.exe", 0x6782E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_020_RetI => new( new FhMethodLocation("FFX.exe", 0x6782F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_021_RetI => new( new FhMethodLocation("FFX.exe", 0x678300) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_022_RetI => new( new FhMethodLocation("FFX.exe", 0x678320) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_023_RetI => new( new FhMethodLocation("FFX.exe", 0x678340) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_024_RetI => new( new FhMethodLocation("FFX.exe", 0x678360) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_025_RetI => new( new FhMethodLocation("FFX.exe", 0x678370) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_026_RetI => new( new FhMethodLocation("FFX.exe", 0x678450) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_027_RetI => new( new FhMethodLocation("FFX.exe", 0x6784C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_028_RetI => new( new FhMethodLocation("FFX.exe", 0x678510) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_029_RetI => new( new FhMethodLocation("FFX.exe", 0x678560) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_02A_RetI => new( new FhMethodLocation("FFX.exe", 0x6785B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_02B_RetI => new( new FhMethodLocation("FFX.exe", 0x677880) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_02C_RetI => new( new FhMethodLocation("FFX.exe", 0x6778A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Sg_02D_RetF => new( new FhMethodLocation("FFX.exe", 0x6778E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_02E_RetI => new( new FhMethodLocation("FFX.exe", 0x677900) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_02F_RetI => new( new FhMethodLocation("FFX.exe", 0x677930) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_030_RetI => new( new FhMethodLocation("FFX.exe", 0x677950) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_031_RetI => new( new FhMethodLocation("FFX.exe", 0x677970) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_032_RetI => new( new FhMethodLocation("FFX.exe", 0x677980) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Sg_033_RetF => new( new FhMethodLocation("FFX.exe", 0x6779C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Sg_034_RetF => new( new FhMethodLocation("FFX.exe", 0x6779E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Sg_035_RetF => new( new FhMethodLocation("FFX.exe", 0x677A00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_036_RetI => new( new FhMethodLocation("FFX.exe", 0x677A10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_037_RetI => new( new FhMethodLocation("FFX.exe", 0x677A70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_038_RetI => new( new FhMethodLocation("FFX.exe", 0x677AB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Sg_039_RetF => new( new FhMethodLocation("FFX.exe", 0x677AC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_03A_RetI => new( new FhMethodLocation("FFX.exe", 0x677B10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_03B_RetI => new( new FhMethodLocation("FFX.exe", 0x677B30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_03C_RetI => new( new FhMethodLocation("FFX.exe", 0x677B60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_03D_RetI => new( new FhMethodLocation("FFX.exe", 0x677BC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_03E_RetI => new( new FhMethodLocation("FFX.exe", 0x677BE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_03F_RetI => new( new FhMethodLocation("FFX.exe", 0x677C20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_040_RetI => new( new FhMethodLocation("FFX.exe", 0x677C30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_041_RetI => new( new FhMethodLocation("FFX.exe", 0x677C60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_042_RetI => new( new FhMethodLocation("FFX.exe", 0x677C70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_043_RetI => new( new FhMethodLocation("FFX.exe", 0x677C80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_044_RetI => new( new FhMethodLocation("FFX.exe", 0x677CB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_045_RetI => new( new FhMethodLocation("FFX.exe", 0x677CE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Sg_046_RetI => new( new FhMethodLocation("FFX.exe", 0x677D00) );

    // ChEvent (5000h-5090h)

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_000_RetI => new( new FhMethodLocation("FFX.exe", 0x678770) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Ch_001_Init => new( new FhMethodLocation("FFX.exe", 0x6799B0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Ch_001_Exec => new( new FhMethodLocation("FFX.exe", 0x679A10) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_001_RetI => new( new FhMethodLocation("FFX.exe", 0x679A20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_002_RetI => new( new FhMethodLocation("FFX.exe", 0x6787A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Ch_003_Init => new( new FhMethodLocation("FFX.exe", 0x6787D0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Ch_003_Exec => new( new FhMethodLocation("FFX.exe", 0x6788A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_004_RetI => new( new FhMethodLocation("FFX.exe", 0x6788D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_005_RetI => new( new FhMethodLocation("FFX.exe", 0x678940) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_006_RetI => new( new FhMethodLocation("FFX.exe", 0x678A40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_007_RetI => new( new FhMethodLocation("FFX.exe", 0x6789A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_008_RetI => new( new FhMethodLocation("FFX.exe", 0x678AC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Ch_009_RetF => new( new FhMethodLocation("FFX.exe", 0x678B30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_00A_RetI => new( new FhMethodLocation("FFX.exe", 0x678C20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_00B_RetI => new( new FhMethodLocation("FFX.exe", 0x678B90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_00C_RetI => new( new FhMethodLocation("FFX.exe", 0x678D30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_00D_RetI => new( new FhMethodLocation("FFX.exe", 0x678DF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_00E_RetI => new( new FhMethodLocation("FFX.exe", 0x678CB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_00F_RetI => new( new FhMethodLocation("FFX.exe", 0x678DA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Ch_010_Init => new( new FhMethodLocation("FFX.exe", 0x679820) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Ch_010_Exec => new( new FhMethodLocation("FFX.exe", 0x679970) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_010_RetI => new( new FhMethodLocation("FFX.exe", 0x679980) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_011_RetI => new( new FhMethodLocation("FFX.exe", 0x678E50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_013_RetI => new( new FhMethodLocation("FFX.exe", 0x678EC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_014_RetI => new( new FhMethodLocation("FFX.exe", 0x678F30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_015_RetI => new( new FhMethodLocation("FFX.exe", 0x678FB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_016_RetI => new( new FhMethodLocation("FFX.exe", 0x6790B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_017_RetI => new( new FhMethodLocation("FFX.exe", 0x679130) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_018_RetI => new( new FhMethodLocation("FFX.exe", 0x6791E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_019_RetI => new( new FhMethodLocation("FFX.exe", 0x679220) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_01A_RetI => new( new FhMethodLocation("FFX.exe", 0x679270) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_01B_RetI => new( new FhMethodLocation("FFX.exe", 0x6792D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_01D_RetI => new( new FhMethodLocation("FFX.exe", 0x679310) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Ch_01E_Init => new( new FhMethodLocation("FFX.exe", 0x679370) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Ch_01E_Exec => new( new FhMethodLocation("FFX.exe", 0x6793C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_01F_RetI => new( new FhMethodLocation("FFX.exe", 0x679450) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_020_RetI => new( new FhMethodLocation("FFX.exe", 0x6794B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_021_RetI => new( new FhMethodLocation("FFX.exe", 0x679510) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_022_RetI => new( new FhMethodLocation("FFX.exe", 0x679550) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_023_RetI => new( new FhMethodLocation("FFX.exe", 0x6795B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_024_RetI => new( new FhMethodLocation("FFX.exe", 0x679620) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_025_RetI => new( new FhMethodLocation("FFX.exe", 0x679670) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_026_RetI => new( new FhMethodLocation("FFX.exe", 0x6796E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_027_RetI => new( new FhMethodLocation("FFX.exe", 0x679860) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Ch_028_RetF => new( new FhMethodLocation("FFX.exe", 0x6799D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_029_RetI => new( new FhMethodLocation("FFX.exe", 0x679A40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_02A_RetI => new( new FhMethodLocation("FFX.exe", 0x679AF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_02B_RetI => new( new FhMethodLocation("FFX.exe", 0x679B70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_02C_RetI => new( new FhMethodLocation("FFX.exe", 0x679BC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_02D_RetI => new( new FhMethodLocation("FFX.exe", 0x679C50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_02E_RetI => new( new FhMethodLocation("FFX.exe", 0x679D10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_02F_RetI => new( new FhMethodLocation("FFX.exe", 0x679D60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_030_RetI => new( new FhMethodLocation("FFX.exe", 0x679DB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_031_RetI => new( new FhMethodLocation("FFX.exe", 0x679E00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_032_RetI => new( new FhMethodLocation("FFX.exe", 0x679E60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_033_RetI => new( new FhMethodLocation("FFX.exe", 0x679EA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_034_RetI => new( new FhMethodLocation("FFX.exe", 0x679F40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_035_RetI => new( new FhMethodLocation("FFX.exe", 0x679FA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_036_RetI => new( new FhMethodLocation("FFX.exe", 0x67A120) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_037_RetI => new( new FhMethodLocation("FFX.exe", 0x67A140) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_038_RetI => new( new FhMethodLocation("FFX.exe", 0x67A250) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Ch_039_RetF => new( new FhMethodLocation("FFX.exe", 0x67A340) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Ch_03A_RetF => new( new FhMethodLocation("FFX.exe", 0x67A3D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_03B_RetI => new( new FhMethodLocation("FFX.exe", 0x67A450) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_03C_RetI => new( new FhMethodLocation("FFX.exe", 0x67A4D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_03D_RetI => new( new FhMethodLocation("FFX.exe", 0x67A520) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_03E_RetI => new( new FhMethodLocation("FFX.exe", 0x67A570) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_03F_RetI => new( new FhMethodLocation("FFX.exe", 0x67A5A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_040_RetI => new( new FhMethodLocation("FFX.exe", 0x67A610) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_041_RetI => new( new FhMethodLocation("FFX.exe", 0x67A740) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_042_RetI => new( new FhMethodLocation("FFX.exe", 0x67A780) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_043_RetI => new( new FhMethodLocation("FFX.exe", 0x67A7C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_044_RetI => new( new FhMethodLocation("FFX.exe", 0x67A810) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_045_RetI => new( new FhMethodLocation("FFX.exe", 0x67A830) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_046_RetI => new( new FhMethodLocation("FFX.exe", 0x67A880) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_047_RetI => new( new FhMethodLocation("FFX.exe", 0x67A900) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_048_RetI => new( new FhMethodLocation("FFX.exe", 0x67A940) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Ch_049_Init => new( new FhMethodLocation("FFX.exe", 0x6785F0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Ch_049_Exec => new( new FhMethodLocation("FFX.exe", 0x678620) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_04A_RetI => new( new FhMethodLocation("FFX.exe", 0x678640) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_04B_RetI => new( new FhMethodLocation("FFX.exe", 0x678670) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_04C_RetI => new( new FhMethodLocation("FFX.exe", 0x6786A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Ch_04D_RetF => new( new FhMethodLocation("FFX.exe", 0x6786E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_04E_RetI => new( new FhMethodLocation("FFX.exe", 0x679800) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Ch_04F_Init => new( new FhMethodLocation("FFX.exe", 0x679340) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Ch_04F_Exec => new( new FhMethodLocation("FFX.exe", 0x679380) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_04F_RetI => new( new FhMethodLocation("FFX.exe", 0x679410) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_050_RetI => new( new FhMethodLocation("FFX.exe", 0x678710) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_051_RetI => new( new FhMethodLocation("FFX.exe", 0x678750) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_052_RetI => new( new FhMethodLocation("FFX.exe", 0x6787E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_053_RetI => new( new FhMethodLocation("FFX.exe", 0x678900) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_054_RetI => new( new FhMethodLocation("FFX.exe", 0x678960) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_055_RetI => new( new FhMethodLocation("FFX.exe", 0x678A10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_056_RetI => new( new FhMethodLocation("FFX.exe", 0x678A80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_057_RetI => new( new FhMethodLocation("FFX.exe", 0x678AF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_058_RetI => new( new FhMethodLocation("FFX.exe", 0x678B70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_059_RetI => new( new FhMethodLocation("FFX.exe", 0x678BD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_05A_RetI => new( new FhMethodLocation("FFX.exe", 0x678C60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_05B_RetI => new( new FhMethodLocation("FFX.exe", 0x678CE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_05C_RetI => new( new FhMethodLocation("FFX.exe", 0x678D70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_05D_RetI => new( new FhMethodLocation("FFX.exe", 0x678DD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Ch_05E_RetF => new( new FhMethodLocation("FFX.exe", 0x678E20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_05F_RetI => new( new FhMethodLocation("FFX.exe", 0x678E90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_060_RetI => new( new FhMethodLocation("FFX.exe", 0x678F00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_061_RetI => new( new FhMethodLocation("FFX.exe", 0x678F80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Ch_062_Init => new( new FhMethodLocation("FFX.exe", 0x679030) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Ch_062_Exec => new( new FhMethodLocation("FFX.exe", 0x679070) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_062_RetI => new( new FhMethodLocation("FFX.exe", 0x679120) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Ch_063_Init => new( new FhMethodLocation("FFX.exe", 0x679160) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Ch_063_Exec => new( new FhMethodLocation("FFX.exe", 0x6791A0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_063_RetI => new( new FhMethodLocation("FFX.exe", 0x679210) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Ch_064_Init => new( new FhMethodLocation("FFX.exe", 0x679240) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Ch_064_Exec => new( new FhMethodLocation("FFX.exe", 0x679290) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_064_RetI => new( new FhMethodLocation("FFX.exe", 0x679300) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_065_RetI => new( new FhMethodLocation("FFX.exe", 0x679420) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Ch_066_Init => new( new FhMethodLocation("FFX.exe", 0x679480) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Ch_066_Exec => new( new FhMethodLocation("FFX.exe", 0x6794D0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_066_RetI => new( new FhMethodLocation("FFX.exe", 0x679540) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_067_RetI => new( new FhMethodLocation("FFX.exe", 0x679580) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Ch_068_Exec => new( new FhMethodLocation("FFX.exe", 0x6795D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_069_RetI => new( new FhMethodLocation("FFX.exe", 0x6795E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_06A_RetI => new( new FhMethodLocation("FFX.exe", 0x679640) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_06B_RetI => new( new FhMethodLocation("FFX.exe", 0x6796C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_06C_RetI => new( new FhMethodLocation("FFX.exe", 0x6797B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_06D_RetI => new( new FhMethodLocation("FFX.exe", 0x679A80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_06E_RetI => new( new FhMethodLocation("FFX.exe", 0x679B30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_06F_RetI => new( new FhMethodLocation("FFX.exe", 0x679B90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_070_RetI => new( new FhMethodLocation("FFX.exe", 0x679C30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_071_RetI => new( new FhMethodLocation("FFX.exe", 0x679CC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_072_RetI => new( new FhMethodLocation("FFX.exe", 0x679CF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_073_RetI => new( new FhMethodLocation("FFX.exe", 0x679D30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_074_RetI => new( new FhMethodLocation("FFX.exe", 0x679D90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_075_RetI => new( new FhMethodLocation("FFX.exe", 0x679DD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_076_RetI => new( new FhMethodLocation("FFX.exe", 0x679E30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_077_RetI => new( new FhMethodLocation("FFX.exe", 0x679E80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_078_RetI => new( new FhMethodLocation("FFX.exe", 0x679EF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_079_RetI => new( new FhMethodLocation("FFX.exe", 0x679F70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_07A_RetI => new( new FhMethodLocation("FFX.exe", 0x679FC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_07B_RetI => new( new FhMethodLocation("FFX.exe", 0x67A000) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_07C_RetI => new( new FhMethodLocation("FFX.exe", 0x67A070) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_07D_RetI => new( new FhMethodLocation("FFX.exe", 0x67A090) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_07E_RetI => new( new FhMethodLocation("FFX.exe", 0x67A210) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_07F_RetI => new( new FhMethodLocation("FFX.exe", 0x67A380) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_080_RetI => new( new FhMethodLocation("FFX.exe", 0x67A410) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_081_RetI => new( new FhMethodLocation("FFX.exe", 0x67A4A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_082_RetI => new( new FhMethodLocation("FFX.exe", 0x67A4F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_083_RetI => new( new FhMethodLocation("FFX.exe", 0x67A540) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_084_RetI => new( new FhMethodLocation("FFX.exe", 0x67A5D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_085_RetI => new( new FhMethodLocation("FFX.exe", 0x67A640) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_086_RetI => new( new FhMethodLocation("FFX.exe", 0x67A680) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_087_RetI => new( new FhMethodLocation("FFX.exe", 0x67A6A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_088_RetI => new( new FhMethodLocation("FFX.exe", 0x67A6B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_089_RetI => new( new FhMethodLocation("FFX.exe", 0x67A700) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_08A_RetI => new( new FhMethodLocation("FFX.exe", 0x67A720) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_08B_RetI => new( new FhMethodLocation("FFX.exe", 0x67A760) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_08C_RetI => new( new FhMethodLocation("FFX.exe", 0x67A7A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_08D_RetI => new( new FhMethodLocation("FFX.exe", 0x67A7E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_08E_RetI => new( new FhMethodLocation("FFX.exe", 0x67A850) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_08F_RetI => new( new FhMethodLocation("FFX.exe", 0x67A8A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Ch_090_RetI => new( new FhMethodLocation("FFX.exe", 0x67A8D0) );

    // Camera (6000h-6089h)

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_000_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8E20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_001_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8EB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_002_RetI => new( new FhMethodLocation("FFX.exe", 0x3B91A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_003_RetI => new( new FhMethodLocation("FFX.exe", 0x3B91E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_004_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9260) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_005_RetI => new( new FhMethodLocation("FFX.exe", 0x3B92A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_006_RetI => new( new FhMethodLocation("FFX.exe", 0x3B92E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_007_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9320) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_008_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9340) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_009_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9360) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_00A_RetI => new( new FhMethodLocation("FFX.exe", 0x3B93C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_00B_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9400) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_00C_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9440) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_00D_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9480) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_00E_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9520) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_00F_RetI => new( new FhMethodLocation("FFX.exe", 0x3B94A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_010_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9640) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_011_RetI => new( new FhMethodLocation("FFX.exe", 0x3B96B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_012_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9780) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_013_RetI => new( new FhMethodLocation("FFX.exe", 0x3B97F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_014_RetI => new( new FhMethodLocation("FFX.exe", 0x3B98C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_015_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9900) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_016_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9AC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_017_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9980) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_018_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9A00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_019_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9A40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Cam_01A_Exec => new( new FhMethodLocation("FFX.exe", 0x3B9B60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_01B_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9C20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_01C_RetI => new( new FhMethodLocation("FFX.exe", 0x3B82B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_01D_RetI => new( new FhMethodLocation("FFX.exe", 0x3B83F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_01E_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8350) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_01F_RetI => new( new FhMethodLocation("FFX.exe", 0x3B84B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_020_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9C80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_021_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9CC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_022_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9D90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_023_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9E10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_024_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9EA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_025_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9F20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_026_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9F40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_027_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9F60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_028_RetI => new( new FhMethodLocation("FFX.exe", 0x3BA060) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_029_RetI => new( new FhMethodLocation("FFX.exe", 0x3BA150) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_02A_RetI => new( new FhMethodLocation("FFX.exe", 0x3BA170) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_02B_RetI => new( new FhMethodLocation("FFX.exe", 0x3BA250) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_02C_RetI => new( new FhMethodLocation("FFX.exe", 0x3BA2D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_02D_RetI => new( new FhMethodLocation("FFX.exe", 0x3BA270) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_02E_RetI => new( new FhMethodLocation("FFX.exe", 0x3B7EF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_02F_RetI => new( new FhMethodLocation("FFX.exe", 0x3B7F60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_030_RetI => new( new FhMethodLocation("FFX.exe", 0x3B7FD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_031_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8040) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_032_RetI => new( new FhMethodLocation("FFX.exe", 0x3B80B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_033_RetI => new( new FhMethodLocation("FFX.exe", 0x3B80D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_034_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8190) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_035_RetI => new( new FhMethodLocation("FFX.exe", 0x3B80F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_036_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8110) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_037_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8130) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Cam_038_Exec => new( new FhMethodLocation("FFX.exe", 0x3B81F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_039_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8250) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_03A_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8F90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_03B_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9030) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_03C_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9380) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_03D_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9F80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_03E_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8650) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_03F_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8670) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_040_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8690) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_041_RetI => new( new FhMethodLocation("FFX.exe", 0x3B86B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_042_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8570) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_043_RetI => new( new FhMethodLocation("FFX.exe", 0x3B85E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_044_RetI => new( new FhMethodLocation("FFX.exe", 0x3B86D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_045_RetI => new( new FhMethodLocation("FFX.exe", 0x3B86F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_046_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9CA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_047_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9CE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_048_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9D20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_049_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9DB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_04A_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9E30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_04B_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9EC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_04C_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8710) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_04D_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8730) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_04E_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8A50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_04F_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8B00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_050_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8B20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_051_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8BB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_052_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8B80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_053_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8BE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_054_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8C10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_055_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8C40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_056_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8C90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_057_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8CC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_058_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8CF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_059_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9FA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_05A_RetI => new( new FhMethodLocation("FFX.exe", 0x3BA080) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_05B_RetI => new( new FhMethodLocation("FFX.exe", 0x3BA190) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_05C_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8E00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_05D_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8E90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_05E_RetI => new( new FhMethodLocation("FFX.exe", 0x3B93A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_05F_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9720) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_060_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9860) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_061_RetI => new( new FhMethodLocation("FFX.exe", 0x3B98E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Cam_062_Exec => new( new FhMethodLocation("FFX.exe", 0x3B99A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Cam_063_Exec => new( new FhMethodLocation("FFX.exe", 0x3B9920) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Cam_064_Init => new( new FhMethodLocation("FFX.exe", 0x3B9A20) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Cam_064_Exec => new( new FhMethodLocation("FFX.exe", 0x3B9AA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_065_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8750) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_066_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8F20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_067_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8F40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_068_RetI => new( new FhMethodLocation("FFX.exe", 0x3B93E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_069_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8F70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_06A_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9010) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_06B_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9420) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_06C_RetI => new( new FhMethodLocation("FFX.exe", 0x3B90D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_06D_RetI => new( new FhMethodLocation("FFX.exe", 0x3B90F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_06E_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9460) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_06F_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9150) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_070_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9180) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_071_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9500) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_072_RetI => new( new FhMethodLocation("FFX.exe", 0x3B91C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_073_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9200) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_074_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9590) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_075_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8AD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_076_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8F60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Cam_077_RetF => new( new FhMethodLocation("FFX.exe", 0x3B9110) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Cam_078_RetF => new( new FhMethodLocation("FFX.exe", 0x3B9170) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_079_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8D40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_07A_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8D60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Cam_07B_Init => new( new FhMethodLocation("FFX.exe", 0x3B8DC0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Cam_07B_Exec => new( new FhMethodLocation("FFX.exe", 0x3B8DE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Cam_07C_Init => new( new FhMethodLocation("FFX.exe", 0x3B8D80) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Cam_07C_Exec => new( new FhMethodLocation("FFX.exe", 0x3B8DA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_07D_RetI => new( new FhMethodLocation("FFX.exe", 0x3B87C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_07E_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8830) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_07F_RetI => new( new FhMethodLocation("FFX.exe", 0x3B88A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_080_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8920) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_081_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9240) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_082_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9280) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_083_RetI => new( new FhMethodLocation("FFX.exe", 0x3B92C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_084_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9300) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_085_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9220) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_086_RetI => new( new FhMethodLocation("FFX.exe", 0x3B9D00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_087_RetI => new( new FhMethodLocation("FFX.exe", 0x3B8990) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_088_RetI => new( new FhMethodLocation("FFX.exe", 0x3B89F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Cam_089_RetI => new( new FhMethodLocation("FFX.exe", 0x3B95B0) );

    // Battle (7000h-7127h)

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_000_RetI => new( new FhMethodLocation("FFX.exe", 0x3A8640) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_001_RetI => new( new FhMethodLocation("FFX.exe", 0x3A39F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Btl_002_Init => new( new FhMethodLocation("FFX.exe", 0x3A3550) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Btl_002_Exec => new( new FhMethodLocation("FFX.exe", 0x3A35E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_003_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3DB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_004_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3AC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_005_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4570) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_006_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3F70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_007_RetI => new( new FhMethodLocation("FFX.exe", 0x3A35C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Btl_008_Exec => new( new FhMethodLocation("FFX.exe", 0x3A39A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_009_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3B40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_00A_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4120) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_00B_RetI => new( new FhMethodLocation("FFX.exe", 0x3A44D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_00C_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4330) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_00D_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6410) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_00E_RetI => new( new FhMethodLocation("FFX.exe", 0x3A56B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_00F_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4D70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_010_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5B10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_011_RetI => new( new FhMethodLocation("FFX.exe", 0x3A42A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_012_RetI => new( new FhMethodLocation("FFX.exe", 0x3A29C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_013_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5860) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_014_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4450) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_015_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4930) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_016_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2A40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_017_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4070) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_018_RetI => new( new FhMethodLocation("FFX.exe", 0x3A50E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_019_RetI => new( new FhMethodLocation("FFX.exe", 0x3A62F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_01A_RetI => new( new FhMethodLocation("FFX.exe", 0x3A63B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_01B_RetI => new( new FhMethodLocation("FFX.exe", 0x3A66C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_01C_RetI => new( new FhMethodLocation("FFX.exe", 0x3A59F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_01D_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5C10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_01E_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6890) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_01F_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5FA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_020_RetI => new( new FhMethodLocation("FFX.exe", 0x3A53D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_021_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6AA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_022_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4CF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_023_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4960) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_024_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4EB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_025_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5D10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_026_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5DA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_027_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5EB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_028_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6990) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_029_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3FD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_02A_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5BE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_02B_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3C70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_02C_RetI => new( new FhMethodLocation("FFX.exe", 0x3A45E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_02D_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4E80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Btl_02E_RetF => new( new FhMethodLocation("FFX.exe", 0x3A6D90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_02F_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5160) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_030_RetI => new( new FhMethodLocation("FFX.exe", 0x3A50B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_031_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2D20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_032_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6F90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_033_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3CE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_034_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5350) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_035_RetI => new( new FhMethodLocation("FFX.exe", 0x3A53C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_036_RetI => new( new FhMethodLocation("FFX.exe", 0x3A71A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_037_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6D50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_038_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6DF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_039_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2B30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_03A_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7380) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_03B_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6F10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Btl_03C_Init => new( new FhMethodLocation("FFX.exe", 0x3A5560) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Btl_03C_Exec => new( new FhMethodLocation("FFX.exe", 0x3A56A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_03D_RetI => new( new FhMethodLocation("FFX.exe", 0x3A59A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Btl_03E_Exec => new( new FhMethodLocation("FFX.exe", 0x3A5A30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Btl_03F_Init => new( new FhMethodLocation("FFX.exe", 0x3A5E10) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Btl_03F_Exec => new( new FhMethodLocation("FFX.exe", 0x3A5EE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_040_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5630) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_041_RetI => new( new FhMethodLocation("FFX.exe", 0x3A56F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_042_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6080) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Btl_043_Init => new( new FhMethodLocation("FFX.exe", 0x3A6A30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Btl_043_Exec => new( new FhMethodLocation("FFX.exe", 0x3A6A60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_044_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6E30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_045_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4DC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_046_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5240) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_047_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5290) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_048_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5410) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_049_RetI => new( new FhMethodLocation("FFX.exe", 0x3A55C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_04A_RetI => new( new FhMethodLocation("FFX.exe", 0x3A58F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Btl_04B_Init => new( new FhMethodLocation("FFX.exe", 0x3A7660) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Btl_04B_Exec => new( new FhMethodLocation("FFX.exe", 0x3A7870) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Btl_04C_Init => new( new FhMethodLocation("FFX.exe", 0x3A7C10) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Btl_04C_Exec => new( new FhMethodLocation("FFX.exe", 0x3A7D10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_04D_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7500) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_04E_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7550) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_04F_RetI => new( new FhMethodLocation("FFX.exe", 0x3A70E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Btl_050_Init => new( new FhMethodLocation("FFX.exe", 0x3A7420) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Btl_050_Exec => new( new FhMethodLocation("FFX.exe", 0x3A7510) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Btl_051_Exec => new( new FhMethodLocation("FFX.exe", 0x3A58B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_052_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7E30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_053_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4490) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_054_RetI => new( new FhMethodLocation("FFX.exe", 0x3A8000) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_055_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3ED0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_056_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6EA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_057_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4240) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_058_RetI => new( new FhMethodLocation("FFX.exe", 0x3A8180) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Btl_059_RetF => new( new FhMethodLocation("FFX.exe", 0x3A6E60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_05A_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4A10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Btl_05B_RetF => new( new FhMethodLocation("FFX.exe", 0x3A2DA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Btl_05C_RetF => new( new FhMethodLocation("FFX.exe", 0x3A2E10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_05D_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5200) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_05E_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5330) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_05F_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4C50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_060_RetI => new( new FhMethodLocation("FFX.exe", 0x3A62C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_061_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6320) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_062_RetI => new( new FhMethodLocation("FFX.exe", 0x3A45A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Btl_063_Exec => new( new FhMethodLocation("FFX.exe", 0x3A5DE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Btl_064_Init => new( new FhMethodLocation("FFX.exe", 0x3A63F0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Btl_064_Exec => new( new FhMethodLocation("FFX.exe", 0x3A64E0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_064_RetI => new( new FhMethodLocation("FFX.exe", 0x3A65F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Btl_065_Exec => new( new FhMethodLocation("FFX.exe", 0x3A6780) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_065_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6970) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_066_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6980) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_067_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6A50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Btl_068_Exec => new( new FhMethodLocation("FFX.exe", 0x3A6AF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_069_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6190) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_06A_RetI => new( new FhMethodLocation("FFX.exe", 0x3A75A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_06B_RetI => new( new FhMethodLocation("FFX.exe", 0x3A84E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_06C_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6BE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Btl_06D_Init => new( new FhMethodLocation("FFX.exe", 0x3A6D20) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Btl_06D_Exec => new( new FhMethodLocation("FFX.exe", 0x3A6DD0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_06D_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6EC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Btl_06E_Init => new( new FhMethodLocation("FFX.exe", 0x3A7040) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Btl_06E_Exec => new( new FhMethodLocation("FFX.exe", 0x3A7210) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_06F_RetI => new( new FhMethodLocation("FFX.exe", 0x3A36E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_070_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7030) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_071_RetI => new( new FhMethodLocation("FFX.exe", 0x3A70D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_072_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6350) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_073_RetI => new( new FhMethodLocation("FFX.exe", 0x3A8610) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_074_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2A10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Btl_075_Init => new( new FhMethodLocation("FFX.exe", 0x3A7910) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Btl_075_Exec => new( new FhMethodLocation("FFX.exe", 0x3A7AC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_076_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5710) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_077_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5470) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_078_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6560) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_079_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7220) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_07A_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2AA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_07B_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7490) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Btl_07C_Exec => new( new FhMethodLocation("FFX.exe", 0x3A7280) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_07D_RetI => new( new FhMethodLocation("FFX.exe", 0x3A8210) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_07E_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2800) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_07F_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5E70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_080_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2C90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Btl_081_Exec => new( new FhMethodLocation("FFX.exe", 0x3A5D80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_082_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4200) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_083_RetI => new( new FhMethodLocation("FFX.exe", 0x3A42C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_084_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4460) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_085_RetI => new( new FhMethodLocation("FFX.exe", 0x3A43C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_086_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2D00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_087_RetI => new( new FhMethodLocation("FFX.exe", 0x3A78A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_088_RetI => new( new FhMethodLocation("FFX.exe", 0x3A79E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_089_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7C60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_08A_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3730) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_08B_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3920) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_08C_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3940) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_08D_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7700) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_08E_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5C40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_08F_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6720) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_090_RetI => new( new FhMethodLocation("FFX.exe", 0x3A49E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_091_RetI => new( new FhMethodLocation("FFX.exe", 0x3A77E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_092_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5F30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_093_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6110) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_094_RetI => new( new FhMethodLocation("FFX.exe", 0x3A72E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_095_RetI => new( new FhMethodLocation("FFX.exe", 0x3A61F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_096_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7B50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Btl_097_Init => new( new FhMethodLocation("FFX.exe", 0x3A57A0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Btl_097_Exec => new( new FhMethodLocation("FFX.exe", 0x3A58E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_098_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2DE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_099_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7360) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_09A_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2ED0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Btl_09B_RetF => new( new FhMethodLocation("FFX.exe", 0x3A3190) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Btl_09C_RetF => new( new FhMethodLocation("FFX.exe", 0x3A3350) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Btl_09D_Exec => new( new FhMethodLocation("FFX.exe", 0x3A3B00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_09E_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3000) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Btl_09F_RetF => new( new FhMethodLocation("FFX.exe", 0x3A3480) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0A0_RetI => new( new FhMethodLocation("FFX.exe", 0x3A73F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0A1_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6B60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Btl_0A2_Init => new( new FhMethodLocation("FFX.exe", 0x3A7460) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Btl_0A2_Exec => new( new FhMethodLocation("FFX.exe", 0x3A7520) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0A3_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3750) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0A4_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7650) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0A5_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7710) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0A6_RetI => new( new FhMethodLocation("FFX.exe", 0x3A77C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Btl_0A7_RetF => new( new FhMethodLocation("FFX.exe", 0x3A5510) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0A8_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5800) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Btl_0A9_Init => new( new FhMethodLocation("FFX.exe", 0x3A6C70) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Btl_0A9_Exec => new( new FhMethodLocation("FFX.exe", 0x3A6CD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0AA_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4EC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0AB_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5380) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Btl_0AC_RetF => new( new FhMethodLocation("FFX.exe", 0x3A5670) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0AD_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2CF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0AE_RetI => new( new FhMethodLocation("FFX.exe", 0x3A77B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0AF_RetI => new( new FhMethodLocation("FFX.exe", 0x3A79C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0B0_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4FA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0B1_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4A80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0B2_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5950) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0B3_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7E10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0B4_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7150) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0B5_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2F60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0B6_RetI => new( new FhMethodLocation("FFX.exe", 0x3A52D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0B7_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6660) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0B8_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7880) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Btl_0B9_RetF => new( new FhMethodLocation("FFX.exe", 0x3A2F00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Btl_0BA_RetF => new( new FhMethodLocation("FFX.exe", 0x3A3060) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0BB_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4D10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Btl_0BC_Init => new( new FhMethodLocation("FFX.exe", 0x3A59B0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Btl_0BC_Exec => new( new FhMethodLocation("FFX.exe", 0x3A5BB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Btl_0BD_Init => new( new FhMethodLocation("FFX.exe", 0x3A5EF0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Btl_0BD_Exec => new( new FhMethodLocation("FFX.exe", 0x3A6050) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Btl_0BE_RetF => new( new FhMethodLocation("FFX.exe", 0x3A37A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0BF_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6B00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Btl_0C0_RetF => new( new FhMethodLocation("FFX.exe", 0x3A6C90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0C1_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4BB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Btl_0C2_RetF => new( new FhMethodLocation("FFX.exe", 0x3A31F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Btl_0C3_RetF => new( new FhMethodLocation("FFX.exe", 0x3A3670) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0C4_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2BC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0C5_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7C40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0C6_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3970) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Btl_0C7_RetF => new( new FhMethodLocation("FFX.exe", 0x3A3A10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0C8_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7600) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0C9_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7760) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0CA_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7AD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0CB_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7B60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0CC_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7CD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0CD_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7D80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0CE_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7F40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0CF_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7F90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0D0_RetI => new( new FhMethodLocation("FFX.exe", 0x3A43A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0D1_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5CD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0D2_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6150) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0D3_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4AB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0D4_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4F40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0D5_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7FC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0D6_RetI => new( new FhMethodLocation("FFX.exe", 0x3A80D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0D7_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3110) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0D8_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3420) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0D9_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3520) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0DA_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4E10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0DB_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3470) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0DC_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7A30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0DD_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7FA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0DE_RetI => new( new FhMethodLocation("FFX.exe", 0x3A79D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0DF_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4F00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0E0_RetI => new( new FhMethodLocation("FFX.exe", 0x3A8470) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0E1_RetI => new( new FhMethodLocation("FFX.exe", 0x3A85C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0E2_RetI => new( new FhMethodLocation("FFX.exe", 0x3A32C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0E3_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7DC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0E4_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2B70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0E5_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4C90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0E6_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4E20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0E7_RetI => new( new FhMethodLocation("FFX.exe", 0x3A86A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0E8_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7AB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0E9_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7B20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0EA_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7B30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0EB_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7D30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0EC_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2C70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0ED_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2CC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0EE_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7BB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0EF_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6510) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Btl_0F0_RetF => new( new FhMethodLocation("FFX.exe", 0x3A3AE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0F1_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2D50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0F2_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2E80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0F3_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3320) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0F4_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2AF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0F5_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2FA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0F6_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3500) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0F7_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3580) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0F8_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3700) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0F9_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5FE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0FA_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3440) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0FB_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3800) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0FC_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3460) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0FD_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3A30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0FE_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3BA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_0FF_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3D20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_100_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7D40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_101_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7F80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_102_RetI => new( new FhMethodLocation("FFX.exe", 0x3A35F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_103_RetI => new( new FhMethodLocation("FFX.exe", 0x3A38A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_104_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6620) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Btl_105_Exec => new( new FhMethodLocation("FFX.exe", 0x3A6750) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_106_RetI => new( new FhMethodLocation("FFX.exe", 0x3A30C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_107_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6820) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_108_RetI => new( new FhMethodLocation("FFX.exe", 0x3A40C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_109_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3E10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_10A_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3990) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Btl_10B_Exec => new( new FhMethodLocation("FFX.exe", 0x3A3AA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_10C_RetI => new( new FhMethodLocation("FFX.exe", 0x3A2C30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_10D_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3C40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_10E_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3C20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_10F_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3CB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_110_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3D50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_111_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4010) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_112_RetI => new( new FhMethodLocation("FFX.exe", 0x3A43F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_113_RetI => new( new FhMethodLocation("FFX.exe", 0x3A80B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_114_RetI => new( new FhMethodLocation("FFX.exe", 0x3A80C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_115_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4580) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_116_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6950) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_117_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4B20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_118_RetI => new( new FhMethodLocation("FFX.exe", 0x3A81F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_119_RetI => new( new FhMethodLocation("FFX.exe", 0x3A8450) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_11A_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4180) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_11B_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3DF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_11C_RetI => new( new FhMethodLocation("FFX.exe", 0x3A8550) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_11D_RetI => new( new FhMethodLocation("FFX.exe", 0x3A4350) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_11E_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3F60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_11F_RetI => new( new FhMethodLocation("FFX.exe", 0x3A6220) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_120_RetI => new( new FhMethodLocation("FFX.exe", 0x3A65C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_121_RetI => new( new FhMethodLocation("FFX.exe", 0x3A67B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_122_RetI => new( new FhMethodLocation("FFX.exe", 0x3A3F10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_123_RetI => new( new FhMethodLocation("FFX.exe", 0x3A29A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_124_RetI => new( new FhMethodLocation("FFX.exe", 0x3A7F00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_125_RetI => new( new FhMethodLocation("FFX.exe", 0x3A8670) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_126_RetI => new( new FhMethodLocation("FFX.exe", 0x3A8570) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Btl_127_RetI => new( new FhMethodLocation("FFX.exe", 0x3A5260) );

    // MapFunc (8000h-806Bh)

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_000_RetI => new( new FhMethodLocation("FFX.exe", 0x51C530) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_001_RetI => new( new FhMethodLocation("FFX.exe", 0x51C5B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_002_RetI => new( new FhMethodLocation("FFX.exe", 0x51C670) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_003_RetI => new( new FhMethodLocation("FFX.exe", 0x51C7A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Map_004_Init => new( new FhMethodLocation("FFX.exe", 0x51AF60) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Map_004_Exec => new( new FhMethodLocation("FFX.exe", 0x51AFA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Map_005_Init => new( new FhMethodLocation("FFX.exe", 0x51C7E0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Map_005_Exec => new( new FhMethodLocation("FFX.exe", 0x51C860) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Map_006_Init => new( new FhMethodLocation("FFX.exe", 0x51AFF0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Map_006_Exec => new( new FhMethodLocation("FFX.exe", 0x51B040) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_007_RetI => new( new FhMethodLocation("FFX.exe", 0x51B050) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_008_RetI => new( new FhMethodLocation("FFX.exe", 0x51B060) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_009_RetI => new( new FhMethodLocation("FFX.exe", 0x51B070) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_00A_RetI => new( new FhMethodLocation("FFX.exe", 0x51B0A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_00B_RetI => new( new FhMethodLocation("FFX.exe", 0x51B130) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_00C_RetI => new( new FhMethodLocation("FFX.exe", 0x51B160) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_00D_RetI => new( new FhMethodLocation("FFX.exe", 0x51C740) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_00E_RetI => new( new FhMethodLocation("FFX.exe", 0x51B190) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_00F_RetI => new( new FhMethodLocation("FFX.exe", 0x51B1A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_010_RetI => new( new FhMethodLocation("FFX.exe", 0x51B1E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_011_RetI => new( new FhMethodLocation("FFX.exe", 0x51B200) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_012_RetI => new( new FhMethodLocation("FFX.exe", 0x51B240) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_013_RetI => new( new FhMethodLocation("FFX.exe", 0x51B260) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_014_RetI => new( new FhMethodLocation("FFX.exe", 0x51B280) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Map_015_RetF => new( new FhMethodLocation("FFX.exe", 0x51B2C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_016_RetI => new( new FhMethodLocation("FFX.exe", 0x51B2E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_017_RetI => new( new FhMethodLocation("FFX.exe", 0x51B320) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_018_RetI => new( new FhMethodLocation("FFX.exe", 0x51B340) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_019_RetI => new( new FhMethodLocation("FFX.exe", 0x51B360) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_01A_RetI => new( new FhMethodLocation("FFX.exe", 0x51B3A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_01B_RetI => new( new FhMethodLocation("FFX.exe", 0x51B3C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_01C_RetI => new( new FhMethodLocation("FFX.exe", 0x51B3E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_01D_RetI => new( new FhMethodLocation("FFX.exe", 0x51B400) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_01E_RetI => new( new FhMethodLocation("FFX.exe", 0x51B430) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_01F_RetI => new( new FhMethodLocation("FFX.exe", 0x51B450) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_020_RetI => new( new FhMethodLocation("FFX.exe", 0x51B470) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_021_RetI => new( new FhMethodLocation("FFX.exe", 0x51B490) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_022_RetI => new( new FhMethodLocation("FFX.exe", 0x51B4B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_023_RetI => new( new FhMethodLocation("FFX.exe", 0x51B4E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Map_024_RetF => new( new FhMethodLocation("FFX.exe", 0x51B530) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Map_025_RetF => new( new FhMethodLocation("FFX.exe", 0x51B540) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_026_RetI => new( new FhMethodLocation("FFX.exe", 0x51B550) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_027_RetI => new( new FhMethodLocation("FFX.exe", 0x51B5C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_028_RetI => new( new FhMethodLocation("FFX.exe", 0x51B5D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Map_029_RetF => new( new FhMethodLocation("FFX.exe", 0x51B600) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_02A_RetI => new( new FhMethodLocation("FFX.exe", 0x51B610) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_02B_RetI => new( new FhMethodLocation("FFX.exe", 0x51B670) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_02C_RetI => new( new FhMethodLocation("FFX.exe", 0x51B680) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_02D_RetI => new( new FhMethodLocation("FFX.exe", 0x51B690) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_02E_RetI => new( new FhMethodLocation("FFX.exe", 0x51B6A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_02F_RetI => new( new FhMethodLocation("FFX.exe", 0x51B6D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_030_RetI => new( new FhMethodLocation("FFX.exe", 0x51B6F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_031_RetI => new( new FhMethodLocation("FFX.exe", 0x51B720) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_032_RetI => new( new FhMethodLocation("FFX.exe", 0x51B740) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Map_033_RetF => new( new FhMethodLocation("FFX.exe", 0x51B770) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_034_RetI => new( new FhMethodLocation("FFX.exe", 0x51B780) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_035_RetI => new( new FhMethodLocation("FFX.exe", 0x51B790) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_036_RetI => new( new FhMethodLocation("FFX.exe", 0x51B7D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_037_RetI => new( new FhMethodLocation("FFX.exe", 0x51B810) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_038_RetI => new( new FhMethodLocation("FFX.exe", 0x51B8B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Map_039_RetF => new( new FhMethodLocation("FFX.exe", 0x51B930) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_03A_RetI => new( new FhMethodLocation("FFX.exe", 0x51B960) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Map_03B_RetF => new( new FhMethodLocation("FFX.exe", 0x51B9E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_03C_RetI => new( new FhMethodLocation("FFX.exe", 0x51BA10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Map_03D_RetF => new( new FhMethodLocation("FFX.exe", 0x51BAA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_03E_RetI => new( new FhMethodLocation("FFX.exe", 0x51BAD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_03F_RetI => new( new FhMethodLocation("FFX.exe", 0x51BBB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_040_RetI => new( new FhMethodLocation("FFX.exe", 0x51BC80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_041_RetI => new( new FhMethodLocation("FFX.exe", 0x51BD50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_042_RetI => new( new FhMethodLocation("FFX.exe", 0x51BE20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_043_RetI => new( new FhMethodLocation("FFX.exe", 0x51BE50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_044_RetI => new( new FhMethodLocation("FFX.exe", 0x51BE80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_045_RetI => new( new FhMethodLocation("FFX.exe", 0x51BEB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_046_RetI => new( new FhMethodLocation("FFX.exe", 0x51BF10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_047_RetI => new( new FhMethodLocation("FFX.exe", 0x51BF60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_048_RetI => new( new FhMethodLocation("FFX.exe", 0x51BFB0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_049_RetI => new( new FhMethodLocation("FFX.exe", 0x51C000) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_04A_RetI => new( new FhMethodLocation("FFX.exe", 0x51C030) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_04B_RetI => new( new FhMethodLocation("FFX.exe", 0x51C060) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_04C_RetI => new( new FhMethodLocation("FFX.exe", 0x51C090) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_04D_RetI => new( new FhMethodLocation("FFX.exe", 0x51C0D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_04E_RetI => new( new FhMethodLocation("FFX.exe", 0x51C110) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_04F_RetI => new( new FhMethodLocation("FFX.exe", 0x51C150) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Map_050_RetF => new( new FhMethodLocation("FFX.exe", 0x51C1A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Map_051_RetF => new( new FhMethodLocation("FFX.exe", 0x51C1C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_052_RetI => new( new FhMethodLocation("FFX.exe", 0x51C1E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_053_RetI => new( new FhMethodLocation("FFX.exe", 0x51C230) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_054_RetI => new( new FhMethodLocation("FFX.exe", 0x51C280) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_055_RetI => new( new FhMethodLocation("FFX.exe", 0x51C2D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Map_056_RetF => new( new FhMethodLocation("FFX.exe", 0x51C330) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Map_057_RetF => new( new FhMethodLocation("FFX.exe", 0x51C350) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_058_RetI => new( new FhMethodLocation("FFX.exe", 0x51C370) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_059_RetI => new( new FhMethodLocation("FFX.exe", 0x51C3C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_05A_RetI => new( new FhMethodLocation("FFX.exe", 0x51C3F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_05B_RetI => new( new FhMethodLocation("FFX.exe", 0x51C410) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_05C_RetI => new( new FhMethodLocation("FFX.exe", 0x51C470) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_05D_RetI => new( new FhMethodLocation("FFX.exe", 0x51C4A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_05E_RetI => new( new FhMethodLocation("FFX.exe", 0x51C4D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_05F_RetI => new( new FhMethodLocation("FFX.exe", 0x51C4F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_060_RetI => new( new FhMethodLocation("FFX.exe", 0x51C510) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_061_RetI => new( new FhMethodLocation("FFX.exe", 0x51C580) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_062_RetI => new( new FhMethodLocation("FFX.exe", 0x51C5A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_063_RetI => new( new FhMethodLocation("FFX.exe", 0x51C600) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_064_RetI => new( new FhMethodLocation("FFX.exe", 0x51C6B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_065_RetI => new( new FhMethodLocation("FFX.exe", 0x51C780) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_066_RetI => new( new FhMethodLocation("FFX.exe", 0x51C7C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_067_RetI => new( new FhMethodLocation("FFX.exe", 0x51C810) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_068_RetI => new( new FhMethodLocation("FFX.exe", 0x51C890) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_069_RetI => new( new FhMethodLocation("FFX.exe", 0x51AF30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_06A_RetI => new( new FhMethodLocation("FFX.exe", 0x51AF80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Map_06B_RetI => new( new FhMethodLocation("FFX.exe", 0x51AFC0) );

    // MnFunc (9000h)

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Mn_000_RetI => new( new FhMethodLocation("FFX.exe", 0x507E60) );

    // MovieFunc (B000h-B00Fh)

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Fmv_000_Init => new( new FhMethodLocation("FFX.exe", 0x36EA20) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Fmv_000_Exec => new( new FhMethodLocation("FFX.exe", 0x36EA90) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Fmv_000_RetI => new( new FhMethodLocation("FFX.exe", 0x36EAA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Fmv_001_Init => new( new FhMethodLocation("FFX.exe", 0x36EAC0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Fmv_001_Exec => new( new FhMethodLocation("FFX.exe", 0x36EAD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Fmv_001_RetI => new( new FhMethodLocation("FFX.exe", 0x36EB00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Fmv_002_RetI => new( new FhMethodLocation("FFX.exe", 0x36EB40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Fmv_003_RetI => new( new FhMethodLocation("FFX.exe", 0x36EB50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Fmv_004_Init => new( new FhMethodLocation("FFX.exe", 0x36EB60) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Fmv_004_Exec => new( new FhMethodLocation("FFX.exe", 0x36EB70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Fmv_004_RetI => new( new FhMethodLocation("FFX.exe", 0x36EBD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Fmv_005_RetI => new( new FhMethodLocation("FFX.exe", 0x36EBE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Fmv_006_RetI => new( new FhMethodLocation("FFX.exe", 0x36EBF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Fmv_007_RetI => new( new FhMethodLocation("FFX.exe", 0x36EC00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Fmv_008_Init => new( new FhMethodLocation("FFX.exe", 0x36EC10) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Fmv_008_Exec => new( new FhMethodLocation("FFX.exe", 0x36EC20) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Fmv_008_RetI => new( new FhMethodLocation("FFX.exe", 0x36EC30) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Fmv_009_Init => new( new FhMethodLocation("FFX.exe", 0x36EC40) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Fmv_009_Exec => new( new FhMethodLocation("FFX.exe", 0x36EC50) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Fmv_009_RetI => new( new FhMethodLocation("FFX.exe", 0x36EC70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Fmv_00A_Init => new( new FhMethodLocation("FFX.exe", 0x36EC90) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Fmv_00A_Exec => new( new FhMethodLocation("FFX.exe", 0x36ECF0) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Fmv_00A_RetI => new( new FhMethodLocation("FFX.exe", 0x36ED20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Fmv_00B_Init => new( new FhMethodLocation("FFX.exe", 0x36ED30) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Fmv_00B_Exec => new( new FhMethodLocation("FFX.exe", 0x36ED40) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Fmv_00B_RetI => new( new FhMethodLocation("FFX.exe", 0x36ED50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Fmv_00C_RetI => new( new FhMethodLocation("FFX.exe", 0x36EDA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Fmv_00D_RetI => new( new FhMethodLocation("FFX.exe", 0x36EDC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Fmv_00E_RetI => new( new FhMethodLocation("FFX.exe", 0x36EDD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Fmv_00F_RetI => new( new FhMethodLocation("FFX.exe", 0x36EDE0) );

    // Debug (C000h-C05Dh)

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_000_RetI => new( new FhMethodLocation("FFX.exe", 0x4783A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_001_RetI => new( new FhMethodLocation("FFX.exe", 0x478410) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_002_RetI => new( new FhMethodLocation("FFX.exe", 0x478460) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_003_RetI => new( new FhMethodLocation("FFX.exe", 0x478490) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_004_RetI => new( new FhMethodLocation("FFX.exe", 0x4784F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_005_RetI => new( new FhMethodLocation("FFX.exe", 0x478530) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_006_RetI => new( new FhMethodLocation("FFX.exe", 0x478560) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_007_RetI => new( new FhMethodLocation("FFX.exe", 0x478580) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_008_RetI => new( new FhMethodLocation("FFX.exe", 0x478310) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_009_RetI => new( new FhMethodLocation("FFX.exe", 0x4785B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_00B_RetI => new( new FhMethodLocation("FFX.exe", 0x478610) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_00C_RetI => new( new FhMethodLocation("FFX.exe", 0x4785D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_00D_RetI => new( new FhMethodLocation("FFX.exe", 0x478650) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_00E_RetI => new( new FhMethodLocation("FFX.exe", 0x478680) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_00F_RetI => new( new FhMethodLocation("FFX.exe", 0x4786D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_010_RetI => new( new FhMethodLocation("FFX.exe", 0x4786F0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_011_RetI => new( new FhMethodLocation("FFX.exe", 0x478710) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_012_RetI => new( new FhMethodLocation("FFX.exe", 0x478720) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_013_RetI => new( new FhMethodLocation("FFX.exe", 0x478730) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_014_RetI => new( new FhMethodLocation("FFX.exe", 0x478740) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_015_RetI => new( new FhMethodLocation("FFX.exe", 0x478750) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_016_RetI => new( new FhMethodLocation("FFX.exe", 0x4787E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_017_RetI => new( new FhMethodLocation("FFX.exe", 0x478800) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_018_RetI => new( new FhMethodLocation("FFX.exe", 0x478820) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_019_RetI => new( new FhMethodLocation("FFX.exe", 0x4784D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_01A_RetI => new( new FhMethodLocation("FFX.exe", 0x478860) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_01B_RetI => new( new FhMethodLocation("FFX.exe", 0x4788A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_01C_RetI => new( new FhMethodLocation("FFX.exe", 0x4788E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_01D_RetI => new( new FhMethodLocation("FFX.exe", 0x478920) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_01E_RetI => new( new FhMethodLocation("FFX.exe", 0x478960) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_01F_RetI => new( new FhMethodLocation("FFX.exe", 0x4789A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_020_RetI => new( new FhMethodLocation("FFX.exe", 0x478A40) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_021_RetI => new( new FhMethodLocation("FFX.exe", 0x478A60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_022_RetI => new( new FhMethodLocation("FFX.exe", 0x478A80) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_023_RetI => new( new FhMethodLocation("FFX.exe", 0x478AC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Dbg_024_Init => new( new FhMethodLocation("FFX.exe", 0x478B90) );
    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Dbg_024_Exec => new( new FhMethodLocation("FFX.exe", 0x478BE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_025_RetI => new( new FhMethodLocation("FFX.exe", 0x478BF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_026_RetI => new( new FhMethodLocation("FFX.exe", 0x478C60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_027_RetI => new( new FhMethodLocation("FFX.exe", 0x478CA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_028_RetI => new( new FhMethodLocation("FFX.exe", 0x478CE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_029_RetI => new( new FhMethodLocation("FFX.exe", 0x478D10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_02A_RetI => new( new FhMethodLocation("FFX.exe", 0x478D90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_02B_RetI => new( new FhMethodLocation("FFX.exe", 0x478DD0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_02C_RetI => new( new FhMethodLocation("FFX.exe", 0x478DE0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_02D_RetI => new( new FhMethodLocation("FFX.exe", 0x4789E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_02E_RetI => new( new FhMethodLocation("FFX.exe", 0x478A10) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_02F_RetI => new( new FhMethodLocation("FFX.exe", 0x478E00) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_030_RetI => new( new FhMethodLocation("FFX.exe", 0x478E50) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_031_RetI => new( new FhMethodLocation("FFX.exe", 0x4784B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_032_RetI => new( new FhMethodLocation("FFX.exe", 0x478E70) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_033_RetI => new( new FhMethodLocation("FFX.exe", 0x478E90) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_034_RetI => new( new FhMethodLocation("FFX.exe", 0x478EA0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_035_RetI => new( new FhMethodLocation("FFX.exe", 0x478ED0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_036_RetI => new( new FhMethodLocation("FFX.exe", 0x478EF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_037_RetI => new( new FhMethodLocation("FFX.exe", 0x478F20) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_038_RetI => new( new FhMethodLocation("FFX.exe", 0x478F60) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_039_RetI => new( new FhMethodLocation("FFX.exe", 0x478FC0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_03A_RetI => new( new FhMethodLocation("FFX.exe", 0x478FF0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_03B_RetI => new( new FhMethodLocation("FFX.exe", 0x479030) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_03C_RetI => new( new FhMethodLocation("FFX.exe", 0x479040) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_03D_RetI => new( new FhMethodLocation("FFX.exe", 0x479050) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_03E_RetI => new( new FhMethodLocation("FFX.exe", 0x479090) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_03F_RetI => new( new FhMethodLocation("FFX.exe", 0x4790B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_040_RetI => new( new FhMethodLocation("FFX.exe", 0x4790D0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_041_RetI => new( new FhMethodLocation("FFX.exe", 0x4790E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_042_RetI => new( new FhMethodLocation("FFX.exe", 0x479100) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_043_RetI => new( new FhMethodLocation("FFX.exe", 0x479110) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_044_RetI => new( new FhMethodLocation("FFX.exe", 0x479120) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_045_RetI => new( new FhMethodLocation("FFX.exe", 0x479140) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_046_RetI => new( new FhMethodLocation("FFX.exe", 0x479160) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Dbg_047_RetF => new( new FhMethodLocation("FFX.exe", 0x479180) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetFloat> AtelFn_Dbg_048_RetF => new( new FhMethodLocation("FFX.exe", 0x479200) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_049_RetI => new( new FhMethodLocation("FFX.exe", 0x4791A0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_04A_RetI => new( new FhMethodLocation("FFX.exe", 0x479240) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_04B_RetI => new( new FhMethodLocation("FFX.exe", 0x4791C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_04C_RetI => new( new FhMethodLocation("FFX.exe", 0x479280) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_04D_RetI => new( new FhMethodLocation("FFX.exe", 0x4791E0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_04E_RetI => new( new FhMethodLocation("FFX.exe", 0x4792C0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_04F_RetI => new( new FhMethodLocation("FFX.exe", 0x479300) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_050_RetI => new( new FhMethodLocation("FFX.exe", 0x479310) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_051_RetI => new( new FhMethodLocation("FFX.exe", 0x479330) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_052_RetI => new( new FhMethodLocation("FFX.exe", 0x479360) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_053_RetI => new( new FhMethodLocation("FFX.exe", 0x479370) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_054_RetI => new( new FhMethodLocation("FFX.exe", 0x478130) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_055_RetI => new( new FhMethodLocation("FFX.exe", 0x478150) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_056_RetI => new( new FhMethodLocation("FFX.exe", 0x4781B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_057_RetI => new( new FhMethodLocation("FFX.exe", 0x478170) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_058_RetI => new( new FhMethodLocation("FFX.exe", 0x478200) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_059_RetI => new( new FhMethodLocation("FFX.exe", 0x478220) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_05A_RetI => new( new FhMethodLocation("FFX.exe", 0x478270) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_05B_RetI => new( new FhMethodLocation("FFX.exe", 0x4782B0) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_05C_RetI => new( new FhMethodLocation("FFX.exe", 0x478340) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Dbg_05D_RetI => new( new FhMethodLocation("FFX.exe", 0x4783F0) );

    // AbilityMap (D000h)

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_RetInt>   AtelFn_Abm_000_RetI => new( new FhMethodLocation("FFX.exe", 0x6445F0) );

    // Default (????h)

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Init>     AtelFn_Nul_XXX_Init => new( new FhMethodLocation("FFX.exe", 0x477690) );

    public static FhMethodHandle<Fahrenheit.FhCall.d_CT_Exec>     AtelFn_Nul_XXX_Exec => new( new FhMethodLocation("FFX.exe", 0x4776A0) );

}
