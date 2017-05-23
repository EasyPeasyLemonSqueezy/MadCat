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

            //if (body.Velocity != Vector2.Zero) {
            //    var direction = body.Velocity;
            //    direction.Normalize();

            //    float j = -(1 + body.Material.Restitution) * body.Velocity.Length() / body.Mass.MassInv;

            //    float jt = -Vector2.Dot(body.Velocity, direction) / body.Mass.MassInv;

            //    if (Single.IsNaN(jt)) {
            //        return;
            //    }

            //    var staticFriction = body.Material.StaticFriction;
            //    var dynamicFriction = body.Material.DynamicFriction;

            //    var impulse = Math.Abs(jt) < j * staticFriction ? direction * jt : direction * (-j) * dynamicFriction;

            //    body.ApplyImpulse(-impulse);
            //}
        }
    }
}
