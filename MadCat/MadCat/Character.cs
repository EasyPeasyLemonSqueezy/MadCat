using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NutEngine;

using Microsoft.Xna.Framework.Input;
using NutInput = NutEngine.Input;
using System;

namespace MadCat
{
    public class Character : Animation, IGameObject
    {
        private enum State
        {
              STAND
            , RUN
            , JUMP
            , MELEE
            , SHOOT
            , SLIDE
        }

        private State state = State.STAND;

        private enum Direction
        {
              RIGHT =  1
            , LEFT  = -1
        }

        private Direction direction = Direction.RIGHT;

        public struct Controls
        {
            public Keys RunRightKey;
            public Keys RunLeftKey;
            public Keys JumpKey;
            public Keys ShootKey;
            public Keys MeleeKey;
            public Keys SlideKey;
        };

        public Controls Control;


        private Animation AdventureGirlIdle;
        private Animation AdventureGirlJump;
        private Animation AdventureGirlMelee;
        private Animation AdventureGirlRun;
        private Animation AdventureGirlShoot;
        private Animation AdventureGirlSlide;

        private Vector2 offset;
        private Vector2 velocity;
        private Vector2 gravitation;

        private float runVelocity  =  400.0f;
        private float jumpVelocity = -800.0f;

        public Character(Texture2D texture)
            : base(texture, new NutPacker.Content.AdventureGirl.Idle())
        {
            AdventureGirlIdle
                = new Animation(texture, new NutPacker.Content.AdventureGirl.Idle()) {
                Duration = .8f
            };
            AdventureGirlJump
                = new Animation(texture, new NutPacker.Content.AdventureGirl.Jump()) {
                Repeat = false,
                Duration = .5f
            };
            AdventureGirlMelee
                = new Animation(texture, new NutPacker.Content.AdventureGirl.Melee()) {
                Repeat = false,
                Duration = .35f
            };
            AdventureGirlRun
                = new Animation(texture, new NutPacker.Content.AdventureGirl.Run()) {
                Duration = .6f
            };
            AdventureGirlShoot
                = new Animation(texture, new NutPacker.Content.AdventureGirl.Shoot()) {
                Duration = .2f,
                Repeat = false
            };
            AdventureGirlSlide
                = new Animation(texture, new NutPacker.Content.AdventureGirl.Slide()) {
                Duration = .5f,
                Repeat = false
            };

            Control = new Controls() {
                  RunRightKey = Keys.Right
                , RunLeftKey  = Keys.Left
                , JumpKey     = Keys.Up
                , ShootKey    = Keys.Z
                , MeleeKey    = Keys.X
                , SlideKey    = Keys.LeftControl
            };

            Effects = SpriteEffects.None;

            Scale = new Vector2(.3f, .3f);
            offset = new Vector2(0, -135); // Just another magic number

            Position = offset;
            velocity = new Vector2();
            gravitation = new Vector2(0, 2000);
        }

        public void Input(NutInput.KeyboardState keyboardState)
        {
            if (state == State.STAND || state == State.RUN) {
                /// Right
                if (keyboardState.IsKeyDown(Control.RunRightKey)) {
                    Run(Direction.RIGHT);
                }

                /// Left
                if (keyboardState.IsKeyDown(Control.RunLeftKey)) {
                    Run(Direction.LEFT);
                }

                /// Jump
                if (keyboardState.IsKeyPressedRightNow(Control.JumpKey)) {
                    Jump();
                }

                /// Melee
                if (keyboardState.IsKeyPressedRightNow(Control.MeleeKey)) {
                    Melee();
                }

                /// Shoot
                if (keyboardState.IsKeyPressedRightNow(Control.ShootKey)) {
                    Shoot();
                }
            }

            if (state == State.RUN) {
                /// Slide
                if (keyboardState.IsKeyPressedRightNow(Control.SlideKey)) {
                    Slide();
                }

                /// Stand
                if (keyboardState.IsKeyReleasedRightNow(Control.RunRightKey) ||
                    keyboardState.IsKeyReleasedRightNow(Control.RunLeftKey)) {
                    Stand();
                }
            }

            if (state == State.JUMP) {
                /// Right
                if (keyboardState.IsKeyDown(Control.RunRightKey)) {
                    velocity.X = runVelocity;
                    direction = Direction.RIGHT;
                }

                /// Left
                if (keyboardState.IsKeyDown(Control.RunLeftKey)) {
                    velocity.X = -runVelocity;
                    direction = Direction.LEFT;
                }
            }
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            velocity = Physics.ApplyAccel(velocity, gravitation, deltaTime);
            Position = Physics.ApplyVelocity(Position, velocity, deltaTime);

            /// If animation stopped and we should return to standing state
            if (!Enabled) {
                if (state == State.MELEE || state == State.SHOOT || state == State.SLIDE) {
                    Stand();
                }
            }

            if (Position.Y - offset.Y >= 0) {
                velocity.Y = 0.0f;
                Position = new Vector2(Position.X + offset.X, offset.Y);

                if (state == State.JUMP) {
                    Stand();
                }
            }

            Effects = direction == Direction.RIGHT
                    ? SpriteEffects.None
                    : SpriteEffects.FlipHorizontally;
        }

        public void Collide(IGameObject other)
        {
            throw new NotImplementedException();
        }

        private void Stand()
        {
            if (state != State.STAND) {
                Change(AdventureGirlIdle);
                state = State.STAND;
                velocity.X = 0.0f;
            }
        }

        private void Run(Direction direction)
        {
            if (state != State.RUN) {
                Change(AdventureGirlRun);
                state = State.RUN;
                this.direction = direction;
                velocity.X = (int)direction * runVelocity;
            }
        }

        private void Jump()
        {
            if (state != State.JUMP) {
                Change(AdventureGirlJump);
                state = State.JUMP;
                velocity.Y = jumpVelocity;
            }
        }

        private void Melee()
        {
            if (state != State.MELEE) {
                Change(AdventureGirlMelee);
                state = State.MELEE;
                velocity.X = 0.0f;
            }
        }

        private void Shoot()
        {
            if (state != State.SHOOT) {
                Change(AdventureGirlShoot);
                state = State.SHOOT;
                velocity.X = 0.0f;
            }
        }

        private void Slide()
        {
            if (state != State.SLIDE) {
                Change(AdventureGirlSlide);
                state = State.SLIDE;
            }
        }
    }
}
