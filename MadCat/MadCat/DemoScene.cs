using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NutInput = NutEngine.Input;
using NutEngine;
using NutPacker.Content;
using System.Collections.Generic;

namespace MadCat
{
    public class DemoScene : Scene
    {
        private Texture2D Texture;

        private Character[] characters;
        private List<Wall> walls;

        public DemoScene(Application app) : base(app)
        {
            Texture = Content.Load<Texture2D>("Demo");

            var background = new Sprite(Texture, Graveyard.Tiles.BG) {
                  Position = new Vector2(App.ScreenWidth / 2, App.ScreenHeight / 2)
                , Scale = new Vector2(.5f, .5f)
            };
            World.AddChild(background);

            /// Create Characters.
            characters = new Character[3];

            characters[0] = new Character(Texture);

            characters[1] = new Character(Texture)
            {
                Control = new Character.Controls()
                {
                    RunRightKey = Keys.D
                    ,
                    RunLeftKey = Keys.A
                    ,
                    JumpKey = Keys.W
                    ,
                    ShootKey = Keys.Q
                    ,
                    MeleeKey = Keys.F
                    ,
                    SlideKey = Keys.S
                }
            };

            characters[2] = new Character(Texture)
            {
                Control = new Character.Controls()
                {
                    RunRightKey = Keys.L
                    ,
                    RunLeftKey = Keys.K
                    ,
                    JumpKey = Keys.O
                    ,
                    ShootKey = Keys.J
                    ,
                    MeleeKey = Keys.P
                    ,
                    SlideKey = Keys.M
                }
            };

            World.AddChild(characters[0]);
            World.AddChild(characters[1]);
            World.AddChild(characters[2]);

            /// Some stupid wall test
            int[,] wallMap = 
            { 
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
                { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 },
                { 0, 0, 0, 1, 1, 1, 0, 1, 0, 0 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            };

            walls = new List<Wall>();

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (wallMap[i, j] == 1)
                    {
                        var position = new Vector2()
                        {
                            X = j * Graveyard.Tiles.Tile_2_.Width - Graveyard.Tiles.Tile_2_.Width / 2.0f,
                            Y = i * Graveyard.Tiles.Tile_2_.Height - Graveyard.Tiles.Tile_2_.Height * 2.0f
                        };

                        var wall = new Wall(Texture, position);

                        World.AddChild(wall);
                        walls.Add(wall); 
                    }
                }
            }
        }

        public override void Update(float deltaTime)
        {
            var keyboardState = NutInput.Keyboard.GetState();

            /// We need three loops for correct order of operations

            /// Input
            foreach (var character in characters)
            {
                character.Input(keyboardState);
            }

            /// Update
            foreach (var character in characters)
            {
                character.Update(deltaTime);
            }

            /// Collisions
            foreach (var character in characters)
            {
                foreach (var wall in walls)
                {
                    character.Collide(wall);
                }
            }
        }
    }
}