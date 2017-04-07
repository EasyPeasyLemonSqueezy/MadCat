using NutEngine.Input;
using NutEngine;

namespace MadCat
{
    public class StandState : IState
    {
        private CharacterComponent character;

        public StandState(CharacterComponent character)
        {
            this.character = character;
        }

        public void Enter()
        {
            var animation = character.Entity.GetComponent<AnimationComponent>().Animation;
            animation.Change(Assets.AdventureGirlIdle);
        }

        public IState UpdateInput(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(character.Control.RunRightKey)) {
                character.Run(CharacterComponent.Direction.RIGHT);
            }
            else if (keyboardState.IsKeyDown(character.Control.RunLeftKey)) {
                character.Run(CharacterComponent.Direction.LEFT);
            }
            else {
                character.Stand();
            }

            if (keyboardState.IsKeyPressedRightNow(character.Control.JumpKey)) {
                character.Jump();
            }

            if (keyboardState.IsKeyPressedRightNow(character.Control.MeleeKey)) {
                return new MeleeState(character);
            }

            if (keyboardState.IsKeyPressedRightNow(character.Control.ShootKey)) {
                return new ShootState(character);
            }

            return null;
        }

        public IState Update(float deltaTime)
        {
            var velocity = character.Entity.GetComponent<VelocityComponent>();

            if (velocity.Velocity.Y != 0) {
                return new JumpState(character);
            }

            if (velocity.Velocity.X != 0 &&
                velocity.Velocity.Y == 0) {
                return new RunState(character);
            }

            return null;
        }
    }
}
