using System;
using UnityEngine;
using ICities;
using ColossalFramework.UI;


namespace ToggleEdgeScrolling
{
    /// <summary>
    /// Class to handle the mod's options panel.
    /// </summary>
    internal static class OptionsPanel
    {
        // Layout constants.
        private const float Margin = 5f;
        private const float LeftMargin = 24f;
        private const float GroupMargin = 40f;


        // Parent UI panel reference.
        internal static UIScrollablePanel optionsPanel;
        private static UIPanel gameOptionsPanel;

        // Instance references.
        private static GameObject optionsGameObject;


        /// <summary>
        /// Options panel setup.
        /// </summary>
        /// <param name="helper">UIHelperBase parent</param>
        internal static void Setup(UIHelperBase helper)
        {
            // Set up tab strip and containers.
            optionsPanel = ((UIHelper)helper).self as UIScrollablePanel;
            optionsPanel.autoLayout = false;
        }


        /// <summary>
        /// Attaches an event hook to options panel visibility, to enable/disable mod hokey when the panel is open.
        /// </summary>
        internal static void OptionsEventHook()
        {
            // Get options panel instance.
            gameOptionsPanel = UIView.library.Get<UIPanel>("OptionsPanel");

            if (gameOptionsPanel == null)
            {
                Logging.Error("couldn't find OptionsPanel");
            }
            else
            {
                // Simple event hook to create/destroy GameObject based on appropriate visibility.
                gameOptionsPanel.eventVisibilityChanged += (control, isVisible) =>
                {
                    // Create/destroy based on whether or not we're now visible.
                    if (isVisible)
                    {
                        Create();
                    }
                    else
                    {
                        Close();
                    }
                };
            }
        }


        /// <summary>
        /// Refreshes the options panel (destroys and rebuilds) on a locale change when the options panel is open.
        /// </summary>
        internal static void LocaleChanged()
        {
            if (gameOptionsPanel != null && gameOptionsPanel.isVisible)
            {
                Logging.KeyMessage("changing locale");

                Close();
                Create();
            }
        }


        /// <summary>
        /// Creates the panel object in-game and displays it.
        /// </summary>
        private static void Create()
        {
            try
            {
                // If no instance already set, create one.
                if (optionsGameObject == null)
                {
                    // Give it a unique name for easy finding with ModTools.
                    optionsGameObject = new GameObject("TESOptionsPanel");
                    optionsGameObject.transform.parent = optionsPanel.transform;

                    // Create a base panel attached to our game object, perfectly overlaying the game options panel.
                    UIPanel panel = optionsGameObject.AddComponent<UIPanel>();
                    panel.width = optionsPanel.width - 10f;
                    panel.height = 725f;
                    panel.clipChildren = false;
                    panel.autoLayout = false;

                    // Needed to ensure position is consistent if we regenerate after initial opening (e.g. on language change).
                    panel.relativePosition = new Vector2(10f, 10f);

                    // Y position indicator.
                    float currentY = Margin;

                    // Language choice.
                    UIDropDown languageDropDown = AddPlainDropDown(panel, Translations.Translate("TRN_CHOICE"), Translations.LanguageList, Translations.Index);
                    languageDropDown.eventSelectedIndexChanged += (control, index) =>
                    {
                        Translations.Index = index;
                        OptionsPanel.LocaleChanged();
                    };
                    languageDropDown.parent.relativePosition = new Vector2(LeftMargin, currentY);
                    currentY += languageDropDown.parent.height + GroupMargin;

                    UICheckBox disableOnStartCheck = AddPlainCheckBox(panel, Margin, currentY, Translations.Translate("TES_OPT_DOS"));
                    disableOnStartCheck.isChecked = ModSettings.disableOnStart;
                    disableOnStartCheck.eventCheckChanged += (control, isChecked) => { ModSettings.disableOnStart = isChecked; };
                }
            }
            catch (Exception e)
            {
                Logging.LogException(e, "exception creating options panel");
            }
        }


        /// <summary>
        /// Closes the panel by destroying the object (removing any ongoing UI overhead).
        /// </summary>
        private static void Close()
        {
            // Save settings first.
            ModSettings.Save();

            // We're no longer visible - destroy our game object.
            if (optionsGameObject != null)
            {
                GameObject.Destroy(optionsGameObject);
                optionsGameObject = null;
            }
        }


        /// <summary>
        /// Creates a plain dropdown using the game's option panel dropdown template.
        /// </summary>
        /// <param name="parent">Parent component</param>
        /// <param name="text">Descriptive label text</param>
        /// <param name="items">Dropdown menu item list</param>
        /// <param name="selectedIndex">Initially selected index (default 0)</param>
        /// <param name="width">Width of dropdown (default 60)</param>
        /// <returns>New dropdown menu using game's option panel template</returns>
        private static UIDropDown AddPlainDropDown(UIComponent parent, string text, string[] items, int selectedIndex = 0, float width = 270f)
        {
            UIPanel panel = parent.AttachUIComponent(UITemplateManager.GetAsGameObject("OptionsDropdownTemplate")) as UIPanel;
            UIDropDown dropDown = panel.Find<UIDropDown>("Dropdown");

            // Set text.
            panel.Find<UILabel>("Label").text = text;

            // Slightly increase width.
            dropDown.autoSize = false;
            dropDown.width = width;

            // Add items.
            dropDown.items = items;
            dropDown.selectedIndex = selectedIndex;

            return dropDown;
        }


        /// <summary>
        /// Creates a plain checkbox using the game's option panel checkbox template.
        /// </summary>
        /// <param name="parent">Parent component</param>
        /// <param name="xPos">Relative x position)</param>
        /// <param name="yPos">Relative y position</param>
        /// <param name="text">Descriptive label text</param>
        /// <returns>New checkbox using the game's option panel template</returns>
        private static UICheckBox AddPlainCheckBox(UIComponent parent, float xPos, float yPos, string text)
        {
            UICheckBox checkBox = parent.AttachUIComponent(UITemplateManager.GetAsGameObject("OptionsCheckBoxTemplate")) as UICheckBox;

            // Set text.
            checkBox.text = text;

            // Position.
            checkBox.relativePosition = new Vector2(xPos, yPos);

            return checkBox;
        }
    }
}