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
        private List<Skull> Skulls { get; set; }

        private MouseState PrevMouseState;

        public DemoScene(Application app) : base(app)
        {
            PrevMouseState = Mouse.GetState();

            Assets.Init(Content);

            Bodies = new BodiesManager();
            Skulls = new List<Skull>();

            Ground = new Ground();

            World.AddChild(Ground.Sprite);
            Bodies.AddBody(Ground.Body);
        }

        public override void Update(float dt)
        {
            var mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed && PrevMouseState.LeftButton == ButtonState.Released) {
                var skull = new Skull(new Vector2(mouseState.Position.X, mouseState.Position.Y));
                Skulls.Add(skull);
                World.AddChild(skull.Sprite);
                Bodies.AddBody(skull.Body);
            }


            Bodies.Update(dt);

            Ground.Update();
            foreach (var skull in Skulls) {
                skull.Update();
            }

            PrevMouseState = mouseState;
        }
    }
}