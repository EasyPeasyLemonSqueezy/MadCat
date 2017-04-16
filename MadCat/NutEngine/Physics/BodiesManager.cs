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
            foreach (var body in Bodies) { // Here will be only rigid bodies
                foreach (var body2 in Bodies) {
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
                // After resolve collision we have to zeroes velocity (After position correction),
                // or we can add some "bounce" effect, it'll be awesome.
                // Probably we should do it in .ResolveCollision.
            }
        }

        public void ApplyImpulses()
        {
            foreach (var body in Bodies) {
                // Dirty hack for pseudostatic bodies.
                if (body.Mass.MassInv != 0) {
                    body.ApplyImpulse();
                }
            }
        }

        /// <summary>
        /// May the Force be with you.
        /// </summary>
        public void ApplyForces(float delta)
        {
            foreach (var body in Bodies) {
                // Dirty hack for pseudostatic bodies.
                if (body.Mass.MassInv != 0) {
                    body.ApplyForce(delta);
                }
            }
        }
    }
}
