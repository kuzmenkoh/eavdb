using System;
using System.Collections.Generic;
using System.Linq;
using EAVDB.Models;
using Microsoft.EntityFrameworkCore;

namespace EAVDB.Services
{
    public class EntityService<TEntity> : IEntityService<TEntity> where TEntity : Entity<TEntity>, new()
    {
        protected readonly EavContext Context;
        public EntityService(EavContext context)
        {
            Context = context;
        }

        public void Create(TEntity entity)
        {
            Context.Add(entity);
            Context.SaveChanges();
        }

        public virtual IEnumerable<TEntity> Read()
        {
            return Context.Set<TEntity>()
                .Include(p => p.Attributes);
        }

        public virtual TEntity Read(int id)
        {
            return Context.Set<TEntity>()
                .Include(p => p.Attributes)
                .SingleOrDefault(p => p.EntityId == id);
        }

        public bool Update(int id, TEntity entity)
        {
            var existing = Context.Set<TEntity>()
                .Include(p => p.Attributes)
                .SingleOrDefault(p => p.EntityId == id);
            if (existing == null)
                return false;
            foreach (var attribute in existing.Attributes)
                Context.Remove(attribute);
            Context.Entry(existing).State = EntityState.Detached;
            entity.EntityId = id;
            Context.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var existing = Context.Set<TEntity>()
                .SingleOrDefault(p => p.EntityId == id);
            if (existing == null)
                return false;
            Context.Remove(existing);
            Context.SaveChanges();
            return true;
        }
    }
}