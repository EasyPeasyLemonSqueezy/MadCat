using Microsoft.Xna.Framework;
using System;

namespace NutEngine.Physics
{
    public static partial class Collider
    {
        // We should use Bodies, because we can't calculate relative velocity in manifold.
        // (Obviously we can, but it will be a bit weird)
        public static void ResolveCollision(Body a, Body b, Manifold manifold)
        {
            var relVelocity = b.Velocity - a.Velocity;
            var velocityAlongNormal = Vector2.Dot(relVelocity, manifold.Normal);

            // Wat?
            if (velocityAlongNormal > 0) {
                return;
            }

            float e = Math.Min(a.Material.Restitution, b.Material.Restitution);

            // I'm done.
        }
    }
}
