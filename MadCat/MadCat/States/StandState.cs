﻿using System;
using NutEngine.Input;
using NutEngine;

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

            return null;
        }

        public IState Update(float deltaTime)
        {
            if (character.Velocity.Y != 0) {
                return new JumpState(character);
            }

            if (character.Velocity.X != 0 &&
                character.Velocity.Y == 0) {
                return new RunState(character);
            }

            return null;
        }
    }
}