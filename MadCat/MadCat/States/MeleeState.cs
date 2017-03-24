using NutEngine;

namespace MadCat
{
    class MeleeState : IState
    {
        private Character character;

        public MeleeState(Character character)
        {
            this.character = character;
        }

        public void Enter()
        {
            character.Stand();
            character.CurrentAnimation.Change(Assets.AdventureGirlMelee);
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
