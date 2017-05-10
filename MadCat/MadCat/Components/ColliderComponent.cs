using NutEngine;
using NutEngine.Physics;
using NutEngine.Physics.Shapes;

namespace MadCat
{
    public class ColliderComponent : Component
    {
        public RigidBody<AABB> Body { get; set; }

        public ColliderComponent(RigidBody<AABB> body)
        {
            Body = body;
        }

        public override void Update(float deltaTime)
        {
            
        }
    }
}
