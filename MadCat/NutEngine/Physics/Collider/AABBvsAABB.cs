using Microsoft.Xna.Framework;
using NutEngine.Physics.Shapes;
using System;

namespace NutEngine.Physics
{
    public static partial class Collider
    {
        //                      A.X extent
        //       <-------------------------------------->
        //      Min
        // A ^   +--------------------------------------+
        // . |   |                                      |
        // Y |   |                                      |
        //   |   |                                      |
        // e |   |              A           Min         |
        // x |   |                           +-------------------------------------+   ^ B
        // t |   |                           |##########|                          |   | .
        // e |   |                           |##########|                          |   | Y
        // n v   +--------------------------------------+                          |   |
        // t                                 |          Max        B               |   | e
        //                                   |                                     |   | x
        //                                   |                                     |   | t
        //                                   |                                     |   | e
        //                                   +-------------------------------------+   v n
        //                                                                         Max   t
        //                                   <------------------------------------->
        //                                                  B.X extent
        // 
        // ### - Manifold.
        public static bool Collide(IBody<AABB> a, IBody<AABB> b, out Manifold<Shape, Shape> manifold)
        {
            manifold = null;

            var distance = b.Position - a.Position;

            // Temporary solution, I don't want to install ValueTuple from nuget.
            // TODO: Update .NET to 4.7
            var aMax = a.Position + a.Shape.Vec;
            var aMin = a.Position - a.Shape.Vec;

            var bMax = b.Position + b.Shape.Vec;
            var bMin = b.Position - b.Shape.Vec;

            // Half extent along x/y axes.
            var aExtent = (aMax - aMin) / 2;
            var bExtent = (bMax - bMin) / 2;

            float xOverlap = aExtent.X + bExtent.X - Math.Abs(distance.X);
            if (xOverlap <= 0) {
                return false;
            }

            float yOverlap = aExtent.Y + bExtent.Y - Math.Abs(distance.Y);
            if (yOverlap <= 0) {
                return false;
            }

            // For new manifold.
            float depth;
            Vector2 normal;
            var contact = new Vector2();


            /// https://github.com/RandyGaul/tinyheaders/blob/master/tinyc2.h
            /// This is the shittest code what I've ever saw, but I hope it works.
            if (xOverlap < yOverlap) {
                depth = xOverlap;

                if (distance.X < 0) {
                    normal = -Vector2.UnitX;
                    contact = new Vector2(a.Position.X - aExtent.X, a.Position.Y);
                }
                else {
                    normal = Vector2.UnitX;
                    contact = new Vector2(a.Position.X + aExtent.X, a.Position.Y);
                }

            }
            else {
                depth = yOverlap;

                if (distance.Y < 0) {
                    normal = -Vector2.UnitY;
                    contact = new Vector2(a.Position.X, a.Position.Y - aExtent.Y);
                }
                else {
                    normal = Vector2.UnitY;
                    contact = new Vector2(a.Position.X, a.Position.Y + aExtent.Y);
                }
            }

            manifold = new Manifold<AABB, AABB>() {
                A = a, B = b,
                Depth = depth,
                Normal = normal,
                Contact = contact
            };

            return true;
        }

        public static bool Collide(IBody<AABB> a, IBody<AABB> b)
        {
            // Temporary solution, I don't want to install valuetuple from nuget
            // TODO: Update .NET to 4.7
            var aMax = a.Position + a.Shape.Vec;
            var aMin = a.Position - a.Shape.Vec;

            var bMax = b.Position + b.Shape.Vec;
            var bMin = b.Position - b.Shape.Vec;

            return !(aMax.X < bMin.X || aMin.X > bMax.X ||
                     aMax.Y < bMin.Y || aMin.Y > bMax.Y);
        }
    }
}
