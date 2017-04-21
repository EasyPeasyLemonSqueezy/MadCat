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

            var position = character.Entity.GetComponent<PositionComponent>();
            Bullet bullet = new Bullet(position.Position, (float)character.Dir, character.Node);
            character.Manager.Add(bullet);
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
