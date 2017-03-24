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
        private Map map;
        private GameObjectManager manager;

        public DemoScene(Application app) : base(app)
        {
            Assets.Init(Content);

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

            map = new Map(World, manager);

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

            background.Position = new Vector2(characters[0].Position.X, App.ScreenHeight / 2);
            Camera.Position = new Vector2(characters[0].Position.X - App.ScreenWidth / 2, -20);

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