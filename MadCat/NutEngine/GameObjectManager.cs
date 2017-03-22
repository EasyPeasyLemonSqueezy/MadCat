using System.Collections.Generic;
namespace NutEngine
{
    public class GameObjectManager
    {
        public List<GameObject> Entities { get; } = new List<GameObject>();
        public CollisionDetector Detector { get; } = new CollisionDetector();

        public void Update(float deltaTime)
        {
            foreach (var entity in Entities) {
                entity.Update(deltaTime);
            }

            Detector.CheckCollisions(Entities);

            Entities.RemoveAll(entity => entity.Invalid);
        }

        public void Add(GameObject entity)
        {
            Entities.Add(entity);
        }
    }
}
