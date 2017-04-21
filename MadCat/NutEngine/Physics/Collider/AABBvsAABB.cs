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
        public static bool Collide(IBody<AABB> a, IBody<AABB> b, out Manifold manifold)
        {
            manifold = null;

            var distance = b.Position - a.Position;

            // Half extent along x/y axes.
            var aExtent = a.Shape.Vec;
            var bExtent = b.Shape.Vec;

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

            manifold = new Manifold() {
                Depth = depth,
                Normal = normal,
            };

            return true;
        }

        public static bool Collide(IBody<AABB> a, IBody<AABB> b)
        {
            (var aMin, var aMax) = a.Shape.MinMax(a.Position);
            (var bMin, var bMax) = b.Shape.MinMax(b.Position);

            return !(aMax.X < bMin.X || aMin.X > bMax.X ||
                     aMax.Y < bMin.Y || aMin.Y > bMax.Y);
        }
    }
}
