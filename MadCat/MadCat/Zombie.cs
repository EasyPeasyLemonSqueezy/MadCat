using Microsoft.Xna.Framework;
using NutEngine;
using NutEngine.Physics;
using NutEngine.Physics.Shapes;

namespace MadCat
{
    public class Zombie : Entity
    {
        private RigidBody<Circle> body;

        public Zombie(Vector2 position, Hero hero)
        {
            var sprite = Assets.Zombie;
            sprite.Scale = new Vector2(0.2f, 0.2f);

            body = new RigidBody<Circle>(new Circle(20f)) {
                Position = position,
                Owner = this
            };
            body.Mass.Mass = 5;
            body.Material.Restitution = 0.5f;

            AddComponents(
                new BodyComponent(body),
                new ZombieComponent(hero),
                new SpriteComponent(sprite),
                new HealthComponent(5)
            );

            Director.Entities.Add(this);
            Director.Bodies.AddBody(body);
            Director.World.AddChild(sprite);

            body.OnCollision = (collided) => {
                if (collided.Owner is Bullet) {
                    GetComponent<HealthComponent>().Health--;
                }
            };
        }
    }
}
