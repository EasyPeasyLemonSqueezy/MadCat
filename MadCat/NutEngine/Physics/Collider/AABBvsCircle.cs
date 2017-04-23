using Microsoft.Xna.Framework;
using NutEngine.Physics.Shapes;
using System;

namespace NutEngine.Physics
{
    public static partial class Collider
    {
        public static bool Collide(IBody<AABB> a, IBody<Circle> c, out Manifold manifold)
        {
            manifold = null;

            (var min, var max) = a.Shape.MinMax(a.Position);

            var closest = new Vector2(
                MathHelper.Clamp(c.Position.X, min.X, max.X),
                MathHelper.Clamp(c.Position.Y, min.Y, max.Y)
                );


            var inside = closest == c.Position;

            if (inside) {
                float newClosestX = c.Position.X > a.Position.X ? max.X : min.X;
                float newClosestY = c.Position.Y > a.Position.Y ? max.Y : min.Y;

                float xDiff = Math.Abs(c.Position.X - newClosestX);
                float yDiff = Math.Abs(c.Position.Y - newClosestY);

                if (xDiff < yDiff) {
                    closest.X = newClosestX;
                }
                else {
                    closest.Y = newClosestY;
                }
            }

            var normal = c.Position - closest;

            if (!inside && normal.LengthSquared() > c.Shape.Radius * c.Shape.Radius) {
                return false;
            }

            float length = normal.Length();

            manifold = new Manifold() {
                Depth = c.Shape.Radius + (inside ? length : -length),
                Normal = (inside ? -1 : 1) * normal / length
            };

            return true;
        }

        public static bool Collide(IBody<Circle> c, IBody<AABB> a, out Manifold manifold)
        {
            if (!Collide(a, c, out manifold)) {
                return false;
            }

            manifold.Normal = -manifold.Normal;
            return true;
        }

        public static bool Collide(IBody<AABB> a, IBody<Circle> c)
        {
            (var min, var max) = a.Shape.MinMax(a.Position);

            var closest = new Vector2(
                MathHelper.Clamp(c.Position.X, min.X, max.X),
                MathHelper.Clamp(c.Position.Y, min.Y, max.Y)
                );

            var distance = c.Position - closest;

            return distance.LengthSquared() < c.Shape.Radius * c.Shape.Radius;
        }

        public static bool Collide(IBody<Circle> c, IBody<AABB> a)
        {
            return Collide(a, c);
        }
    }
}
