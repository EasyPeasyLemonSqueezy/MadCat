using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NutEngine;

using Microsoft.Xna.Framework.Input;
using NutInput = NutEngine.Input;
using System.Collections.Generic;
using System;

namespace MadCat
{
    public class Character : GameObject
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

        public Animation currentAnimation;

        private Animation AdventureGirlIdle;
        private Animation AdventureGirlJump;
        private Animation AdventureGirlMelee;
        private Animation AdventureGirlRun;
        private Animation AdventureGirlShoot;
        private Animation AdventureGirlSlide;

        private Texture2D TextureBullet;
        private Node node;
        private GameObjectManager manager;

        public Vector2 position;
        private Vector2 velocity;
        private Vector2 gravitation;

        private float runVelocity  =  400.0f;
        private float jumpVelocity = -800.0f;

        public Character(Texture2D texture, Texture2D textureBullet, Node node, GameObjectManager manager)
        {
            TextureBullet = textureBullet;
            this.manager = manager;
            this.node = node;

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

            /// Fuck this
            currentAnimation 
                = new Animation(texture, new NutPacker.Content.AdventureGirl.Idle()) {
                Duration = .8f
            };

            currentAnimation.Effects = SpriteEffects.None; /// Flip
            currentAnimation.Scale = new Vector2(.3f, .3f);

            node.AddChild(currentAnimation);

            Control = new Controls() {
                  RunRightKey = Keys.Right
                , RunLeftKey  = Keys.Left
                , JumpKey     = Keys.Up
                , ShootKey    = Keys.Z
                , MeleeKey    = Keys.X
                , SlideKey    = Keys.Down
            };

            position = new Vector2(0.0f, -500.0f);
            velocity = new Vector2();
            gravitation = new Vector2(0, 2000);

            Collider = new AABB() {
                  X = position.X
                , Y = position.Y
                , Width = 81.0f
                , Height = 130.0f
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
            currentAnimation.Color = Color.White;
            currentAnimation.Update(deltaTime);

            /// Stand
            if (velocity == Vector2.Zero
                && state != State.STAND
                && state != State.MELEE
                && state != State.SHOOT
                ) {
                state = State.STAND;
                currentAnimation.Change(AdventureGirlIdle);
            }

            /// Jump
            else if (
                velocity.Y != 0.0f
                && state != State.JUMP
                ) {
                state = State.JUMP;
                currentAnimation.Change(AdventureGirlJump);
            }

            /// Run
            else if (
                velocity.X != 0.0f
                && velocity.Y == 0.0f
                && state != State.RUN
                && state != State.SLIDE
                ) {
                state = State.RUN;
                currentAnimation.Change(AdventureGirlRun);
            }

            velocity = Physics.ApplyAccel(velocity, gravitation, deltaTime);
            position = Physics.ApplyVelocity(position, velocity, deltaTime);

            Collider.X = position.X - Collider.Width / 2.0f;
            Collider.Y = position.Y - Collider.Height / 2.0f;

            currentAnimation.Position = position;

            /// If animation stopped and we should return to standing state
            if (!currentAnimation.Enabled &&
                  (state == State.MELEE
                || state == State.SHOOT
                || state == State.SLIDE)
                ) {
                state = State.STAND;
                currentAnimation.Change(AdventureGirlIdle);
            }

            /// Set flipped or not
            currentAnimation.Effects = direction == Direction.RIGHT
                                     ? SpriteEffects.None
                                     : SpriteEffects.FlipHorizontally;
        }

        public void CollideWall(Wall wall)
        {
            var response = Collider.Response(wall.Collider);

            /// If we don't do this, we will stuck in the wall
            if (response.Y != 0.0f) {
                velocity.Y = 0.0f;
            }

            if (response.X != 0.0f) {
                velocity.X = 0.0f;
            }

            position += response;
            Collider.X = position.X - Collider.Width / 2.0f;
            Collider.Y = position.Y - Collider.Height / 2.0f;
        }

        /// <summary>
        /// Just testing collision rules
        /// </summary>
        public void SetColor(Color color)
        {
            currentAnimation.Color = color;
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
                currentAnimation.Change(AdventureGirlMelee);
                state = State.MELEE;
                velocity.X = 0.0f;
            }
        }

        private void Shoot()
        {
            if (state != State.SHOOT) {
                currentAnimation.Change(AdventureGirlShoot);
                state = State.SHOOT;
                velocity.X = 0.0f;

                Bullet bullet = new Bullet(TextureBullet, position, (float)direction, node);
                manager.Add(bullet);
            }
        }

        private void Slide()
        {
            if (state != State.SLIDE) {
                currentAnimation.Change(AdventureGirlSlide);
                state = State.SLIDE;
            }
        }

        public override void Cleanup()
        {
            currentAnimation.CommitSuicide();
        }
    }
}
