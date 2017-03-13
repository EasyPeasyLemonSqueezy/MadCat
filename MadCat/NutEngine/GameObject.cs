namespace NutEngine
{
    public abstract class GameObject
    {
        public AABB Collider { get; set; }

        public abstract void Update(float deltaTime);

        public void Collide(GameObject other)
        {
            Collide((dynamic) other);
        }
    }
}
