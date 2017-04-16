using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NutEngine;
using NutPacker.Content;

namespace MadCat
{
    public static class Assets
    {
        public static Texture2D Texture { get; private set; }
        public static Sprite Ground { get; private set; }
        public static Sprite Bones { get; private set; }

        public static void Init(ContentManager Content)
        {
            Texture = Content.Load<Texture2D>("Demo");

            Ground = new Sprite(Texture, Graveyard.Tiles.Tile_15_);
            Bones = new Sprite(Texture, Graveyard.Tiles.Bones_2_);
        }
    }
}