using Microsoft.Xna.Framework.Input;
using NutEngine;
using NutEngine.Physics;
using Microsoft.Xna.Framework;
using NutPacker.Content;
using NutInput = NutEngine.Input;
using System;

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

            float bgScale = App.ScreenWidth / Logo.Mozilla_Mascot.Size.X;
            var bg = Assets.Mozilla;
            bg.Position += new Vector2(App.ScreenWidth, App.ScreenHeight) / 2;
            bg.Scale *= bgScale;
            World.AddChild(bg);

            Bodies = new BodiesManager();

            int pos = 0;
            var center = new Vector2(300, App.ScreenHeight / 2 - 50); // 50 - size of logo

            for (float angle = MathHelper.PiOver2; angle < MathHelper.Pi; angle += MathHelper.PiOver4 / 6) {
                var chrome = new ChromeLogo(new Vector2(center.X + 250 * (float)Math.Cos(angle), center.Y + 250 * (float)Math.Sin(angle)));
                World.AddChild(chrome.Sprite);
                Bodies.AddBody(chrome.Body);

                chrome = new ChromeLogo(new Vector2(App.ScreenWidth - 300 - 250 * (float)Math.Cos(angle), center.Y + 250 * (float)Math.Sin(angle)));
                World.AddChild(chrome.Sprite);
                Bodies.AddBody(chrome.Body);
            }

            pos = 400;
            while (pos < app.ScreenWidth - 400) {
                var cat = new CatInTheJar(new Vector2(pos, 200));
                World.AddChild(cat.Sprite);
                Bodies.AddBody(cat.Body);


                pos += (int)(cat.Sprite.TextureRegion.Frame.Size.X * cat.Sprite.Scale.X);
            }

            pos = 300;
            while (pos < App.ScreenWidth - 300) {
                var chrome = new ChromeLogo(new Vector2(pos, App.ScreenHeight / 2 + 200)); // 50 - size of logo
                World.AddChild(chrome.Sprite);
                Bodies.AddBody(chrome.Body);

                chrome = new ChromeLogo(new Vector2(pos, 300)); // 50 - size of logo
                World.AddChild(chrome.Sprite);
                Bodies.AddBody(chrome.Body);

                pos += (int)(chrome.Sprite.TextureRegion.Frame.Size.X * chrome.Sprite.Scale.X * .75f);
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

            // Remove bodies from Manager and World if those under the screen.
            Bodies.KillSome(body => {
                if (body.Position.Y > App.ScreenHeight) {
                    // We should rethink about this way
                    if (body.Owner is GoogleLogo google) {
                        google.Sprite.CommitSuicide();
                    }

                    return true;
                }

                return false;
            });

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