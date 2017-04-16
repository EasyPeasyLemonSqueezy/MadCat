﻿using Microsoft.Xna.Framework;
using NutEngine.Physics.Shapes;

namespace NutEngine.Physics
{
    public class RigidBody<ShapeType> : IBody<ShapeType>
        where ShapeType : Shape
    {
        public ShapeType Shape { get; private set; }
        public MassData Mass { get; private set; }
        public Material Material { get; private set; }
        public object Owner { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Force { get; set; }

        public RigidBody(ShapeType shape)
        {
            Shape = shape;
            Mass = new MassData();
            Material = new Material();
        }

        public void ApplyImpulse(Vector2 impulse)
        {
            Velocity += impulse * Mass.MassInv;
        }

        public void IntegrateVelocity(float dt)
        {
            Position += Velocity * dt;
        }

        public void ApplyForce(Vector2 force)
        {
            Force += force;
        }

        public void IntegrateForces(float dt)
        {
            Velocity += (Force * Mass.MassInv) * dt / 2f;
        }
    }
}