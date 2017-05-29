using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NutEngine;
using NutPacker.Content;

namespace MadCat
{
    public static class Assets
    {
        public static Texture2D Texture;
        public static SpriteFont Font;

        public static Sprite Hero {
            get => new Sprite(Texture, Sprites.Hero);
        }
        public static Sprite Background {
            get => new Sprite(Texture, Sprites.Background);
        }
        public static Sprite Bullet {
            get => new Sprite(Texture, Sprites.Bullet);
        }
        public static Sprite Box {
            get => new Sprite(Texture, Sprites.Box);
        }
        public static Sprite Aim {
            get => new Sprite(Texture, Sprites.Aim);
        }
        public static Sprite Zombie {
            get => new Sprite(Texture, Sprites.Zombie);
        }
        public static Sprite Blood1 {
            get => new Sprite(Texture, Sprites.Blood1);
        }
        public static Sprite Blood2 {
            get => new Sprite(Texture, Sprites.Blood2);
        }
        public static Sprite Blood3 {
            get => new Sprite(Texture, Sprites.Blood3);
        }

        public static void Init(ContentManager Content)
        {
            Texture = Content.Load<Texture2D>("Demo");
            Font = Content.Load<SpriteFont>("myFont");
        }
    }
}
