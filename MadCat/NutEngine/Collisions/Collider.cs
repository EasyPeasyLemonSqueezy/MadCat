namespace NutEngine
{
    /// <summary>
    /// Base class for all shapes that can collide
    /// </summary>
    public abstract class Collider
    {
        public bool Intersects(Collider other)
        {
            return other.Intersects(this);
        }
    }
}
