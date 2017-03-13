using System;
using System.Collections.Generic;

namespace NutEngine
{
    public class CollisionDetector
    {
        public delegate void Callback();

        private Dictionary<Tuple<Type, Type>, Callback> typeRules;

        public CollisionDetector()
        {
            typeRules = new Dictionary<Tuple<Type, Type>, Callback>();
        }

        public void CheckCollisions(IEnumerable<GameObject> entities)
        {
            foreach (var first in entities) {
                foreach (var second in entities) {
                    if (first != second) {
                        var types = new Tuple<Type, Type>(first.GetType(), second.GetType());

                        if (typeRules.ContainsKey(types)) {
                            if (first.Collider.Intersects(second.Collider)) {
                                typeRules[types]();
                            }
                        } 
                    }
                }
            }
        }

        public void AddTypeRule<T1, T2>(Callback callback)
        {
            typeRules.Add(new Tuple<Type, Type>(typeof(T1), typeof(T2)), callback);
        }
    }
}
