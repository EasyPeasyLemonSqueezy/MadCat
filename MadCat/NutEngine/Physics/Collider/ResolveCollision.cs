using Microsoft.Xna.Framework;
using NutEngine.Physics.Shapes;
using System;

namespace NutEngine.Physics
{
    public static partial class Collider
    {
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

            collision.A.ApplyImpulse(-j * collision.Manifold.Normal);
            collision.B.ApplyImpulse(j * collision.Manifold.Normal);

            var tangent = relVelocity - (collision.Manifold.Normal * Vector2.Dot(relVelocity, collision.Manifold.Normal));
            tangent.Normalize();

            float jt = -Vector2.Dot(relVelocity, tangent) / (collision.A.Mass.MassInv + collision.B.Mass.MassInv);

            var staticFriction = (float)Math.Sqrt(collision.A.Material.StaticFriction * collision.B.Material.StaticFriction);
            var dynamicFriction = (float)Math.Sqrt(collision.A.Material.DynamicFriction * collision.B.Material.DynamicFriction);

            var tangentImpulse = Math.Abs(jt) < j * staticFriction ? tangent * jt : tangent * (-j) * dynamicFriction;

            collision.A.ApplyImpulse(-tangentImpulse);
            collision.B.ApplyImpulse(tangentImpulse);
        }
    }
}
