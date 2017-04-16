using Microsoft.Xna.Framework;

namespace NutEngine.Physics.Shapes
{
    public class Circle : Shape
    {
        // I hope we change radius less often than using sector for fast check intersections.
        private float radius;
        public float Radius {
            get => radius;
            set {
                radius = value;
                sector = new AABB(new Vector2(value, value));
            }
        }

        private AABB sector;
        public override AABB Sector => sector;

        public Circle(float radius)
        {
            Radius = radius;
        }
    }
}
