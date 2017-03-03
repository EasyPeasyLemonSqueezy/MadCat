using Microsoft.Xna.Framework;

namespace NutEngine.Camera
{
    public class OrthographicSRTCamera : Camera2D
    {
        /// TODO: Считать матрицу без 4 умножений
        /// и делать это по-разному в унаследованных камерах
        public override Matrix2D Transform {
            get {
                return Matrix2D.CreateTranslation(-Origin)
                     * Matrix2D.CreateTranslation(-Position)
                     * Matrix2D.CreateRotation(Rotation)
                     * Matrix2D.CreateScale(new Vector2(Zoom, Zoom))
                     * Matrix2D.CreateTranslation(Origin);
            }
        }


        public OrthographicSRTCamera(Vector2 frame) : base(frame) { }
    }
}
