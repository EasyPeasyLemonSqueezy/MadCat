using NutEngine;

namespace MadCat
{
    class MeleeState : IState
    {
        private Entity entity;

        public MeleeState(Entity entity)
        {
            this.entity = entity;
        }

        public void Enter()
        {
            var character = entity.GetComponent<CharacterComponent>();
            var animation = entity.GetComponent<AnimationComponent>().Animation;

            animation.Change(Assets.AdventureGirlMelee);
            character.Stand();
        }

        public IState UpdateInput(NutEngine.Input.KeyboardState keyboardState)
        {
            return null;
        }

        public IState Update(float deltaTime)
        {
            var character = entity.GetComponent<CharacterComponent>();
            var animation = entity.GetComponent<AnimationComponent>().Animation;

            if (!animation.Enabled) {
                return new StandState(entity);
            }

            return null;
        }
    }
}
