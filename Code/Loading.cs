using System.Reflection;
using ICities;
using ColossalFramework;
using UnifiedUI.Helpers;


namespace ToggleEdgeScrolling
{
    /// <summary>
    /// Main loading class: the mod runs from here.
    /// </summary>
    public class Loading : LoadingExtensionBase
    {
        // Reference to game edge scrolling SavedBool.
        SavedBool edgeScrollSavedBool;
        UUICustomButton uuiButton;


        /// <summary>
        /// Called by the game when level loading is complete.
        /// </summary>
        /// <param name="mode">Loading mode (e.g. game, editor, scenario, etc.)</param>
        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);

            // Reflect game's edge scrolling saved bool.
            FieldInfo edgeScrolling = typeof(CameraController).GetField("m_edgeScrolling", BindingFlags.NonPublic | BindingFlags.Instance);
            if (edgeScrolling?.GetValue(Singleton<CameraController>.instance) is SavedBool savedBool)
            {
                // Set reference.
                edgeScrollSavedBool = savedBool;

                // Add UUI button.
                uuiButton = UUIHelpers.RegisterCustomButton(
                    name: TESMod.ModName,
                    groupName: null, // default group
                    tooltip: Translations.Translate("TES_NAM"),
                    icon: UUIHelpers.LoadTexture(UUIHelpers.GetFullPath<TESMod>("Resources", "TES-UUI.png")),
                    onToggle: (value) => ToggleEdgeScrolling(value),
                    hotkeys: new UUIHotKeys { ActivationKey = ModSettings.ToggleSavedKey });

                // Set initial state.
                uuiButton.IsPressed = edgeScrollSavedBool;
            }
            else
            {
                Logging.Error("unable to reflect CameraController.m_edgeScrolling");
            }
        }


        /// <summary>
        /// Event handler for edge scrolling button toggle.
        /// </summary>
        /// <param name="scrollingEnabled">True to enable edge scrolling, false to disable</param>
        private void ToggleEdgeScrolling(bool scrollingEnabled)
        {
            // Set game value.
            edgeScrollSavedBool.value = scrollingEnabled;

            // Update button state.
            uuiButton.IsPressed = edgeScrollSavedBool;
        }
    }
}
