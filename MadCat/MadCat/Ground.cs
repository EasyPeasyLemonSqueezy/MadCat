using Microsoft.Xna.Framework;
using NutEngine;
using NutEngine.Physics;

namespace MadCat
{
    public class Ground
    {
        public Sprite Sprite { get; private set; }
        public Body Body { get; private set; }

        public Ground()
        {
            Sprite = Assets.Ground;

            Body = new Body() {
                Owner = this,
                Position = new Vector2(200, 200),
            };

            Update();
        }

        public void Update()
        {
            Sprite.Position = Body.Position;
        }
    }
}
