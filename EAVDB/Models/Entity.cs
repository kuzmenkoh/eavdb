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
        public virtual List<Attribute<TEntity>> Attributes { get; set; }
    }
}