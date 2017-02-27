using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NutPacker;

namespace NutEngine
{
    public class Animation : Node, IDrawable
    {
        public Texture2D Atlas { get; private set; }

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

        private int CurrentIndex;
        public Rectangle CurrentFrame {
            get {
                return SpriteSheet[CurrentIndex];
            }
        }

        public bool Repeat { get; set; }
        public bool Enabled { get; set; }
        public float Duration { get; set; }
        public AnimationType AnimationType { get; set; }

        public Color Color { get; set; }

        /// Center of the rotation, by default - center of the first frame.
        public Vector2 Origin { get; set; }
        public SpriteEffects Effects { get; set; }
        public float LayerDepth { get; set; }


        public Animation(Texture2D atlas, ISpriteSheet spriteSheet) : base()
        {
            Atlas = atlas;
            SpriteSheet = spriteSheet;

            ElapsedTime = 0;

            CurrentIndex = 0;
            Duration = 1;
            Repeat = true;
            Enabled = true;
            AnimationType = AnimationTypes.Linear;

            Color = Color.White;

            Effects = SpriteEffects.None;
            LayerDepth = 0;
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
            Atlas = animation.Atlas;
            SpriteSheet = animation.SpriteSheet;
            Duration = animation.Duration;
            Repeat = animation.Repeat;
            Enabled = animation.Enabled;
            AnimationType = animation.AnimationType;
            Color = animation.Color;
            Effects = animation.Effects;
            LayerDepth = animation.LayerDepth;
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

        public void Draw(SpriteBatch spriteBatch, Transform2D currentTransform)
        {
            Vector2 position, scale;
            float rotation;

            currentTransform.Decompose(out scale, out rotation, out position);

            spriteBatch.Draw(
                  Atlas
                , position
                , CurrentFrame
                , Color
                , rotation
                , Origin
                , scale
                , Effects
                , LayerDepth);
        }
    }
}
