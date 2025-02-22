﻿//-----------------------------------------------------------------------
// <copyright file="MainForm.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> The main form for interacting with OpenMario. </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using OpenMario.Core.Engine;
    using OpenMario.Environments.OnePlayerEnvironments;

    /// <summary>
    /// The main window for interacting with OpenMario.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// The engine.
        /// </summary>
        private readonly Engine engine;

        /// <summary>
        /// The _environment.
        /// </summary>
        private Core.Environment.Environment environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
            this.environment = new LevelOne();
            this.environment.Load();
            this.environment.RegisterAllKeys(this);

            this.engine = new Engine();
            this.engine.Load(this, null);
            this.engine.OnNewFrame += (o, e) => this.Tick(e);
            this.FormClosing += (o, e) => this.engine.Dispose();
            this.engine.Start();
        }

        /// <summary>
        /// The tick.
        /// </summary>
        /// <param name="e">
        /// The <see cref="FrameEventArgs"/> for <see cref="MainForm"/>
        /// </param>
        public void Tick(FrameEventArgs e)
        {
            using (var g = Graphics.FromImage(e.Frame))
            {
                this.environment.Update();
                if (this.environment.IsRunning == false)
                {
                    this.environment = new LevelOne();
                    this.environment.Load();
                    this.environment.RegisterAllKeys(this);
                }
                this.environment.Render(g);
                g.Flush();
            }

            canvas.Image = e.Frame;
        }

        /// <summary>
        /// The draw debug method for <see cref="Engine"/>
        /// </summary>
        /// <param name="g">
        /// The <see cref="Graphics"/> for the Draw Debug method..
        /// </param>
        
    }
}