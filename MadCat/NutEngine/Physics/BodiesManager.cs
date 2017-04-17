using Microsoft.Xna.Framework;
using NutEngine.Physics.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NutEngine.Physics
{
    public class BodiesManager
    {
        private HashSet<IBody<Shape>> Bodies { get; set; }

        // First - only not static bodies, second - all bodies.
        private HashSet<Tuple<IBody<Shape>, IBody<Shape>>> Pairs { get; set; }
        public HashSet<Collision> Collisions { get; private set; }

        public BodiesManager()
        {
            Bodies = new HashSet<IBody<Shape>>();
            Pairs = new HashSet<Tuple<IBody<Shape>, IBody<Shape>>>();
            Collisions = new HashSet<Collision>();
        }

        public void CalculateCollisions()
        {
            foreach (var pair in Pairs) {
                if (Collider.Collide(pair.Item1, pair.Item2, out var manifold)) {
                    Collisions.Add(new Collision(pair.Item1, pair.Item2, manifold));
                }
            }
        }

        public void ResolveCollisions()
        {
            foreach (var collision in Collisions) {
                Collider.ResolveCollision(collision);
            }
        }

        public void IntegrateVelocities(float dt)
        {
            foreach (var body in Bodies) {
                body.IntegrateVelocity(dt);
            }
        }

        /// <summary>
        /// May the Force be with you.
        /// </summary>
        public void IntegrateForces(float dt)
        {
            foreach (var body in Bodies) {
                body.IntegrateForces(dt);
            }
        }

        public void ClearForces()
        {
            foreach (var body in Bodies) {
                body.Force = Vector2.Zero;
            }
        }

        public void PositionAdjustment()
        {
            foreach (var collision in Collisions) {
                collision.PositionAdjustment();
            }
        }

        public void Update(float dt)
        {
            CalculateCollisions();
            IntegrateForces(dt); // I think we should do it before collision calculation.
            ResolveCollisions(); // check
            IntegrateVelocities(dt);
            PositionAdjustment();
            ClearForces();
            Collisions.Clear();
        }

        public bool AddBody(IBody<Shape> body)
        {
            if (Bodies.Contains(body)) {
                return false;
            }

            if (body.Mass.MassInv != 0) { // If not static
                foreach (var b in Bodies) {
                    Pairs.Add(Tuple.Create(body, b));
                }
            }

            Bodies.Add(body);
            return true;
        }

        public bool KillSome(IBody<Shape> body)
        {
            if (!Bodies.Contains(body)) {
                return false;
            }

            Pairs.RemoveWhere(pair => pair.Item2 == body);

            if (body.Mass.MassInv != 0) { // If not static
                Pairs.RemoveWhere(pair => pair.Item1 == body);
            }

            Bodies.Remove(body);
            return true;
        }

        public bool KillSome(IEnumerable<IBody<Shape>> bodies)
        {
            Pairs.RemoveWhere(pair => bodies.Contains(pair.Item1)
                                   || bodies.Contains(pair.Item2));

            bool result = true;
            foreach (var body in bodies) {
                result &= Bodies.Remove(body);
            }

            return result;
        }

        public IEnumerable<IBody<Shape>> GetBodies()
        {
            foreach (var body in Bodies) {
                yield return body;
            }
        }
    }
}
