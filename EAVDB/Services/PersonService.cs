using System.Collections.Generic;
using System.Linq;
using EAVDB.Models;
using Microsoft.EntityFrameworkCore;

namespace EAVDB.Services
{
    public class PersonService : EntityService<Person>
    {
        public PersonService(EavContext context) : base(context)
        {
        }
        
        public override Person Read(int id)
        {
            return Context.Persons
                .Include(p => p.Attributes)
                .Include(p => p.Records)
                    .ThenInclude(r => r.Attributes)
                .SingleOrDefault(p => p.EntityId == id);
        }

        public override IEnumerable<Person> Read()
        {
            return Context.Persons
                .Include(p => p.Attributes)
                .Include(p => p.Records)
                    .ThenInclude(r => r.Attributes);
        }
    }
}