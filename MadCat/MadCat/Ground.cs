using Microsoft.Xna.Framework;
using NutEngine;
using NutEngine.Physics;
using NutEngine.Physics.Shapes;

namespace MadCat
{
    public class Ground
    {
        public Sprite Sprite { get; set; }
        public RigidBody<AABB> Body { get; private set; }

        public Ground()
        {
            Sprite = Assets.Ground;

            var size = Sprite.TextureRegion.Frame.Size;
            Body = new RigidBody<AABB>(new AABB(new Vector2(size.X, size.Y) / 2)) {
                Position = new Vector2(300, 300),
                Owner = this,
            };

            Body.Material.Restitution = .8f;

            Update();
        }

        public void Update()
        {
            Sprite.Position = Body.Position;
        }
    }
}
