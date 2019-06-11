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
            result.Records = Operations.Select(r => r.ToRecord()).ToList();
            return result;
        }

        public Patient FromPerson(M.Person person)
        {
            FromEntity(person);
            Name = person.Name;
            Surname = person.Surname;
            Operations = person.Records.Select(r => new Operation().FromRecord(r)).ToList();
            return this;
        }
    }
}