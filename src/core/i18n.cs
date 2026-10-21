// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

/* [fkelava 14/2/25 14:52]
 * A locale file takes the name {LANG_ID}.json, where {LANG_ID} is a ISO 639 ID.
 * https://www.localeplanet.com/icu/ can help you refer to them.
 *
 * It must contain a flat map with string keys and string values.
 */
using LocaleData = Dictionary<string, string>;

/// <summary>
///     Provides methods for retrieving localization data.
/// </summary>
public sealed partial class FhLocalization {

    /* [fkelava 08/07/26 00:07]
     * For initialization, see `i18n_cctor.cs`.
     */

    private static readonly string[] _s_locale_ids;
    private static readonly string   _s_locale_default = _init_locale_default();
    private static readonly string   _s_loc_kit_path   = _init_loc_kit_path();

    private static readonly Dictionary<string, LocaleData> _s_locales = [];

    /* [fkelava 30/09/26 19:47]
     * One of the rare differences between previous and later remaster
     * versions is that Simplified Chinese (zh-Hans) support was later added.
     *
     * Steam only supports Traditional (zh-Hant) though.
     */

    /// <summary>
    ///     Gets the default localization language for the current game language.
    /// </summary>
    /// <returns>The ISO 639 language ID of the default localization language.</returns>
    private static string _init_locale_default() {
        return FhGlobal.lang_id switch {
            FhLangId.English  => "en-US",
            FhLangId.French   => "fr-FR",
            FhLangId.Spanish  => "es-ES",
            FhLangId.German   => "de-DE",
            FhLangId.Italian  => "it-IT",
            FhLangId.Japanese or
            FhLangId.Debug    => "ja-JP",
            FhLangId.Chinese  => "zh-Hant",
            FhLangId.Korean   => "ko-KR",
            _                 => "en-US"
        };
    }

    /// <summary>
    ///     Gets the path of the LocKit binary for the active game and language.
    /// </summary>
    /// <returns>The relative path of the active LocKit binary file in the VBF.</returns>
    private static string _init_loc_kit_path() {
        ReadOnlySpan<char> game_name = FhUtil.select("ffx", "ffx2", "ffx2");

        return FhGlobal.lang_id switch {
            FhLangId.English  => $"/{game_name}_data/gamedata/ps3data/lockit/{game_name}_loc_kit_ps3_us.bin",
            FhLangId.French   => $"/{game_name}_data/gamedata/ps3data/lockit/{game_name}_loc_kit_ps3_fr.bin",
            FhLangId.Spanish  => $"/{game_name}_data/gamedata/ps3data/lockit/{game_name}_loc_kit_ps3_sp.bin",
            FhLangId.German   => $"/{game_name}_data/gamedata/ps3data/lockit/{game_name}_loc_kit_ps3_de.bin",
            FhLangId.Italian  => $"/{game_name}_data/gamedata/ps3data/lockit/{game_name}_loc_kit_ps3_it.bin",
            FhLangId.Japanese or
            FhLangId.Debug    => $"/{game_name}_data/gamedata/ps3data/lockit/{game_name}_loc_kit_ps3_jp.bin",
            FhLangId.Chinese  => $"/{game_name}_data/gamedata/ps3data/lockit/{game_name}_loc_kit_ps3_ch.bin",
            FhLangId.Korean   => $"/{game_name}_data/gamedata/ps3data/lockit/{game_name}_loc_kit_ps3_kr.bin",
            _                 => $"/{game_name}_data/gamedata/ps3data/lockit/{game_name}_loc_kit_ps3_us.bin"
        };
    }

    /// <summary>
    ///     Loads localization data for a given mod.
    /// </summary>
    /// <param name="mod">The context of the mod to load localization data for.</param>
    private void _init_mod(FhModContext mod) {
        foreach (DirectoryInfo module_lang_dir in mod.Paths.LangDir.EnumerateDirectories()) {
            _init_module(mod, module_lang_dir);
        }
    }

    /// <summary>
    ///     Loads localization data for a given mod's module.
    /// </summary>
    /// <param name="mod">The context of the mod the module belongs to.</param>
    /// <param name="module_lang_dir">The directory containing the module's localization data.</param>
    private void _init_module(FhModContext mod, DirectoryInfo module_lang_dir) {
        foreach (FileInfo lang_file in module_lang_dir.EnumerateFiles("*.json", SearchOption.TopDirectoryOnly)) {
            string lang_id = Path.GetFileNameWithoutExtension(lang_file.FullName);

            string mod_name    = mod.Manifest.Id;
            string module_name = module_lang_dir.Name; // ex. Fahrenheit.Runtime.FhSaveUiModule

            if (!_s_locales.TryGetValue(lang_id, out LocaleData? locale)) {
                FhInternal.Log.Warning($"Mod '{mod_name}', module '{module_name}': ignoring unknown locale '{lang_id}'.");
                continue;
            }

            /* [fkelava 08/07/26 13:40]
             * Per our usual policy, we throw on I/O errors in our own directories.
             * The try-block is simply to provide a description of what locale faulted.
             */
            LocaleData user_locale;
            try {
                using FileStream lang_file_stream = lang_file.Open(FileMode.Open, FileAccess.Read);

                user_locale = JsonSerializer.Deserialize<LocaleData>(lang_file_stream, FhUtil.InternalJsonOpts)
                    ?? throw new Exception("Invalid or uninterpretable language file.");
            }
            catch {
                FhInternal.Log.Error($"While parsing locale {lang_id} from mod {mod_name} for module {module_name}:");
                throw;
            }

            foreach (KeyValuePair<string, string> user_string in user_locale) {
                string user_key = user_string.Key;
                string key      = $"{module_name}.{user_key}";

                if (locale.ContainsKey(key)) {
                    FhInternal.Log.Warning($"Key '{user_key}' in locale '{lang_id}' of module '{module_name}' superseded by '{mod_name}'.");
                }

                locale[key] = user_string.Value;
            }

            FhInternal.Log.Info($"Loaded locale {lang_id} from mod {mod_name} for module {module_name}.");
        }
    }


    /// <summary>
    ///     Loads localization data for all mods.
    /// </summary>
    internal void initialize() {
        /* [fkelava 07/07/26 23:44]
         * The expected directory structure is mods/{MOD_ID}/lang/{MODULE_TYPE_NAME}/{LANG_ID}.json,
         * ex. `mods/fhtemplate/lang/Fahrenheit.Modules.TemplateModule/en_US.json`
         *
         * Since this results in quite a bit of nesting, loading is broken up into multiple methods
         * to avoid three levels of `foreach` in a single method.
         */

        foreach (FhModContext mod in FhApi.Mods.get_mods()) {
            _init_mod(mod);
        }
    }

    /// <summary>
    ///     Retrieves a localized string with the given ID, for the given language.
    /// </summary>
    /// <param name="id">The ID of the localized string.</param>
    /// <param name="owner">The module containing the localization.</param>
    /// <param name="lang_id">The ISO 639 language ID of the language to get the string in.</param>
    /// <returns>The localized string, or the ID if unavailable.</returns>
    /// <remarks>
    ///     You may pass either a fully qualified ID, or a short ID
    ///     and a non-<c>null</c> <paramref name="owner"/>.
    ///     <para/>
    ///     If no <paramref name="lang_id"/> is specified, the current game language is used.
    /// </remarks>
    public string localize(
        string    id,
        FhModule? owner   = null,
        string?   lang_id = null
    ) {
        string composite_id = (owner == null)
            ? id
            : $"{owner.ModuleType}.{id}";

        lang_id ??= _s_locale_default;
        return _s_locales.TryGetValue(lang_id, out LocaleData? locale) && locale.TryGetValue(composite_id, out string? localized_string)
            ? localized_string
            : id;
    }
}
