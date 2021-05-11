using ActivityPlanner2.Data;
using ActivityPlanner2.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ActivityPlanner2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        IPersonRepository context;
        public PeopleController(IPersonRepository context)
        {
            this.context = context;
        }

        // GET: api/<PeopleController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetListOfPeople()
        {
            var result = await context.GetListOfPeople();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/<PeopleController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(string id)
        {
            var result = await context.GetPersonById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/<PeopleController>
        [HttpPost]
        public async Task<ActionResult<Person>> Post([FromBody] Person value)
        {
            var check = await context.GetPersonById(value.Id);

            if (check != null)
            {
                return Conflict();
            }

            await context.AddPerson(value);

            var result = await context.GetPersonById(value.Id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT api/<PeopleController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Person>> Put(string id, [FromBody] Person value)
        {
            var check = await context.GetPersonById(id);

            if (check == null)
            {
                return NotFound();
            }

            await context.UpdatePerson(value);

            var result = await context.GetPersonById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // DELETE api/<PeopleController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var check = await context.GetPersonById(id);

            if (check == null)
            {
                return NotFound();
            }

            await context.DeletePerson(id);

            return NoContent();    
        }
    }
}
