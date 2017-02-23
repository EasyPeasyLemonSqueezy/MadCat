using Microsoft.Xna.Framework.Graphics;

namespace NutEngine
{
    public interface IDrawable
    {
        void Draw(SpriteBatch spriteBatch, Transform2D currentTransform);
    }
}