﻿//-----------------------------------------------------------------------
// <copyright file="LevelOne.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Level design for LevelOne. Contains all additions to the environment and is whats used to render the level. </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Environments.OnePlayerEnvironments
{
    using OpenMario.Core.Actors.Concrete;
    using OpenMario.Core.Players;
    using VectorClass;

    /// <summary>
    /// The level class for Open Mario.  This will be the first level that the player interacts with.
    /// </summary>
    public class LevelOne : Core.Environment.Environment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LevelOne"/> class.
        /// </summary>
        public LevelOne()
        {
            // Sounds
            this.MusicAsset = @"Assets\overworldtheme.mp3";

            // Controls
            Players.Add(new PlayerOne());

            // Backgrounds
            Actors.Add(new Cloud());

            // Players
            Mario mario = new Mario(Players[0]);
            Actors.Add(mario);

            // Actors
            Actors.Add(new OrangeLand { Position = new Vector2D_Dbl(0, 400), Width = 1500, Height = 50 });
            Actors.Add(new QuestionBox { Position = new Vector2D_Dbl(300, 300) });
            Actors.Add(new Goomba(mario) { Position = new Vector2D_Dbl(420, 100) });
            Actors.Add(new Coin { Position = new Vector2D_Dbl(450, 250) });
            Actors.Add(new GreenStaticPipe { Position = new Vector2D_Dbl(200, 340) });
            Actors.Add(new Coin { Position = new Vector2D_Dbl(380, 300) });
            Actors.Add(new GreenStaticPipe { Position = new Vector2D_Dbl(520, 340) });
            Actors.Add(new Coin { Position = new Vector2D_Dbl(520, 300) });
            Actors.Add(new GreenStaticPipe { Position = new Vector2D_Dbl(660, 340) });
            Actors.Add(new Lava { Position = new Vector2D_Dbl(560, 380), Width = 100, Height = 20 });
            Actors.Add(new Coin { Position = new Vector2D_Dbl(660, 300) });
            Actors.Add(new Teleport(this) { Position = new Vector2D_Dbl(1060, 340) });

            this.Width = 1500;
        }
    }
}