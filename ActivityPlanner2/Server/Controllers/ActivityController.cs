using ActivityPlanner2.Data;
using ActivityPlanner2.Data.ServerModels;
using ActivityPlanner2.Shared.DTOs;
using ActivityPlanner2.Shared.ExtensionMethods;
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
    public class ActivityController : ControllerBase
    {
        IPersonRepository personContext;
        IActivityRepository activityContext;

        public ActivityController(IPersonRepository personContext, IActivityRepository activityContext)
        {
            this.personContext = personContext;
            this.activityContext = activityContext;
        }
        // GET: api/<ActivityController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityDTO>>> Get()
        {
            var result = await activityContext.GetListOfActivities();

            if (result == null)
            {
                return NotFound();
            }

            try
            {
                IEnumerable<ActivityDTO> castResult = result.Cast<ActivityDTO>();

                return Ok(castResult);
            }
            catch (InvalidCastException ex)
            {
                return NotFound(ex);
            }
        }

        // GET api/<ActivityController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityDTO>> Get(int id)
        {
            var result = await activityContext.GetActivityById(id);

            if (result == null)
            {
                return NotFound();
            }

            try
            {
                ActivityDTO castResult = (ActivityDTO)result;

                return Ok(castResult);
            }
            catch (InvalidCastException ex)
            {
                return NotFound(ex);
            }
        }

        // POST api/<ActivityController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ActivityDTO value)
        {
            Activity ActivityToAdd = new();

            await activityContext.AddActivityFromDTO(value, ActivityToAdd);

            var AddedActivity = activityContext.GetActivityById(ActivityToAdd.Id);

            if (AddedActivity == null)
            {
                return NotFound();
            }

            return Created($"/Activity/{ActivityToAdd.Id}", AddedActivity);
        }

        // PUT api/<ActivityController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ActivityDTO value)
        {
            Activity savedActivity = await activityContext.GetActivityById(id);

            if (savedActivity == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE api/<ActivityController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            activityContext.DeleteActivity(id);

            return Ok();
        }
    }
}
