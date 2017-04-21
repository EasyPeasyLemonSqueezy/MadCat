using NutEngine.Physics.Shapes;

namespace NutEngine.Physics
{
    public static partial class Collider
    {
        public static bool Collide(IBody<IShape> a, IBody<IShape> b, out Manifold manifold)
        {
            return Collide((dynamic)a, (dynamic)b, out manifold);
        }
    }
}
