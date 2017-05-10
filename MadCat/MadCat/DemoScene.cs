using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NutInput = NutEngine.Input;
using NutEngine;
using NutEngine.Physics;
using System;

namespace MadCat
{
    public class DemoScene : Scene
    {
        private EntityManager manager;
        private BodiesManager bodies;

        public DemoScene(Application app) : base(app)
        {
            Assets.Init(Content);

            manager = new EntityManager();
            bodies = new BodiesManager();
        }

        public override void Update(float deltaTime)
        {
            var keyboardState = NutInput.Keyboard.State;

            manager.Update(deltaTime);
            bodies.Update(deltaTime);
        }
    }
}