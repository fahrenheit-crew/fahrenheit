// SPDX-License-Identifier: MIT

/* [fkelava 7/5/23 15:37]
 * Many primitive IDs and enumerations in this game are given merely as `#define`s. What the true underlying type is is unknown.
 *
 * Fahrenheit introduces strongly typed constants for all such IDs and enumerations. Because I have to guess the underlying type,
 * odds are I got it wrong. For this reason I introduce type aliases for each; this way you can change the underlying datatype
 * of any primitive declaration here, in one spot. You should follow this approach when adding new constants.
 */

global using T_X2CommandId         = System.UInt16; // ids/com_mon, com_ply, item
global using T_X2TargetId          = System.Int32;  // ids/target
global using T_X2BtlId             = System.UInt32; // ids/btl
global using T_X2ChrStatId         = System.Int32;  // ids/chr_stat
global using T_X2SeTypeId          = System.Byte;   // ids/setype
global using T_X2BtlSeTypeId       = System.Byte;   // ids/btl_setype
global using T_X2BtlRequestTagId   = System.Int16;  // ids/btl_req_tags
global using T_X2BtlRequestActorId = System.Int16;  // ids/btl_req_tags
global using T_X2BtlVoiceId        = System.UInt32; // ids/btl_voice
global using T_X2PlySaveId         = System.Int32;  // ids/plysave
global using T_X2JobId             = System.UInt16; // ids/job
global using T_X2PlateId           = System.UInt16; // ids/plate
global using T_X2AutoAbilityId     = System.UInt16; // ids/a_ability
global using T_X2AccessoryId       = System.UInt16; // ids/accessory
global using T_X2KeyItemId         = System.UInt16; // ids/important