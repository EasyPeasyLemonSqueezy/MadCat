using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NutEngine;

using Microsoft.Xna.Framework.Input;
using NutInput = NutEngine.Input;

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

        private StateMachine stateMachine;

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

        public Animation CurrentAnimation { get; }
        private Label name;

        private Node node;
        private GameObjectManager manager;

        public Vector2 position;
        private Vector2 velocity;
        private Vector2 gravitation;

        private float runVelocity  =  400.0f;
        private float jumpVelocity = -800.0f;

        public Character(Node node, GameObjectManager manager)
        {
            stateMachine = new StateMachine(new StandState(this));

            this.manager = manager;
            this.node = node;

            CurrentAnimation 
                = new Animation(Assets.Texture, new NutPacker.Content.AdventureGirl.Idle()) {
                Duration = .8f
            };

            CurrentAnimation.Effects = SpriteEffects.None; /// Flip
            CurrentAnimation.Scale = new Vector2(.3f, .3f);

            node.AddChild(CurrentAnimation);

            name = new Label(Assets.Font, "MadCat") {
                ZOrder = 1,
                Color = Color.BlueViolet,
                Position = new Vector2(0, -300),
                Scale = new Vector2(2)
            };

            CurrentAnimation.AddChild(name);

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
            CurrentAnimation.Color = Color.White;
            CurrentAnimation.Update(deltaTime);

            /// Stand
            if (velocity == Vector2.Zero
                && state != State.STAND
                && state != State.MELEE
                && state != State.SHOOT
                ) {
                state = State.STAND;
                CurrentAnimation.Change(Assets.AdventureGirlIdle);
            }

            /// Jump
            else if (
                velocity.Y != 0.0f
                && state != State.JUMP
                ) {
                state = State.JUMP;
                CurrentAnimation.Change(Assets.AdventureGirlJump);
            }

            /// Run
            else if (
                velocity.X != 0.0f
                && velocity.Y == 0.0f
                && state != State.RUN
                && state != State.SLIDE
                ) {
                state = State.RUN;
                CurrentAnimation.Change(Assets.AdventureGirlRun);
            }

            velocity = Physics.ApplyAccel(velocity, gravitation, deltaTime);
            position = Physics.ApplyVelocity(position, velocity, deltaTime);

            Collider.X = position.X - Collider.Width / 2.0f;
            Collider.Y = position.Y - Collider.Height / 2.0f;

            CurrentAnimation.Position = position;

            /// If animation stopped and we should return to standing state
            if (!CurrentAnimation.Enabled &&
                  (state == State.MELEE
                || state == State.SHOOT
                || state == State.SLIDE)
                ) {
                state = State.STAND;
                CurrentAnimation.Change(Assets.AdventureGirlIdle);
            }

            /// Set flipped or not
            CurrentAnimation.Effects = direction == Direction.RIGHT
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
            CurrentAnimation.Color = color;
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
                CurrentAnimation.Change(Assets.AdventureGirlMelee);
                state = State.MELEE;
                velocity.X = 0.0f;
            }
        }

        private void Shoot()
        {
            if (state != State.SHOOT) {
                CurrentAnimation.Change(Assets.AdventureGirlShoot);
                state = State.SHOOT;
                velocity.X = 0.0f;

                Bullet bullet = new Bullet(position, (float)direction, node);
                manager.Add(bullet);
            }
        }

        private void Slide()
        {
            if (state != State.SLIDE) {
                CurrentAnimation.Change(Assets.AdventureGirlSlide);
                state = State.SLIDE;
            }
        }

        public override void Cleanup()
        {
            CurrentAnimation.CommitSuicide();
        }
    }
}
