using NutEngine.Physics.Shapes;

namespace NutEngine.Physics
{
    public static partial class Collider
    {
        public static bool Collide(Shape a, Shape b, out Manifold manifold)
        {
            return Collide((dynamic)a, (dynamic)b, out manifold);
        }
    }
}
