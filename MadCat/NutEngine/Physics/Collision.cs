﻿using Microsoft.Xna.Framework;
using NutEngine.Physics.Shapes;

namespace NutEngine.Physics
{
    public class Collision
    {
        public IBody<Shape> A { get; set; }
        public IBody<Shape> B { get; set; }
        public Manifold Manifold { get; set; }

        public Collision(IBody<Shape> a, IBody<Shape> b, Manifold manifold)
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
    }
}
