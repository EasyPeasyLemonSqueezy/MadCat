using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace NutEngine
{
    public class Label : Node, IDrawable
    {
        public SpriteFont Font { get; private set; }
        public string Text { get; set; }

        public Vector2 Origin {
            get => new Vector2(OriginAbs.X / Font.MeasureString(Text).X, OriginAbs.Y / Font.MeasureString(Text).Y);
            set => OriginAbs = new Vector2(Font.MeasureString(Text).X * value.X, Font.MeasureString(Text).Y * value.Y);
        }
        public Vector2 OriginAbs { get; set; }

        public Color Color { get; set; }
        public SpriteEffects Effects { get; set; }
        public float LayerDepth { get; set; }

        public Label(SpriteFont font, string text) : base()
        {
            Font = font;
            Text = text;
            
            Initialize();
        }

        protected new void Initialize()
        {
            OriginAbs = Font.MeasureString(Text) / 2;
            Color = Color.White;
            Effects = SpriteEffects.None;
            LayerDepth = 0;
        }

        public void Draw(SpriteBatch spriteBatch, Matrix2D currentTransform)
        {
            var position = currentTransform.Translation;
            var rotation = currentTransform.Rotation;
            var scale    = currentTransform.Scale;

            spriteBatch.DrawString(
                  Font
                , Text
                , position
                , Color
                , rotation
                , OriginAbs
                , scale
                , Effects
                , LayerDepth);
        }
    }
}