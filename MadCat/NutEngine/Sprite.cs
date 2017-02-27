using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NutEngine
{
    public class Sprite : Node, IDrawable
    {
        public Texture2D Atlas { get; private set; }

        private Rectangle? frame;
        public Rectangle? Frame {
            get {
                return frame;
            }
            set {
                frame = value;

                if (value != null) {
                    Origin = new Vector2(Frame.Value.Width / 2f, Frame.Value.Height / 2f);
                }
                else {
                    Origin = new Vector2(Atlas.Width / 2.0f, Atlas.Height / 2.0f);
                }
            }
        }
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

            Effects = SpriteEffects.None;
            LayerDepth = 0;
        }

        public void Change(Sprite sprite)
        {
            Atlas = sprite.Atlas;
            Frame = sprite.Frame;
            Color = sprite.Color;
            Effects = sprite.Effects;
            LayerDepth = sprite.LayerDepth;
        }

        public void Draw(SpriteBatch spriteBatch, Matrix2D currentTransform)
        {
            var position = currentTransform.Translation;
            var rotation = currentTransform.Rotation;
            var scale = currentTransform.Scale;

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
