using NutInput = NutEngine.Input;
using NutEngine;
using NutEngine.Physics;

namespace MadCat
{
    public class DemoScene : Scene
    {
        private BodiesManager Bodies { get; set; }

        public DemoScene(Application app) : base(app)
        {
            Assets.Init(Content);
            Bodies = new BodiesManager();
        }

        public override void Update(float deltaTime)
        {
            var keyboardState = NutInput.Keyboard.GetState();
            
            
        }
    }
}