using Microsoft.Xna.Framework;
using NutEngine.Physics.Shapes;
using System;

namespace NutEngine.Physics
{
    public class Collision
    {
        public IBody<IShape> A { get; set; }
        public IBody<IShape> B { get; set; }
        public IntersectionArea Intersection { get; set; }

        /// Penetration allowance
        public static float Slop { get; set; } = .05f;
        /// Penetration percentage to correct
        public static float PercentCorrection { get; set; } = .5f;

        public Collision(IBody<IShape> a, IBody<IShape> b, IntersectionArea intersection)
        {
            A = a;
            B = b;
            Intersection = intersection;
        }

        public void PositionAdjustment()
        {
            var correction = (MathHelper.Max(Intersection.Depth - Slop, 0)
                           / (A.Mass.MassInv + B.Mass.MassInv))
                           * Intersection.Normal
                           * PercentCorrection;

            A.Position -= correction * A.Mass.MassInv;
            B.Position += correction * B.Mass.MassInv;
        }

        public void ResolveCollision()
        {
            var relVelocity = B.Velocity - A.Velocity;
            var velocityAlongNormal = Vector2.Dot(relVelocity, Intersection.Normal);

            // Only if objects moving towards each other
            if (velocityAlongNormal > 0) {
                return;
            }

            // https://gamedevelopment.tutsplus.com/tutorials/how-to-create-a-custom-2d-physics-engine-the-basics-and-impulse-resolution--gamedev-6331
            float e = Math.Min(A.Material.Restitution, B.Material.Restitution);

            float j = -(1 + e) * velocityAlongNormal / (A.Mass.MassInv + B.Mass.MassInv);

            A.ApplyImpulse(-j * Intersection.Normal);
            B.ApplyImpulse(j * Intersection.Normal);

            var tangent = relVelocity - (Intersection.Normal * Vector2.Dot(relVelocity, Intersection.Normal));
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
