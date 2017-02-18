using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

using NutPacker;
using Microsoft.Xna.Framework;

namespace NutEngine
{
    public class Animation : Node, IDrawable
    {
        public Texture2D Atlas { get; }
        public SpriteSheet SpriteSheet { get; }

        public int CurrentIndex { get; }
        public Rectangle CurrentFrame {
            get {
                return SpriteSheet[CurrentIndex];
            }
        }

        public int  Duration { get; set; }
        public bool Repeat { get; set; }
        public bool Enabled { get; set; }

        public Color Color { get; set; }
        public Vector2 Origin { get; set; }
        public SpriteEffects Effects { get; set; }
        public float LayerDepth { get; set; }


        public Animation(Texture2D atlas, SpriteSheet spriteSheet) : base()
        {
            Atlas = atlas;
            SpriteSheet = spriteSheet;
            CurrentIndex = 0;

            Duration = 0;
            Repeat = true;
            Enabled = true;

            Color = Color.White;

            var center = CurrentFrame.Center;
            Origin = new Vector2(center.X, center.Y);

            Effects = SpriteEffects.None;
            LayerDepth = 0;
        }

        public override void Visit(SpriteBatch spriteBatch, Transform2D currentTransform)
        {


            base.Visit(spriteBatch, currentTransform);
        }

        public void Draw(SpriteBatch spriteBatch, Transform2D currentTransform)
        {
            Vector2 position, scale;
            float rotation;

            currentTransform.Decompose(out scale, out rotation, out position);

            spriteBatch.Draw(
                  Atlas
                , position
                , CurrentFrame
                , Color
                , rotation
                , Origin
                , scale
                , Effects
                , LayerDepth);
        }
    }
}
