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
        public static bool Collide(AABB a, AABB b, out Manifold manifold)
        {
            manifold = null;

            var distance = b.Middle - a.Middle; // I'm not sure but it seems to work.

            // Half extent along x/y axes.
            var aExtent = (a.Max - a.Min) / 2;
            var bExtent = (b.Max - b.Min) / 2;

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
            var contacts = new Vector2[1];


            /// https://github.com/RandyGaul/tinyheaders/blob/master/tinyc2.h
            /// This is the shittest code what I've ever saw, but I hope it works.
            if (xOverlap < yOverlap) {
                depth = xOverlap;

                if (distance.X < 0) {
                    normal = -Vector2.UnitX;
                    contacts[0] = new Vector2(a.Middle.X - aExtent.X, a.Middle.Y);
                }
                else {
                    normal = Vector2.UnitX;
                    contacts[0] = new Vector2(a.Middle.X + aExtent.X, a.Middle.Y);
                }

            }
            else {
                depth = yOverlap;

                if (distance.Y < 0) {
                    normal = -Vector2.UnitY;
                    contacts[0] = new Vector2(a.Middle.X, a.Middle.Y - aExtent.Y);
                }
                else {
                    normal = Vector2.UnitY;
                    contacts[0] = new Vector2(a.Middle.X, a.Middle.Y + aExtent.Y);
                }
            }

            manifold = new Manifold() {
                A = a, B = b,
                Depth = depth,
                Normal = normal,
                Contacts = contacts
            };

            return true;
        }

        public static bool Collide(AABB a, AABB b)
        {
            return !(a.Max.X < b.Min.X || a.Min.X > b.Max.X ||
                     a.Max.Y < b.Min.Y || a.Min.Y > b.Max.Y);
        }
    }
}
