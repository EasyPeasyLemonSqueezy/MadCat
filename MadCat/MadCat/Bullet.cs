using Microsoft.Xna.Framework;
using NutEngine;
using NutEngine.Physics;
using NutEngine.Physics.Shapes;

namespace MadCat
{
    public class Bullet : Entity
    {
        private RigidBody<Circle> body;

        public Bullet(Vector2 position, Vector2 direction)
        {
            var sprite = Assets.Bullet;
            sprite.Scale = new Vector2(0.03f, 0.03f);

            body = new RigidBody<Circle>(new Circle(5f)) {
                Position = position + direction * 10f,
                Owner = this
            };
            body.Mass.Mass = 1;
            body.Material.Restitution = 1f;

            body.ApplyImpulse(direction * 1250f);

            AddComponents(
                new BodyComponent(body),
                new SpriteComponent(sprite)
            );

            Director.Entities.Add(this);
            Director.Bodies.AddBody(body);
            Director.World.AddChild(sprite);

            body.OnCollision = (collided) => {
                if (!(collided.Owner is Hero)) {
                    Invalid = true;
                }
            };
        }
    }
}
