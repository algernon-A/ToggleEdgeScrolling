﻿// <copyright file="ModSettings.cs" company="algernon (K. Algernon A. Sheppard)">
// Copyright (c) algernon (K. Algernon A. Sheppard). All rights reserved.
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace ToggleEdgeScrolling
{
    using System.IO;
    using System.Xml.Serialization;
    using AlgernonCommons.Keybinding;
    using AlgernonCommons.XML;
    using UnityEngine;

    /// <summary>
    /// Global mod settings.
    /// </summary>
    [XmlRoot("ToggleEdgeScrolling")]
    public class ModSettings : SettingsXMLBase
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
        private static readonly UnsavedInputKey UUIKey = new UnsavedInputKey(name: "Toggle Edge Scrolling hotkey", keyCode: KeyCode.S, control: false, shift: true, alt: true);

        /// <summary>
        /// Gets or sets the toggle key.
        /// </summary>
        [XmlElement("ToggleKey")]
        public Keybinding XMLToggleKey
        {
            get => UUIKey.Keybinding;

            set => UUIKey.Keybinding = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether edge scrolling should be automatically disabled on start.
        /// </summary>
        [XmlElement("DisableOnStart")]
        public bool XMLDisableOnStart
        {
            get => DisableOnStart;

            set => DisableOnStart = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether edge scrolling should be automatically disabled when the application loses focus.
        /// </summary>
        [XmlElement("DisableInBackground")]
        public bool XMLDisableInBackground
        {
            get => EdgeScrolling.DisableInBackground;

            set => EdgeScrolling.DisableInBackground = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether edge scrolling should be automatically disabled on start.
        /// </summary>
        [XmlIgnore]
        internal static bool DisableOnStart { get; set; } = false;

        /// <summary>
        /// Gets the current hotkey as a UUI UnsavedInputKey.
        /// </summary>
        [XmlIgnore]
        internal static UnsavedInputKey ToggleKey => UUIKey;

        /// <summary>
        /// Loads settings from file.
        /// </summary>
        internal static void Load() => XMLFileUtils.Load<ModSettings>(SettingsFile);

        /// <summary>
        /// Saves settings to file.
        /// </summary>
        internal static void Save() => XMLFileUtils.Save<ModSettings>(SettingsFile);
    }
}