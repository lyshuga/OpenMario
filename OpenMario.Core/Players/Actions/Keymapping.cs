﻿//-----------------------------------------------------------------------
// <copyright file="KeyMapping.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Declares all of the key mappings for a given player. </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Core.Players.Actions
{
    using System.Windows.Forms;

    public class KeyMapping
    {
        /// <summary>
        /// Enumerates the different types of keys.
        /// </summary>
        public enum KeyAction
        {
            /// <summary>
            /// Move the actor to the left.
            /// </summary>
            LEFT,

            /// <summary>
            /// Move the actor "up" this is differnet from jump and would likely be used for air controls or swimming.
            /// </summary>
            UP,

            /// <summary>
            /// Move the actor to the right.
            /// </summary>
            RIGHT,

            /// <summary>
            /// Move the actor down. This would be down when swimming and duck when on solid land.
            /// </summary>
            DOWN,
            
            /// <summary>
            /// Have the actor "jump" into the air.
            /// </summary>
            JUMP,
            
            /// <summary>
            /// Have the actor "shoot."  This will be used for fireballs etc.
            /// </summary>
            SHOOT,

            /// <summary>
            /// Have the actor run.
            /// </summary>
            RUN,
        }

        public enum KeyPressType
        {
            DOWN,
            UP
        }

        public Keys Key { get; set; }
        public KeyAction Action { get; set; }
        public KeyPressType PressType { get; set; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;
            return this.Action == ((KeyMapping)obj).Action;
                // && this.Key == ((KeyMapping)obj).Key
                // && this.PressType == ((KeyMapping)obj).PressType;
        }
    }
}
