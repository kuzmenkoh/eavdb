using M = EAVDB.Models;

namespace MedicalCrud.Controllers.SerializeModels
{
    public class Operation : Entity
    {
        public int PatientId { get; set; }
        
        public M.Record ToRecord()
        {
            var result = ToEntity<M.Record>();
            result.PersonId = PatientId;
            return result;
        }

        protected void FillFromRecord(M.Record record)
        {
            FillFromEntity(record);
            PatientId = record.PersonId;
        }

        public static Operation FromRecord(M.Record record)
        {
            var result = new Operation();
            result.FillFromRecord(record);
            return result;
        }
    }
}