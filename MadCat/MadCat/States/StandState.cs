using NutEngine.Input;
using NutEngine;

namespace MadCat
{
    public class StandState : IState
    {
        private Entity entity;

        public StandState(Entity entity)
        {
            this.entity = entity;
        }

        public void Enter()
        {
            var animation = entity.GetComponent<AnimationComponent>().Animation;
            animation.Change(Assets.AdventureGirlIdle);
        }

        public IState UpdateInput(KeyboardState keyboardState)
        {
            var character = entity.GetComponent<CharacterComponent>();

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
                return new MeleeState(entity);
            }

            if (keyboardState.IsKeyPressedRightNow(character.Control.ShootKey)) {
                return new ShootState(entity);
            }

            return null;
        }

        public IState Update(float deltaTime)
        {
            var velocity = entity.GetComponent<VelocityComponent>();

            if (velocity.Velocity.Y != 0) {
                return new JumpState(entity);
            }

            if (velocity.Velocity.X != 0 &&
                velocity.Velocity.Y == 0) {
                return new RunState(entity);
            }

            return null;
        }
    }
}
