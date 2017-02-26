using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NutEngine;
using NutPacker;

namespace MadCat
{
    public class AdventureGirl : Animation, NutEngine.IDrawable
    {
        private ISpriteSheet AdventureGirlDead;
        private ISpriteSheet AdventureGirlIdle;
        private ISpriteSheet AdventureGirlJump;
        private ISpriteSheet AdventureGirlMelee;
        private ISpriteSheet AdventureGirlRun;
        private ISpriteSheet AdventureGirlShoot;
        private ISpriteSheet AdventureGirlSlide;

        public Vector2 Offset;

        /// Physics
        private Vector2 Velocity;
        private Vector2 Acceleration; // Gravitation
        private float MagicNumber;

        private float RunVelocity;

        private bool Hover { get { return Position.Y - Offset.Y < 0; } }
        private bool JumpInProgress  { get { return SpriteSheet == AdventureGirlJump  && Enabled; } }
        private bool SlideInProgress { get { return SpriteSheet == AdventureGirlSlide && Enabled; } }
        private bool ShootInProgress { get { return SpriteSheet == AdventureGirlShoot && Enabled; } }
        private bool MeleeInProgress { get { return SpriteSheet == AdventureGirlMelee && Enabled; } }
        private bool DeathInProgress { get { return SpriteSheet == AdventureGirlDead  && Enabled; } }
        private bool SmthInProgress  {
            get {
                return JumpInProgress
                    || SlideInProgress
                    || ShootInProgress
                    || MeleeInProgress
                    || DeathInProgress
                    || Hover;
            }
        }

        public AdventureGirl(Texture2D Texture) : base(Texture, new NutPacker.Content.AdventureGirl.Idle())
        {
            AdventureGirlDead  = new NutPacker.Content.AdventureGirl.Dead();
            AdventureGirlIdle  = new NutPacker.Content.AdventureGirl.Idle();
            AdventureGirlJump  = new NutPacker.Content.AdventureGirl.Jump();
            AdventureGirlMelee = new NutPacker.Content.AdventureGirl.Melee();
            AdventureGirlRun   = new NutPacker.Content.AdventureGirl.Run();
            AdventureGirlShoot = new NutPacker.Content.AdventureGirl.Shoot();
            AdventureGirlSlide = new NutPacker.Content.AdventureGirl.Slide();

            Offset = new Vector2(0, -135); // Just another magic number
            Position += Offset;
            Scale = new Vector2(.3f, .3f);

            RunVelocity = 400;
            MagicNumber = 200;

            /// Physics
            Velocity = new Vector2();
            Acceleration = new Vector2(0, 10) * MagicNumber;
        }

        public void Stay()
        {
            if (SpriteSheet != AdventureGirlIdle && !SmthInProgress) {
                SpriteSheet = AdventureGirlIdle;
                Restart();
                Repeat = true;
                Duration = .8f;
            }
        }

        public void RunRight(float deltaTime)
        {
            if (!SlideInProgress) {
                Position += new Vector2(RunVelocity * deltaTime, 0);
                Effects = SpriteEffects.None;
            }

            if (SpriteSheet != AdventureGirlRun && !SmthInProgress) {
                SpriteSheet = AdventureGirlRun;
                Restart();
                Repeat = true;
                Duration = .6f;
            }
        }

        public void RunLeft(float deltaTime)
        {
            if (!SlideInProgress) {
                Position += new Vector2(-RunVelocity * deltaTime, 0);
                Effects = SpriteEffects.FlipHorizontally;
            }

            if (SpriteSheet != AdventureGirlRun && !SmthInProgress) {
                SpriteSheet = AdventureGirlRun;
                Restart();
                Repeat = true;
                Duration = .6f;
            }
        }

        public void Jump()
        {
            if (Position.Y == Offset.Y) {
                Velocity += new Vector2(0, -4) * MagicNumber;
                SpriteSheet = AdventureGirlJump;
                Repeat = false;
                Duration = .5f;
            }
        }

        public void Melee()
        {
            if (SpriteSheet != AdventureGirlMelee && !SmthInProgress) {
                SpriteSheet = AdventureGirlMelee;
                Repeat = false;
                Duration = .35f;
            }
        }

        public void Shoot()
        {
            if (SpriteSheet != AdventureGirlShoot && !SmthInProgress) {
                SpriteSheet = AdventureGirlShoot;
                Repeat = false;
                Duration = .2f;
            }
        }

        public void SlideLeft()
        {
            if (SpriteSheet != AdventureGirlShoot && !SmthInProgress) {
                SpriteSheet = AdventureGirlSlide;
                Repeat = false;
                Duration = .5f;

                Effects = SpriteEffects.FlipHorizontally;
            }
        }

        public void SlideRight()
        {
            if (SpriteSheet != AdventureGirlShoot && !SmthInProgress) {
                SpriteSheet = AdventureGirlSlide;
                Repeat = false;
                Duration = .5f;

                Effects = SpriteEffects.None;
            }
        }

        public override void Update(float deltaTime)
        {
            var velocity = Physics.Velocity(Velocity, Acceleration, deltaTime);
            var position = Physics.Position(Position, Velocity, Acceleration, deltaTime);

            if (SlideInProgress) {
                if (Effects == SpriteEffects.FlipHorizontally) {
                    Position += new Vector2(-RunVelocity * 1.5f * deltaTime, 0); // left
                }
                else {
                    Position += new Vector2(RunVelocity * 1.5f * deltaTime, 0); // right
                }
            }

            if (position.Y - Offset.Y >= 0) {
                if (JumpInProgress) {
                    SpriteSheet = AdventureGirlIdle;
                }

                Velocity = new Vector2();
                Position = new Vector2(Position.X + Offset.X, Offset.Y);
            }
            else {
                Position = position;
                Velocity = velocity;
            }

            base.Update(deltaTime);
        }
    }
}
