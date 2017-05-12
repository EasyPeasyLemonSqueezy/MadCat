namespace NutEngine
{
    public abstract class Component
    {
        public Entity Entity { get; set; }
        public abstract void Update(float deltaTime);
    }
}
