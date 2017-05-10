using Microsoft.Xna.Framework;
using NutEngine;
using NutEngine.Physics;
using NutPacker.Content;

namespace MadCat
{
    public class Map
    {
        private int[,] wallMap = {
                  { 0,  0, 0, 0, 0,  0, 0, 0, 0,  0, 0,  0,  0, 0, 0, 0,  0, 0,  0,  0,  0, 0, 0,  0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0,  0, 0,  0,  0, 0,  0, 0,  0,  0, 0, 0,  0, 0, 0,  0,  0, 0, 0, 0}
                , { 0,  0, 0, 0, 0,  0, 0, 0, 0,  0, 0,  0,  0, 0, 0, 0,  0, 0,  0,  0,  0, 0, 0,  0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0,  0, 0,  0,  0, 0,  0, 0,  0,  0, 0, 0,  0, 0, 0,  0,  0, 0, 0, 0}
                , { 0,  0, 0, 0, 0,  0, 0, 0, 0,  0, 0, 14, 16, 0, 0, 0,  0, 0,  0,  0,  0, 0, 0,  0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0,  0, 0,  0,  0, 0,  0, 0,  0,  0, 0, 0,  0, 0, 0,  0,  0, 0, 0, 0}
                , { 0,  0, 0, 0, 0,  0, 0, 0, 0, 15, 0,  0,  0, 0, 0, 0,  0, 0, 14, 15, 16, 0, 0, 15, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0,  0, 0, 14, 16, 0,  0, 0, 14, 16, 0, 0,  0, 0, 1,  0,  0, 0, 0, 1}
                , { 3,  0, 0, 0, 0,  3, 0, 0, 0,  0, 0,  0,  0, 0, 0, 2,  0, 0,  0,  0,  0, 0, 0,  0, 0, 0, 0, 0, 1,  3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 15, 0,  0,  0, 0, 15, 0,  0,  0, 0, 0,  3, 0, 4,  3,  0, 0, 0, 4}
                , { 6,  0, 0, 1, 2,  6, 0, 2, 0,  0, 0,  0,  0, 0, 1, 5,  3, 0,  0,  0,  0, 0, 2,  0, 0, 0, 0, 1, 8,  6, 0, 2, 0, 0, 0, 0, 0, 0, 1,  3, 0,  0, 0,  0,  0, 0,  0, 0,  0,  0, 0, 1,  6, 7, 4, 10,  3, 0, 0, 4}
                , { 10, 2, 2, 8, 5, 10, 2, 5, 2,  2, 2,  2,  2, 2, 8, 5, 10, 2,  2,  2,  2, 2, 5,  2, 2, 2, 2, 8, 5, 10, 2, 5, 2, 2, 2, 2, 2, 2, 8, 10, 2,  2, 2,  2,  2, 2,  2, 2,  2,  2, 2, 8, 10, 2, 8,  5, 10, 2, 2, 8}
            };

        public Map(Node world, EntityManager manager, BodiesManager bodies)
        {
            for (int i = 0; i < 7; i++) {
                for (int j = 0; j < 60; j++) {
                    if (wallMap[i, j] != 0) {
                        var frame = Graveyard.Tiles.Tile_2_;

                        if (wallMap[i, j] == 1)
                            frame = Graveyard.Tiles.Tile_1_;
                        if (wallMap[i, j] == 2)
                            frame = Graveyard.Tiles.Tile_2_;
                        if (wallMap[i, j] == 3)
                            frame = Graveyard.Tiles.Tile_3_;
                        if (wallMap[i, j] == 4)
                            frame = Graveyard.Tiles.Tile_4_;
                        if (wallMap[i, j] == 5)
                            frame = Graveyard.Tiles.Tile_5_;
                        if (wallMap[i, j] == 6)
                            frame = Graveyard.Tiles.Tile_6_;
                        if (wallMap[i, j] == 7)
                            frame = Graveyard.Tiles.Bones_3_;
                        if (wallMap[i, j] == 8)
                            frame = Graveyard.Tiles.Tile_8_;
                        if (wallMap[i, j] == 9)
                            frame = Graveyard.Tiles.Tile_9_;
                        if (wallMap[i, j] == 10)
                            frame = Graveyard.Tiles.Tile_10_;
                        if (wallMap[i, j] == 14)
                            frame = Graveyard.Tiles.Tile_14_;
                        if (wallMap[i, j] == 15)
                            frame = Graveyard.Tiles.Tile_15_;
                        if (wallMap[i, j] == 16)
                            frame = Graveyard.Tiles.Tile_16_;

                        var position = new Vector2() {
                            X = j * frame.Width - frame.Width / 2.0f,
                            Y = i * frame.Height - frame.Height * 2.0f
                        };

                        var wall = new Wall(world, position, frame, manager);
                        bodies.AddBody(wall.Body);
                    }
                }
            }
        }
    }
}
