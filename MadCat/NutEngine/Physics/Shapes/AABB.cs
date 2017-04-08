using Microsoft.Xna.Framework;

namespace NutEngine.Physics.Shapes
{
    public class AABB : Shape
    {
        public Vector2 Min { get; set; }
        public Vector2 Max { get; set; }

        public Vector2 Middle => (Max + Min) / 2;       

        public override AABB Sector => this;

        public AABB(Vector2 min, Vector2 max)
        {
            Min = min;
            Max = max;
        }
    }
}