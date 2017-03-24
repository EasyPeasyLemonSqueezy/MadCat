using NutEngine;
using NutEngine.Input;

namespace MadCat
{
    public class StandState : IState
    {
        private Character character;

        public StandState(Character character)
        {
            this.character = character;
        }

        public void Enter()
        {
            character.CurrentAnimation.Change(Assets.AdventureGirlIdle);
        }

        public void Exit()
        {
        }

        public IState Update(float deltaTime)
        {
            return null;
        }

        public IState UpdateInput(KeyboardState keyboardState)
        {
            return null;
        }
    }
}
