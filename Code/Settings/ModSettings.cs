using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using ColossalFramework;


namespace ToggleEdgeScrolling
{
    /// <summary>
    /// Global mod settings.
    /// </summary>
	[XmlRoot("ToggleEdgeScrolling")]
    public class ModSettings
    {
        // Settings file name.
        [XmlIgnore]
        private static readonly string SettingsFileName = "ToggleEdgeScrolling.xml";

        // User settings directory.
        [XmlIgnore]
        private static readonly string UserSettingsDir = ColossalFramework.IO.DataLocation.localApplicationData;

        // Full userdir settings file name.
        [XmlIgnore]
        private static readonly string SettingsFile = Path.Combine(UserSettingsDir, SettingsFileName);

        // UUI hotkey.
        [XmlIgnore]
        private static readonly UnsavedInputKey uuiKey = new UnsavedInputKey(name: "Toggle Edge Scrolling hotkey", keyCode: KeyCode.S, control: false, shift: true, alt: true);

        // Automatically disable edge scrolling on game load.
        [XmlIgnore]
        internal static bool disableOnStart = false;


        // Language.
        [XmlElement("Language")]
        public string Language
        {
            get => Translations.Language;

            set => Translations.Language = value;
        }

        // Hotkey element.
        [XmlElement("ToggleKey")]
        public KeyBinding ToggleKey
        {
            get => uuiKey.KeyBinding;

            set => uuiKey.KeyBinding = value;
        }


        /// <summary>
        // Automatically disable edge scrolling on load.
        /// </summary>
        [XmlElement("DisableOnStart")]
        public bool DisableOnStart
        {
            get => disableOnStart;

            set => disableOnStart = value;
        }


        /// <summary>
        /// Current hotkey as UUI UnsavedInputKey.
        /// </summary>
        [XmlIgnore]
        internal static UnsavedInputKey UUIKey => uuiKey;


        /// <summary>
        /// The current hotkey settings as ColossalFramework InputKey.
        /// </summary>
        /// </summary>
        [XmlIgnore]
        internal static InputKey CurrentHotkey
        {
            get => uuiKey.value;

            set => uuiKey.value = value;
        }


        /// <summary>
        /// Load settings from XML file.
        /// </summary>
        internal static void Load()
        {
            try
            {
                // Attempt to read new settings file (in user settings directory).
                string fileName = SettingsFile;
                if (!File.Exists(fileName))
                {
                    // No settings file in user directory; use application directory instead.
                    fileName = SettingsFileName;

                    if (!File.Exists(fileName))
                    {
                        Logging.Message("no settings file found");
                        return;
                    }
                }

                // Read settings file.
                using (StreamReader reader = new StreamReader(fileName))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(ModSettings));
                    if (!(xmlSerializer.Deserialize(reader) is ModSettings settingsFile))
                    {
                        Logging.Error("couldn't deserialize settings file");
                    }
                }
            }
            catch (Exception e)
            {
                Logging.LogException(e, "exception reading XML settings file");
            }
        }


        /// <summary>
        /// Save settings to XML file.
        /// </summary>
        internal static void Save()
        {
            try
            {
                // Pretty straightforward.
                using (StreamWriter writer = new StreamWriter(SettingsFile))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(ModSettings));
                    xmlSerializer.Serialize(writer, new ModSettings());
                }

                // Cleaning up after ourselves - delete any old config file in the application direcotry.
                if (File.Exists(SettingsFileName))
                {
                    File.Delete(SettingsFileName);
                }
            }
            catch (Exception e)
            {
                Logging.LogException(e, "exception saving XML settings file");
            }
        }
    }


    /// <summary>
    /// Basic keybinding class - code and modifiers.
    /// </summary>
    public class KeyBinding
    {
        [XmlAttribute("KeyCode")]
        public int keyCode;

        [XmlAttribute("Control")]
        public bool control;

        [XmlAttribute("Shift")]
        public bool shift;

        [XmlAttribute("Alt")]
        public bool alt;


        /// <summary>
        /// Encode keybinding as saved input key for UUI.
        /// </summary>
        /// <returns></returns>
        internal InputKey Encode() => SavedInputKey.Encode((KeyCode)keyCode, control, shift, alt);
    }


    /// <summary>
    /// UUI unsaved input key.
    /// </summary>
    public class UnsavedInputKey : UnifiedUI.Helpers.UnsavedInputKey
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Reference name</param>
        /// <param name="keyCode">Keycode</param>
        /// <param name="control">Control modifier key status</param>
        /// <param name="shift">Shift modifier key status</param>
        /// <param name="alt">Alt modifier key status</param>
        public UnsavedInputKey(string name, KeyCode keyCode, bool control, bool shift, bool alt) :
            base(keyName: name, modName: "Repaint", Encode(keyCode, control: control, shift: shift, alt: alt))
        {
        }


        /// <summary>
        /// Called by UUI when a key conflict is resolved.
        /// Used here to save the new key setting.
        /// </summary>
        public override void OnConflictResolved() => ModSettings.Save();


        /// <summary>
        /// 
        /// </summary>
        public KeyBinding KeyBinding
        {
            get => new KeyBinding { keyCode = (int)Key, control = Control, shift = Shift, alt = Alt };
            set => this.value = value.Encode();
        }
    }
}