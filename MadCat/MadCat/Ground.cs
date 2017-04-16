using Microsoft.Xna.Framework;
using NutEngine;
using NutEngine.Physics;
using NutEngine.Physics.Shapes;

namespace MadCat
{
    public class Ground
    {
        public Sprite Sprite { get; set; }
        public RigidBody<Circle> Body { get; private set; }

        public Ground()
        {
            Sprite = Assets.Ground;

            var size = Sprite.TextureRegion.Frame.Size;
            Body = new RigidBody<Circle>(new Circle(size.X / 2)) {
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
