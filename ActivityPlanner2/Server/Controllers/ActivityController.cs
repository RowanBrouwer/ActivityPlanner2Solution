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
    public class ActivityController : ControllerBase
    {
        IPersonRepository personContext;
        IActivityRepository activityContext;
        IPersonInviteRepository invitePersonContext;
        IPersonOrganizedActivityRepository organizeContext;

        public ActivityController(IPersonRepository personContext, IActivityRepository activityContext, IPersonInviteRepository invitePersonContext, IPersonOrganizedActivityRepository organizeContext)
        {
            this.personContext = personContext;
            this.activityContext = activityContext;
            this.invitePersonContext = invitePersonContext;
            this.organizeContext = organizeContext;
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
            Activity ActivityToAdd = new Activity();

            try
            {
                ActivityToAdd = (Activity)value;
            }
            catch (InvalidCastException ex)
            {
                return NotFound(ex);
            }

            var InvitedPeopleDTOObject = value.InvitedGuests;

            var InvitedPeople = new List<PersonInvites>();

            foreach (var person in InvitedPeopleDTOObject)
            {
                var invite = new PersonInvites()
                {
                    Accepted = person.Accepted,
                    ActivityId = person.ActivityId,
                    PersonId = person.PersonId,
                };

                await invitePersonContext.AddInvite(invite);

                InvitedPeople.Add(invite);
            }

            ActivityToAdd.InvitedGuests = InvitedPeople;

            var personOrginizedActivitiesDTO = value.Organizers;

            var Organizers = new List<PersonOrganizedActivity>();

            foreach (var person in personOrginizedActivitiesDTO)
            {
                var invite = new PersonOrganizedActivity()
                {
                    OrganizedActivityId = person.ActivityId,
                    OrganizerId = person.PersonId,
                };

                await organizeContext.AddInvite(invite);

                Organizers.Add(invite);
            }
            ActivityToAdd.InvitedGuests = InvitedPeople;

            await activityContext.AddActivity(ActivityToAdd);

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

            List<PersonInvites> DTOPersonInviteList = new();
            List<PersonInvites> UpdatedInviteList = new();

            if (value.InvitedGuests != null)
            {
                try
                {
                    DTOPersonInviteList = value.InvitedGuests.Cast<PersonInvites>().ToList();
                }
                catch (InvalidCastException ex)
                {
                    return NotFound(ex);
                } 
            }

            foreach (var person in DTOPersonInviteList)
            {
                var invite = new PersonInvites()
                {
                    ActivityId = person.ActivityId,
                    PersonId = person.PersonId,
                    Activity = await activityContext.GetActivityById(person.ActivityId),
                    Person = await personContext.GetPersonById(person.PersonId),
                    Accepted = person.Accepted
                };
                UpdatedInviteList.Add(invite);
            }

            List<PersonOrganizedActivity> DTOOrganizerList = new();
            List<PersonOrganizedActivity> UpdatedOrganizerList = new();

            if (value.InvitedGuests != null)
            {
                try
                {
                    DTOOrganizerList = value.InvitedGuests.Cast<PersonOrganizedActivity>().ToList();
                }
                catch (InvalidCastException ex)
                {
                    return NotFound(ex);
                }
            }

            foreach (var person in DTOOrganizerList)
            {
                var invite = new PersonOrganizedActivity()
                {
                    OrganizedActivityId = person.OrganizedActivityId,
                    OrganizerId = person.OrganizerId,
                    OrganizedActivity = await activityContext.GetActivityById(person.OrganizedActivityId),
                    Organizer = await personContext.GetPersonById(person.OrganizerId)
                };
                UpdatedOrganizerList.Add(invite);
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
