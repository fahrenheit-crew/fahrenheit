// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Tests;

[TestFixture]
public class FhExcelTests {

    [Test]
    public void ffx_excel_sizeof() {
        using (Assert.EnterMultipleScope()) {
            Assert.That(Unsafe.SizeOf<FFX.AutoAbility>(),         Is.EqualTo(0x6C)); // a_ability.bin
            Assert.That(Unsafe.SizeOf<FFX.Command>(),             Is.EqualTo(0x5C)); // monmagic{1|2}.bin
            Assert.That(Unsafe.SizeOf<FFX.PCommand>(),            Is.EqualTo(0x60)); // {item|command}.bin
            Assert.That(Unsafe.SizeOf<FFX.PCommandData>(),        Is.EqualTo(0x04)); // ^
            Assert.That(Unsafe.SizeOf<FFX.CtbBase>(),             Is.EqualTo(0x02)); // ctb_base.bin
            Assert.That(Unsafe.SizeOf<FFX.CustomizationRecipe>(), Is.EqualTo(0x08)); // kaizou.bin
            Assert.That(Unsafe.SizeOf<FFX.Equipment>(),           Is.EqualTo(0x16)); // weapon.bin
            Assert.That(Unsafe.SizeOf<FFX.UnownedEquipment>(),    Is.EqualTo(0x10)); // buki_get.bin
            Assert.That(Unsafe.SizeOf<FFX.Shop>(),                Is.EqualTo(0x22)); // *_shop.bin
            Assert.That(Unsafe.SizeOf<FFX.HelpText>(),            Is.EqualTo(0x08)); // btl_txt.bin
            Assert.That(Unsafe.SizeOf<FFX.NameHelpText>(),        Is.EqualTo(0x10)); // *_txt.bin
            Assert.That(Unsafe.SizeOf<FFX.PlyRom>(),              Is.EqualTo(0x2C)); // ply_rom.bin
            Assert.That(Unsafe.SizeOf<FFX.PlySave>(),             Is.EqualTo(0x94)); // ply_save.bin
            Assert.That(Unsafe.SizeOf<FFX.Treasure>(),            Is.EqualTo(0x04)); // takara.bin
            Assert.That(Unsafe.SizeOf<FFX.WeaponName>(),          Is.EqualTo(0x48)); // w_name.bin

            Assert.That(Unsafe.SizeOf<FFX.Battle.MonStats>(),         Is.EqualTo(0x80));
            Assert.That(Unsafe.SizeOf<FFX.Battle.ChrEquipmentLoot>(), Is.EqualTo(0xE5));
            Assert.That(Unsafe.SizeOf<FFX.Battle.ChrItemLoot>(),      Is.EqualTo(0x0C));
            Assert.That(Unsafe.SizeOf<FFX.Battle.ChrStealLoot>(),     Is.EqualTo(0x09));
            Assert.That(Unsafe.SizeOf<FFX.Battle.ChrLoot>(),          Is.EqualTo(0x118));
        }
    }

}
