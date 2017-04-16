using Microsoft.Xna.Framework;
using NutEngine.Physics.Shapes;

namespace NutEngine.Physics
{
    public class Manifold
    {
        public IBody<Shape> A { get; set; }
        public IBody<Shape> B { get; set; }

        public float Depth { get; set; }
        public Vector2 Normal { get; set; }
        public Vector2 Contact { get; set; }
    }
}
