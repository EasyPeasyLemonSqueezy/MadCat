using Microsoft.Xna.Framework;
using NutEngine;
using System;

namespace MadCat
{
    public class ColliderComponent : Component
    {
        public override Type[] Dependencies { get; } = {
            typeof(VelocityComponent)
        };
        public AABB Collider { get; set; }

        public ColliderComponent(AABB collider)
        {
            Collider = collider;
        }

        public override void Update(float deltaTime)
        {
            var position = Entity.GetComponent<PositionComponent>();
            Collider.X = position.Position.X - Collider.Width / 2.0f;
            Collider.Y = position.Position.Y - Collider.Height / 2.0f;
        }
    }
}
