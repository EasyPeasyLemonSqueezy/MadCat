using Microsoft.Xna.Framework.Input;
using NutEngine;
using NutEngine.Physics;
using Microsoft.Xna.Framework;
using NutPacker.Content;
using NutInput = NutEngine.Input;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace MadCat
{
    public class PauseScene : Scene
    {
        private MouseState PrevMouseState;

        private SpriteFont font;
        private Label label;
        private Label labelContinue;

        public PauseScene(Application app) : base(app)
        {
            App.IsMouseVisible = true;

            font = Content.Load<SpriteFont>("myFont");
            label = new Label(font, "Pause") {
                ZOrder = 3,
                Color = Color.White,
                Position = new Vector2(App.ScreenWidth / 2, 100),
                Scale = new Vector2(1.5f, 1.5f)
            };
            World.AddChild(label);

            labelContinue = new Label(font, "Continue") {
                ZOrder = 3,
                Color = Color.Black,
                Position = new Vector2(App.ScreenWidth / 2, App.ScreenHeight / 2),
                Scale = new Vector2(.7f, .7f)
            };
            World.AddChild(labelContinue);

            PrevMouseState = Mouse.GetState();

            Assets.Init(Content);

            var background = new Sprite(Assets.Texture, Graveyard.Tiles.BG) {
                Position = new Vector2(App.ScreenWidth / 2, App.ScreenHeight / 2)
                ,
                Scale = new Vector2(.5f, .5f)
                ,
                Color = Color.Gray
            };
            World.AddChild(background);
        }

        public override void Update(float dt)
        {
            var mouseState = Mouse.GetState();
            var keyboardState = NutInput.Keyboard.State;

            if (Math.Abs(mouseState.Position.X - App.ScreenWidth / 2) < 60 && Math.Abs(mouseState.Position.Y - App.ScreenHeight / 2) < 10) {
                labelContinue.Color = Color.Aquamarine;
                if (mouseState.LeftButton == ButtonState.Pressed && PrevMouseState.LeftButton == ButtonState.Released) {
                    App.Scenes.Pop();
                }
            }
            else {
                labelContinue.Color = Color.Black;
            }

            if (keyboardState.IsKeyPressedRightNow(Keys.Enter)) {
                App.Scenes.Pop();
            }
        }
    }
}