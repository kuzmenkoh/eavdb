using System.Collections.Generic;
using EAVDB.Models;

namespace EntityCrud.Controllers.SerializeModels
{
    public class Entity
    {
        public int Id { get; set; }
        public Dictionary<string, object> Attributes { get; set; }

        public TEntity ToEntity<TEntity>() where TEntity : Entity<TEntity>, new()
        {
            var result = new TEntity {EntityId = Id};
            if (Attributes != null)
            {
                result.Attributes = new List<Attribute<TEntity>>();
                foreach (var pair in Attributes)
                    result.SetAttribute(pair.Key, pair.Value);
            }

            return result;
        }

        protected void FillFromEntity<TEntity>(TEntity entity) where TEntity : Entity<TEntity>
        {
            Id = entity.EntityId;
            if (entity.Attributes != null)
            {
                Attributes = new Dictionary<string, object>();
                foreach (var attribute in entity.Attributes)
                    Attributes[attribute.Name] = attribute.GetValue();
            }
        }
    }
}