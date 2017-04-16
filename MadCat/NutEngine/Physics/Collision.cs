using Microsoft.Xna.Framework;
using NutEngine.Physics.Shapes;

namespace NutEngine.Physics
{
    // Probably here should be just Shape instead of generic First(Second)ShapeType
    public class Collision<FirstShapeType, SecondShapeType>
        where FirstShapeType : Shape
        where SecondShapeType : Shape
    {
        public IBody<FirstShapeType> A { get; set; }
        public IBody<SecondShapeType> B { get; set; }
        public Manifold<FirstShapeType, SecondShapeType> Manifold { get; set; }

        public Collision(IBody<FirstShapeType> a, IBody<SecondShapeType> b, Manifold<FirstShapeType, SecondShapeType> manifold)
        {
            A = a;
            B = b;
            Manifold = manifold;
        }

        public void PositionAdjustment()
        {
            const float kSlop = .05f; // Penetration allowance
            const float percent = .5f; // Penetration percentage to correct

            var correction = (MathHelper.Max(Manifold.Depth - kSlop, 0)
                           / (A.Mass.MassInv + B.Mass.MassInv))
                           * Manifold.Normal
                           * percent;

            A.Position -= correction * A.Mass.MassInv;
            B.Position += correction * B.Mass.MassInv;
        }
    }
}
