using System.Collections.Generic;
using System.Linq;
using M = EAVDB.Models;

namespace MedicalCrud.Controllers.SerializeModels
{
    public class Patient : Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public IEnumerable<Operation> Operations { get; set; }

        public M.Person ToPerson()
        {
            var result = ToEntity<M.Person>();
            result.Name = Name;
            result.Surname = Surname;
            result.Records = Operations?.Select(r => r.ToRecord()).ToList();
            return result;
        }

        protected void FillFromPerson(M.Person person)
        {
            FillFromEntity(person);
            Name = person.Name;
            Surname = person.Surname;
            Operations = person.Records?.Select(Operation.FromRecord).ToList();
        }

        public static Patient FromPerson(M.Person person)
        {
            var result = new Patient();
            result.FillFromPerson(person);
            return result;
        }
    }
}