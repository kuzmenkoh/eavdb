using System.Collections.Generic;
using System.Linq;
using EAVDB.Services;
using Microsoft.AspNetCore.Mvc;
using EntityCrud.Controllers.SerializeModels;

namespace EntityCrud.Controllers
{
    [ApiController, Route("api/Persons/{personId:int}/Records/")]
    public class PersonRecordController : ControllerBase
    {
        private readonly IPersonRecordService _recordService;

        public PersonRecordController(IPersonRecordService recordService)
        {
            _recordService = recordService;
        }
        
        /// <summary>
        /// Get person records
        /// </summary>
        /// <param name="personId">Person id</param>
        [HttpGet]
        public ActionResult<IEnumerable<Record>> Get([FromRoute] int personId)
        {
            var records = _recordService.ReadPersonRecords(personId);
            if (records == null)
                return NotFound();
            return Ok(records.Select(Record.FromRecord).ToList());
        }
        
        /// <summary>
        /// Get person record by id
        /// </summary>
        [HttpGet("{id:int}")]
        public ActionResult<Record> Get([FromRoute] int personId, int id)
        {
            var record = _recordService.Read(personId, id);
            if (record == null)
                return NotFound();
            return Ok(Record.FromRecord(record));
        }

        /// <summary>
        /// Create person record
        /// </summary>
        [HttpPost]
        public ActionResult Post([FromRoute] int personId, [FromBody] Record value)
        {
            return _recordService.Create(personId, value.ToRecord()) ? (ActionResult)Ok() : NotFound();
        }

        /// <summary>
        /// Update person record
        /// </summary>
        [HttpPut("{id:int}")]
        public ActionResult Put([FromRoute] int personId, int id, [FromBody] Record value)
        {
            return _recordService.Update(personId, id, value.ToRecord()) ? (ActionResult)Ok() : NotFound();
        }

        /// <summary>
        /// Delete person record
        /// </summary>
        [HttpDelete("{id:int}")]
        public ActionResult Delete([FromRoute] int personId, int id)
        {
            return _recordService.Delete(personId, id) ? (ActionResult)Ok() : NotFound();
        }
    }
}