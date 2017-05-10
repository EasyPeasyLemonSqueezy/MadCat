using NutEngine;
using Microsoft.Xna.Framework;
using NutEngine.Physics;
using NutEngine.Physics.Shapes;

namespace MadCat
{
    public class Wall : Entity
    {
        public RigidBody<AABB> Body { get; private set; }

        public Wall(Node node, Vector2 position, Rectangle frame, EntityManager manager)
        {
            var sprite = new Sprite(Assets.Texture, frame) {
                Position = position
            };
            node.AddChild(sprite);

            Body = new RigidBody<AABB>(new AABB(new Vector2(frame.Width / 2.0f, frame.Height / 2.0f))) {
                Position = position,
                Owner = this,
            };

            manager.Add(this);
            AddComponents(
                new PositionComponent(position),
                new ColliderComponent(Body),
                new SpriteComponent(sprite)
            );
        }
    }
}
