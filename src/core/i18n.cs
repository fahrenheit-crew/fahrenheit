// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

/* [fkelava 14/2/25 14:52]
 * A locale file takes the name {LANG_ID}.json, where {LANG_ID} is a ISO 639 ID.
 * https://www.localeplanet.com/icu/ can help you refer to them.
 *
 * It is to contain a flat map with string keys and string values.
 */
using LocaleData = Dictionary<string, string>;

public sealed partial class FhLocalization {

    /* [fkelava 08/07/26 00:07]
     * For initialization, see `i18n_cctor.cs`.
     */

    private readonly static string[]                       _s_locale_ids;
    private readonly static Dictionary<string, LocaleData> _s_locales = [];

    /// <summary>
    ///     Returns the ISO 639 language ID for the current game language.
    /// </summary>
    private string get_default_lang_id() {
        return FhGlobal.lang_id switch {
            FhLangId.English  => "en",
            FhLangId.French   => "fr",
            FhLangId.Spanish  => "es",
            FhLangId.German   => "de",
            FhLangId.Italian  => "it",
            FhLangId.Japanese => "ja",
            FhLangId.Chinese  => "zh",
            FhLangId.Korean   => "ko",
            FhLangId.Debug    or
            _                 => "en"
        };
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
            load_for_mod(mod);
        }
    }

    /// <summary>
    ///     Loads localization data for the given <paramref name="mod"/>.
    /// </summary>
    internal void load_for_mod(FhModContext mod) {
        foreach (DirectoryInfo module_lang_dir in mod.Paths.LangDir.EnumerateDirectories()) {
            load_for_module(mod, module_lang_dir);
        }
    }

    /// <summary>
    ///     Loads localization data for a given <paramref name="mod"/>'s module.
    /// </summary>
    internal void load_for_module(FhModContext mod, DirectoryInfo module_dir) {
        foreach (FileInfo lang_file in module_dir.EnumerateFiles("*.json", SearchOption.TopDirectoryOnly)) {
            string lang_id = Path.GetFileNameWithoutExtension(lang_file.FullName);

            string mod_name    = mod.Manifest.Name; // We use the 'pretty' name to make errors more legible.
            string module_name = module_dir.Name;   // ex. Fahrenheit.Runtime.FhSaveUiModule

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
                FhInternal.Log.Error($"While parsing locale {lang_id} for mod {mod_name}:");
                throw;
            }

            foreach (KeyValuePair<string, string> user_string in user_locale) {
                string user_key = user_string.Key;
                string key      = $"{module_name}.{user_key}";

                if (locale.ContainsKey(key)) {
                    FhInternal.Log.Warning($"Key '{user_key}' in locale '{lang_id}' of module '{module_name}' superseded by '{mod_name}'");
                }

                locale[key] = user_string.Value;
            }

            FhInternal.Log.Info($"Loaded locale {lang_id} for mod {mod_name}.");
        }
    }

    /// <summary>
    ///     Attempts to retrieve the string with ID <paramref name="id"/>
    ///     for the locale with ID <paramref name="lang_id"/>,
    ///     falling back to <paramref name="id"/> if unavailable.
    /// </summary>
    public string localize(
        string    id,
        FhModule? caller  = null,
        string?   lang_id = null
    ) {
        string composite_id = (caller == null)
            ? id
            : $"{caller.ModuleType}.{id}";

        lang_id ??= get_default_lang_id();
        return _s_locales.TryGetValue(lang_id, out LocaleData? locale) && locale.TryGetValue(composite_id, out string? localized_string)
            ? localized_string
            : id;
    }
}
