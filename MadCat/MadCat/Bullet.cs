using NutEngine;
using Microsoft.Xna.Framework;

namespace MadCat
{
    public class Bullet : Entity
    {
        public Bullet(Vector2 position, float direction, Node node)
        {
            var sprite = new Sprite(Assets.TextureBullet);
            sprite.Position = position;
            sprite.Scale = new Vector2(0.05f, 0.05f);
            node.AddChild(sprite);

            Collider = new AABB {
                X = position.X,
                Y = position.Y,
                Width = 10.0f,
                Height = 10.0f
            };

            AddComponent(new PositionComponent(position));
            AddComponent(new VelocityComponent(new Vector2(500, 0) * direction));
            AddComponent(new SpriteComponent(new Sprite(Assets.TextureBullet)));
            AddComponent(new ColliderComponent(Collider));
        }

        public override void Cleanup()
        {
            var sprite = GetComponent<SpriteComponent>();
            sprite.Sprite.CommitSuicide();
        }
    }
}