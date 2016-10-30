using Microsoft.Xna.Framework.Graphics;

namespace NutEngine
{
    interface IDrawable
    {
        void Draw(SpriteBatch spriteBatch, Transform2D currentTransform);
    }
}