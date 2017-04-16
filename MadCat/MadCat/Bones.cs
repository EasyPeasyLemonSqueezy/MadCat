using Microsoft.Xna.Framework;
using NutEngine;
using NutEngine.Physics;

namespace MadCat
{
    public class Bones
    {
        public Sprite Sprite { get; private set; }
        public Body Body { get; private set; }

        public Bones()
        {
            Sprite = Assets.Bones;

            Body = new Body() {
                Owner = this,
                Position = new Vector2(200, 100),
            };

            Update();
        }

        public void Update()
        {
            Sprite.Position = Body.Position;
        }
    }
}
