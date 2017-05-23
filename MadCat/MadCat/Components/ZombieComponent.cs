using NutEngine;
using System;

namespace MadCat
{
    public class ZombieComponent : Component
    {
        private const float runVelocity = 50.0f;
        private Hero hero;

        public ZombieComponent(Hero hero)
        {
            this.hero = hero;
        }

        public override void Update(float deltaTime)
        {
            var body = Entity.GetComponent<BodyComponent>().Body;
            var heroBody = hero.GetComponent<BodyComponent>().Body;

            var vec = (heroBody.Position - body.Position);
            vec.Normalize();
            vec *= runVelocity;

            body.Velocity = vec;

            var sprite = Entity.GetComponent<SpriteComponent>().Sprite;
            sprite.Rotation = (float)Math.Atan2(vec.Y, vec.X);
        }
    }
}
