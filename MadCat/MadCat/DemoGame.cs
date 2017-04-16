﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NutEngine;

namespace MadCat
{
    public class DemoGame : Application
    {
        private const int SCREEN_WIDTH = 960;
        private const int SCREEN_HEIGHT = 540;

        public DemoGame()
        {
            IsMouseVisible = true;

            Graphics = new GraphicsDeviceManager(this) {
                PreferredBackBufferWidth = SCREEN_WIDTH,
                PreferredBackBufferHeight = SCREEN_HEIGHT,
                GraphicsProfile = GraphicsProfile.HiDef
            };

            Content.RootDirectory = "Content"; /// Folder with content.
        }

        protected override void Initialize()
        {
            base.Initialize();

            var startScene = new BrowserScene(this);

            RunWithScene(startScene);
        }
    }
}