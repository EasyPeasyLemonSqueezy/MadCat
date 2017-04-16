using Microsoft.Xna.Framework;
using System;

namespace NutEngine.Physics.Shapes
{
    public class AABB : Shape
    {
        // +---------+
        // |         |
        // |         |
        // |         |
        // |         |
        // |    #    |
        // |     \   |
        // |      \  |
        // |       \ |
        // |        \|
        // +---------+
        // Vector from center to bottom right corner.
        public Vector2 Vec { get; set; }

        public override AABB Sector => this;

        public AABB(Vector2 vec)
        {
            Vec = vec;
        }

        // TODO: Update .NET to 4.7
        //public (Vector2, Vector2) MinMax(Vector2 offset)
        //{
        //    return (offset - Vec, offset + Vec);
        //}
    }
}