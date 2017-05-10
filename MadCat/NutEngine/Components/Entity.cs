using System;
using System.Collections.Generic;
using System.Linq;

namespace NutEngine
{
    public class Entity : IDisposable
    {
        public bool Invalid { get; set; }
        public EntityManager Manager { get; set; }

        private Dictionary<Type, Component> components =
            new Dictionary<Type, Component>();

        public virtual void Update(float deltaTime)
        {
            foreach (var component in components.Values) {
                component.Update(deltaTime);
            }
        }

        public virtual void Dispose()
        {
            foreach (var component in components.Values) {
                if (component is IDisposable disposable) {
                    disposable.Dispose();
                }
            }
        }

        public void AddComponent(Component component)
        {
            component.Entity = this;
            components[component.GetType()] = component;
            SortComponents();
        }

        public void AddComponent<T>(params object[] parameters)
            where T : Component
        {
            var component = (T)Activator.CreateInstance(typeof(T), parameters);
            AddComponent(component);
        }

        public void AddComponents(params Component[] components)
        {
            foreach (var component in components) {
                component.Entity = this;
                this.components[component.GetType()] = component;
            }
            SortComponents();
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

        private void SortComponents()
        {
            var values = components.Values;
            var sorted = TopologicalSort.Sort(
                values,
                c => EntityManager.GetDependency(c.GetType()),
                c => c.GetType()
            );
            components = sorted.ToDictionary(c => c.GetType(), c => c);
        }
    }
}
