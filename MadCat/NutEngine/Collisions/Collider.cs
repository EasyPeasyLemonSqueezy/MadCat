namespace NutEngine
{
    public abstract class Collider
    {
        public bool Intersects(Collider other)
        {
            return other.Intersects(this);
        }
    }
}
