using Microsoft.Xna.Framework.Graphics;

namespace NutEngine
{
    public interface IDrawable
    {
        void Draw(SpriteBatch spriteBatch, Matrix2D currentTransform);
    }
}