namespace OpenMario.Environments.OnePlayerEnvironments
{
    using OpenMario.Core.Actors.Concrete;
    using OpenMario.Core.Environment;
    using OpenMario.Core.Players;
    using VectorClass;

    /// <summary>
    /// The level class for Open Mario.  This will be the second level that the player interacts with.
    /// </summary>
    public class LevelTwo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LevelTwo"/> class.
        /// </summary>
        public void InsertObjects(Environment env)
        {
            env.MusicAsset = @"Assets\overworldtheme.mp3";


            env.Players.Add(new PlayerOne());

            // Backgrounds
            env.Actors.Add(new Cloud());

            // Actors
            env.Actors.Add(new OrangeLand { Position = new Vector2D_Dbl(0, 400), Width = 1500, Height = 50 });
            env.Actors.Add(new QuestionBox { Position = new Vector2D_Dbl(300, 300) });
            env.Actors.Add(new QuestionBox { Position = new Vector2D_Dbl(520, 300) });
            env.Actors.Add(new QuestionBox { Position = new Vector2D_Dbl(780, 300) });
            //env.Actors.Add(new Goomba { Position = new Vector2D_Dbl(200, 100), WalkingVelocity = new Vector2D_Dbl(1, 0) });
            //env.Actors.Add(new Goomba { Position = new Vector2D_Dbl(800, 100), WalkingVelocity = new Vector2D_Dbl(1, 0) });
            env.Actors.Add(new GreenStaticPipe { Position = new Vector2D_Dbl(380, 340) });
            env.Actors.Add(new GreenStaticPipe { Position = new Vector2D_Dbl(660, 340) });
            env.Actors.Add(new GreenStaticPipe { Position = new Vector2D_Dbl(900, 340) });
            env.Actors.Add(new Coin { Position = new Vector2D_Dbl(380, 300) });
            env.Actors.Add(new Coin { Position = new Vector2D_Dbl(450, 250) });
            env.Actors.Add(new Coin { Position = new Vector2D_Dbl(520, 200) });
            env.Actors.Add(new Coin { Position = new Vector2D_Dbl(780, 250) });
            env.Actors.Add(new Coin { Position = new Vector2D_Dbl(1000, 300) });
            env.Actors.Add(new Lava { Position = new Vector2D_Dbl(420, 380), Width = 240, Height = 20 });

            // Players
            env.Actors.Add(new Mario(env.Players[0]));

            env.Width = 1500;
        }
    }
}