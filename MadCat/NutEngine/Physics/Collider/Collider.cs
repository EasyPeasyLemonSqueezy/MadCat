using NutEngine.Physics.Shapes;

namespace NutEngine.Physics
{
    public static partial class Collider
    {
        public static bool Collide<FirstShapeType, SecondShapeType>
            (IBody<FirstShapeType> a, IBody<SecondShapeType> b, out Manifold<FirstShapeType, SecondShapeType> manifold)
            where FirstShapeType : Shape
            where SecondShapeType : Shape
        {
            return Collide((dynamic)a, (dynamic)b, out manifold);
        }
    }
}
