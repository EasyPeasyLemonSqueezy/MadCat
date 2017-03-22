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
        private Dictionary<Tuple<Type, Type>, Action<GameObject, GameObject>> typeRules;

        public CollisionDetector()
        {
            typeRules = new Dictionary<Tuple<Type, Type>, Action<GameObject, GameObject>>();
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
                        var types = Tuple.Create(first.GetType(), second.GetType());
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
        public void AddTypeRule<T1, T2>(Action<GameObject, GameObject> rule)
            where T1: GameObject where T2 : GameObject
        {
            typeRules.Add(Tuple.Create(typeof(T1), typeof(T2)), rule);
        }
    }
}
