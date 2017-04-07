using NutEngine;
using Microsoft.Xna.Framework;

namespace MadCat
{
    public class VelocityComponent : Component
    {
        public Vector2 Velocity { get; set; }

        public VelocityComponent(Vector2 velocity)
        {
            Velocity = velocity;
        }

        public override void Update(float deltaTime)
        {
            var position = Entity.GetComponent<PositionComponent>();
            position.Position = Physics.ApplyVelocity(position.Position, Velocity, deltaTime);
        }
    }
}
