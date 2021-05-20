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

        public async Task AddInvite(PersonOrganizedActivity invite)
        {
            invite.OrganizedActivity = await activityRepository.GetActivityById(invite.OrganizedActivityId);
            invite.Organizer = await PersonContext.GetPersonById(invite.OrganizerId);

            DbContext.PersonOrginizers.Add(invite);
            DbContext.SaveChanges();
        }

        public Task DeleteInvite(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PersonOrganizedActivity> GetInviteByActivityId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PersonOrganizedActivity> GetInviteByPersonId(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateInvite(PersonOrganizedActivity invite)
        {
            throw new NotImplementedException();
        }
    }
}
