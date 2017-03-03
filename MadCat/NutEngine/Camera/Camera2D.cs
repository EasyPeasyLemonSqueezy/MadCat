using Microsoft.Xna.Framework;

namespace NutEngine.Camera
{
    public abstract class Camera2D
    {
        /// TODO: Считать матрицу без 5 умножений
        /// и делать это по-разному в унаследованных камерах
        public abstract Matrix2D Transform { get; }

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Zoom { get; set; }

        public Vector2 Frame { get; set; }
        public Vector2 Origin { get; set; }

        public Camera2D(Vector2 frame)
        {
            Frame = frame;
            Origin = frame / 2;
        }
    }
}
