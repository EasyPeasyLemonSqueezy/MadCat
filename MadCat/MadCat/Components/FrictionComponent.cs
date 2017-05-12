using NutEngine;

namespace MadCat
{
    public class FrictionComponent : Component
    {
        public override void Update(float deltaTime)
        {
            var body = Entity.GetComponent<BodyComponent>().Body;
            var force = -body.Velocity;
            body.ApplyForce(force * 100f);
        }
    }
}
