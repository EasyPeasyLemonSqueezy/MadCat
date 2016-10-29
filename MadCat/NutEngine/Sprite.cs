using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NutEngine
{
    public class Sprite : Node
    {
        private Texture2D texture;

        public Sprite(Texture2D texture) : base()
        {
            this.texture = texture;
        }

        public override void Visit(SpriteBatch spriteBatch)
        {
            base.Visit(spriteBatch); /// Применили все преобразования

            Vector2 position, scale;
            float rotation;

            /// Достать из матрицы преобразования всю нужную информацию.
            transform.Decompose(out scale, out rotation, out position);

            /// Отрисовать спрайт в позиции с масштабом и поворотом.
            spriteBatch.Draw(
                  texture
                , position
                , null
                , Color.White
                , rotation
                , new Vector2(texture.Width / 2.0f, texture.Height / 2.0f)
                , scale, SpriteEffects.None, 0);
        }
    }
}
