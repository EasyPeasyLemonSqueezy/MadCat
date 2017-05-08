using System.Collections.Generic;

namespace NutEngine
{
    public class EntityManager
    {
        public List<Entity> Entities { get; } = new List<Entity>();

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
            Entities.Add(entity);
        }
    }
}
