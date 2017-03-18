using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NutEngine
{
    public class Sprite : Node, IDrawable
    {
        public TextureRegion TextureRegion { get; protected set; }

        /// Center of the transformations, by default - center of the frame/texture.
        public Vector2 Origin { get; set; }

        public Color Color { get; set; }
        public SpriteEffects Effects { get; set; }
        public float LayerDepth { get; set; }

        public Sprite(Texture2D texture) : base()
        {
            TextureRegion = new TextureRegion(texture);
            Initialize();
        }

        public Sprite(Texture2D texture, Rectangle frame) : base()
        {
            TextureRegion = new TextureRegion(texture, frame);
            Initialize();
        }

        protected new void Initialize()
        {
            Origin = new Vector2(
                  TextureRegion.Frame.Width  / 2f
                , TextureRegion.Frame.Height / 2f
                );

            Color = Color.White;

            Effects = SpriteEffects.None;
            LayerDepth = 0;
        }

        public void Change(Sprite sprite)
        {
            TextureRegion = sprite.TextureRegion;
            
            Origin = new Vector2(
                  TextureRegion.Frame.Width  / 2f
                , TextureRegion.Frame.Height / 2f
                );

            Color = sprite.Color;
            Effects = sprite.Effects;
            LayerDepth = sprite.LayerDepth;
        }

        public void Draw(SpriteBatch spriteBatch, Matrix2D currentTransform)
        {
            var position = currentTransform.Translation;
            var rotation = currentTransform.Rotation;
            var scale    = currentTransform.Scale;

            spriteBatch.Draw(
                  TextureRegion.Texture
                , position
                , TextureRegion.Frame
                , Color
                , rotation
                , Origin
                , scale
                , Effects
                , LayerDepth);
        }
    }
}
