using Microsoft.Xna.Framework;
using NutEngine;
using NutEngine.Physics;
using NutEngine.Physics.Shapes;

namespace MadCat
{
    public class ChromeLogo
    {
        public Sprite Sprite { get; set; }
        public RigidBody<Circle> Body { get; private set; }

        public ChromeLogo(Vector2 position)
        {
            Sprite = Assets.ChromeLogo;
            Sprite.Scale *= .2f;

            var size = Sprite.TextureRegion.Frame.Size;
            Body = new RigidBody<Circle>(new Circle(size.X * Sprite.Scale.X / 2)) {
                Position = position,
                Owner = this
            };

            Body.Material.Restitution = .3f;
            Body.Material.StaticFriction = .2f;
            Body.Material.DynamicFriction = .2f;

            Update();
        }

        public void Update()
        {
            Sprite.Position = Body.Position;
        }
    }
}
