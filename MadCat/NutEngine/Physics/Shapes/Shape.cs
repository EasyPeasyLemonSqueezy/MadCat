namespace NutEngine.Physics.Shapes
{
    public abstract class Shape
    {
        public abstract AABB Sector { get; }

        public bool Collide(Shape shape)
        {
            return Collider.Collide((dynamic)this, (dynamic)shape);
        }
    }
}
