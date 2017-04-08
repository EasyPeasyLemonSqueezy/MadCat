using Microsoft.Xna.Framework;

namespace NutEngine.Physics.Shapes
{
    public class Circle : Shape
    {
        public float Radius { get; set; }
        public Vector2 Position { get; set; }

        public override AABB Sector
            => new AABB(Position - new Vector2(Radius, Radius),
                        Position + new Vector2(Radius, Radius));

        public Circle(float radius, Vector2 position)
        {
            Radius = radius;
            Position = position;
        }
    }
}
