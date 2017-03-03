using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace NutEngine
{
    /// <summary>
    /// Класс приложения, от которого наследуется наша игра.
    /// Этот класс в свою очередь наследуется от класса monogame Game.
    /// Application занимается всеми внутренними процессами движка:
    /// управляет текущей сценой, меняет ее и тд.
    /// </summary>
    public class Application : Game
    {
        protected GraphicsDeviceManager graphics; /// Управляет окном игры
        protected SpriteBatch spriteBatch; /// То, чем мы рисуем
        public Stack<Scene> scenes; /// Сцены

        public SpriteBatch Batcher { get { return spriteBatch; } }

        public float ScreenWidth { get { return GraphicsDevice.PresentationParameters.BackBufferWidth; } }
        public float ScreenHeight { get { return GraphicsDevice.PresentationParameters.BackBufferHeight; } }

        /// <summary>
        /// В конструкторе Application создается окно игры и инициализируются
        /// прочие важные вещи. До того, как он завершит свою работу, не получится
        /// загрузить картинки, звуки и тд. Поэтому в monogame есть отдельный метод
        /// Initialize, предназначенный для подобных вещей.
        /// </summary>
        /// <remarks>
        /// Initialize необходимо перегрузить в классе игры и сделать все, что
        /// нужно при запуске игры.
        /// </remarks>
        protected override void Initialize()
        {
            base.Initialize();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            scenes = new Stack<Scene>();
        }

        /// <summary>
        /// Метод, в котором обновляется состояние игры, то есть вызывается
        /// обновление состояния текущей сцены.
        /// </summary>
        /// <param name="gameTime">
        /// Информация о том, сколько времени прошло с прошлого кадра.
        /// Мы оттуда просто берем время в секундах и передаем дальше в игру.
        /// </param>
        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            scenes.Peek().Update(deltaTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// Вызывает отрисовку текущей сцены.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            scenes.Peek().Draw();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Вызовется при выходе игры, чтобы удостовериться, что
        /// весь контент был освобожден.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
        }

        /// <summary>
        /// Запустить первую сцену при запуске игры или сменить текущую.
        /// </summary>
        public void runWithScene(Scene scene)
        {
            if (scenes.Count == 0) {
                scenes.Push(scene);
            }
            else {
                scenes.Pop();
                scenes.Push(scene);
            }
        }
    }
}
