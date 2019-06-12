using System.Collections.Generic;
using System.Linq;
using EAVDB.Services;
using Microsoft.AspNetCore.Mvc;
using M = EAVDB.Models;
using MedicalCrud.Controllers.SerializeModels;

namespace MedicalCrud.Controllers
{
    [ApiController, Route("api/Patients")]
    public class PatientController : ControllerBase
    {
        private readonly IEntityService<M.Person> _entityService;

        public PatientController(IEntityService<M.Person> entityService)
        {
            _entityService = entityService;
        }

        /// <summary>
        /// Get all persons
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Patient>> Get()
        {
            return Ok(_entityService.Read().Select(Patient.FromPerson).ToList());
        }

        /// <summary>
        /// Get person by id
        /// </summary>
        [HttpGet("{id:int}")]
        public ActionResult<Patient> Get(int id)
        {
            var person = _entityService.Read(id);
            if (person == null)
                return NotFound();
            return Ok(Patient.FromPerson(person));
        }

        /// <summary>
        /// Create person
        /// </summary>
        [HttpPost]
        public ActionResult Post([FromBody] Patient value)
        {
            _entityService.Create(value.ToPerson());
            return Ok();
        }

        /// <summary>
        /// Update person
        /// </summary>
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] Patient value)
        {
            return _entityService.Update(id, value.ToPerson()) ? (ActionResult) Ok() : NotFound();
        }

        /// <summary>
        /// Delete person
        /// </summary>
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            return _entityService.Delete(id) ? (ActionResult) Ok() : NotFound();
        }
    }
}