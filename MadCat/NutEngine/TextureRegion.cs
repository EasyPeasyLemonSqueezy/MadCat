using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NutEngine
{
    public class TextureRegion
    {
        public Texture2D Texture { get; private set; }
        public Rectangle Frame { get; set; }

        public TextureRegion(Texture2D texture)
            : this(texture, new Rectangle(0, 0, texture.Width, texture.Height)) { }

        public TextureRegion(Texture2D texture, Rectangle frame)
        {
            Texture = texture;
            Frame = frame;
        }
    }
}
