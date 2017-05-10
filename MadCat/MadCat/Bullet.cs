using NutEngine;
using Microsoft.Xna.Framework;
using NutEngine.Physics;
using NutEngine.Physics.Shapes;

namespace MadCat
{
    public class Bullet : Entity
    {
        public RigidBody<AABB> Body { get; private set; }

        public Bullet(Vector2 position, float direction, Node node, EntityManager manager)
        {
            var sprite = new Sprite(Assets.TextureBullet) {
                Position = position,
                Scale = new Vector2(0.05f, 0.05f)
            };
            node.AddChild(sprite);

            Body = new RigidBody<AABB>(new AABB(new Vector2(10f / 2f, 10f / 2f))) {
                Position = position,
                Owner = this,
                Acceleration = new Vector2(0f, 2000f)
            };

            Body.Mass.MassInv = 5;
            Body.Material.Restitution = .5f;

            Body.ApplyImpulse(new Vector2(500, 0) * direction);

            manager.Add(this);

            AddComponents(
                new ColliderComponent(Body),
                new SpriteComponent(sprite)
            );
        }
    }
}