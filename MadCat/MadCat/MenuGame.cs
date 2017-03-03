using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using NutEngine;


namespace MadCat
{
    class MenuGame : Application
    {
        private const int SCREEN_WIDTH = 960;
        private const int SCREEN_HEIGHT = 540;

        public MenuGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.ToggleFullScreen();
            graphics.ApplyChanges();


            Content.RootDirectory = "Content"; /// Папка со всеми ресурсами
        }

        protected override void Initialize()
        {
            base.Initialize();
            var startScene = new MenuScene(this); ///Запустить первую сцены игры

            runWithScene(startScene);
        }
    }
}
