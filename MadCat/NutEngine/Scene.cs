using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace NutEngine
{
    public abstract class Scene
    {
        protected Application App { get; }
        protected ContentManager Content { get; }
        protected SpriteBatch Batcher { get; }

        /// Корень графа сцены, который является родителем
        /// для всех объектов в мире игры.
        protected Node RootNode { get; set; }

        /// <summary>
        /// Сохраняем в сцене ссылки на нашу игру, то, чем рисуем
        /// и на менеджер контента, которым загружаем все ресурсы.
        /// </summary>
        public Scene(Application app)
        {
            App = app;
            Batcher = app.Batcher;
            Content = app.Content;
            RootNode = new Node();
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

            Transform2D transform = new Transform2D();
            RootNode.Visit(Batcher, transform);

            Batcher.End();
        }
    }
}
