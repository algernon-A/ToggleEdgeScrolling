// <copyright file="OptionsPanel.cs" company="algernon (K. Algernon A. Sheppard)">
// Copyright (c) algernon (K. Algernon A. Sheppard). All rights reserved.
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace ToggleEdgeScrolling
{
    using AlgernonCommons.Keybinding;
    using AlgernonCommons.Translation;
    using AlgernonCommons.UI;
    using ColossalFramework.UI;

    /// <summary>
    /// Toggle Edge Scrolling options panel.
    /// </summary>
    public class OptionsPanel : OptionsPanelBase
    {
        // Layout constants.
        private const float Margin = 5f;
        private const float LeftMargin = 24f;
        private const float GroupMargin = 40f;

        /// <summary>
        /// Performs on-demand panel setup.
        /// </summary>
        protected override void Setup()
        {
            autoLayout = false;
            float currentY = Margin;

            // Language choice.
            UIDropDown languageDropDown = UIDropDowns.AddPlainDropDown(this, LeftMargin, currentY, Translations.Translate("LANGUAGE_CHOICE"), Translations.LanguageList, Translations.Index);
            languageDropDown.eventSelectedIndexChanged += (c, index) =>
            {
                Translations.Index = index;
                OptionsPanelManager<OptionsPanel>.LocaleChanged();
            };
            currentY += languageDropDown.parent.height + GroupMargin;

            // Hotkey control.
            OptionsKeymapping uuiKeymapping = OptionsKeymapping.AddKeymapping(this, LeftMargin, currentY, Translations.Translate("HOTKEY"), ModSettings.ToggleKey.Keybinding);
            currentY += uuiKeymapping.Panel.height + GroupMargin;

            // Disable on start checkbox.
            UICheckBox disableOnStartCheck = UICheckBoxes.AddPlainCheckBox(this, Margin, currentY, Translations.Translate("DISABLE_LOAD"));
            disableOnStartCheck.isChecked = ModSettings.DisableOnStart;
            disableOnStartCheck.eventCheckChanged += (c, isChecked) => { ModSettings.DisableOnStart = isChecked; };
            currentY += disableOnStartCheck.height + Margin;

            // Disable on start checkbox.
            UICheckBox disableInBackgroundCheck = UICheckBoxes.AddPlainCheckBox(this, Margin, currentY, Translations.Translate("DISABLE_BACKGROUND"));
            disableInBackgroundCheck.isChecked = EdgeScrolling.DisableInBackground;
            disableInBackgroundCheck.eventCheckChanged += (c, isChecked) => { EdgeScrolling.DisableInBackground = isChecked; };
        }
    }
}