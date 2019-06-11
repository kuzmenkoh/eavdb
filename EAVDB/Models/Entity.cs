using System;
using System.Collections.Generic;
using System.Linq;

namespace EAVDB.Models
{
    /// <summary>
    /// Provides support to maintaining list of connected Attributes.
    /// </summary>
    /// <typeparam name="TEntity">Type of Entity with Attributes</typeparam>
    public abstract class Entity<TEntity> where TEntity : Entity<TEntity>
    {
        public int EntityId { get; set; }
        public List<Attribute<TEntity>> Attributes { get; set; } = new List<Attribute<TEntity>>();
        public Attribute<TEntity> GetAttribute(string name) => Attributes.SingleOrDefault(a => a.Name == name);
        public void RemoveAttribute(string name) => Attributes.RemoveAll(a => a.Name == name);

        public virtual Attribute<TEntity> CreateAttribute(string attributeName, object value)
        {
            if (value is string s)
                return new AttributeString<TEntity> { Entity = (TEntity)this, Name = attributeName, StringValue = s };
            if (value is int i)
                return new AttributeInt<TEntity> { Entity = (TEntity)this, Name = attributeName, IntValue = i };
            if (value is long l)
                return new AttributeInt<TEntity> { Entity = (TEntity)this, Name = attributeName, IntValue = (int)l };
            throw new ArgumentException($"Unexpected type {value.GetType()} for Entity Attribute");
        }

        public object this[string attributeName]
        {
            get => GetAttribute(attributeName)?.Value;
            set
            {
                RemoveAttribute(attributeName);
                if (value != null)
                    Attributes.Add(CreateAttribute(attributeName, value));
            }
        }
    }
}