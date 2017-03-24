using NutEngine;

namespace MadCat
{
    class SlideState : IState
    {
        private Character character;

        public SlideState(Character character)
        {
            this.character = character;
        }

        public void Enter()
        {
            character.CurrentAnimation.Change(Assets.AdventureGirlSlide);
        }

        public IState UpdateInput(NutEngine.Input.KeyboardState keyboardState)
        {
            return null;
        }

        public IState Update(float deltaTime)
        {
            if (!character.CurrentAnimation.Enabled) {
                return new StandState(character);
            }

            return null;
        }
    }
}
