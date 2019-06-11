using System.Collections.Generic;
using System.Linq;
using EAVDB.Services;
using Microsoft.AspNetCore.Mvc;
using M = EAVDB.Models;
using EntityCrud.Controllers.SerializeModels;

namespace EntityCrud.Controllers
{
    [ApiController, Route("api/Records")]
    public class RecordController: ControllerBase
    {
        private readonly IEntityService<M.Record> _entityService;
        public RecordController(IEntityService<M.Record> entityService)
        {
            _entityService = entityService;
        }

        /// <summary>
        /// Get all records
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Record>> Get()
        {
            return Ok(_entityService.Read().Select(p => new Record().FromRecord(p)).ToList());
        }

        /// <summary>
        /// Get record by id
        /// </summary>
        [HttpGet("{id:int}")]
        public ActionResult<Record> Get(int id)
        {
            var record = _entityService.Read(id);
            if (record == null)
                return NotFound();
            return Ok(new Record().FromRecord(record));
        }

        /// <summary>
        /// Create record
        /// </summary>
        [HttpPost]
        public ActionResult Post([FromBody] Record value)
        {
            _entityService.Create(value.ToRecord());
            return Ok();
        }

        /// <summary>
        /// Update record
        /// </summary>
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] Record value)
        {
            return _entityService.Update(id, value.ToRecord()) ? (ActionResult)Ok() : NotFound();
        }

        /// <summary>
        /// Delete record
        /// </summary>
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            return _entityService.Delete(id) ? (ActionResult)Ok() : NotFound();
        }
    }
}