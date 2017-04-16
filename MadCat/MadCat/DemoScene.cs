using Microsoft.Xna.Framework.Input;
using NutInput = NutEngine.Input;
using NutEngine;
using NutEngine.Physics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MadCat
{
    public class DemoScene : Scene
    {
        private BodiesManager Bodies { get; set; }

        private Ground Ground { get; set; }
        private Skull Skull { get; set; }

        public DemoScene(Application app) : base(app)
        {
            Assets.Init(Content);

            Bodies = new BodiesManager();

            Skull = new Skull();
            Ground = new Ground();

            World.AddChild(Ground.Sprite);
            Bodies.Bodies.Add(Ground.Body);
            World.AddChild(Skull.Sprite);
            Bodies.Bodies.Add(Skull.Body);
        }

        public override void Update(float dt)
        {
            var keyboardState = NutInput.Keyboard.GetState();

            Bodies.Update(dt);

            Ground.Update();
            Skull.Update();
        }
    }
}