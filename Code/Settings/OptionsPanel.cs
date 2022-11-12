// <copyright file="OptionsPanel.cs" company="algernon (K. Algernon A. Sheppard)">
// Copyright (c) algernon (K. Algernon A. Sheppard). All rights reserved.
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace ToggleEdgeScrolling
{
    using AlgernonCommons.Translation;
    using AlgernonCommons.UI;
    using ColossalFramework.UI;
    using UnityEngine;

    /// <summary>
    /// Toggle Edge Scrolling options panel.
    /// </summary>
    public class OptionsPanel : UIPanel
    {
        // Layout constants.
        private const float Margin = 5f;
        private const float LeftMargin = 24f;
        private const float GroupMargin = 40f;

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsPanel"/> class.
        /// </summary>
        internal OptionsPanel()
        {
            autoLayout = false;
            float currentY = Margin;

            // Language choice.
            UIDropDown languageDropDown = UIDropDowns.AddPlainDropDown(this, LeftMargin, currentY, Translations.Translate("TRN_CHOICE"), Translations.LanguageList, Translations.Index);
            languageDropDown.eventSelectedIndexChanged += (c, index) =>
            {
                Translations.Index = index;
                OptionsPanelManager<OptionsPanel>.LocaleChanged();
            };
            currentY += languageDropDown.parent.height + GroupMargin;

            // Hotkey control.
            UUIKeymapping uuiKeymapping = gameObject.AddComponent<UUIKeymapping>();
            uuiKeymapping.Panel.relativePosition = new Vector2(LeftMargin, currentY);
            currentY += uuiKeymapping.Panel.height + GroupMargin;

            // Disable on start checkbox.
            UICheckBox disableOnStartCheck = UICheckBoxes.AddPlainCheckBox(this, Margin, currentY, Translations.Translate("TES_OPT_DOS"));
            disableOnStartCheck.isChecked = ModSettings.DisableOnStart;
            disableOnStartCheck.eventCheckChanged += (c, isChecked) => { ModSettings.DisableOnStart = isChecked; };
            currentY += disableOnStartCheck.height + Margin;

            // Disable on start checkbox.
            UICheckBox disableInBackgroundCheck = UICheckBoxes.AddPlainCheckBox(this, Margin, currentY, Translations.Translate("TES_OPT_DIB"));
            disableInBackgroundCheck.isChecked = EdgeScrolling.DisableInBackground;
            disableInBackgroundCheck.eventCheckChanged += (c, isChecked) => { EdgeScrolling.DisableInBackground = isChecked; };
        }
    }
}