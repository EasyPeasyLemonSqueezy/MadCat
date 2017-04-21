using NutEngine;
using NutEngine.Input;
using Microsoft.Xna.Framework;

namespace MadCat
{
    public class JumpState : IState
    {
        private Entity entity;

        public JumpState(Entity entity)
        {
            this.entity = entity;
        }

        public void Enter()
        {
            var animation = entity.GetComponent<AnimationComponent>().Animation;
            animation.Change(Assets.AdventureGirlJump);
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

            return null;
        }

        public IState Update(float deltaTime)
        {
            var character = entity.GetComponent<CharacterComponent>();
            var velocity = entity.GetComponent<VelocityComponent>();

            if (velocity.Velocity == Vector2.Zero) {
                return new StandState(entity);
            }

            if (velocity.Velocity.X != 0 &&
                velocity.Velocity.Y == 0) {
                return new RunState(entity);
            }

            return null;
        }
    }
}
