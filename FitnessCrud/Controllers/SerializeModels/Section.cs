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

        protected void FillFromRecord(M.Record record)
        {
            FillFromEntity(record);
            MemberId = record.PersonId;
        }

        public static Section FromRecord(M.Record record)
        {
            var result = new Section();
            result.FillFromRecord(record);
            return result;
        }
    }
}