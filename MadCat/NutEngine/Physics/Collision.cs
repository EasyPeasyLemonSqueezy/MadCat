using Microsoft.Xna.Framework;

namespace NutEngine.Physics
{
    public class Collision
    {
        public Body A { get; set; }
        public Body B { get; set; }
        public Manifold Manifold { get; set; }

        public Collision(Body a, Body b, Manifold manifold)
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

            // Here we also should correct position of shapes(NOT BODIES)
            A.Position -= correction * A.Mass.MassInv;
            B.Position += correction * B.Mass.MassInv;
        }
    }
}
