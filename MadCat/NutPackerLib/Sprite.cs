namespace NutPacker
{
    using System.Drawing;

    /// <summary>
    /// Contains array of <see cref="Rectangle"/> and indexed property getter for it.
    /// </summary>
    public abstract class Sprite
    {
        protected Rectangle[] Frames;

        public Rectangle this[int index] {
            get {
                return Frames[index];
            }
        }
    }
}
