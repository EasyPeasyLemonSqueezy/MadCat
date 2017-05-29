using Microsoft.Xna.Framework;
using NutEngine;
using System;

namespace MadCat
{
    public class Blood : Entity
    {
        public Blood(Vector2 position)
        {
            Sprite sprite = null;

            var random = new Random();
            int blood = random.Next(1, 3);

            switch (blood) {
                case 1:
                    sprite = Assets.Blood1;
                    break;
                case 2:
                    sprite = Assets.Blood2;
                    break;
                case 3:
                    sprite = Assets.Blood3;
                    break;
            }

            sprite.Position = position;
            sprite.Scale = new Vector2(0.2f, 0.2f);
            sprite.ZOrder = -10;

            AddComponent(new SpriteComponent(sprite));

            Director.Entities.Add(this);
            Director.World.AddChild(sprite);
        }
    }
}
