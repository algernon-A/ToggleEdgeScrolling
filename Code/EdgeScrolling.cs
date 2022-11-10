// <copyright file="EdgeScrolling.cs" company="algernon (K. Algernon A. Sheppard)">
// Copyright (c) algernon (K. Algernon A. Sheppard). All rights reserved.
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace ToggleEdgeScrolling
{
    using System.Reflection;
    using AlgernonCommons;
    using AlgernonCommons.Translation;
    using ColossalFramework;
    using UnifiedUI.Helpers;

    /// <summary>
    /// Static class to manage edge scrolling.
    /// </summary>
    internal static class EdgeScrolling
    {
        // Reference to game edge scrolling SavedBool.
        private static SavedBool edgeScrollSavedBool;
        private static UUICustomButton uuiButton;


        /// <summary>
        /// Performs setup.
        /// </summary>
        internal static void Setup()
        {
            // Reflect game's edge scrolling saved bool.
            FieldInfo edgeScrolling = typeof(CameraController).GetField("m_edgeScrolling", BindingFlags.NonPublic | BindingFlags.Instance);
            if (edgeScrolling?.GetValue(Singleton<CameraController>.instance) is SavedBool savedBool)
            {
                // Set reference.
                edgeScrollSavedBool = savedBool;

                // Disable edge scrolling on startup, if we're doing that.
                savedBool.value &= !ModSettings.disableOnStart;

                // Add UUI button.
                uuiButton = UUIHelpers.RegisterCustomButton(
                    name: Mod.Instance.Name,
                    groupName: null, // default group
                    tooltip: Translations.Translate("TES_NAM"),
                    icon: UUIHelpers.LoadTexture(UUIHelpers.GetFullPath<Mod>("Resources", "TES-UUI.png")),
                    onToggle: (value) => SetEdgeScrolling(value),
                    hotkeys: new UUIHotKeys { ActivationKey = ModSettings.ToggleKey }
                    );

                // Set UUI button initial state.
                uuiButton.IsPressed = edgeScrollSavedBool;
            }
            else
            {
                Logging.Error("unable to reflect CameraController.m_edgeScrolling");
            }
        }


        /// <summary>
        /// Toggles the current edge scrolling state.
        /// </summary>
        internal static void ToggleEdgeScrolling() => SetEdgeScrolling(!edgeScrollSavedBool);


        /// <summary>
        /// Sets/disables edge scrolling.
        /// </summary>
        /// <param name="scrollingEnabled">True to enable edge scrolling, false to disable</param>
        private static void SetEdgeScrolling(bool scrollingEnabled)
        {
            // Set game value.
            edgeScrollSavedBool.value = scrollingEnabled;

            // Update button state.
            uuiButton.IsPressed = edgeScrollSavedBool;
        }
    }
}