using Microsoft.Xna.Framework;

namespace NutEngine
{
    public class Camera : Node
    {
        public override Matrix2D Transform
        {
            get
            {
                return Matrix2D.CreateTranslation(-Position) *
                       Matrix2D.CreateTranslation(-Origin) *
                       Matrix2D.CreateRotation(Rotation) *
                       Matrix2D.CreateScale(Scale) *
                       Matrix2D.CreateTranslation(Origin);
            }
        }

        public float Zoom
        {
            get { return Scale.X; }
            set { Scale = new Vector2(value, value); }
        }

        public Vector2 Rect { get; set; }

        /// <summary>
        /// Центр, относительно которого происходят преобразования.
        /// Принимает Vector2 со значениями от 0 до 1 и переводит
        /// их в значения внутри кадра камеры.
        /// </summary>
        public Vector2 origin;
        public Vector2 Origin
        {
            get { return origin; }
            set { origin = new Vector2(value.X * Rect.X, value.Y * Rect.Y); }
        }

        public Camera(float width, float height) : base()
        {
            Rect = new Vector2(width, height);
            Origin = new Vector2(0.5f, 0.5f);
        }
    }
}
