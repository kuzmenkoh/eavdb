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

        protected void FillFromRecord(M.Record record)
        {
            FillFromEntity(record);
            PersonId = record.PersonId;
        }

        public static Record FromRecord(M.Record record)
        {
            var result = new Record();
            result.FillFromRecord(record);
            return result;
        }
    }
}