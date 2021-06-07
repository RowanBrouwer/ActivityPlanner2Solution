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
    [Route("api/[controller]/[action]")]
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
        [ActionName("GetAllActivitys")]
        public async Task<ActionResult<IEnumerable<ActivityDTO>>> Get()
        {
            var result = await activityContext.GetListOfActivities();

            if (result == null)
            {
                return NotFound();
            }

            try
            {
                List<ActivityDTO> castResult = new List<ActivityDTO>();

                foreach (var activity in result)
                {
                    castResult.Add((ActivityDTO)activity);
                }

                return Ok(castResult);
            }
            catch (InvalidCastException ex)
            {
                return NotFound(ex);
            }
        }

        // GET api/<ActivityController>/5
        [HttpGet("{id}")]
        [ActionName("GetActivityById")]
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

        [HttpGet("{id}")]
        [ActionName("GetInvitedPeopleByActivityId")]
        public async Task<ActionResult<BasePersonDTO>> GetInvitedPeopleByActivityId(int id)
        {
            var result = await activityContext.GetInvitesdByActivityId(id);

            if (result == null)
            {
                return NotFound();
            }

            try
            {
                BasePersonDTO castResult = (BasePersonDTO)result;

                return Ok(castResult);
            }
            catch (InvalidCastException ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet("{id}")]
        [ActionName("GetOrganizersByActivityId")]
        public async Task<ActionResult<BasePersonDTO>> GetOrganizersByActivityId(int id)
        {
            var result = await activityContext.GetOrganizersByActivityid(id);

            if (result == null)
            {
                return NotFound();
            }

            try
            {
                BasePersonDTO castResult = (BasePersonDTO)result;

                return Ok(castResult);
            }
            catch (InvalidCastException ex)
            {
                return NotFound(ex);
            }
        }

        // POST api/<ActivityController>
        [HttpPost]
        [ActionName("PostActivity")]
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
        [ActionName("PutActivity")]
        public async Task<ActionResult> Put(int id, [FromBody] ActivityDTO value)
        {
            Activity savedActivity = await activityContext.GetActivityById(id);

            if (savedActivity == null)
            {
                return NotFound();
            }

            await activityContext.UpdateActivityFromDTO(id, value);

            return NoContent();
        }

        // DELETE api/<ActivityController>/5
        [HttpDelete("{id}")]
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            activityContext.DeleteActivity(id);

            return Ok();
        }
    }
}
