using System;

namespace NutEngine
{
    public abstract class Component
    {
        public Entity Entity { get; set; }
        public virtual Type[] GetDependencies() { return new Type[0]; }
        public abstract void Update(float deltaTime);
    }
}
