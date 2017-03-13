using System.Collections.Generic;

namespace NutEngine
{
    public class CollisionDetector
    {
        public void CheckCollisions(IEnumerable<GameObject> entities)
        {
            foreach (var first in entities)
            {
                foreach (var second in entities)
                {
                    if (first.Collider.Intersects(second.Collider))
                    {
                        first.Collide(second);
                    }
                }
            }
        }
    }
}
