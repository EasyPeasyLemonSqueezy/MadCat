using Microsoft.Xna.Framework;
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
                Entity.RemoveComponent<InputComponent>();
                Entity.RemoveComponent<WeaponComponent>();
                Entity.RemoveComponent<ZombieComponent>();
                Entity.GetComponent<SpriteComponent>().Sprite.Color = Color.DarkRed;

                if (Entity.HasComponent<BodyComponent>()) {
                    Entity.AddComponent(new FrictionComponent());
                }
            }
        }
    }
}
