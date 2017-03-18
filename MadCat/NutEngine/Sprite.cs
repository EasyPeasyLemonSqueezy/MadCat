using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NutEngine
{
    public class Sprite : Node, IDrawable
    {
        public TextureRegion TextureRegion { get; protected set; }

        /// Center of the transformations, by default - center of the frame/texture.
        public Vector2 Origin {
            get => new Vector2(OriginAbs.X / TextureRegion.Frame.Width, OriginAbs.Y / TextureRegion.Frame.Height);
            set => OriginAbs = new Vector2(TextureRegion.Frame.Width * value.X, TextureRegion.Frame.Height * value.Y);
        }
        public Vector2 OriginAbs { get; set; }

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
            Origin = new Vector2(.5f, .5f);

            Color = Color.White;

            Effects = SpriteEffects.None;
            LayerDepth = 0;
        }

        public void Change(Sprite sprite)
        {
            TextureRegion = sprite.TextureRegion;
            
            Origin = new Vector2(.5f, .5f);

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
                , OriginAbs
                , scale
                , Effects
                , LayerDepth);
        }
    }
}
