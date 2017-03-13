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
        private List<GameObject> entities;

        private CollisionDetector detector;

        public DemoScene(Application app) : base(app)
        {
            Texture = Content.Load<Texture2D>("Demo");

            var background = new Sprite(Texture, Graveyard.Tiles.BG) {
                  Position = new Vector2(App.ScreenWidth / 2, App.ScreenHeight / 2)
                , Scale = new Vector2(.5f, .5f)
            };
            World.AddChild(background);

            entities = new List<GameObject>();
            detector = new CollisionDetector();

            /// Create Characters.
            characters = new Character[3];

            characters[0] = new Character(Texture, World);

            characters[1] = new Character(Texture, World) {
                Control = new Character.Controls() {
                      RunRightKey = Keys.D
                    , RunLeftKey = Keys.A
                    , JumpKey = Keys.W
                    , ShootKey = Keys.Q
                    , MeleeKey = Keys.F
                    , SlideKey = Keys.S
                }
            };

            characters[2] = new Character(Texture, World) {
                Control = new Character.Controls() {
                      RunRightKey = Keys.L
                    , RunLeftKey = Keys.K
                    , JumpKey = Keys.O
                    , ShootKey = Keys.J
                    , MeleeKey = Keys.P
                    , SlideKey = Keys.M
                }
            };

            entities.Add(characters[0]);
            entities.Add(characters[1]);
            entities.Add(characters[2]);

            /// Some stupid wall test
            int[,] wallMap = { 
                  { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
                , { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
                , { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
                , { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }
                , { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 }
                , { 0, 0, 0, 1, 1, 1, 0, 1, 0, 0 }
                , { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
            };

            for (int i = 0; i < 7; i++) {
                for (int j = 0; j < 10; j++) {
                    if (wallMap[i, j] == 1) {
                        var position = new Vector2() {
                            X = j * Graveyard.Tiles.Tile_2_.Width- Graveyard.Tiles.Tile_2_.Width / 2.0f,
                            Y = i * Graveyard.Tiles.Tile_2_.Height - Graveyard.Tiles.Tile_2_.Height * 2.0f
                        };

                        var wall = new Wall(Texture, World, position);
                        entities.Add(wall);
                    }
                }
            }
        }

        public override void Update(float deltaTime)
        {
            var keyboardState = NutInput.Keyboard.GetState();
            
            /// Input
            foreach (var character in characters) {
                character.Input(keyboardState);
            }

            /// Update
            foreach (var entity in entities) {
                entity.Update(deltaTime);
            }

            detector.CheckCollisions(entities); /// Collisions

            if (keyboardState.IsKeyDown(Keys.NumPad9)) {
                Camera.Rotation += deltaTime;
            }
            if (keyboardState.IsKeyDown(Keys.NumPad7)) {
                Camera.Rotation -= deltaTime;
            }
            if (keyboardState.IsKeyDown(Keys.NumPad8)) {
                Camera.Position -= new Vector2(0, 100 * deltaTime);
            }
            if (keyboardState.IsKeyDown(Keys.NumPad2)) {
                Camera.Position += new Vector2(0, 100 * deltaTime);
            }
            if (keyboardState.IsKeyDown(Keys.NumPad3)) {
                Camera.Zoom *= 80 * deltaTime;
            }
            if (keyboardState.IsKeyDown(Keys.NumPad1)) {
                Camera.Zoom /= 80 * deltaTime;
            }
        }
    }
}