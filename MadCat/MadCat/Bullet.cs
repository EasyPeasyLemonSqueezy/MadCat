using NutEngine;
using Microsoft.Xna.Framework;

namespace MadCat
{
    class Bullet : Entity
    {
        public Vector2 velocity { get; set; }
        private Vector2 gravitation;
        private Vector2 position;
        public Sprite sprite;

        public Bullet(Vector2 position, float direction, Node node)
        {
            this.position = position;
            velocity = new Vector2(500, 0) * direction;
            gravitation = new Vector2(0, 2000);
            sprite = new Sprite(Assets.TextureBullet);
            sprite.Position = position;
            sprite.Scale = new Vector2(0.05f, 0.05f);

            Collider = new AABB() {
                  X = position.X
                , Y = position.Y
                , Width = 10.0f
                , Height = 10.0f
            };
            node.AddChild(sprite);
        }

        public override void Update(float deltaTime)
        {
            sprite.Position = position = Physics.ApplyVelocity(position, velocity, deltaTime);

            Collider.X = position.X - Collider.Width / 2.0f;
            Collider.Y = position.Y - Collider.Height / 2.0f;
        }

        public override void Cleanup()
        {
            sprite.CommitSuicide();
        }
    }
}