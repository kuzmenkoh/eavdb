using System.Collections.Generic;
using EAVDB.Models;

namespace MedicalCrud.Controllers.SerializeModels
{
    public class Entity
    {
        public int Id { get; set; }
        public Dictionary<string, object> Attributes { get; set; }

        public TEntity ToEntity<TEntity>() where TEntity : Entity<TEntity>, new()
        {
            var result = new TEntity {EntityId = Id};
            foreach (var pair in Attributes)
                result[pair.Key] = pair.Value;
            return result;
        }

        public Entity FromEntity<TEntity>(TEntity entity) where TEntity : Entity<TEntity>
        {
            Id = entity.EntityId;
            foreach (var attribute in entity.Attributes)
                Attributes[attribute.Name] = attribute.GetValue();
            return this;
        }
    }
}