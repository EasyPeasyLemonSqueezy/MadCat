using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NutPacker;

namespace NutEngine
{
    public class Animation : Node, IDrawable
    {
        public Texture2D Atlas { get; }

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
                var center = currentFrame.Center - currentFrame.Location;
                Origin = new Vector2(center.X, center.Y);
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

            var center = CurrentFrame.Center - CurrentFrame.Location;
            Origin = new Vector2(center.X, center.Y);

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
