using System;
using System.Collections.Generic;

namespace NutEngine
{
    public class Entity : ICleanup
    {
        public bool Invalid { get; set; }
        public AABB Collider { get; set; }

        private Dictionary<Type, Component> components =
            new Dictionary<Type, Component>();

        public virtual void Update(float deltaTime)
        {
            foreach (var component in components.Values) {
                component.Update(deltaTime);
            }
        }

        public virtual void Cleanup()
        {
            foreach (var component in components.Values) {
                if (component is ICleanup cleanup) {
                    cleanup.Cleanup();
                }
            }
        }

        public Entity AddComponent(Component component)
        {
            component.Entity = this;
            components[component.GetType()] = component;
            return this;
        }

        public Entity RemoveComponent<T>()
        {
            components.Remove(typeof(T));
            return this;
        }

        public bool HasComponent<T>()
        {
            return components.ContainsKey(typeof(T));
        }

        public Component GetComponent<T>()
        {
            if (HasComponent<T>()) {
                return components[typeof(T)];
            }
            return null;
        }
    }
}
