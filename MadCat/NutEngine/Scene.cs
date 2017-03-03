using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using NutEngine.Camera;

namespace NutEngine
{
    public abstract class Scene
    {
        protected Application App { get; }
        protected ContentManager Content { get; }
        protected SpriteBatch Batcher { get; }

        protected Node World { get; set; }
        protected Camera2D Camera { get; set; }

        /// <summary>
        /// Сохраняем в сцене ссылки на нашу игру, то, чем рисуем
        /// и на менеджер контента, которым загружаем все ресурсы.
        /// </summary>
        public Scene(Application app)
        {
            App = app;
            Batcher = app.Batcher;
            Content = app.Content;
            World = new Node();
            Camera = new OrthographicSRTCamera(new Vector2(App.ScreenWidth, App.ScreenHeight));
        }

        /// <summary>
        /// Метод, в котором обновляем состояние сцены.
        /// Его необходимо перегрузить в каждой конкретной сцене.
        /// </summary>
        public abstract void Update(float deltaTime);

        /// <summary>
        /// Отрисовывает сцену.
        /// Вызывает метод Visit у каждого узла,
        /// начиная с корня (dfs).
        /// </summary>
        public void Draw()
        {
            App.GraphicsDevice.Clear(Color.Black); /// Залить все черным

            Batcher.Begin();

            var transform = Camera.Transform;
            World.Visit(Batcher, transform);

            Batcher.End();
        }
    }
}
