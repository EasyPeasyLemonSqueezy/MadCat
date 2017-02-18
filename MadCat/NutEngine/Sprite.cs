using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NutEngine
{
    public class Sprite : Node, IDrawable
    {
        public Texture2D Atlas { get; }

        public Rectangle? Frame { get; set; }
        public Color Color { get; set; }

        /// Center of the rotation, by default - center of the frame/atlas.
        public Vector2 Origin { get; set; }
        public SpriteEffects Effects { get; set; }
        public float LayerDepth { get; set; }

        public Sprite(Texture2D atlas, Rectangle? frame = null) : base()
        {
            Atlas = atlas;
            Frame = frame;

            Color = Color.White;

            if (Frame != null) {
                var center = Frame.Value.Center;
                Origin = new Vector2(center.X, center.Y);
            }
            else {
                Origin = new Vector2(Atlas.Width / 2.0f, Atlas.Height / 2.0f);
            }

            Effects = SpriteEffects.None;
            LayerDepth = 0;
        }

        public void Draw(SpriteBatch spriteBatch, Transform2D currentTransform)
        {
            Vector2 position, scale;
            float rotation;

            currentTransform.Decompose(out scale, out rotation, out position);

            spriteBatch.Draw(
                  Atlas
                , position
                , Frame
                , Color
                , rotation
                , Origin
                , scale
                , Effects
                , LayerDepth);
        }
    }
}
