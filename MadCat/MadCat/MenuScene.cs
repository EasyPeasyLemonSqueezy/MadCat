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

        public MenuScene(Application app) : base(app)
        {
            App.IsMouseVisible = true;

            font = Content.Load<SpriteFont>("myFont");
            label = new Label(font, "Adventure gurl") {
                  ZOrder = 3
                , Color = Color.White
                , Position = new Vector2(App.ScreenWidth / 2, 100)
                , Scale = new Vector2(1.5f, 1.5f)
            };
            World.AddChild(label);

            labelStart = new Label(font, "Start") {
                  ZOrder = 3
                , Color = Color.Black
                , Position = new Vector2(App.ScreenWidth / 2, App.ScreenHeight / 2)
                , Scale = new Vector2(.7f, .7f)
            };
            World.AddChild(labelStart);

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

            if (Math.Abs(mouseState.Position.X - App.ScreenWidth / 2) < 50 && Math.Abs(mouseState.Position.Y - App.ScreenHeight / 2) < 10) {
                labelStart.Color = Color.Aquamarine;
                if (mouseState.LeftButton == ButtonState.Pressed && PrevMouseState.LeftButton == ButtonState.Released) {
                    App.Scenes.Push(new DemoScene(App));
                }
            }
            else {
                labelStart.Color = Color.Black;
            }

            if (keyboardState.IsKeyPressedRightNow(Keys.Enter)) {
                var startScene = new DemoScene(App);
                App.RunWithScene(startScene);
            }
        }
    }
}