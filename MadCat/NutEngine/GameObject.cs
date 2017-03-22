namespace NutEngine
{
    /// <summary>
    /// Base class for all game object that can collide
    /// and can be updated
    /// </summary>
    public abstract class GameObject
    {
        public bool Invalid { get; set; }
        public AABB Collider { get; set; }
        public abstract void Update(float deltaTime);
    }
}
