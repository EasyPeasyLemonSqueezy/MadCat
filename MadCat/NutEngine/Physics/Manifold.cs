using Microsoft.Xna.Framework;
using NutEngine.Physics.Shapes;

namespace NutEngine.Physics
{
    public class Manifold<FirstShapeType, SecondShapeType>
        where FirstShapeType : Shape
        where SecondShapeType : Shape
    {
        public IBody<FirstShapeType> A { get; set; }
        public IBody<SecondShapeType> B { get; set; }

        public float Depth { get; set; }
        public Vector2 Normal { get; set; }
        public Vector2 Contact { get; set; }

        public void ApplyImpulse()
        {

        }
    }
}
