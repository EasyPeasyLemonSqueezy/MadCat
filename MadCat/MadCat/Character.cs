using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NutEngine;

using Microsoft.Xna.Framework.Input;
using NutInput = NutEngine.Input;

namespace MadCat
{
    public class Character : Animation
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

        private Vector2 velocity;
        private Vector2 gravitation;

        private float runVelocity  =  400.0f;
        private float jumpVelocity = -800.0f;

        private AABB bounds;

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

            /// Flip
            Effects = SpriteEffects.None;

            Scale = new Vector2(.3f, .3f);

            Position = new Vector2(0.0f, -500.0f);
            velocity = new Vector2();
            gravitation = new Vector2(0, 2000);

            bounds = new AABB() {
                  X = Position.X
                , Y = Position.Y
                , Width = 81.0f
                , Height = 135.0f
            };
        }

        public void Input(NutInput.KeyboardState keyboardState)
        {
            if (state == State.STAND || state == State.RUN || state == State.JUMP)
            {
                /// Right
                if (keyboardState.IsKeyDown(Control.RunRightKey)) {
                    Run(Direction.RIGHT);
                }
                /// Left
                else if (keyboardState.IsKeyDown(Control.RunLeftKey)) {
                    Run(Direction.LEFT);
                }
                /// Stop
                else {
                    Stand();
                }
            }

            if (state == State.STAND || state == State.RUN) {
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
            }
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            /// Stand
            if (velocity == Vector2.Zero
                && state != State.STAND
                && state != State.MELEE
                && state != State.SHOOT
                ) {
                state = State.STAND;
                Change(AdventureGirlIdle);
            }

            /// Jump
            else if (
                velocity.Y != 0.0f
                && state != State.JUMP
                ) {
                state = State.JUMP;
                Change(AdventureGirlJump);
            }

            /// Run
            else if (
                velocity.X != 0.0f
                && state != State.RUN
                && state != State.SLIDE
                ) {
                state = State.RUN;
                Change(AdventureGirlRun);
            }

            velocity = Physics.ApplyAccel(velocity, gravitation, deltaTime);
            Position = Physics.ApplyVelocity(Position, velocity, deltaTime);

            bounds.X = Position.X - bounds.Width / 2.0f;
            bounds.Y = Position.Y - bounds.Height / 2.0f;

            /// If animation stopped and we should return to standing state
            if (!Enabled &&
                  (state == State.MELEE
                || state == State.SHOOT
                || state == State.SLIDE)
                ) {
                state = State.STAND;
                Change(AdventureGirlIdle);
            }

            /// Set flipped or not
            Effects = direction == Direction.RIGHT
                    ? SpriteEffects.None
                    : SpriteEffects.FlipHorizontally;
        }

        public void Collide(Wall wall)
        {
            if (bounds.Intersects(wall.Bounds)) {
                var response = bounds.Response(wall.Bounds);

                /// If we don't do this, we will stuck in the wall
                if (response.Y != 0.0f) {
                    velocity.Y = 0.0f;
                }

                if (response.X != 0.0f) {
                    velocity.X = 0.0f;
                }

                Position += bounds.Response(wall.Bounds);
                bounds.X = Position.X - bounds.Width  / 2.0f;
                bounds.Y = Position.Y - bounds.Height / 2.0f;
            }
        }

        private void Stand()
        {
            velocity.X = 0.0f;
        }

        private void Run(Direction direction)
        {
            this.direction = direction;
            velocity.X = (int)direction * runVelocity;
        }

        private void Jump()
        {
            velocity.Y = jumpVelocity;
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
