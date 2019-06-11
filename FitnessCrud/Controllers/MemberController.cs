using System.Collections.Generic;
using System.Linq;
using EAVDB.Services;
using Microsoft.AspNetCore.Mvc;
using M = EAVDB.Models;
using FitnessCrud.Controllers.SerializeModels;

namespace FitnessCrud.Controllers
{
    [ApiController, Route("api/Members")]
    public class MemberController : ControllerBase
    {
        private readonly IEntityService<M.Person> _entityService;

        public MemberController(IEntityService<M.Person> entityService)
        {
            _entityService = entityService;
        }

        /// <summary>
        /// Get all persons
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Member>> Get()
        {
            return Ok(_entityService.Read().Select(p => new Member().FromPerson(p)).ToList());
        }

        /// <summary>
        /// Get person by id
        /// </summary>
        [HttpGet("{id:int}")]
        public ActionResult<Member> Get(int id)
        {
            var person = _entityService.Read(id);
            if (person == null)
                return NotFound();
            return Ok(new Member().FromPerson(person));
        }

        /// <summary>
        /// Create person
        /// </summary>
        [HttpPost]
        public ActionResult Post([FromBody] Member value)
        {
            _entityService.Create(value.ToPerson());
            return Ok();
        }

        /// <summary>
        /// Update person
        /// </summary>
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] Member value)
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