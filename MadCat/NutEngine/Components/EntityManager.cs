using System;
using System.Collections.Generic;

namespace NutEngine
{
    public class EntityManager
    {
        public List<Entity> Entities { get; } = new List<Entity>();
        private static Dictionary<Type, Type[]> Dependencies = new Dictionary<Type, Type[]>();

        public void Update(float deltaTime)
        {
            foreach (var entity in Entities) {
                entity.Update(deltaTime);
            }

            Entities.RemoveAll(
            entity => {
                if (entity.Invalid) {
                    entity.Dispose();
                    return true;
                }
                return false;
            });
        }

        public void Add(Entity entity)
        {
            entity.Manager = this;
            Entities.Add(entity);
        }

        public static void AddDependency<T>(params Type[] types)
            where T : Component
        {
            Dependencies.Add(typeof(T), types);
        }

        public static Type[] GetDependency(Type type)
        {
            if (!Dependencies.ContainsKey(type)) {
                Dependencies.Add(type, new Type[0]);
            }
            return Dependencies[type];
        }
    }
}
