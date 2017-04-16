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

        public Ground(Vector2 position)
        {
            Sprite = Assets.Ground;

            var size = Sprite.TextureRegion.Frame.Size;
            Body = new RigidBody<AABB>(new AABB(new Vector2(size.X / 2, size.Y / 2))) {
                Position = position,
                Owner = this
            };

            Body.Material.Restitution = .3f;

            Update();
        }

        public void Update()
        {
            Sprite.Position = Body.Position;
        }
    }
}
