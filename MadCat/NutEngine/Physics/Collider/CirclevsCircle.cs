using Microsoft.Xna.Framework;
using NutEngine.Physics.Shapes;

namespace NutEngine.Physics
{
    public static partial class Collider
    {
        public static bool Collide(IBody<Circle> a, IBody<Circle> b, out Manifold manifold)
        {
            // Probably here should be the sector collisions check,
            // but it needs additional method without manifolds
            //
            // "There is only one god, and His name is Code.
            // And there is only one thing we say to Code: 'not today'."

            var normal = b.Position - a.Position;
            float radius = a.Shape.Radius + b.Shape.Radius;

            if (normal.LengthSquared() >= radius * radius) {
                manifold = null;
                return false; // Circles doesn't collide
            }
            else {
                float distance = normal.Length();

                if (distance == 0) {
                    manifold = new Manifold() {
                        A = a, B = b,
                        Depth = a.Shape.Radius,
                        Normal = Vector2.UnitX,
                        Contact = a.Position
                    };
                }
                else {
                    var normalizable = normal / distance;

                    manifold = new Manifold() {
                        A = a, B = b,
                        Depth = radius - distance,
                        Normal = normalizable,
                        Contact = normalizable * a.Shape.Radius + a.Position
                    };
                }

                return true;
            }
        }

        public static bool Collide(IBody<Circle> a, IBody<Circle> b)
        {
            var rSquare = (a.Shape.Radius + b.Shape.Radius) * (a.Shape.Radius + b.Shape.Radius);

            return rSquare < (a.Position.X - b.Position.X) * (a.Position.X - b.Position.X)
                           + (a.Position.Y - b.Position.Y) * (a.Position.Y - b.Position.Y);
        }
    }
}
