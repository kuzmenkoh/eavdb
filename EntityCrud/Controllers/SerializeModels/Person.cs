using System.Collections.Generic;
using System.Linq;
using M = EAVDB.Models;
using Record = EntityCrud.Controllers.SerializeModels.Record;

namespace EntityCrud.Controllers.SerializeModels
{
    public class Person : Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public IEnumerable<Record> Records { get; set; }

        public M.Person ToPerson()
        {
            var result = ToEntity<M.Person>();
            result.Name = Name;
            result.Surname = Surname;
            result.Records = Records.Select(r => r.ToRecord()).ToList();
            return result;
        }

        public Person FromPerson(M.Person person)
        {
            FromEntity(person);
            Name = person.Name;
            Surname = person.Surname;
            Records = person.Records.Select(r => new Record().FromRecord(r)).ToList();
            return this;
        }
    }
}