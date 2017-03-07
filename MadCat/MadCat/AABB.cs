using Microsoft.Xna.Framework;
using System;

namespace MadCat
{
    /// <summary>
    /// AABB (Axis Aligned Bounding Box).
    /// Rectangle without rotation for collision detection.
    /// </summary>
    public class AABB
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public float MinX { get { return X; } }
        public float MaxX { get { return X + Width; } }
        public float MinY { get { return Y; } }
        public float MaxY { get { return Y + Height; } }

        public bool Intersects(AABB other)
        {
            if (MaxX >= other.MinX && MinX <= other.MaxX &&
                MaxY >= other.MinY && MinY <= other.MaxY)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get response for collided AABBs.
        /// </summary>
        /// <returns>Vector for this AABB to nearest point where AABBs are not collide.</returns>
        public Vector2 Response(AABB other)
        {
            /// I think, we can(must) rewrite all of this
            float left = other.MinX - MaxX;
            float right = other.MaxX - MinX;
            float bottom = other.MinY - MaxY;
            float top = other.MaxY - MinY;

            Vector2 translation = new Vector2();

            translation.X = Math.Abs(left) < Math.Abs(right) ? left : right;
            translation.Y = Math.Abs(bottom) < Math.Abs(top) ? bottom : top;

            if (Math.Abs(translation.X) < Math.Abs(translation.Y))
            {
                translation.Y = 0.0f;
            }
            else
            {
                translation.X = 0.0f;
            }

            return translation;
        }
    }
}
