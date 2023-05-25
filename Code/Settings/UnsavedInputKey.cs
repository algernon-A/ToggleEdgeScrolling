// <copyright file="UnsavedInputKey.cs" company="algernon (K. Algernon A. Sheppard)">
// Copyright (c) algernon (K. Algernon A. Sheppard). All rights reserved.
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace ToggleEdgeScrolling
{
    using AlgernonCommons.Keybinding;
    using UnityEngine;

    /// <summary>
    /// UUI unsaved input key.
    /// </summary>
    public class UnsavedInputKey : UnifiedUI.Helpers.UnsavedInputKey
    {
        private Keybinding _binding;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsavedInputKey"/> class.
        /// </summary>
        /// <param name="name">Reference name.</param>
        /// <param name="keyCode">Keycode.</param>
        /// <param name="control">Control modifier key status.</param>
        /// <param name="shift">Shift modifier key status.</param>
        /// <param name="alt">Alt modifier key status.</param>
        public UnsavedInputKey(string name, KeyCode keyCode, bool control, bool shift, bool alt)
            : base(keyName: name, modName: "Toggle Edge Scrolling", Encode(keyCode, control: control, shift: shift, alt: alt))
        {
            _binding = new Keybinding(Key, Control, Shift, Alt);
        }

        /// <summary>
        /// Gets or sets the current key as a Keybinding.
        /// </summary>
        public Keybinding Keybinding
        {
            get => _binding;

            set
            {
                _binding = value;
                this.value = value.Encode();
            }
        }

        /// <summary>
        /// Called by UUI when a key conflict is resolved.
        /// Used here to save the new key setting.
        /// </summary>
        public override void OnConflictResolved() => ModSettings.Save();
    }
}
