using NutEngine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using NutPacker.Content;
using System;

namespace MadCat
{
    public class Wall : GameObject
    {
        private Sprite sprite;

        public Wall(Texture2D texture, Node node, Vector2 position)
        {
            sprite = new Sprite(texture, Graveyard.Tiles.Tile_2_);
            sprite.Position = position;

            node.AddChild(sprite);

            Collider = new AABB() {
                  X = position.X - Graveyard.Tiles.Tile_2_.Width  / 2.0f
                , Y = position.Y - Graveyard.Tiles.Tile_2_.Height / 2.0f
                , Width  = Graveyard.Tiles.Tile_2_.Width
                , Height = Graveyard.Tiles.Tile_2_.Height
            };
        }

        public override void Update(float deltaTime)
        {
        }
    }
}
