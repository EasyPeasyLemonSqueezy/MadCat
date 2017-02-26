using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NutInput = NutEngine.Input;
using NutEngine;
using NutPacker.Content;

namespace MadCat
{
    public class DemoScene : Scene
    {
        private float ScreenHeight;
        private float ScreenWidth;

        private Texture2D Texture;
        private AdventureGirl AdventureGirl;
        private Node Ground;

        public DemoScene(Application app) : base(app)
        {
            ScreenHeight = app.GraphicsDevice.PresentationParameters.BackBufferHeight;
            ScreenWidth = app.GraphicsDevice.PresentationParameters.BackBufferWidth;

            Texture = Content.Load<Texture2D>("Demo");

            var background = new Sprite(Texture, Graveyard.Tiles.BG) {
                  Position = new Vector2(ScreenWidth / 2, ScreenHeight / 2)
                , Scale = new Vector2(.5f, .5f)
            };
            World.AddChild(background);

            Ground = new Node() {
                Position = new Vector2(0, ScreenHeight)
            };

            for (int i = 0; i < 10; i++) {
                var sprite = new Sprite(Texture, Graveyard.Tiles.Tile_2_) {
                    Position = new Vector2(i * Graveyard.Tiles.Tile_2_.Width, 0)
                };

                Ground.AddChild(sprite);
            }

            Ground.AddChild(new Sprite(Texture, Graveyard.Objects.TombStone_1_) {
                  Position = new Vector2(200, -105)
                , Scale = new Vector2(1.5f, 1.5f)
            });
            Ground.AddChild(new Sprite(Texture, Graveyard.Objects.TombStone_2_) {
                  Position = new Vector2(700, -120)
                , Scale = new Vector2(1.5f, 1.5f)
            });
            Ground.AddChild(new Sprite(Texture, Graveyard.Objects.Bush_1_) {
                  Position = new Vector2(400, -75)
                , Scale = new Vector2(.5f, .5f)
                , ZOrder = 10
            });


            AdventureGirl = new AdventureGirl(Texture);

            Ground.AddChild(AdventureGirl);
            World.AddChild(Ground);
        }

        public override void Update(float deltaTime)
        {
            AdventureGirl.Update(deltaTime);

            var keyboardState = NutInput.Keyboard.GetState();

            /// Jump
            if (keyboardState.IsKeyPressedRightNow(Keys.Space)) {
                AdventureGirl.Jump();
            }

            /// Shoot
            if (keyboardState.IsKeyPressedRightNow(Keys.LeftControl)) {
                AdventureGirl.Shoot();
            }

            /// Melee
            if (keyboardState.IsKeyPressedRightNow(Keys.F)) {
                AdventureGirl.Melee();
            }

            /// Right
            if (keyboardState.IsKeyDown(Keys.Right)) {
                if (keyboardState.IsKeyDown(Keys.LeftShift)) {
                    AdventureGirl.SlideRight();
                }
                else {
                    AdventureGirl.RunRight(deltaTime);
                }
            }

            /// Left
            if (keyboardState.IsKeyDown(Keys.Left)) {
                if (keyboardState.IsKeyDown(Keys.LeftShift)) {
                    AdventureGirl.SlideLeft();
                }
                else {
                    AdventureGirl.RunLeft(deltaTime);
                }
            }

            if (keyboardState.IsKeyDown(Keys.Up)) {
                Ground.Position += new Vector2(0, -200 * deltaTime);
            }

            if (keyboardState.IsKeyDown(Keys.Down)) {
                Ground.Position += new Vector2(0, 200 * deltaTime);
            }
            
            /// Stand
            if (keyboardState.GetPressedKeys().Length == 0) {
                AdventureGirl.Stay();
            }
        }
    }
}