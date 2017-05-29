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
            Assets.Init(Content);

            App.IsMouseVisible = false;
            Color = Color.White;

            var logo = Assets.NutLogo;

            logo.Position = new Vector2(App.ScreenWidth / 2, 250);
            logo.Scale = new Vector2(.7f, .7f);
            World.AddChild(logo);
          
            font = Content.Load<SpriteFont>("Jokerman");
            label = new Label(font, "Powered by NutEngine") {
                  ZOrder = 3
                , Color = Color.Black
                , Position = new Vector2(App.ScreenWidth / 2, App.ScreenHeight * 3 / 4)
                , Scale = new Vector2(.3f, .3f)
            };

            World.AddChild(label);
        }

        public override void Update(float dt)
        {
            Elapsed += dt;

            if (Elapsed > 1) {
                App.RunWithScene(new MenuScene(App));
            }
        }
    }
}
