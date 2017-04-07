using NutEngine;
using NutEngine.Input;
using Microsoft.Xna.Framework;

namespace MadCat
{
    public class JumpState : IState
    {
        private CharacterComponent character;

        public JumpState(CharacterComponent character)
        {
            this.character = character;
        }

        public void Enter()
        {
            var animation = character.Entity.GetComponent<AnimationComponent>().Animation;
            animation.Change(Assets.AdventureGirlJump);
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

            return null;
        }

        public IState Update(float deltaTime)
        {
            var velocity = character.Entity.GetComponent<VelocityComponent>();

            if (velocity.Velocity == Vector2.Zero) {
                return new StandState(character);
            }

            if (velocity.Velocity.X != 0 &&
                velocity.Velocity.Y == 0) {
                return new RunState(character);
            }

            return null;
        }
    }
}
