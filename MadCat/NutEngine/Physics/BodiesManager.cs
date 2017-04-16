using NutEngine.Physics.Shapes;
using System.Collections.Generic;

namespace NutEngine.Physics
{
    public class BodiesManager
    {
        public HashSet<IBody<Shape>> Bodies { get; private set; }
        public HashSet<Collision<Shape, Shape>> Collisions { get; private set; }

        public BodiesManager()
        {
            Bodies = new HashSet<IBody<Shape>>();
            Collisions = new HashSet<Collision<Shape, Shape>>();
        }

        public void CalculateCollisions()
        {
            // In one wonderful day, here will be only one loop by pairs - (RigidBody, IBody)
            // It's called "Broad Phase"
            foreach (var body in Bodies) {
                foreach (var body2 in Bodies) {
                    if (body != body2) {
                        if (Collider.Collide(body, body2, out var manifold)) {
                            Collisions.Add(new Collision<Shape, Shape>(body, body2, manifold));
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
                    // body.ApplyImpulse(Vector2.Zero); // TODO: fix it
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
                    // body.ApplyForce(Vector2.Zero); // TODO: fix it
                }
            }
        }
    }
}
