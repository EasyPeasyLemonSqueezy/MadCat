using NutEngine;
using Microsoft.Xna.Framework;

namespace MadCat
{
    public class Bullet : Entity
    {
        public Bullet(Vector2 position, float direction, Node node)
        {
            var sprite = new Sprite(Assets.TextureBullet) {
                Position = position,
                Scale = new Vector2(0.05f, 0.05f)
            };
            node.AddChild(sprite);

            Collider = new AABB {
                X = position.X,
                Y = position.Y,
                Width = 10.0f,
                Height = 10.0f
            };

            AddComponents(
                new PositionComponent(position),
                new VelocityComponent(new Vector2(500, 0) * direction),
                new SpriteComponent(sprite),
                new ColliderComponent(Collider)
            );
        }
    }
}