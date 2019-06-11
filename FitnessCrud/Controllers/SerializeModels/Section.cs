using M = EAVDB.Models;

namespace FitnessCrud.Controllers.SerializeModels
{
    public class Section : Entity
    {
        public int MemberId { get; set; }
        
        public M.Record ToRecord()
        {
            var result = ToEntity<M.Record>();
            result.PersonId = MemberId;
            return result;
        }

        public Section FromRecord(M.Record record)
        {
            FromEntity(record);
            MemberId = record.PersonId;
            return this;
        }
    }
}