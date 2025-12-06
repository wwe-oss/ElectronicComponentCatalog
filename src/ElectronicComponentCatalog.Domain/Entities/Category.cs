using System;
using System.Collections.Generic;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Entities
{
    /// <summary>
    /// Represents a category grouping for components (e.g., Resistors, Capacitors).
    /// </summary>
    public class Category
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public string Description { get; private set; }
        private readonly List<Component> _components = new();

        public IReadOnlyCollection<Component> Components => _components.AsReadOnly();

        public Category(string name, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
        }

        public void AddComponent(Component component)
        {
            if (component == null) throw new ArgumentNullException(nameof(component));
            if (_components.Exists(c => c.Name == component.Name))
                throw new InvalidOperationException($"Component '{component.Name}' already exists in category '{Name}'.");
            _components.Add(component);
        }

        public void RemoveComponent(Component component)
        {
            if (component == null) throw new ArgumentNullException(nameof(component));
            _components.Remove(component);
        }
    }
}
