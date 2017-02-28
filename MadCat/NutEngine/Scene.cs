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

        private Node rootNode;
        protected Node World { get; set; }
        protected Camera Camera { get; set; }

        /// <summary>
        /// Сохраняем в сцене ссылки на нашу игру, то, чем рисуем
        /// и на менеджер контента, которым загружаем все ресурсы.
        /// </summary>
        public Scene(Application app)
        {
            App = app;
            Batcher = app.Batcher;
            Content = app.Content;
            rootNode = new Node();
            World = new Node();
            Camera = new Camera(0.0f, 0.0f); /// TODO: Игре нужно знать о высоте и ширине окна
            rootNode.AddChild(World);
            rootNode.AddChild(Camera);
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
            rootNode.Visit(Batcher, transform);

            Batcher.End();
        }
    }
}
