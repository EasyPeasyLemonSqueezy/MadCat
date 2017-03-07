using Microsoft.Xna.Framework;

namespace NutEngine.Camera
{
    public class OrthographicTRSCamera : Camera2D
    {
        public override Matrix2D Transform
            => Matrix2D.CreateTranslation(-Origin)
             * Matrix2D.CreateTRS(-Position, new Vector2(Zoom, Zoom), Rotation)
             * Matrix2D.CreateTranslation(Origin);


        public OrthographicTRSCamera(Vector2 frame) : base(frame) { }
    }
}
