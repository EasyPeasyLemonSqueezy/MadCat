using NutEngine;

namespace MadCat
{
    class ShootState : IState
    {
        private Entity entity;

        public ShootState(Entity entity)
        {
            this.entity = entity;
        }

        public void Enter()
        {
            var character = entity.GetComponent<CharacterComponent>();
            var animation = entity.GetComponent<AnimationComponent>().Animation;
            animation.Change(Assets.AdventureGirlShoot);

            character.Stand();

            var body = entity.GetComponent<ColliderComponent>().Body;
            Bullet bullet = new Bullet(body.Position, (float)character.Dir, character.Node, character.Manager);
            character.Bodies.AddBody(bullet.Body);
        }

        public IState UpdateInput(NutEngine.Input.KeyboardState keyboardState)
        {
            return null;
        }

        public IState Update(float deltaTime)
        {
            var animation = entity.GetComponent<AnimationComponent>().Animation;

            if (!animation.Enabled) {
                return new StandState(entity);
            }

            return null;
        }
    }
}
