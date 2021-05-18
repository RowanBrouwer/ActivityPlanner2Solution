using ActivityPlanner2.Data.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner2.Data
{
    public interface IPersonInviteRepository
    {
        Task<PersonInvites> GetActivityByPersonId(string id);
        Task<PersonInvites> GetActivityByActivityId(int id);
        Task AddActivity(PersonInvites NewActivityToAdd);
        Task UpdateActivity(PersonInvites updatedActivityData);
        Task DeleteActivity(int id);
    }
}
