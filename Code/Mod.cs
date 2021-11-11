using ICities;
using ColossalFramework.UI;


namespace ToggleEdgeScrolling
{
    public class TESMod : IUserMod
    {
        public static string ModName => "Toggle Edge Scrolling";
        public static string Version => "1.0.1";

        public string Name => ModName + " " + Version;
        public string Description => Translations.Translate("TES_DESC");


        /// <summary>
        /// Called by the game when the mod is enabled.
        /// </summary>
        public void OnEnabled()
        {
            // Load the settings file.
            ModSettings.Load();
        }


        /// <summary>
        /// Called by the game when the mod options panel is setup.
        /// </summary>
        public void OnSettingsUI(UIHelperBase helper)
        {
            // Language options.
            UIHelperBase languageGroup = helper.AddGroup(Translations.Translate("TRN_CHOICE"));
            UIDropDown languageDropDown = (UIDropDown)languageGroup.AddDropdown(Translations.Translate("TRN_CHOICE"), Translations.LanguageList, Translations.Index, (value) => { Translations.Index = value; ModSettings.Save(); });
            languageDropDown.autoSize = false;
            languageDropDown.width = 270f;

            // Tool activation hotkey.
            languageDropDown.parent.parent.gameObject.AddComponent<OptionsKeymapping>();

            // Disable scrolling on load.
            helper.AddCheckbox(Translations.Translate("TES_OPT_DOS"), ModSettings.disableOnStart, (value) => { ModSettings.disableOnStart = value; ModSettings.Save(); });
        }
    }
}
