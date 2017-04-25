using Microsoft.Xna.Framework;
using NutEngine;
using NutEngine.Physics;
using NutEngine.Physics.Shapes;

namespace MadCat
{
    public class CatInTheJar
    {
        public Sprite Sprite { get; set; }
        public RigidBody<AABB> Body { get; private set; }

        public CatInTheJar(Vector2 position)
        {
            Sprite = Assets.Cat;
            Sprite.Scale *= 0.15f;

            var size = new Vector2(Sprite.TextureRegion.Frame.Size.X, Sprite.TextureRegion.Frame.Size.Y);
            Body = new RigidBody<AABB>(new AABB(size * Sprite.Scale.X / 2)) {
                Position = position,
                Owner = this
            };

            Body.Material.Restitution = .5f;
            Body.Material.StaticFriction = .1f;
            Body.Material.DynamicFriction = .1f;

            Update();
        }

        public void Update()
        {
            Sprite.Position = Body.Position;
        }
    }
}
