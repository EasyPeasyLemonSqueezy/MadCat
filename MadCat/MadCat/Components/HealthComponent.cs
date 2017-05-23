using NutEngine;

namespace MadCat
{
    public class HealthComponent : Component
    {
        public int Health { get; set; }

        public HealthComponent(int health)
        {
            Health = health;
        }

        public override void Update(float deltaTime)
        {
            if (Health <= 0) {
                Entity.RemoveComponent<BodyComponent>();
            }
        }
    }
}
