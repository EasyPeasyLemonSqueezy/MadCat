using NutEngine;

namespace MadCat
{
    public class JustEmptyScene : Scene
    {
        public JustEmptyScene(Application app) : base(app)
        {
            App.IsMouseVisible = false;
        }

        public override void Update(float deltaTime) { }
    }
}