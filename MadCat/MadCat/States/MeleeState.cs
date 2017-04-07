using NutEngine;

namespace MadCat
{
    class MeleeState : IState
    {
        private CharacterComponent character;

        public MeleeState(CharacterComponent character)
        {
            this.character = character;
        }

        public void Enter()
        {
            var animation = character.Entity.GetComponent<AnimationComponent>().Animation;
            animation.Change(Assets.AdventureGirlMelee);
            character.Stand();
        }

        public IState UpdateInput(NutEngine.Input.KeyboardState keyboardState)
        {
            return null;
        }

        public IState Update(float deltaTime)
        {
            var animation = character.Entity.GetComponent<AnimationComponent>().Animation;

            if (!animation.Enabled) {
                return new StandState(character);
            }

            return null;
        }
    }
}
