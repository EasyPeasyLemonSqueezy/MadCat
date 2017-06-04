using System;
using NutEngine;
using NutEngine.Input;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace MadCat
{
    public class InputComponent : Component
    {
        private const float runVelocity = 200.0f;

        public override void Update(float deltaTime)
        {
            var body = Entity.GetComponent<BodyComponent>().Body;
            //var weapon = Entity.GetComponent<WeaponComponent>();

            var keyboardState = NutEngine.Input.Keyboard.State;

            var velocity = new Vector2();

            if (keyboardState.IsKeyDown(Keys.A)) {
                velocity.X = -runVelocity;
            }
            else if (keyboardState.IsKeyDown(Keys.D)) {
                velocity.X = runVelocity;
            }
            else {
                velocity.X = 0;
            }

            if (keyboardState.IsKeyDown(Keys.W)) {
                velocity.Y = -runVelocity;
            }
            else if (keyboardState.IsKeyDown(Keys.S)) {
                velocity.Y = runVelocity;
            }
            else {
                velocity.Y = 0;
            }

            body.Velocity = velocity;

            //var mouseState = Mouse.GetState();
            //weapon.Shooting = mouseState.LeftButton == ButtonState.Pressed;
        }
    }
}
