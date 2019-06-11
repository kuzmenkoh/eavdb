using System.Collections.Generic;
using System.Linq;
using EAVDB.Services;
using EntityCrud.Controllers.SerializeModels;
using Microsoft.AspNetCore.Mvc;
using M = EAVDB.Models;

namespace EntityCrud.Controllers
{
    [ApiController, Route("api/Persons")]
    public class PersonController : ControllerBase
    {
        private readonly IEntityService<M.Person> _entityService;

        public PersonController(IEntityService<M.Person> entityService)
        {
            _entityService = entityService;
        }

        /// <summary>
        /// Get all persons
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get()
        {
            return Ok(_entityService.Read().Select(p => new Person().FromPerson(p)).ToList());
        }

        /// <summary>
        /// Get person by id
        /// </summary>
        [HttpGet("{id:int}")]
        public ActionResult<Person> Get(int id)
        {
            var person = _entityService.Read(id);
            if (person == null)
                return NotFound();
            return Ok(new Person().FromPerson(person));
        }

        /// <summary>
        /// Create person
        /// </summary>
        [HttpPost]
        public ActionResult Post([FromBody] Person value)
        {
            _entityService.Create(value.ToPerson());
            return Ok();
        }

        /// <summary>
        /// Update person
        /// </summary>
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] Person value)
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