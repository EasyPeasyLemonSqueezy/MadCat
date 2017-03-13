using System;
using System.Collections.Generic;

namespace NutEngine
{
    /// <summary>
    /// CollisionDetector helps to check collisions between GameObjects
    /// and do what you need when they collide
    /// </summary>
    public class CollisionDetector
    {
        public delegate void Rule(GameObject first, GameObject second);

        private Dictionary<Tuple<Type, Type>, Rule> typeRules;

        public CollisionDetector()
        {
            typeRules = new Dictionary<Tuple<Type, Type>, Rule>();
        }

        /// <summary>
        /// Checks collisions between all entities 
        /// and applies rules to them if they collide
        /// </summary>
        /// <param name="entities">Collection with GameObjects that can collide</param>
        public void CheckCollisions(IEnumerable<GameObject> entities)
        {
            foreach (var first in entities) {
                foreach (var second in entities) {
                    if (first != second) {
                        var types = new Tuple<Type, Type>(first.GetType(), second.GetType());
                        if (typeRules.ContainsKey(types)) {
                            if (first.Collider.Intersects(second.Collider)) {
                                typeRules[types](first, second);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Add collision rule for two types.
        /// Types can be the same.
        /// </summary>
        /// <param name="rule">Delegate or lambda that takes two GameObjects and do some things with them</param>
        public void AddTypeRule<T1, T2>(Rule rule)
        {
            typeRules.Add(new Tuple<Type, Type>(typeof(T1), typeof(T2)), rule);
        }
    }
}
