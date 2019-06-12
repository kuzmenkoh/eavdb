using System.Collections.Generic;
using System.Linq;
using EAVDB.Services;
using FitnessCrud.Controllers.SerializeModels;
using Microsoft.AspNetCore.Mvc;

namespace FitnessCrud.Controllers
{
    [ApiController, Route("api/Members/{personId:int}/Sections/")]
    public class MembersSectionController : ControllerBase
    {
        private readonly IPersonRecordService _recordService;

        public MembersSectionController(IPersonRecordService recordService)
        {
            _recordService = recordService;
        }
        
        /// <summary>
        /// Get person records
        /// </summary>
        /// <param name="personId">Person id</param>
        [HttpGet]
        public ActionResult<IEnumerable<Section>> Get([FromRoute] int personId)
        {
            var records = _recordService.ReadPersonRecords(personId);
            if (records == null)
                return NotFound();
            return Ok(records.Select(Section.FromRecord).ToList());
        }
        
        /// <summary>
        /// Get person record by id
        /// </summary>
        [HttpGet("{id:int}")]
        public ActionResult<Section> Get([FromRoute] int personId, int id)
        {
            var record = _recordService.Read(personId, id);
            if (record == null)
                return NotFound();
            return Ok(Section.FromRecord(record));
        }

        /// <summary>
        /// Create person record
        /// </summary>
        [HttpPost]
        public ActionResult Post([FromRoute] int personId, [FromBody] Section value)
        {
            return _recordService.Create(personId, value.ToRecord()) ? (ActionResult)Ok() : NotFound();
        }

        /// <summary>
        /// Update person record
        /// </summary>
        [HttpPut("{id:int}")]
        public ActionResult Put([FromRoute] int personId, int id, [FromBody] Section value)
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