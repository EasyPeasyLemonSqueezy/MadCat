using Microsoft.Xna.Framework;

namespace MadCat
{
    public static class Physics
    {
        public static Vector2 ApplyAccel(Vector2 velocity, Vector2 accel, float deltaTime)
        {
            return velocity + accel * deltaTime;
        }

        public static Vector2 ApplyVelocity(Vector2 position, Vector2 velocity, float deltaTime)
        {
            return position + velocity * deltaTime;
        }
    }
}
