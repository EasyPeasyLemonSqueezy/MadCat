using Microsoft.Xna.Framework;

namespace MadCat
{
    public static class Physics
    {
        public static Vector2 Speed(Vector2 Speed, Vector2 Acceleration, float Time)
        {
            return Speed + Acceleration * Time;
        }

        public static Vector2 Position(Vector2 Position, Vector2 Speed, Vector2 Acceleration, float Time)
        {
            return Position + (Speed + (Acceleration * Time) / 2) * Time;
        }
    }
}
