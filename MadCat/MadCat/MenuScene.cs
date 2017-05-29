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
    public class MenuScene : Scene
    {
        private MouseState PrevMouseState;

        private SpriteFont font;
        private Label label;
        private Label labelStart;
        private Label labelExit;

        public MenuScene(Application app) : base(app)
        {
            App.IsMouseVisible = true;
            font = Content.Load<SpriteFont>("myFont");
            label = new Label(font, "Adventure girl") {
                  ZOrder = 3
                , Color = Color.White
                , Position = new Vector2(App.ScreenWidth / 2, 100)
                , Scale = new Vector2(1.5f, 1.5f)
            };
            World.AddChild(label);

            labelStart = new Label(font, "Start") {
                  ZOrder = 3
                , Color = Color.Black
                , Position = new Vector2(App.ScreenWidth / 2, App.ScreenHeight / 2 - 35)
                , Scale = new Vector2(.7f, .7f)
            };
            World.AddChild(labelStart);

            labelExit = new Label(font, "Exit game")
            {
                ZOrder = 3,
                Color = Color.Black,
                Position = new Vector2(App.ScreenWidth / 2, App.ScreenHeight / 2 + 20),
                Scale = new Vector2(.7f, .7f)
            };
            World.AddChild(labelExit);

            PrevMouseState = Mouse.GetState();

            Assets.Init(Content);

            var background = new Sprite(Assets.Texture, Graveyard.Tiles.BG) {
                  Position = new Vector2(App.ScreenWidth / 2, App.ScreenHeight / 2)
                , Scale = new Vector2(.5f, .5f)
                , Color = Color.Gray
            };
            World.AddChild(background); 
        }

        public override void Update(float dt)
        {
            var mouseState = Mouse.GetState();
            var keyboardState = NutInput.Keyboard.State;
            

            if (Math.Abs(mouseState.Position.X - App.ScreenWidth / 2) < 50 && Math.Abs(mouseState.Position.Y - App.ScreenHeight / 2 + 35) < 10) {
                labelStart.Color = Color.Aquamarine;
                if (mouseState.LeftButton == ButtonState.Pressed && PrevMouseState.LeftButton == ButtonState.Released) {
                    App.Scenes.Push(new DemoScene(App));
                }
            }
            else {
                labelStart.Color = Color.Black;
            }

            if (Math.Abs(mouseState.Position.X - App.ScreenWidth / 2) < 90 && Math.Abs(mouseState.Position.Y - App.ScreenHeight / 2 - 20) < 10) {
                labelExit.Color = Color.Aquamarine;
                if (mouseState.LeftButton == ButtonState.Pressed && PrevMouseState.LeftButton == ButtonState.Released) {
                    App.RunWithScene(new JustEmptyScene(App));
                }
            }
            else {
                labelExit.Color = Color.Black;
            }

            if (keyboardState.IsKeyPressedRightNow(Keys.Enter)) {
                App.Scenes.Push(new DemoScene(App));
            }

            if (keyboardState.IsKeyPressedRightNow(Keys.Escape)) {
                App.RunWithScene(new JustEmptyScene(App));
            }

            PrevMouseState = mouseState;
        }
    }
}