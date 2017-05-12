using Microsoft.Xna.Framework;
using NutEngine;
using NutEngine.Physics;
using NutEngine.Physics.Shapes;

namespace MadCat
{
    public class Hero : Entity
    {
        private RigidBody<Circle> body;

        public Hero(Vector2 position, Node world, EntityManager entities, BodiesManager bodies)
        {
            var sprite = Assets.Hero;
            sprite.Scale = new Vector2(0.2f, 0.2f);

            body = new RigidBody<Circle>(new Circle(20f)) {
                Position = position,
                Owner = this
            };
            body.Mass.Mass = 5;
            body.Material.Restitution = 0.5f;

            AddComponents(
                new InputComponent(),
                new BodyComponent(body, bodies),
                new SpriteComponent(sprite),
                new AimComponent(world)
            );

            entities.Add(this);
            bodies.AddBody(body);
            world.AddChild(sprite);
        }
    }
}
