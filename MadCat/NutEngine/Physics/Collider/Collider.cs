﻿using NutEngine.Physics.Shapes;

namespace NutEngine.Physics
{
    public static partial class Collider
    {
        public static bool Collide(IBody<IShape> a, IBody<IShape> b, out IntersectionArea intersection)
        {
            return Collide((dynamic)a, (dynamic)b, out intersection);
        }

        public static bool Collide(IBody<IShape> a, IBody<IShape> b)
        {
            return Collide((dynamic)a, (dynamic)b);
        }
    }
}
