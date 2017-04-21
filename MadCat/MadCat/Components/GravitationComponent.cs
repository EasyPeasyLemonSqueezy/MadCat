using NutEngine;
using Microsoft.Xna.Framework;
using System;

namespace MadCat
{
    public class GravitationComponent : Component
    {
        public Vector2 Gravitation { get; set; } = new Vector2(0, 2000.0f);

        public override void Update(float deltaTime)
        {
            var velocity = Entity.GetComponent<VelocityComponent>();
            velocity.Velocity = Physics.ApplyAccel(velocity.Velocity, Gravitation, deltaTime);
        }

        public override Type[] GetDependencies()
        {
            return new Type[] {
                typeof(CharacterComponent),
            };
        }
    }
}
