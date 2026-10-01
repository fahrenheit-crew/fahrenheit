// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit.Tools.EEdit;

/// <summary>
///     The well-known Excel file types in enum form, used for internal ordering.
/// </summary>
internal enum EEditMode {
    // Common
    NULL = 1,
    TXT  = 2,

    A_ABILITY = 3,
    BTL_TXT   = 4,
    COMMAND   = 5,
    IMPORTANT = 6,
    ITEM      = 7,
    ITEM_SHOP = 8,
    MONMAGIC  = 9,
    MONSTER   = 10,
    PARTY     = 11,
    PLY_ROM   = 12,
    PLY_SAVE  = 13,
    PREPARE   = 14,
    RATE      = 15,
    ST_NUMBER = 16,
    TAKARA    = 17,

    // X
    AMAPDATA   = 18,
    ARMS_SHOP  = 19,
    BUKI_GET   = 20,
    C_ABILITY  = 21,
    CTB_BASE   = 22,
    ITEM_GET   = 23,
    KAIZOU     = 24,
    MENU       = 25,
    MENU_PANEL = 26,
    MONMAGIC1  = 27,
    PANEL      = 28,
    SHOP_ARMS  = 29,
    SPHERE     = 30,
    SUM_ASSURE = 31,
    SUM_GROW   = 32,
    W_NAME     = 33,
    WEAPON     = 34,

    // X-2
    ACCESSORY = 35,
    EXT_PARTY = 36,
    JOB       = 37,
    MON_GET   = 38,
    OVERSOUL  = 39,
    PLATE     = 40,
    ROM       = 41,

    // LM
    LM_ACCESARY                = 42,
    LM_BIN_STATUS_COUNT        = 43,
    LM_CAPACITY                = 44,
    LM_COMMAND                 = 45,
    LM_DEF_STCOUNT_INDEX_COUNT = 46,
    LM_DRESS                   = 47,
    LM_DRESSCOM                = 48,
    LM_FLOORNAME               = 49,
    LM_FLOOROBJ                = 50,
    LM_HIT                     = 51,
    LM_ITEM                    = 52,
    LM_ITEMBOX                 = 53,
    LM_LVEXP                   = 54,
    LM_LVHPMP                  = 55,
    LM_MES                     = 56,
    LM_MONMAGIC                = 57,
    LM_MONSTER                 = 58,
    LM_PLAYER                  = 59,
    LM_STATUSCHG               = 60,
    LM_TRAP                    = 61,
    LM_WAREHOUSE               = 62,
    LM_YS_DEF                  = 63,
    LM_YS_DEF_BIN              = 64,
    LM_MES_CHK                 = 65,

    COUNT_TYPES = 66,
}

internal static partial class UI {

    /// <summary>
    ///     Obtains a descriptive display string for the <see cref="EEditComponent"/>
    ///     that implements editing the selected <paramref name="mode"/>.
    /// </summary>
    private static string _get_component_name_by_mode(EEditMode mode) {
        return mode switch {
            EEditMode.NULL => $"(Unknown Type)",
            EEditMode.TXT  => $"{nameof(NameHelpText)} - (*_txt.bin)",

            EEditMode.A_ABILITY => $"{nameof(AutoAbility)} - (a_ability.bin)",
            EEditMode.BTL_TXT   => $"{nameof(HelpText)} - (btl_txt.bin)",
            EEditMode.COMMAND   => $"{nameof(PCommand)} - (command.bin)",
            EEditMode.IMPORTANT => $"{nameof(KeyItem)} - (important.bin)",
            EEditMode.ITEM_GET  => $"(item_get.bin)",
            EEditMode.ITEM_SHOP => $"(item_shop.bin)",
            EEditMode.MONMAGIC  => $"(monmagic.bin)",
            EEditMode.MONSTER   => $"(monster*.bin)",
            EEditMode.PARTY     => $"(party.bin)",
            EEditMode.PLY_ROM   => $"{nameof(PlyRom)} - (ply_rom.bin)",
            EEditMode.PLY_SAVE  => $"{nameof(PlySave)} - (ply_save.bin)",
            EEditMode.PREPARE   => $"{nameof(MixRecipe)} - (prepare.bin)",
            EEditMode.RATE      => $"{nameof(Rate)} - (*_rate.bin)",
            EEditMode.ST_NUMBER => $"{nameof(StNumber)} - (st_number.bin)",
            EEditMode.TAKARA    => $"{nameof(Treasure)} - (takara.bin)",

            EEditMode.AMAPDATA   => $"(amapdata.bin)",
            EEditMode.ARMS_SHOP  => $"(arms_shop.bin)",
            EEditMode.BUKI_GET   => $"{nameof(UnownedEquipment)} - (buki_get.bin)",
            EEditMode.C_ABILITY  => $"(c_ability.bin)",
            EEditMode.CTB_BASE   => $"(ctb_base.bin)",
            EEditMode.ITEM       => $"{nameof(PCommand)} - (item.bin)",
            EEditMode.KAIZOU     => $"{nameof(CustomizationRecipe)} - (kaizou.bin)",
            EEditMode.MENU       => $"(menu.bin)",
            EEditMode.MENU_PANEL => $"(menu_panel.bin)",
            EEditMode.MONMAGIC1  => $"{nameof(Command)} - (monmagic*.bin)",
            EEditMode.PANEL      => $"{nameof(SphereGridNodeType)} - (panel.bin)",
            EEditMode.SHOP_ARMS  => $"(shop_arms.bin)",
            EEditMode.SPHERE     => $"{nameof(Sphere)} - (sphere.bin)",
            EEditMode.SUM_ASSURE => $"{nameof(AeonStatBoostsMinimum)} - (sum_assure.bin)",
            EEditMode.SUM_GROW   => $"{nameof(AeonAbilityRecipe)} - (sum_grow.bin)",
            EEditMode.W_NAME     => $"{nameof(WeaponName)} - (w_name.bin)",
            EEditMode.WEAPON     => $"{nameof(Equipment)} - (weapon.bin)",

            EEditMode.ACCESSORY => $"(accessory.bin)",
            EEditMode.EXT_PARTY => $"(ext_party.bin)",
            EEditMode.JOB       => $"(job.bin)",
            EEditMode.MON_GET   => $"(mon_get.bin)",
            EEditMode.OVERSOUL  => $"(oversoul.bin)",
            EEditMode.PLATE     => $"(plate.bin)",
            EEditMode.ROM       => $"(rom.bin)",

            EEditMode.LM_ACCESARY                => $"(lm_accesary.bin)",
            EEditMode.LM_BIN_STATUS_COUNT        => $"(lm_bin_status_count.bin)",
            EEditMode.LM_CAPACITY                => $"(lm_capacity.bin)",
            EEditMode.LM_COMMAND                 => $"(lm_command.bin)",
            EEditMode.LM_DEF_STCOUNT_INDEX_COUNT => $"(lm_def_stcount_index_count.bin)",
            EEditMode.LM_DRESS                   => $"(lm_dress.bin)",
            EEditMode.LM_DRESSCOM                => $"(lm_dresscom.bin)",
            EEditMode.LM_FLOORNAME               => $"(lm_floorname.bin)",
            EEditMode.LM_FLOOROBJ                => $"(lm_floorobj.bin)",
            EEditMode.LM_HIT                     => $"(lm_hit.bin)",
            EEditMode.LM_ITEM                    => $"(lm_item.bin)",
            EEditMode.LM_ITEMBOX                 => $"(lm_itembox.bin)",
            EEditMode.LM_LVEXP                   => $"(lm_lvexp.bin)",
            EEditMode.LM_LVHPMP                  => $"(lm_lvhpmp.bin)",
            EEditMode.LM_MES                     => $"(lm_mes.bin)",
            EEditMode.LM_MONMAGIC                => $"(lm_monmagic.bin)",
            EEditMode.LM_MONSTER                 => $"(lm_monster.bin)",
            EEditMode.LM_PLAYER                  => $"(lm_player.bin)",
            EEditMode.LM_STATUSCHG               => $"(lm_statuschg.bin)",
            EEditMode.LM_TRAP                    => $"(lm_trap.bin)",
            EEditMode.LM_WAREHOUSE               => $"(lm_warehouse.bin)",
            EEditMode.LM_YS_DEF                  => $"(lm_ys_def.bin)",
            EEditMode.LM_YS_DEF_BIN              => $"(lm_ys_def_bin.bin)",
            EEditMode.LM_MES_CHK                 => $"(mes_chk.bin)",

            _ => throw new NotImplementedException("UNREACHABLE")
        };
    }

    /// <summary>
    ///     Obtains the <see cref="EEditComponent"/> that implements editing the selected <paramref name="mode"/>.
    /// </summary>
    private static EEditComponent _get_component_by_mode(EEditMode mode) {
        return mode switch {
            EEditMode.TXT => new EditorTextPair(),

            EEditMode.A_ABILITY => new EditorAAbility(),
            EEditMode.BTL_TXT   => new EditorText(),
            EEditMode.COMMAND   => new EditorCommand(),
            EEditMode.IMPORTANT => new EditorImportant(),
            EEditMode.ITEM_GET  => throw new NotImplementedException(),
            EEditMode.ITEM_SHOP => throw new NotImplementedException(),
            EEditMode.MONMAGIC  => throw new NotImplementedException(),
            EEditMode.MONSTER   => throw new NotImplementedException(),
            EEditMode.PARTY     => throw new NotImplementedException(),
            EEditMode.PLY_ROM   => new EditorPlyRom(),
            EEditMode.PLY_SAVE  => new EditorPlySave(),
            EEditMode.PREPARE   => new EditorPrepare(),
            EEditMode.RATE      => new EditorRate(),
            EEditMode.ST_NUMBER => new EditorStNumber(),
            EEditMode.TAKARA    => new EditorTakara(),

            EEditMode.AMAPDATA   => throw new NotImplementedException(),
            EEditMode.ARMS_SHOP  => throw new NotImplementedException(),
            EEditMode.BUKI_GET   => new EditorBukiGet(),
            EEditMode.C_ABILITY  => throw new NotImplementedException(),
            EEditMode.CTB_BASE   => throw new NotImplementedException(),
            EEditMode.ITEM       => new EditorItem(),
            EEditMode.KAIZOU     => new EditorKaizou(),
            EEditMode.MENU       => throw new NotImplementedException(),
            EEditMode.MENU_PANEL => throw new NotImplementedException(),
            EEditMode.MONMAGIC1  => new EditorMonmagic1(),
            EEditMode.PANEL      => new EditorPanel(),
            EEditMode.SHOP_ARMS  => throw new NotImplementedException(),
            EEditMode.SPHERE     => new EditorSphere(),
            EEditMode.SUM_ASSURE => new EditorSumAssure(),
            EEditMode.SUM_GROW   => new EditorSumGrow(),
            EEditMode.W_NAME     => new EditorWeaponName(),
            EEditMode.WEAPON     => new EditorWeapon(),

            EEditMode.ACCESSORY => throw new NotImplementedException(),
            EEditMode.EXT_PARTY => throw new NotImplementedException(),
            EEditMode.JOB       => throw new NotImplementedException(),
            EEditMode.MON_GET   => throw new NotImplementedException(),
            EEditMode.OVERSOUL  => throw new NotImplementedException(),
            EEditMode.PLATE     => throw new NotImplementedException(),
            EEditMode.ROM       => throw new NotImplementedException(),

            EEditMode.LM_ACCESARY                => throw new NotImplementedException(),
            EEditMode.LM_BIN_STATUS_COUNT        => throw new NotImplementedException(),
            EEditMode.LM_CAPACITY                => throw new NotImplementedException(),
            EEditMode.LM_COMMAND                 => throw new NotImplementedException(),
            EEditMode.LM_DEF_STCOUNT_INDEX_COUNT => throw new NotImplementedException(),
            EEditMode.LM_DRESS                   => throw new NotImplementedException(),
            EEditMode.LM_DRESSCOM                => throw new NotImplementedException(),
            EEditMode.LM_FLOORNAME               => throw new NotImplementedException(),
            EEditMode.LM_FLOOROBJ                => throw new NotImplementedException(),
            EEditMode.LM_HIT                     => throw new NotImplementedException(),
            EEditMode.LM_ITEM                    => throw new NotImplementedException(),
            EEditMode.LM_ITEMBOX                 => throw new NotImplementedException(),
            EEditMode.LM_LVEXP                   => throw new NotImplementedException(),
            EEditMode.LM_LVHPMP                  => throw new NotImplementedException(),
            EEditMode.LM_MES                     => throw new NotImplementedException(),
            EEditMode.LM_MONMAGIC                => throw new NotImplementedException(),
            EEditMode.LM_MONSTER                 => throw new NotImplementedException(),
            EEditMode.LM_PLAYER                  => throw new NotImplementedException(),
            EEditMode.LM_STATUSCHG               => throw new NotImplementedException(),
            EEditMode.LM_TRAP                    => throw new NotImplementedException(),
            EEditMode.LM_WAREHOUSE               => throw new NotImplementedException(),
            EEditMode.LM_YS_DEF                  => throw new NotImplementedException(),
            EEditMode.LM_YS_DEF_BIN              => throw new NotImplementedException(),
            EEditMode.LM_MES_CHK                 => throw new NotImplementedException(),

            EEditMode.NULL or
            _              => new EditorNull()
        };
    }

    /// <summary>
    ///     If the name of the file at <paramref name="file_path"/> is well-known,
    ///     returns the correct <see cref="EEditComponent"/> to edit it.
    /// </summary>
    private static EEditMode _get_mode_by_file_name(string file_path) {

        /* [fkelava 15/02/26 17:28]
         * https://gist.github.com/fkelava/77d3ab26e81d23bc6b22899eaef3dc27
         */

        return Path.GetFileName(file_path) switch {
            "arms_txt.bin"   or
            "btlend_txt.bin" or
            "build_txt.bin"  or
            "config_txt.bin" or
            "help_txt.bin"   or
            "item_txt.bin"   or
            "menu_txt.bin"   or
            "menu_txt2.bin"  or
            "mmain_txt.bin"  or
            "name_txt.bin"   or
            "save_txt.bin"   or
            "status_txt.bin" or
            "summon_txt.bin" => EEditMode.TXT,

            "arms_rate.bin"  or
            "item_rate.bin"  => EEditMode.RATE,

            "a_ability.bin"  => EEditMode.A_ABILITY,
            "btl_txt.bin"    => EEditMode.BTL_TXT,
            "command.bin"    => EEditMode.COMMAND,
            "important.bin"  => EEditMode.IMPORTANT,
            "item.bin"       => EEditMode.ITEM,
            "item_shop.bin"  => EEditMode.ITEM_SHOP,
            "monmagic.bin"   => EEditMode.MONMAGIC,

            "monster1.bin"   or
            "monster2.bin"   or
            "monster3.bin"   => EEditMode.MONSTER,

            "party.bin"      => EEditMode.PARTY,
            "ply_rom.bin"    => EEditMode.PLY_ROM,
            "ply_save.bin"   => EEditMode.PLY_SAVE,
            "prepare.bin"    => EEditMode.PREPARE,
            "st_number.bin"  => EEditMode.ST_NUMBER,
            "takara.bin"     => EEditMode.TAKARA,

            "amapdata.bin"   => EEditMode.AMAPDATA,
            "arms_shop.bin"  => EEditMode.ARMS_SHOP,
            "buki_get.bin"   => EEditMode.BUKI_GET,
            "c_ability.bin"  => EEditMode.C_ABILITY,
            "ctb_base.bin"   => EEditMode.CTB_BASE,
            "item_get.bin"   => EEditMode.ITEM_GET,
            "kaizou.bin"     => EEditMode.KAIZOU,
            "menu.bin"       => EEditMode.MENU,
            "menu_panel.bin" => EEditMode.MENU_PANEL,

            "monmagic1.bin"  or
            "monmagic2.bin"  => EEditMode.MONMAGIC1,

            "panel.bin"      => EEditMode.PANEL,
            "shop_arms.bin"  => EEditMode.SHOP_ARMS,
            "sphere.bin"     => EEditMode.SPHERE,
            "sum_assure.bin" => EEditMode.SUM_ASSURE,
            "sum_grow.bin"   => EEditMode.SUM_GROW,
            "w_name.bin"     => EEditMode.W_NAME,
            "weapon.bin"     => EEditMode.WEAPON,

            "accessory.bin" => EEditMode.ACCESSORY,
            "ext_party.bin" => EEditMode.EXT_PARTY,
            "job.bin"       => EEditMode.JOB,
            "mon_get.bin"   => EEditMode.MON_GET,
            "oversoul.bin"  => EEditMode.OVERSOUL,
            "plate.bin"     => EEditMode.PLATE,
            "rom.bin"       => EEditMode.ROM,

            "lm_accesary.bin"                => EEditMode.LM_ACCESARY,
            "lm_bin_status_count.bin"        => EEditMode.LM_BIN_STATUS_COUNT,
            "lm_capacity.bin"                => EEditMode.LM_CAPACITY,
            "lm_command.bin"                 => EEditMode.LM_COMMAND,
            "lm_def_stcount_index_count.bin" => EEditMode.LM_DEF_STCOUNT_INDEX_COUNT,
            "lm_dress.bin"                   => EEditMode.LM_DRESS,
            "lm_dresscom.bin"                => EEditMode.LM_DRESSCOM,
            "lm_floorname.bin"               => EEditMode.LM_FLOORNAME,
            "lm_floorobj.bin"                => EEditMode.LM_FLOOROBJ,
            "lm_hit.bin"                     => EEditMode.LM_HIT,
            "lm_item.bin"                    => EEditMode.LM_ITEM,
            "lm_itembox.bin"                 => EEditMode.LM_ITEMBOX,
            "lm_lvexp.bin"                   => EEditMode.LM_LVEXP,
            "lm_lvhpmp.bin"                  => EEditMode.LM_LVHPMP,
            "lm_mes.bin"                     => EEditMode.LM_MES,
            "lm_monmagic.bin"                => EEditMode.LM_MONMAGIC,
            "lm_monster.bin"                 => EEditMode.LM_MONSTER,
            "lm_player.bin"                  => EEditMode.LM_PLAYER,
            "lm_statuschg.bin"               => EEditMode.LM_STATUSCHG,
            "lm_trap.bin"                    => EEditMode.LM_TRAP,
            "lm_warehouse.bin"               => EEditMode.LM_WAREHOUSE,
            "lm_ys_def.bin"                  => EEditMode.LM_YS_DEF,
            "lm_ys_def_bin.bin"              => EEditMode.LM_YS_DEF_BIN,
            "mes_chk.bin"                    => EEditMode.LM_MES_CHK,

            _ => EEditMode.NULL,
        };
    }
}
