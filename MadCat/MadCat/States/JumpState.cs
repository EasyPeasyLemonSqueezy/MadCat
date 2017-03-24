using NutEngine;
using NutEngine.Input;
using Microsoft.Xna.Framework;

namespace MadCat
{
    public class JumpState : IState
    {
        private Character character;

        public JumpState(Character character)
        {
            this.character = character;
        }

        public void Enter()
        {
            character.CurrentAnimation.Change(Assets.AdventureGirlJump);
        }

        public IState UpdateInput(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(character.Control.RunRightKey)) {
                character.Run(Character.Direction.RIGHT);
            }
            else if (keyboardState.IsKeyDown(character.Control.RunLeftKey)) {
                character.Run(Character.Direction.LEFT);
            }
            else {
                character.Stand();
            }

            return null;
        }

        public IState Update(float deltaTime)
        {
            if (character.Velocity == Vector2.Zero) {
                return new StandState(character);
            }

            if (character.Velocity.X != 0 &&
                character.Velocity.Y == 0) {
                return new RunState(character);
            }

            return null;
        }
    }
}
