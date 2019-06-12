using System.Collections.Generic;
using System.Linq;
using EAVDB.Services;
using MedicalCrud.Controllers.SerializeModels;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCrud.Controllers
{
    [ApiController, Route("api/Patients/{personId:int}/Operations/")]
    public class PatientOperationController : ControllerBase
    {
        private readonly IPersonRecordService _recordService;

        public PatientOperationController(IPersonRecordService recordService)
        {
            _recordService = recordService;
        }
        
        /// <summary>
        /// Get person records
        /// </summary>
        /// <param name="personId">Person id</param>
        [HttpGet]
        public ActionResult<IEnumerable<Operation>> Get([FromRoute] int personId)
        {
            var records = _recordService.ReadPersonRecords(personId);
            if (records == null)
                return NotFound();
            return Ok(records.Select(Operation.FromRecord).ToList());
        }
        
        /// <summary>
        /// Get person record by id
        /// </summary>
        [HttpGet("{id:int}")]
        public ActionResult<Operation> Get([FromRoute] int personId, int id)
        {
            var record = _recordService.Read(personId, id);
            if (record == null)
                return NotFound();
            return Ok(Operation.FromRecord(record));
        }

        /// <summary>
        /// Create person record
        /// </summary>
        [HttpPost]
        public ActionResult Post([FromRoute] int personId, [FromBody] Operation value)
        {
            return _recordService.Create(personId, value.ToRecord()) ? (ActionResult)Ok() : NotFound();
        }

        /// <summary>
        /// Update person record
        /// </summary>
        [HttpPut("{id:int}")]
        public ActionResult Put([FromRoute] int personId, int id, [FromBody] Operation value)
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