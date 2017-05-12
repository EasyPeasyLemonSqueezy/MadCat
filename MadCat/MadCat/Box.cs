using Microsoft.Xna.Framework;
using NutEngine;
using NutEngine.Physics;
using NutEngine.Physics.Shapes;

namespace MadCat
{
    public class Box : Entity
    {
        private RigidBody<AABB> body;

        public Box(Vector2 position)
        {
            var sprite = Assets.Box;
            sprite.Scale = new Vector2(0.22f, 0.22f);

            body = new RigidBody<AABB>(new AABB(new Vector2(25f, 25f))) {
                Position = position,
                Owner = this
            };
            body.Mass.Mass = 5;
            body.Material.Restitution = 0.8f;

            AddComponents(
                new FrictionComponent(),
                new BodyComponent(body),
                new SpriteComponent(sprite)
            );

            Director.World.AddChild(sprite);
            Director.Entities.Add(this);
            Director.Bodies.AddBody(body);
        }
    }
}
