using Newtonsoft.Json;

namespace EAVDB.Models
{
    public abstract class Attribute<TEntity>
    {
        public int EntityId { get; set; }

        public string Name { get; set; }
        
        public TEntity Entity { get; set; }

        public abstract object GetValue();
    }
}
