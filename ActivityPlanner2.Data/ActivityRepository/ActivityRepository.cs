using ActivityPlanner2.Data.ServerModels;
using ActivityPlanner2.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner2.Data
{
    public class ActivityRepository : IActivityRepository
    {
        ApplicationDbContext context;
        IPersonInviteRepository inviteContext;
        IPersonOrganizedActivityRepository organizedContext;

        public ActivityRepository(ApplicationDbContext context, IPersonInviteRepository inviteContext, IPersonOrganizedActivityRepository organizedContext)
        {
            this.context = context;
            this.inviteContext = inviteContext;
            this.organizedContext = organizedContext;
        }

        public async Task<Activity> AddActivityFromActivity(Activity activity)
        {
            context.Activities.Add(activity);

            await context.SaveChangesAsync();

            return activity;
        }

        public async Task AddActivityFromDTO(ActivityDTO NewActivityToAdd, Activity ActivityToAdd)
        {
            try
            {
                ActivityToAdd = (Activity)NewActivityToAdd;
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine(ex);
            }

            ActivityToAdd = await AddActivityFromActivity(ActivityToAdd);

            ActivityToAdd.InvitedGuests = await PersonInviteDtoListToPersonInviteList(NewActivityToAdd, ActivityToAdd.Id);

            ActivityToAdd.Organizers = await OrganizersDtoListToOrganizerList(NewActivityToAdd, ActivityToAdd.Id);

            await UpdateActivityFromActivity(ActivityToAdd);

            await context.SaveChangesAsync();
        }

        public async Task DeleteActivity(int Id)
        {
            var activityToDelete = await GetActivityById(Id);

            await inviteContext.DeleteInvitesByActivityId(activityToDelete.Id);

            await organizedContext.DeleteOrganizedActivitiesByActivityId(activityToDelete.Id);

            context.Activities.Remove(activityToDelete);
            await context.SaveChangesAsync();
        }

        public async Task<Activity> GetActivityById(int id)
        {
            var result = await context.Activities.FindAsync(id);
            return result;
        }

        public Task<IEnumerable<Activity>> GetListOfActivities()
        {
            IEnumerable<Activity> result = context.Activities;

            return Task.FromResult(result);
        }

        public Task<IEnumerable<Activity>> GetListOfActivitiesByName(string name)
        {
            IEnumerable<Activity> result = context.Activities.Where(a => a.ActivityName.Contains(name));

            return Task.FromResult(result);
        }

        public Task<IEnumerable<Activity>> GetListOfInvitedActivitiesByPersonId(string id)
        {
            IEnumerable<Activity> result = context.Activities.Where(p => p.InvitedGuests.Any(i => i.PersonId == id));

            return Task.FromResult(result);
        }

        public Task<IEnumerable<Activity>> GetListOfOrganizedActivitiesByPersonId(string id)
        {
            IEnumerable<Activity> result = context.Activities.Where(p => p.Organizers.Any(i => i.OrganizerId == id));

            return Task.FromResult(result);
        }

        public async Task UpdateActivityFromActivity(Activity activity)
        {
            context.Activities.Update(activity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateActivityFromDTO(int id, ActivityDTO value)
        {
            var savedActivity = await GetActivityById(id);

            if (value.InvitedGuests != null)
            {
                try
                {
                    savedActivity.InvitedGuests = await PersonInviteDtoListToPersonInviteList(value, savedActivity.Id);
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine(ex);
                }
            }

            if (value.Organizers != null)
            {
                try
                {
                    savedActivity.Organizers = await OrganizersDtoListToOrganizerList(value, savedActivity.Id);
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine(ex);
                }
            }

            context.Update(savedActivity);

            await context.SaveChangesAsync();
        }


        public async Task<IEnumerable<PersonOrganizedActivity>> OrganizersDtoListToOrganizerList(ActivityDTO value, int Id)
        {
            var Organizers = new List<PersonOrganizedActivity>();

            foreach (var personOrganizer in value.Organizers)
            {
                personOrganizer.ActivityId = Id;

                if (await organizedContext.GetOrganizedActivitiesByPersonIdAndActivityId(personOrganizer.PersonId, personOrganizer.ActivityId) != null)
                {
                    var OrganizerToUpdate = (PersonOrganizedActivity)personOrganizer;
                    await organizedContext.UpdateOrganizedActivities(OrganizerToUpdate);
                }
                else
                {
                    var OrganizerToAdd = (PersonOrganizedActivity)personOrganizer;
                    context.PersonOrginizers.Add(OrganizerToAdd);
                }

                Organizers.Add((PersonOrganizedActivity)personOrganizer);
            }

            return Organizers;
        }

        public async Task<IEnumerable<PersonInvites>> PersonInviteDtoListToPersonInviteList(ActivityDTO value, int Id)
        {
            var InvitedPeople = new List<PersonInvites>();

            foreach (var personInvite in value.InvitedGuests)
            {
                personInvite.ActivityId = Id;

                if (await inviteContext.GetInviteByPersonIdAndActivityId(personInvite.PersonId, personInvite.ActivityId) != null)
                {
                    var InviteToUpdate = (PersonInvites)personInvite;
                    await inviteContext.UpdateInvite(InviteToUpdate);
                }
                else
                {
                    var invite = (PersonInvites)personInvite;
                    context.PersonActivities.Add(invite);
                }

                InvitedPeople.Add((PersonInvites)personInvite);

            }

            return InvitedPeople;
        }

        public async Task<IEnumerable<Person>> GetInvitesdByActivityId(int id)
        {
            var result = context.People.Where(p => p.Invites.Any(a => a.ActivityId == id));
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<Person>> GetOrganizersByActivityid(int id)
        {
            var result = context.People.Where(p => p.OrganizedActivities.Any(a => a.OrganizedActivityId == id));
            return await Task.FromResult(result);
        }
    }
}
