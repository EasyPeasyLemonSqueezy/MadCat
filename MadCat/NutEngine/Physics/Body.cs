using Microsoft.Xna.Framework;
using NutEngine.Physics.Shapes;

namespace NutEngine.Physics
{
    public class Body
    {
        public Shape Shape { get; private set; }
        public MassData Mass { get; private set; }
        public Material Material { get; private set; }
        public Vector2 Velocity { get; set; }
        public float Force { get; set; }
    }
}
