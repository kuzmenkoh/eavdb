using System.Collections.Generic;
using System.Linq;
using EAVDB.Models;
using Microsoft.EntityFrameworkCore;

namespace EAVDB.Services
{
    public class PersonRecordService : EntityService<Record>, IPersonRecordService
    {
        public PersonRecordService(EavContext context) : base(context)
        {
        }

        public bool Exists(int personId, int id)
        {
            return Context.Records.Any(r => r.EntityId == id && r.Person.EntityId == personId);
        }
        
        public bool Create(int personId, Record entity)
        {
            if (!Context.Records.Any(r => r.EntityId == personId))
                return false;
            entity.PersonId = personId;
            Context.Add(entity);
            Context.SaveChanges();
            return true;
        }

        public IEnumerable<Record> ReadPersonRecords(int personId)
        {
            return Context.Persons
                .Include(p => p.Records)
                    .ThenInclude(r => r.Attributes)
                .SingleOrDefault(p => p.EntityId == personId)?.Records;
        }

        public Record Read(int personId, int id)
        {
            return Context.Records
                .Include(r => r.Attributes)
                .SingleOrDefault(r => r.EntityId == id && r.Person.EntityId == personId);
        }

        public bool Update(int personId, int id, Record entity)
        {
            return Exists(personId, id) && Update(id, entity);
        }

        public bool Delete(int personId, int id)
        {
            return Exists(personId, id) && Delete(id);
        }
    }
}