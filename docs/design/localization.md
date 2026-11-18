# Localization

Fahrenheit mods can use the localization system to automatically adjust to the active game language or a user-provided one.

## Localization API

To localize a string for the current game language or a specific one, use `FhApi.Localization.localize`. For example:
```cs
public class TemplateModule : FhModule {
    public void fn() {
        // To get your module's string with a given ID for the current game language
        string loc = FhApi.Localization.localize("string_a", this);

        // To get another module's string with a given ID for the current game language
        string loc = FhApi.Localization.localize("Fahrenheit.TemplateModule.string_b");

        // To get your module's string with a given ID for a specific language
        string loc = FhApi.Localization.localize("string_a", this, "de-DE");

        // To get another module's string with a given ID for a specific language
        string loc = FhApi.Localization.localize("Fahrenheit.TemplateModule.string_b", null, "de-DE");
        // or...
        string loc = FhApi.Localization.localize("Fahrenheit.TemplateModule.string_b", lang_id: "de-DE");
    }
}
```
For this to work, you must provide the necessary localization data. See below.

## Localization folder

The designated location for localization file storage is your mod's [`lang`](Mod-structure#lang) folder.

In that folder:
- Create **one folder per module**, named after the module's full type name. 
- Place localization data files within, one for each supported locale.

For example: 
- `lang/Fahrenheit.Mods.Template.TemplateModule/en-US.json`
- `lang/Fahrenheit.Mods.Template.TemplateModule/zh-Hant.json`
- `lang/Fahrenheit.Mods.Template.OtherModule/en-US.json`

> [!IMPORTANT]
> The folder must be named precisely after the module's full type name. In the following example:
> 
> ```cs
> namespace Fahrenheit.Mods.Template;
> 
> public class TemplateModule : FhModule {
> ```
> the full type name is `Fahrenheit.Mods.Template.TemplateModule`.

> [!TIP]
> By creating a folder for another mod's module, you can modify or even _add_ localization to other mods, as long as you follow them in the load order.

## Localization file format

A Fahrenheit localization file is a [JSON file](https://www.json.org/json-en.html) containing a single dictionary with string keys and values.

An example is given here:
```json
{
    "chr_stat_desc.str": "Strength affects the damage of your physical attacks.",
    "chr_stat_desc.mag": "Magic affects the damage of your magical attacks.",
    "chr_stat_desc.def": "Defense affects the damage you take from physical attacks.",
    "chr_stat_desc.mdf": "Magic Defense affects the damage you take from magical attacks.",
    "chr_stat_desc.agi": "Agility affects how quickly you get turns in combat.",
    "chr_stat_desc.acc": "Accuracy affects your chance to hit physical attacks.",
    "chr_stat_desc.eva": "Evasion affects your chance to dodge physical attacks.",
    "chr_stat_desc.lck": "Luck affects your chance to hit and dodge physical attacks, and deal critical hits."
}
```

> [!IMPORTANT]
> A localization data file must be named exactly `{LANG_ID}.json`, where `{LANG_ID}` is a ISO 639 language ID. 
>
> For example: 
> - `en-US.json`
> - `zh-Hant.json`
> - `ko-KR.json`
> - `de-DE.json` 
>
> For the list of recognized language IDs, see [`src/core/i18n_cctor.cs`](https://github.com/fahrenheit-crew/fahrenheit/blob/main/src/core/i18n_cctor.cs).
>
> For the list of what language IDs map to the game's selection of languages, see [`src/core/i18n.cs:37-46`](https://github.com/fahrenheit-crew/fahrenheit/blob/main/src/core/i18n.cs#L37-L46)
