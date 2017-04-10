using Microsoft.Xna.Framework;
using NutEngine.Physics.Shapes;

namespace NutEngine.Physics
{
    public class Body
    {
        public Shape Shape { get; private set; }
        public MassData Mass { get; private set; }
        public Material Material { get; private set; }
        public object Owner { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Impulse { get; set; }
        public Vector2 Force { get; set; }

        public void ApplyImpulse()
        {
            Velocity += Mass.MassInv * Impulse;
            Impulse = Vector2.Zero;
        }

        public void ApplyForce(float delta)
        {
            /// No, it's not undefined behaviour.
            /// Evaluation from left to right.
            Position += (Velocity + (Velocity += (Force * Mass.MassInv * delta))) * delta / 2;

            Force = Vector2.Zero;
        }
    }
}
