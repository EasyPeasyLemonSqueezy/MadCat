using Microsoft.Xna.Framework;
using NutEngine;
using NutEngine.Physics;
using NutEngine.Physics.Shapes;

namespace MadCat
{
    public class Wall : Entity
    {
        private RigidBody<AABB> body;

        public Wall(Vector2 position, Vector2 size)
        {
            body = new RigidBody<AABB>(new AABB(size / 2f)) {
                Position = position + size / 2f,
                Owner = this
            };
            body.Mass.Mass = 0;
            body.Material.Restitution = 1f;

            AddComponents(
                new BodyComponent(body)
            );

            Director.Entities.Add(this);
            Director.Bodies.AddBody(body);
        }
    }
}
