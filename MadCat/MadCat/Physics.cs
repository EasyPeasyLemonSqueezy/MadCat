using Microsoft.Xna.Framework;

namespace MadCat
{
    public static class Physics
    {
        public static Vector2 Velocity(Vector2 Velocity, Vector2 Acceleration, float Time)
        {
            return Velocity + Acceleration * Time;
        }

        public static Vector2 Position(Vector2 Position, Vector2 Velocity, Vector2 Acceleration, float Time)
        {
            return Position + (Velocity + (Acceleration * Time) / 2) * Time;
        }
    }
}
