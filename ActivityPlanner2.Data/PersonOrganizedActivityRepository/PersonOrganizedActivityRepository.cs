using ActivityPlanner2.Data.ServerModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner2.Data
{
    public class PersonOrganizedActivityRepository : IPersonOrganizedActivityRepository
    {
        ApplicationDbContext DbContext;
        IPersonRepository PersonContext;
        IActivityRepository activityRepository;

        public PersonOrganizedActivityRepository(ApplicationDbContext DbContext, IPersonRepository PersonContext, IActivityRepository activityRepository)
        {
            this.DbContext = DbContext;
            this.PersonContext = PersonContext;
            this.activityRepository = activityRepository;
        }

        public async Task AddOrganizedActivities(PersonOrganizedActivity invite)
        {
            invite.OrganizedActivity = await activityRepository.GetActivityById(invite.OrganizedActivityId);
            invite.Organizer = await PersonContext.GetPersonById(invite.OrganizerId);

            await DbContext.PersonOrginizers.AddAsync(invite);
            DbContext.SaveChanges();
        }

        public async Task DeleteOrganizedActivitiesByPersonIdAndActivityId(string personId, int activityId)
        {
            var personInviteToDelete = await GetOrganizedActivitiesByPersonIdAndActivityId(personId, activityId);
            DbContext.PersonOrginizers.Remove(personInviteToDelete);

            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteOrganizedActivitiesByActivityId(int id)
        {
            var personInviteToDelete = await GetOrganizedActivitiesByActivityId(id);
            DbContext.PersonOrginizers.RemoveRange(personInviteToDelete);

            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteOrganizedActivitiesByPersonId(string id)
        {
            var personInviteToDelete = await GetOrganizedActivitiesByPersonId(id);
            DbContext.PersonOrginizers.RemoveRange(personInviteToDelete);

            await DbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PersonOrganizedActivity>> GetOrganizedActivitiesByActivityId(int id)
        {
            var result = DbContext.PersonOrginizers.Where(i => i.OrganizedActivityId == id);

            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<PersonOrganizedActivity>> GetOrganizedActivitiesByPersonId(string id)
        {
            var result = DbContext.PersonOrginizers.Where(i => i.OrganizerId == id);

            return await Task.FromResult(result);
        }

        public async Task<PersonOrganizedActivity> GetOrganizedActivitiesByPersonIdAndActivityId(string personId, int activityId)
        {
            var result = DbContext.PersonOrginizers.Where(i => i.OrganizedActivityId == activityId && i.OrganizerId == personId).FirstOrDefault();

            return await Task.FromResult(result);
        }

        public async Task UpdateOrganizedActivities(PersonOrganizedActivity updatedActivityData)
        {
            DbContext.PersonOrginizers.Update(updatedActivityData);
            await DbContext.SaveChangesAsync();
        }
    }
}
