using ActivityPlanner2.Data.ServerModels;
using ActivityPlanner2.Shared.DTOs;
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
        IActivityLogic Logic;
        IPersonInviteRepository inviteContext;
        IPersonOrganizedActivityRepository organizedContext;

        public ActivityRepository(ApplicationDbContext context, IActivityLogic Logic, IPersonInviteRepository inviteContext, IPersonOrganizedActivityRepository organizedContext)
        {
            this.context = context;
            this.Logic = Logic;
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

            ActivityToAdd.InvitedGuests = await Logic.PersonInviteDtoListToPersonInviteList(NewActivityToAdd, ActivityToAdd.Id);

            ActivityToAdd.Organizers = await Logic.OrganizersDtoListToOrganizerList(NewActivityToAdd, ActivityToAdd.Id);

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
                    savedActivity.InvitedGuests = await Logic.PersonInviteDtoListToPersonInviteList(value, value.Id);
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
                    savedActivity.Organizers = await Logic.OrganizersDtoListToOrganizerList(value, value.Id);
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine(ex);
                }
            }

            context.Update(savedActivity);

            await context.SaveChangesAsync();
        }
    }
}
