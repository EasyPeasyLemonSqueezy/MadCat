using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NutEngine;
using NutPacker;
using NutPacker.Content;
using System.Collections.Generic;

namespace MadCat
{
    public class DemoScene : Scene
    {
        private float ScreenHeight;
        private float ScreenWidth;

        private KeyboardState PrevKeyboardState;

        private Texture2D Texture;
        private AdventureGirl AdventureGirl;

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

            var ground = new Node() {
                Position = new Vector2(0, ScreenHeight)
            };

            for (int i = 0; i < 10; i++) {
                var sprite = new Sprite(Texture, Graveyard.Tiles.Tile_2_) {
                    Position = new Vector2(i * Graveyard.Tiles.Tile_2_.Width, 0)
                };

                ground.AddChild(sprite);
            }

            AdventureGirl = new AdventureGirl(Texture);


            ground.AddChild(AdventureGirl);
            World.AddChild(ground);
        }

        public override void Update(float deltaTime)
        {
            AdventureGirl.Update(deltaTime);

            var keyboardState = Keyboard.GetState();

            /// Jump
            if (keyboardState.IsKeyDown(Keys.Space) && PrevKeyboardState.IsKeyUp(Keys.Space)) {
                AdventureGirl.Jump();
            }

            /// Shoot
            else if (keyboardState.IsKeyDown(Keys.LeftControl) && PrevKeyboardState.IsKeyUp(Keys.LeftControl)) {
                AdventureGirl.Shoot();
            }

            else if (keyboardState.IsKeyDown(Keys.F) && PrevKeyboardState.IsKeyUp(Keys.F)) {
                AdventureGirl.Melee();
            }

            /// Right
            else if (keyboardState.IsKeyDown(Keys.Right)) {
                if (keyboardState.IsKeyDown(Keys.LeftShift)) {
                    AdventureGirl.SlideRight();
                }
                else {
                    AdventureGirl.RunRight(deltaTime);
                }
            }

            /// Left
            else if (keyboardState.IsKeyDown(Keys.Left)) {
                if (keyboardState.IsKeyDown(Keys.LeftShift)) {
                    AdventureGirl.SlideLeft();
                }
                else {
                    AdventureGirl.RunLeft(deltaTime);
                }
            }

            /// Stand
            else {
                AdventureGirl.Stay();
            }

            PrevKeyboardState = keyboardState;
        }
    }
}