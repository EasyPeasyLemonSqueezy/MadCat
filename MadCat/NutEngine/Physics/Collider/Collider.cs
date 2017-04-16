using NutEngine.Physics.Shapes;

namespace NutEngine.Physics
{
    public static partial class Collider
    {
        public static bool Collide(IBody<Shape> a, IBody<Shape> b, out Manifold manifold)
        {
            return Collide((dynamic)a, (dynamic)b, out manifold);
        }
    }
}
