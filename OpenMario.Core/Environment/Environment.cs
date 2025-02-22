
namespace OpenMario.Core.Environment
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using OpenMario.Core.Actors;
    using OpenMario.Core.Actors.Concrete;
    using OpenMario.Core.Players;
    using VectorClass;
    using WMPLib;

    /// <summary>
    /// The environment.
    /// </summary>
    public abstract class Environment : IDisposable
    {
        public bool isBoxActivated = false;
        private long framesFromStart = 0;
        private long lastFrameLavaPoped = 0;
        public QuestionBox ActiveBox;
        public bool isNewLevel;
        public Form f;

        /// <summary>
        /// Initializes a new instance of the <see cref="Environment"/> class.
        /// </summary>
        protected Environment()
        {
            this.Players = new List<BasePlayer>();
            this.Actors = new List<BaseActor>();
            this.ActorsToRemove = new List<BaseActor>();
            this.Width = Engine.Engine.DefaultWidth;
            this.Height = Engine.Engine.DefaultHeight;
            this.ViewportWidth = Engine.Engine.DefaultWidth;
            this.ViewportHeight = Engine.Engine.DefaultHeight;
            this.ViewportPosition = new Vector2D_Dbl(0, 0);
            this.ViewportVelocity = new Vector2D_Dbl(0, 0);
            this.IsRunning = true;
            this.isNewLevel = false;
        }

        /// <summary>
        /// Gets or sets the starting position.
        /// TODO - StartPosition not used yet.
        /// </summary>
        public Point StartingPosition { get; set; }

        /// <summary>
        /// Gets or sets the players.
        /// </summary>
        public List<BasePlayer> Players { get; set; }

        /// <summary>
        /// Gets or sets the actors.
        /// </summary>
        public List<BaseActor> Actors { get; set; }

        /// <summary>
        /// Gets or sets the actors to remove.
        /// </summary>
        public List<BaseActor> ActorsToRemove { get; set; }

        /// <summary>
        /// Gets or sets the viewport position.
        /// </summary>
        public Vector2D_Dbl ViewportPosition { get; set; }

        /// <summary>
        /// Gets or sets the viewport velocity.
        /// </summary>
        public Vector2D_Dbl ViewportVelocity { get; set; }

        /// <summary>
        /// Gets or sets the music player.
        /// </summary>
        public WindowsMediaPlayer MusicPlayer { get; set; }

        /// <summary>
        /// Gets or sets the music asset.
        /// </summary>
        public string MusicAsset { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the viewport width.
        /// </summary>
        public int ViewportWidth { get; set; }

        /// <summary>
        /// Gets or sets the viewport height.
        /// </summary>
        public int ViewportHeight { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="Environment"/> is running.
        /// </summary>
        public bool IsRunning { get; set; }

        public FallingLava FallingLava;

        /// <summary>
        /// The register all the keys.
        /// </summary>
        /// <param name="f">
        /// The <see cref="Form"/> for the keys.
        /// </param>
        public void RegisterAllKeys(Form f)
        {
            this.f = f;
            foreach (var p in this.Players)
            {
                p.RegisterKeyMappings(f);
            }
        }



        /// <summary>
        /// The update method for <see cref="Environment"/>
        /// </summary>
        public void Update()
        {
            if (!this.IsRunning)
            {
                return;
            }

            if (this.isNewLevel)
            {
                this.Actors = new List<BaseActor>();
                this.Players = new List<BasePlayer>();
                InsertObjects();
                Load();
                this.isNewLevel = false;
                foreach (var p in this.Players)
                {
                    p.RegisterKeyMappings(this.f);
                }
            }

            foreach (var a in this.Actors)
            {
                a.Update(this.Actors);
            }

            if (this.isBoxActivated)
            {
                var coin = new Coin { Position = new Vector2D_Dbl(ActiveBox.Position.X, ActiveBox.Position.Y - 40) };
                coin.Load(this);
                this.Actors.Add(coin);

                this.isBoxActivated = false;
            }

            if (framesFromStart - lastFrameLavaPoped == 120)
            {
                this.ActorsToRemove.Add(this.FallingLava);
                this.FallingLava = new FallingLava { Position = new Vector2D_Dbl(200, -10), Width = 20, Height = 20 };
                this.FallingLava.Load(this);
                this.Actors.Add(FallingLava);
                lastFrameLavaPoped = framesFromStart;
            }
            else
            {
                framesFromStart++;
            }

            // The following is for updating the viewport.
            // var scrollingactors = Actors.Where(x => x.EnvironmentEffect == BaseActor.EnvironmentEffectType.ScrollsWithViewport);
            foreach (var a in this.Actors.Where(x => x.EnvironmentEffect == BaseActor.EnvironmentEffectType.ControlsViewportScroll))
            {
                // Lets update the viewport if the actor is controlling our scroll.
                var leftthresh = this.ViewportWidth * (1d / 3d);
                var rightthresh = this.ViewportWidth * (1d / 2d);
                
                if (this.CalculateRelativePosition(a).X <= leftthresh
                    && a.Velocity.X > 0
                    && this.ViewportPosition.X > 0)
                {
                    this.ViewportPosition -= new Vector2D_Dbl(leftthresh - this.CalculateRelativePosition(a).X, 0);
                }
                
                if (this.CalculateRelativePosition(a).X >= rightthresh
                    && a.Velocity.X < 0
                    && this.ViewportPosition.X + this.ViewportWidth < this.Width)
                {
                    this.ViewportPosition += new Vector2D_Dbl(this.CalculateRelativePosition(a).X - rightthresh, 0);
                }
            }

            // Remove Unloaded Actors.
            foreach (var a in this.ActorsToRemove)
            {
                this.Actors.Remove(a);
            }

            // If all players are dead, move on.
            // TODO: Support more than just Mario class.
            if (!this.Actors.Any(x => x.GetType() == typeof(Mario) && ((Mario)x).IsAlive))
            {
                this.IsRunning = false;
            }

        }

        /// <summary>
        /// The calculate relative position of the <see cref="BaseActor"/>
        /// </summary>
        /// <param name="a">
        /// The <see cref="BaseActor"/>
        /// </param>
        /// <returns>
        /// The <see cref="Vector2D_Dbl"/>.
        /// </returns>
        public Vector2D_Dbl CalculateRelativePosition(BaseActor a)
        {
            return a.Position - this.ViewportPosition;
        }

        /// <summary>
        /// The rendering of the <see cref="Graphics"/>
        /// </summary>
        /// <param name="g">
        /// The <see cref="Graphics"/> to render.
        /// </param>
        public void Render(Graphics g)
        {
            foreach (var a in this.Actors)
            {
                a.Draw(g);
            }
        }

        /// <summary>
        /// The loading of the <see cref="Environment"/>
        /// </summary>
        public void Load()
        {
            // TODO: Don't just load all, load in what is in the viewport.
            foreach (var a in this.Actors)
            {
                a.Load(this);
            }

            // Music
            if (string.IsNullOrWhiteSpace(this.MusicAsset))
            {
                return;
            }

            this.MusicPlayer = new WindowsMediaPlayer { URL = this.MusicAsset };
            this.MusicPlayer.controls.play();
        }

        public void InsertObjects()
        {
            this.MusicAsset = @"Assets\overworldtheme.mp3";


            this.Players.Add(new PlayerOne());

            // Backgrounds
            this.Actors.Add(new Cloud());

            // Actors
            this.Actors.Add(new OrangeLand { Position = new Vector2D_Dbl(0, 400), Width = 1500, Height = 50 });
            this.Actors.Add(new QuestionBox { Position = new Vector2D_Dbl(300, 300) });
            this.Actors.Add(new QuestionBox { Position = new Vector2D_Dbl(520, 300) });
            this.Actors.Add(new QuestionBox { Position = new Vector2D_Dbl(780, 300) });
            //env.Actors.Add(new Goomba { Position = new Vector2D_Dbl(200, 100), WalkingVelocity = new Vector2D_Dbl(1, 0) });
            //env.Actors.Add(new Goomba { Position = new Vector2D_Dbl(800, 100), WalkingVelocity = new Vector2D_Dbl(1, 0) });
            this.Actors.Add(new GreenStaticPipe { Position = new Vector2D_Dbl(380, 340) });
            this.Actors.Add(new GreenStaticPipe { Position = new Vector2D_Dbl(660, 340) });
            this.Actors.Add(new GreenStaticPipe { Position = new Vector2D_Dbl(900, 340) });
            this.Actors.Add(new Coin { Position = new Vector2D_Dbl(380, 300) });
            this.Actors.Add(new Coin { Position = new Vector2D_Dbl(450, 250) });
            this.Actors.Add(new Coin { Position = new Vector2D_Dbl(520, 200) });
            this.Actors.Add(new Coin { Position = new Vector2D_Dbl(780, 250) });
            this.Actors.Add(new Coin { Position = new Vector2D_Dbl(1000, 300) });
            this.Actors.Add(new Lava { Position = new Vector2D_Dbl(420, 380), Width = 240, Height = 20 });

            // Players
            this.Actors.Add(new Mario(this.Players[0]));

            this.Width = 1500;
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
        }
    }
}