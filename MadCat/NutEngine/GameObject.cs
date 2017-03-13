namespace NutEngine
{
    public abstract class GameObject
    {
        public AABB Collider { get; set; }

        public abstract void Update(float deltaTime);

        public bool Collide(GameObject other)
        {
            return Collide((dynamic) other);
        }
    }
}
