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
        Timer timer;
        int t = 0;

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
            
            timer = new Timer();
            timer.Enabled = true;
        }

        public override void Update(float dt)
        { 
            t++;
            var keyboardState = NutInput.Keyboard.State;
            if (t > 200) {
                App.RunWithScene(new MenuScene(App));
            }
        }
    }
}
