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
            AdventureGirl.Position += new Vector2(500, -100);


            ground.AddChild(AdventureGirl);
            World.AddChild(ground);
        }

        public override void Update(float deltaTime)
        {
            AdventureGirl.Update(deltaTime);

            var keyboardState = Keyboard.GetState();

            /// Right
            if (keyboardState.IsKeyDown(Keys.Right)) {
                AdventureGirl.RunRight(deltaTime);
            }

            /// Left
            else if (keyboardState.IsKeyDown(Keys.Left)) {
                AdventureGirl.RunLeft(deltaTime);
            }

            /// Stand
            else {
                AdventureGirl.Stay(deltaTime);
            }

            PrevKeyboardState = keyboardState;
        }
    }
}