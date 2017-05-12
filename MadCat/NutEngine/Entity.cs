using System;
using System.Collections.Generic;

namespace NutEngine
{
    public class Entity : ICleanup
    {
        public bool Invalid { get; set; }

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

        public void AddComponent(Component component)
        {
            component.Entity = this;
            components[component.GetType()] = component;
        }

        public void AddComponent<T>(params object[] parameters)
            where T : Component
        {
            var component = (T)Activator.CreateInstance(typeof(T), parameters);
            component.Entity = this;
            components[component.GetType()] = component;
        }

        public void RemoveComponent<T>()
            where T : Component
        {
            components.Remove(typeof(T));
        }

        public bool HasComponent<T>()
            where T : Component
        {
            return components.ContainsKey(typeof(T));
        }

        public T GetComponent<T>()
            where T : Component
        {
            if (HasComponent<T>()) {
                return components[typeof(T)] as T;
            }
            return null;
        }
    }
}
