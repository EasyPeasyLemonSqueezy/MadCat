using NutEngine;

namespace MadCat
{
    class SlideState : IState
    {
        private Entity entity;

        public SlideState(Entity entity)
        {
            this.entity = entity;
        }

        public void Enter()
        {
            var animation = entity.GetComponent<AnimationComponent>().Animation;
            animation.Change(Assets.AdventureGirlSlide);
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
