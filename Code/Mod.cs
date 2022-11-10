﻿// <copyright file="Mod.cs" company="algernon (K. Algernon A. Sheppard)">
// Copyright (c) algernon (K. Algernon A. Sheppard). All rights reserved.
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace ToggleEdgeScrolling
{
    using AlgernonCommons;
    using AlgernonCommons.Translation;
    using ICities;

    /// <summary>
    /// The base mod class for instantiation by the game.
    /// </summary>
    public sealed class Mod : OptionsMod<OptionsPanel>, IUserMod
    {
        /// <summary>
        /// Gets the mod's base display name (name only).
        /// </summary>
        public override string BaseName => "Toggle Edge Scrolling";

        /// <summary>
        /// Gets the mod's description for display in the content manager.
        /// </summary>
        public string Description => Translations.Translate("TES_DESC");

        /// <summary>
        /// Saves settings file.
        /// </summary>
        public override void SaveSettings() => ModSettings.Save();

        /// <summary>
        /// Loads settings file.
        /// </summary>
        public override void LoadSettings() => ModSettings.Load();
    }
}
