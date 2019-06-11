using M = EAVDB.Models;

namespace EntityCrud.Controllers.SerializeModels
{
    public class Record : Entity
    {
        public int PersonId { get; set; }
        
        public M.Record ToRecord()
        {
            var result = ToEntity<M.Record>();
            result.PersonId = PersonId;
            return result;
        }

        public Record FromRecord(M.Record record)
        {
            FromEntity(record);
            PersonId = record.PersonId;
            return this;
        }
    }
}