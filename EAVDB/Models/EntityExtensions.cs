using System;
using System.Linq;

namespace EAVDB.Models
{
    public static class EntityExtensions
    {
        public static Attribute<TEntity> CreateAttribute<TEntity>(this TEntity entity, string attributeName, object value)
        {
            if (value is string s)
                return new AttributeString<TEntity> { Entity = entity, Name = attributeName, StringValue = s };
            if (value is int i)
                return new AttributeInt<TEntity> { Entity = entity, Name = attributeName, IntValue = i };
            if (value is long l)
                return new AttributeInt<TEntity> { Entity = entity, Name = attributeName, IntValue = (int)l };
            throw new ArgumentException($"Unexpected type {value.GetType()} for Entity Attribute");
        }

        public static object GetAttribute<TEntity>(this TEntity entity, string attributeName) where TEntity : Entity<TEntity>
        {
            return entity.Attributes.SingleOrDefault(a => a.Name == attributeName)?.GetValue();
        }

        public static void SetAttribute<TEntity>(this TEntity entity, string attributeName, object value) where TEntity : Entity<TEntity>
        {
            entity.Attributes.RemoveAll(a => a.Name == attributeName);
            if (value != null)
                entity.Attributes.Add(entity.CreateAttribute(attributeName, value));
        }
    }
}