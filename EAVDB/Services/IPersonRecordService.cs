using System.Collections.Generic;
using EAVDB.Models;

namespace EAVDB.Services
{
    public interface IPersonRecordService
    {
        bool Create(int personId, Record entity);
        IEnumerable<Record> ReadPersonRecords(int personId);
        Record Read(int personId, int id);
        bool Update(int personId, int id, Record entity);
        bool Delete(int personId, int id);
    }
}