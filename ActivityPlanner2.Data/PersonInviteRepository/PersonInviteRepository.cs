using ActivityPlanner2.Data.ServerModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner2.Data
{
    public class PersonInviteRepository : IPersonInviteRepository
    {
        ApplicationDbContext DbContext;
        IPersonRepository PersonContext;
        IActivityRepository activityRepository;

        public PersonInviteRepository(ApplicationDbContext DbContext, IPersonRepository PersonContext, IActivityRepository activityRepository)
        {
            this.DbContext = DbContext;
            this.PersonContext = PersonContext;
            this.activityRepository = activityRepository;
        }
        public async Task AddInvite(PersonInvites invite)
        {
            invite.Activity = await activityRepository.GetActivityById(invite.ActivityId);
            invite.Person = await PersonContext.GetPersonById(invite.PersonId);

            await DbContext.PersonActivities.AddAsync(invite);
            DbContext.SaveChanges();
        }

        public async Task DeleteInviteByPersonIdAndActivityId(string personId, int activityId)
        {
            var personInviteToDelete = await GetInviteByPersonIdAndActivityId(personId, activityId);
            DbContext.PersonActivities.Remove(personInviteToDelete);

            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteInvitesByActivityId(int id)
        {
            var personInviteToDelete = await GetInvitesByActivityId(id);
            DbContext.PersonActivities.RemoveRange(personInviteToDelete);

            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteInvitesByPersonId(string id)
        {
            var personInviteToDelete = await GetInvitesByPersonId(id);
            DbContext.PersonActivities.RemoveRange(personInviteToDelete);

            await DbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PersonInvites>> GetInvitesByActivityId(int id)
        {
            var result = DbContext.PersonActivities.Where(i => i.ActivityId == id);

            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<PersonInvites>> GetInvitesByPersonId(string id)
        {
            var result = DbContext.PersonActivities.Where(i => i.PersonId == id);

            return await Task.FromResult(result);
        }

        public async Task<PersonInvites> GetInviteByPersonIdAndActivityId(string personId, int activityId)
        {
            var result = DbContext.PersonActivities.Where(i => i.ActivityId == activityId && i.PersonId == personId).FirstOrDefault();

            return await Task.FromResult(result);
        }

        public async Task UpdateInvite(PersonInvites updatedActivityData)
        {
            var InviteToUpdate = DbContext.PersonActivities.Find(updatedActivityData.PersonId, updatedActivityData.ActivityId);

            InviteToUpdate.Accepted = updatedActivityData.Accepted;
            InviteToUpdate.Activity = await activityRepository.GetActivityById(updatedActivityData.ActivityId);
            InviteToUpdate.Person = await PersonContext.GetPersonById(updatedActivityData.PersonId);

            DbContext.PersonActivities.Update(InviteToUpdate);
            await DbContext.SaveChangesAsync();
        }
    }
}
