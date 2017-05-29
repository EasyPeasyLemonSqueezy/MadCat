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

        protected Color Color { get; set; }

        public Scene(Application app)
        {
            App = app;
            Batcher = app.Batcher;
            Content = app.Content;
            World = new Node();
            Camera = new OrthographicSRTCamera(new Vector2(App.ScreenWidth, App.ScreenHeight));
            Color = Color.Black;
        }

        /// <summary>
        /// Update current game state.
        /// You should realize it in every your scene.
        /// </summary>
        public abstract void Update(float deltaTime);

        /// <summary>
        /// Draw the scene.
        /// Execute <see cref="Node.Visit(SpriteBatch, Matrix2D)"/>
        /// on each node, start from root node - <see cref="World"/>.
        /// </summary>
        public void Draw()
        {
            /// Fill background with black color.
            App.GraphicsDevice.Clear(Color);

            Batcher.Begin();

            var transform = Camera.Transform;
            World.Visit(Batcher, transform);

            Batcher.End();
        }
    }
}
