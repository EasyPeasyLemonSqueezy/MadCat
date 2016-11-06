namespace NutPacker
{
    using System.Drawing;

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
