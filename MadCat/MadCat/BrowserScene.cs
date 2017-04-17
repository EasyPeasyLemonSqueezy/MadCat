﻿using Microsoft.Xna.Framework.Input;
using NutEngine;
using NutEngine.Physics;
using Microsoft.Xna.Framework;
using NutPacker.Content;
using NutInput = NutEngine.Input;

namespace MadCat
{
    public class BrowserScene : Scene
    {
        private BodiesManager Bodies { get; set; }

        private MouseState PrevMouseState;
        private float GoogleScale = 1;

        public BrowserScene(Application app) : base(app)
        {
            PrevMouseState = Mouse.GetState();

            Assets.Init(Content);

            float bgScale = .5275f; // Scale to 960/540
            var bg = Assets.Mozilla;
            bg.Position += new Vector2(Logo.Mozilla_Mascot.Size.X, Logo.Mozilla_Mascot.Size.Y) * bgScale / 2;
            bg.Scale *= bgScale;
            World.AddChild(bg);

            Bodies = new BodiesManager();

            int pos = 0;
            while (pos < App.ScreenWidth) {
                var chrome = new ChromeLogo(new Vector2(pos, 500));
                World.AddChild(chrome.Sprite);
                Bodies.AddBody(chrome.Body);

                pos += (int)(chrome.Sprite.TextureRegion.Frame.Size.X * chrome.Sprite.Scale.X);
            }
        }

        public override void Update(float dt)
        {
            var mouseState = Mouse.GetState();

            UpdateScale(NutInput.Keyboard.GetState());

            if (mouseState.LeftButton == ButtonState.Pressed && PrevMouseState.LeftButton == ButtonState.Released) {
                var google = new GoogleLogo(new Vector2(mouseState.Position.X, mouseState.Position.Y), GoogleScale);
                World.AddChild(google.Sprite);
                Bodies.AddBody(google.Body);
            }


            Bodies.Update(dt);

            Bodies.KillSome(body => body.Position.Y > App.ScreenHeight);

            foreach (var body in Bodies.GetBodies()) {
                if (body.Owner is GoogleLogo google) {
                    google.Update();
                }
            }

            PrevMouseState = mouseState;
        }

        private void UpdateScale(NutInput.KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.D1)) {
                GoogleScale = 1f;
            }
            else if (keyboardState.IsKeyDown(Keys.D2)) {
                GoogleScale = 1.5f;
            }
            else if (keyboardState.IsKeyDown(Keys.D3)) {
                GoogleScale = 2f;
            }
        }
    }
}