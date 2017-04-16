using Microsoft.Xna.Framework;
using System;

namespace NutEngine.Physics
{
    public static partial class Collider
    {
        // We should use Bodies, because we can't calculate relative velocity in manifold.
        // (Obviously we can, but it will be a bit weird)
        public static void ResolveCollision(Collision collision)
        {
            var relVelocity = collision.B.Velocity - collision.A.Velocity;
            var velocityAlongNormal = Vector2.Dot(relVelocity, collision.Manifold.Normal);

            // Only if objects moving towards each other
            if (velocityAlongNormal > 0) {
                return;
            }

            // https://gamedevelopment.tutsplus.com/tutorials/how-to-create-a-custom-2d-physics-engine-the-basics-and-impulse-resolution--gamedev-6331
            float e = Math.Min(collision.A.Material.Restitution, collision.B.Material.Restitution);

            float j = -(1 + e) * velocityAlongNormal / (collision.A.Mass.MassInv + collision.B.Mass.MassInv);

            collision.A.Impulse += j * collision.Manifold.Normal;
            collision.B.Impulse -= j * collision.Manifold.Normal;
        }
    }
}
