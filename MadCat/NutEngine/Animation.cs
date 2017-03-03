using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NutPacker;

namespace NutEngine
{
    public class Animation : Sprite
    {
        private ISpriteSheet spriteSheet;
        public ISpriteSheet SpriteSheet {
            get {
                return spriteSheet;
            }
            set {
                spriteSheet = value;
                ElapsedTime = 0;
                CurrentIndex = 0;

                var currentFrame = value[CurrentIndex];
                Origin = new Vector2(currentFrame.Width / 2f, currentFrame.Height / 2f);
            }
        }

        private float ElapsedTime;

        private int currentIndex;
        private int CurrentIndex {
            get {
                return currentIndex;
            }
            set {
                currentIndex = value;
                TextureRegion.Frame = SpriteSheet[value];
            }
        }

        public Rectangle CurrentFrame {
            get {
                return SpriteSheet[CurrentIndex];
            }
        }

        public bool Repeat { get; set; }
        public bool Enabled { get; set; }
        public float Duration { get; set; }
        public AnimationType AnimationType { get; set; }


        public Animation(Texture2D texture, ISpriteSheet spriteSheet)
            : base(texture, spriteSheet[0])
        {
            SpriteSheet = spriteSheet;
            Initialize();
        }

        protected new void Initialize()
        {
            base.Initialize();

            ElapsedTime = 0;

            CurrentIndex = 0;
            Duration = 1;
            Repeat = true;
            Enabled = true;
            AnimationType = AnimationTypes.Linear;
        }

        public void Start()
        {
            Enabled = true;
        }

        public void Stop()
        {
            Enabled = false;
            ElapsedTime = 0;
            CurrentIndex = 0;
        }

        public void Pause()
        {
            Enabled = false;
        }

        public void Restart()
        {
            Enabled = true;
            ElapsedTime = 0;
            CurrentIndex = 0;
        }

        public void Change(Animation animation)
        {
            TextureRegion = animation.TextureRegion;
            SpriteSheet   = animation.SpriteSheet;
            Duration      = animation.Duration;
            Repeat        = animation.Repeat;
            Enabled       = animation.Enabled;
            AnimationType = animation.AnimationType;
            Color         = animation.Color;
            Effects       = animation.Effects;
            LayerDepth    = animation.LayerDepth;
        }

        public virtual void Update(float deltaTime)
        {
            if (Enabled) {
                ElapsedTime += deltaTime;

                if (ElapsedTime >= Duration) {
                    if (Repeat) {
                        ElapsedTime %= Duration;
                        CurrentIndex = AnimationType(SpriteSheet.Length, Duration, ElapsedTime);
                    }
                    else {
                        ElapsedTime = 0;
                        CurrentIndex = SpriteSheet.Length - 1;
                        Enabled = false;
                    }
                }
                else {
                    CurrentIndex = AnimationType(SpriteSheet.Length, Duration, ElapsedTime);
                }
            }
        }
    }
}
