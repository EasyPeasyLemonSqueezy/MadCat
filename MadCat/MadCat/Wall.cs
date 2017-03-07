using NutEngine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using NutPacker.Content;

namespace MadCat
{
    public class Wall : Sprite
    {
        public AABB Bounds { get; }

        public Wall(Texture2D texture, Vector2 position)
            : base(texture, Graveyard.Tiles.Tile_2_)
        {
            Position = position;

            Bounds = new AABB() {
                  X = position.X - Graveyard.Tiles.Tile_2_.Width  / 2.0f
                , Y = position.Y - Graveyard.Tiles.Tile_2_.Height / 2.0f
                , Width  = Graveyard.Tiles.Tile_2_.Width
                , Height = Graveyard.Tiles.Tile_2_.Height
            };
        }
    }
}
