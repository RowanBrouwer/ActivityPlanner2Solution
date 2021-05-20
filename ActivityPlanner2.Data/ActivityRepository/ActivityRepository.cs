using ActivityPlanner2.Data.ServerModels;
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
        public ActivityRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddActivity(Activity NewActivityToAdd)
        {
            context.Activities.Add(NewActivityToAdd);
            await context.SaveChangesAsync();
        }

        public async Task DeleteActivity(int Id)
        {
            var activityToDelete = await GetActivityById(Id);

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

        public Task<IEnumerable<Activity>> GetListOfActivitiesByPersonId(string id)
        {
            IEnumerable<Activity> result = context.Activities.Where(p => p.InvitedGuests.Any(i => i.PersonId == id));

            return Task.FromResult(result);
        }

        public async Task UpdateActivity(Activity updatedActivityData)
        {
            context.Activities.Update(updatedActivityData);
            await context.SaveChangesAsync();
        }
    }
}
