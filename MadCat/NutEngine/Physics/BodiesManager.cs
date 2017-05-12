using Microsoft.Xna.Framework;
using NutEngine.Physics.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NutEngine.Physics
{
    public class BodiesManager
    {
        private HashSet<IBody<IShape>> Bodies { get; set; }

        private HashSet<Tuple<IBody<IShape>, IBody<IShape>>> Pairs { get; set; }
        public HashSet<Collision> Collisions { get; private set; }

        public BodiesManager()
        {
            Bodies = new HashSet<IBody<IShape>>();
            Pairs = new HashSet<Tuple<IBody<IShape>, IBody<IShape>>>();
            Collisions = new HashSet<Collision>();
        }

        private void CalculateCollisions()
        {
            foreach (var pair in Pairs) {
                if (Collider.Collide(pair.Item1, pair.Item2, out var manifold)) {
                    Collisions.Add(new Collision(pair.Item1, pair.Item2, manifold));
                }
            }
        }

        private void ResolveCollisions()
        {
            foreach (var collision in Collisions) {
                collision.ResolveCollision();
            }
        }

        private void IntegrateVelocities(float dt)
        {
            foreach (var body in Bodies) {
                body.IntegrateVelocity(dt);
            }
        }

        /// <summary>
        /// May the Force be with you.
        /// </summary>
        private void IntegrateForces(float dt)
        {
            foreach (var body in Bodies) {
                body.IntegrateForces(dt);
            }
        }

        private void ClearForces()
        {
            foreach (var body in Bodies) {
                body.Force = Vector2.Zero;
            }
        }

        private void PositionAdjustment()
        {
            foreach (var collision in Collisions) {
                collision.PositionAdjustment();
            }
        }

        public void Update(float dt)
        {
            CalculateCollisions();
            IntegrateForces(dt); // I think we should do it before collision calculation.
            ResolveCollisions();
            IntegrateVelocities(dt);
            PositionAdjustment();
            ClearForces();
            OnUpdateAll();
            OnCollisionAll();
            Collisions.Clear();
        }

        public bool AddBody(IBody<IShape> body)
        {
            if (Bodies.Contains(body)) {
                return false;
            }

            foreach (var b in Bodies) {
                if (b.Mass.MassInv + body.Mass.MassInv != 0) {
                    Pairs.Add(Tuple.Create(b, body));
                }
            }

            Bodies.Add(body);
            return true;
        }

        public bool KillSome(IBody<IShape> body)
        {
            if (!Bodies.Contains(body)) {
                return false;
            }

            Pairs.RemoveWhere(pair => pair.Item1 == body
                                   || pair.Item2 == body);

            Bodies.Remove(body);
            return true;
        }

        public bool KillSome(IEnumerable<IBody<IShape>> bodies)
        {
            var bodySet = new HashSet<IBody<IShape>>(bodies);

            Pairs.RemoveWhere(pair => bodySet.Contains(pair.Item1)
                                   || bodySet.Contains(pair.Item2));

            bool result = true;
            foreach (var body in bodies) {
                result &= Bodies.Remove(body);
            }

            return result;
        }

        public bool KillSome(Func<IBody<IShape>, bool> predicate)
        {
            var bodies = Bodies.Where(predicate).ToList();

            return KillSome(bodies);
        }

        public IEnumerable<IBody<IShape>> GetBodies()
        {
            foreach (var body in Bodies) {
                yield return body;
            }
        }

        private void OnUpdateAll()
        {
            foreach (var body in Bodies) {
                body.OnUpdate?.Invoke();
            }
        }

        private void OnCollisionAll()
        {
            foreach (var collision in Collisions) {
                collision.OnCollision();
            }
        }
    }
}
