using ActivityPlanner2.Data.ServerModels;
using System;
using System.Collections.Generic;
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

        public async Task DeleteInviteByActivityId(int id)
        {
            var personInviteToDelete = await GetInviteByActivityId(id);
            DbContext.PersonActivities.Remove(personInviteToDelete);
        }

        public async Task DeleteInviteByPersonId(string id)
        {
            var personInviteToDelete = await GetInviteByPersonId(id);
            DbContext.PersonActivities.Remove(personInviteToDelete);
        }

        public async Task<PersonInvites> GetInviteByActivityId(int id)
        {
            var result = DbContext.PersonActivities.Where(i => i.ActivityId == id).FirstOrDefault();

            return await Task.FromResult(result);
        }

        public async Task<PersonInvites> GetInviteByPersonId(string id)
        {
            var result = DbContext.PersonActivities.Where(i => i.PersonId == id).FirstOrDefault();

            return await Task.FromResult(result);
        }

        public async Task UpdateInvite(PersonInvites updatedActivityData)
        {
            DbContext.PersonActivities.Update(updatedActivityData);
            await DbContext.SaveChangesAsync();
        }
    }
}
