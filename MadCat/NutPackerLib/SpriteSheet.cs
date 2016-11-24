namespace NutPacker
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Contains array of <see cref="Rectangle"/>,
    /// Length property, and indexed property getter for it.
    /// </summary>
    public abstract class SpriteSheet
    {
        protected Rectangle[] Frames;

        public int Length {
            get {
                return Frames.Length;
            }
        }

        public Rectangle this[int index] {
            get {
                return Frames[index];
            }
        }
    }
}
