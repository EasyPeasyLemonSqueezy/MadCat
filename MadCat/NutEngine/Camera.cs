using Microsoft.Xna.Framework;

namespace NutEngine
{
    public class Camera
    {
        /// TODO: Считать матрицу без 5 умножений
        /// и делать это по-разному в унаследованных камерах
        public Matrix2D Transform
        {
            get
            {
                return Matrix2D.CreateTranslation(-Position) *
                       Matrix2D.CreateTranslation(-Origin) *
                       Matrix2D.CreateRotation(Rotation) *
                       Matrix2D.CreateScale(new Vector2(Zoom, Zoom)) *
                       Matrix2D.CreateTranslation(Origin);
            }
        }

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Zoom { get; set; }

        public Vector2 Rect { get; set; }

        private Vector2 origin;
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
