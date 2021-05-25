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
        Task<IEnumerable<PersonInvites>> GetInvitesByPersonId(string id);
        Task<PersonInvites> GetInviteByPersonIdAndActivityId(string personId, int activityId);
        Task<IEnumerable<PersonInvites>> GetInvitesByActivityId(int id);
        Task AddInvite(PersonInvites Invite);
        Task UpdateInvite(PersonInvites Invite);
        Task DeleteInvitesByActivityId(int id);
        Task DeleteInvitesByPersonId(string id);
        Task DeleteInviteByPersonIdAndActivityId(string personId, int activityId);
    }
}
