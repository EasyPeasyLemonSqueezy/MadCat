using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NutInput = NutEngine.Input;
using NutEngine;
using NutPacker.Content;

namespace MadCat
{
    public class DemoScene : Scene
    {
        private Sprite background;

        private Character[] characters;
        private GameObjectManager manager;

        public DemoScene(Application app) : base(app)
        {
            background = new Sprite(Assets.Texture, Graveyard.Tiles.BG) {
                  Position = new Vector2(App.ScreenWidth / 2, App.ScreenHeight / 2)
                , Scale = new Vector2(.5f, .5f)
            };
            World.AddChild(background);

            manager = new GameObjectManager();

            /// Create Characters.
            characters = new Character[3];

            characters[0] = new Character(World, manager);

            characters[1] = new Character(World, manager) {
                Control = new Character.Controls() {
                      RunRightKey = Keys.D
                    , RunLeftKey = Keys.A
                    , JumpKey = Keys.W
                    , ShootKey = Keys.Q
                    , MeleeKey = Keys.F
                    , SlideKey = Keys.S
                }
            };

            characters[2] = new Character(World, manager) {
                Control = new Character.Controls() {
                      RunRightKey = Keys.L
                    , RunLeftKey = Keys.K
                    , JumpKey = Keys.O
                    , ShootKey = Keys.J
                    , MeleeKey = Keys.P
                    , SlideKey = Keys.M
                }
            };

            manager.Add(characters[0]);
            manager.Add(characters[1]);
            manager.Add(characters[2]);

            /// Some stupid wall test
            int[,] wallMap = {
                  { 0,  0, 0, 0, 0,  0, 0, 0, 0,  0, 0,  0,  0, 0, 0, 0,  0, 0,  0,  0,  0, 0, 0,  0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0,  0, 0,  0,  0, 0,  0, 0,  0,  0, 0, 0,  0, 0, 0,  0,  0, 0, 0, 0}
                , { 0,  0, 0, 0, 0,  0, 0, 0, 0,  0, 0,  0,  0, 0, 0, 0,  0, 0,  0,  0,  0, 0, 0,  0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0,  0, 0,  0,  0, 0,  0, 0,  0,  0, 0, 0,  0, 0, 0,  0,  0, 0, 0, 0}
                , { 0,  0, 0, 0, 0,  0, 0, 0, 0,  0, 0, 14, 16, 0, 0, 0,  0, 0,  0,  0,  0, 0, 0,  0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0,  0, 0,  0,  0, 0,  0, 0,  0,  0, 0, 0,  0, 0, 0,  0,  0, 0, 0, 0}
                , { 0,  0, 0, 0, 0,  0, 0, 0, 0, 15, 0,  0,  0, 0, 0, 0,  0, 0, 14, 15, 16, 0, 0, 15, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0,  0, 0, 14, 16, 0,  0, 0, 14, 16, 0, 0,  0, 0, 1,  0,  0, 0, 0, 1}
                , { 3,  0, 0, 0, 0,  3, 0, 0, 0,  0, 0,  0,  0, 0, 0, 2,  0, 0,  0,  0,  0, 0, 0,  0, 0, 0, 0, 0, 1,  3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 15, 0,  0,  0, 0, 15, 0,  0,  0, 0, 0,  3, 0, 4,  3,  0, 0, 0, 4}
                , { 6,  0, 0, 1, 2,  6, 0, 2, 0,  0, 0,  0,  0, 0, 1, 5,  3, 0,  0,  0,  0, 0, 2,  0, 0, 0, 0, 1, 8,  6, 0, 2, 0, 0, 0, 0, 0, 0, 1,  3, 0,  0, 0,  0,  0, 0,  0, 0,  0,  0, 0, 1,  6, 7, 4, 10,  3, 0, 0, 4}
                , { 10, 2, 2, 8, 5, 10, 2, 5, 2,  2, 2,  2,  2, 2, 8, 5, 10, 2,  2,  2,  2, 2, 5,  2, 2, 2, 2, 8, 5, 10, 2, 5, 2, 2, 2, 2, 2, 2, 8, 10, 2,  2, 2,  2,  2, 2,  2, 2,  2,  2, 2, 8, 10, 2, 8,  5, 10, 2, 2, 8}
            };

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

                        var wall = new Wall(World, position, frame);
                        manager.Add(wall);
                    }
                }
            }

            manager.Detector.AddTypeRule<Character, Wall>(
                (first, second) => {
                    var character = first as Character;
                    var wall = second as Wall;
                    character.CollideWall(wall);
                });

            manager.Detector.AddTypeRule<Character, Character>(
                (first, second) => {
                    var c = first as Character;
                    c.SetColor(Color.Red);
                });

            manager.Detector.AddTypeRule<Bullet, Wall>(
                (first, second) => {
                    var bullet = first as Bullet;
                    var wall = second as Wall;                  
                    
                    bullet.Invalid = true;
                });

            manager.Detector.AddTypeRule<Bullet, Character>(
                (first, second) => {
                    var c = second as Character;
                    c.SetColor(Color.Aqua);
                });
        }

        public override void Update(float deltaTime)
        {
            var keyboardState = NutInput.Keyboard.GetState();
            
            /// Input
            foreach (var character in characters) {
                character.Input(keyboardState);
            }

            manager.Update(deltaTime);

            background.Position = new Vector2(characters[0].position.X, App.ScreenHeight / 2);
            Camera.Position = new Vector2(characters[0].position.X - App.ScreenWidth / 2, -20);

            /*
                        /// Camera test
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
            */
        }
    }
}