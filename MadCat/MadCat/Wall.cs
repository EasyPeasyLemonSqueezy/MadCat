using NutEngine;
using Microsoft.Xna.Framework;

namespace MadCat
{
    public class Wall : GameObject
    {
        private Sprite sprite;

        public Wall(Node node, Vector2 position, Rectangle frame)
        {
            sprite = new Sprite(Assets.Texture, frame) {
                Position = position
            };

            node.AddChild(sprite);

            Collider = new AABB() {
                  X = position.X - frame.Width / 2.0f
                , Y = position.Y - frame.Height / 2.0f
                , Width = frame.Width
                , Height = frame.Height
            };
        }

        public override void Update(float deltaTime)
        {
        }

        public override void Cleanup()
        {
            sprite.CommitSuicide();
        }
    }
}
