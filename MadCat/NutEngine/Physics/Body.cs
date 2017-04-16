﻿using Microsoft.Xna.Framework;
using NutEngine.Physics.Shapes;

namespace NutEngine.Physics
{
    public class Body
    {
        public Shape Shape { get; private set; }
        public MassData Mass { get; private set; }
        public Material Material { get; private set; }
        public object Owner { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Force { get; private set; }

        public Body(Shape shape)
        {
            Shape = shape;
            Mass = new MassData();
            Material = new Material();
        }

        public void ApplyImpulse(Vector2 impulse)
        {
            Velocity += impulse * Mass.MassInv;
        }

        public void ApplyForce(Vector2 force)
        {
            Force += force;
        }
    }
}
