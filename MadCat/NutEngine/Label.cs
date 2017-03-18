using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace NutEngine
{
    public class Label : Node, IDrawable
    {
        public SpriteFont Font { get; private set; }
        public string Text { get; set; }

        public Color Color { get; set; }
        public Vector2 Origin { get; set; }
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
            Origin = Font.MeasureString(Text) / 2;
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
                , Origin
                , scale
                , Effects
                , LayerDepth);
        }
    }
}