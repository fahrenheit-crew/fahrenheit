// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

using Fahrenheit.Atel;

namespace Fahrenheit.Tests;

[TestFixture]
public class FhLayoutTests {

    /* [fkelava 21/11/26 02:38]
     * The generic Phyre classes have the same size in all generic instantiations, so testing any of them suffices.
     */

    [Test] public unsafe void PE_PClassMember()             => Assert.That(sizeof(PClassMember),                  Is.EqualTo(0x14));
    [Test] public unsafe void PE_PClassData()               => Assert.That(sizeof(PClassData),                    Is.EqualTo(0x18));
    [Test] public unsafe void PE_PClassDataMember()         => Assert.That(sizeof(PClassDataMember),              Is.EqualTo(0x2C));
    [Test] public unsafe void PE_PType()                    => Assert.That(sizeof(PType),                         Is.EqualTo(0x2C));
    [Test] public unsafe void PE_PAnnotationSemantic()      => Assert.That(sizeof(PAnnotationSemantic),           Is.EqualTo(0x08));
    [Test] public unsafe void PE_PApplication()             => Assert.That(sizeof(PApplication),                  Is.EqualTo(0x3A0));
    [Test] public unsafe void PE_PStreamFile()              => Assert.That(sizeof(PStreamFile),                   Is.EqualTo(0x08));
    [Test] public unsafe void PE_PWorld()                   => Assert.That(sizeof(PWorld),                        Is.EqualTo(0x10));
    [Test] public unsafe void PE_PTexture2D()               => Assert.That(sizeof(PTexture2D),                    Is.EqualTo(0x74));
    [Test] public unsafe void PE_PTexture2DD3D11()          => Assert.That(sizeof(PTexture2DD3D11),               Is.EqualTo(0x74));
    [Test] public unsafe void PE_PTexture2DBase()           => Assert.That(sizeof(PTexture2DBase),                Is.EqualTo(0x24));
    [Test] public unsafe void PE_PTextureCommonBase()       => Assert.That(sizeof(PTextureCommonBase),            Is.EqualTo(0x1C));
    [Test] public unsafe void PE_PTextureFormatBase()       => Assert.That(sizeof(PTextureFormatBase),            Is.EqualTo(0x14));
    [Test] public unsafe void PE_PArray()                   => Assert.That(sizeof(PArray<int>),                   Is.EqualTo(0x08));
    [Test] public unsafe void PE_PSharray()                 => Assert.That(sizeof(PSharray<int>),                 Is.EqualTo(0x08));
    [Test] public unsafe void PE_PString()                  => Assert.That(sizeof(PString),                       Is.EqualTo(0x04));
    [Test] public unsafe void PE_PNamedSemanticDescriptor() => Assert.That(sizeof(PNamedSemanticDescriptor),      Is.EqualTo(0x34));
    [Test] public unsafe void PE_PCluster()                 => Assert.That(sizeof(PCluster),                      Is.EqualTo(0x50));
    [Test] public unsafe void PE_PNamespace()               => Assert.That(sizeof(PNamespace),                    Is.EqualTo(0x1C));
    [Test] public unsafe void PE_PFreeList()                => Assert.That(sizeof(PFreeList<int>),                Is.EqualTo(0x18));
    [Test] public unsafe void PE_PSimpleDoubleListElement() => Assert.That(sizeof(PSimpleDoubleListElement<int>), Is.EqualTo(0x08));
    [Test] public unsafe void PE_PClassDescriptorDynamic()  => Assert.That(sizeof(PClassDescriptorDynamic),       Is.EqualTo(0xB8));

    [Test] public unsafe void __ALLOC_STRUCT() => Assert.That(sizeof(__ALLOC_STRUCT), Is.EqualTo(0x30));
    [Test] public unsafe void __ALLOC_DATA  () => Assert.That(sizeof(__ALLOC_DATA),   Is.EqualTo(0x18));

    [Test] public unsafe void Atel_AtelRequest()           => Assert.That(sizeof(AtelRequest),           Is.EqualTo(0x08));
    [Test] public unsafe void Atel_AtelSignal()            => Assert.That(sizeof(AtelSignal),            Is.EqualTo(0x16));
    [Test] public unsafe void Atel_MapEntrance()           => Assert.That(sizeof(MapEntrance),           Is.EqualTo(0x20));
    [Test] public unsafe void Atel_AtelScriptHeader()      => Assert.That(sizeof(AtelScriptHeader),      Is.EqualTo(0x34));
    [Test] public unsafe void Atel_AtelScriptChunk()       => Assert.That(sizeof(AtelScriptChunk),       Is.EqualTo(0x38));
    [Test] public unsafe void Atel_AtelWorkThread()        => Assert.That(sizeof(AtelWorkThread),        Is.EqualTo(0x4C));
    [Test] public unsafe void Atel_AtelWorkThreadStorage() => Assert.That(sizeof(AtelWorkThreadStorage), Is.EqualTo(0x14));

    [Test] public unsafe void FFX_Battle_PosAreaSomeInfo() => Assert.That(sizeof(FFX.Battle.PosAreaSomeInfo), Is.EqualTo(0x10));
    [Test] public unsafe void FFX_Battle_BtlArea()         => Assert.That(sizeof(FFX.Battle.BtlArea),         Is.EqualTo(0x60));
    [Test] public unsafe void FFX_Battle_BtlDebugFlags()   => Assert.That(sizeof(FFX.Battle.BtlDebugFlags),   Is.EqualTo(0x28));

    [Test] public unsafe void FFX_AutoAbility()         => Assert.That(sizeof(FFX.AutoAbility),         Is.EqualTo(0x6C)); // a_ability.bin
    [Test] public unsafe void FFX_Command()             => Assert.That(sizeof(FFX.Command),             Is.EqualTo(0x5C)); // monmagic{1|2}.bin
    [Test] public unsafe void FFX_PCommand()            => Assert.That(sizeof(FFX.PCommand),            Is.EqualTo(0x60)); // {item|command}.bin
    [Test] public unsafe void FFX_PCommandData()        => Assert.That(sizeof(FFX.PCommandData),        Is.EqualTo(0x04)); // ^
    [Test] public unsafe void FFX_CtbBase()             => Assert.That(sizeof(FFX.CtbBase),             Is.EqualTo(0x02)); // ctb_base.bin
    [Test] public unsafe void FFX_CustomizationRecipe() => Assert.That(sizeof(FFX.CustomizationRecipe), Is.EqualTo(0x08)); // kaizou.bin
    [Test] public unsafe void FFX_Equipment()           => Assert.That(sizeof(FFX.Equipment),           Is.EqualTo(0x16)); // weapon.bin
    [Test] public unsafe void FFX_UnownedEquipment()    => Assert.That(sizeof(FFX.UnownedEquipment),    Is.EqualTo(0x10)); // buki_get.bin
    [Test] public unsafe void FFX_Shop()                => Assert.That(sizeof(FFX.Shop),                Is.EqualTo(0x22)); // *_shop.bin
    [Test] public unsafe void FFX_HelpText()            => Assert.That(sizeof(FFX.HelpText),            Is.EqualTo(0x08)); // btl_txt.bin
    [Test] public unsafe void FFX_NameHelpText()        => Assert.That(sizeof(FFX.NameHelpText),        Is.EqualTo(0x10)); // *_txt.bin
    [Test] public unsafe void FFX_PlyRom()              => Assert.That(sizeof(FFX.PlyRom),              Is.EqualTo(0x2C)); // ply_rom.bin
    [Test] public unsafe void FFX_PlySave()             => Assert.That(sizeof(FFX.PlySave),             Is.EqualTo(0x94)); // ply_save.bin
    [Test] public unsafe void FFX_Treasure()            => Assert.That(sizeof(FFX.Treasure),            Is.EqualTo(0x04)); // takara.bin
    [Test] public unsafe void FFX_WeaponName()          => Assert.That(sizeof(FFX.WeaponName),          Is.EqualTo(0x48)); // w_name.bin

    [Test] public unsafe void FFX_Battle_MonStats()         => Assert.That(sizeof(FFX.Battle.MonStats),         Is.EqualTo(0x80));
    [Test] public unsafe void FFX_Battle_ChrEquipmentLoot() => Assert.That(sizeof(FFX.Battle.ChrEquipmentLoot), Is.EqualTo(0xE5));
    [Test] public unsafe void FFX_Battle_ChrItemLoot()      => Assert.That(sizeof(FFX.Battle.ChrItemLoot),      Is.EqualTo(0x0C));
    [Test] public unsafe void FFX_Battle_ChrStealLoot()     => Assert.That(sizeof(FFX.Battle.ChrStealLoot),     Is.EqualTo(0x09));
    [Test] public unsafe void FFX_Battle_ChrLoot()          => Assert.That(sizeof(FFX.Battle.ChrLoot),          Is.EqualTo(0x118));

}
