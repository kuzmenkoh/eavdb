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

        public Operation FromRecord(M.Record record)
        {
            FromEntity(record);
            PatientId = record.PersonId;
            return this;
        }
    }
}