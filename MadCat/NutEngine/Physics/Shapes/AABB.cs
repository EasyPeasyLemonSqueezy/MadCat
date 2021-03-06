﻿using Microsoft.Xna.Framework;

namespace NutEngine.Physics.Shapes
{
    public class AABB : IShape
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

        public AABB Sector => this;

        public AABB(Vector2 vec)
        {
            Vec = vec;
        }

        public (Vector2 Min, Vector2 Max) MinMax(Vector2 offset)
        {
            return (offset - Vec, offset + Vec);
        }
    }
}