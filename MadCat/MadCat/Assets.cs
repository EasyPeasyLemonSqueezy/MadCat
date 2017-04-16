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

        public static void Init(ContentManager Content)
        {
            Texture = Content.Load<Texture2D>("Demo");
        }
    }
}