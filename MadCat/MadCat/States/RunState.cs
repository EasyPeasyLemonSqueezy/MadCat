using System;
using Microsoft.Xna.Framework;
using NutEngine;
using NutEngine.Input;

namespace MadCat
{
    public class RunState : IState
    {
        private Entity entity;

        public RunState(Entity entity)
        {
            this.entity = entity;
        }

        public void Enter()
        {
            var animation = entity.GetComponent<AnimationComponent>().Animation;
            animation.Change(Assets.AdventureGirlRun);
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

            if (keyboardState.IsKeyPressedRightNow(character.Control.SlideKey)) {
                return new SlideState(entity);
            }

            return null;
        }

        public IState Update(float deltaTime)
        {
            var character = entity.GetComponent<CharacterComponent>();
            var body = entity.GetComponent<ColliderComponent>().Body;


            if (body.Velocity == Vector2.Zero) {
                return new StandState(entity);
            }

            if (body.Velocity.Y != 0) {
                return new JumpState(entity);
            }

            return null;
        }
    }
}
