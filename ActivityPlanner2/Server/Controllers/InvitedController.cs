using ActivityPlanner2.Data;
using ActivityPlanner2.Data.ServerModels;
using ActivityPlanner2.Shared.DTOs;
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
    public class InvitedController : ControllerBase
    {
        IActivityRepository activityContext;
        IPersonInviteRepository inviteContext;
        IPersonRepository personContext;
        public InvitedController(IActivityRepository activityContext, IPersonInviteRepository inviteContext, IPersonRepository personContext)
        {
            this.activityContext = activityContext;
            this.inviteContext = inviteContext;
            this.personContext = personContext;
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
        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<ActivityDTO>>> Get(string name)
        {
            var person = await personContext.GetPersonByUserName(name);

            var result = await activityContext.GetListOfInvitedActivitiesByPersonId(person.Id);

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
            Task deleteTask = inviteContext.DeleteInviteByPersonIdAndActivityId(DTO.PersonId, DTO.ActivityId);
            await deleteTask;

            if (deleteTask.IsCanceled || deleteTask.IsFaulted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
