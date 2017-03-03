using Microsoft.Xna.Framework;
using NutEngine;

namespace MadCat
{
    public class DemoGame : Application
    {
        private const int SCREEN_WIDTH = 960;
        private const int SCREEN_HEIGHT = 540;

        public DemoGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            Graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            Graphics.ApplyChanges();

            Content.RootDirectory = "Content"; /// Folder with content.
        }

        protected override void Initialize()
        {
            base.Initialize();
            var startScene = new DemoScene(this);

            RunWithScene(startScene);
        }
    }
}