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

            DbContext.PersonActivities.Add(invite);
            DbContext.SaveChanges();
        }

        public Task DeleteInvite(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PersonInvites> GetInviteByActivityId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PersonInvites> GetInviteByPersonId(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateInvite(PersonInvites updatedActivityData)
        {
            throw new NotImplementedException();
        }
    }
}
