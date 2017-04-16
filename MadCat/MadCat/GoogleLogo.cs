using Microsoft.Xna.Framework;
using NutEngine;
using NutEngine.Physics;
using NutEngine.Physics.Shapes;

namespace MadCat
{
    public class GoogleLogo
    {
        public Sprite Sprite { get; set; }
        public RigidBody<Circle> Body { get; private set; }

        public GoogleLogo(Vector2 position)
        {
            Sprite = Assets.GoogleLogo;
            Sprite.Scale *= .1f;

            var size = Sprite.TextureRegion.Frame.Size.X - 100; // Offset from borders
            Body = new RigidBody<Circle>(new Circle(size * Sprite.Scale.X / 2)) {
                Position = position,
                Owner = this,
                Acceleration = new Vector2(0, 1000)
            };

            Body.Mass.MassInv = 1;
            Body.Material.Restitution = .5f;

            Update();
        }

        public void Update()
        {
            Sprite.Position = Body.Position;
        }
    }
}
