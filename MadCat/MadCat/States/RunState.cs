using System;
using Microsoft.Xna.Framework;
using NutEngine;
using NutEngine.Input;

namespace MadCat
{
    public class RunState : IState
    {
        private Character character;

        public RunState(Character character)
        {
            this.character = character;
        }

        public void Enter()
        {
            character.CurrentAnimation.Change(Assets.AdventureGirlRun);
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

            if (keyboardState.IsKeyPressedRightNow(character.Control.JumpKey)) {
                character.Jump();
            }

            if (keyboardState.IsKeyPressedRightNow(character.Control.MeleeKey)) {
                return new MeleeState(character);
            }

            if (keyboardState.IsKeyPressedRightNow(character.Control.ShootKey)) {
                return new ShootState(character);
            }

            if (keyboardState.IsKeyPressedRightNow(character.Control.SlideKey)) {
                return new SlideState(character);
            }

            return null;
        }

        public IState Update(float deltaTime)
        {
            if (character.Velocity == Vector2.Zero) {
                return new StandState(character);
            }

            if (character.Velocity.Y != 0) {
                return new JumpState(character);
            }

            return null;
        }
    }
}
