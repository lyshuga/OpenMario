﻿
namespace OpenMario.Core.Actors.Concrete
{
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using VectorClass;

    /// <summary>
    /// The question box.
    /// </summary>
    public class Teleport : StaticBox
    {
        /// <summary>
        /// The drawable object.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private Bitmap drawable;
        private bool isActivated = false;
        private bool previouslyActivated = false;
        public Environment.Environment env;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionBox"/> class.
        /// </summary>
        public Teleport(Environment.Environment env)
        {
            this.Width = 40;
            this.Height = 40;
            this.env = env;
        }

        /// <summary>
        /// The Load method for <see cref="QuestionBox"/>
        /// </summary>
        /// <param name="env">The env.</param>
        public override void Load(Environment.Environment env)
        {
            this.Environment = env;

            Bitmap B = new Bitmap(this.Width, this.Height);
            for (int i = 0; i < B.Height; i++)
                for (int j = 0; j < B.Width; j++)
                    B.SetPixel(j, i, Color.Orange);
            this.drawable = B;
        }

        /// <summary>
        /// The drawing method for the <see cref="QuestionBox"/> actor.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> for the <see cref="QuestionBox"/> actor</param>
        public override void Draw(Graphics g)
        {
            var pos = Environment.CalculateRelativePosition(this);
            g.DrawImage(this.drawable, (int)pos.X, (int)pos.Y);
        }
    }
}
