using System.Collections.Generic;

namespace NutEngine.Physics
{
    public class BodiesManager
    {
        public HashSet<Body> Bodies { get; private set; }
        public HashSet<Collision> Collisions { get; private set; }

        public BodiesManager()
        {
            Bodies = new HashSet<Body>();
            Collisions = new HashSet<Collision>();
        }

        public void CalculateCollisions()
        {
            foreach (var body in Bodies) {
                foreach (var body2 in Bodies) { // Here will be only static bodies
                    if (body != body2) {
                        if (Collider.Collide(body.Shape, body2.Shape, out var manifold)) {
                            Collisions.Add(new Collision(body, body2, manifold));
                        }
                    }
                }
            }
        }

        public void ResolveCollisions()
        {
            foreach (var collision in Collisions) {
                Collider.ResolveCollision(collision);
            }
        }

        public void ApplyImpulses()
        {
            foreach (var body in Bodies) {
                body.ApplyImpulse();
            }
        }

        /// <summary>
        /// May the Force be with you.
        /// </summary>
        public void ApplyForces(float delta)
        {
            foreach (var body in Bodies) {
                body.ApplyForce(delta);
            }
        }
    }
}
