using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NutEngine;
using NutPacker;

namespace MadCat
{
    public class AdventureGirl : Animation, NutEngine.IDrawable
    {
        private SpriteSheet AdventureGirlDead;
        private SpriteSheet AdventureGirlIdle;
        private SpriteSheet AdventureGirlJump;
        private SpriteSheet AdventureGirlMelee;
        private SpriteSheet AdventureGirlRun;
        private SpriteSheet AdventureGirlShoot;
        private SpriteSheet AdventureGirlSlide;

        /// Physics
        private Vector2 Speed;
        private Vector2 Acceleration;

        public AdventureGirl(Texture2D Texture) : base(Texture, new NutPacker.Content.AdventureGirl.Idle())
        {
            AdventureGirlDead  = new NutPacker.Content.AdventureGirl.Dead();
            AdventureGirlIdle  = new NutPacker.Content.AdventureGirl.Idle();
            AdventureGirlJump  = new NutPacker.Content.AdventureGirl.Jump();
            AdventureGirlMelee = new NutPacker.Content.AdventureGirl.Melee();
            AdventureGirlRun   = new NutPacker.Content.AdventureGirl.Run();
            AdventureGirlShoot = new NutPacker.Content.AdventureGirl.Shoot();
            AdventureGirlSlide = new NutPacker.Content.AdventureGirl.Slide();

            Scale = new Vector2(.3f, .3f);

            /// Physics
            Speed = new Vector2();
            Acceleration = new Vector2(0, 10);
        }

        public void Stay(float deltaTime)
        {
            if (SpriteSheet != AdventureGirlIdle) {
                SpriteSheet = AdventureGirlIdle;
            }
        }

        public void RunRight(float deltaTime)
        {
            Position += new Vector2(300 * deltaTime, 0);

            if (SpriteSheet != AdventureGirlRun) {
                SpriteSheet = AdventureGirlRun;
            }

            Effects = SpriteEffects.None;
        }

        public void RunLeft(float deltaTime)
        {
            Position += new Vector2(-300 * deltaTime, 0);

            if (SpriteSheet != AdventureGirlRun) {
                SpriteSheet = AdventureGirlRun;
            }

            Effects = SpriteEffects.FlipHorizontally;
        }

        public void Jump()
        {
            Speed += new Vector2(0, -50);
            SpriteSheet = AdventureGirlJump;
        }

        public void Melee(float deltaTime)
        {
            SpriteSheet = AdventureGirlMelee;
        }

        public void Shoot(float deltaTime)
        {
            SpriteSheet = AdventureGirlShoot;
        }

        public void SlideLeft(float deltaTime) { }

        public void SlideRight(float deltaTime) { }

        public override void Update(float deltaTime)
        {
            //var position = Physics.Position(Position, Speed, Acceleration, deltaTime);
            //var speed = Physics.Speed(Speed, Acceleration, deltaTime);

            

            base.Update(deltaTime);
        }
    }
}
