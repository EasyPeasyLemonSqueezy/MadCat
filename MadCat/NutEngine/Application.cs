using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace NutEngine
{
    /// <summary>
    /// Application class.
    /// </summary>
    /// <remarks>
    /// Application is a wrapper for <see cref="Game"/>.
    /// It uses for change scenes, contain <see cref="Batcher"/>
    /// and other things.
    /// You should inherit your game class from it.
    /// </remarks>
    public class Application : Game
    {
        protected GraphicsDeviceManager Graphics; /// Game window control.
        protected SpriteBatch SpriteBatch;
        public Stack<Scene> Scenes;

        public SpriteBatch Batcher => SpriteBatch;
        public float ScreenWidth => GraphicsDevice.PresentationParameters.BackBufferWidth;
        public float ScreenHeight => GraphicsDevice.PresentationParameters.BackBufferHeight;

        /// <summary>
        /// You should override this method in your game class
        /// and for example start your first scene.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Scenes = new Stack<Scene>();
        }

        /// <summary>
        /// Update current game scene
        /// </summary>
        /// <param name="gameTime"> Elapsed time. </param>
        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Scenes.Peek().Update(deltaTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// Draw the current scene.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            Scenes.Peek().Draw();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Release all loaded content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
        }

        /// <summary>
        /// Start a new scene, or change current.
        /// </summary>
        public void RunWithScene(Scene scene)
        {
            if (Scenes.Count == 0) {
                Scenes.Push(scene);
            }
            else {
                Scenes.Pop();
                Scenes.Push(scene);
            }
        }
    }
}
