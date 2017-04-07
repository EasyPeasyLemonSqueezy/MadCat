using System;
using Microsoft.Xna.Framework;
using NutEngine;
using NutEngine.Input;

namespace MadCat
{
    public class RunState : IState
    {
        private CharacterComponent character;

        public RunState(CharacterComponent character)
        {
            this.character = character;
        }

        public void Enter()
        {
            var animation = character.Entity.GetComponent<AnimationComponent>().Animation;
            animation.Change(Assets.AdventureGirlRun);
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

            if (keyboardState.IsKeyPressedRightNow(character.Control.SlideKey)) {
                return new SlideState(character);
            }

            return null;
        }

        public IState Update(float deltaTime)
        {
            var velocity = character.Entity.GetComponent<VelocityComponent>();

            if (velocity.Velocity == Vector2.Zero) {
                return new StandState(character);
            }

            if (velocity.Velocity.Y != 0) {
                return new JumpState(character);
            }

            return null;
        }
    }
}
