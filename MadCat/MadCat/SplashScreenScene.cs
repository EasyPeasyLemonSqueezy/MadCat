using Microsoft.Xna.Framework.Input;
using NutEngine;
using NutEngine.Physics;
using Microsoft.Xna.Framework;
using NutPacker.Content;
using NutInput = NutEngine.Input;
using System;
using Microsoft.Xna.Framework.Graphics;
using System.Timers;

namespace MadCat
{
    public class SplashScreenScene : Scene
    {
        private SpriteFont font;
        private Label label;
        private float Elapsed;

        public SplashScreenScene(Application app) : base(app)
        {
            App.IsMouseVisible = false;
            Color = Color.White;
          
            font = Content.Load<SpriteFont>("myFont");
            label = new Label(font, "Powered by NutEngine") {
                  ZOrder = 3
                , Color = Color.Black
                , Position = new Vector2(App.ScreenWidth / 2, App.ScreenHeight/2)
                , Scale = new Vector2(.7f, .7f)
            };

            World.AddChild(label);
        }

        public override void Update(float dt)
        {
            Elapsed += dt;

            if (Elapsed > 3) {
                App.RunWithScene(new MenuScene(App));
            }
        }
    }
}
