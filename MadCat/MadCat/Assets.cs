using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NutEngine;
using NutPacker.Content;

namespace MadCat
{
    public static class Assets
    {
        public static Texture2D Texture { get; private set; }
        public static Sprite Ground {
            get => new Sprite(Texture, Graveyard.Tiles.Tile_15_);
        }
        public static Sprite Skull {
            get => new Sprite(Texture, Graveyard.Tiles.Bones_2_);
        }

        public static Sprite ChromeLogo {
            get => new Sprite(Texture, Logo._61004);
        }
        public static Sprite GoogleLogo {
            get => new Sprite(Texture, Logo.Google);
        }
        public static Sprite Mozilla {
            get => new Sprite(Texture, Logo.Mozilla_Mascot);
        }
        public static Sprite Cat {
            get => new Sprite(Texture, Logo.Cat);
        }

        public static void Init(ContentManager Content)
        {
            Texture = Content.Load<Texture2D>("Demo");
        }
    }
}