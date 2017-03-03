using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NutEngine;



using Microsoft.Xna.Framework.Input;
using NutInput = NutEngine.Input;
using NutPacker.Content;
using System.Collections.Generic;

namespace MadCat
{
    public class LastAdventureGirl : Node, NutEngine.IDrawable
    {

        List<Keys> Buttons = new List<Keys>();

        enum b { Shoot, RunRight, RunLeft, Jump, Melee, Slide };

        private Animation CurrentAnimation;

        private Animation AdventureGirlIdle;
        private Animation AdventureGirlJump;
        private Animation AdventureGirlMelee;
        private Animation AdventureGirlRun;
        private Animation AdventureGirlShoot;
        private Animation AdventureGirlSlide;

        public Vector2 Offset;

        /// Physics
        private Vector2 Velocity;
        private Vector2 Acceleration; // Gravitation
        private float MagicNumber;
        private SpriteEffects Effects;

        private float RunVelocity;

        private bool Hover { get { return Position.Y - Offset.Y < 0; } }
        private bool JumpInProgress { get { return CurrentAnimation == AdventureGirlJump && CurrentAnimation.Enabled; } }
        private bool SlideInProgress { get { return CurrentAnimation == AdventureGirlSlide && CurrentAnimation.Enabled; } }
        private bool ShootInProgress { get { return CurrentAnimation == AdventureGirlShoot && CurrentAnimation.Enabled; } }
        private bool MeleeInProgress { get { return CurrentAnimation == AdventureGirlMelee && CurrentAnimation.Enabled; } }
        private bool SmthInProgress
        {
            get
            {
                return JumpInProgress
                    || SlideInProgress
                    || ShootInProgress
                    || MeleeInProgress
                    || Hover;
            }
        }

        public LastAdventureGirl(Texture2D Texture) : base()
        {
            var size = new Vector2(100f, 162f);

            AdventureGirlIdle = new Animation(Texture, new NutPacker.Content.AdventureGirl.Idle())
            {
                Duration = .8f
            };
            AdventureGirlJump = new Animation(Texture, new NutPacker.Content.AdventureGirl.Jump())
            {
                Repeat = false
                ,
                Duration = .5f
            };
            AdventureGirlMelee = new Animation(Texture, new NutPacker.Content.AdventureGirl.Melee())
            {
                Repeat = false
                ,
                Duration = .35f
            };
            AdventureGirlRun = new Animation(Texture, new NutPacker.Content.AdventureGirl.Run())
            {
                Duration = .6f
            };
            AdventureGirlShoot = new Animation(Texture, new NutPacker.Content.AdventureGirl.Shoot())
            {
                Duration = .2f
                ,
                Repeat = false
            };
            AdventureGirlSlide = new Animation(Texture, new NutPacker.Content.AdventureGirl.Slide())
            {
                Duration = .5f
                ,
                Repeat = false
            };

            CurrentAnimation = AdventureGirlIdle;

            Effects = SpriteEffects.None;

            Offset = new Vector2(0, -135); // Just another magic number
            Position += Offset;
            Scale = new Vector2(.3f, .3f);

            RunVelocity = 400;
            MagicNumber = 200;

            /// Physics
            Velocity = new Vector2();
            Acceleration = new Vector2(0, 10) * MagicNumber;

            Buttons.Add(Keys.O);
            Buttons.Add(Keys.Right);
            Buttons.Add(Keys.Left);
            Buttons.Add(Keys.Space);
            Buttons.Add(Keys.RightShift);
            Buttons.Add(Keys.Down);

        }

        public LastAdventureGirl(Texture2D Texture,Keys bShoot,Keys bRunRight,Keys bRunLeft,Keys bJump, Keys bMelee, Keys bSlide) : base()
        {
            var size = new Vector2(100f, 162f);

            AdventureGirlIdle = new Animation(Texture, new NutPacker.Content.AdventureGirl.Idle())
            {
                Duration = .8f
            };
            AdventureGirlJump = new Animation(Texture, new NutPacker.Content.AdventureGirl.Jump())
            {
                Repeat = false
                ,
                Duration = .5f
            };
            AdventureGirlMelee = new Animation(Texture, new NutPacker.Content.AdventureGirl.Melee())
            {
                Repeat = false
                ,
                Duration = .35f
            };
            AdventureGirlRun = new Animation(Texture, new NutPacker.Content.AdventureGirl.Run())
            {
                Duration = .6f
            };
            AdventureGirlShoot = new Animation(Texture, new NutPacker.Content.AdventureGirl.Shoot())
            {
                Duration = .2f
                ,
                Repeat = false
            };
            AdventureGirlSlide = new Animation(Texture, new NutPacker.Content.AdventureGirl.Slide())
            {
                Duration = .5f
                ,
                Repeat = false
            };

            CurrentAnimation = AdventureGirlIdle;

            Effects = SpriteEffects.None;

            Offset = new Vector2(0, -135); // Just another magic number
            Position += Offset;
            Scale = new Vector2(.3f, .3f);

            RunVelocity = 400;
            MagicNumber = 200;

            /// Physics
            Velocity = new Vector2();
            Acceleration = new Vector2(0, 10) * MagicNumber;
           

            Buttons.Add(bShoot);
            Buttons.Add(bRunRight);
            Buttons.Add(bRunLeft);
            Buttons.Add(bJump);
            Buttons.Add(bMelee);
            Buttons.Add(bSlide);
        }



        public void Stay()
        {
            if (CurrentAnimation != AdventureGirlIdle && !SmthInProgress)
            {
                ChangeAnimation(AdventureGirlIdle);
            }
        }

        public void RunRight(float deltaTime)
        {
            if (!SlideInProgress)
            {
                Position += new Vector2(RunVelocity * deltaTime, 0);
                Effects = SpriteEffects.None;
            }

            if (CurrentAnimation != AdventureGirlRun && !SmthInProgress)
            {
                ChangeAnimation(AdventureGirlRun);
            }
        }

        public void RunLeft(float deltaTime)
        {
            if (!SlideInProgress)
            {
                Position += new Vector2(-RunVelocity * deltaTime, 0);
                Effects = SpriteEffects.FlipHorizontally;
            }

            if (CurrentAnimation != AdventureGirlRun && !SmthInProgress)
            {
                ChangeAnimation(AdventureGirlRun);
            }
        }

        public void Jump()
        {
            if (Position.Y == Offset.Y)
            {
                Velocity += new Vector2(0, -4) * MagicNumber;

                ChangeAnimation(AdventureGirlJump);
            }
        }

        public void Melee()
        {
            if (CurrentAnimation != AdventureGirlMelee && !SmthInProgress)
            {
                ChangeAnimation(AdventureGirlMelee);
            }
        }

        public void Shoot()
        {
            if (CurrentAnimation != AdventureGirlShoot && !SmthInProgress)
            {
                ChangeAnimation(AdventureGirlShoot);
            }
        }

        public void SlideLeft()
        {
            if (CurrentAnimation != AdventureGirlSlide && !SmthInProgress)
            {
                Effects = SpriteEffects.FlipHorizontally;

                ChangeAnimation(AdventureGirlSlide);
            }
        }

        public void SlideRight()
        {
            if (CurrentAnimation != AdventureGirlSlide && !SmthInProgress)
            {
                Effects = SpriteEffects.None;

                ChangeAnimation(AdventureGirlSlide);
            }
        }

        private void ChangeAnimation(Animation Animation)
        {
            CurrentAnimation = Animation;
            CurrentAnimation.Restart();
        }

        public void Update(float deltaTime)
        {
            var velocity = Physics.Velocity(Velocity, Acceleration, deltaTime);
            var position = Physics.Position(Position, Velocity, Acceleration, deltaTime);

            if (SlideInProgress)
            {
                if (Effects == SpriteEffects.FlipHorizontally)
                {
                    Position += new Vector2(-RunVelocity * 1.5f * deltaTime, 0); // left
                }
                else
                {
                    Position += new Vector2(RunVelocity * 1.5f * deltaTime, 0); // right
                }
            }

            if (position.Y - Offset.Y >= 0)
            {
                if (JumpInProgress)
                {
                    ChangeAnimation(AdventureGirlIdle);
                }

                Velocity = new Vector2();
                Position = new Vector2(Position.X + Offset.X, Offset.Y);
            }
            else
            {
                Position = position;
                Velocity = velocity;
            }

            CurrentAnimation.Effects = Effects;
            CurrentAnimation.Update(deltaTime);




            var keyboardState = NutInput.Keyboard.GetState();

            /// Jump
            if (keyboardState.IsKeyPressedRightNow(Buttons[(int)b.Jump]))
            {
                Jump();
            }

            /// Shoot
            if (keyboardState.IsKeyPressedRightNow(Buttons[(int)b.Shoot]))
            {
                Shoot();
            }

            /// Melee
            if (keyboardState.IsKeyPressedRightNow(Buttons[(int)b.Melee]))
            {
                Melee();
            }

            /// Right
            if (keyboardState.IsKeyDown(Buttons[(int)b.RunRight]))
            {
                if (keyboardState.IsKeyDown(Buttons[(int)b.Slide]))
                {
                    SlideRight();
                }
                else
                {
                    RunRight(deltaTime);
                }
            }

            /// Left
            if (keyboardState.IsKeyDown(Buttons[(int)b.RunLeft]))
            {
                if (keyboardState.IsKeyDown(Buttons[(int)b.Slide]))
                {
                    SlideLeft();
                }
                else
                {
                    RunLeft(deltaTime);
                }
            }

            /// Stand
            if (keyboardState.GetPressedKeys().Length == 0)
            {
                Stay();
            }

        }

        public void Draw(SpriteBatch spriteBatch, Matrix2D currentTransform)
        {
            CurrentAnimation.Draw(spriteBatch, currentTransform);
        }
    }
}
