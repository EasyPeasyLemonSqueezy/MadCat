using Microsoft.Xna.Framework;
using NutEngine.Physics.Shapes;
using System;

namespace NutEngine.Physics
{
    public class Collision
    {
        public IBody<IShape> A { get; set; }
        public IBody<IShape> B { get; set; }
        public Manifold Manifold { get; set; }

        public Collision(IBody<IShape> a, IBody<IShape> b, Manifold manifold)
        {
            A = a;
            B = b;
            Manifold = manifold;
        }

        public void PositionAdjustment()
        {
            const float kSlop = .05f; // Penetration allowance
            const float percent = .5f; // Penetration percentage to correct

            var correction = (MathHelper.Max(Manifold.Depth - kSlop, 0)
                           / (A.Mass.MassInv + B.Mass.MassInv))
                           * Manifold.Normal
                           * percent;

            A.Position -= correction * A.Mass.MassInv;
            B.Position += correction * B.Mass.MassInv;
        }

        public void ResolveCollision()
        {
            var relVelocity = B.Velocity - A.Velocity;
            var velocityAlongNormal = Vector2.Dot(relVelocity, Manifold.Normal);

            // Only if objects moving towards each other
            if (velocityAlongNormal > 0) {
                return;
            }

            // https://gamedevelopment.tutsplus.com/tutorials/how-to-create-a-custom-2d-physics-engine-the-basics-and-impulse-resolution--gamedev-6331
            float e = Math.Min(A.Material.Restitution, B.Material.Restitution);

            float j = -(1 + e) * velocityAlongNormal / (A.Mass.MassInv + B.Mass.MassInv);

            A.ApplyImpulse(-j * Manifold.Normal);
            B.ApplyImpulse(j * Manifold.Normal);

            var tangent = relVelocity - (Manifold.Normal * Vector2.Dot(relVelocity, Manifold.Normal));
            tangent.Normalize();

            float jt = -Vector2.Dot(relVelocity, tangent) / (A.Mass.MassInv + B.Mass.MassInv);

            if (Single.IsNaN(jt)) {
                return;
            }

            var staticFriction = (float)Math.Sqrt(A.Material.StaticFriction * B.Material.StaticFriction);
            var dynamicFriction = (float)Math.Sqrt(A.Material.DynamicFriction * B.Material.DynamicFriction);

            var tangentImpulse = Math.Abs(jt) < j * staticFriction ? tangent * jt : tangent * (-j) * dynamicFriction;

            A.ApplyImpulse(-tangentImpulse);
            B.ApplyImpulse(tangentImpulse);
        }

        public void OnCollision()
        {
            A.OnCollision?.Invoke(B);
            B.OnCollision?.Invoke(A);
        }
    }
}
