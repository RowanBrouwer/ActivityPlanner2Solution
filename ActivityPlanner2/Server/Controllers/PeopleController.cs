using ActivityPlanner2.Data;
using ActivityPlanner2.Data.ServerModels;
using ActivityPlanner2.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ActivityPlanner2.Server.Controllers
{
    [Route("api/[controller]/[action]")]
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
        [ActionName("GetAllPeople")]
        public async Task<ActionResult<IEnumerable<BasePersonDTO>>> GetListOfPeople()
        {
            var result = await context.GetListOfPeople();

            if (result == null)
            {
                return NotFound();
            }

            IEnumerable<BasePersonDTO> castedResult = result.ToList().ConvertAll(p => (BasePersonDTO)p);

            return Ok(castedResult);
        }

        // GET api/<PeopleController>/5
        [HttpGet("{userName}")]
        [ActionName("GetPersonByName")]
        public async Task<ActionResult<BasePersonDTO>> GetPersonByName(string userName)
        {
            var result = await context.GetPersonByUserName(userName);

            if (result == null)
            {
                return NotFound();

            }

            return Ok((BasePersonDTO)result);
        }

        // GET api/<PeopleController>/5
        [HttpGet("{Id}")]
        [ActionName("GetPersonById")]
        public async Task<ActionResult<BasePersonDTO>> GetPersonById(string Id)
        {
            var result = await context.GetPersonById(Id);

            if (result == null)
            {
                return NotFound();

            }

            return Ok((BasePersonDTO)result);
        }

        // POST api/<PeopleController>
        [HttpPost]
        public async Task<ActionResult<BasePersonDTO>> Post([FromBody] BasePersonDTO value)
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

            return Ok((BasePersonDTO)result);
        }

        // PUT api/<PeopleController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<BasePersonDTO>> Put(string id, [FromBody] BasePersonDTO value)
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

            return Ok((BasePersonDTO)result);
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
