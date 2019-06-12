using System.Collections.Generic;
using System.Linq;
using M = EAVDB.Models;

namespace FitnessCrud.Controllers.SerializeModels
{
    public class Member : Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public IEnumerable<Section> Sections { get; set; }

        public M.Person ToPerson()
        {
            var result = ToEntity<M.Person>();
            result.Name = Name;
            result.Surname = Surname;
            result.Records = Sections?.Select(r => r.ToRecord()).ToList();
            return result;
        }

        protected void FillFromPerson(M.Person person)
        {
            FillFromEntity(person);
            Name = person.Name;
            Surname = person.Surname;
            Sections = person.Records?.Select(Section.FromRecord).ToList();
        }

        public static Member FromPerson(M.Person person)
        {
            var result = new Member();
            result.FillFromPerson(person);
            return result;
        }
    }
}