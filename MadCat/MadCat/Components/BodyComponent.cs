using NutEngine;
using NutEngine.Physics;
using NutEngine.Physics.Shapes;
using System;

namespace MadCat
{
    public class BodyComponent : Component, IDisposable
    {
        public BodiesManager Bodies { get; set; }
        public IBody<IShape> Body { get; set; }

        public BodyComponent(IBody<IShape> body, BodiesManager bodies)
        {
            Body = body;
        }

        public override void Update(float deltaTime)
        {
        }

        public void Dispose()
        {
            Bodies.KillSome(Body);
        }
    }
}
