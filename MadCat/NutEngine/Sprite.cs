using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NutEngine
{
    public class Sprite : Node, IDrawable
    {
        private Texture2D texture;

        public bool FlippedX { get; set; }
        public bool FlippedY { get; set; }
        public Color Color { get; set; }

        public Sprite(Texture2D texture) : base()
        {
            this.texture = texture;
            Color = Color.White;
        }

        public void Draw(SpriteBatch spriteBatch, Transform2D currentTransform)
        {
            Vector2 position, scale;
            float rotation;

            /// Достать из матрицы преобразования всю нужную информацию
            currentTransform.Decompose(out scale, out rotation, out position);

            /// Перевернуть спрайт по горизонтали и вертикали, если надо
            SpriteEffects effects = SpriteEffects.None;
            effects |= FlippedX ? SpriteEffects.FlipHorizontally : 0;
            effects |= FlippedY ? SpriteEffects.FlipVertically : 0;

            /// Отрисовать спрайт в позиции, с масштабом и поворотом
            spriteBatch.Draw(
                  texture
                , position
                , null
                , Color
                , rotation
                , new Vector2(texture.Width / 2.0f, texture.Height / 2.0f)
                , scale, effects, 0);
        }
    }
}
