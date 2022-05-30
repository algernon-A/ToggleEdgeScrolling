﻿using ICities;
using ColossalFramework.UI;


namespace ToggleEdgeScrolling
{
    public class TESMod : IUserMod
    {
        public static string ModName => "Toggle Edge Scrolling";
        public static string Version => "1.1.2";

        public string Name => ModName + " " + Version;
        public string Description => Translations.Translate("TES_DESC");


        /// <summary>
        /// Called by the game when the mod is enabled.
        /// </summary>
        public void OnEnabled()
        {
            // Load the settings file.
            ModSettings.Load();

            // Attaching options panel event hook - check to see if UIView is ready.
            if (UIView.GetAView() != null)
            {
                // It's ready - attach the hook now.
                OptionsPanel.OptionsEventHook();
            }
            else
            {
                // Otherwise, queue the hook for when the intro's finished loading.
                LoadingManager.instance.m_introLoaded += OptionsPanel.OptionsEventHook;
            }
        }


        /// <summary>
        /// Called by the game when the mod options panel is setup.
        /// </summary>
        public void OnSettingsUI(UIHelperBase helper)
        {
            // Setup options panel reference.
            OptionsPanel.optionsPanel = ((UIHelper)helper).self as UIScrollablePanel;
            OptionsPanel.optionsPanel.autoLayout = false;
        }
    }
}
