using Microsoft.Xna.Framework;
using NutEngine.Physics.Materials;

namespace NutEngine.Physics
{
    public interface IBody<out ShapeType>
    {
        ShapeType Shape { get; }
        MassData Mass { get; }
        Material Material { get; }
        object Owner { get; }

        Vector2 Position { get; set; }
        Vector2 Velocity { get; set; }
        Vector2 Force { get; set; }

        void ApplyImpulse(Vector2 impulse); // Because C# sucks
        void IntegrateVelocity(float dt); // Because C# sucks
        void ApplyForce(Vector2 force); // Because C# sucks
        void IntegrateForces(float dt); // Because C# sucks
    }
}
