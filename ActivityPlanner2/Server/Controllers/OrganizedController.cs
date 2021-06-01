using ActivityPlanner2.Data;
using ActivityPlanner2.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ActivityPlanner2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizedController : ControllerBase
    {
        IActivityRepository activityContext;
        IPersonOrganizedActivityRepository organizedContext;
        public OrganizedController(IActivityRepository activityContext, IPersonOrganizedActivityRepository organizedContext)
        {
            this.activityContext = activityContext;
            this.organizedContext = organizedContext;
        }

        // GET: api/<InvitedController>
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
                var castResult = new List<ActivityDTO>();

                foreach (var activity in result)
                {
                    castResult.Add((ActivityDTO)activity);
                }
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine(ex);
            }

            return Ok();
        }

        // GET api/<InvitedController>/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<ActivityDTO>>> Get(string userId)
        {
            var result = await activityContext.GetListOfOrganizedActivitiesByPersonId(userId);

            if (result == null)
            {
                return NotFound();
            }

            try
            {
                var castResult = new List<ActivityDTO>();

                foreach (var activity in result)
                {
                    castResult.Add((ActivityDTO)activity);
                }
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine(ex);
            }

            return Ok(result);
        }

        // DELETE api/<InvitedController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(PersonInvitesDTO DTO)
        {
            Task deleteTask = organizedContext.DeleteOrganizedActivitiesByPersonIdAndActivityId(DTO.PersonId, DTO.ActivityId);
            await deleteTask;

            if (deleteTask.IsCanceled || deleteTask.IsFaulted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
