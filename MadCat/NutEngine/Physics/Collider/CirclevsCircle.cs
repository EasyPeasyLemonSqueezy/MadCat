using Microsoft.Xna.Framework;
using NutEngine.Physics.Shapes;

namespace NutEngine.Physics
{
    public static partial class Collider
    {
        public static bool Collide(IBody<Circle> a, IBody<Circle> b, out IntersectionArea intersection)
        {
            // Probably here should be the sector collisions check,
            // but it needs additional method without manifolds
            //
            // "There is only one god, and His name is Code.
            // And there is only one thing we say to Code: 'not today'."

            var normal = b.Position - a.Position;
            float radius = a.Shape.Radius + b.Shape.Radius;

            if (normal.LengthSquared() >= radius * radius) {
                intersection = null;
                return false; // Circles doesn't collide
            }
            else {
                float distance = normal.Length();

                if (distance == 0) {
                    intersection = new IntersectionArea() {
                        Depth = a.Shape.Radius,
                        Normal = Vector2.UnitX,
                    };
                }
                else {
                    var normalizable = normal / distance;

                    intersection = new IntersectionArea() {
                        Depth = radius - distance,
                        Normal = normalizable,
                    };
                }

                return true;
            }
        }

        public static bool Collide(IBody<Circle> a, IBody<Circle> b)
        {
            var rSquare = (a.Shape.Radius + b.Shape.Radius) * (a.Shape.Radius + b.Shape.Radius);

            return rSquare > (a.Position.X - b.Position.X) * (a.Position.X - b.Position.X)
                           + (a.Position.Y - b.Position.Y) * (a.Position.Y - b.Position.Y);
        }
    }
}
