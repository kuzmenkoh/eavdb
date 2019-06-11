using System.Collections.Generic;
using EAVDB.Models;

namespace EAVDB.Services
{
    public interface IEntityService<TEntity> where TEntity : Entity<TEntity>
    {
        void Create(TEntity entity);
        IEnumerable<TEntity> Read();
        TEntity Read(int id);
        bool Update(int id, TEntity entity);
        bool Delete(int id);
    }
}