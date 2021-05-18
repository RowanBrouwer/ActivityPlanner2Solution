using ActivityPlanner2.Data.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner2.Data
{
    public class PersonInviteRepository : IPersonInviteRepository
    {
        public Task AddActivity(PersonInvites NewActivityToAdd)
        {
            throw new NotImplementedException();
        }

        public Task DeleteActivity(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PersonInvites> GetActivityByActivityId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PersonInvites> GetActivityByPersonId(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateActivity(PersonInvites updatedActivityData)
        {
            throw new NotImplementedException();
        }
    }
}
