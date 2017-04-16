using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NutInput = NutEngine.Input;
using NutEngine;
using NutPacker.Content;

namespace MadCat
{
    public class DemoScene : Scene
    {
        public DemoScene(Application app) : base(app)
        {
            
        }

        public override void Update(float deltaTime)
        {
            var keyboardState = NutInput.Keyboard.GetState();
            
            
        }
    }
}